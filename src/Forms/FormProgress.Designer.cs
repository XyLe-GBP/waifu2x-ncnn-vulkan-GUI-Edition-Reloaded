
namespace NVGE
{
    partial class FormProgress
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProgress));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_ProgressText = new System.Windows.Forms.Label();
            this.label_Pos = new System.Windows.Forms.Label();
            this.button_Abort = new System.Windows.Forms.Button();
            this.backgroundWorker_Progress = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Video = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Delete = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Split = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker_Convert = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label_ProgressText
            // 
            resources.ApplyResources(this.label_ProgressText, "label_ProgressText");
            this.label_ProgressText.Name = "label_ProgressText";
            // 
            // label_Pos
            // 
            resources.ApplyResources(this.label_Pos, "label_Pos");
            this.label_Pos.Name = "label_Pos";
            // 
            // button_Abort
            // 
            resources.ApplyResources(this.button_Abort, "button_Abort");
            this.button_Abort.Name = "button_Abort";
            this.button_Abort.UseVisualStyleBackColor = true;
            this.button_Abort.Click += new System.EventHandler(this.Button_Abort_Click);
            // 
            // backgroundWorker_Progress
            // 
            this.backgroundWorker_Progress.WorkerReportsProgress = true;
            this.backgroundWorker_Progress.WorkerSupportsCancellation = true;
            this.backgroundWorker_Progress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Progress_DoWork);
            this.backgroundWorker_Progress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Progress_ProgressChanged);
            this.backgroundWorker_Progress.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Progress_RunWorkerCompleted);
            // 
            // backgroundWorker_Video
            // 
            this.backgroundWorker_Video.WorkerReportsProgress = true;
            this.backgroundWorker_Video.WorkerSupportsCancellation = true;
            this.backgroundWorker_Video.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Video_DoWork);
            // 
            // backgroundWorker_Delete
            // 
            this.backgroundWorker_Delete.WorkerReportsProgress = true;
            this.backgroundWorker_Delete.WorkerSupportsCancellation = true;
            this.backgroundWorker_Delete.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Delete_DoWork);
            // 
            // backgroundWorker_Split
            // 
            this.backgroundWorker_Split.WorkerReportsProgress = true;
            this.backgroundWorker_Split.WorkerSupportsCancellation = true;
            this.backgroundWorker_Split.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Split_DoWork);
            // 
            // backgroundWorker_Convert
            // 
            this.backgroundWorker_Convert.WorkerReportsProgress = true;
            this.backgroundWorker_Convert.WorkerSupportsCancellation = true;
            this.backgroundWorker_Convert.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_Convert_DoWork);
            this.backgroundWorker_Convert.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_Convert_ProgressChanged);
            this.backgroundWorker_Convert.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_Convert_RunWorkerCompleted);
            // 
            // FormProgress
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.button_Abort);
            this.Controls.Add(this.label_Pos);
            this.Controls.Add(this.label_ProgressText);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormProgress";
            this.Load += new System.EventHandler(this.FormProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_ProgressText;
        private System.Windows.Forms.Label label_Pos;
        private System.Windows.Forms.Button button_Abort;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Progress;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Video;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Delete;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Split;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Convert;
    }
}