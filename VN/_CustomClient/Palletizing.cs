using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.AppService;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Client
{
    public partial class Palletizing : SkinForm
    {
        #region Field

        /// <summary>
        /// PCB 검증 결과
        /// </summary>
        private enum VerificationResult
        {
            /// <summary>
            /// OK?
            /// </summary>
            OK

          , ERR00
          , ERR01
          , ERR02
          , ERR03
          , ERR10
          , ERR11
          , ERR12
          , ERR13
          , ERR14
          , ERR20
          , ERR21
          , ERR22
          , Save_OK
          , Save_NG
           ,
        };
        Boolean isProcess; 
        private string OrderQty { get; set; }
        private int CurrentPcbQty { get; set; }
        private int CurrentBoxQty { get; set; }
        private int StandardBoxQty { get; set; }

        private DataTable PalletDataTable
        {
            get
            {
                var queryGetPalletListWithWorkOrder = $@"
                                                        SELECT ROW_NUMBER() OVER (ORDER BY MAX(PH.Updated)) AS Count
                                                             , PH.PalletBcd
                                                             , CONVERT(NCHAR(19), MAX(PH.Updated), 120)     AS Updated
                                                          FROM PackingHist AS PH WITH (NOLOCK)
                                                         WHERE IoType = 'OUT'
                                                           AND WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                                                         GROUP BY PH.PalletBcd
                                                        ;
                                                        ";
                return DbAccess.Default.GetDataTable(queryGetPalletListWithWorkOrder);
            }
        }

        private string _childMaterial;

        #endregion

        #region Constructor

        public Palletizing()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        public void Init()
        {
            try
            {
             
                lbl_workorder.Text = string.Empty;
                lbl_item.Text = string.Empty;
                lbl_Qty.Text = @"0 / 0";
                lbl_Error.Visible = false;
                lbl_workorder.Text = WbtCustomService.ActiveValues.WorkOrder;

                lbl_item.Text = $@"{WbtCustomService.ActiveValues.Material} / {WbtCustomService.ActiveValues.MaterialSpec}";
                OrderQty = WbtCustomService.ActiveValues.OrderQty.ToString("####");
                if (DbAccess.Default.ExecuteScalar($@"SELECT BoxQtyInPallet FROM Material WHERE Material = '{WbtCustomService.ActiveValues.Material}'") is int standardBoxQty)
                {
                    StandardBoxQty = standardBoxQty;
                }
                else
                {
                    MessageBox.ShowCaption("Chưa cài đặt số lượng PCB/Box hoặc số lượng Pallet。\r\n" + "quantity of boxes per pallet is not set. \r\n", "Error", MessageBoxIcon.Error);
                    Close();
                }

                CurrentPcbQty = GetCurrentOutQty();
                lbl_Qty.Text = $@"{CurrentPcbQty} / {OrderQty}";
                lbl_PalletQty.Text = $@"{CurrentBoxQty} / {StandardBoxQty}";
                string queryGetChildMaterial = $@"
                                            SELECT CHILD_ITEM_CD
                                              FROM BOM B
                                                   INNER JOIN Material M
                                                              ON B.CHILD_ITEM_CD = M.Material AND M.Bunch = '25'
                                             WHERE ITEM_CD = '{WbtCustomService.ActiveValues.Material}'
                                            ";
                var childDt = DbAccess.Default.GetDataTable(queryGetChildMaterial);
                if (childDt.Rows.Count <= 0)
                {
                    System.Windows.Forms.MessageBox.Show("Not found child material.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                }
                else
                {
                    _childMaterial = childDt.Rows[0]["CHILD_ITEM_CD"].ToString();
                }

                dgv_List.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
                dgv_palletInfo.DataSource = PalletDataTable;

                comboBox_Line.Items.Add(DbAccess.Default.ExecuteScalar($@"SELECT [dbo].[GetLineWithWorkOrder]('{WbtCustomService.ActiveValues.WorkOrder}')"));
                comboBox_Line.SelectedIndex = 0;
                dateTimePicker_PackingDate.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.Init", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void SetLayoutViewWithVerificationResult(string barcode, VerificationResult verificationResult)
        {
            lbl_Error.Visible = false;
            switch (verificationResult)
            {
                case VerificationResult.OK:
                    lbl_Current.ForeColor = Color.DodgerBlue;
                    SetScanBarcodeView(barcode, "OK");
                    var boxQty = DbAccess.Default.IsExist("Packing", $"BoxBcd = '{barcode}'");
                    dgv_List.Rows.Add(dgv_List.Rows.Count + 1, barcode, boxQty, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    CurrentPcbQty += boxQty;
                    lbl_Qty.Text = string.Format($@"{CurrentPcbQty} / {OrderQty}");
                    if (dgv_List.Rows.Count > 0)
                    {
                        var index = dgv_List.Rows.Count - 1;
                        dgv_List.FirstDisplayedScrollingRowIndex = index;
                        dgv_List.Rows[index].Selected = true;
                    }
                    break;
                case VerificationResult.ERR02:
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR02 - Lỗi độ dài Barcode。Barcode Length Error.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.ERR03:
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR03 - Barcode này không phải Barcode Box。This barcode is not a Box barcode.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.ERR10:
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR10 - Quá trình hiện tại đã hoàn tất Current process already passed.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.ERR13:
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR13 - Không xác định được Barcode Unknown Barcode.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.ERR14:
                    lbl_Error.Visible = true;
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Text = "ERR14 - Box này đã được tạo Pallet。 This box has been palletized.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.ERR22:
                    lbl_Current.ForeColor = Color.Red;
                    SetScanBarcodeView(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR22 - Sai thông tin danh mục Wrong Item Information.";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                case VerificationResult.Save_OK:
                    CurrentPcbQty = GetCurrentOutQty();
                    lbl_Qty.Text = string.Format($@"{CurrentPcbQty} / {OrderQty}");
                    if (dgv_palletInfo.Rows.Count > 0)
                    {
                        var index = dgv_palletInfo.Rows.Count - 1;
                        dgv_palletInfo.FirstDisplayedScrollingRowIndex = index;
                        dgv_palletInfo.Rows[index].Selected = true;
                    }
                    break;
                case VerificationResult.Save_NG:
                    lbl_Current.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR06 - Lỗi cơ sở dữ liệu DB error";
                    textBox_Scan.Clear();
                    textBox_Scan.Focus();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(verificationResult), verificationResult, null);
            }
        }

        public void SetScanBarcodeView(string barcode, string result)
        {
            if (string.IsNullOrEmpty(lbl_Current.Text))
            {
                lbl_Current.Text = barcode;
            }
            else
            {
                if (lbl_Current.ForeColor == Color.Red)
                {
                    lbl_Current.Text = barcode;
                }
                else
                {
                    lbl_prev.Text = lbl_Current.Text;
                    lbl_Current.Text = barcode;
                }
            }

            lbl_Current.ForeColor = result == "OK" ? Color.DodgerBlue : Color.Red;
        }

        private bool VerifyBarcode(string barcode)
        {
            //barcode length check
            if (!(barcode.Length == 19 || barcode.Length == 21))
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR02);
                return false;
            }

            if (comboBox_Line.SelectedIndex < 0)
            {
                System.Windows.Forms.MessageBox.Show("Please select a line.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // barcode check
            if (barcode.Substring(0, 1) != "B")
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR03);
                return false;
            }

            if (dgv_List.Rows.Cast<DataGridViewRow>().Any(dataGridViewRow => barcode.Equals(dataGridViewRow.Cells["BoxBcd"].Value.ToString())))
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR10);
                return false;
            }

            //StockHist check
            if (DbAccess.Default.IsExist("StockHist", $"BoxBcd = '{barcode}'") > 0)
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR10);
                return false;
            }

            //Packing check
            if (DbAccess.Default.IsExist("Packing", $"BoxBcd = '{barcode}'") == 0)
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR13);
                return false;
            }

            //Material check
            if (!_childMaterial.Equals($"{DbAccess.Default.ExecuteScalar($"SELECT Material FROM Packing WHERE BoxBcd = '{barcode}'")}"))
            {
                SetLayoutViewWithVerificationResult(barcode, VerificationResult.ERR22);
                return false;
            }

            return true;
        }

        private bool PrintPalletLabel(string partNo, string model, int qty, string palletBcd)
        {
            try
            {
                var clsBarcode = new clsBarcode.clsBarcode();
                clsBarcode.LoadFromXml(DbAccess.Default.ExecuteScalar("SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'").ToString());
                clsBarcode.Data.SetText("PARTNO", partNo);
                clsBarcode.Data.SetText("MODEL", model);
                clsBarcode.Data.SetText("QTY", $"{qty} EA");
                clsBarcode.Data.SetText("PALLETBCD", palletBcd);
                clsBarcode.Print(false);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.PrintPalletLabel", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool InsertResult(string palletBcd, string boxList)
        {
            try
            {
                string query = $@"
                               EXEC [Sp_WorkPcStock]
                                  @PS_GUBUN = 'SAVE_STOCK_IN'
                                , @PS_PALLET = '{palletBcd}'
                                , @PS_BOXLIST = '{boxList}'
                                , @PS_MATERIAL = '{WbtCustomService.ActiveValues.Material}'
                                , @PS_WORKORDER = '{WbtCustomService.ActiveValues.WorkOrder}'
                                , @PS_WORKCENTER = '{WbtCustomService.ActiveValues.Workcenter}'
                                , @PS_UPDATEUSER = '{WiseApp.Id}'
                                ";
                DbAccess.Default.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.InsertResult", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Good(int count)
        {
            try
            {
                var entryRequest = new EntryRequest();
                entryRequest.Mode = WeMaintMode.Insert;
                entryRequest.ActiveJob = null;
                entryRequest.Count = 0;
                entryRequest.HistNo = null;
                entryRequest.Workcenter = null;
                entryRequest.UseMultiOrder = false;
                entryRequest.UserColumns = null;
                entryRequest.From = null;
                entryRequest.Cavity = 0;
                entryRequest.IsCaivity = false;
                entryRequest.SourceHist = null;
                entryRequest.NotifyLimit = 0;
                entryRequest.Pausing = false;
                entryRequest.DasDependence = false;
                entryRequest.UserColumns = new SortedList<string, object>();
                entryRequest.From = WiseApp.Id;
                entryRequest.Mode = WeMaintMode.Insert;
                entryRequest.ActiveJob = WbtCustomService.ActiveValues.ActiveJob;
                entryRequest.Workcenter = WbtCustomService.ActiveValues.Workcenter;
                entryRequest.Count = count;
                entryRequest.Cavity = 1;
                JobControl.Good.Insert(entryRequest);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.Good", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void InsertIntoSysLog(string source, string message)
        {
            DbAccess.Default.ExecuteQuery
                (
                 $@"
                INSERT
                  INTO SysLog ( Type
                              , Category
                              , Source
                              , Message
                              , [user]
                              , Updated )
                VALUES ( 'E'
                       , 'Client'
                       , '{source}'
                       , LEFT(ISNULL(N'{message}', ''), 3000)
                       , '{WbtCustomService.ActiveValues.Workcenter}'
                       , GETDATE() )
                "
                );
        }

        private int GetCurrentOutQty()
        {
            try
            {
                var query = $@"
                        SELECT COALESCE(SUM(OutQty), 0) AS OutQty
                          FROM OutputHist WITH (NOLOCK)
                         WHERE WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                        ";
                return Convert.ToInt32(DbAccess.Default.ExecuteScalar(query));
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.GetCurrentOutQty", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void InStockProcess()
        {
            try
            {

                var dataTable = DbAccess.Default.GetDataTable($@"SELECT LG_ITEM_CD, LG_ITEM_NM FROM Material WHERE Material = '{WbtCustomService.ActiveValues.Material}'");
                var itemCodeLG = dataTable.Rows[0]["LG_ITEM_CD"].ToString();
                var itemNameLG = dataTable.Rows[0]["LG_ITEM_NM"].ToString();
                var qty = dgv_List.Rows.Cast<DataGridViewRow>().Sum(dataGridViewRow => Convert.ToInt32(dataGridViewRow.Cells["Qty"].Value));
                var boxList = ( from DataGridViewRow dataGridViewRow in dgv_List.Rows select dataGridViewRow.Cells["BoxBcd"].Value.ToString()).ToList();
                var packingDate = $"{dateTimePicker_PackingDate.Value:yyyy-MM-dd}";
                var packingDateCode = DbAccess.Default.ExecuteScalar($"EXEC SP_Convert_Date '{packingDate}'") as string;
                var line = comboBox_Line.SelectedItem as string;
                var baseCode = $"P{qty:000#}{packingDateCode}{line}{itemCodeLG.Substring(3, 8)}";
                var seq = Convert.ToInt32(DbAccess.Default.ExecuteScalar($"SELECT dbo.GetNextLabelSeqWithType('PALLET', '{baseCode}')"));
                var palletBcd = $"{baseCode}{seq:0#}";
                //
                if (!InsertResult(palletBcd, string.Join(",", boxList.ToArray()))) return;
                //해당동작 실패 시 앞에 작업을 막을 방법에 대해서 고민...
                if (!Good(qty)) return;
                //해당동작 실패 시 앞에 작업을 막을 방법에 대해서 고민...
                if (!InsertIntoPalletPrintHist(itemCodeLG, itemNameLG, qty, palletBcd, packingDateCode, line, packingDate, seq))
                {
                    return;
                }

                PrintPalletLabel(itemCodeLG, itemNameLG, qty, palletBcd);
                lbl_PalletBarcode.Text = palletBcd;
                dgv_palletInfo.DataSource = PalletDataTable;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.InStockProcess", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetLayoutViewWithVerificationResult("", VerificationResult.Save_NG);
            }
        }

        private void ClearBoxScanList()
        {
            CurrentPcbQty = GetCurrentOutQty();
            lbl_Qty.Text = $@"{CurrentPcbQty} / {OrderQty}";
            CurrentBoxQty = 0;
            lbl_PalletQty.Text = $@"{CurrentBoxQty} / {StandardBoxQty}";

            dgv_List.Rows.Clear();
        }

        private bool InsertIntoPalletPrintHist(string partNo, string model, int qty, string palletBcd, string dateCode, string line, string date, int seq)
        {
            try
            {
                string query =
                    $@"
                    INSERT
                      INTO PalletbcdPrintHist
                      ( LG_PartNo
                      , Model
                      , Qty
                      , PalletBcd
                      , Mfg_ymd
                      , Mfg_Line
                      , ProductionDate
                      , ProductionLine
                      , Material
                      , SerialNo
                      , Reprint
                      , Updated
                      , Updater )
                    VALUES ( '{partNo}'
                           , '{model}'
                           , '{qty}'
                           , '{palletBcd}'
                           , '{dateCode}'
                           , '{line}'
                           , '{date}'
                           , '{WbtCustomService.ActiveValues.Workcenter}'
                           , '{WbtCustomService.ActiveValues.Material}'
                           , '{seq:00}'
                           , 0
                           , GETDATE()
                           , '{WbtCustomService.ActiveValues.Workcenter}' )
                    ";
                DbAccess.Default.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.InsertIntoPalletPrintHist", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool InsertIntoPalletRePrintHist(string palletBcd)
        {
            try
            {
                string query =
                    $@"
                    INSERT
                      INTO PalletbcdPrintHist
                      ( LG_PartNo
                      , Model
                      , Qty
                      , PalletBcd
                      , Mfg_ymd
                      , Mfg_Line
                      , ProductionDate
                      , ProductionLine
                      , Material
                      , SerialNo
                      , Reprint
                      , Updated
                      , Updater )
                    SELECT TOP 1 LG_PartNo
                               , Model
                               , Qty
                               , PalletBcd
                               , Mfg_ymd
                               , Mfg_Line
                               , ProductionDate
                               , ProductionLine
                               , Material
                               , SerialNo
                               , Reprint + 1
                               , GETDATE()
                               , '{WbtCustomService.ActiveValues.Workcenter}'
                      FROM PalletbcdPrintHist WITH (NOLOCK)
                     WHERE PalletBcd = '{palletBcd}'
                     ORDER BY SerialNo DESC
                    ";
                DbAccess.Default.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.InsertIntoPalletRePrintHist", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Event

        private void Palletizing_Load(object sender, EventArgs e)
        {
            Init();
            textBox_Scan.Focus();
        }

        private void textBox_Scan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (isProcess) return;
            try
            {
                isProcess = true;
                var scanData = textBox_Scan.Text.ToUpper();

                //gmryu 2023-11-02 작업지시 수량 초과 방지
                int ScanQty = 0;
                ScanQty = Convert.ToInt32(DbAccess.Default.ExecuteScalar(
                    $@"  select count(PcbBcd) from KeyRelation where BoxBcd = '{scanData}' "));
                int ComPareOrderQty = int.Parse(OrderQty);
                if ((ScanQty + CurrentPcbQty) > ComPareOrderQty)
                {
                    System.Windows.Forms.MessageBox.Show("Scan Qty :" + (ScanQty + CurrentPcbQty) + " > Order Qty : " + ComPareOrderQty);
                    return;
                }


                if (!VerifyBarcode(scanData))
                {
                    //NG
                    return;
                }

                SetLayoutViewWithVerificationResult(scanData, VerificationResult.OK);
                textBox_Scan.Clear();
                textBox_Scan.Focus();
                if (CurrentBoxQty + 1 >= StandardBoxQty)
                {
                    InStockProcess();
                    ClearBoxScanList();
                    SetLayoutViewWithVerificationResult(scanData, VerificationResult.Save_OK);
                    return;
                }

                CurrentBoxQty += 1;
                lbl_PalletQty.Text = $@"{CurrentBoxQty} / {StandardBoxQty}";
                textBox_Scan.Clear();
                textBox_Scan.Focus();
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.textBox_Scan_KeyUp", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isProcess = false;
            }
        }

        private void btn_testPrint_Pallet_Click(object sender, EventArgs e)
        {
            PrintPalletLabel("EAY99999999", "MODEL_TEST", 10, "P0010N8SG9999999901");
        }

        private void btn_Palletizing_Click(object sender, EventArgs e)
        {                 
            if (CurrentBoxQty <= 0) { System.Windows.Forms.MessageBox.Show("Scan Box Barcode = 0"); return; };         
            if (System.Windows.Forms.MessageBox.Show
                  (
                   "Bạn có chắc chắn không？ \r\nAre you sure?", "Đóng những hộp còn lại(Pallet Remainder Closing)",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question
                  )
              != DialogResult.Yes) return;
            if (isProcess) return;
            try
            {
                isProcess = true;
                InStockProcess();
                ClearBoxScanList();
                textBox_Scan.Clear();
                textBox_Scan.Focus();
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing.btn_Palletizing_Click", ex.Message);
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isProcess = false;
            }
           
        }

        private void button_PalletReprint_Click(object sender, EventArgs e)
        {
            var palletBarcode = lbl_PalletBarcode.Text;
            var dataRow = DbAccess.Default.GetDataRow
                (
                 $@"
                SELECT TOP 1 LG_PartNo
                           , Model
                           , Qty
                           , PalletBcd
                  FROM PalletbcdPrintHist
                 WHERE PalletBcd = '{palletBarcode}'
                 ORDER BY Reprint DESC
                ;
                "
                );
            if (dataRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Not found print history. (pallet)");
                return;
            }

            PrintPalletLabel($"{dataRow["LG_PartNo"]}", $"{dataRow["Model"]}", Convert.ToInt32(dataRow["Qty"]), $"{dataRow["PalletBcd"]}");
            InsertIntoPalletRePrintHist(palletBarcode);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearBoxScanList();
        }

        #endregion

        private void dgv_palletInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                switch (dataGridViewColumn.Name)
                {
                    case "Count":
                        dataGridViewColumn.Width = 50;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "PalletBcd":
                        dataGridViewColumn.Width = 200;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Updated":
                        dataGridViewColumn.Width = 200;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }
                 
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dgv_palletInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            lbl_PalletBarcode.Text = dataGridView.Rows[e.RowIndex].Cells["PalletBcd"].Value as string;
        }
    }
}
