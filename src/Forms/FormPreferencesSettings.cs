using NVGE.Localization;
using System;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormPreferencesSettings : Form
    {
        public FormPreferencesSettings()
        {
            InitializeComponent();
        }

        private void FormPreferencesSettings_Load(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            switch (bool.Parse(Config.Entry["CheckUpdateWithStartup"].Value))
            {
                case true:
                    {
                        checkBox_checkupdate.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_checkupdate.Checked = false;
                        break;
                    }
            }
            switch (bool.Parse(Config.Entry["CheckUpdateFFWithStartup"].Value))
            {
                case true:
                    {
                        checkBox_checkupdateff.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_checkupdateff.Checked = false;
                        break;
                    }
            }
            switch (bool.Parse(Config.Entry["CustomSplashImage"].Value))
            {
                case true:
                    {
                        checkBox_splashImage.Checked = true;
                        label_imagepath.Enabled = true;
                        textBox_imagepath.Enabled = true;
                        button_browsecimg.Enabled = true;
                        textBox_imagepath.Text = Config.Entry["SplashImagePath"].Value;
                        break;
                    }
                case false:
                    {
                        checkBox_splashImage.Checked = false;
                        label_imagepath.Enabled = false;
                        textBox_imagepath.Enabled = false;
                        button_browsecimg.Enabled = false;
                        break;
                    }
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (checkBox_splashImage.Checked != false)
            {
                if (!string.IsNullOrWhiteSpace(textBox_imagepath.Text))
                {
                    Config.Entry["CustomSplashImage"].Value = "true";
                    Config.Entry["SplashImagePath"].Value = textBox_imagepath.Text;
                }
                else
                {
                    MessageBox.Show(Strings.SplashImagePathInvalidCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Config.Entry["CustomSplashImage"].Value = "false";
                Config.Entry["SplashImagePath"].Value = "";
            }
            if (checkBox_checkupdate.Checked != false)
            {
                Config.Entry["CheckUpdateWithStartup"].Value = "true";
            }
            else
            {
                Config.Entry["CheckUpdateWithStartup"].Value = "false";
            }
            if (checkBox_checkupdateff.Checked != false)
            {
                Config.Entry["CheckUpdateFFWithStartup"].Value = "true";
            }
            else
            {
                Config.Entry["CheckUpdateFFWithStartup"].Value = "false";
            }

            Config.Save(Common.xmlpath);

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CheckBox_splashImage_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_splashImage.Checked != false)
            {
                label_imagepath.Enabled = true;
                textBox_imagepath.Enabled = true;
                button_browsecimg.Enabled = true;
            }
            else
            {
                label_imagepath.Enabled = false;
                textBox_imagepath.Enabled = false;
                textBox_imagepath.Text = null;
                button_browsecimg.Enabled = false;
            }
        }

        private void Button_browsecimg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = "Bitmap (*.bmp)|*.bmp;|Portable Network Graphics (*.png)|*.png;|Joint Photographic Experts Group (*.jpg, *.jpeg)|*.jpg;*.jpeg;|Tag Image File Format (*.tif, *.tiff)|*.tif;*.tiff|All Supported Files|*.bmp;*.png;*.jpg;*.jpeg;*.tif;*.tiff",
                FilterIndex = 5,
                Title = Strings.OpenImageCaption,
                Multiselect = false,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var img = System.Drawing.Image.FromFile(ofd.FileName);
                if (img.Width != 490 || img.Height != 330)
                {
                    MessageBox.Show(Strings.SplashImageSizeInvalidCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox_imagepath.Text = null;
                    return;
                }

                textBox_imagepath.Text = ofd.FileName;
            }
            else
            {
                return;
            }
        }
    }
}
