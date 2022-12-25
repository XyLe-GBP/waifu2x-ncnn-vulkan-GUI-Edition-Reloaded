using NVGE.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormProgress : Form
    {
        private readonly int Flag, UseEngine;
        private readonly string waifu2xPath = Directory.GetCurrentDirectory() + @"\res\engines\waifu2x\waifu2x-ncnn-vulkan.exe";
        private readonly string realcuganPath = Directory.GetCurrentDirectory() + @"\res\engines\realcugan\realcugan-ncnn-vulkan.exe";
        private readonly string realesrganPath = Directory.GetCurrentDirectory() + @"\res\engines\realesrgan\realesrgan-ncnn-vulkan.exe";
        private readonly string upscaledPath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";

        #region NetworkCommon
        private static readonly HttpClientHandler handler = new()
        {
            UseProxy = false,
            UseCookies = false
        };
        private static readonly HttpClient appUpdatechecker = new(handler);
        #endregion

        /// <summary>
        /// Forms for various processes such as downloading, file processing, etc.
        /// </summary>
        /// <param name="flag">
        /// <para>Value of which process to perform.</para>
        /// <para>0 or 1: Image upscaling / multi</para>
        /// <para>2 or 3: Video upscaling / second</para>
        /// <para>4: File delete</para>
        /// <para>5: Split images from video</para>
        /// <para>6: Download or update FFmpeg</para>
        /// <para>7: Convert image(s)</para>
        /// <para>8: Update Application</para>
        /// </param>
        /// <param name= "useengine">
        /// <para>Specifies the engine used for conversion. By default, '0' (waifu2x-ncnn-vulkan) is used.</para>
        /// <para>0: waifu2x-ncnn-vulkan</para>
        /// <para>1: realcugan-ncnn-vulkan</para>
        /// <para>2: realesrgan-ncnn-vulkan</para>
        /// </param>
        public FormProgress(int flag, int useengine = 0)
        {
            InitializeComponent();
            Flag = flag;
            UseEngine = useengine;
        }

        private void BackgroundWorker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            switch (Flag)
            {
                case 0: // single image
                    switch (UseEngine)
                    {
                        case 0: // waifu2x
                            {
                                foreach (var filename in Common.ImageFileName)
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value);
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[0], ".png") + "\"";//FileInfo fi = new(file);

                                    Process ps = new();
                                    ProcessStartInfo pi = new();

                                    if (ft == ".ico")
                                    {
                                        pi.FileName = waifu2xPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath.Replace(".ico", ".png") + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                                        pi.UseShellExecute = true;
                                        ps = Process.Start(pi);
                                    }
                                    else
                                    {
                                        pi.FileName = waifu2xPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                                        pi.UseShellExecute = true;
                                        ps = Process.Start(pi);
                                    }

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
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            break;
                                        }
                                        else
                                        {
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            continue;
                                        }
                                    }
                                }
                            }
                            break;
                        case 1: // realcugan
                            {
                                foreach (var filename in Common.ImageFileName)
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value);
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[0], ".png") + "\"";//FileInfo fi = new(file);

                                    Process ps = new();
                                    ProcessStartInfo pi = new();

                                    if (ft == ".ico")
                                    {
                                        pi.FileName = realcuganPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath.Replace(".ico", ".png") + "\"").Replace("realcugan-ncnn-vulkan ", "");
                                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                                        pi.UseShellExecute = true;
                                        ps = Process.Start(pi);
                                    }
                                    else
                                    {
                                        pi.FileName = realcuganPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath + "\"").Replace("realcugan-ncnn-vulkan ", "");
                                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                                        pi.UseShellExecute = true;
                                        ps = Process.Start(pi);
                                    }

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
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            break;
                                        }
                                        else
                                        {
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            continue;
                                        }
                                    }
                                }
                            }
                            break;
                        case 2: // realesrgan
                            {
                                foreach (var filename in Common.ImageFileName)
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value);
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[0], ".png") + "\"";//FileInfo fi = new(file);

                                    Process ps = new();
                                    ProcessStartInfo pi = new();

                                    if (ft == ".ico")
                                    {
                                        pi.FileName = realesrganPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath.Replace(".ico", ".png") + "\"").Replace("realesrgan-ncnn-vulkan ", "");
                                        pi.WindowStyle = ProcessWindowStyle.Hidden;
                                        pi.UseShellExecute = true;
                                        ps = Process.Start(pi);
                                    }
                                    else
                                    {
                                        pi.FileName = realesrganPath;
                                        pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.SFDSavePath + "\"").Replace("realesrgan-ncnn-vulkan ", "");
                                        pi.CreateNoWindow = true;
                                        pi.UseShellExecute = false;
                                        pi.RedirectStandardOutput = true;
                                        ps = Process.Start(pi);

                                        Common.Log = ps.StandardOutput;
                                    }

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
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            break;
                                        }
                                        else
                                        {
                                            worker.ReportProgress(Directory.GetFiles(upscaledPath, "*").Length);
                                            continue;
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case 1: // multi image
                    {
                        switch (UseEngine)
                        {
                            case 0: // waifu2x
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value), i = 0;
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    foreach (var filename in Common.ImageFileName)
                                    {

                                        string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[i], ".png") + "\"";

                                        ProcessStartInfo pi = new();
                                        Process ps = new();

                                        if (ft == ".ico")
                                        {
                                            pi.FileName = waifu2xPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(".ico", ".png") + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }
                                        else
                                        {
                                            pi.FileName = waifu2xPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(Common.ImageFileExt[i], ft) + "\"").Replace("waifu2x-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }

                                        i++;

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
                                }
                                break;
                            case 1: // realcugan
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value), i = 0;
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    foreach (var filename in Common.ImageFileName)
                                    {

                                        string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[i], ".png") + "\"";

                                        ProcessStartInfo pi = new();
                                        Process ps = new();

                                        if (ft == ".ico")
                                        {
                                            pi.FileName = realcuganPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(".ico", ".png") + "\"").Replace("realcugan-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }
                                        else
                                        {
                                            pi.FileName = realcuganPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(Common.ImageFileExt[i], ft) + "\"").Replace("realcugan-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }

                                        i++;

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
                                }
                                break;
                            case 2: // realesrgan
                                {
                                    Config.Load(Common.xmlpath);
                                    int fmt = int.Parse(Config.Entry["Format"].Value), i = 0;
                                    string ft = fmt switch
                                    {
                                        0 => ".jpg",
                                        1 => ".png",
                                        2 => ".webp",
                                        3 => ".ico",
                                        _ => ".png",
                                    };
                                    foreach (var filename in Common.ImageFileName)
                                    {

                                        string fnf = "\"" + Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + filename.Replace(Common.ImageFileExt[i], ".png") + "\"";

                                        ProcessStartInfo pi = new();
                                        Process ps = new();

                                        if (ft == ".ico")
                                        {
                                            pi.FileName = realesrganPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(".ico", ".png") + "\"").Replace("realesrgan-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }
                                        else
                                        {
                                            pi.FileName = realesrganPath;
                                            pi.Arguments = Common.ImageParam.Replace("$InFile", fnf).Replace("$OutFile", "\"" + Common.FBDSavePath + @"\" + filename.Replace(Common.ImageFileExt[i], ft) + "\"").Replace("realesrgan-ncnn-vulkan ", "");
                                            pi.WindowStyle = ProcessWindowStyle.Hidden;
                                            pi.UseShellExecute = true;
                                            ps = Process.Start(pi);
                                        }

                                        i++;

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
                                }
                                break;
                            default:
                                break;
                        }
                        
                    }
                    break;
                case 2: // single video
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
                case 3: // multi video
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
                case 4: // delete
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
                case 5: // Split images from video
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
                case 6: // Download or update FFmpeg
                    {
                        Uri uri = new("https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip");

                        if (Common.downloadClient == null)
                        {
#pragma warning disable SYSLIB0014 // 型またはメンバーが旧型式です
                            Common.downloadClient = new System.Net.WebClient();
#pragma warning restore SYSLIB0014 // 型またはメンバーが旧型式です
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
                                Common.DlcancelFlag = 1;
                                return;
                            }
                            else
                            {
                                worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames, "*.*").Length); // Dummy RP
                            }
                        }
                        Common.downloadClient.Dispose();
                        break;
                    }
                case 7: // Convert Image(s)
                    backgroundWorker_Convert.RunWorkerAsync();
                    while (backgroundWorker_Convert.IsBusy)
                    {
                        if (backgroundWorker_Progress.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        worker.ReportProgress(Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*").Length);
                    }
                    break;
                case 8: // Update Application
                    {

                        // Debug URI:https://dl.cdn.xyle-official.com/content/app/utils/waifu2x/debug/debug.zip
                        // Debug Portable URI:https://dl.cdn.xyle-official.com/content/app/utils/waifu2x/debug/debug-p.zip
                        // Release URI:https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases/download/v" + Common.GitHubLatestVersion + "/waifu2x-nvger-release.zip"
                        // Release Portable URI:https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases/download/v" + Common.GitHubLatestVersion + "/waifu2x-nvger-portable.zip

                        Uri uri;

                        switch (Common.ApplicationPortable)
                        {
                            case false:
                                {
                                    uri = new("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases/download/v" + Common.GitHubLatestVersion + "/waifu2x-nvger-release.zip");
                                }
                                break;
                            case true:
                                {
                                    uri = new("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases/download/v" + Common.GitHubLatestVersion + "/waifu2x-nvger-portable.zip");
                                }
                                break;
                        }

                        if (Common.downloadClient == null)
                        {
#pragma warning disable SYSLIB0014 // 型またはメンバーが旧型式です
                            Common.downloadClient = new System.Net.WebClient();
#pragma warning restore SYSLIB0014 // 型またはメンバーが旧型式です
                            Common.downloadClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(DownloadClient_DownloadProgressChanged);
                            Common.downloadClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadClient_DownloadFileCompleted);
                        }

                        Common.DlsFlag = 1;

                        switch (Common.ApplicationPortable)
                        {
                            case false: // release
                                {
                                    Common.downloadClient.DownloadFileAsync(uri, Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip");
                                }
                                break;
                            case true: // portable
                                {
                                    Common.downloadClient.DownloadFileAsync(uri, Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip");
                                }
                                break;
                        }

                        while (Common.downloadClient.IsBusy)
                        {
                            if (backgroundWorker_Progress.CancellationPending)
                            {
                                e.Cancel = true;
                                Common.DlcancelFlag = 1;
                                return;
                            }
                            else
                            {
                                worker.ReportProgress(Directory.GetFiles(Common.DeletePathFrames, "*.*").Length); // Dummy RP
                            }
                        }
                        Common.downloadClient.Dispose();
                        break;
                    }
                default:
                    break;
            }
        }

        private void BackgroundWorker_Progress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (Flag)
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
                case 7:
                    progressBar1.Value = e.ProgressPercentage;
                    label_Pos.Text = e.ProgressPercentage.ToString() + " / " + Common.ProgMax + " " + Strings.ConvertScalled;
                    break;
                case 8:
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
                if (Common.DlcancelFlag != 0)
                {
                    Common.AbortFlag = 1;
                    Common.downloadClient.Dispose();
                    Close();
                }
                else
                {
                    MessageBox.Show(Strings.ProgressAborted, Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Common.AbortFlag = 1;
                    Close();
                }
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
            switch (Flag)
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
                case 7:
                    progressBar1.Maximum = Common.ImageFile.Length;
                    label_ProgressText.Text = Strings.ConvertProgress;
                    label_Pos.Text = Strings.Initalize;
                    button_Abort.Enabled = false;
                    button_Abort.Visible = false;
                    backgroundWorker_Progress.RunWorkerAsync();
                    break;
                case 8: // update
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
            switch (UseEngine)
            {
                case 0: // waifu2x
                    {
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = waifu2xPath;
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
                    break;
                case 1: // realcugan
                    {
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = realcuganPath;
                        pi.Arguments = Common.ImageParam.Replace("$InFile", Common.DeletePathFrames).Replace("$OutFile", Common.DeletePathFrames2x).Replace("realcugan-ncnn-vulkan ", "");
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
                    break;
                case 2: // realesrgan
                    {
                        ProcessStartInfo pi = new();
                        Process ps;
                        pi.FileName = realesrganPath;
                        pi.Arguments = Common.ImageParam.Replace("$InFile", Common.DeletePathFrames).Replace("$OutFile", Common.DeletePathFrames2x).Replace("realesrgan-ncnn-vulkan ", "");
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
                    break;
                default:
                    break;
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
                        backgroundWorker_Progress.CancelAsync();
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
            Config.Load(Common.xmlpath);
            string vl = Config.Entry["VideoLocation"].Value;
            string al = Config.Entry["AudioLocation"].Value;
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
            Config.Load(Common.xmlpath);
            int enc = int.Parse(Config.Entry["OutputCodecIndex"].Value);
            string ffp = Config.Entry["FFmpegLocation"].Value;
            string vl = Config.Entry["VideoLocation"].Value;
            string al = Config.Entry["AudioLocation"].Value;
            string vlpath, alpath;
            string acodec = enc switch
            {
                3 => "audio.m4a",
                4 => "audio.mp3",
                _ => "audio.wav",
            };
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
            Common.DLInfo = string.Format(Strings.DLInfo, e.ProgressPercentage, e.TotalBytesToReceive / 1024, e.BytesReceived / 1024);
        }

        private void DownloadClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Common.DlFlag = 2;
                Common.DLlog = Common.SFDRandomNumber() + ": Download cancelled.";
                Common.downloadClient.Dispose();
            }   
            else if (e.Error != null)
            {
                Common.DlFlag = 3;
                Common.DLlog = Common.SFDRandomNumber() + ": " + e.Error.Message;
                Common.downloadClient.Dispose();
            }
            else
            {
                Common.DlFlag = 1;
                Common.DLlog = Common.SFDRandomNumber() + ": Download completed.";
                Common.downloadClient.Dispose();
            }
        }

        private void BackgroundWorker_Convert_DoWork(object sender, DoWorkEventArgs e)
        {
            Config.Load(Common.xmlpath);
            int fmt = int.Parse(Config.Entry["Format"].Value);
            List<string> lst = new();
            List<string> lst2 = new();
            string ext = fmt switch
            {
                0 => ".jpg",
                1 => ".png",
                2 => ".webp",
                3 => ".ico",
                _ => ".png",
            };
            if (Common.ConvMultiFlag == 0)
            {
                FileInfo file = new(Common.ImageFile[0]);

                lst.Add(file.Name);
                lst2.Add(file.Extension);
                Common.ImageFileName = lst.ToArray();
                Common.ImageFileExt = lst2.ToArray();

                string dest1 = Regex.Replace(file.Name, file.Extension, ".w2xnvg", RegexOptions.IgnoreCase);
                string dest2 = Regex.Replace(file.Name, file.Extension, ".png", RegexOptions.IgnoreCase);

                switch (file.Extension.ToUpper())
                {
                    case ".GIF":
                        if (ImageConvert.IMAGEtoPNG(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                        {
                            File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                            break;
                        }
                        else
                        {
                            MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                            Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                            return;
                        }
                    default:
                        if (fmt == 1 || fmt == 3 || ext == ".png")
                        {
                            if (ImageConvert.IMAGEtoPNG32(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                            {
                                File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                                break;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                return;
                            }
                        }
                        else
                        {
                            if (ImageConvert.IMAGEtoPNG(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                            {
                                File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                                break;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                return;
                            }
                        }
                }
            }
            else
            {
                foreach (var sources in Common.ImageFile)
                {
                    FileInfo file = new(sources);

                    lst.Add(file.Name);
                    lst2.Add(file.Extension);

                    string dest1 = Regex.Replace(file.Name, file.Extension, ".w2xnvg", RegexOptions.IgnoreCase);
                    string dest2 = Regex.Replace(file.Name, file.Extension, ".png", RegexOptions.IgnoreCase);

                    switch (file.Extension.ToUpper())
                    {
                        case ".GIF":
                            if (ImageConvert.IMAGEtoPNG(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                            {
                                File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                                break;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                return;
                            }
                        default:
                            if (fmt == 1 || fmt == 3 || ext == ".png")
                            {
                                if (ImageConvert.IMAGEtoPNG32(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                                {
                                    File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                    return;
                                }
                            }
                            else
                            {
                                if (ImageConvert.IMAGEtoPNG(file.Directory + @"\" + file.Name, file.Directory + @"\" + dest1) != false)
                                {
                                    File.Move(file.Directory + @"\" + dest1, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dest2);
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "no such file or directory."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");
                                    Common.DeleteDirectoryFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");
                                    return;
                                }
                            }
                    }
                }

                Common.ImageFileName = lst.ToArray();
                Common.ImageFileExt = lst2.ToArray();
            }
        }

        private void BackgroundWorker_Convert_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Unused
        }

        private void BackgroundWorker_Convert_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Unused
        }
    }
}
