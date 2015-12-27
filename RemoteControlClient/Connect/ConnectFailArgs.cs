using System;

namespace iWay.RemoteControlClient.Connect
{
    public class ConnectFailArgs : EventArgs
    {
        public Exception Reason
        {
            get;
            set;
        }

        public object Extra
        {
            get;
            set;
        }
    }
}
