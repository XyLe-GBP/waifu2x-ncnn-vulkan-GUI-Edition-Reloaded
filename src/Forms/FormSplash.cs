using System;
using System.ComponentModel;
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
            ModernWinFormsTheme.Apply(this);
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            switch (bool.Parse(Config.Entry["CustomSplashImage"].Value))
            {
                case true:
                    {
                        Bitmap bimg = new(Properties.Resources.waifu2x_splash);
                        Bitmap cimg = new(Config.Entry["SplashImagePath"].Value);
                        Graphics g = Graphics.FromImage(cimg);
                        g.DrawImage(bimg, 0, 0, bimg.Width, bimg.Height);
                        BackgroundImage = cimg;
                        break;
                    }
                case false:
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
                        break;
                    }
            }
            
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProgressMsg
        {
            set
            {
                label_log.Text = value;
            }
        }
    }
}
