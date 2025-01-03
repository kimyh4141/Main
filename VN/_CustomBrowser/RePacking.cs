﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class RePacking : Form
    {
        #region Field

        private DataTable _dataTableSubList = new DataTable();

        private readonly DataTable _dataTableDataGridViewSub = new DataTable();
        private readonly DataTable _dataTableChange = new DataTable();
        private CustomEnum.LabelType _currentLabelType = CustomEnum.LabelType.None;
        private bool _isNew;
        private SerialPort _serialPort = new SerialPort() { PortName = "COM3", BaudRate = 115200, DataBits = 8, StopBits =StopBits.One, NewLine = "\r\n"};
        private RePackingNew _rePackingNew = new RePackingNew();

        #endregion

        #region Constructor

        public RePacking()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private List<string> GetSerialPorts()
        {
            List<string> portNames = new List<string>();
            string[] port = SerialPort.GetPortNames();

            foreach (string portName in port)
            {
                portNames.Add(portName);
            }

            portNames.Sort();
            return portNames;
        }

        private void BindCombo_Method()
        {
            var method = new List<string>();

            method.Add("Hand Scanner");

            if (SerialPort.GetPortNames().Length > 0)
            {
                method.Add("Serial");
            }

            method.Insert(0, "");

            cb_Scan.DataSource = method;
        }

        private void BindCombo_PortName()
        {
            List<string> ports = GetSerialPorts();

            ports.Insert(0, "");

            comboBox_PortName.DataSource = ports;
        }


        private CustomEnum.LabelType GetLabelType(string barcode)
        {
            if (barcode.StartsWith("T")
                && barcode.Length == 17)
            {
                return CustomEnum.LabelType.Pcb;
            }

            if (barcode.StartsWith("B")
                && barcode.Length == 19)
            {
                return CustomEnum.LabelType.ProductBox;
            }

            if (barcode.StartsWith("P")
                && (barcode.Length == 19 || barcode.Length == 21))
            {
                return CustomEnum.LabelType.Pallet;
            }

            return CustomEnum.LabelType.None;
        }

        /// <summary>
        /// 스캔된 라벨의 정보를 Set
        /// </summary>
        /// <param name="labelType"></param>
        /// <param name="barcode"></param>
        private bool SetScanLabelInformation(CustomEnum.LabelType labelType, string barcode)
        {
            var query = new StringBuilder();
            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                    query.AppendLine
                    (
                        $@"
                        DECLARE @Barcode NVARCHAR(50) = '{barcode}'
                        ;

                        SELECT T.TableName
                             , T.BoxBcd AS Barcode
                             , T.PcbBcd AS SubBarcode
                             , T.Material
                             , CASE
                                   WHEN COALESCE(M_TOP.LG_ITEM_CD, '') = ''
                                       THEN M.LG_ITEM_CD
                                       ELSE M_TOP.LG_ITEM_CD
                               END      AS LG_ITEM_CD
                             , M.Text
                             , CASE
                                   WHEN COALESCE(M_TOP.LG_ITEM_CD, '') = ''
                                       THEN M.LG_ITEM_NM
                                       ELSE M_TOP.LG_ITEM_NM
                               END      AS LG_ITEM_NM
                             , M.Spec
                          FROM (
                               SELECT 'Stock' AS TableName
                                    , BoxBcd
                                    , PcbBcd
                                    , Material
                                 FROM Stock
                                WHERE BoxBcd = @Barcode
                                UNION ALL
                               SELECT 'Packing' AS TableName
                                    , BoxBcd
                                    , PcbBcd
                                    , Material
                                 FROM Packing
                                WHERE BoxBcd = @Barcode
                               ) AS                     T
                               LEFT OUTER JOIN Material M
                                               ON T.Material = M.Material
                               LEFT OUTER JOIN Material M_TOP
                                               ON M.TOP_ITEM_CD = M_TOP.Material
                         ORDER BY
                             Barcode
                        ;
                        "
                    );
                    break;
                case CustomEnum.LabelType.Pallet:
                    query.AppendLine
                    (
                        $@"
                        DECLARE @Barcode NVARCHAR(50) = '{barcode}'
                        ;

                        SELECT T.TableName
                             , T.PalletBcd AS Barcode
                             , T.BoxBcd    AS SubBarcode
                             , T.Material
                             , CASE
                                   WHEN COALESCE(M_TOP.LG_ITEM_CD, '') = ''
                                       THEN M.LG_ITEM_CD
                                       ELSE M_TOP.LG_ITEM_CD
                               END         AS LG_ITEM_CD
                             , M.Text
                             , CASE
                                   WHEN COALESCE(M_TOP.LG_ITEM_CD, '') = ''
                                       THEN M.LG_ITEM_NM
                                       ELSE M_TOP.LG_ITEM_NM
                               END         AS LG_ITEM_NM
                             , M.Spec
                          FROM (
                               SELECT DISTINCT
                                      'Stock' AS TableName
                                    , PalletBcd
                                    , BoxBcd
                                    , Material
                                 FROM Stock
                                WHERE PalletBcd = @Barcode
                                UNION ALL
                               SELECT DISTINCT
                                      'Packing' AS TableName
                                    , PalletBcd
                                    , BoxBcd
                                    , Material
                                 FROM Packing
                                WHERE PalletBcd = @Barcode
                               ) AS                     T
                               LEFT OUTER JOIN Material M
                                               ON T.Material = M.Material
                               LEFT OUTER JOIN Material M_TOP
                                               ON M.TOP_ITEM_CD = M_TOP.Material
                         ORDER BY
                             Barcode
                        ;
                        "
                    );
                    break;
            }

            try
            {
                _dataTableSubList = DbAccess.Default.GetDataTable(query.ToString());
                var count = _dataTableSubList.Rows.Count;
                if (count <= 0)
                {
                    System.Windows.Forms.MessageBox.Show($@"Thông tin Label không tồn tại。(Label information does not exist.)", "Cảnh báo(Warning)",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                textBox_Qty.Text = $@"{count:#,###}";
                foreach (DataRow dataRow in _dataTableSubList.Rows)
                {
                    if (_dataTableSubList.Rows.IndexOf(dataRow).Equals(0))
                    {
                        textBox_Material.Text = dataRow["Material"].ToString();
                        textBox_ItemCode.Text = dataRow["LG_ITEM_CD"].ToString();
                        textBox_MaterialName.Text = dataRow["Text"].ToString();
                        textBox_ItemName.Text = dataRow["LG_ITEM_NM"].ToString();
                        textBox_Spec.Text = dataRow["Spec"].ToString();
                        comboBox_Location.SelectedValue =
                            dataRow["TableName"].ToString() == "Stock" ? "Warehouse" : "Packing";
                    }

                    _dataTableDataGridViewSub.Rows.Add(dataRow["SubBarcode"]);
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string GetNewBarcode(CustomEnum.LabelType labelType, string barcode, int qty)
        {
            var barcodeBase = string.Empty;
            if (_isNew)
            {
                string dateCode = _rePackingNew.GetValue("DateCode").ToString();
                string line = _rePackingNew.GetValue("Line").ToString();
                string partNo = textBox_ItemCode.Text.Substring(3, 8);
                barcodeBase = string.Concat(dateCode, line, partNo);
                int seq = GetBarcodeSeq(labelType, barcodeBase);
                switch (labelType)
                {
                    case CustomEnum.LabelType.ProductBox:
                        return seq < 0 ? "" : $@"B{qty:00}{barcodeBase}{seq:0000}";
                    case CustomEnum.LabelType.Pallet:
                        return seq < 0 ? "" : $@"P{qty:0000}{barcodeBase}{seq:00}";
                    default:
                        return "";
                }
            }

            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                {
                    barcodeBase = barcode.Substring(3, 12);
                    int seq = GetBarcodeSeq(labelType, barcodeBase);
                    return seq < 0 ? "" : $@"B{qty:00}{barcodeBase}{seq:0000}";
                }
                case CustomEnum.LabelType.Pallet:
                {
                    barcodeBase = barcode.Substring(5, 12);
                    int seq = GetBarcodeSeq(labelType, barcodeBase);
                    return seq < 0 ? "" : $@"P{qty:0000}{barcodeBase}{seq:00}";
                }
                case CustomEnum.LabelType.None:
                case CustomEnum.LabelType.Pcb:
                default:
                    return "";
            }
        }

        private int GetCurrentPcbQty(CustomEnum.LabelType labelType)
        {
            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                    return _dataTableDataGridViewSub.Rows.Count;
                case CustomEnum.LabelType.Pallet:
                    var list = (from DataRow dataRow in _dataTableDataGridViewSub.Rows select $"'{dataRow["Barcode"]}'").ToList();
                    switch (comboBox_Location.SelectedValue.ToString())
                    {
                        case "Packing":
                            return DbAccess.Default.IsExist("Packing", $"BoxBcd IN ({string.Join(",", list.ToArray())})");
                        case "Warehouse":
                            return DbAccess.Default.IsExist("Stock", $"BoxBcd IN ({string.Join(",", list.ToArray())})");
                        default:
                            return -1;
                    }
                case CustomEnum.LabelType.None:
                case CustomEnum.LabelType.Pcb:
                default:
                    return -1;
            }
        }

        private int GetBarcodeSeq(CustomEnum.LabelType labelType, string barcodeBase)
        {
            var query = new StringBuilder();
            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                    query.AppendLine
                    (
                        $@"
                        DECLARE @BarcodeBase NVARCHAR(20) = '%{barcodeBase}%'
                        ;

                        SELECT MAX(Seq)
                          FROM (
                                   SELECT COALESCE(MAX(CONVERT(INTEGER, SerialNo)), 0) + 1 AS Seq
                                     FROM BoxbcdPrintHist
                                    WHERE BoxBarcode_2 LIKE @BarcodeBase
                               ) AS T
                        ;
                        "
                    );
                    break;
                case CustomEnum.LabelType.Pallet:
                    query.AppendLine
                    (
                        $@"
                        DECLARE @BarcodeBase NVARCHAR(20) = '%{barcodeBase}%'
                        ;

                        SELECT MAX(Seq)
                          FROM (
                                   SELECT COALESCE(MAX(CONVERT(INTEGER, SerialNo)), 0) + 1 AS Seq
                                     FROM PalletbcdPrintHist
                                    WHERE PalletBcd LIKE @BarcodeBase
                               ) AS T
                        ;
                        "
                    );
                    break;
            }

            return Convert.ToInt32(DbAccess.Default.ExecuteScalar(query.ToString()));
        }

        /// <summary>
        /// 변경리스트 저장
        /// </summary>
        /// <returns></returns>
        private bool SaveChangeList(bool removeAll = false)
        {
            CustomEnum.RepackingProcessType type;
            var oldBarcode = textBox_ScanBarcode.Text.Trim();
            var newBarcode = string.Empty;
            var query = new StringBuilder();
            query.AppendLine
            (
                $@"
                    DECLARE @TimeStamp DATETIME = GETDATE()
                    DECLARE @TimeStampVC NVARCHAR(50) = CONVERT(NVARCHAR, @TimeStamp, 20)
                "
            );

            if (removeAll)
            {
                type = CustomEnum.RepackingProcessType.UnpackingAll;
            }
            else
            {
                if (_dataTableDataGridViewSub.Rows.Count <= 0)
                {
                    type = CustomEnum.RepackingProcessType.Unpacking;
                }
                else
                {
                    var qty = GetCurrentPcbQty(_currentLabelType);
                    type = _isNew ? CustomEnum.RepackingProcessType.New : CustomEnum.RepackingProcessType.Change;
                    newBarcode = GetNewBarcode(_currentLabelType, oldBarcode, qty);
                    //Print
                    PrintOutLabel(ref query, _currentLabelType, newBarcode, oldBarcode, qty);
                }
            }
            GetProcessQuery(ref query, type, newBarcode, oldBarcode);
            int executeRowCount = 0;
            var connectionString = DbAccess.Default.ConnectionString;
            //System.Data.SqlClient.SqlConnection connection = null;

            if (string.IsNullOrEmpty(query.ToString())) return false;
            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    System.Data.SqlClient.SqlTransaction transaction = null;
                    try
                    {
                        transaction = connection.BeginTransaction();
                        using (var cmd = new System.Data.SqlClient.SqlCommand(query.ToString(), connection))
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
            return true;
        }

        /// <summary>
        /// 재포장
        /// </summary>
        private string GetProcessQuery(ref StringBuilder query, CustomEnum.RepackingProcessType updateType,
            string newBarcode, string oldBarcode)
        {
            var material = textBox_Material.Text.Trim();
            var isReceipt = comboBox_Location.SelectedValue.ToString() == "Warehouse" ? 1 : 0;
            switch (updateType)
            {
                case CustomEnum.RepackingProcessType.New:
                case CustomEnum.RepackingProcessType.Change:
                    //여기서 Unpacking
                    AppendQueryMain(ref query, CustomEnum.RepackingProcessType.Unpacking, _currentLabelType, isReceipt, newBarcode, oldBarcode);
                    foreach (DataRow dataRow in _dataTableDataGridViewSub.Rows)
                    {
                        var subBarcode = dataRow["Barcode"].ToString();
                        AppendQueryMain(ref query, updateType, _currentLabelType, isReceipt, newBarcode, subBarcode);
                    }
                    break;
                case CustomEnum.RepackingProcessType.Unpacking:
                    AppendQueryMain(ref query, updateType, _currentLabelType, isReceipt, newBarcode, oldBarcode);
                    break;
                case CustomEnum.RepackingProcessType.UnpackingAll:
                    AppendQueryMain(ref query, updateType, _currentLabelType, isReceipt, newBarcode, oldBarcode);
                    break;
            }

            AppendQueryRePackingHist(ref query, material, updateType, _currentLabelType, isReceipt, newBarcode, oldBarcode);
            return query.ToString();
        }

        /// <summary>
        /// 참조 쿼리에 재포장 메인쿼리 추가
        /// </summary>
        private static void AppendQueryMain(ref StringBuilder query, CustomEnum.RepackingProcessType updateType, CustomEnum.LabelType labelType, int isReceipt, string newBarcode, string oldBarcode)
        {
            var tableName = isReceipt == 1 ? "Stock" : "Packing";
            switch (updateType)
            {
                case CustomEnum.RepackingProcessType.New:
                case CustomEnum.RepackingProcessType.Change:
                    switch (labelType)
                    {
                        case CustomEnum.LabelType.ProductBox:
                            query.AppendLine
                            (
                                $@"
                                UPDATE KeyRelation
                                   SET BoxBcd     = '{newBarcode}'
                                     , UpdateUser = '{WiseApp.Id}'
                                     , Repacked   = @TimeStampVC
                                     , Updated    = @TimeStamp
                                 WHERE PcbBcd = '{oldBarcode}'
                                "
                            );
                            query.AppendLine
                            (
                                $@"
                                UPDATE {tableName}
                                   SET BoxBcd  = '{newBarcode}'
                                     , Updated = @TimeStamp
                                 WHERE PcbBcd = '{oldBarcode}'
                                "
                            );
                            break;
                        case CustomEnum.LabelType.Pallet:
                            query.AppendLine
                            (
                                $@"
                                UPDATE KeyRelation 
                                   SET PalletBcd  = '{newBarcode}'
                                     , UpdateUser = '{WiseApp.Id}'
                                     , Repacked   = @TimeStampVC
                                     , Updated    = @TimeStamp
                                 WHERE BoxBcd = '{oldBarcode}'
                                "
                            );
                            query.AppendLine
                            (
                                $@"
                                UPDATE {tableName}
                                   SET PalletBcd  = '{newBarcode}'
                                     , Updated = @TimeStamp
                                 WHERE BoxBcd = '{oldBarcode}'
                                "
                            );
                            break;
                    }

                    break;
                case CustomEnum.RepackingProcessType.Unpacking:
                    switch (labelType)
                    {
                        case CustomEnum.LabelType.ProductBox:
                            query.AppendLine
                            (
                                $@"
                                UPDATE KeyRelation
                                   SET BoxBcd     = NULL
                                     , UpdateUser = '{WiseApp.Id}'
                                     , Repacked   = @TimeStampVC
                                     , Updated    = @TimeStamp
                                 WHERE BoxBcd = '{oldBarcode}'
                                "
                            );
                            query.AppendLine
                            (
                                $@"
                                UPDATE {tableName}
                                   SET BoxBcd  = NULL
                                     , Updated = @TimeStamp
                                 WHERE BoxBcd = '{oldBarcode}'
                                "
                            );
                            break;
                        case CustomEnum.LabelType.Pallet:
                            query.AppendLine
                            (
                                $@"
                                UPDATE KeyRelation
                                   SET PalletBcd  = NULL
                                     , UpdateUser = '{WiseApp.Id}'
                                     , Repacked   = @TimeStampVC
                                     , Updated    = @TimeStamp
                                 WHERE PalletBcd = '{oldBarcode}'
                                "
                            );
                            query.AppendLine
                            (
                                $@"
                                UPDATE {tableName}
                                   SET PalletBcd = NULL
                                     , Updated   = @TimeStamp
                                 WHERE PalletBcd = '{oldBarcode}'
                                "
                            );
                            break;
                    }

                    break;
                case CustomEnum.RepackingProcessType.UnpackingAll:
                    switch (labelType)
                    {
                        case CustomEnum.LabelType.ProductBox:
                            query.AppendLine
                            (
                                $@"
                            UPDATE KeyRelation
                               SET BoxBcd     = NULL
                                 , UpdateUser = '{WiseApp.Id}'
                                 , Repacked   = @TimeStampVC
                                 , Updated    = @TimeStamp
                             WHERE BoxBcd = '{oldBarcode}'
                            "
                            );
                            query.AppendLine
                            (
                                $@"
                            UPDATE {tableName}
                               SET BoxBcd  = NULL
                                 , Updated = @TimeStamp
                             WHERE BoxBcd = '{oldBarcode}'
                            "
                            );
                            break;
                        case CustomEnum.LabelType.Pallet:
                            query.AppendLine
                            (
                                $@"
                            UPDATE KeyRelation
                               SET PalletBcd  = NULL
                                 , BoxBcd     = NULL
                                 , UpdateUser = '{WiseApp.Id}'
                                 , Repacked   = @TimeStampVC
                                 , Updated    = @TimeStamp
                             WHERE PalletBcd = '{oldBarcode}'
                            "
                            );
                            query.AppendLine
                            (
                                $@"
                            UPDATE {tableName}
                               SET PalletBcd = NULL
                                 , BoxBcd    = NULL
                                 , Updated   = @TimeStamp
                             WHERE PalletBcd = '{oldBarcode}'
                            "
                            );
                            break;
                    }

                    break;
            }
        }

        /// <summary>
        /// 재포장이력
        /// </summary>
        private static void AppendQueryRePackingHist(ref StringBuilder query, string material, CustomEnum.RepackingProcessType updateType, CustomEnum.LabelType labelType, int isReceipt, string newBarcode, string oldBarcode)
        {
            //재포장이력
            query.AppendLine
            (
                $@"
                    INSERT
                      INTO RePackingHist
                      ( Material
                      , UpdateType
                      , PackType
                      , IsReceipt
                      , NewBarcode
                      , OldBarcode
                      , UpdateUser
                      , Updated )
                    VALUES ( '{material}'
                           , '{Enum.GetName(typeof(CustomEnum.RepackingProcessType), updateType)}'
                           , '{Enum.GetName(typeof(CustomEnum.LabelType), labelType)}'
                           , '{isReceipt}'
                           , '{newBarcode}'
                           , '{oldBarcode}'
                           , '{WiseApp.Id}'
                           , @TimeStamp
                            )
                "
            );
        }

        private bool PrintOutLabel(ref StringBuilder query, CustomEnum.LabelType labelType, string newBarcode,
            string oldBarcode, int qty)
        {
            var cBarcode = new clsBarcode.clsBarcode();
            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                    cBarcode.LoadFromXml(DbAccess.Default
                        .ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'").ToString());
                    if (_isNew)
                    {
                        //Print
                        cBarcode.Data.SetText("PARTNO", textBox_ItemCode.Text);
                        cBarcode.Data.SetText("QTY", qty + " EA");
                        cBarcode.Data.SetText("DESC", textBox_MaterialName.Text);
                        cBarcode.Data.SetText("SPEC", textBox_ItemName.Text);
                        cBarcode.Data.SetText("DATE", $"{_rePackingNew.GetValue("Date"):yyyy. MM. dd}");
                        cBarcode.Data.SetText("BARCODE1", $"{textBox_ItemCode.Text}{qty:0000}");
                        cBarcode.Data.SetText("BARCODE2", newBarcode);

                        //gmryu 2023-10-10 일반라인/영문 라인 구분
                        char lineCheck = newBarcode[newBarcode.Length - 13];
                        cBarcode.Data.SetText("Y2SOLUTION", $"Y2 SOLUTION{(char.IsDigit(lineCheck) ? "(VN)" : "")}");
                        //PrintHist
                        query.AppendLine
                        (
                            $@"
                            INSERT
                              INTO BoxbcdPrintHist
                                  (
                                      BoxBarcode_1
                                  ,   BoxBarcode_2
                                  ,   Material
                                  ,   LG_PartNo
                                  ,   Spec
                                  ,   Description
                                  ,   Qty
                                  ,   ProductionDate
                                  ,   ProductionLine
                                  ,   Mfg_Line
                                  ,   Mfg_ymd
                                  ,   SerialNo
                                  ,   Reprint
                                  ,   Updated
                                  ,   Updater
                                  )
                            VALUES
                                (
                                    '{textBox_ItemCode.Text}{qty:0000}'
                                ,   '{newBarcode}'
                                ,   '{textBox_Material.Text}'
                                ,   '{textBox_ItemCode.Text}'
                                ,   '{textBox_ItemName.Text.Replace("'", "")}'
                                ,   '{textBox_MaterialName.Text}'
                                ,   {qty}
                                ,   '{_rePackingNew.GetValue("Date"):yyyy-MM-dd}'
                                ,   'RePacking'
                                ,   '{_rePackingNew.GetValue("Line")}'
                                ,   '{_rePackingNew.GetValue("DateCode")}'
                                ,   '{newBarcode.Substring(15, 4)}'
                                ,   0
                                ,   @TimeStamp
                                ,   '{WiseApp.Id}'
                                )
                        "
                        );
                    }
                    else
                    {
                        var prodDate = Convert.ToDateTime(DbAccess.Default
                            .ExecuteScalar(
                                $"SELECT dbo.GetDateWithDateCode(SUBSTRING('{oldBarcode}', 4, 3)) AS ProdDate")
                            .ToString());
                        cBarcode.LoadFromXml(DbAccess.Default
                            .ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'").ToString());
                        //Print
                        cBarcode.Data.SetText("PARTNO", textBox_ItemCode.Text);
                        cBarcode.Data.SetText("QTY", qty + " EA");
                        cBarcode.Data.SetText("DESC", textBox_MaterialName.Text);
                        cBarcode.Data.SetText("SPEC", textBox_ItemName.Text);
                        cBarcode.Data.SetText("DATE", $"{prodDate:yyyy. MM. dd}");
                        cBarcode.Data.SetText("BARCODE1", $"{textBox_ItemCode.Text}{qty:0000}");
                        cBarcode.Data.SetText("BARCODE2", newBarcode);

                        //gmryu 2023-10-10 일반라인/영문 라인 구분
                        char lineCheck = newBarcode[newBarcode.Length - 13];
                        cBarcode.Data.SetText("Y2SOLUTION", $"Y2 SOLUTION{(char.IsDigit(lineCheck) ? "(VN)" : "")}");
                        //PrintHist
                        query.AppendLine
                        (
                            $@"
                        INSERT
                          INTO BoxbcdPrintHist
                              (
                                  BoxBarcode_1
                              ,   BoxBarcode_2
                              ,   Material
                              ,   LG_PartNo
                              ,   Spec
                              ,   Description
                              ,   Qty
                              ,   ProductionDate
                              ,   ProductionLine
                              ,   Mfg_Line
                              ,   Mfg_ymd
                              ,   SerialNo
                              ,   Reprint
                              ,   Updated
                              ,   Updater
                              )
                        SELECT TOP 1
                               '{textBox_ItemCode.Text}{qty:0000}'
                             , '{newBarcode}'
                             , Material
                             , LG_PartNo
                             , Spec
                             , Description
                             , {qty}
                             , ProductionDate
                             , 'RePacking'
                             , Mfg_Line
                             , Mfg_ymd
                             , '{newBarcode.Substring(15, 4)}'
                             , 0
                             , @TimeStamp
                             , '{WiseApp.Id}'
                          FROM BoxbcdPrintHist
                         WHERE BoxBarcode_2 = '{oldBarcode}'
                         ORDER BY
                             SerialNo DESC
                        ;
                        "
                        );
                    }

                    break;
                case CustomEnum.LabelType.Pallet:
                    cBarcode.LoadFromXml(DbAccess.Default
                        .ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'").ToString());
                    if (_isNew)
                    {
                        //Print
                        cBarcode.Data.SetText("PARTNO", textBox_ItemCode.Text);
                        cBarcode.Data.SetText("MODEL", textBox_ItemName.Text);
                        cBarcode.Data.SetText("QTY", qty + " EA");
                        cBarcode.Data.SetText("PALLETBCD", newBarcode);
                        //PrintHist
                        query.AppendLine
                        (
                            $@"
                        INSERT
                          INTO PalletbcdPrintHist
                              (
                                  PalletBcd
                              ,   LG_PartNo
                              ,   Model
                              ,   Material
                              ,   Qty
                              ,   ProductionDate
                              ,   ProductionLine
                              ,   Mfg_ymd
                              ,   Mfg_Line
                              ,   SerialNo
                              ,   Reprint
                              ,   Updated
                              ,   Updater
                              )
                        VALUES
                            (
                                '{newBarcode}'
                            ,   '{textBox_ItemCode.Text}'
                            ,   '{textBox_ItemName.Text.Replace("'", "")}'
                            ,   '{textBox_Material.Text}'
                            ,   {qty}
                            ,   '{_rePackingNew.GetValue("Date"):yyyy-MM-dd}'
                            ,   'RePacking'
                            ,   '{_rePackingNew.GetValue("DateCode")}'
                            ,   '{_rePackingNew.GetValue("Line")}'
                            ,   '{newBarcode.Substring(17, 2)}'
                            ,   0
                            ,   @TimeStamp
                            ,   '{WiseApp.Id}'
                            )
                        "
                        );
                    }
                    else
                    {
                        //Print
                        cBarcode.Data.SetText("PARTNO", textBox_ItemCode.Text);
                        cBarcode.Data.SetText("MODEL", textBox_ItemName.Text);
                        cBarcode.Data.SetText("QTY", qty + " EA");
                        cBarcode.Data.SetText("PALLETBCD", newBarcode);
                        //PrintHist
                        query.AppendLine
                        (
                            $@"
                        INSERT
                          INTO PalletbcdPrintHist
                              (
                                  PalletBcd
                              ,   LG_PartNo
                              ,   Model
                              ,   Material
                              ,   Qty
                              ,   ProductionDate
                              ,   ProductionLine
                              ,   Mfg_ymd
                              ,   Mfg_Line
                              ,   SerialNo
                              ,   Reprint
                              ,   Updated
                              ,   Updater
                              )
                        SELECT TOP 1
                               '{newBarcode}'
                             , LG_PartNo
                             , Model
                             , Material
                             , {qty}
                             , ProductionDate
                             , 'RePacking'
                             , Mfg_ymd
                             , Mfg_Line
                             , '{newBarcode.Substring(17, 2)}'
                             , 0
                             , @TimeStamp
                             , '{WiseApp.Id}'
                          FROM PalletbcdPrintHist
                         WHERE PalletBcd = '{oldBarcode}'
                         ORDER BY
                             SerialNo DESC
                        ;
                        "
                        );
                    }

                    break;
            }

            cBarcode.Print(false);
            return true;
        }

        private void RefreshChangeList()
        {
            _dataTableDataGridViewSub.Clear();
            _dataTableChange.Clear();
            foreach (DataRow dataRow in _dataTableSubList.Rows)
            {
                _dataTableDataGridViewSub.Rows.Add(dataRow["SubBarcode"]);
            }

            textBox_SubScanBarcode.Text = string.Empty;
        }
        private void SaveClear()
        {
            _isNew = false;
            button_New.Enabled = true;

            textBox_ScanBarcode.Text = string.Empty;
            textBox_ScanBarcode.ReadOnly = false;

            comboBox_Location.SelectedIndex = 0;

            textBox_Material.Text = string.Empty;
            textBox_Type.Text = string.Empty;
            textBox_Qty.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;
            textBox_ItemCode.Text = string.Empty;
            textBox_ItemName.Text = string.Empty;
            textBox_Datetime.Text = string.Empty; //gmryu

            groupBox_SubList.Enabled = false;
            tableLayoutPanel_Right.Enabled = false;

            _dataTableDataGridViewSub.Clear();
            _dataTableChange.Clear();

            _currentLabelType = CustomEnum.LabelType.None;
        }

        private void Clear()
        {
            _isNew = false;
            button_New.Enabled = true;

            textBox_ScanBarcode.Text = string.Empty;
            textBox_ScanBarcode.ReadOnly = false;

            comboBox_Location.SelectedIndex = 0;

            textBox_Material.Text = string.Empty;
            textBox_Type.Text = string.Empty;
            textBox_Qty.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;
            textBox_ItemCode.Text = string.Empty;
            textBox_ItemName.Text = string.Empty;
            textBox_Datetime.Text = string.Empty; //gmryu

            cb_Scan.SelectedIndex = 0;
            comboBox_PortName.SelectedIndex = 0;

            groupBox_SubList.Enabled = false;
            tableLayoutPanel_Right.Enabled = false;

            _dataTableDataGridViewSub.Clear();
            _dataTableChange.Clear();

            _currentLabelType = CustomEnum.LabelType.None;
        }

        private void InsertIntoSysLog(string strSource, string strMsg)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery(
                $"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', '{strSource}', LEFT(ISNULL(N'{strMsg}',''),3000), 'Browser', GETDATE())");
        }

        #endregion

        #region Event

        private void RePacking_Load(object sender, EventArgs e)
        {
            _dataTableChange.Columns.Add("Seq");
            _dataTableChange.Columns.Add("Type");
            _dataTableChange.Columns.Add("Barcode");
            dataGridView_List.DataSource = _dataTableDataGridViewSub;

            _dataTableDataGridViewSub.Columns.Add("Barcode");
            dataGridView_ChangeHist.DataSource = _dataTableChange;

            var locationList = new BindingList<object>
            {
                new { Key = "", Value = "" }, new { Key = "Packing", Value = "Đóng gói(Packing)" },
                new { Key = "Warehouse", Value = "Kho(Warehouse)" }
            };
            comboBox_Location.DataSource = locationList;
            comboBox_Location.ValueMember = "Key";
            comboBox_Location.DisplayMember = "Value";
            comboBox_Location.SelectedIndex = 0;

            try
            {
                _serialPort.DataReceived += _serialPort_DataReceived;
                GetSerialPorts();
                BindCombo_Method();
                BindCombo_PortName();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to open serial port","Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!(sender is SerialPort serialPort)) return;

            try
            {
                var barcode = serialPort.ReadLine().Trim();

                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        textBox_SubScanBarcode.Text = barcode;
                        ScanBarcode(barcode);
                        textBox_SubScanBarcode.Text = string.Empty;
                    }));
                }

            }
            catch (IOException ex)
            {
                
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dataTableChange.Rows.Count <= 0)
                {
                    System.Windows.Forms.MessageBox.Show($@"Không có hạng mục thay đổi。(Nothing has changed.)",
                        "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(textBox_ItemCode.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập thông tin kiểm tra sản phẩm。(Please enter the product reference information.) - LG_ITEM_CD",
                        "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(textBox_ItemName.Text))
                {
                    MessageBox.Show(
                        "Vui lòng nhập thông tin kiểm tra sản phẩm。(Please enter the product reference information.) - LG_ITEM_NM",
                        "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)",
                        "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;
                if (SaveChangeList())
                {
                    //저장완료 메시지
                    System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)",
                        "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //Clear
                    SaveClear();
                }
                else
                {
                    //저장실패
                }
            }
            catch (Exception exception)
            {
                InsertIntoSysLog("button_Save_Click", $"{exception}");
                throw;
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)",
                    "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                Clear();
            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)",
                    "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                RefreshChangeList();
            }
        }

        private bool OnScanBarcode(CustomEnum.LabelType labelType, string barcode)
        {
            switch (labelType)
            {
                case CustomEnum.LabelType.ProductBox:
                    try
                    {
                        var query = $@"
                                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
                                ;

                                SELECT SUM(Count) AS Count
                                  FROM (
                                       SELECT COUNT(PcbBcd) AS Count
                                         FROM Stock
                                        WHERE BoxBcd = '{barcode}'
                                          AND COALESCE(PalletBcd, '') != ''
                                       ) C
                                ;
                                ";
                        if (DbAccess.Default.ExecuteScalar(query) is int result)
                        {
                            if (result > 0)
                            {
                                System.Windows.Forms.MessageBox.Show(
                                    $@"Đã được bao gồm trong Pallet。(Already included in Pallet.)", "Cảnh báo(Warning)",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return false;
                            }

                            goto default;
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    return false;
                case CustomEnum.LabelType.None:
                case CustomEnum.LabelType.Pcb:
                    System.Windows.Forms.MessageBox.Show($@"Loại Label không đúng。(Label of type not allowed.)",
                        "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                default:
                    return SetScanLabelInformation(_currentLabelType, barcode);
            }
        }

        private void textBox_ScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.KeyCode != Keys.Enter) return;
            var barcode = textBox.Text.Trim();
            _currentLabelType = GetLabelType(barcode);

            if (OnScanBarcode(_currentLabelType, barcode))
            {
                textBox_ScanBarcode.ReadOnly = true;
                textBox_Type.Text = Enum.GetName(typeof(CustomEnum.LabelType), _currentLabelType);

                groupBox_SubList.Enabled = true;
                tableLayoutPanel_Right.Enabled = true;
                comboBox_PortName.Enabled = false;
            }
            else
            {
                textBox_ScanBarcode.Text = string.Empty;
            }

            if (cb_Scan.SelectedIndex == 2)
            {
                comboBox_PortName.Enabled = true;
            }
            else
            {
                comboBox_PortName.Enabled = false;
            }
        }
        private void cb_Scan_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = cb_Scan.SelectedItem.ToString();

            if (string.IsNullOrEmpty(cb_Scan.Text))
            {
                textBox_SubScanBarcode.ReadOnly = true;
                comboBox_PortName.Enabled = false;
                comboBox_PortName.SelectedIndex = -1;
            }

            else if (selectedValue == "Serial")
            {
                comboBox_PortName.Enabled = true;
                textBox_SubScanBarcode.ReadOnly = true;
            }
            else
            {
                textBox_SubScanBarcode.ReadOnly = false;
                comboBox_PortName.Enabled = false;
                comboBox_PortName.SelectedIndex = 0;
            }
        }


        private void ScanBarcode(string barcode)
        {
            if (0 > cb_Scan.SelectedIndex)
            {
                System.Windows.Forms.MessageBox.Show($"Vui lòng kiểm tra phương thức quét。(Please check the scan method.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var subScanBarcode = barcode;

            
            if (_dataTableChange.Rows.Cast<DataRow>().Any(dataRow => dataRow["Barcode"].Equals(subScanBarcode)))
            {
                System.Windows.Forms.MessageBox.Show($"Đã được bao gồm trong Pallet。(Already included in ChangeHist.)",
                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (DataRow dataRow in _dataTableDataGridViewSub.Rows)
            {
                if (!dataRow["Barcode"].Equals(subScanBarcode)) continue;
                _dataTableChange.Rows.Add(_dataTableChange.Rows.Count + 1, "OUT", subScanBarcode);
                _dataTableDataGridViewSub.Rows.Remove(dataRow);
                return;
            }

            try
            {
                var labelType = GetLabelType(subScanBarcode);
                string tableName = comboBox_Location.SelectedValue.ToString() == "Warehouse" ? "Stock" : "Packing";
                if (0 >= DbAccess.Default.IsExist(tableName,
                        $"(PalletBcd = '{subScanBarcode}' OR BoxBcd = '{subScanBarcode}' OR PcbBcd = '{subScanBarcode}') AND Material = '{textBox_Material.Text}'"))
                {
                    System.Windows.Forms.MessageBox.Show($@"Sản phẩm không phù hợp！(The product does not match.)",
                        "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                switch (_currentLabelType)
                {
                    case CustomEnum.LabelType.ProductBox:
                        {
                            if (labelType != CustomEnum.LabelType.Pcb)
                            {
                                System.Windows.Forms.MessageBox.Show($@"Loại Label không đúng。(Label of type not allowed.)",
                                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (!(DbAccess.Default.ExecuteScalar(
                                        $"SELECT DISTINCT COALESCE(BoxBcd, '') FROM {tableName} WHERE PcbBcd = '{subScanBarcode}';")
                                    is string result))
                            {
                                System.Windows.Forms.MessageBox.Show(
                                    $@"Không tìm thấy thông tin 'PCB'。(Information of 'PCB' not found.)",
                                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (!string.IsNullOrEmpty(result))
                            {
                                System.Windows.Forms.MessageBox.Show(
                                    $"Đã được bao gồm trong Box。(Already included in Box.)\r\nBox : {result}",
                                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        break;
                    case CustomEnum.LabelType.Pallet:
                        {
                            if (labelType != CustomEnum.LabelType.ProductBox)
                            {
                                System.Windows.Forms.MessageBox.Show($@"Loại Label không đúng。(Label of type not allowed.)",
                                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (!(DbAccess.Default.ExecuteScalar(
                                        $"SELECT DISTINCT COALESCE(PalletBcd, '') FROM {tableName} WHERE BoxBcd = '{subScanBarcode}';")
                                    is string result))
                            {
                                System.Windows.Forms.MessageBox.Show(
                                    $@"Không tìm thấy thông tin Box。(Information of 'Box' not found.)", "Cảnh báo(Warning)",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (comboBox_Location.SelectedValue.ToString() != "Packing" && !string.IsNullOrEmpty(result))
                            {
                                System.Windows.Forms.MessageBox.Show(
                                    $"Đã được bao gồm trong Pallet。(Already included in Pallet.)\r\nPallet : {result}",
                                    "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        break;
                    case CustomEnum.LabelType.None:
                    case CustomEnum.LabelType.Pcb:
                    default:
                        System.Windows.Forms.MessageBox.Show($@"Loại Label không đúng。(Label of type not allowed.)",
                            "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                }

                _dataTableChange.Rows.Add(_dataTableChange.Rows.Count + 1, "IN", subScanBarcode);
                _dataTableDataGridViewSub.Rows.Add(subScanBarcode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{ex.Message}", "Lỗi(Error)",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox_SubScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.KeyCode != Keys.Enter) return;
            ScanBarcode(textBox.Text.Trim());
            textBox.Text = string.Empty;
        }

        private void dataGridView_ChangeHist_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                switch (dataGridViewColumn.Name)
                {
                    case "Seq":
                        dataGridViewColumn.Width = 50;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Type":
                        dataGridViewColumn.Width = 50;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Barcode":
                        dataGridViewColumn.Width = 200;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                }

                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView_ChangeHist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            dataGridView.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }

        private void button_New_Click(object sender, EventArgs e)
        {
            _rePackingNew.ShowDialog();
            if (_rePackingNew.DialogResult != DialogResult.OK) return;

            _isNew = true;
            button_New.Enabled = false;

            _currentLabelType = _rePackingNew.GetValue("Type") as string == "Box"
                ? CustomEnum.LabelType.ProductBox
                : CustomEnum.LabelType.Pallet;
            comboBox_Location.SelectedValue = _rePackingNew.GetValue("Location") as string;

            textBox_Material.Text = _rePackingNew.GetValue("Material") as string;
            textBox_ItemCode.Text = _rePackingNew.GetValue("ItemCode") as string;
            textBox_MaterialName.Text = _rePackingNew.GetValue("Name") as string;
            textBox_ItemName.Text = _rePackingNew.GetValue("ItemName") as string;
            textBox_Spec.Text = _rePackingNew.GetValue("Spec") as string;
            textBox_Datetime.Text = $@"{_rePackingNew.GetValue("Date"):yyyy-MM-dd}";

            textBox_ScanBarcode.ReadOnly = true;
            textBox_ScanBarcode.Text = "";
            textBox_Type.Text = Enum.GetName(typeof(CustomEnum.LabelType), _currentLabelType);
            groupBox_SubList.Enabled = true;
            tableLayoutPanel_Right.Enabled = true;

            textBox_SubScanBarcode.Focus();
        }

        private void button_RemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)",
                        "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)) return;
                if (SaveChangeList(true))
                {
                    //저장완료 메시지
                    System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)",
                        "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    //Clear
                    Clear();
                }
                else
                {
                    //저장실패
                }
            }
            catch (Exception exception)
            {
                InsertIntoSysLog("RePacking", $"{exception}");
                System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)",
                    "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void comboBox_PortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                if (string.IsNullOrEmpty(comboBox_PortName.Text)) return;
                _serialPort.PortName = comboBox_PortName.Text;
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}\r\n{ex.StackTrace}", "Không thể mở cổng.(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RePacking_FormClosing(object sender, FormClosingEventArgs e)
        {
            _serialPort.Close();
        }

        #endregion
    }
}