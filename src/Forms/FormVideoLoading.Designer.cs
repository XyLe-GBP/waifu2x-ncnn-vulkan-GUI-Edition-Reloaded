namespace NVGE
{
    partial class FormVideoLoading
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
            label1 = new System.Windows.Forms.Label();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(14, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(211, 15);
            label1.TabIndex = 0;
            label1.Text = "Loading video. Please wait a moment...";
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(12, 36);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(215, 23);
            progressBar1.TabIndex = 1;
            // 
            // FormVideoLoading
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(239, 71);
            ControlBox = false;
            Controls.Add(progressBar1);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormVideoLoading";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Loading...";
            Load += FormVideoLoading_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}