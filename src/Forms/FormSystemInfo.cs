using NVGE.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Vortice.DXGI;

namespace NVGE
{
    public partial class FormSystemInfo : Form
    {
        private readonly List<GpuAdapterInfo> gpuAdapters = new();

        public FormSystemInfo()
        {
            InitializeComponent();
            ModernWinFormsTheme.Apply(this);
            comboBox_GPU.SelectedIndexChanged += ComboBox_GPU_SelectedIndexChanged;
        }

        private void FormSystemInfo_Load(object sender, EventArgs e)
        {
            string[] OSInfo = new string[17];
            string[] CPUInfo = new string[3];
            string[] GPUInfo = new string[3];
            string[] BIOSInfo = new string[5];
            string[] BaseInfo = new string[3];

            SafeReadSystemInfo(() => SystemInfo.GetSystemInformation(OSInfo));
            SafeReadSystemInfo(() => SystemInfo.GetProcessorsInformation(CPUInfo));
            SafeReadSystemInfo(() => SystemInfo.GetVideoControllerInformation(GPUInfo));
            SafeReadSystemInfo(() => SystemInfo.GetBIOSInformation(BIOSInfo));
            SafeReadSystemInfo(() => SystemInfo.GetBaseBoardInformation(BaseInfo));

            LoadGpuAdapters(GPUInfo);
            PopulateGpuComboBox();

            CPUVendor(CPUInfo[0]);
            label_CPUName.Text = ValueOrUnknown(CPUInfo[0]);
            label_CPUCores.Text = ValueOrUnknown(CPUInfo[1]);
            label_CPUThreads.Text = ValueOrUnknown(CPUInfo[2]);
            label_OSName.Text = GetOsDisplayName(OSInfo);
            label_MBMaker.Text = ValueOrUnknown(BaseInfo[0]);
            label_MBModel.Text = ValueOrUnknown(BaseInfo[2]);
            ApplySelectedGpuInfo();
        }

        private void LoadGpuAdapters(string[] fallbackGpuInfo)
        {
            gpuAdapters.Clear();
            LoadDxgiAdapters();
            LoadWmiVideoControllers();

            if (gpuAdapters.Count == 0 && fallbackGpuInfo?.Length == 3)
            {
                AddOrUpdateGpuAdapter(
                    fallbackGpuInfo[0],
                    FormatAdapterRam(fallbackGpuInfo[2]),
                    fallbackGpuInfo[1]);
            }
        }

        private void LoadDxgiAdapters()
        {
            try
            {
                if (DXGI.CreateDXGIFactory1(out IDXGIFactory1 factory).Failure)
                {
                    return;
                }

                using (factory)
                {
                    for (int iAdapter = 0; ; iAdapter++)
                    {
                        if (factory.EnumAdapters((uint)iAdapter, out IDXGIAdapter adapter).Failure)
                        {
                            break;
                        }

                        using (adapter)
                        {
                            string adapterName = NormalizeDisplayName(adapter.Description.Description);
                            if (string.IsNullOrWhiteSpace(adapterName) ||
                                adapter.Description.DedicatedVideoMemory == 0)
                            {
                                continue;
                            }

                            string memoryText = adapter.Description.DedicatedVideoMemory / 1024 / 1024 + "MB";
                            Debug.WriteLine($"[adapter {iAdapter}]");
                            Debug.WriteLine($"Description:{adapterName}");
                            Debug.WriteLine($"DedicatedVideoMemory:{memoryText}");
                            AddOrUpdateGpuAdapter(adapterName, memoryText, null);
                        }
                    }
                }
            }
            catch (Exception ex) when (IsSystemInfoException(ex))
            {
                Debug.WriteLine(ex);
            }
        }

        private void LoadWmiVideoControllers()
        {
            try
            {
                using ManagementObjectSearcher searcher = new(
                    "SELECT Name, DriverVersion, AdapterRAM, CurrentBitsPerPixel, CurrentHorizontalResolution FROM Win32_VideoController");
                using ManagementObjectCollection controllers = searcher.Get();
                foreach (ManagementObject controller in controllers)
                {
                    using (controller)
                    {
                        string name = NormalizeDisplayName(controller["Name"]?.ToString());
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            continue;
                        }

                        bool activeDisplay =
                            controller["CurrentBitsPerPixel"] != null ||
                            controller["CurrentHorizontalResolution"] != null;
                        bool alreadyKnown = FindGpuAdapter(name) != null;
                        if (!activeDisplay && !alreadyKnown && gpuAdapters.Count > 0)
                        {
                            continue;
                        }

                        AddOrUpdateGpuAdapter(
                            name,
                            FormatAdapterRam(controller["AdapterRAM"]?.ToString()),
                            controller["DriverVersion"]?.ToString());
                    }
                }
            }
            catch (Exception ex) when (IsSystemInfoException(ex))
            {
                Debug.WriteLine(ex);
            }
        }

        private void PopulateGpuComboBox()
        {
            comboBox_GPU.BeginUpdate();
            try
            {
                comboBox_GPU.Items.Clear();
                foreach (GpuAdapterInfo adapter in gpuAdapters)
                {
                    comboBox_GPU.Items.Add(adapter);
                }

                comboBox_GPU.Enabled = comboBox_GPU.Items.Count > 1;
                if (comboBox_GPU.Items.Count > 0)
                {
                    comboBox_GPU.SelectedIndex = 0;
                }
            }
            finally
            {
                comboBox_GPU.EndUpdate();
            }
        }

        private void AddOrUpdateGpuAdapter(string name, string memoryText, string driverVersion)
        {
            name = NormalizeDisplayName(name);
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            GpuAdapterInfo existing = FindGpuAdapter(name);
            if (existing == null)
            {
                gpuAdapters.Add(new GpuAdapterInfo(
                    name,
                    ValueOrUnknown(memoryText),
                    ValueOrUnknown(driverVersion)));
                return;
            }

            if (IsUnknown(existing.MemoryText) && !string.IsNullOrWhiteSpace(memoryText))
            {
                existing.MemoryText = memoryText;
            }
            if (IsUnknown(existing.DriverVersion) && !string.IsNullOrWhiteSpace(driverVersion))
            {
                existing.DriverVersion = driverVersion;
            }
        }

        private GpuAdapterInfo FindGpuAdapter(string name)
        {
            string normalizedName = NormalizeGpuName(name);
            return gpuAdapters.FirstOrDefault(adapter =>
                string.Equals(NormalizeGpuName(adapter.Name), normalizedName, StringComparison.OrdinalIgnoreCase));
        }

        private void ComboBox_GPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplySelectedGpuInfo();
        }

        private void ApplySelectedGpuInfo()
        {
            if (comboBox_GPU.SelectedItem is not GpuAdapterInfo selectedGpu)
            {
                selectedGpu = gpuAdapters.FirstOrDefault();
            }

            if (selectedGpu == null)
            {
                label_GPUName.Text = "Unknown";
                label_GPUMaker.Text = "Unknown";
                label_GPUDriver.Text = "Unknown";
                label_GPUMemory.Text = "Unknown";
                pictureBox_GPU.Image = null;
                return;
            }

            label_GPUName.Text = ValueOrUnknown(selectedGpu.Name);
            label_GPUMaker.Text = GPUVendor(selectedGpu.Name);
            label_GPUDriver.Text = ValueOrUnknown(selectedGpu.DriverVersion);
            label_GPUMemory.Text = ValueOrUnknown(selectedGpu.MemoryText);
        }

        private static string NormalizeDisplayName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return Regex.Replace(value.Trim().TrimEnd('\0'), @"\s+", " ");
        }

        private static string NormalizeGpuName(string value)
        {
            return NormalizeDisplayName(value).ToUpperInvariant();
        }

        private static string FormatAdapterRam(string adapterRam)
        {
            if (!long.TryParse(adapterRam, out long bytes) || bytes <= 0)
            {
                return "Unknown";
            }

            return bytes / 1024 / 1024 + "MB";
        }

        private static string GetOsDisplayName(string[] osInfo)
        {
            string name = osInfo?.Length > 1 ? ValueOrUnknown(osInfo[1]) : "Unknown";
            string version = osInfo?.Length > 3 ? osInfo[3] : null;
            return string.IsNullOrWhiteSpace(version) ? name : name + " - " + version;
        }

        private static string ValueOrUnknown(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        private static bool IsUnknown(string value)
        {
            return string.IsNullOrWhiteSpace(value) ||
                string.Equals(value, "Unknown", StringComparison.OrdinalIgnoreCase);
        }

        private static void SafeReadSystemInfo(Action readAction)
        {
            try
            {
                readAction();
            }
            catch (Exception ex) when (IsSystemInfoException(ex))
            {
                Debug.WriteLine(ex);
            }
        }

        private static bool IsSystemInfoException(Exception ex)
        {
            return ex is ManagementException ||
                ex is UnauthorizedAccessException ||
                ex is COMException ||
                ex is InvalidOperationException;
        }

        private void CPUVendor(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return;
            }

            if (Name.Contains("Intel"))
            {
                if (Name.Contains("Celeron") || Name.Contains("Cerelon"))
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
                else if (Name.Contains("Core 2")) // Core 2
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
                else if (Name.Contains("i5")) // Core i5
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
                else if (Name.Contains("i7")) // Core i7
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
                else if (Name.Contains("i9")) // Core i9
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
                else if (Name.Contains("Ultra")) // Core Ultra
                {
                    if (Name.Contains("3"))
                    {
                        pictureBox_CPU.Image = Resources.ultra_3;
                    }
                    else if (Name.Contains("5"))
                    {
                        pictureBox_CPU.Image = Resources.ultra_5;
                    }
                    else if (Name.Contains("7"))
                    {
                        pictureBox_CPU.Image = Resources.ultra_7;
                    }
                    else if (Name.Contains("9"))
                    {
                        pictureBox_CPU.Image = Resources.ultra_9;
                    }
                }
                else if (Name.Contains("Xeon")) // Xeon
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
            pictureBox_GPU.Image = null;

            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Unknown";
            }

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

        private sealed class GpuAdapterInfo
        {
            public GpuAdapterInfo(string name, string memoryText, string driverVersion)
            {
                Name = name;
                MemoryText = memoryText;
                DriverVersion = driverVersion;
            }

            public string Name { get; }
            public string MemoryText { get; set; }
            public string DriverVersion { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
