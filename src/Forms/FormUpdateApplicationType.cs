using System;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormUpdateApplicationType : Form
    {
        public FormUpdateApplicationType()
        {
            InitializeComponent();
        }

        private void FormUpdateApplicationType_Load(object sender, EventArgs e)
        {
            comboBox_method.SelectedIndex = 1;
            Common.ApplicationPortable = false;
        }

        private void ComboBox_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            switch (comboBox_method.SelectedIndex)
            {
                case 0:
                    Common.ApplicationPortable = true;
                    break;
                case 1:
                    Common.ApplicationPortable = false;
                    break;
                default:
                    Common.ApplicationPortable = false;
                    break;
            }
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
