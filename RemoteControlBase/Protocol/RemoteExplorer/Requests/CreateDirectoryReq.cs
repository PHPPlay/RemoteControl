using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests
{
    public class CreateDirectoryReq : BasicReq
    {
        public string Container;

        public String Name;

        public bool IgnoreAlreadyExisted;
    }
}
