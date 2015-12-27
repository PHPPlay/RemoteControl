using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class RenameContentProcessor : BasicProcessor
    {
        public RenameContentProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            RenameContentReq req = mSocketTalker.ReceiveObject<RenameContentReq>();
            RenameContentRes res = new RenameContentRes();
            try
            {
                Content content = new Content(req.ContentPath);
                switch (content.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("无法找到文件或目录 " + content.Path + " 。");
                    case Content.TYPE_DRIVER:
                        throw new KnownException("不能重命名驱动器 " + content.Path + " 。");
                    case Content.TYPE_FILE:
                        if(IsFileOrDirectoryNameValid(req.NewContentName) == false)
                        {
                            throw new KnownException("新的文件名称 " + req.NewContentName + " 是无效的。");
                        }
                        string newFilePath = GetRenamedFilePath(content.Path, req.NewContentName);
                        File.Move(content.Path, newFilePath);
                        break;
                    case Content.TYPE_DIRECTORY:
                        if (IsFileOrDirectoryNameValid(req.NewContentName) == false)
                        {
                            throw new KnownException("新的目录名称 " + req.NewContentName + " 是无效的。");
                        }
                        string newDirectoryPath = GetRenamedDirectoryPath(content.Path, req.NewContentName);
                        Directory.Move(content.Path, newDirectoryPath);
                        break;
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_RENAME_CONTENT);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_RENAME_CONTENT);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
