namespace NVGE.src.Forms
{
    partial class FormImageConvertTarget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageConvertTarget));
            label1 = new System.Windows.Forms.Label();
            comboBox_Extension = new System.Windows.Forms.ComboBox();
            button_OK = new System.Windows.Forms.Button();
            button_Cancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // comboBox_Extension
            // 
            comboBox_Extension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox_Extension.FormattingEnabled = true;
            comboBox_Extension.Items.AddRange(new object[] { resources.GetString("comboBox_Extension.Items"), resources.GetString("comboBox_Extension.Items1"), resources.GetString("comboBox_Extension.Items2"), resources.GetString("comboBox_Extension.Items3"), resources.GetString("comboBox_Extension.Items4"), resources.GetString("comboBox_Extension.Items5"), resources.GetString("comboBox_Extension.Items6"), resources.GetString("comboBox_Extension.Items7"), resources.GetString("comboBox_Extension.Items8"), resources.GetString("comboBox_Extension.Items9"), resources.GetString("comboBox_Extension.Items10"), resources.GetString("comboBox_Extension.Items11"), resources.GetString("comboBox_Extension.Items12"), resources.GetString("comboBox_Extension.Items13") });
            resources.ApplyResources(comboBox_Extension, "comboBox_Extension");
            comboBox_Extension.Name = "comboBox_Extension";
            // 
            // button_OK
            // 
            resources.ApplyResources(button_OK, "button_OK");
            button_OK.Name = "button_OK";
            button_OK.UseVisualStyleBackColor = true;
            button_OK.Click += Button_OK_Click;
            // 
            // button_Cancel
            // 
            resources.ApplyResources(button_Cancel, "button_Cancel");
            button_Cancel.Name = "button_Cancel";
            button_Cancel.UseVisualStyleBackColor = true;
            button_Cancel.Click += Button_Cancel_Click;
            // 
            // FormImageConvertTarget
            // 
            AcceptButton = button_OK;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = button_Cancel;
            ControlBox = false;
            Controls.Add(button_Cancel);
            Controls.Add(button_OK);
            Controls.Add(comboBox_Extension);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Name = "FormImageConvertTarget";
            Load += FormImageConvertTarget_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_Extension;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
    }
}