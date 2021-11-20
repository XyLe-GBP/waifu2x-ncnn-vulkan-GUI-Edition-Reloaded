using OpenCvSharp;
using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh");
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Text = "waifu2x-nvge ( build: 1.23.2120.1120-Beta )";
            var ini = new IniFile(@".\settings.ini");
            string[] OSInfo = new string[17];
            string[] CPUInfo = new string[3];
            string[] GPUInfo = new string[3];
            SystemInfo.GetSystemInformation(OSInfo);
            SystemInfo.GetProcessorsInformation(CPUInfo);
            SystemInfo.GetVideoControllerInformation(GPUInfo);
            label_File.Text = Strings.NotReadedString;
            label_Size.Text = Strings.NotReadedString;
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

            /*Uri ffsources = new("https://www.gyan.dev/ffmpeg/builds/release-version");
            Task<string> webTask = Common.GetWebPageAsync(ffsources);
            webTask.Wait();*/
            string netversion;// = webTask.Result;
            WebClient wc = new();

            Stream st = wc.OpenRead("https://www.gyan.dev/ffmpeg/builds/release-version");
            StreamReader sr = new(st);
            netversion = sr.ReadToEnd();

            sr.Close();
            st.Close();

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
            {
                DialogResult dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    Common.ProgressFlag = 6;
                    Common.ProgMin = 0;
                    FormProgress Form = new();
                    Form.ShowDialog();
                    Form.Dispose();

                    if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                    {
                        using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName == "ffmpeg-" + netversion + "-essentials_build/bin/ffmpeg.exe")
                            {
                                entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                            }
                        }

                        if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                            ini.WriteString("FFMPEG", "LATEST_VERSION", netversion);
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

            if (ini.GetString("FFMPEG", "LATEST_VERSION") == "")
            {
                DialogResult dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                    {
                        File.Delete(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                    }
                    Common.ProgressFlag = 6;
                    Common.ProgMin = 0;
                    FormProgress Form = new();
                    Form.ShowDialog();
                    Form.Dispose();

                    if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                    {
                        using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName == "ffmpeg-" + netversion + "-essentials_build/bin/ffmpeg.exe")
                            {
                                entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name, true);
                            }
                        }

                        if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                        {
                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                            ini.WriteString("FFMPEG", "LATEST_VERSION", netversion);
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
                switch (ini.GetString("FFMPEG", "LATEST_VERSION").CompareTo(netversion))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, Strings.LatestString + netversion + "\n" + Strings.CurrentString + ini.GetString("FFMPEG", "LATEST_VERSION") + "\n" + Strings.FFUpdate, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                            {
                                File.Delete(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                Common.ProgressFlag = 6;
                                Common.ProgMin = 0;
                                FormProgress Form = new();
                                Form.ShowDialog();
                                Form.Dispose();

                                if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                                {
                                    using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                                    foreach (ZipArchiveEntry entry in archive.Entries)
                                    {
                                        if (entry.FullName == "ffmpeg-" + netversion + @"-essentials_build/bin/ffmpeg.exe")
                                        {
                                            entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name);
                                        }
                                    }

                                    if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                                    {
                                        ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                        ini.WriteString("FFMPEG", "LATEST_VERSION", netversion);
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
                            else
                            {
                                dr = MessageBox.Show(Strings.DLConfirm, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (dr == DialogResult.Yes)
                                {
                                    Common.ProgressFlag = 6;
                                    Common.ProgMin = 0;
                                    FormProgress Form = new();
                                    Form.ShowDialog();
                                    Form.Dispose();

                                    if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
                                    {
                                        using ZipArchive archive = ZipFile.OpenRead(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                                        foreach (ZipArchiveEntry entry in archive.Entries)
                                        {
                                            if (entry.FullName == "ffmpeg-" + netversion + @"-essentials_build/bin/ffmpeg.exe")
                                            {
                                                entry.ExtractToFile(Directory.GetCurrentDirectory() + @"\res\" + entry.Name);
                                            }
                                        }

                                        if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                                        {
                                            ini.WriteString("VIDEO_SETTINGS", "FFMPEG_INDEX", Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                            ini.WriteString("FFMPEG", "LATEST_VERSION", netversion);
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
                    label_File.Text = ofd.FileName;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    closeFileCToolStripMenuItem.Enabled = true;
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
                    label_File.Text = Strings.IMGS;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    closeFileCToolStripMenuItem.Enabled = true;
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
                label_File.Text = ofd.FileName;
                label_Size.Text = sz + Strings.SizeString;
                button_Video.Enabled = true;
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
                label_File.Text = Strings.NotReadedString;
                label_Size.Text = Strings.NotReadedString;
                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                closeFileCToolStripMenuItem.Enabled = false;
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

        private void CheckForUpdatesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string netversion;
                WebClient wc = new();

                Stream st = wc.OpenRead("https://raw.githubusercontent.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/master/VERSIONINFO");
                StreamReader sr = new(st);
                netversion = sr.ReadToEnd();

                sr.Close();
                st.Close();

                //FileVersionInfo ver = FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

                switch (ver.FileVersion.ToString().CompareTo(netversion[8..].Replace("\n", "")))
                {
                    case -1:
                        DialogResult dr = MessageBox.Show(this, Strings.LatestString + netversion[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.UpdateConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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
                        MessageBox.Show(this, Strings.LatestString + netversion[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.Uptodate, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case 1:
                        throw new Exception(netversion[8..].Replace("\n", "").ToString() + " < " + ver.FileVersion.ToString());
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
                    FileInfo file = new(Common.ImageFile[0]);

                    switch (file.Extension.ToUpper())
                    {
                        case ".GIF":
                            if (ImageConvert.IMAGEtoPNG(file.Directory + "\\" + file.Name, file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg")) != false)
                            {
                                File.Move(file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg"), Directory.GetCurrentDirectory() + @"\_temp-project\images\" + file.Name.Replace(file.Extension, ".png"));
                                break;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                return;
                            }
                        default:
                            if (ImageConvert.IMAGEtoPNG(file.Directory + "\\" + file.Name, file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg")) != false)
                            {
                                File.Move(file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg"), Directory.GetCurrentDirectory() + @"\_temp-project\images\" + file.Name.Replace(file.Extension, ".png"));
                                break;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                return;
                            }
                    }

                    switch (fmt)
                    {
                        case 0:
                            ft = "Joint Photographic Experts Group (*.jpg)|*.jpg";
                            break;
                        case 1:
                            ft = "Portable Network Graphics (*.png)|*.png";
                            break;
                        case 2:
                            ft = "Google webp (*.webp)|*.webp";
                            break;
                        case 3:
                            ft = "Icon (*.ico)|*.ico";
                            break;
                        default:
                            ft = "Portable Network Graphics (*.png)|*.png";
                            break;
                    }
                    
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
                        Common.ProgressFlag = 0;
                        Common.SFDSavePath = sfd.FileName;
                        Common.ProgMin = 0;
                        Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images", "*.*").Length;
                        FormProgress Form = new();
                        Form.ShowDialog();
                        Form.Dispose();

                        if (fmt == 3)
                        {
                            ImageConvert.IMAGEtoICON(sfd.FileName.Replace(".ico", ".png"), sfd.FileName);
                            File.Delete(sfd.FileName.Replace(".ico", ".png"));
                        }

                        if (Common.AbortFlag != 0)
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                            label_File.Text = Strings.NotReadedString;
                            label_Size.Text = Strings.NotReadedString;
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            return;
                        }

                        if (File.Exists(sfd.FileName))
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                            label_File.Text = Strings.NotReadedString;
                            label_Size.Text = Strings.NotReadedString;
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            MessageBox.Show(Strings.IMGUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("EXPLORER.EXE", @"/select,""" + sfd.FileName + @"""");
                        }
                        else
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                            label_File.Text = Strings.NotReadedString;
                            label_Size.Text = Strings.NotReadedString;
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            MessageBox.Show(Strings.IMGUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                else
                {
                    foreach (var sources in Common.ImageFile)
                    {
                        FileInfo file = new(sources);
                        switch (file.Extension.ToUpper())
                        {
                            case ".GIF":
                                if (ImageConvert.IMAGEtoPNG(file.Directory + "\\" + file.Name, file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg")) != false)
                                {
                                    File.Move(file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg"), Directory.GetCurrentDirectory() + @"\_temp-project\images\" + file.Name.Replace(file.Extension, ".png"));
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                    return;
                                }
                            default:
                                if (ImageConvert.IMAGEtoPNG(file.Directory + "\\" + file.Name, file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg")) != false)
                                {
                                    File.Move(file.Directory + "\\" + file.Name.Replace(file.Extension, ".w2xnvg"), Directory.GetCurrentDirectory() + @"\_temp-project\images\" + file.Name.Replace(file.Extension, ".png"));
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                    return;
                                }
                        }
                    }

                    FolderBrowserDialog fbd = new();
                    fbd.Description = Strings.FBDImageTitle;
                    fbd.RootFolder = Environment.SpecialFolder.Desktop;
                    fbd.SelectedPath = @"C:\";
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
                                FormProgress Form = new();
                                Form.ShowDialog();
                                Form.Dispose();
                            }
                            else
                            {
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                label_File.Text = Strings.NotReadedString;
                                label_Size.Text = Strings.NotReadedString;
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
                                return;
                            }
                        }
                        else
                        {
                            FormProgress Form = new();
                            Form.ShowDialog();
                            Form.Dispose();
                        }

                        if (Common.AbortFlag != 0)
                        {
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                            label_File.Text = Strings.NotReadedString;
                            label_Size.Text = Strings.NotReadedString;
                            toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                            toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                            button_Image.Enabled = false;
                            closeFileCToolStripMenuItem.Enabled = false;
                            return;
                        }

                        foreach (var file in Directory.GetFiles(Common.FBDSavePath, "*.*"))
                        {
                            if (!File.Exists(file))
                            {
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                                label_File.Text = Strings.NotReadedString;
                                label_Size.Text = Strings.NotReadedString;
                                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                                button_Image.Enabled = false;
                                closeFileCToolStripMenuItem.Enabled = false;
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
                        label_File.Text = Strings.NotReadedString;
                        label_Size.Text = Strings.NotReadedString;
                        toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                        toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                        button_Image.Enabled = false;
                        closeFileCToolStripMenuItem.Enabled = false;
                        MessageBox.Show(Strings.IMGUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("EXPLORER.EXE", Common.FBDSavePath);
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
            
            FormProgress Form = new();
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

                var video = new VideoCapture(Common.VideoPath);

                Common.DeletePath = delvlpath;
                Common.ProgressFlag = 5;
                Common.ProgMin = 0;
                Common.ProgMax = video.FrameCount;

                video.Dispose();

                Form.ShowDialog();

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

                Form.ShowDialog();
                Form.Dispose();

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

                Common.ProgressFlag = 4;
                Common.DeleteFlag = 0;
                Common.ProgMin = 0;
                Common.ProgMax = Directory.GetFiles(Common.DeletePath, "*.*").Length;

                Form.ShowDialog();

                foreach (var file in Directory.GetFiles(delvlpath2x, "*.*"))
                {
                    FileInfo fi = new(file);
                    File.Move(file, delvlpath + "\\" + fi.Name);
                }

                Common.ProgressFlag = 3;
                Common.ProgMin = 0;
                Common.ProgMax = Directory.GetFiles(delvlpath, "*.*").Length;

                Form.ShowDialog();
                Form.Dispose();

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
            if (Common.MergeParam.Length <= 70)
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var ini = new IniFile(@".\settings.ini");
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

                if (File.Exists(Common.SFDSavePath))
                {
                    Common.ProgressFlag = 4;
                    Common.DeleteFlag = 1;
                    Common.ProgMin = 0;
                    Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                    FormProgress Form = new();
                    Form.ShowDialog();
                    Form.Dispose();

                    Common.UpscaleFlag = 0;
                    label_File.Text = Strings.NotReadedString;
                    label_Size.Text = Strings.NotReadedString;
                    toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                    button_Video.Text = Strings.ButtonUpscaleVideo;
                    button_Video.Enabled = false;
                    button_Merge.Enabled = false;
                    closeFileCToolStripMenuItem.Enabled = false;
                    MessageBox.Show(Strings.VRUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("EXPLORER.EXE", @"/select,""" + Common.SFDSavePath + @"""");
                }
                else
                {
                    Common.ProgressFlag = 4;
                    Common.DeleteFlag = 1;
                    Common.ProgMin = 0;
                    Common.ProgMax = Directory.GetFiles(Common.DeletePathFrames, "*.*").Length + Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length + 1;

                    FormProgress Form = new();
                    Form.ShowDialog();
                    Form.Dispose();

                    Common.UpscaleFlag = 0;
                    button_Video.Text = Strings.ButtonUpscaleVideo;
                    label_File.Text = Strings.NotReadedString;
                    label_Size.Text = Strings.NotReadedString;
                    toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                    button_Video.Enabled = false;
                    button_Merge.Enabled = false;
                    closeFileCToolStripMenuItem.Enabled = false;
                    MessageBox.Show(Strings.VRUPError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return;
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
            Common.DeleteDirectory(Directory.GetCurrentDirectory() + @"\_temp-project");
        }


    }
}
