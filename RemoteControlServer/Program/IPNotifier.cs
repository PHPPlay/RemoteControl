using System.Threading;
using System.Net.Sockets;
using iWay.RemoteControlBase.Utilities;

namespace iWay.RemoteControlServer.Program
{
    public class IPNotifier
    {
        private string mMailServer;
        private string mMailAccount;
        private string mMailPassword;
        private string mMailSender;
        private string mMailReceiver;

        public IPNotifier(params string[] args)
        {
            mMailServer = args[0];
            mMailAccount = args[1];
            mMailPassword = args[2];
            mMailSender = args[3];
            mMailReceiver = args[4];
        }

        private string mSendedIPAddress;
        private Thread mCheckIPThread;

        private string GetCurrentIPAddress()
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect("www.iway-server.com", 45367);
                byte[] buffer = new byte[32];
                int count = socket.Receive(buffer);
                socket.Close();
                if (count != 4)
                {
                    return null;
                }
                else
                {
                    return buffer[0] + "." + buffer[1] + "." + buffer[2] + "." + buffer[3];
                }
            }
            catch
            {
                return null;
            }
        }

        private void CheckIPAddress()
        {
            while (true)
            {
                string ip = GetCurrentIPAddress();
                if (ip != mSendedIPAddress)
                {
                    try
                    {
                        MailSender mailSender = new MailSender();
                        mailSender.SetServerInfo(mMailServer);
                        mailSender.SetLoginInfo(mMailAccount, mMailPassword);
                        mailSender.SetReceivers(mMailReceiver);
                        mailSender.SetContent("IP Address", ip);
                        mailSender.Send();
                        mSendedIPAddress = ip;
                    }
                    catch
                    {
                    }
                }
                Thread.Sleep(1 * 60 * 60 * 1000);
            }
        }

        public void Start()
        {
            mCheckIPThread = new Thread(CheckIPAddress);
            mCheckIPThread.IsBackground = true;
            mCheckIPThread.Start();
        }
    }
}