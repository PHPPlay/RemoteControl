using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using iWay.RemoteControlBase.Network;
using iWay.RemoteControlBase.Network.SocketTalker;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.Program;

namespace iWay.RemoteControlClient.RemoteExplorer
{
    public partial class ContentGetWindow : Form
    {
        private Thread mUIThread;

        public ContentGetWindow()
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

        private void ContentGetWindow_Load(object sender, EventArgs e)
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
            GetContentReq req = new GetContentReq();
            req.ContentPath = ContentPath;
            mClient.SendRequest(ProtocolTypes.TYPE_GET_CONTENT, req);
            LogEvent("正在请求下载文件...");
        }

        private void OnConnectFail(object sender, ConnectFailArgs e)
        {
            LogEvent("连接失败！请关闭窗口并重试。");
        }

        private void OnOutputReceived(object sender, OnResponseReceivedArgs e)
        {
            switch (e.ResponseType)
            {
                case ProtocolTypes.TYPE_GET_CONTENT:
                    {
                        GetContentRes res = e.GetResponse<GetContentRes>();
                        if (res.ErorrOccured)
                        {
                            LogEvent("请求下载文件失败，请关闭窗口并重试。");
                        }
                        else
                        {
                            LogEvent("请求下载文件成功。正在接收文件...");
                            mReceiveFilesThread = new Thread(ReceiveFiles);
                            mReceiveFilesThread.IsBackground = true;
                            mReceiveFilesThread.Start();
                        }
                    }
                    break;
            }
        }

        private void OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
            LogEvent("连接出错！请关闭窗口并重试。");
        }

        private Thread mReceiveFilesThread;

        private void ReceiveFiles()
        {
            SocketTalker socketTalker = new SocketTalker(mConnector.Socket);
            FileInfo fileInfo = null;
            FileStream fileStream = null;
            try
            {
                while (true)
                {
                    string name = socketTalker.ReceiveString();
                    if (name == "  ")
                    {
                        break;
                    }
                    LogEvent("正在接收 " + name + " ...");
                    string status = socketTalker.ReceiveString();
                    if (status == "OK")
                    {
                        string path = Path.Combine(ContainerPath, name);
                        string dir = Path.GetDirectoryName(path);
                        Directory.CreateDirectory(dir);
                        fileInfo = new FileInfo(path);
                        fileStream = fileInfo.OpenWrite();
                        long length = socketTalker.ReceiveLong();
                        byte[] buffer = new byte[16 * 1024];
                        long received = 0;
                        while (received != length)
                        {
                            long toReceive = length - received;
                            if (toReceive < buffer.Length)
                            {
                                int count = socketTalker.Socket.Receive(buffer, (int)toReceive, SocketFlags.None);
                                received += count;
                                fileStream.Write(buffer, 0, count);
                            }
                            else
                            {
                                int count = socketTalker.Socket.Receive(buffer, buffer.Length, SocketFlags.None);
                                received += count;
                                fileStream.Write(buffer, 0, count);
                            }
                        }
                        fileStream.Flush();
                        fileStream.Close();
                    }
                    else
                    {
                        LogEvent("接收 " + name + " 失败，远程文件无法读取！");
                    }
                }
                LogEvent("接收完成。");
            }
            catch
            {
                LogEvent("接收文件出错，数据可能不完整，请关闭窗口并重试。");
            }
            finally
            {
                if (socketTalker != null)
                {
                    socketTalker.Close();
                }
                if (fileInfo != null)
                {
                    fileInfo = null;
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        private void ContentGetWindow_FormClosed(object sender, FormClosedEventArgs e)
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
