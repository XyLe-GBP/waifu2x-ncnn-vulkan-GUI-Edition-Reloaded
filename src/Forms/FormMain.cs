using NVGE.Localization;
using OpenCvSharp;
using PrivateProfile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormMain : Form
    {
        #region NetworkCommon
        private static readonly HttpClientHandler handler = new()
        {
            UseProxy = false,
            UseCookies = false
        };
        private static readonly HttpClient appUpdatechecker = new(handler);
        private static readonly HttpClient FFupdatechecker = new(handler);
        #endregion

        public FormMain()
        {
            InitializeComponent();
            pictureBox_DD.AllowDrop = true;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            Text = "waifu2x-nvge ( build: " + ver.FileVersion.ToString() + "-Beta )";
            label1.Text = Strings.DragDropCaption;
            var ini = new IniFile(@".\settings.ini");
            string[] OSInfo = new string[17];
            string[] CPUInfo = new string[3];
            string[] GPUInfo = new string[3];
            SystemInfo.GetSystemInformation(OSInfo);
            SystemInfo.GetProcessorsInformation(CPUInfo);
            SystemInfo.GetVideoControllerInformation(GPUInfo);
            ResetLabels();
            label_OS.Text = OSInfo[1] + " - " + OSInfo[3] + " [ build: " + OSInfo[4] + " ]";
            label_Processor.Text = CPUInfo[0] + " [ " + CPUInfo[1] + " Core / " + CPUInfo[2] + " Threads ]";
            label_Graphic.Text = GPUInfo[0] + " - " + GPUInfo[1] + " [ " + GPUInfo[2] + " RAM ]";
            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
            Common.FFmpegPath = ini.GetString("VIDEO_SETTINGS", "FFMPEG_INDEX");
            Common.ImageParam = ini.GetString("IMAGE_SETTINGS", "PARAMS");
            Common.VideoParam = ini.GetString("VIDEO_SETTINGS", "CMDV_INDEX");
            Common.AudioParam = ini.GetString("VIDEO_SETTINGS", "CMDA_INDEX");
            Common.MergeParam = ini.GetString("VIDEO_SETTINGS", "CMDF_INDEX");
            if (Directory.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\"))
            {
                Common.DeleteDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\");
            }
            var dic = new Dictionary<string, string>()
            {
                { "Hokkaido"   ,""},
                { "Osaka"   ,""},
                { "Nagoya"  ,""},
                { "Fukuoka" ,""}
            };
            Json.SerializeJson(dic);
            if (ini.GetString("VIDEO_SETTINGS", "VL_INDEX") != "")
            {
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames");
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames2x");
                Common.DeletePathFrames = ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames";
                Common.DeletePathFrames2x = ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames2x";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x");
                Common.DeletePathFrames = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                Common.DeletePathFrames2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
            }
            if (ini.GetString("VIDEO_SETTINGS", "AL_INDEX") != "")
            {
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "AL_INDEX") + @"\audio");
                Common.DeletePathAudio = ini.GetString("VIDEO_SETTINGS", "AL_INDEX") + @"\audio";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\audio");
                Common.DeletePathAudio = Directory.GetCurrentDirectory() + @"\_temp-project\audio";
            }
            
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images");

            string hv = null!;

            using Stream hcs = await Task.Run(() => Network.GetWebStreamAsync(FFupdatechecker, Network.GetUri("https://www.gyan.dev/ffmpeg/builds/release-version")));
            using StreamReader hsr = new(hcs);
            hv = await Task.Run(() => hsr.ReadToEndAsync());

            Common.ProgMin = 0;
            using FormProgress Form = new(6);

            // FFmpeg not exist
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
            {
                DialogResult dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Form.ShowDialog();

                    if (Common.AbortFlag != 0)
                    {
                        Common.DlcancelFlag = 0;
                        ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                        MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                        {
                            using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.FullName == "ffmpeg-" + hv + "-essentials_build/bin/ffmpeg.exe")
                                {
                                    entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                                }
                            }

                            if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                ini.WriteString("FFMPEG", "LATEST_VERSION", hv);
                                MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                            MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                    }
                }
                else
                {
                    return;
                }
            }
            else if (ini.GetString("FFMPEG", "LATEST_VERSION") == "") // Unknown version ffmpeg
            {
                DialogResult dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                    }
                    Form.ShowDialog();

                    if (Common.AbortFlag != 0)
                    {
                        Common.DlcancelFlag = 0;
                        ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                        MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                        {
                            using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                            foreach (ZipArchiveEntry entry in archive.Entries)
                            {
                                if (entry.FullName == "ffmpeg-" + hv + "-essentials_build/bin/ffmpeg.exe")
                                {
                                    entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                                }
                            }

                            if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                ini.WriteString("FFMPEG", "LATEST_VERSION", hv);
                                MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                            MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else // Update ffmpeg
            {
                switch (ini.GetString("FFMPEG", "LATEST_VERSION").CompareTo(hv))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, Strings.LatestString + hv + "\n" + Strings.CurrentString + ini.GetString("FFMPEG", "LATEST_VERSION") + "\n" + Strings.FFUpdate, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                            {
                                File.Delete(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                Form.ShowDialog();

                                if (Common.AbortFlag != 0)
                                {
                                    Common.DlcancelFlag = 0;
                                    ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                                    {
                                        using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                                        foreach (ZipArchiveEntry entry in archive.Entries)
                                        {
                                            if (entry.FullName == "ffmpeg-" + hv + "-essentials_build/bin/ffmpeg.exe")
                                            {
                                                entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                                            }
                                        }

                                        if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                                        {
                                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                            ini.WriteString("FFMPEG", "LATEST_VERSION", hv);
                                            MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                                        MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    Form.ShowDialog();

                                    if (Common.AbortFlag != 0)
                                    {
                                        Common.DlcancelFlag = 0;
                                        ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                                        MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                                        {
                                            using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                                            foreach (ZipArchiveEntry entry in archive.Entries)
                                            {
                                                if (entry.FullName == "ffmpeg-" + hv + "-essentials_build/bin/ffmpeg.exe")
                                                {
                                                    entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                                                }
                                            }

                                            if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                                            {
                                                ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                                ini.WriteString("FFMPEG", "LATEST_VERSION", hv);
                                                MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", "");
                                            MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                            return;
                        }
                        else
                        {
                            return;
                        }
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }
            }
        }

        private void OpenImegeIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterImage,
                FilterIndex = 11,
                Title = Strings.OFDImageTitle,
                Multiselect = true,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                List<string> lst = new();
                foreach (string files in ofd.FileNames)
                {
                    lst.Add(files);
                }
                Common.ImageFile = lst.ToArray();
                if (Common.ImageFile.Length == 1)
                {
                    FileInfo file = new(ofd.FileName);
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = ofd.FileName;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    closeFileCToolStripMenuItem.Enabled = true;
                    label1.Visible = false;
                    ImageConvert.IMAGEtoPNG32Async(file.FullName, Directory.GetCurrentDirectory() + @"\tmp.png");
                    pictureBox_DD.ImageLocation = Directory.GetCurrentDirectory() + @"\tmp.png";
                    return;
                }
                else
                {
                    long FS = 0;
                    foreach (string file in Common.ImageFile)
                    {
                        FileInfo fi = new(file);
                        FS += fi.Length;
                    }
                    string sz = string.Format("{0} ", FS);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = Strings.IMGS;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    closeFileCToolStripMenuItem.Enabled = true;
                    label1.Text = Strings.MultipleImageCaption;
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void OpenVideoVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterVideo,
                FilterIndex = 1,
                Title = Strings.OFDVideoTitle,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Common.VideoPath = ofd.FileName;
                FileInfo file = new(ofd.FileName);
                long FileSize = file.Length;
                string sz = string.Format("{0} ", FileSize);
                toolStripStatusLabel_Status.Text = Strings.ReadedString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                ReadLabels();
                label_File.Text = ofd.FileName;
                label_Size.Text = sz + Strings.SizeString;
                button_Video.Enabled = true;
                closeFileCToolStripMenuItem.Enabled = true;
                label1.Text = Strings.VideoCaption;

                using var video = new VideoCapture(Common.VideoPath);
                switch (video.Fps.ToString())
                {
                    case "30":
                        ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("30", "30.00"));
                        break;
                    case "60":
                        ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("60", "60.00"));
                        break;
                    default:
                        if (video.Fps.ToString().Length > 5)
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Substring(0, 5));
                        }
                        else
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString());
                        }
                        break;
                }

                return;
            }
            else
            {
                return;
            }
        }

        private void CloseFileCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.ImageFile == null && Common.VideoPath == null || Common.VideoPath == "")
            {
                MessageBox.Show(Strings.FileNotReadedWarning, Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (button_Image.Enabled == true)
                {
                    button_Image.Enabled = false;
                }
                if (button_Video.Enabled == true)
                {
                    button_Video.Enabled = false;
                }
                if (button_Merge.Enabled == true)
                {
                    button_Merge.Enabled = false;
                }
                button_Video.Text = Strings.ButtonUpscaleVideo;
                ResetLabels();
                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                closeFileCToolStripMenuItem.Enabled = false;
                label1.Visible = true;
                label1.Text = Strings.DragDropCaption;
                pictureBox_DD.Image = null;
                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
            }
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void UpscalingSettingsUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            Form form = new FormImageUpscaleSettings();
            form.ShowDialog();
            form.Dispose();
            Common.ImageParam = ini.GetString("IMAGE_SETTINGS", "PARAMS");
            return;
        }

        private void VideoUpscalingSettingsVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            Form form = new FormVideoUpscaleSettings();
            form.ShowDialog();
            form.Dispose();
            if (ini.GetString("VIDEO_SETTINGS", "VL_INDEX") != "")
            {
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames");
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames2x");
                Common.DeletePathFrames = ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames";
                Common.DeletePathFrames2x = ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames2x";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x");
                Common.DeletePathFrames = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                Common.DeletePathFrames2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
            }
            if (ini.GetString("VIDEO_SETTINGS", "AL_INDEX") != "")
            {
                Directory.CreateDirectory(ini.GetString("VIDEO_SETTINGS", "AL_INDEX") + @"\audio");
                Common.DeletePathAudio = ini.GetString("VIDEO_SETTINGS", "AL_INDEX") + @"\audio";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\audio");
                Common.DeletePathAudio = Directory.GetCurrentDirectory() + @"\_temp-project\audio";
            }
            Common.FFmpegPath = ini.GetString("VIDEO_SETTINGS", "FFMPEG_INDEX");
            Common.VideoParam = ini.GetString("VIDEO_SETTINGS", "CMDV_INDEX");
            Common.AudioParam = ini.GetString("VIDEO_SETTINGS", "CMDA_INDEX");
            Common.MergeParam = ini.GetString("VIDEO_SETTINGS", "CMDF_INDEX");
            return;
        }

        private void ChangeVideoResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            string ffp = ini.GetString("VIDEO_SETTINGS", "FFMPEG_INDEX");
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterVideo,
                FilterIndex = 1,
                Title = Strings.VROFDTitle,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Common.VROpenPath = ofd.FileName;
                SaveFileDialog sfd = new()
                {
                    FileName = "",
                    InitialDirectory = "",
                    Filter = Strings.FilterVideo,
                    FilterIndex = 1,
                    Title = Strings.VRSFDTitle,
                    OverwritePrompt = true,
                    RestoreDirectory = true
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Common.VRSavePath = sfd.FileName;
                    Form form = new FormVideoResolution();
                    form.ShowDialog();
                    form.Dispose();

                    if (Common.VRCFlag != 1)
                    {
                        return;
                    }
                    else
                    {
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = ffp;
                        pi.Arguments = Common.VRParam.Replace("$InFile", Common.VROpenPath).Replace("$OutFile", Common.VRSavePath);
                        pi.WindowStyle = ProcessWindowStyle.Normal;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);
                        ps.WaitForExit();

                        if (File.Exists(Common.VRSavePath))
                        {
                            MessageBox.Show(Strings.VRCNG, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Strings.VRCNGError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void AboutWaifu2xncnnvulkanGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout Form = new();
            Form.ShowDialog();
            Form.Dispose();
            return;
        }

        private async void CheckForUpdatesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string hv = null!;

                using Stream hcs = await Task.Run(() => Network.GetWebStreamAsync(appUpdatechecker, Network.GetUri("https://raw.githubusercontent.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/master/VERSIONINFO")));
                using StreamReader hsr = new(hcs);
                hv = await Task.Run(() => hsr.ReadToEndAsync());

                FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

                switch (ver.FileVersion.ToString().CompareTo(hv[8..].Replace("\n", "")))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, Strings.LatestString + hv[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.UpdateConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            Common.OpenURI("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases");
                            return;
                        }
                        else
                        {
                            return;
                        }
                    case 0:
                        MessageBox.Show(this, Strings.LatestString + hv[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.Uptodate, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        throw new Exception(hv[8..].Replace("\n", "").ToString() + " < " + ver.FileVersion.ToString());
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, string.Format(Strings.UnExpectedError, ex.ToString()), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void Button_Image_Click(object sender, EventArgs e)
        {
            if (Common.ImageParam.Length < 69 || Common.ImageParam == "")
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ini = new IniFile(@".\settings.ini");
            int fmt = ini.GetInt("IMAGE_SETTINGS", "FORMAT_INDEX", 65535);
            string ft;
            if (Common.ImageFile.Length != 0)
            {
                if (Common.ImageFile.Length <= 1)
                {
                    string ext;
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");

                    switch (fmt)
                    {
                        case 0:
                            ft = "Joint Photographic Experts Group (*.jpg)|*.jpg";
                            ext = ".jpg";
                            break;
                        case 1:
                            ft = "Portable Network Graphics (*.png)|*.png";
                            ext = ".png";
                            break;
                        case 2:
                            ft = "Google webp (*.webp)|*.webp";
                            ext = ".webp";
                            break;
                        case 3:
                            ft = "Icon (*.ico)|*.ico";
                            ext = ".ico";
                            break;
                        default:
                            ft = "Portable Network Graphics (*.png)|*.png";
                            ext = ".png";
                            break;
                    }

                    Common.ConvMultiFlag = 0;
                    Common.ProgMin = 0;
                    Common.ProgMax = Common.ImageFile.Length;

                    using FormProgress Form1 = new(7);
                    Form1.ShowDialog();

                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");

                    using FormProgress Form2 = new(0);
                    Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + Common.ImageFileName[0].Replace(Common.ImageFileExt[0], ext);
                    Common.ProgMin = 0;
                    Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                    Common.stopwatch = new Stopwatch();
                    Common.stopwatch.Start();

                    Form2.ShowDialog();

                    if (fmt == 3)
                    {
                        bool err = ImageConvert.IMAGEtoICON(Common.SFDSavePath.Replace(".ico", ".png"), Common.SFDSavePath);
                        File.Delete(Common.SFDSavePath.Replace(".ico", ".png"));
                        if (err != true)
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                            ResetLabels();
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            label1.Visible = true;
                            label1.Text = Strings.DragDropCaption;
                            pictureBox_DD.Image = null;
                            File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                            MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (Common.AbortFlag != 0)
                    {
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                        ResetLabels();
                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                        button_Image.Enabled = false;
                        closeFileCToolStripMenuItem.Enabled = false;
                        label1.Visible = true;
                        label1.Text = Strings.DragDropCaption;
                        pictureBox_DD.Image = null;
                        File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                        Common.stopwatch.Stop();
                        Common.stopwatch.Reset();
                        return;
                    }

                    Common.stopwatch.Stop();
                    Common.timeSpan = Common.stopwatch.Elapsed;

                    FormImageUpscaleDetail formImageUpscaleDetail = new();
                    formImageUpscaleDetail.ShowDialog();
                    formImageUpscaleDetail.Dispose();

                    Common.stopwatch.Reset();

                    if (Common.ImgDetFlag == 0)
                    {
                        SaveFileDialog sfd = new()
                        {
                            FileName = Common.SFDRandomNumber() + "_Upscaled",
                            InitialDirectory = "",
                            Filter = ft,
                            FilterIndex = 1,
                            Title = "",
                            OverwritePrompt = true,
                            RestoreDirectory = true
                        };
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            if (File.Exists(sfd.FileName))
                            {
                                File.Delete(sfd.FileName);
                                File.Move(Common.SFDSavePath, sfd.FileName);
                            }
                            else
                            {
                                File.Move(Common.SFDSavePath, sfd.FileName);
                            }
                            
                            if (File.Exists(sfd.FileName))
                            {
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Visible = true;
                                label1.Text = Strings.DragDropCaption;
                                pictureBox_DD.Image = null;
                                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                                MessageBox.Show(Strings.IMGUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start("EXPLORER.EXE", @"/select,""" + sfd.FileName + @"""");
                            }
                            else
                            {
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Visible = true;
                                label1.Text = Strings.DragDropCaption;
                                pictureBox_DD.Image = null;
                                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                                MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                            ResetLabels();
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            label1.Visible = true;
                            label1.Text = Strings.DragDropCaption;
                            pictureBox_DD.Image = null;
                            File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                        }
                    }
                    else
                    {
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                        ResetLabels();
                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                        button_Image.Enabled = false;
                        closeFileCToolStripMenuItem.Enabled = false;
                        label1.Visible = true;
                        label1.Text = Strings.DragDropCaption;
                        pictureBox_DD.Image = null;
                        File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                    }

                }
                else
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");

                    Common.ConvMultiFlag = 1;
                    Common.ProgMin = 0;
                    Common.ProgMax = Common.ImageFile.Length;

                    using FormProgress Form1 = new(7);
                    Form1.ShowDialog();

                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");

                    using FormProgress Form2 = new(1);
                    Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                    Common.ProgMin = 0;
                    Common.ProgMax = Common.ImageFile.Length;

                    Common.stopwatch = new Stopwatch();
                    Common.stopwatch.Start();

                    Form2.ShowDialog();

                    if (fmt == 3)
                    {
                        foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                        {
                            FileInfo fi = new(file);
                            bool err = ImageConvert.IMAGEtoICON(Common.FBDSavePath + @"\" + fi.Name, Common.FBDSavePath + @"\" + fi.Name.Replace(fi.Extension, ".ico"));
                            File.Delete(Common.FBDSavePath + @"\" + fi.Name);
                            if (err != true)
                            {
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Text = Strings.DragDropCaption;
                                MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    if (Common.AbortFlag != 0)
                    {
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                        ResetLabels();
                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                        button_Image.Enabled = false;
                        closeFileCToolStripMenuItem.Enabled = false;
                        label1.Text = Strings.DragDropCaption;
                        Common.stopwatch.Stop();
                        Common.stopwatch.Reset();
                        return;
                    }

                    Common.stopwatch.Stop();
                    Common.timeSpan = Common.stopwatch.Elapsed;

                    FormImageUpscaleDetail formImageUpscaleDetail = new();
                    formImageUpscaleDetail.ShowDialog();
                    formImageUpscaleDetail.Dispose();

                    Common.stopwatch.Reset();

                    if (Common.ImgDetFlag == 0)
                    {
                        FolderBrowserDialog fbd = new();
                        fbd.Description = Strings.FBDImageTitle;
                        fbd.RootFolder = Environment.SpecialFolder.Desktop;
                        fbd.SelectedPath = Application.ExecutablePath.Replace(Path.GetFileName(Application.ExecutablePath), "");
                        fbd.ShowNewFolderButton = true;

                        if (fbd.ShowDialog(this) == DialogResult.OK)
                        {
                            Common.ProgressFlag = 1;
                            Common.FBDSavePath = fbd.SelectedPath;
                            Common.ProgMin = 0;
                            Common.ProgMax = Common.ImageFile.Length;

                            if (Directory.GetFiles(Common.FBDSavePath, "*.*").Length != 0)
                            {
                                DialogResult dr = MessageBox.Show(Strings.MSGAlready, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    Common.DeleteDirectoryFiles(Common.FBDSavePath);
                                    foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                                    {
                                        FileInfo fi = new(file);
                                        File.Move(file, Common.FBDSavePath + @"\" + fi.Name);
                                        if (!File.Exists(Common.FBDSavePath + @"\" + fi.Name))
                                        {
                                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                            ResetLabels();
                                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                            button_Image.Enabled = false;
                                            closeFileCToolStripMenuItem.Enabled = false;
                                            label1.Text = Strings.DragDropCaption;
                                            MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                        
                                    }
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                    ResetLabels();
                                    toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                    button_Image.Enabled = false;
                                    closeFileCToolStripMenuItem.Enabled = false;
                                    label1.Text = Strings.DragDropCaption;
                                    MessageBox.Show(Strings.IMGUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Process.Start("EXPLORER.EXE", Common.FBDSavePath);
                                }
                                else
                                {
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                    ResetLabels();
                                    toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                    button_Image.Enabled = false;
                                    closeFileCToolStripMenuItem.Enabled = false;
                                    label1.Text = Strings.DragDropCaption;
                                    return;
                                }
                            }
                            else
                            {
                                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                                {
                                    FileInfo fi = new(file);
                                    File.Move(file, Common.FBDSavePath + @"\" + fi.Name);
                                    if (!File.Exists(Common.FBDSavePath + @"\" + fi.Name))
                                    {
                                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                        ResetLabels();
                                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                        button_Image.Enabled = false;
                                        closeFileCToolStripMenuItem.Enabled = false;
                                        label1.Text = Strings.DragDropCaption;
                                        MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    if (fmt == 3)
                                    {
                                        ImageConvert.IMAGEtoICON(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png", Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".ico");
                                        File.Delete(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png");
                                    }
                                }
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Text = Strings.DragDropCaption;
                                MessageBox.Show(Strings.IMGUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start("EXPLORER.EXE", Common.FBDSavePath);
                            }
                        }
                        else
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                            ResetLabels();
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            label1.Text = Strings.DragDropCaption;
                            return;
                        }
                    }
                    else
                    {
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                        Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                        ResetLabels();
                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                        button_Image.Enabled = false;
                        closeFileCToolStripMenuItem.Enabled = false;
                        label1.Text = Strings.DragDropCaption;
                        return;
                    }
                }
            }
        }

        private void Button_Video_Click(object sender, EventArgs e)
        {
            if (Common.ImageParam.Length < 69 || Common.ImageParam == "")
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Common.VideoParam.Length <= 50 || Common.VideoParam == "")
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Common.AudioParam.Length <= 50 || Common.AudioParam == "")
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var ini = new IniFile(@".\settings.ini");
            int enc = ini.GetInt("VIDEO_SETTINGS", "ENCODE_INDEX", 65535);
            string vl = ini.GetString("VIDEO_SETTINGS", "VL_INDEX");
            string al = ini.GetString("VIDEO_SETTINGS", "AL_INDEX");
            string delvlpath, delvlpath2x, alpath, acodec;

            switch (enc)
            {
                case 3:
                    acodec = "audio.m4a";
                    break;
                case 4:
                    acodec = "audio.mp3";
                    break;
                default:
                    acodec = "audio.wav";
                    break;
            }

            if (Common.UpscaleFlag == 0)
            {
                using FormProgress Form1 = new(5);
                using FormProgress Form2 = new(2);
                if (vl != "")
                {
                    if (!Directory.Exists(vl + @"\image-frames\"))
                    {
                        Directory.CreateDirectory(vl + @"\image-frames\");
                        Directory.CreateDirectory(vl + @"\image-frames2x\");
                    }
                    delvlpath = vl + @"\image-frames\";
                }
                else
                {
                    delvlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                }
                if (al != "")
                {
                    if (!Directory.Exists(al + @"\audio\"))
                    {
                        Directory.CreateDirectory(al + @"\audio\");
                    }
                    alpath = al + @"\audio\" + acodec;
                }
                else
                {
                    alpath = Directory.GetCurrentDirectory() + @"\_temp-project\audio\" + acodec;
                }

                using var video = new VideoCapture(Common.VideoPath);

                Common.DeletePath = delvlpath;
                Common.ProgMin = 0;
                Common.ProgMax = video.FrameCount;

                Form1.ShowDialog();

                if (Common.AbortFlag != 0)
                {
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames);
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames2x);
                    Common.DeleteDirectoryFiles(Common.DeletePathAudio);
                    return;
                }

                if (!File.Exists(alpath))
                {
                    MessageBox.Show(Strings.AudioNot, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Common.ProgressFlag = 2;
                Common.ProgMin = 0;
                Common.ProgMax = Directory.GetFiles(delvlpath, "*.*").Length;

                Form2.ShowDialog();

                if (Common.AbortFlag != 0)
                {
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames);
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames2x);
                    Common.DeleteDirectoryFiles(Common.DeletePathAudio);
                    return;
                }

                Common.UpscaleFlag = 1;
                button_Video.Text = Strings.ButtonReUpscaleVideo;
                button_Merge.Enabled = true;
                MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using FormProgress Form1 = new(4);
                using FormProgress Form2 = new(3);
                if (vl != "")
                {
                    if (!Directory.Exists(vl + @"\image-frames\"))
                    {
                        Directory.CreateDirectory(vl + @"\image-frames\");
                        Directory.CreateDirectory(vl + @"\image-frames2x\");
                    }
                    delvlpath = vl + @"\image-frames\";
                    delvlpath2x = vl + @"\image-frames2x\";
                }
                else
                {
                    delvlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                    delvlpath2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
                }

                Common.DeleteFlag = 0;
                Common.ProgMin = 0;
                Common.ProgMax = Directory.GetFiles(Common.DeletePath, "*.*").Length;

                Form1.ShowDialog();

                foreach (var file in Directory.GetFiles(delvlpath2x, "*.*"))
                {
                    FileInfo fi = new(file);
                    File.Move(file, delvlpath + "\\" + fi.Name);
                }

                Common.ProgMin = 0;
                Common.ProgMax = Directory.GetFiles(delvlpath, "*.*").Length;

                Form2.ShowDialog();

                if (Common.AbortFlag != 0)
                {
                    Common.UpscaleFlag = 0;
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames);
                    Common.DeleteDirectoryFiles(Common.DeletePathFrames2x);
                    Common.DeleteDirectoryFiles(Common.DeletePathAudio);
                    button_Video.Text = Strings.ButtonUpscaleVideo;
                    return;
                }

                Common.UpscaleFlag = 1;
                MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button_Merge_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            int cvot = ini.GetInt("VIDEO_SETTINGS", "CVOT_INDEX", 65535), vot = ini.GetInt("VIDEO_SETTINGS", "VOT_INDEX", 65535);

            if (cvot != 0)
            {
                if (vot != 0) // OpenCV
                {
                    SaveFileDialog sfd = new()
                    {
                        FileName = Common.SFDRandomNumber() + "_Upscaled",
                        InitialDirectory = "",
                        Filter = Strings.FilterVideo,
                        FilterIndex = 1,
                        Title = Strings.SFDVideoTitle,
                        OverwritePrompt = true,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Common.SFDSavePath = sfd.FileName;

                        string sfps = ini.GetString("VIDEO_SETTINGS", "FPS_INDEX");
                        if (sfps != "")
                        {
                            if (sfps.Contains('.'))
                            {
                                float fps = float.Parse(sfps);
                                Mat mat;

                                double[] imgsize = Common.GetImageSize(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x", "*")[0]);

                                using var writer = new VideoWriter(sfd.FileName, FourCC.H264, fps, new OpenCvSharp.Size(imgsize[0], imgsize[1]));

                                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x", "*"))
                                {
                                    mat = Cv2.ImRead(item);
                                    writer.Write(mat);
                                    mat.Dispose();
                                }
                            }
                            else
                            {
                                int fps = int.Parse(sfps);
                                Mat mat;

                                double[] imgsize = Common.GetImageSize(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x", "*")[0]);

                                using var writer = new VideoWriter(sfd.FileName, FourCC.H264, fps, new OpenCvSharp.Size(imgsize[0], imgsize[1]));

                                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x", "*"))
                                {
                                    mat = Cv2.ImRead(item);
                                    writer.Write(mat);
                                    mat.Dispose();
                                }
                            }

                            using FormProgress Form = new(4);
                            if (File.Exists(Common.SFDSavePath))
                            {
                                Common.ProgressFlag = 4;
                                Common.DeleteFlag = 1;
                                Common.ProgMin = 0;
                                Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                                Form.ShowDialog();

                                Common.UpscaleFlag = 0;
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Video.Text = Strings.ButtonUpscaleVideo;
                                button_Video.Enabled = false;
                                button_Merge.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Text = Strings.DragDropCaption;
                                MessageBox.Show(Strings.VRUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start("EXPLORER.EXE", @"/select,""" + Common.SFDSavePath + @"""");
                            }
                            else
                            {
                                Common.ProgressFlag = 4;
                                Common.DeleteFlag = 1;
                                Common.ProgMin = 0;
                                Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                                Form.ShowDialog();

                                Common.UpscaleFlag = 0;
                                button_Video.Text = Strings.ButtonUpscaleVideo;
                                ResetLabels();
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Video.Enabled = false;
                                button_Merge.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                label1.Text = Strings.DragDropCaption;
                                MessageBox.Show(Strings.VRUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else // FFmpeg
                {
                    if (Common.MergeParam.Length <= 70)
                    {
                        MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int enc = ini.GetInt("VIDEO_SETTINGS", "ENCODE_INDEX", 65535);
                    string ffp = ini.GetString("VIDEO_SETTINGS", "FFMPEG_INDEX");
                    string vl = ini.GetString("VIDEO_SETTINGS", "VL_INDEX");
                    string al = ini.GetString("VIDEO_SETTINGS", "AL_INDEX");
                    string vlpath, alpath, acodec;
                    switch (enc)
                    {
                        case 3:
                            acodec = "audio.m4a";
                            break;
                        case 4:
                            acodec = "audio.mp3";
                            break;
                        default:
                            acodec = "audio.wav";
                            break;
                    }
                    if (vl != "")
                    {
                        vlpath = vl + @"\image-frames2x\image-%09d.png";
                    }
                    else
                    {
                        vlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x\image-%09d.png";
                    }
                    if (al != "")
                    {
                        alpath = al + @"\audio\" + acodec;
                    }
                    else
                    {
                        alpath = Directory.GetCurrentDirectory() + @"\_temp-project\audio\" + acodec;
                    }

                    SaveFileDialog sfd = new()
                    {
                        FileName = Common.SFDRandomNumber() + "_Upscaled",
                        InitialDirectory = "",
                        Filter = Strings.FilterVideo,
                        FilterIndex = 1,
                        Title = Strings.SFDVideoTitle,
                        OverwritePrompt = true,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        Common.SFDSavePath = sfd.FileName;
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = ffp;
                        pi.Arguments = Common.MergeParam.Replace("$InImage", vlpath).Replace("$InAudio", alpath).Replace("$OutFile", "\"" + Common.SFDSavePath + "\"").Replace(Common.FFmpegPath + " ", "");
                        pi.WindowStyle = ProcessWindowStyle.Normal;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);
                        ps.WaitForExit();

                        using FormProgress Form = new(4);

                        if (File.Exists(Common.SFDSavePath))
                        {
                            Common.DeleteFlag = 1;
                            Common.ProgMin = 0;
                            Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                            Form.ShowDialog();

                            Common.UpscaleFlag = 0;
                            ResetLabels();
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Video.Text = Strings.ButtonUpscaleVideo;
                            button_Video.Enabled = false;
                            button_Merge.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            label1.Text = Strings.DragDropCaption;
                            MessageBox.Show(Strings.VRUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("EXPLORER.EXE", @"/select,""" + Common.SFDSavePath + @"""");
                        }
                        else
                        {
                            Common.DeleteFlag = 1;
                            Common.ProgMin = 0;
                            Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                            Form.ShowDialog();

                            Common.UpscaleFlag = 0;
                            button_Video.Text = Strings.ButtonUpscaleVideo;
                            ResetLabels();
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Video.Enabled = false;
                            button_Merge.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            label1.Text = Strings.DragDropCaption;
                            MessageBox.Show(Strings.VRUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            string vl = ini.GetString("VIDEO_SETTINGS", "VL_INDEX");
            string vlpath;

            if (vl != "")
            {
                vlpath = vl + @"\image-frames2x";
            }
            else
            {
                vlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
            }

            DialogResult dr;

            if (Directory.GetFiles(vlpath, "*.*").Length != 0)
            {
                dr = MessageBox.Show(Strings.ConfirmClose, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            if (ini.GetString("VIDEO_SETTINGS", "VL_INDEX") != "")
            {
                Common.DeleteDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames");
                Common.DeleteDirectory(ini.GetString("VIDEO_SETTINGS", "VL_INDEX") + @"\image-frames2x");
            }
            if (ini.GetString("VIDEO_SETTINGS", "AL_INDEX") != "")
            {
                Common.DeleteDirectory(ini.GetString("VIDEO_SETTINGS", "AL_INDEX") + @"\audio");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\tmp.png"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
            }
            Common.DeleteDirectory(Directory.GetCurrentDirectory() + @"\_temp-project");
        }

        private void ResetStatusALL()
        {
            Common.UpscaleFlag = 0;
            label_File.Text = Strings.NotReadedString;
            label_Size.Text = Strings.NotReadedString;
            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
            button_Video.Text = Strings.ButtonUpscaleVideo;
            button_Image.Enabled = false;
            button_Video.Enabled = false;
            button_Merge.Enabled = false;
            closeFileCToolStripMenuItem.Enabled = false;
            label1.Visible = true;
            label1.Text = Strings.DragDropCaption;
            pictureBox_DD.Image = null;
        }

        private void ResetLabels()
        {
            label_default.Visible = true;
            label_default.Text = Strings.NotReadedString;
            label_File.Visible = false;
            label_Size.Visible = false;
        }

        private void ReadLabels()
        {
            label_default.Visible = false;
            label_File.Visible = true;
            label_Size.Visible = true;
        }

        private void Label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Label1_DragDrop(object sender, DragEventArgs e)
        {
            string[] dd = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> lst = new();
            foreach (string files in dd)
            {
                lst.Add(files);
            }
            Common.ImageFile = lst.ToArray();

            if (Common.ImageFile.Length == 1)
            {
                FileInfo file = new(dd[0]);
                string ext = file.Extension.ToUpper();
                if (ext == ".BMP" || ext == ".DIB" || ext == ".EPS" || ext == ".WEBP" || ext == ".GIF" || ext == ".ICO" || ext == ".ICNS" || ext == ".JFIF" || ext == ".JPG" || ext == ".JPE" || ext == ".JPEG" || ext == ".PJPEG" || ext == ".PJP" || ext == ".PNG" || ext == ".PICT" || ext == ".SVG" || ext == ".SVGZ" || ext == ".TIF" || ext == ".TIFF")
                {
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = dd[0];
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    label1.Visible = false;

                    ImageConvert.IMAGEtoPNG32Async(file.FullName, Directory.GetCurrentDirectory() + @"\tmp.png");

                    pictureBox_DD.ImageLocation = Directory.GetCurrentDirectory() + @"\tmp.png";
                    closeFileCToolStripMenuItem.Enabled = true;
                }
                else if (ext == ".AVI" || ext == ".MP4" || ext == ".MOV" || ext == ".WMV" || ext == ".MKV" || ext == ".WEBM")
                {
                    IniFile ini = new(@".\settings.ini");

                    Common.VideoPath = file.FullName;
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = file.FullName;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Video.Enabled = true;
                    label1.Text = Strings.VideoCaption;
                    closeFileCToolStripMenuItem.Enabled = true;

                    var video = new VideoCapture(Common.VideoPath);
                    switch (video.Fps.ToString())
                    {
                        case "30":
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("30", "30.00"));
                            break;
                        case "60":
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("60", "60.00"));
                            break;
                        default:
                            if (video.Fps.ToString().Length > 5)
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Substring(0, 5));
                            }
                            else
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString());
                            }
                            break;
                    }

                    video.Dispose();

                    return;
                }
                else
                {
                    MessageBox.Show(Strings.FmtErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                foreach (var fd in dd)
                {
                    FileInfo fi = new(fd);
                    string ext = fi.Extension.ToUpper();
                    switch (ext)
                    {
                        case ".BMP":
                            continue;
                        case ".DIB":
                            continue;
                        case ".EPS":
                            continue;
                        case ".WEBP":
                            continue;
                        case ".GIF":
                            continue;
                        case ".ICO":
                            continue;
                        case ".ICNS":
                            continue;
                        case ".JFIF":
                            continue;
                        case ".JPG":
                            continue;
                        case ".JPE":
                            continue;
                        case ".JPEG":
                            continue;
                        case ".PJPEG":
                            continue;
                        case ".PJP":
                            continue;
                        case ".PNG":
                            continue;
                        case ".PICT":
                            continue;
                        case ".SVG":
                            continue;
                        case ".SVGZ":
                            continue;
                        case ".TIF":
                            continue;
                        case ".TIFF":
                            continue;
                        default:
                            MessageBox.Show(Strings.ImgOnlyMultipleCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                }

                long FS = 0;
                foreach (string file in Common.ImageFile)
                {
                    FileInfo fi = new(file);
                    FS += fi.Length;
                }
                string sz = string.Format("{0} ", FS);
                toolStripStatusLabel_Status.Text = Strings.ReadedString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                ReadLabels();
                label_File.Text = Strings.IMGS;
                label_Size.Text = sz + Strings.SizeString;
                button_Image.Enabled = true;
                label1.Text = Strings.MultipleImageCaption;
                closeFileCToolStripMenuItem.Enabled = true;
                return;
            }
        }

        private void pictureBox_DD_Click(object sender, EventArgs e)
        {
            FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\tmp.png");
            formShowPicture.ShowDialog();
            formShowPicture.Dispose();
        }

        private void pictureBox_DD_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void pictureBox_DD_DragDrop(object sender, DragEventArgs e)
        {
            string[] dd = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (pictureBox_DD.Image != null)
            {
                pictureBox_DD.Image = null;
                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
            }

            List<string> lst = new();
            foreach (string files in dd)
            {
                lst.Add(files);
            }
            Common.ImageFile = lst.ToArray();

            if (Common.ImageFile.Length == 1)
            {
                FileInfo file = new(dd[0]);
                string ext = file.Extension.ToUpper();
                if (ext == ".BMP" || ext == ".DIB" || ext == ".EPS" || ext == ".WEBP" || ext == ".GIF" || ext == ".ICO" || ext == ".ICNS" || ext == ".JFIF" || ext == ".JPG" || ext == ".JPE" || ext == ".JPEG" || ext == ".PJPEG" || ext == ".PJP" || ext == ".PNG" || ext == ".PICT" || ext == ".SVG" || ext == ".SVGZ" || ext == ".TIF" || ext == ".TIFF")
                {
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = dd[0];
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;

                    ImageConvert.IMAGEtoPNG32Async(file.FullName, Directory.GetCurrentDirectory() + @"\tmp.png");

                    pictureBox_DD.ImageLocation = Directory.GetCurrentDirectory() + @"\tmp.png";
                    closeFileCToolStripMenuItem.Enabled = true;
                }
                else if (ext == ".AVI" || ext == ".MP4" || ext == ".MOV" || ext == ".WMV" || ext == ".MKV" || ext == ".WEBM")
                {
                    IniFile ini = new(@".\settings.ini");

                    Common.VideoPath = file.FullName;
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = file.FullName;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Video.Enabled = true;
                    label1.Visible = true;
                    label1.Text = Strings.VideoCaption;
                    closeFileCToolStripMenuItem.Enabled = true;

                    using var video = new VideoCapture(Common.VideoPath);
                    switch (video.Fps.ToString())
                    {
                        case "30":
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("30", "30.00"));
                            break;
                        case "60":
                            ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Replace("60", "60.00"));
                            break;
                        default:
                            if (video.Fps.ToString().Length > 5)
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString().Substring(0, 5));
                            }
                            else
                            {
                                ini.WriteString("VIDEO_SETTINGS", "FPS_INDEX", video.Fps.ToString());
                            }
                            break;
                    }

                    return;
                }
                else
                {
                    MessageBox.Show(Strings.FmtErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                foreach (var fd in dd)
                {
                    FileInfo fi = new(fd);
                    string ext = fi.Extension.ToUpper();
                    switch (ext)
                    {
                        case ".BMP":
                            continue;
                        case ".DIB":
                            continue;
                        case ".EPS":
                            continue;
                        case ".WEBP":
                            continue;
                        case ".GIF":
                            continue;
                        case ".ICO":
                            continue;
                        case ".ICNS":
                            continue;
                        case ".JFIF":
                            continue;
                        case ".JPG":
                            continue;
                        case ".JPE":
                            continue;
                        case ".JPEG":
                            continue;
                        case ".PJPEG":
                            continue;
                        case ".PJP":
                            continue;
                        case ".PNG":
                            continue;
                        case ".PICT":
                            continue;
                        case ".SVG":
                            continue;
                        case ".SVGZ":
                            continue;
                        case ".TIF":
                            continue;
                        case ".TIFF":
                            continue;
                        default:
                            MessageBox.Show(Strings.ImgOnlyMultipleCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                }

                long FS = 0;
                foreach (string file in Common.ImageFile)
                {
                    FileInfo fi = new(file);
                    FS += fi.Length;
                }
                string sz = string.Format("{0} ", FS);
                toolStripStatusLabel_Status.Text = Strings.ReadedString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                ReadLabels();
                label_File.Text = Strings.IMGS;
                label_Size.Text = sz + Strings.SizeString;
                button_Image.Enabled = true;
                label1.Visible = true;
                label1.Text = Strings.MultipleImageCaption;
                closeFileCToolStripMenuItem.Enabled = true;
                return;
            }
        }
    }
}
