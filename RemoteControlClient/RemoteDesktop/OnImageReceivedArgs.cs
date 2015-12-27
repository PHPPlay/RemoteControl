using System;
using System.Drawing;

namespace iWay.RemoteControlClient.RemoteDesktop
{
    public class OnImageReceivedArgs : EventArgs
    {
        public Image ReceivedImage
        {
            get;
            set;
        }
    }
}
