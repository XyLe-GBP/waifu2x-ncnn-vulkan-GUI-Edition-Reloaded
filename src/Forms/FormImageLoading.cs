using ImageMagick;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageLoading : Form
    {
        private readonly bool IsImage = false;
        private readonly string inpath, outpath;
        private readonly Image image;
        public FormImageLoading(string inputpath, string outputpath, bool IsImageObj = false, Image image = null)
        {
            inpath = inputpath;
            outpath = outputpath;
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
            await Task.Run(() => Main());
            Close();
        }

        private void Main()
        {
            if (IsImage == true)
            {
                using var image = new MagickImage(ImageConvert.ImageToByteArray(this.image));
                image.Write(outpath, MagickFormat.Png32);
            }
            else
            {
                using var image = new MagickImage(inpath);
                image.Write(outpath, MagickFormat.Png32);
            }
        }
    }
}
