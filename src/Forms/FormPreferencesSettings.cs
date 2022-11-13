using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVGE
{
    public partial class FormPreferencesSettings : Form
    {
        public FormPreferencesSettings()
        {
            InitializeComponent();
        }

        private void FormPreferencesSettings_Load(object sender, EventArgs e)
        {
            Config.Load(Common.xmlpath);

            switch (bool.Parse(Config.Entry["CheckUpdateWithStartup"].Value))
            {
                case true:
                    {
                        checkBox_checkupdate.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_checkupdate.Checked = false;
                        break;
                    }
            }
            switch (bool.Parse(Config.Entry["CheckUpdateFFWithStartup"].Value))
            {
                case true:
                    {
                        checkBox_checkupdateff.Checked = true;
                        break;
                    }
                case false:
                    {
                        checkBox_checkupdateff.Checked = false;
                        break;
                    }
            }
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (checkBox_checkupdate.Checked != false)
            {
                Config.Entry["CheckUpdateWithStartup"].Value = "true";
            }
            else
            {
                Config.Entry["CheckUpdateWithStartup"].Value = "false";
            }
            if (checkBox_checkupdateff.Checked != false)
            {
                Config.Entry["CheckUpdateFFWithStartup"].Value = "true";
            }
            else
            {
                Config.Entry["CheckUpdateFFWithStartup"].Value = "false";
            }

            Config.Save(Common.xmlpath);

            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
