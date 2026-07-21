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

                Common.LoadConfigOrReset();
                EnginePackageManager.EnsureEngineDirectories();
                switch (Common.GetConfigInt("ApplicationLanguage", 0))
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
