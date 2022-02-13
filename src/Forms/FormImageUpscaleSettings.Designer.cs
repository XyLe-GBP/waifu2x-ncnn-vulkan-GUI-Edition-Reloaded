
namespace NVGE
{
    partial class FormImageUpscaleSettings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageUpscaleSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_Blocksize = new System.Windows.Forms.TextBox();
            this.comboBox_Rdlevel = new System.Windows.Forms.ComboBox();
            this.comboBox_GPU = new System.Windows.Forms.ComboBox();
            this.comboBox_Uplevel = new System.Windows.Forms.ComboBox();
            this.checkBox_Advanced = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_CMD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox_Format = new System.Windows.Forms.ComboBox();
            this.comboBox_Thread = new System.Windows.Forms.ComboBox();
            this.comboBox_Model = new System.Windows.Forms.ComboBox();
            this.checkBox_TTA = new System.Windows.Forms.CheckBox();
            this.checkBox_Verbose = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox_Blocksize);
            this.groupBox1.Controls.Add(this.comboBox_Rdlevel);
            this.groupBox1.Controls.Add(this.comboBox_GPU);
            this.groupBox1.Controls.Add(this.comboBox_Uplevel);
            this.groupBox1.Controls.Add(this.checkBox_Advanced);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textBox_Blocksize
            // 
            resources.ApplyResources(this.textBox_Blocksize, "textBox_Blocksize");
            this.textBox_Blocksize.Name = "textBox_Blocksize";
            this.toolTip1.SetToolTip(this.textBox_Blocksize, resources.GetString("textBox_Blocksize.ToolTip"));
            this.textBox_Blocksize.TextChanged += new System.EventHandler(this.TextBox_Blocksize_TextChanged);
            this.textBox_Blocksize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Blocksize_KeyPress);
            // 
            // comboBox_Rdlevel
            // 
            this.comboBox_Rdlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Rdlevel.FormattingEnabled = true;
            this.comboBox_Rdlevel.Items.AddRange(new object[] {
            resources.GetString("comboBox_Rdlevel.Items"),
            resources.GetString("comboBox_Rdlevel.Items1"),
            resources.GetString("comboBox_Rdlevel.Items2"),
            resources.GetString("comboBox_Rdlevel.Items3"),
            resources.GetString("comboBox_Rdlevel.Items4")});
            resources.ApplyResources(this.comboBox_Rdlevel, "comboBox_Rdlevel");
            this.comboBox_Rdlevel.Name = "comboBox_Rdlevel";
            this.toolTip1.SetToolTip(this.comboBox_Rdlevel, resources.GetString("comboBox_Rdlevel.ToolTip"));
            this.comboBox_Rdlevel.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Rdlevel_SelectedIndexChanged);
            // 
            // comboBox_GPU
            // 
            this.comboBox_GPU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GPU.FormattingEnabled = true;
            this.comboBox_GPU.Items.AddRange(new object[] {
            resources.GetString("comboBox_GPU.Items"),
            resources.GetString("comboBox_GPU.Items1"),
            resources.GetString("comboBox_GPU.Items2"),
            resources.GetString("comboBox_GPU.Items3"),
            resources.GetString("comboBox_GPU.Items4")});
            resources.ApplyResources(this.comboBox_GPU, "comboBox_GPU");
            this.comboBox_GPU.Name = "comboBox_GPU";
            this.toolTip1.SetToolTip(this.comboBox_GPU, resources.GetString("comboBox_GPU.ToolTip"));
            this.comboBox_GPU.SelectedIndexChanged += new System.EventHandler(this.ComboBox_GPU_SelectedIndexChanged);
            // 
            // comboBox_Uplevel
            // 
            this.comboBox_Uplevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Uplevel.FormattingEnabled = true;
            this.comboBox_Uplevel.Items.AddRange(new object[] {
            resources.GetString("comboBox_Uplevel.Items"),
            resources.GetString("comboBox_Uplevel.Items1"),
            resources.GetString("comboBox_Uplevel.Items2"),
            resources.GetString("comboBox_Uplevel.Items3")});
            resources.ApplyResources(this.comboBox_Uplevel, "comboBox_Uplevel");
            this.comboBox_Uplevel.Name = "comboBox_Uplevel";
            this.toolTip1.SetToolTip(this.comboBox_Uplevel, resources.GetString("comboBox_Uplevel.ToolTip"));
            this.comboBox_Uplevel.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Uplevel_SelectedIndexChanged);
            // 
            // checkBox_Advanced
            // 
            resources.ApplyResources(this.checkBox_Advanced, "checkBox_Advanced");
            this.checkBox_Advanced.Name = "checkBox_Advanced";
            this.toolTip1.SetToolTip(this.checkBox_Advanced, resources.GetString("checkBox_Advanced.ToolTip"));
            this.checkBox_Advanced.UseVisualStyleBackColor = true;
            this.checkBox_Advanced.CheckedChanged += new System.EventHandler(this.CheckBox_Advanced_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_CMD);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.comboBox_Format);
            this.groupBox2.Controls.Add(this.comboBox_Thread);
            this.groupBox2.Controls.Add(this.comboBox_Model);
            this.groupBox2.Controls.Add(this.checkBox_TTA);
            this.groupBox2.Controls.Add(this.checkBox_Verbose);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBox_CMD
            // 
            resources.ApplyResources(this.textBox_CMD, "textBox_CMD");
            this.textBox_CMD.Name = "textBox_CMD";
            this.toolTip1.SetToolTip(this.textBox_CMD, resources.GetString("textBox_CMD.ToolTip"));
            this.textBox_CMD.TextChanged += new System.EventHandler(this.TextBox_CMD_TextChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboBox_Format
            // 
            this.comboBox_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_Format, "comboBox_Format");
            this.comboBox_Format.FormattingEnabled = true;
            this.comboBox_Format.Items.AddRange(new object[] {
            resources.GetString("comboBox_Format.Items"),
            resources.GetString("comboBox_Format.Items1"),
            resources.GetString("comboBox_Format.Items2"),
            resources.GetString("comboBox_Format.Items3"),
            resources.GetString("comboBox_Format.Items4")});
            this.comboBox_Format.Name = "comboBox_Format";
            this.toolTip1.SetToolTip(this.comboBox_Format, resources.GetString("comboBox_Format.ToolTip"));
            this.comboBox_Format.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Format_SelectedIndexChanged);
            // 
            // comboBox_Thread
            // 
            this.comboBox_Thread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_Thread, "comboBox_Thread");
            this.comboBox_Thread.FormattingEnabled = true;
            this.comboBox_Thread.Items.AddRange(new object[] {
            resources.GetString("comboBox_Thread.Items"),
            resources.GetString("comboBox_Thread.Items1"),
            resources.GetString("comboBox_Thread.Items2"),
            resources.GetString("comboBox_Thread.Items3"),
            resources.GetString("comboBox_Thread.Items4")});
            this.comboBox_Thread.Name = "comboBox_Thread";
            this.toolTip1.SetToolTip(this.comboBox_Thread, resources.GetString("comboBox_Thread.ToolTip"));
            this.comboBox_Thread.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Thread_SelectedIndexChanged);
            // 
            // comboBox_Model
            // 
            this.comboBox_Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_Model, "comboBox_Model");
            this.comboBox_Model.FormattingEnabled = true;
            this.comboBox_Model.Items.AddRange(new object[] {
            resources.GetString("comboBox_Model.Items"),
            resources.GetString("comboBox_Model.Items1"),
            resources.GetString("comboBox_Model.Items2")});
            this.comboBox_Model.Name = "comboBox_Model";
            this.toolTip1.SetToolTip(this.comboBox_Model, resources.GetString("comboBox_Model.ToolTip"));
            this.comboBox_Model.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Model_SelectedIndexChanged);
            // 
            // checkBox_TTA
            // 
            resources.ApplyResources(this.checkBox_TTA, "checkBox_TTA");
            this.checkBox_TTA.Name = "checkBox_TTA";
            this.toolTip1.SetToolTip(this.checkBox_TTA, resources.GetString("checkBox_TTA.ToolTip"));
            this.checkBox_TTA.UseVisualStyleBackColor = true;
            this.checkBox_TTA.CheckedChanged += new System.EventHandler(this.CheckBox_TTA_CheckedChanged);
            // 
            // checkBox_Verbose
            // 
            resources.ApplyResources(this.checkBox_Verbose, "checkBox_Verbose");
            this.checkBox_Verbose.Name = "checkBox_Verbose";
            this.toolTip1.SetToolTip(this.checkBox_Verbose, resources.GetString("checkBox_Verbose.ToolTip"));
            this.checkBox_Verbose.UseVisualStyleBackColor = true;
            this.checkBox_Verbose.CheckedChanged += new System.EventHandler(this.CheckBox_Verbose_CheckedChanged);
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
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
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NVGE.Properties.Resources.waifu2x_api;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // FormImageUpscaleSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormImageUpscaleSettings";
            this.Load += new System.EventHandler(this.FormImageUpscaleSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Blocksize;
        private System.Windows.Forms.ComboBox comboBox_GPU;
        private System.Windows.Forms.ComboBox comboBox_Uplevel;
        private System.Windows.Forms.ComboBox comboBox_Rdlevel;
        private System.Windows.Forms.CheckBox checkBox_Advanced;
        private System.Windows.Forms.ComboBox comboBox_Format;
        private System.Windows.Forms.ComboBox comboBox_Thread;
        private System.Windows.Forms.ComboBox comboBox_Model;
        private System.Windows.Forms.CheckBox checkBox_TTA;
        private System.Windows.Forms.CheckBox checkBox_Verbose;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_CMD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}