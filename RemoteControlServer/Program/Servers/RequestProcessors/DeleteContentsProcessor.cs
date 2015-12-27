using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class DeleteContentsProcessor : BasicProcessor
    {
        public DeleteContentsProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            DeleteContentsReq req = mSocketTalker.ReceiveObject<DeleteContentsReq>();
            DeleteContentsRes res = new DeleteContentsRes();
            res.DeleteResults = new string[req.ContentPaths.Count];
            for (int i = 0; i < req.ContentPaths.Count; i++)
            {
                Content content = new Content(req.ContentPaths[i]);
                try
                {
                    switch (content.Type)
                    {
                        case Content.TYPE_NOT_FOUND:
                            throw new KnownException("此路径所代表的不是一个文件或目录。");
                        case Content.TYPE_FILE:
                            File.Delete(content.Path);
                            break;
                        case Content.TYPE_DRIVER:
                            throw new KnownException("此路径所代表的是一个驱动器。");
                        case Content.TYPE_DIRECTORY:
                            Directory.Delete(content.Path, true);
                            break;
                    }
                }
                catch (Exception e)
                {
                    res.ErorrOccured = true;
                    res.DeleteResults[i] = content.Path + " " + e.Message;
                }
            }

            mSocketTalker.SendInt(ProtocolTypes.TYPE_DELETE_CONTENTS);
            mSocketTalker.SendObject(res);
        }
    }
}
