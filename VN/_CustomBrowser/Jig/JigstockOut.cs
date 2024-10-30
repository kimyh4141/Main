using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;


namespace WiseM.Browser
{
    public partial class JigstockOut : SkinForm
    {
        //private CustomPanelLinkEventArgs e = null;
        public JigstockOut()
        {
            InitializeComponent();
            //this.e = e;
            //string Jig = e.DataGridView.CurrentRow.Cells["Jig"].Value as string;
            //string JigName = e.DataGridView.CurrentRow.Cells["Text"].Value as string;
            //string FromLocation = e.DataGridView.CurrentRow.Cells["LocationBunch"].Value as string + " : " + e.DataGridView.CurrentRow.Cells["LocationBunchName"].Value as string;
            this.LocationGroup();
			this.Worker();
            //this.TB_JigName.Text = JigName;
            //this.Tb_Jig.Text = Jig;
            //this.TB_LocationBunch.Text = FromLocation;
            //this.TB_LocationBunchName.Text = e.DataGridView.CurrentRow.Cells["Location"].Value.ToString();
        }

        //지그 위치 그룹
        private void LocationGroup()
        {
            this.Combo_LocationGroup.Text = string.Empty;
            this.Combo_LocationGroup.Items.Clear();
            string SelectQuery = " Select  * From Common where Category = '600' and  status = 1 ";
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_LocationGroup.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_LocationGroup.Items.Add(dt.Rows[i]["Common"].ToString().Trim() + "/" + dt.Rows[i]["Text"].ToString().Trim());
                }
            }
        }
		private void Worker()
		{
			this.ComBo_Recipient.Text = string.Empty;
			this.ComBo_Recipient.Items.Clear();
			string SelectQuery = " Select  Common From Common where Category = '2' and  status = 1 ";
			DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

			if (dt.Rows.Count <= 0)
				return;
			else
			{
				this.ComBo_Recipient.Items.Clear();
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					this.ComBo_Recipient.Items.Add(dt.Rows[i]["Common"].ToString().Trim() );
				}
			}
		}
		private void Combo_LocationGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Combo_Location.Text = string.Empty;
            //this.Combo_Location.Items.Clear();
            //if ( this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL02" || this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL21")
            //{
            //    this.Combo_Location.Items.Add("None");
            //}
            //else if (this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL01")
            //{
            //    this.Combo_Location.Items.Add("In");
            //    this.Combo_Location.Items.Add("Out");
            //}
            //else
            //{
            //    this.Combo_Location.Items.Add("None");
            //}
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
         
            try
            {
                string messageStr = "선택한 Jig 데이터를 출고처리합니다..  ";
                if (DialogResult.Yes == WiseM.MessageBox.Show(messageStr, "StockIn", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        string InsertQuery = " Insert Into JigStockHist Values ( ";
                        InsertQuery += " 'Out' , Null , '" + this.dataGridView1.Rows[i].Cells["LocationFrom"].Value.ToString() + "' , '" + this.dataGridView1.Rows[i].Cells["Jigcode"].Value.ToString() + "' , getdate() , ";
                        InsertQuery += " '" + WiseApp.Id + "' , '" + this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() + "', N'" + this.dataGridView1.Rows[i].Cells["Recipient"].Value.ToString() + "') ";

                        string UpdateQuery = string.Empty;
                        if (this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() == "JL01")
                        {
                            UpdateQuery = " Update Jig Set CurrentStatus = '2', LocationBunch = '" + this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() + "' ";
                            //UpdateQuery += " , Location = '" + this.dataGridView1.Rows[i].Cells["LocationTo"].Value.ToString() + "' "
                            UpdateQuery += " , Location = 'In' "
                                        + " where Jig = '" + this.dataGridView1.Rows[i].Cells["Jigcode"].Value.ToString() + "' ";
                        }
                        else if (this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() == "JL02")
                        {
                            UpdateQuery = " Update Jig Set CurrentStatus = '1', LocationBunch = '" + this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() + "' ";
                            //UpdateQuery += " , Location = '" + this.dataGridView1.Rows[i].Cells["LocationTo"].Value.ToString() + "' "
                            UpdateQuery += " , Location = 'None' "
                                        + " where Jig = '" + this.dataGridView1.Rows[i].Cells["Jigcode"].Value.ToString() + "' ";
                        }
                        else
                        {
                            UpdateQuery = " Update Jig Set CurrentStatus = '3', LocationBunch = '" + this.dataGridView1.Rows[i].Cells["LocationBunchTo"].Value.ToString() + "' ";
                            //UpdateQuery += " , Location = '" + this.dataGridView1.Rows[i].Cells["LocationTo"].Value.ToString() + "' "
                            UpdateQuery += " , Location = 'None' "
                                        + " where Jig = '" + this.dataGridView1.Rows[i].Cells["Jigcode"].Value.ToString() + "' ";
                        }
                        DbAccess.Default.ExecuteQuery(InsertQuery);
                        DbAccess.Default.ExecuteQuery(UpdateQuery);
                    }
                    WiseM.MessageBox.Show("Success , processing stock out.", "OK", MessageBoxIcon.None);
                    this.Tb_Jig.Text = string.Empty;
                    this.dataGridView1.Rows.Clear();
                    this.Tb_Jig.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex + " ", "Error", MessageBoxIcon.Error);
            }
        }

        private void Tb_Jig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.Combo_LocationGroup.Text)
                    //|| string.IsNullOrEmpty(this.Combo_Location.Text)
                    || string.IsNullOrEmpty(this.ComBo_Recipient.Text))
                {
                    MessageBox.Show("Error! Is not select data. (Location, LocationGroup)", "Error", MessageBoxIcon.Error);
                    this.Tb_Jig.Text = string.Empty;
                    return;
                }

                for (int k = 0; k < this.dataGridView1.Rows.Count; k++)
                {
                    if (this.dataGridView1.Rows[k].Cells["JigCode"].Value.ToString() == this.Tb_Jig.Text)
                    {
                        MessageBox.Show("Error! Is already process Data", "Error", MessageBoxIcon.Error);
                        this.Tb_Jig.Text = string.Empty;
                        return;
                    }
                }

                DataTable dta = DbAccess.Default.GetDataTable("Select *  From JigRepairHist  w1 where Jig = '" + this.Tb_Jig.Text + "' And Started is not null and Ended is null  ");
                if (dta.Rows.Count > 0)
                {
                    WiseM.MessageBox.Show("Error , processing of repair confirm.  ", "Information", MessageBoxIcon.None);
                    return;
                }

                DataTable dt = DbAccess.Default.GetDataTable("Select * , (select Case When NextMaintDate > GETDATE() then 'N' else 'Y' End NextMaintDate From Jiginfo where Jig = w1.Jig) NextMaintDate,"
                             + " (Select Text From Common where Category = '600' and Common = w1.LocationBunch) LocationBunchName  From Jig w1 where Jig = '" + this.Tb_Jig.Text + "' ");

                if (dt.Rows[0]["LocationBunch"].ToString() == this.Combo_LocationGroup.Text.Substring(0, 4))
                {
                    WiseM.MessageBox.Show("Error , This Jig position is " + this.Combo_LocationGroup.Text + ".", "Information", MessageBoxIcon.None);
                    return;
                }

                if (dt.Rows[0]["NextMaintDate"].ToString() == "Y")
                {
                    WiseM.MessageBox.Show("Error , No preventive maintenance was done.", "Information", MessageBoxIcon.None);
                    return;
                }
                
                this.dataGridView1.Rows.Add(1);
                DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                dr.Cells["JigCode"].Value = dt.Rows[0]["Jig"].ToString().Trim();
                dr.Cells["JigName"].Value = dt.Rows[0]["Text"].ToString().Trim();
                dr.Cells["LocationFrom"].Value = dt.Rows[0]["LocationBunch"].ToString().Trim();
                dr.Cells["LocationBunchTo"].Value = this.Combo_LocationGroup.Text.Split('/')[0].Trim();
                dr.Cells["LocationTo"].Value = this.Combo_Location.Text.Trim();
                dr.Cells["Recipient"].Value = this.ComBo_Recipient.Text.Trim();
                       
                this.Tb_Jig.Text = string.Empty;
                this.Tb_Jig.Focus();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.dataGridView1.Rows.Remove(this.dataGridView1.CurrentRow);
        }

    }
}
