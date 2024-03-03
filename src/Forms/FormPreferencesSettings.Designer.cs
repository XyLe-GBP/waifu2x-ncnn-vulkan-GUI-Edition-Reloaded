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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferencesSettings));
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            checkBox_hidesplash = new System.Windows.Forms.CheckBox();
            button_browsecimg = new System.Windows.Forms.Button();
            textBox_imagepath = new System.Windows.Forms.TextBox();
            label_imagepath = new System.Windows.Forms.Label();
            checkBox_splashImage = new System.Windows.Forms.CheckBox();
            checkBox_checkupdate = new System.Windows.Forms.CheckBox();
            checkBox_checkupdateff = new System.Windows.Forms.CheckBox();
            tabPage2 = new System.Windows.Forms.TabPage();
            checkBox_fs_dest = new System.Windows.Forms.CheckBox();
            groupBox_fspref = new System.Windows.Forms.GroupBox();
            button_fs_browsedest = new System.Windows.Forms.Button();
            textBox_fs_dest = new System.Windows.Forms.TextBox();
            label_fs_dest = new System.Windows.Forms.Label();
            groupBox_fs = new System.Windows.Forms.GroupBox();
            radioButton_fs_fixed = new System.Windows.Forms.RadioButton();
            radioButton_fs_every = new System.Windows.Forms.RadioButton();
            tabPage3 = new System.Windows.Forms.TabPage();
            groupBox_a_png = new System.Windows.Forms.GroupBox();
            radioButton_a_png_default = new System.Windows.Forms.RadioButton();
            radioButton_a_png_64 = new System.Windows.Forms.RadioButton();
            radioButton_a_png_48 = new System.Windows.Forms.RadioButton();
            radioButton_a_png_32 = new System.Windows.Forms.RadioButton();
            radioButton_a_png_24 = new System.Windows.Forms.RadioButton();
            radioButton_a_png_8 = new System.Windows.Forms.RadioButton();
            checkBox_a_png_enable = new System.Windows.Forms.CheckBox();
            button_OK = new System.Windows.Forms.Button();
            button_Cancel = new System.Windows.Forms.Button();
            toolTip_info = new System.Windows.Forms.ToolTip(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox_fspref.SuspendLayout();
            groupBox_fs.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox_a_png.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            toolTip_info.SetToolTip(tabControl1, resources.GetString("tabControl1.ToolTip"));
            // 
            // tabPage1
            // 
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Controls.Add(checkBox_hidesplash);
            tabPage1.Controls.Add(button_browsecimg);
            tabPage1.Controls.Add(textBox_imagepath);
            tabPage1.Controls.Add(label_imagepath);
            tabPage1.Controls.Add(checkBox_splashImage);
            tabPage1.Controls.Add(checkBox_checkupdate);
            tabPage1.Controls.Add(checkBox_checkupdateff);
            tabPage1.Name = "tabPage1";
            toolTip_info.SetToolTip(tabPage1, resources.GetString("tabPage1.ToolTip"));
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_hidesplash
            // 
            resources.ApplyResources(checkBox_hidesplash, "checkBox_hidesplash");
            checkBox_hidesplash.Name = "checkBox_hidesplash";
            toolTip_info.SetToolTip(checkBox_hidesplash, resources.GetString("checkBox_hidesplash.ToolTip"));
            checkBox_hidesplash.UseVisualStyleBackColor = true;
            // 
            // button_browsecimg
            // 
            resources.ApplyResources(button_browsecimg, "button_browsecimg");
            button_browsecimg.Name = "button_browsecimg";
            toolTip_info.SetToolTip(button_browsecimg, resources.GetString("button_browsecimg.ToolTip"));
            button_browsecimg.UseVisualStyleBackColor = true;
            button_browsecimg.Click += Button_browsecimg_Click;
            // 
            // textBox_imagepath
            // 
            resources.ApplyResources(textBox_imagepath, "textBox_imagepath");
            textBox_imagepath.Name = "textBox_imagepath";
            textBox_imagepath.ReadOnly = true;
            toolTip_info.SetToolTip(textBox_imagepath, resources.GetString("textBox_imagepath.ToolTip"));
            // 
            // label_imagepath
            // 
            resources.ApplyResources(label_imagepath, "label_imagepath");
            label_imagepath.Name = "label_imagepath";
            toolTip_info.SetToolTip(label_imagepath, resources.GetString("label_imagepath.ToolTip"));
            // 
            // checkBox_splashImage
            // 
            resources.ApplyResources(checkBox_splashImage, "checkBox_splashImage");
            checkBox_splashImage.Name = "checkBox_splashImage";
            toolTip_info.SetToolTip(checkBox_splashImage, resources.GetString("checkBox_splashImage.ToolTip"));
            checkBox_splashImage.UseVisualStyleBackColor = true;
            checkBox_splashImage.CheckedChanged += CheckBox_splashImage_CheckedChanged;
            // 
            // checkBox_checkupdate
            // 
            resources.ApplyResources(checkBox_checkupdate, "checkBox_checkupdate");
            checkBox_checkupdate.Checked = true;
            checkBox_checkupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_checkupdate.Name = "checkBox_checkupdate";
            toolTip_info.SetToolTip(checkBox_checkupdate, resources.GetString("checkBox_checkupdate.ToolTip"));
            checkBox_checkupdate.UseVisualStyleBackColor = true;
            // 
            // checkBox_checkupdateff
            // 
            resources.ApplyResources(checkBox_checkupdateff, "checkBox_checkupdateff");
            checkBox_checkupdateff.Checked = true;
            checkBox_checkupdateff.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_checkupdateff.Name = "checkBox_checkupdateff";
            toolTip_info.SetToolTip(checkBox_checkupdateff, resources.GetString("checkBox_checkupdateff.ToolTip"));
            checkBox_checkupdateff.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Controls.Add(checkBox_fs_dest);
            tabPage2.Controls.Add(groupBox_fspref);
            tabPage2.Controls.Add(groupBox_fs);
            tabPage2.Name = "tabPage2";
            toolTip_info.SetToolTip(tabPage2, resources.GetString("tabPage2.ToolTip"));
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox_fs_dest
            // 
            resources.ApplyResources(checkBox_fs_dest, "checkBox_fs_dest");
            checkBox_fs_dest.Checked = true;
            checkBox_fs_dest.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_fs_dest.Name = "checkBox_fs_dest";
            toolTip_info.SetToolTip(checkBox_fs_dest, resources.GetString("checkBox_fs_dest.ToolTip"));
            checkBox_fs_dest.UseVisualStyleBackColor = true;
            // 
            // groupBox_fspref
            // 
            resources.ApplyResources(groupBox_fspref, "groupBox_fspref");
            groupBox_fspref.Controls.Add(button_fs_browsedest);
            groupBox_fspref.Controls.Add(textBox_fs_dest);
            groupBox_fspref.Controls.Add(label_fs_dest);
            groupBox_fspref.Name = "groupBox_fspref";
            groupBox_fspref.TabStop = false;
            toolTip_info.SetToolTip(groupBox_fspref, resources.GetString("groupBox_fspref.ToolTip"));
            // 
            // button_fs_browsedest
            // 
            resources.ApplyResources(button_fs_browsedest, "button_fs_browsedest");
            button_fs_browsedest.Name = "button_fs_browsedest";
            toolTip_info.SetToolTip(button_fs_browsedest, resources.GetString("button_fs_browsedest.ToolTip"));
            button_fs_browsedest.UseVisualStyleBackColor = true;
            button_fs_browsedest.Click += button_fs_browsedest_Click;
            // 
            // textBox_fs_dest
            // 
            resources.ApplyResources(textBox_fs_dest, "textBox_fs_dest");
            textBox_fs_dest.Name = "textBox_fs_dest";
            textBox_fs_dest.ReadOnly = true;
            toolTip_info.SetToolTip(textBox_fs_dest, resources.GetString("textBox_fs_dest.ToolTip"));
            // 
            // label_fs_dest
            // 
            resources.ApplyResources(label_fs_dest, "label_fs_dest");
            label_fs_dest.Name = "label_fs_dest";
            toolTip_info.SetToolTip(label_fs_dest, resources.GetString("label_fs_dest.ToolTip"));
            // 
            // groupBox_fs
            // 
            resources.ApplyResources(groupBox_fs, "groupBox_fs");
            groupBox_fs.Controls.Add(radioButton_fs_fixed);
            groupBox_fs.Controls.Add(radioButton_fs_every);
            groupBox_fs.Name = "groupBox_fs";
            groupBox_fs.TabStop = false;
            toolTip_info.SetToolTip(groupBox_fs, resources.GetString("groupBox_fs.ToolTip"));
            // 
            // radioButton_fs_fixed
            // 
            resources.ApplyResources(radioButton_fs_fixed, "radioButton_fs_fixed");
            radioButton_fs_fixed.Name = "radioButton_fs_fixed";
            toolTip_info.SetToolTip(radioButton_fs_fixed, resources.GetString("radioButton_fs_fixed.ToolTip"));
            radioButton_fs_fixed.UseVisualStyleBackColor = true;
            radioButton_fs_fixed.CheckedChanged += radioButton_fs_fixed_CheckedChanged;
            // 
            // radioButton_fs_every
            // 
            resources.ApplyResources(radioButton_fs_every, "radioButton_fs_every");
            radioButton_fs_every.Checked = true;
            radioButton_fs_every.Name = "radioButton_fs_every";
            radioButton_fs_every.TabStop = true;
            toolTip_info.SetToolTip(radioButton_fs_every, resources.GetString("radioButton_fs_every.ToolTip"));
            radioButton_fs_every.UseVisualStyleBackColor = true;
            radioButton_fs_every.CheckedChanged += radioButton_fs_every_CheckedChanged;
            // 
            // tabPage3
            // 
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Controls.Add(groupBox_a_png);
            tabPage3.Name = "tabPage3";
            toolTip_info.SetToolTip(tabPage3, resources.GetString("tabPage3.ToolTip"));
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox_a_png
            // 
            resources.ApplyResources(groupBox_a_png, "groupBox_a_png");
            groupBox_a_png.Controls.Add(radioButton_a_png_default);
            groupBox_a_png.Controls.Add(radioButton_a_png_64);
            groupBox_a_png.Controls.Add(radioButton_a_png_48);
            groupBox_a_png.Controls.Add(radioButton_a_png_32);
            groupBox_a_png.Controls.Add(radioButton_a_png_24);
            groupBox_a_png.Controls.Add(radioButton_a_png_8);
            groupBox_a_png.Controls.Add(checkBox_a_png_enable);
            groupBox_a_png.Name = "groupBox_a_png";
            groupBox_a_png.TabStop = false;
            toolTip_info.SetToolTip(groupBox_a_png, resources.GetString("groupBox_a_png.ToolTip"));
            // 
            // radioButton_a_png_default
            // 
            resources.ApplyResources(radioButton_a_png_default, "radioButton_a_png_default");
            radioButton_a_png_default.Checked = true;
            radioButton_a_png_default.Name = "radioButton_a_png_default";
            radioButton_a_png_default.TabStop = true;
            toolTip_info.SetToolTip(radioButton_a_png_default, resources.GetString("radioButton_a_png_default.ToolTip"));
            radioButton_a_png_default.UseVisualStyleBackColor = true;
            // 
            // radioButton_a_png_64
            // 
            resources.ApplyResources(radioButton_a_png_64, "radioButton_a_png_64");
            radioButton_a_png_64.Name = "radioButton_a_png_64";
            toolTip_info.SetToolTip(radioButton_a_png_64, resources.GetString("radioButton_a_png_64.ToolTip"));
            radioButton_a_png_64.UseVisualStyleBackColor = true;
            // 
            // radioButton_a_png_48
            // 
            resources.ApplyResources(radioButton_a_png_48, "radioButton_a_png_48");
            radioButton_a_png_48.Name = "radioButton_a_png_48";
            toolTip_info.SetToolTip(radioButton_a_png_48, resources.GetString("radioButton_a_png_48.ToolTip"));
            radioButton_a_png_48.UseVisualStyleBackColor = true;
            // 
            // radioButton_a_png_32
            // 
            resources.ApplyResources(radioButton_a_png_32, "radioButton_a_png_32");
            radioButton_a_png_32.Name = "radioButton_a_png_32";
            toolTip_info.SetToolTip(radioButton_a_png_32, resources.GetString("radioButton_a_png_32.ToolTip"));
            radioButton_a_png_32.UseVisualStyleBackColor = true;
            // 
            // radioButton_a_png_24
            // 
            resources.ApplyResources(radioButton_a_png_24, "radioButton_a_png_24");
            radioButton_a_png_24.Name = "radioButton_a_png_24";
            toolTip_info.SetToolTip(radioButton_a_png_24, resources.GetString("radioButton_a_png_24.ToolTip"));
            radioButton_a_png_24.UseVisualStyleBackColor = true;
            // 
            // radioButton_a_png_8
            // 
            resources.ApplyResources(radioButton_a_png_8, "radioButton_a_png_8");
            radioButton_a_png_8.Name = "radioButton_a_png_8";
            toolTip_info.SetToolTip(radioButton_a_png_8, resources.GetString("radioButton_a_png_8.ToolTip"));
            radioButton_a_png_8.UseVisualStyleBackColor = true;
            // 
            // checkBox_a_png_enable
            // 
            resources.ApplyResources(checkBox_a_png_enable, "checkBox_a_png_enable");
            checkBox_a_png_enable.Name = "checkBox_a_png_enable";
            toolTip_info.SetToolTip(checkBox_a_png_enable, resources.GetString("checkBox_a_png_enable.ToolTip"));
            checkBox_a_png_enable.UseVisualStyleBackColor = true;
            checkBox_a_png_enable.CheckedChanged += checkBox_a_png_enable_CheckedChanged;
            // 
            // button_OK
            // 
            resources.ApplyResources(button_OK, "button_OK");
            button_OK.Name = "button_OK";
            toolTip_info.SetToolTip(button_OK, resources.GetString("button_OK.ToolTip"));
            button_OK.UseVisualStyleBackColor = true;
            button_OK.Click += Button_OK_Click;
            // 
            // button_Cancel
            // 
            resources.ApplyResources(button_Cancel, "button_Cancel");
            button_Cancel.Name = "button_Cancel";
            toolTip_info.SetToolTip(button_Cancel, resources.GetString("button_Cancel.ToolTip"));
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += Button_Cancel_Click;
            // 
            // FormPreferencesSettings
            // 
            AcceptButton = button_OK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button_Cancel;
            ControlBox = false;
            Controls.Add(button_Cancel);
            Controls.Add(button_OK);
            Controls.Add(tabControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormPreferencesSettings";
            toolTip_info.SetToolTip(this, resources.GetString("$this.ToolTip"));
            Load += FormPreferencesSettings_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBox_fspref.ResumeLayout(false);
            groupBox_fspref.PerformLayout();
            groupBox_fs.ResumeLayout(false);
            groupBox_fs.PerformLayout();
            tabPage3.ResumeLayout(false);
            groupBox_a_png.ResumeLayout(false);
            groupBox_a_png.PerformLayout();
            ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox checkBox_hidesplash;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox_fspref;
        private System.Windows.Forms.GroupBox groupBox_fs;
        private System.Windows.Forms.RadioButton radioButton_fs_fixed;
        private System.Windows.Forms.RadioButton radioButton_fs_every;
        private System.Windows.Forms.CheckBox checkBox_fs_dest;
        private System.Windows.Forms.Button button_fs_browsedest;
        private System.Windows.Forms.TextBox textBox_fs_dest;
        private System.Windows.Forms.Label label_fs_dest;
        private System.Windows.Forms.GroupBox groupBox_a_png;
        private System.Windows.Forms.RadioButton radioButton_a_png_24;
        private System.Windows.Forms.RadioButton radioButton_a_png_8;
        private System.Windows.Forms.CheckBox checkBox_a_png_enable;
        private System.Windows.Forms.RadioButton radioButton_a_png_32;
        private System.Windows.Forms.RadioButton radioButton_a_png_64;
        private System.Windows.Forms.RadioButton radioButton_a_png_48;
        private System.Windows.Forms.ToolTip toolTip_info;
        private System.Windows.Forms.RadioButton radioButton_a_png_default;
    }
}