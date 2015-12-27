using System;

namespace iWay.RemoteControlServer.Program.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public string CorrectPassword
        {
            get;
            set;
        }

        public string ProvidedPassword
        {
            get;
            set;
        }
    }
}
