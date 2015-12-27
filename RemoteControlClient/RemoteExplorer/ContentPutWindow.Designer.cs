namespace iWay.RemoteControlClient.RemoteExplorer
{
    partial class ContentPutWindow
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
            this.mLogTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.mLogTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mLogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.mLogTextBox.Multiline = true;
            this.mLogTextBox.Name = "textBox1";
            this.mLogTextBox.ReadOnly = true;
            this.mLogTextBox.Size = new System.Drawing.Size(384, 162);
            this.mLogTextBox.TabIndex = 0;
            // 
            // ContentPutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 162);
            this.Controls.Add(this.mLogTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContentPutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上传";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ContentPutWindow_FormClosed);
            this.Load += new System.EventHandler(this.ContentPutWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mLogTextBox;
    }
}