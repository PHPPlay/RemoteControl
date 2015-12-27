using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions
{
    public class KnownException : Exception
    {
        public KnownException(string message)
            : base(message)
        {
        }
    }
}
