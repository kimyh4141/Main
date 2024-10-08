using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class ChangeCount : SkinForm
    {
        private CustomPanelLinkEventArgs e = null;
        public ChangeCount(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
            Process();
        }

        private void Process()
        {
            this.textBox1.Text = e.DataGridView.CurrentRow.Cells["WorkOrder"].Value.ToString();
            this.textBox2.Text = e.DataGridView.CurrentRow.Cells["OrderQty"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox2.Text) || this.textBox2.Text == "0")
            {
                WiseM.MessageBox.Show("Input Data fail!", "Warning", MessageBoxIcon.Warning);
                return;
            }
            if (WiseM.MessageBox.Show("Do you want to Update Data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string UpdateQuery = " Update WorkOrder Set OrderQty = '" + this.textBox2.Text + "', Updater = '" + WiseApp.Id + "' where Workorder = '" + this.textBox1.Text + "'";
                DbAccess.Default.ExecuteQuery(UpdateQuery);

                WiseM.MessageBox.Show("Update Successfully!!", "Warning", MessageBoxIcon.Warning);
                this.Close();

            }
        }
    }
}
