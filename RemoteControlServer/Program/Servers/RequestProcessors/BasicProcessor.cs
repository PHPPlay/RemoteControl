using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public abstract class BasicProcessor
    {
        protected SocketTalker mSocketTalker;

        public BasicProcessor(SocketTalker socketTalker)
        {
            mSocketTalker = socketTalker;
        }

        protected string GetFileName(string validPath)
        {
            int lastSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            return validPath.Substring(lastSeperatorIndex + 1);
        }

        protected string GetFileContainer(string validPath)
        {
            int lastSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            return validPath.Substring(0, lastSeperatorIndex + 1);
        }

        protected string GetDirectoryContainer(string validPath)
        {
            int lastSecondSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar, validPath.Length - 2);
            return validPath.Substring(0, lastSecondSeperatorIndex + 1);
        }

        protected string GetDirectoryName(string validPath)
        {
            int lastSecondSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar, validPath.Length - 2);
            return validPath.Substring(lastSecondSeperatorIndex + 1);
        }

        protected bool IsFileOrDirectoryNameValid(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                if (name.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        protected string GetRenamedFilePath(string validFilePath, string validName)
        {
            return GetFileContainer(validFilePath) + validName;
        }

        protected string GetRenamedDirectoryPath(string validDirectoryPath, string validName)
        {
            return GetDirectoryContainer(validDirectoryPath) + validName + Path.DirectorySeparatorChar;
        }

        protected string GetMovedFilePath(string validFilePath, string validConteinerPath)
        {
            return validConteinerPath + GetFileName(validFilePath);
        }

        protected string GetCopiedFilePath(string validFilePath, string validConteinerPath)
        {
            return GetMovedFilePath(validFilePath, validConteinerPath);
        }

        protected string GetMovedDirectoryPath(string validDirectoryPath, string validConteinerPath)
        {
            return validConteinerPath + GetDirectoryName(validDirectoryPath);
        }

        protected string GetCopiedDirectoryPath(string validDirectoryPath, string validConteinerPath)
        {
            return GetMovedDirectoryPath(validDirectoryPath, validConteinerPath);
        }

        protected bool IsInTheSameDriver(string validContentPath1, string validContentPath2)
        {
            return validContentPath1.Substring(0, 2) == validContentPath2.Substring(0, 2);
        }

        public virtual bool CanProcessNextRequest
        {
            get
            {
                return true;
            }
        }

        public abstract void ProcessRequest();
    }
}
