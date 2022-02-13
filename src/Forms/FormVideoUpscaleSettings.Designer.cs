
namespace NVGE
{
    partial class FormVideoUpscaleSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVideoUpscaleSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Advanced = new System.Windows.Forms.CheckBox();
            this.button_FFmpeg_path = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_FFmpeg = new System.Windows.Forms.TextBox();
            this.textBox_FPS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.checkBox_VOT = new System.Windows.Forms.CheckBox();
            this.comboBox_VOT = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBox_preset = new System.Windows.Forms.ComboBox();
            this.checkBox_preset = new System.Windows.Forms.CheckBox();
            this.comboBox_AE = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox_AE = new System.Windows.Forms.CheckBox();
            this.checkBox_AB = new System.Windows.Forms.CheckBox();
            this.textBox_CMDF = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox_CMDA = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox_MAC = new System.Windows.Forms.CheckBox();
            this.checkBox_MVC = new System.Windows.Forms.CheckBox();
            this.button_AL = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button_FC = new System.Windows.Forms.Button();
            this.button_VL = new System.Windows.Forms.Button();
            this.textBox_AL = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_VL = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_AB = new System.Windows.Forms.TextBox();
            this.checkBox_MSOL = new System.Windows.Forms.CheckBox();
            this.textBox_ACodec = new System.Windows.Forms.TextBox();
            this.textBox_CMDV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_VCodec = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox_MI = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox_CC = new System.Windows.Forms.CheckBox();
            this.checkBox_OAO = new System.Windows.Forms.CheckBox();
            this.checkBox_DS = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_DSS = new System.Windows.Forms.CheckBox();
            this.checkBox_SO = new System.Windows.Forms.CheckBox();
            this.textBox_CRF = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBox_EIA = new System.Windows.Forms.CheckBox();
            this.checkBox_OOF = new System.Windows.Forms.CheckBox();
            this.checkBox_HI = new System.Windows.Forms.CheckBox();
            this.checkBox_NVENC = new System.Windows.Forms.CheckBox();
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
            this.groupBox1.Controls.Add(this.checkBox_Advanced);
            this.groupBox1.Controls.Add(this.button_FFmpeg_path);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_FFmpeg);
            this.groupBox1.Controls.Add(this.textBox_FPS);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBox_Advanced
            // 
            resources.ApplyResources(this.checkBox_Advanced, "checkBox_Advanced");
            this.checkBox_Advanced.Name = "checkBox_Advanced";
            this.toolTip1.SetToolTip(this.checkBox_Advanced, resources.GetString("checkBox_Advanced.ToolTip"));
            this.checkBox_Advanced.UseVisualStyleBackColor = true;
            this.checkBox_Advanced.CheckedChanged += new System.EventHandler(this.CheckBox_Advanced_CheckedChanged);
            // 
            // button_FFmpeg_path
            // 
            resources.ApplyResources(this.button_FFmpeg_path, "button_FFmpeg_path");
            this.button_FFmpeg_path.Name = "button_FFmpeg_path";
            this.button_FFmpeg_path.UseVisualStyleBackColor = true;
            this.button_FFmpeg_path.Click += new System.EventHandler(this.Button_FFmpeg_path_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // textBox_FFmpeg
            // 
            resources.ApplyResources(this.textBox_FFmpeg, "textBox_FFmpeg");
            this.textBox_FFmpeg.Name = "textBox_FFmpeg";
            this.textBox_FFmpeg.ReadOnly = true;
            this.toolTip1.SetToolTip(this.textBox_FFmpeg, resources.GetString("textBox_FFmpeg.ToolTip"));
            this.textBox_FFmpeg.TextChanged += new System.EventHandler(this.TextBox_FFmpeg_TextChanged);
            // 
            // textBox_FPS
            // 
            resources.ApplyResources(this.textBox_FPS, "textBox_FPS");
            this.textBox_FPS.Name = "textBox_FPS";
            this.toolTip1.SetToolTip(this.textBox_FPS, resources.GetString("textBox_FPS.ToolTip"));
            this.textBox_FPS.TextChanged += new System.EventHandler(this.TextBox_FPS_TextChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.checkBox_VOT);
            this.groupBox2.Controls.Add(this.comboBox_VOT);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.comboBox_preset);
            this.groupBox2.Controls.Add(this.checkBox_preset);
            this.groupBox2.Controls.Add(this.comboBox_AE);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.checkBox_AE);
            this.groupBox2.Controls.Add(this.checkBox_AB);
            this.groupBox2.Controls.Add(this.textBox_CMDF);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBox_CMDA);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.checkBox_MAC);
            this.groupBox2.Controls.Add(this.checkBox_MVC);
            this.groupBox2.Controls.Add(this.button_AL);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button_FC);
            this.groupBox2.Controls.Add(this.button_VL);
            this.groupBox2.Controls.Add(this.textBox_AL);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textBox_VL);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox_AB);
            this.groupBox2.Controls.Add(this.checkBox_MSOL);
            this.groupBox2.Controls.Add(this.textBox_ACodec);
            this.groupBox2.Controls.Add(this.textBox_CMDV);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_VCodec);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.checkBox_MI);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.checkBox_CC);
            this.groupBox2.Controls.Add(this.checkBox_OAO);
            this.groupBox2.Controls.Add(this.checkBox_DS);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBox_DSS);
            this.groupBox2.Controls.Add(this.checkBox_SO);
            this.groupBox2.Controls.Add(this.textBox_CRF);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.checkBox_EIA);
            this.groupBox2.Controls.Add(this.checkBox_OOF);
            this.groupBox2.Controls.Add(this.checkBox_HI);
            this.groupBox2.Controls.Add(this.checkBox_NVENC);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // checkBox_VOT
            // 
            resources.ApplyResources(this.checkBox_VOT, "checkBox_VOT");
            this.checkBox_VOT.Name = "checkBox_VOT";
            this.toolTip1.SetToolTip(this.checkBox_VOT, resources.GetString("checkBox_VOT.ToolTip"));
            this.checkBox_VOT.UseVisualStyleBackColor = true;
            this.checkBox_VOT.CheckedChanged += new System.EventHandler(this.CheckBox_VOT_CheckedChanged);
            // 
            // comboBox_VOT
            // 
            this.comboBox_VOT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_VOT, "comboBox_VOT");
            this.comboBox_VOT.FormattingEnabled = true;
            this.comboBox_VOT.Items.AddRange(new object[] {
            resources.GetString("comboBox_VOT.Items"),
            resources.GetString("comboBox_VOT.Items1")});
            this.comboBox_VOT.Name = "comboBox_VOT";
            this.toolTip1.SetToolTip(this.comboBox_VOT, resources.GetString("comboBox_VOT.ToolTip"));
            this.comboBox_VOT.SelectedIndexChanged += new System.EventHandler(this.ComboBox_VOT_SelectedIndexChanged);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // comboBox_preset
            // 
            this.comboBox_preset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_preset, "comboBox_preset");
            this.comboBox_preset.FormattingEnabled = true;
            this.comboBox_preset.Items.AddRange(new object[] {
            resources.GetString("comboBox_preset.Items"),
            resources.GetString("comboBox_preset.Items1"),
            resources.GetString("comboBox_preset.Items2"),
            resources.GetString("comboBox_preset.Items3"),
            resources.GetString("comboBox_preset.Items4"),
            resources.GetString("comboBox_preset.Items5"),
            resources.GetString("comboBox_preset.Items6"),
            resources.GetString("comboBox_preset.Items7"),
            resources.GetString("comboBox_preset.Items8")});
            this.comboBox_preset.Name = "comboBox_preset";
            this.toolTip1.SetToolTip(this.comboBox_preset, resources.GetString("comboBox_preset.ToolTip"));
            this.comboBox_preset.SelectedIndexChanged += new System.EventHandler(this.ComboBox_preset_SelectedIndexChanged);
            // 
            // checkBox_preset
            // 
            resources.ApplyResources(this.checkBox_preset, "checkBox_preset");
            this.checkBox_preset.Name = "checkBox_preset";
            this.checkBox_preset.UseVisualStyleBackColor = true;
            this.checkBox_preset.CheckedChanged += new System.EventHandler(this.CheckBox_preset_CheckedChanged);
            // 
            // comboBox_AE
            // 
            this.comboBox_AE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_AE, "comboBox_AE");
            this.comboBox_AE.FormattingEnabled = true;
            this.comboBox_AE.Items.AddRange(new object[] {
            resources.GetString("comboBox_AE.Items"),
            resources.GetString("comboBox_AE.Items1"),
            resources.GetString("comboBox_AE.Items2"),
            resources.GetString("comboBox_AE.Items3"),
            resources.GetString("comboBox_AE.Items4")});
            this.comboBox_AE.Name = "comboBox_AE";
            this.comboBox_AE.SelectedIndexChanged += new System.EventHandler(this.ComboBox_AE_SelectedIndexChanged);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // checkBox_AE
            // 
            resources.ApplyResources(this.checkBox_AE, "checkBox_AE");
            this.checkBox_AE.Name = "checkBox_AE";
            this.checkBox_AE.UseVisualStyleBackColor = true;
            this.checkBox_AE.CheckedChanged += new System.EventHandler(this.CheckBox_AE_CheckedChanged);
            // 
            // checkBox_AB
            // 
            resources.ApplyResources(this.checkBox_AB, "checkBox_AB");
            this.checkBox_AB.Name = "checkBox_AB";
            this.checkBox_AB.UseVisualStyleBackColor = true;
            this.checkBox_AB.CheckedChanged += new System.EventHandler(this.CheckBox_AB_CheckedChanged);
            // 
            // textBox_CMDF
            // 
            resources.ApplyResources(this.textBox_CMDF, "textBox_CMDF");
            this.textBox_CMDF.Name = "textBox_CMDF";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // textBox_CMDA
            // 
            resources.ApplyResources(this.textBox_CMDA, "textBox_CMDA");
            this.textBox_CMDA.Name = "textBox_CMDA";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // checkBox_MAC
            // 
            resources.ApplyResources(this.checkBox_MAC, "checkBox_MAC");
            this.checkBox_MAC.Name = "checkBox_MAC";
            this.checkBox_MAC.UseVisualStyleBackColor = true;
            this.checkBox_MAC.CheckedChanged += new System.EventHandler(this.CheckBox_MAC_CheckedChanged);
            // 
            // checkBox_MVC
            // 
            resources.ApplyResources(this.checkBox_MVC, "checkBox_MVC");
            this.checkBox_MVC.Name = "checkBox_MVC";
            this.checkBox_MVC.UseVisualStyleBackColor = true;
            this.checkBox_MVC.CheckedChanged += new System.EventHandler(this.CheckBox_MVC_CheckedChanged);
            // 
            // button_AL
            // 
            resources.ApplyResources(this.button_AL, "button_AL");
            this.button_AL.Name = "button_AL";
            this.button_AL.UseVisualStyleBackColor = true;
            this.button_AL.Click += new System.EventHandler(this.Button_AL_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // button_FC
            // 
            resources.ApplyResources(this.button_FC, "button_FC");
            this.button_FC.Name = "button_FC";
            this.button_FC.UseVisualStyleBackColor = true;
            // 
            // button_VL
            // 
            resources.ApplyResources(this.button_VL, "button_VL");
            this.button_VL.Name = "button_VL";
            this.button_VL.UseVisualStyleBackColor = true;
            this.button_VL.Click += new System.EventHandler(this.Button_VL_Click);
            // 
            // textBox_AL
            // 
            resources.ApplyResources(this.textBox_AL, "textBox_AL");
            this.textBox_AL.Name = "textBox_AL";
            this.textBox_AL.ReadOnly = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // textBox_VL
            // 
            resources.ApplyResources(this.textBox_VL, "textBox_VL");
            this.textBox_VL.Name = "textBox_VL";
            this.textBox_VL.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // textBox_AB
            // 
            resources.ApplyResources(this.textBox_AB, "textBox_AB");
            this.textBox_AB.Name = "textBox_AB";
            this.textBox_AB.TextChanged += new System.EventHandler(this.TextBox_AB_TextChanged);
            // 
            // checkBox_MSOL
            // 
            resources.ApplyResources(this.checkBox_MSOL, "checkBox_MSOL");
            this.checkBox_MSOL.Name = "checkBox_MSOL";
            this.checkBox_MSOL.UseVisualStyleBackColor = true;
            this.checkBox_MSOL.CheckedChanged += new System.EventHandler(this.CheckBox_MSOL_CheckedChanged);
            // 
            // textBox_ACodec
            // 
            resources.ApplyResources(this.textBox_ACodec, "textBox_ACodec");
            this.textBox_ACodec.Name = "textBox_ACodec";
            this.toolTip1.SetToolTip(this.textBox_ACodec, resources.GetString("textBox_ACodec.ToolTip"));
            this.textBox_ACodec.TextChanged += new System.EventHandler(this.TextBox_ACodec_TextChanged);
            // 
            // textBox_CMDV
            // 
            resources.ApplyResources(this.textBox_CMDV, "textBox_CMDV");
            this.textBox_CMDV.Name = "textBox_CMDV";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textBox_VCodec
            // 
            resources.ApplyResources(this.textBox_VCodec, "textBox_VCodec");
            this.textBox_VCodec.Name = "textBox_VCodec";
            this.toolTip1.SetToolTip(this.textBox_VCodec, resources.GetString("textBox_VCodec.ToolTip"));
            this.textBox_VCodec.TextChanged += new System.EventHandler(this.TextBox_VCodec_TextChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // checkBox_MI
            // 
            resources.ApplyResources(this.checkBox_MI, "checkBox_MI");
            this.checkBox_MI.Name = "checkBox_MI";
            this.checkBox_MI.UseVisualStyleBackColor = true;
            this.checkBox_MI.CheckedChanged += new System.EventHandler(this.CheckBox_MI_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBox_CC
            // 
            resources.ApplyResources(this.checkBox_CC, "checkBox_CC");
            this.checkBox_CC.Name = "checkBox_CC";
            this.checkBox_CC.UseVisualStyleBackColor = true;
            this.checkBox_CC.CheckedChanged += new System.EventHandler(this.CheckBox_CC_CheckedChanged);
            // 
            // checkBox_OAO
            // 
            resources.ApplyResources(this.checkBox_OAO, "checkBox_OAO");
            this.checkBox_OAO.Name = "checkBox_OAO";
            this.toolTip1.SetToolTip(this.checkBox_OAO, resources.GetString("checkBox_OAO.ToolTip"));
            this.checkBox_OAO.UseVisualStyleBackColor = true;
            this.checkBox_OAO.CheckedChanged += new System.EventHandler(this.CheckBox_OAO_CheckedChanged);
            // 
            // checkBox_DS
            // 
            resources.ApplyResources(this.checkBox_DS, "checkBox_DS");
            this.checkBox_DS.Name = "checkBox_DS";
            this.checkBox_DS.UseVisualStyleBackColor = true;
            this.checkBox_DS.CheckedChanged += new System.EventHandler(this.CheckBox_DS_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // checkBox_DSS
            // 
            resources.ApplyResources(this.checkBox_DSS, "checkBox_DSS");
            this.checkBox_DSS.Name = "checkBox_DSS";
            this.toolTip1.SetToolTip(this.checkBox_DSS, resources.GetString("checkBox_DSS.ToolTip"));
            this.checkBox_DSS.UseVisualStyleBackColor = true;
            this.checkBox_DSS.CheckedChanged += new System.EventHandler(this.CheckBox_DSS_CheckedChanged);
            // 
            // checkBox_SO
            // 
            resources.ApplyResources(this.checkBox_SO, "checkBox_SO");
            this.checkBox_SO.Name = "checkBox_SO";
            this.toolTip1.SetToolTip(this.checkBox_SO, resources.GetString("checkBox_SO.ToolTip"));
            this.checkBox_SO.UseVisualStyleBackColor = true;
            this.checkBox_SO.CheckedChanged += new System.EventHandler(this.CheckBox_SO_CheckedChanged);
            // 
            // textBox_CRF
            // 
            resources.ApplyResources(this.textBox_CRF, "textBox_CRF");
            this.textBox_CRF.Name = "textBox_CRF";
            this.textBox_CRF.TextChanged += new System.EventHandler(this.TextBox_CRF_TextChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // checkBox_EIA
            // 
            resources.ApplyResources(this.checkBox_EIA, "checkBox_EIA");
            this.checkBox_EIA.Name = "checkBox_EIA";
            this.toolTip1.SetToolTip(this.checkBox_EIA, resources.GetString("checkBox_EIA.ToolTip"));
            this.checkBox_EIA.UseVisualStyleBackColor = true;
            this.checkBox_EIA.CheckedChanged += new System.EventHandler(this.CheckBox_EIA_CheckedChanged);
            // 
            // checkBox_OOF
            // 
            resources.ApplyResources(this.checkBox_OOF, "checkBox_OOF");
            this.checkBox_OOF.Name = "checkBox_OOF";
            this.checkBox_OOF.UseVisualStyleBackColor = true;
            this.checkBox_OOF.CheckedChanged += new System.EventHandler(this.CheckBox_OOF_CheckedChanged);
            // 
            // checkBox_HI
            // 
            resources.ApplyResources(this.checkBox_HI, "checkBox_HI");
            this.checkBox_HI.Name = "checkBox_HI";
            this.toolTip1.SetToolTip(this.checkBox_HI, resources.GetString("checkBox_HI.ToolTip"));
            this.checkBox_HI.UseVisualStyleBackColor = true;
            this.checkBox_HI.CheckedChanged += new System.EventHandler(this.CheckBox_HI_CheckedChanged);
            // 
            // checkBox_NVENC
            // 
            resources.ApplyResources(this.checkBox_NVENC, "checkBox_NVENC");
            this.checkBox_NVENC.Name = "checkBox_NVENC";
            this.checkBox_NVENC.UseVisualStyleBackColor = true;
            this.checkBox_NVENC.CheckedChanged += new System.EventHandler(this.CheckBox_NVENC_CheckedChanged);
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
            this.pictureBox1.Image = global::NVGE.Properties.Resources.ffmpeg;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // FormVideoUpscaleSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormVideoUpscaleSettings";
            this.Load += new System.EventHandler(this.FormVideoUpscaleSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_Advanced;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_FFmpeg_path;
        private System.Windows.Forms.Button button_FC;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_FFmpeg;
        private System.Windows.Forms.TextBox textBox_AB;
        private System.Windows.Forms.TextBox textBox_ACodec;
        private System.Windows.Forms.TextBox textBox_VCodec;
        private System.Windows.Forms.TextBox textBox_FPS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_CMDV;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox_MI;
        private System.Windows.Forms.CheckBox checkBox_CC;
        private System.Windows.Forms.CheckBox checkBox_OAO;
        private System.Windows.Forms.CheckBox checkBox_DS;
        private System.Windows.Forms.CheckBox checkBox_DSS;
        private System.Windows.Forms.CheckBox checkBox_SO;
        private System.Windows.Forms.TextBox textBox_CRF;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox_EIA;
        private System.Windows.Forms.CheckBox checkBox_OOF;
        private System.Windows.Forms.CheckBox checkBox_HI;
        private System.Windows.Forms.CheckBox checkBox_NVENC;
        private System.Windows.Forms.Button button_AL;
        private System.Windows.Forms.Button button_VL;
        private System.Windows.Forms.TextBox textBox_AL;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_VL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox_MSOL;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_MAC;
        private System.Windows.Forms.CheckBox checkBox_MVC;
        private System.Windows.Forms.TextBox textBox_CMDA;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_CMDF;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox_AE;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBox_AE;
        private System.Windows.Forms.CheckBox checkBox_AB;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBox_preset;
        private System.Windows.Forms.CheckBox checkBox_preset;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBox_VOT;
        private System.Windows.Forms.ComboBox comboBox_VOT;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}