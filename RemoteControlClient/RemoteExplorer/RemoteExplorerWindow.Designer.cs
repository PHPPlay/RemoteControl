namespace iWay.RemoteControlClient.RemoteExplorer
{
    partial class RemoteExplorerWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteExplorerWindow));
            this.mContentList = new System.Windows.Forms.ListView();
            this.mContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mDownloadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mUploadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mUploadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mUploadFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mCutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mPasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mRenameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mDeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mNewFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mPropertiesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mCopyItemPathMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mExcuteRemoteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mExcuteRemoteShowWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mExcuteRemoteHideWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mOpenRemoteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mImageList = new System.Windows.Forms.ImageList(this.components);
            this.mErrorLable = new System.Windows.Forms.Label();
            this.mCurrentDirectoryEditor = new System.Windows.Forms.ComboBox();
            this.mGoBackButton = new System.Windows.Forms.Button();
            this.mGoToOrRefreshButton = new System.Windows.Forms.Button();
            this.mGoHomeButton = new System.Windows.Forms.Button();
            this.mContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mContentList
            // 
            this.mContentList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mContentList.ContextMenuStrip = this.mContextMenu;
            this.mContentList.HideSelection = false;
            this.mContentList.LargeImageList = this.mImageList;
            this.mContentList.Location = new System.Drawing.Point(8, 42);
            this.mContentList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mContentList.Name = "mContentList";
            this.mContentList.Size = new System.Drawing.Size(609, 363);
            this.mContentList.TabIndex = 17;
            this.mContentList.UseCompatibleStateImageBehavior = false;
            this.mContentList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mContentList_MouseDoubleClick);
            // 
            // mContextMenu
            // 
            this.mContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mDownloadMenuItem,
            this.mUploadMenuItem,
            this.mToolStripSeparator1,
            this.mCutMenuItem,
            this.mCopyMenuItem,
            this.mPasteMenuItem,
            this.mToolStripSeparator2,
            this.mRenameMenuItem,
            this.mDeleteMenuItem,
            this.mNewFolderMenuItem,
            this.mToolStripSeparator4,
            this.mPropertiesMenuItem,
            this.mToolStripSeparator3,
            this.mCopyItemPathMenuItem,
            this.mExcuteRemoteMenuItem,
            this.mOpenRemoteMenuItem});
            this.mContextMenu.Name = "contextMenu";
            this.mContextMenu.Size = new System.Drawing.Size(153, 314);
            this.mContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.mContextMenu_Opening);
            // 
            // mDownloadMenuItem
            // 
            this.mDownloadMenuItem.Name = "mDownloadMenuItem";
            this.mDownloadMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mDownloadMenuItem.Text = "下载";
            this.mDownloadMenuItem.Click += new System.EventHandler(this.mDownloadMenuItem_Click);
            // 
            // mUploadMenuItem
            // 
            this.mUploadMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mUploadFileMenuItem,
            this.mUploadFolderMenuItem});
            this.mUploadMenuItem.Name = "mUploadMenuItem";
            this.mUploadMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mUploadMenuItem.Text = "上传";
            // 
            // mUploadFileMenuItem
            // 
            this.mUploadFileMenuItem.Name = "mUploadFileMenuItem";
            this.mUploadFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mUploadFileMenuItem.Text = "文件";
            this.mUploadFileMenuItem.Click += new System.EventHandler(this.mUploadFileMenuItem_Click);
            // 
            // mUploadFolderMenuItem
            // 
            this.mUploadFolderMenuItem.Name = "mUploadFolderMenuItem";
            this.mUploadFolderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mUploadFolderMenuItem.Text = "文件夹";
            this.mUploadFolderMenuItem.Click += new System.EventHandler(this.mUploadFolderMenuItem_Click);
            // 
            // mToolStripSeparator1
            // 
            this.mToolStripSeparator1.Name = "mToolStripSeparator1";
            this.mToolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // mCutMenuItem
            // 
            this.mCutMenuItem.Name = "mCutMenuItem";
            this.mCutMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mCutMenuItem.Text = "剪切";
            this.mCutMenuItem.Click += new System.EventHandler(this.mCutMenuItem_Click);
            // 
            // mCopyMenuItem
            // 
            this.mCopyMenuItem.Name = "mCopyMenuItem";
            this.mCopyMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mCopyMenuItem.Text = "复制";
            this.mCopyMenuItem.Click += new System.EventHandler(this.mCopyMenuItem_Click);
            // 
            // mPasteMenuItem
            // 
            this.mPasteMenuItem.Name = "mPasteMenuItem";
            this.mPasteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mPasteMenuItem.Text = "粘贴";
            this.mPasteMenuItem.Click += new System.EventHandler(this.mPasteMenuItem_Click);
            // 
            // mToolStripSeparator2
            // 
            this.mToolStripSeparator2.Name = "mToolStripSeparator2";
            this.mToolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mRenameMenuItem
            // 
            this.mRenameMenuItem.Name = "mRenameMenuItem";
            this.mRenameMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mRenameMenuItem.Text = "重命名";
            this.mRenameMenuItem.Click += new System.EventHandler(this.mRenameMenuItem_Click);
            // 
            // mDeleteMenuItem
            // 
            this.mDeleteMenuItem.Name = "mDeleteMenuItem";
            this.mDeleteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mDeleteMenuItem.Text = "删除";
            this.mDeleteMenuItem.Click += new System.EventHandler(this.mDeleteMenuItem_Click);
            // 
            // mNewFolderMenuItem
            // 
            this.mNewFolderMenuItem.Name = "mNewFolderMenuItem";
            this.mNewFolderMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mNewFolderMenuItem.Text = "新建文件夹";
            this.mNewFolderMenuItem.Click += new System.EventHandler(this.mNewFolderMenuItem_Click);
            // 
            // mToolStripSeparator4
            // 
            this.mToolStripSeparator4.Name = "mToolStripSeparator4";
            this.mToolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // mPropertiesMenuItem
            // 
            this.mPropertiesMenuItem.Name = "mPropertiesMenuItem";
            this.mPropertiesMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mPropertiesMenuItem.Text = "属性";
            this.mPropertiesMenuItem.Click += new System.EventHandler(this.mPropertiesMenuItem_Click);
            // 
            // mToolStripSeparator3
            // 
            this.mToolStripSeparator3.Name = "mToolStripSeparator3";
            this.mToolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // mCopyItemPathMenuItem
            // 
            this.mCopyItemPathMenuItem.Name = "mCopyItemPathMenuItem";
            this.mCopyItemPathMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mCopyItemPathMenuItem.Text = "复制路径";
            this.mCopyItemPathMenuItem.Click += new System.EventHandler(this.mCopyItemPathMenuItem_Click);
            // 
            // mExcuteRemoteMenuItem
            // 
            this.mExcuteRemoteMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mExcuteRemoteShowWindowMenuItem,
            this.mExcuteRemoteHideWindowMenuItem});
            this.mExcuteRemoteMenuItem.Name = "mExcuteRemoteMenuItem";
            this.mExcuteRemoteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mExcuteRemoteMenuItem.Text = "远程执行";
            // 
            // mExcuteRemoteShowWindowMenuItem
            // 
            this.mExcuteRemoteShowWindowMenuItem.Name = "mExcuteRemoteShowWindowMenuItem";
            this.mExcuteRemoteShowWindowMenuItem.Size = new System.Drawing.Size(122, 22);
            this.mExcuteRemoteShowWindowMenuItem.Text = "允许窗口";
            this.mExcuteRemoteShowWindowMenuItem.Click += new System.EventHandler(this.mExcuteRemoteShowWindowMenuItem_Click);
            // 
            // mExcuteRemoteHideWindowMenuItem
            // 
            this.mExcuteRemoteHideWindowMenuItem.Name = "mExcuteRemoteHideWindowMenuItem";
            this.mExcuteRemoteHideWindowMenuItem.Size = new System.Drawing.Size(122, 22);
            this.mExcuteRemoteHideWindowMenuItem.Text = "隐藏窗口";
            this.mExcuteRemoteHideWindowMenuItem.Click += new System.EventHandler(this.mExcuteRemoteHideWindowMenuItem_Click);
            // 
            // mOpenRemoteMenuItem
            // 
            this.mOpenRemoteMenuItem.Name = "mOpenRemoteMenuItem";
            this.mOpenRemoteMenuItem.Size = new System.Drawing.Size(152, 22);
            this.mOpenRemoteMenuItem.Text = "远程打开";
            this.mOpenRemoteMenuItem.Click += new System.EventHandler(this.openRemoteStripMenuItem_Click);
            // 
            // mImageList
            // 
            this.mImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("mImageList.ImageStream")));
            this.mImageList.TransparentColor = System.Drawing.Color.Empty;
            this.mImageList.Images.SetKeyName(0, "driver.png");
            this.mImageList.Images.SetKeyName(1, "file.png");
            this.mImageList.Images.SetKeyName(2, "folder.png");
            // 
            // mErrorLable
            // 
            this.mErrorLable.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mErrorLable.AutoSize = true;
            this.mErrorLable.BackColor = System.Drawing.Color.White;
            this.mErrorLable.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mErrorLable.Location = new System.Drawing.Point(75, 196);
            this.mErrorLable.Name = "mErrorLable";
            this.mErrorLable.Size = new System.Drawing.Size(474, 21);
            this.mErrorLable.TabIndex = 15;
            this.mErrorLable.Text = "无法显示当前文件夹，可能是因为文件夹不存在或没有权限读取。";
            this.mErrorLable.Visible = false;
            // 
            // mCurrentDirectoryEditor
            // 
            this.mCurrentDirectoryEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mCurrentDirectoryEditor.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mCurrentDirectoryEditor.FormattingEnabled = true;
            this.mCurrentDirectoryEditor.Location = new System.Drawing.Point(78, 8);
            this.mCurrentDirectoryEditor.Name = "mCurrentDirectoryEditor";
            this.mCurrentDirectoryEditor.Size = new System.Drawing.Size(398, 27);
            this.mCurrentDirectoryEditor.TabIndex = 20;
            this.mCurrentDirectoryEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mCurrentDirectoryEditor_KeyPress);
            // 
            // mGoBackButton
            // 
            this.mGoBackButton.Location = new System.Drawing.Point(7, 7);
            this.mGoBackButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mGoBackButton.Name = "mGoBackButton";
            this.mGoBackButton.Size = new System.Drawing.Size(65, 28);
            this.mGoBackButton.TabIndex = 18;
            this.mGoBackButton.Text = "后退";
            this.mGoBackButton.UseVisualStyleBackColor = true;
            this.mGoBackButton.Click += new System.EventHandler(this.mGoBackButton_Click);
            // 
            // mGoToOrRefreshButton
            // 
            this.mGoToOrRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mGoToOrRefreshButton.Location = new System.Drawing.Point(482, 7);
            this.mGoToOrRefreshButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mGoToOrRefreshButton.Name = "mGoToOrRefreshButton";
            this.mGoToOrRefreshButton.Size = new System.Drawing.Size(65, 28);
            this.mGoToOrRefreshButton.TabIndex = 16;
            this.mGoToOrRefreshButton.Text = "刷新";
            this.mGoToOrRefreshButton.UseVisualStyleBackColor = true;
            this.mGoToOrRefreshButton.Click += new System.EventHandler(this.mGoToOrRefreshButton_Click);
            // 
            // mGoHomeButton
            // 
            this.mGoHomeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mGoHomeButton.Location = new System.Drawing.Point(553, 7);
            this.mGoHomeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mGoHomeButton.Name = "mGoHomeButton";
            this.mGoHomeButton.Size = new System.Drawing.Size(65, 28);
            this.mGoHomeButton.TabIndex = 19;
            this.mGoHomeButton.Text = "主页";
            this.mGoHomeButton.UseVisualStyleBackColor = true;
            this.mGoHomeButton.Click += new System.EventHandler(this.mGoHomeButton_Click);
            // 
            // RemoteExplorerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 412);
            this.Controls.Add(this.mErrorLable);
            this.Controls.Add(this.mCurrentDirectoryEditor);
            this.Controls.Add(this.mGoBackButton);
            this.Controls.Add(this.mGoToOrRefreshButton);
            this.Controls.Add(this.mGoHomeButton);
            this.Controls.Add(this.mContentList);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "RemoteExplorerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程资源管理器客户端";
            this.Load += new System.EventHandler(this.Window_Loaded);
            this.mContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView mContentList;
        private System.Windows.Forms.Label mErrorLable;
        private System.Windows.Forms.ComboBox mCurrentDirectoryEditor;
        private System.Windows.Forms.Button mGoBackButton;
        private System.Windows.Forms.Button mGoToOrRefreshButton;
        private System.Windows.Forms.Button mGoHomeButton;
        private System.Windows.Forms.ImageList mImageList;
        private System.Windows.Forms.ContextMenuStrip mContextMenu;
        private System.Windows.Forms.ToolStripMenuItem mDownloadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mUploadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mUploadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mUploadFolderMenuItem;
        private System.Windows.Forms.ToolStripSeparator mToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mCutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mCopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mPasteMenuItem;
        private System.Windows.Forms.ToolStripSeparator mToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mRenameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mDeleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mNewFolderMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mPropertiesMenuItem;
        private System.Windows.Forms.ToolStripSeparator mToolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mCopyItemPathMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mExcuteRemoteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mOpenRemoteMenuItem;
        private System.Windows.Forms.ToolStripSeparator mToolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mExcuteRemoteShowWindowMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mExcuteRemoteHideWindowMenuItem;


    }
}

