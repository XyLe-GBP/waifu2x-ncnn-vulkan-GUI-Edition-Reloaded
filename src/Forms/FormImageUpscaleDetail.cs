using NVGE.Localization;
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
        //private int[] sourcesize = new int[2];

        private void FormImageUpscaleDetail_Load(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);
            int reduction = int.Parse(Config.Entry["Reduction"].Value), useengine = int.Parse(Config.Entry["ConversionType"].Value), model = int.Parse(Config.Entry["Model"].Value), gpu = int.Parse(Config.Entry["GPU"].Value), scale = int.Parse(Config.Entry["Scale"].Value), blksize = int.Parse(Config.Entry["Blocksize"].Value), thread = int.Parse(Config.Entry["Thread"].Value), format = int.Parse(Config.Entry["Format"].Value);
            bool vbo = bool.Parse(Config.Entry["Verbose"].Value), tta = bool.Parse(Config.Entry["TTA"].Value);

            label_sec.Text = Common.timeSpan.TotalSeconds.ToString() + "s";

            switch (useengine)
            {
                case 0:
                    label_rdl.Text = reduction switch
                    {
                        0 => "No Reduction",
                        1 => "Level 0",
                        2 => "Level 1",
                        3 => "Level 2",
                        4 => "Level 3",
                        _ => "Unknown",
                    };
                    label_scale.Text = scale switch
                    {
                        0 => "x1",
                        1 => "x2",
                        2 => "x4",
                        3 => "x8",
                        4 => "x16",
                        _ => "Unknown",
                    };
                    label_gpu.Text = gpu switch
                    {
                        0 => "Autodetect",
                        1 => "iGPU (CPU)",
                        2 => "dGPU (GPU 0)",
                        3 => "dGPU (GPU 1)",
                        4 => "dGPU (GPU 2)",
                        _ => "Unknown",
                    };
                    break;
                case 1:
                    label_rdl.Text = reduction switch
                    {
                        0 => "No Reduction",
                        1 => "Level 0",
                        2 => "Level 1",
                        3 => "Level 2",
                        4 => "Level 3",
                        _ => "Unknown",
                    };
                    label_scale.Text = scale switch
                    {
                        0 => "x1",
                        1 => "x2",
                        2 => "x3",
                        3 => "x4",
                        4 => "x8",
                        5 => "x16",
                        _ => "Unknown",
                    };
                    label_gpu.Text = gpu switch
                    {
                        0 => "Autodetect",
                        1 => "iGPU (CPU)",
                        2 => "dGPU (GPU 0)",
                        3 => "dGPU (GPU 1)",
                        4 => "dGPU (GPU 2)",
                        _ => "Unknown",
                    };
                    break;
                case 2:
                    label_rdl.Text = reduction switch
                    {
                        _ => "Null",
                    };
                    label_scale.Text = scale switch
                    {
                        0 => "x4",
                        1 => "x8",
                        2 => "x16",
                        3 => "x32",
                        _ => "Unknown",
                    };
                    label_gpu.Text = gpu switch
                    {
                        0 => "Autodetect",
                        1 => "dGPU (GPU 0)",
                        2 => "dGPU (GPU 1)",
                        3 => "dGPU (GPU 2)",
                        _ => "Unknown",
                    };
                    break;
                default:
                    label_rdl.Text = reduction switch
                    {
                        0 => "No Reduction",
                        1 => "Level 0",
                        2 => "Level 1",
                        3 => "Level 2",
                        4 => "Level 3",
                        _ => "Unknown",
                    };
                    label_scale.Text = scale switch
                    {
                        0 => "x1",
                        1 => "x2",
                        2 => "x4",
                        3 => "x8",
                        4 => "x16",
                        _ => "Unknown",
                    };
                    label_gpu.Text = gpu switch
                    {
                        0 => "Autodetect",
                        1 => "iGPU (CPU)",
                        2 => "dGPU (GPU 0)",
                        3 => "dGPU (GPU 1)",
                        4 => "dGPU (GPU 2)",
                        _ => "Unknown",
                    };
                    break;
            }

            label_blks.Text = blksize switch
            {
                0 => "Autodetect",
                _ => blksize.ToString() + "Blocks",
            };

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
                case 4:
                    ext = Common.ManualImageFormat;
                    break;
                default:
                    ext = ".png";
                    break;
            }

            switch (vbo)
            {
                case false:
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
                case true:
                    label16.Visible = false;
                    label_vbs.Text = "Enabled";

                    label_model.Text = useengine switch
                    {
                        0 => model switch
                        {
                            0 => "CUnet (models-cunet)",
                            1 => "RGB (models-upconv_7_anime_style_art_rgb)",
                            2 => "Photo (models-upconv_7_photo)",
                            _ => "Unknown",
                        },
                        1 => model switch
                        {
                            0 => "SE (models-se)",
                            1 => "Nose (models-nose)",
                            _ => "Unknown",
                        },
                        2 => model switch
                        {
                            0 => "Real-ESRGAN (realesrgan-x4plus)",
                            1 => "Real-ESRGAN Photo (realesrnet-x4plus)",
                            2 => "Real-ESRGAN Anime (realesrgan-x4plus-anime)",
                            _ => "Unknown",
                        },
                        _ => model switch
                        {
                            0 => "CUnet (models-cunet)",
                            1 => "RGB (models-upconv_7_anime_style_art_rgb)",
                            2 => "Photo (models-upconv_7_photo)",
                            _ => "Unknown",
                        },
                    };
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
                        case 4:
                            ext = Common.ManualImageFormat;
                            label_fmt.Text = ext + " (Custom)";
                            break;
                        default:
                            ext = ".png";
                            label_fmt.Text= "Unknown";
                            break;
                    }

                    label_thread.Text = thread switch
                    {
                        0 => "1:2:2",
                        1 => "1:2",
                        2 => "2",
                        3 => "2:2",
                        4 => "2:2:2",
                        _ => "Unknown",
                    };
                    switch (tta)
                    {
                        case false:
                            label_tta.Text = "Disabled";
                            break;
                        case true:
                            label_tta.Text = "Enabled";
                            break;
                    }
                    break;
            }

            if (Common.ImageFile.Length <= 1)
            {
                string dest1 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ".png");
                string dest2 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ext);

                /*File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1);
                File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2);

                ImageConvert.GetImageSize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, sourcesize);

                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1));
                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2));*/

                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;

                switch (format)
                {
                    case 2:
                        {
                            label_NotSupported.Visible = false;
                            label_webp.Visible = true;

                            PictureboxRefleshed();
                        }
                        break;
                    case 4:
                        {
                            if (ext.ToUpper() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".BMP" && ext.ToUpper() != ".WEBP")
                            {
                                label_NotSupported.Visible = true;
                                label_webp.Visible = false;
                            }
                            else if (ext.ToUpper() == ".WEBP")
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = true;
                            }
                            else
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = false;

                                /*File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext);
                                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext));*/

                                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext;
                            }

                            PictureboxRefleshed();
                        }
                        break;
                    default:
                        {
                            label_NotSupported.Visible = false;
                            pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2;

                            PictureboxRefleshed();
                        }
                        break;
                }
            }
            else
            {
                string dest1 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ".png");
                string dest2 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ext);

                /*File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1);
                File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2);
                int[] sourcesize = new int[2];

                ImageConvert.GetImageSize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, sourcesize);

                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1));
                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2));*/

                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;

                if (ext.ToUpper() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".BMP" && ext.ToUpper() != ".WEBP")
                {
                    label_NotSupported.Visible = true;
                    label_webp.Visible = false;
                }
                else if (ext.ToUpper() == ".WEBP")
                {
                    label_NotSupported.Visible = false;
                    label_webp.Visible = true;
                }
                else
                {
                    label_NotSupported.Visible = false;
                    label_webp.Visible = false;

                    /*ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2));*/

                    pictureBox_UpscaledImage.ImageLocation = pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2;
                }

                pos = 0;
                button_next.Enabled = true;
                button_prev.Enabled = false;

                PictureboxRefleshed();
            }
        }

        private void PictureBox_SourceImage_Click(object sender, EventArgs e)
        {
            string dest1 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ".png");
            string dest2 = Common.ReplaceForRegex(Common.ImageFileName[pos], Common.ImageFileExt[pos], ".png");
            if (Common.ImageFile.Length <= 1)
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1);
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
            else
            {
                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                formShowPicture.ShowDialog();
                formShowPicture.Dispose();
            }
        }

        private void PictureBox_UpscaledImage_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);
            int format = int.Parse(Config.Entry["Format"].Value);

            string dest1 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ext);
            string dest2 = Common.ReplaceForRegex(Common.ImageFileName[pos], Common.ImageFileExt[pos], ext);

            /*int[] sourcesize = new int[2];

            ImageConvert.GetImageSize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, sourcesize);*/

            switch (format)
            {
                case 2:
                    {
                        break;
                    }
                case 4:
                    {
                        if (ext.ToUpper() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".BMP" && ext.ToUpper() != ".WEBP")
                        {
                            MessageBox.Show("Since the converted image format '" + ext + "' is not supported, previewing in the viewer is not available.", Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (ext.ToUpper() == ".WEBP")
                        {
                            if (Common.ImageFile.Length <= 1)
                            {
                                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + "\"" + ext + "\"");
                            }
                            else
                            {
                                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + "\"" + dest2 + "\"");
                            }
                        }
                        else
                        {
                            if (Common.ImageFile.Length <= 1)
                            {
                                /*File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext);
                                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_final" + ext));*/

                                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext);
                                formShowPicture.ShowDialog();
                                formShowPicture.Dispose();
                            }
                            else
                            {
                                /*File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2);
                                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2));*/

                                FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2);
                                formShowPicture.ShowDialog();
                                formShowPicture.Dispose();
                            }
                        }
                    }
                    break;
                default:
                    {
                        if (Common.ImageFile.Length <= 1)
                        {
                            FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest1);
                            formShowPicture.ShowDialog();
                            formShowPicture.Dispose();
                        }
                        else
                        {
                            FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2);
                            formShowPicture.ShowDialog();
                            formShowPicture.Dispose();
                        }
                    }
                    break;
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
            Config.Load(Common.xmlpath);
            int format = int.Parse(Config.Entry["Format"].Value);

            string dest1 = Common.ReplaceForRegex(Common.ImageFileName[pos], Common.ImageFileExt[pos], ".png");
            string dest2 = Common.ReplaceForRegex(Common.ImageFileName[pos], Common.ImageFileExt[pos], ext);

            /*ImageConvert.GetImageSize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, sourcesize);

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1))
            {
                File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1);
                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\prv_" + dest1));
            }
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2))
            {
                File.Copy(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2);
                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2, (int)(sourcesize[0] * 0.5), (int)(sourcesize[1] * 0.5), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\prv_" + dest2));
            }*/

            switch (format)
            {
                case 2:
                    {
                        label_NotSupported.Visible = false;
                        label_webp.Visible = true;
                    }
                    break;
                case 4:
                    {
                        if (Common.ImageFile.Length <= 1)
                        {
                            if (ext.ToUpper() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".BMP" && ext.ToUpper() != ".WEBP")
                            {
                                label_NotSupported.Visible = true;
                                label_webp.Visible = false;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.Image = null;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                            else if (ext.ToUpper() == ".WEBP")
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = true;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                            else
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = false;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + ext;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                        }
                        else
                        {
                            if (ext.ToUpper() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".BMP" && ext.ToUpper() != ".WEBP")
                            {
                                label_NotSupported.Visible = true;
                                label_webp.Visible = false;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.Image = null;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                            else if (ext.ToUpper() == ".WEBP")
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = true;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                            else
                            {
                                label_NotSupported.Visible = false;
                                label_webp.Visible = false;
                                pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                                pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2;
                                pictureBox_SourceImage.Refresh();
                                pictureBox_UpscaledImage.Refresh();
                            }
                        }
                    }
                    break;
                default:
                    {
                        label_NotSupported.Visible = false;
                        pictureBox_UpscaledImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dest2;
                        pictureBox_SourceImage.ImageLocation = Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest1;
                        
                        pictureBox_SourceImage.Refresh();
                        pictureBox_UpscaledImage.Refresh();
                    }
                    break;
            }
        }

        private void Label_webp_Click(object sender, EventArgs e)
        {
            string dest1 = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ext);
            string dest2 = Common.ReplaceForRegex(Common.ImageFileName[pos], Common.ImageFileExt[pos], ext);

            if (Common.ImageFile.Length <= 1)
            {
                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + "\"" + dest1 + "\"");
            }
            else
            {
                Common.OpenURI(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + "\"" + dest2 + "\"");
            }
        }
    }
}
