using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace iWay.RemoteControlServer.Program
{
    public class RCListener
    {
        private int mListenPort;
        private string mPassword;
        private Socket mListenSocket;
        private Thread mClientAcceptThread;
        private List<RCHandler> mHandlers;

        public RCListener(int listenPort, string password)
        {
            mListenPort = listenPort;
            mPassword = password;
            mHandlers = new List<RCHandler>();
        }

        private void AcceptClient()
        {
            while (true)
            {
                Socket socket = mListenSocket.Accept();
                RCHandler handler = new RCHandler(socket, mPassword);
                mHandlers.Add(handler);
                handler.BeginHandle();
            }
        }

        public void Start()
        {
            mListenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mListenSocket.Bind(new IPEndPoint(IPAddress.Any, mListenPort));
            mListenSocket.Listen(256);

            mClientAcceptThread = new Thread(AcceptClient);
            mClientAcceptThread.Start();
        }
    }
}