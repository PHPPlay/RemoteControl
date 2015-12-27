using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace iWay.RemoteControlBase.Protocol.RemoteExplorer
{
    public class Content
    {
        public const int TYPE_NOT_FOUND = 0;
        public const int TYPE_DRIVER = 1;
        public const int TYPE_FILE = 2;
        public const int TYPE_DIRECTORY = 3;

        private int mType;
        private string mPath;

        public Content(string path)
        {
            if (File.Exists(path))
            {
                mType = TYPE_FILE;
                mPath = path;
                if (mPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                {
                    mPath = mPath.Substring(0, mPath.Length - 1);
                }
                return;
            }
            if (Directory.Exists(path))
            {
                mPath = path;
                if (mPath.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()) == false)
                {
                    mPath += System.IO.Path.DirectorySeparatorChar;
                }
                if (mPath.Length == 3)
                {
                    mType = TYPE_DRIVER;
                }
                else
                {
                    mType = TYPE_DIRECTORY;
                }
                return;
            }
            mType = TYPE_NOT_FOUND;
            mPath = path;
            if (String.IsNullOrWhiteSpace(mPath))
            {
                mPath = "空";
            }
        }

        public int Type
        {
            get
            {
                return mType;
            }
        }

        public string Path
        {
            get
            {
                return mPath;
            }
        }
    }
}