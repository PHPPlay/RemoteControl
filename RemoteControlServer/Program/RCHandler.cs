using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using iWay.RemoteControlBase;
using iWay.RemoteControlBase.Network;
using iWay.RemoteControlBase.Utilities;
using iWay.RemoteControlServer.Program.Exceptions;
using iWay.RemoteControlServer.Program.Servers;

namespace iWay.RemoteControlServer.Program
{
    public class RCHandler
    {
        private Socket mSocket;
        private string mPassword;
        private Thread mHandleThread;
        private RCServer mRCServer;

        public RCHandler(Socket socket, string password)
        {
            mSocket = socket;
            mPassword = password;
        }

        private byte[] ReceiveAndDecryptData(RSACryptoServiceProvider rsaProvider)
        {
            byte[] dataBuffer = new byte[1024];
            int dataCount = mSocket.Receive(dataBuffer);
            byte[] dataEncrypted = BytesUtils.GetRange(dataBuffer, 0, dataCount);
            byte[] dataDecrypted = rsaProvider.Decrypt(dataEncrypted, true);
            return dataDecrypted;
        }

        private void Handle()
        {
            try
            {
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                string mCryptoProviderPublicKey = rsaProvider.ToXmlString(false);
                byte[] keyData = Encoding.ASCII.GetBytes(mCryptoProviderPublicKey);
                mSocket.Send(keyData);

                byte[] passwordDataDecrypted = ReceiveAndDecryptData(rsaProvider);
                string password = Encoding.ASCII.GetString(passwordDataDecrypted);

                byte[] okData = Encoding.ASCII.GetBytes(Consts.SERVER_OK);
                byte[] wrongPasswordData = Encoding.ASCII.GetBytes(Consts.SERVER_WRONG_PASSWORD);
                byte[] wrongConnectInfoData = Encoding.ASCII.GetBytes(Consts.SERVER_INVALID_CONNECT_INFO);

                if (password == mPassword)
                {
                    mSocket.Send(okData);
                }
                else
                {
                    mSocket.Send(wrongPasswordData);
                    throw new WrongPasswordException();
                }

                byte[] connectInfoData = ReceiveAndDecryptData(rsaProvider);
                TripleDESCryptoServiceProvider tdesProvider;
                tdesProvider = new TripleDESCryptoServiceProvider();
                tdesProvider.Key = BytesUtils.GetRange(connectInfoData, 0, 24);
                tdesProvider.IV = BytesUtils.GetRange(connectInfoData, 24, 8);
                int connectType = BitConverter.ToInt32(connectInfoData, 32);
                switch (connectType)
                {
                    case ConnectType.TYPE_REMOTE_CONSOLE:
                        mSocket.Send(okData);
                        mRCServer = new RemoteConsoleServer(mSocket, tdesProvider);
                        break;
                    case ConnectType.TYPE_REMOTE_DESKTOP:
                        mSocket.Send(okData);
                        mRCServer = new RemoteDesktopServer(mSocket);
                        break;
                    case ConnectType.TYPE_REMOTE_EXPLORER:
                        mSocket.Send(okData);
                        mRCServer = new RemoteExplorerServer(mSocket);
                        break;
                    default:
                        mSocket.Send(wrongConnectInfoData);
                        throw new InvalidConnectTypeException();
                }
                mRCServer.BeginService();
            }
            catch
            {
                mSocket.Close();
            }
        }

        public void BeginHandle()
        {
            mHandleThread = new Thread(Handle);
            mHandleThread.IsBackground = true;
            mHandleThread.Start();
        }
    }
}
