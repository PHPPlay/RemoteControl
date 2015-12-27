using System;
using iWay.RemoteControlClient.Connect;
using System.Windows.Forms;
using System.Threading;
using iWay.RemoteControlClient.Program;

namespace iWay.RemoteControlClient.RemoteConsole
{
    public class RemoteConsoleClient : RCClient
    {
        public RemoteConsoleClient(Connector connector)
            : base(connector.Socket, connector.TDESProvider) 
        {
        }

        private EventHandler<OnOutputReceivedArgs> mOnOutputReceivedHandler;

        public void SetOnOutputReceivedHandler(EventHandler<OnOutputReceivedArgs> handler)
        {
            mOnOutputReceivedHandler = handler;
        }

        public void SendInput(string text)
        {
            try
            {
                if (text.EndsWith("\r\n"))
                {
                    mSocketTalker.SendString(text);
                }
                else
                {
                    mSocketTalker.SendString(text + "\r\n");
                }
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        private Thread mReceiveOutputThread;

        private void ReceiveOutput()
        {
            try
            {
                while(true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    OnOutputReceivedArgs args = new OnOutputReceivedArgs();
                    args.ReceivedOutput = mSocketTalker.ReceiveString();
                    mForm.Invoke(mOnOutputReceivedHandler, this, args);
                }
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void BeginReceiveOutput()
        {
            mReceiveOutputThread = new Thread(ReceiveOutput);
            mReceiveOutputThread.IsBackground = true;
            mReceiveOutputThread.Start();
        }
    }
}
