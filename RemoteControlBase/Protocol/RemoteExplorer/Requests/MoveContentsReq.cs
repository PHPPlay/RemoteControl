using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests
{
    public class MoveContentsReq : BasicReq
    {
        public List<string> ContentPaths;

        public string Container;
    }
}
