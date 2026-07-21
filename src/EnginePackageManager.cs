using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NVGE
{
    public sealed class EnginePackage
    {
        public EnginePackage(int id, string displayName, string folderName, string executableName, string owner, string repository)
        {
            Id = id;
            DisplayName = displayName;
            FolderName = folderName;
            ExecutableName = executableName;
            Owner = owner;
            Repository = repository;
        }

        public int Id { get; }
        public string DisplayName { get; }
        public string FolderName { get; }
        public string ExecutableName { get; }
        public string Owner { get; }
        public string Repository { get; }
        public string RepositoryUrl => $"https://github.com/{Owner}/{Repository}";
        public string InstallDirectory => Path.Combine(EnginePackageManager.EnginesRootDirectory, FolderName);
        public string ExecutablePath => Path.Combine(InstallDirectory, ExecutableName);
    }

    public sealed class EngineInstallProgress
    {
        public EngineInstallProgress(string status, int percent)
        {
            Status = status;
            Percent = percent;
        }

        public string Status { get; }
        public int Percent { get; }
    }

    public sealed class EngineModel
    {
        public EngineModel(string displayName, string commandArgument)
        {
            DisplayName = displayName;
            CommandArgument = commandArgument;
        }

        public string DisplayName { get; }
        public string CommandArgument { get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }

    public static class EnginePackageManager
    {
        private static readonly HttpClient httpClient = CreateHttpClient();

        public static readonly EnginePackage Waifu2x = new(0, "waifu2x-ncnn-vulkan", "waifu2x", "waifu2x-ncnn-vulkan.exe", "nihui", "waifu2x-ncnn-vulkan");
        public static readonly EnginePackage RealCugan = new(1, "realcugan-ncnn-vulkan", "realcugan", "realcugan-ncnn-vulkan.exe", "nihui", "realcugan-ncnn-vulkan");
        public static readonly EnginePackage RealEsrgan = new(2, "Real-ESRGAN-ncnn-vulkan", "realesrgan", "realesrgan-ncnn-vulkan.exe", "xinntao", "Real-ESRGAN-ncnn-vulkan");

        public static IReadOnlyList<EnginePackage> Engines { get; } = new[] { Waifu2x, RealCugan, RealEsrgan };
        public static string EnginesRootDirectory => Path.Combine(Directory.GetCurrentDirectory(), "res", "engines");

        public static void EnsureEngineDirectories()
        {
            Directory.CreateDirectory(EnginesRootDirectory);
            foreach (EnginePackage engine in Engines)
            {
                Directory.CreateDirectory(engine.InstallDirectory);
            }
        }

        public static EnginePackage GetEngine(int id)
        {
            return Engines.FirstOrDefault(engine => engine.Id == id);
        }

        public static bool IsEngineInstalled(int id)
        {
            EnginePackage engine = GetEngine(id);
            return engine != null && IsEngineInstalled(engine);
        }

        public static bool IsEngineInstalled(EnginePackage engine)
        {
            return File.Exists(engine.ExecutablePath);
        }

        public static bool HasAnyInstalledEngine()
        {
            return Engines.Any(IsEngineInstalled);
        }

        public static int GetFirstInstalledEngineId()
        {
            EnginePackage engine = Engines.FirstOrDefault(IsEngineInstalled);
            return engine?.Id ?? -1;
        }

        public static bool IsEngineReady(int id)
        {
            EnginePackage engine = GetEngine(id);
            return engine != null && IsEngineInstalled(engine) && GetAvailableModels(engine).Count > 0;
        }

        public static IReadOnlyList<EngineModel> GetAvailableModels(int id)
        {
            EnginePackage engine = GetEngine(id);
            return engine == null ? Array.Empty<EngineModel>() : GetAvailableModels(engine);
        }

        public static IReadOnlyList<EngineModel> GetAvailableModels(EnginePackage engine)
        {
            if (engine == null || !IsEngineInstalled(engine) || !Directory.Exists(engine.InstallDirectory))
            {
                return Array.Empty<EngineModel>();
            }

            if (engine.Id == RealEsrgan.Id)
            {
                return GetRealEsrganModels(engine);
            }

            return Directory.GetDirectories(engine.InstallDirectory)
                .Where(DirectoryContainsModelFiles)
                .Select(directory =>
                {
                    string modelDirectoryName = Path.GetFileName(directory);
                    return new EngineModel(modelDirectoryName, " -m " + modelDirectoryName);
                })
                .OrderBy(model => model.DisplayName, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        public static async Task DownloadAndInstallAsync(
            EnginePackage engine,
            IProgress<EngineInstallProgress> progress,
            CancellationToken cancellationToken = default)
        {
            if (engine == null)
            {
                throw new ArgumentNullException(nameof(engine));
            }

            EnsureEngineDirectories();
            progress?.Report(new EngineInstallProgress("Checking latest GitHub release...", 0));

            GitHubAsset asset = await GetLatestWindowsAssetAsync(engine, cancellationToken);
            string downloadDirectory = Path.Combine(EnginesRootDirectory, "_downloads");
            string extractDirectory = Path.Combine(downloadDirectory, engine.FolderName + "_extract");
            string zipPath = Path.Combine(downloadDirectory, engine.FolderName + ".zip");

            Directory.CreateDirectory(downloadDirectory);
            TryDeleteFile(zipPath);
            TryDeleteDirectory(extractDirectory);

            try
            {
                progress?.Report(new EngineInstallProgress("Downloading " + asset.Name + "...", 0));
                await DownloadFileAsync(asset.BrowserDownloadUrl, zipPath, progress, cancellationToken);

                progress?.Report(new EngineInstallProgress("Extracting package...", 95));
                ZipFile.ExtractToDirectory(zipPath, extractDirectory, true);

                string executablePath = Directory.GetFiles(
                    extractDirectory,
                    engine.ExecutableName,
                    SearchOption.AllDirectories).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(executablePath))
                {
                    throw new FileNotFoundException("The downloaded package does not contain " + engine.ExecutableName + ".");
                }

                string sourceDirectory = Path.GetDirectoryName(executablePath);
                CopyDirectory(sourceDirectory, engine.InstallDirectory);

                if (!File.Exists(engine.ExecutablePath))
                {
                    throw new FileNotFoundException("The engine executable was not installed.", engine.ExecutablePath);
                }

                progress?.Report(new EngineInstallProgress("Installed " + engine.DisplayName + ".", 100));
            }
            finally
            {
                TryDeleteFile(zipPath);
                TryDeleteDirectory(extractDirectory);
            }
        }

        private static IReadOnlyList<EngineModel> GetRealEsrganModels(EnginePackage engine)
        {
            string modelsDirectory = Path.Combine(engine.InstallDirectory, "models");
            if (!Directory.Exists(modelsDirectory))
            {
                return Array.Empty<EngineModel>();
            }

            return Directory.GetFiles(modelsDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(path =>
                    string.Equals(Path.GetExtension(path), ".bin", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(Path.GetExtension(path), ".param", StringComparison.OrdinalIgnoreCase))
                .Select(path => Path.GetFileNameWithoutExtension(path))
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name, StringComparer.OrdinalIgnoreCase)
                .Select(name => new EngineModel(name, " -n " + name))
                .ToList();
        }

        private static bool DirectoryContainsModelFiles(string directory)
        {
            return Directory.EnumerateFiles(directory, "*.param", SearchOption.TopDirectoryOnly).Any() ||
                Directory.EnumerateFiles(directory, "*.bin", SearchOption.TopDirectoryOnly).Any();
        }

        private static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("waifu2x-nvger");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            client.Timeout = TimeSpan.FromMinutes(30);
            return client;
        }

        private static async Task<GitHubAsset> GetLatestWindowsAssetAsync(EnginePackage engine, CancellationToken cancellationToken)
        {
            string apiUrl = $"https://api.github.com/repos/{engine.Owner}/{engine.Repository}/releases/latest";
            string json = await httpClient.GetStringAsync(apiUrl, cancellationToken);
            GitHubRelease release = JsonConvert.DeserializeObject<GitHubRelease>(json);
            GitHubAsset asset = release?.Assets?
                .Where(item => !string.IsNullOrWhiteSpace(item.Name) && item.Name.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                .Select(item => new { Asset = item, Score = GetWindowsAssetScore(item.Name) })
                .Where(item => item.Score > 0)
                .OrderByDescending(item => item.Score)
                .Select(item => item.Asset)
                .FirstOrDefault();

            if (asset == null || string.IsNullOrWhiteSpace(asset.BrowserDownloadUrl))
            {
                throw new InvalidOperationException("A Windows zip asset was not found in the latest release of " + engine.Repository + ".");
            }

            return asset;
        }

        private static int GetWindowsAssetScore(string assetName)
        {
            string name = assetName.ToLowerInvariant();
            int score = 0;
            if (name.Contains("windows"))
            {
                score += 100;
            }
            if (name.Contains("win64") || name.Contains("win-x64") || name.Contains("x64"))
            {
                score += 20;
            }
            if (name.Contains("win"))
            {
                score += 10;
            }
            if (name.Contains("arm"))
            {
                score -= 50;
            }
            return score;
        }

        private static async Task DownloadFileAsync(
            string url,
            string destinationPath,
            IProgress<EngineInstallProgress> progress,
            CancellationToken cancellationToken)
        {
            using HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;
            await using Stream source = await response.Content.ReadAsStreamAsync(cancellationToken);
            await using FileStream destination = new(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None);

            byte[] buffer = new byte[1024 * 128];
            long totalRead = 0;
            int read;
            while ((read = await source.ReadAsync(buffer, cancellationToken)) > 0)
            {
                await destination.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                totalRead += read;
                if (totalBytes.HasValue && totalBytes.Value > 0)
                {
                    int percent = Math.Min(94, (int)(totalRead * 94 / totalBytes.Value));
                    progress?.Report(new EngineInstallProgress("Downloading package...", percent));
                }
            }
        }

        private static void CopyDirectory(string sourceDirectory, string destinationDirectory)
        {
            Directory.CreateDirectory(destinationDirectory);

            foreach (string directory in Directory.GetDirectories(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(sourceDirectory, directory);
                Directory.CreateDirectory(Path.Combine(destinationDirectory, relativePath));
            }

            foreach (string file in Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(sourceDirectory, file);
                string destinationPath = Path.Combine(destinationDirectory, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));
                File.Copy(file, destinationPath, true);
            }
        }

        private static void TryDeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private static void TryDeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        private sealed class GitHubRelease
        {
            [JsonProperty("assets")]
            public List<GitHubAsset> Assets { get; set; }
        }

        private sealed class GitHubAsset
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("browser_download_url")]
            public string BrowserDownloadUrl { get; set; }
        }
    }
}
