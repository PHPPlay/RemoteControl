using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using System.Collections.Generic;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class CopyContentsProcessor : BasicProcessor
    {
        public CopyContentsProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        private void CopyFile(Content content, Content containerContent)
        {
            string copiedFilePath = GetCopiedFilePath(content.Path, containerContent.Path);
            Content copiedFileContent = new Content(copiedFilePath);
            if (copiedFileContent.Type != Content.TYPE_NOT_FOUND)
            {
                throw new KnownException("容器中已经存在同名文件或目录，不能复制到此容器中。");
            }
            File.Copy(content.Path, copiedFilePath);
        }

        private void CopyDirectory(Content content, Content containerContent)
        {
            string copiedDirectoryPath = GetCopiedDirectoryPath(content.Path, containerContent.Path);
            Content copiedDirectoryContent = new Content(copiedDirectoryPath);
            if (copiedDirectoryContent.Type != Content.TYPE_NOT_FOUND)
            {
                throw new KnownException("容器中已经存在同名文件或目录，不能复制到此容器中。");
            }
            Directory.CreateDirectory(copiedDirectoryPath);
            string[] files = Directory.GetFiles(content.Path);
            foreach (string file in files)
            {
                Content fileContent = new Content(file);
                CopyFile(fileContent, copiedDirectoryContent);
            }
            string[] directories = Directory.GetDirectories(content.Path);
            foreach (string directory in directories)
            {
                Content directoryContent = new Content(directory);
                CopyDirectory(directoryContent, copiedDirectoryContent);
            }
        }

        public override void ProcessRequest()
        {
            CopyContentsReq req = mSocketTalker.ReceiveObject<CopyContentsReq>();
            CopyContentsRes res = new CopyContentsRes();
            Content containerContent = new Content(req.Container);
            try
            {    
                switch (containerContent.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("找不到路径 " + containerContent.Path + " 所代表的容器。");
                    case Content.TYPE_FILE:
                        throw new KnownException("路径 " + containerContent.Path + " 代表的是一个文件，不能作为容器。");
                }
                res.CopyResults = new string[req.ContentPaths.Count];
                for (int i = 0; i < req.ContentPaths.Count; i++)
                {
                    Content content = new Content(req.ContentPaths[i]);
                    try
                    {
                        switch (content.Type)
                        {
                            case Content.TYPE_NOT_FOUND:
                                throw new KnownException("找不到此路径所代表的内容。");
                            case Content.TYPE_DRIVER:
                                throw new KnownException("此路径所代表的是一个驱动器，不能被移动。");
                            case Content.TYPE_FILE:
                                CopyFile(content, containerContent);
                                break;
                            case Content.TYPE_DIRECTORY:
                                CopyDirectory(content, containerContent);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        res.CopyResults[i] = content.Path + " " + e.Message;
                    }
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_COPY_CONTENTS);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_COPY_CONTENTS);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
