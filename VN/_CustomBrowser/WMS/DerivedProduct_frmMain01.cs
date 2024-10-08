
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

namespace WiseM.Browser
{
    public partial class DerivedProduct_frmMain01 : Form
    {
        private DataTable dtMapping = new DataTable();
        private DataTable dtPacking = new DataTable();
        private DataTable dtCombined = new DataTable();

        private string strUser = "";

        public DerivedProduct_frmMain01(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void DerivedProduct_frmMain01_Load(object sender, EventArgs e)
        {
            this.btnClear.PerformClick();
        }

        private void InsertIntoSysLog(string strMsg)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'DerivedProduct', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dtMapping.Clear();
            this.dtPacking.Clear();
            this.dtCombined.Clear();
            this.txtMappingQty.Text = "0";
            this.txtPackingQty.Text = "0";
            this.txtCombinedQty.Text = "0";
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        private void btnSelMappingExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    DefaultExt = @"Excel Files(.xlsx)|*.xlsx",
                    Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm",
                    FilterIndex = 2
                };

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;

                string strColumns = "Old_Material, New_Material, Old_PCB, New_PCB";

                int intRc = 0;
                this.dtMapping = GetDataTableWithExcel($"{openFileDialog.FileName}", strColumns, ref intRc);
                if (intRc != 0) return;


                // 중복 PCB번호 및 PCB번호 길이 등 검증
                List<string> list = new List<string>();
                List<string> errList = new List<string>();
                bool isEmptyColumnExists = false;

                foreach (DataRow row in this.dtMapping.Rows)
                {
                    string strOld_Material = row["Old_Material"].ToString();
                    string strNew_Material = row["New_Material"].ToString();
                    string strOld_PCB = row["Old_PCB"].ToString();
                    string strNew_PCB = row["New_PCB"].ToString();

                    if (strOld_Material == "" || strNew_Material == "" || strOld_PCB == "" || strNew_PCB == "")
                    {
                        isEmptyColumnExists = true;
                        break;
                    }

                    if ((list.Contains(strOld_PCB) || strOld_PCB.Length != 17) && !errList.Contains(strOld_PCB)) errList.Add(strOld_PCB);
                    else list.Add(strOld_PCB);

                    if ((list.Contains(strNew_PCB) || strNew_PCB.Length != 17) && !errList.Contains(strNew_PCB)) errList.Add(strNew_PCB);
                    else list.Add(strNew_PCB);
                }

                if (isEmptyColumnExists)
                {
                    string strTmp = "비어있는 컬럼이 있으면 안됩니다.\n\n";
                    strTmp += "There should be no empty columns.\n\n\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (errList.Count > 0)
                {
                    string strTmp = "중복되거나, PCB번호 길이가 다른 번호가 존재합니다.\n\n";
                    strTmp += "Duplicate, or wrong length PCB Numbers exist.\n\n\n";

                    foreach (string item in errList)
                        strTmp += item + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.dgvMapping.DataSource = this.dtMapping;

                this.txtMappingQty.Text = this.dtMapping.Rows.Count.ToString();

                foreach (DataGridViewColumn col in this.dgvMapping.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                this.dgvMapping.Columns["Old_Material"].Width = 120;
                this.dgvMapping.Columns["New_Material"].Width = 120;
                this.dgvMapping.Columns["Old_PCB"].Width = 180;
                this.dgvMapping.Columns["New_PCB"].Width = 180;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSelPackingExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    DefaultExt = @"Excel Files(.xlsx)|*.xlsx",
                    Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm",
                    FilterIndex = 2
                };

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;

                string strColumns = "New_Pallet, New_Box, New_PCB";

                int intRc = 0;
                this.dtPacking = GetDataTableWithExcel($"{openFileDialog.FileName}", strColumns, ref intRc);
                if (intRc != 0) return;


                // 중복 PCB번호 및 PCB번호 길이 등 검증
                List<string> list = new List<string>();
                List<string> errList = new List<string>();
                bool isEmptyColumnExists = false;

                foreach (DataRow row in this.dtPacking.Rows)
                {
                    string strNew_Pallet = row["New_Pallet"].ToString();
                    string strNew_Box = row["New_Box"].ToString();
                    string strNew_PCB = row["New_PCB"].ToString();

                    if (strNew_Pallet == "" || strNew_Box == "" || strNew_PCB == "")
                    {
                        isEmptyColumnExists = true;
                        break;
                    }

                    if ((list.Contains(strNew_PCB) || strNew_PCB.Length != 17) && !errList.Contains(strNew_PCB)) errList.Add(strNew_PCB);
                    else list.Add(strNew_PCB);
                }

                if (isEmptyColumnExists)
                {
                    string strTmp = "비어있는 컬럼이 있으면 안됩니다.\n\n";
                    strTmp += "There should be no empty columns.\n\n\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (errList.Count > 0)
                {
                    string strTmp = "중복되거나, PCB번호 길이가 다른 번호가 존재합니다.\n\n";
                    strTmp += "Duplicate, or wrong length PCB Numbers exist.\n\n\n";

                    foreach (string item in errList)
                        strTmp += item + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.dgvPacking.DataSource = this.dtPacking;

                this.txtPackingQty.Text = this.dtPacking.Rows.Count.ToString();

                foreach (DataGridViewColumn col in this.dgvPacking.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                this.dgvPacking.Columns["New_Pallet"].Width = 210;
                this.dgvPacking.Columns["New_Box"].Width = 210;
                this.dgvPacking.Columns["New_PCB"].Width = 180;
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

                    string sheetName = dt.Rows[0]["TABLE_NAME"].ToString();                                                   //엑셀 첫 번째 시트명
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

        private void btnCombine_Click(object sender, EventArgs e)
        {
            if (!this.txtMappingQty.Text.Equals(this.txtPackingQty.Text))
            {
                string strTmp = "매핑수량과 패킹수량은 같아야 합니다.\n\n";
                strTmp += "The Mapping-Qty and the Packing-Qty must be the same.\n\n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            this.dtCombined.Clear();
            this.dtCombined.Columns.Clear();
            this.dtCombined.Columns.AddRange(new DataColumn[] {
                new DataColumn("Old_Material"),
                new DataColumn("New_Material"),
                new DataColumn("New_Pallet"),
                new DataColumn("New_Box"),
                new DataColumn("Old_PCB"),
                new DataColumn("New_PCB")
            });

            foreach (DataRow row in this.dtMapping.Rows)
            {
                string strNew_PCB = row["New_PCB"].ToString();
                DataRow[] fRows = dtPacking.Select($"New_PCB = '{strNew_PCB}'");

                if (fRows.Length == 0)
                {
                    string strTmp = "패킹정보에 없는 PCB번호가 존재합니다.\n\n";
                    strTmp += "There is a PCB that dows not exist int the Packing Info.\n\n";
                    strTmp += $"[ {strNew_PCB} ] \n\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (fRows.Length > 1)
                {
                    string strTmp = "중복된 PCB번호가 존재합니다.\n\n";
                    strTmp += "Duplicate PCBs exist.\n\n";
                    strTmp += $"[ {strNew_PCB} ] \n\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.dtCombined.Rows.Add(row["Old_Material"],
                                         row["New_Material"],
                                         fRows[0]["New_Pallet"],
                                         fRows[0]["New_Box"],
                                         row["Old_PCB"],
                                         row["New_PCB"]);
            }

            this.dgvCombined.DataSource = this.dtCombined;

            this.txtCombinedQty.Text = this.dtCombined.Rows.Count.ToString();

            foreach (DataGridViewColumn col in this.dgvCombined.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dgvCombined.Columns["Old_Material"].Width = 120;
            this.dgvCombined.Columns["New_Material"].Width = 120;
            this.dgvCombined.Columns["New_Pallet"].Width = 210;
            this.dgvCombined.Columns["New_Box"].Width = 210;
            this.dgvCombined.Columns["Old_PCB"].Width = 180;
            this.dgvCombined.Columns["New_PCB"].Width = 180;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtCombined.Rows.Count < 1)
                {
                    string strTmp = "PCB 내역이 입력되지 않았습니다.\n\n";
                    strTmp += "There is no PCB number.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 실적 처리
                string PS_GUBUN = "DERIVEDPRODUCT_SAVE";
                string PS_Excel = "";
                string PS_ClientId = this.strUser;

                MemoryStream ms1 = new MemoryStream();
                StreamWriter sw1 = new StreamWriter(ms1);
                sw1.AutoFlush = true;
                foreach (DataRow row in this.dtCombined.Rows)
                {
                    sw1.Write(row["Old_Material"] + "\x07");
                    sw1.Write(row["New_Material"] + "\x07");
                    sw1.Write(row["New_Pallet"] + "\x07");
                    sw1.Write(row["New_Box"] + "\x07");
                    sw1.Write(row["Old_PCB"] + "\x07");
                    sw1.Write(row["New_PCB"] + "\x07");

                    //PS_Excel += row["Old_Material"].ToString() + "\x07";
                    //PS_Excel += row["New_Material"].ToString() + "\x07";
                    //PS_Excel += row["New_Pallet"].ToString() + "\x07";
                    //PS_Excel += row["New_Box"].ToString() + "\x07";
                    //PS_Excel += row["Old_PCB"].ToString() + "\x07";
                    //PS_Excel += row["New_PCB"].ToString() + "\x07";
                }

                PS_Excel = Encoding.Default.GetString(ms1.ToArray());
                if (PS_Excel.Length > 0) PS_Excel = PS_Excel.Remove(PS_Excel.Length - 1, 1);

                string strCmd = $@"exec [Sp_DerivedProductProcedure]
                                @PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_Excel      = '{PS_Excel}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd, CommandType.Text, null, 10800);
                if (ds1 == null || ds1.Tables.Count == 0)
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

                    string strTmp = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"] + "\n";
                    foreach (DataRow row in ds1.Tables[ds1.Tables.Count - 2].Rows)
                        strTmp += row[0] + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MessageBox.Show("Saved successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                this.btnClear.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

    }
}
