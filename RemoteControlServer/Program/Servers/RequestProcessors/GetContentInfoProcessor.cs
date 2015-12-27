using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using System.Collections.Generic;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Data;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class GetContentInfoProcessor : BasicProcessor
    {
        public GetContentInfoProcessor(SocketTalker socketTalker)
            :base(socketTalker)
        {
        }

        public override void ProcessRequest()
        {
            GetContentInfoReq req = mSocketTalker.ReceiveObject<GetContentInfoReq>();
            GetContentInfoRes res = new GetContentInfoRes();
            try
            {
                res.InfoList = new List<TextInfo>();
                Content content = new Content(req.ContentPath);
                switch (content.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("路径 " + content.Path + " 代表的不是一个驱动器、文件或目录，无法获取信息。");
                    case Content.TYPE_DRIVER:
                        DriveInfo driveInfo = new DriveInfo(content.Path);
                        res.InfoList.Add(new TextInfo("AvailableFreeSpace", driveInfo.AvailableFreeSpace.ToString() + " Bytes"));
                        res.InfoList.Add(new TextInfo("AvailableFreDriveFormateSpace", driveInfo.DriveFormat.ToString()));
                        res.InfoList.Add(new TextInfo("DriveType", driveInfo.DriveType.ToString()));
                        res.InfoList.Add(new TextInfo("IsReady", driveInfo.IsReady.ToString()));
                        res.InfoList.Add(new TextInfo("Name", driveInfo.Name.ToString()));
                        res.InfoList.Add(new TextInfo("RootDirectory", driveInfo.RootDirectory.ToString()));
                        res.InfoList.Add(new TextInfo("TotalFreeSpace", driveInfo.TotalFreeSpace.ToString() + " Bytes"));
                        res.InfoList.Add(new TextInfo("TotalSize", driveInfo.TotalSize.ToString() + " Bytes"));
                        res.InfoList.Add(new TextInfo("VolumeLabel", driveInfo.VolumeLabel.ToString()));
                        break;
                    case Content.TYPE_FILE:
                        FileInfo fileInfo = new FileInfo(content.Path);
                        res.InfoList.Add(new TextInfo("Attributes", fileInfo.Attributes.ToString()));
                        res.InfoList.Add(new TextInfo("CreationTime", fileInfo.CreationTime.ToString()));
                        res.InfoList.Add(new TextInfo("CreationTimeUtc", fileInfo.CreationTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("Directory", fileInfo.Directory.ToString()));
                        res.InfoList.Add(new TextInfo("DirectoryName", fileInfo.DirectoryName.ToString()));
                        res.InfoList.Add(new TextInfo("Exists", fileInfo.Exists.ToString()));
                        res.InfoList.Add(new TextInfo("Extension", fileInfo.Extension.ToString()));
                        res.InfoList.Add(new TextInfo("FullName", fileInfo.FullName.ToString()));
                        res.InfoList.Add(new TextInfo("IsReadOnly", fileInfo.IsReadOnly.ToString()));
                        res.InfoList.Add(new TextInfo("LastAccessTime", fileInfo.LastAccessTime.ToString()));
                        res.InfoList.Add(new TextInfo("LastAccessTime", fileInfo.LastAccessTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("LastWriteTime", fileInfo.LastWriteTime.ToString()));
                        res.InfoList.Add(new TextInfo("LastWriteTimeUtc", fileInfo.LastWriteTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("Length", fileInfo.Length.ToString() + " Bytes"));
                        res.InfoList.Add(new TextInfo("Name", fileInfo.Name.ToString()));
                        break;
                    case Content.TYPE_DIRECTORY:
                        DirectoryInfo directoryInfo = new DirectoryInfo(content.Path);
                        res.InfoList.Add(new TextInfo("Attributes", directoryInfo.Attributes.ToString()));
                        res.InfoList.Add(new TextInfo("CreationTime", directoryInfo.CreationTime.ToString()));
                        res.InfoList.Add(new TextInfo("CreationTimeUtc", directoryInfo.CreationTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("Exists", directoryInfo.Exists.ToString()));
                        res.InfoList.Add(new TextInfo("Extension", directoryInfo.Extension.ToString()));
                        res.InfoList.Add(new TextInfo("FullName", directoryInfo.FullName.ToString()));
                        res.InfoList.Add(new TextInfo("LastAccessTime", directoryInfo.LastAccessTime.ToString()));
                        res.InfoList.Add(new TextInfo("LastAccessTime", directoryInfo.LastAccessTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("LastWriteTime", directoryInfo.LastWriteTime.ToString()));
                        res.InfoList.Add(new TextInfo("LastWriteTimeUtc", directoryInfo.LastWriteTimeUtc.ToString()));
                        res.InfoList.Add(new TextInfo("Name", directoryInfo.Name.ToString()));
                        res.InfoList.Add(new TextInfo("Parent", directoryInfo.Parent.FullName.ToString()));
                        res.InfoList.Add(new TextInfo("Root", directoryInfo.Root.ToString()));
                        break;
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_GET_CONTENT_INFO);
                mSocketTalker.SendObject(res);
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_GET_CONTENT_INFO);
                mSocketTalker.SendObject(res);
            }
        }
    }
}
