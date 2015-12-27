using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class CreateDirectoryProcessor : BasicProcessor
    {
        public CreateDirectoryProcessor(SocketTalker socketTalker)
            : base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            CreateDirectoryReq req = mSocketTalker.ReceiveObject<CreateDirectoryReq>();
            CreateDirectoryRes res = new CreateDirectoryRes();
            try
            {
                Content content = new Content(req.Container);
                switch (content.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("无法在路径 " + content.Path + " 上创建目录，因为它代表的不是一个驱动器或目录。");
                    case Content.TYPE_DRIVER:
                        if (IsFileOrDirectoryNameValid(req.Name) == false)
                        {
                            throw new KnownException("要创建的目录名称 " + req.Name + "是无效的。");
                        }
                        Directory.CreateDirectory(content.Path + req.Name + Path.DirectorySeparatorChar);
                        break;
                    case Content.TYPE_FILE:
                        throw new KnownException("无法在路径 " + content.Path + " 上创建目录，因为它代表是一个文件。");
                    case Content.TYPE_DIRECTORY:
                        if (IsFileOrDirectoryNameValid(req.Name) == false)
                        {
                            throw new KnownException("要创建的目录名称 " + req.Name + "是无效的。");
                        }
                        Directory.CreateDirectory(content.Path + req.Name + Path.DirectorySeparatorChar);
                        break;
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_CREATE_DIRECTORY);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_CREATE_DIRECTORY);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
