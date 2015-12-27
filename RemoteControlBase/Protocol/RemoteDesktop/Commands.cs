using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteDesktop
{
    public static class Commands
    {
        public const int MouseMove = 0;
        public const int MouseDown = 1;
        public const int MouseUp = 2;
        public const int KeyboardDown = 3;
        public const int KeyboardUp = 4;
        public const int ImageQualityChange = 5;
        public const int RefreshSpanChange = 6;
    }
}
