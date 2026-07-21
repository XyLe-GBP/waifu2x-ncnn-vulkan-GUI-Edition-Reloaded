
namespace NVGE
{
    partial class FormAbout
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
            button_OK = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            pictureBox4 = new System.Windows.Forms.PictureBox();
            label7 = new System.Windows.Forms.Label();
            pictureBox5 = new System.Windows.Forms.PictureBox();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label_FFVersion = new System.Windows.Forms.Label();
            linkLabel3 = new System.Windows.Forms.LinkLabel();
            linkLabel4 = new System.Windows.Forms.LinkLabel();
            linkLabel5 = new System.Windows.Forms.LinkLabel();
            linkLabel7 = new System.Windows.Forms.LinkLabel();
            label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // button_OK
            // 
            button_OK.Location = new System.Drawing.Point(497, 596);
            button_OK.Name = "button_OK";
            button_OK.Size = new System.Drawing.Size(75, 23);
            button_OK.TabIndex = 0;
            button_OK.Text = "Done!";
            button_OK.UseVisualStyleBackColor = true;
            button_OK.Click += Button_OK_Click;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Yu Gothic UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(560, 32);
            label1.TabIndex = 1;
            label1.Text = "waifu2x ncnn Vulkan  - GUI Edition Reloaded (waifu2x-nVGE)";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(12, 42);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(560, 15);
            label2.TabIndex = 2;
            label2.Text = "Super-Resolution Convolutional Neural Network（SRCNN）";
            label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new System.Drawing.Point(30, 442);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(128, 128);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += PictureBox1_Paint;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            linkLabel1.Location = new System.Drawing.Point(220, 467);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(102, 25);
            linkLabel1.TabIndex = 4;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "My GitHub";
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            linkLabel2.Location = new System.Drawing.Point(355, 467);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(195, 25);
            linkLabel2.TabIndex = 5;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "XyLe's Official website";
            linkLabel2.LinkClicked += LinkLabel2_LinkClicked;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.opencv_logo;
            pictureBox2.Location = new System.Drawing.Point(108, 353);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(64, 64);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(292, 364);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(136, 15);
            label3.TabIndex = 7;
            label3.Text = "This application uses the";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(304, 391);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(181, 15);
            label4.TabIndex = 8;
            label4.Text = " Copyright © 2023, OpenCV team";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label6.Location = new System.Drawing.Point(175, 83);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(232, 30);
            label6.TabIndex = 11;
            label6.Text = "Copyright © 2026, XyLe";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.magick_logo;
            pictureBox4.Location = new System.Drawing.Point(33, 242);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new System.Drawing.Size(200, 100);
            pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 12;
            pictureBox4.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(298, 263);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(136, 15);
            label7.TabIndex = 13;
            label7.Text = "This application uses the";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.ffmpeg;
            pictureBox5.Location = new System.Drawing.Point(33, 127);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new System.Drawing.Size(267, 116);
            pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 14;
            pictureBox5.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(267, 297);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(265, 15);
            label8.TabIndex = 15;
            label8.Text = "Copyright © 1999-2023, ImageMagick Studio LLC";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(306, 165);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(136, 15);
            label9.TabIndex = 16;
            label9.Text = "This application uses the";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(311, 201);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(121, 15);
            label10.TabIndex = 17;
            label10.Text = "Used FFmpeg version:";
            // 
            // label_FFVersion
            // 
            label_FFVersion.AutoSize = true;
            label_FFVersion.Location = new System.Drawing.Point(427, 201);
            label_FFVersion.Name = "label_FFVersion";
            label_FFVersion.Size = new System.Drawing.Size(57, 15);
            label_FFVersion.TabIndex = 18;
            label_FFVersion.Text = "FFVersion";
            // 
            // linkLabel3
            // 
            linkLabel3.AutoSize = true;
            linkLabel3.Location = new System.Drawing.Point(431, 263);
            linkLabel3.Name = "linkLabel3";
            linkLabel3.Size = new System.Drawing.Size(72, 15);
            linkLabel3.TabIndex = 19;
            linkLabel3.TabStop = true;
            linkLabel3.Text = "Magick.NET.";
            linkLabel3.LinkClicked += LinkLabel3_LinkClicked;
            // 
            // linkLabel4
            // 
            linkLabel4.AutoSize = true;
            linkLabel4.Location = new System.Drawing.Point(439, 165);
            linkLabel4.Name = "linkLabel4";
            linkLabel4.Size = new System.Drawing.Size(51, 15);
            linkLabel4.TabIndex = 20;
            linkLabel4.TabStop = true;
            linkLabel4.Text = "FFmpeg.";
            linkLabel4.LinkClicked += LinkLabel4_LinkClicked;
            // 
            // linkLabel5
            // 
            linkLabel5.AutoSize = true;
            linkLabel5.Location = new System.Drawing.Point(425, 364);
            linkLabel5.Name = "linkLabel5";
            linkLabel5.Size = new System.Drawing.Size(83, 15);
            linkLabel5.TabIndex = 21;
            linkLabel5.TabStop = true;
            linkLabel5.Text = "OpenCVSharp.";
            linkLabel5.LinkClicked += LinkLabel5_LinkClicked;
            // 
            // linkLabel7
            // 
            linkLabel7.AutoSize = true;
            linkLabel7.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            linkLabel7.Location = new System.Drawing.Point(430, 517);
            linkLabel7.Name = "linkLabel7";
            linkLabel7.Size = new System.Drawing.Size(137, 25);
            linkLabel7.TabIndex = 23;
            linkLabel7.TabStop = true;
            linkLabel7.Text = "Donation Page";
            linkLabel7.LinkClicked += LinkLabel7_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            label5.Location = new System.Drawing.Point(175, 520);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(244, 21);
            label5.TabIndex = 24;
            label5.Text = "If you like this app, buy me a beer.";
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(584, 631);
            ControlBox = false;
            Controls.Add(label5);
            Controls.Add(linkLabel7);
            Controls.Add(linkLabel5);
            Controls.Add(linkLabel4);
            Controls.Add(linkLabel3);
            Controls.Add(label_FFVersion);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(pictureBox5);
            Controls.Add(label7);
            Controls.Add(pictureBox4);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox2);
            Controls.Add(linkLabel2);
            Controls.Add(linkLabel1);
            Controls.Add(pictureBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button_OK);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "FormAbout";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "About waifu2x-ncnn-vulkan GUI Edition Reloaded";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label_FFVersion;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.Label label5;
    }
}