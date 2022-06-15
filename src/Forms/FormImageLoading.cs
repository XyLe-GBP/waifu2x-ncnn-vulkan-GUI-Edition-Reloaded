using ImageMagick;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageLoading : Form
    {
        private readonly string inpath, outpath;
        public FormImageLoading(string inputpath, string outputpath)
        {
            inpath = inputpath;
            outpath = outputpath;
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
            using var image = new MagickImage(inpath);
            image.Write(outpath, MagickFormat.Png);
        }
    }
}
