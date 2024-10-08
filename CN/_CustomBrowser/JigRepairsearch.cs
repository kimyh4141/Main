using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class JigRepairsearch : SkinForm
    {
        public DataTable dtinfo = new DataTable();
        public string SearchInfo = string.Empty;
        public int Count = 0;
        public JigRepairsearch()
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
                SelectQuery = "select Distinct Model from MaterialMapping order by Model  ";
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
            string Pathday = string.Empty;
            if (this.button1.BackColor == Color.Red) //Over
            {
                Pathday = "0";
            }
            else if (this.button2.BackColor == Color.Red) //Month
            {
                Pathday = "-30";
            }
            else if (this.button3.BackColor == Color.Red) //Week
            {
                Pathday = "-7";
            }
            else if (this.button4.BackColor == Color.Red) //Day
            {
                Pathday = "-1";
            }
            else
            {
                Pathday = "-1";
            }


            if (this.listBox1.Items.Count == 0)
            {
                if (string.IsNullOrEmpty(this.Combo_Customer.Text) && string.IsNullOrEmpty(this.Combo_Model.Text))
                {                  
                          JigSearch  = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                          JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer,  (select Location from Jig Where Jig = w1.Jig) Location,  ";
                          JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                          JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                          JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                          JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                          JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay , ";
                          JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                          JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1)  b ";
                          JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                          JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                          JigSearch += " left outer join jig w3 ";
                          JigSearch += " on  w1.jig = w3.jig ";
                          if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                          {
                              JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  "; 
                          }
                          else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                          {
                              JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  "; 
                          }
                          else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                          {
                              JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  "; 
                          }
                          else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                          {
                              JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  "; 
                          }
                          else //예정보전일자가 오늘날짜보다 늦은 모든것
                          {
                              JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";  
                          }
                          
                }
                else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                {
                    if (string.IsNullOrEmpty(this.Combo_Model.Text))
                    {
                        JigSearch  = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                        JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer, (select Location from Jig Where Jig = w1.Jig) Location,  ";
                        JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, "                                   ;
                        JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                        JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                        JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                        JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                        JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                        JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                        JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                        JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                        JigSearch += " left outer join (Select * from jig  where Customer = '" + this.Combo_Customer.Text + "' ) w3 ";
                        JigSearch += " on  w1.jig = w3.jig ";
                        if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else //예정보전일자가 오늘날짜보다 늦은 모든것
                        {
                            JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";  
                        }
                    }
                    else
                    {
                        JigSearch  = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                        JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer, (select Location from Jig Where Jig = w1.Jig) Location,  " ;
                        JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                        JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                        JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                        JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                        JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                        JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                        JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                        JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                        JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                        JigSearch += " left outer join (Select * from jig  where Model = '" + this.Combo_Model.Text + "' ) w3 ";
                        JigSearch += " on  w1.jig = w3.jig ";
                        if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else //예정보전일자가 오늘날짜보다 늦은 모든것
                        {
                            JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";  
                        }
                    }
                }
                else
                {
                       JigSearch  = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                        JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer,  (select Location from Jig Where Jig = w1.Jig) Location, ";
                        JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                        JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                        JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                        JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ,  ";
                        JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                        JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                        JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                        JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                        JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                        JigSearch += " left outer join (Select * from jig  where Customer = '" + this.Combo_Customer.Text+ "'  and  Model = '" + this.Combo_Model.Text + "' ) w3 ";
                        JigSearch += " on  w1.jig = w3.jig ";
                        if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                        {
                            JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                        }
                        else //예정보전일자가 오늘날짜보다 늦은 모든것
                        {
                            JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";  
                        }
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


                            JigSearch = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                            JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer, (select Location from Jig Where Jig = w1.Jig) Location,  ";
                            JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                            JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                            JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                            JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                            JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay  ,";
                            JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                            JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                            JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                            JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                            JigSearch += " left outer join ( Select * from Jig j1 where jig in ( " + JigCode + ")) w3 ";
                            JigSearch += " on  w1.jig = w3.jig ";
                            if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else //예정보전일자가 오늘날짜보다 늦은 모든것
                            {
                                JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                        }
                        else if (string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
                        {
                            if (string.IsNullOrEmpty(this.Combo_Model.Text))
                            {
                                JigSearch = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                                JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer,(select Location from Jig Where Jig = w1.Jig) Location,   ";
                                JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                                JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                                JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                                JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                                JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                                JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                                JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                                JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                                JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                                JigSearch += " left outer join ( Select * from Jig j1 where Customer = '" + this.Combo_Customer.Text + "' and jig in ( " + JigCode + ")) w3 ";
                                JigSearch += " on  w1.jig = w3.jig ";
                                if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else //예정보전일자가 오늘날짜보다 늦은 모든것
                                {
                                    JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                            }
                            else
                            {
                                JigSearch = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                                JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer, (select Location from Jig Where Jig = w1.Jig) Location,  ";
                                JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                                JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                                JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                                JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                                JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                                JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                                JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                                JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where  (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                                JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                                JigSearch += " left outer join ( Select * from Jig j1 where Model = '" + this.Combo_Model.Text + "' and jig in ( " + JigCode + ")) w3 ";
                                JigSearch += " on  w1.jig = w3.jig ";
                                if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                                {
                                    JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                                else //예정보전일자가 오늘날짜보다 늦은 모든것
                                {
                                    JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";
                                }
                            }
                        }
                        else
                        {
                            JigSearch = " select w1.Jig, (Select Text From Jig Where Jig = w1.Jig)JigName , NextMaintDate, LastMaintStarted, TotalMaintCount, JigMaintHist, MaintStarted , MaintPerson, ";
                            JigSearch += "      (Select Model from Jig where Jig = w1.Jig) Model , (select Customer from Jig Where Jig = w1.Jig) Customer, (select Location from Jig Where Jig = w1.Jig) Location,  ";
                            JigSearch += "        MaintDescription, ConfirmDate, ConfirmOwner,ConfirmDescription, Spec_A, Spec_B, Spec_C, Spec_D, Spec_E,VisualA,VisualB,VisualC,VisualD,VisualE, ";
                            JigSearch += "        VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement, w3.CurrentStatus , w3.Status,";
                            JigSearch += "        VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement, ";
                            JigSearch += "        VisualF,VisualG,VisualI,VisualH,VisualJ, ";
                            JigSearch += "        DATEDIFF(DD, w1.NextMaintDate, Getdate()) ToPastDay,  ";
                            JigSearch += " (Select COUNT(*) From ( select DATEDIFF(DD, NextMaintDate, Getdate()) ToPastDay   from Jiginfo ) x where ToPastDay = -1) a  ,";
                            JigSearch += "  (Select COUNT(*) From  Jiginfo w1 join (Select * From Jig where Status = 1 and CurrentStatus Not in ('2')) w2 on w1.Jig = w2.Jig    where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1) b   ";
                            JigSearch += " from (Select *  from  jiginfo Where Jig in ( Select Jig From Jig Where (Discarded is  null Or Discarded = '')) ) w1  left outer join jigMaintHist w2 ";
                            JigSearch += " on w1.LastMaintHist = w2.JigMaintHist ";
                            JigSearch += " left outer join ( Select * from Jig j1 where Customer = '" + this.Combo_Customer + "' and  Model = '" + this.Combo_Model.Text + "' and jig in ( " + JigCode + ")) w3 ";
                            JigSearch += " on  w1.jig = w3.jig ";
                            if (this.button1.BackColor == Color.Red) //Over 보전일자 지난것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) >= 1   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button2.BackColor == Color.Red) //Month 남은 일자가 30일 보다 큰것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -8  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -30    and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button3.BackColor == Color.Red) //Week 날짜 기준 남은 날짜가 한달보다 작고 7일보다 큰것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) <= -1  And DATEDIFF(DD, NextMaintDate, Getdate()) >= -7   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else if (this.button4.BackColor == Color.Red) //Day 날짜 기준 남은 날짜가 오늘이거나 하루 남은것
                            {
                                JigSearch += " where  DATEDIFF(DD, NextMaintDate, Getdate()) = 0   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
                            else //예정보전일자가 오늘날짜보다 늦은 모든것
                            {
                                JigSearch += " where   w1.NextMaintDate <= Getdate()   and status = 1 and CurrentStatus Not in ('2')  ";
                            }
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

               dr.Cells["Check"].Value = false;
               dr.Cells["Jig3"].Value = dt.Rows[i]["Jig"].ToString().Trim();
               dr.Cells["JigName3"].Value = dt.Rows[i]["JigName"].ToString().Trim();
               dr.Cells["Jig_Model"].Value = dt.Rows[i]["Model"].ToString().Trim();
               dr.Cells["Jig_Customer"].Value = dt.Rows[i]["Customer"].ToString().Trim();
               dr.Cells["Location"].Value = dt.Rows[i]["Location"].ToString().Trim();
               if (string.IsNullOrEmpty(dt.Rows[i]["NextMaintDate"].ToString().Trim()))
               { dr.Cells["NextMaintDate3"].Value = ""; }
               else
               { dr.Cells["NextMaintDate3"].Value = Convert.ToDateTime(dt.Rows[i]["NextMaintDate"].ToString().Trim()).ToString("yyyy-MM-dd"); }

               if (string.IsNullOrEmpty(dt.Rows[i]["LastMaintStarted"].ToString().Trim()))
               { dr.Cells["LastMaintStarted3"].Value = ""; }
               else
               { dr.Cells["LastMaintStarted3"].Value = Convert.ToDateTime(dt.Rows[i]["LastMaintStarted"].ToString().Trim()).ToString("yyyy-MM-dd"); }

               dr.Cells["TotalMaintCount3"].Value = dt.Rows[i]["TotalMaintCount"].ToString().Trim();
               dr.Cells["JigMaintHist1"].Value = dt.Rows[i]["JigMaintHist"].ToString().Trim();

               if (string.IsNullOrEmpty(dt.Rows[i]["MaintStarted"].ToString().Trim()))
               { dr.Cells["MaintStarted3"].Value = ""; }
               else
               { dr.Cells["MaintStarted3"].Value = Convert.ToDateTime(dt.Rows[i]["MaintStarted"].ToString().Trim()).ToString("yyyy-MM-dd"); }
               
               dr.Cells["MaintPerson3"].Value = dt.Rows[i]["MaintPerson"].ToString().Trim();

               if (string.IsNullOrEmpty(dt.Rows[i]["ConfirmDate"].ToString().Trim()))
               { dr.Cells["ConfirmDate3"].Value = ""; }
               else
               { dr.Cells["ConfirmDate3"].Value = Convert.ToDateTime(dt.Rows[i]["ConfirmDate"].ToString().Trim()).ToString("yyyy-MM-dd"); }
               
               dr.Cells["ConfirmOwner3"].Value = dt.Rows[i]["ConfirmOwner"].ToString().Trim();
               dr.Cells["Spec_A3"].Value = dt.Rows[i]["Spec_A"].ToString().Trim();
               dr.Cells["Spec_B3"].Value = dt.Rows[i]["Spec_B"].ToString().Trim();
               dr.Cells["Spec_C3"].Value = dt.Rows[i]["Spec_C"].ToString().Trim();
               dr.Cells["Spec_D3"].Value = dt.Rows[i]["Spec_D"].ToString().Trim();
               dr.Cells["Spec_E3"].Value = dt.Rows[i]["Spec_E"].ToString().Trim();
               dr.Cells["VisualA_Judgement3"].Value = dt.Rows[i]["VisualA_Judgement"].ToString().Trim();
               dr.Cells["VisualA1"].Value = dt.Rows[i]["VisualA"].ToString().Trim();
               dr.Cells["VisualB_Judgement3"].Value = dt.Rows[i]["VisualB_Judgement"].ToString().Trim();
               dr.Cells["VisualB1"].Value = dt.Rows[i]["VisualB"].ToString().Trim();
               dr.Cells["VisualC_Judgement3"].Value = dt.Rows[i]["VisualC_Judgement"].ToString().Trim();
               dr.Cells["VisualC1"].Value = dt.Rows[i]["VisualC"].ToString().Trim();
               dr.Cells["VisualD_Judgement3"].Value = dt.Rows[i]["VisualD_Judgement"].ToString().Trim();
               dr.Cells["VisualD1"].Value = dt.Rows[i]["VisualD"].ToString().Trim();
               dr.Cells["VisualE_Judgement3"].Value = dt.Rows[i]["VisualE_Judgement"].ToString().Trim();
               dr.Cells["VisualE1"].Value = dt.Rows[i]["VisualE"].ToString().Trim();
               dr.Cells["VisualF_Judgement3"].Value = dt.Rows[i]["VisualF_Judgement"].ToString().Trim();
               dr.Cells["VisualF1"].Value = dt.Rows[i]["VisualF"].ToString().Trim();
               dr.Cells["VisualG_Judgement3"].Value = dt.Rows[i]["VisualG_Judgement"].ToString().Trim();
               dr.Cells["VisualG1"].Value = dt.Rows[i]["VisualG"].ToString().Trim();
               dr.Cells["VisualH_Judgement3"].Value = dt.Rows[i]["VisualH_Judgement"].ToString().Trim();
               dr.Cells["VisualH1"].Value = dt.Rows[i]["VisualH"].ToString().Trim();
               dr.Cells["VisualI_Judgement3"].Value = dt.Rows[i]["VisualI_Judgement"].ToString().Trim();
               dr.Cells["VisualI1"].Value = dt.Rows[i]["VisualI"].ToString().Trim();
               dr.Cells["VisualJ_Judgement3"].Value = dt.Rows[i]["VisualJ_Judgement"].ToString().Trim();
               dr.Cells["VisualJ1"].Value = dt.Rows[i]["VisualJ"].ToString().Trim();    
               dr.Cells["ToPastDay3"].Value = dt.Rows[i]["ToPastDay"].ToString().Trim();
           }
            
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            JigPeriodMsg jpm = new JigPeriodMsg();
            string dt1, dt2;
            string attach = "";

            jpm.ShowDialog();
            dt1 = jpm.period1;
            dt2 = jpm.period2;

            if (dt1.Length == 10 && dt2.Length == 10)
            {
                attach = " and MaintStarted between '" + dt1 + "' and '" + dt2 + " 23:59:59.999' ";
            }

            string JigDetailData = " select ROW_NUMBER() over(order by Jig) STT, JigMaintHist, Jig, MaintStarted, MaintEnded, MaintPerson, ConfirmDate, ConfirmOwner, ConfirmDescription, Spec_A, Spec_B ,VisualA,VisualB,VisualC,VisualD,VisualE ,VisualF,VisualG,VisualH,VisualI,VisualJ "
                                 + "       ,Spec_C,Spec_D,Spec_E,VisualA_Judgement,VisualB_Judgement,VisualC_Judgement,VisualD_Judgement,VisualE_Judgement "
                                 + "       ,VisualF_Judgement,VisualG_Judgement,VisualH_Judgement,VisualI_Judgement,VisualJ_Judgement  "
                                 + " from JigMaintHist "
                                 +  " where Jig = '" + this.dataGridView1.CurrentRow.Cells["Jig3"].Value.ToString() + "' " + attach
                                 + " Order by MaintEnded desc ";
                               
            
            DataTable Detaildt = DbAccess.Default.GetDataTable(JigDetailData);

            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < Detaildt.Rows.Count; i++)
            {
                this.dataGridView2.Rows.Add(1);
                DataGridViewRow dr = this.dataGridView2.Rows[this.dataGridView2.RowCount - 1];
                if (this.dataGridView2.Columns.Count < 1) return;

                dr.Cells["STT"].Value = Detaildt.Rows[i]["STT"].ToString().Trim();
                dr.Cells["jigMainthist"].Value = Detaildt.Rows[i]["jigMainthist"].ToString().Trim();
                dr.Cells["Jig1"].Value = Detaildt.Rows[i]["Jig"].ToString().Trim();

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["MaintStarted"].ToString().Trim()))
                { dr.Cells["MaintStarted1"].Value = ""; }
                else
                { dr.Cells["MaintStarted1"].Value = Convert.ToDateTime(Detaildt.Rows[i]["MaintStarted"].ToString().Trim()).ToString("yyyy-MM-dd"); }

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["MaintEnded"].ToString().Trim()))
                { dr.Cells["MaintEnded1"].Value = ""; }
                else
                { dr.Cells["MaintEnded1"].Value = Convert.ToDateTime(Detaildt.Rows[i]["MaintEnded"].ToString().Trim()).ToString("yyyy-MM-dd"); }      
                
                dr.Cells["MaintPerson1"].Value = Detaildt.Rows[i]["MaintPerson"].ToString().Trim();

                if (string.IsNullOrEmpty(Detaildt.Rows[i]["ConfirmDate"].ToString().Trim()))
                { dr.Cells["ConfirmDate1"].Value = ""; }
                else
                { dr.Cells["ConfirmDate1"].Value = Convert.ToDateTime(Detaildt.Rows[i]["ConfirmDate"].ToString().Trim()).ToString("yyyy-MM-dd"); }      
                
                dr.Cells["ConfirmOwner1"].Value = Detaildt.Rows[i]["ConfirmOwner"].ToString().Trim();
                dr.Cells["ConfirmDescription1"].Value = Detaildt.Rows[i]["ConfirmDescription"].ToString().Trim();
                dr.Cells["Spec_A1"].Value = Detaildt.Rows[i]["Spec_A"].ToString().Trim();
                dr.Cells["Spec_B1"].Value = Detaildt.Rows[i]["Spec_B"].ToString().Trim();
                dr.Cells["Spec_C1"].Value = Detaildt.Rows[i]["Spec_C"].ToString().Trim();
                dr.Cells["Spec_D1"].Value = Detaildt.Rows[i]["Spec_D"].ToString().Trim();
                dr.Cells["Spec_E1"].Value = Detaildt.Rows[i]["Spec_E"].ToString().Trim();
                dr.Cells["VisualA"].Value = Detaildt.Rows[i]["VisualA"].ToString().Trim();
                dr.Cells["VisualA_Judgement1"].Value = Detaildt.Rows[i]["VisualA_Judgement"].ToString().Trim();
                dr.Cells["VisualB"].Value = Detaildt.Rows[i]["VisualB"].ToString().Trim();
                dr.Cells["VisualB_Judgement1"].Value = Detaildt.Rows[i]["VisualB_Judgement"].ToString().Trim();
                dr.Cells["VisualC"].Value = Detaildt.Rows[i]["VisualC"].ToString().Trim();
                dr.Cells["VisualC_Judgement1"].Value = Detaildt.Rows[i]["VisualC_Judgement"].ToString().Trim();
                dr.Cells["VisualD"].Value = Detaildt.Rows[i]["VisualD"].ToString().Trim();
                dr.Cells["VisualD_Judgement1"].Value = Detaildt.Rows[i]["VisualD_Judgement"].ToString().Trim();
                dr.Cells["VisualE"].Value = Detaildt.Rows[i]["VisualE"].ToString().Trim();
                dr.Cells["VisualE_Judgement1"].Value = Detaildt.Rows[i]["VisualE_Judgement"].ToString().Trim();

                dr.Cells["VisualF_Judgement"].Value = Detaildt.Rows[i]["VisualF_Judgement"].ToString().Trim();
                dr.Cells["VisualF"].Value = Detaildt.Rows[i]["VisualF"].ToString().Trim();
                dr.Cells["VisualG_Judgement"].Value = Detaildt.Rows[i]["VisualG_Judgement"].ToString().Trim();
                dr.Cells["VisualG"].Value = Detaildt.Rows[i]["VisualG"].ToString().Trim();
                dr.Cells["VisualH_Judgement"].Value = Detaildt.Rows[i]["VisualH_Judgement"].ToString().Trim();
                dr.Cells["VisualH"].Value = Detaildt.Rows[i]["VisualH"].ToString().Trim();
                dr.Cells["VisualI_Judgement"].Value = Detaildt.Rows[i]["VisualI_Judgement"].ToString().Trim();
                dr.Cells["VisualI"].Value = Detaildt.Rows[i]["VisualI"].ToString().Trim();
                dr.Cells["VisualJ_Judgement"].Value = Detaildt.Rows[i]["VisualJ_Judgement"].ToString().Trim();
                dr.Cells["VisualJ"].Value = Detaildt.Rows[i]["VisualJ"].ToString().Trim();  
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

        private void Btn_RepairStart_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                string JigCode = string.Empty;
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["Check"].Value.ToString()) == true)
                {
                    DataTable dt = DbAccess.Default.GetDataTable("Select * From JigMaintHist where Jig = '" + this.dataGridView1.Rows[i].Cells["Jig3"].Value.ToString() + "' and ConfirmDate is null ");
                    if (dt.Rows.Count > 0)
                    {
                     
                    }
                    else
                    {
                        JigCode = this.dataGridView1.Rows[i].Cells["Jig3"].Value.ToString();
                        RepairStart RepairStart = new RepairStart();
                        RepairStart.ProcessStart(JigCode);
                    }
                }
            }
            MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
            this.Process();
        }

        private void Btn_RepairInsert_Click(object sender, EventArgs e)
        {
            string JigCode = string.Empty;
            string JigMaintHist = string.Empty;
            JigCode = this.dataGridView1.CurrentRow.Cells["Jig3"].Value.ToString();
            JigMaintHist = this.dataGridView1.CurrentRow.Cells["JigMaintHist1"].Value.ToString();
            JigRepairInsert JigRepairInsert = new JigRepairInsert(JigCode, JigMaintHist);
            JigRepairInsert.ShowDialog();
        }

        private void Btn_RepairConfirm_Click(object sender, EventArgs e)
        {
            if (WiseApp.Id == "shlee" || WiseApp.Id == "kim6843" )
            { }
            else
            {
                MessageBox.Show("Error! You don't have enough permissions.", "Error", MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                string JigCode = string.Empty;
                string JigMaintHist = string.Empty;
                if (Convert.ToBoolean(this.dataGridView1.Rows[i].Cells["Check"].Value.ToString()) == true)
                {
                    JigCode = this.dataGridView1.Rows[i].Cells["Jig3"].Value.ToString();
                    JigMaintHist = this.dataGridView1.Rows[i].Cells["JigMaintHist1"].Value.ToString();
                    if (string.IsNullOrEmpty(JigMaintHist))
                    { }
                    else
                    {
                        JigConfirmProcess JigConfirmProcess = new JigConfirmProcess();
                        JigConfirmProcess.ProcessStart(JigCode,JigMaintHist);                       
                    }
                }
            }
            MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
            this.Process();
        }

        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e) //Over
        {
            if (this.button1.BackColor == Color.DarkBlue)
            {
                this.button1.BackColor = Color.Red;
                this.button2.BackColor = Color.DarkBlue;
                this.button3.BackColor = Color.DarkBlue;
                this.button4.BackColor = Color.DarkBlue;
            }
            else
            {
                this.button1.BackColor = Color.DarkBlue;
            }
        }

        private void button4_Click(object sender, EventArgs e) //Day
        {
            if (this.button4.BackColor == Color.DarkBlue)
            {
                this.button4.BackColor = Color.Red;
                this.button1.BackColor = Color.DarkBlue;
                this.button3.BackColor = Color.DarkBlue;
                this.button2.BackColor = Color.DarkBlue;
            }
            else
            {
                this.button4.BackColor = Color.DarkBlue;
            }
        }

        private void button3_Click(object sender, EventArgs e) //Week
        {
            if (this.button3.BackColor == Color.DarkBlue)
            {
                this.button3.BackColor = Color.Red;
                this.button2.BackColor = Color.DarkBlue;
                this.button1.BackColor = Color.DarkBlue;
                this.button4.BackColor = Color.DarkBlue;
            }
            else
            {
                this.button3.BackColor = Color.DarkBlue;
            }
        }

        private void button2_Click(object sender, EventArgs e) //Month
        {
            if (this.button2.BackColor == Color.DarkBlue)
            {
                this.button2.BackColor = Color.Red;
                this.button1.BackColor = Color.DarkBlue;
                this.button3.BackColor = Color.DarkBlue;
                this.button4.BackColor = Color.DarkBlue;
            }
            else
            {
                this.button2.BackColor = Color.DarkBlue;
            }
        }

       

     
   
    }
}
