using System;

namespace iWay.RemoteControlServer.Program.Exceptions
{
    public class InvalidConnectTypeException : Exception
    {
        public string Reason
        {
            get;
            set;
        }
    }
}
