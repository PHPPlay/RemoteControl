using System;

namespace iWay.RemoteControlClient.RemoteConsole
{
    public class OnOutputReceivedArgs : EventArgs
    {
        public string ReceivedOutput
        {
            get;
            set;
        }
    }
}
