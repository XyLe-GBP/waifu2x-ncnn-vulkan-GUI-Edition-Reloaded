using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    class Common
    {
        public static System.Net.WebClient downloadClient = null;
        public static int ProgressFlag, UpscaleFlag, ProgMin, ProgMax, DLProgMax, DLProgchanged, DlsFlag, DlFlag = 0, AbortFlag, DeleteFlag, VRCFlag;
        public static string DLlog, DLInfo;
        public static string DeletePath, DeletePathFrames, DeletePathFrames2x, DeletePathAudio;
        public static string FFmpegPath;
        public static string[] ImageFile = null;
        public static string VideoPath = null;
        public static string SFDSavePath, FBDSavePath;
        public static string VROpenPath;
        public static string VRSavePath;
        public static string VRParam;
        public static string ImageParam;
        public static string VideoParam;
        public static string AudioParam;
        public static string MergeParam;

        public static string CheckVideoAudioCodec(string VideoPath)
        {
            ProcessStartInfo pi = new();
            Process ps;
            pi.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
            pi.Arguments = "/c " + FFmpegPath + " -i " + VideoPath;
            pi.CreateNoWindow = true;
            pi.UseShellExecute = false;
            pi.RedirectStandardOutput = true;
            pi.RedirectStandardInput = false;
            ps = Process.Start(pi);

            string fb = ps.StandardOutput.ReadToEnd();

            ps.WaitForExit();

            return fb.Substring(fb.IndexOf("Audio: ") + 7, 10);
        }

        /// <summary>
        /// Process.Start: Open URI for .NET
        /// </summary>
        /// <param name="URI">http://~ または https://~ から始まるウェブサイトのURL</param>
        public static void OpenURI(string URI)
        {
            try
            {
                Process.Start(URI);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    //Windowsのとき  
                    URI = URI.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {URI}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    //Linuxのとき  
                    Process.Start("xdg-open", URI);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    //Macのとき  
                    Process.Start("open", URI);
                }
                else
                {
                    throw;
                }
            }

            return;
        }

        public static async Task<string> GetWebPageAsync(Uri uri)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");
            client.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");
            client.Timeout = TimeSpan.FromSeconds(10.0);

            try
            {
                return await client.GetStringAsync(uri);
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("An exception occured.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Exception ex = e;
                while (ex != null)
                {
                    MessageBox.Show(string.Format("log: {0} ", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = ex.InnerException;
                }
            }
            catch (TaskCanceledException e)
            {
                MessageBox.Show("Connection timed out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(string.Format("log: {0} ", e.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public static string SFDRandomNumber()
        {
            DateTime dt = DateTime.Now;
            return dt.Year + "-" + dt.Month + "-" + dt.Day + "-" + dt.Hour + "-" + dt.Minute + "-" + dt.Second;
        }

        public static void DeleteDirectory(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            string[] filePaths = Directory.GetFiles(targetDirectoryPath);
            foreach (string filePath in filePaths)
            {
                File.SetAttributes(filePath, FileAttributes.Normal);
                File.Delete(filePath);
            }

            string[] directoryPaths = Directory.GetDirectories(targetDirectoryPath);
            foreach (string directoryPath in directoryPaths)
            {
                DeleteDirectory(directoryPath);
            }

            Directory.Delete(targetDirectoryPath, false);
        }

        public static void DeleteDirectoryFiles(string targetDirectoryPath)
        {
            if (!Directory.Exists(targetDirectoryPath))
            {
                return;
            }

            DirectoryInfo di = new(targetDirectoryPath);
            FileInfo[] fi = di.GetFiles();
            foreach (var file in fi)
            {
                file.Delete();
            }
            return;
        }
    }

    class ImageConvert
    {
        public static bool IMAGEtoPNG(string IMAGEpath, string PNGpath)
        {
            if (!File.Exists(IMAGEpath))
            {
                return false;
            }
            else
            {
                using var image = new MagickImage(IMAGEpath);
                image.Write(PNGpath, MagickFormat.Png);
                if (File.Exists(PNGpath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool IMAGEtoICON(string IMAGEpath, string ICONpath)
        {
            if (!File.Exists(IMAGEpath))
            {
                return false;
            }
            else
            {
                using var image = new MagickImage(IMAGEpath);
                image.Write(ICONpath, MagickFormat.Ico);
                if (File.Exists(ICONpath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    class SystemInfo
    {
        public static void GetSystemInformation(string[] buffers)
        {
            if (buffers.Length != 17)
            {
                return;
            }
            else
            {
                var mc = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    buffers[0] = string.Format("{0}", mo["Name"]);
                    buffers[1] = string.Format("{0}", mo["Caption"]);
                    buffers[2] = string.Format("{0}", mo["Description"]);
                    buffers[3] = string.Format("{0}", mo["Version"]);
                    buffers[4] = string.Format("{0}", mo["BuildNumber"]);
                    buffers[5] = string.Format("{0}", mo["Manufacturer"]);
                    buffers[6] = string.Format("{0}", mo["Locale"]);
                    buffers[7] = string.Format("{0}", mo["OSLanguage"]);
                    buffers[8] = string.Format("{0}", mo["SerialNumber"]);
                    buffers[9] = string.Format("{0}", mo["InstallDate"]);
                    buffers[10] = string.Format("{0}", mo["LastBootUpTime"]);
                    buffers[11] = string.Format("{0}", mo["WindowsDirectory"]);
                    buffers[12] = string.Format("{0}", mo["SystemDevice"]);
                    buffers[13] = string.Format("{0}", mo["SystemDrive"]);
                    buffers[14] = string.Format("{0}", mo["BootDevice"]);
                    buffers[15] = string.Format("{0}", mo["PlusProductID"]);
                    buffers[16] = string.Format("{0}", mo["PlusVersionNumber"]);
                }
            }
        }

        public static void GetProcessorsInformation(string[] buffers)
        {
            if (buffers.Length != 3)
            {
                return;
            }
            else
            {
                var mc = new ManagementClass("Win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    buffers[0] = string.Format("{0}", mo["Name"]);
                    buffers[1] = string.Format("{0}", mo["NumberOfEnabledCore"]);
                    buffers[2] = string.Format("{0}", mo["NumberOfLogicalProcessors"]);
                }
            }
        }

        public static void GetVideoControllerInformation(string[] buffers)
        {
            if (buffers.Length != 3)
            {
                return;
            }
            else
            {
                var mc = new ManagementClass("Win32_VideoController");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    buffers[0] = string.Format("{0}", mo["Name"]);
                    buffers[1] = string.Format("{0}", mo["DriverVersion"]);
                    buffers[2] = string.Format("{0}", mo["AdapterRAM"]);
                }
            }
        }
    }
}

namespace PrivateProfile
{
    /// <summary>
    /// Ini ファイルの読み書きを扱うクラスです。
    /// </summary>
    public class IniFile
    {
        [DllImport("kernel32.dll")]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        /// <summary>
        /// Ini ファイルのファイルパスを取得、設定します。
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="filePath">Ini ファイルのファイルパス</param>
        public IniFile(string filePath)
        {
            FilePath = filePath;
        }
        /// <summary>
        /// Ini ファイルから文字列を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public string GetString(string section, string key, string defaultValue = "")
        {
            var sb = new StringBuilder(1024);
            var r = GetPrivateProfileString(section, key, defaultValue, sb, (uint)sb.Capacity, FilePath);
            return sb.ToString();
        }
        /// <summary>
        /// Ini ファイルから整数を取得します。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="defaultValue">値が取得できない場合の初期値</param>
        /// <returns></returns>
        public int GetInt(string section, string key, int defaultValue = 0)
        {
            return (int)GetPrivateProfileInt(section, key, defaultValue, FilePath);
        }
        /// <summary>
        /// Ini ファイルに文字列を書き込みます。
        /// </summary>
        /// <param name="section">セクション名</param>
        /// <param name="key">項目名</param>
        /// <param name="value">書き込む値</param>
        /// <returns></returns>
        public bool WriteString(string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, FilePath);
        }
    }
}