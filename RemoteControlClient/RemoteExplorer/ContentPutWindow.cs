using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlBase.Network;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlClient.Program;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using iWay.RemoteControlBase.Network.SocketTalker;
using System.IO;
using System.Net.Sockets;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Exceptions;

namespace iWay.RemoteControlClient.RemoteExplorer
{
    public partial class ContentPutWindow : Form
    {
        private Thread mUIThread;

        public ContentPutWindow()
        {
            InitializeComponent();
            mUIThread = Thread.CurrentThread;
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Password { get; set; }

        public string ContentPath { get; set; }

        public string ContainerPath { get; set; }

        private Connector mConnector;
        private delegate void EventLogger(string message);

        private void LogEvent(string message)
        {
            if (Thread.CurrentThread != mUIThread)
            {
                Invoke(new EventLogger(LogEvent), message);
            }
            else
            {
                if (mLogTextBox.Text.Length > 0)
                {
                    mLogTextBox.AppendText("\r\n");
                }
                mLogTextBox.AppendText(DateTime.Now.ToString() + "  " + message);
            }
        }

        private void ContentPutWindow_Load(object sender, EventArgs e)
        {
            mConnector = new Connector(Host, Port, Password, ConnectType.TYPE_REMOTE_EXPLORER);
            mConnector.SetForm(this);
            mConnector.SetConnectSucceedHandler(OnConnectSucceed);
            mConnector.SetConnectFailHandler(OnConnectFail);
            mConnector.BeginConnect();
            LogEvent("正在连接...");
        }

        private RemoteExplorerClient mClient;

        private void OnConnectSucceed(object sender, ConnectSucceedArgs e)
        {
            LogEvent("连接成功！");
            mClient = new RemoteExplorerClient(mConnector);
            mClient.SetForm(this);
            mClient.setOnResponseReceivedHandler(OnOutputReceived);
            mClient.SetOnConnectionErrorHandler(OnConnectionError);
            mClient.BeginReceiveResponse(true);
            PutContentReq req = new PutContentReq();
            mClient.SendRequest(ProtocolTypes.TYPE_PUT_CONTENT, req);
            LogEvent("正在请求上传文件...");
        }

        private void OnConnectFail(object sender, ConnectFailArgs e)
        {
            LogEvent("连接失败！请关闭窗口并重试。");
        }

        private void OnOutputReceived(object sender, OnResponseReceivedArgs e)
        {
            switch (e.ResponseType)
            {
                case ProtocolTypes.TYPE_PUT_CONTENT:
                    {
                        PutContentRes res = e.GetResponse<PutContentRes>();
                        if (res.ErorrOccured)
                        {
                            LogEvent("请求上传文件失败，请关闭窗口并重试。");
                        }
                        else
                        {
                            LogEvent("请求上传文件成功。正在上传文件...");
                            mSendFilesThread = new Thread(SendContent);
                            mSendFilesThread.IsBackground = true;
                            mSendFilesThread.Start();
                        }
                    }
                    break;
            }
        }

        private void OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            LogEvent("连接出错！请关闭窗口并重试。");
        }

        protected string GetFileName(string validPath)
        {
            int lastSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            return validPath.Substring(lastSeperatorIndex + 1);
        }

        protected string GetFileContainer(string validPath)
        {
            int lastSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
            return validPath.Substring(0, lastSeperatorIndex + 1);
        }

        protected string GetDirectoryContainer(string validPath)
        {
            int lastSecondSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar, validPath.Length - 2);
            return validPath.Substring(0, lastSecondSeperatorIndex + 1);
        }

        protected string GetDirectoryName(string validPath)
        {
            int lastSecondSeperatorIndex = validPath.LastIndexOf(System.IO.Path.DirectorySeparatorChar, validPath.Length - 2);
            return validPath.Substring(lastSecondSeperatorIndex + 1);
        }

        private Thread mSendFilesThread;
        private int mFileNameStartIndex;
        private SocketTalker mSocketTalker;
        private FileInfo mFileInfo = null;
        private FileStream mFileStream = null;
        private int mBufferSize = 16 * 1024;

        private void SendFile(string filePath)
        {
            string fileName = filePath.Substring(mFileNameStartIndex);
            try
            {
                mFileInfo = new FileInfo(filePath);
                mFileStream = mFileInfo.OpenRead();
                if (mFileStream.CanRead)
                {
                    string path = Path.Combine(ContainerPath, fileName);
                    mSocketTalker.SendString(path);
                    string result = mSocketTalker.ReceiveString();
                    if (result == "OK")
                    {
                        LogEvent("正在上传 " + fileName + " ...");
                        mSocketTalker.SendLong(mFileStream.Length);
                        byte[] buffer = new byte[mBufferSize];
                        while (mFileStream.Position != mFileStream.Length)
                        {
                            int count = mFileStream.Read(buffer, 0, buffer.Length);
                            mSocketTalker.SendData(buffer, 0, count);
                        }
                        LogEvent("上传 " + fileName + " 完成。");
                    }
                    else
                    {
                        throw new Exception("远程文件 " + path + " 不可写。");
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
                LogEvent("上传 " + fileName + " 失败，" + e.Message);
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

        private void SendDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach(string file in files)
            {
                SendFile(file);
            }
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (string directory in directories)
            {
                SendDirectory(directory);
            }
        }

        private void SendContent()
        {
            mSocketTalker = new SocketTalker(mConnector.Socket);
            try
            {
                Content content = new Content(ContentPath);
                switch (content.Type)
                {
                    case Content.TYPE_NOT_FOUND:
                        mSocketTalker.SendString("  ");
                        throw new KnownException("找不到路径 " + content.Path + " 所代表的内容。");
                    case Content.TYPE_DRIVER:
                        mSocketTalker.SendString("  ");
                        throw new KnownException("路径 " + content.Path + " 代表的是一个驱动器，无法下载。");
                    case Content.TYPE_FILE:
                        mFileNameStartIndex = GetFileContainer(content.Path).Length;
                        SendFile(content.Path);
                        mSocketTalker.SendString("  ");
                        break;
                    case Content.TYPE_DIRECTORY:
                        mFileNameStartIndex = GetDirectoryContainer(content.Path).Length;
                        SendDirectory(content.Path);
                        mSocketTalker.SendString("  ");
                        break;
                }

                LogEvent("上传完成。");
            }
            catch
            {
                LogEvent("上传文件出错，数据可能不完整，请关闭窗口并重试。");
            }
            finally
            {
                if (mSocketTalker != null)
                {
                    mSocketTalker.Close();
                }
                if (mFileInfo != null)
                {
                    mFileInfo = null;
                }
                if (mFileStream != null)
                {
                    mFileStream.Close();
                }
            }
        }

        private void ContentPutWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mConnector != null)
            {
                mConnector.CancleConnect();
            }
            if (mClient != null)
            {
                mClient.Close(null);
            }
        }
    }
}
