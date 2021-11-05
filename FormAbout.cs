using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
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
            pictureBox1.ImageLocation = "https://avatars.githubusercontent.com/u/59692068?v=4";
            label_FFVersion.Text = ini.GetString("FFMPEG", "LATEST_VERSION", "Unknown");
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

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
