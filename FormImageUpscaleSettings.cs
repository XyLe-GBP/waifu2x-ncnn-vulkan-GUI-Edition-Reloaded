using PrivateProfile;
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
    public partial class FormImageUpscaleSettings : Form
    {
        private static string rdlevel = " -n 3", uplevel = " -s 2", usegpu = " -g default", blocksize = " -t 0", thread = "", format = "", model = "", vboutput = "", tta = "", cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel +  uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;

        public FormImageUpscaleSettings()
        {
            InitializeComponent();
        }

        private void FormImageUpscaleSettings_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            int rdi = ini.GetInt("IMAGE_SETTINGS", "REDUCTION_INDEX", 65535), upi = ini.GetInt("IMAGE_SETTINGS", "UPSCALE_INDEX", 65535), gpui = ini.GetInt("IMAGE_SETTINGS", "GPU_INDEX", 65535), advi = ini.GetInt("IMAGE_SETTINGS", "ADVANCED_INDEX", 65535), tri = ini.GetInt("IMAGE_SETTINGS", "THREAD_INDEX", 65535), fi = ini.GetInt("IMAGE_SETTINGS", "FORMAT_INDEX", 65535), mi = ini.GetInt("IMAGE_SETTINGS", "MODEL_INDEX", 65535), vboi = ini.GetInt("IMAGE_SETTINGS", "VBO_INDEX", 65535), ti = ini.GetInt("IMAGE_SETTINGS", "TTA_INDEX", 65535);
            string bsi = ini.GetString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", ""), pi = ini.GetString("IMAGE_SETTINGS", "PARAMS", "");

            if (rdi != 65535)
            {
                switch (rdi)
                {
                    case 0:
                        rdlevel = " -n -1";
                        comboBox_Rdlevel.SelectedIndex = rdi;
                        break;
                    case 1:
                        rdlevel = " -n 0";
                        comboBox_Rdlevel.SelectedIndex = rdi;
                        break;
                    case 2:
                        rdlevel = " -n 1";
                        comboBox_Rdlevel.SelectedIndex = rdi;
                        break;
                    case 3:
                        rdlevel = " -n 2";
                        comboBox_Rdlevel.SelectedIndex = rdi;
                        break;
                    case 4:
                        rdlevel = " -n 3";
                        comboBox_Rdlevel.SelectedIndex = rdi;
                        break;
                    default:
                        rdlevel = " -n 2";
                        comboBox_Rdlevel.SelectedIndex = 3;
                        break;
                }
            }
            else
            {
                rdlevel = " -n 3";
                comboBox_Rdlevel.SelectedIndex = 3;
            }

            if (upi != 65535)
            {
                switch (upi)
                {
                    case 0:
                        uplevel = " -s 1";
                        comboBox_Uplevel.SelectedIndex = upi;
                        break;
                    case 1:
                        uplevel = " -s 2";
                        comboBox_Uplevel.SelectedIndex = upi;
                        break;
                    default:
                        uplevel = " -s 2";
                        comboBox_Uplevel.SelectedIndex = 1;
                        break;
                }
            }
            else
            {
                uplevel = " -s 2";
                comboBox_Uplevel.SelectedIndex = 1;
            }

            if (gpui != 65535)
            {
                switch (gpui)
                {
                    case 0:
                        usegpu = " -g default";
                        comboBox_GPU.SelectedIndex = gpui;
                        break;
                    case 1:
                        usegpu = " -g -1";
                        comboBox_GPU.SelectedIndex = gpui;
                        break;
                    case 2:
                        usegpu = " -g 0";
                        comboBox_GPU.SelectedIndex = gpui;
                        break;
                    case 3:
                        usegpu = " -g 1";
                        comboBox_GPU.SelectedIndex = gpui;
                        break;
                    case 4:
                        usegpu = " -g 2";
                        comboBox_GPU.SelectedIndex = gpui;
                        break;
                    default:
                        usegpu = " -g default";
                        comboBox_GPU.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                usegpu = " -g default";
                comboBox_GPU.SelectedIndex = 0;
            }

            if (bsi != "")
            {
                textBox_Blocksize.Text = bsi;
            }
            else
            {
                textBox_Blocksize.Text = "0";
            }

            if (advi != 65535)
            {
                switch (advi)
                {
                    case 0:
                        checkBox_Advanced.Checked = false;
                        comboBox_Thread.Enabled = false;
                        comboBox_Format.Enabled = false;
                        comboBox_Model.Enabled = false;
                        checkBox_Verbose.Enabled = false;
                        checkBox_TTA.Enabled = false;
                        textBox_CMD.Enabled = false;
                        textBox_CMD.Text = "";
                        break;
                    case 1:
                        checkBox_Advanced.Checked = true;
                        comboBox_Thread.Enabled = true;
                        comboBox_Format.Enabled = true;
                        comboBox_Model.Enabled = true;
                        checkBox_Verbose.Enabled = true;
                        checkBox_TTA.Enabled = true;
                        textBox_CMD.Enabled = true;
                        break;
                    default:
                        checkBox_Advanced.Checked = false;
                        comboBox_Thread.Enabled = false;
                        comboBox_Format.Enabled = false;
                        comboBox_Model.Enabled = false;
                        checkBox_Verbose.Enabled = false;
                        checkBox_TTA.Enabled = false;
                        textBox_CMD.Enabled = false;
                        textBox_CMD.Text = "";
                        break;
                }
            }
            else
            {
                checkBox_Advanced.Checked = false;
                comboBox_Thread.Enabled = false;
                comboBox_Format.Enabled = false;
                comboBox_Model.Enabled = false;
                checkBox_Verbose.Enabled = false;
                checkBox_TTA.Enabled = false;
                textBox_CMD.Enabled = false;
                textBox_CMD.Text = "";
            }

            if (tri != 65535)
            {
                switch (tri)
                {
                    case 0:
                        thread = " -j 1:2:2";
                        comboBox_Thread.SelectedIndex = tri;
                        break;
                    case 1:
                        thread = " -j 1:2";
                        comboBox_Thread.SelectedIndex = tri;
                        break;
                    case 2:
                        thread = " -j 2";
                        comboBox_Thread.SelectedIndex = tri;
                        break;
                    case 3:
                        thread = " -j 2:2";
                        comboBox_Thread.SelectedIndex = tri;
                        break;
                    case 4:
                        thread = " -j 2:2:2";
                        comboBox_Thread.SelectedIndex = tri;
                        break;
                    default:
                        thread = " -j 1:2:2";
                        comboBox_Thread.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                thread = " -j 1:2:2";
                comboBox_Thread.SelectedIndex = 0;
            }

            if (fi != 65535)
            {
                switch (fi)
                {
                    case 0:
                        format = " -f jpg";
                        comboBox_Format.SelectedIndex = fi;
                        break;
                    case 1:
                        format = " -f png";
                        comboBox_Format.SelectedIndex = fi;
                        break;
                    case 2:
                        format = " -f webp";
                        comboBox_Format.SelectedIndex = fi;
                        break;
                    default:
                        format = " -f png";
                        comboBox_Format.SelectedIndex = 1;
                        break;
                }
            }
            else
            {
                format = " -f png";
                comboBox_Format.SelectedIndex = 1;
            }
            
            if (mi != 65535)
            {
                switch (mi)
                {
                    case 0:
                        model = " -m models-cunet";
                        comboBox_Model.SelectedIndex = mi;
                        break;
                    case 1:
                        model = " -m models-upconv_7_anime_style_art_rgb";
                        comboBox_Model.SelectedIndex = mi;
                        break;
                    case 2:
                        model = " -m models-upconv_7_photo";
                        comboBox_Model.SelectedIndex = mi;
                        break;
                    default:
                        model = " -m models-cunet";
                        comboBox_Model.SelectedIndex = 0;
                        break;
                }
            }
            else
            {
                model = " -m models-cunet";
                comboBox_Model.SelectedIndex = 0;
            }

            if (vboi != 65535)
            {
                switch (vboi)
                {
                    case 0:
                        vboutput = "";
                        checkBox_Verbose.Checked = false;
                        break;
                    case 1:
                        vboutput = " -v";
                        checkBox_Verbose.Checked = true;
                        break;
                    default:
                        vboutput = "";
                        checkBox_Verbose.Checked = false;
                        break;
                }
            }
            else
            {
                vboutput = "";
                checkBox_Verbose.Checked = false;
            }

            if (ti != 65535)
            {
                switch (ti)
                {
                    case 0:
                        tta = "";
                        checkBox_TTA.Checked = false;
                        break;
                    case 1:
                        tta = " -x";
                        checkBox_TTA.Checked = false;
                        break;
                    default:
                        tta = "";
                        checkBox_TTA.Checked = false;
                        break;
                }
            }
            else
            {
                tta = "";
                checkBox_TTA.Checked = false;
            }

            if (pi != "")
            {
                if (advi != 65535)
                {
                    switch (advi)
                    {
                        case 0:
                            textBox_CMD.Text = "";
                            break;
                        case 1:
                            textBox_CMD.Text = pi;
                            break;
                        default:
                            textBox_CMD.Text = "";
                            break;
                    }
                }
            }
            else
            {
                textBox_CMD.Text = "";
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
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        rdlevel = " -n 0";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        rdlevel = " -n 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        rdlevel = " -n 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        rdlevel = " -n 3";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        rdlevel = " -n 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
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
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 1:
                        rdlevel = " -n 0";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 2:
                        rdlevel = " -n 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 3:
                        rdlevel = " -n 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 4:
                        rdlevel = " -n 3";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    default:
                        rdlevel = " -n 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                }
            }
        }

        private void ComboBox_Uplevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_Uplevel.SelectedIndex)
                {
                    case 0:
                        uplevel = " -s 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        uplevel = " -s 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        uplevel = " -s 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                }
            }
            else
            {
                switch (comboBox_Uplevel.SelectedIndex)
                {
                    case 0:
                        uplevel = " -s 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 1:
                        uplevel = " -s 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    default:
                        uplevel = " -s 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                }
            }
        }

        private void ComboBox_GPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_GPU.SelectedIndex)
                {
                    case 0:
                        usegpu = " -g default";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        usegpu = " -g -1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        usegpu = " -g 0";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        usegpu = " -g 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        usegpu = " -g 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        usegpu = " -g default";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                }
            }
            else
            {
                switch (comboBox_GPU.SelectedIndex)
                {
                    case 0:
                        usegpu = " -g default";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 1:
                        usegpu = " -g -1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 2:
                        usegpu = " -g 0";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 3:
                        usegpu = " -g 1";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    case 4:
                        usegpu = " -g 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                    default:
                        usegpu = " -g default";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        break;
                }
            }
        }

        private void TextBox_Blocksize_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                blocksize = " -t " + textBox_Blocksize.Text;
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                blocksize = " -t " + textBox_Blocksize.Text;
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
            }
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
                textBox_CMD.Enabled = true;
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
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
                if (comboBox_Model.SelectedIndex != 0)
                {
                    model = " -m models-cunet";
                    comboBox_Model.SelectedIndex = 0;
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
                textBox_CMD.Enabled = false;
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
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
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        thread = " -j 1:2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        thread = " -j 2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        thread = " -j 2:2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        thread = " -j 2:2:2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    default:
                        thread = " -j 1:2:2";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                }
            }
            else
            {

            }
        }

        private void ComboBox_Format_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Format.SelectedIndex)
            {
                case 0:
                    format = " -f jpg";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
                case 1:
                    format = " -f png";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
                case 2:
                    format = " -f webp";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
                default:
                    format = " -f png";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
            }
        }

        private void ComboBox_Model_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Model.SelectedIndex)
            {
                case 0:
                    model = " -m models-cunet";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
                case 1:
                    if (comboBox_Uplevel.SelectedIndex == 1)
                    {
                        model = " -m models-upconv_7_anime_style_art_rgb";
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
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
                        cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                        textBox_CMD.Text = cmdparam;
                        break;
                    }
                    else
                    {
                        break;
                    }
                default:
                    model = " -m models-cunet";
                    cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                    textBox_CMD.Text = cmdparam;
                    break;
            }
        }

        private void CheckBox_Verbose_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Verbose.Checked != false)
            {
                vboutput = " -v";
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                vboutput = "";
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                textBox_CMD.Text = cmdparam;
            }
        }

        private void CheckBox_TTA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TTA.Checked != false)
            {
                tta = " -x";
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                textBox_CMD.Text = cmdparam;
            }
            else
            {
                tta = "";
                cmdparam = "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
                textBox_CMD.Text = cmdparam;
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

            var ini = new IniFile(@".\settings.ini");
            ini.WriteString("IMAGE_SETTINGS", "REDUCTION_INDEX", comboBox_Rdlevel.SelectedIndex.ToString());
            ini.WriteString("IMAGE_SETTINGS", "UPSCALE_INDEX", comboBox_Uplevel.SelectedIndex.ToString());
            ini.WriteString("IMAGE_SETTINGS", "GPU_INDEX", comboBox_GPU.SelectedIndex.ToString());
            switch (textBox_Blocksize.Text)
            {
                case "0":
                    ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", "0");
                    break;
                case "00":
                    ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", textBox_Blocksize.Text.Replace("00", "0"));
                    break;
                case "000":
                    ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", textBox_Blocksize.Text.Replace("000", "0"));
                    break;
                case "0000":
                    ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", textBox_Blocksize.Text.Replace("0000", "0"));
                    break;
                default:
                    if (textBox_Blocksize.Text.StartsWith("0"))
                    {
                        ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", textBox_Blocksize.Text.Replace("0", ""));
                    }
                    else
                    {
                        ini.WriteString("IMAGE_SETTINGS", "BLOCKSIZE_INDEX", textBox_Blocksize.Text);
                    }
                    break;
            }
            
            if (checkBox_Advanced.Checked == false)
            {
                ini.WriteString("IMAGE_SETTINGS", "ADVANCED_INDEX", "0");
            }
            else
            {
                ini.WriteString("IMAGE_SETTINGS", "ADVANCED_INDEX", "1");
            }
            ini.WriteString("IMAGE_SETTINGS", "THREAD_INDEX", comboBox_Thread.SelectedIndex.ToString());
            ini.WriteString("IMAGE_SETTINGS", "FORMAT_INDEX", comboBox_Format.SelectedIndex.ToString());
            ini.WriteString("IMAGE_SETTINGS", "MODEL_INDEX", comboBox_Model.SelectedIndex.ToString());
            if (checkBox_Verbose.Checked == false)
            {
                ini.WriteString("IMAGE_SETTINGS", "VBO_INDEX", "0");
            }
            else
            {
                ini.WriteString("IMAGE_SETTINGS", "VBO_INDEX", "1");
            }
            
            if (checkBox_TTA.Checked == false)
            {
                ini.WriteString("IMAGE_SETTINGS", "TTA_INDEX", "0");
            }
            else
            {
                ini.WriteString("IMAGE_SETTINGS", "TTA_INDEX", "1");
            }
            
            ini.WriteString("IMAGE_SETTINGS", "PARAMS", cmdparam);
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
