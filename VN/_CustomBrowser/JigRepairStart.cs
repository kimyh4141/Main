using System;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class JigRepairStart : SkinForm
    {
        private CustomPanelLinkEventArgs e = null;

        public JigRepairStart(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
            this.TB_JigCode.Text = this.e.DataGridView.CurrentRow.Cells["Jig"].Value.ToString();
            this.TB_RepairReason.Focus();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TB_RepairReason.Text))
            {
                WiseM.MessageBox.Show("Error, is not data repairreason.", "OK", MessageBoxIcon.None);
                return;
            }
            try
            {
                string insertQeury = " Insert into JigRepairHist Values (N'" + this.TB_JigCode.Text + "' , getdate() , Null , N'" + this.TB_RepairReason.Text + "', 1)";
                DbAccess.Default.ExecuteQuery(insertQeury);
                WiseM.MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
               
                this.Close();
            }
            catch(Exception ex)
            {
            
            }
        }
    }
}
