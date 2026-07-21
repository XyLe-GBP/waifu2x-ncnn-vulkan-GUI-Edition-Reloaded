using NVGE.Localization;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NVGE
{
    public sealed class FormEngineManager : Form
    {
        private readonly ListView listViewEngines = new();
        private readonly Button buttonDownload = new();
        private readonly Button buttonOpenFolder = new();
        private readonly Button buttonClose = new();
        private readonly ProgressBar progressBar = new();
        private readonly Label labelStatus = new();

        public FormEngineManager()
        {
            InitializeComponent();
            ModernWinFormsTheme.Apply(this);
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            Text = "Engine Manager";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(760, 360);

            listViewEngines.FullRowSelect = true;
            listViewEngines.GridLines = true;
            listViewEngines.HideSelection = false;
            listViewEngines.MultiSelect = false;
            listViewEngines.View = View.Details;
            listViewEngines.Location = new Point(12, 12);
            listViewEngines.Size = new Size(736, 210);
            listViewEngines.Columns.Add("Engine", 210);
            listViewEngines.Columns.Add("Status", 120);
            listViewEngines.Columns.Add("Repository", 380);
            listViewEngines.DoubleClick += ButtonDownload_Click;

            progressBar.Location = new Point(12, 236);
            progressBar.Size = new Size(736, 22);

            labelStatus.Location = new Point(12, 266);
            labelStatus.Size = new Size(736, 24);
            labelStatus.Text = "Select an engine and download it from the official GitHub release.";

            buttonDownload.Location = new Point(12, 308);
            buttonDownload.Size = new Size(180, 32);
            buttonDownload.Text = "Download / Update";
            buttonDownload.Click += ButtonDownload_Click;

            buttonOpenFolder.Location = new Point(204, 308);
            buttonOpenFolder.Size = new Size(150, 32);
            buttonOpenFolder.Text = "Open Folder";
            buttonOpenFolder.Click += ButtonOpenFolder_Click;

            buttonClose.Location = new Point(598, 308);
            buttonClose.Size = new Size(150, 32);
            buttonClose.Text = "Close";
            buttonClose.Click += (_, _) => Close();

            Controls.Add(listViewEngines);
            Controls.Add(progressBar);
            Controls.Add(labelStatus);
            Controls.Add(buttonDownload);
            Controls.Add(buttonOpenFolder);
            Controls.Add(buttonClose);

            Load += FormEngineManager_Load;

            ResumeLayout(false);
        }

        private void FormEngineManager_Load(object sender, EventArgs e)
        {
            RefreshEngineList();
        }

        private void RefreshEngineList()
        {
            EnginePackageManager.EnsureEngineDirectories();
            listViewEngines.Items.Clear();

            foreach (EnginePackage engine in EnginePackageManager.Engines)
            {
                bool installed = EnginePackageManager.IsEngineInstalled(engine);
                bool hasModels = installed && EnginePackageManager.GetAvailableModels(engine).Count > 0;
                var item = new ListViewItem(engine.DisplayName)
                {
                    Tag = engine
                };
                item.SubItems.Add(installed ? hasModels ? "Installed" : "Models missing" : "Not installed");
                item.SubItems.Add(engine.RepositoryUrl);
                item.ForeColor = installed && hasModels ? SystemColors.ControlText : Color.DarkRed;
                listViewEngines.Items.Add(item);
            }

            if (listViewEngines.Items.Count > 0 && listViewEngines.SelectedItems.Count == 0)
            {
                listViewEngines.Items[0].Selected = true;
            }
        }

        private async void ButtonDownload_Click(object sender, EventArgs e)
        {
            if (listViewEngines.SelectedItems.Count == 0)
            {
                return;
            }

            if (listViewEngines.SelectedItems[0].Tag is not EnginePackage engine)
            {
                return;
            }

            try
            {
                SetBusy(true);
                progressBar.Value = 0;
                var progress = new Progress<EngineInstallProgress>(value =>
                {
                    progressBar.Value = Math.Max(progressBar.Minimum, Math.Min(progressBar.Maximum, value.Percent));
                    labelStatus.Text = value.Status;
                });

                await EnginePackageManager.DownloadAndInstallAsync(engine, progress);
                RefreshEngineList();
                if (EnginePackageManager.GetAvailableModels(engine).Count == 0)
                {
                    MessageBox.Show(
                        engine.DisplayName + " has been installed, but no usable model was found.",
                        Strings.MSGWarning,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(engine.DisplayName + " has been installed.", Strings.MSGInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.UnExpectedError, ex.Message), Strings.MSGError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetBusy(false);
            }
        }

        private void ButtonOpenFolder_Click(object sender, EventArgs e)
        {
            EnginePackageManager.EnsureEngineDirectories();
            Process.Start(new ProcessStartInfo
            {
                FileName = EnginePackageManager.EnginesRootDirectory,
                UseShellExecute = true
            });
        }

        private void SetBusy(bool isBusy)
        {
            buttonDownload.Enabled = !isBusy;
            buttonOpenFolder.Enabled = !isBusy;
            buttonClose.Enabled = !isBusy;
            listViewEngines.Enabled = !isBusy;
            if (!isBusy)
            {
                labelStatus.Text = "Select an engine and download it from the official GitHub release.";
            }
        }
    }
}
