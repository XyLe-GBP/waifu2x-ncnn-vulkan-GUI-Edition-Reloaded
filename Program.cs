using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string mutexName = "waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp";
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
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\waifu2x-ncnn-vulkan.exe"))
                {
                    MessageBox.Show("The required file 'waifu2x-ncnn-vulkan.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\res\image2png.exe"))
                {
                    MessageBox.Show("The required file 'image2png.exe' does not exist.\nClose the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
