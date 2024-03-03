using ImageMagick;
using NVGE.Localization;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageLoading : Form
    {
        private readonly bool IsImage = false, IsMethod = false;
        private readonly string inpath, outpath;
        private readonly Image image;
        private readonly int pngmethod = 0;
        public FormImageLoading(string inputpath, string outputpath, bool IsImageObj = false, Image image = null)
        {
            Config.Load(Common.xmlpath);

            inpath = inputpath;
            outpath = outputpath;
            IsMethod = bool.Parse(Config.Entry["IsPNGMethod"].Value);
            pngmethod = int.Parse(Config.Entry["PNGMethodType"].Value);

            if (IsImageObj == true)
            {
                if (image != null)
                {
                    IsImage = true;
                    this.image = image;
                }
                else
                {
                    MessageBox.Show("Object is not set to an object instance.", Localization.Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
            }
            
            InitializeComponent();
        }

        private async void FormImageLoading_Load(object sender, System.EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            await Task.Run(Main);
            Close();
        }

        private void Main()
        {
            if (IsImage == true)
            {
                using var image = new MagickImage(ImageConvert.ImageToByteArray(this.image));
                if (IsMethod)
                {
                    switch (pngmethod)
                    {
                        case 0:
                            {
                                image.Write(outpath, MagickFormat.Png32);
                            }
                            break;
                        case 1:
                            {
                                image.Write(outpath, MagickFormat.Png8);
                            }
                            break;
                        case 2:
                            {
                                image.Write(outpath, MagickFormat.Png24);
                            }
                            break;
                        case 3:
                            {
                                image.Write(outpath, MagickFormat.Png32);
                            }
                            break;
                        case 4:
                            {
                                image.Write(outpath, MagickFormat.Png48);
                            }
                            break;
                        case 5:
                            {
                                image.Write(outpath, MagickFormat.Png64);
                            }
                            break;
                        default:
                            {
                                image.Write(outpath, MagickFormat.Png00);
                            }
                            break;
                    }
                }
                else
                {
                    image.Write(outpath, MagickFormat.Png32);
                }
            }
            else
            {
                using var image = new MagickImage(inpath);
                image.Depth = 16;
                if (IsMethod)
                {
                    switch (pngmethod)
                    {
                        case 0:
                            {
                                image.Write(outpath, MagickFormat.Png32);
                            }
                            break;
                        case 1:
                            {
                                image.Write(outpath, MagickFormat.Png8);
                            }
                            break;
                        case 2:
                            {
                                image.Write(outpath, MagickFormat.Png24);
                            }
                            break;
                        case 3:
                            {
                                image.Write(outpath, MagickFormat.Png32);
                            }
                            break;
                        case 4:
                            {
                                image.Write(outpath, MagickFormat.Png48);
                            }
                            break;
                        case 5:
                            {
                                image.Write(outpath, MagickFormat.Png64);
                            }
                            break;
                        default:
                            {
                                image.Write(outpath, MagickFormat.Png00);
                            }
                            break;
                    }
                }
                else
                {
                    image.Write(outpath, MagickFormat.Png32);
                }
            }
        }

    }
}
