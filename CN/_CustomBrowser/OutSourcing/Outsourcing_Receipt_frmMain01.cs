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
    public partial class Outsourcing_Receipt_frmMain01 : Form
    {
        #region F

        private DataTable dtWO = new DataTable();
        private DataTable dtPcb = new DataTable();
        private DataTable dtMat = new DataTable();

        private string strReceiptType = "";
        private string strUser = "";

        #endregion

        #region Construtor

        public Outsourcing_Receipt_frmMain01(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        #endregion

        #region Method

        private DataTable GetDataTableWithExcel(string selectPath, string strColumns, ref int intRc)
        {
            Cursor.Current = Cursors.WaitCursor;

            string oledbConnectionString;

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

        private void ClearScreen()
        {
            dtWO.Rows.Clear();
            dtWO.Columns.Clear();
            dtPcb = null;
            dtMat = null;
            txtPcbQty.Text = "";
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO {Outsourcing_Receipt_frmMain00.strDbName}SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Outsourcing_Receipt', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch
            {
            }
        }

        #endregion

        #region Event

        private void Outsourcing_Receipt_frmMain01_Load(object sender, EventArgs e)
        {
            try
            {
                var dictionary = new Dictionary<string, string>
                {
                    { "980328", "青岛工厂(청도공장)" },
                    { "C0010", "青岛稀世电子(시스)" },
                    { "C0058", "青岛丰汇电子(평휘)" }
                };
                cmbWorkCenter.DataSource = new BindingSource(dictionary, null);
                cmbWorkCenter.ValueMember = "key";
                cmbWorkCenter.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSelWO_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rbSemiFinished.Checked && !rbFinished.Checked)
                {
                    string strTmp = "입고 형태가 선택되지 않았습니다.\n\n";
                    strTmp += "You have to select Receipt Type.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                strReceiptType = rbSemiFinished.Checked ? "Semi-Finished" : "Finished";

                if (cmbWorkCenter.SelectedIndex < 0)
                {
                    string strTmp = "외주워크센터가 선택되지 않았습니다.\n\n";
                    strTmp += "You have to select Outsourcing WorkCenter.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var _form1 = new Outsourcing_Receipt_frmSub10_SelectWO(strUser, strReceiptType, cmbWorkCenter.SelectedValue as string);
                if (DialogResult.Yes != _form1.ShowDialog())
                    return;

                dtWO = _form1.dtSelectedWO;
                dgv01.DataSource = dtWO;

                foreach (DataGridViewColumn col in dgv01.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                // this.dtPcb.Rows.Clear();
                // this.dtMat.Rows.Clear();
                dtPcb = null;
                dtMat = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSelPcbExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtWO.Rows.Count == 0 || dtWO.Select("isnull(WorkOrder,'') = ''").Length == dtWO.Rows.Count)
                {
                    string strTmp = "MES 작업지시가 선택되지 않았습니다.\n\n";
                    strTmp += "You have to select WorkOrder for MES.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var openFileDialog = new OpenFileDialog
                {
                    Multiselect = false,
                    DefaultExt = @"Excel Files(.xlsx)|*.xlsx",
                    Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm",
                    FilterIndex = 2
                };

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;

                string strColumns = this.strReceiptType == "Finished" ? "Material, Pallet, Box, PCB" : "PCB";

                int intRc = 0;
                this.dtPcb = GetDataTableWithExcel($"{openFileDialog.FileName}", strColumns, ref intRc);
                if (intRc != 0) return;


                // 중복 PCB번호 및 PCB번호 길이 검증
                var list = new List<string>();
                var errList = new List<string>();

                foreach (DataRow row in this.dtPcb.Rows)
                {
                    string strPcb = row["PCB"].ToString();
                    if ((list.Contains(strPcb) || strPcb.Length != 17) && !errList.Contains(strPcb))
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

                    strTmp = errList.Aggregate(strTmp, (current, item) => current + (item + "\n"));

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                // PCB 인쇄여부 검증 ('시스'것만, 'Semi-Finished' 및 'Finished' 모두 검증. '평휘'것은 Outsourcing_FinishedGoods 프로그램에서 처리하는 것임)
                string PS_GUBUN = "OUTSOURCING_VERIFY_PCB";
                string PS_ClientId = this.strUser;
                string PS_WorkOrder = "";
                string PS_PcbType = this.strReceiptType;
                string PS_PcbNo = "";
                string PS_MatAndQty = "";

                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms)
                {
                    AutoFlush = true
                };
                foreach (DataRow row in this.dtPcb.Rows)
                {
                    sw.Write(row["PCB"] + "\x07");
                }

                PS_PcbNo = Encoding.UTF8.GetString(ms.ToArray());
                if (PS_PcbNo.Length > 0) PS_PcbNo = PS_PcbNo.Remove(PS_PcbNo.Length - 1, 1);

                string strCmd = $@"exec {Outsourcing_Receipt_frmMain00.strDbName}[Sp_OutSourcingProcedureV4]
                            @PS_GUBUN		= '{PS_GUBUN}'
                            ,@PS_ClientId   = '{PS_ClientId}'
                            ,@PS_WorkOrder  = '{PS_WorkOrder}'
                            ,@PS_PcbType    = '{PS_PcbType}'
                            ,@PS_PcbNo      = '{PS_PcbNo}'
                            ,@PS_MatAndQty  = '{PS_MatAndQty}'
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
                    //N이상 시 오류발생
                    // foreach (DataRow row in ds1.Tables[ds1.Tables.Count - 2].Rows)
                    //     strTmp += row[0].ToString() + "\n";

                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                // 완제품인 경우 자재(제품)코드/Box번호/Pallet번호가 반드시 있어야 함.
                if (this.strReceiptType == "Finished")
                {
                    if (dtPcb.Select("isnull(Material,'') = ''").Any())
                    {
                        string strTmp = "자재(제품)코드가 없는 PCB가 존재합니다.\n\n";
                        strTmp += "PCBs without Material(Product) Code exist.\n\n";
                        MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (dtPcb.Select("isnull(Box,'') = ''").Any())
                    {
                        string strTmp = "박스번호가 없는 PCB가 존재합니다.\n\n";
                        strTmp += "PCBs without Box Numbers exist.\n\n";
                        MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (dtPcb.Select("isnull(Pallet,'') = ''").Any())
                    {
                        string strTmp = "팔레트번호가 없는 PCB가 존재합니다.\n\n";
                        strTmp += "PCBs without Pallet Numbers exist.\n\n";
                        MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }


                dgv02.DataSource = dtPcb;

                txtPcbQty.Text = dtPcb.Rows.Count.ToString();

                foreach (DataGridViewColumn col in dgv02.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSelMaterialExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtWO.Rows.Count == 0 ||
                    dtWO.Select("isnull(WorkOrder,'') = ''").Length == dtWO.Rows.Count)
                {
                    string strTmp = "MES 작업지시가 선택되지 않았습니다.\n\n";
                    strTmp += "You have to select WorkOrder for MES.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var openFileDialog = new OpenFileDialog()
                {
                    Multiselect = false,
                    DefaultExt = @"Excel Files(.xlsx)|*.xlsx",
                    Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm",
                    FilterIndex = 2
                };

                if (DialogResult.OK != openFileDialog.ShowDialog()) return;
                int intRc = 0;
                dtMat = GetDataTableWithExcel($"{openFileDialog.FileName}", "WorkOrder, Material, Qty", ref intRc);
                if (intRc != 0) return;


                // 워크오더, 자재코드, 수량 검증
                foreach (DataRow row in dtMat.Rows)
                {
                    string strWO = row["WorkOrder"].ToString();
                    string strMat = row["Material"].ToString();

                    bool qtyFail = false;
                    try
                    {
                        Single sngQty = Convert.ToSingle(row["Qty"]);
                    }
                    catch
                    {
                        qtyFail = true;
                    }

                    if (strWO.Length < 1 ||
                        strMat.Length < 1 ||
                        qtyFail == true ||
                        !dtWO.Select($"WorkOrder ='{strWO}'").Any())
                    {
                        string strTmp = "작업지시번호가 일치하지 않거나,\n자재코드가 없거나,\n수량이 잘못된 정보가 존재합니다.\n\n";
                        strTmp += "WorkOrder does not match,\nor Material Code does not exist,\nor wrong Qty exist.\n\n";
                        MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }


                dgv03.DataSource = dtMat;

                foreach (DataGridViewColumn col in dgv03.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtWO.Rows.Count < 1 || dtWO.Select("isnull(WorkOrder,'') = ''").Length == dtWO.Rows.Count)
                {
                    string strTmp = "MES 작업지시가 선택되지 않았습니다.\n\n";
                    strTmp += "You have to select WorkOrder for MES.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (dtPcb.Rows.Count < 1)
                {
                    string strTmp = "PCB 내역이 입력되지 않았습니다.\n\n";
                    strTmp += "There is no PCB number.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // if (this.dtMat.Rows.Count < 1)
                // {
                //     string strTmp = "사용자재내역이 입력되지 않았습니다.\n\n";
                //     strTmp += "There is no Used Material Information.\n\n";
                //     MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //     return;
                // }


                // 실적 처리
                string PS_GUBUN = "OUTSOURCING_SAVE";
                string PS_ClientId = this.strUser;
                string PS_WorkOrder = "";
                string PS_PcbType = this.strReceiptType;
                string PS_PcbNo = "";
                string PS_MatAndQty = "";

                foreach (DataRow row in dtWO.Rows)
                {
                    if (row["WorkOrder"].ToString() == "") continue;

                    PS_WorkOrder += row["Key_Routing"] + "\x03" + row["WorkOrder"] + "\x07";
                }

                if (PS_WorkOrder.Length > 0) PS_WorkOrder = PS_WorkOrder.Remove(PS_WorkOrder.Length - 1, 1);

                foreach (DataRow row in dtPcb.Rows)
                {
                    if (strReceiptType == "Semi-Finished")
                    {
                        PS_PcbNo += row["PCB"] + "\x07";
                    }
                    else
                    {
                        PS_PcbNo += row["Material"] + "\x07";
                        PS_PcbNo += row["Pallet"] + "\x07";
                        PS_PcbNo += row["Box"] + "\x07";
                        PS_PcbNo += row["PCB"] + "\x07";
                    }
                }

                if (PS_PcbNo.Length > 0) PS_PcbNo = PS_PcbNo.Remove(PS_PcbNo.Length - 1, 1);

                if (dtMat != null)
                {
                    foreach (DataRow row in this.dtMat.Rows)
                    {
                        PS_MatAndQty += row["WorkOrder"] + "\x07";
                        PS_MatAndQty += row["Material"] + "\x07";
                        PS_MatAndQty += row["Qty"] + "\x07";
                    }

                    if (PS_MatAndQty.Length > 0) PS_MatAndQty = PS_MatAndQty.Remove(PS_MatAndQty.Length - 1, 1);
                }

                string strCmd = $@"exec {Outsourcing_Receipt_frmMain00.strDbName}[Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                                ,@PS_WorkOrder  = '{PS_WorkOrder}'
                                ,@PS_PcbType    = '{PS_PcbType}'
                                ,@PS_PcbNo      = '{PS_PcbNo}'
                                ,@PS_WorkCenter = '{cmbWorkCenter.SelectedValue}'
                                ,@PS_MatAndQty  = '{PS_MatAndQty}'
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

                this.ClearScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void rbSemiFinished_CheckedChanged(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void rbFinished_CheckedChanged(object sender, EventArgs e)
        {
            ClearScreen();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        #endregion
    }
}