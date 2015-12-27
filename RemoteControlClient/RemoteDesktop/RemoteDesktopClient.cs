using System;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using iWay.RemoteControlBase.Protocol.RemoteDesktop;
using iWay.RemoteControlClient.Program;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlBase.Utilities;

namespace iWay.RemoteControlClient.RemoteDesktop
{
    public class RemoteDesktopClient : RCClient
    {
        public RemoteDesktopClient(Connector connector)
            : base(connector.Socket)
        {
        }

        private EventHandler<OnImageReceivedArgs> mOnImageReceivedHandler;

        public void setOnImageReceivedHandler(EventHandler<OnImageReceivedArgs> handler)
        {
            mOnImageReceivedHandler = handler;
        }

        public void SendMouseMove(float xPercent, float yPercent)
        {
            try
            {
                mSocketTalker.SendInt(Commands.MouseMove);
                mSocketTalker.SendFloat(xPercent);
                mSocketTalker.SendFloat(yPercent);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendMouseDown(int button, float xPercent, float yPercent)
        {
            try
            {
                mSocketTalker.SendInt(Commands.MouseDown);
                mSocketTalker.SendInt(button);
                mSocketTalker.SendFloat(xPercent);
                mSocketTalker.SendFloat(yPercent);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendMouseUp(int button, float xPercent, float yPercent)
        {
            try
            {
                mSocketTalker.SendInt(Commands.MouseUp);
                mSocketTalker.SendInt(button);
                mSocketTalker.SendFloat(xPercent);
                mSocketTalker.SendFloat(yPercent);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendKeyboardDown(int key)
        {
            try
            {
                mSocketTalker.SendInt(Commands.KeyboardDown);
                mSocketTalker.SendInt(key);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendKeyboardUp(int key)
        {
            try
            {
                mSocketTalker.SendInt(Commands.MouseUp);
                mSocketTalker.SendInt(key);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendImageQualityChange(float imageQuality)
        {
            try
            {
                mSocketTalker.SendInt(Commands.ImageQualityChange);
                mSocketTalker.SendFloat(imageQuality);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void SendRefreshSpanChange(int refreshSpan)
        {
            try
            {
                mSocketTalker.SendInt(Commands.RefreshSpanChange);
                mSocketTalker.SendInt(refreshSpan);
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        private Thread mReceiveImageThread;

        private void ReceiveImage()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    byte[] packet = mSocketTalker.ReceivePacket();
                    Image image = BitmapUtils.ConvertFromBytes(packet);
                    OnImageReceivedArgs args = new OnImageReceivedArgs();
                    args.ReceivedImage = image;
                    mForm.Invoke(mOnImageReceivedHandler, this, args);
                }
            }
            catch (Exception e)
            {
                Close(e);
            }
        }

        public void BeginReceiveOutput()
        {
            mReceiveImageThread = new Thread(ReceiveImage);
            mReceiveImageThread.IsBackground = true;
            mReceiveImageThread.Start();
        }
    }
}
