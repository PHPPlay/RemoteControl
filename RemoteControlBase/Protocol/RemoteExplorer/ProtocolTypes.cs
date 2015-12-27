using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer
{
    public static class ProtocolTypes
    {
        public const int TYPE_LIST_CONTENTS = 0;
        public const int TYPE_LIST_ALL_FILES = 1;
        public const int TYPE_LIST_ALL_DIRECTORIES = 2;
        public const int TYPE_DELETE_CONTENTS = 3;
        public const int TYPE_RENAME_CONTENT = 4;
        public const int TYPE_CREATE_DIRECTORY = 5;
        public const int TYPE_START_PROCESS = 6;
        public const int TYPE_GET_CONTENT_INFO = 7;
        public const int TYPE_MOVE_CONTENTS = 8;
        public const int TYPE_COPY_CONTENTS = 9;
        public const int TYPE_GET_CONTENT = 10;
        public const int TYPE_PUT_CONTENT = 11;
    }
}
