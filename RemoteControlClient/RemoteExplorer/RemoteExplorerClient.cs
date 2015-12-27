using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.Program;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;

namespace iWay.RemoteControlClient.RemoteExplorer
{
    public class RemoteExplorerClient : RCClient
    {
        public RemoteExplorerClient(Connector connector)
            : base(connector.Socket)
        {
        }

        private EventHandler<OnResponseReceivedArgs> mOnResponseReceivedHandler;

        public void setOnResponseReceivedHandler(EventHandler<OnResponseReceivedArgs> handler)
        {
            mOnResponseReceivedHandler = handler;
        }

        public void SendRequest(int protocalType, BasicReq req)
        {
            mSocketTalker.SendInt(protocalType);
            mSocketTalker.SendObject(req);
        }

        private Thread mReceiveResponseThread;
        private bool mIsOnlyOnce;

        public void ReceiveResponse()
        {
            try
            {
                do
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    int protocalType = mSocketTalker.ReceiveInt();
                    string responseJSON = mSocketTalker.ReceiveString();
                    OnResponseReceivedArgs args = new OnResponseReceivedArgs();
                    args.ResponseType = protocalType;
                    args.ResponseJSON = responseJSON;
                    mForm.Invoke(mOnResponseReceivedHandler, this, args);
                } while (mIsOnlyOnce == false);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void BeginReceiveResponse(bool once)
        {
            mIsOnlyOnce = once;
            mReceiveResponseThread = new Thread(ReceiveResponse);
            mReceiveResponseThread.IsBackground = true;
            mReceiveResponseThread.Start();
        }

        public void BeginReceiveResponse()
        {
            BeginReceiveResponse(false);
        }
    }
}
