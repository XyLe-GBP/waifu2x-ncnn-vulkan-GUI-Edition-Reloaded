using System;
using System.Drawing;
using System.IO;
using System.Net;
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
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                string url = "https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/raw/master/Properties/waifu2x-splash.png";
                Task<Stream> st = stream.GetStreamAsync(url);
                Bitmap bitmap = new(st.Result);

                BackgroundImage = bitmap;
            }
            else
            {
                BackgroundImage = Properties.Resources.waifu2x_splash;
            }
            
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
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
