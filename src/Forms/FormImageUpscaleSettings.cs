using NVGE.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageUpscaleSettings : Form
    {
        private static string rdlevel = " -n 3", uplevel = " -s 2", usegpu = " -g default", blocksize = " -t 0", thread = "", format = "", model = "", vboutput = "", tta = "", syncgap = "", cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel +  uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;

        public FormImageUpscaleSettings()
        {
            InitializeComponent();
        }

        private void FormImageUpscaleSettings_Load(object sender, EventArgs e)
        {
            comboBox_GPU.Items.Clear();
            string[] CPUInfo = new string[3];
            SystemInfo.GetProcessorsInformation(CPUInfo);
            List<string> GPUNList = new();
            GPUNList = SystemInfo.GetGraphicsCardNamesInformation();

            foreach (var GPU in GPUNList)
            {
                if (GPU.Contains("Intel"))
                {
                    if (GPU.Contains("HD Graphics"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                    if (GPU.Contains("Iris"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                    if (GPU.Contains("Xe Graphics"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                }
                else if (GPU.Contains("Radeon"))
                {
                    if (GPU.Contains("Vega 11"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                    if (GPU.Contains("Vega 8"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                    if (GPU.Contains("Vega 7"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                    if (GPU.Contains("Vega 6"))
                    {
                        comboBox_GPU.Items.Add("iGPU [ " + GPU + " ]");
                    }
                }
                else
                {
                    comboBox_GPU.Items.Add("dGPU [ " + GPU + " ]");
                }
            }
            comboBox_GPU.Items.Add(Strings.SelectGPUAutoCaption);
            ArrayList array = ArrayList.Adapter(comboBox_GPU.Items);
            array.Reverse();

            

            Config.Load(Common.xmlpath);

            

            switch (int.Parse(Config.Entry["ConversionType"].Value))
            {
                case 0: // waifu2x
                    comboBox_Rdlevel.Enabled = true;
                    comboBox_syncgap.Enabled = false;
                    label1.Enabled = true;
                    label14.Enabled = false;
                    comboBox_engine.SelectedIndex = 0;

                    comboBox_Uplevel.Items.Clear();
                    comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);

                    switch (int.Parse(Config.Entry["Reduction"].Value))
                    {
                        case 0:
                            rdlevel = " -n -1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 1:
                            rdlevel = " -n 0";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 2:
                            rdlevel = " -n 1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 3:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 4:
                            rdlevel = " -n 3";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        default:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = 3;
                            break;
                    }

                    switch (int.Parse(Config.Entry["Scale"].Value))
                    {
                        case 0:
                            uplevel = " -s 1";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 1:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 2:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 3:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 4:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        default:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = 1;
                            break;
                    }

                    switch (int.Parse(Config.Entry["GPU"].Value))
                    {
                        case 0:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 1:
                            usegpu = " -g -1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 2:
                            usegpu = " -g 0";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 3:
                            usegpu = " -g 1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 4:
                            usegpu = " -g 2";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        default:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = 0;
                            break;
                    }

                    break;
                case 1: // cugan
                    comboBox_Rdlevel.Enabled = true;
                    comboBox_syncgap.Enabled = true;
                    label1.Enabled = true;
                    label14.Enabled = true;
                    comboBox_engine.SelectedIndex = 1;

                    comboBox_Uplevel.Items.Clear();
                    comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("3" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);

                    switch (int.Parse(Config.Entry["Reduction"].Value))
                    {
                        case 0:
                            rdlevel = " -n -1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 1:
                            rdlevel = " -n 0";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 2:
                            rdlevel = " -n 1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 3:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 4:
                            rdlevel = " -n 3";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        default:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = 3;
                            break;
                    }

                    switch (int.Parse(Config.Entry["Scale"].Value))
                    {
                        case 0:
                            uplevel = " -s 1";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 1:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 2:
                            uplevel = " -s 3";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 3:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 4:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        default:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = 1;
                            break;
                    }

                    switch (int.Parse(Config.Entry["GPU"].Value))
                    {
                        case 0:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 1:
                            usegpu = " -g -1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 2:
                            usegpu = " -g 0";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 3:
                            usegpu = " -g 1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 4:
                            usegpu = " -g 2";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        default:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = 0;
                            break;
                    }

                    switch (int.Parse(Config.Entry["SyncGap"].Value))
                    {
                        case 0:
                            syncgap = " -c 0";
                            comboBox_syncgap.SelectedIndex = int.Parse(Config.Entry["SyncGap"].Value);
                            break;
                        case 1:
                            syncgap = " -c 1";
                            comboBox_syncgap.SelectedIndex = int.Parse(Config.Entry["SyncGap"].Value);
                            break;
                        case 2:
                            syncgap = " -c 2";
                            comboBox_syncgap.SelectedIndex = int.Parse(Config.Entry["SyncGap"].Value);
                            break;
                        case 3:
                            syncgap = " -c 3";
                            comboBox_syncgap.SelectedIndex = int.Parse(Config.Entry["SyncGap"].Value);
                            break;
                        default:
                            syncgap = " -c 3";
                            comboBox_syncgap.SelectedIndex = 3;
                            break;
                    }

                    break;
                case 2: // esrgan
                    rdlevel = "";
                    comboBox_Rdlevel.Enabled = false;
                    comboBox_syncgap.Enabled = false;
                    label1.Enabled = false;
                    label14.Enabled = false;
                    comboBox_engine.SelectedIndex = 2;

                    comboBox_Uplevel.Items.Clear();
                    comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("12" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
                    comboBox_Uplevel.Items.Add("32" + Strings.UplevelCaption);

                    switch (int.Parse(Config.Entry["Scale"].Value))
                    {
                        case 0:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 1:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 2:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 3:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        default:
                            uplevel = " -s 4";
                            comboBox_Uplevel.SelectedIndex = 0;
                            break;
                    }

                    switch (int.Parse(Config.Entry["GPU"].Value))
                    {
                        case 0:
                            usegpu = " -g auto";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 1:
                            usegpu = " -g 0";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 2:
                            usegpu = " -g 1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 3:
                            usegpu = " -g 2";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        default:
                            usegpu = " -g auto";
                            comboBox_GPU.SelectedIndex = 0;
                            break;
                    }

                    break;
                default:
                    comboBox_Rdlevel.Enabled = true;
                    label1.Enabled = true;
                    comboBox_engine.SelectedIndex = 0;

                    switch (int.Parse(Config.Entry["Reduction"].Value))
                    {
                        case 0:
                            rdlevel = " -n -1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 1:
                            rdlevel = " -n 0";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 2:
                            rdlevel = " -n 1";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 3:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        case 4:
                            rdlevel = " -n 3";
                            comboBox_Rdlevel.SelectedIndex = int.Parse(Config.Entry["Reduction"].Value);
                            break;
                        default:
                            rdlevel = " -n 2";
                            comboBox_Rdlevel.SelectedIndex = 3;
                            break;
                    }

                    switch (int.Parse(Config.Entry["Scale"].Value))
                    {
                        case 0:
                            uplevel = " -s 1";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 1:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 2:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 3:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        case 4:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = int.Parse(Config.Entry["Scale"].Value);
                            break;
                        default:
                            uplevel = " -s 2";
                            comboBox_Uplevel.SelectedIndex = 1;
                            break;
                    }

                    switch (int.Parse(Config.Entry["GPU"].Value))
                    {
                        case 0:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 1:
                            usegpu = " -g -1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 2:
                            usegpu = " -g 0";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 3:
                            usegpu = " -g 1";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        case 4:
                            usegpu = " -g 2";
                            comboBox_GPU.SelectedIndex = int.Parse(Config.Entry["GPU"].Value);
                            break;
                        default:
                            usegpu = " -g default";
                            comboBox_GPU.SelectedIndex = 0;
                            break;
                    }
                    break;
            }

            if (int.Parse(Config.Entry["Blocksize"].Value) != 0)
            {
                textBox_Blocksize.Text = Config.Entry["Blocksize"].Value;
            }
            else
            {
                textBox_Blocksize.Text = "0";
            }

            switch (bool.Parse(Config.Entry["UpScaleDetail"].Value))
            {
                case true:
                    checkBox_updetail.Checked = true;
                    break;
                case false:
                    checkBox_updetail.Checked = false;
                    break;
            }

            switch (bool.Parse(Config.Entry["DestFolder"].Value))
            {
                case true:
                    checkBox_destfolder.Checked = true;
                    break;
                case false:
                    checkBox_destfolder.Checked = false;
                    break;
            }

            switch (bool.Parse(Config.Entry["IAdvanced"].Value))
            {
                case false:
                    checkBox_Advanced.Checked = false;
                    comboBox_Thread.Enabled = false;
                    comboBox_Format.Enabled = false;
                    comboBox_Model.Enabled = false;
                    checkBox_Verbose.Enabled = false;
                    checkBox_TTA.Enabled = false;
                    checkBox_pixel.Enabled = false;
                    textBox_CMD.Enabled = false;
                    textBox_CMD.Text = null;
                    break;
                case true:
                    checkBox_Advanced.Checked = true;
                    comboBox_Thread.Enabled = true;
                    comboBox_Format.Enabled = true;
                    comboBox_Model.Enabled = true;
                    checkBox_Verbose.Enabled = true;
                    checkBox_TTA.Enabled = true;
                    checkBox_pixel.Enabled = true;
                    textBox_CMD.Enabled = true;
                    break;
            }

            switch (int.Parse(Config.Entry["Thread"].Value))
            {
                case 0:
                    thread = " -j 1:2:2";
                    comboBox_Thread.SelectedIndex = int.Parse(Config.Entry["Thread"].Value);
                    break;
                case 1:
                    thread = " -j 1:2";
                    comboBox_Thread.SelectedIndex = int.Parse(Config.Entry["Thread"].Value);
                    break;
                case 2:
                    thread = " -j 2";
                    comboBox_Thread.SelectedIndex = int.Parse(Config.Entry["Thread"].Value);
                    break;
                case 3:
                    thread = " -j 2:2";
                    comboBox_Thread.SelectedIndex = int.Parse(Config.Entry["Thread"].Value);
                    break;
                case 4:
                    thread = " -j 2:2:2";
                    comboBox_Thread.SelectedIndex = int.Parse(Config.Entry["Thread"].Value);
                    break;
                default:
                    thread = " -j 1:2:2";
                    comboBox_Thread.SelectedIndex = 0;
                    break;
            }

            switch (int.Parse(Config.Entry["Format"].Value))
            {
                case 0:
                    format = " -f jpg";
                    comboBox_Format.SelectedIndex = int.Parse(Config.Entry["Format"].Value);
                    break;
                case 1:
                    format = " -f png";
                    comboBox_Format.SelectedIndex = int.Parse(Config.Entry["Format"].Value);
                    break;
                case 2:
                    format = " -f webp";
                    comboBox_Format.SelectedIndex = int.Parse(Config.Entry["Format"].Value);
                    break;
                case 3:
                    format = " -f png";
                    comboBox_Format.SelectedIndex = int.Parse(Config.Entry["Format"].Value);
                    break;
                case 4:
                    format = " -f png";
                    comboBox_Format.SelectedIndex = int.Parse(Config.Entry["Format"].Value);
                    break;
                default:
                    format = " -f png";
                    comboBox_Format.SelectedIndex = 1;
                    break;
            }

            ToolTip tt = new();

            switch (int.Parse(Config.Entry["ConversionType"].Value))
            {
                case 0: // waifu2x
                    
                    toolTip1.SetToolTip(comboBox_Model, Strings.waifu2xModelToolTipCaption);

                    comboBox_Model.Items.Clear();
                    comboBox_Model.Items.Add("CUnet (models-cunet)");
                    comboBox_Model.Items.Add("RGB (models-upconv_7_anime_style_art_rgb)");
                    comboBox_Model.Items.Add("Photo (models-upconv_7_photo)");

                    switch (int.Parse(Config.Entry["Model"].Value))
                    {
                        case 0:
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 1:
                            model = " -m models-upconv_7_anime_style_art_rgb";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 2:
                            model = " -m models-upconv_7_photo";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        default:
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = 0;
                            break;
                    }
                    break;
                case 1: // cugan

                    toolTip1.SetToolTip(comboBox_Model, Strings.cuganModelToolTipCaption);

                    comboBox_Model.Items.Clear();
                    comboBox_Model.Items.Add("SE (models-se)");
                    comboBox_Model.Items.Add("Nose (models-nose)");

                    switch (int.Parse(Config.Entry["Model"].Value))
                    {
                        case 0:
                            model = " -m models-se";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 1:
                            model = " -m models-nose";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        default:
                            model = " -m models-se";
                            comboBox_Model.SelectedIndex = 0;
                            break;
                    }
                    break;
                case 2: // esrgan

                    toolTip1.SetToolTip(comboBox_Model, Strings.realesrganModelToolTipCaption);

                    comboBox_Model.Items.Clear();
                    comboBox_Model.Items.Add("Real-ESRGAN (realesrgan-x4plus)");
                    comboBox_Model.Items.Add("Real-ESRGAN Photo (realesrnet-x4plus)");
                    comboBox_Model.Items.Add("Real-ESRGAN Anime (realesrgan-x4plus-anime)");

                    switch (int.Parse(Config.Entry["Model"].Value))
                    {
                        case 0:
                            model = " -n realesrgan-x4plus";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 1:
                            model = " -n realesrnet-x4plus";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 2:
                            model = " -n realesrgan-x4plus-anime";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        default:
                            model = " -n realesrgan-x4plus";
                            comboBox_Model.SelectedIndex = 0;
                            break;
                    }
                    break;
                default:

                    tt.SetToolTip(comboBox_Model, Strings.waifu2xModelToolTipCaption);

                    comboBox_Model.Items.Clear();
                    comboBox_Model.Items.Add("CUnet (models-cunet)");
                    comboBox_Model.Items.Add("RGB (models-upconv_7_anime_style_art_rgb)");
                    comboBox_Model.Items.Add("Photo (models-upconv_7_photo)");

                    switch (int.Parse(Config.Entry["Model"].Value))
                    {
                        case 0:
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 1:
                            model = " -m models-upconv_7_anime_style_art_rgb";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        case 2:
                            model = " -m models-upconv_7_photo";
                            comboBox_Model.SelectedIndex = int.Parse(Config.Entry["Model"].Value);
                            break;
                        default:
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = 0;
                            break;
                    }
                    break;
            }

            switch (bool.Parse(Config.Entry["Verbose"].Value))
            {
                case false:
                    vboutput = "";
                    checkBox_Verbose.Checked = false;
                    break;
                case true:
                    vboutput = " -v";
                    checkBox_Verbose.Checked = true;
                    break;
            }

            switch (bool.Parse(Config.Entry["TTA"].Value))
            {
                case false:
                    tta = "";
                    checkBox_TTA.Checked = false;
                    break;
                case true:
                    tta = " -x";
                    checkBox_TTA.Checked = true;
                    break;
            }

            switch (bool.Parse(Config.Entry["Pixel"].Value))
            {
                case false:
                    checkBox_pixel.Checked = false;
                    label10.Enabled = false;
                    label11.Enabled = false;
                    textBox_width.Enabled = false;
                    textBox_height.Enabled = false;
                    break;
                case true:
                    checkBox_pixel.Checked = true;
                    label10.Enabled = true;
                    label11.Enabled = true;
                    textBox_width.Enabled = true;
                    textBox_height.Enabled = true;
                    textBox_width.Text = int.Parse(Config.Entry["Pixel_width"].Value) switch
                    {
                        _ => Config.Entry["Pixel_width"].Value.ToString(),
                    };
                    textBox_height.Text = int.Parse(Config.Entry["Pixel_height"].Value) switch
                    {
                        _ => Config.Entry["Pixel_height"].Value.ToString(),
                    };
                    break;
            }

            if (Config.Entry["Param"].Value.Length != 0)
            {
                switch (bool.Parse(Config.Entry["IAdvanced"].Value))
                {
                    case false:
                        textBox_CMD.Text = RefleshParams();
                        break;
                    case true:
                        textBox_CMD.Text = Config.Entry["Param"].Value;
                        break;
                }
            }
            else
            {
                textBox_CMD.Text = RefleshParams();
            }

            return;
        }

        private void TextBox_Blocksize_KeyPress(object sender, KeyPressEventArgs e)
        {
            ToolTip tT1 = new(components);
            tT1.IsBalloon = true;
            tT1.ToolTipTitle = "許可されていない文字";
            tT1.ToolTipIcon = ToolTipIcon.Error;
            tT1.SetToolTip(textBox_Blocksize, "このフィールドには数値以外の値を入力することはできません。");

            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                tT1.Active = true;
                e.Handled = true;
            }

            tT1.Active = false;
            tT1.SetToolTip(textBox_Blocksize, null);
        }

        private void ComboBox_Rdlevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_Rdlevel.SelectedIndex)
                {
                    case 0:
                        rdlevel = " -n -1";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        rdlevel = " -n 0";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        rdlevel = " -n 1";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        rdlevel = " -n 2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        rdlevel = " -n 3";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        rdlevel = " -n 2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                }
            }
            else
            {
                switch (comboBox_Rdlevel.SelectedIndex)
                {
                    case 0:
                        rdlevel = " -n -1";
                        cmdparam = RefleshParams();
                        break;
                    case 1:
                        rdlevel = " -n 0";
                        cmdparam = RefleshParams();
                        break;
                    case 2:
                        rdlevel = " -n 1";
                        cmdparam = RefleshParams();
                        break;
                    case 3:
                        rdlevel = " -n 2";
                        cmdparam = RefleshParams();
                        break;
                    case 4:
                        rdlevel = " -n 3";
                        cmdparam = RefleshParams();
                        break;
                    default:
                        rdlevel = " -n 2";
                        cmdparam = RefleshParams();
                        break;
                }
            }
        }

        private void ComboBox_Uplevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_engine.SelectedIndex)
                {
                    case 0: // waifu2x
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 1: // cugan
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    uplevel = " -s 3";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 5:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 2: // esrgan
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                default:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (comboBox_engine.SelectedIndex)
                {
                    case 0:
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                            }
                        }
                        break;
                    case 1:
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                                case 2:
                                    uplevel = " -s 3";
                                    cmdparam = RefleshParams();
                                    break;
                                case 3:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    break;
                                case 4:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    break;
                                case 5:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                            }
                        }
                        break;
                    case 2:
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                default:
                                    uplevel = " -s 4";
                                    cmdparam = RefleshParams();
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0:
                                    uplevel = " -s 1";
                                    cmdparam = RefleshParams();
                                    break;
                                case 1:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                                default:
                                    uplevel = " -s 2";
                                    cmdparam = RefleshParams();
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void ComboBox_GPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = (string)comboBox_GPU.Items[1];
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_engine.SelectedIndex)
                {
                    case 0:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 1:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 2:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                }
            }
            else
            {
                switch (comboBox_engine.SelectedIndex)
                {
                    case 0:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 1:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    case 2:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            switch (comboBox_GPU.SelectedIndex)
                            {
                                case 0:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 1:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 0";
                                    }
                                    else
                                    {
                                        usegpu = " -g -1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 2:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 1";
                                    }
                                    else
                                    {
                                        usegpu = " -g 0";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 3:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g 2";
                                    }
                                    else
                                    {
                                        usegpu = " -g 1";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                case 4:
                                    if (!item.Contains("iGPU"))
                                    {
                                        usegpu = " -g default";
                                    }
                                    else
                                    {
                                        usegpu = " -g 2";
                                    }
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                default:
                                    usegpu = " -g default";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void TextBox_Blocksize_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                blocksize = " -t " + textBox_Blocksize.Text;
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                blocksize = " -t " + textBox_Blocksize.Text;
                cmdparam = RefleshParams();
            }
        }

        private void TextBox_width_KeyPress(object sender, KeyPressEventArgs e)
        {
            ToolTip tT1 = new(components);
            tT1.IsBalloon = true;
            tT1.ToolTipTitle = "許可されていない文字";
            tT1.ToolTipIcon = ToolTipIcon.Error;
            tT1.SetToolTip(textBox_Blocksize, "このフィールドには数値以外の値を入力することはできません。");

            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                tT1.Active = true;
                e.Handled = true;
            }

            tT1.Active = false;
            tT1.SetToolTip(textBox_Blocksize, null);
        }

        private void TextBox_height_KeyPress(object sender, KeyPressEventArgs e)
        {
            ToolTip tT1 = new(components);
            tT1.IsBalloon = true;
            tT1.ToolTipTitle = "許可されていない文字";
            tT1.ToolTipIcon = ToolTipIcon.Error;
            tT1.SetToolTip(textBox_Blocksize, "このフィールドには数値以外の値を入力することはできません。");

            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                tT1.Active = true;
                e.Handled = true;
            }

            tT1.Active = false;
            tT1.SetToolTip(textBox_Blocksize, null);
        }

        private void CheckBox_Advanced_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                comboBox_Thread.Enabled = true;
                comboBox_Format.Enabled = true;
                comboBox_Model.Enabled = true;
                checkBox_Verbose.Enabled = true;
                checkBox_TTA.Enabled = true;
                checkBox_pixel.Enabled = true;
                textBox_CMD.Enabled = true;
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                if (comboBox_Thread.SelectedIndex != 0)
                {
                    thread = " -j 1:2:2";
                    comboBox_Thread.SelectedIndex = 0;
                }
                if (comboBox_Format.SelectedIndex != 1)
                {
                    format = " -f png";
                    comboBox_Format.SelectedIndex = 1;
                }
                switch (comboBox_engine.SelectedIndex)
                {
                    case 0:
                        if (comboBox_Model.SelectedIndex != 0)
                        {
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = 0;
                        }
                        break;
                    case 1:
                        if (comboBox_Model.SelectedIndex != 0)
                        {
                            model = " -m models-se";
                            comboBox_Model.SelectedIndex = 0;
                        }
                        break;
                    case 2:
                        if (comboBox_Model.SelectedIndex != 0)
                        {
                            model = " -n realesrgan-x4plus";
                            comboBox_Model.SelectedIndex = 0;
                        }
                        break;
                    default:
                        if (comboBox_Model.SelectedIndex != 0)
                        {
                            model = " -m models-cunet";
                            comboBox_Model.SelectedIndex = 0;
                        }
                        break;
                }
                if (checkBox_Verbose.Checked != false)
                {
                    vboutput = "";
                    checkBox_Verbose.Checked = false;
                }
                if (checkBox_TTA.Checked != false)
                {
                    tta = "";
                    checkBox_TTA.Checked = false;
                }
                comboBox_Thread.Enabled = false;
                comboBox_Format.Enabled = false;
                comboBox_Model.Enabled = false;
                checkBox_Verbose.Enabled = false;
                checkBox_TTA.Enabled = false;
                checkBox_pixel.Enabled = false;
                textBox_CMD.Enabled = false;
                cmdparam = RefleshParams();
                textBox_CMD.Text = "";
            }
        }

        private void ComboBox_Thread_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_GPU.SelectedIndex <= 2)
            {
                switch (comboBox_Thread.SelectedIndex)
                {
                    case 0:
                        thread = " -j 1:2:2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        thread = " -j 1:2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        thread = " -j 2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        thread = " -j 2:2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        thread = " -j 2:2:2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        thread = " -j 1:2:2";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                }
            }
            else
            {

            }
        }

        private void RadioButton_waifu2x_CheckedChanged(object sender, EventArgs e)
        {
            rdlevel = " -n 2";
            comboBox_Rdlevel.Enabled = true;
            comboBox_Rdlevel.SelectedIndex = 3;
            label1.Enabled = true;
            comboBox_Uplevel.Items.Clear();
            comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
            comboBox_Uplevel.SelectedIndex = 1;

            comboBox_Model.Items.Clear();
            comboBox_Model.Items.Add("CUnet (models-cunet)");
            comboBox_Model.Items.Add("RGB (models-upconv_7_anime_style_art_rgb)");
            comboBox_Model.Items.Add("Photo (models-upconv_7_photo)");
            comboBox_Model.SelectedIndex = 0;
            toolTip1.SetToolTip(comboBox_Model, Strings.waifu2xModelToolTipCaption);
            model = " -m models-cunet";
            cmdparam = RefleshParams();
        }

        private void RadioButton_realesrgan_CheckedChanged(object sender, EventArgs e)
        {
            rdlevel = "";
            comboBox_Rdlevel.Enabled = false;
            label1.Enabled = false;
            comboBox_Uplevel.Items.Clear();
            comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("12" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
            comboBox_Uplevel.Items.Add("32" + Strings.UplevelCaption);
            comboBox_Uplevel.SelectedIndex = 0;

            comboBox_Model.Items.Clear();
            comboBox_Model.Items.Add("Real-ESRGAN (realesrgan-x4plus)");
            comboBox_Model.Items.Add("Real-ESRGAN Photo (realesrnet-x4plus)");
            comboBox_Model.Items.Add("Real-ESRGAN Anime (realesrgan-x4plus-anime)");
            comboBox_Model.SelectedIndex = 0;
            toolTip1.SetToolTip(comboBox_Model, Strings.realesrganModelToolTipCaption);
            model = " -n realesrgan-x4plus";
            cmdparam = RefleshParams();
        }

        private void ComboBox_engine_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_engine.SelectedIndex)
            {
                case 0:
                    {
                        rdlevel = " -n 2";
                        comboBox_Rdlevel.Enabled = true;
                        comboBox_Rdlevel.SelectedIndex = 3;
                        label1.Enabled = true;
                        syncgap = "";
                        comboBox_syncgap.Enabled = false;
                        label14.Enabled = false;
                        comboBox_Uplevel.Items.Clear();
                        comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
                        comboBox_Uplevel.SelectedIndex = 1;

                        comboBox_Model.Items.Clear();
                        comboBox_Model.Items.Add("CUnet (models-cunet)");
                        comboBox_Model.Items.Add("RGB (models-upconv_7_anime_style_art_rgb)");
                        comboBox_Model.Items.Add("Photo (models-upconv_7_photo)");
                        comboBox_Model.SelectedIndex = 0;
                        toolTip1.SetToolTip(comboBox_Model, Strings.waifu2xModelToolTipCaption);
                        model = " -m models-cunet";
                        cmdparam = RefleshParams();
                    }
                    break;
                case 1:
                    {
                        rdlevel = " -n 4";
                        comboBox_Rdlevel.Enabled = true;
                        comboBox_Rdlevel.SelectedIndex = 3;
                        label1.Enabled = true;
                        syncgap = " -c 3";
                        comboBox_syncgap.SelectedIndex = 3;
                        comboBox_syncgap.Enabled = true;
                        label14.Enabled = true;
                        comboBox_Uplevel.Items.Clear();
                        comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("3" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
                        comboBox_Uplevel.SelectedIndex = 1;

                        comboBox_Model.Items.Clear();
                        comboBox_Model.Items.Add("SE (models-se)");
                        comboBox_Model.Items.Add("Nose (models-nose)");
                        comboBox_Model.SelectedIndex = 0;
                        toolTip1.SetToolTip(comboBox_Model, Strings.cuganModelToolTipCaption);
                        model = " -m models-se";
                        cmdparam = RefleshParams();
                    }
                    break;
                case 2:
                    {
                        rdlevel = "";
                        comboBox_Rdlevel.Enabled = false;
                        label1.Enabled = false;
                        syncgap = "";
                        comboBox_syncgap.Enabled = false;
                        label14.Enabled = false;
                        comboBox_Uplevel.Items.Clear();
                        comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("12" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("32" + Strings.UplevelCaption);
                        comboBox_Uplevel.SelectedIndex = 0;

                        comboBox_Model.Items.Clear();
                        comboBox_Model.Items.Add("Real-ESRGAN (realesrgan-x4plus)");
                        comboBox_Model.Items.Add("Real-ESRGAN Photo (realesrnet-x4plus)");
                        comboBox_Model.Items.Add("Real-ESRGAN Anime (realesrgan-x4plus-anime)");
                        comboBox_Model.SelectedIndex = 0;
                        toolTip1.SetToolTip(comboBox_Model, Strings.realesrganModelToolTipCaption);
                        model = " -n realesrgan-x4plus";
                        cmdparam = RefleshParams();
                    }
                    break;
                default:
                    {
                        rdlevel = " -n 2";
                        comboBox_Rdlevel.Enabled = true;
                        comboBox_Rdlevel.SelectedIndex = 3;
                        label1.Enabled = true;
                        syncgap = "";
                        comboBox_syncgap.Enabled = false;
                        label14.Enabled = false;
                        comboBox_Uplevel.Items.Clear();
                        comboBox_Uplevel.Items.Add("1" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("2" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("4" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("8" + Strings.UplevelCaption);
                        comboBox_Uplevel.Items.Add("16" + Strings.UplevelCaption);
                        comboBox_Uplevel.SelectedIndex = 1;

                        comboBox_Model.Items.Clear();
                        comboBox_Model.Items.Add("CUnet (models-cunet)");
                        comboBox_Model.Items.Add("RGB (models-upconv_7_anime_style_art_rgb)");
                        comboBox_Model.Items.Add("Photo (models-upconv_7_photo)");
                        comboBox_Model.SelectedIndex = 0;
                        toolTip1.SetToolTip(comboBox_Model, Strings.waifu2xModelToolTipCaption);
                        model = " -m models-cunet";
                        cmdparam = RefleshParams();
                    }
                    break;
            }
        }

        private void ComboBox_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Format.SelectedIndex)
            {
                case 0:
                    format = " -f jpg";
                    cmdparam = RefleshParams();
                    textBox_CMD.Text = cmdparam;
                    break;
                case 1:
                    format = " -f png";
                    cmdparam = RefleshParams();
                    textBox_CMD.Text = cmdparam;
                    break;
                case 2:
                    format = " -f webp";
                    cmdparam = RefleshParams();
                    textBox_CMD.Text = cmdparam;
                    break;
                default:
                    format = " -f png";
                    cmdparam = RefleshParams();
                    textBox_CMD.Text = cmdparam;
                    break;
            }
        }

        private void ComboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_engine.SelectedIndex)
            {
                case 0:
                    {
                        switch (comboBox_Model.SelectedIndex)
                        {
                            case 0:
                                model = " -m models-cunet";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                            case 1:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -m models-upconv_7_anime_style_art_rgb";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            case 2:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -m models-upconv_7_photo";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            default:
                                model = " -m models-cunet";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                        }
                    }
                    break;
                case 1:
                    {
                        switch (comboBox_Model.SelectedIndex)
                        {
                            case 0:
                                model = " -m models-se";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                            case 1:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -m models-nose";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            default:
                                model = " -m models-se";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        switch (comboBox_Model.SelectedIndex)
                        {
                            case 0:
                                model = " -n realesrgan-x4plus";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                            case 1:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -n realesrnet-x4plus";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            case 2:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -n realesrgan-x4plus-anime";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            default:
                                model = " -n realesrgan-x4plus";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                        }
                    }
                    break;
                default:
                    {
                        switch (comboBox_Model.SelectedIndex)
                        {
                            case 0:
                                model = " -m models-cunet";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                            case 1:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -m models-upconv_7_anime_style_art_rgb";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            case 2:
                                if (comboBox_Uplevel.SelectedIndex == 1)
                                {
                                    model = " -m models-upconv_7_photo";
                                    cmdparam = RefleshParams();
                                    textBox_CMD.Text = cmdparam;
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            default:
                                model = " -m models-cunet";
                                cmdparam = RefleshParams();
                                textBox_CMD.Text = cmdparam;
                                break;
                        }
                    }
                    break;
            }
        }

        private void CheckBox_Verbose_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Verbose.Checked != false)
            {
                vboutput = " -v";
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                vboutput = "";
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
        }

        private void CheckBox_TTA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TTA.Checked != false)
            {
                tta = " -x";
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                tta = "";
                cmdparam = RefleshParams();
                textBox_CMD.Text = cmdparam;
            }
        }

        private void CheckBox_pixel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pixel.Checked != false)
            {
                label10.Enabled = true;
                label11.Enabled = true;
                textBox_width.Enabled = true;
                textBox_height.Enabled = true;
            }
            else
            {
                label10.Enabled = false;
                label11.Enabled = false;
                textBox_width.Enabled = false;
                textBox_height.Enabled = false;
                textBox_width.Text = null;
                textBox_height.Text = null;
            }
        }

        private void TextBox_CMD_TextChanged(object sender, EventArgs e)
        {
            textBox_CMD.Text = cmdparam;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (textBox_Blocksize.Text == "")
            {
                MessageBox.Show(Strings.ErrorBlockNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox_Blocksize.Text, "[^0-9]") != false)
            {
                MessageBox.Show(Strings.ErrorBlockNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (uint.Parse(textBox_Blocksize.Text) != 0 && uint.Parse(textBox_Blocksize.Text) < 21)
            {
                MessageBox.Show(Strings.ErrorBlockIncorrect, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (uint.Parse(textBox_Blocksize.Text) != 0 && uint.Parse(textBox_Blocksize.Text) > 5000)
            {
                MessageBox.Show(Strings.ErrorBlockIncorrect2, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            switch (comboBox_engine.SelectedIndex)
            {
                case 0: // waifu2x
                    switch (comboBox_Model.SelectedIndex)
                    {
                        case 0: // cunet
                            break;
                        case 1: // rgb
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0: // x1
                                    MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                case 1: // x2
                                    break;
                                case 2: // x4
                                    break;
                                case 3: // x8
                                    break;
                            }
                            break;
                        case 2: // photo
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0: // x1
                                    MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                case 1: // x2
                                    break;
                                case 2: // x4
                                    break;
                                case 3: // x8
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case 1: // cugan
                    switch (comboBox_Model.SelectedIndex)
                    {
                        case 0: // se
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0: // x1
                                    break;
                                case 1: // x2
                                    break;
                                case 2: // x3
                                    switch (comboBox_Rdlevel.SelectedIndex)
                                    {
                                        case 0: // -1
                                            break;
                                        case 1: // 0
                                            break;
                                        case 2: // 1
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 3: // 2
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 4: // 3
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case 3: // x4
                                    switch (comboBox_Rdlevel.SelectedIndex)
                                    {
                                        case 0: // -1
                                            break;
                                        case 1: // 0
                                            break;
                                        case 2: // 1
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 3: // 2
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 4: // 3
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                            break;
                        case 1: // nose
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0: // x1
                                    return;
                                case 1: // x2
                                    switch (comboBox_Rdlevel.SelectedIndex)
                                    {
                                        case 0: // -1
                                            break;
                                        case 1: // 0
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 2: // 1
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 3: // 2
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 4: // 3
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        default:
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                    }
                                    break;
                                case 2: // x3
                                    return;
                                case 3: // x4
                                    return;
                            }
                            break;
                        default:
                            switch (comboBox_Uplevel.SelectedIndex)
                            {
                                case 0: // x1
                                    break;
                                case 1: // x2
                                    break;
                                case 2: // x3
                                    switch (comboBox_Rdlevel.SelectedIndex)
                                    {
                                        case 0: // -1
                                            break;
                                        case 1: // 0
                                            break;
                                        case 2: // 1
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 3: // 2
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 4: // 3
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case 3: // x4
                                    switch (comboBox_Rdlevel.SelectedIndex)
                                    {
                                        case 0: // -1
                                            break;
                                        case 1: // 0
                                            break;
                                        case 2: // 1
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 3: // 2
                                            MessageBox.Show(Strings.ModelNotSupportedOptionCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        case 4: // 3
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                            }
                            break;
                    }
                    break;
                case 2: // esrgan
                    break;
            }
            
            if (checkBox_pixel.Checked != false)
            {
                if (textBox_width.Text == "")
                {
                    MessageBox.Show(Strings.PixelValueNotSetCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox_width.Text, "[^0-9]") != false)
                {
                    MessageBox.Show(Strings.PixelValueNotSetCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox_height.Text == "")
                {
                    MessageBox.Show(Strings.PixelValueNotSetCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox_height.Text, "[^0-9]") != false)
                {
                    MessageBox.Show(Strings.PixelValueNotSetCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Config.Entry["ConversionType"].Value = comboBox_engine.SelectedIndex.ToString();
            Config.Entry["Reduction"].Value = comboBox_Rdlevel.SelectedIndex.ToString();
            Config.Entry["Scale"].Value = comboBox_Uplevel.SelectedIndex.ToString();
            Config.Entry["GPU"].Value = comboBox_GPU.SelectedIndex.ToString();
            Config.Entry["SyncGap"].Value = comboBox_syncgap.SelectedIndex.ToString();

            switch (textBox_Blocksize.Text)
            {
                case "0":
                    Config.Entry["Blocksize"].Value = "0";
                    break;
                case "00":
                    Config.Entry["Blocksize"].Value = textBox_Blocksize.Text.Replace("00", "0");
                    break;
                case "000":
                    Config.Entry["Blocksize"].Value = textBox_Blocksize.Text.Replace("000", "0");
                    break;
                case "0000":
                    Config.Entry["Blocksize"].Value = textBox_Blocksize.Text.Replace("0000", "0");
                    break;
                default:
                    if (textBox_Blocksize.Text.StartsWith("0"))
                    {
                        Config.Entry["Blocksize"].Value = "0";
                    }
                    else
                    {
                        Config.Entry["Blocksize"].Value = textBox_Blocksize.Text;
                    }
                    break;
            }

            switch (checkBox_updetail.Checked)
            {
                case true:
                    Config.Entry["UpScaleDetail"].Value = "true";
                    break;
                case false:
                    Config.Entry["UpScaleDetail"].Value = "false";
                    break;
            }

            switch (checkBox_destfolder.Checked)
            {
                case true:
                    Config.Entry["DestFolder"].Value = "true";
                    break;
                case false:
                    Config.Entry["DestFolder"].Value = "false";
                    break;
            }

            switch (checkBox_Advanced.Checked)
            {
                case false:
                    Config.Entry["IAdvanced"].Value = "false";
                    break;
                case true:
                    Config.Entry["IAdvanced"].Value = "true";
                    break;
            }

            Config.Entry["Thread"].Value = comboBox_Thread.SelectedIndex.ToString();
            Config.Entry["Format"].Value = comboBox_Format.SelectedIndex.ToString();
            Config.Entry["Model"].Value = comboBox_Model.SelectedIndex.ToString();

            switch (checkBox_Verbose.Checked)
            {
                case false:
                    Config.Entry["Verbose"].Value = "false";
                    break;
                case true:
                    Config.Entry["Verbose"].Value = "true";
                    break;
            }
            
            switch (checkBox_TTA.Checked)
            {
                case false:
                    Config.Entry["TTA"].Value = "false";
                    break;
                case true:
                    Config.Entry["TTA"].Value = "true";
                    break;
            }

            switch (checkBox_pixel.Checked)
            {
                case false:
                    Config.Entry["Pixel"].Value = "false";
                    break;
                case true:
                    Config.Entry["Pixel"].Value = "true";
                    break;
            }

            Config.Entry["Pixel_width"].Value = textBox_width.Text switch
            {
                _ => textBox_width.Text,
            };
            Config.Entry["Pixel_height"].Value = textBox_height.Text switch
            {
                _ => textBox_height.Text,
            };
            Config.Entry["Param"].Value = cmdparam;

            Config.Save(Common.xmlpath);

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private string RefleshParams()
        {
            switch (comboBox_engine.SelectedIndex)
            {
                case 0:
                    {
                        return "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    }
                case 1:
                    {
                        return "realcugan-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + syncgap + thread + format + model + vboutput + tta;
                    }
                case 2:
                    {
                        return "realesrgan-ncnn-vulkan -i $InFile -o $OutFile" + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    }
                default:
                    {
                        return "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    }
            }
        }
    }
}
