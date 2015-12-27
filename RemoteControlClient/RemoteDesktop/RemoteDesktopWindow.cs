using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.Program;

namespace iWay.RemoteControlClient.RemoteDesktop
{
    public partial class RemoteDesktopWindow : Form
    {
        private RemoteDesktopClient mClient;

        public RemoteDesktopWindow(Connector connector)
        {
            InitializeComponent();
            mClient = new RemoteDesktopClient(connector);
            mClient.SetForm(this);
            mClient.setOnImageReceivedHandler(OnImageReceived);
            mClient.SetOnConnectionErrorHandler(OnConnectionError);
            mClient.BeginReceiveOutput();
        }

        private void OnImageReceived(object sender, OnImageReceivedArgs e)
        {
            viewer.Image = e.ReceivedImage;
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

        private void changeImageQualityMenu_Click(object sender, EventArgs e)
        {
            string s = ((ToolStripMenuItem)sender).Text;
            s = s.Remove(s.LastIndexOf('%'));
            int n = int.Parse(s);
            mClient.SendImageQualityChange(n);
            foreach (ToolStripMenuItem t in changeImageQualityMenu.DropDownItems)
                t.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
        }

        private void changeRefreshSpanMenu_Click(object sender, EventArgs e)
        {
            string s = ((ToolStripMenuItem)sender).Text;
            s = s.Remove(s.LastIndexOf(' '));
            int n = int.Parse(s);
            mClient.SendRefreshSpanChange(n);
            foreach (ToolStripMenuItem t in changeRefreshSpanMenu.DropDownItems)
                t.Checked = false;
            ((ToolStripMenuItem)sender).Checked = true;
        }

        private void mouseClickMenu_Click(object sender, EventArgs e)
        {
            mouseClickMenu.Checked = !mouseClickMenu.Checked;
        }

        private void mouseMoveMenu_Click(object sender, EventArgs e)
        {
            mouseMoveMenu.Checked = !mouseMoveMenu.Checked;
        }

        private void keyboardInputMenu_Click(object sender, EventArgs e)
        {
            keyboardInputMenu.Checked = !keyboardInputMenu.Checked;
        }

        private void viewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseMoveMenu.Checked)
                return;
            Point pbase = PointToScreen(viewer.Location);
            float px = (float)e.X / viewer.Width;
            float py = (float)e.Y / viewer.Height;
            mClient.SendMouseMove(px, py);
        }

        private void viewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mouseClickMenu.Checked)
                return;
            Point pbase = PointToScreen(viewer.Location);
            float px = (float)e.X / viewer.Width;
            float py = (float)e.Y / viewer.Height;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mClient.SendMouseDown(0, px, py);
                    break;
                case MouseButtons.Middle:
                    mClient.SendMouseDown(1, px, py);
                    break;
                case MouseButtons.Right:
                    mClient.SendMouseDown(2, px, py);
                    break;
            }
        }

        private void viewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (!mouseClickMenu.Checked)
                return;
            Point pbase = PointToScreen(viewer.Location);
            float px = (float)e.X / viewer.Width;
            float py = (float)e.Y / viewer.Height;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mClient.SendMouseUp(0, px, py);
                    break;
                case MouseButtons.Middle:
                    mClient.SendMouseUp(1, px, py);
                    break;
                case MouseButtons.Right:
                    mClient.SendMouseUp(2, px, py);
                    break;
            }
        }

        private void this_KeyDown(object sender, KeyEventArgs e)
        {
            if (keyboardInputMenu.Checked)
                mClient.SendKeyboardDown((int)e.KeyCode);
        }

        private void this_KeyUp(object sender, KeyEventArgs e)
        {
            if (keyboardInputMenu.Checked)
                mClient.SendKeyboardUp((int)e.KeyCode);
        }

        private void resetWindowSizeMenu_Click(object sender, EventArgs e)
        {
            if (viewer.Image != null && this.WindowState != FormWindowState.Maximized)
            {
                float f = (float)viewer.Image.Width / viewer.Image.Height;
                Size s = new Size();
                s.Width = (int)(viewer.Height * f);
                s.Height = this.ClientSize.Height;
                this.ClientSize = s;
            }
        }

        private void windowSnapMenu_Click(object sender, EventArgs e)
        {
            if (viewer.Image != null)
            {
                Bitmap bmp = new Bitmap(viewer.Image);
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "截图另存为...";
                dialog.FileName = "截图.jpg";
                dialog.Filter = "位图文件|*.bmp";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(dialog.FileName);
                }
            }
            else
            {
                MessageBox.Show("当前目前没有图像可以供保存。");
            }
        }
    }
}
