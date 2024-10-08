using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class JigSearch : SkinForm
    {
        public DataTable dtinfo = new DataTable();
        public string SearchInfo = string.Empty;
        public int Count = 0;
        public JigSearch()
        {           
            InitializeComponent();
            Process();
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

                SelectQuery = "Select Model From MaterialMapping Where Material in (select Material  from Material where  Kind = '"+ this.Combo_Customer.Text + "') order by Model ";
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
            if (this.listBox1.Items.Count == 0)
            {
                if (string.IsNullOrEmpty(this.Combo_Customer.Text) && string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                             + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                             + "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b "
                             + "  from "
                             + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                             + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                             + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                             + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                             + "    from Jig j1 where Discarded is  null ) x   "
                             + "   Order by Jig ";
                }
                 else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    if (string.IsNullOrEmpty(this.Combo_Model.Text))
                    {
                        JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                            + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay  = -1) a  ,"
                            + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay > 0) b "
                            + "  from "
                            + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                            + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                            + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                            + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                            + "    from Jig j1  where Customer = '" + this.Combo_Customer.Text + "' And Discarded is  null ) x   "
                            + "   Order by Jig ";
                    }
                    else
                    {
                        JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                               + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                               + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay > 0) b "
                               + "  from "
                               + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                               + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                               + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                               + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                               + "    from Jig j1  where Model = '" + this.Combo_Model.Text + "' And Discarded is  null ) x   "
                               + "   Order by Jig ";
                    }
                }
                else
                {
                    JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                                  + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                                  + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay > 0) b "
                                  + "  from "
                                  + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                                  + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                                  + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                                  + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                                  + "    from Jig j1  where Model = '" + this.Combo_Model.Text + "' and Customer = '" + this.Combo_Customer.Text + "' And Discarded is  null  ) x   "
                                  + "   Order by Jig ";
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
                    JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                             + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                             + "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b "
                             + "  from "
                             + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                             + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                             + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                             + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                             + "    from Jig j1 where jig in ( " + JigCode + ") and Discarded is  null ) x   "
                             + "   Order by Jig ";
                }
                else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    if (string.IsNullOrEmpty(this.Combo_Model.Text))
                    {
                        JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                            + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                            + "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b "
                            + "  from "
                            + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                            + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                            + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                            + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                            + "    from Jig j1  where Customer = '" + this.Combo_Customer.Text + "' and jig in ( " + JigCode + ") And Discarded is  null ) x   "
                            + "   Order by Jig ";
                    }
                    else
                    {
                        JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                               + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                               + "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b "
                               + "  from "
                               + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                               + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                               + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                               + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                               + "    from Jig j1  where Model = '" + this.Combo_Model.Text + "' and jig in ( " + JigCode + ") And Discarded is  null ) x   "
                               + "   Order by Jig ";
                    }
                }
                else
                {
                    JigSearch = "  select * ,DATEDIFF(DD, x.NextMaintDate, Getdate()) ToPastDay, "
                                  + " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,"
                                  + "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b "
                                  + "  from "
                                  + "   (SELECT [Jig],[Text],[Customer],[Model],[Status],[CurrentStatus],[LocationBunch],"
                                  + "          [Location],[MaintPeriodUnit],[MaintPeriord],[MaintDate],"
                                  + "          [DrawingApplied] ,[FirstUsed]     ,[Updated],[Updater],[Discarder],[DisCardReason],[Discarded],"
                                  + "          Case When len(FileData) > 0 Then 'Y' Else 'N' End ImageYN ,(select NextMaintDate From Jiginfo where jig = j1.jig) NextMaintDate"
                                  + "    from Jig j1  where Model = '" + this.Combo_Model.Text + "' and Customer = '" + this.Combo_Customer.Text + "'   and jig in ( " + JigCode + ") And Discarded is  null) x   "
                                  + "   Order by Jig ";
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

           this.TB_7dayQty.Text = dt.Rows[0]["a"].ToString();
           this.Lb_MaterialName.Text = dt.Rows[0]["b"].ToString();

           this.dataGridView1.Rows.Clear();
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               this.dataGridView1.Rows.Add(1);
               DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
               if (this.dataGridView1.Columns.Count < 1) return;

               dr.Cells["Jig"].Value = dt.Rows[i]["Jig"].ToString().Trim();
               dr.Cells["Text"].Value = dt.Rows[i]["Text"].ToString().Trim();
               dr.Cells["Customer1"].Value = dt.Rows[i]["Customer"].ToString().Trim();
               dr.Cells["Model1"].Value = dt.Rows[i]["Model"].ToString().Trim();
               dr.Cells["Status"].Value = dt.Rows[i]["Status"].ToString().Trim();
               dr.Cells["CurrentStatus"].Value = dt.Rows[i]["CurrentStatus"].ToString().Trim();
               dr.Cells["LocationBunch"].Value = dt.Rows[i]["LocationBunch"].ToString().Trim();
               dr.Cells["Location"].Value = dt.Rows[i]["Location"].ToString().Trim();
               dr.Cells["MaintPeriodUnit"].Value = dt.Rows[i]["MaintPeriodUnit"].ToString().Trim();
               dr.Cells["MaintPeriord"].Value = dt.Rows[i]["MaintPeriord"].ToString().Trim();
               dr.Cells["MaintDate"].Value = dt.Rows[i]["MaintDate"].ToString().Trim();

               if(string.IsNullOrEmpty(dt.Rows[i]["FirstUsed"].ToString().Trim()))
               {dr.Cells["FirstUsed"].Value = "";}
               else
               {dr.Cells["FirstUsed"].Value = Convert.ToDateTime(dt.Rows[i]["FirstUsed"].ToString().Trim()).ToString("yyyy-MM-dd");}         
               
               dr.Cells["Discarder"].Value = dt.Rows[i]["Discarder"].ToString().Trim();
               dr.Cells["DisCardReason"].Value = dt.Rows[i]["DisCardReason"].ToString().Trim();
               
               if(string.IsNullOrEmpty(dt.Rows[i]["Discarded"].ToString().Trim()))
               {dr.Cells["Discarded"].Value = "";}
               else
               {dr.Cells["Discarded"].Value = Convert.ToDateTime(dt.Rows[i]["Discarded"].ToString().Trim()).ToString("yyyy-MM-dd");}         
               
               if(string.IsNullOrEmpty(dt.Rows[i]["NextMaintDate"].ToString().Trim()))
               {dr.Cells["NextMaintDate"].Value = "";}
               else
               {dr.Cells["NextMaintDate"].Value = Convert.ToDateTime(dt.Rows[i]["NextMaintDate"].ToString().Trim()).ToString("yyyy-MM-dd");}      
               
               dr.Cells["ToPastDay"].Value = dt.Rows[i]["ToPastDay"].ToString().Trim();
           }
            
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string JigDetailData = " select JigMaintHist, Jig, MaintStarted, MaintEnded, MaintPerson, ConfirmDate, ConfirmOwner, ConfirmDescription, Spec_A, Spec_B   "
                                 + "        ,Spec_C,Spec_D,Spec_E,VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement,VisualA,VisualB,VisualC,VisualD,VisualE "
                                 + "        ,VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement,VisualF,VisualG,VisualH,VisualI,VisualJ  "
                                 + " from JigMaintHist "
                                 + " where Jig = '" + this.dataGridView1.CurrentRow.Cells["Jig"].Value.ToString() + "' "
                                 + " Order by Jig ";
            DataTable Detaildt = DbAccess.Default.GetDataTable(JigDetailData);

            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < Detaildt.Rows.Count; i++)
            {
                this.dataGridView2.Rows.Add(1);
                DataGridViewRow dr = this.dataGridView2.Rows[this.dataGridView2.RowCount - 1];
                if (this.dataGridView2.Columns.Count < 1) return;

                dr.Cells["jigMainthist"].Value = Detaildt.Rows[i]["jigMainthist"].ToString().Trim();
                dr.Cells["Jig1"].Value = Detaildt.Rows[i]["Jig"].ToString().Trim();

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["MaintStarted"].ToString().Trim()))
                { dr.Cells["MaintStarted"].Value = ""; }
                else
                { dr.Cells["MaintStarted"].Value = Convert.ToDateTime(Detaildt.Rows[i]["MaintStarted"].ToString().Trim()).ToString("yyyy-MM-dd"); }

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["MaintEnded"].ToString().Trim()))
                { dr.Cells["MaintEnded"].Value = ""; }
                else
                { dr.Cells["MaintEnded"].Value = Convert.ToDateTime(Detaildt.Rows[i]["MaintEnded"].ToString().Trim()).ToString("yyyy-MM-dd"); }      
                
                dr.Cells["MaintPerson"].Value = Detaildt.Rows[i]["MaintPerson"].ToString().Trim();

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["ConfirmDate"].ToString().Trim()))
                { dr.Cells["ConfirmDate"].Value = ""; }
                else
                { dr.Cells["ConfirmDate"].Value = Convert.ToDateTime(Detaildt.Rows[i]["ConfirmDate"].ToString().Trim()).ToString("yyyy-MM-dd"); }      
                
                dr.Cells["ConfirmOwner"].Value = Detaildt.Rows[i]["ConfirmOwner"].ToString().Trim();
                dr.Cells["ConfirmDescription"].Value = Detaildt.Rows[i]["ConfirmDescription"].ToString().Trim();
                if (string.IsNullOrEmpty(Detaildt.Rows[i]["Spec_A"].ToString().Trim()))
                { dr.Cells["Spec_A"].Value = "0";}
                else
                { dr.Cells["Spec_A"].Value = Detaildt.Rows[i]["Spec_A"].ToString().Trim(); }

                 if (string.IsNullOrEmpty(Detaildt.Rows[i]["Spec_B"].ToString().Trim()))
                { dr.Cells["Spec_B"].Value = "0";}
                else
                { dr.Cells["Spec_B"].Value = Detaildt.Rows[i]["Spec_B"].ToString().Trim(); }
               
                  if (string.IsNullOrEmpty(Detaildt.Rows[i]["Spec_C"].ToString().Trim()))
                  { dr.Cells["Spec_C"].Value = "0"; }
                else
                { dr.Cells["Spec_C"].Value = Detaildt.Rows[i]["Spec_C"].ToString().Trim(); }
                  if (string.IsNullOrEmpty(Detaildt.Rows[i]["Spec_D"].ToString().Trim()))
                  { dr.Cells["Spec_D"].Value = "0"; }
                  else
                  { dr.Cells["Spec_D"].Value = Detaildt.Rows[i]["Spec_D"].ToString().Trim(); }

                  if (string.IsNullOrEmpty(Detaildt.Rows[i]["Spec_E"].ToString().Trim()))
                  { dr.Cells["Spec_E"].Value = "0"; }
                  else
                  { dr.Cells["Spec_E"].Value = Detaildt.Rows[i]["Spec_E"].ToString().Trim(); }

               
                dr.Cells["VisualA"].Value = Detaildt.Rows[i]["VisualA"].ToString().Trim();
                dr.Cells["VisualA_Judgement"].Value = Detaildt.Rows[i]["VisualA_Judgement"].ToString().Trim();
                dr.Cells["VisualB"].Value = Detaildt.Rows[i]["VisualB"].ToString().Trim();
                dr.Cells["VisualB_Judgement"].Value = Detaildt.Rows[i]["VisualB_Judgement"].ToString().Trim();
                dr.Cells["VisualC"].Value = Detaildt.Rows[i]["VisualC"].ToString().Trim();
                dr.Cells["VisualC_Judgement"].Value = Detaildt.Rows[i]["VisualC_Judgement"].ToString().Trim();
                dr.Cells["VisualD"].Value = Detaildt.Rows[i]["VisualD"].ToString().Trim();
                dr.Cells["VisualD_Judgement"].Value = Detaildt.Rows[i]["VisualD_Judgement"].ToString().Trim();
                dr.Cells["VisualE"].Value = Detaildt.Rows[i]["VisualE"].ToString().Trim();
                dr.Cells["VisualE_Judgement"].Value = Detaildt.Rows[i]["VisualE_Judgement"].ToString().Trim();

                dr.Cells["VisualF"].Value = Detaildt.Rows[i]["VisualF"].ToString().Trim();
                dr.Cells["VisualF_Judgement"].Value = Detaildt.Rows[i]["VisualF_Judgement"].ToString().Trim();
                dr.Cells["VisualG"].Value = Detaildt.Rows[i]["VisualG"].ToString().Trim();
                dr.Cells["VisualG_Judgement"].Value = Detaildt.Rows[i]["VisualG_Judgement"].ToString().Trim();
                dr.Cells["VisualH"].Value = Detaildt.Rows[i]["VisualH"].ToString().Trim();
                dr.Cells["VisualH_Judgement"].Value = Detaildt.Rows[i]["VisualH_Judgement"].ToString().Trim();
                dr.Cells["VisualI"].Value = Detaildt.Rows[i]["VisualI"].ToString().Trim();
                dr.Cells["VisualI_Judgement"].Value = Detaildt.Rows[i]["VisualI_Judgement"].ToString().Trim();
                dr.Cells["VisualJ"].Value = Detaildt.Rows[i]["VisualJ"].ToString().Trim();
                dr.Cells["VisualJ_Judgement"].Value = Detaildt.Rows[i]["VisualJ_Judgement"].ToString().Trim();
               
            }
        }

        //버튼 커서 움직일때 이벤트
        #region
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Update.BackColor = Color.DarkBlue;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Update.BackColor = Color.Blue;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Search.BackColor = Color.DarkBlue;
        }

        private void button7_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Update.BackColor = Color.Blue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            this.Btn_Insert.BackColor = Color.DarkBlue;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            this.Btn_Insert.BackColor = Color.Blue;
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

        private void Btn_Insert_Click(object sender, EventArgs e)
        {
            JigBasisManagement JigBasisManagement = new JigBasisManagement();
            JigBasisManagement.ShowDialog();
        }

        private void Btn_Update_Click(object sender, EventArgs e)
        {
            JigBasisManagementUpdate JigBasisManagement = new JigBasisManagementUpdate(this.dataGridView1.CurrentRow.Cells["Jig"].Value.ToString());
            JigBasisManagement.ShowDialog();
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              int RowCount = e.ColumnIndex;
              if (RowCount == 33)
              {
                  string JigCode = string.Empty;
                  string JigMaintHist = string.Empty;
                  JigCode = this.dataGridView2.CurrentRow.Cells["Jig1"].Value.ToString();
                  JigMaintHist = this.dataGridView2.CurrentRow.Cells["JigMaintHist"].Value.ToString();
                  JigRepairInsert1 JigRepairInsert1 = new JigRepairInsert1(JigCode, JigMaintHist);
                  JigRepairInsert1.ShowDialog();

              }
           
        }

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            if (WiseApp.Id == "kim6843" || WiseApp.Id == "shlee")
            {
                string currentJig = this.dataGridView1.CurrentRow.Cells["Jig"].Value as string;
                string messageStr = "선택한 Jig 데이터를 삭제합니다. Jig Information = " + currentJig + "' ";
                if (DialogResult.Yes == WiseM.MessageBox.Show(messageStr, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    DataTable dt = DbAccess.Default.GetDataTable("Select * From JigMaintHist where Jig = '" + currentJig + "' ");
                    if (dt.Rows.Count > 0)
                    {
                        WiseM.MessageBox.Show("입출고, 보수 이력이 존재 함으로 삭제 할 수 없습니다.", "Information", MessageBoxIcon.None);
                        return;
                    }
                    else
                    {
                        try
                        {
                            int i = 0;
                            string DeleteQuery = "Delete  From Jig where Jig = '" + currentJig + "' ";
                            string DeleteQuery1 = "Delete  From JigInfo where Jig = '" + currentJig + "' ";
                            DbAccess.Default.ExecuteQuery(DeleteQuery);
                            DbAccess.Default.ExecuteQuery(DeleteQuery1);
                            WiseM.MessageBox.Show("데이터 삭제가 완료되었습니다.", "OK", MessageBoxIcon.None);
                            Process();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
            else
            {
                WiseM.MessageBox.Show("해당 기능은 관리자 이외에 다른사람이 이용할 수 없습니다.", "Information", MessageBoxIcon.None);
                return;
            }
        }

 
   
    }
}
