using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests
{
    public class StartProcessReq : BasicReq
    {
        public string FileName;
        public string Arguments;
        public bool UseShellExecute;
        public bool CreateNoWindow;
    }
}
