using System.Windows.Forms;
using iWay.RemoteControlBase;

namespace iWay.RemoteControlServer.Program
{
    static class Program
    {

        static RCListener mRCListener;
        static IPNotifier mIPNotifier;

        static void Main(string[] args)
        {
            string password;
            string mailServer;
            string mailAccount;
            string mailPassword;
            string mailSender;
            string mailReceiver;

            switch (args.Length)
            {
                case 1:
                    password = args[0];

                    mRCListener = new RCListener(Consts.SERVER_LISTEN_PORT, password);
                    mRCListener.Start();
                    break;
                case 6:
                    password = args[0];
                    mailServer = args[1];
                    mailAccount = args[2];
                    mailPassword = args[3];
                    mailSender = args[4];
                    mailReceiver = args[5];

                    mRCListener = new RCListener(Consts.SERVER_LISTEN_PORT, password);
                    mRCListener.Start();
                    mIPNotifier = new IPNotifier(mailServer, mailAccount, mailPassword, mailSender, mailReceiver);
                    mIPNotifier.Start();
                    break;
                default:
                    MessageBox.Show("远程控制被控端的启动参数不正确。");
                    break;
            }
        }
    }
}
