using NVGE.Localization;
using System;
using System.IO;
using System.Net.Http;
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

        //public string[] OSInfo { get; set; }
        //public string[] CPUInfo { get; set; }
        //public string[] GPUInfo { get; set; }

        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
            BackgroundImage = Properties.Resources.waifu2x_splash;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            //Main();
        }

        /*private void Main()
        {
            foreach (var files in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\res", "*", SearchOption.AllDirectories))
            {
                FileInfo fi = new(files);
                label_log.Text = string.Format(Strings.SplashFormFileCaption, fi.Name);
                Refresh();
            }
            string[] oi = new string[17];
            string[] ci = new string[3];
            string[] gi = new string[3];
            SystemInfo.GetSystemInformation(oi);
            SystemInfo.GetProcessorsInformation(ci);
            SystemInfo.GetVideoControllerInformation(gi);
            OSInfo = oi;
            CPUInfo = ci;
            GPUInfo = gi;
        }*/

        public string ProgressMsg
        {
            set
            {
                label_log.Text = value;
            }
        }
    }
}
