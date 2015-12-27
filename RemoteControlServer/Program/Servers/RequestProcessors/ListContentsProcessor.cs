using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class ListContentsProcessor : BasicProcessor
    {
        public ListContentsProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            ListContentsReq req = mSocketTalker.ReceiveObject<ListContentsReq>();
            ListContentsRes res = new ListContentsRes();
            try
            {
                if (String.IsNullOrEmpty(req.DriverOrDirectoryPath))
                {
                    res.Drivers = Directory.GetLogicalDrives();
                }
                else
                {
                    Content content = new Content(req.DriverOrDirectoryPath);
                    switch (content.Type)
                    {
                        case Content.TYPE_NOT_FOUND:
                            throw new KnownException("无法找到文件或目录 " + content.Path + " 。");
                        case Content.TYPE_DRIVER:
                            res.Directories = Directory.GetDirectories(content.Path);
                            res.Files = Directory.GetFiles(content.Path);
                            break;
                        case Content.TYPE_FILE:
                            throw new KnownException("路径 " + content.Path + " 是一个文件，无法列出内容。");
                        case Content.TYPE_DIRECTORY:
                            res.Directories = Directory.GetDirectories(content.Path);
                            res.Files = Directory.GetFiles(content.Path);
                            break;
                    }
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_LIST_CONTENTS);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_LIST_CONTENTS);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
