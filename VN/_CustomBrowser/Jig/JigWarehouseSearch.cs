using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class JigWarehouseSearch : SkinForm
    {
        public DataTable dtinfo = new DataTable();
        public string SearchInfo = string.Empty;
        public int Count = 0;
        public JigWarehouseSearch()
        {           
            InitializeComponent();
            cbType.Text = "ALL";
            tbDt.Text = DateTime.Today.ToString("yyyy-MM-dd");
            //Process();
            this.Model();
            this.Customer();
        }

        //고객사 데이터 추출
        private void Customer()
        {
            this.Combo_Customer.Text = string.Empty;
            this.Combo_Customer.Items.Clear();
            string SelectQuery = " Select  * From Customer where status = 1 ";
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_Customer.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_Customer.Items.Add(dt.Rows[i]["Customer"].ToString().Trim());
                }
            }
        }

        //모델 데이터 추출
        private void Model()
        {
            this.Combo_Model.Text = string.Empty;
            this.Combo_Model.Items.Clear();
            //string SelectQuery = " select Distinct Model  from MaterialMapping where Status = 1 order by Model  ";
            string SelectQuery = string.Empty;
            if (string.IsNullOrEmpty(this.Combo_Customer.Text))
            {
                SelectQuery = "select Distinct Model from MaterialMapping order by Model ";
            }
            else
            {

                SelectQuery = "Select Model From MaterialMapping Where Material in (select Material  from Material where  Kind = '" + this.Combo_Customer.Text + "') order by Model";
            }
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);
            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_Model.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_Model.Items.Add(dt.Rows[i]["Model"].ToString().Trim());
                }
            }
        }

        private void Process()
        {
            string JigSearch = string.Empty;
            string attach2 = "";

            if (tbDt.Text.Length == 10)
            {
                attach2 = " and Updated between '" + tbDt.Text + "' and '" + tbDt.Text + " 23:59:59.999' ";
            }

            //(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate
            if (this.listBox1.Items.Count == 0)
            {
                if (string.IsNullOrEmpty(this.Combo_Customer.Text) && string.IsNullOrEmpty(this.Combo_Model.Text))
                {                  
                          JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                    + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                    + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN , "
                                    + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                    + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                    + " From Jig j1 "
                                    + " where  status = 1 " + attach2
                                    + " Order by Jig ";
                }
                else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    if (string.IsNullOrEmpty(this.Combo_Model.Text))
                    {

                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN , "
                                + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                + " From Jig j1 "
                                + " Where Customer = '" + this.Combo_Customer.Text + "' and status = 1 " + attach2
                                + " Order by j1.Jig ";                        
                    }
                    else
                    {
                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                 + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                 + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,  "
                                 + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                 + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                 + " From Jig j1 "
                                 + " Where Model = '" + this.Combo_Model.Text + "' and Discarded is  null " + attach2
                                 + " Order by j1.Jig "; 
                    }
                }
                else
                {
                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                   + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                   + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,  "
                                   + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                   + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                   + " From Jig j1 "
                                   + " Where Customer = '" + this.Combo_Customer.Text + "'  And Model = '" + this.Combo_Model.Text + "'  and status = 1 " + attach2
                                   + " Order by j1.Jig "; 
                }
            }
            else
            {
                string JigCode = string.Empty;
                for (int k = 0; k < this.listBox1.Items.Count; k++)
                {                 
                        JigCode += "'" + this.listBox1.Items[k].ToString() + "',";                 
                }
                        JigCode += "'a'";
                if (string.IsNullOrEmpty(this.Combo_Customer.Text) && string.IsNullOrEmpty(this.Combo_Model.Text))
                {


                    JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                  + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                  + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,  "
                                  + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                  + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                  + " From Jig j1 "
                                  + " Where Jig in (" + JigCode + ")  and status = 1 " + attach2
                                  + " Order by j1.Jig "; 
                }
                else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    if (string.IsNullOrEmpty(this.Combo_Model.Text))
                    {
                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                   + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                   + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN , "
                                   + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                   + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                   + " From Jig j1 "
                                   + " Where Jig in (" + JigCode + ") and Customer = '" + this.Combo_Customer.Text + "'  and status = 1 " + attach2
                                   + " Order by j1.Jig "; 
                    }
                    else
                    {
                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                  + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                  + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,  "
                                  + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                  + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                  + " From Jig j1 "
                                  + " Where Jig in (" + JigCode + ")  And Model = '" + this.Combo_Model.Text + "'  and status = 1 " + attach2
                                  + " Order by j1.Jig "; 
                    }
                }
                else
                {
                        JigSearch = " SELECT ROW_NUMBER() over(order by j1.Jig) STT, j1.Jig,[Text],[Status],[CurrentStatus],[LocationBunch], "
                                  + "        (select TEXT From Common Where Category = '600' and Common = j1.LocationBunch) LocationBunchName,[Location],[Customer],        "
                                  + "        Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN , "
                                  + "        (select top 1 Updated from JigStockHist where j1.Jig = Jig order by updated desc) Updated,  "
                                  + "        (select top 1 IoType from JigStockHist where j1.Jig = Jig order by updated desc) IoType  "
                                  + " From Jig j1 "
                                  + " Where Jig in (" + JigCode + ") and Customer = '" + this.Combo_Customer.Text + "'  And Model = '" + this.Combo_Model.Text + "' and status = 1  " + attach2
                                  + " Order by j1.Jig "; 
                }
            }
           
           DataTable dt = DbAccess.Default.GetDataTable(JigSearch);


           if (dt.Rows.Count == 0)
           {
               WiseM.MessageBox.Show("Error, is not find data!", "Warning", MessageBoxIcon.Information);
               this.dataGridView1.Rows.Clear();
               this.dataGridView2.Rows.Clear();
               return;
           }

           this.dataGridView1.Rows.Clear();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               this.dataGridView1.Rows.Add(1);
               DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
               if (this.dataGridView1.Columns.Count < 1) return;


                dr.Cells["STT"].Value = dt.Rows[i]["STT"].ToString().Trim();
                dr.Cells["Jig"].Value = dt.Rows[i]["Jig"].ToString().Trim();
               dr.Cells["JigName"].Value = dt.Rows[i]["Text"].ToString().Trim();
               dr.Cells["Status"].Value = dt.Rows[i]["Status"].ToString().Trim();
               dr.Cells["CurrentStatus"].Value = dt.Rows[i]["CurrentStatus"].ToString().Trim();
               dr.Cells["LocationBunch"].Value = dt.Rows[i]["LocationBunch"].ToString().Trim();
               dr.Cells["LocationBunchName"].Value = dt.Rows[i]["LocationBunchName"].ToString().Trim();
               dr.Cells["Location"].Value = dt.Rows[i]["Location"].ToString().Trim();
               dr.Cells["Customer1"].Value = dt.Rows[i]["Customer"].ToString().Trim();

                //if (string.IsNullOrEmpty(dt.Rows[i]["NextMaintDate"].ToString().Trim()))
                //{ dr.Cells["NextMaintDate"].Value = ""; }
                //else
                //{ dr.Cells["NextMaintDate"].Value = Convert.ToDateTime(dt.Rows[i]["NextMaintDate"].ToString().Trim()).ToString("yyyy-MM-dd"); }
                dr.Cells["IO"].Value = dt.Rows[i]["IoType"].ToString().Trim();
                dr.Cells["Updated_"].Value = dt.Rows[i]["Updated"].ToString().Trim();
            }
            
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //string attach1 = "";
            //string attach2 = "";

            //if (cbType.SelectedIndex != 0)
            //{
            //    attach1 = " and IoType = '" + cbType.Text + "' ";
            //}
            //if (string.IsNullOrEmpty(tbDt.Text) || tbDt.Text.Length != 10)
            //{
            //    System.Windows.Forms.MessageBox.Show("Set the period.");
            //    return;
            //}

            //attach2 = " and Updated between '" + tbDt.Text + "' and '" + tbDt.Text + " 23:59:59.999' ";

            string JigDetailData = " Select * From JigStockHist  Where Jig = '" + this.dataGridView1.CurrentRow.Cells["Jig"].Value.ToString() + "' " //+ attach1 + attach2
                                 + " Order by Updated desc ";
            //System.Windows.Forms.MessageBox.Show(JigDetailData);
            DataTable Detaildt = DbAccess.Default.GetDataTable(JigDetailData);

            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < Detaildt.Rows.Count; i++)
            {
                this.dataGridView2.Rows.Add(1);
                DataGridViewRow dr = this.dataGridView2.Rows[this.dataGridView2.RowCount - 1];
                if (this.dataGridView2.Columns.Count < 1) return;

                dr.Cells["JigStockHist"].Value = Detaildt.Rows[i]["JigStockHist"].ToString().Trim();
                dr.Cells["IoType"].Value = Detaildt.Rows[i]["IoType"].ToString().Trim();
                dr.Cells["WarehouseFrom"].Value = Detaildt.Rows[i]["WarehouseFrom"].ToString().Trim();
                dr.Cells["WarehouseTo"].Value = Detaildt.Rows[i]["WarehouseTo"].ToString().Trim();
                dr.Cells["Jig1"].Value = Detaildt.Rows[i]["Jig"].ToString().Trim();

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["Updated"].ToString().Trim()))
                { dr.Cells["Updated"].Value = ""; }
                else
                { dr.Cells["Updated"].Value = Convert.ToDateTime(Detaildt.Rows[i]["Updated"].ToString().Trim()).ToString("yyyy-MM-dd"); }
                dr.Cells["Updater"].Value = Detaildt.Rows[i]["Updater"].ToString().Trim();
                dr.Cells["Recipient"].Value = Detaildt.Rows[i]["Recipient"].ToString();

              
            }
        }

        //버튼 커서 움직일때 이벤트
        #region
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_RepairInsert.BackColor = Color.DarkBlue;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_RepairInsert.BackColor = Color.Blue;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Search.BackColor = Color.DarkBlue;
        }

        private void button7_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_RepairInsert.BackColor = Color.Blue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_RepairStart.BackColor = Color.DarkBlue;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_RepairStart.BackColor = Color.Blue;
        }
        #endregion

        //바코드 선택 조회
        private void TB_JigCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                for (int i = 0; i < this.listBox1.Items.Count; i++)
                {
                    if (this.listBox1.Items[i].ToString() == this.TB_JigCode.Text)
                    {
                        WiseM.MessageBox.Show("Error, is already data!", "Warning", MessageBoxIcon.Information);
                        this.TB_JigCode.Text = string.Empty;
                        return;
                    }
                }
                this.listBox1.Items.Add(this.TB_JigCode.Text);
                this.TB_JigCode.Text = string.Empty;
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void Btn_RepairStart_Click(object sender, EventArgs e)
        {
            JigWareHouseIn JigWareHouseIn = new JigWareHouseIn();
            JigWareHouseIn.ShowDialog();
        }

        private void Btn_RepairInsert_Click(object sender, EventArgs e)
        {
            JigstockOut JigStockOut = new JigstockOut();
            JigStockOut.ShowDialog();
        }
		
	}
}
