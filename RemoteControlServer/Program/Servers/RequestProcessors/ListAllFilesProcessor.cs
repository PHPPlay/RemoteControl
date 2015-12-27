using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class ListAllFilesProcessor : BasicProcessor
    {
        public ListAllFilesProcessor(SocketTalker socketTalker)
            : base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            ListAllFilesReq req = mSocketTalker.ReceiveObject<ListAllFilesReq>();
            ListAllFilesRes res = new ListAllFilesRes();
            try
            {
                if (String.IsNullOrEmpty(req.DriverOrDirectoryPath))
                {
                    throw new KnownException("要求列出所有文件的驱动器或目录的路径是空的。");
                }
                else
                {
                    Content content = new Content(req.DriverOrDirectoryPath);
                    switch (content.Type)
                    {
                        case Content.TYPE_NOT_FOUND:
                            throw new KnownException("无法找到驱动器或目录 " + content.Path + " 。");
                        case Content.TYPE_DRIVER:
                            res.AllFiles = Directory.GetFiles(content.Path);
                            break;
                        case Content.TYPE_FILE:
                            throw new KnownException("路径 " + content.Path + " 是一个文件，无法列出内容。");
                        case Content.TYPE_DIRECTORY:
                            res.AllFiles = Directory.GetFiles(content.Path);
                            break;
                    }

                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_LIST_ALL_FILES);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_LIST_ALL_FILES);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
