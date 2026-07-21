
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVideoUpscaleSettings));
            groupBox1 = new System.Windows.Forms.GroupBox();
            checkBox_Advanced = new System.Windows.Forms.CheckBox();
            button_FFmpeg_path = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            textBox_FFmpeg = new System.Windows.Forms.TextBox();
            textBox_FPS = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label16 = new System.Windows.Forms.Label();
            checkBox_VOT = new System.Windows.Forms.CheckBox();
            comboBox_VOT = new System.Windows.Forms.ComboBox();
            label15 = new System.Windows.Forms.Label();
            comboBox_preset = new System.Windows.Forms.ComboBox();
            checkBox_preset = new System.Windows.Forms.CheckBox();
            comboBox_AE = new System.Windows.Forms.ComboBox();
            label14 = new System.Windows.Forms.Label();
            checkBox_AE = new System.Windows.Forms.CheckBox();
            checkBox_AB = new System.Windows.Forms.CheckBox();
            textBox_CMDF = new System.Windows.Forms.TextBox();
            label13 = new System.Windows.Forms.Label();
            textBox_CMDA = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            checkBox_MAC = new System.Windows.Forms.CheckBox();
            checkBox_MVC = new System.Windows.Forms.CheckBox();
            button_AL = new System.Windows.Forms.Button();
            label7 = new System.Windows.Forms.Label();
            button_FC = new System.Windows.Forms.Button();
            button_VL = new System.Windows.Forms.Button();
            textBox_AL = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            textBox_VL = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            textBox_AB = new System.Windows.Forms.TextBox();
            checkBox_MSOL = new System.Windows.Forms.CheckBox();
            textBox_ACodec = new System.Windows.Forms.TextBox();
            textBox_CMDV = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            textBox_VCodec = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            checkBox_MI = new System.Windows.Forms.CheckBox();
            label3 = new System.Windows.Forms.Label();
            checkBox_CC = new System.Windows.Forms.CheckBox();
            checkBox_OAO = new System.Windows.Forms.CheckBox();
            checkBox_DS = new System.Windows.Forms.CheckBox();
            label2 = new System.Windows.Forms.Label();
            checkBox_DSS = new System.Windows.Forms.CheckBox();
            checkBox_SO = new System.Windows.Forms.CheckBox();
            textBox_CRF = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            checkBox_EIA = new System.Windows.Forms.CheckBox();
            checkBox_OOF = new System.Windows.Forms.CheckBox();
            checkBox_HI = new System.Windows.Forms.CheckBox();
            checkBox_NVENC = new System.Windows.Forms.CheckBox();
            button_OK = new System.Windows.Forms.Button();
            button_Cancel = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(checkBox_Advanced);
            groupBox1.Controls.Add(button_FFmpeg_path);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(textBox_FFmpeg);
            groupBox1.Controls.Add(textBox_FPS);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            toolTip1.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // checkBox_Advanced
            // 
            resources.ApplyResources(checkBox_Advanced, "checkBox_Advanced");
            checkBox_Advanced.Name = "checkBox_Advanced";
            toolTip1.SetToolTip(checkBox_Advanced, resources.GetString("checkBox_Advanced.ToolTip"));
            checkBox_Advanced.UseVisualStyleBackColor = true;
            checkBox_Advanced.CheckedChanged += CheckBox_Advanced_CheckedChanged;
            // 
            // button_FFmpeg_path
            // 
            resources.ApplyResources(button_FFmpeg_path, "button_FFmpeg_path");
            button_FFmpeg_path.Name = "button_FFmpeg_path";
            toolTip1.SetToolTip(button_FFmpeg_path, resources.GetString("button_FFmpeg_path.ToolTip"));
            button_FFmpeg_path.UseVisualStyleBackColor = true;
            button_FFmpeg_path.Click += Button_FFmpeg_path_Click;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip1.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // textBox_FFmpeg
            // 
            resources.ApplyResources(textBox_FFmpeg, "textBox_FFmpeg");
            textBox_FFmpeg.Name = "textBox_FFmpeg";
            textBox_FFmpeg.ReadOnly = true;
            toolTip1.SetToolTip(textBox_FFmpeg, resources.GetString("textBox_FFmpeg.ToolTip"));
            textBox_FFmpeg.TextChanged += TextBox_FFmpeg_TextChanged;
            // 
            // textBox_FPS
            // 
            resources.ApplyResources(textBox_FPS, "textBox_FPS");
            textBox_FPS.Name = "textBox_FPS";
            toolTip1.SetToolTip(textBox_FPS, resources.GetString("textBox_FPS.ToolTip"));
            textBox_FPS.TextChanged += TextBox_FPS_TextChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip1.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(checkBox_VOT);
            groupBox2.Controls.Add(comboBox_VOT);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(comboBox_preset);
            groupBox2.Controls.Add(checkBox_preset);
            groupBox2.Controls.Add(comboBox_AE);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(checkBox_AE);
            groupBox2.Controls.Add(checkBox_AB);
            groupBox2.Controls.Add(textBox_CMDF);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(textBox_CMDA);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(checkBox_MAC);
            groupBox2.Controls.Add(checkBox_MVC);
            groupBox2.Controls.Add(button_AL);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(button_FC);
            groupBox2.Controls.Add(button_VL);
            groupBox2.Controls.Add(textBox_AL);
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(textBox_VL);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(textBox_AB);
            groupBox2.Controls.Add(checkBox_MSOL);
            groupBox2.Controls.Add(textBox_ACodec);
            groupBox2.Controls.Add(textBox_CMDV);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(textBox_VCodec);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(checkBox_MI);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(checkBox_CC);
            groupBox2.Controls.Add(checkBox_OAO);
            groupBox2.Controls.Add(checkBox_DS);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(checkBox_DSS);
            groupBox2.Controls.Add(checkBox_SO);
            groupBox2.Controls.Add(textBox_CRF);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(checkBox_EIA);
            groupBox2.Controls.Add(checkBox_OOF);
            groupBox2.Controls.Add(checkBox_HI);
            groupBox2.Controls.Add(checkBox_NVENC);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            toolTip1.SetToolTip(groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip1.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // checkBox_VOT
            // 
            resources.ApplyResources(checkBox_VOT, "checkBox_VOT");
            checkBox_VOT.Name = "checkBox_VOT";
            toolTip1.SetToolTip(checkBox_VOT, resources.GetString("checkBox_VOT.ToolTip"));
            checkBox_VOT.UseVisualStyleBackColor = true;
            checkBox_VOT.CheckedChanged += CheckBox_VOT_CheckedChanged;
            // 
            // comboBox_VOT
            // 
            resources.ApplyResources(comboBox_VOT, "comboBox_VOT");
            comboBox_VOT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_VOT.FormattingEnabled = true;
            comboBox_VOT.Items.AddRange(new object[] { resources.GetString("comboBox_VOT.Items"), resources.GetString("comboBox_VOT.Items1") });
            comboBox_VOT.Name = "comboBox_VOT";
            toolTip1.SetToolTip(comboBox_VOT, resources.GetString("comboBox_VOT.ToolTip"));
            comboBox_VOT.SelectedIndexChanged += ComboBox_VOT_SelectedIndexChanged;
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            toolTip1.SetToolTip(label15, resources.GetString("label15.ToolTip"));
            // 
            // comboBox_preset
            // 
            resources.ApplyResources(comboBox_preset, "comboBox_preset");
            comboBox_preset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_preset.FormattingEnabled = true;
            comboBox_preset.Items.AddRange(new object[] { resources.GetString("comboBox_preset.Items"), resources.GetString("comboBox_preset.Items1"), resources.GetString("comboBox_preset.Items2"), resources.GetString("comboBox_preset.Items3"), resources.GetString("comboBox_preset.Items4"), resources.GetString("comboBox_preset.Items5"), resources.GetString("comboBox_preset.Items6"), resources.GetString("comboBox_preset.Items7"), resources.GetString("comboBox_preset.Items8") });
            comboBox_preset.Name = "comboBox_preset";
            toolTip1.SetToolTip(comboBox_preset, resources.GetString("comboBox_preset.ToolTip"));
            comboBox_preset.SelectedIndexChanged += ComboBox_preset_SelectedIndexChanged;
            // 
            // checkBox_preset
            // 
            resources.ApplyResources(checkBox_preset, "checkBox_preset");
            checkBox_preset.Name = "checkBox_preset";
            toolTip1.SetToolTip(checkBox_preset, resources.GetString("checkBox_preset.ToolTip"));
            checkBox_preset.UseVisualStyleBackColor = true;
            checkBox_preset.CheckedChanged += CheckBox_preset_CheckedChanged;
            // 
            // comboBox_AE
            // 
            resources.ApplyResources(comboBox_AE, "comboBox_AE");
            comboBox_AE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_AE.FormattingEnabled = true;
            comboBox_AE.Items.AddRange(new object[] { resources.GetString("comboBox_AE.Items"), resources.GetString("comboBox_AE.Items1"), resources.GetString("comboBox_AE.Items2"), resources.GetString("comboBox_AE.Items3"), resources.GetString("comboBox_AE.Items4") });
            comboBox_AE.Name = "comboBox_AE";
            toolTip1.SetToolTip(comboBox_AE, resources.GetString("comboBox_AE.ToolTip"));
            comboBox_AE.SelectedIndexChanged += ComboBox_AE_SelectedIndexChanged;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            toolTip1.SetToolTip(label14, resources.GetString("label14.ToolTip"));
            // 
            // checkBox_AE
            // 
            resources.ApplyResources(checkBox_AE, "checkBox_AE");
            checkBox_AE.Name = "checkBox_AE";
            toolTip1.SetToolTip(checkBox_AE, resources.GetString("checkBox_AE.ToolTip"));
            checkBox_AE.UseVisualStyleBackColor = true;
            checkBox_AE.CheckedChanged += CheckBox_AE_CheckedChanged;
            // 
            // checkBox_AB
            // 
            resources.ApplyResources(checkBox_AB, "checkBox_AB");
            checkBox_AB.Name = "checkBox_AB";
            toolTip1.SetToolTip(checkBox_AB, resources.GetString("checkBox_AB.ToolTip"));
            checkBox_AB.UseVisualStyleBackColor = true;
            checkBox_AB.CheckedChanged += CheckBox_AB_CheckedChanged;
            // 
            // textBox_CMDF
            // 
            resources.ApplyResources(textBox_CMDF, "textBox_CMDF");
            textBox_CMDF.Name = "textBox_CMDF";
            toolTip1.SetToolTip(textBox_CMDF, resources.GetString("textBox_CMDF.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip1.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // textBox_CMDA
            // 
            resources.ApplyResources(textBox_CMDA, "textBox_CMDA");
            textBox_CMDA.Name = "textBox_CMDA";
            toolTip1.SetToolTip(textBox_CMDA, resources.GetString("textBox_CMDA.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            toolTip1.SetToolTip(label12, resources.GetString("label12.ToolTip"));
            // 
            // checkBox_MAC
            // 
            resources.ApplyResources(checkBox_MAC, "checkBox_MAC");
            checkBox_MAC.Name = "checkBox_MAC";
            toolTip1.SetToolTip(checkBox_MAC, resources.GetString("checkBox_MAC.ToolTip"));
            checkBox_MAC.UseVisualStyleBackColor = true;
            checkBox_MAC.CheckedChanged += CheckBox_MAC_CheckedChanged;
            // 
            // checkBox_MVC
            // 
            resources.ApplyResources(checkBox_MVC, "checkBox_MVC");
            checkBox_MVC.Name = "checkBox_MVC";
            toolTip1.SetToolTip(checkBox_MVC, resources.GetString("checkBox_MVC.ToolTip"));
            checkBox_MVC.UseVisualStyleBackColor = true;
            checkBox_MVC.CheckedChanged += CheckBox_MVC_CheckedChanged;
            // 
            // button_AL
            // 
            resources.ApplyResources(button_AL, "button_AL");
            button_AL.Name = "button_AL";
            toolTip1.SetToolTip(button_AL, resources.GetString("button_AL.ToolTip"));
            button_AL.UseVisualStyleBackColor = true;
            button_AL.Click += Button_AL_Click;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip1.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // button_FC
            // 
            resources.ApplyResources(button_FC, "button_FC");
            button_FC.Name = "button_FC";
            toolTip1.SetToolTip(button_FC, resources.GetString("button_FC.ToolTip"));
            button_FC.UseVisualStyleBackColor = true;
            // 
            // button_VL
            // 
            resources.ApplyResources(button_VL, "button_VL");
            button_VL.Name = "button_VL";
            toolTip1.SetToolTip(button_VL, resources.GetString("button_VL.ToolTip"));
            button_VL.UseVisualStyleBackColor = true;
            button_VL.Click += Button_VL_Click;
            // 
            // textBox_AL
            // 
            resources.ApplyResources(textBox_AL, "textBox_AL");
            textBox_AL.Name = "textBox_AL";
            textBox_AL.ReadOnly = true;
            toolTip1.SetToolTip(textBox_AL, resources.GetString("textBox_AL.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip1.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // textBox_VL
            // 
            resources.ApplyResources(textBox_VL, "textBox_VL");
            textBox_VL.Name = "textBox_VL";
            textBox_VL.ReadOnly = true;
            toolTip1.SetToolTip(textBox_VL, resources.GetString("textBox_VL.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip1.SetToolTip(label10, resources.GetString("label10.ToolTip"));
            // 
            // textBox_AB
            // 
            resources.ApplyResources(textBox_AB, "textBox_AB");
            textBox_AB.Name = "textBox_AB";
            toolTip1.SetToolTip(textBox_AB, resources.GetString("textBox_AB.ToolTip"));
            textBox_AB.TextChanged += TextBox_AB_TextChanged;
            // 
            // checkBox_MSOL
            // 
            resources.ApplyResources(checkBox_MSOL, "checkBox_MSOL");
            checkBox_MSOL.Name = "checkBox_MSOL";
            toolTip1.SetToolTip(checkBox_MSOL, resources.GetString("checkBox_MSOL.ToolTip"));
            checkBox_MSOL.UseVisualStyleBackColor = true;
            checkBox_MSOL.CheckedChanged += CheckBox_MSOL_CheckedChanged;
            // 
            // textBox_ACodec
            // 
            resources.ApplyResources(textBox_ACodec, "textBox_ACodec");
            textBox_ACodec.Name = "textBox_ACodec";
            toolTip1.SetToolTip(textBox_ACodec, resources.GetString("textBox_ACodec.ToolTip"));
            textBox_ACodec.TextChanged += TextBox_ACodec_TextChanged;
            // 
            // textBox_CMDV
            // 
            resources.ApplyResources(textBox_CMDV, "textBox_CMDV");
            textBox_CMDV.Name = "textBox_CMDV";
            toolTip1.SetToolTip(textBox_CMDV, resources.GetString("textBox_CMDV.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // textBox_VCodec
            // 
            resources.ApplyResources(textBox_VCodec, "textBox_VCodec");
            textBox_VCodec.Name = "textBox_VCodec";
            toolTip1.SetToolTip(textBox_VCodec, resources.GetString("textBox_VCodec.ToolTip"));
            textBox_VCodec.TextChanged += TextBox_VCodec_TextChanged;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            toolTip1.SetToolTip(label9, resources.GetString("label9.ToolTip"));
            // 
            // checkBox_MI
            // 
            resources.ApplyResources(checkBox_MI, "checkBox_MI");
            checkBox_MI.Name = "checkBox_MI";
            toolTip1.SetToolTip(checkBox_MI, resources.GetString("checkBox_MI.ToolTip"));
            checkBox_MI.UseVisualStyleBackColor = true;
            checkBox_MI.CheckedChanged += CheckBox_MI_CheckedChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip1.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // checkBox_CC
            // 
            resources.ApplyResources(checkBox_CC, "checkBox_CC");
            checkBox_CC.Name = "checkBox_CC";
            toolTip1.SetToolTip(checkBox_CC, resources.GetString("checkBox_CC.ToolTip"));
            checkBox_CC.UseVisualStyleBackColor = true;
            checkBox_CC.CheckedChanged += CheckBox_CC_CheckedChanged;
            // 
            // checkBox_OAO
            // 
            resources.ApplyResources(checkBox_OAO, "checkBox_OAO");
            checkBox_OAO.Name = "checkBox_OAO";
            toolTip1.SetToolTip(checkBox_OAO, resources.GetString("checkBox_OAO.ToolTip"));
            checkBox_OAO.UseVisualStyleBackColor = true;
            checkBox_OAO.CheckedChanged += CheckBox_OAO_CheckedChanged;
            // 
            // checkBox_DS
            // 
            resources.ApplyResources(checkBox_DS, "checkBox_DS");
            checkBox_DS.Name = "checkBox_DS";
            toolTip1.SetToolTip(checkBox_DS, resources.GetString("checkBox_DS.ToolTip"));
            checkBox_DS.UseVisualStyleBackColor = true;
            checkBox_DS.CheckedChanged += CheckBox_DS_CheckedChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip1.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // checkBox_DSS
            // 
            resources.ApplyResources(checkBox_DSS, "checkBox_DSS");
            checkBox_DSS.Name = "checkBox_DSS";
            toolTip1.SetToolTip(checkBox_DSS, resources.GetString("checkBox_DSS.ToolTip"));
            checkBox_DSS.UseVisualStyleBackColor = true;
            checkBox_DSS.CheckedChanged += CheckBox_DSS_CheckedChanged;
            // 
            // checkBox_SO
            // 
            resources.ApplyResources(checkBox_SO, "checkBox_SO");
            checkBox_SO.Name = "checkBox_SO";
            toolTip1.SetToolTip(checkBox_SO, resources.GetString("checkBox_SO.ToolTip"));
            checkBox_SO.UseVisualStyleBackColor = true;
            checkBox_SO.CheckedChanged += CheckBox_SO_CheckedChanged;
            // 
            // textBox_CRF
            // 
            resources.ApplyResources(textBox_CRF, "textBox_CRF");
            textBox_CRF.Name = "textBox_CRF";
            toolTip1.SetToolTip(textBox_CRF, resources.GetString("textBox_CRF.ToolTip"));
            textBox_CRF.TextChanged += TextBox_CRF_TextChanged;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip1.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // checkBox_EIA
            // 
            resources.ApplyResources(checkBox_EIA, "checkBox_EIA");
            checkBox_EIA.Name = "checkBox_EIA";
            toolTip1.SetToolTip(checkBox_EIA, resources.GetString("checkBox_EIA.ToolTip"));
            checkBox_EIA.UseVisualStyleBackColor = true;
            checkBox_EIA.CheckedChanged += CheckBox_EIA_CheckedChanged;
            // 
            // checkBox_OOF
            // 
            resources.ApplyResources(checkBox_OOF, "checkBox_OOF");
            checkBox_OOF.Name = "checkBox_OOF";
            toolTip1.SetToolTip(checkBox_OOF, resources.GetString("checkBox_OOF.ToolTip"));
            checkBox_OOF.UseVisualStyleBackColor = true;
            checkBox_OOF.CheckedChanged += CheckBox_OOF_CheckedChanged;
            // 
            // checkBox_HI
            // 
            resources.ApplyResources(checkBox_HI, "checkBox_HI");
            checkBox_HI.Name = "checkBox_HI";
            toolTip1.SetToolTip(checkBox_HI, resources.GetString("checkBox_HI.ToolTip"));
            checkBox_HI.UseVisualStyleBackColor = true;
            checkBox_HI.CheckedChanged += CheckBox_HI_CheckedChanged;
            // 
            // checkBox_NVENC
            // 
            resources.ApplyResources(checkBox_NVENC, "checkBox_NVENC");
            checkBox_NVENC.Name = "checkBox_NVENC";
            toolTip1.SetToolTip(checkBox_NVENC, resources.GetString("checkBox_NVENC.ToolTip"));
            checkBox_NVENC.UseVisualStyleBackColor = true;
            checkBox_NVENC.CheckedChanged += CheckBox_NVENC_CheckedChanged;
            // 
            // button_OK
            // 
            resources.ApplyResources(button_OK, "button_OK");
            button_OK.Name = "button_OK";
            toolTip1.SetToolTip(button_OK, resources.GetString("button_OK.ToolTip"));
            button_OK.UseVisualStyleBackColor = true;
            button_OK.Click += Button_OK_Click;
            // 
            // button_Cancel
            // 
            resources.ApplyResources(button_Cancel, "button_Cancel");
            button_Cancel.Name = "button_Cancel";
            toolTip1.SetToolTip(button_Cancel, resources.GetString("button_Cancel.ToolTip"));
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += Button_Cancel_Click;
            // 
            // FormVideoUpscaleSettings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(button_Cancel);
            Controls.Add(button_OK);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormVideoUpscaleSettings";
            toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            Load += FormVideoUpscaleSettings_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);

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
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox checkBox_VOT;
        private System.Windows.Forms.ComboBox comboBox_VOT;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}