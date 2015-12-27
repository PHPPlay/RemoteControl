using System;
using System.Windows.Forms;

namespace iWay.RemoteControlClient.Common
{
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return this.Text; }
            set { this.Text = value; }
        }

        public bool AllowEmpty
        {
            get;
            set;
        }

        public string Result
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public int SelectionStart
        {
            get { return textBox1.SelectionStart; }
            set { textBox1.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return textBox1.SelectionLength; }
            set { textBox1.SelectionLength = value; }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                button1_Click(null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!AllowEmpty && textBox1.Text == "")
                return;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
