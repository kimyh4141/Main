using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.WMS
{
    public partial class NewBarcode : Form
    {
        #region Field

        private readonly DataTable _dataTable = new DataTable
                                                {
                                                    Columns = {"Barcode", "QtyinBox", "BoxSeq",}
                                                };

        private CustomEnum.RawMaterialPrintType _currentPrintType;
        private DataRow _currentDataRow;
        private bool _isWork;
        
        #endregion

        #region Constructor

        public NewBarcode()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void SetDetail(DataRow dataRow)
        {
            if (dataRow == null) return;
            textBox_Material.Text = dataRow["Material"].ToString();
            textBox_MaterialName.Text = dataRow["MaterialName"].ToString();
            textBox_Spec.Text = dataRow["Spec"].ToString();
            textBox_Supplier.Text = dataRow["Supplier"].ToString();
            textBox_SupplierName.Text = dataRow["SupplierName"].ToString();
            if (dataRow["Date"] is DateTime date)
            {
                dateTimePicker_Date.Value = date;
            }

            textBox_Type.Text = dataRow["Type"].ToString();
            numericUpDown_BoxCount.Value = 1;
            numericUpDown_QtyperBox.Text = dataRow["qty"].ToString();
        }

        private void ClearInformation()
        {
            textBox_Change.Text = "";
            textBox_RePrint.Text = "";
            textBox_Remain.Text = "";
            textBox_Split.Text = "";
            textBox_Material.Text = "";
            textBox_MaterialName.Text = "";
            textBox_Spec.Text = "";
            textBox_Type.Text = "";
            textBox_Supplier.Text = "";
            textBox_SupplierName.Text = "";
            numericUpDown_QtyperBox.Value = 1;
            numericUpDown_BoxCount.Value = 1;
            textBox_Qty.Text = "";
            dataGridView_BoxList.Rows.Clear();
            _dataTable.Rows.Clear();
            label_totalQty.Text = "0";
        }

        private void ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType printType)
        {
            if (_isWork) return;
            _isWork = true;
            _currentPrintType = printType;

            switch (printType)
            {
                case CustomEnum.RawMaterialPrintType.Base:
                    break;
                case CustomEnum.RawMaterialPrintType.Change:
                    checkBox_Change.Checked = true;
                    textBox_Change.Enabled = true;

                    checkBox_RePrint.Checked = false;
                    textBox_RePrint.Enabled = false;

                    checkBox_Remain.Checked = false;
                    textBox_Remain.Enabled = false;

                    checkBox_Split.Checked = false;
                    textBox_Split.Enabled = false;

                    textBox_Material.ReadOnly = true;

                    textBox_MaterialName.ReadOnly = true;

                    textBox_Spec.ReadOnly = true;

                    textBox_Type.ReadOnly = true;

                    textBox_Supplier.ReadOnly = true;

                    textBox_SupplierName.ReadOnly = true;

                    button_SupplierSearch.Enabled = false;

                    dateTimePicker_Date.Enabled = false;

                    numericUpDown_QtyperBox.Enabled = false;

                    numericUpDown_BoxCount.Enabled = false;

                    textBox_SplitScanBarcode.Enabled = false;
                    button_ScanAdd.Enabled = false;
                    button_ScanClear.Enabled = false;

                    button_Add.Enabled = false;
                    button_Clear.Enabled = false;
                    button_Print.Enabled = false;

                    tableLayoutPanel_ListView.Hide();
                    break;
                case CustomEnum.RawMaterialPrintType.Reprint:
                    checkBox_Change.Checked = false;
                    textBox_Change.Enabled = false;

                    checkBox_RePrint.Checked = true;
                    textBox_RePrint.Enabled = true;

                    checkBox_Remain.Checked = false;
                    textBox_Remain.Enabled = false;

                    checkBox_Split.Checked = false;
                    textBox_Split.Enabled = false;

                    textBox_Material.ReadOnly = true;

                    textBox_MaterialName.ReadOnly = true;

                    textBox_Spec.ReadOnly = true;

                    textBox_Type.ReadOnly = true;

                    textBox_Supplier.ReadOnly = true;

                    textBox_SupplierName.ReadOnly = true;

                    button_SupplierSearch.Enabled = false;

                    dateTimePicker_Date.Enabled = false;

                    numericUpDown_QtyperBox.Enabled = false;

                    numericUpDown_BoxCount.Enabled = false;

                    textBox_SplitScanBarcode.Enabled = false;
                    button_ScanAdd.Enabled = false;
                    button_ScanClear.Enabled = false;

                    button_Add.Enabled = false;
                    button_Clear.Enabled = false;
                    button_Print.Enabled = false;

                    tableLayoutPanel_ListView.Hide();
                    break;
                case CustomEnum.RawMaterialPrintType.Remain:
                    checkBox_Change.Checked = false;
                    textBox_Change.Enabled = false;

                    checkBox_RePrint.Checked = false;
                    textBox_RePrint.Enabled = false;

                    checkBox_Remain.Checked = true;
                    textBox_Remain.Enabled = false;

                    checkBox_Split.Checked = false;
                    textBox_Split.Enabled = false;

                    textBox_Material.ReadOnly = false;

                    textBox_MaterialName.ReadOnly = true;

                    textBox_Spec.ReadOnly = true;

                    textBox_Type.ReadOnly = true;

                    textBox_Supplier.ReadOnly = true;

                    textBox_SupplierName.ReadOnly = true;

                    button_SupplierSearch.Enabled = true;

                    dateTimePicker_Date.Enabled = true;

                    numericUpDown_QtyperBox.Enabled = true;

                    numericUpDown_BoxCount.Enabled = true;

                    textBox_SplitScanBarcode.Enabled = false;
                    button_ScanAdd.Enabled = false;
                    button_ScanClear.Enabled = false;

                    button_Add.Enabled = true;
                    button_Clear.Enabled = true;
                    button_Print.Enabled = true;

                    tableLayoutPanel_ListView.Show();
                    break;
                case CustomEnum.RawMaterialPrintType.Split:
                    checkBox_Change.Checked = false;
                    textBox_Change.Enabled = false;

                    checkBox_RePrint.Checked = false;
                    textBox_RePrint.Enabled = false;

                    checkBox_Remain.Checked = false;
                    textBox_Remain.Enabled = false;

                    checkBox_Split.Checked = true;
                    textBox_Split.Enabled = true;

                    textBox_Material.ReadOnly = true;

                    textBox_MaterialName.ReadOnly = true;

                    textBox_Spec.ReadOnly = true;

                    textBox_Type.ReadOnly = true;

                    textBox_Supplier.ReadOnly = true;

                    button_SupplierSearch.Enabled = false;

                    textBox_SupplierName.ReadOnly = true;

                    dateTimePicker_Date.Enabled = false;

                    numericUpDown_QtyperBox.Enabled = true;

                    numericUpDown_BoxCount.Enabled = true;

                    textBox_SplitScanBarcode.Enabled = false;
                    button_ScanAdd.Enabled = false;
                    button_ScanClear.Enabled = false;

                    button_Add.Enabled = false;
                    button_Clear.Enabled = false;
                    button_Print.Enabled = false;

                    tableLayoutPanel_ListView.Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(printType), printType, null);
            }

            _isWork = false;
            ClearInformation();
        }

        /// <summary>
        /// 1. Confirm
        /// 2. Verify
        /// 3. Write Database(Hist, Other)
        /// 4. Print Out
        /// </summary>
        /// <param name="rawMaterialPrintType"></param>
        private void PrintOut(CustomEnum.RawMaterialPrintType rawMaterialPrintType)
        {
            string barcodeBase;

            string barcode;
            string material;
            string materialName;
            string spec;
            string type;
            string supplier;
            string supplierName;
            string date;
            int boxQty;
            int qty;
            int seq;
            string moistureSensitivityLevel;

            DataRow dataRow;

            clsBarcode.clsBarcode cBarcode;

            var afterQuery = new StringBuilder();

            switch (rawMaterialPrintType)
            {
                case CustomEnum.RawMaterialPrintType.Base:
                    break;
                case CustomEnum.RawMaterialPrintType.Change:
                    barcodeBase = textBox_Change.Text;
                    material = textBox_Material.Text;
                    dataRow = _currentDataRow;
                    materialName = textBox_MaterialName.Text;
                    spec = textBox_Spec.Text;
                    supplier = textBox_Supplier.Text;
                    supplierName = textBox_SupplierName.Text;
                    date = dateTimePicker_Date.Value.ToString("yyyy-MM-dd");
                    moistureSensitivityLevel = dataRow["MSLevel"].ToString();
                    type = dataRow["Type"].ToString();
                    cBarcode = GetLabelClassByType(type);

                    qty = (int) numericUpDown_QtyperBox.Value;
                    seq = GetBoxSeq($"{material}{supplier}{dateTimePicker_Date.Value:yyyyMMdd}", qty);
                    barcode = $"{material}{supplier}{dateTimePicker_Date.Value:yyyyMMdd}{qty:000000#}{seq:00#}";

                    cBarcode.Data.SetText("BARCODE1", barcode);
                    cBarcode.Data.SetText("RAWMATERIAL", material);
                    cBarcode.Data.SetText("RAWMATERIALNAME", materialName);
                    cBarcode.Data.SetText("SPEC", spec);
                    cBarcode.Data.SetText("SUPPLIER", $"{supplier}");
                    cBarcode.Data.SetText("SUPPLIERNAME", $"{supplierName}");
                    cBarcode.Data.SetText("PRODUCTDATE", $"{date}");
                    cBarcode.Data.SetText("QTY", $"{qty}");
                    cBarcode.Data.SetText("SEQ", $"{seq:00#}");
                    cBarcode.Data.SetText("LEVEL", string.IsNullOrEmpty(moistureSensitivityLevel) ? $"" : $"MSL:{moistureSensitivityLevel}");

                    WritePrintHist(barcode, material, supplier, date, qty, seq.ToString("00#"));
                    cBarcode.Print(false);
                    cBarcode.Data.Clear();
                    afterQuery.AppendLine
                        (
                         $@"
                        DECLARE @NewBarcode  NVARCHAR(50) = '{barcode}'
                        DECLARE @OldBarcode  NVARCHAR(50) = '{barcodeBase}'
                        DECLARE @Material    NVARCHAR(50) = '{material}'
                        DECLARE @OldMaterial NVARCHAR(50) = '{dataRow["Material"]}'
                        DECLARE @BoxSeq      NVARCHAR(50) = '{seq:00#}'
                        DECLARE @Qty         INT = '{qty}'
                        DECLARE @IF_TIME     NVARCHAR(50) = GETDATE()
                        --NEW 재고 추가
                        INSERT
                          INTO Rm_Stock ( Rm_BarCode
                                        , Rm_Material
                                        , Rm_Supplier
                                        , Rm_ProdDate
                                        , Rm_QtyinBox
                                        , Rm_BoxSeq
                                        , Rm_Bunch
                                        , Rm_StockQty
                                        , Rm_Status
                                        , Rm_LocationGroup
                                        , Rm_Location
                                        , Storage   
                                        , Rm_Created
                                        , Rm_Updated
                                        )
                        SELECT TOP 1 @NewBarcode
                                   , @Material
                                   , Rm_Supplier
                                   , Rm_ProdDate
                                   , Rm_QtyinBox
                                   , @BoxSeq
                                   , 'CHANGE'
                                   , Rm_StockQty
                                   , 1
                                   , Rm_LocationGroup
                                   , Rm_Location
                                   , Storage   
                                   , @IF_TIME
                                   , @IF_TIME
                          FROM Rm_Stock
                         WHERE Rm_BarCode = @OldBarcode
                         ORDER BY Rm_Updated DESC
                        --NEW 재고이력 추가
                        INSERT
                          INTO Rm_StockHist ( Rm_BarCode
                                            , Rm_IO_Type
                                            , Rm_Material
                                            , Rm_Supplier
                                            , Rm_ProdDate
                                            , Rm_QtyinBox
                                            , Rm_BoxSeq
                                            , Rm_Bunch
                                            , Rm_StockQty
                                            , Rm_Status
                                            , Rm_LocationGroup
                                            , Rm_Location
                                            , ERP_SL_CD_FROM
                                            , ERP_SL_CD_TO
                                            , SendStatusErp
                                            , Rm_Created
                                            , Rm_Updated
                                            )
                        SELECT TOP 1 @NewBarcode
                                   , 'IN'
                                   , @Material
                                   , Rm_Supplier
                                   , Rm_ProdDate
                                   , Rm_QtyinBox
                                   , @BoxSeq
                                   , 'CHANGE'
                                   , Rm_StockQty
                                   , 1
                                   , Rm_LocationGroup
                                   , Rm_Location
                                   , ERP_SL_CD_FROM
                                   , ERP_SL_CD_TO
                                   , 1
                                   , @IF_TIME
                                   , @IF_TIME
                          FROM Rm_StockHist
                         WHERE Rm_BarCode = @OldBarcode
                         ORDER BY Rm_Updated DESC
                        --OLD 재고 삭제
                        DELETE
                          FROM Rm_Stock
                         WHERE Rm_BarCode = @OldBarcode
                        ;
                        --변경이력 추가
                        INSERT
                          INTO Rm_BCR ( Rm_Origin
                                      , Rm_NewBarcodeList
                                      , Issue
                                      , Created
                                      )
                        VALUES ( @OldBarcode
                               , @NewBarcode
                               , 'Change'
                               , @IF_TIME
                               )
                        ;
                        --ERP 전송
                        INSERT
                          INTO MES_IF_VN.dbo.MTE_INV_TRANS ( IF_TIME
                                                           , I_PROC_STEP
                                                           , I_APPLY_STATUS
                                                           , DOCUMENT_DT
                                                           , MOV_TYPE
                                                           , SL_CD
                                                           , IN_OUT_FLAG
                                                           , ITEM_CD_FR
                                                           , ITEM_CD_TO
                                                           , QTY
                                                           , APPLY_FLAG
                                                           )
                        VALUES ( @IF_TIME
                               , 'C'
                               , 'R'
                               , @IF_TIME
                               , 'T61'
                               , 'VP30'
                               , 'I'
                               , @OldMaterial
                               , @Material
                               , @Qty
                               , 'N'
                               )
                        ;
                        "
                        );
                    break;
                case CustomEnum.RawMaterialPrintType.Reprint:
                    barcodeBase = textBox_RePrint.Text;
                    if (DbAccess.Default.IsExist("RmPrintHist", $"Rm_Barcode = '{barcodeBase}'") < 1)
                    {
                        MessageBox.Show("Không có lịch sử in ra。(No print out history.)", "Không có lịch sử in ra。(No print out history.)", MessageBoxIcon.Warning);
                        return;
                    }

                    //원자재정보 가져온다
                    dataRow = DbAccess.Default.GetDataRow
                        (
                         $@"
                        SELECT TOP 1 RM.RawMaterial AS Material
                                   , RM.Text        AS MaterialName
                                   , RM.Spec
                                   , CASE
                                         WHEN COALESCE(RM.MSLevel, '') != ''
                                             THEN 'MSL'
                                         WHEN RM.Type = 'Reel'
                                             THEN 'Reel'
                                         ELSE 'Box'
                                     END            AS Type
                                   , RM.MSLevel
                                   , RPH.Rm_Suppler AS Supplier
                                   , S.Text         AS SupplierName
                                   , Rm_StockDt     AS Date
                          FROM RmPrintHist RPH
                               JOIN RawMaterial RM
                                    ON RPH.Rm_Material = RM.RawMaterial
                               JOIN Supply S
                                    ON RPH.Rm_Suppler = S.Supply
                         WHERE RPH.Rm_BarCode = '{barcodeBase}'
                         ORDER BY Rm_Updated DESC
                        ;
                        "
                        );
                    type = dataRow["Type"].ToString();
                    cBarcode = GetLabelClassByType(type);
                    barcode = barcodeBase;
                    material = dataRow["Material"].ToString();
                    materialName = dataRow["MaterialName"].ToString();
                    spec = dataRow["Spec"].ToString();
                    supplier = dataRow["Supplier"].ToString();
                    supplierName = dataRow["SupplierName"].ToString();
                    date = dataRow["Date"].ToString();
                    qty = int.Parse(barcode.Substring(barcode.Length - 10, 7));
                    seq = int.Parse(barcode.Substring(barcode.Length - 3, 3));
                    moistureSensitivityLevel = dataRow["MSLevel"].ToString();

                    cBarcode.Data.SetText("BARCODE1", barcode);
                    cBarcode.Data.SetText("RAWMATERIAL", material);
                    cBarcode.Data.SetText("RAWMATERIALNAME", materialName);
                    cBarcode.Data.SetText("SPEC", spec);
                    cBarcode.Data.SetText("SUPPLIER", $"{supplier}");
                    cBarcode.Data.SetText("SUPPLIERNAME", $"{supplierName}");
                    cBarcode.Data.SetText("PRODUCTDATE", $"{date}");
                    cBarcode.Data.SetText("QTY", $"{qty}");
                    cBarcode.Data.SetText("SEQ", $"{seq:00#}");
                    cBarcode.Data.SetText("LEVEL", string.IsNullOrEmpty(moistureSensitivityLevel) ? $"" : $"MSL:{moistureSensitivityLevel}");

                    WritePrintHist(barcode, material, supplier, date, qty, seq.ToString("00#"));
                    cBarcode.Print(false);
                    cBarcode.Data.Clear();
                    break;
                //임의발행
                case CustomEnum.RawMaterialPrintType.Remain:
                    material = textBox_Material.Text;
                    supplier = textBox_Supplier.Text;
                    barcodeBase = string.Concat(material, supplier, dateTimePicker_Date.Value.ToString("yyyyMMdd"));
                    dataRow = DbAccess.Default.GetDataRow
                        (
                         $@"
                        SELECT TOP 1
                               RM.RawMaterial
                             , CASE
                                   WHEN COALESCE(RM.MSLevel, '') != ''
                                       THEN 'MSL'
                                   WHEN RM.Type = 'Reel'
                                       THEN 'Reel'
                                       ELSE 'Box'
                               END AS Type
                             , RM.MSLevel
                          FROM RawMaterial RM
                         WHERE RM.RawMaterial = '{material}'
                        ;
                        "
                        );
                    materialName = textBox_MaterialName.Text;
                    spec = textBox_Spec.Text;
                    supplier = textBox_Supplier.Text;
                    supplierName = textBox_SupplierName.Text;
                    date = dateTimePicker_Date.Value.ToString("yyyy-MM-dd");
                    moistureSensitivityLevel = dataRow["MSLevel"].ToString();

                    type = dataRow["Type"].ToString();
                    cBarcode = GetLabelClassByType(type);

                    foreach (DataGridViewRow dataGridViewRow in dataGridView_BoxList.Rows)
                    {
                        boxQty = int.Parse(dataGridViewRow.Cells["BoxQty"].Value.ToString());
                        qty = int.Parse(dataGridViewRow.Cells["Qty"].Value.ToString());
                        seq = GetBoxSeq(barcodeBase, qty);
                        if (seq < 0)
                        {
                            return;
                        }

                        for (var i = 0; i < boxQty; i++)
                        {
                            barcode = barcodeBase + qty.ToString("000000#") + seq.ToString("00#");
                            cBarcode.Data.SetText("BARCODE1", barcode);
                            cBarcode.Data.SetText("RAWMATERIAL", material);
                            cBarcode.Data.SetText("RAWMATERIALNAME", materialName);
                            cBarcode.Data.SetText("SPEC", spec);
                            cBarcode.Data.SetText("SUPPLIER", $"{supplier}");
                            cBarcode.Data.SetText("SUPPLIERNAME", $"{supplierName}");
                            cBarcode.Data.SetText("PRODUCTDATE", $"{date}");
                            cBarcode.Data.SetText("QTY", $"{qty}");
                            cBarcode.Data.SetText("SEQ", $"{seq:00#}");
                            cBarcode.Data.SetText("LEVEL", string.IsNullOrEmpty(moistureSensitivityLevel) ? $"" : $"MSL:{moistureSensitivityLevel}");
                            WritePrintHist(barcode, material, supplier, date, qty, seq.ToString("00#"));
                            //cBarcode.Data.Clear();
                            cBarcode.Data.AddLabel();
                            seq++;
                        }
                    }

                    cBarcode.Print(false);
                    break;
                case CustomEnum.RawMaterialPrintType.Split:
                    barcodeBase = textBox_Split.Text;
                    if (!(DbAccess.Default.IsExist("Rm_Stock", $"Rm_Barcode = '{barcodeBase}'") >= 1))
                    {
                        MessageBox.Show("Không tìm thấy Barcode。(Barcode not found.)", "Không tìm thấy Barcode(Barcode not found.)", MessageBoxIcon.Warning);
                        return;
                    }

                    type = textBox_Type.Text;
                    cBarcode = GetLabelClassByType(type);
                    material = textBox_Material.Text;
                    materialName = textBox_MaterialName.Text;
                    spec = textBox_Spec.Text;
                    supplier = textBox_Supplier.Text;
                    supplierName = textBox_SupplierName.Text;
                    date = dateTimePicker_Date.Value.ToString("yyyy-MM-dd");
                    moistureSensitivityLevel = _currentDataRow["MSLevel"].ToString();
                    string tempBarcode = material + supplier + date.Replace("-", "");
                    var splitBarcodeList = new List<string>();
                    int maxQty = int.Parse(_currentDataRow["Qty"].ToString());
                    int totalQty = int.Parse(label_totalQty.Text);

                    if (maxQty > totalQty)
                    {
                        dataGridView_BoxList.Rows.Add(maxQty - totalQty, 1, maxQty - totalQty);
                    }

                    //등록 추가
                    foreach (DataGridViewRow dataGridViewRow in dataGridView_BoxList.Rows)
                    {
                        boxQty = int.Parse(dataGridViewRow.Cells["BoxQty"].Value.ToString());
                        qty = int.Parse(dataGridViewRow.Cells["Qty"].Value.ToString());
                        seq = GetBoxSeq(tempBarcode, qty);
                        if (seq < 0)
                        {
                            return;
                        }

                        for (var i = 0; i < boxQty; i++)
                        {
                            barcode = $"{tempBarcode}{qty:000000#}{seq:00#}";
                            splitBarcodeList.Add(barcode);
                            cBarcode.Data.SetText("BARCODE1", barcode);
                            cBarcode.Data.SetText("RAWMATERIAL", material);
                            cBarcode.Data.SetText("RAWMATERIALNAME", materialName);
                            cBarcode.Data.SetText("SPEC", spec);
                            cBarcode.Data.SetText("SUPPLIER", $"{supplier}");
                            cBarcode.Data.SetText("SUPPLIERNAME", $"{supplierName}");
                            cBarcode.Data.SetText("PRODUCTDATE", $"{date}");
                            cBarcode.Data.SetText("QTY", $"{qty}");
                            cBarcode.Data.SetText("SEQ", $"{seq:00#}");
                            cBarcode.Data.SetText("LEVEL", string.IsNullOrEmpty(moistureSensitivityLevel) ? $"" : $"MSL:{moistureSensitivityLevel}");
                            WritePrintHist(barcode, material, supplier, date, qty, $"{seq:00#}");
                            afterQuery.AppendLine
                                (
                                 $@"
                                INSERT
                                  INTO Rm_StockHist (
                                                      Rm_BarCode
                                                    , Rm_IO_Type
                                                    , Rm_Material
                                                    , Rm_Supplier
                                                    , Rm_ProdDate
                                                    , Rm_QtyinBox
                                                    , Rm_BoxSeq
                                                    , Rm_Bunch
                                                    , Rm_StockQty
                                                    , Rm_Status
                                                    , Rm_LocationGroup
                                                    , Rm_Location
                                                    , SendStatusErp
                                                    , ERP_SL_CD_FROM
                                                    , ERP_SL_CD_TO
                                                    , Rm_Created
                                                    , Rm_Updated
                                                    )
                                SELECT TOP 1 '{barcode}'
                                           , 'IN'
                                           , RS.Rm_Material
                                           , RS.Rm_Supplier
                                           , RS.Rm_ProdDate
                                           , '{qty:000000#}'
                                           , '{seq:00#}'
                                           , 'SPLIT'
                                           , '{qty}'
                                           , RS.Rm_Status
                                           , RS.Rm_LocationGroup
                                           , RS.Rm_Location
                                           , 0
                                           , ''
                                           , RS.Storage
                                           , GETDATE()
                                           , GETDATE()
                                  FROM Rm_Stock AS RS
                                 WHERE 1 = 1
                                   AND Rm_BarCode = '{barcodeBase}'
                                 ORDER BY Rm_Updated DESC
                                ;

                                INSERT
                                  INTO Rm_Stock (
                                                  Rm_BarCode
                                                , Rm_Material
                                                , Rm_Supplier
                                                , Rm_ProdDate
                                                , Rm_QtyinBox
                                                , Rm_BoxSeq
                                                , Rm_Bunch
                                                , Rm_StockQty
                                                , Rm_RemainQty
                                                , Rm_Status
                                                , Storage
                                                , Rm_LocationGroup
                                                , Rm_Location
                                                )
                                SELECT TOP 1 '{barcode}'
                                           , RS.Rm_Material
                                           , RS.Rm_Supplier
                                           , RS.Rm_ProdDate
                                           , '{qty:000000#}'
                                           , '{seq:00#}'
                                           , 'SPLIT'
                                           , '{qty}'
                                           , 0
                                           , RS.Rm_Status
                                           , RS.Storage
                                           , RS.Rm_LocationGroup
                                           , RS.Rm_Location
                                  FROM Rm_Stock AS RS
                                 WHERE 1 = 1
                                   AND RS.Rm_BarCode = '{barcodeBase}'
                                 ORDER BY RS.Rm_Updated DESC
                                ;
                                "
                                );
                            cBarcode.Data.AddLabel();
                            seq++;
                            
                        }
                    }
                    cBarcode.Print(false);

                    //스캔 추가
                    foreach (DataGridViewRow dataGridViewRow in dataGridView_ScanList.Rows)
                    {
                        barcode = dataGridViewRow.Cells["Barcode"].Value.ToString();
                        splitBarcodeList.Add(barcode);
                        qty = int.Parse(dataGridViewRow.Cells["QtyinBox"].Value.ToString());
                        afterQuery.AppendLine
                            (
                             $@"
                                INSERT
                                  INTO Rm_StockHist (
                                                      Rm_BarCode
                                                    , Rm_IO_Type
                                                    , Rm_Material
                                                    , Rm_Supplier
                                                    , Rm_ProdDate
                                                    , Rm_QtyinBox
                                                    , Rm_BoxSeq
                                                    , Rm_Bunch
                                                    , Rm_StockQty
                                                    , Rm_Status
                                                    , Rm_LocationGroup
                                                    , Rm_Location
                                                    , SendStatusErp
                                                    , ERP_SL_CD_FROM
                                                    , ERP_SL_CD_TO
                                                    , Rm_Created
                                                    , Rm_Updated
                                                    )
                                SELECT TOP 1 '{barcode}'
                                           , 'IN'
                                           , RS.Rm_Material
                                           , RS.Rm_Supplier
                                           , RS.Rm_ProdDate
                                           , '{qty:000000#}'
                                           , '{dataGridViewRow.Cells["BoxSeq"].Value}'
                                           , 'SPLIT'
                                           , '{qty}'
                                           , RS.Rm_Status
                                           , RS.Rm_LocationGroup
                                           , RS.Rm_Location
                                           , 0
                                           , ''
                                           , RS.Storage
                                           , GETDATE()
                                           , GETDATE()
                                  FROM Rm_Stock AS RS
                                 WHERE 1 = 1
                                   AND RS.Rm_BarCode = '{barcodeBase}'
                                 ORDER BY RS.Rm_Updated DESC
                                ;

                                INSERT
                                  INTO Rm_Stock ( Rm_BarCode
                                                , Rm_Material
                                                , Rm_Supplier
                                                , Rm_ProdDate
                                                , Rm_QtyinBox
                                                , Rm_BoxSeq
                                                , Rm_Bunch
                                                , Rm_StockQty
                                                , Rm_RemainQty
                                                , Rm_Status
                                                , Storage  
                                                , Rm_LocationGroup
                                                , Rm_Location )
                                SELECT TOP 1 '{barcode}'
                                           , Rm_Material
                                           , Rm_Supplier
                                           , Rm_ProdDate
                                           , '{qty:000000#}'
                                           , '{dataGridViewRow.Cells["BoxSeq"].Value}'
                                           , 'SPLIT'
                                           , '{qty}'
                                           , 0
                                           , Rm_Status
                                           , Storage   
                                           , Rm_LocationGroup
                                           , Rm_Location
                                  FROM Rm_Stock
                                 WHERE 1 = 1
                                   AND Rm_BarCode = '{barcodeBase}'
                                 ORDER BY Rm_Updated DESC
                                ;
                                "
                            );
                    }

                    afterQuery.AppendLine
                        (
                         $@"
                        --이전 바코드 이력추가
                        INSERT
                          INTO Rm_StockHist (
                                              Rm_BarCode
                                            , Rm_IO_Type
                                            , Rm_Material
                                            , Rm_Supplier
                                            , Rm_ProdDate
                                            , Rm_QtyinBox
                                            , Rm_BoxSeq
                                            , Rm_Bunch
                                            , Rm_StockQty
                                            , Rm_Status
                                            , Rm_LocationGroup
                                            , Rm_Location
                                            , SendStatusErp
                                            , ERP_SL_CD_FROM
                                            , ERP_SL_CD_TO
                                            , Rm_Created
                                            , Rm_Updated
                                            )
                        SELECT TOP 1 Rm_BarCode
                                   , 'OUT'
                                   , Rm_Material
                                   , Rm_Supplier
                                   , Rm_ProdDate
                                   , Rm_QtyinBox
                                   , Rm_BoxSeq
                                   , 'SPLIT'
                                   , Rm_StockQty
                                   , Rm_Status
                                   , Rm_LocationGroup
                                   , Rm_Location
                                   , 0
                                   , RS.Storage
                                   , ''
                                   , GETDATE()
                                   , GETDATE()
                          FROM Rm_Stock AS RS
                         WHERE 1 = 1
                           AND Rm_BarCode = '{barcodeBase}'
                         ORDER BY Rm_Updated DESC
                        ;

                        --이전 바코드 삭제
                        DELETE
                          FROM Rm_Stock
                         WHERE Rm_BarCode = '{barcodeBase}'
                        ;

                        --바코드 변경이력 Update
                        INSERT
                          INTO Rm_BCR
                              (
                                  Rm_Origin
                              ,   Rm_BarcodeBase
                              ,   Rm_OutQty
                              ,   Rm_RemainQty
                              ,   Rm_NewBarcodeList
                              ,   Issue
                              ,   Created
                              )
                        VALUES
                            (
                                '{barcodeBase}'
                            ,   ''
                            ,   ''
                            ,   ''
                            ,   '{string.Join(",", splitBarcodeList.ToArray())}'
                            ,   'Split'
                            ,   GETDATE()
                            )
                        ;
                        "
                        );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_currentPrintType), _currentPrintType, null);
            }

            try
            {
                int executeRowCount = 0;
                var connectionString = DbAccess.Default.ConnectionString;
                //System.Data.SqlClient.SqlConnection connection = null;

                if (string.IsNullOrEmpty(afterQuery.ToString())) return;
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        System.Data.SqlClient.SqlTransaction transaction = null;
                        try
                        {
                            transaction = connection.BeginTransaction();
                            using (var cmd = new System.Data.SqlClient.SqlCommand(afterQuery.ToString(), connection))
                            {
                                cmd.Transaction = transaction;
                                executeRowCount = cmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction?.Rollback();
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        
                        System.Windows.Forms.MessageBox.Show(ex + "\r\n" + connectionString);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetTextBoxHint(Control control, string hint)
        {
            if (!(control is TextBox textBox)) return;
            textBox.Enter += delegate(object sender, EventArgs args)
            {
                if (!control.Text.Equals(hint)) return;
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            };
            textBox.Leave += delegate(object sender, EventArgs args)
            {
                if (!string.IsNullOrEmpty(control.Text)) return;
                control.Text = hint;
                control.ForeColor = Color.Silver;
            };
            control.Text = hint;
            control.ForeColor = Color.Silver;
        }

        /// <summary>
        /// MaterialType:: Box, Reel, Msl
        /// </summary>
        /// <returns></returns>
        private clsBarcode.clsBarcode GetLabelClassByType(string type)
        {
            var cBarcode = new clsBarcode.clsBarcode();
            var bcdName = "";
            switch (type)
            {
                case "Box":
                    bcdName = "Label_RmBox";
                    break;
                case "Reel":
                    bcdName = "Label_RmReel";
                    break;
                case "MSL":
                    bcdName = "Label_RmReel";
                    break;
            }

            var bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='{bcdName}'");

            cBarcode.LoadFromXml(bcdData.ToString());
            return cBarcode;
        }

        private void WritePrintHist(string barcode, string material, string suppler, string date, int qty, string seq)
        {
            var query = new StringBuilder();
            query.AppendLine
                (
                 $@"
                INSERT
                  INTO RmPrintHist
                      (
                          Rm_BarCode
                      ,   Rm_Material
                      ,   Rm_Suppler
                      ,   Rm_Bunch
                      ,   Rm_StockDt
                      ,   Rm_Qty
                      ,   Rm_BoxSeq
                      ,   Rm_Updated
                      )
                VALUES
                    (
                        '{barcode}'
                    ,   '{material}'
                    ,   '{suppler}'
                    ,   NULL
                    ,   '{date}'
                    ,   {qty}
                    ,   '{seq}'
                    ,   GetDate()
                    )
                ;
                "
                );
            try
            {
                DbAccess.Default.ExecuteQuery(query.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetAfterPrintOut(CustomEnum.RawMaterialPrintType rawMaterialPrintType)
        {
            dataGridView_BoxList.DataSource = null;
            dataGridView_BoxList.Rows.Clear();
            ChangeControlPropertiesByPrintType(rawMaterialPrintType);
            ClearInformation();
        }

        private int GetBoxSeq(string barcodeBase, int qty)
        {
            var query = new StringBuilder();
            query.AppendLine
                (
                 $@"
                SELECT MAX(seq) AS seq
                  FROM (
                           SELECT COALESCE(MAX(TRY_CONVERT(INTEGER, RIGHT(Rm_BarCode, 3))), 0) + 1 AS seq
                             FROM RmPrintHist
                            WHERE Rm_BarCode LIKE '{barcodeBase}{qty:000000#}%'
                            UNION ALL
                           SELECT COALESCE(MAX(TRY_CONVERT(INTEGER, RIGHT(Rm_BarCode, 3))), 0) + 1 AS seq
                             FROM Rm_StockHist
                            WHERE Rm_BarCode LIKE '{barcodeBase}{qty:000000#}%'
                       ) AS T
                ;
                "
                );
            try
            {
                return DbAccess.Default.ExecuteScalar(query.ToString()) is int seq ? seq : -1;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        #endregion

        #region Event

        private void NewBarcode_Load(object sender, EventArgs e)
        {
            checkBox_Remain.Checked = true;
            //ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType.Remain);

            KeyPreview = false;

            dataGridView_BoxList.ColumnCount = 3;
            dataGridView_BoxList.Columns[0].Name = "Qty";
            dataGridView_BoxList.Columns[1].Name = "BoxQty";
            dataGridView_BoxList.Columns[2].Name = "TotalQty";

            dataGridView_ScanList.DataSource = _dataTable;

            // 빈레코드 표시 안하기
            dataGridView_BoxList.AllowUserToAddRows = false;
            dataGridView_ScanList.AllowUserToAddRows = false;
            dataGridView_ScanList.ReadOnly = true;
            //헤더 숨김
            dataGridView_BoxList.RowHeadersVisible = false;
            dataGridView_ScanList.RowHeadersVisible = false;
            // //수량변경 후 출고된 바코드 입력
            // SetTextBoxHint(textBox_ChangeBCD, "输入数量变更的出库条形码");
            // //재 출력할 바코드 입력
            // SetTextBoxHint(textBox_RePrintBCD, "输入要重新输出的条形码");
            // //임의로 출력할 바코드 입력
            // SetTextBoxHint(textBox_RemainBCD, "输入任意输出的条形码");
            // //분할할 바코드 입력
            // SetTextBoxHint(textBox_SplitBCD, "输入要分割的条形码");
        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn in ra không？(Do you want to print it out?)", "Xác nhận(Confirm)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            PrintOut(_currentPrintType);
            SetAfterPrintOut(_currentPrintType);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            dataGridView_BoxList.Rows.Clear();
            _dataTable.Rows.Clear();
            label_totalQty.Text = "0";
        }

        private void numericUpDown_BoxCount_ValueChanged(object sender, EventArgs e)
        {
            var inQty = (int) numericUpDown_QtyperBox.Value;
            var boxQty = (int) numericUpDown_BoxCount.Value;
            var sumQty = inQty * boxQty;
            textBox_Qty.Text = $@"{sumQty}";
        }

        private void numericUpDown_QtyperBox_ValueChanged(object sender, EventArgs e)
        {
            var inQty = (int) numericUpDown_QtyperBox.Value;
            var boxQty = (int) numericUpDown_BoxCount.Value;
            var sumQty = inQty * boxQty;
            textBox_Qty.Text = $@"{sumQty}";
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            //검증 필요 필수값 체크
            if (string.IsNullOrEmpty(textBox_Material.Text))
            {
                MessageBox.Show("Vui lòng nhập vật liệu。(Please enter the material.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBox_Supplier.Text))
            {
                MessageBox.Show("Vui lòng chọn loại hình。(Please select the supplier.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var inQty = numericUpDown_QtyperBox.Value;
            var boxQty = numericUpDown_BoxCount.Value;
            var qty = Convert.ToInt32(inQty * boxQty);
            if (_currentPrintType == CustomEnum.RawMaterialPrintType.Split)
            {
                int maxQty = Convert.ToInt32(_currentDataRow["Qty"]);
                int totalQty = Convert.ToInt32(label_totalQty.Text);
                if (maxQty < totalQty + qty)
                {
                    //수량이 초과되었습니다.
                    MessageBox.Show("Vượt quá số lượng。(Quantity exceeded.)", "Vượt quá số lượng(Quantity exceeded.)", MessageBoxIcon.Warning);
                    return;
                }
            }

            dataGridView_BoxList.Rows.Add(inQty, boxQty, qty.ToString());
            label_totalQty.Text = $@"{Convert.ToInt32(label_totalQty.Text) + qty}";
        }

        private void button_SupplierSearch_Click(object sender, EventArgs e)
        {
            //검증 필요 필수값 체크
            if (string.IsNullOrEmpty(textBox_Material.Text))
            {
                MessageBox.Show("Vui lòng nhập vật liệu。(Please enter the material.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newBarcodeSupplierSearch = new NewBarcodeSupplierSearch(textBox_Material.Text, textBox_Supplier.Text, textBox_SupplierName.Text);
            if (DialogResult.OK != newBarcodeSupplierSearch.ShowDialog()) return;
            textBox_Supplier.Text = newBarcodeSupplierSearch.GetResult("Code");
            textBox_SupplierName.Text = newBarcodeSupplierSearch.GetResult("Name");
        }

        private void textBox_Material_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var dataRow = DbAccess.Default.GetDataRow
                (
                 $@"
                SELECT TOP 1
                    RM.RawMaterial
                     , RM.Text
                     , RM.Spec
                     , CASE
                           WHEN COALESCE(RM.MSLevel, '') != ''
                               THEN 'MSL'
                           WHEN RM.Type = 'Reel'
                               THEN 'Reel'
                               ELSE 'Box'
                       END                         AS Type
                     , case when RM.Bunch = '25' then '000000' else  COALESCE(RMBS.Supplier, '') end AS Supplier
                     , COALESCE(S.Text, '')        AS SupplierName
                  FROM RawMaterial                           RM
                       LEFT OUTER JOIN RawMaterialBySupplier RMBS
                                       ON RM.RawMaterial = RMBS.RawMaterial
                       LEFT OUTER JOIN Supply                S
                                       ON RMBS.Supplier = S.Supply
                 WHERE RM.RawMaterial = '{textBox_Material.Text}'
	             order by RMBS.Updated desc
                "
                );
            if (dataRow is null)
            {
                textBox_Material.Text = "";
                textBox_MaterialName.Text = "";
                textBox_Spec.Text = "";
                textBox_Supplier.Text = "";
                textBox_SupplierName.Text = "";
                MessageBox.Show("Không tìm thấy thông tin vật liệu。(Material information not found.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                textBox_Material.Text = dataRow["RawMaterial"] as string;
                textBox_MaterialName.Text = dataRow["Text"] as string;
                textBox_Spec.Text = dataRow["Spec"] as string;
                textBox_Type.Text = dataRow["Type"] as string;
                textBox_Supplier.Text = dataRow["Supplier"] as string;
                textBox_SupplierName.Text = dataRow["SupplierName"] as string;
            }

            ActiveControl = button_SupplierSearch;
        }

        private void checkBox_Change_CheckedChanged(object sender, EventArgs e)
        {
            ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType.Change);
        }

        private void checkBox_RePrint_CheckedChanged(object sender, EventArgs e)
        {
            ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType.Reprint);
        }

        private void checkBox_Remain_CheckedChanged(object sender, EventArgs e)
        {
            ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType.Remain);
        }

        private void checkBox_Split_CheckedChanged(object sender, EventArgs e)
        {
            ChangeControlPropertiesByPrintType(CustomEnum.RawMaterialPrintType.Split);
        }

        private void textBox_RePrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!(sender is TextBox textBox)) return;
            var barcode = textBox.Text;
            if (DbAccess.Default.IsExist("RmPrintHist", $"Rm_Barcode = '{barcode}'") < 1)
            {
                MessageBox.Show("Barcode không tồn tại。(Nonexistent barcode.)", "Không tồn tại(Nonexistent)", MessageBoxIcon.Warning);
                return;
            }

            button_Add.Enabled = false;
            button_Clear.Enabled = false;
            button_Print.Enabled = true;
            textBox.Enabled = false;
        }

        private void textBox_Change_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!(sender is TextBox textBox)) return;
            var barcode = textBox.Text;
            if (DbAccess.Default.IsExist("Rm_Stock", $"Rm_Barcode = '{barcode}'") < 1)
            {
                MessageBox.Show("Barcode không tồn tại。(Nonexistent barcode.)", "Không tồn tại(Nonexistent)", MessageBoxIcon.Warning);
                return;
            }

            _currentDataRow = DbAccess.Default.GetDataRow
                (
                 $@"
                SELECT RS.Rm_Barcode             AS Barcode
                     , RS.Rm_Material            AS Material
                     , RM.Text                   AS MaterialName
                     , RM.Spec
                     , CASE
                           WHEN COALESCE(RM.MSLevel, '') != ''
                               THEN 'MSL'
                           WHEN RM.Type = 'Reel'
                               THEN 'Reel'
                               ELSE 'Box'
                       END                       AS Type
                     , RM.MSLevel
                     , RS.Rm_Supplier            AS Supplier
                     , S.Text                    AS SupplierName
                     , CONVERT(DATE, RS.Rm_ProdDate) AS Date
                     , RS.Rm_StockQty            AS Qty
                  FROM Rm_Stock                    RS
                       LEFT OUTER JOIN RawMaterial RM
                                       ON RS.Rm_Material = RM.RawMaterial
                       LEFT OUTER JOIN Supply      S
                                       ON RS.Rm_Supplier = S.Supply
                 WHERE RS.Rm_Barcode = '{barcode}'
                 "
                );
            SetDetail(_currentDataRow);
            button_Add.Enabled = true;
            button_Clear.Enabled = true;
            button_Print.Enabled = true;
            textBox_Material.ReadOnly = false;

            textBox.Enabled = false;
        }

        private void textBox_Split_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!(sender is TextBox textBox)) return;
            var barcode = textBox.Text;
            if (DbAccess.Default.IsExist("Rm_Stock", $"Rm_Barcode = '{barcode}'") < 1)
            {
                MessageBox.Show("Barcode không tồn tại。(Nonexistent barcode.)", "Không tồn tại(Nonexistent)", MessageBoxIcon.Warning);
                return;
            }

            _currentDataRow = DbAccess.Default.GetDataRow
                (
                 $@"
                SELECT RS.Rm_Barcode             AS Barcode
                     , RS.Rm_Material            AS Material
                     , RM.Text                   AS MaterialName
                     , RM.Spec
                     , CASE
                           WHEN COALESCE(RM.MSLevel, '') != ''
                               THEN 'MSL'
                           WHEN RM.Type = 'Reel'
                               THEN 'Reel'
                               ELSE 'Box'
                       END                       AS Type
                     , RM.MSLevel
                     , RS.Rm_Supplier            AS Supplier
                     , S.Text                    AS SupplierName
                     , RS.Rm_ProdDate            AS Date
                     , RS.Rm_StockQty            AS Qty
                  FROM Rm_Stock                    RS
                       LEFT OUTER JOIN RawMaterial RM
                                       ON RS.Rm_Material = RM.RawMaterial
                       LEFT OUTER JOIN Supply      S
                                       ON RS.Rm_Supplier = S.Supply
                 WHERE RS.Rm_Barcode = '{barcode}'
                "
                );
            SetDetail(_currentDataRow);
            button_Add.Enabled = true;
            button_Clear.Enabled = true;
            button_Print.Enabled = true;
            textBox.Enabled = false;
            textBox_SplitScanBarcode.Enabled = true;
            button_ScanAdd.Enabled = true;
            button_ScanClear.Enabled = true;
        }

        private void button_TestPrint_Click(object sender, EventArgs e)
        {
            var newBarcodeTestPrint = new NewBarcodeTestPrint();
            newBarcodeTestPrint.ShowDialog();
        }

        private void dataGridView_BoxList_DataSourceChanged(object sender, EventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private bool AddSplitScanList(string barcode)
        {
            try
            {
                //분할하려는 바코드와 동일할 때
                if (textBox_Split.Text.Equals(barcode))
                {
                    MessageBox.Show("(the same barcode.)", "", MessageBoxIcon.Warning);
                    return false;
                }

                //이미 리스트에 존재할 때
                if (_dataTable.Rows.Cast<DataRow>().Any(row => row["Barcode"].Equals(barcode)))
                {
                    MessageBox.Show("已扫描资材。(Recently scanned material.)", "Không tồn tại(Recently scanned)", MessageBoxIcon.Warning);
                    return false;
                }

                //이미 창고에 존재하는 바코드일 때
                if (DbAccess.Default.IsExist("Rm_StockHist", $"Rm_BarCode = '{barcode}'") > 0)
                {
                    MessageBox.Show("仓库里存在的条形码。(existent barcode.)", "Không tồn tại(existent)", MessageBoxIcon.Warning);
                    return false;
                }

                var dataRow = DbAccess.Default.GetDataRow($"EXEC Sp_Convert_RmBarcode '', '{barcode}'");
                if (dataRow["Message"].ToString() != "OK")
                {
                    MessageBox.Show($"{dataRow["Message"]}", "Warning", MessageBoxIcon.Warning);
                    return false;
                }

                var qty = Convert.ToInt32(dataRow["QtyinBox"]);
                int maxQty = Convert.ToInt32(_currentDataRow["Qty"]);
                int totalQty = Convert.ToInt32(label_totalQty.Text);
                if (maxQty < totalQty + qty)
                {
                    //수량이 초과되었습니다.
                    MessageBox.Show("Vượt quá số lượng。(Quantity exceeded.)", "Vượt quá số lượng(Quantity exceeded.)", MessageBoxIcon.Warning);
                    return false;
                }

                label_totalQty.Text = $@"{Convert.ToInt32(label_totalQty.Text) + qty}";
                _dataTable.ImportRow(dataRow);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{ex.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void textBox_SplitScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.KeyCode != Keys.Enter) return;
            AddSplitScanList(textBox.Text);
            textBox.Text = string.Empty;
        }

        private void button_ScanAdd_Click(object sender, EventArgs e)
        {
            var textBox = textBox_SplitScanBarcode;
            AddSplitScanList(textBox.Text);
            textBox.Text = string.Empty;
        }

        private void button_ScanClear_Click(object sender, EventArgs e)
        {
            //따로따로 하려면 총 수량 계산해줘야함.
            _dataTable.Rows.Clear();
        }

        private void dataGridView_ScanList_DataSourceChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_ScanList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                switch (dataGridViewColumn.Name)
                {
                    case "Barcode":
                        dataGridViewColumn.Width = 250;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "QtyinBox":
                        dataGridViewColumn.Width = 100;
                        dataGridViewColumn.DefaultCellStyle.Format = "#,#";
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "BoxSeq":
                        dataGridViewColumn.Width = 70;
                        dataGridViewColumn.DefaultCellStyle.Format = "#,#";
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }

                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion


    }
}
