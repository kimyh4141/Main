using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class JigWareHouseIn : SkinForm
    {
        public JigWareHouseIn()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int k = 0; k < this.dataGridView1.Rows.Count; k++)
                {
                    if (this.dataGridView1.Rows[k].Cells["JigCode"].Value.ToString() == this.textBox1.Text)
                    {
                        MessageBox.Show("Error! Is already process Data", "Error", MessageBoxIcon.Error);
                        this.textBox1.Text = string.Empty;
                        return;
                    }
                }

                DataTable dta = DbAccess.Default.GetDataTable("Select *  From JigRepairHist  w1 where Jig = '" + this.textBox1.Text + "' And Started is not null and Ended is null  ");
                if (dta.Rows.Count > 0)
                {
                    WiseM.MessageBox.Show("Error , processing of repair confirm.  ", "Information", MessageBoxIcon.None);
                    return;
                }

                DataTable dt = DbAccess.Default.GetDataTable("Select * , (select Case When NextMaintDate > GETDATE() then 'N' else 'Y' End NextMaintDate From Jiginfo where Jig = w1.Jig) NextMaintDate, (Select Text From Common where Category = '600' and Common = w1.LocationBunch) LocationBunchName  From Jig w1 where Jig = '" + this.textBox1.Text + "' ");
                
                //if (dt.Rows[0]["NextMaintDate"].ToString() == "Y")
                //{
                //    WiseM.MessageBox.Show("Error , preventive maintenance be overdue  ", "Information", MessageBoxIcon.None);
                //    return;
                //}
                
                if (dt.Rows[0]["LocationBunch"].ToString() == "JL02")
                {
                    WiseM.MessageBox.Show("Error , This Jig position is Jigstorage.", "Information", MessageBoxIcon.None);
                    return;
                }
                else
                {
                    this.dataGridView1.Rows.Add(1);
                    DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                    dr.Cells["JigCode"].Value = dt.Rows[0]["Jig"].ToString().Trim();
                    dr.Cells["JigName"].Value = dt.Rows[0]["Text"].ToString().Trim();
                    dr.Cells["LocationBunch"].Value = dt.Rows[0]["LocationBunch"].ToString().Trim();
                    dr.Cells["LocationBunchName"].Value = dt.Rows[0]["LocationBunchName"].ToString().Trim();
                    dr.Cells["Location"].Value = dt.Rows[0]["Location"].ToString().Trim();                    
                }
                this.textBox1.Text = string.Empty;
                this.textBox1.Focus();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);

        }

        private void Btn_WarehouseIn_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows.Count <= 0)
            {
                WiseM.MessageBox.Show("Not Found List", "Empty", MessageBoxIcon.None);
                return;
            }

            try
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    string InsertQuery = " Insert into JigStockHist Values (";
                    InsertQuery += " 'In' , Null, (Select LocationBunch From Jig where Jig ='" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "') , '" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "', getdate(), '" + WiseApp.Id + "' , 'JL02',Null) ";
                    
                    string UpdateQuery = string.Empty;
                    if (this.dataGridView1.Rows[i].Cells["LocationBunch"].Value.ToString() == "JL01")
                    {
                        UpdateQuery = "Update Jig set CurrentStatus = '1' , LocationBunch = 'JL02' , Location = 'None' where Jig = '" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "' ";
                    }
                    else if (this.dataGridView1.Rows[i].Cells["LocationBunch"].Value.ToString() == "JL02")
                    {
                        UpdateQuery = "Update Jig set CurrentStatus = '1' , LocationBunch = 'JL02' , Location = 'None' where Jig = '" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "' ";
                    }
                    else
                    {
                        UpdateQuery = "Update Jig set CurrentStatus = '1' , LocationBunch = 'JL02' , Location = 'None' where Jig = '" + this.dataGridView1.Rows[i].Cells["JigCode"].Value.ToString() + "' ";
                    }
                    DbAccess.Default.ExecuteQuery(InsertQuery);
                    DbAccess.Default.ExecuteQuery(UpdateQuery);
                }

                WiseM.MessageBox.Show("Success , processing stock in.", "OK", MessageBoxIcon.None);
                this.textBox1.Text = string.Empty;
                this.dataGridView1.Rows.Clear();
                this.textBox1.Focus();
            }
            catch (Exception ex)
            {
                WiseM.MessageBox.Show("Error , Data processing problems ", "Information", MessageBoxIcon.None);
                return;
            }


        }

      
    }
}
