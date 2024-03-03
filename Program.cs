using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace NVGE
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string mutexName = "NVGE";
            System.Threading.Mutex mutex = new(false, mutexName);

            bool hasHandle = false;
            try
            {
                if (!File.Exists(Common.xmlpath))
                {
                    Common.InitConfig();
                }

                try
                {
                    hasHandle = mutex.WaitOne(0, false);
                }
                catch (System.Threading.AbandonedMutexException)
                {
                    hasHandle = true;
                }

                if (hasHandle == false)
                {
                    MessageBox.Show("Multiple launch of applications is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\engines\waifu2x\waifu2x-ncnn-vulkan.exe"))
                {
                    MessageBox.Show("The required file 'waifu2x-ncnn-vulkan.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\engines\realcugan\realcugan-ncnn-vulkan.exe"))
                {
                    MessageBox.Show("The required file 'realcugan-ncnn-vulkan.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\engines\realesrgan\realesrgan-ncnn-vulkan.exe"))
                {
                    MessageBox.Show("The required file 'realesrgan-ncnn-vulkan.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\updater.exe"))
                {
                    MessageBox.Show("The required file 'updater.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Config.Load(Common.xmlpath);
                switch (int.Parse(Config.Entry["ApplicationLanguage"].Value))
                {
                    case 0:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
                        break;
                    case 1:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja");
                        break;
                    case 2:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh");
                        break;
                    default:
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo("");
                        break;
                }

                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            finally
            {
                if (hasHandle)
                {
                    mutex.ReleaseMutex();
                }
                mutex.Close();
            }
        }
    }
}
