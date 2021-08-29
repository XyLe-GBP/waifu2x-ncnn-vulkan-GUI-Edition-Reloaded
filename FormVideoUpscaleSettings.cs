using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    public partial class FormVideoUpscaleSettings : Form
    {
        private static string fps;
        private static string videofps = " -framerate" + fps;
        private static string pathFF;
        private static string hidebnr = " -hide_banner";
        private static string copychapters = " -map_chapters -1";
        private static string metadatainfo = " -map_metadata -1";
        private static string vc = " hevc_nvenc";
        private static string Videocodec = " -vcodec" + vc;
        private static string ac = " aac";
        private static string Audiocodec = " -acodec" + ac;
        private static string ab = " 192";
        private static string Audiobitrate = " -b:a" + ab + "k";
        private static string Videoloc = " $OutFile";
        private static string Audioloc = " $OutFile";
        private static string oao = " -vn";
        private static string intaac = " -strict -2";
        private static string overw = " -y";
        private static string qp = " 0";
        private static string qpl = " -qp" + qp;
        private static string ds = " -sn";
        private static string dds = " -dn";
        private static string ae = " pcm_s24le";
        private static string aes = " -c:a" + ae;
        private static string pre = "";//" veryslow";
        private static string preset = "";//" -preset" + pre;
        private static readonly string ql = " 1";
        private static string qv = " -q:v" + ql;
        private static string cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
        private static string cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
        private static string cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";

        public FormVideoUpscaleSettings()
        {
            InitializeComponent();
        }

        private void FormVideoUpscaleSettings_Load(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");

            int eas = ini.GetInt("VIDEO_SETTINGS", "ADVANCED_INDEX", 65535), eiaac = ini.GetInt("VIDEO_SETTINGS", "IAAC_INDEX", 65535), dss = ini.GetInt("VIDEO_SETTINGS", "DSS_INDEX", 65535), cc = ini.GetInt("VIDEO_SETTINGS", "CC_INDEX", 65535), oao = ini.GetInt("VIDEO_SETTINGS", "OAO_INDEX", 65535), hi = ini.GetInt("VIDEO_SETTINGS", "HI_INDEX", 65535), eso = ini.GetInt("VIDEO_SETTINGS", "SO_INDEX", 65535), dds = ini.GetInt("VIDEO_SETTINGS", "DS_INDEX", 65535), mi = ini.GetInt("VIDEO_SETTINGS", "MI_INDEX", 65535), oof = ini.GetInt("VIDEO_SETTINGS", "OOF_INDEX", 65535), useNVENC = ini.GetInt("VIDEO_SETTINGS", "NVENC_INDEX", 65535), mvc = ini.GetInt("VIDEO_SETTINGS", "MVC_INDEX", 65535), mac = ini.GetInt("VIDEO_SETTINGS", "MAC_INDEX", 65535), ab = ini.GetInt("VIDEO_SETTINGS", "AB_INDEX", 65535), ae = ini.GetInt("VIDEO_SETTINGS", "AE_INDEX", 65535), pre = ini.GetInt("VIDEO_SETTINGS", "PRESET_INDEX", 65535), msol = ini.GetInt("VIDEO_SETTINGS", "MSOL_INDEX", 65535);
            string vfps = ini.GetString("VIDEO_SETTINGS", "FPS_INDEX");
            string pathFFmpeg = ini.GetString("VIDEO_SETTINGS", "FFMPEG_INDEX");
            string crf = ini.GetString("VIDEO_SETTINGS", "CRF_INDEX");
            string vca = ini.GetString("VIDEO_SETTINGS", "VC_INDEX");
            string aca = ini.GetString("VIDEO_SETTINGS", "AC_INDEX");
            string abitrate = ini.GetString("VIDEO_SETTINGS", "BIT_INDEX");
            int enc = ini.GetInt("VIDEO_SETTINGS", "ENCODE_INDEX", 65535);
            int h264 = ini.GetInt("VIDEO_SETTINGS", "H264_INDEX", 65535);
            string vl = ini.GetString("VIDEO_SETTINGS", "VL_INDEX");
            string al = ini.GetString("VIDEO_SETTINGS", "AL_INDEX");
            string cmdparam1 = ini.GetString("VIDEO_SETTINGS", "CMDV_INDEX");
            string cmdparam2 = ini.GetString("VIDEO_SETTINGS", "CMDA_INDEX");
            string cmdparam3 = ini.GetString("VIDEO_SETTINGS", "CMDF_INDEX");
                                            
            if (vfps != "")
            {
                textBox_FPS.Text = vfps;
                fps = vfps;
            }
            else
            {
                textBox_FPS.Text = "";
            }
            if (pathFFmpeg != "")
            {
                textBox_FFmpeg.Text = pathFFmpeg;
                pathFF = pathFFmpeg;
            }
            else
            {
                textBox_FFmpeg.Text = "";
            }
            if (eas != 65535)
            {
                switch (eas)
                {
                    case 0:
                        button_FC.Enabled = false;
                        checkBox_EIA.Enabled = false;
                        checkBox_EIA.Checked = false;
                        checkBox_DSS.Enabled = false;
                        checkBox_DSS.Checked = false;
                        checkBox_CC.Enabled = false;
                        checkBox_CC.Checked = false;
                        checkBox_OAO.Enabled = false;
                        checkBox_OAO.Checked = false;
                        checkBox_HI.Enabled = false;
                        checkBox_HI.Checked = false;
                        checkBox_SO.Enabled = false;
                        checkBox_SO.Checked = false;
                        checkBox_DS.Enabled = false;
                        checkBox_DS.Checked = false;
                        checkBox_MI.Enabled = false;
                        checkBox_MI.Checked = false;
                        checkBox_OOF.Enabled = false;
                        checkBox_OOF.Checked = false;
                        checkBox_NVENC.Enabled = false;
                        checkBox_NVENC.Checked = false;
                        checkBox_MVC.Enabled = false;
                        checkBox_MVC.Checked = false;
                        checkBox_MAC.Enabled = false;
                        checkBox_MAC.Checked = false;
                        checkBox_AE.Enabled = false;
                        checkBox_AE.Checked = false;
                        checkBox_AB.Enabled = false;
                        checkBox_AB.Checked = false;
                        checkBox_preset.Enabled = false;
                        checkBox_preset.Checked = false;
                        checkBox_MSOL.Enabled = false;
                        checkBox_MSOL.Checked = false;
                        label2.Enabled = false;
                        label3.Enabled = false;
                        label4.Enabled = false;
                        label7.Enabled = false;
                        label8.Enabled = false;
                        label9.Enabled = false;
                        label10.Enabled = false;
                        label11.Enabled = false;
                        label12.Enabled = false;
                        label13.Enabled = false;
                        label14.Enabled = false;
                        label15.Enabled = false;
                        textBox_CMDV.Enabled = false;
                        textBox_CMDA.Enabled = false;
                        textBox_CMDF.Enabled = false;
                        break;
                    case 1:
                        button_FC.Enabled = true;
                        checkBox_EIA.Enabled = true;
                        checkBox_DSS.Enabled = true;
                        checkBox_CC.Enabled = true;
                        checkBox_OAO.Enabled = true;
                        checkBox_HI.Enabled = true;
                        checkBox_SO.Enabled = true;
                        checkBox_DS.Enabled = true;
                        checkBox_MI.Enabled = true;
                        checkBox_OOF.Enabled = true;
                        checkBox_NVENC.Enabled = true;
                        checkBox_MVC.Enabled = true;
                        checkBox_MAC.Enabled = true;
                        checkBox_AE.Enabled = true;
                        checkBox_AB.Enabled = true;
                        checkBox_preset.Enabled = true;
                        checkBox_MSOL.Enabled = true;
                        label9.Enabled = true;
                        label12.Enabled = true;
                        label13.Enabled = true;
                        textBox_CMDV.Enabled = true;
                        textBox_CMDA.Enabled = true;
                        textBox_CMDF.Enabled = true;
                        if (eiaac != 65535)
                        {
                            switch (eiaac)
                            {
                                case 0:
                                    checkBox_EIA.Checked = false;
                                    intaac = "";
                                    break;
                                case 1:
                                    checkBox_EIA.Checked = true;
                                    break;
                                default:
                                    checkBox_EIA.Checked = false;
                                    intaac = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_EIA.Checked = false;
                            intaac = "";
                        }
                        if (dss != 65535)
                        {
                            switch (dss)
                            {
                                case 0:
                                    checkBox_DSS.Checked = false;
                                    ds = "";
                                    break;
                                case 1:
                                    checkBox_DSS.Checked = true;
                                    break;
                                default:
                                    checkBox_DSS.Checked = false;
                                    ds = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_DSS.Checked = false;
                            ds = "";
                        }
                        if (cc != 65535)
                        {
                            switch (cc)
                            {
                                case 0:
                                    checkBox_CC.Checked = false;
                                    copychapters = "";
                                    break;
                                case 1:
                                    checkBox_CC.Checked = true;
                                    break;
                                default:
                                    checkBox_CC.Checked = false;
                                    copychapters = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_CC.Checked = false;
                            copychapters = "";
                        }
                        if (oao != 65535)
                        {
                            switch (oao)
                            {
                                case 0:
                                    checkBox_OAO.Checked = false;
                                    FormVideoUpscaleSettings.oao = "";
                                    break;
                                case 1:
                                    checkBox_OAO.Checked = true;
                                    break;
                                default:
                                    checkBox_OAO.Checked = false;
                                    FormVideoUpscaleSettings.oao = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_OAO.Checked = false;
                            FormVideoUpscaleSettings.oao = "";
                        }
                        if (hi != 65535)
                        {
                            switch (hi)
                            {
                                case 0:
                                    checkBox_HI.Checked = false;
                                    hidebnr = "";
                                    break;
                                case 1:
                                    checkBox_HI.Checked = true;
                                    break;
                                default:
                                    checkBox_HI.Checked = false;
                                    hidebnr = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_HI.Checked = false;
                            hidebnr = "";
                        }
                        if (eso != 65535)
                        {
                            switch (eso)
                            {
                                case 0:
                                    checkBox_SO.Checked = false;
                                    break;
                                case 1:
                                    checkBox_SO.Checked = true;
                                    break;
                                default:
                                    checkBox_SO.Checked = false;
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_SO.Checked = false;
                        }
                        if (dds != 65535)
                        {
                            switch (dds)
                            {
                                case 0:
                                    checkBox_DS.Checked = false;
                                    FormVideoUpscaleSettings.dds = "";
                                    break;
                                case 1:
                                    checkBox_DS.Checked = true;
                                    break;
                                default:
                                    checkBox_DS.Checked = false;
                                    FormVideoUpscaleSettings.dds = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_DS.Checked = false;
                            FormVideoUpscaleSettings.dds = "";
                        }
                        if (mi != 65535)
                        {
                            switch (mi)
                            {
                                case 0:
                                    checkBox_MI.Checked = false;
                                    metadatainfo = "";
                                    break;
                                case 1:
                                    checkBox_MI.Checked = true;
                                    break;
                                default:
                                    checkBox_MI.Checked = false;
                                    metadatainfo = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_MI.Checked = false;
                            metadatainfo = "";
                        }
                        if (oof != 65535)
                        {
                            switch (oof)
                            {
                                case 0:
                                    checkBox_OOF.Checked = false;
                                    overw = "";
                                    break;
                                case 1:
                                    checkBox_OOF.Checked = true;
                                    break;
                                default:
                                    checkBox_OOF.Checked = false;
                                    overw = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_OOF.Checked = false;
                            overw = "";
                        }
                        if (useNVENC != 65535)
                        {
                            switch (useNVENC)
                            {
                                case 0:
                                    checkBox_NVENC.Checked = false;
                                    label8.Enabled = false;
                                    textBox_CRF.Enabled = false;
                                    qpl = "";
                                    break;
                                case 1:
                                    checkBox_NVENC.Checked = true;
                                    label8.Enabled = true;
                                    textBox_CRF.Enabled = true;
                                    if (crf != "")
                                    {
                                        textBox_CRF.Text = crf;
                                        qpl = " -qp " + crf;
                                    }
                                    else
                                    {
                                        textBox_CRF.Text = "";
                                    }
                                    break;
                                default:
                                    checkBox_NVENC.Checked = false;
                                    label8.Enabled = false;
                                    textBox_CRF.Enabled = false;
                                    qpl = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_NVENC.Checked = false;
                            label8.Enabled = false;
                            textBox_CRF.Enabled = false;
                            qpl = "";
                        }
                        if (mvc != 65535)
                        {
                            switch (mvc)
                            {
                                case 0:
                                    checkBox_MVC.Checked = false;
                                    label2.Enabled = false;
                                    textBox_VCodec.Enabled = false;
                                    Videocodec = "";
                                    break;
                                case 1:
                                    checkBox_MVC.Checked = true;
                                    label2.Enabled = true;
                                    textBox_VCodec.Enabled = true;
                                    if (vca != "")
                                    {
                                        textBox_VCodec.Text = vca;
                                        Videocodec = " -vcodec " + vca;
                                    }
                                    else
                                    {
                                        textBox_VCodec.Text = "";
                                    }
                                    break;
                                default:
                                    checkBox_MVC.Checked = false;
                                    label2.Enabled = false;
                                    textBox_VCodec.Enabled = false;
                                    Videocodec = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_MVC.Checked = false;
                            label2.Enabled = false;
                            textBox_VCodec.Enabled = false;
                            Videocodec = "";
                        }
                        if (mac != 65535)
                        {
                            switch (mac)
                            {
                                case 0:
                                    checkBox_MAC.Checked = false;
                                    label3.Enabled = false;
                                    textBox_ACodec.Enabled = false;
                                    Audiocodec = "";
                                    break;
                                case 1:
                                    checkBox_MAC.Checked = true;
                                    label3.Enabled = true;
                                    textBox_ACodec.Enabled = true;
                                    if (aca != "")
                                    {
                                        textBox_ACodec.Text = aca;
                                        Audiocodec = " -acodec " + aca;
                                    }
                                    else
                                    {
                                        textBox_ACodec.Text = "";
                                    }
                                    break;
                                default:
                                    checkBox_MAC.Checked = false;
                                    label3.Enabled = false;
                                    textBox_ACodec.Enabled = false;
                                    Audiocodec = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_MAC.Checked = false;
                            label3.Enabled = false;
                            textBox_ACodec.Enabled = false;
                            Audiocodec = "";
                        }
                        if (ab != 65535)
                        {
                            switch (ab)
                            {
                                case 0:
                                    checkBox_AB.Checked = false;
                                    label4.Enabled = false;
                                    label7.Enabled = false;
                                    textBox_AB.Enabled = false;
                                    Audiobitrate = "";
                                    break;
                                case 1:
                                    checkBox_AB.Checked = true;
                                    label4.Enabled = true;
                                    label7.Enabled = true;
                                    textBox_AB.Enabled = true;
                                    if (abitrate != "")
                                    {
                                        textBox_AB.Text = abitrate;
                                        Audiobitrate = " -b:a " + abitrate + "k";
                                    }
                                    else
                                    {
                                        textBox_AB.Text = "";
                                    }
                                    break;
                                default:
                                    checkBox_AB.Checked = false;
                                    label4.Enabled = false;
                                    label7.Enabled = false;
                                    textBox_AB.Enabled = false;
                                    Audiobitrate = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_AB.Checked = false;
                            label4.Enabled = false;
                            label7.Enabled = false;
                            textBox_AB.Enabled = false;
                            Audiobitrate = "";
                        }
                        if (ae != 65535)
                        {
                            switch (ae)
                            {
                                case 0:
                                    checkBox_AE.Checked = false;
                                    comboBox_AE.Enabled = false;
                                    label14.Enabled = false;
                                    aes = "";
                                    break;
                                case 1:
                                    checkBox_AE.Checked = true;
                                    comboBox_AE.Enabled = true;
                                    label14.Enabled = true;
                                    if (enc != 65535)
                                    {
                                        comboBox_AE.SelectedIndex = enc;
                                        switch (enc)
                                        {
                                            case 0:
                                                aes = " -c:a pcm_s16le";
                                                break;
                                            case 1:
                                                aes = " -c:a pcm_s24le";
                                                break;
                                            case 2:
                                                aes = " -c:a pcm_s32le";
                                                break;
                                            case 3:
                                                aes = " -c:a aac";
                                                break;
                                            case 4:
                                                aes = " -c:a libmp3lame";
                                                break;
                                            default:
                                                aes = " -c:a pcm_s24le";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        comboBox_AE.SelectedIndex = 1;
                                        aes = " -c:a pcm_s24le";
                                    }
                                    break;
                                default:
                                    checkBox_AE.Checked = false;
                                    comboBox_AE.Enabled = false;
                                    label14.Enabled = false;
                                    aes = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_AE.Checked = false;
                            comboBox_AE.Enabled = false;
                            label14.Enabled = false;
                            aes = "";
                        }
                        if (pre != 65535)
                        {
                            switch (pre)
                            {
                                case 0:
                                    checkBox_preset.Checked = false;
                                    label15.Enabled = false;
                                    comboBox_preset.Enabled = false;
                                    preset = "";
                                    break;
                                case 1:
                                    checkBox_preset.Checked = true;
                                    label15.Enabled = true;
                                    comboBox_preset.Enabled = true;
                                    switch (h264)
                                    {
                                        case 0:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset ultrafast";
                                            break;
                                        case 1:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset superfast";
                                            break;
                                        case 2:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset veryfast";
                                            break;
                                        case 3:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset faster";
                                            break;
                                        case 4:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset fast";
                                            break;
                                        case 5:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset medium";
                                            break;
                                        case 6:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset slow";
                                            break;
                                        case 7:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset slower";
                                            break;
                                        case 8:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset veryslow";
                                            break;
                                        default:
                                            comboBox_preset.SelectedIndex = h264;
                                            preset = " -preset veryslow";
                                            break;
                                    }
                                    break;
                                default:
                                    checkBox_preset.Checked = false;
                                    label15.Enabled = false;
                                    comboBox_preset.Enabled = false;
                                    preset = "";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_preset.Checked = false;
                            label15.Enabled = false;
                            comboBox_preset.Enabled = false;
                            preset = "";
                        }
                        if (msol != 65535)
                        {
                            switch (msol)
                            {
                                case 0:
                                    checkBox_MSOL.Checked = false;
                                    label10.Enabled = false;
                                    label11.Enabled = false;
                                    button_VL.Enabled = false;
                                    button_AL.Enabled = false;
                                    Videoloc = " $OutFile";
                                    Audioloc = " $OutFile";
                                    break;
                                case 1:
                                    checkBox_MSOL.Checked = true;
                                    label10.Enabled = true;
                                    label11.Enabled = true;
                                    button_VL.Enabled = true;
                                    button_AL.Enabled = true;
                                    if (vl != "")
                                    {
                                        textBox_VL.Text = vl;
                                        Videoloc = " " + vl;
                                    }
                                    else
                                    {
                                        textBox_VL.Text = "";
                                        Videoloc = " $OutFile";
                                    }
                                    if (al != "")
                                    {
                                        textBox_AL.Text = al;
                                        Audioloc = " " + al;
                                    }
                                    else
                                    {
                                        textBox_AL.Text = "";
                                        Audioloc = " $OutFile";
                                    }
                                    break;
                                default:
                                    checkBox_MSOL.Checked = false;
                                    label10.Enabled = false;
                                    label11.Enabled = false;
                                    button_VL.Enabled = false;
                                    button_AL.Enabled = false;
                                    Videoloc = " $OutFile";
                                    Audioloc = " $OutFile";
                                    break;
                            }
                        }
                        else
                        {
                            checkBox_MSOL.Checked = false;
                            label10.Enabled = false;
                            label11.Enabled = false;
                            button_VL.Enabled = false;
                            button_AL.Enabled = false;
                            Videoloc = " $OutFile";
                            Audioloc = " $OutFile";
                        }
                        if (cmdparam1 != "")
                        {
                            textBox_CMDV.Text = cmdparam1;
                        }
                        else
                        {
                            textBox_CMDV.Text = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                        }
                        if (cmdparam2 != "")
                        {
                            textBox_CMDA.Text = cmdparam2;
                        }
                        else
                        {
                            textBox_CMDA.Text = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                        }
                        if (cmdparam3 != "")
                        {
                            textBox_CMDF.Text = cmdparam3;
                        }
                        else
                        {
                            textBox_CMDF.Text = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                        }

                        break;
                }
            }
            else
            {
                button_FC.Enabled = false;
                checkBox_EIA.Enabled = false;
                checkBox_EIA.Checked = false;
                checkBox_DSS.Enabled = false;
                checkBox_DSS.Checked = false;
                checkBox_CC.Enabled = false;
                checkBox_CC.Checked = false;
                checkBox_OAO.Enabled = false;
                checkBox_OAO.Checked = false;
                checkBox_HI.Enabled = false;
                checkBox_HI.Checked = false;
                checkBox_SO.Enabled = false;
                checkBox_SO.Checked = false;
                checkBox_DS.Enabled = false;
                checkBox_DS.Checked = false;
                checkBox_MI.Enabled = false;
                checkBox_MI.Checked = false;
                checkBox_OOF.Enabled = false;
                checkBox_OOF.Checked = false;
                checkBox_NVENC.Enabled = false;
                checkBox_NVENC.Checked = false;
                checkBox_MVC.Enabled = false;
                checkBox_MVC.Checked = false;
                checkBox_MAC.Enabled = false;
                checkBox_MAC.Checked = false;
                checkBox_AE.Enabled = false;
                checkBox_AE.Checked = false;
                checkBox_AB.Enabled = false;
                checkBox_AB.Checked = false;
                checkBox_preset.Enabled = false;
                checkBox_preset.Checked = false;
                checkBox_MSOL.Enabled = false;
                checkBox_MSOL.Checked = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = false;
                label10.Enabled = false;
                label11.Enabled = false;
                label12.Enabled = false;
                label13.Enabled = false;
                label14.Enabled = false;
                label15.Enabled = false;
                textBox_CMDV.Enabled = false;
                textBox_CMDA.Enabled = false;
                textBox_CMDF.Enabled = false;
            }

            return;
        }


        private void CheckBox_preset_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_preset.Checked != false)
            {
                comboBox_preset.Enabled = true;
                comboBox_preset.SelectedIndex = 8;
                label15.Enabled = true;
                preset = " -preset";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                comboBox_preset.Enabled = false;
                label15.Enabled = false;
                preset = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_MSOL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MSOL.Checked != false)
            {
                textBox_AL.Enabled = true;
                textBox_VL.Enabled = true;
                button_AL.Enabled = true;
                button_VL.Enabled = true;
                label10.Enabled = true;
                label11.Enabled = true;
            }
            else
            {
                textBox_AL.Enabled = false;
                textBox_AL.Text = "";
                Audioloc = " $OutFile";
                textBox_VL.Enabled = false;
                textBox_VL.Text = "";
                Videoloc = " $OutFile";
                button_AL.Enabled = false;
                button_VL.Enabled = false;
                label10.Enabled = false;
                label11.Enabled = false;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }



        private void CheckBox_AE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AE.Checked != false)
            {
                comboBox_AE.Enabled = true;
                comboBox_AE.SelectedIndex = 1;
                label14.Enabled = true;
                aes = " -c:a";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                comboBox_AE.Enabled = false;
                label14.Enabled = false;
                aes = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_AB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AB.Checked != false)
            {
                textBox_AB.Enabled = true;
                label4.Enabled = true;
                label7.Enabled = true;
                Audiobitrate = " -b:a";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                textBox_AB.Enabled = false;
                label4.Enabled = false;
                label7.Enabled = false;
                Audiobitrate = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }



        private void CheckBox_MAC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MAC.Checked != false)
            {
                textBox_ACodec.Enabled = true;
                label3.Enabled = true;
                Audiocodec = " -acodec";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                textBox_ACodec.Text = "";
                textBox_ACodec.Enabled = false;
                label3.Enabled = false;
                Audiocodec = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_MVC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MVC.Checked != false)
            {
                textBox_VCodec.Enabled = true;
                label2.Enabled = true;
                Videocodec = " -vcodec";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                textBox_VCodec.Text = "";
                textBox_VCodec.Enabled = false;
                label2.Enabled = false;
                Videocodec = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_NVENC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NVENC.Checked != false)
            {
                textBox_CRF.Enabled = true;
                label8.Enabled = true;
                qpl = " -qp";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                textBox_CRF.Text = "";
                textBox_CRF.Enabled = false;
                label8.Enabled = false;
                qpl = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_OOF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_OOF.Checked != false)
            {
                overw = " -y";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                overw = "";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_MI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MI.Checked != false)
            {
                metadatainfo = " -map_metadata -1";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                metadatainfo = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_DS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DS.Checked != false)
            {
                dds = " -dn";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                dds = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_SO_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBox_HI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HI.Checked != false)
            {
                hidebnr = " -hide_banner";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                hidebnr = "";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_OAO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_OAO.Checked != false)
            {
                oao = " -vn";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                oao = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_CC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_CC.Checked != false)
            {
                copychapters = " -map_chapters -1";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                copychapters = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_DSS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DSS.Checked != false)
            {
                ds = " -sn";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
            else
            {
                ds = "";
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void CheckBox_EIA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_EIA.Checked != false)
            {
                intaac = " -strict -2";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                intaac = "";
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDF.Text = cmdparam_v3;
            }
        }

        private void CheckBox_Advanced_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                if (textBox_FPS.Text == "")
                {
                    MessageBox.Show("Textbox Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }
                else if (textBox_FPS.TextLength <= 4)
                {
                    MessageBox.Show("Textbox Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }
                if (textBox_FFmpeg.Text == "")
                {
                    MessageBox.Show("Textbox Error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }
                button_FC.Enabled = true;
                checkBox_EIA.Enabled = true;
                checkBox_DSS.Enabled = true;
                checkBox_CC.Enabled = true;
                checkBox_OAO.Enabled = true;
                checkBox_HI.Enabled = true;
                checkBox_SO.Enabled = true;
                checkBox_DS.Enabled = true;
                checkBox_MI.Enabled = true;
                checkBox_OOF.Enabled = true;
                checkBox_NVENC.Enabled = true;
                checkBox_MVC.Enabled = true;
                checkBox_MAC.Enabled = true;
                checkBox_AE.Enabled = true;
                checkBox_AB.Enabled = true;
                checkBox_preset.Enabled = true;
                checkBox_MSOL.Enabled = true;
                label9.Enabled = true;
                label12.Enabled = true;
                label13.Enabled = true;
                textBox_CMDV.Enabled = true;
                textBox_CMDA.Enabled = true;
                textBox_CMDF.Enabled = true;
                hidebnr = "";
                qv = "";
                overw = "";
                copychapters = "";
                metadatainfo = "";
                oao = "";
                ds = "";
                dds = "";
                aes = "";
                intaac = "";
                qpl = "";
                preset = "";
                Videoloc = " $OutFile";
                Audioloc = " $OutFile";
                Videocodec = "";
                Audiocodec = "";
                Audiobitrate = "";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                button_FC.Enabled = false;
                checkBox_EIA.Enabled = false;
                checkBox_EIA.Checked = false;
                checkBox_DSS.Enabled = false;
                checkBox_DSS.Checked = false;
                checkBox_CC.Enabled = false;
                checkBox_CC.Checked = false;
                checkBox_OAO.Enabled = false;
                checkBox_OAO.Checked = false;
                checkBox_HI.Enabled = false;
                checkBox_HI.Checked = false;
                checkBox_SO.Enabled = false;
                checkBox_SO.Checked = false;
                checkBox_DS.Enabled = false;
                checkBox_DS.Checked = false;
                checkBox_MI.Enabled = false;
                checkBox_MI.Checked = false;
                checkBox_OOF.Enabled = false;
                checkBox_OOF.Checked = false;
                checkBox_NVENC.Enabled = false;
                checkBox_NVENC.Checked = false;
                checkBox_MVC.Enabled = false;
                checkBox_MVC.Checked = false;
                checkBox_MAC.Enabled = false;
                checkBox_MAC.Checked = false;
                checkBox_AE.Enabled = false;
                checkBox_AE.Checked = false;
                checkBox_AB.Enabled = false;
                checkBox_AB.Checked = false;
                checkBox_preset.Enabled = false;
                checkBox_preset.Checked = false;
                checkBox_MSOL.Enabled = false;
                checkBox_MSOL.Checked = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label7.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = false;
                label10.Enabled = false;
                label11.Enabled = false;
                label12.Enabled = false;
                label13.Enabled = false;
                label14.Enabled = false;
                label15.Enabled = false;
                textBox_CMDV.Enabled = false;
                textBox_CMDA.Enabled = false;
                textBox_CMDF.Enabled = false;
                videofps = " -framerate " + textBox_FPS.Text;
                hidebnr = " -hide_banner";
                copychapters = " -map_chapters -1";
                metadatainfo = " -map_metadata -1";
                Videocodec = " -vcodec libx264 -pix_fmt yuv420p";
                Audiocodec = " -acodec aac";
                Audiobitrate = " -b:a 320k";
                Videoloc = " $OutFile";
                Audioloc = " $OutFile";
                oao = " -vn";
                intaac = " -strict -2";
                overw = " -y";
                qpl = " -qp 0";
                ds = " -sn";
                dds = " -dn";
                aes = " -c:a pcm_s24le";
                preset = " -preset veryslow";
                qv = " -q:v 1";
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = "";
                textBox_CMDA.Text = "";
                textBox_CMDF.Text = "";
            }
        }

        private void Button_FFmpeg_path_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = "Application (ffmpeg.exe)|ffmpeg.exe",
                FilterIndex = 1,
                Title = "Select FFmpeg Application Location",
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (checkBox_Advanced.Checked != false)
                {
                    textBox_FFmpeg.Text = ofd.FileName;
                    cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDV.Text = cmdparam_v1;
                    textBox_CMDA.Text = cmdparam_v2;
                    textBox_CMDF.Text = cmdparam_v3;
                }
                else
                {
                    textBox_FFmpeg.Text = ofd.FileName;
                    cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                }
                return;
            }
            else
            {
                return;
            }
        }

        private void Button_AL_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new();
            fbd.Description = "";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = @"C:\";
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBox_AL.Text = fbd.SelectedPath;
                Audioloc = " " + fbd.SelectedPath;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                textBox_CMDA.Text = cmdparam_v2;
            }
        }

        private void Button_VL_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new();
            fbd.Description = "";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.SelectedPath = @"C:\";
            fbd.ShowNewFolderButton = true;

            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                textBox_VL.Text = fbd.SelectedPath;
                Videoloc = " " + fbd.SelectedPath;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                textBox_CMDV.Text = cmdparam_v1;
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (textBox_FPS.Text == "")
            {
                MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox_FPS.TextLength <= 4)
            {
                MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox_FPS.Text.Contains(".") != true)
            {
                MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox_FFmpeg.Text == "")
            {
                MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox_Advanced.Checked != false)
            {
                if (checkBox_NVENC.Checked != false)
                {
                    if (textBox_CRF.TextLength < 1)
                    {
                        MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MVC.Checked != false)
                {
                    if (textBox_VCodec.TextLength < 2)
                    {
                        MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MAC.Checked != false)
                {
                    if (textBox_ACodec.TextLength < 2)
                    {
                        MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MSOL.Checked != false)
                {
                    if (textBox_AL.Text == "")
                    {
                        MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (textBox_VL.Text == "")
                    {
                        MessageBox.Show(Strings.FieldError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            var ini = new IniFile(@".\settings.ini");
            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", textBox_FPS.Text);
            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", textBox_FFmpeg.Text);
            if (checkBox_Advanced.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "ADVANCED_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "ADVANCED_INDEX", "1");
            }
            if (checkBox_EIA.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "IAAC_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "IAAC_INDEX", "1");
            }
            if (checkBox_DSS.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "DSS_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "DSS_INDEX", "1");
            }
            if (checkBox_CC.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "CC_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "CC_INDEX", "1");
            }
            if (checkBox_OAO.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "OAO_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "OAO_INDEX", "1");
            }
            if (checkBox_HI.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "HI_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "HI_INDEX", "1");
            }
            if (checkBox_SO.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "SO_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "SO_INDEX", "1");
            }
            if (checkBox_DS.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "DS_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "DS_INDEX", "1");
            }
            if (checkBox_MI.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "MI_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "MI_INDEX", "1");
            }
            if (checkBox_OOF.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "OOF_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "OOF_INDEX", "1");
            }
            if (checkBox_NVENC.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "NVENC_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "NVENC_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "CRF_INDEX", textBox_CRF.Text);
            if (checkBox_MVC.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "MVC_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "MVC_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "VC_INDEX", textBox_VCodec.Text);
            if (checkBox_MAC.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "MAC_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "MAC_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "AC_INDEX", textBox_ACodec.Text);
            if (checkBox_AB.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "AB_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "AB_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "BIT_INDEX", textBox_AB.Text);
            if (checkBox_AE.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "AE_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "AE_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "ENCODE_INDEX", comboBox_AE.SelectedIndex.ToString());
            if (checkBox_preset.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "PRESET_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "PRESET_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "H264_INDEX", comboBox_preset.SelectedIndex.ToString());
            if (checkBox_MSOL.Checked == false)
            {
                ini.WriteString("VIDEO_SETTINGS", "MSOL_INDEX", "0");
            }
            else
            {
                ini.WriteString("VIDEO_SETTINGS", "MSOL_INDEX", "1");
            }
            ini.WriteString("VIDEO_SETTINGS", "VL_INDEX", textBox_VL.Text);
            ini.WriteString("VIDEO_SETTINGS", "AL_INDEX", textBox_AL.Text);
            ini.WriteString("VIDEO_SETTINGS", "CMDV_INDEX", cmdparam_v1);
            ini.WriteString("VIDEO_SETTINGS", "CMDA_INDEX", cmdparam_v2);
            ini.WriteString("VIDEO_SETTINGS", "CMDF_INDEX", cmdparam_v3);
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ComboBox_AE_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_AE.SelectedIndex)
            {
                case 0:
                    ae = " pcm_s16le";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
                case 1:
                    ae = " pcm_s24le";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
                case 2:
                    ae = " pcm_s32le";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
                case 3:
                    ae = " aac";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
                case 4:
                    ae = " libmp3lame";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
                default:
                    ae = " pcm_s24le";
                    aes = " -c:a" + ae;
                    cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    textBox_CMDA.Text = cmdparam_v2;
                    break;
            }
        }

        private void ComboBox_preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_preset.SelectedIndex)
            {
                case 0:
                    pre = " ultrafast";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 1:
                    pre = " superfast";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 2:
                    pre = " veryfast";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 3:
                    pre = " faster";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 4:
                    pre = " fast";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 5:
                    pre = " medium";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 6:
                    pre = " slow";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 7:
                    pre = " slower";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                case 8:
                    pre = " veryslow";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
                default:
                    pre = " veryslow";
                    preset = " -preset" + pre;
                    cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    textBox_CMDF.Text = cmdparam_v3;
                    break;
            }
        }

        private void TextBox_FFmpeg_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                pathFF = textBox_FFmpeg.Text;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                pathFF = textBox_FFmpeg.Text;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            }
        }

        private void TextBox_FPS_TextChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                fps = " " + textBox_FPS.Text;
                videofps = " -framerate" + fps;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                textBox_CMDV.Text = cmdparam_v1;
                textBox_CMDA.Text = cmdparam_v2;
                textBox_CMDF.Text = cmdparam_v3;
            }
            else
            {
                fps = " " + textBox_FPS.Text;
                videofps = " -framerate" + fps;
                cmdparam_v1 = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                cmdparam_v2 = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            }
        }

        private void TextBox_AB_TextChanged(object sender, EventArgs e)
        {
            ab = " " + textBox_AB.Text + "k";
            Audiobitrate = " -b:a" + ab;
            cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            textBox_CMDF.Text = cmdparam_v3;
        }

        private void TextBox_ACodec_TextChanged(object sender, EventArgs e)
        {
            ac = " " + textBox_ACodec.Text;
            Audiocodec = " -acodec" + ac;
            cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            textBox_CMDF.Text = cmdparam_v3;
        }

        private void TextBox_VCodec_TextChanged(object sender, EventArgs e)
        {
            vc = " " + textBox_VCodec.Text;
            Videocodec = " -vcodec" + vc;
            cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            textBox_CMDF.Text = cmdparam_v3;
        }

        private void TextBox_CRF_TextChanged(object sender, EventArgs e)
        {
            qp = " " + textBox_CRF.Text;
            qpl = " -qp" + qp;
            cmdparam_v3 = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
            textBox_CMDF.Text = cmdparam_v3;
        }
    }
}
