using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using iWay.RemoteControlBase.Utilities;

namespace iWay.RemoteControlClient.Connect
{
    public class Connector
    {
        #region Properties

        private string mHost;
        private int mPort;
        private string mPassword;
        private int mConnectType;

        private Socket mSocket;
        private TripleDESCryptoServiceProvider mCryptoServiceProvider;

        public Connector(string host, int port, string password, int connectType)
        {
            mHost = host;
            mPort = port;
            mPassword = password;
            mConnectType = connectType;
        }

        public string Host
        {
            get
            {
                return mHost;
            }
        }

        public int Port
        {
            get
            {
                return mPort;
            }
        }

        public string Password
        {
            get
            {
                return mPassword;
            }
        }

        public int ConnectType
        {
            get
            {
                return mConnectType;
            }
        }

        public Socket Socket
        {
            get
            {
                return mSocket;
            }
        }

        public TripleDESCryptoServiceProvider TDESProvider
        {
            get
            {
                return mCryptoServiceProvider;
            }
        }

        public ICryptoTransform Encryptor
        {
            get
            {
                if (mCryptoServiceProvider == null)
                {
                    return null;
                }
                else
                {
                    return mCryptoServiceProvider.CreateEncryptor();
                }
            }
        }

        public ICryptoTransform Decryptor
        {
            get
            {
                if (mCryptoServiceProvider == null)
                {
                    return null;
                }
                else
                {
                    return mCryptoServiceProvider.CreateDecryptor();
                }
            }
        }

        #endregion

        #region Connect

        private Thread mConnectThread;
        private Form mForm;
        private EventHandler<ConnectSucceedArgs> mConnectSucceedHandler;
        private EventHandler<ConnectFailArgs> mConnectFailHandler;

        public void SetForm(Form form)
        {
            mForm = form;
        }

        public void SetConnectSucceedHandler(EventHandler<ConnectSucceedArgs> handler)
        {
            mConnectSucceedHandler = handler;
        }

        public void SetConnectFailHandler(EventHandler<ConnectFailArgs> handler)
        {
            mConnectFailHandler = handler;
        }

        private void Connect()
        {
            try
            {  
                byte[] dataBuffer = new byte[1024];
                int readCount = 0;

                mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                mSocket.Connect(mHost, mPort);
                RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
                readCount = Socket.Receive(dataBuffer);
                string cryptoProviderPublicKey = Encoding.ASCII.GetString(dataBuffer, 0, readCount);
                rsaProvider.FromXmlString(cryptoProviderPublicKey);

                byte[] passwordData = Encoding.ASCII.GetBytes(mPassword);
                byte[] passwordDataEncrypted = rsaProvider.Encrypt(passwordData, true);
                mSocket.Send(passwordDataEncrypted);

                readCount = mSocket.Receive(dataBuffer);
                string result = Encoding.ASCII.GetString(dataBuffer, 0, readCount);
                if (result != "OK")
                {
                    InvalidPasswordException exception = new InvalidPasswordException();
                    exception.Password = mPassword;
                    exception.ServerMessage = result;
                    throw exception;
                }

                mCryptoServiceProvider = new TripleDESCryptoServiceProvider();
                byte[] keyData = mCryptoServiceProvider.Key;
                byte[] ivData = mCryptoServiceProvider.IV;
                byte[] typeData = BitConverter.GetBytes(mConnectType);
                byte[] connectData = BytesUtils.Merge(keyData, ivData, typeData);
                byte[] connectDataEncrypted = rsaProvider.Encrypt(connectData, true);
                mSocket.Send(connectDataEncrypted);

                readCount = mSocket.Receive(dataBuffer);
                result = Encoding.ASCII.GetString(dataBuffer, 0, readCount);
                if (result != "OK")
                {
                    InvalidConnectInfoException exception = new InvalidConnectInfoException();
                    exception.ServerMessage = result;
                    throw exception;
                }

                ConnectSucceedArgs args = new ConnectSucceedArgs();
                mForm.Invoke(mConnectSucceedHandler, this, args);
            }
            catch (Exception ex)
            {
                mSocket.Close();
                mSocket = null;
                mCryptoServiceProvider = null;

                ConnectFailArgs args = new ConnectFailArgs();
                args.Reason = ex;
                mForm.Invoke(mConnectFailHandler, this, args);
            }
        }

        public void BeginConnect()
        {
            if (mConnectThread != null)
            {
                throw new InvalidOperationException("Connecting");
            }
            mConnectThread = new Thread(Connect);
            mConnectThread.IsBackground = true;
            mConnectThread.Start();
        }

        public void CancleConnect()
        {
            if (mConnectThread != null)
            {
                mConnectThread.Interrupt();
            }
        }

        #endregion
    }
}
