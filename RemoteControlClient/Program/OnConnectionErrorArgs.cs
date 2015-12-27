using System;

namespace iWay.RemoteControlClient.Program
{
    public class OnConnectionErrorArgs : EventArgs
    {
        public Exception Error
        {
            get;
            set;
        }
    }
}
