using PrivateProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PrivateProfile.IniFile;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        private void BackgroundWorker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            switch (Common.ProgressFlag)
            {
                case 0:
                    foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images", "*.*"))
                    {
                        Process ps;
                        ProcessStartInfo pi = new();
                        pi.FileName = ".\\res\\waifu2x-ncnn-vulkan.exe";
                        pi.Arguments = Common.ImageParam.Replace("$InFile", file).Replace("$OutFile", "\"" + Common.SFDSavePath + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);

                        while (!ps.HasExited)
                        {
                            if (backgroundWorker_Progress.CancellationPending)
                            {
                                if (!ps.HasExited)
                                {
                                    ps.Kill();
                                }
                                ps.Close();
                                e.Cancel = true;
                                return;
                            }
                            else if (ps.HasExited == true)
                            {
                                FileInfo fi = new(file);
                                worker.ReportProgress(Directory.GetFiles(fi.DirectoryName, fi.Name).Length);
                                break;
                            }
                            else
                            {
                                FileInfo fi = new(file);
                                worker.ReportProgress(Directory.GetFiles(fi.DirectoryName, fi.Name).Length);
                                continue;
                            }
                        }
                    }
                    break;
                case 1:
                    var ini = new IniFile(@".\settings.ini");
                    int fmt = ini.GetInt("IMAGE_SETTINGS", "FORMAT_INDEX", 65535);
                    string ft;
                    switch (fmt)
                    {
                        case 0:
                            ft = ".jpg";
                            break;
                        case 1:
                            ft = ".png";
                            break;
                        case 2:
                            ft = ".webp";
                            break;
                        default:
                            ft = ".png";
                            break;
                    }

                    foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images", "*.*"))
                    {
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = ".\\res\\waifu2x-ncnn-vulkan.exe";
                        pi.Arguments = Common.ImageParam.Replace("$InFile", file).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ft + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);

                        while (!ps.HasExited)
                        {
                            if (backgroundWorker_Progress.CancellationPending)
                            {
                                if (!ps.HasExited)
                                {
                                    ps.Kill();
                                }
                                ps.Close();
                                e.Cancel = true;
                                return;
                            }
                            else if (ps.HasExited == true)
                            {
                                worker.ReportProgress(Directory.GetFiles(Common.FBDSavePath, "*.*").Length);
                                break;
                            }
                            else
                            {
                                worker.ReportProgress(Directory.GetFiles(Common.FBDSavePath, "*.*").Length);
                                continue;
                            }
                        }
                    }
                    break;
                case 2:
                    backgroundWorker_Video.RunWorkerAsync();
                    while (backgroundWorker_Video.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length);
                    }
                    break;
                case 3:
                    backgroundWorker_Video.RunWorkerAsync();
                    while (backgroundWorker_Video.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames2x, "*.*").Length);
                    }
                    break;
                case 4:
                    backgroundWorker_Delete.RunWorkerAsync();
                    while (backgroundWorker_Delete.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        worker.ReportProgress(Directory.GetFiles(Common.DeletePath, "*.*").Length);
                    }
                    break;
                case 5:
                    backgroundWorker_Split.RunWorkerAsync();
                    while (backgroundWorker_Split.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames, "*.*").Length);
                    }
                    break;
                case 6:
                    Uri uri = new("https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip");

                    if (Common.downloadClient == null)
                    {
                        Common.downloadClient = new System.Net.WebClient();
                        Common.downloadClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DownloadClient_DownloadProgressChanged);
                        Common.downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadClient_DownloadFileCompleted);
                    }

                    Common.DlsFlag = 1;
                    Common.downloadClient.DownloadFileAsync(uri, Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
                    while (Common.downloadClient.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (Common.DlFlag == 1) // OK
                        {
                            break;
                        }
                        else if (Common.DlFlag == 2) // Cancelled
                        {
                            break;
                        }
                        else if (Common.DlFlag == 3) // Error
                        {
                            break;
                        }
                        else
                        {
                            worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames, "*.*").Length); // Dummy RP
                            continue;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void BackgroundWorker_Progress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (Common.ProgressFlag)
            {
                case 0:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.IMGScalled;
                    break;
                case 1:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.IMGSScalled;
                    break;
                case 2:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.VScalled;
                    break;
                case 3:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.VRScalled;
                    break;
                case 4:
                    progressBar1.Value = Common.ProgMax - e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.DELScalled;
                    break;
                case 5:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.MERGEScalled;
                    break;
                case 6:
                    progressBar1.Value = Common.DLProgchanged;
                    label_Pos.Text = Common.DLInfo;
                    break;
                default:
                    break;
            }
        }

        private void BackgroundWorker_Progress_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show(Strings.ProgressAborted, Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Common.AbortFlag = 1;
                Close();
            }
            else
            {
                Common.AbortFlag = 0;
                Close();
            }
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            button_Abort.Enabled = true;
            button_Abort.Visible = true;
            progressBar1.Minimum = Common.ProgMin;
            progressBar1.Maximum = Common.ProgMax;
            progressBar1.Value = 0;
            switch (Common.ProgressFlag)
            {
                case 0: // Image
                    label_ProgressText.Text = Strings.UPIMGProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 1: // Multiple Images
                    label_ProgressText.Text = Strings.UPIMGSProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 2: // Video First Upscaling
                    label_ProgressText.Text = Strings.UPVProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 3: // Video Second Upscaling
                    label_ProgressText.Text = Strings.UPVRProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 4: // File Delete
                    label_ProgressText.Text = Strings.DELProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 5: // frame
                    button_Abort.Enabled = false;
                    button_Abort.Visible = false;
                    label_ProgressText.Text = Strings.MERGEProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 6: // download
                    progressBar1.Maximum = 100;
                    label_ProgressText.Text = Strings.DLProgress;
                    label_Pos.Text = Strings.Initalize;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                default:
                    Close();
                    break;
            }
        }

        private void BackgroundWorker_Video_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            ProcessStartInfo pi = new();
            Process ps;
            pi.FileName = ".\\res\\waifu2x-ncnn-vulkan.exe";
            pi.Arguments = Common.ImageParam.Replace("$InFile", Common.DeletePathFrames).Replace("$OutFile", Common.DeletePathFrames2x).Replace("waifu2x-ncnn-vulkan ", "");
            pi.WindowStyle = ProcessWindowStyle.Hidden;
            pi.UseShellExecute = true;
            ps = Process.Start(pi);

            while (!ps.HasExited)
            {
                if (backgroundWorker_Video.CancellationPending)
                {
                    if (!ps.HasExited)
                    {
                        ps.Kill();
                    }
                    ps.Close();
                    e.Cancel = true;
                    return;
                }
                else if (ps.HasExited == true)
                {
                    return;
                }
                else
                {
                    continue;
                }
            }
        }

        private void Button_Abort_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            if (Common.downloadClient != null)
            {
                dr = MessageBox.Show(Strings.DLAbortConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    if (Common.downloadClient != null)
                    {
                        Common.downloadClient.CancelAsync();
                    }

                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                dr = MessageBox.Show(Strings.ProgressAbortConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    backgroundWorker_Progress.CancelAsync();
                    if (backgroundWorker_Video.IsBusy)
                    {
                        backgroundWorker_Video.CancelAsync();
                    }
                    if (backgroundWorker_Delete.IsBusy)
                    {
                        backgroundWorker_Delete.CancelAsync();
                    }

                    return;
                }
                else
                {
                    return;
                }
            }
        }

        private void BackgroundWorker_Delete_DoWork(object sender, DoWorkEventArgs e)
        {
            var ini = new IniFile(@".\settings.ini");
            string vl = ini.GetString("VIDEO_SETTINGS", "VL_INDEX");
            string al = ini.GetString("VIDEO_SETTINGS", "AL_INDEX");
            string delvlpath, delvlpath2x, delalpath;

            if(vl != "") {
                delvlpath = vl + @"\image-frames\";
                delvlpath2x = vl + @"\image-frames2x\";
            }
            else
            {
                delvlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                delvlpath2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
            }
            if (al != "")
            {
                delalpath = al + @"\audio\";
            }
            else
            {
                delalpath = Directory.GetCurrentDirectory() + @"\_temp-project\audio\";
            }

            BackgroundWorker worker = (BackgroundWorker)sender;
            switch (Common.DeleteFlag)
            {
                case 0:
                    Common.DeleteDirectoryFiles(Common.DeletePath);
                    break;
                case 1:
                    Common.DeleteDirectoryFiles(delvlpath);
                    Common.DeleteDirectoryFiles(delvlpath2x);
                    Common.DeleteDirectoryFiles(delalpath);
                    break;
                default:
                    Common.DeleteDirectoryFiles(Common.DeletePath);
                    break;
            }
        }

        private void BackgroundWorker_Split_DoWork(object sender, DoWorkEventArgs e)
        {
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
                vlpath = vl + @"\image-frames\image-%09d.png";
            }
            else
            {
                vlpath = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames\image-%09d.png";
            }
            if (al != "")
            {
                alpath = al + @"\audio\" + acodec;
            }
            else
            {
                alpath = Directory.GetCurrentDirectory() + @"\_temp-project\audio\" + acodec;
            }

            ProcessStartInfo pi = new();
            Process ps;
            pi.FileName = ffp;
            pi.Arguments = Common.VideoParam.Replace("$InFile", "\"" + Common.VideoPath + "\"").Replace("$OutFile", vlpath).Replace(ffp + " ", "");
            pi.WindowStyle = ProcessWindowStyle.Hidden;
            pi.UseShellExecute = true;
            ps = Process.Start(pi);
            ps.WaitForExit();

            pi.Arguments = Common.AudioParam.Replace("$InFile", "\"" + Common.VideoPath + "\"").Replace("$OutFile", alpath).Replace(ffp + " ", "");
            ps = Process.Start(pi);
            ps.WaitForExit();
        }

        private void DownloadClient_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (Common.DlsFlag == 1)
            {
                Common.DLProgMax = 100;
                Common.DlsFlag = 0;
            }
            Common.DLProgchanged = e.ProgressPercentage;
            Common.DLInfo = string.Format(Strings.DLInfo, e.ProgressPercentage, e.TotalBytesToReceive, e.BytesReceived);
        }

        private void DownloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Common.DlFlag = 2;
                Common.DLlog = Common.SFDRandomNumber() + ": Download cancelled.";
            }   
            else if (e.Error != null)
            {
                Common.DlFlag = 3;
                Common.DLlog = Common.SFDRandomNumber() + ": " + e.Error.Message;
            }
            else
            {
                Common.DlFlag = 1;
                Common.DLlog = Common.SFDRandomNumber() + ": Download completed.";
            }
        }
    }
}
