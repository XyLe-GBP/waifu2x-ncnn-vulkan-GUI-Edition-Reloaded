using NVGE.Localization;
using NVGE.src.Forms;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

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

        //static FormSplash fs;
        static FormSplashWPF fs;
        static object lockobj;
        private bool RunUpdate = false;
        private Task UpdateTask;
        private ProcessStartInfo UpdateProcess;

        public FormMain()
        {
            InitializeComponent();
            pictureBox_DD.AllowDrop = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);
            Text = "waifu2x-nvger ( build: " + ver.FileVersion.ToString() + "-Beta )";

            if (!File.Exists(Common.xmlpath))
            {
                Common.InitConfig();
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\updated.dat"))
            {
                TopMost = true;
                TopMost = false;
            }

            lockobj = new object();

            try
            {
                lock (lockobj)
                {
                    ThreadStart ts = new(StartThread);
                    Thread thread = new(ts)
                    {
                        Name = "Splash",
                        IsBackground = true
                    };
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();

                    DlgMsg d = new(ShowMessage);

                    //fs?.Invoke(d, "Initializing...");
                    fs?.Dispatcher.Invoke(d, "Initializing...");
                    label1.Text = Strings.DragDropCaption;
                    Thread.Sleep(200);

                    foreach (var files in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\res", "*", SearchOption.AllDirectories))
                    {
                        FileInfo fi = new(files);
                        if (fs != null)
                        {
                            fs.Dispatcher.Invoke(d, string.Format(Strings.SplashFormFileCaption, fi.Name));
                            Thread.Sleep(10);
                        }
                    }

                    //fs?.Invoke(d, Strings.SplashFormConfigCaption);
                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, Strings.SplashFormConfigCaption);
                    }


                    Config.Load(Common.xmlpath);

                    Common.FFmpegPath = Config.Entry["FFmpegLocation"].Value;
                    Common.ImageParam = Config.Entry["Param"].Value;
                    Common.VideoParam = Config.Entry["VideoParam"].Value;
                    Common.AudioParam = Config.Entry["AudioParam"].Value;
                    Common.MergeParam = Config.Entry["MergeParam"].Value;

                    if (Directory.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\"))
                    {
                        Common.DeleteDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\");
                    }
                    if (Config.Entry["VideoLocation"].Value != "")
                    {
                        Directory.CreateDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames");
                        Directory.CreateDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames2x");
                        Common.DeletePathFrames = Config.Entry["VideoLocation"].Value + @"\image-frames";
                        Common.DeletePathFrames2x = Config.Entry["VideoLocation"].Value + @"\image-frames2x";
                    }
                    else
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames");
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x");
                        Common.DeletePathFrames = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                        Common.DeletePathFrames2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
                    }
                    if (Config.Entry["AudioLocation"].Value != "")
                    {
                        Directory.CreateDirectory(Config.Entry["AudioLocation"].Value + @"\audio");
                        Common.DeletePathAudio = Config.Entry["AudioLocation"].Value + @"\audio";
                    }
                    else
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\audio");
                        Common.DeletePathAudio = Directory.GetCurrentDirectory() + @"\_temp-project\audio";
                    }

                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images");
                    Thread.Sleep(200);

                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, Strings.SplashFormSystemCaption);
                    }
                    string[] OSInfo = new string[17];
                    string[] CPUInfo = new string[3];
                    string[] GPUInfo = new string[3];
                    SystemInfo.GetSystemInformation(OSInfo);
                    SystemInfo.GetProcessorsInformation(CPUInfo);
                    SystemInfo.GetVideoControllerInformation(GPUInfo);

                    List<string> GPUList = new();
                    List<long> GPURAMList = new();
                    string[] vn = null;
                    long[] vr = null;

                    VRAM v = new();
                    VRAMInfo vram = new(vn, vr);
                    vram = v.GetdGPUInfo();
                    GPUList = new(vram.Name);//SystemInfo.GetGraphicsCardsInformation();
                    GPURAMList = new(vram.VRAM);//SystemInfo.GetGraphicsCardNamesInformation();
                    if (GPUList.Count == 0)
                    {
                        MessageBox.Show(Strings.GPUInfomationFailedCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (GPURAMList.Count == 0)
                    {
                        MessageBox.Show(Strings.GPUInfomationFailedCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    ResetLabels();
                    //label_OS.Text = OSInfo[1] + " - " + OSInfo[3] + " [ build: " + OSInfo[4] + " ]";
                    //label_Processor.Text = CPUInfo[0] + " [ " + CPUInfo[1] + " Core / " + CPUInfo[2] + " Threads ]";
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);

                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, "Detected OS: " + OSInfo[1]);
                    }
                    Thread.Sleep(10);
                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, "Detected CPU: " + CPUInfo[0]);
                    }
                    Thread.Sleep(10);

                    int gpucount = 0;
                    foreach (var gpu in GPUList)
                    {
                        if (gpu != null && GPURAMList[gpucount] != 0 && !Common.GPUList.Contains(gpu))
                        {
                            fs?.Dispatcher.Invoke(d, "Detected GPU: " + gpu);
                            Thread.Sleep(10);
                            //comboBox_GPU.Items.Add(gpu);
                            Common.GPUList.Add(gpu);
                            Common.GPURAMList.Add(GPURAMList[gpucount]);
                        }
                        else if (gpu != null && GPURAMList[gpucount] == 0)
                        {
                            fs?.Dispatcher.Invoke(d, "Detected Discrate iGPU: " + gpu);
                            Thread.Sleep(10);
                            //comboBox_GPU.Items.Add(gpu);
                            Common.GPUList.Add(gpu);
                            Common.GPURAMList.Add(GPURAMList[gpucount]);
                        }
                        gpucount++;
                    }

                    /*comboBox_GPU.SelectedIndex = 0;
                    if (Common.GPUList.Count == 1)
                    {
                        comboBox_GPU.Enabled = false;
                    }
                    else
                    {
                        comboBox_GPU.Enabled = true;
                    }

                    label_Graphic.Text = Common.GPUList[0] + " [ " + Common.GPURAMList[0] + " MiB RAM ]";*/

                    /*if (GPUList.Count == 1)
                    {
                        if (fs != null)
                        {
                            fs.Dispatcher.Invoke(d, "Detected GPU: " + GPUNList);
                        }
                        Thread.Sleep(10);
                        comboBox_GPU.Items.Add(GPUNList[0]);
                        comboBox_GPU.SelectedIndex = 0;
                        comboBox_GPU.Enabled = false;
                        label_Graphic.Text = GPUList[0];
                    }
                    else
                    {
                        foreach (var GPU in GPUNList)
                        {
                            if (fs != null)
                            {
                                fs.Dispatcher.Invoke(d, "Detected GPU: " + GPU);
                            }
                            Thread.Sleep(10);
                            comboBox_GPU.Items.Add(GPU);
                        }
                        comboBox_GPU.SelectedIndex = 0;
                        comboBox_GPU.Enabled = true;
                        label_Graphic.Text = GPUList[0];
                    }*/

                    ResetLabels();
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);

                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, Strings.SplashFormUpdateCaption);
                    }
                    Thread.Sleep(200);
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\updated.dat"))
                    {
                        if (fs != null)
                        {
                            fs.Dispatcher.Invoke(d, Strings.SplashFormUpdatingCaption);
                        }
                        File.Delete(Directory.GetCurrentDirectory() + @"\updated.dat");
                        string updpath = Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().LastIndexOf('\\')];
                        File.Delete(updpath + @"\updater.exe");
                        File.Delete(updpath + @"\waifu2x-nvger.zip");
                        Common.DeleteDirectory(updpath + @"\updater-temp");
                        Thread.Sleep(200);

                        if (fs != null)
                        {
                            fs.Dispatcher.Invoke(d, Strings.SplashFormUpdatedCaption);
                        }
                        Thread.Sleep(200);

                        using Form dummy = new();
                        dummy.TopMost = true;
                        MessageBox.Show(dummy, Strings.UpdateCompletedCaption, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dummy.TopMost = false;
                    }
                    else
                    {
                        if (bool.Parse(Config.Entry["CheckUpdateWithStartup"].Value) == true)
                        {
                            UpdateTask = Task.Run(CheckForUpdatesForInit);
                            UpdateTask.Wait();

                            if (RunUpdate == true)
                            {
                                CloseSplash();
                                Activate();
                                Close();
                                return;
                            }
                        }
                    }

                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, Strings.SplashFormFFCaption);
                    }

                    if (bool.Parse(Config.Entry["CheckUpdateFFWithStartup"].Value) == true)
                    {
                        var ffupdate = Task.Run(() => CheckForFFmpeg());
                        ffupdate.Wait();
                    }

                    if (fs != null)
                    {
                        fs.Dispatcher.Invoke(d, Strings.SplashFormFinalCaption);
                    }
                    Thread.Sleep(200);
                }

                if (Common.GlobalException is not null)
                {
                    MessageBox.Show(this, string.Format(Strings.UnExpectedError, Common.GlobalException), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                CloseSplash();
                Activate();
            }
            catch (Exception ex)
            {
                using Form dummy = new();
                dummy.TopMost = true;
                MessageBox.Show(dummy, "An unexpected error has occurred.\n\n" + ex, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dummy.TopMost = false;

                CloseSplash();
                Activate();
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
                    if (file.Extension.ToUpper() == ".EPS")
                    {
                        if (Common.CheckGhostscript())
                        {
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
                            MessageBox.Show(Strings.NotInstalledGSCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
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
                }
                else
                {
                    if (lst.Contains(".eps") || lst.Contains(".EPS"))
                    {
                        if (!Common.CheckGhostscript())
                        {
                            MessageBox.Show(Strings.NotInstalledGSCaption2, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterVideo,
                FilterIndex = 8,
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
                if (file.Extension.ToUpper() == ".GIF")
                {
                    button_Video.Text = Strings.ButtonUpScaleGIF;
                    label1.Text = Strings.AnimGIFCaption;
                    Common.GIFflag = true;
                }
                else
                {
                    button_Video.Text = Strings.ButtonUpscaleVideo;
                    label1.Text = Strings.VideoCaption;
                    Common.GIFflag = false;
                }

                Common.GetVideoFps(Common.VideoPath);

                return;
            }
            else
            {
                return;
            }
        }

        private void OpenFromClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                var img = Clipboard.GetImage();
                if (img != null)
                {
                    ImageConvert.IMAGEtoPNG32Async(Common.xmlpath, Directory.GetCurrentDirectory() + @"\tmp.png", true, img);
                    FileInfo file = new(Directory.GetCurrentDirectory() + @"\tmp.png");
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = Strings.ClipboardCaption;
                    label_Size.Text = sz + Strings.SizeString;
                    button_Image.Enabled = true;
                    closeFileCToolStripMenuItem.Enabled = true;
                    label1.Visible = false;
                    pictureBox_DD.ImageLocation = Directory.GetCurrentDirectory() + @"\tmp.png";
                    Common.ImageFile = new string[1];
                    Common.ImageFile[0] = Directory.GetCurrentDirectory() + @"\tmp.png";
                }
            }
            else
            {
                MessageBox.Show(Strings.ClipboardErrorCaption, Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void CloseFileCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Common.ImageFile == null && Common.VideoPath == null)
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
                if (closeFileCToolStripMenuItem.Enabled == true)
                {
                    closeFileCToolStripMenuItem.Enabled = false;
                }
                if (Common.GIFflag == true)
                {
                    Common.GIFflag = false;
                }
                button_Video.Text = Strings.ButtonUpscaleVideo;
                ResetLabels();
                toolStripStatusLabel_Status.Text = Strings.NotReadedStatusString;
                toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 255, 0, 0);
                label1.Visible = true;
                label1.Text = Strings.DragDropCaption;
                pictureBox_DD.Image = null;
                if (File.Exists(Directory.GetCurrentDirectory() + @"\tmp.png"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
                }
                if (File.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4"))
                {
                    File.Delete(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4");
                }
            }
        }

        private void ExitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            return;
        }

        private void UpscalingSettingsUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            using FormImageUpscaleSettings form = new();
            form.ShowDialog();
            Common.ImageParam = Config.Entry["Param"].Value;
            return;
        }

        private void VideoUpscalingSettingsVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            using FormVideoUpscaleSettings form = new();
            form.ShowDialog();
            if (Config.Entry["VideoLocation"].Value != "")
            {
                Directory.CreateDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames");
                Directory.CreateDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames2x");
                Common.DeletePathFrames = Config.Entry["VideoLocation"].Value + @"\image-frames";
                Common.DeletePathFrames2x = Config.Entry["VideoLocation"].Value + @"\image-frames2x";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames");
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x");
                Common.DeletePathFrames = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames";
                Common.DeletePathFrames2x = Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x";
            }
            if (Config.Entry["AudioLocation"].Value != "")
            {
                Directory.CreateDirectory(Config.Entry["AudioLocation"].Value + @"\audio");
                Common.DeletePathAudio = Config.Entry["AudioLocation"].Value + @"\audio";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\audio");
                Common.DeletePathAudio = Directory.GetCurrentDirectory() + @"\_temp-project\audio";
            }
            Common.FFmpegPath = Config.Entry["FFmpegLocation"].Value;
            Common.VideoParam = Config.Entry["VideoParam"].Value;
            Common.AudioParam = Config.Entry["AudioParam"].Value;
            Common.MergeParam = Config.Entry["MergeParam"].Value;
            return;
        }

        private void AboutWaifu2xncnnvulkanGUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FormAbout Form = new();
            Form.ShowDialog();
            return;
        }

        private void Button_Image_Click(object sender, EventArgs e)
        {
            if (Common.ImageParam.Length < 69 || Common.ImageParam.Length == 0 || string.IsNullOrWhiteSpace(Common.ImageParam))
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using FormImageUpscaleSettings form = new();
                form.ShowDialog();
                return;
            }

            Config.Load(Common.xmlpath);

            int fmt = int.Parse(Config.Entry["Format"].Value);
            int engine = int.Parse(Config.Entry["ConversionType"].Value);

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
                        case 4:
                            FormImageManualFormat form = new();
                            form.ShowDialog();
                            form.Dispose();
                            ft = Common.ManualImageFormatFilter;
                            ext = ".png";
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
                    string dst = Common.ReplaceForRegex(Common.ImageFileName[0], Common.ImageFileExt[0], ext);

                    int scl = int.Parse(Config.Entry["Scale"].Value);
                    switch (engine)
                    {
                        case 0: // waifu2x
                            {
                                switch (scl)
                                {
                                    case 2: // x4
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x8
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x16
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        case 1: // cugan
                            {
                                switch (scl)
                                {
                                    case 4: // x8
                                        {
                                            using FormProgress Form2 = new(0, 1);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 5: // x16
                                        {
                                            using FormProgress Form2 = new(0, 1);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 6: // x32
                                        {
                                            using FormProgress Form2 = new(0, 1);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(0, 1);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        case 2: // realesrgan
                            {
                                switch (scl)
                                {
                                    case 1: // x8
                                        {
                                            using FormProgress Form2 = new(0, 2);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 2: // x12
                                        {
                                            using FormProgress Form2 = new(0, 2);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x16
                                        {
                                            using FormProgress Form2 = new(0, 2);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x32
                                        {
                                            using FormProgress Form2 = new(0, 2);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            using FormProgress Form2 = new(0, 2);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        default:
                            {
                                switch (scl)
                                {
                                    case 2: // x4
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x8
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x16
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            File.Move(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + dst);

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(0);
                                            Common.SFDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\" + dst;
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*").Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                    }


                    switch (fmt)
                    {
                        case 3: // Icon
                            {
                                string dst2 = Common.ReplaceForRegex(Common.SFDSavePath, ".ico", ".png");
                                bool err = ImageConvert.IMAGEtoICON(dst2, Common.SFDSavePath);
                                File.Delete(dst2);
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
                            break;
                        case 4: // Custom
                            {
                                bool err = ImageConvert.IMAGEtoAnyIMAGE(Common.SFDSavePath, Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat);
                                File.Delete(Common.SFDSavePath);
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
                            break;
                        default:
                            break;
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

                    switch (bool.Parse(Config.Entry["UpScaleDetail"].Value))
                    {
                        case true:
                            FormImageUpscaleDetail formImageUpscaleDetail = new();
                            formImageUpscaleDetail.ShowDialog();
                            formImageUpscaleDetail.Dispose();
                            break;
                        case false:
                            Common.ImgDetFlag = 0;
                            break;
                    }

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
                                switch (fmt)
                                {
                                    case 4:
                                        switch (bool.Parse(Config.Entry["Pixel"].Value))
                                        {
                                            case true:
                                                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat));
                                                break;
                                            case false:
                                                break;
                                        }
                                        File.Move(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat, sfd.FileName);
                                        break;
                                    default:
                                        switch (bool.Parse(Config.Entry["Pixel"].Value))
                                        {
                                            case true:
                                                ImageConvert.IMAGEResize(Common.SFDSavePath, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(Common.SFDSavePath));
                                                break;
                                            case false:
                                                break;
                                        }
                                        File.Move(Common.SFDSavePath, sfd.FileName);
                                        break;
                                }
                            }
                            else
                            {
                                switch (fmt)
                                {
                                    case 4:
                                        switch (bool.Parse(Config.Entry["Pixel"].Value))
                                        {
                                            case true:
                                                ImageConvert.IMAGEResize(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat));
                                                break;
                                            case false:
                                                break;
                                        }
                                        File.Move(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x\final" + Common.ManualImageFormat, sfd.FileName);
                                        break;
                                    default:
                                        switch (bool.Parse(Config.Entry["Pixel"].Value))
                                        {
                                            case true:
                                                ImageConvert.IMAGEResize(Common.SFDSavePath, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(Common.SFDSavePath));
                                                break;
                                            case false:
                                                break;
                                        }
                                        File.Move(Common.SFDSavePath, sfd.FileName);
                                        break;
                                }
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
                                switch (bool.Parse(Config.Entry["DestFolder"].Value))
                                {
                                    case true:
                                        Process.Start("EXPLORER.EXE", @"/select,""" + sfd.FileName + @"""");
                                        break;
                                    case false:
                                        break;
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
                    switch (fmt)
                    {
                        case 4:
                            FormImageManualFormat form = new();
                            form.ShowDialog();
                            form.Dispose();
                            break;
                    }
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources");

                    Common.ConvMultiFlag = 1;
                    Common.ProgMin = 0;
                    Common.ProgMax = Common.ImageFile.Length;

                    using FormProgress Form1 = new(7);
                    Form1.ShowDialog();

                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x");

                    int scl = int.Parse(Config.Entry["Scale"].Value);
                    switch (engine)
                    {
                        case 0: // waifu2x
                            {
                                switch (scl)
                                {
                                    case 2: // x4
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x8
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x16
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        case 1: // realcugan
                            {
                                switch (scl)
                                {
                                    case 4: // x8
                                        {
                                            using FormProgress Form2 = new(1, 1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 5: // x16
                                        {
                                            using FormProgress Form2 = new(1, 1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 6: // x32
                                        {
                                            using FormProgress Form2 = new(1, 1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(1, 1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        case 2: // realesrgan
                            {
                                switch (scl)
                                {
                                    case 1: // x8
                                        {
                                            using FormProgress Form2 = new(1, 2);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 2: // x12
                                        {
                                            using FormProgress Form2 = new(1, 2);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x16
                                        {
                                            using FormProgress Form2 = new(1, 2);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x32
                                        {
                                            using FormProgress Form2 = new(1, 2);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(1, 2);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                        default:
                            {
                                switch (scl)
                                {
                                    case 2: // x4
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 3: // x8
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    case 4: // x16
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }

                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\sources", "*.*"))
                                            {
                                                File.Delete(file);
                                            }
                                            foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*.*"))
                                            {
                                                FileInfo fi = new(file);
                                                File.Move(file, Directory.GetCurrentDirectory() + @"\_temp-project\images\sources\" + fi.Name);
                                            }

                                            Form2.ShowDialog();

                                            if (Common.AbortFlag != 0)
                                            {
                                                break;
                                            }
                                        }
                                        break;
                                    default: // nml
                                        {
                                            using FormProgress Form2 = new(1);
                                            Common.FBDSavePath = Directory.GetCurrentDirectory() + @"\_temp-project\images\2x";
                                            Common.ProgMin = 0;
                                            Common.ProgMax = Common.ImageFile.Length;

                                            Common.stopwatch = new Stopwatch();
                                            Common.stopwatch.Start();

                                            Form2.ShowDialog();
                                        }
                                        break;
                                }
                            }
                            break;
                    }

                    switch (fmt)
                    {
                        case 3: // Icon
                            {
                                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                                {
                                    FileInfo fi = new(file);
                                    string dst = Common.ReplaceForRegex(fi.Name, fi.Extension, ".ico");
                                    bool err = ImageConvert.IMAGEtoICON(Common.FBDSavePath + @"\" + fi.Name, Common.FBDSavePath + @"\" + dst);
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
                            break;
                        case 4: // Custom
                            {
                                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                                {
                                    FileInfo fi = new(file);
                                    string dst = Common.ReplaceForRegex(fi.Name, fi.Extension, Common.ManualImageFormat);
                                    bool err = ImageConvert.IMAGEtoAnyIMAGE(Common.FBDSavePath + @"\" + fi.Name, Common.FBDSavePath + @"\" + dst);
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
                            break;
                        default:
                            break;
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

                    switch (bool.Parse(Config.Entry["UpScaleDetail"].Value))
                    {
                        case true:
                            FormImageUpscaleDetail formImageUpscaleDetail = new();
                            formImageUpscaleDetail.ShowDialog();
                            formImageUpscaleDetail.Dispose();
                            break;
                        case false:
                            Common.ImgDetFlag = 0;
                            break;
                    }

                    Common.stopwatch.Reset();

                    if (Common.ImgDetFlag == 0)
                    {
                        FolderBrowserDialog fbd = new()
                        {
                            Description = Strings.FBDImageTitle,
                            RootFolder = Environment.SpecialFolder.Desktop,
                            SelectedPath = Application.ExecutablePath.Replace(Path.GetFileName(Application.ExecutablePath), ""),
                            ShowNewFolderButton = true
                        };

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
                                        switch (bool.Parse(Config.Entry["Pixel"].Value))
                                        {
                                            case true:
                                                ImageConvert.IMAGEResize(file, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(file));
                                                break;
                                            case false:
                                                break;
                                        }
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
                                    switch (bool.Parse(Config.Entry["DestFolder"].Value))
                                    {
                                        case true:
                                            Process.Start("EXPLORER.EXE", Common.FBDSavePath);
                                            break;
                                        case false:
                                            break;
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
                                foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\_temp-project\images\2x", "*"))
                                {
                                    FileInfo fi = new(file);
                                    switch (bool.Parse(Config.Entry["Pixel"].Value))
                                    {
                                        case true:
                                            ImageConvert.IMAGEResize(file, int.Parse(Config.Entry["Pixel_width"].Value), int.Parse(Config.Entry["Pixel_height"].Value), ImageConvert.GetFormat(file));
                                            break;
                                        case false:
                                            break;
                                    }
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
                                    switch (fmt)
                                    {
                                        case 3:
                                            {
                                                ImageConvert.IMAGEtoICON(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png", Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".ico");
                                                File.Delete(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png");
                                            }
                                            break;
                                        case 4:
                                            {
                                                ImageConvert.IMAGEtoAnyIMAGE(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png", Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + Common.ManualImageFormat);
                                                File.Delete(Common.FBDSavePath + @"\" + Common.SFDRandomNumber() + ".png");
                                            }
                                            break;
                                        default:
                                            break;
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
                                switch (bool.Parse(Config.Entry["DestFolder"].Value))
                                {
                                    case true:
                                        Process.Start("EXPLORER.EXE", Common.FBDSavePath);
                                        break;
                                    case false:
                                        break;
                                }
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
            if (Common.ImageParam.Length < 69 || Common.ImageParam.Length == 0 || string.IsNullOrWhiteSpace(Common.ImageParam))
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using FormImageUpscaleSettings form = new();
                form.ShowDialog();
                return;
            }
            if (Common.VideoParam.Length <= 50 || Common.VideoParam.Length == 0 || string.IsNullOrWhiteSpace(Common.VideoParam))
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using FormVideoUpscaleSettings form = new();
                form.ShowDialog();
                return;
            }
            if (Common.AudioParam.Length <= 50 || Common.AudioParam.Length == 0 || string.IsNullOrWhiteSpace(Common.AudioParam))
            {
                MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                using FormVideoUpscaleSettings form = new();
                form.ShowDialog();
                return;
            }
            if (Common.downloadClient != null)
            {
                Common.downloadClient = null;
            }

            Config.Load(Common.xmlpath);

            int engine = int.Parse(Config.Entry["ConversionType"].Value);
            string ffp = Config.Entry["FFmpegLocation"].Value;
            int enc = int.Parse(Config.Entry["OutputCodecIndex"].Value);
            string vl = Config.Entry["VideoLocation"].Value;
            string al = Config.Entry["AudioLocation"].Value;
            string delvlpath, delvlpath2x, alpath;
            string acodec = enc switch
            {
                3 => "audio.m4a",
                4 => "audio.mp3",
                _ => "audio.wav",
            };
            if (Common.UpscaleFlag == 0)
            {
                using FormProgress Form1 = new(5);

                switch (engine)
                {
                    case 0: // waifu2x
                        {
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

                            if (Common.GIFflag == true)
                            {
                                Process ps = new();
                                ProcessStartInfo pi = new()
                                {
                                    FileName = ffp,
                                    Arguments = "-i \"" + Common.VideoPath + "\" -movflags faststart -pix_fmt yuv420p -vf \"scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2\" " + Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    UseShellExecute = true
                                };
                                ps = Process.Start(pi);
                                ps.WaitForExit();

                                Common.VideoPath = Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4";
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

                            if (!File.Exists(alpath) && Common.GIFflag != true)
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

                            if (Common.GIFflag == true)
                            {
                                button_Video.Text = Strings.ButtonReUpScaleGIF;
                            }
                            else
                            {
                                button_Video.Text = Strings.ButtonReUpscaleVideo;
                            }

                            button_Merge.Enabled = true;
                            MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 1: // cugan
                        {
                            using FormProgress Form2 = new(2, 1);
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

                            if (Common.GIFflag == true)
                            {
                                Process ps = new();
                                ProcessStartInfo pi = new()
                                {
                                    FileName = ffp,
                                    Arguments = "-i \"" + Common.VideoPath + "\" -movflags faststart -pix_fmt yuv420p -vf \"scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2\" " + Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    UseShellExecute = true
                                };
                                ps = Process.Start(pi);
                                ps.WaitForExit();

                                Common.VideoPath = Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4";
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

                            if (!File.Exists(alpath) && Common.GIFflag != true)
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

                            if (Common.GIFflag == true)
                            {
                                button_Video.Text = Strings.ButtonReUpScaleGIF;
                            }
                            else
                            {
                                button_Video.Text = Strings.ButtonReUpscaleVideo;
                            }

                            button_Merge.Enabled = true;
                            MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case 2: // esrgan
                        {
                            using FormProgress Form2 = new(2, 2);
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

                            if (Common.GIFflag == true)
                            {
                                Process ps = new();
                                ProcessStartInfo pi = new()
                                {
                                    FileName = ffp,
                                    Arguments = "-i \"" + Common.VideoPath + "\" -movflags faststart -pix_fmt yuv420p -vf \"scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2\" " + Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    UseShellExecute = true
                                };
                                ps = Process.Start(pi);
                                ps.WaitForExit();

                                Common.VideoPath = Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4";
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

                            if (!File.Exists(alpath) && Common.GIFflag != true)
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

                            if (Common.GIFflag == true)
                            {
                                button_Video.Text = Strings.ButtonReUpScaleGIF;
                            }
                            else
                            {
                                button_Video.Text = Strings.ButtonReUpscaleVideo;
                            }

                            button_Merge.Enabled = true;
                            MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    default:
                        {
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

                            if (Common.GIFflag == true)
                            {
                                Process ps = new();
                                ProcessStartInfo pi = new()
                                {
                                    FileName = ffp,
                                    Arguments = "-i \"" + Common.VideoPath + "\" -movflags faststart -pix_fmt yuv420p -vf \"scale = trunc(iw / 2) * 2:trunc(ih / 2) * 2\" " + Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4",
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    UseShellExecute = true
                                };
                                ps = Process.Start(pi);
                                ps.WaitForExit();

                                Common.VideoPath = Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4";
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

                            if (!File.Exists(alpath) && Common.GIFflag != true)
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

                            if (Common.GIFflag == true)
                            {
                                button_Video.Text = Strings.ButtonReUpScaleGIF;
                            }
                            else
                            {
                                button_Video.Text = Strings.ButtonReUpscaleVideo;
                            }

                            button_Merge.Enabled = true;
                            MessageBox.Show(Strings.VUP, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                }
            }
            else
            {
                using FormProgress Form1 = new(4);
                switch (engine)
                {
                    case 0: //waifu2x
                        {
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
                        break;
                    case 1: // cugan
                        {
                            using FormProgress Form2 = new(3, 1);
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
                        break;
                    case 2: // esrgan
                        {
                            using FormProgress Form2 = new(3, 2);
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
                        break;
                    default:
                        {
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
                        break;
                }
            }
        }

        private void Button_Merge_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            if (Common.GIFflag == true) // アニメーション GIFかどうか
            {
                string ffp = Config.Entry["FFmpegLocation"].Value;
                string sfps = Config.Entry["FPS"].Value;

                SaveFileDialog sfd = new()
                {
                    FileName = Common.SFDRandomNumber() + "_Upscaled",
                    InitialDirectory = "",
                    Filter = Strings.FilterMPEG4,
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
                    pi.Arguments = "-r " + sfps + " -i \"" + Directory.GetCurrentDirectory() + @"\_temp-project\image-frames2x\image-%09d.png" + "\" -vcodec libx264 -pix_fmt yuv420p -r 60 \"" + Common.SFDSavePath + "\"";
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
                        Common.GIFflag = false;
                        File.Delete(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4");
                        return;
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
                        File.Delete(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4");
                        Common.GIFflag = false;
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else // GIFじゃない場合
            {
                bool cvot = bool.Parse(Config.Entry["VideoGeneration"].Value);
                int vot = int.Parse(Config.Entry["GenerationIndex"].Value);

                if (cvot != false) // 生成方法を指定しているかどうか
                {
                    if (vot != 0) // OpenCV
                    {
                        SaveFileDialog sfd = new()
                        {
                            FileName = Common.SFDRandomNumber() + "_Upscaled",
                            InitialDirectory = "",
                            Filter = Strings.FilterMPEG4,
                            FilterIndex = 1,
                            Title = Strings.SFDVideoTitle,
                            OverwritePrompt = true,
                            RestoreDirectory = true
                        };
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            Common.SFDSavePath = sfd.FileName;

                            string sfps = Config.Entry["FPS"].Value;
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
                            Filter = Strings.FilterMPEG4,
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
                else
                {
                    if (Common.MergeParam.Length <= 70)
                    {
                        MessageBox.Show(Strings.SettingError, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

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
                        Filter = Strings.FilterMPEG4,
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
            Config.Load(Common.xmlpath);

            string vl = Config.Entry["VideoLocation"].Value;
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
            Config.Load(Common.xmlpath);
            if (Config.Entry["VideoLocation"].Value != "")
            {
                Common.DeleteDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames");
                Common.DeleteDirectory(Config.Entry["VideoLocation"].Value + @"\image-frames2x");
            }
            if (Config.Entry["AudioLocation"].Value != "")
            {
                Common.DeleteDirectory(Config.Entry["AudioLocation"].Value + @"\audio");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\ffmpeg-release-essentials.zip");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\tmp.png"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\tmp.png");
            }
            if (File.Exists(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4"))
            {
                File.Delete(Directory.GetCurrentDirectory() + @"\_temp-project\tmp.mp4");
            }
            Common.DeleteDirectory(Directory.GetCurrentDirectory() + @"\_temp-project");
            if (RunUpdate == true)
            {
                string updpath = Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().LastIndexOf('\\')];
                UpdateProcess = new()
                {
                    FileName = updpath + @"\updater.exe",
                    Arguments = null,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Normal,
                };
                Process.Start(UpdateProcess);
            }
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
            Common.GIFflag = false;
            button_Video.Text = Strings.ButtonUpscaleVideo;
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
                if (ext == ".BMP" || ext == ".DIB" || ext == ".WEBP" || ext == ".ICO" || ext == ".ICNS" || ext == ".JFIF" || ext == ".JPG" || ext == ".JPE" || ext == ".JPEG" || ext == ".PJPEG" || ext == ".PJP" || ext == ".PNG" || ext == ".PICT" || ext == ".SVG" || ext == ".SVGZ" || ext == ".TIF" || ext == ".TIFF")
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
                    return;
                }
                else if (ext == ".EPS")
                {
                    if (Common.CheckGhostscript())
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
                        return;
                    }
                    else
                    {
                        MessageBox.Show(Strings.NotInstalledGSCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ext == ".GIF")
                {
                    Common.VideoPath = file.FullName;
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = dd[0];
                    label_Size.Text = sz + Strings.SizeString;
                    button_Video.Enabled = true;
                    label1.Visible = true;
                    label1.Text = Strings.AnimGIFCaption;
                    button_Video.Text = Strings.ButtonUpScaleGIF;
                    closeFileCToolStripMenuItem.Enabled = true;
                    Common.GIFflag = true;

                    Common.GetVideoFps(Common.VideoPath);

                    return;
                }
                else if (ext == ".AVI" || ext == ".MP4" || ext == ".MOV" || ext == ".WMV" || ext == ".MKV" || ext == ".WEBM")
                {
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

                    Common.GetVideoFps(Common.VideoPath);

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

                if (lst.Contains(".eps") || lst.Contains(".EPS"))
                {
                    if (!Common.CheckGhostscript())
                    {
                        MessageBox.Show(Strings.NotInstalledGSCaption2, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void PictureBox_DD_Click(object sender, EventArgs e)
        {
            FormShowPicture formShowPicture = new(Directory.GetCurrentDirectory() + @"\tmp.png");
            formShowPicture.ShowDialog();
            formShowPicture.Dispose();
        }

        private void PictureBox_DD_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void PictureBox_DD_DragDrop(object sender, DragEventArgs e)
        {
            button_Video.Text = Strings.ButtonUpscaleVideo;
            Common.GIFflag = false;
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
                if (ext == ".BMP" || ext == ".DIB" || ext == ".WEBP" || ext == ".ICO" || ext == ".ICNS" || ext == ".JFIF" || ext == ".JPG" || ext == ".JPE" || ext == ".JPEG" || ext == ".PJPEG" || ext == ".PJP" || ext == ".PNG" || ext == ".PICT" || ext == ".SVG" || ext == ".SVGZ" || ext == ".TIF" || ext == ".TIFF")
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

                    return;
                }
                else if (ext == ".EPS")
                {
                    if (Common.CheckGhostscript())
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

                        return;
                    }
                    else
                    {
                        MessageBox.Show(Strings.NotInstalledGSCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (ext == ".GIF")
                {
                    Common.VideoPath = file.FullName;
                    long FileSize = file.Length;
                    string sz = string.Format("{0} ", FileSize);
                    toolStripStatusLabel_Status.Text = Strings.ReadedString;
                    toolStripStatusLabel_Status.ForeColor = Color.FromArgb(0, 0, 225, 0);
                    ReadLabels();
                    label_File.Text = dd[0];
                    label_Size.Text = sz + Strings.SizeString;
                    button_Video.Enabled = true;
                    label1.Visible = true;
                    label1.Text = Strings.AnimGIFCaption;
                    button_Video.Text = Strings.ButtonUpScaleGIF;
                    closeFileCToolStripMenuItem.Enabled = true;
                    Common.GIFflag = true;

                    Common.GetVideoFps(Common.VideoPath);

                    return;
                }
                else if (ext == ".AVI" || ext == ".MP4" || ext == ".MOV" || ext == ".WMV" || ext == ".MKV" || ext == ".WEBM")
                {
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

                    Common.GetVideoFps(Common.VideoPath);

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

                if (lst.Contains(".eps") || lst.Contains(".EPS"))
                {
                    if (!Common.CheckGhostscript())
                    {
                        MessageBox.Show(Strings.NotInstalledGSCaption2, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        /// <summary>
        /// 動画解像度の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeVideoResolutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            string ffp = Config.Entry["FFmpegLocation"].Value;
            if (ffp is null || ffp.Length is 0)
            {
                MessageBox.Show(Strings.FFmpegLocationNotFoundCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                    FileName = Common.SFDRandomNumber(),
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

        /// <summary>
        /// 動画形式の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeVideoFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);
            FileInfo fi, fi2;

            string ffp = Config.Entry["FFmpegLocation"].Value;
            if (ffp is null || ffp.Length is 0)
            {
                MessageBox.Show(Strings.FFmpegLocationNotFoundCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterVideo,
                FilterIndex = 8,
                Title = Strings.OpenFormatChangeDialogCaption,
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string open = ofd.FileName;
                SaveFileDialog sfd = new()
                {
                    FileName = Common.SFDRandomNumber(),
                    InitialDirectory = "",
                    Filter = Strings.FilterVideo,
                    FilterIndex = 1,
                    Title = Strings.SaveFormatChangeDialogCaption,
                    OverwritePrompt = true,
                    RestoreDirectory = true
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string save = sfd.FileName;
                    fi = new FileInfo(open);
                    fi2 = new FileInfo(save);

                    if (fi.Extension.ToUpper() == fi2.Extension.ToUpper())
                    {
                        MessageBox.Show(Strings.VideoFormatChange_FmtErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ProcessStartInfo pi = new();
                    Process ps;
                    pi.FileName = ffp;
                    pi.Arguments = "-i \"" + open + "\" \"" + save + "\"";
                    pi.WindowStyle = ProcessWindowStyle.Normal;
                    pi.UseShellExecute = true;
                    ps = Process.Start(pi);
                    ps.WaitForExit();

                    if (File.Exists(save))
                    {
                        MessageBox.Show(Strings.VideoFormatChange_SuccessCaption, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("EXPLORER.EXE", @"/select,""" + save + @"""");
                        return;
                    }
                    else
                    {
                        MessageBox.Show(Strings.VideoFormatChange_ErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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

        /// <summary>
        /// 画像形式の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeImageFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fi, fi2;

            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterImage,
                FilterIndex = 11,
                Title = Strings.OpenFormatChangeDialogCaption,
                RestoreDirectory = true,
                Multiselect = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] opens = ofd.FileNames;

                if (opens.Length == 1)
                {
                    SaveFileDialog sfd = new()
                    {
                        FileName = Common.SFDRandomNumber(),
                        InitialDirectory = "",
                        Filter = Strings.FilterImage,
                        FilterIndex = 1,
                        Title = Strings.SaveFormatChangeDialogCaption,
                        OverwritePrompt = true,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string save = sfd.FileName;
                        fi = new FileInfo(opens[0]);
                        fi2 = new FileInfo(save);

                        if (fi.Extension.ToUpper() == fi2.Extension.ToUpper())
                        {
                            MessageBox.Show(Strings.VideoFormatChange_FmtErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (File.Exists(save))
                        {
                            File.Delete(save);
                        }
                        ImageConvert.IMAGEtoAnyIMAGE(opens[0], save);

                        if (File.Exists(save))
                        {
                            MessageBox.Show(Strings.VideoFormatChange_SuccessCaption, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("EXPLORER.EXE", @"/select,""" + save + @"""");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Strings.VideoFormatChange_ErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else // multiple
                {
                    FolderBrowserDialog fbd = new()
                    {
                        InitialDirectory = "",
                        ShowNewFolderButton = true,
                    };
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        int count = 0;

                        using FormImageConvertTarget Form = new();
                        Form.ShowDialog();

                        if (string.IsNullOrEmpty(Common.ImageConversionExtension))
                        {
                            return;
                        }
                        string destext = Common.ImageConversionExtension;

                        foreach (var item in opens)
                        {
                            fi = new FileInfo(item);
                            string fname = fi.Name, savepath = fbd.SelectedPath + @"\" + fname + destext;

                            if (File.Exists(savepath))
                            {
                                File.Delete(savepath);
                            }
                            ImageConvert.IMAGEtoAnyIMAGE(item, savepath);

                            if (File.Exists(savepath))
                            {
                                count++;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.ImageMultipleConversionWarning, fname, destext.Replace(".", "")), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        MessageBox.Show(string.Format(Strings.ImageMultipleConversionSuccess, count, destext.Replace(".", "")), Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("EXPLORER.EXE", @"/select,""" + fbd.SelectedPath + @"""");
                        return;
                    }
                }


            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// アップデート確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void CheckForUpdatesUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    string hv = null!;

                    using Stream hcs = await Task.Run(() => Network.GetWebStreamAsync(appUpdatechecker, Network.GetUri("https://raw.githubusercontent.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/master/VERSIONINFO")));
                    using StreamReader hsr = new(hcs);
                    hv = await Task.Run(hsr.ReadToEndAsync);

                    FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

                    switch (ver.FileVersion.ToString().CompareTo(hv[8..].Replace("\n", "")))
                    {
                        case -1:
                            DialogResult dr = MessageBox.Show(this, Strings.LatestString + hv[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.AppUpdateConfirmCaption, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                using FormUpdateApplicationType fuat = new();
                                fuat.ShowDialog();

                                if (File.Exists(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip"))
                                {
                                    File.Delete(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip");
                                }

                                Common.ProgMin = 0;
                                using FormProgress form = new(8);
                                form.ShowDialog();

                                if (Common.AbortFlag != 0)
                                {
                                    Common.DlcancelFlag = 0;
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                string updpath = Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().LastIndexOf('\\')];

                                if (File.Exists(updpath + @"\updater.exe"))
                                {
                                    File.Delete(updpath + @"\updater.exe");
                                }
                                if (Directory.Exists(updpath + @"\updater-temp"))
                                {
                                    Common.DeleteDirectory(updpath + @"\updater-temp");
                                }
                                if (File.Exists(updpath + @"\waifu2x-nvger.zip"))
                                {
                                    File.Delete(updpath + @"\waifu2x-nvger.zip");
                                }

                                File.Move(Directory.GetCurrentDirectory() + @"\res\updater.exe", updpath + @"\updater.exe");
                                string wtext;
                                switch (Common.ApplicationPortable)
                                {
                                    case false:
                                        {
                                            wtext = Directory.GetCurrentDirectory() + "\r\nrelease";
                                        }
                                        break;
                                    case true:
                                        {
                                            wtext = Directory.GetCurrentDirectory() + "\r\nportable";
                                        }
                                        break;
                                }
                                File.WriteAllText(updpath + @"\updater.txt", wtext);
                                File.Move(updpath + @"\updater.txt", updpath + @"\updater.dat");
                                if (File.Exists(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip"))
                                {
                                    File.Move(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip", updpath + @"\waifu2x-nvger.zip");
                                }

                                ProcessStartInfo pi = new()
                                {
                                    FileName = updpath + @"\updater.exe",
                                    Arguments = null,
                                    UseShellExecute = true,
                                    WindowStyle = ProcessWindowStyle.Normal,
                                };
                                Process.Start(pi);
                                Close();
                                return;
                            }
                            else
                            {
                                DialogResult dr2 = MessageBox.Show(this, Strings.UpdateConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dr2 == DialogResult.Yes)
                                {
                                    Common.OpenURI("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases");
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        case 0:
                            MessageBox.Show(this, Strings.LatestString + hv[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.Uptodate, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case 1:
                            throw new Exception(hv[8..].Replace("\n", "").ToString() + " < " + ver.FileVersion.ToString() + "\nあんたデバッグ版使ってるやろ！！！");
                    }
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, string.Format(Strings.UnExpectedError, ex.ToString()), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show(this, Strings.NotNetworkConnectionCaption, Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 起動時のアップデート確認
        /// </summary>
        private async Task CheckForUpdatesForInit()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                try
                {
                    string hv = null!;

                    // Debug URI:https://dl.cdn.xyle-official.com/content/app/utils/waifu2x/debug/VERSIONINFO
                    // Release URI:https://raw.githubusercontent.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/master/VERSIONINFO

                    using Stream hcs = await Task.Run(() => Network.GetWebStreamAsync(appUpdatechecker, Network.GetUri("https://raw.githubusercontent.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/master/VERSIONINFO")));
                    using StreamReader hsr = new(hcs);
                    hv = await Task.Run(hsr.ReadToEndAsync);
                    Common.GitHubLatestVersion = hv[8..].Replace("\n", "");

                    FileVersionInfo ver = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

                    switch (ver.FileVersion.ToString().CompareTo(hv[8..].Replace("\n", "")))
                    {
                        case -1:
                            DialogResult dr = MessageBox.Show(Strings.LatestString + hv[8..].Replace("\n", "") + "\n" + Strings.CurrentString + ver.FileVersion + "\n" + Strings.AppUpdateConfirmCaption, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                using FormUpdateApplicationType fuat = new();
                                fuat.ShowDialog();

                                if (File.Exists(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip"))
                                {
                                    File.Delete(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip");
                                }

                                Common.ProgMin = 0;
                                using FormProgress form = new(8);
                                form.ShowDialog();

                                if (Common.AbortFlag != 0)
                                {
                                    Common.DlcancelFlag = 0;
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                string updpath = Directory.GetCurrentDirectory()[..Directory.GetCurrentDirectory().LastIndexOf('\\')];

                                if (File.Exists(updpath + @"\updater.exe"))
                                {
                                    File.Delete(updpath + @"\updater.exe");
                                }
                                if (Directory.Exists(updpath + @"\updater-temp"))
                                {
                                    Common.DeleteDirectory(updpath + @"\updater-temp");
                                }
                                if (File.Exists(updpath + @"\waifu2x-nvger.zip"))
                                {
                                    File.Delete(updpath + @"\waifu2x-nvger.zip");
                                }

                                File.Move(Directory.GetCurrentDirectory() + @"\res\updater.exe", updpath + @"\updater.exe");
                                string wtext;
                                switch (Common.ApplicationPortable)
                                {
                                    case false:
                                        {
                                            wtext = Directory.GetCurrentDirectory() + "\r\nrelease";
                                        }
                                        break;
                                    case true:
                                        {
                                            wtext = Directory.GetCurrentDirectory() + "\r\nportable";
                                        }
                                        break;
                                }
                                File.WriteAllText(updpath + @"\updater.txt", wtext);
                                File.Move(updpath + @"\updater.txt", updpath + @"\updater.dat");
                                if (File.Exists(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip"))
                                {
                                    File.Move(Directory.GetCurrentDirectory() + @"\res\waifu2x-nvger.zip", updpath + @"\waifu2x-nvger.zip");
                                }

                                RunUpdate = true;

                                return;
                            }
                            else
                            {
                                DialogResult dr2 = MessageBox.Show(this, Strings.UpdateConfirm, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dr2 == DialogResult.Yes)
                                {
                                    Common.OpenURI("https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/releases");
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        case 0: // Latest
                            break;
                        case 1:
                            throw new Exception(hv[8..].Replace("\n", "").ToString() + " < " + ver.FileVersion.ToString() + "\nあんたデバッグ版使ってるやろ！！！");
                    }
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private async Task CheckForFFmpeg()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
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
                            Config.Entry["FFmpegLocation"].Value = "";
                            Config.Save(Common.xmlpath);
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
                                    Config.Entry["FFmpegLocation"].Value = Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe";
                                    Config.Entry["FFmpegVersion"].Value = "essentials_build " + hv;
                                    Config.Save(Common.xmlpath);
                                    MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                Config.Entry["FFmpegLocation"].Value = "";
                                Config.Save(Common.xmlpath);
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
                else if (Config.Entry["FFmpegVersion"].Value == "") // Unknown version ffmpeg
                {
                    if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                    {
                        DialogResult dr = MessageBox.Show(Strings.UnknownFFmpegCaption, Strings.MSGWarning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                                Config.Entry["FFmpegLocation"].Value = "";
                                Config.Save(Common.xmlpath);
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
                                        Config.Entry["FFmpegLocation"].Value = Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe";
                                        Config.Entry["FFmpegVersion"].Value = "essentials_build " + hv;
                                        Config.Save(Common.xmlpath);
                                        MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    Config.Entry["FFmpegLocation"].Value = "";
                                    Config.Save(Common.xmlpath);
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                Config.Entry["FFmpegLocation"].Value = "";
                                Config.Save(Common.xmlpath);
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
                                        Config.Entry["FFmpegLocation"].Value = Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe";
                                        Config.Entry["FFmpegVersion"].Value = "essentials_build " + hv;
                                        Config.Save(Common.xmlpath);
                                        MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    Config.Entry["FFmpegLocation"].Value = "";
                                    Config.Save(Common.xmlpath);
                                    MessageBox.Show(string.Format(Strings.UnExpectedError, Common.DLlog), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }

                }
                else // Update ffmpeg
                {
                    switch (Config.Entry["FFmpegVersion"].Value.CompareTo(hv))
                    {
                        case -1:
                            DialogResult dr = MessageBox.Show(this, Strings.LatestString + hv + "\n" + Strings.CurrentString + Config.Entry["FFmpegVersion"].Value + "\n" + Strings.FFUpdate, Strings.MSGConfirm, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dr == DialogResult.Yes)
                            {
                                if (File.Exists(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe"))
                                {
                                    File.Delete(Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe");
                                    Form.ShowDialog();

                                    if (Common.AbortFlag != 0)
                                    {
                                        Common.DlcancelFlag = 0;
                                        Config.Entry["FFmpegLocation"].Value = "";
                                        Config.Save(Common.xmlpath);
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
                                                Config.Entry["FFmpegLocation"].Value = Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe";
                                                Config.Entry["FFmpegVersion"].Value = "essentials_build " + hv;
                                                Config.Save(Common.xmlpath);
                                                MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            Config.Entry["FFmpegLocation"].Value = "";
                                            Config.Save(Common.xmlpath);
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
                                            Config.Entry["FFmpegLocation"].Value = "";
                                            Config.Save(Common.xmlpath);
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
                                                    Config.Entry["FFmpegLocation"].Value = Directory.GetCurrentDirectory() + @"\res\ffmpeg.exe";
                                                    Config.Entry["FFmpegVersion"].Value = "essentials_build " + hv;
                                                    Config.Save(Common.xmlpath);
                                                    MessageBox.Show(Strings.DLSuccess, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                {
                                                    MessageBox.Show(string.Format(Strings.UnExpectedError, "extract failed."), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                Config.Entry["FFmpegLocation"].Value = "";
                                                Config.Save(Common.xmlpath);
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
            else
            {
                return;
            }
        }

        #region SplashScreenCommon
        private static void StartThread()
        {
            //fs = new FormSplash();
            fs = new FormSplashWPF();
            //Application.Run(fs);
            fs.ShowDialog();
        }


        private static void CloseSplash()
        {
            SplashDlg d = new(CloseForm);
            fs?.Dispatcher.Invoke(d);
        }

        private delegate void SplashDlg();
        private static void CloseForm()
        {
            fs.Close();
        }

        private delegate void DlgMsg(string message);
        private static void ShowMessage(string message)
        {
            //fs.label_log.Text = message;
            fs.TextBlock_Log.Text = message;
        }
        #endregion

        /*private void ComboBox_GPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Common.GPUList.Count == 0)
            {
                MessageBox.Show(Strings.GPUInfomationFailedCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            label_Graphic.Text = Common.GPUList[comboBox_GPU.SelectedIndex] + " [ " + Common.GPURAMList[comboBox_GPU.SelectedIndex] + " MiB RAM]";
        }*/

        private void PreferencesSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FormPreferencesSettings Form = new();
            Form.ShowDialog();
        }

        private void GetSystemInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using FormSystemInfo Form = new();
            Form.ShowDialog();
        }

        private void ConvertCRgbaSDEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo fi, fi2;

            OpenFileDialog ofd = new()
            {
                FileName = "",
                InitialDirectory = "",
                Filter = Strings.FilterImage,
                FilterIndex = 11,
                Title = Strings.OpenFormatChangeDialogCaption,
                RestoreDirectory = true,
                Multiselect = true,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string[] opens = ofd.FileNames;

                if (opens.Length == 1)
                {
                    SaveFileDialog sfd = new()
                    {
                        FileName = Common.SFDRandomNumber(),
                        InitialDirectory = "",
                        Filter = "DirectDraw Surface (*.dds)|*.dds||",
                        FilterIndex = 1,
                        Title = Strings.SaveFormatChangeDialogCaption,
                        OverwritePrompt = true,
                        RestoreDirectory = true
                    };
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string save = sfd.FileName;
                        fi = new FileInfo(opens[0]);
                        fi2 = new FileInfo(save);

                        if (fi.Extension.ToUpper() == fi2.Extension.ToUpper())
                        {
                            MessageBox.Show(Strings.VideoFormatChange_FmtErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (File.Exists(save))
                        {
                            File.Delete(save);
                        }
                        ImageConvert.IMAGEtoAnyIMAGE(opens[0], save);

                        if (File.Exists(save))
                        {
                            MessageBox.Show(Strings.VideoFormatChange_SuccessCaption, Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("EXPLORER.EXE", @"/select,""" + save + @"""");
                            return;
                        }
                        else
                        {
                            MessageBox.Show(Strings.VideoFormatChange_ErrorCaption, Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else // multiple
                {
                    FolderBrowserDialog fbd = new()
                    {
                        InitialDirectory = "",
                        ShowNewFolderButton = true,
                    };
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        int count = 0;

                        Common.ImageConversionExtension = ".DDS";

                        if (string.IsNullOrEmpty(Common.ImageConversionExtension))
                        {
                            return;
                        }
                        string destext = Common.ImageConversionExtension;

                        foreach (var item in opens)
                        {
                            fi = new FileInfo(item);
                            string fname = fi.Name, savepath = fbd.SelectedPath + @"\" + fname + destext;

                            if (File.Exists(savepath))
                            {
                                File.Delete(savepath);
                            }
                            ImageConvert.IMAGEtoAnyIMAGE(item, savepath);

                            if (File.Exists(savepath))
                            {
                                count++;
                            }
                            else
                            {
                                MessageBox.Show(string.Format(Strings.ImageMultipleConversionWarning, fname, destext.Replace(".", "")), Strings.MSGWarning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        MessageBox.Show(string.Format(Strings.ImageMultipleConversionSuccess, count, destext.Replace(".", "")), Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Process.Start("EXPLORER.EXE", @"/select,""" + fbd.SelectedPath + @"""");
                        return;
                    }
                }


            }
            else
            {
                return;
            }
        }
    }
}
