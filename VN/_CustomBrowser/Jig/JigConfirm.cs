using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class JigConfirm : SkinForm
    {
        private CustomPanelLinkEventArgs e = null;

        public JigConfirm(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TB_JigCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string SearchData = " Select *,(Select Text From Jig where Jig = w1.Jig) Text  From JigRepairHist w1 where Jig = '" + this.TB_JigCode.Text + "' and Started is not null And Ended is null ";
                DataTable dt = DbAccess.Default.GetDataTable(SearchData);

                if (dt.Rows.Count == 0)
                {
                  
                    WiseM.MessageBox.Show("Error, this jig is not repair start.", "OK", MessageBoxIcon.None);
                    return;
                }
                else
                {
                    this.dataGridView1.Rows.Add(1);
                    DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                    dr.Cells["JigCode"].Value = dt.Rows[0]["Jig"].ToString().Trim();
                    dr.Cells["JigName"].Value = dt.Rows[0]["Text"].ToString().Trim();
                    dr.Cells["Started"].Value = Convert.ToDateTime(dt.Rows[0]["Started"].ToString()).ToString("yyyy-MM-dd");
                    dr.Cells["Reason"].Value = dt.Rows[0]["Comment"].ToString().Trim();                 

                }
            }
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            string UpdateQuery = string.Empty;
            for(int i = 0 ; i < this.dataGridView1.Rows.Count ; i++ )
            {
                UpdateQuery = " Update JigRepairHist Set Status = 0 , Ended = Getdate() where Jig = '" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "' and Status = 1 and Ended is null ";
                DbAccess.Default.ExecuteQuery(UpdateQuery);
            }
            WiseM.MessageBox.Show("Success , processing stock out.", "OK", MessageBoxIcon.None);
            this.dataGridView1.Rows.Clear();
        }

    }
}
