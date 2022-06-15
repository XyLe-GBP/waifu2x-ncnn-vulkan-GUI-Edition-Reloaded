namespace NVGE
{
    partial class FormShowPicture
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
            this.pictureBox_Main = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel_size = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMousePosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageXY = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox_Main
            // 
            this.pictureBox_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_Main.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Main.Name = "pictureBox_Main";
            this.pictureBox_Main.Size = new System.Drawing.Size(984, 586);
            this.pictureBox_Main.TabIndex = 0;
            this.pictureBox_Main.TabStop = false;
            this.pictureBox_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Main_Paint);
            this.pictureBox_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_Main_MouseDown);
            this.pictureBox_Main.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_Main_MouseMove);
            this.pictureBox_Main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox_Main_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel_size,
            this.lblImageSize,
            this.lblMousePosition,
            this.lblImageXY});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel_size
            // 
            this.statusLabel_size.Name = "statusLabel_size";
            this.statusLabel_size.Size = new System.Drawing.Size(27, 17);
            this.statusLabel_size.Text = "Size";
            // 
            // lblImageSize
            // 
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(59, 17);
            this.lblImageSize.Text = "ImageSize";
            // 
            // lblMousePosition
            // 
            this.lblMousePosition.Name = "lblMousePosition";
            this.lblMousePosition.Size = new System.Drawing.Size(62, 17);
            this.lblMousePosition.Text = "MouseLoc";
            // 
            // lblImageXY
            // 
            this.lblImageXY.Name = "lblImageXY";
            this.lblImageXY.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblImageXY.Size = new System.Drawing.Size(53, 17);
            this.lblImageXY.Text = "ImageXY";
            // 
            // FormShowPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormShowPicture";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormShowPicture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShowPicture_FormClosing);
            this.Load += new System.EventHandler(this.FormShowPicture_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormShowPicture_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormShowPicture_KeyUp);
            this.Resize += new System.EventHandler(this.FormShowPicture_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Main)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Main;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel_size;
        private System.Windows.Forms.ToolStripStatusLabel lblImageXY;
        private System.Windows.Forms.ToolStripStatusLabel lblMousePosition;
        private System.Windows.Forms.ToolStripStatusLabel lblImageSize;
    }
}