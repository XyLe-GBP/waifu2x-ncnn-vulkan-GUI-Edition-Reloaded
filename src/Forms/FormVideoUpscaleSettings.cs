using NVGE.Localization;
using System;
using System.IO;
using System.Windows.Forms;

namespace NVGE
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
        private static string pre = "";
        private static string preset = "";
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
            Config.Load(Common.xmlpath);

            if (Config.Entry["FPS"].Value != "")
            {
                textBox_FPS.Text = Config.Entry["FPS"].Value;
                fps = Config.Entry["FPS"].Value;
            }
            else
            {
                textBox_FPS.Text = "";
            }

            if (Config.Entry["FFmpegLocation"].Value != "")
            {
                textBox_FFmpeg.Text = Config.Entry["FFmpegLocation"].Value;
                pathFF = Config.Entry["FFmpegLocation"].Value;
            }
            else
            {
                textBox_FFmpeg.Text = "";
            }

            switch (bool.Parse(Config.Entry["VAdvanced"].Value))
            {
                case false:
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
                    checkBox_VOT.Enabled = false;
                    checkBox_VOT.Checked = false;
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
                    label16.Enabled = false;
                    textBox_CMDV.Enabled = false;
                    textBox_CMDA.Enabled = false;
                    textBox_CMDF.Enabled = false;
                    break;
                case true:
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
                    checkBox_VOT.Enabled = true;
                    label9.Enabled = true;
                    label12.Enabled = true;
                    label13.Enabled = true;
                    textBox_CMDV.Enabled = true;
                    textBox_CMDA.Enabled = true;
                    textBox_CMDF.Enabled = true;

                    switch (bool.Parse(Config.Entry["InternalAAC"].Value))
                    {
                        case false:
                            checkBox_EIA.Checked = false;
                            intaac = "";
                            break;
                        case true:
                            checkBox_EIA.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["Subtitle"].Value))
                    {
                        case false:
                            checkBox_DSS.Checked = false;
                            ds = "";
                            break;
                        case true:
                            checkBox_DSS.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["CopyingChapters"].Value))
                    {
                        case false:
                            checkBox_CC.Checked = false;
                            copychapters = "";
                            break;
                        case true:
                            checkBox_CC.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["OutputAudioOnly"].Value))
                    {
                        case false:
                            checkBox_OAO.Checked = false;
                            FormVideoUpscaleSettings.oao = "";
                            break;
                        case true:
                            checkBox_OAO.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["HideInformations"].Value))
                    {
                        case false:
                            checkBox_HI.Checked = false;
                            hidebnr = "";
                            break;
                        case true:
                            checkBox_HI.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["Sequential"].Value))
                    {
                        case false:
                            checkBox_SO.Checked = false;
                            break;
                        case true:
                            checkBox_SO.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["DataStream"].Value))
                    {
                        case false:
                            checkBox_DS.Checked = false;
                            FormVideoUpscaleSettings.dds = "";
                            break;
                        case true:
                            checkBox_DS.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["Metadata"].Value))
                    {
                        case false:
                            checkBox_MI.Checked = false;
                            metadatainfo = "";
                            break;
                        case true:
                            checkBox_MI.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["Overwrite"].Value))
                    {
                        case false:
                            checkBox_OOF.Checked = false;
                            overw = "";
                            break;
                        case true:
                            checkBox_OOF.Checked = true;
                            break;
                    }

                    switch (bool.Parse(Config.Entry["NVENC"].Value))
                    {
                        case false:
                            checkBox_NVENC.Checked = false;
                            label8.Enabled = false;
                            textBox_CRF.Enabled = false;
                            qpl = "";
                            break;
                        case true:
                            checkBox_NVENC.Checked = true;
                            label8.Enabled = true;
                            textBox_CRF.Enabled = true;
                            if (Config.Entry["CRFLevel"].Value != "")
                            {
                                textBox_CRF.Text = Config.Entry["CRFLevel"].Value;
                                qpl = " -qp " + Config.Entry["CRFLevel"].Value;
                            }
                            else
                            {
                                textBox_CRF.Text = "";
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["VideoCodec"].Value))
                    {
                        case false:
                            checkBox_MVC.Checked = false;
                            label2.Enabled = false;
                            textBox_VCodec.Enabled = false;
                            Videocodec = "";
                            break;
                        case true:
                            checkBox_MVC.Checked = true;
                            label2.Enabled = true;
                            textBox_VCodec.Enabled = true;
                            if (Config.Entry["VideoCodecIndex"].Value != "")
                            {
                                textBox_VCodec.Text = Config.Entry["VideoCodecIndex"].Value;
                                Videocodec = " -vcodec " + Config.Entry["VideoCodecIndex"].Value;
                            }
                            else
                            {
                                textBox_VCodec.Text = "";
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["AudioCodec"].Value))
                    {
                        case false:
                            checkBox_MAC.Checked = false;
                            label3.Enabled = false;
                            textBox_ACodec.Enabled = false;
                            Audiocodec = "";
                            break;
                        case true:
                            checkBox_MAC.Checked = true;
                            label3.Enabled = true;
                            textBox_ACodec.Enabled = true;
                            if (Config.Entry["AudioCodecIndex"].Value != "")
                            {
                                textBox_ACodec.Text = Config.Entry["AudioCodecIndex"].Value;
                                Audiocodec = " -acodec " + Config.Entry["AudioCodecIndex"].Value;
                            }
                            else
                            {
                                textBox_ACodec.Text = "";
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["AudioBitrate"].Value))
                    {
                        case false:
                            checkBox_AB.Checked = false;
                            label4.Enabled = false;
                            label7.Enabled = false;
                            textBox_AB.Enabled = false;
                            Audiobitrate = "";
                            break;
                        case true:
                            checkBox_AB.Checked = true;
                            label4.Enabled = true;
                            label7.Enabled = true;
                            textBox_AB.Enabled = true;
                            if (Config.Entry["AudioBitrateIndex"].Value != "")
                            {
                                textBox_AB.Text = Config.Entry["AudioBitrateIndex"].Value;
                                Audiobitrate = " -b:a " + Config.Entry["AudioBitrateIndex"].Value + "k";
                            }
                            else
                            {
                                textBox_AB.Text = "";
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["AudioOutputCodec"].Value))
                    {
                        case false:
                            checkBox_AE.Checked = false;
                            comboBox_AE.Enabled = false;
                            label14.Enabled = false;
                            aes = "";
                            break;
                        case true:
                            checkBox_AE.Checked = true;
                            comboBox_AE.Enabled = true;
                            label14.Enabled = true;
                            comboBox_AE.SelectedIndex = int.Parse(Config.Entry["OutputCodecIndex"].Value);
                            aes = int.Parse(Config.Entry["OutputCodecIndex"].Value) switch
                            {
                                0 => " -c:a pcm_s16le",
                                1 => " -c:a pcm_s24le",
                                2 => " -c:a pcm_s32le",
                                3 => " -c:a aac",
                                4 => " -c:a libmp3lame",
                                _ => " -c:a pcm_s24le",
                            };
                            break;
                    }

                    switch (bool.Parse(Config.Entry["H264"].Value))
                    {
                        case false:
                            checkBox_preset.Checked = false;
                            label15.Enabled = false;
                            comboBox_preset.Enabled = false;
                            preset = "";
                            break;
                        case true:
                            checkBox_preset.Checked = true;
                            label15.Enabled = true;
                            comboBox_preset.Enabled = true;
                            switch (int.Parse(Config.Entry["H264Index"].Value))
                            {
                                case 0:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset ultrafast";
                                    break;
                                case 1:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset superfast";
                                    break;
                                case 2:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset veryfast";
                                    break;
                                case 3:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset faster";
                                    break;
                                case 4:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset fast";
                                    break;
                                case 5:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset medium";
                                    break;
                                case 6:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset slow";
                                    break;
                                case 7:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset slower";
                                    break;
                                case 8:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset veryslow";
                                    break;
                                default:
                                    comboBox_preset.SelectedIndex = int.Parse(Config.Entry["H264Index"].Value);
                                    preset = " -preset veryslow";
                                    break;
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["OutputLocation"].Value))
                    {
                        case false:
                            checkBox_MSOL.Checked = false;
                            label10.Enabled = false;
                            label11.Enabled = false;
                            button_VL.Enabled = false;
                            button_AL.Enabled = false;
                            Videoloc = " $OutFile";
                            Audioloc = " $OutFile";
                            break;
                        case true:
                            checkBox_MSOL.Checked = true;
                            label10.Enabled = true;
                            label11.Enabled = true;
                            button_VL.Enabled = true;
                            button_AL.Enabled = true;
                            if (Config.Entry["VideoLocation"].Value != "")
                            {
                                textBox_VL.Text = Config.Entry["VideoLocation"].Value;
                                Videoloc = " " + Config.Entry["VideoLocation"].Value;
                            }
                            else
                            {
                                textBox_VL.Text = "";
                                Videoloc = " $OutFile";
                            }
                            if (Config.Entry["AudioLocation"].Value != "")
                            {
                                textBox_AL.Text = Config.Entry["AudioLocation"].Value;
                                Audioloc = " " + Config.Entry["AudioLocation"].Value;
                            }
                            else
                            {
                                textBox_AL.Text = "";
                                Audioloc = " $OutFile";
                            }
                            break;
                    }

                    switch (bool.Parse(Config.Entry["VideoGeneration"].Value))
                    {
                        case false:
                            checkBox_VOT.Checked = false;
                            comboBox_VOT.Enabled = false;
                            label16.Enabled = false;
                            break;
                        case true:
                            {
                                checkBox_EIA.Checked = false;
                                checkBox_EIA.Enabled = false;
                                checkBox_DS.Checked = false;
                                checkBox_DS.Enabled = false;
                                checkBox_DSS.Checked = false;
                                checkBox_DSS.Enabled = false;
                                checkBox_CC.Enabled = false;
                                checkBox_CC.Checked = false;
                                checkBox_OAO.Enabled = false;
                                checkBox_OAO.Checked = false;
                                checkBox_HI.Enabled = false;
                                checkBox_HI.Checked = false;
                                checkBox_SO.Enabled = false;
                                checkBox_SO.Checked = false;
                                checkBox_MI.Enabled = false;
                                checkBox_MI.Checked = false;
                                checkBox_OOF.Enabled = false;
                                checkBox_OOF.Checked = false;
                                checkBox_NVENC.Enabled = false;
                                checkBox_NVENC.Checked = false;
                                label8.Enabled = false;
                                textBox_CRF.Text = null;
                                textBox_CRF.Enabled = false;
                                checkBox_AE.Enabled = false;
                                checkBox_AE.Checked = false;
                                label14.Enabled = false;
                                comboBox_AE.Enabled = false;
                                checkBox_preset.Enabled = false;
                                checkBox_preset.Checked = false;
                                label15.Enabled = false;
                                comboBox_preset.Enabled = false;
                                textBox_CMDA.Enabled = false;
                                textBox_CMDA.Text = null;
                                textBox_CMDF.Enabled = false;
                                textBox_CMDF.Text = null;
                                textBox_CMDV.Enabled = false;
                                textBox_CMDV.Text = null;

                                checkBox_VOT.Checked = true;
                                comboBox_VOT.Enabled = true;
                                label16.Enabled = true;
                                comboBox_VOT.SelectedIndex = int.Parse(Config.Entry["GenerationIndex"].Value) switch
                                {
                                    0 => int.Parse(Config.Entry["GenerationIndex"].Value),
                                    1 => int.Parse(Config.Entry["GenerationIndex"].Value),
                                    _ => 0,
                                };
                                break;
                            }
                    }

                    if (Config.Entry["VideoParam"].Value != "")
                    {
                        textBox_CMDV.Text = Config.Entry["VideoParam"].Value;
                    }
                    else
                    {
                        textBox_CMDV.Text = pathFF + hidebnr + " -i $InFile" + qv + overw + Videoloc;
                    }
                    if (Config.Entry["AudioParam"].Value != "")
                    {
                        textBox_CMDA.Text = Config.Entry["AudioParam"].Value;
                    }
                    else
                    {
                        textBox_CMDA.Text = pathFF + hidebnr + " -i $InFile" + copychapters + metadatainfo + oao + ds + dds + aes + overw + Audioloc;
                    }
                    if (Config.Entry["MergeParam"].Value != "")
                    {
                        textBox_CMDF.Text = Config.Entry["MergeParam"].Value;
                    }
                    else
                    {
                        textBox_CMDF.Text = pathFF + videofps + hidebnr + intaac + " -i $InImage -i $InAudio -r" + videofps.Replace(" -framerate", "") + qpl + Videocodec + preset + Audiocodec + Audiobitrate + overw + " $OutFile";
                    }

                    break;
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

        private void CheckBox_VOT_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_VOT.Checked != false)
            {
                comboBox_VOT.Enabled = true;
                label16.Enabled = true;
            }
            else
            {
                comboBox_VOT.Enabled = false;
                label16.Enabled = false;
            }
        }

        private void CheckBox_Advanced_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Advanced.Checked != false)
            {
                if (textBox_FPS.Text == "")
                {
                    MessageBox.Show("The FPS has not been specified correctly.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }
                else if (textBox_FPS.TextLength <= 4)
                {
                    MessageBox.Show("The FPS has not been specified correctly.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }
                if (textBox_FFmpeg.Text == "")
                {
                    MessageBox.Show("The location of FFmpeg is not specified.", Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    checkBox_Advanced.Checked = false;
                    return;
                }

                DialogResult dr = MessageBox.Show("Activating advanced setting items may interfere with upscaling.\nIt is recommended that only advanced users use them.\nDo you really want to enable it?", Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
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
                    checkBox_VOT.Enabled = true;
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
                    checkBox_Advanced.Checked = false;
                    return;
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
                checkBox_VOT.Enabled = false;
                checkBox_VOT.Checked = false;
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
                label16.Enabled = false;
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
                Filter = "Application|ffmpeg.exe",
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
            fbd.SelectedPath = Application.ExecutablePath.Replace(Path.GetFileName(Application.ExecutablePath), "");
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
            fbd.SelectedPath = Application.ExecutablePath.Replace(Path.GetFileName(Application.ExecutablePath), "");
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
                MessageBox.Show(Strings.ErrorFPS, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox_FPS.TextLength <= 4)
            {
                MessageBox.Show(Strings.ErrorFPSChar, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox_FPS.Text.Contains('.') != true)
            {
                MessageBox.Show(Strings.ErrorFPSDot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox_FFmpeg.Text == "")
            {
                MessageBox.Show(Strings.ErrorFFmpegPath, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (checkBox_Advanced.Checked != false)
            {
                if (checkBox_NVENC.Checked != false)
                {
                    if (textBox_CRF.TextLength < 1)
                    {
                        MessageBox.Show(Strings.ErrorNVENCNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MVC.Checked != false)
                {
                    if (textBox_VCodec.TextLength < 2)
                    {
                        MessageBox.Show(Strings.ErrorVCodecNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MAC.Checked != false)
                {
                    if (textBox_ACodec.TextLength < 2)
                    {
                        MessageBox.Show(Strings.ErrorACodecNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (checkBox_MSOL.Checked != false)
                {
                    if (textBox_AL.Text == "")
                    {
                        MessageBox.Show(Strings.ErrorALocNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (textBox_VL.Text == "")
                    {
                        MessageBox.Show(Strings.ErrorVLocNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            Config.Entry["FPS"].Value = textBox_FPS.Text;
            Config.Entry["FFmpegLocation"].Value = textBox_FFmpeg.Text;

            switch (checkBox_Advanced.Checked)
            {
                case false:
                    Config.Entry["VAdvanced"].Value = "false";
                    break;
                case true:
                    Config.Entry["VAdvanced"].Value = "true";
                    break;
            }

            switch (checkBox_EIA.Checked)
            {
                case false:
                    Config.Entry["InternalAAC"].Value = "false";
                    break;
                case true:
                    Config.Entry["InternalAAC"].Value = "true";
                    break;
            }

            switch (checkBox_DSS.Checked)
            {
                case false:
                    Config.Entry["Subtitle"].Value = "false";
                    break;
                case true:
                    Config.Entry["Subtitle"].Value = "true";
                    break;
            }

            switch (checkBox_CC.Checked)
            {
                case false:
                    Config.Entry["CopyingChapters"].Value = "false";
                    break;
                case true:
                    Config.Entry["CopyingChapters"].Value = "true";
                    break;
            }
            
            switch (checkBox_OAO.Checked)
            {
                case false:
                    Config.Entry["OutputAudioOnly"].Value = "false";
                    break;
                case true:
                    Config.Entry["OutputAudioOnly"].Value = "true";
                    break;
            }

            switch (checkBox_HI.Checked)
            {
                case false:
                    Config.Entry["HideInformations"].Value = "false";
                    break;
                case true:
                    Config.Entry["HideInformations"].Value = "true";
                    break;
            }

            switch (checkBox_SO.Checked)
            {
                case false:
                    Config.Entry["Sequential"].Value = "false";
                    break;
                case true:
                    Config.Entry["Sequential"].Value = "true";
                    break;
            }

            switch (checkBox_DS.Checked)
            {
                case false:
                    Config.Entry["DataStream"].Value = "false";
                    break;
                case true:
                    Config.Entry["DataStream"].Value = "true";
                    break;
            }

            switch (checkBox_MI.Checked)
            {
                case false:
                    Config.Entry["Metadata"].Value = "false";
                    break;
                case true:
                    Config.Entry["Metadata"].Value = "true";
                    break;
            }

            switch (checkBox_OOF.Checked)
            {
                case false:
                    Config.Entry["Overwrite"].Value = "false";
                    break;
                case true:
                    Config.Entry["Overwrite"].Value = "true";
                    break;
            }

            switch (checkBox_NVENC.Checked)
            {
                case false:
                    Config.Entry["NVENC"].Value = "false";
                    break;
                case true:
                    Config.Entry["NVENC"].Value = "true";
                    break;
            }

            Config.Entry["CRFLevel"].Value = textBox_CRF.Text;
            
            switch (checkBox_MVC.Checked)
            {
                case false:
                    Config.Entry["VideoCodec"].Value = "false";
                    break;
                case true:
                    Config.Entry["VideoCodec"].Value = "true";
                    break;
            }

            Config.Entry["VideoCodecIndex"].Value = textBox_VCodec.Text;

            switch (checkBox_MAC.Checked)
            {
                case false:
                    Config.Entry["AudioCodec"].Value = "false";
                    break;
                case true:
                    Config.Entry["AudioCodec"].Value = "true";
                    break;
            }

            Config.Entry["AudioCodecIndex"].Value = textBox_ACodec.Text;
            
            switch (checkBox_AB.Checked)
            {
                case false:
                    Config.Entry["AudioBitrate"].Value = "false";
                    break;
                case true:
                    Config.Entry["AudioBitrate"].Value = "true";
                    break;
            }

            Config.Entry["AudioBitrateIndex"].Value = textBox_AB.Text;

            switch (checkBox_AE.Checked)
            {
                case false:
                    Config.Entry["AudioOutputCodec"].Value = "false";
                    break;
                case true:
                    Config.Entry["AudioOutputCodec"].Value = "true";
                    break;
            }

            Config.Entry["OutputCodecIndex"].Value = comboBox_AE.SelectedIndex.ToString();

            switch (checkBox_preset.Checked)
            {
                case false:
                    Config.Entry["H264"].Value = "false";
                    break;
                case true:
                    Config.Entry["H264"].Value = "true";
                    break;
            }

            Config.Entry["H264Index"].Value = comboBox_preset.SelectedIndex.ToString();

            switch (checkBox_MSOL.Checked)
            {
                case false:
                    Config.Entry["OutputLocation"].Value = "false";
                    break;
                case true:
                    Config.Entry["OutputLocation"].Value = "true";
                    break;
            }

            Config.Entry["AudioLocation"].Value = textBox_AL.Text;
            Config.Entry["VideoLocation"].Value = textBox_VL.Text;

            switch (checkBox_VOT.Checked)
            {
                case false:
                    Config.Entry["VideoGeneration"].Value = "false";
                    break;
                case true:
                    Config.Entry["VideoGeneration"].Value = "true";
                    break;
            }

            Config.Entry["GenerationIndex"].Value = comboBox_VOT.SelectedIndex.ToString();

            Config.Entry["VideoParam"].Value = cmdparam_v1;
            Config.Entry["AudioParam"].Value = cmdparam_v2;
            Config.Entry["MergeParam"].Value = cmdparam_v3;

            Config.Save(Common.xmlpath);

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

        private void ComboBox_VOT_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_VOT.SelectedIndex)
            {
                case 0:
                    checkBox_EIA.Enabled = true;
                    checkBox_DS.Enabled = true;
                    checkBox_DSS.Enabled = true;
                    checkBox_CC.Enabled = true;
                    checkBox_OAO.Enabled = true;
                    checkBox_HI.Enabled = true;
                    checkBox_SO.Enabled = true;
                    checkBox_MI.Enabled = true;
                    checkBox_OOF.Enabled = true;
                    checkBox_NVENC.Enabled = true;
                    checkBox_AE.Enabled = true;
                    checkBox_preset.Enabled = true;
                    textBox_CMDA.Enabled = true;
                    textBox_CMDF.Enabled = true;
                    textBox_CMDV.Enabled = true;
                    break;
                case 1:
                    checkBox_EIA.Checked = false;
                    checkBox_EIA.Enabled = false;
                    checkBox_DS.Checked = false;
                    checkBox_DS.Enabled = false;
                    checkBox_DSS.Checked = false;
                    checkBox_DSS.Enabled = false;
                    checkBox_CC.Enabled = false;
                    checkBox_CC.Checked = false;
                    checkBox_OAO.Enabled = false;
                    checkBox_OAO.Checked = false;
                    checkBox_HI.Enabled = false;
                    checkBox_HI.Checked = false;
                    checkBox_SO.Enabled = false;
                    checkBox_SO.Checked = false;
                    checkBox_MI.Enabled = false;
                    checkBox_MI.Checked = false;
                    checkBox_OOF.Enabled = false;
                    checkBox_OOF.Checked = false;
                    checkBox_NVENC.Enabled = false;
                    checkBox_NVENC.Checked = false;
                    label8.Enabled = false;
                    textBox_CRF.Text = null;
                    textBox_CRF.Enabled = false;
                    checkBox_AE.Enabled = false;
                    checkBox_AE.Checked = false;
                    label14.Enabled = false;
                    comboBox_AE.Enabled = false;
                    checkBox_preset.Enabled = false;
                    checkBox_preset.Checked = false;
                    label15.Enabled = false;
                    comboBox_preset.Enabled = false;
                    textBox_CMDA.Enabled = false;
                    textBox_CMDA.Text = null;
                    textBox_CMDF.Enabled = false;
                    textBox_CMDF.Text = null;
                    textBox_CMDV.Enabled = false;
                    textBox_CMDV.Text = null;
                    break;
                default:
                    checkBox_EIA.Enabled = true;
                    checkBox_DS.Enabled = true;
                    checkBox_DSS.Enabled = true;
                    checkBox_CC.Enabled = true;
                    checkBox_OAO.Enabled = true;
                    checkBox_HI.Enabled = true;
                    checkBox_SO.Enabled = true;
                    checkBox_MI.Enabled = true;
                    checkBox_OOF.Enabled = true;
                    checkBox_NVENC.Enabled = true;
                    checkBox_AE.Enabled = true;
                    checkBox_preset.Enabled = true;
                    textBox_CMDA.Enabled = true;
                    textBox_CMDF.Enabled = true;
                    textBox_CMDV.Enabled = true;
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
