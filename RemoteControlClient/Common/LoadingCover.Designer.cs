namespace iWay.RemoteControlClient.Common
{
    partial class LoadingCover
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mLoadingLabel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.mLoadingLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.mLoadingLabel.AutoSize = true;
            this.mLoadingLabel.Location = new System.Drawing.Point(45, 47);
            this.mLoadingLabel.Name = "label1";
            this.mLoadingLabel.Size = new System.Drawing.Size(71, 12);
            this.mLoadingLabel.TabIndex = 2;
            this.mLoadingLabel.Text = "正在加载...";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(47, 69);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(202, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            // 
            // LoadingCover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mLoadingLabel);
            this.Controls.Add(this.progressBar1);
            this.Name = "LoadingCover";
            this.Size = new System.Drawing.Size(295, 160);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLoadingLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}
