using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Data;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses
{
    public class GetContentInfoRes : BasicRes
    {
        public List<TextInfo> InfoList;
    }
}
