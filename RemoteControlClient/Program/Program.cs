using System;
using System.Windows.Forms;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.RemoteConsole;
using iWay.RemoteControlClient.RemoteDesktop;
using iWay.RemoteControlClient.RemoteExplorer;
using iWay.RemoteControlBase.Network;

namespace iWay.RemoteControlClient.Program
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ConnectWindow connectWindow = new ConnectWindow();
            Application.Run(connectWindow);

            if (connectWindow.IsConnected)
            {
                switch (connectWindow.Connector.ConnectType)
                {
                    case ConnectType.TYPE_REMOTE_CONSOLE:
                        Application.Run(new RemoteConsoleWindow(connectWindow.Connector));
                        break;
                    case ConnectType.TYPE_REMOTE_DESKTOP:
                        Application.Run(new RemoteDesktopWindow(connectWindow.Connector));
                        break;
                    case ConnectType.TYPE_REMOTE_EXPLORER:
                        Application.Run(new RemoteExplorerWindow(connectWindow.Connector));
                        break;
                }
            }
        }
    }
}
