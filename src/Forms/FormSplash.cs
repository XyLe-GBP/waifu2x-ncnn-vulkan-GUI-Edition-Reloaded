using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormSplash : Form
    {
        #region NetworkCommon
        private static readonly HttpClientHandler handler = new()
        {
            UseProxy = false,
            UseCookies = false
        };
        private static readonly HttpClient stream = new(handler);
        #endregion

        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.waifu2x_splash;
        }

        public string ProgressMsg
        {
            set
            {
                label_log.Text = value;
            }
        }
    }
}
