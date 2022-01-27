using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    class Common
    {
        [DllImport("kernel32.dll",CharSet = CharSet.Auto)]
        private static extern uint GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]string lpszLongPath,[MarshalAs(UnmanagedType.LPTStr)]StringBuilder lpszShortPath,uint cchBuffer);
        #region Flags
        /// <summary>
        /// ダウンロード機能用変数
        /// </summary>
        public static System.Net.WebClient downloadClient = null;

        /// <summary>
        /// フラグ用変数
        /// </summary>
        public static int ProgressFlag;
        public static int UpscaleFlag;
        public static int DlcancelFlag;
        public static int DlFlag = 0;
        /// <summary>
        /// ダウンロードが開始されたかのフラグ
        /// </summary>
        public static int DlsFlag;
        /// <summary>
        /// 処理がキャンセルされたかどうかのフラグ
        /// </summary>
        public static int AbortFlag;
        public static int DeleteFlag;
        public static int VRCFlag;
        public static int ImgDetFlag;
        /// <summary>
        /// 複数画像を変換するかどうかのフラグ
        /// </summary>
        public static int ConvMultiFlag;
        /// <summary>
        /// プログレスバー下限
        /// </summary>
        public static int ProgMin;
        /// <summary>
        /// プログレスバー上限
        /// </summary>
        public static int ProgMax;
        /// <summary>
        /// ダウンロード用プログレスバー上限
        /// </summary>
        public static int DLProgMax;
        /// <summary>
        /// ダウンロード用プログレスバー進行状況
        /// </summary>
        public static int DLProgchanged;
        

        public static string DLlog, DLInfo;
        public static string DeletePath, DeletePathFrames, DeletePathFrames2x, DeletePathAudio;
        public static string FFmpegPath;
        public static string[] ImageFile = null, ImageFileName = null, ImageFileExt = null;
        public static string VideoPath = null;
        public static string SFDSavePath, FBDSavePath;
        public static string VROpenPath;
        public static string VRSavePath;
        public static string VRParam;
        /// <summary>
        /// コマンドライン引数の格納用変数
        /// </summary>
        public static string ImageParam;
        /// <summary>
        /// コマンドライン引数の格納用変数
        /// </summary>
        public static string VideoParam;
        /// <summary>
        /// コマンドライン引数の格納用変数
        /// </summary>
        public static string AudioParam;
        /// <summary>
        /// コマンドライン引数の格納用変数
        /// </summary>
        public static string MergeParam;
        /// <summary>
        /// 処理時間を計測するための変数
        /// </summary>
        public static Stopwatch stopwatch = null;
        public static TimeSpan timeSpan;
#endregion

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

        /// <summary>
        /// 画像ファイルのピクセルサイズを取得
        /// </summary>
        /// <param name="path">画像ファイルのパス</param>
        /// <returns>画像ファイルのピクセル</returns>
        public static double[] GetImageSize(string path)
        {
            double[] array = new double[1];
            using Bitmap bmp = new(path);
            array[0] = bmp.Width;
            array[1] = bmp.Height;
            if (array[0] != 0 && array[1] != 0)
            {
                return array;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 現在の時刻を取得する
        /// </summary>
        /// <returns>YYYY-MM-DD-HH-MM-SS (例：2000-01-01-00-00-00)</returns>
        public static string SFDRandomNumber()
        {
            DateTime dt = DateTime.Now;
            return dt.Year + "-" + dt.Month + "-" + dt.Day + "-" + dt.Hour + "-" + dt.Minute + "-" + dt.Second;
        }

        /// <summary>
        /// 指定したディレクトリ内のファイルも含めてディレクトリを削除する
        /// </summary>
        /// <param name="targetDirectoryPath">削除するディレクトリのパス</param>
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

        /// <summary>
        /// 指定したディレクトリ内のファイルのみを削除する
        /// </summary>
        /// <param name="targetDirectoryPath">削除するディレクトリのパス</param>
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

        public static int Gcd(int a, int b)
        {
            //a > b にする（スワップ）
            if (a < b)
            {
                int tmp = a;
                a = b;
                b = tmp;
            }

            //ユークリッドの互除法
            int r = -1;
            while (r != 0)
            {
                r = a % b;
                a = b;
                b = r;
            }

            return a;  //b には r = 0 の値が入るため、a を返す
        }

        /// <summary>
        /// 短いファイルパス名を取得する
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>短いパス名</returns>
        public static string GetShortPath(string path)
        {
            StringBuilder sb = new(1024);
            uint ret = GetShortPathName(path, sb, (uint)sb.Capacity);
            if (ret == 0)
                throw new Exception("短いファイル名の取得に失敗しました。");
            return sb.ToString();
        }

    }

    class ImageConvert
    {
        /// <summary>
        /// 画像をPNGに変換する
        /// </summary>
        /// <param name="IMAGEpath">変換する元画像のパス</param>
        /// <param name="PNGpath">変換画像の保存パス</param>
        /// <returns>真偽値 (true:変換成功, false:変換失敗)</returns>
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

        /// <summary>
        /// 画像をPNGに変換する (透過画像対応)
        /// </summary>
        /// <param name="IMAGEpath">変換する元画像のパス</param>
        /// <param name="PNGpath">変換画像の保存パス</param>
        /// <returns>真偽値 (true:変換成功, false:変換失敗)</returns>
        public static bool IMAGEtoPNG32(string IMAGEpath, string PNGpath)
        {
            if (!File.Exists(IMAGEpath))
            {
                return false;
            }
            else
            {
                using var image = new MagickImage(IMAGEpath);
                image.Write(PNGpath, MagickFormat.Png32);
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

        public static bool IMAGEtoPNG32Async(string IMAGEpath, string PNGpath)
        {
            FormImageLoading form = new(IMAGEpath, PNGpath);
            if (!File.Exists(IMAGEpath))
            {
                form.Dispose();
                return false;
            }
            else
            {
                form.ShowDialog();

                if (File.Exists(PNGpath))
                {
                    form.Dispose();
                    return true;
                }
                else
                {
                    form.Dispose();
                    return false;
                }
            }
        }

        /// <summary>
        /// 画像をアイコンファイルに変換する
        /// </summary>
        /// <param name="IMAGEpath">変換する元画像のパス</param>
        /// <param name="ICONpath">変換アイコンの保存パス</param>
        /// <returns>真偽値 (true:変換成功, false:変換失敗)</returns>
        public static bool IMAGEtoICON(string IMAGEpath, string ICONpath)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("The allowed pixels (256x256) of the icon file have been exceeded.\n\n" + ex.Message, "An error occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

    class Json
    {
        public static void SerializeJson(Dictionary<string, string> dic)
        {
            using FileStream fileStream = File.Create("settings.json");
            using StreamWriter writer = new(fileStream, Encoding.UTF8);
            var JsonStr = JsonSerializer.Serialize(dic);
            writer.Write(JsonStr);
        }

        public static void DeserializeJson(string jsonpath)
        {
            using StreamReader sr = File.OpenText(jsonpath);
            var JsonStr = JsonSerializer.Serialize(sr);
            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(JsonStr);
            foreach (var item in json)
            {

            }
        }
    }

    class Network
    {
        public static Uri GetUri(string uri)
        {
            return new Uri(uri);
        }

        public static async Task<Stream> GetWebStreamAsync(HttpClient httpClient, Uri uri)
        {
            return await httpClient.GetStreamAsync(uri);
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

namespace BitmaPlus
{
    /// <summary>
    /// Bitmap処理を高速化するためのクラス
    /// </summary>
    class BitmapPlus
    {
        /// <summary>
        /// オリジナルのBitmapオブジェクト
        /// </summary>
        private Bitmap _bmp = null;

        /// <summary>
        /// Bitmapに直接アクセスするためのオブジェクト
        /// </summary>
        private BitmapData _img = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public BitmapPlus(Bitmap original)
        {
            // オリジナルのBitmapオブジェクトを保存
            _bmp = original;
        }

        /// <summary>
        /// Bitmap処理の高速化開始
        /// </summary>
        public void BeginAccess()
        {
            // Bitmapに直接アクセスするためのオブジェクト取得(LockBits)
            _img = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Bitmap処理の高速化終了
        /// </summary>
        public void EndAccess()
        {
            if (_img != null)
            {
                // Bitmapに直接アクセスするためのオブジェクト開放(UnlockBits)
                _bmp.UnlockBits(_img);
                _img = null;
            }
        }

        /// <summary>
        /// BitmapのGetPixel同等
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <returns>Colorオブジェクト</returns>
        public Color GetPixel(int x, int y)
        {
            if (_img == null)
            {
                // Bitmap処理の高速化を開始していない場合はBitmap標準のGetPixel
                return _bmp.GetPixel(x, y);
            }

            // Bitmap処理の高速化を開始している場合はBitmapメモリへの直接アクセス
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte b = Marshal.ReadByte(adr, pos + 0);
            byte g = Marshal.ReadByte(adr, pos + 1);
            byte r = Marshal.ReadByte(adr, pos + 2);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// BitmapのSetPixel同等
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <param name="col">Colorオブジェクト</param>
        public void SetPixel(int x, int y, Color col)
        {
            if (_img == null)
            {
                // Bitmap処理の高速化を開始していない場合はBitmap標準のSetPixel
                _bmp.SetPixel(x, y, col);
                return;
            }

            // Bitmap処理の高速化を開始している場合はBitmapメモリへの直接アクセス
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            Marshal.WriteByte(adr, pos + 0, col.B);
            Marshal.WriteByte(adr, pos + 1, col.G);
            Marshal.WriteByte(adr, pos + 2, col.R);
        }
    }
}
