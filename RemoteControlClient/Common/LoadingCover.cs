using System.Windows.Forms;

namespace iWay.RemoteControlClient.Common
{
    public partial class LoadingCover : UserControl
    {
        public LoadingCover()
        {
            InitializeComponent();
        }

        public string LoadingText
        {
            get
            {
                return mLoadingLabel.Text;
            }
            set
            {
                mLoadingLabel.Text = value;
            }
        }
    }
}
