namespace iWay.RemoteControlClient.Connect
{
    partial class ConnectWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mConnectPasswordEditor = new System.Windows.Forms.TextBox();
            this.mConnectButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.mConnectTypeSelector = new System.Windows.Forms.ComboBox();
            this.mToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.mConnectAddressEditor = new System.Windows.Forms.TextBox();
            this.mLoadingCover = new iWay.RemoteControlClient.Common.LoadingCover();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // mConnectPasswordEditor
            // 
            this.mConnectPasswordEditor.Location = new System.Drawing.Point(83, 64);
            this.mConnectPasswordEditor.Name = "mConnectPasswordEditor";
            this.mConnectPasswordEditor.Size = new System.Drawing.Size(184, 20);
            this.mConnectPasswordEditor.TabIndex = 3;
            this.mConnectPasswordEditor.UseSystemPasswordChar = true;
            // 
            // mConnectButton
            // 
            this.mConnectButton.Location = new System.Drawing.Point(113, 145);
            this.mConnectButton.Name = "mConnectButton";
            this.mConnectButton.Size = new System.Drawing.Size(75, 23);
            this.mConnectButton.TabIndex = 1;
            this.mConnectButton.Text = "连接";
            this.mConnectButton.UseVisualStyleBackColor = true;
            this.mConnectButton.Click += new System.EventHandler(this.mConnectButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "类型：";
            // 
            // mConnectTypeSelector
            // 
            this.mConnectTypeSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mConnectTypeSelector.FormattingEnabled = true;
            this.mConnectTypeSelector.Items.AddRange(new object[] {
            "远程控制台",
            "远程桌面",
            "远程资源管理器"});
            this.mConnectTypeSelector.Location = new System.Drawing.Point(83, 105);
            this.mConnectTypeSelector.Name = "mConnectTypeSelector";
            this.mConnectTypeSelector.Size = new System.Drawing.Size(184, 21);
            this.mConnectTypeSelector.TabIndex = 6;
            // 
            // mConnectAddressEditor
            // 
            this.mConnectAddressEditor.Location = new System.Drawing.Point(83, 23);
            this.mConnectAddressEditor.Name = "mConnectAddressEditor";
            this.mConnectAddressEditor.Size = new System.Drawing.Size(184, 20);
            this.mConnectAddressEditor.TabIndex = 2;
            // 
            // mLoadingCover
            // 
            this.mLoadingCover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mLoadingCover.LoadingText = "正在加载...";
            this.mLoadingCover.Location = new System.Drawing.Point(0, 0);
            this.mLoadingCover.Name = "mLoadingCover";
            this.mLoadingCover.Size = new System.Drawing.Size(300, 194);
            this.mLoadingCover.TabIndex = 8;
            this.mLoadingCover.Visible = false;
            // 
            // ConnectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 194);
            this.Controls.Add(this.mLoadingCover);
            this.Controls.Add(this.mConnectAddressEditor);
            this.Controls.Add(this.mConnectTypeSelector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mConnectButton);
            this.Controls.Add(this.mConnectPasswordEditor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConnectWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接到远程计算机";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mConnectPasswordEditor;
        private System.Windows.Forms.Button mConnectButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox mConnectTypeSelector;
        private System.Windows.Forms.ToolTip mToolTip;
        private Common.LoadingCover mLoadingCover;
        private System.Windows.Forms.TextBox mConnectAddressEditor;
    }
}