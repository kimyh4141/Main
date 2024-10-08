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

namespace WiseM.Browser.Checksheet
{
    public partial class CsCheckSheetSpec : SkinForm
    {
        public static string ThisText = string.Empty;

        public static string ManagerCheck = string.Empty;

        private List<CsCheckSheetSpec_User> ControlItems = null;

        // 현재 페이지 번호
        private int CurrentPage = 1;

        // 한 페이지에 몇개의 Row를 넣을것인지
        private int RowSize = 15;  

        //기본 Spec
        DataTable dt = null;      
        
        DataTable CheckDT = null;

        public CsCheckSheetSpec()
        {
            InitializeComponent();
        }

        private void CsCheckSheetSpec_Load(object sender, EventArgs e)
        {
            ThisText = this.Text;

            ControlItems = new List<CsCheckSheetSpec_User>();

            this.dtp_CheckDate.Value = DateTime.Today;

            //입력날짜
            this.lbl_InputDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            GetCheckSheetSpec();
        }

        private void GetCheckSheetSpec()
        {
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    CsCheckSheetSpec_User cscontrol = new CsCheckSheetSpec_User()
                    {
                        Seq = row["Seq"].ToString()
                        ,
                        CsCode = row["CsCode"].ToString()
                        ,
                        CsShift = cb_Shift.Text.Split(':')[0].ToString()
                        ,
                        CheckGroup = row["CheckGroup"].ToString()
                        ,
                        Checkitems = row["Checkitems"].ToString()
                        ,
                        Min = row["ValueMin"].ToString()
                        ,
                        Max = row["ValueMax"].ToString()
                        ,
                        DataUnit = row["DataUnit"].ToString()
                        ,
                        Values = ""
                        ,
                        Remark = ""
                        ,
                        type = Int32.Parse(row["DataType"].ToString())
                        ,
                        Dock = DockStyle.Fill
                    };

                    cscontrol.ct04btnclick += new CsCheckSheetSpec_User.CsType04BtnClick(cscontrol_ct04btnclick);

                    cscontrol.cs06seq += new CsCheckSheetSpec_User.CS06SEQ(cscontrol_cs06seq);

                    ControlItems.Add(cscontrol);
                }

                SetCombo_Line();    //라인명 콤보박스

                cb_Shift.SelectedIndex = 0;

                Paging(CurrentPage);    //페이징 나누기
            }
            catch { }
        }

        //이벤트
        private void cscontrol_cs06seq(string CsCode, string Seq)
        {
            if (CsCode.Equals("CS06"))
            {
                if (Seq.Equals("3"))
                {
                    ControlItems[2].Values = (Convert.ToDecimal(string.IsNullOrEmpty(ControlItems[1].Values) ? "0" : ControlItems[1].Values) - Convert.ToDecimal(string.IsNullOrEmpty(ControlItems[0].Values) ? "0" : ControlItems[0].Values)).ToString();
                    
                }
                else if (Seq.Equals("5"))
                {
                    ControlItems[4].Values = (Convert.ToDecimal(string.IsNullOrEmpty(ControlItems[3].Values) ? "0" : ControlItems[3].Values) - Convert.ToDecimal(string.IsNullOrEmpty(ControlItems[0].Values) ? "0" : ControlItems[0].Values)).ToString();
                }
            }
        }

        //이벤트 
        void cscontrol_ct04btnclick(string CsCode, string Type, string Seq)
        {
            if (Type.Equals(""))
            {
                ControlItems[0].Values = Type;
                for (int i = 1; i < ControlItems.Count; i++)
                {
                    ControlItems[i].Min = "";
                    ControlItems[i].Max = "";
                }
            }
            else
            {
                DataTable CSDT = DbAccess.Default.GetDataTable("select * from CsSpecialValue2 where CsCode='" + CsCode + "' and Type='" + Type + "'");
                ControlItems[0].Values = Type;
                for (int i = 1; i < ControlItems.Count; i++)
                {
                    ControlItems[i].Min = CSDT.Rows[i]["ValueMin"].ToString();
                    ControlItems[i].Max = CSDT.Rows[i]["ValueMax"].ToString();
                }
            }
        }

        //Line 콤보박스
        private void SetCombo_Line()
        {
            string query = string.Empty;

            query += "\r\n SELECT   CONCAT(Line, ':', LineName) AS Line_LineName ";
            query += "\r\n          ,Line ";
            query += "\r\n FROM     CsLine ";

            DataTable dt = DbAccess.Default.GetDataTable(query);

            this.cb_Line.DataSource = dt;
            this.cb_Line.DisplayMember = "Line_LineName";
            this.cb_Line.ValueMember = "Line";
        }

        //페이징 '<' , '>' 에서 사용 됨.
        private void Paging(int page)
        {
            //페이징 나누기 윗쪽 RowSize변수를 통해 최대 한페이지에 15개까지 넣을수 있음.

            for (int i = 0 + ((page - 1) * RowSize); i < (page * RowSize); i++)
            {
                if (ControlItems.Count > i) // 
                {
                    ControlItems[i].Visible = true;
                    this.Controls.Find("panel_r" + ((i % RowSize) + 1).ToString(), true)[0].Controls.Clear();                //빈칸
                    this.Controls.Find("panel_r" + ((i % RowSize) + 1).ToString(), true)[0].Controls.Add(ControlItems[i]);
                }
                else   //페이지 2
                {
                    this.Controls.Find("panel_r" + ((i % RowSize) + 1).ToString(), true)[0].Controls.Clear();
                }
            }
        }

        //저장
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cb_User.Text))
            {
                System.Windows.Forms.MessageBox.Show("Please select worker.");
                return;
            }
            if (string.IsNullOrEmpty(Convert.ToString(cb_Line.SelectedValue)))
            {
                System.Windows.Forms.MessageBox.Show("Please select line.");
                return;
            }
            if (this.lbl_Period.Text.ToUpper().Equals("SHIFTLY") && string.IsNullOrEmpty(cb_Shift.Text))
            {
                System.Windows.Forms.MessageBox.Show("Please select shift.");
                return;
            }

            string csDate = dtp_CheckDate.Value.ToString("yyyy-MM-dd");
            string csCode = cb_CheckSheetCode.Text.Split(':')[0];
            string checker = cb_User.Text.Split(':')[0];

            string shift = "";

            if (this.lbl_Period.Text.ToUpper().Equals("SHIFTLY") && string.IsNullOrEmpty(cb_Shift.Text) == false)
            {
                shift = cb_Shift.Text.Split(':')[0];
            }

            // Type : 0 이 '이상'상태에서 기존 값이 이상이면 비고란에 빈값이 아닌지를 판별하여 팅겨내게  어떤 라우팅 : 어떤 항목

            string CheckQuery = string.Empty;

            CheckQuery += "\r\n SELECT * FROM CsCheckSheet ";
            CheckQuery += "\r\n WHERE    CsCode = '" + cb_CheckSheetCode.Text.Split(':')[0] + "' ";
            CheckQuery += "\r\n      AND CsDate = '" + dtp_CheckDate.Value.ToString("yyyy-MM-dd") + "' ";
            CheckQuery += "\r\n      AND CsShift = '" + shift + "' ";

            CheckDT = DbAccess.Default.GetDataTable(CheckQuery);

            if (CheckDT.Rows.Count > 0) //기존 Hist의 값이 있을경우
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["DataType"].ToString().Equals("0"))    //DataType : 0
                    {
                        //if (CheckDT.Rows[i]["Values"].ToString().Equals("조치완료") && !ControlItems[i].Values.Equals("조치완료"))  //기존 Hist에서 값이 이상인것
                        if (CheckDT.Rows[i]["Values"].ToString().Equals("Completed") && !ControlItems[i].Values.Equals("Completed"))  //기존 Hist에서 값이 이상인것
                        {

                            //System.Windows.Forms.MessageBox.Show("체크항목 : " + ControlItems[i].Checkitems + "\n\n기존값이 '조치완료' 이므로 수정이 불가합니다.");
                            System.Windows.Forms.MessageBox.Show("Check : " + ControlItems[i].Checkitems + "\n\n'Completed' is cannot be modified.");
                            return;

                        }
                        //else if (CheckDT.Rows[i]["Values"].ToString().Equals("이상") && !ControlItems[i].Values.Equals("이상"))  //기존 Hist에서 값이 이상인것
                        else if (CheckDT.Rows[i]["Values"].ToString().Equals("Bad") && !ControlItems[i].Values.Equals("Bad"))  //기존 Hist에서 값이 이상인것
                        {
                            if (string.IsNullOrEmpty(ControlItems[i].tb_ReMark.Text))
                            {
                                //System.Windows.Forms.MessageBox.Show("체크항목 : " + ControlItems[i].Checkitems + "\n\n기존값이 '이상' 이므로 비고란을 작성해주세요.");
                                System.Windows.Forms.MessageBox.Show("Check : " + ControlItems[i].Checkitems + "\n\n'Bad' need to fill in the remark.");
                                ControlItems[i].tb_ReMark.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        //if (!dt.Rows[i]["DataType"].ToString().Equals("0") && CheckDT.Rows[i]["Values"].ToString().Equals("계획정지"))
                        if (!dt.Rows[i]["DataType"].ToString().Equals("0") && CheckDT.Rows[i]["Values"].ToString().Equals("PlanStop"))
                        {
                            
                        }
                        else if (!string.IsNullOrEmpty(CheckDT.Rows[i]["Values"].ToString())) //Hist에 있는 값이 빈값이 아닐때
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["ValueMin"].ToString()) || string.IsNullOrEmpty(dt.Rows[i]["ValueMax"].ToString()))
                            {
                                //Spec에서 ValueMin,ValueMax 값이 NULL
                            }
                            else if (!(Convert.ToDecimal(string.IsNullOrEmpty(dt.Rows[i]["ValueMin"].ToString()) ? Convert.ToDecimal(0) : dt.Rows[i]["ValueMin"]) <= Convert.ToDecimal(CheckDT.Rows[i]["Values"])
                                 && Convert.ToDecimal(string.IsNullOrEmpty(dt.Rows[i]["ValueMax"].ToString()) ? Convert.ToDecimal(0) : dt.Rows[i]["ValueMax"]) >= Convert.ToDecimal(CheckDT.Rows[i]["Values"]))    //  기존 하한 < 기존값 < 상한 이 아닌 
                            && !ControlItems[i].Values.Equals(CheckDT.Rows[i]["Values"]))  //변경값과  기존값이 같지 않을경우 
                            {
                                if (string.IsNullOrEmpty(ControlItems[i].tb_ReMark.Text)) //비고란이 비워있을 떄 
                                {
                                    //System.Windows.Forms.MessageBox.Show("체크항목 : " + ControlItems[i].Checkitems + "\n\n기존값이 상/하한치를 벗어나 있으므로 비고란을 작성해주세요.");
                                    System.Windows.Forms.MessageBox.Show("Check : " + ControlItems[i].Checkitems + "\n\nPlease fill in the remark as the existing value is outside the upper/lower limit.");
                                    ControlItems[i].tb_ReMark.Focus();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string seq = ControlItems[i].lbl_Seq.Text;
                    string checkGroup = ControlItems[i].lbl_CheckGroup.Text;
                    string checkItem = ControlItems[i].lbl_CheckItem.Text;
                    string checkValue = ControlItems[i].Values;
                    string remark = ControlItems[i].tb_ReMark.Text;

                    string type = "";

                    if (dt.Rows[0]["CsCode"].ToString().Equals("CS24") || dt.Rows[0]["CsCode"].ToString().Equals("CS25"))
                    {
                        type = ControlItems[0].Values;
                    }

                    string query = this.Save(csCode, csDate, shift, seq, checkGroup, checkItem, checkValue, remark, checker, ManagerCheck, WiseApp.CurrentUser.Name, type);

                    DbAccess.Default.ExecuteQuery(query);
                }

                System.Windows.Forms.MessageBox.Show("Success!");

                cb_User.Text = "";
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Fail!\r\n{ex.Message}", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            cb_flash();

            Paging(CurrentPage);    //페이징 나누기 
        }

        private string Save(string csCode, string csDate, string csShift, string seq, string checkGroup, string checkItems, string values, string remark, string checker, string confirmer, string updater, string type)
        {
            string query = string.Empty;

            if(string.IsNullOrEmpty(confirmer) == true)
            {
                query += "\r\n";
                query += "\r\n IF (SELECT TOP 1 CsCode FROM CsCheckSheet WHERE CsCode = '" + csCode + "' AND CsDate = '" + csDate + "' AND CsShift = '" + csShift + "' AND Seq = '" + seq + "') IS NULL ";
                query += "\r\n BEGIN ";

                //if (string.IsNullOrEmpty(values) == false || string.IsNullOrEmpty(remark) == false)
                //{
                    #region 최초

                    query += "\r\n";    // CsCheckSheet Insert
                    query += "\r\n      INSERT INTO CsCheckSheet (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Updated, Updater, [Type]) ";
                    query += "\r\n      VALUES ( ";
                    query += "\r\n              '" + csCode + "' ";
                    query += "\r\n             ,'" + csDate + "' ";
                    query += "\r\n             ,'" + csShift + "' ";
                    query += "\r\n             ,'" + seq + "' ";
                    query += "\r\n             ,'" + checkGroup + "' ";
                    query += "\r\n             ,'" + checkItems + "' ";
                    query += "\r\n             ,'" + values + "' ";
                    query += "\r\n             ,'" + remark + "' ";
                    query += "\r\n             ,GETDATE() ";
                    query += "\r\n             ,'" + checker + "' ";
                    query += "\r\n             ,GETDATE() ";
                    query += "\r\n             ,'" + updater + "' ";
                    query += "\r\n             ,'" + type + "' ";
                    query += "\r\n             ) ";
                    query += "\r\n";    // CsCheckSheetHist Insert
                    query += "\r\n      INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Updated, Updater, [Type]) ";
                    query += "\r\n      VALUES ( ";
                    query += "\r\n              '" + csCode + "' ";
                    query += "\r\n             ,'" + csDate + "' ";
                    query += "\r\n             ,'" + csShift + "' ";
                    query += "\r\n             ,'" + seq + "' ";
                    query += "\r\n             ,'" + checkGroup + "' ";
                    query += "\r\n             ,'" + checkItems + "' ";
                    query += "\r\n             ,'" + values + "' ";
                    query += "\r\n             ,'" + remark + "' ";
                    query += "\r\n             ,GETDATE() ";
                    query += "\r\n             ,'" + checker + "' ";
                    query += "\r\n             ,GETDATE() ";
                    query += "\r\n             ,'" + updater + "' ";
                    query += "\r\n             ,'" + type + "' ";
                    query += "\r\n             ) ";

                    #endregion
                //}
                //else
                //{
                //    query += "\r\n RETURN ";
                //}

                query += "\r\n";
                query += "\r\n END ";
                query += "\r\n ELSE ";
                query += "\r\n BEGIN ";

                query += "\r\n";    // 변동된 값이 있을 경우 진행
                query += "\r\n      IF EXISTS (SELECT TOP 1 NULL FROM CsCheckSheet WHERE    (CsCode = '" + csCode + "' AND CsDate = '" + csDate + "' AND CsShift = '" + csShift + "' AND Seq = '" + seq + "') ";
                query += "\r\n                                                          AND ( ";
                query += "\r\n                                                                  ISNULL(CheckGroup, '')  <> '" + checkGroup + "' ";
                query += "\r\n                                                               OR ISNULL(CheckItems, '')  <> '" + checkItems + "' ";
                query += "\r\n                                                               OR ISNULL([Values], '')    <> '" + values + "' ";
                query += "\r\n                                                               OR ISNULL(Remark, '')      <> '" + remark + "' ";
                query += "\r\n                                                               OR ISNULL(Checker, '')     <> '" + checker + "' ";
                query += "\r\n                                                               OR ISNULL(Updater, '')     <> '" + updater + "' ";
                query += "\r\n                                                               OR ISNULL([Type], '')      <> '" + type + "' ";
                query += "\r\n                                                              ) ";
                query += "\r\n                ) ";
                query += "\r\n      BEGIN ";

                #region 수정

                query += "\r\n";        // CsCheckSheet Update
                query += "\r\n           UPDATE CsCheckSheet ";
                query += "\r\n           SET     CheckGroup = '" + checkGroup + "' ";
                query += "\r\n                  ,CheckItems = '" + checkItems + "' ";
                query += "\r\n                  ,[Values]   = '" + values + "' ";
                query += "\r\n                  ,Remark     = '" + remark + "' ";
                query += "\r\n                  ,Checked    = GETDATE() ";
                query += "\r\n                  ,Checker    = '" + checker + "' ";
                query += "\r\n                  ,Updated    = GETDATE() ";
                query += "\r\n                  ,Updater    = '" + updater + "' ";
                query += "\r\n                  ,[Type]     = '" + type + "' ";
                query += "\r\n           WHERE  CsCode  = '" + csCode + "' ";
                query += "\r\n              AND CsDate  = '" + csDate + "' ";
                query += "\r\n              AND CsShift = '" + csShift + "' ";
                query += "\r\n              AND Seq     = '" + seq + "' ";
                query += "\r\n";        // CsCheckSheetHist Insert
                query += "\r\n           INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Updated, Updater, [Type]) ";
                query += "\r\n           VALUES ( ";
                query += "\r\n                   '" + csCode + "' ";
                query += "\r\n                  ,'" + csDate + "' ";
                query += "\r\n                  ,'" + csShift + "' ";
                query += "\r\n                  ,'" + seq + "' ";
                query += "\r\n                  ,'" + checkGroup + "' ";
                query += "\r\n                  ,'" + checkItems + "' ";
                query += "\r\n                  ,'" + values + "' ";
                query += "\r\n                  ,'" + remark + "' ";
                query += "\r\n                  ,GETDATE() ";
                query += "\r\n                  ,'" + checker + "' ";
                query += "\r\n                  ,GETDATE() ";
                query += "\r\n                  ,'" + updater + "' ";
                query += "\r\n                  ,'" + type + "' ";
                query += "\r\n                  ) ";

                #endregion

                query += "\r\n";
                query += "\r\n      END ";
                query += "\r\n";
                query += "\r\n END ";
            }
            else
            {
                query += "\r\n";
                query += "\r\n IF (SELECT TOP 1 CsCode FROM CsCheckSheet WHERE CsCode = '" + csCode + "' AND CsDate = '" + csDate + "' AND CsShift = '" + csShift + "' AND Seq = '" + seq + "') IS NULL ";
                query += "\r\n BEGIN ";

                #region 최초

                query += "\r\n";    // CsCheckSheet Insert
                query += "\r\n      INSERT INTO CsCheckSheet (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Confirmed, Confirmer, Updated, Updater, [Type]) ";
                query += "\r\n      VALUES ( ";
                query += "\r\n              '" + csCode + "' ";
                query += "\r\n             ,'" + csDate + "' ";
                query += "\r\n             ,'" + csShift + "' ";
                query += "\r\n             ,'" + seq + "' ";
                query += "\r\n             ,'" + checkGroup + "' ";
                query += "\r\n             ,'" + checkItems + "' ";
                query += "\r\n             ,'" + values + "' ";
                query += "\r\n             ,'" + remark + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + checker + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + confirmer + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + updater + "' ";
                query += "\r\n             ,'" + type + "' ";
                query += "\r\n             ) ";
                query += "\r\n";    // CsCheckSheetHist Insert
                query += "\r\n      INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Confirmed, Confirmer, Updated, Updater, [Type]) ";
                query += "\r\n      VALUES ( ";
                query += "\r\n              '" + csCode + "' ";
                query += "\r\n             ,'" + csDate + "' ";
                query += "\r\n             ,'" + csShift + "' ";
                query += "\r\n             ,'" + seq + "' ";
                query += "\r\n             ,'" + checkGroup + "' ";
                query += "\r\n             ,'" + checkItems + "' ";
                query += "\r\n             ,'" + values + "' ";
                query += "\r\n             ,'" + remark + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + checker + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + confirmer + "' ";
                query += "\r\n             ,GETDATE() ";
                query += "\r\n             ,'" + updater + "' ";
                query += "\r\n             ,'" + type + "' ";
                query += "\r\n             ) ";

                #endregion

                query += "\r\n";
                query += "\r\n END ";
                query += "\r\n ELSE ";
                query += "\r\n BEGIN ";

                query += "\r\n";    // 변동된 값이 있을 경우 진행
                query += "\r\n      IF EXISTS (SELECT TOP 1 NULL FROM CsCheckSheet WHERE    (CsCode = '" + csCode + "' AND CsDate = '" + csDate + "' AND CsShift = '" + csShift + "' AND Seq = '" + seq + "') ";
                query += "\r\n                                                          AND ( ";
                query += "\r\n                                                                  ISNULL(CheckGroup, '')  <> '" + checkGroup + "' ";
                query += "\r\n                                                               OR ISNULL(CheckItems, '')  <> '" + checkItems + "' ";
                query += "\r\n                                                               OR ISNULL([Values], '')    <> '" + values + "' ";
                query += "\r\n                                                               OR ISNULL(Remark, '')      <> '" + remark + "' ";
                query += "\r\n                                                               OR ISNULL(Checker, '')     <> '" + checker + "' ";
                query += "\r\n                                                               OR ISNULL(Confirmer, '')    = '' ";
                query += "\r\n                                                               OR ISNULL(Updater, '')     <> '" + updater + "' ";
                query += "\r\n                                                               OR ISNULL([Type], '')      <> '" + type + "' ";
                query += "\r\n                                                              ) ";
                query += "\r\n                ) ";
                query += "\r\n      BEGIN ";

                #region 수정

                query += "\r\n";        // CsCheckSheet Update
                query += "\r\n           UPDATE CsCheckSheet ";
                query += "\r\n           SET     CheckGroup = '" + checkGroup + "' ";
                query += "\r\n                  ,CheckItems = '" + checkItems + "' ";
                query += "\r\n                  ,[Values]   = '" + values + "' ";
                query += "\r\n                  ,Remark     = '" + remark + "' ";
                query += "\r\n                  ,Checked    = GETDATE() ";
                query += "\r\n                  ,Checker    = '" + checker + "' ";
                query += "\r\n                  ,Confirmed    = CASE WHEN ISNULL(Confirmer, '') = '' ";
                query += "\r\n                                  THEN GETDATE() ";
                query += "\r\n                                  ELSE Confirmed ";
                query += "\r\n                                  END ";
                query += "\r\n                  ,Confirmer    = CASE WHEN ISNULL(Confirmer, '') = '' ";
                query += "\r\n                                  THEN '" + confirmer + "' ";
                query += "\r\n                                  ELSE Confirmer ";
                query += "\r\n                                  END ";
                query += "\r\n                  ,Updated    = GETDATE() ";
                query += "\r\n                  ,Updater    = '" + updater + "' ";
                query += "\r\n                  ,[Type]     = '" + type + "' ";
                query += "\r\n           WHERE  CsCode  = '" + csCode + "' ";
                query += "\r\n              AND CsDate  = '" + csDate + "' ";
                query += "\r\n              AND CsShift = '" + csShift + "' ";
                query += "\r\n              AND Seq     = '" + seq + "' ";
                query += "\r\n";        // CsCheckSheetHist Insert
                query += "\r\n           INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Updated, Updater, [Type]) ";
                query += "\r\n           SELECT TOP 1 ";
                query += "\r\n                  CsCode ";
                query += "\r\n                 ,CsDate ";
                query += "\r\n                 ,CsShift ";
                query += "\r\n                 ,Seq ";
                query += "\r\n                 ,CheckGroup ";
                query += "\r\n                 ,CheckItems ";
                query += "\r\n                 ,[Values] ";
                query += "\r\n                 ,Renark ";
                query += "\r\n                 ,Checked ";
                query += "\r\n                 ,Checker ";
                query += "\r\n                 ,Confirmed ";
                query += "\r\n                 ,Confirmer ";
                query += "\r\n                 ,Updated ";
                query += "\r\n                 ,Updater ";
                query += "\r\n                 ,[Type] ";
                query += "\r\n           FROM   CsCheckSheet ";
                query += "\r\n           WHERE  CsCode  = '" + csCode + "' ";
                query += "\r\n              AND CsDate  = '" + csDate + "' ";
                query += "\r\n              AND CsShift = '" + csShift + "' ";
                query += "\r\n              AND Seq     = '" + seq + "' ";

                #endregion

                query += "\r\n";
                query += "\r\n END ";
            }

            return query;
        }

        private void btn_PrePage_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            this.tb_CurrentPage.Text = CurrentPage.ToString() + " / " + ((dt.Rows.Count / RowSize) + 1).ToString();

            Paging(CurrentPage);            
        }

        private void btn_NextPage_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            if ((dt.Rows.Count / RowSize) + 1 < CurrentPage)
            {
                CurrentPage = (dt.Rows.Count / RowSize) + 1;
            }
            this.tb_CurrentPage.Text = CurrentPage.ToString() + " / " + ((dt.Rows.Count / RowSize) + 1).ToString();

            Paging(CurrentPage);            
        }

        //재입력 입력, 비고 빈칸으로 만들기 Button
        private void btn_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ControlItems[i].Values = "";
                ControlItems[i].tb_ReMark.Text = "";
                ControlItems[i].tb_Value.BackColor = default(Color);
                ControlItems[i].btn_Value.BackColor = default(Color);
            }
        }

        //관리자 확인 Button
        private void btn_ManagerConfirm_Click(object sender, EventArgs e)
        {
            LoginManager lm = new LoginManager(dtp_CheckDate.Value.ToString("yyyy-MM-dd"), cb_Shift.Text.Split(':')[0], Convert.ToString(cb_Line.SelectedValue), this.Text);

            DialogResult dr = lm.ShowDialog();

            if (dr == DialogResult.OK)
            {
                string shift = "";

                if (this.lbl_Period.Text.ToUpper().Equals("SHIFTLY") == true)
                    shift = this.cb_Shift.Text.Split(':')[0];

                string query = string.Empty;

                StringBuilder query_Hist = new StringBuilder();

                for (int i = 0; i < ControlItems.Count; i++)
                {
                    DateTime convDate = DateTime.Now;

                    //여기는 한번에 관리자 체크 하는게 좋아 보이기도 함.
                    if (ControlItems[i].Values == "")
                    {

                    }
                    else
                    {
                        query = string.Empty;

                        query += "\r\n UPDATE    CsCheckSheet ";
                        query += "\r\n SET       Confirmed = GETDATE() ";
                        query += "\r\n           ,Confirmer = '" + ManagerCheck + "' ";
                        query += "\r\n WHERE     CsDate = '" + dtp_CheckDate.Value.ToString("yyyy-MM-dd") + "' ";
                        query += "\r\n       AND CsCode = '" + ControlItems[i].CsCode + "' ";
                        query += "\r\n       AND CsShift = '" + shift + "' ";
                        query += "\r\n       AND Seq = '" + ControlItems[i].Seq + "' ";
                        query += "\r\n       AND ISNULL(Confirmer, '') = '' ";

                        string query_Temp = string.Empty;

                        query_Temp += "\r\n";
                        query_Temp += "\r\n SELECT  TOP 1 Seq ";
                        query_Temp += "\r\n FROM    CsCheckSheet ";
                        query_Temp += "\r\n WHERE   CsDate = '" + dtp_CheckDate.Value.ToString("yyyy-MM-dd") + "' ";
                        query_Temp += "\r\n     AND CsCode = '" + ControlItems[i].CsCode + "' ";
                        query_Temp += "\r\n     AND CsShift = '" + shift + "' ";
                        query_Temp += "\r\n     AND Seq = '" + ControlItems[i].Seq + "' ";
                        query_Temp += "\r\n     AND ISNULL(Confirmer, '') = '' ";

                        object obj = DbAccess.Default.ExecuteScalar(query_Temp);

                        if (string.IsNullOrEmpty(Convert.ToString(obj)) == false)
                        {
                            query_Hist.Append("\r\n");
                            query_Hist.Append("\r\n INSERT INTO CsCheckSheetHist (CsCode, CsDate, CsShift, Seq, CheckGroup, CheckItems, [Values], Remark, Checked, Checker, Confirmed, Confirmer, Updated, Updater, [Type]) ");
                            query_Hist.Append("\r\n SELECT  CsCode ");
                            query_Hist.Append("\r\n         ,CsDate ");
                            query_Hist.Append("\r\n         ,CsShift ");
                            query_Hist.Append("\r\n         ,Seq ");
                            query_Hist.Append("\r\n         ,CheckGroup ");
                            query_Hist.Append("\r\n         ,CheckItems ");
                            query_Hist.Append("\r\n         ,[Values] ");
                            query_Hist.Append("\r\n         ,Remark ");
                            query_Hist.Append("\r\n         ,Checked ");
                            query_Hist.Append("\r\n         ,Checker ");
                            query_Hist.Append("\r\n         ,Confirmed ");
                            query_Hist.Append("\r\n         ,Confirmer ");
                            query_Hist.Append("\r\n         ,Updated ");
                            query_Hist.Append("\r\n         ,Updater ");
                            query_Hist.Append("\r\n         ,[Type] ");
                            query_Hist.Append("\r\n FROM    CsCheckSheet ");
                            query_Hist.Append("\r\n WHERE   CsDate = '" + dtp_CheckDate.Value.ToString("yyyy-MM-dd") + "' ");
                            query_Hist.Append("\r\n     AND CsCode = '" + ControlItems[i].CsCode + "' ");
                            query_Hist.Append("\r\n     AND CsShift = '" + shift + "' ");
                            query_Hist.Append("\r\n     AND Seq = '" + ControlItems[i].Seq + "' ");
                        }

                        DbAccess.Default.ExecuteQuery(query);
                    }
                }

                if (string.IsNullOrEmpty(query_Hist.ToString()) == false)
                    DbAccess.Default.ExecuteQuery(query_Hist.ToString());

                this.l_manager.Text = Convert.ToString(DbAccess.Default.ExecuteScalar("Select Text from Users where Userid = '" + ManagerCheck + "'")); //관리자 확인시 확인으로 했던 사람 이름 넣기 
            }
        }

        //패널쪽에 실선 그려서 표처럼 만들기 Paint
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Color c = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, (sender as Control).ClientRectangle, 
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid);
        }

        private void panel_Paint_top(object sender, PaintEventArgs e)
        {
            Color c = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, (sender as Control).ClientRectangle,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid);
        }

        //모형이 바뀌면 다시 그리기?
        private void panel_Resize(object sender, EventArgs e)
        {
            (sender as Control).Invalidate();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //D:주간/N:야간 값에 따른 변화
        private void cb_Shift_TextChanged(object sender, EventArgs e)
        {


            cb_flash();
                 
            Paging(CurrentPage);    //페이징 나누기
        }

        //라인명 값에 따른 변화
        private void cb_Route_TextChanged(object sender, EventArgs e)
        {
            cb_flash();

            Paging(CurrentPage);    //페이징 나누기 
        }

        //작성일자 값에 따른 변화
        private void dtp_CheckDate_ValueChanged(object sender, EventArgs e)
        {
            if (!string.Format("{0:u}", DateTime.Now).Substring(0, 10).Equals(dtp_CheckDate.Value.ToString("yyyy-MM-dd")))
            {
                System.Windows.Forms.MessageBox.Show("선택 날짜와 오늘 날짜가 다릅니다.");
            }
            cb_flash();
            
            Paging(CurrentPage);    //페이징 나누기 
        }

        //체크시트에 따라
        private void cb_CheckSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cb_CheckSheetCode.Text) == false)
            {
                DataTable dt = (DataTable)this.cb_CheckSheetCode.DataSource;

                DataRow row = dt.AsEnumerable().Where(r => r["CsCode"].Equals(this.cb_CheckSheetCode.Text)).FirstOrDefault();

                if (row != null)
                {
                    string period = Convert.ToString(row["CheckPeriod"]);

                    this.lbl_Period.Text = period;

                    if (period.ToUpper().Equals("SHIFTLY") == true)
                        this.cb_Shift.Enabled = true;
                    else
                        this.cb_Shift.Enabled = false;
                }
            }

            cb_flash();
            Paging(CurrentPage);    //페이징 나누기 
        }

        private void cb_flash()
        {
            l_manager.Text = "";

            ControlItems.Clear();
            
            string shift = "";

            if(this.lbl_Period.Text.ToUpper().Equals("SHIFTLY") == true)
            {
                shift = cb_Shift.Text.Split(':')[0];
            }

            string ShiftCheckQuery = string.Empty;

            ShiftCheckQuery += "\r\n SELECT * ";
            ShiftCheckQuery += "\r\n FROM   CsCheckSheet ";
            ShiftCheckQuery += "\r\n WHERE  CsDate = '" + dtp_CheckDate.Value.ToString("yyyy-MM-dd") + "' ";
            ShiftCheckQuery += "\r\n    AND CsCode = '" + cb_CheckSheetCode.Text.Split(':')[0] + "' ";
            ShiftCheckQuery += "\r\n    AND CsShift = '" + shift + "' ";
            ShiftCheckQuery += "\r\n ORDER BY Seq ";

            CheckDT = DbAccess.Default.GetDataTable(ShiftCheckQuery);

            if (CheckDT.Rows.Count > 0)
            {
                l_manager.Text = string.IsNullOrEmpty(CheckDT.Rows[0]["Confirmer"].ToString()) ? "" : Convert.ToString(DbAccess.Default.ExecuteScalar("Select Text from Users where Userid='" + CheckDT.Rows[0]["Confirmer"].ToString() + "'"));
            }
            else
            {
                l_manager.Text = "";
            }

            string script = "select * from CsSpecDetail where CsCode = '" + cb_CheckSheetCode.Text.Split(':')[0] + "' ";

            dt = DbAccess.Default.GetDataTable(script);

            try
            {
                if (CheckDT.Rows.Count > 0)     //Hist에 값이 있으면 입력,비고 가져오기
                {
                    //체크시트 CS24,25 의 값
                    DataTable CSDT = DbAccess.Default.GetDataTable("select * from CsSpecialValue2 where CsCode='" + cb_CheckSheetCode.Text.Split(':')[0] + "' and Type='" + CheckDT.Rows[0]["Values"].ToString() + "'");

                    for (int i = 0; i < CheckDT.Rows.Count; i++)
                    {
                        CsCheckSheetSpec_User cscontrol = new CsCheckSheetSpec_User()
                        {
                            Seq = dt.Rows[i]["Seq"].ToString()
                                ,
                            CsCode = dt.Rows[i]["CsCode"].ToString()
                             ,
                            CsShift = cb_Shift.Text.Split(':')[0].ToString()
                                 ,
                            CheckGroup = dt.Rows[i]["CheckGroup"].ToString()
                                ,
                            Checkitems = dt.Rows[i]["Checkitems"].ToString()
                                ,
                            Min = ((dt.Rows[i]["CsCode"].ToString().Equals("CS24") || dt.Rows[i]["CsCode"].ToString().Equals("CS25")) ? CSDT.Rows[i]["ValueMin"].ToString() : dt.Rows[i]["ValueMin"].ToString())
                                ,
                            Max = ((dt.Rows[i]["CsCode"].ToString().Equals("CS24") || dt.Rows[i]["CsCode"].ToString().Equals("CS25")) ? CSDT.Rows[i]["ValueMax"].ToString() : dt.Rows[i]["ValueMax"].ToString())//dt.Rows[i]["ValueMax"].ToString()
                                ,
                            type = Int32.Parse(dt.Rows[i]["DataType"].ToString())

                                    //Hist의 값
                                    ,
                            DataUnit = dt.Rows[i]["DataUnit"].ToString()
                                ,
                            Values = CheckDT.Rows[i]["Values"].ToString()
                                ,
                            Remark = CheckDT.Rows[i]["Remark"].ToString()

                                ,
                            Dock = DockStyle.Fill
                        };
                        cscontrol.ct04btnclick += new CsCheckSheetSpec_User.CsType04BtnClick(cscontrol_ct04btnclick);
                        ControlItems.Add(cscontrol);
                    }
                }
                else     // 없으면 빈칸으로 
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CsCheckSheetSpec_User cscontrol = new CsCheckSheetSpec_User()
                        {
                            Seq = dt.Rows[i]["Seq"].ToString()

                            ,
                            CsCode = dt.Rows[i]["CsCode"].ToString()
                            ,
                            CsShift = cb_Shift.Text.Split(':')[0].ToString()

                            ,
                            CheckGroup = dt.Rows[i]["CheckGroup"].ToString()
                            ,
                            Checkitems = dt.Rows[i]["Checkitems"].ToString()
                            ,
                            Min = dt.Rows[i]["ValueMin"].ToString()
                            ,
                            Max = dt.Rows[i]["ValueMax"].ToString()
                            ,
                            DataUnit = dt.Rows[i]["DataUnit"].ToString()
                            ,
                            Values = ""
                            ,
                            Remark = ""
                            ,
                            type = Int32.Parse(dt.Rows[i]["DataType"].ToString())

                            ,
                            Dock = DockStyle.Fill
                        };
                        cscontrol.ct04btnclick += new CsCheckSheetSpec_User.CsType04BtnClick(cscontrol_ct04btnclick);

                        cscontrol.cs06seq += new CsCheckSheetSpec_User.CS06SEQ(cscontrol_cs06seq);
                        ControlItems.Add(cscontrol);


                    }
                }
                this.tb_CurrentPage.Text = CurrentPage.ToString() + " / " + ((dt.Rows.Count / RowSize) + 1).ToString();        //가운데 페이지 ->  (현재 페이지 수)/(전체 페이지 수)
            }
            catch { }
        }

        //라인 변경시 작업자 이름
        private void cb_userdT()
        {
            try
            {
                string query = string.Empty;

                query += "\r\n SELECT  W.Worker ";
                query += "\r\n         ,W.Text ";
                query += "\r\n         ,W.Bunch ";
                query += "\r\n         ,W.Shift ";
                query += "\r\n         ,W.Updated ";
                query += "\r\n         ,W.Status ";
                query += "\r\n FROM    Worker W ";
                query += "\r\n JOIN    Common C ";
                query += "\r\n     ON  W.Bunch = C.Common ";
                query += "\r\n WHERE   W.Status = 1 ";
                
                DataTable dt = DbAccess.Default.GetDataTable(query);

                this.cb_User.Items.Clear();
                this.cb_User.Items.Add("");

                foreach (DataRow dr in dt.Rows)
                {
                    this.cb_User.Items.Add(dr["Worker"].ToString() + ":" + dr["Text"].ToString());
                }
            }
            catch { }
        }


        private void cb_Line_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectQuery = string.Empty;

                selectQuery += "\r\n SELECT  CS.CsCode + ':' + CsName AS CsCode ";
                selectQuery += "\r\n         ,Line ";
                selectQuery += "\r\n         ,CS.CheckPeriod ";
                selectQuery += "\r\n FROM   CsSpec CS ";
                selectQuery += "\r\n JOIN   CsSpecDetail CCSS ";
                selectQuery += "\r\n    ON  CS.CsCode = CCSS.CsCode ";
                selectQuery += "\r\n WHERE  Line = '" + this.cb_Line.Text.Split(':')[0] + "' ";
                selectQuery += "\r\n    AND CS.Status = 1 ";
                selectQuery += "\r\n GROUP BY CS.CsCode, Line, CsName, CS.CheckPeriod ";

                DataTable dt = DbAccess.Default.GetDataTable(selectQuery);

                cb_CheckSheetCode.DataSource = dt;
                this.cb_CheckSheetCode.SelectedIndex = 0;
                cb_CheckSheetCode.DisplayMember = "CsCode";     //보여주는 곳
            }
            catch { }

            cb_userdT();             //체크시트 라인명 변경에 따라 작업장정보에 있는 것 가져오기
        }

        private void btn_AllPlanStop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ControlItems.Count; i++)
            {
                if (ControlItems[i].type == 0)
                {
                    //ControlItems[i].Values = "계획정지";
                    ControlItems[i].Values = "PlanStop";
                    ControlItems[i].btn_Value.BackColor = default(Color);

                }
                else if (ControlItems[i].type == 1 || ControlItems[i].type == 2 || ControlItems[i].type == 3)
                {
                    //ControlItems[i].Values = "계획정지";
                    ControlItems[i].Values = "PlanStop";
                }
            }
        }
    }
}
