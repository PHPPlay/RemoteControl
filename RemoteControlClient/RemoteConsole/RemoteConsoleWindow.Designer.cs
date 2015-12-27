namespace iWay.RemoteControlClient.RemoteConsole
{
    partial class RemoteConsoleWindow
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
            this.mConsoleOutput = new System.Windows.Forms.TextBox();
            this.mConsoleInputEditor = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mConsoleOutput
            // 
            this.mConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mConsoleOutput.Location = new System.Drawing.Point(12, 12);
            this.mConsoleOutput.Multiline = true;
            this.mConsoleOutput.Name = "mConsoleOutput";
            this.mConsoleOutput.ReadOnly = true;
            this.mConsoleOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.mConsoleOutput.Size = new System.Drawing.Size(560, 336);
            this.mConsoleOutput.TabIndex = 1;
            // 
            // mConsoleInputEditor
            // 
            this.mConsoleInputEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mConsoleInputEditor.Location = new System.Drawing.Point(12, 354);
            this.mConsoleInputEditor.Name = "mConsoleInputEditor";
            this.mConsoleInputEditor.Size = new System.Drawing.Size(560, 21);
            this.mConsoleInputEditor.TabIndex = 0;
            this.mConsoleInputEditor.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mConsoleInputEditor_KeyPress);
            // 
            // RemoteConsoleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 387);
            this.Controls.Add(this.mConsoleInputEditor);
            this.Controls.Add(this.mConsoleOutput);
            this.Name = "RemoteConsoleWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "远程控制台";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mConsoleOutput;
        private System.Windows.Forms.TextBox mConsoleInputEditor;

    }
}