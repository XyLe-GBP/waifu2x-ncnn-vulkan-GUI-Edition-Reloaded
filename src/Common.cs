using ImageMagick;
using NVGE.Localization;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace NVGE
{
    class Common
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetShortPathName([MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath, uint cchBuffer);
        #region Flags
        public static string xmlpath = Directory.GetCurrentDirectory() + @"\app.config";

        /// <summary>
        /// ダウンロード機能用変数
        /// </summary>
        public static System.Net.WebClient downloadClient = null;

        /// <summary>
        /// フラグ用変数
        /// </summary>
        public static int ProgressFlag = 0;
        public static int UpscaleFlag = 0;
        public static int DlcancelFlag = 0;
        public static int DlFlag = 0;
        /// <summary>
        /// GIFアニメーションかどうかのフラグ
        /// </summary>
        public static bool GIFflag = false;
        /// <summary>
        /// ダウンロードが開始されたかのフラグ
        /// </summary>
        public static int DlsFlag = 0;
        /// <summary>
        /// 処理がキャンセルされたかどうかのフラグ
        /// </summary>
        public static int AbortFlag = 0;
        public static int DeleteFlag = 0;
        public static int VRCFlag = 0;
        public static int ImgDetFlag = 0;
        /// <summary>
        /// 複数画像を変換するかどうかのフラグ
        /// </summary>
        public static int ConvMultiFlag;
        /// <summary>
        /// プログレスバー下限
        /// </summary>
        public static int ProgMin = 0;
        /// <summary>
        /// プログレスバー上限
        /// </summary>
        public static int ProgMax = 0;
        /// <summary>
        /// ダウンロード用プログレスバー上限
        /// </summary>
        public static int DLProgMax = 0;
        /// <summary>
        /// ダウンロード用プログレスバー進行状況
        /// </summary>
        public static int DLProgchanged = 0;
        /// <summary>
        /// カスタムを選んだ際の形式
        /// </summary>
        public static string ManualImageFormat;
        public static string ManualImageFormatFilter;

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
            pi.FileName = Environment.GetEnvironmentVariable("ComSpec");
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

        public static string GetAssemblyDateTimeVersion()
        {
            var obj = new object();
            var asm = obj.GetType().Assembly;
            var ver = asm.GetName().Version;
            var build = ver.Build;
            var revision = ver.Revision;
            var baseDate = new DateTime(2000, 1, 1);

            return baseDate.AddDays(build).AddSeconds(revision * 2).ToString();
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

        /// <summary>
        /// 大文字小文字を区別しないで置き換え
        /// </summary>
        /// <param name="inStr">置き換える対象の文字列</param>
        /// <param name="oldStr">検索文字列</param>
        /// <param name="newStr">置き換える文字列</param>
        /// <returns></returns>
        public static string ReplaceForRegex(string inStr, string oldStr, string newStr)
        {
            string dstStr = Regex.Replace(inStr, oldStr, newStr, RegexOptions.IgnoreCase);
            return dstStr;
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

        /// <summary>
        /// 動画のFPSを取得
        /// </summary>
        /// <param name="path">動画ファイルのパス</param>
        public static void GetVideoFps(string path)
        {
            using var video = new VideoCapture(path);
            switch (video.Fps.ToString())
            {
                case "30":
                    Config.Entry["FPS"].Value = video.Fps.ToString().Replace("30", "30.00");
                    Config.Save(xmlpath);
                    break;
                case "60":
                    Config.Entry["FPS"].Value = video.Fps.ToString().Replace("60", "60.00");
                    Config.Save(xmlpath);
                    break;
                default:
                    if (video.Fps.ToString().Length > 5)
                    {
                        Config.Entry["FPS"].Value = video.Fps.ToString()[..5];
                        Config.Save(xmlpath);
                    }
                    else
                    {
                        Config.Entry["FPS"].Value = video.Fps.ToString();
                        Config.Save(xmlpath);
                    }
                    break;
            }
        }

        /// <summary>
        /// 設定ファイルに全てを書き出す
        /// </summary>
        public static void InitConfig()
        {
            if (Config.Entry["FFmpegLocation"].Value == null)
            {
                Config.Entry["FFmpegLocation"].Value = "";
            }
            if (Config.Entry["Param"].Value == null)
            {
                Config.Entry["Param"].Value = "";
            }
            if (Config.Entry["VideoParam"].Value == null)
            {
                Config.Entry["VideoParam"].Value = "";
            }
            if (Config.Entry["AudioParam"].Value == null)
            {
                Config.Entry["AudioParam"].Value = "";
            }
            if (Config.Entry["MergeParam"].Value == null)
            {
                Config.Entry["MergeParam"].Value = "";
            }
            if (Config.Entry["VideoLocation"].Value == null)
            {
                Config.Entry["VideoLocation"].Value = "";
            }
            if (Config.Entry["AudioLocation"].Value == null)
            {
                Config.Entry["AudioLocation"].Value = "";
            }
            if (Config.Entry["Reduction"].Value == null)
            {
                Config.Entry["Reduction"].Value = "-1";
            }
            if (Config.Entry["Scale"].Value == null)
            {
                Config.Entry["Scale"].Value = "-1";
            }
            if (Config.Entry["GPU"].Value == null)
            {
                Config.Entry["GPU"].Value = "-1";
            }
            if (Config.Entry["Blocksize"].Value == null)
            {
                Config.Entry["Blocksize"].Value = "0";
            }
            if (Config.Entry["IAdvanced"].Value == null)
            {
                Config.Entry["IAdvanced"].Value = "false";
            }
            if (Config.Entry["Thread"].Value == null)
            {
                Config.Entry["Thread"].Value = "-1";
            }
            if (Config.Entry["Format"].Value == null)
            {
                Config.Entry["Format"].Value = "-1";
            }
            if (Config.Entry["Model"].Value == null)
            {
                Config.Entry["Model"].Value = "-1";
            }
            if (Config.Entry["Verbose"].Value == null)
            {
                Config.Entry["Verbose"].Value = "false";
            }
            if (Config.Entry["TTA"].Value == null)
            {
                Config.Entry["TTA"].Value = "false";
            }
            if (Config.Entry["Pixel"].Value == null)
            {
                Config.Entry["Pixel"].Value = "false";
            }
            if (Config.Entry["Pixel_width"].Value == null)
            {
                Config.Entry["Pixel_width"].Value = "-1";
            }
            if (Config.Entry["Pixel_height"].Value == null)
            {
                Config.Entry["Pixel_height"].Value = "-1";
            }
            if (Config.Entry["Param"].Value == null)
            {
                Config.Entry["Param"].Value = "";
            }
            if (Config.Entry["FPS"].Value == null)
            {
                Config.Entry["FPS"].Value = "";
            }
            if (Config.Entry["FFmpegLocation"].Value == null)
            {
                Config.Entry["FFmpegLocation"].Value = "";
            }
            if (Config.Entry["VAdvanced"].Value == null)
            {
                Config.Entry["VAdvanced"].Value = "false";
            }
            if (Config.Entry["InternalAAC"].Value == null)
            {
                Config.Entry["InternalAAC"].Value = "false";
            }
            if (Config.Entry["Subtitle"].Value == null)
            {
                Config.Entry["Subtitle"].Value = "false";
            }
            if (Config.Entry["CopyingChapters"].Value == null)
            {
                Config.Entry["CopyingChapters"].Value = "false";
            }
            if (Config.Entry["OutputAudioOnly"].Value == null)
            {
                Config.Entry["OutputAudioOnly"].Value = "false";
            }
            if (Config.Entry["HideInformations"].Value == null)
            {
                Config.Entry["HideInformations"].Value = "false";
            }
            if (Config.Entry["Sequential"].Value == null)
            {
                Config.Entry["Sequential"].Value = "false";
            }
            if (Config.Entry["DataStream"].Value == null)
            {
                Config.Entry["DataStream"].Value = "false";
            }
            if (Config.Entry["Metadata"].Value == null)
            {
                Config.Entry["Metadata"].Value = "false";
            }
            if (Config.Entry["Overwrite"].Value == null)
            {
                Config.Entry["Overwrite"].Value = "false";
            }
            if (Config.Entry["NVENC"].Value == null)
            {
                Config.Entry["NVENC"].Value = "false";
            }
            if (Config.Entry["CRFLevel"].Value == null)
            {
                Config.Entry["CRFLevel"].Value = "";
            }
            if (Config.Entry["VideoCodec"].Value == null)
            {
                Config.Entry["VideoCodec"].Value = "false";
            }
            if (Config.Entry["VideoCodecIndex"].Value == null)
            {
                Config.Entry["VideoCodecIndex"].Value = "";
            }
            if (Config.Entry["AudioCodec"].Value == null)
            {
                Config.Entry["AudioCodec"].Value = "false";
            }
            if (Config.Entry["AudioCodecIndex"].Value == null)
            {
                Config.Entry["AudioCodecIndex"].Value = "";
            }
            if (Config.Entry["AudioBitrate"].Value == null)
            {
                Config.Entry["AudioBitrate"].Value = "false";
            }
            if (Config.Entry["AudioBitrateIndex"].Value == null)
            {
                Config.Entry["AudioBitrateIndex"].Value = "";
            }
            if (Config.Entry["AudioOutputCodec"].Value == null)
            {
                Config.Entry["AudioOutputCodec"].Value = "false";
            }
            if (Config.Entry["OutputCodecIndex"].Value == null)
            {
                Config.Entry["OutputCodecIndex"].Value = "-1";
            }
            if (Config.Entry["H264"].Value == null)
            {
                Config.Entry["H264"].Value = "false";
            }
            if (Config.Entry["H264Index"].Value == null)
            {
                Config.Entry["H264Index"].Value = "-1";
            }
            if (Config.Entry["OutputLocation"].Value == null)
            {
                Config.Entry["OutputLocation"].Value = "false";
            }
            if (Config.Entry["VideoLocation"].Value == null)
            {
                Config.Entry["VideoLocation"].Value = "";
            }
            if (Config.Entry["AudioLocation"].Value == null)
            {
                Config.Entry["AudioLocation"].Value = "";
            }
            if (Config.Entry["VideoGeneration"].Value == null)
            {
                Config.Entry["VideoGeneration"].Value = "false";
            }
            if (Config.Entry["GenerationIndex"].Value == null)
            {
                Config.Entry["GenerationIndex"].Value = "-1";
            }
            if (Config.Entry["VideoParam"].Value == null)
            {
                Config.Entry["VideoParam"].Value = "";
            }
            if (Config.Entry["AudioParam"].Value == null)
            {
                Config.Entry["AudioParam"].Value = "";
            }
            if (Config.Entry["MergeParam"].Value == null)
            {
                Config.Entry["MergeParam"].Value = "";
            }
            if (Config.Entry["FFmpegVersion"].Value == null)
            {
                Config.Entry["FFmpegVersion"].Value = "";
            }
            Config.Save(xmlpath);
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
                    if (image.Width > 256 || image.Height > 256)
                    {
                        image.Resize(256, 256);
                    }

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
                MessageBox.Show("An error occured.\n\n" + ex.Message, "An error occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 画像をリサイズする
        /// </summary>
        /// <param name="IMAGEpath">画像のパス</param>
        /// <param name="width">横ピクセル</param>
        /// <param name="height">縦ピクセル</param>
        /// <param name="fmt">出力形式</param>
        /// <returns></returns>
        public static bool IMAGEResize(string IMAGEpath, int width, int height, MagickFormat fmt = MagickFormat.Png32)
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

                    image.Resize(width, height);
                    image.Write(IMAGEpath, fmt);
                    if (File.Exists(IMAGEpath))
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
                MessageBox.Show("An error occured.\n\n" + ex.Message, "An error occured.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static MagickFormat GetFormat(string IMAGEpath)
        {
            try
            {
                if (!File.Exists(IMAGEpath))
                {
                    return MagickFormat.Unknown;
                }
                else
                {
                    FileInfo fi = new(IMAGEpath);
                    MagickFormat fmt = new();
                    switch (fi.Extension.ToUpper())
                    {
                        case ".BMP":
                            fmt = MagickFormat.Bmp3;
                            break;
                        case ".DIB":
                            fmt = MagickFormat.Dib;
                            break;
                        case ".EPS":
                            fmt = MagickFormat.Eps;
                            break;
                        case ".WEBP":
                            fmt = MagickFormat.WebP;
                            break;
                        case ".GIF":
                            fmt = MagickFormat.Gif;
                            break;
                        case ".ICO":
                            fmt = MagickFormat.Ico;
                            break;
                        case ".ICNS":
                            fmt = MagickFormat.Icon;
                            break;
                        case ".JFIF":
                            fmt = MagickFormat.Jpg;
                            break;
                        case ".JPG":
                            fmt = MagickFormat.Jpg;
                            break;
                        case ".JPE":
                            fmt = MagickFormat.Jpe;
                            break;
                        case ".JPEG":
                            fmt = MagickFormat.Jpeg;
                            break;
                        case ".PJPEG":
                            fmt = MagickFormat.Pjpeg;
                            break;
                        case ".PJP":
                            fmt = MagickFormat.Pjpeg;
                            break;
                        case ".PNG":
                            fmt = MagickFormat.Png32;
                            break;
                        case ".PICT":
                            fmt = MagickFormat.Pict;
                            break;
                        case ".SVG":
                            fmt = MagickFormat.Svg;
                            break;
                        case ".SVGZ":
                            fmt = MagickFormat.Svgz;
                            break;
                        case ".TIF":
                            fmt = MagickFormat.Tif;
                            break;
                        case ".TIFF":
                            fmt = MagickFormat.Tiff;
                            break;
                        default:
                            break;
                    }
                    return fmt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return MagickFormat.Unknown;
            }
        }

        public static bool IMAGEtoAnyIMAGE(string IMAGEpath, string IMAGEpath2)
        {
            try
            {
                if (!File.Exists(IMAGEpath))
                {
                    return false;
                }
                else
                {
                    FileInfo fi = new(IMAGEpath2);
                    MagickFormat fmt = new();
                    switch (fi.Extension.ToUpper())
                    {
                        case ".BMP":
                            fmt = MagickFormat.Bmp3;
                            break;
                        case ".DIB":
                            fmt = MagickFormat.Dib;
                            break;
                        case ".EPS":
                            fmt = MagickFormat.Eps;
                            break;
                        case ".WEBP":
                            fmt = MagickFormat.WebP;
                            break;
                        case ".GIF":
                            fmt = MagickFormat.Gif;
                            break;
                        case ".ICO":
                            fmt = MagickFormat.Ico;
                            break;
                        case ".ICNS":
                            fmt = MagickFormat.Icon;
                            break;
                        case ".JFIF":
                            fmt = MagickFormat.Jpg;
                            break;
                        case ".JPG":
                            fmt = MagickFormat.Jpg;
                            break;
                        case ".JPE":
                            fmt = MagickFormat.Jpe;
                            break;
                        case ".JPEG":
                            fmt = MagickFormat.Jpeg;
                            break;
                        case ".PJPEG":
                            fmt = MagickFormat.Pjpeg;
                            break;
                        case ".PJP":
                            fmt = MagickFormat.Pjpeg;
                            break;
                        case ".PNG":
                            fmt = MagickFormat.Png32;
                            break;
                        case ".PICT":
                            fmt = MagickFormat.Pict;
                            break;
                        case ".SVG":
                            fmt = MagickFormat.Svg;
                            break;
                        case ".SVGZ":
                            fmt = MagickFormat.Svgz;
                            break;
                        case ".TIF":
                            fmt = MagickFormat.Tif;
                            break;
                        case ".TIFF":
                            fmt = MagickFormat.Tiff;
                            break;
                        default:
                            break;
                    }
                    using var image = new MagickImage(IMAGEpath);
                    image.Write(IMAGEpath2, fmt);
                    if (File.Exists(IMAGEpath2))
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    class Config
    {
        /// <summary>
        /// ルートエントリ
        /// </summary>
        public static ConfigEntry Entry = new() { Key = "ConfigRoot" };
        public static void Load(string filename)
        {
            if (!File.Exists(filename))
                return;
            var xmlSerializer = new XmlSerializer(typeof(ConfigEntry));
            using var streamReader = new StreamReader(filename, Encoding.UTF8);
            using var xmlReader = XmlReader.Create(streamReader, new XmlReaderSettings() { CheckCharacters = false });
            Entry = (ConfigEntry)xmlSerializer.Deserialize(xmlReader); // （3）
        }
        public static void Save(string filename)
        {
            var serializer = new XmlSerializer(typeof(ConfigEntry));
            using var streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            serializer.Serialize(streamWriter, Entry);
        }
    }

    /// <summary>
    /// ConfigEntryクラス。設定の1レコード
    /// </summary>
    public class ConfigEntry
    {
        /// <summary>
        /// 設定レコードののキー
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 設定レコードの値
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 子アイテム
        /// </summary>
        public List<ConfigEntry> Children { get; set; }
        /// <summary>
        /// キーを指定して子アイテムからConfigEntryを取得します。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ConfigEntry Get(string key)
        {
            var entry = Children?.FirstOrDefault(rec => rec.Key == key);
            if (entry == null)
            {
                if (Children == null)
                    Children = new List<ConfigEntry>();
                entry = new ConfigEntry() { Key = key };
                Children.Add(entry);
            }
            return entry;
        }
        /// <summary>
        /// 子アイテムにConfigEntryを追加します。
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="o">設定値</param>
        public void Add(string key, string o)
        {
            ConfigEntry entry = Children?.FirstOrDefault(rec => rec.Key == key);
            if (entry != null)
                entry.Value = o;
            else
            {
                if (Children == null)
                    Children = new List<ConfigEntry>();
                entry = new ConfigEntry() { Key = key, Value = o };
                Children.Add(entry);
            }
        }
        /// <summary>
        /// 子アイテムからConfigEntryを取得します。存在しなければ新しいConfigEntryが作成されます。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public ConfigEntry this[string key]
        {
            set => Add(key, null);
            get => Get(key);
        }
        /// <summary>
        /// 子アイテムからConfigEntryを取得します。存在しなければ新しいConfigEntryが作成されます。
        /// </summary>
        /// <param name="keys">キー、カンマで区切って階層指定します</param>
        /// <returns></returns>
        public ConfigEntry this[params string[] keys]
        {
            set
            {
                ConfigEntry entry = this;
                for (int i = 0; i < keys.Length; i++)
                {
                    entry = entry[keys[i]];
                }
            }
            get
            {
                ConfigEntry entry = this;
                for (int i = 0; i < keys.Length; i++)
                {
                    entry = entry[keys[i]];
                }
                return entry;
            }
        }

        /// <summary>
        /// 指定したキーが子アイテムに存在するか調べます。再帰的調査はされません。
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>キーが存在すればTrue</returns>
        public bool Exists(string key) => Children?.Any(c => c.Key == key) ?? false;
        /// <summary>
        /// 指定したキーが子アイテムに存在するか調べます。階層をまたいだ指定をします。
        /// </summary>
        /// <param name="keys">キー、カンマで区切って階層指定します。</param>
        /// <returns>キーが存在すればTrue</returns>
        public bool Exists(params string[] keys)
        {
            ConfigEntry entry = this;
            for (int i = 0; i < keys.Length; i++)
            {
                if (entry.Exists(keys[i]) == false)
                    return false;
                entry = entry[keys[i]];
            }
            return true;
        }
    }

    /// <summary>
    /// ネットワーク系関数
    /// </summary>
    class Network
    {
        /// <summary>
        /// 文字列をURIに変換
        /// </summary>
        /// <param name="uri">URI文字列</param>
        /// <returns></returns>
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

namespace BitmapUtils
{
    /// <summary>
    /// Bitmap処理を高速化するためのクラス
    /// </summary>
    class BitmapPlus
    {
        /// <summary>
        /// オリジナルのBitmapオブジェクト
        /// </summary>
        private readonly Bitmap _bmp = null;

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
