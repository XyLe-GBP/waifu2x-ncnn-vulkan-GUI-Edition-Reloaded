using NVGE.Localization;
using PrivateProfile;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            label_FFVersion.Text = ini.GetString("FFMPEG", "LATEST_VERSION", "Unknown");
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                pictureBox1.ImageLocation = "https://avatars.githubusercontent.com/u/59692068?v=4";
            }
            else
            {
                pictureBox1.Image = Properties.Resources._2021_9_13_18_29_59_Upscaled;
            }
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
                Bitmap canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                Graphics g = Graphics.FromImage(canvas);
                GraphicsPath gp = new GraphicsPath();
                gp.AddEllipse(g.VisibleClipBounds);
                Region rgn = new Region(gp);
                pictureBox1.Region = rgn;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.UnExpectedError, ex.ToString()), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://github.com/XyLe-GBP");
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://xyle-official.com");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://github.com/dlemstra/Magick.NET");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://ffmpeg.org/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://github.com/shimat/opencvsharp");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Common.OpenURI("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-gui-edition-reloaded");
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
