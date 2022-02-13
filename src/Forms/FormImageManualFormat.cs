using System;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageManualFormat : Form
    {
        public FormImageManualFormat()
        {
            InitializeComponent();
        }

        private void FormImageManualFormat_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    Common.ManualImageFormat = ".bmp";
                    Common.ManualImageFormatFilter = "Bitmap Image(*.bmp)|*.bmp";
                    break;
                case 1:
                    Common.ManualImageFormat = ".eps";
                    Common.ManualImageFormatFilter = "Encapsulated PostScript File/Format(*.eps)|*.eps";
                    break;
                case 2:
                    Common.ManualImageFormat = ".webp";
                    Common.ManualImageFormatFilter = "Google WebP(*.webp)|*.webp";
                    break;
                case 3:
                    Common.ManualImageFormat = ".gif";
                    Common.ManualImageFormatFilter = "Graphics Interchange Format(*.gif)|*.gif";
                    break;
                case 4:
                    Common.ManualImageFormat = ".ico";
                    Common.ManualImageFormatFilter = "Icon(*.ico)|*.ico";
                    break;
                case 5:
                    Common.ManualImageFormat = ".jpg";
                    Common.ManualImageFormatFilter = "Joint Photographic Experts Group(*.jpg)|*.jpg";
                    break;
                case 6:
                    Common.ManualImageFormat = ".png";
                    Common.ManualImageFormatFilter = "Portable Network Graphics(*.png)|*.png";
                    break;
                case 7:
                    Common.ManualImageFormat = ".pict";
                    Common.ManualImageFormatFilter = "QuickDraw Picture(*.pict)|*.pict";
                    break;
                case 8:
                    Common.ManualImageFormat = ".svg";
                    Common.ManualImageFormatFilter = "Scalable Vector Graphics(*.svg)|*.svg";
                    break;
                case 9:
                    Common.ManualImageFormat = ".tif";
                    Common.ManualImageFormatFilter = "Tagged Image File Format(*.tif)|*.tif";
                    break;
                default:
                    break;
            }
            Close();
        }
    }
}
