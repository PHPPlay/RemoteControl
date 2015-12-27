using System;
using System.IO;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using System.Net.Sockets;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class GetContentProcessor : BasicProcessor
    {
        public GetContentProcessor(SocketTalker socketTalker)
            : base(socketTalker)
        {
        }

        public override bool CanProcessNextRequest
        {
            get
            {
                return false;
            }
        }

        private FileInfo mFileInfo = null;
        private FileStream mFileStream = null;
        private int mBufferSize = 16 * 1024;

        private void SendFile(Content content, Content containerContent)
        {
            string fileName = content.Path.Substring(containerContent.Path.Length);
            mSocketTalker.SendString(fileName);
            try
            {
                mFileInfo = new FileInfo(content.Path);
                mFileStream = mFileInfo.OpenRead();
                if (mFileStream.CanRead)
                {
                    mSocketTalker.SendString("OK");
                    mSocketTalker.SendLong(mFileStream.Length);
                    byte[] buffer = new byte[mBufferSize];
                    while (mFileStream.Position != mFileStream.Length)
                    {
                        int count = mFileStream.Read(buffer, 0, buffer.Length);
                        mSocketTalker.SendData(buffer, 0, count);
                    }
                }
                else
                {
                    throw new Exception("文件 " + fileName + " 不可读。");
                }
            }
            catch (SocketException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                mSocketTalker.SendString("ER-" + e.Message);
            }
            finally
            {
                if (mFileInfo != null)
                {
                    mFileInfo = null;
                }
                if (mFileStream != null)
                {
                    mFileStream.Close();
                    mFileStream = null;
                }
            }
        }

        private void SendDirectory(Content content, Content containerContent)
        {
            string[] files = Directory.GetFiles(content.Path);
            foreach (string file in files)
            {
                SendFile(new Content(file), containerContent);
            }
            string[] directories = Directory.GetDirectories(content.Path);
            foreach (string directory in directories)
            {
                SendDirectory(new Content(directory), containerContent);
            }
        }

        public override void ProcessRequest()
        {
            GetContentReq req = mSocketTalker.ReceiveObject<GetContentReq>();
            GetContentRes res = new GetContentRes();
            Content content = new Content(req.ContentPath);
            try
            {
                switch (content.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        throw new KnownException("找不到路径 " + content.Path + " 所代表的内容。");
                    case Content.TYPE_DRIVER:
                        throw new KnownException("路径 " + content.Path + " 代表的是一个驱动器，无法下载。");
                }

                mSocketTalker.SendInt(ProtocolTypes.TYPE_GET_CONTENT);
                mSocketTalker.SendObject(res);

                switch (content.Type)
                {
                    case Content.TYPE_FILE:
                        SendFile(content, new Content(GetFileContainer(content.Path)));
                        mSocketTalker.SendString("  ");
                        break;
                    case Content.TYPE_DIRECTORY:
                        SendDirectory(content, new Content(GetDirectoryContainer(content.Path)));
                        mSocketTalker.SendString("  ");
                        break;
                }
            }
            catch (SocketException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                res.ErorrOccured = true;
                res.ErrorMessage = e.Message;

                mSocketTalker.SendInt(ProtocolTypes.TYPE_GET_CONTENT);
                mSocketTalker.SendObject(res);
            }
        }
    }
}