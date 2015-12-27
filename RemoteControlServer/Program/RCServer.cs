using System.Net.Sockets;
using iWay.RemoteControlBase.Network.SocketTalker;
using System.Security.Cryptography;

namespace iWay.RemoteControlServer.Program
{
    public abstract class RCServer
    {
        protected SocketTalker mSocketTalker;

        public RCServer(Socket socket, TripleDESCryptoServiceProvider tdesProvider)
        {
            ICryptoTransform encryptor = tdesProvider.CreateEncryptor();
            ICryptoTransform decryptor = tdesProvider.CreateDecryptor();
            mSocketTalker = new SocketTalker(socket, encryptor, decryptor);
        }

        public RCServer(Socket socket)
        {
            mSocketTalker = new SocketTalker(socket);
        }

        public abstract void BeginService();

        private object mCloseLock = new object();
        private bool mIsClosed = false;

        public bool IsClosed
        {
            get
            {
                return mIsClosed;
            }
        }

        protected virtual void CloseCreatedResources()
        {
        }

        protected void Close()
        {
            lock (mCloseLock)
            {
                if (mIsClosed)
                {
                    return;
                }
                mSocketTalker.Close();
                CloseCreatedResources();
                mIsClosed = true;
            }
        }
    }
}
