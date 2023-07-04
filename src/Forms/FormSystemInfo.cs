using NVGE.Properties;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vortice.DXGI;

namespace NVGE
{
    public partial class FormSystemInfo : Form
    {
        public FormSystemInfo()
        {
            InitializeComponent();
        }

        private void FormSystemInfo_Load(object sender, EventArgs e)
        {
            int adaptercount = 0;
            string[] OSInfo = new string[17];
            string[] CPUInfo = new string[3];
            string[] GPUInfo = new string[3];
            string[] BIOSInfo = new string[5];
            string[] BaseInfo= new string[3];
            SystemInfo.GetSystemInformation(OSInfo);
            SystemInfo.GetProcessorsInformation(CPUInfo);
            SystemInfo.GetVideoControllerInformation(GPUInfo);
            SystemInfo.GetBIOSInformation(BIOSInfo);
            SystemInfo.GetBaseBoardInformation(BaseInfo);

            if (DXGI.CreateDXGIFactory1(out IDXGIFactory1 factory).Failure)
            {
                return;
            }

            using (factory)
            {
                for (int iAdapter = 0; ; iAdapter++)
                {
                    if (factory.EnumAdapters(iAdapter, out IDXGIAdapter adapter).Failure)
                    {
                        break;
                    }

                    using (adapter)
                    {
                        if (adapter.Description.DedicatedVideoMemory == 0)
                        {
                            continue;
                        }
                        adaptercount++;

                        Debug.WriteLine($"[adapter {iAdapter}]");
                        Debug.WriteLine($"Description:{adapter.Description.Description}");
                        comboBox_GPU.Items.Add(adapter.Description.Description);
                        Debug.WriteLine($"DedicatedVideoMemory:{adapter.Description.DedicatedVideoMemory / 1024 / 1024} MB");
                        label_GPUMemory.Text = adapter.Description.DedicatedVideoMemory / 1024 / 1024 + "MB";
                    }
                }
            }

            if (adaptercount > 1)
            {
                comboBox_GPU.Enabled = true;
            }
            else
            {
                comboBox_GPU.Enabled = false;
            }

            /*List<string> GPUList = new();
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

            foreach (var gpu in GPUList)
            {
                comboBox_GPU.Items.Add(gpu);
            }

            comboBox_GPU.SelectedIndex = 0;
            if (Common.GPUList.Count == 1)
            {
                comboBox_GPU.Enabled = false;
            }
            else
            {
                comboBox_GPU.Enabled = true;
            }*/

            CPUVendor(CPUInfo[0]);
            label_CPUName.Text = CPUInfo[0];//CPUInfo[0];
            label_CPUCores.Text = CPUInfo[1];
            label_CPUThreads.Text = CPUInfo[2];
            label_OSName.Text = OSInfo[1] + " - " + OSInfo[3];// + " [ build: " + OSInfo[4] + " ]";
            label_MBMaker.Text = BaseInfo[0];
            label_MBModel.Text = BaseInfo[2];
            label_GPUName.Text = GPUInfo[0];
            label_GPUMaker.Text = GPUVendor(GPUInfo[0]);
            label_GPUDriver.Text = GPUInfo[1];
            comboBox_GPU.SelectedIndex = 0;
        }

        private void CPUVendor(string Name)
        {
            if (Name.Contains("Intel"))
            {
                if (Name.Contains("Cerelon"))
                {
                    if (Regex.IsMatch(Name, "G[6]...")) // 12 [Alder Lake]
                    {
                        pictureBox_CPU.Image = Resources.celeron_12th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[5]...")) // 10 [Comet Lake]
                    {
                        pictureBox_CPU.Image = Resources.celeron_6_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[4]...")) // 9 [Coffee Lake]
                    {
                        pictureBox_CPU.Image = Resources.celeron_6_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[3]...")) // 6 [Skylake]
                    {
                        pictureBox_CPU.Image = Resources.celeron_6_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G18..")) // 4 [Haswell]
                    {
                        pictureBox_CPU.Image = Resources.celeron_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G16..")) // 3 [Ivy Bridge]
                    {
                        pictureBox_CPU.Image = Resources.celeron_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G5..")) // 2 [Sandy Bridge]
                    {
                        pictureBox_CPU.Image = Resources.celeron_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G4..")) // 2 [Sandy Bridge]
                    {
                        pictureBox_CPU.Image = Resources.celeron_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G11..")) // 1 [Nehalem]
                    {
                        pictureBox_CPU.Image = Resources.celeron_1st_logo;
                    }
                }
                else if(Name.Contains("Pentium"))
                {
                    if (Regex.IsMatch(Name, "G[6]...")) // 1 [Clarkdale]
                    {
                        pictureBox_CPU.Image = Resources.pentium_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[5]...")) // 9 [Coffee Lake]
                    {
                        pictureBox_CPU.Image = Resources.pentium_6_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[4]...")) // 6,7
                    {
                        pictureBox_CPU.Image = Resources.pentium_6_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[3]...")) // 4 [Haswell]
                    {
                        pictureBox_CPU.Image = Resources.pentium_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[2]...")) // 3 [Ivy Bridge]
                    {
                        pictureBox_CPU.Image = Resources.pentium_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G[1]...")) // 3 [Ivy Bridge]
                    {
                        pictureBox_CPU.Image = Resources.pentium_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G8..")) // 2
                    {
                        pictureBox_CPU.Image = Resources.pentium_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "G6..")) // 2
                    {
                        pictureBox_CPU.Image = Resources.pentium_2_3_logo;
                    }
                }
                else if (Name.Contains("Atom")) // Atom
                {
                    if (Name.Contains("x3"))
                    {
                        pictureBox_CPU.Image = Resources.atom_x3_6_10th_logo;
                    }
                    else if (Name.Contains("x5"))
                    {
                        pictureBox_CPU.Image = Resources.atom_x5_6_10th_logo;
                    }
                    else if (Name.Contains("x7"))
                    {
                        pictureBox_CPU.Image = Resources.atom_x7_6_10th_logo;
                    }
                }
                else if (Name.Contains("Core 2"))
                {
                    if (Name.Contains("Duo"))
                    {
                        pictureBox_CPU.Image = Resources._2duo_1st_logo;
                    }
                    else if (Name.Contains("Quad"))
                    {
                        pictureBox_CPU.Image = Resources._2quad_1st_logo;
                    }
                    else if (Name.Contains("Extreme"))
                    {
                        pictureBox_CPU.Image = Resources._2extreme_1_logo;
                    }
                }
                else if (Name.Contains("Core m")) // Corem
                {
                    if (Name.Contains("3"))
                    {
                        pictureBox_CPU.Image = Resources.m3_8th_logo;
                    }
                    else if (Name.Contains("5"))
                    {
                        pictureBox_CPU.Image = Resources.m5_logo;
                    }
                    else if (Name.Contains("7"))
                    {
                        pictureBox_CPU.Image = Resources.m7_logo;
                    }
                }
                else if (Name.Contains("i3")) // Core i3
                {
                    if (Regex.IsMatch(Name, "-5..")) // 1
                    {
                        pictureBox_CPU.Image = Resources.i3_1st;
                    }
                    else if(Regex.IsMatch(Name, "-2...")) // 2
                    {
                        pictureBox_CPU.Image = Resources.i3_2nd_3rd_logo;
                    }
                    else if (Regex.IsMatch(Name, "-3...")) // 3
                    {
                        pictureBox_CPU.Image = Resources.i3_2nd_3rd_logo;
                    }
                    else if (Regex.IsMatch(Name, "-4...")) // 4
                    {
                        pictureBox_CPU.Image = Resources.i3_4th_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-5...")) // 5
                    {
                        pictureBox_CPU.Image = Resources.i3_4th_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-6...")) // 6
                    {
                        pictureBox_CPU.Image = Resources.i3_6th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-7...")) // 7
                    {
                        pictureBox_CPU.Image = Resources.i3_7th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-8...")) // 8
                    {
                        pictureBox_CPU.Image = Resources.i3_8th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-9...")) // 9
                    {
                        pictureBox_CPU.Image = Resources.i3_9th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-10[1-3]..")) // 10
                    {
                        pictureBox_CPU.Image = Resources.i3_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-121..")) // 12
                    {
                        pictureBox_CPU.Image = Resources.i3_11_13th_logo;
                    }
                }
                else if (Name.Contains("i5"))
                {
                    if (Name.Contains("XE") || Name.Contains('X'))
                    {
                        pictureBox_CPU.Image = Resources.i5_x_logo;
                    }
                    if (Regex.IsMatch(Name, "-[6-7]..")) // 1
                    {
                        pictureBox_CPU.Image = Resources.i5_1st_logo;
                    }
                    else if (Regex.IsMatch(Name, "-2...")) // 2
                    {
                        pictureBox_CPU.Image = Resources.i5_2_3th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-3...")) // 3
                    {
                        pictureBox_CPU.Image = Resources.i5_2_3th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-4...")) // 4
                    {
                        pictureBox_CPU.Image = Resources.i5_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-5...")) // 5
                    {
                        pictureBox_CPU.Image = Resources.i5_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-6...")) // 6
                    {
                        pictureBox_CPU.Image = Resources.i5_6th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-7...")) // 7
                    {
                        pictureBox_CPU.Image = Resources.i5_7th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-8...")) // 8
                    {
                        pictureBox_CPU.Image = Resources.i5_8th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-9...")) // 9
                    {
                        pictureBox_CPU.Image = Resources.i5_9th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-10[4-6]..")) // 10
                    {
                        pictureBox_CPU.Image = Resources.i5_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-11[4-6]..")) // 11
                    {
                        pictureBox_CPU.Image = Resources.i5_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-12[4-6]..")) // 12
                    {
                        pictureBox_CPU.Image = Resources.i5_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-13[4-6]..")) // 13
                    {
                        pictureBox_CPU.Image = Resources.i5_11_13th_logo;
                    }
                }
                else if (Name.Contains("i7"))
                {
                    if (Name.Contains("XE") || Name.Contains('X'))
                    {
                        pictureBox_CPU.Image = Resources.i7_x_logo;
                    }
                    else if (Regex.IsMatch(Name, "-8..")) // 1
                    {
                        pictureBox_CPU.Image = Resources.i7_1st_logo;
                    }
                    else if (Regex.IsMatch(Name, "-2...")) // 2
                    {
                        pictureBox_CPU.Image = Resources.i7_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "-3...")) // 3
                    {
                        pictureBox_CPU.Image = Resources.i7_2_3_logo;
                    }
                    else if (Regex.IsMatch(Name, "-4...")) // 4
                    {
                        pictureBox_CPU.Image = Resources.i7_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-5...")) // 5
                    {
                        pictureBox_CPU.Image = Resources.i7_4_5th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-6...")) // 6
                    {
                        pictureBox_CPU.Image = Resources.i7_6th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-7...")) // 7
                    {
                        pictureBox_CPU.Image = Resources.i7_7th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-8...")) // 8
                    {
                        pictureBox_CPU.Image = Resources.i7_8th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-9...")) // 9
                    {
                        pictureBox_CPU.Image = Resources.i7_9th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-107..")) // 10
                    {
                        pictureBox_CPU.Image = Resources.i7_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-117..")) // 11
                    {
                        pictureBox_CPU.Image = Resources.i7_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-127..")) // 12
                    {
                        pictureBox_CPU.Image = Resources.i7_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-137..")) // 13
                    {
                        pictureBox_CPU.Image = Resources.i7_11_13th_logo;
                    }
                    else
                    {

                    }
                }
                else if (Name.Contains("i9"))
                {
                    if (Name.Contains("XE"))
                    {
                        pictureBox_CPU.Image = Resources.i9_9th_extreme_logo;
                    }
                    else if (Name.Contains('X'))
                    {
                        pictureBox_CPU.Image = Resources.i9_x_logo;
                    }
                    else if (Regex.IsMatch(Name, "-9...")) // 9
                    {
                        pictureBox_CPU.Image = Resources.i9_9th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-109..")) // 10
                    {
                        pictureBox_CPU.Image = Resources.i9_10th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-119..")) // 11
                    {
                        pictureBox_CPU.Image = Resources.i9_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-129..")) // 12
                    {
                        pictureBox_CPU.Image = Resources.i9_11_13th_logo;
                    }
                    else if (Regex.IsMatch(Name, "-139..")) // 13
                    {
                        pictureBox_CPU.Image = Resources.i9_11_13th_logo;
                    }
                }
                else if (Name.Contains("Xeon"))
                {
                    pictureBox_CPU.Image = Resources.xeon_6_10th_logo;
                }
                else
                {
                    pictureBox_CPU.Image = Resources.intel_inside;
                }
            }
            else if (Name.Contains("AMD"))
            {
                if (Name.Contains("Ryzen"))
                {
                    if (Name.Contains("Ryzen 3"))
                    {
                        if (Name.Contains("Pro"))
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_3_pro_logo;
                        }
                        else
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_3_logo;
                        }
                    }
                    else if (Name.Contains("Ryzen 5"))
                    {
                        if (Name.Contains("Pro"))
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_5_pro_logo;
                        }
                        else
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_5_logo;
                        }
                    }
                    else if (Name.Contains("Ryzen 7"))
                    {
                        if (Name.Contains("Pro"))
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_7_pro_logo;
                        }
                        else
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_7_logo;
                        }
                    }
                    else if (Name.Contains("Ryzen 9"))
                    {
                        pictureBox_CPU.Image = Resources.amd_ryzen_9_logo;
                    }
                    else if (Name.Contains("Ryzen Threadripper"))
                    {
                        if (Name.Contains("Pro"))
                        {
                            pictureBox_CPU.Image = Resources.amd_ryzen_threadripper_pro;
                        }
                        else
                        {
                            pictureBox_CPU.Image = Resources.ryzen_threadripper_logo;
                        }
                    }
                }
                else if (Name.Contains("Athlon"))
                {
                    pictureBox_CPU.Image = Resources.amd_athlon_logo;
                }
                else
                {
                    pictureBox_CPU.Image = Resources.amd_inside;
                }
            }
            else
            {

            }
        }

        private string GPUVendor(string Name)
        {
            if (Name.Contains("NVIDIA"))
            {
                if (Name.Contains("GTX"))
                {
                    pictureBox_GPU.Image = Resources.nvidia_gtx_logo;
                }
                else if (Name.Contains("RTX"))
                {
                    pictureBox_GPU.Image = Resources.nvidia_rtx_logo;
                }

                return "NVIDIA Corporation";
            }
            else if (Name.Contains("Radeon") || Name.Contains("AMD"))
            {
                if (Name.Contains("Vega"))
                {
                    pictureBox_GPU.Image = Resources.radeon_vega_logo;
                }
                else if (Name.Contains("RX"))
                {
                    pictureBox_GPU.Image = Resources.radeon_rx_logo;
                }
                else
                {
                    pictureBox_GPU.Image = Resources.radeon_logo;
                }
                
                return "Advanced Micro Devices Corporation";
            }
            else if (Name.Contains("Intel"))
            {
                if (Name.Contains("HD"))
                {
                    pictureBox_GPU.Image = Resources.intel_hd_logo;
                }
                else if (Name.Contains("Iris"))
                {
                    pictureBox_GPU.Image = Resources.intel_iris_logo;
                }
                else if(Name.Contains("Arc"))
                {
                    pictureBox_GPU.Image = Resources.intel_arc_logo;
                }
                
                return "Intel Corporation";
            }
            else
            {
                return "Unknown";
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
