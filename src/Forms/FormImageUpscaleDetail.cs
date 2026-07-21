using NVGE.Localization;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormImageUpscaleDetail : Form
    {
        private const string TempProjectDirectoryName = "_temp-project";
        private const string ImagesDirectoryName = "images";
        private const string SourcesDirectoryName = "sources";
        private const string UpscaledDirectoryName = "2x";

        private string ext = ".png";
        private int pos;
        private string currentSourceImagePath;
        private string currentUpscaledImagePath;

        public FormImageUpscaleDetail()
        {
            InitializeComponent();
            ModernWinFormsTheme.Apply(this);
            FormClosed += FormImageUpscaleDetail_FormClosed;
        }

        private void FormImageUpscaleDetail_Load(object sender, EventArgs e)
        {
            Common.LoadConfigOrReset();

            int reduction = GetConfigInt("Reduction", 0);
            int useengine = GetConfigInt("ConversionType", 0);
            int model = GetConfigInt("Model", 0);
            int gpu = GetConfigInt("GPU", 0);
            int scale = GetConfigInt("Scale", 1);
            int blksize = GetConfigInt("Blocksize", 0);
            int thread = GetConfigInt("Thread", 0);
            int format = GetConfigInt("Format", 1);
            bool vbo = GetConfigBool("Verbose", false);
            bool tta = GetConfigBool("TTA", false);

            ext = GetOutputExtension(format);
            ApplyDetailLabels(reduction, useengine, model, gpu, scale, blksize, thread, format, vbo, tta);

            pos = 0;
            if (GetImageCount() == 0)
            {
                ClearPreviewImage(pictureBox_SourceImage);
                ClearPreviewImage(pictureBox_UpscaledImage);
                currentSourceImagePath = null;
                currentUpscaledImagePath = null;
                label_NotSupported.Visible = true;
                label_webp.Visible = false;
                UpdateNavigationButtons();
                return;
            }

            PictureboxRefleshed();
        }

        private void ApplyDetailLabels(
            int reduction,
            int useengine,
            int model,
            int gpu,
            int scale,
            int blksize,
            int thread,
            int format,
            bool vbo,
            bool tta)
        {
            label_sec.Text = Common.timeSpan.TotalSeconds.ToString() + "s";
            label_rdl.Text = GetReductionLabel(useengine, reduction);
            label_scale.Text = GetScaleLabel(useengine, scale);
            label_gpu.Text = GetGpuLabel(useengine, gpu);
            label_blks.Text = blksize == 0 ? "Autodetect" : blksize + "Blocks";

            SetVerboseLabelsVisible(vbo);
            if (!vbo)
            {
                return;
            }

            label_vbs.Text = "Enabled";
            label_model.Text = GetModelLabel(useengine, model);
            label_fmt.Text = GetFormatLabel(format, ext);
            label_thread.Text = thread switch
            {
                0 => "1:2:2",
                1 => "1:2",
                2 => "2",
                3 => "2:2",
                4 => "2:2:2",
                _ => "Unknown",
            };
            label_tta.Text = tta ? "Enabled" : "Disabled";
        }

        private void SetVerboseLabelsVisible(bool visible)
        {
            label_vbs.Visible = visible;
            label_tta.Visible = visible;
            label_thread.Visible = visible;
            label_model.Visible = visible;
            label_fmt.Visible = visible;
            label7.Visible = visible;
            label9.Visible = visible;
            label12.Visible = visible;
            label13.Visible = visible;
            label14.Visible = visible;
            label16.Visible = !visible;
        }

        private static string GetReductionLabel(int useengine, int reduction)
        {
            if (useengine == 2)
            {
                return "Null";
            }

            return reduction switch
            {
                0 => "No Reduction",
                1 => "Level 0",
                2 => "Level 1",
                3 => "Level 2",
                4 => "Level 3",
                5 => "Conservative",
                _ => "Unknown",
            };
        }

        private static string GetScaleLabel(int useengine, int scale)
        {
            return useengine switch
            {
                1 => scale switch
                {
                    0 => "x1",
                    1 => "x2",
                    2 => "x3",
                    3 => "x4",
                    4 => "x8",
                    5 => "x16",
                    _ => "Unknown",
                },
                2 => scale switch
                {
                    0 => "x4",
                    1 => "x8",
                    2 => "x16",
                    3 => "x32",
                    _ => "Unknown",
                },
                _ => scale switch
                {
                    0 => "x1",
                    1 => "x2",
                    2 => "x4",
                    3 => "x8",
                    4 => "x16",
                    _ => "Unknown",
                },
            };
        }

        private static string GetGpuLabel(int useengine, int gpu)
        {
            if (useengine == 2)
            {
                return gpu switch
                {
                    0 => "Autodetect",
                    1 => "dGPU (GPU 0)",
                    2 => "dGPU (GPU 1)",
                    3 => "dGPU (GPU 2)",
                    _ => "Unknown",
                };
            }

            return gpu switch
            {
                0 => "Autodetect",
                1 => "iGPU (CPU)",
                2 => "dGPU (GPU 0)",
                3 => "dGPU (GPU 1)",
                4 => "dGPU (GPU 2)",
                _ => "Unknown",
            };
        }

        private static string GetModelLabel(int useengine, int model)
        {
            try
            {
                var models = EnginePackageManager.GetAvailableModels(useengine);
                if (model >= 0 && model < models.Count)
                {
                    return models[model].DisplayName;
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }

            return useengine switch
            {
                0 => model switch
                {
                    0 => "CUnet (models-cunet)",
                    1 => "RGB (models-upconv_7_anime_style_art_rgb)",
                    2 => "Photo (models-upconv_7_photo)",
                    _ => "Unknown",
                },
                1 => model switch
                {
                    0 => "SE (models-se)",
                    1 => "Nose (models-nose)",
                    2 => "Pro (models-pro)",
                    _ => "Unknown",
                },
                2 => model switch
                {
                    0 => "Real-ESRGAN (realesrgan-x4plus)",
                    1 => "Real-ESRGAN Photo (realesrnet-x4plus)",
                    2 => "Real-ESRGAN Anime (realesrgan-x4plus-anime)",
                    _ => "Unknown",
                },
                _ => "Unknown",
            };
        }

        private static string GetFormatLabel(int format, string extension)
        {
            return format switch
            {
                0 => "JPEG",
                1 => "PNG",
                2 => "WEBP",
                3 => "ICO",
                4 => extension + " (Custom)",
                _ => "Unknown",
            };
        }

        private static string GetOutputExtension(int format)
        {
            return format switch
            {
                0 => ".jpg",
                1 => ".png",
                2 => ".webp",
                3 => ".ico",
                4 => NormalizeExtension(Common.ManualImageFormat),
                _ => ".png",
            };
        }

        private void PictureBox_SourceImage_Click(object sender, EventArgs e)
        {
            OpenImageInViewer(currentSourceImagePath);
        }

        private void PictureBox_UpscaledImage_Click(object sender, EventArgs e)
        {
            OpenImageInViewer(currentUpscaledImagePath);
        }

        private void Button_prev_Click(object sender, EventArgs e)
        {
            if (pos <= 0)
            {
                return;
            }

            pos--;
            PictureboxRefleshed();
        }

        private void Button_next_Click(object sender, EventArgs e)
        {
            int count = GetImageCount();
            if (pos >= count - 1)
            {
                return;
            }

            pos++;
            PictureboxRefleshed();
        }

        private void Button_ok_Click(object sender, EventArgs e)
        {
            Common.ImgDetFlag = 0;
            Close();
        }

        private void Button_cancel_Click(object sender, EventArgs e)
        {
            Common.ImgDetFlag = 1;
            Close();
        }

        private void PictureboxRefleshed()
        {
            Common.LoadConfigOrReset();

            int count = GetImageCount();
            if (count == 0)
            {
                currentSourceImagePath = null;
                currentUpscaledImagePath = null;
                ClearPreviewImage(pictureBox_SourceImage);
                ClearPreviewImage(pictureBox_UpscaledImage);
                label_NotSupported.Visible = true;
                label_webp.Visible = false;
                UpdateNavigationButtons();
                return;
            }

            pos = Math.Max(0, Math.Min(pos, count - 1));

            int format = GetConfigInt("Format", 1);
            ext = GetOutputExtension(format);

            string[] sourceCandidates = GetSourceImageCandidates(pos);
            string[] upscaledCandidates = GetUpscaledImageCandidates(pos, format);
            string existingUpscaledPath = GetFirstExistingFile(upscaledCandidates);

            bool sourceLoaded = SetPreviewImage(pictureBox_SourceImage, sourceCandidates, out currentSourceImagePath);
            bool upscaledLoaded = SetPreviewImage(pictureBox_UpscaledImage, upscaledCandidates, out currentUpscaledImagePath);
            if (!upscaledLoaded)
            {
                currentUpscaledImagePath = existingUpscaledPath;
            }

            pictureBox_SourceImage.Enabled = sourceLoaded;
            pictureBox_UpscaledImage.Enabled = upscaledLoaded;

            bool canOpenWebpExternally = !upscaledLoaded && sourceLoaded && IsWebpPath(existingUpscaledPath);
            label_webp.Visible = canOpenWebpExternally;
            label_NotSupported.Visible = !sourceLoaded || (!upscaledLoaded && !canOpenWebpExternally);

            UpdateNavigationButtons();
        }

        private string[] GetSourceImageCandidates(int index)
        {
            return new[]
            {
                GetOriginalImagePath(index),
                GetConvertedSourceImagePath(index),
            };
        }

        private string[] GetUpscaledImageCandidates(int index, int format)
        {
            string outputName = ChangeExtension(GetImageFileName(index), ext);
            string tempOutputPath = string.IsNullOrWhiteSpace(outputName)
                ? null
                : Path.Combine(GetUpscaledImagesDirectory(), outputName);
            string customSingleOutputPath = Path.Combine(GetUpscaledImagesDirectory(), "final" + ext);

            return new[]
            {
                format == 4 && IsSingleImage() ? customSingleOutputPath : tempOutputPath,
                tempOutputPath,
                customSingleOutputPath,
                Common.SFDSavePath,
            };
        }

        private static string GetConvertedSourceImagePath(int index)
        {
            string fileName = ChangeExtension(GetImageFileName(index), ".png");
            return string.IsNullOrWhiteSpace(fileName)
                ? null
                : Path.Combine(GetSourceImagesDirectory(), fileName);
        }

        private static string GetOriginalImagePath(int index)
        {
            if (Common.ImageFile == null || index < 0 || index >= Common.ImageFile.Length)
            {
                return null;
            }

            return Common.ImageFile[index];
        }

        private static string GetImageFileName(int index)
        {
            if (Common.ImageFileName != null &&
                index >= 0 &&
                index < Common.ImageFileName.Length &&
                !string.IsNullOrWhiteSpace(Common.ImageFileName[index]))
            {
                return Path.GetFileName(Common.ImageFileName[index]);
            }

            string imagePath = GetOriginalImagePath(index);
            return string.IsNullOrWhiteSpace(imagePath) ? null : Path.GetFileName(imagePath);
        }

        private static string ChangeExtension(string fileName, string extension)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            return Path.ChangeExtension(Path.GetFileName(fileName), NormalizeExtension(extension));
        }

        private static string NormalizeExtension(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return ".png";
            }

            extension = extension.Trim();
            return extension.StartsWith(".", StringComparison.Ordinal) ? extension : "." + extension;
        }

        private static string GetTempImagesDirectory()
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(),
                TempProjectDirectoryName,
                ImagesDirectoryName);
        }

        private static string GetSourceImagesDirectory()
        {
            return Path.Combine(GetTempImagesDirectory(), SourcesDirectoryName);
        }

        private static string GetUpscaledImagesDirectory()
        {
            return Path.Combine(GetTempImagesDirectory(), UpscaledDirectoryName);
        }

        private static int GetImageCount()
        {
            if (Common.ImageFileName != null && Common.ImageFileName.Length > 0)
            {
                return Common.ImageFileName.Length;
            }

            return Common.ImageFile?.Length ?? 0;
        }

        private static bool IsSingleImage()
        {
            return GetImageCount() <= 1;
        }

        private void UpdateNavigationButtons()
        {
            int count = GetImageCount();
            button_prev.Enabled = count > 1 && pos > 0;
            button_next.Enabled = count > 1 && pos < count - 1;
        }

        private static bool SetPreviewImage(PictureBox pictureBox, string[] imagePaths, out string loadedPath)
        {
            Image oldImage = pictureBox.Image;
            pictureBox.ImageLocation = null;
            loadedPath = null;

            try
            {
                foreach (string imagePath in imagePaths)
                {
                    if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                    {
                        continue;
                    }

                    try
                    {
                        int maximumWidth = Math.Max(pictureBox.ClientSize.Width * 2, 1);
                        int maximumHeight = Math.Max(pictureBox.ClientSize.Height * 2, 1);
                        pictureBox.Image = ImageConvert.LoadBitmapForPreview(
                            imagePath,
                            maximumWidth,
                            maximumHeight);
                        loadedPath = imagePath;
                        return true;
                    }
                    catch (Exception ex) when (
                        ex is ArgumentException ||
                        ex is IOException ||
                        ex is UnauthorizedAccessException ||
                        ex is NotSupportedException)
                    {
                    }
                }

                pictureBox.Image = null;
                return false;
            }
            finally
            {
                oldImage?.Dispose();
            }
        }

        private static string GetFirstExistingFile(string[] imagePaths)
        {
            foreach (string imagePath in imagePaths)
            {
                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    return imagePath;
                }
            }

            return null;
        }

        private static bool IsWebpPath(string imagePath)
        {
            return !string.IsNullOrWhiteSpace(imagePath) &&
                string.Equals(Path.GetExtension(imagePath), ".webp", StringComparison.OrdinalIgnoreCase);
        }

        private void OpenImageInViewer(string imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
            {
                MessageBox.Show(
                    this,
                    "The image file could not be found.",
                    Strings.MSGWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                PictureboxRefleshed();
                return;
            }

            using FormShowPicture formShowPicture = new(imagePath);
            formShowPicture.ShowDialog(this);
        }

        private void Label_webp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(currentUpscaledImagePath) || !File.Exists(currentUpscaledImagePath))
            {
                MessageBox.Show(
                    this,
                    "The image file could not be found.",
                    Strings.MSGWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                PictureboxRefleshed();
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo(currentUpscaledImagePath)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    "The image file could not be opened.\n" + ex.Message,
                    Strings.MSGWarning,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void FormImageUpscaleDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClearPreviewImage(pictureBox_SourceImage);
            ClearPreviewImage(pictureBox_UpscaledImage);
        }

        private static void ClearPreviewImage(PictureBox pictureBox)
        {
            Image oldImage = pictureBox.Image;
            pictureBox.ImageLocation = null;
            pictureBox.Image = null;
            oldImage?.Dispose();
        }

        private static int GetConfigInt(string key, int defaultValue)
        {
            try
            {
                return Common.GetConfigInt(key, defaultValue);
            }
            catch
            {
                Common.LoadConfigOrReset();
                return Common.GetConfigInt(key, defaultValue);
            }
        }

        private static bool GetConfigBool(string key, bool defaultValue)
        {
            try
            {
                return bool.TryParse(Config.Entry[key].Value, out bool value) ? value : defaultValue;
            }
            catch
            {
                Common.LoadConfigOrReset();
                return bool.TryParse(Config.Entry[key].Value, out bool value) ? value : defaultValue;
            }
        }
    }
}
