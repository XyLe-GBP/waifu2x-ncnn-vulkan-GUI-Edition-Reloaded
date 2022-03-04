using NVGE.Localization;
using System;
using System.Windows.Forms;

namespace NVGE
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
            Config.Load(Common.xmlpath);

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
                    rdlevel = " -n 3";
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

            if (int.Parse(Config.Entry["Blocksize"].Value) != 0)
            {
                textBox_Blocksize.Text = Config.Entry["Blocksize"].Value;
            }
            else
            {
                textBox_Blocksize.Text = "0";
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
                    switch (int.Parse(Config.Entry["Pixel_width"].Value))
                    {
                        default:
                            textBox_width.Text = Config.Entry["Pixel_width"].Value.ToString();
                            break;
                    }
                    switch (int.Parse(Config.Entry["Pixel_height"].Value))
                    {
                        default:
                            textBox_height.Text = Config.Entry["Pixel_height"].Value.ToString();
                            break;
                    }
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
            else
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
        }

        private void ComboBox_GPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                switch (comboBox_GPU.SelectedIndex)
                {
                    case 0:
                        usegpu = " -g default";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 1:
                        usegpu = " -g -1";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 2:
                        usegpu = " -g 0";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 3:
                        usegpu = " -g 1";
                        cmdparam = RefleshParams();
                        textBox_CMD.Text = cmdparam;
                        break;
                    case 4:
                        usegpu = " -g 2";
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
            else
            {
                switch (comboBox_GPU.SelectedIndex)
                {
                    case 0:
                        usegpu = " -g default";
                        cmdparam = RefleshParams();
                        break;
                    case 1:
                        usegpu = " -g -1";
                        cmdparam = RefleshParams();
                        break;
                    case 2:
                        usegpu = " -g 0";
                        cmdparam = RefleshParams();
                        break;
                    case 3:
                        usegpu = " -g 1";
                        cmdparam = RefleshParams();
                        break;
                    case 4:
                        usegpu = " -g 2";
                        cmdparam = RefleshParams();
                        break;
                    default:
                        usegpu = " -g default";
                        cmdparam = RefleshParams();
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
            if (checkBox_pixel.Checked != false)
            {
                if (textBox_width.Text == "")
                {
                    MessageBox.Show("The pixel value was not entered correctly.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox_height.Text == "")
                {
                    MessageBox.Show("The pixel value was not entered correctly.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Config.Entry["Reduction"].Value = comboBox_Rdlevel.SelectedIndex.ToString();
            Config.Entry["Scale"].Value = comboBox_Uplevel.SelectedIndex.ToString();
            Config.Entry["GPU"].Value = comboBox_GPU.SelectedIndex.ToString();

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

            switch (textBox_width.Text)
            {
                default:
                    Config.Entry["Pixel_width"].Value = textBox_width.Text;
                    break;
            }

            switch (textBox_height.Text)
            {
                default:
                    Config.Entry["Pixel_height"].Value = textBox_height.Text;
                    break;
            }

            Config.Entry["Param"].Value = cmdparam;

            Config.Save(Common.xmlpath);

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string RefleshParams()
        {
            return "waifu2x-ncnn-vulkan -i $InFile -o $OutFile" + rdlevel + uplevel + blocksize + usegpu + thread + format + model + vboutput + tta;
        }
    }
}
