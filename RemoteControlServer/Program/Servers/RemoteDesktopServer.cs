using System.Drawing;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using iWay.RemoteControlBase.Utilities;
using iWay.RemoteControlBase.Protocol.RemoteDesktop;

namespace iWay.RemoteControlServer.Program.Servers
{
    public class RemoteDesktopServer : RCServer
    {
        public RemoteDesktopServer(Socket socket)
            :base(socket)
        {
        }

        private Thread mSendImageThread;
        private Thread mReceiveEventsThread;

        public override void BeginService()
        {
            mSendImageThread = new Thread(SendImage);
            mSendImageThread.IsBackground = true;
            mSendImageThread.Start();
            mReceiveEventsThread = new Thread(ReceiveEvents);
            mReceiveEventsThread.IsBackground = true;
            mReceiveEventsThread.Start();
        }

        private float imageQuality = 0.5f;
        private int refreshSpan = 1000;

        private void SendImage()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    Bitmap bmp = BitmapUtils.CopyFromScreen();
                    byte[] dat = BitmapUtils.ConvertToBytes(bmp, imageQuality);
                    mSocketTalker.SendPacket(dat);
                    Thread.Sleep(refreshSpan);
                }
            }
            catch
            {
                Close();
            }
        }

        private void ReceiveEvents()
        {
            try
            {
                while (true)
                {
                    if (IsClosed)
                    {
                        return;
                    }
                    int cmd = mSocketTalker.ReceiveInt();
                    switch (cmd)
                    {
                        case Commands.MouseMove:
                            {
                                float x_proportion = mSocketTalker.ReceiveFloat();
                                float y_proportion = mSocketTalker.ReceiveFloat();
                                int x_position = (int)(x_proportion * Screen.PrimaryScreen.Bounds.Width);
                                int y_position = (int)(y_proportion * Screen.PrimaryScreen.Bounds.Height);
                                InputSimulator.SetCursorPosition(x_position, y_position);
                            }
                            break;
                        case Commands.MouseDown:
                            {
                                int button_id = mSocketTalker.ReceiveInt();
                                float x_proportion = mSocketTalker.ReceiveFloat();
                                float y_proportion = mSocketTalker.ReceiveFloat();
                                int x_position = (int)(x_proportion * Screen.PrimaryScreen.Bounds.Width);
                                int y_position = (int)(y_proportion * Screen.PrimaryScreen.Bounds.Height);
                                InputSimulator.CreateMouseDown(button_id, x_position, y_position);
                            }
                            break;
                        case Commands.MouseUp:
                            {
                                int button_id = mSocketTalker.ReceiveInt();
                                float x_proportion = mSocketTalker.ReceiveFloat();
                                float y_proportion = mSocketTalker.ReceiveFloat();
                                int x_position = (int)(x_proportion * Screen.PrimaryScreen.Bounds.Width);
                                int y_position = (int)(y_proportion * Screen.PrimaryScreen.Bounds.Height);
                                InputSimulator.CreateMouseUp(button_id, x_position, y_position);
                            }
                            break;
                        case Commands.KeyboardDown:
                            {
                                int key = mSocketTalker.ReceiveInt();
                                InputSimulator.CreateKeyboardDown((byte)key);
                            }
                            break;
                        case Commands.KeyboardUp:
                            {
                                int key = mSocketTalker.ReceiveInt();
                                InputSimulator.CreateKeyboardUp((byte)key);
                            }
                            break;
                        case Commands.ImageQualityChange:
                            {
                                imageQuality = mSocketTalker.ReceiveFloat();
                            }
                            break;
                        case Commands.RefreshSpanChange:
                            {
                                refreshSpan = mSocketTalker.ReceiveInt();
                            }
                            break;
                    }
                }
            }
            catch
            {
                Close();
            }
        }
    }
}
