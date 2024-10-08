
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

namespace WiseM.Browser
{
    public partial class Outsourcing_FinishedGoods_frmMain01 : Form
    {
        private DataTable dtPcb = new DataTable();

        private string strUser = "";

        public Outsourcing_FinishedGoods_frmMain01(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void btnSelPcbExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog()
                                     {
                                         Multiselect = false, DefaultExt = @"Excel Files(.xlsx)|*.xlsx", Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm", FilterIndex = 2
                                     };

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;

                string strColumns = "Material, Pallet, Box, PCB";

                int intRc = 0;
                this.dtPcb = GetDataTableWithExcel($"{openFileDialog.FileName}", strColumns, ref intRc);
                if (intRc != 0) return;


                // 중복 PCB번호 및 PCB번호 길이 검증
                List<string> list = new List<string>();
                List<string> errList = new List<string>();

                foreach (DataRow row in this.dtPcb.Rows)
                {
                    string strPcb = row["PCB"].ToString();
                    if ((list.Contains(strPcb) || strPcb.Length != 17)
                        && !errList.Contains(strPcb))
                    {
                        errList.Add(strPcb);
                    }
                    else
                    {
                        list.Add(strPcb);
                    }
                }

                if (errList.Count > 0)
                {
                    string strTmp = "중복되거나, PCB번호 길이가 다른 번호가 존재합니다.\n\n";
                    strTmp += "Duplicate, or wrong length PCB Numbers exist.\n\n\n";

                    foreach (string item in errList)
                        strTmp += item + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 완제품인 경우 자재(제품)코드/Box번호/Pallet번호가 반드시 있어야 함.
                if (this.dtPcb.Select("isnull(Material,'') = ''").Length > 0)
                {
                    string strTmp = "자재(제품)코드가 없는 PCB가 존재합니다.\n\n";
                    strTmp += "PCBs without Material(Product) Code exist.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (this.dtPcb.Select("isnull(Box,'') = ''").Length > 0)
                {
                    string strTmp = "박스번호가 없는 PCB가 존재합니다.\n\n";
                    strTmp += "PCBs without Box Numbers exist.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (this.dtPcb.Select("isnull(Pallet,'') = ''").Length > 0)
                {
                    string strTmp = "팔레트번호가 없는 PCB가 존재합니다.\n\n";
                    strTmp += "PCBs without Pallet Numbers exist.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                this.dgv02.DataSource = this.dtPcb;

                this.txtPcbQty.Text = this.dtPcb.Rows.Count.ToString();

                foreach (DataGridViewColumn col in this.dgv02.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private DataTable GetDataTableWithExcel(string selectPath, string strColumns, ref int intRc)
        {
            Cursor.Current = Cursors.WaitCursor;

            string oledbConnectionString = string.Empty;

            if (selectPath.IndexOf(".xlsx", StringComparison.Ordinal) > -1)
            {
                //엑셀 2007
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + selectPath + ";Extended Properties=\"Excel 12.0\"";
            }
            else
            {
                //엑셀 2003 및 이하 버전
                oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + selectPath + ";Extended Properties=\"Excel 8.0\"";
            }

            DataTable dataTable = null;
            try
            {
                using (var oleDbConnection = new OleDbConnection(oledbConnectionString))
                {
                    oleDbConnection.Open();
                    DataTable dt = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string sheetName = dt.Rows[0]["TABLE_NAME"].ToString(); //엑셀 첫 번째 시트명
                    //string sQuery = $" SELECT WorkOrder, Material, Qty FROM [{sheetName}] "; //쿼리
                    string sQuery = $" SELECT {strColumns} FROM [{sheetName}] "; //쿼리

                    dataTable = new DataTable();
                    var oleDbDataAdapter = new OleDbDataAdapter(sQuery, oleDbConnection);
                    oleDbDataAdapter.Fill(dataTable);
                }

                intRc = 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                intRc = -1;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            return dataTable;
        }

        private static string ReplaceTextWithRegex(string text)
        {
            return Regex.Replace(text, @"[^a-zA-Z0-9가-힣]", "", RegexOptions.Singleline);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtPcb.Rows.Count < 1)
                {
                    string strTmp = "PCB 내역이 입력되지 않았습니다.\n\n";
                    strTmp += "There is no PCB number.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 실적 처리
                string PS_GUBUN = "OUTSOURCING_FINISHEDGOODS";
                string PS_ClientId = this.strUser;
                string PS_PcbNo = "";

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, Encoding.Default))
                    {
                        streamWriter.AutoFlush = true;
                        foreach (DataRow row in dtPcb.Rows)
                        {
                            streamWriter.Write($"{ReplaceTextWithRegex($"{row["Material"]}")}\x07");
                            streamWriter.Write($"{ReplaceTextWithRegex($"{row["Pallet"]}")}\x07");
                            streamWriter.Write($"{ReplaceTextWithRegex($"{row["Box"]}")}\x07");
                            streamWriter.Write($"{ReplaceTextWithRegex($"{row["PCB"]}")}\x07");
                        }
                    }
                    PS_PcbNo = Encoding.Default.GetString(memoryStream.ToArray());
                }
                if (PS_PcbNo.Length > 0) PS_PcbNo = PS_PcbNo.Remove(PS_PcbNo.Length - 1, 1);

                string strCmd = $@"exec [Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                                ,@PS_PcbNo      = '{PS_PcbNo}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd, CommandType.Text, null, 10800);
                if (ds1 == null
                    || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (ds1.Tables.Count == 1)
                    {
                        if (intRC == -999)
                        {
                            MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                        }
                    }

                    string strTmp = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() + "\n";
                    foreach (DataRow row in ds1.Tables[ds1.Tables.Count - 2].Rows)
                        strTmp += row[0].ToString() + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show("Saved successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                this.ClearScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void ClearScreen()
        {
            this.dtPcb.Rows.Clear();
            this.dtPcb.Columns.Clear();
            this.txtPcbQty.Text = "";
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Outsourcing_FinishedGoods', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch
            {
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        private void btnMgmt_Click(object sender, EventArgs e)
        {
            try
            {
                Outsourcing_FinishedGoods_frmMain10 _form1 = new Outsourcing_FinishedGoods_frmMain10(this.strUser);
                _form1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }
    }
}
