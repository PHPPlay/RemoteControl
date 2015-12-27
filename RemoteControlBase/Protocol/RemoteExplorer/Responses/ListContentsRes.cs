using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses
{
    public class ListContentsRes : BasicRes
    {
        public string[] Drivers;

        public string[] Directories;

        public string[] Files;
    }
}
