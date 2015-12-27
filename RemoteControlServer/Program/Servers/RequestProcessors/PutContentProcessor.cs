using System;
using System.IO;
using System.Net.Sockets;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;

namespace iWay.RemoteControlServer.Program.Servers.RequestProcessors
{
    public class PutContentProcessor : BasicProcessor
    {
        public PutContentProcessor(SocketTalker socketTalker)
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

        public override void ProcessRequest()
        {
            PutContentReq req = mSocketTalker.ReceiveObject<PutContentReq>();
            PutContentRes res = new PutContentRes();
            try
            {
                mSocketTalker.SendInt(ProtocolTypes.TYPE_PUT_CONTENT);
                mSocketTalker.SendObject(res);

                while (true)
                {
                    string contentPath = mSocketTalker.ReceiveString();
                    if (contentPath == "  ")
                    {
                        break;
                    }
                    Content content = new Content(contentPath);
                    if (content.Type != Content.TYPE_NOT_FOUND)
                    {
                        mSocketTalker.SendString("ER");
                    }
                    else
                    {
                        FileInfo fileInfo = null;
                        FileStream fileStream = null;
                        try
                        {
                            fileInfo = new FileInfo(content.Path);
                            if (fileInfo.Exists)
                            {
                                throw new Exception("文件 " + fileInfo.FullName + " 已存在，请先删除。");
                            }
                            DirectoryInfo directoryInfo = fileInfo.Directory;
                            if (directoryInfo.Exists == false)
                            {
                                directoryInfo.Create();
                            }
                            fileStream = fileInfo.OpenWrite();
                            if (fileStream.CanWrite)
                            {
                                mSocketTalker.SendString("OK");
                                long length = mSocketTalker.ReceiveLong();
                                byte[] buffer = new byte[16 * 1024];
                                long received = 0;
                                while (received != length)
                                {
                                    long toReceive = length - received;
                                    if (toReceive < buffer.Length)
                                    {
                                        int count = mSocketTalker.Socket.Receive(buffer, (int)toReceive, SocketFlags.None);
                                        received += count;
                                        fileStream.Write(buffer, 0, count);
                                    }
                                    else
                                    {
                                        int count = mSocketTalker.Socket.Receive(buffer, buffer.Length, SocketFlags.None);
                                        received += count;
                                        fileStream.Write(buffer, 0, count);
                                    }
                                }
                                fileStream.Flush();
                                fileStream.Close();
                            }
                            else
                            {
                                throw new Exception("文件 " + fileInfo.FullName + " 不可写。");
                            }
                        }
                        catch (Exception e)
                        {
                            mSocketTalker.SendString("ER-" + e.Message);
                        }
                        finally
                        {
                            if (fileInfo != null)
                            {
                                fileInfo = null;
                            }
                            if (fileStream != null)
                            {
                                fileStream.Close();
                                fileStream = null;
                            }
                        }
                    }
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