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
            switch (bool.Parse(Config.Entry["IsNoSplash"].Value))
            {
                case true:
                    {
                        checkBox_hidesplash.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_hidesplash.Checked = false;
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
            switch (bool.Parse(Config.Entry["IsFixedLocation"].Value))
            {
                case true:
                    {
                        radioButton_fs_every.Checked = false;
                        radioButton_fs_fixed.Checked = true;
                        groupBox_fspref.Enabled = true;
                        label_fs_dest.Enabled = true;
                        textBox_fs_dest.Enabled = true;
                        textBox_fs_dest.Text = Config.Entry["FixedLocationPath"].Value;
                        button_fs_browsedest.Enabled = true;
                        break;
                    }
                case false:
                    {
                        radioButton_fs_every.Checked = true;
                        radioButton_fs_fixed.Checked = false;
                        groupBox_fspref.Enabled = false;
                        label_fs_dest.Enabled = false;
                        textBox_fs_dest.Enabled = false;
                        textBox_fs_dest.Text = "";
                        button_fs_browsedest.Enabled = false;
                        break;
                    }
            }
            switch (bool.Parse(Config.Entry["IsShowDirectory"].Value))
            {
                case true:
                    {
                        checkBox_fs_dest.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_fs_dest.Checked = false;
                        break;
                    }
            }
            switch (bool.Parse(Config.Entry["IsPNGMethod"].Value))
            {
                case true:
                    {
                        checkBox_a_png_enable.Checked = true;
                        radioButton_a_png_default.Enabled = false;
                        radioButton_a_png_8.Enabled = true;
                        radioButton_a_png_24.Enabled = true;
                        radioButton_a_png_32.Enabled = true;
                        radioButton_a_png_48.Enabled = true;
                        radioButton_a_png_64.Enabled = true;
                        switch (int.Parse(Config.Entry["PNGMethodType"].Value))
                        {
                            case 1:
                                radioButton_a_png_8.Checked = true;
                                break;
                            case 2:
                                radioButton_a_png_24.Checked = true;
                                break;
                            case 3:
                                radioButton_a_png_32.Checked = true;
                                break;
                            case 4:
                                radioButton_a_png_48.Checked = true;
                                break;
                            case 5:
                                radioButton_a_png_64.Checked = true;
                                break;
                            default:
                                radioButton_a_png_default.Checked = true;
                                break;
                        }
                        break;
                    }
                case false:
                    {
                        checkBox_a_png_enable.Checked = false;
                        radioButton_a_png_8.Enabled = false;
                        radioButton_a_png_24.Enabled = false;
                        radioButton_a_png_32.Enabled = false;
                        radioButton_a_png_48.Enabled = false;
                        radioButton_a_png_64.Enabled = false;
                        radioButton_a_png_default.Enabled = true;
                        radioButton_a_png_default.Checked = true;
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

            if (radioButton_fs_fixed.Checked)
            {
                if (!string.IsNullOrWhiteSpace(textBox_fs_dest.Text))
                {
                    Config.Entry["IsFixedLocation"].Value = "true";
                    Config.Entry["FixedLocationPath"].Value = textBox_fs_dest.Text;
                }
                else
                {
                    MessageBox.Show("destination folder is not selected.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Config.Entry["IsFixedLocation"].Value = "false";
                Config.Entry["FixedLocationPath"].Value = "";
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
            if (checkBox_hidesplash.Checked != false)
            {
                Config.Entry["IsNoSplash"].Value = "true";
            }
            else
            {
                Config.Entry["IsNoSplash"].Value = "false";
            }
            if (checkBox_fs_dest.Checked)
            {
                Config.Entry["IsShowDirectory"].Value = "true";
            }
            else
            {
                Config.Entry["IsShowDirectory"].Value = "false";
            }
            if (checkBox_a_png_enable.Checked)
            {
                Config.Entry["IsPNGMethod"].Value = "true";
                if (radioButton_a_png_8.Checked)
                {
                    Config.Entry["PNGMethodType"].Value = "1";
                }
                if (radioButton_a_png_24.Checked)
                {
                    Config.Entry["PNGMethodType"].Value = "2";
                }
                if (radioButton_a_png_32.Checked)
                {
                    Config.Entry["PNGMethodType"].Value = "3";
                }
                if (radioButton_a_png_48.Checked)
                {
                    Config.Entry["PNGMethodType"].Value = "4";
                }
                if (radioButton_a_png_64.Checked)
                {
                    Config.Entry["PNGMethodType"].Value = "5";
                }
            }
            else
            {
                Config.Entry["IsPNGMethod"].Value = "false";
                Config.Entry["PNGMethodType"].Value = "0";
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

        private void radioButton_fs_every_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_fs_every.Checked)
            {
                groupBox_fspref.Enabled = false;
                label_fs_dest.Enabled = false;
                textBox_fs_dest.Enabled = false;
                textBox_fs_dest.Text = "";
                button_fs_browsedest.Enabled = false;
            }
            else
            {
                groupBox_fspref.Enabled = true;
                label_fs_dest.Enabled = true;
                textBox_fs_dest.Enabled = true;
                textBox_fs_dest.Text = "";
                button_fs_browsedest.Enabled = true;
            }
        }

        private void radioButton_fs_fixed_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_fs_fixed.Checked)
            {
                groupBox_fspref.Enabled = true;
                label_fs_dest.Enabled = true;
                textBox_fs_dest.Enabled = true;
                textBox_fs_dest.Text = "";
                button_fs_browsedest.Enabled = true;
            }
            else
            {
                groupBox_fspref.Enabled = false;
                label_fs_dest.Enabled = false;
                textBox_fs_dest.Enabled = false;
                textBox_fs_dest.Text = "";
                button_fs_browsedest.Enabled = false;
            }
        }

        private void button_fs_browsedest_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new()
            {
                ShowNewFolderButton = true,
                InitialDirectory = "",
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBox_fs_dest.Text = fbd.SelectedPath;
            }
            else
            {
                return;
            }
        }

        private void checkBox_a_png_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_a_png_enable.Checked)
            {
                radioButton_a_png_8.Enabled = true;
                radioButton_a_png_24.Enabled = true;
                radioButton_a_png_32.Enabled = true;
                radioButton_a_png_48.Enabled = true;
                radioButton_a_png_64.Enabled = true;
                radioButton_a_png_default.Enabled = false;
                radioButton_a_png_32.Checked = true;
            }
            else
            {
                radioButton_a_png_8.Enabled = false;
                radioButton_a_png_24.Enabled = false;
                radioButton_a_png_32.Enabled = false;
                radioButton_a_png_48.Enabled = false;
                radioButton_a_png_64.Enabled = false;
                radioButton_a_png_default.Enabled = true;
                radioButton_a_png_default.Checked = true;
            }
        }
    }
}
