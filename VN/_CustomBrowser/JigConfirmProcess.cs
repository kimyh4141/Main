using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    class JigConfirmProcess
    {
        private string JigMaintHist1 = string.Empty;
        private string JigCode1 = string.Empty;
        private string date = string.Empty;
        public void ProcessStart(string JigCode, string JigMaintHist)
        {
            JigMaintHist1 = JigMaintHist;
            JigCode1 = JigCode;
            try
            {
                //     JigInfo Table  NextMaintDate 계산, LastMaintHist -> Null, LastMaintStarted -> Null
                //JigMaintHist Table  MaintEnded -> Getdate() , ConfirmDate - > Getdate(), ConfirmOwner -> ID 

                //입력했을 때, 메세지 처리 

                string SearchDate = " select w2.NextMaintDate ,  w1.MaintPeriodUnit, w1.MaintPeriord , w1.MaintDate  "
                                  + " from Jig w1 join Jiginfo w2 on w1.jig = w2.jig   where w1.jig = '" + JigCode1 + "' ";
                DataTable SearchDatedt = DbAccess.Default.GetDataTable(SearchDate);

                #region

                if (SearchDatedt.Rows[0]["MaintPeriodUnit"].ToString() == "Y")
                {
                    string YearNo = string.Empty;
                    string YYQuery = string.Empty;
                    string YearsCount = SearchDatedt.Rows[0]["MaintPeriord"].ToString();
                    string StartDate = Convert.ToDateTime(SearchDatedt.Rows[0]["NextMaintDate"].ToString()).ToString("yyyy-01-01");
                    if (Convert.ToInt16(SearchDatedt.Rows[0]["MaintDate"].ToString()) >= 0 || Convert.ToInt16(SearchDatedt.Rows[0]["MaintDate"].ToString()) < 365)
                    {

                        YYQuery = "select  Dateadd(DD," + SearchDatedt.Rows[0]["MaintDate"].ToString() + ",DATEADD(YY," + YearsCount + ",Getdate())) DueDate ";
                    }
                    else
                    {
                        MessageBox.Show("Error! Date Setting is fault", "Error", MessageBoxIcon.Error);

                        return;
                    }
                    DataTable YYQuerydt = DbAccess.Default.GetDataTable(YYQuery);


                    date = Convert.ToDateTime(YYQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
                else if (SearchDatedt.Rows[0]["MaintPeriodUnit"].ToString() == "M")
                {
                    string DayNo = string.Empty;
                    string MMQuery = string.Empty;
                    string DaysCount = SearchDatedt.Rows[0]["MaintPeriord"].ToString();
                    string StartDate = Convert.ToDateTime(SearchDatedt.Rows[0]["NextMaintDate"].ToString()).ToString("yyyy-MM-dd");
                    if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "0")
                    {
                        DayNo = "";
                        MMQuery = "select DATEADD(MM," + DaysCount + ",Getdate()) DueDate ";
                    }
                    else
                    {

                        MMQuery = " select CONVERT(varchar(7), DATEADD(MM," + DaysCount + ",Getdate()),120)+'-" + SearchDatedt.Rows[0]["MaintDate"].ToString() + "' DueDate ";


                    }

                    DataTable MMQuerydt = DbAccess.Default.GetDataTable(MMQuery);
                    date = Convert.ToDateTime(MMQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
                else if (SearchDatedt.Rows[0]["MaintPeriodUnit"].ToString() == "W")
                {
                    #region
                    string WeeksNo = string.Empty;
                    string DwQuery = string.Empty;
                    string WeeksCount = ((Convert.ToInt16(SearchDatedt.Rows[0]["MaintDate"].ToString()) - 1) * 7).ToString();
                    string StartDate = Convert.ToDateTime(SearchDatedt.Rows[0]["NextMaintDate"].ToString()).ToString("yyyy-MM-dd");
                    if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "0")
                    {
                        WeeksNo = " 마다";
                        DwQuery = "select DATEADD(DD,7,Getdate()) DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "1")
                    {
                        WeeksNo = " 일요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,13+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,12+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,11+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,10+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,9+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "2")
                    {
                        WeeksNo = " 월요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,1+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,5+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,4+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,3+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,2+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "3")
                    {
                        WeeksNo = " 화요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,2+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,5+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,4+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,3+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "4")
                    {
                        WeeksNo = " 수요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,3+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,9+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,5+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,4+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "5")
                    {
                        WeeksNo = " 목요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,4+" + WeeksCount + ",'Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,10+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,9+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,5+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "6")
                    {
                        WeeksNo = " 금요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,5+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,11+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,10+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,9+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else if (SearchDatedt.Rows[0]["MaintDate"].ToString() == "7")
                    {
                        WeeksNo = " 토요일 마다";
                        DwQuery = "select Case When DATEPART(dw,Getdate()) = 1 Then  DATEADD(DD,6+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 2 Then  DATEADD(DD,12+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 3 Then  DATEADD(DD,11+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 4 Then  DATEADD(DD,10+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 5 Then  DATEADD(DD,9+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 6 Then  DATEADD(DD,8+" + WeeksCount + ",Getdate()) "
                                + " When DATEPART(dw,Getdate()) = 7 Then  DATEADD(DD,7+" + WeeksCount + ",Getdate())  End DueDate ";
                    }
                    else
                    {
                        MessageBox.Show("Error! Date Setting is fault", "Error", MessageBoxIcon.Error);
                        return;
                    }


                    DataTable DwQuerydt = DbAccess.Default.GetDataTable(DwQuery);
                    date = Convert.ToDateTime(DwQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                    #endregion
                }
                else if (SearchDatedt.Rows[0]["MaintPeriodUnit"].ToString() == "D")
                {
                    string DDQuery = string.Empty;
                    //string DaysCount = this.TB_MaintPeriod.Text;
                    string StartDate = Convert.ToDateTime(SearchDatedt.Rows[0]["NextMaintDate"].ToString()).ToString("yyyy-MM-dd");
                    DDQuery = "select  Dateadd(DD,1,'" + StartDate + "') DueDate ";
                    DataTable DDQuerydt = DbAccess.Default.GetDataTable(DDQuery);
                    date = Convert.ToDateTime(DDQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                }
                else
                {
                    MessageBox.Show("Error! Date Setting is fault (Y,W,M.D is not Choice)", "Error", MessageBoxIcon.Error);
                    return;
                }
                #endregion



                string UpdateQuery = " Update Jiginfo Set  NextMaintDate = '" + date + "' , LastMaintHist = Null , LastMaintStarted = Null ";
                UpdateQuery += " where Jig = '" + JigCode1 + "' ";

                string UpdateQuery1 = " Update JigMaintHist Set MaintEnded = Getdate() , ConfirmDate = Getdate() , ConfirmOwner = '" + WiseApp.Id + "' ";
                UpdateQuery1 += " Where JigMaintHist = '" + JigMaintHist1 + "' ";
                DbAccess.Default.ExecuteQuery(UpdateQuery);
                DbAccess.Default.ExecuteQuery(UpdateQuery1);
                //MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);

              

            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex + "", "OK", MessageBoxIcon.Warning);
            }
        }
    }
}
