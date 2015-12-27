using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.Common;
using iWay.RemoteControlClient.Program;
using iWay.RemoteControlBase.Protocol.RemoteExplorer;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Responses;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Requests;
using System.Text;
using iWay.RemoteControlBase.Protocol.RemoteExplorer.Data;

namespace iWay.RemoteControlClient.RemoteExplorer
{
    public partial class RemoteExplorerWindow : Form
    {
        private Connector mConnector;
        private RemoteExplorerClient mClient;

        public RemoteExplorerWindow(Connector connector)
        {
            InitializeComponent();
            mConnector = connector;
            mClient = new RemoteExplorerClient(connector);
            mClient.SetForm(this);
            mClient.setOnResponseReceivedHandler(OnOutputReceived);
            mClient.SetOnConnectionErrorHandler(OnConnectionError);
            mClient.BeginReceiveResponse();
        }

        private string mCurrentDirectory = "";

        private void SendListContentsReq()
        {
            mCurrentDirectoryEditor.Text = mCurrentDirectory;
            ListContentsReq req = new ListContentsReq();
            req.DriverOrDirectoryPath = mCurrentDirectory;
            mClient.SendRequest(ProtocolTypes.TYPE_LIST_CONTENTS, req);
        }

        private void OnOutputReceived(object sender, OnResponseReceivedArgs e)
        {
            switch (e.ResponseType)
            {
                case ProtocolTypes.TYPE_LIST_CONTENTS:
                    {
                        ListContentsRes res = e.GetResponse<ListContentsRes>();
                        if (res.ErorrOccured)
                        {
                            mErrorLable.Text = res.ErrorMessage;
                            mErrorLable.Show();
                            mContentList.Items.Clear();
                        }
                        else
                        {
                            mErrorLable.Text = "";
                            mErrorLable.Hide();
                            mContentList.Items.Clear();

                            if (res.Drivers != null && res.Drivers.Length > 0)
                            {
                                foreach (string driver in res.Drivers)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.ImageKey = "driver.png";
                                    item.Text = driver;
                                    mContentList.Items.Add(item);
                                }
                            }
                            if (res.Directories != null && res.Directories.Length > 0)
                            {
                                foreach (string directory in res.Directories)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.ImageKey = "folder.png";
                                    item.Text = Path.GetFileName(directory);
                                    mContentList.Items.Add(item);
                                }
                            }
                            if (res.Files != null && res.Files.Length > 0)
                            {
                                foreach (string file in res.Files)
                                {
                                    ListViewItem item = new ListViewItem();
                                    item.ImageKey = "file.png";
                                    item.Text = Path.GetFileName(file);
                                    mContentList.Items.Add(item);
                                }
                            }
                        }
                        mGoToOrRefreshButton.Text = "刷新";
                    }
                    break;
                case ProtocolTypes.TYPE_RENAME_CONTENT:
                    {
                        RenameContentRes res = e.GetResponse<RenameContentRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "重命名失败");
                        }
                        SendListContentsReq();
                    }
                    break;
                case ProtocolTypes.TYPE_DELETE_CONTENTS:
                    {
                        DeleteContentsRes res = e.GetResponse<DeleteContentsRes>();
                        if (res.ErorrOccured)
                        {
                            StringBuilder builder = new StringBuilder();
                            foreach(string result in res.DeleteResults)
                            {
                                if (result != null)
                                {
                                    builder.AppendLine(result);
                                }
                            }
                            MessageBox.Show(builder.ToString(), "删除出现错误");
                        }
                        SendListContentsReq();
                    }
                    break;
                case ProtocolTypes.TYPE_CREATE_DIRECTORY:
                    {
                        CreateDirectoryRes res = e.GetResponse<CreateDirectoryRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "新建文件夹失败");
                        }
                        SendListContentsReq();
                    }
                    break;
                case ProtocolTypes.TYPE_START_PROCESS:
                    {
                        StartProcessRes res = e.GetResponse<StartProcessRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "打开进程失败");
                        }
                    }
                    break;
                case ProtocolTypes.TYPE_GET_CONTENT_INFO:
                    {
                        GetContentInfoRes res = e.GetResponse<GetContentInfoRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "获取属性失败");
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            foreach (TextInfo result in res.InfoList)
                            {
                                builder.AppendLine(result.Name + " : " + result.Value);
                            }
                            MessageBox.Show(builder.ToString(), "数学");
                        }
                    }
                    break;
                case ProtocolTypes.TYPE_MOVE_CONTENTS:
                    {
                        MoveContentsRes res = e.GetResponse<MoveContentsRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "移动出现错误");
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            foreach (string result in res.MoveResults)
                            {
                                if (result != null)
                                {
                                    builder.AppendLine(result);
                                }
                            }
                            if (builder.Length > 0)
                            {
                                MessageBox.Show(builder.ToString(), "移动出现错误");
                            }
                        }
                        SendListContentsReq();
                    }
                    break;
                case ProtocolTypes.TYPE_COPY_CONTENTS:
                    {
                        CopyContentsRes res = e.GetResponse<CopyContentsRes>();
                        if (res.ErorrOccured)
                        {
                            MessageBox.Show(res.ErrorMessage, "复制出现错误");
                        }
                        else
                        {
                            StringBuilder builder = new StringBuilder();
                            foreach (string result in res.CopyResults)
                            {
                                if (result != null)
                                {
                                    builder.AppendLine(result);
                                }
                            }
                            if (builder.Length > 0)
                            {
                                MessageBox.Show(builder.ToString(), "复制出现错误");
                            }
                        }
                        SendListContentsReq();
                    }
                    break;
            }
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            SendListContentsReq();
        }

        private void mGoBackButton_Click(object sender, EventArgs e)
        {
            mCurrentDirectory = Path.GetDirectoryName(mCurrentDirectory);
            SendListContentsReq();
        }

        private void mCurrentDirectoryEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                mCurrentDirectoryEditor.Items.Insert(0, mCurrentDirectoryEditor.Text);
                mGoToOrRefreshButton_Click(null, null);
            }
            else
            {
                mGoToOrRefreshButton.Text = "前往";
            }
        }

        private void mGoToOrRefreshButton_Click(object sender, EventArgs e)
        {
            mCurrentDirectory = mCurrentDirectoryEditor.Text;
            SendListContentsReq();
        }

        private void mGoHomeButton_Click(object sender, EventArgs e)
        {
            mCurrentDirectory = null;
            SendListContentsReq();
        }

        private void mContentList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = mContentList.SelectedItems[0];
            if (item.ImageKey == "driver.png")
                mCurrentDirectory = item.Text;
            else if (item.ImageKey == "folder.png")
                mCurrentDirectory = Path.Combine(mCurrentDirectory, item.Text);
            else
                return;
            SendListContentsReq();
        }

        private void mContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (ToolStripItem item in mContextMenu.Items)
            {
                item.Enabled = false;
            }
            if (mErrorLable.Visible)
            {
                return;
            }
            if (mCurrentDirectory == "")
            {
                mPropertiesMenuItem.Enabled = mContentList.SelectedItems.Count > 0;
                if (mContentList.SelectedItems.Count == 1)
                {
                    mCopyItemPathMenuItem.Enabled = true;
                }
            }
            else if (mContentList.SelectedItems.Count == 0)
            {
                mUploadMenuItem.Enabled = true;
                mPasteMenuItem.Enabled = mPasteType != "";
                mNewFolderMenuItem.Enabled = true;
                mPropertiesMenuItem.Enabled = true;
            }
            else if (mContentList.SelectedItems.Count == 1)
            {
                if (mContentList.SelectedItems[0].ImageKey != "drive.png")
                {
                    mDownloadMenuItem.Enabled = true;
                }
                mCutMenuItem.Enabled = true;
                mCopyMenuItem.Enabled = true;
                mRenameMenuItem.Enabled = true;
                mDeleteMenuItem.Enabled = true;
                mPropertiesMenuItem.Enabled = true;
                mCopyItemPathMenuItem.Enabled = true;
                if (mContentList.SelectedItems[0].ImageKey == "file.png")
                {
                    if (mContentList.SelectedItems[0].Text.ToLower().EndsWith(".exe"))
                    {
                        mExcuteRemoteMenuItem.Enabled = true;
                    }
                    mOpenRemoteMenuItem.Enabled = true;
                }
            }
            else if (mContentList.SelectedItems.Count > 1)
            {
                mCutMenuItem.Enabled = true;
                mCopyMenuItem.Enabled = true;
                mDeleteMenuItem.Enabled = true;
            }
        }

        private void mDownloadMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择一个文件夹来保存所下载的文件";
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            
            string selectedItem = mContentList.SelectedItems[0].Text;
            string savePath = Path.Combine(dialog.SelectedPath, selectedItem);
            if (File.Exists(savePath) || Directory.Exists(savePath))
            {
                MessageBox.Show(selectedItem + " 已存在于 " + savePath + " 无法保存。", "文件已存在");
            }


            ThreadStart threadStart = () =>
            {
                ContentGetWindow window = new ContentGetWindow();
                window.Host = mConnector.Host;
                window.Port = mConnector.Port;
                window.Password = mConnector.Password;
                window.ContentPath = Path.Combine(mCurrentDirectory, selectedItem);
                window.ContainerPath = dialog.SelectedPath;
                Application.Run(window);
            };
            Thread thread = new Thread(threadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        private void mUploadFileMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择要上传的文件";
            if (dialog.ShowDialog() != DialogResult.OK || String.IsNullOrEmpty(dialog.FileName))
            {
                return;
            }

            ThreadStart threadStart = () =>
            {
                ContentPutWindow window = new ContentPutWindow();
                window.Host = mConnector.Host;
                window.Port = mConnector.Port;
                window.Password = mConnector.Password;
                window.ContentPath = dialog.FileName;
                window.ContainerPath = mCurrentDirectory;
                Application.Run(window);
            };
            Thread thread = new Thread(threadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        private void mUploadFolderMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择要上传的文件夹";
            dialog.ShowNewFolderButton = false;
            if (dialog.ShowDialog() != DialogResult.OK || String.IsNullOrEmpty(dialog.SelectedPath))
            {
                return;
            }

            ThreadStart threadStart = () =>
            {
                ContentPutWindow window = new ContentPutWindow();
                window.Host = mConnector.Host;
                window.Port = mConnector.Port;
                window.Password = mConnector.Password;
                window.ContentPath = dialog.SelectedPath;
                window.ContainerPath = mCurrentDirectory;
                Application.Run(window);
            };
            Thread thread = new Thread(threadStart);
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        private string mPasteType = "";
        private List<string> mPasteSources = new List<string>();

        private void mCutMenuItem_Click(object sender, EventArgs e)
        {
            mPasteType = "Cut";
            mPasteSources.Clear();
            foreach (ListViewItem item in mContentList.SelectedItems)
            {
                string name = item.Text;
                string path = Path.Combine(mCurrentDirectory, name);
                mPasteSources.Add(path);
            }
        }

        private void mCopyMenuItem_Click(object sender, EventArgs e)
        {
            mPasteType = "Copy";
            mPasteSources.Clear();
            foreach (ListViewItem item in mContentList.SelectedItems)
            {
                string name = item.Text;
                string path = Path.Combine(mCurrentDirectory, name);
                mPasteSources.Add(path);
            }
        }

        private void mPasteMenuItem_Click(object sender, EventArgs e)
        {
            if (mPasteType == "Cut")
            {
                MoveContentsReq req = new MoveContentsReq();
                req.ContentPaths = mPasteSources;
                req.Container = mCurrentDirectory;
                mClient.SendRequest(ProtocolTypes.TYPE_MOVE_CONTENTS, req);
            }
            if (mPasteType == "Copy")
            {
                CopyContentsReq req = new CopyContentsReq();
                req.ContentPaths = mPasteSources;
                req.Container = mCurrentDirectory;
                mClient.SendRequest(ProtocolTypes.TYPE_COPY_CONTENTS, req);
            }
        }

        private void mRenameMenuItem_Click(object sender, EventArgs e)
        {
            string currentName = mContentList.SelectedItems[0].Text;
            InputDialog inputDialog = new InputDialog();
            inputDialog.Title = "请输入新的名称";
            inputDialog.AllowEmpty = false;
            inputDialog.Result = currentName;
            inputDialog.SelectionStart = 0;
            int length = currentName.LastIndexOf('.');
            inputDialog.SelectionLength = length > 0 ? length : currentName.Length;
            if (inputDialog.ShowDialog() == DialogResult.OK)
            {
                if (inputDialog.Result == currentName)
                {
                    return;
                }
                RenameContentReq req = new RenameContentReq();
                req.ContentPath = Path.Combine(mCurrentDirectory, currentName);
                req.NewContentName = inputDialog.Result;
                mClient.SendRequest(ProtocolTypes.TYPE_RENAME_CONTENT, req);
            }
        }

        private void mDeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除这些项目吗？", "确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                DeleteContentsReq req = new DeleteContentsReq();
                req.ContentPaths = new List<string>();
                foreach (ListViewItem item in mContentList.SelectedItems)
                {
                    string contentPath = Path.Combine(mCurrentDirectory, item.Text);
                    req.ContentPaths.Add(contentPath);
                }
                mClient.SendRequest(ProtocolTypes.TYPE_DELETE_CONTENTS, req);
            }
        }

        private void mNewFolderMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog dialog = new InputDialog();
            dialog.Title = "请输入文件夹名称";
            dialog.AllowEmpty = false;
            dialog.Result = "新建文件夹";
            dialog.SelectionStart = 0;
            dialog.SelectionLength = dialog.Result.Length;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                CreateDirectoryReq req = new CreateDirectoryReq();
                req.Container = mCurrentDirectory;
                req.Name = dialog.Result;
                mClient.SendRequest(ProtocolTypes.TYPE_CREATE_DIRECTORY, req);
            }
        }

        private void mPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            string name = mContentList.SelectedItems.Count > 0 ? mContentList.SelectedItems[0].Text : mCurrentDirectory;
            string path = Path.Combine(mCurrentDirectory, name);
            GetContentInfoReq req = new GetContentInfoReq();
            req.ContentPath = path;
            mClient.SendRequest(ProtocolTypes.TYPE_GET_CONTENT_INFO, req);
        }

        private void mCopyItemPathMenuItem_Click(object sender, EventArgs e)
        {
            Exception exception = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    string name = mContentList.SelectedItems[0].Text;
                    string path = Path.Combine(mCurrentDirectory, name);
                    Clipboard.SetText(path);
                    return;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    Thread.Sleep(100);
                }
            }
            MessageBox.Show(exception.Message, "复制文件路径时出现问题");
        }

        private void mExcuteRemoteShowWindowMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog dialog = new InputDialog();
            dialog.Title = "请输入参数，如果没有请点击确定";
            dialog.AllowEmpty = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StartProcessReq req = new StartProcessReq();
                req.FileName = Path.Combine(mCurrentDirectory, mContentList.SelectedItems[0].Text);
                req.Arguments = dialog.Result;
                req.UseShellExecute = false;
                req.CreateNoWindow = false;
                mClient.SendRequest(ProtocolTypes.TYPE_START_PROCESS, req);
            }
        }

        private void mExcuteRemoteHideWindowMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog dialog = new InputDialog();
            dialog.Title = "请输入参数，如果没有请点击确定";
            dialog.AllowEmpty = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StartProcessReq req = new StartProcessReq();
                req.FileName = Path.Combine(mCurrentDirectory, mContentList.SelectedItems[0].Text);
                req.Arguments = dialog.Result;
                req.UseShellExecute = false;
                req.CreateNoWindow = true;
                mClient.SendRequest(ProtocolTypes.TYPE_START_PROCESS, req);
            }
        }

        private void openRemoteStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcessReq req = new StartProcessReq();
            req.FileName = "explorer";
            req.Arguments = Path.Combine(mCurrentDirectory, mContentList.SelectedItems[0].Text);
            req.UseShellExecute = false;
            req.CreateNoWindow = true;
            mClient.SendRequest(ProtocolTypes.TYPE_START_PROCESS, req);
        }

        private void OnConnectionError(object sender, OnConnectionErrorArgs e)
        {
#if DEBUG
            MessageBox.Show("连接出现错误，请重新连接。\r\n\r\n" + e.Error, Text);
#else
            MessageBox.Show("连接出现错误，请重新连接。" + e.Error.StackTrace, Text);
#endif
            Close();
        }

    }
}
