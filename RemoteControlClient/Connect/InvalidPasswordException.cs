using System;

namespace iWay.RemoteControlClient.Connect
{
    public class InvalidPasswordException : Exception
    {
        public string Password
        {
            get;
            set;
        }

        public string ServerMessage
        {
            get;
            set;
        }
    }
}
