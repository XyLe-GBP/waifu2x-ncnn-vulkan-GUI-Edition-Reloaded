using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;

namespace NVGE
{
    public partial class FormVideoLoading : Form
    {
        private readonly string inpath, outpath;
        public FormVideoLoading(string inputpath, string outputpath)
        {
            inpath = inputpath;
            outpath = outputpath;

            InitializeComponent();
        }

        private async void FormVideoLoading_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 50;
            await Task.Run(Main);
            Close();
        }

        private async void Main()
        {
            if (File.Exists(outpath))
            {
                Common.bimg?.Dispose();
                Common.cimg?.Dispose();
                Common.graph?.Dispose();
                File.Delete(outpath);
            }
            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(inpath, outpath, TimeSpan.FromSeconds(1.0d));
            await conversion.Start();
            Common.IsVideoThumbnailGenerated = true;
        }
    }
}
