using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests
{
    public class RenameContentReq : BasicReq
    {
        public string ContentPath;

        public string NewContentName;
    }
}
