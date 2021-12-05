namespace waifu2x_ncnn_vulkan_GUI_Edition_C_Sharp
{
    partial class FormImageUpscaleDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageUpscaleDetail));
            this.pictureBox_SourceImage = new System.Windows.Forms.PictureBox();
            this.pictureBox_UpscaledImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_prev = new System.Windows.Forms.Button();
            this.button_next = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label_sec = new System.Windows.Forms.Label();
            this.label_model = new System.Windows.Forms.Label();
            this.label_scale = new System.Windows.Forms.Label();
            this.groupBox_info1 = new System.Windows.Forms.GroupBox();
            this.label_rdl = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label_gpu = new System.Windows.Forms.Label();
            this.label_blks = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_vbs = new System.Windows.Forms.Label();
            this.groupBox_info2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label_fmt = new System.Windows.Forms.Label();
            this.label_thread = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label_tta = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label_webp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SourceImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UpscaledImage)).BeginInit();
            this.groupBox_info1.SuspendLayout();
            this.groupBox_info2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_SourceImage
            // 
            resources.ApplyResources(this.pictureBox_SourceImage, "pictureBox_SourceImage");
            this.pictureBox_SourceImage.Name = "pictureBox_SourceImage";
            this.pictureBox_SourceImage.TabStop = false;
            this.pictureBox_SourceImage.Click += new System.EventHandler(this.PictureBox_SourceImage_Click);
            // 
            // pictureBox_UpscaledImage
            // 
            resources.ApplyResources(this.pictureBox_UpscaledImage, "pictureBox_UpscaledImage");
            this.pictureBox_UpscaledImage.Name = "pictureBox_UpscaledImage";
            this.pictureBox_UpscaledImage.TabStop = false;
            this.pictureBox_UpscaledImage.Click += new System.EventHandler(this.PictureBox_UpscaledImage_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // button_prev
            // 
            resources.ApplyResources(this.button_prev, "button_prev");
            this.button_prev.Name = "button_prev";
            this.button_prev.UseVisualStyleBackColor = true;
            this.button_prev.Click += new System.EventHandler(this.Button_prev_Click);
            // 
            // button_next
            // 
            resources.ApplyResources(this.button_next, "button_next");
            this.button_next.Name = "button_next";
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.Button_next_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // button_ok
            // 
            resources.ApplyResources(this.button_ok, "button_ok");
            this.button_ok.Name = "button_ok";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.Button_ok_Click);
            // 
            // button_cancel
            // 
            resources.ApplyResources(this.button_cancel, "button_cancel");
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.Button_cancel_Click);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label_sec
            // 
            resources.ApplyResources(this.label_sec, "label_sec");
            this.label_sec.Name = "label_sec";
            // 
            // label_model
            // 
            resources.ApplyResources(this.label_model, "label_model");
            this.label_model.Name = "label_model";
            // 
            // label_scale
            // 
            resources.ApplyResources(this.label_scale, "label_scale");
            this.label_scale.Name = "label_scale";
            // 
            // groupBox_info1
            // 
            resources.ApplyResources(this.groupBox_info1, "groupBox_info1");
            this.groupBox_info1.Controls.Add(this.label_rdl);
            this.groupBox_info1.Controls.Add(this.label15);
            this.groupBox_info1.Controls.Add(this.label6);
            this.groupBox_info1.Controls.Add(this.label_scale);
            this.groupBox_info1.Controls.Add(this.label_sec);
            this.groupBox_info1.Controls.Add(this.label8);
            this.groupBox_info1.Controls.Add(this.label10);
            this.groupBox_info1.Controls.Add(this.label_gpu);
            this.groupBox_info1.Controls.Add(this.label_blks);
            this.groupBox_info1.Controls.Add(this.label11);
            this.groupBox_info1.Name = "groupBox_info1";
            this.groupBox_info1.TabStop = false;
            // 
            // label_rdl
            // 
            resources.ApplyResources(this.label_rdl, "label_rdl");
            this.label_rdl.Name = "label_rdl";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label_gpu
            // 
            resources.ApplyResources(this.label_gpu, "label_gpu");
            this.label_gpu.Name = "label_gpu";
            // 
            // label_blks
            // 
            resources.ApplyResources(this.label_blks, "label_blks");
            this.label_blks.Name = "label_blks";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label_vbs
            // 
            resources.ApplyResources(this.label_vbs, "label_vbs");
            this.label_vbs.Name = "label_vbs";
            // 
            // groupBox_info2
            // 
            resources.ApplyResources(this.groupBox_info2, "groupBox_info2");
            this.groupBox_info2.Controls.Add(this.label16);
            this.groupBox_info2.Controls.Add(this.label_fmt);
            this.groupBox_info2.Controls.Add(this.label_vbs);
            this.groupBox_info2.Controls.Add(this.label9);
            this.groupBox_info2.Controls.Add(this.label_thread);
            this.groupBox_info2.Controls.Add(this.label_model);
            this.groupBox_info2.Controls.Add(this.label14);
            this.groupBox_info2.Controls.Add(this.label_tta);
            this.groupBox_info2.Controls.Add(this.label7);
            this.groupBox_info2.Controls.Add(this.label13);
            this.groupBox_info2.Controls.Add(this.label12);
            this.groupBox_info2.Name = "groupBox_info2";
            this.groupBox_info2.TabStop = false;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label_fmt
            // 
            resources.ApplyResources(this.label_fmt, "label_fmt");
            this.label_fmt.Name = "label_fmt";
            // 
            // label_thread
            // 
            resources.ApplyResources(this.label_thread, "label_thread");
            this.label_thread.Name = "label_thread";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label_tta
            // 
            resources.ApplyResources(this.label_tta, "label_tta");
            this.label_tta.Name = "label_tta";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label_webp
            // 
            resources.ApplyResources(this.label_webp, "label_webp");
            this.label_webp.Name = "label_webp";
            this.label_webp.Click += new System.EventHandler(this.label_webp_Click);
            // 
            // FormImageUpscaleDetail
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.label_webp);
            this.Controls.Add(this.groupBox_info2);
            this.Controls.Add(this.groupBox_info1);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.button_prev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox_UpscaledImage);
            this.Controls.Add(this.pictureBox_SourceImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormImageUpscaleDetail";
            this.Load += new System.EventHandler(this.FormImageUpscaleDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_SourceImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_UpscaledImage)).EndInit();
            this.groupBox_info1.ResumeLayout(false);
            this.groupBox_info1.PerformLayout();
            this.groupBox_info2.ResumeLayout(false);
            this.groupBox_info2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_SourceImage;
        private System.Windows.Forms.PictureBox pictureBox_UpscaledImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_prev;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label_sec;
        private System.Windows.Forms.Label label_model;
        private System.Windows.Forms.Label label_scale;
        private System.Windows.Forms.GroupBox groupBox_info1;
        private System.Windows.Forms.Label label_vbs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox_info2;
        private System.Windows.Forms.Label label_fmt;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_thread;
        private System.Windows.Forms.Label label_tta;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label_blks;
        private System.Windows.Forms.Label label_gpu;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label_rdl;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_webp;
    }
}