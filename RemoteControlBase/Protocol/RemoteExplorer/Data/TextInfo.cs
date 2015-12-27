using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer.Data
{
    public class TextInfo
    {
        public string Name;
        public string Value;

        public TextInfo()
        {
        }

        public TextInfo(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
