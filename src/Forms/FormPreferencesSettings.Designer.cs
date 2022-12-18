namespace NVGE
{
    partial class FormPreferencesSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferencesSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button_browsecimg = new System.Windows.Forms.Button();
            this.textBox_imagepath = new System.Windows.Forms.TextBox();
            this.label_imagepath = new System.Windows.Forms.Label();
            this.checkBox_splashImage = new System.Windows.Forms.CheckBox();
            this.checkBox_checkupdate = new System.Windows.Forms.CheckBox();
            this.checkBox_checkupdateff = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.button_browsecimg);
            this.tabPage1.Controls.Add(this.textBox_imagepath);
            this.tabPage1.Controls.Add(this.label_imagepath);
            this.tabPage1.Controls.Add(this.checkBox_splashImage);
            this.tabPage1.Controls.Add(this.checkBox_checkupdate);
            this.tabPage1.Controls.Add(this.checkBox_checkupdateff);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button_browsecimg
            // 
            resources.ApplyResources(this.button_browsecimg, "button_browsecimg");
            this.button_browsecimg.Name = "button_browsecimg";
            this.button_browsecimg.UseVisualStyleBackColor = true;
            this.button_browsecimg.Click += new System.EventHandler(this.Button_browsecimg_Click);
            // 
            // textBox_imagepath
            // 
            resources.ApplyResources(this.textBox_imagepath, "textBox_imagepath");
            this.textBox_imagepath.Name = "textBox_imagepath";
            this.textBox_imagepath.ReadOnly = true;
            // 
            // label_imagepath
            // 
            resources.ApplyResources(this.label_imagepath, "label_imagepath");
            this.label_imagepath.Name = "label_imagepath";
            // 
            // checkBox_splashImage
            // 
            resources.ApplyResources(this.checkBox_splashImage, "checkBox_splashImage");
            this.checkBox_splashImage.Name = "checkBox_splashImage";
            this.checkBox_splashImage.UseVisualStyleBackColor = true;
            this.checkBox_splashImage.CheckedChanged += new System.EventHandler(this.CheckBox_splashImage_CheckedChanged);
            // 
            // checkBox_checkupdate
            // 
            resources.ApplyResources(this.checkBox_checkupdate, "checkBox_checkupdate");
            this.checkBox_checkupdate.Checked = true;
            this.checkBox_checkupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_checkupdate.Name = "checkBox_checkupdate";
            this.checkBox_checkupdate.UseVisualStyleBackColor = true;
            // 
            // checkBox_checkupdateff
            // 
            resources.ApplyResources(this.checkBox_checkupdateff, "checkBox_checkupdateff");
            this.checkBox_checkupdateff.Checked = true;
            this.checkBox_checkupdateff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_checkupdateff.Name = "checkBox_checkupdateff";
            this.checkBox_checkupdateff.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            resources.ApplyResources(this.button_OK, "button_OK");
            this.button_OK.Name = "button_OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // button_Cancel
            // 
            resources.ApplyResources(this.button_Cancel, "button_Cancel");
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // FormPreferencesSettings
            // 
            this.AcceptButton = this.button_OK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ControlBox = false;
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormPreferencesSettings";
            this.Load += new System.EventHandler(this.FormPreferencesSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkBox_checkupdate;
        private System.Windows.Forms.CheckBox checkBox_checkupdateff;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_browsecimg;
        private System.Windows.Forms.TextBox textBox_imagepath;
        private System.Windows.Forms.Label label_imagepath;
        private System.Windows.Forms.CheckBox checkBox_splashImage;
    }
}