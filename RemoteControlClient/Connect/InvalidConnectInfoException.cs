using System;

namespace iWay.RemoteControlClient.Connect
{
    public class InvalidConnectInfoException : Exception
    {
        public string ServerMessage
        {
            get;
            set;
        }
    }
}
