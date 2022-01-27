using PrivateProfile;
using System;
using System.IO;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageUpscaleDetail : Form
    {
        public FormImageUpscaleDetail()
        {
            InitializeComponent();
        }

        private string ext;
        private int pos = 0;

        private void FormImageUpscaleDetail_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            int reduction = ini.GetInt("IMAGE_SETTINGS", "REDUCTION_INDEX"), model = ini.GetInt("IMAGE_SETTINGS", "MODEL_INDEX"), gpu = ini.GetInt("IMAGE_SETTINGS", "GPU_INDEX"), scale = ini.GetInt("IMAGE_SETTINGS", "UPSCALE_INDEX"), blksize = ini.GetInt("IMAGE_SETTINGS", "BLOCKSIZE_INDEX"), thread = ini.GetInt("IMAGE_SETTINGS", "THREAD_INDEX"), format = ini.GetInt("IMAGE_SETTINGS", "FORMAT_INDEX"), vbo = ini.GetInt("IMAGE_SETTINGS", "VBO_INDEX"), tta = ini.GetInt("IMAGE_SETTINGS", "TTA_INDEX");

            label_sec.Text = Common.timeSpan.TotalSeconds.ToString() + "s";

            switch (reduction)
            {
                case 0:
                    label_rdl.Text = "No Reduction";
                    break;
                case 1:
                    label_rdl.Text = "Level 0";
                    break;
                case 2:
                    label_rdl.Text = "Level 1";
                    break;
                case 3:
                    label_rdl.Text = "Level 2";
                    break;
                case 4:
                    label_rdl.Text = "Level 3";
                    break;
                default:
                    label_rdl.Text = "Unknown";
                    break;
            }

            switch (scale)
            {
                case 0:
                    label_scale.Text = "x1";
                    break;
                case 1:
                    label_scale.Text = "x2";
                    break;
                default:
                    label_scale.Text = "Unknown";
                    break;
            }

            switch (gpu)
            {
                case 0:
                    label_gpu.Text = "Autodetect";
                    break;
                case 1:
                    label_gpu.Text = "iGPU (CPU)";
                    break;
                case 2:
                    label_gpu.Text = "dGPU (GPU 0)";
                    break;
                case 3:
                    label_gpu.Text = "dGPU (GPU 1)";
                    break;
                case 4:
                    label_gpu.Text = "dGPU (GPU 2)";
                    break;
                default:
                    label_gpu.Text = "Unknown";
                    break;
            }

            switch (blksize)
            {
                case 0:
                    label_blks.Text = "Autodetect";
                    break;
                default:
                    label_blks.Text = blksize.ToString() + "Blocks";
                    break;
            }

            switch (format)
            {
                case 0:
                    ext = ".jpg";
                    break;
                case 1:
                    ext = ".png";
                    break;
                case 2:
                    ext = ".webp";
                    label_webp.Visible = true;
                    break;
                case 3:
                    ext = ".ico";
                    break;
                default:
                    ext = ".png";
                    break;
            }

            switch (vbo)
            {
                case 0:
                    label_vbs.Visible = false;
                    label_tta.Visible = false;
                    label_thread.Visible = false;
                    label_model.Visible = false;
                    label_fmt.Visible = false;
                    label7.Visible = false;
                    label9.Visible = false;
                    label12.Visible = false;
                    label13.Visible = false;
                    label14.Visible = false;
                    break;
                case 1:
                    label16.Visible = false;
                    label_vbs.Text = "Enabled";

                    switch (model)
                    {
                        case 0:
                            label_model.Text = "CUnet (models-cunet)";
                            break;
                        case 1:
                            label_model.Text = "RGB (models-upconv_7_anime_style_art_rgb)";
                            break;
                        case 2:
                            label_model.Text = "Photo (models-upconv_7_photo)";
                            break;
                        default:
                            label_model.Text = "Unknown";
                            break;
                    }

                    switch (format)
                    {
                        case 0:
                            ext = ".jpg";
                            label_fmt.Text = "JPEG";
                            break;
                        case 1:
                            ext = ".png";
                            label_fmt.Text = "PNG";
                            break;
                        case 2:
                            ext = ".webp";
                            label_fmt.Text = "WEBP";
                            break;
                        case 3:
                            ext = ".ico";
                            label_fmt.Text = "ICO";
                            break;
                        default:
                            ext = ".png";
                            label_fmt.Text= "Unknown";
                            break;
                    }

                    switch (thread)
                    {
                        case 0:
                            label_thread.Text = "1:2:2";
                            break;
                        case 1:
                            label_thread.Text = "1:2";
                            break;
                        case 2:
                            label_thread.Text = "2";
                            break;
                        case 3:
                            label_thread.Text = "2:2";
                            break;
                        case 4:
                            label_thread.Text = "2:2:2";
                            break;
                        default:
                            label_thread.Text = "Unknown";
                            break;
                    }

                    switch (tta)
                    {
                        case 0:
                            label_tta.Text = "Disabled";
                            break;
                        case 1:
                            label_tta.Text = "Enabled";
                            break;
                        default:
                            label_tta.Text = "Unknown";
                            break;
                    }
                    break;
            }

            if (Common.ImageFile.Length <= 1)
            {
                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ".png");
                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ext);
                PictureboxRefleshed();
            }
            else
            {
                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ".png");
                pictureBox_UpscaledImage.ImageLocation = pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ext);
                pos = 0;
                button_next.Enabled = true;
                button_prev.Enabled = false;
                PictureboxRefleshed();
            }
        }

        private void PictureBox_SourceImage_Click(object sender, EventArgs e)
        {
            if (Common.ImageFile.Length <= 1)
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ".png"));
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
            else
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + Common.ImageFileName[pos].Replace(Common.ImageFileExt[pos], ".png"));
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
        }

        private void PictureBox_UpscaledImage_Click(object sender, EventArgs e)
        {
            if (Common.ImageFile.Length <= 1)
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ext));
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
            else
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[pos].Replace(Common.ImageFileExt[pos], ext));
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
        }

        private void Button_prev_Click(object sender, EventArgs e)
        {
            if (pos == Common.ImageFile.Length)
            {
                pos--;
                button_next.Enabled = true;
                PictureboxRefleshed();
            }
            else
            {
                if (pos == 1)
                {
                    pos--;
                    button_prev.Enabled = false;
                    PictureboxRefleshed();
                }
                else if (pos == Common.ImageFile.Length - 1)
                {
                    pos--;
                    button_next.Enabled = true;
                    PictureboxRefleshed();
                }
                else
                {
                    pos--;
                    PictureboxRefleshed();
                }
            }
        }

        private void Button_next_Click(object sender, EventArgs e)
        {
            if (pos == 0)
            {
                pos++;
                button_prev.Enabled = true;
                PictureboxRefleshed();
            }
            else
            {
                if (pos == Common.ImageFile.Length - 2)
                {
                    pos++;
                    button_next.Enabled = false;
                    PictureboxRefleshed();
                }
                else
                {
                    pos++;
                    PictureboxRefleshed();
                }
            }
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            Common.ImgDetFlag = 0;
            Close();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Common.ImgDetFlag = 1;
            Close();
        }

        private void PictureboxRefleshed()
        {
            pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + Common.ImageFileName[pos].Replace(Common.ImageFileExt[pos], ".png");
            pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[pos].Replace(Common.ImageFileExt[pos], ext);
            pictureBox_SourceImage.Refresh();
            pictureBox_UpscaledImage.Refresh();
        }

        private void label_webp_Click(object sender, EventArgs e)
        {
            if (Common.ImageFile.Length <= 1)
            {
                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + "\"" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ext) + "\"");
            }
            else
            {
                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + "\"" + Common.ImageFileName[pos].Replace(Common.ImageFileExt[pos], ext) + "\"");
            }
        }
    }
}
