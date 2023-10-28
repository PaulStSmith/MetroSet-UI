using System;
using System.Windows.Forms;
using MetroSet.UI.Enums;
using MetroSet.UI.Forms;

namespace MetroSet.UI_Example
{
    public partial class Form1 : MetroSetForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MetroSetSwitch2_SwitchedChanged(object sender)
        {
            if (styleManager1.Style == Style.Light)
            {
                styleManager1.Style = Style.Dark;
            }
            else
            {
                styleManager1.Style = Style.Light;
            }
        }

        private void MetroSetButton3_Click(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo);
        }

        private void MetroSetButton4_Click(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        }

        private void MetroSetButton5_Click(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private void MetroSetButton6_Click(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }

        private void MetroSetButton7_Click_1(object sender, EventArgs e)
        {
            MetroSetMessageBox.Show(this, "A new update available, do you want to update it now ?", "Available Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void MetroSetDefaultButton1_Click(object sender, EventArgs e)
        {
            styleManager1.OpenTheme();
        }

        private void MetroSetListBox2_SelectedIndexChanged(object sender)
        {
            MetroSetMessageBox.Show(this, _metroSetListBox2.SelectedText);
        }

        private void textBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
