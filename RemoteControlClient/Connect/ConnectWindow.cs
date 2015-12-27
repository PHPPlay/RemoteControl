using System;
using System.Windows.Forms;
using iWay.RemoteControlBase.Network;
using iWay.RemoteControlBase;

namespace iWay.RemoteControlClient.Connect
{
    public partial class ConnectWindow : Form
    {
        private Connector mConnector;
        private bool mIsConnected;

        public ConnectWindow()
        {
            InitializeComponent();
            mConnectTypeSelector.SelectedIndex = 0;

#if DEBUG
            mConnectAddressEditor.Text = "127.0.0.1";
            mConnectPasswordEditor.Text = "12345678";
#endif
        }

        public Connector Connector
        {
            get
            {
                return mConnector;
            }
        }

        public bool IsConnected
        {
            get
            {
                return mIsConnected;
            }
        }

        private void OnConnectSucceed(object sender, ConnectSucceedArgs e)
        {
            mIsConnected = true;
            Close();
        }

        private void OnConnectFail(object sender, ConnectFailArgs e)
        {
            mLoadingCover.Visible = false;

            mToolTip.ToolTipIcon = ToolTipIcon.Error;
            mToolTip.ToolTipTitle = "连接失败";
            mToolTip.IsBalloon = true;
            mToolTip.Show(e.Reason.Message, mConnectButton, 2000);
        }

        private void mConnectButton_Click(object sender, EventArgs e)
        {
            string host = mConnectAddressEditor.Text;
            if (host.Length == 0)
            {
                mToolTip.ToolTipIcon = ToolTipIcon.Error;
                mToolTip.ToolTipTitle = "输入有误";
                mToolTip.Show("请输入正确的地址。可以是IP地址或者域名。", mConnectAddressEditor, 2000);
                return;
            }
            int port = Consts.SERVER_LISTEN_PORT;
            string password = mConnectPasswordEditor.Text;
            if (password.Length == 0)
            {
                mToolTip.ToolTipIcon = ToolTipIcon.Error;
                mToolTip.ToolTipTitle = "输入有误";
                mToolTip.Show("请输入正确的密码。密码为8~16位字母或数字。", mConnectPasswordEditor, 2000);
                return;
            }
            int connectType = 0;
            switch (mConnectTypeSelector.SelectedIndex)
            {
                case 0:
                    connectType = ConnectType.TYPE_REMOTE_CONSOLE;
                    break;
                case 1:
                    connectType = ConnectType.TYPE_REMOTE_DESKTOP;
                    break;
                case 2:
                    connectType = ConnectType.TYPE_REMOTE_EXPLORER;
                    break;
            }

            mLoadingCover.LoadingText = "正在连接...";
            mLoadingCover.Visible = true;

            mConnector = new Connector(host, port, password, connectType);
            mConnector.SetForm(this);
            mConnector.SetConnectSucceedHandler(OnConnectSucceed);
            mConnector.SetConnectFailHandler(OnConnectFail);
            mConnector.BeginConnect();
        }
    }
}
