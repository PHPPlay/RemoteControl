namespace iWay.RemoteControlClient.RemoteDesktop
{
    partial class RemoteDesktopWindow
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
            this.controlMenu = new System.Windows.Forms.MenuStrip();
            this.resetWindowSizeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.windowSnapMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.changeImageQualityMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.changeRefreshSpanMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseClickMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.mouseMoveMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.keyboardInputMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.viewer = new System.Windows.Forms.PictureBox();
            this.controlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).BeginInit();
            this.SuspendLayout();
            // 
            // controlMenu
            // 
            this.controlMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetWindowSizeMenu,
            this.windowSnapMenu,
            this.changeImageQualityMenu,
            this.changeRefreshSpanMenu,
            this.inputToolStripMenuItem});
            this.controlMenu.Location = new System.Drawing.Point(0, 0);
            this.controlMenu.Name = "controlMenu";
            this.controlMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.controlMenu.Size = new System.Drawing.Size(623, 24);
            this.controlMenu.TabIndex = 1;
            // 
            // resetWindowSizeMenu
            // 
            this.resetWindowSizeMenu.Name = "resetWindowSizeMenu";
            this.resetWindowSizeMenu.Size = new System.Drawing.Size(91, 20);
            this.resetWindowSizeMenu.Text = "重设窗口比例";
            this.resetWindowSizeMenu.Click += new System.EventHandler(this.resetWindowSizeMenu_Click);
            // 
            // windowSnapMenu
            // 
            this.windowSnapMenu.Name = "windowSnapMenu";
            this.windowSnapMenu.Size = new System.Drawing.Size(91, 20);
            this.windowSnapMenu.Text = "截取当前桌面";
            this.windowSnapMenu.Click += new System.EventHandler(this.windowSnapMenu_Click);
            // 
            // changeImageQualityMenu
            // 
            this.changeImageQualityMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11});
            this.changeImageQualityMenu.Name = "changeImageQualityMenu";
            this.changeImageQualityMenu.Size = new System.Drawing.Size(67, 20);
            this.changeImageQualityMenu.Text = "图像质量";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem2.Text = "10%";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem3.Text = "20%";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem4.Text = "30%";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem5.Text = "40%";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Checked = true;
            this.toolStripMenuItem6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem6.Text = "50%";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem7.Text = "60%";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem8.Text = "70%";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem9.Text = "80%";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem10.Text = "90%";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(102, 22);
            this.toolStripMenuItem11.Text = "100%";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.changeImageQualityMenu_Click);
            // 
            // changeRefreshSpanMenu
            // 
            this.changeRefreshSpanMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msToolStripMenuItem5,
            this.msToolStripMenuItem3,
            this.msToolStripMenuItem4,
            this.msToolStripMenuItem,
            this.msToolStripMenuItem6,
            this.msToolStripMenuItem1,
            this.msToolStripMenuItem7,
            this.msToolStripMenuItem2});
            this.changeRefreshSpanMenu.Name = "changeRefreshSpanMenu";
            this.changeRefreshSpanMenu.Size = new System.Drawing.Size(91, 20);
            this.changeRefreshSpanMenu.Text = "图像刷新间隔";
            // 
            // msToolStripMenuItem5
            // 
            this.msToolStripMenuItem5.Name = "msToolStripMenuItem5";
            this.msToolStripMenuItem5.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem5.Text = "125 ms";
            this.msToolStripMenuItem5.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem3
            // 
            this.msToolStripMenuItem3.Name = "msToolStripMenuItem3";
            this.msToolStripMenuItem3.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem3.Text = "250 ms";
            this.msToolStripMenuItem3.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem4
            // 
            this.msToolStripMenuItem4.Name = "msToolStripMenuItem4";
            this.msToolStripMenuItem4.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem4.Text = "500 ms";
            this.msToolStripMenuItem4.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem
            // 
            this.msToolStripMenuItem.Checked = true;
            this.msToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.msToolStripMenuItem.Name = "msToolStripMenuItem";
            this.msToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem.Text = "1000 ms";
            this.msToolStripMenuItem.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem6
            // 
            this.msToolStripMenuItem6.Name = "msToolStripMenuItem6";
            this.msToolStripMenuItem6.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem6.Text = "1500 ms";
            this.msToolStripMenuItem6.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem1
            // 
            this.msToolStripMenuItem1.Name = "msToolStripMenuItem1";
            this.msToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem1.Text = "2000 ms";
            this.msToolStripMenuItem1.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem7
            // 
            this.msToolStripMenuItem7.Name = "msToolStripMenuItem7";
            this.msToolStripMenuItem7.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem7.Text = "2500 ms";
            this.msToolStripMenuItem7.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // msToolStripMenuItem2
            // 
            this.msToolStripMenuItem2.Name = "msToolStripMenuItem2";
            this.msToolStripMenuItem2.Size = new System.Drawing.Size(117, 22);
            this.msToolStripMenuItem2.Text = "3000 ms";
            this.msToolStripMenuItem2.Click += new System.EventHandler(this.changeRefreshSpanMenu_Click);
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseClickMenu,
            this.mouseMoveMenu,
            this.keyboardInputMenu});
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.inputToolStripMenuItem.Text = "输入控制";
            // 
            // mouseClickMenu
            // 
            this.mouseClickMenu.Checked = true;
            this.mouseClickMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mouseClickMenu.Name = "mouseClickMenu";
            this.mouseClickMenu.Size = new System.Drawing.Size(146, 22);
            this.mouseClickMenu.Text = "启用鼠标点击";
            this.mouseClickMenu.Click += new System.EventHandler(this.mouseClickMenu_Click);
            // 
            // mouseMoveMenu
            // 
            this.mouseMoveMenu.Name = "mouseMoveMenu";
            this.mouseMoveMenu.Size = new System.Drawing.Size(146, 22);
            this.mouseMoveMenu.Text = "启用鼠标移动";
            this.mouseMoveMenu.Click += new System.EventHandler(this.mouseMoveMenu_Click);
            // 
            // keyboardInputMenu
            // 
            this.keyboardInputMenu.Checked = true;
            this.keyboardInputMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.keyboardInputMenu.Name = "keyboardInputMenu";
            this.keyboardInputMenu.Size = new System.Drawing.Size(146, 22);
            this.keyboardInputMenu.Text = "启用键盘输入";
            this.keyboardInputMenu.Click += new System.EventHandler(this.keyboardInputMenu_Click);
            // 
            // viewer
            // 
            this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewer.Location = new System.Drawing.Point(0, 24);
            this.viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.viewer.Name = "viewer";
            this.viewer.Size = new System.Drawing.Size(623, 373);
            this.viewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.viewer.TabIndex = 2;
            this.viewer.TabStop = false;
            this.viewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewer_MouseDown);
            this.viewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewer_MouseMove);
            this.viewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewer_MouseUp);
            // 
            // RemoteDesktopWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 397);
            this.Controls.Add(this.viewer);
            this.Controls.Add(this.controlMenu);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RemoteDesktopWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程桌面";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.this_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.this_KeyUp);
            this.controlMenu.ResumeLayout(false);
            this.controlMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip controlMenu;
        private System.Windows.Forms.ToolStripMenuItem resetWindowSizeMenu;
        private System.Windows.Forms.ToolStripMenuItem windowSnapMenu;
        private System.Windows.Forms.ToolStripMenuItem changeImageQualityMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem changeRefreshSpanMenu;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem msToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mouseClickMenu;
        private System.Windows.Forms.ToolStripMenuItem mouseMoveMenu;
        private System.Windows.Forms.ToolStripMenuItem keyboardInputMenu;
        private System.Windows.Forms.PictureBox viewer;
    }
}

