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
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        FileInfo fi = new(file);
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = ".\\res\\waifu2x-ncnn-vulkan.exe";
                        pi.Arguments = Common.ImageParam.Replace("$InFile", file).Replace("$OutFile", Common.SFDSavePath).Replace("waifu2x-ncnn-vulkan ", "");
                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);
                        ps.WaitForExit();
                        worker.ReportProgress(Directory.GetFiles(fi.DirectoryName, fi.Name).Length);
                    }
                    break;
                case 1:
                    foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images", "*.*"))
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        FileInfo fi = new(file);
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = ".\\res\\waifu2x-ncnn-vulkan.exe";
                        pi.Arguments = Common.ImageParam.Replace("$InFile", file).Replace("$OutFile", Common.FBDSavePath + @"\" + fi.Name).Replace("waifu2x-ncnn-vulkan ", "");
                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                        pi.UseShellExecute = true;
                        ps = Process.Start(pi);
                        ps.WaitForExit();
                        worker.ReportProgress(Directory.GetFiles(Common.FBDSavePath, "*.*").Length);
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
                    label_ProgressText.Text = Strings.MERGEProgress;
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
            ps.WaitForExit();
        }

        private void Button_Abort_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(Strings.ProgressAbortConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
            pi.Arguments = Common.VideoParam.Replace("$InFile", Common.VideoPath).Replace("$OutFile", vlpath).Replace(ffp + " ", "");
            pi.WindowStyle = ProcessWindowStyle.Hidden;
            pi.UseShellExecute = true;
            ps = Process.Start(pi);
            ps.WaitForExit();

            pi.Arguments = Common.AudioParam.Replace("$InFile", Common.VideoPath).Replace("$OutFile", alpath).Replace(ffp + " ", "");
            ps = Process.Start(pi);
            ps.WaitForExit();
        }
    }
}
