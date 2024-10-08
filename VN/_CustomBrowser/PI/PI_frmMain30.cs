
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
using EX = Excel;


namespace WiseM.Browser
{
    public partial class PI_frmMain30 : Form
    {
        private DataTable dtPI = new DataTable();
        private DataTable dtPI_List = new DataTable();

        private string PS_RESVNO = "";
        private string strPI_Status = "";
        private string strUser = "";

        public PI_frmMain30(string strUser, string PS_RESVNO)
        {
            InitializeComponent();

            this.strUser = strUser;
            this.PS_RESVNO = PS_RESVNO;
        }

        private void PI_frmMain30_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            GetDataFromPI_ResvMaster();
            GetDataFromPI_Master();
            SetButtonsAndGrid();
        }

        private void GetDataFromPI_ResvMaster()
        {
            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_GET_RESVMASTER";
                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO		= '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (intRC != -999)
                        throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                dtPI = ds1.Tables[0];
                dgv01.DataSource = dtPI;

                dgv01.Columns["Seq"].Visible = false;

                strPI_Status = dtPI.Rows[0]["PI_Status"].ToString();

                foreach (DataGridViewColumn col in dgv01.Columns)
                {
                    //if (col.Name != "Chk") col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void GetDataFromPI_Master()
        {
            try
            {
                string PS_BUNCH = "RawMaterial";

                string PS_GUBUN;
                switch (strPI_Status)
                {
                    case "OnGoing":
                        PS_GUBUN = "RM_GET_PIMASTER";
                        break;
                    case "ScanEnded":
                        PS_GUBUN = "RM_GET_PIMASTER_AFTER_SCANINFO_APPLIED";
                        break;
                    case "Completed":
                    case "Deleted":
                        PS_GUBUN = "RM_GET_PIMASTER";
                        break;
                    default:
                        MessageBox.Show("Something wrong. Call IT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO		= '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (intRC != -999)
                        throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                dtPI_List.Rows.Clear();
                dtPI_List.Columns.Clear();

                dtPI_List = ds1.Tables[0];

                
                dgv02.DataSource = dtPI_List;
                dgv02.Columns["RecordID"].Visible = false;

                foreach (DataGridViewColumn col in dgv02.Columns)
                {
                    col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                if (strPI_Status == "ScanEnded")
                {
                    #region 이거 시간 많이 걸림
                    //foreach (DataGridViewRow row in this.dgv02.Rows)
                    //{
                    //    if ((bool)row.Cells["ApplyFromPI"].Value == true)
                    //        row.Cells["ApplyFromPI"].ReadOnly = false;
                    //    else if ((bool)row.Cells["DeleteStock"].Value == true)
                    //        row.Cells["DeleteStock"].ReadOnly = false;
                    //}
                    #endregion

                    dgv02.Columns["ApplyFromPI"].ReadOnly = false;
                    dgv02.Columns["DeleteStock"].ReadOnly = false;

                }

                #region Linq Test...
                //DataTable dtLoc = this.dtPI_List.AsEnumerable()
                //                    .Where(r => r["Location"].Equals("L-8")).CopyToDataTable();

                //DataTable dtLoc = this.dtPI_List.AsEnumerable()
                //                    .GroupBy(g => new { Col1 = g["Location"] })
                //                    .Select(g => g.OrderBy(r => r["Location"]).First())
                //                    .CopyToDataTable();

                //DataTable dtLoc = this.dtPI_List.Select("").CopyToDataTable();

                //var dtloc = this.dtPI_List.AsEnumerable()
                //                    .GroupBy(r => r.Field<string>("Location"))
                //                    ;

                //var dtLoc = this.dtPI_List.AsEnumerable()
                //                    //.GroupBy(r => new { Location = r.Field<string>("Location") })
                //                    .Select(r => r["Location"]).Distinct().OrderBy(r => r);

                //var items = this.dtPI_List.Select("Location<> 'sdklfjoisdahuiosdhfsduifhsduiof'").Select(r => r["Location"]).OrderBy(x => x).Distinct();


                //var query = from loc in this.dtPI_List.AsEnumerable()
                //            group loc by new
                //            {
                //                LOCATION = loc.Field<String>("Location")
                //            } into g
                //            select new
                //            {
                //                LOCATION = g.Key.LOCATION
                //            };

                //var items = query.ToArray(); //.OrderBy(g => g);


                //DataTable dtLoc = this.dtPI_List.AsEnumerable()
                //                    .GroupBy(r => new
                //                    {
                //                        Location = r.Field<string>("Location")
                //                    })
                //                    .OrderBy(g => g.Key.Location)
                //                    .SelectMany(g => g.OrderBy(r => r.Field<string>("Location")))
                //                    .CopyToDataTable();


                //var query = from r in this.dtPI_List.AsEnumerable()
                //            orderby r.Field<string>("Location")
                //            group r by r.Field<string>("Location") into gr
                //            select new { gr.Key };
                //var items = query.ToArray();


                //var A = this.dtPI_List.AsEnumerable();
                //var B = A.Select(r => r["Location"]);
                //var C = B.Distinct();
                //var D = C.OrderBy(x => x.ToString());
                #endregion

                var items = dtPI_List.AsEnumerable().Select(r => r["Location"]).OrderBy(x => x.ToString()).Distinct();

                cboLoc.Items.Clear();
                cboLoc.Items.Add("[All]");

                foreach (object item in items)
                {
                    cboLoc.Items.Add(item);
                }

                cboLoc.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            ComboBox cbo = (ComboBox)sender;

            Cursor.Current = Cursors.WaitCursor;

            string strLoc = cbo.SelectedItem.ToString() == "[All]" ? "%%" : cbo.SelectedItem.ToString();

            dtPI_List.DefaultView.RowFilter = $"Location Like '{strLoc}'";

            lblBoxCnt.Text = dtPI_List.DefaultView.Count.ToString("#,###");
            try
            {
                lblSum.Text = Convert.ToUInt64(dtPI_List.Compute("SUM(Qty)", dtPI_List.DefaultView.RowFilter)).ToString("#,###");
            }
            catch (InvalidCastException)
            {
                lblSum.Text = "0";
            }
        }


        private void SetButtonsAndGrid()
        {
            switch (strPI_Status)
            {
                case "OnGoing":
                    SetButtonColor(btnEndingBarcodeScan, true);
                    SetButtonColor(btnFinishPI, false);
                    SetButtonColor(btnDelete, true);
                    SetButtonColor(btnSave, false);
                    checkBox_ApplyFromPI.Enabled = false;
                    checkBox_DeleteStock.Enabled = false;
                    break;
                case "ScanEnded":
                    SetButtonColor(btnEndingBarcodeScan, false);
                    SetButtonColor(btnFinishPI, true);
                    SetButtonColor(btnDelete, true);
                    SetButtonColor(btnSave, true);
                    checkBox_ApplyFromPI.Enabled = true;
                    checkBox_DeleteStock.Enabled = true;
                    break;
                case "Completed":
                    SetButtonColor(btnEndingBarcodeScan, false);
                    SetButtonColor(btnFinishPI, false);
                    SetButtonColor(btnDelete, false);
                    SetButtonColor(btnSave, false);
                    checkBox_ApplyFromPI.Enabled = false;
                    checkBox_DeleteStock.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void SetButtonColor(Button btn, bool enableFlag)
        {
            btn.Enabled = enableFlag;
            if (enableFlag)
            {
                btn.BackColor = Color.Yellow;
                btn.ForeColor = Color.Black;
            }
            else
            {
                btn.BackColor = Color.Gainsboro;
                btn.ForeColor = Color.Gray;
            }
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO {PI_frmMain00.strDbName}SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'PhysicalInventory', REPLACE(LEFT(ISNULL(N'{strMsg}',''),3000), '''', ''''''), '{this.strUser}', GETDATE())");
            }
            catch { }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        private void btnEndingBarcodeScan_Click(object sender, EventArgs e)
        {
            string strMsg = "바코드 스캔을 종료하면, 더이상 재고조사를 위한 바코드 스캔을 할 수 없습니다.\n\n"
                            + "정말로 바코드 스캔을 종료하겠습니까 ?\n\n\n"
                            + "If you perform 'ScanEnded', you can no longer scan the Barcodes for Physical-Inventory.\n\n"
                            + "Do you really want to perform 'ScanEnded' ?\n\n";

            if (DialogResult.Yes != MessageBox.Show(strMsg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;
            
            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_SET_PIMASTER_WITH_SCANINFO";
                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO     = '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
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

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string strTmp = "바코드 스캔이 성공적으로 종료되었습니다.\n\n더이상 바코드를 스캔할 수 없습니다.\n\n\n"
                                + "Barcode Scan ended successfully.\n\n"
                                + "You cannot scan the barcode from now on.\n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.None);

                this.GetDataFromPI_ResvMaster();
                this.GetDataFromPI_Master();
                this.SetButtonsAndGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void dgv02_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView) sender;

            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string strColName = dgv.Columns[e.ColumnIndex].Name;
            if (strColName != "ApplyFromPI" && strColName != "DeleteStock") e.Cancel = true;

            if (dgv.Rows[e.RowIndex].Cells["Rm_Material"].Value.ToString() == "" && strColName == "DeleteStock")
            {
                e.Cancel = true;
                return;
            }

            if (dgv.Rows[e.RowIndex].Cells["Material"].Value.ToString() == "" && strColName == "ApplyFromPI")
            {
                e.Cancel = true;
                return;
            }

            if (dgv.Rows[e.RowIndex].Cells["Rm_Material"].Value.ToString() != "" && dgv.Rows[e.RowIndex].Cells["Material"].Value.ToString() != "" &&
                (strColName == "ApplyFromPI" || strColName == "DeleteStock"))
            {
                e.Cancel = true;
                return;
            }
        }

        private void btnFinishPI_Click(object sender, EventArgs e)
        {
            string strMsg = "재고조사 정보의 수정을 종료하고, 재고정보를 MES 및 ERP 시스템에 반영합니다.\n\n"
                            + "이 작업은 재고조사 정보의 수량에 따라 꽤 많은 시간이 소요될 수 있습니다.\n\n"
                            + "실행 도중에 프로그램을 종료하지 마십시오.\n\n"
                            + "계속하시겠습니까?\n\n\n"
                            + "End modification of Physical-Inventory, and apply to MES & ERP systems.\n\n"
                            + "This can take a lot of time depending on the quantity of PI information.\n\n"
                            + "Do not exit this program while running.\n\n"
                            + "Do you wish to continue?\n\n";

            if (DialogResult.Yes != MessageBox.Show(strMsg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                // 변경된 행이 있으면 먼저 저장(처리) 하고..
                if (!this.SaveChanges(false))
                {
                    return;
                }

                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_SET_PIMASTER_FINALIZE";
                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO     = '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd, CommandType.Text, null, 10800);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
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

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Physical Inventory [ {this.PS_RESVNO} ] completed successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                this.PI_frmMain30_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strMsg = "현재 재고조사가 진행중입니다.\n\n"
                            + "정말로 삭제하겠습니까?\n\n\n"
                            + "Currently Physical-Inventory is in progress.\n\n"
                            + "Do you wish to delete?\n\n";

            if (DialogResult.Yes != MessageBox.Show(strMsg, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_SET_RESVMASTER_DELETE";
                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO     = '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
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

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Physical Inventory [ {this.PS_RESVNO} ] deleted successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveChanges(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private bool SaveChanges(bool isDisplaySucceedMessage)
        {
            try
            {
                if (this.dtPI_List.GetChanges() == null)    // 변경된 행 없음.
                {
                    if (isDisplaySucceedMessage)
                    {
                        MessageBox.Show($"변경된 데이터가 존재하지 않습니다.\n\n\nChanged data does not exist.\n\n", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                    }
                    return true;
                }

                DataTable dt1 = this.dtPI_List.GetChanges();

                #region 41574건으로 테스트 결과, MemoryStream으로 사용시 29.85초에서 0.4초로 단축됨 (413645 Bytes).
                //string PS_PI_LIST = "";
                //foreach (DataRow row in this.dtPI_List.Rows)
                //{
                //    PS_PI_LIST += row["RecordID"].ToString() + "\x07";
                //    PS_PI_LIST += (bool) row["ApplyFromPI"] ? "1\x07" : "0\x07";
                //    PS_PI_LIST += (bool) row["DeleteStock"] ? "1\x07" : "0\x07";
                //}
                #endregion

                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms);
                sw.AutoFlush = true;

                foreach (DataRow row in dt1.Rows)
                {
                    sw.Write(row["RecordID"].ToString() + "\x07");
                    sw.Write((bool)row["ApplyFromPI"] ? "1\x07" : "0\x07");
                    sw.Write((bool)row["DeleteStock"] ? "1\x07" : "0\x07");
                }
                string PS_PI_LIST = Encoding.UTF8.GetString(ms.ToArray());


                if (PS_PI_LIST.Length > 0) PS_PI_LIST = PS_PI_LIST.Remove(PS_PI_LIST.Length - 1, 1);

                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_SET_PIMASTER_WITH_MODIFIED_INFO";
                string PS_RESVNO = this.PS_RESVNO;
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO     = '{PS_RESVNO}'
                                ,@PS_PI_LIST    = '{PS_PI_LIST}'
                                ,@PS_ClientId   = '{PS_ClientId}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd, CommandType.Text, null, 10800);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (intRC == -999)
                    {
                        MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                    else
                    {
                        throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    }
                }

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                this.dtPI_List.AcceptChanges();

                if (isDisplaySucceedMessage)
                {
                    MessageBox.Show($"Saved successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
                return false;
            }
        }

        private void btn_Excel_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel File (*.xlsx)|*.xlsx";
            saveFileDialog.Title = "Save To Excel";
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_List";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
               // System.Windows.Forms.MessageBox.Show("Save To Excel Process");
                EX.Application excelApp = new EX.Application();
                excelApp.Visible = true;
                EX.Workbook workbook = excelApp.Application.Workbooks.Add();
                EX.Worksheet worksheet = (EX.Worksheet)workbook.Sheets[1];
                for (int j = 1; j < dgv02.Columns.Count; j++)
                {
                    worksheet.Cells[1, j] = dgv02.Columns[j].HeaderText;
                }
                int rowCount = dgv02.Rows.Count;
                int colCount = dgv02.Columns.Count - 1;
                object[,] data = new object[rowCount, colCount];
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 1; j < colCount + 1; j++)
                    {
                        data[i, j - 1] = dgv02.Rows[i].Cells[j].Value;
                    }
                }
                try
                {
                    EX.Range startCell = (EX.Range)worksheet.Cells[2, 1];
                    EX.Range endCell = (EX.Range)worksheet.Cells[rowCount + 1, colCount];
                    EX.Range writeRange = worksheet.Range[startCell, endCell];
                    writeRange.Value = data;
                    workbook.SaveAs(saveFileDialog.FileName);
                    workbook.Close();
                    excelApp.Quit();
                }
                catch (Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show("Save Fail!" + Environment.NewLine + ex.StackTrace);
                }finally
                {
                       ReleaseObject(worksheet);
                ReleaseObject(workbook);
                ReleaseObject(excelApp);
                }
             
                System.Windows.Forms.MessageBox.Show("Save!");
            }
        }
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;

            }
            finally
            {
                GC.Collect();
            }
        }

        private void checkBox_ApplyFromPI_CheckedChanged(object sender, EventArgs e)
        {
            if (strPI_Status != "ScanEnded")
            {
                System.Windows.Forms.MessageBox.Show("Status is Not  ScanEnded!");
                return;
            }

            for (int i = 0; i < dgv02.Rows.Count; i++)
            {

                if (dgv02.Rows[i].Cells["Material"].Value.ToString() == "")
                {
                }
                else if (dgv02.Rows[i].Cells["Material"].Value.ToString() != "" && dgv02.Rows[i].Cells["Rm_Material"].Value.ToString() != "")
                {
                }
                else
                {
                    dgv02.Rows[i].Cells["ApplyFromPI"].Value = this.checkBox_ApplyFromPI.Checked;
                }
            }
        }

        private void checkBox_DeleteStock_CheckedChanged(object sender, EventArgs e)
        {
            if (strPI_Status != "ScanEnded")
            {
                System.Windows.Forms.MessageBox.Show("Status is Not  ScanEnded!");
                return;
            }

            for (int i = 0; i < dgv02.Rows.Count; i++)
            {

                if (dgv02.Rows[i].Cells["Rm_Material"].Value.ToString() == "")
                {
                }
                else if (dgv02.Rows[i].Cells["Material"].Value.ToString() != "" && dgv02.Rows[i].Cells["Rm_Material"].Value.ToString() != "")
                {
                }
                else
                {
                    dgv02.Rows[i].Cells["DeleteStock"].Value = this.checkBox_DeleteStock.Checked;
                }

            }
        }
    }
}