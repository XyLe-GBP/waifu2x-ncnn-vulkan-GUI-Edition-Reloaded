using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace NVGE
{
    /// <summary>
    /// FormSplashWPF.xaml の相互作用ロジック
    /// </summary>
    public partial class FormSplashWPF : Window
    {
        #region NetworkCommon
        private static readonly HttpClientHandler handler = new()
        {
            UseProxy = false,
            UseCookies = false
        };
        private static readonly HttpClient stream = new(handler);
        #endregion
        public FormSplashWPF()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Config.Load(Common.xmlpath);

            switch (bool.Parse(Config.Entry["CustomSplashImage"].Value))
            {
                case true:
                    {
                        Bitmap bimg = new(Properties.Resources.waifu2x_splash);
                        Bitmap cimg = new(Config.Entry["SplashImagePath"].Value);
                        Graphics g = Graphics.FromImage(cimg);
                        g.DrawImage(bimg, 0, 0, bimg.Width, bimg.Height);

                        image.Source = BIMG.ToBitmapImage(cimg);
                        break;
                    }
                case false:
                    {
                        if (NetworkInterface.GetIsNetworkAvailable())
                        {
                            string url = "https://github.com/XyLe-GBP/waifu2x-ncnn-vulkan-GUI-Edition-Reloaded/raw/master/Properties/waifu2x-splash.png";
                            Task<Stream> st = stream.GetStreamAsync(url);
                            Bitmap bitmap = new(st.Result);

                            image.Source = BIMG.ToBitmapImage(bitmap);
                        }
                        else
                        {
                            image.Source = BIMG.ToBitmapImage(Properties.Resources.waifu2x_splash);
                        }
                        break;
                    }
            }
        }

        public string ProgressMsg
        {
            set
            {
                TextBlock_Log.Text = value;
            }
        }
    }

    internal static class BIMG
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }

    
}
