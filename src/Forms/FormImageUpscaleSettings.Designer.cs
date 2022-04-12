
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
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_height = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_width = new System.Windows.Forms.TextBox();
            this.checkBox_pixel = new System.Windows.Forms.CheckBox();
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
            this.label12 = new System.Windows.Forms.Label();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBox_engine = new System.Windows.Forms.ComboBox();
            this.checkBox_updetail = new System.Windows.Forms.CheckBox();
            this.checkBox_destfolder = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox_syncgap = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip1.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // textBox_Blocksize
            // 
            resources.ApplyResources(this.textBox_Blocksize, "textBox_Blocksize");
            this.textBox_Blocksize.Name = "textBox_Blocksize";
            this.textBox_Blocksize.ShortcutsEnabled = false;
            this.toolTip1.SetToolTip(this.textBox_Blocksize, resources.GetString("textBox_Blocksize.ToolTip"));
            this.textBox_Blocksize.TextChanged += new System.EventHandler(this.TextBox_Blocksize_TextChanged);
            this.textBox_Blocksize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_Blocksize_KeyPress);
            // 
            // comboBox_Rdlevel
            // 
            resources.ApplyResources(this.comboBox_Rdlevel, "comboBox_Rdlevel");
            this.comboBox_Rdlevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Rdlevel.FormattingEnabled = true;
            this.comboBox_Rdlevel.Items.AddRange(new object[] {
            resources.GetString("comboBox_Rdlevel.Items"),
            resources.GetString("comboBox_Rdlevel.Items1"),
            resources.GetString("comboBox_Rdlevel.Items2"),
            resources.GetString("comboBox_Rdlevel.Items3"),
            resources.GetString("comboBox_Rdlevel.Items4")});
            this.comboBox_Rdlevel.Name = "comboBox_Rdlevel";
            this.toolTip1.SetToolTip(this.comboBox_Rdlevel, resources.GetString("comboBox_Rdlevel.ToolTip"));
            this.comboBox_Rdlevel.SelectedIndexChanged += new System.EventHandler(this.ComboBox_Rdlevel_SelectedIndexChanged);
            // 
            // comboBox_GPU
            // 
            resources.ApplyResources(this.comboBox_GPU, "comboBox_GPU");
            this.comboBox_GPU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GPU.FormattingEnabled = true;
            this.comboBox_GPU.Items.AddRange(new object[] {
            resources.GetString("comboBox_GPU.Items"),
            resources.GetString("comboBox_GPU.Items1"),
            resources.GetString("comboBox_GPU.Items2"),
            resources.GetString("comboBox_GPU.Items3"),
            resources.GetString("comboBox_GPU.Items4")});
            this.comboBox_GPU.Name = "comboBox_GPU";
            this.toolTip1.SetToolTip(this.comboBox_GPU, resources.GetString("comboBox_GPU.ToolTip"));
            this.comboBox_GPU.SelectedIndexChanged += new System.EventHandler(this.ComboBox_GPU_SelectedIndexChanged);
            // 
            // comboBox_Uplevel
            // 
            resources.ApplyResources(this.comboBox_Uplevel, "comboBox_Uplevel");
            this.comboBox_Uplevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Uplevel.FormattingEnabled = true;
            this.comboBox_Uplevel.Items.AddRange(new object[] {
            resources.GetString("comboBox_Uplevel.Items"),
            resources.GetString("comboBox_Uplevel.Items1"),
            resources.GetString("comboBox_Uplevel.Items2"),
            resources.GetString("comboBox_Uplevel.Items3"),
            resources.GetString("comboBox_Uplevel.Items4")});
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
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip1.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // textBox_height
            // 
            resources.ApplyResources(this.textBox_height, "textBox_height");
            this.textBox_height.Name = "textBox_height";
            this.toolTip1.SetToolTip(this.textBox_height, resources.GetString("textBox_height.ToolTip"));
            this.textBox_height.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_height_KeyPress);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip1.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // textBox_width
            // 
            resources.ApplyResources(this.textBox_width, "textBox_width");
            this.textBox_width.Name = "textBox_width";
            this.toolTip1.SetToolTip(this.textBox_width, resources.GetString("textBox_width.ToolTip"));
            this.textBox_width.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_width_KeyPress);
            // 
            // checkBox_pixel
            // 
            resources.ApplyResources(this.checkBox_pixel, "checkBox_pixel");
            this.checkBox_pixel.Name = "checkBox_pixel";
            this.toolTip1.SetToolTip(this.checkBox_pixel, resources.GetString("checkBox_pixel.ToolTip"));
            this.checkBox_pixel.UseVisualStyleBackColor = true;
            this.checkBox_pixel.CheckedChanged += new System.EventHandler(this.CheckBox_pixel_CheckedChanged);
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
            this.toolTip1.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // comboBox_Format
            // 
            resources.ApplyResources(this.comboBox_Format, "comboBox_Format");
            this.comboBox_Format.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            resources.ApplyResources(this.comboBox_Thread, "comboBox_Thread");
            this.comboBox_Thread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            resources.ApplyResources(this.comboBox_Model, "comboBox_Model");
            this.comboBox_Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
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
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip1.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // button_OK
            // 
            resources.ApplyResources(this.button_OK, "button_OK");
            this.button_OK.Name = "button_OK";
            this.toolTip1.SetToolTip(this.button_OK, resources.GetString("button_OK.ToolTip"));
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // button_Cancel
            // 
            resources.ApplyResources(this.button_Cancel, "button_Cancel");
            this.button_Cancel.Name = "button_Cancel";
            this.toolTip1.SetToolTip(this.button_Cancel, resources.GetString("button_Cancel.ToolTip"));
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // comboBox_engine
            // 
            resources.ApplyResources(this.comboBox_engine, "comboBox_engine");
            this.comboBox_engine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_engine.FormattingEnabled = true;
            this.comboBox_engine.Items.AddRange(new object[] {
            resources.GetString("comboBox_engine.Items"),
            resources.GetString("comboBox_engine.Items1"),
            resources.GetString("comboBox_engine.Items2")});
            this.comboBox_engine.Name = "comboBox_engine";
            this.toolTip1.SetToolTip(this.comboBox_engine, resources.GetString("comboBox_engine.ToolTip"));
            this.comboBox_engine.SelectedIndexChanged += new System.EventHandler(this.ComboBox_engine_SelectedIndexChanged);
            // 
            // checkBox_updetail
            // 
            resources.ApplyResources(this.checkBox_updetail, "checkBox_updetail");
            this.checkBox_updetail.Checked = true;
            this.checkBox_updetail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_updetail.Name = "checkBox_updetail";
            this.toolTip1.SetToolTip(this.checkBox_updetail, resources.GetString("checkBox_updetail.ToolTip"));
            this.checkBox_updetail.UseVisualStyleBackColor = true;
            // 
            // checkBox_destfolder
            // 
            resources.ApplyResources(this.checkBox_destfolder, "checkBox_destfolder");
            this.checkBox_destfolder.Checked = true;
            this.checkBox_destfolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_destfolder.Name = "checkBox_destfolder";
            this.toolTip1.SetToolTip(this.checkBox_destfolder, resources.GetString("checkBox_destfolder.ToolTip"));
            this.checkBox_destfolder.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabControl1, resources.GetString("tabControl1.ToolTip"));
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Controls.Add(this.comboBox_syncgap);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.checkBox_destfolder);
            this.tabPage1.Controls.Add(this.checkBox_updetail);
            this.tabPage1.Controls.Add(this.comboBox_engine);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.comboBox_Uplevel);
            this.tabPage1.Controls.Add(this.textBox_Blocksize);
            this.tabPage1.Controls.Add(this.comboBox_Rdlevel);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.comboBox_GPU);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Name = "tabPage1";
            this.toolTip1.SetToolTip(this.tabPage1, resources.GetString("tabPage1.ToolTip"));
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox_syncgap
            // 
            resources.ApplyResources(this.comboBox_syncgap, "comboBox_syncgap");
            this.comboBox_syncgap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_syncgap.FormattingEnabled = true;
            this.comboBox_syncgap.Items.AddRange(new object[] {
            resources.GetString("comboBox_syncgap.Items"),
            resources.GetString("comboBox_syncgap.Items1"),
            resources.GetString("comboBox_syncgap.Items2"),
            resources.GetString("comboBox_syncgap.Items3")});
            this.comboBox_syncgap.Name = "comboBox_syncgap";
            this.toolTip1.SetToolTip(this.comboBox_syncgap, resources.GetString("comboBox_syncgap.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip1.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip1.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // tabPage2
            // 
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Controls.Add(this.textBox_CMD);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.checkBox_Advanced);
            this.tabPage2.Controls.Add(this.comboBox_Thread);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.comboBox_Model);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.textBox_height);
            this.tabPage2.Controls.Add(this.comboBox_Format);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.checkBox_pixel);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.textBox_width);
            this.tabPage2.Controls.Add(this.checkBox_Verbose);
            this.tabPage2.Controls.Add(this.checkBox_TTA);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Name = "tabPage2";
            this.toolTip1.SetToolTip(this.tabPage2, resources.GetString("tabPage2.ToolTip"));
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FormImageUpscaleSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormImageUpscaleSettings";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.FormImageUpscaleSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
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
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_height;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_width;
        private System.Windows.Forms.CheckBox checkBox_pixel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBox_engine;
        private System.Windows.Forms.CheckBox checkBox_destfolder;
        private System.Windows.Forms.CheckBox checkBox_updetail;
        private System.Windows.Forms.ComboBox comboBox_syncgap;
        private System.Windows.Forms.Label label14;
    }
}