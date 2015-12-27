using iWay.RemoteControlBase.Network.SocketTalker;
using System.Security.Cryptography;
using System.Net.Sockets;
using System;
using System.Windows.Forms;

namespace iWay.RemoteControlClient.Program
{
    public abstract class RCClient
    {
        protected SocketTalker mSocketTalker;
        protected Form mForm;
        protected EventHandler<OnConnectionErrorArgs> mOnConnectionErrorHandler;

        public RCClient(Socket socket, TripleDESCryptoServiceProvider tdesProvider)
        {
            ICryptoTransform encryptor = tdesProvider.CreateEncryptor();
            ICryptoTransform decryptor = tdesProvider.CreateDecryptor();
            mSocketTalker = new SocketTalker(socket, encryptor, decryptor);
        }

        public RCClient(Socket socket)
        {
            mSocketTalker = new SocketTalker(socket);
        }

        public void SetForm(Form form)
        {
            mForm = form;
        }

        public void SetOnConnectionErrorHandler(EventHandler<OnConnectionErrorArgs> handler)
        {
            mOnConnectionErrorHandler = handler;
        }

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

        public void Close(Exception e)
        {
            lock (mCloseLock)
            {
                if (mIsClosed)
                {
                    return;
                }
                mSocketTalker.Close();
                CloseCreatedResources();
                if (mForm != null && mOnConnectionErrorHandler != null)
                {
                    OnConnectionErrorArgs args = new OnConnectionErrorArgs();
                    args.Error = e;
                    mForm.Invoke(mOnConnectionErrorHandler, this, args);
                }
                mIsClosed = true;
            }
        }
    }
}
