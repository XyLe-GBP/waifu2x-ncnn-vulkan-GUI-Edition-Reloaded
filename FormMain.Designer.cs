
namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.label1 = new System.Windows.Forms.Label();
            this.button_Image = new System.Windows.Forms.Button();
            this.button_Video = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImegeIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeFileCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upscalingSettingsUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoUpscalingSettingsVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeVideoResolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutWaifu2xncnnvulkanGUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_OS = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_Graphic = new System.Windows.Forms.Label();
            this.label_Processor = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Merge = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_Size = new System.Windows.Forms.Label();
            this.label_File = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button_Image
            // 
            resources.ApplyResources(this.button_Image, "button_Image");
            this.button_Image.Name = "button_Image";
            this.button_Image.UseVisualStyleBackColor = true;
            this.button_Image.Click += new System.EventHandler(this.Button_Image_Click);
            // 
            // button_Video
            // 
            resources.ApplyResources(this.button_Video, "button_Video");
            this.button_Video.Name = "button_Video";
            this.button_Video.UseVisualStyleBackColor = true;
            this.button_Video.Click += new System.EventHandler(this.Button_Video_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem,
            this.settingsCToolStripMenuItem,
            this.toolsTToolStripMenuItem,
            this.aboutAToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImegeIToolStripMenuItem,
            this.openVideoVToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeFileCToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitXToolStripMenuItem});
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            resources.ApplyResources(this.fileFToolStripMenuItem, "fileFToolStripMenuItem");
            // 
            // openImegeIToolStripMenuItem
            // 
            this.openImegeIToolStripMenuItem.Name = "openImegeIToolStripMenuItem";
            resources.ApplyResources(this.openImegeIToolStripMenuItem, "openImegeIToolStripMenuItem");
            this.openImegeIToolStripMenuItem.Click += new System.EventHandler(this.OpenImegeIToolStripMenuItem_Click);
            // 
            // openVideoVToolStripMenuItem
            // 
            this.openVideoVToolStripMenuItem.Name = "openVideoVToolStripMenuItem";
            resources.ApplyResources(this.openVideoVToolStripMenuItem, "openVideoVToolStripMenuItem");
            this.openVideoVToolStripMenuItem.Click += new System.EventHandler(this.OpenVideoVToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // closeFileCToolStripMenuItem
            // 
            resources.ApplyResources(this.closeFileCToolStripMenuItem, "closeFileCToolStripMenuItem");
            this.closeFileCToolStripMenuItem.Name = "closeFileCToolStripMenuItem";
            this.closeFileCToolStripMenuItem.Click += new System.EventHandler(this.CloseFileCToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // exitXToolStripMenuItem
            // 
            this.exitXToolStripMenuItem.Name = "exitXToolStripMenuItem";
            resources.ApplyResources(this.exitXToolStripMenuItem, "exitXToolStripMenuItem");
            this.exitXToolStripMenuItem.Click += new System.EventHandler(this.ExitXToolStripMenuItem_Click);
            // 
            // settingsCToolStripMenuItem
            // 
            this.settingsCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.upscalingSettingsUToolStripMenuItem,
            this.videoUpscalingSettingsVToolStripMenuItem});
            this.settingsCToolStripMenuItem.Name = "settingsCToolStripMenuItem";
            resources.ApplyResources(this.settingsCToolStripMenuItem, "settingsCToolStripMenuItem");
            // 
            // upscalingSettingsUToolStripMenuItem
            // 
            this.upscalingSettingsUToolStripMenuItem.Name = "upscalingSettingsUToolStripMenuItem";
            resources.ApplyResources(this.upscalingSettingsUToolStripMenuItem, "upscalingSettingsUToolStripMenuItem");
            this.upscalingSettingsUToolStripMenuItem.Click += new System.EventHandler(this.UpscalingSettingsUToolStripMenuItem_Click);
            // 
            // videoUpscalingSettingsVToolStripMenuItem
            // 
            this.videoUpscalingSettingsVToolStripMenuItem.Name = "videoUpscalingSettingsVToolStripMenuItem";
            resources.ApplyResources(this.videoUpscalingSettingsVToolStripMenuItem, "videoUpscalingSettingsVToolStripMenuItem");
            this.videoUpscalingSettingsVToolStripMenuItem.Click += new System.EventHandler(this.VideoUpscalingSettingsVToolStripMenuItem_Click);
            // 
            // toolsTToolStripMenuItem
            // 
            this.toolsTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeVideoResolutionToolStripMenuItem});
            this.toolsTToolStripMenuItem.Name = "toolsTToolStripMenuItem";
            resources.ApplyResources(this.toolsTToolStripMenuItem, "toolsTToolStripMenuItem");
            // 
            // changeVideoResolutionToolStripMenuItem
            // 
            this.changeVideoResolutionToolStripMenuItem.Name = "changeVideoResolutionToolStripMenuItem";
            resources.ApplyResources(this.changeVideoResolutionToolStripMenuItem, "changeVideoResolutionToolStripMenuItem");
            this.changeVideoResolutionToolStripMenuItem.Click += new System.EventHandler(this.ChangeVideoResolutionToolStripMenuItem_Click);
            // 
            // aboutAToolStripMenuItem
            // 
            this.aboutAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutWaifu2xncnnvulkanGUIToolStripMenuItem,
            this.toolStripMenuItem3,
            this.checkForUpdatesUToolStripMenuItem});
            this.aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
            resources.ApplyResources(this.aboutAToolStripMenuItem, "aboutAToolStripMenuItem");
            // 
            // aboutWaifu2xncnnvulkanGUIToolStripMenuItem
            // 
            this.aboutWaifu2xncnnvulkanGUIToolStripMenuItem.Name = "aboutWaifu2xncnnvulkanGUIToolStripMenuItem";
            resources.ApplyResources(this.aboutWaifu2xncnnvulkanGUIToolStripMenuItem, "aboutWaifu2xncnnvulkanGUIToolStripMenuItem");
            this.aboutWaifu2xncnnvulkanGUIToolStripMenuItem.Click += new System.EventHandler(this.AboutWaifu2xncnnvulkanGUIToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // checkForUpdatesUToolStripMenuItem
            // 
            this.checkForUpdatesUToolStripMenuItem.Name = "checkForUpdatesUToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesUToolStripMenuItem, "checkForUpdatesUToolStripMenuItem");
            this.checkForUpdatesUToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdatesUToolStripMenuItem_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_OS);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label_Graphic);
            this.groupBox1.Controls.Add(this.label_Processor);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label_OS
            // 
            resources.ApplyResources(this.label_OS, "label_OS");
            this.label_OS.Name = "label_OS";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label_Graphic
            // 
            resources.ApplyResources(this.label_Graphic, "label_Graphic");
            this.label_Graphic.Name = "label_Graphic";
            // 
            // label_Processor
            // 
            resources.ApplyResources(this.label_Processor, "label_Processor");
            this.label_Processor.Name = "label_Processor";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Merge);
            this.groupBox2.Controls.Add(this.button_Image);
            this.groupBox2.Controls.Add(this.button_Video);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // button_Merge
            // 
            resources.ApplyResources(this.button_Merge, "button_Merge");
            this.button_Merge.Name = "button_Merge";
            this.button_Merge.UseVisualStyleBackColor = true;
            this.button_Merge.Click += new System.EventHandler(this.Button_Merge_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_Size);
            this.groupBox3.Controls.Add(this.label_File);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label_Size
            // 
            resources.ApplyResources(this.label_Size, "label_Size");
            this.label_Size.Name = "label_Size";
            // 
            // label_File
            // 
            this.label_File.AutoEllipsis = true;
            resources.ApplyResources(this.label_File, "label_File");
            this.label_File.Name = "label_File";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_Status});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel_Status
            // 
            this.toolStripStatusLabel_Status.Name = "toolStripStatusLabel_Status";
            resources.ApplyResources(this.toolStripStatusLabel_Status, "toolStripStatusLabel_Status");
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_Image;
        private System.Windows.Forms.Button button_Video;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImegeIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVideoVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeFileCToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upscalingSettingsUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoUpscalingSettingsVToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_Graphic;
        private System.Windows.Forms.Label label_Processor;
        private System.Windows.Forms.ToolStripMenuItem toolsTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeVideoResolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutWaifu2xncnnvulkanGUIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesUToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_Size;
        private System.Windows.Forms.Label label_File;
        private System.Windows.Forms.Button button_Merge;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Status;
        private System.Windows.Forms.Label label_OS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

