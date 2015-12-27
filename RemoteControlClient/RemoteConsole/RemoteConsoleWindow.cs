using System.Windows.Forms;
using iWay.RemoteControlClient.Connect;
using iWay.RemoteControlClient.Program;

namespace iWay.RemoteControlClient.RemoteConsole
{
    public partial class RemoteConsoleWindow : Form
    {
        private RemoteConsoleClient mClient;

        public RemoteConsoleWindow(Connector connector)
        {
            InitializeComponent();
            mClient = new RemoteConsoleClient(connector);
            mClient.SetForm(this);
            mClient.SetOnOutputReceivedHandler(OnOutputReceived);
            mClient.SetOnConnectionErrorHandler(OnConnectionError);
            mClient.BeginReceiveOutput();
        }

        private void OnOutputReceived(object sender, OnOutputReceivedArgs e)
        {
            mConsoleOutput.AppendText(e.ReceivedOutput);
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

        private void mConsoleInputEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                mClient.SendInput(mConsoleInputEditor.Text);
                mConsoleInputEditor.Clear();
            }
        }
    }
}
