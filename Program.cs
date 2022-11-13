using System;
using System.IO;
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
