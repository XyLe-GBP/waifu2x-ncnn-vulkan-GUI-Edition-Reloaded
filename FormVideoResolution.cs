using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    public partial class FormVideoResolution : Form
    {
        public FormVideoResolution()
        {
            InitializeComponent();
        }

        private void FormVideoResolution_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            comboBox1.SelectedIndex = 6;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("QUXGA (3200x2400)");
            comboBox1.Items.Add("4M (2304x1728)");
            comboBox1.Items.Add("QXGA (2048x1536)");
            comboBox1.Items.Add("UXGA (1600x1200)");
            comboBox1.Items.Add("HV (1440x1080)");
            comboBox1.Items.Add("SXGA+ (1400x1050)");
            comboBox1.Items.Add("QVGA (1280x960)");
            comboBox1.Items.Add("XGA+ (1152x864)");
            comboBox1.Items.Add("XGA (1024x768)");
            comboBox1.Items.Add("SVGA (800x600)");
            comboBox1.Items.Add("PAL (768x576)");
            comboBox1.Items.Add("NTSC (720x483)");
            comboBox1.Items.Add("VGA (640x480)");
            comboBox1.Items.Add("CGA (640x200)");
            comboBox1.Items.Add("QVGA (320x240)");
            comboBox1.Items.Add("QQVGA (160x120)");
            comboBox1.Items.Add("sQCIF (128x96)");
            comboBox1.SelectedIndex = 8;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("16K UHV 8640p (15360x8640)");
            comboBox1.Items.Add("8K SHV 4320p (7680x4320)");
            comboBox1.Items.Add("4K UHD 2160p (3840x2160)");
            comboBox1.Items.Add("WQHD+ (3200x1800)");
            comboBox1.Items.Add("WQHD 1440p (2560x1440)");
            comboBox1.Items.Add("QWXGA (2048x1152)");
            comboBox1.Items.Add("FHD 1080p (1920x1080)");
            comboBox1.Items.Add("WXGA++ (1600x900)");
            comboBox1.Items.Add("FWXGA (1366x768)");
            comboBox1.Items.Add("HD 720p (1280x720)");
            comboBox1.Items.Add("WSVGA (1024x576)");
            comboBox1.Items.Add("FWVGA (854x480)");
            comboBox1.Items.Add("One-Seg (320x180)");
            comboBox1.SelectedIndex = 6;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked != false)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0: // QUXGA
                        Common.VRParam = "-i $InFile -s 3200x2400 $OutFile";
                        break;
                    case 1: // 4M
                        Common.VRParam = "-i $InFile -s 2304x1728 $OutFile";
                        break;
                    case 2: // QXGA
                        Common.VRParam = "-i $InFile -s 2048x1536 $OutFile";
                        break;
                    case 3: // UXGA
                        Common.VRParam = "-i $InFile -s 1600x1200 $OutFile";
                        break;
                    case 4: // HV
                        Common.VRParam = "-i $InFile -s 1440x1080 $OutFile";
                        break;
                    case 5: // SXGA+
                        Common.VRParam = "-i $InFile -s 1400x1050 $OutFile";
                        break;
                    case 6: // QVGA
                        Common.VRParam = "-i $InFile -s 1280x960 $OutFile";
                        break;
                    case 7: // XGA+
                        Common.VRParam = "-i $InFile -s 1152x864 $OutFile";
                        break;
                    case 8: // XGA
                        Common.VRParam = "-i $InFile -s 1024x768 $OutFile";
                        break;
                    case 9: // SVGA
                        Common.VRParam = "-i $InFile -s 800x600 $OutFile";
                        break;
                    case 10: // PAL
                        Common.VRParam = "-i $InFile -s 768x576 $OutFile";
                        break;
                    case 11: // NTSC
                        Common.VRParam = "-i $InFile -s 720x483 $OutFile";
                        break;
                    case 12: // VGA
                        Common.VRParam = "-i $InFile -s 640x480 $OutFile";
                        break;
                    case 13: // CGA
                        Common.VRParam = "-i $InFile -s 640x200 $OutFile";
                        break;
                    case 14: // QVGA
                        Common.VRParam = "-i $InFile -s 320x240 $OutFile";
                        break;
                    case 15: // QQVGA
                        Common.VRParam = "-i $InFile -s 160x120 $OutFile";
                        break;
                    case 16: // sQCIF
                        Common.VRParam = "-i $InFile -s 128x96 $OutFile";
                        break;
                    default: // Unknown
                        Common.VRParam = "";
                        break;
                }
                return;
            }
            else if (radioButton2.Checked != false)
            {
                switch (comboBox1.SelectedIndex)
                {
                    case 0: // 16K UHV
                        Common.VRParam = "-i $InFile -s 15360x8640 $OutFile";
                        break;
                    case 1: // 8K SHV
                        Common.VRParam = "-i $InFile -s 7680x4320 $OutFile";
                        break;
                    case 2: // 4K UHD
                        Common.VRParam = "-i $InFile -s 3840x2160 $OutFile";
                        break;
                    case 3: // WQHD+
                        Common.VRParam = "-i $InFile -s 3200x1800 $OutFile";
                        break;
                    case 4: // WQHD
                        Common.VRParam = "-i $InFile -s 2560x1440 $OutFile";
                        break;
                    case 5: // QWXGA
                        Common.VRParam = "-i $InFile -s 2048x1152 $OutFile";
                        break;
                    case 6: // FHD
                        Common.VRParam = "-i $InFile -s 1920x1080 $OutFile";
                        break;
                    case 7: // WXGA++
                        Common.VRParam = "-i $InFile -s 1600x900 $OutFile";
                        break;
                    case 8: // FWXGA
                        Common.VRParam = "-i $InFile -s 1366x768 $OutFile";
                        break;
                    case 9: // HD
                        Common.VRParam = "-i $InFile -s 1280x720 $OutFile";
                        break;
                    case 10: // WSVGA
                        Common.VRParam = "-i $InFile -s 1024x576 $OutFile";
                        break;
                    case 11: // FWVGA
                        Common.VRParam = "-i $InFile -s 854x480 $OutFile";
                        break;
                    case 12: // One-Seg
                        Common.VRParam = "-i $InFile -s 320x180 $OutFile";
                        break;
                    default: // Unknown
                        Common.VRParam = "";
                        break;
                }
                return;
            }
            else
            {

            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Common.VRCFlag = 1;
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Common.VRCFlag = 0;
            Close();
        }
    }
}
