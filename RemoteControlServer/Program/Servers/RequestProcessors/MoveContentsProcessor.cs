using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class MoveContentsProcessor : BasicProcessor
    {
        public MoveContentsProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            MoveContentsReq req = mSocketTalker.ReceiveObject<MoveContentsReq>();
            MoveContentsRes res = new MoveContentsRes();
            try
            {
                Content containerContent = new Content(req.Container);
                switch (containerContent.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("找不到路径 " + containerContent.Path + " 所代表的容器。");
                    case Content.TYPE_FILE:
                        throw new KnownException("路径 " + containerContent.Path + " 代表的是一个文件，不能作为容器。");
                }
                res.MoveResults = new string[req.ContentPaths.Count];
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
                                if (IsInTheSameDriver(content.Path, containerContent.Path) == false)
                                {
                                    throw new KnownException("无法在不同的驱动器之间移动文件，可以先复制后删除。");
                                }
                                string movedFilePath = GetMovedFilePath(content.Path, containerContent.Path);
                                if (new Content(movedFilePath).Type != Content.TYPE_NOT_FOUND)
                                {
                                    throw new KnownException("容器中已经存在同名文件或目录，不能移动到此容器中。");
                                }
                                File.Move(content.Path, movedFilePath);
                                break;
                            case Content.TYPE_DIRECTORY:
                                if (IsInTheSameDriver(content.Path, containerContent.Path) == false)
                                {
                                    throw new KnownException("无法在不同的驱动器之间移动目录，可以先复制后删除。");
                                }
                                string movedDirectoryPath = GetMovedDirectoryPath(content.Path, containerContent.Path);
                                if (new Content(movedDirectoryPath).Type != Content.TYPE_NOT_FOUND)
                                {
                                    throw new KnownException("容器中已经存在同名文件或目录，不能移动到此容器中。");
                                }
                                Directory.Move(content.Path, movedDirectoryPath);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        res.MoveResults[i] = content.Path + " " + e.Message;
                    }
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_MOVE_CONTENTS);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_MOVE_CONTENTS);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
