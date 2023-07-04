using System;
using System.Windows.Forms;

namespace NVGE.src.Forms
{
    public partial class FormImageConvertTarget : Form
    {
        public FormImageConvertTarget()
        {
            InitializeComponent();
        }

        private void FormImageConvertTarget_Load(object sender, EventArgs e)
        {

        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            switch (comboBox_Extension.SelectedIndex)
            {
                case 0:
                    Common.ImageConversionExtension = ".bmp";
                    break;
                case 1:
                    Common.ImageConversionExtension = ".eps";
                    break;
                case 2:
                    Common.ImageConversionExtension = ".webp";
                    break;
                case 3:
                    Common.ImageConversionExtension = ".gif";
                    break;
                case 4:
                    Common.ImageConversionExtension = ".ico";
                    break;
                case 5:
                    Common.ImageConversionExtension = ".jpeg";
                    break;
                case 6:
                    Common.ImageConversionExtension = ".png";
                    break;
                case 7:
                    Common.ImageConversionExtension = ".pict";
                    break;
                case 8:
                    Common.ImageConversionExtension = ".svg";
                    break;
                case 9:
                    Common.ImageConversionExtension = ".tif";
                    break;
                default:
                    Common.ImageConversionExtension = "";
                    break;
            }
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Common.ImageConversionExtension = "";
            Close();
        }
    }
}
