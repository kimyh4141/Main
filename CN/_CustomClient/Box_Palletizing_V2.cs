using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Data.SqlClient;
using WiseM.AppService;
using WiseM.Data;
using WiseM.Forms;
using clsBarcode;
using SortOrder = System.Windows.Forms.SortOrder;

namespace WiseM.Client
{
    public partial class Box_Palletizing_V2 : SkinForm
    {
        private delegate void OnSerialReceiveDelegate(byte[] bRcvBytes);

        private delegate void ReceivedFrom_DioDelegate(string strDio);

        private delegate void ReceivedFrom_1stScannerDelegate(string strScanner_1st);

        private delegate void ReceivedFrom_2ndScannerDelegate(string strScanner_2nd);

        #region Fieid

        private WbtUserService _WUService;
        string _Workorder = WbtCustomService.ActiveValues.WorkOrder;
        string _Material = WbtCustomService.ActiveValues.Material;
        string _Workcenter = WbtCustomService.ActiveValues.Workcenter;

        string _Scan1;
        string _Scan2;

        DataTable dt1 = new DataTable();
        DataTable dtQ = new DataTable();

        string ini_Qty = "0";
        string _orderQty;

        string _boxQty;
        string _palletQty;

        string _topMaterial;
        string _routing;
        string line;

        string boxBcd;
        string palletBcd;

        string boxCount;
        string palletCount;

        string _type;
        string _date;
        string _boxBarcode;
        string _palletBarcode;

        /// <summary>
        /// AutoScannerCheckTimer ReadTimeOut
        /// </summary>
        // private System.Timers.Timer _timerAutoScannerReadTimeOut = null;
        private System.IO.Ports.SerialPort serialPort1 = new SerialPort(); // for Digital IO

        private System.IO.Ports.SerialPort serialPort2 = new SerialPort(); // for AutoScanner #1
        private System.IO.Ports.SerialPort serialPort3 = new SerialPort(); // for AutoScanner #2

        private byte bSlaveId;
        private string strComPortName;
        private string singlePort;
        private string leftPort;
        private string rightPort;
        private string thresholdQty;

        bool boolDioAvailable = true; // DIO로 명령을 보낼 수 있는 플래그
        // DIO로 명령 보낸후 false,
        // DIO에서 응답이 오거나, TimeOut 걸리면 true

        System.Timers.Timer tmrDio1 = new System.Timers.Timer() {AutoReset = false, Interval = 1000};     // OK 접점 꺼주기 위해
        System.Timers.Timer tmrDio2 = new System.Timers.Timer() {AutoReset = false, Interval = 1000};     // NG 접점 꺼주기 위해
        System.Timers.Timer tmrDioReply = new System.Timers.Timer() {AutoReset = false, Interval = 1000}; // DIO가 응답을 안 할 수 있으니..

        private System.Timers.Timer _tmrIgnoreDataFromSerialPort2 = new System.Timers.Timer() {AutoReset = false, Interval = 3000}; // 오토스캐너로부터 데이터 수신후, 일정시간 내에 수신되는 데이터 무시
        private System.Timers.Timer _tmrIgnoreDataFromSerialPort3 = new System.Timers.Timer() {AutoReset = false, Interval = 3000};

        bool boolIgnoreDataFromSerialPort2 = false; // true:IgnoreTimer가 가동중이므로, 이때 수신되는 데이터 무시.
        bool boolIgnoreDataFromSerialPort3 = false;

        int int1stLabelFlag = 0; // 0:Init  1:OK  2:NG
        int int2ndLabelFlag = 0;

        string str1stLabel = ""; // 1st Scanner 값
        string str2ndLabel = "";

        string str1stErrMsg = ""; // 1st Scanner 에러메세지
        string str2ndErrMsg = "";

        #endregion

        #region Construtor

        public Box_Palletizing_V2(string type, string date, string palletBarcode, string boxBarcode)
        {
            InitializeComponent();

            try
            {
                this._type = type;
                this._date = date;
                this._palletBarcode = palletBarcode;
                this._boxBarcode = boxBarcode;

                string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\PackagingConfig";

                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                FileInfo fileInfo = new FileInfo(path + @"\Config.ini");

                if (!fileInfo.Exists)
                {
                    WritePrivateProfileString("Setting", "Digital I/O", "COM3", path + @"\Config.ini");
                    WritePrivateProfileString("Setting", "SINGLE", "COM4", path + @"\Config.ini");
                    WritePrivateProfileString("Setting", "Left", "COM4", path + @"\Config.ini");
                    WritePrivateProfileString("Setting", "Right", "COM5", path + @"\Config.ini");
                }

                var tempStrComPortName1 = new StringBuilder();
                var tempStrComPortName2 = new StringBuilder();
                var tempStrComPortName3 = new StringBuilder();
                var tempStrComPortName4 = new StringBuilder();

                GetPrivateProfileString("Setting", "Digital I/O", "", tempStrComPortName1, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "SINGLE", "", tempStrComPortName2, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "Left", "", tempStrComPortName3, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "Right", "", tempStrComPortName4, 256, path + @"\Config.ini");

                this.bSlaveId = (byte) (1);
                this.strComPortName = tempStrComPortName1.ToString();
                this.singlePort = tempStrComPortName2.ToString();
                this.leftPort = tempStrComPortName3.ToString();
                this.rightPort = tempStrComPortName4.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            Init();
        }

        #endregion

        #region Method

        private void Init()
        {
            try
            {
                lbl_workorder.Text = string.Empty;
                lbl_item.Text = string.Empty;
                lbl_Qty.Text = "0 / 0";
                dtQ.Rows.Clear();
                lbl_Error.Visible = false;
                lbl_BoxQty.Text = "0 / 0";
                lbl_PalletQty.Text = "0 / 0";
                lbl_Date.Text = $"{DateTime.ParseExact(_date, "yyyyMMdd", null).ToString("yyyy-MM-dd")}";

                lbl_workorder.Text = _Workorder;

                try
                {
                    string strCmd = $@"exec [Sp_WorkPcProcedureV3]
                                 @PS_GUBUN		= 'GET_TOPMOST_MATERIAL'
                                ,@PS_MATERIAL	= '{_Material}'";

                    DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                    if (ds1 == null
                        || ds1.Tables.Count == 0)
                        throw new Exception("Network problem occurred.");

                    if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                    {
                        System.Windows.Forms.MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    _topMaterial = ds1.Tables[0].Rows[0]["ITEM_CD"].ToString();

                    if (!string.IsNullOrEmpty(_boxBarcode)
                        || !string.IsNullOrEmpty(_palletBarcode))
                    {
                        string tempQuery = 
                            $@"
                            INSERT INTO BoxTemp (WorkOrder, WorkCenter, PcbBcd, BoxBcd, PalletBcd)
                            SELECT WorkOrder, '{_Workcenter}', PcbBcd, BoxBcd, NULL 
                            FROM Packing WHERE PalletBcd = '{_palletBarcode}'

                            UPDATE BoxTemp SET BoxBcd = NULL
                            WHERE BoxBcd = '{_boxBarcode}'

                            DELETE Packing WHERE PalletBcd = '{_palletBarcode}'

                            INSERT INTO PackingHist (IoType, PcbBcd, BoxBcd, PalletBcd, Material, Created, Updated, WorkOrder)
                            SELECT 'Repacking', PcbBcd, BoxBcd, PalletBcd, Material, GETDATE(), GETDATE(), WorkOrder
                            FROM Packing WHERE PalletBcd = '{_palletBarcode}'
                            ";
                        DbAccess.Default.ExecuteQuery(tempQuery);
                    }

                    string qty_Q = $@"SELECT QtyInBox, BoxQtyInPallet FROM Material WHERE Material = '{_topMaterial}'";
                    DataTable dt_qty = DbAccess.Default.GetDataTable(qty_Q);

                    string Q;
                    Q = $"SELECT m.Material, m.Spec, m.Type, o.OrderQty, c.Kind, c.Routing, m.Model, m.Text ";
                    Q += $"FROM WorkOrder o ";
                    Q += $"join Material m on o.Material = m.Material ";
                    Q += $"join WorkCenter c on o.Workcenter = c.Workcenter ";
                    Q += $"WHERE WorkOrder = '{_Workorder}'";

                    try
                    {
                        dtQ = DbAccess.Default.GetDataTable(Q);
                        if (dtQ == null
                            || dtQ.Rows.Count <= 0)
                        {
                            throw new Exception("Not Found Workorder");
                        }

                        lbl_item.Text = _Material + " / " + dtQ.Rows[0]["Spec"];
                        _orderQty = dtQ.Rows[0]["OrderQty"].ToString();
                        _routing = dtQ.Rows[0]["Routing"].ToString();
                    }
                    catch (Exception ex)
                    {
                        DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Process_Management.init', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                        System.Windows.Forms.MessageBox.Show($"检索基线信息时出现问题。\r\nA problem occurred while retrieving baseline information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                        return;
                    }

                    string ini_Q;
                    ini_Q = $@"
                            SELECT COALESCE(B.Count, 0) + COALESCE(OH.OutQty, 0) Cnt
                              FROM WorkOrder         WO
                                   LEFT OUTER JOIN (
                                                   SELECT B.WorkOrder
                                                       , COUNT(( B.PcbBcd )) AS Count
                                                       FROM BoxTemp B
                                                   WHERE 1 = 1
                                                       AND B.PalletBcd IS NULL
                                                   GROUP BY
                                                       B.WorkOrder
                                                   ) B
                                                   ON WO.WorkOrder = B.WorkOrder
                                   LEFT OUTER JOIN (
                                                   SELECT SUM(OH.OutQty) AS OutQty
                                                       , OH.WorkOrder
                                                       FROM OutputHist AS OH
                                                   GROUP BY
                                                       OH.WorkOrder
                                                   ) OH
                                                   ON WO.WorkOrder = OH.WorkOrder
                             WHERE WO.WorkOrder = '{_Workorder}'

                            SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated
                            FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL ORDER BY RecordId

                            SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.BoxBcd, BT.Updated
                            FROM (SELECT BT.BoxBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{_Workcenter}' AND PalletBcd IS NULL AND BoxBcd IS NOT NULL
                            GROUP BY BT.BoxBcd) AS BT ORDER BY BT.Updated

                            SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.PalletBcd, BT.Updated
                            FROM (SELECT BT.PalletBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{_Workcenter}' AND PalletBcd IS NOT NULL
                            GROUP BY BT.PalletBcd) AS BT ORDER BY BT.Updated

                            SELECT TOP 1 BoxBcd FROM BoxTemp WHERE WorkOrder = '{_Workorder}' AND BoxBcd IS NOT NULL ORDER BY RecordId DESC

                            SELECT TOP 1 PalletBcd FROM BoxTemp WHERE WorkOrder = '{_Workorder}' AND PalletBcd IS NOT NULL ORDER BY RecordId DESC
                                ";

                    DataSet dt_ini = DbAccess.Default.GetDataSet(ini_Q);
                    string count_pcb = dt_ini.Tables[0].Rows[0]["Cnt"].ToString();
                    string count_noBoxingPcb = dt_ini.Tables[1].Rows.Count.ToString();
                    string count_box = dt_ini.Tables[2].Rows.Count.ToString();

                    ini_Qty = count_pcb;

                    dgv_pcbInfo.DataSource = dt_ini.Tables[1];
                    dgv_boxInfo.DataSource = dt_ini.Tables[2];
                    dgv_palletInfo.DataSource = dt_ini.Tables[3];
                    AutoScrollDatagridView();

                    lbl_type.Text = _type;

                    lbl_item.Text = _Material + " / " + dtQ.Rows[0]["Spec"];
                    _orderQty = dtQ.Rows[0]["OrderQty"].ToString();
                    _boxQty = dt_qty.Rows[0]["QtyInBox"].ToString();
                    _palletQty = dt_qty.Rows[0]["BoxQtyInPallet"].ToString();

                    if (_boxQty == "0"
                        || _palletQty == "0")
                    {
                        MessageBox.ShowCaption("未设置每箱pcb数量或每托盘箱数。\r\n" + "The quantity of pcb per box or quantity of boxes per pallet is not set. \r\n", "Error", MessageBoxIcon.Error);
                        this.Close();
                    }

                    lbl_Qty.Text = $"{count_pcb} / {_orderQty}";
                    lbl_BoxQty.Text = $"{count_noBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $"{count_box} / {_palletQty}";

                    lbl_BoxBarcode.Text = dt_ini.Tables[4].Rows.Count <= 0 ? "-" : dt_ini.Tables[4].Rows[0]["BoxBcd"].ToString();

                    lbl_PalletBarcode.Text = dt_ini.Tables[5].Rows.Count <= 0 ? "-" : dt_ini.Tables[5].Rows[0]["PalletBcd"].ToString();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.init', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                    System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                if (lbl_type.Text == "SINGLE")
                {
                    lbl_prev2.Visible = false;
                    btn_Reset.Visible = false;
                    tableLayoutPanel12.SetColumnSpan(tableLayoutPanel14, 2);
                    tableLayoutPanel1.SetColumnSpan(lbl_prev1, 2);
                    lbl_prev1.Text = string.Empty;

                    lbl_Current2.Visible = false;
                    tableLayoutPanel1.SetColumnSpan(lbl_Current1, 2);
                    lbl_Current1.Text = string.Empty;
                }
                else
                {
                    lbl_prev2.Visible = true;
                    tableLayoutPanel16.SetColumnSpan(lbl_prev1, 1);
                    lbl_prev1.Text = string.Empty;
                    lbl_prev2.Text = string.Empty;

                    lbl_Current2.Visible = true;
                    tableLayoutPanel15.SetColumnSpan(lbl_Current1, 1);
                    lbl_Current1.Text = string.Empty;
                    lbl_Current2.Text = string.Empty;
                }

                if (lbl_type.Text == "SINGLE")
                {
                    serialPort2.PortName = singlePort;
                    serialPort2.BaudRate = 115200;
                    serialPort2.DataBits = 8;
                    serialPort2.StopBits = StopBits.One;
                    serialPort2.NewLine = "\r\n";
                    serialPort2.Open();
                    serialPort2.DataReceived += SerialPort2_DataReceived;
                }
                else
                {
                    serialPort2.PortName = leftPort;
                    serialPort2.BaudRate = 115200;
                    serialPort2.DataBits = 8;
                    serialPort2.StopBits = StopBits.One;
                    serialPort2.NewLine = "\r\n";
                    serialPort2.Open();
                    serialPort2.DataReceived += SerialPort2_DataReceived;

                    serialPort3.PortName = rightPort;
                    serialPort3.BaudRate = 115200;
                    serialPort3.DataBits = 8;
                    serialPort3.StopBits = StopBits.One;
                    serialPort3.NewLine = "\r\n";
                    serialPort3.Open();
                    serialPort3.DataReceived += SerialPort3_DataReceived;
                }

                dt1.Columns.AddRange
                    (
                     new DataColumn[]
                     {
                         new DataColumn("Cnt"), new DataColumn("PCB"), new DataColumn("(约会时间)DateTime")
                     }
                    );

                SetLogMessage("Program Started");

                dgv_pcbInfo.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
                dgv_boxInfo.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
                dgv_palletInfo.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);

                btn_Boxing.Font = new Font("Tahoma", 20, FontStyle.Bold);
                btn_Palletizing.Font = new Font("Tahoma", 20, FontStyle.Bold);
                btn_testPrint_Box.Font = new Font("Tahoma", 14, FontStyle.Bold);
                btn_testPrint_Pallet.Font = new Font("Tahoma", 14, FontStyle.Bold);

                foreach (DataGridViewColumn column in dgv_pcbInfo.Columns)
                {
                    switch (column.Index)
                    {
                        case 0:
                            column.Width = 70;
                            break;
                        case 1:
                            column.Width = 180;
                            break;
                        case 2:
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            break;
                    }

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewColumn column in dgv_boxInfo.Columns)
                {
                    switch (column.Index)
                    {
                        case 0:
                            column.Width = 70;
                            break;
                        case 1:
                            column.Width = 200;
                            break;
                        case 2:
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            break;
                    }

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                foreach (DataGridViewColumn column in dgv_palletInfo.Columns)
                {
                    switch (column.Index)
                    {
                        case 0:
                            column.Width = 70;
                            break;
                        case 1:
                            column.Width = 180;
                            break;
                        case 2:
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            break;
                    }

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message);
            }
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        private void InvokeProcessControls_Dio(string strDio)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ReceivedFrom_DioDelegate(ReceivedFrom_Dio), strDio);
            }
            else
            {
                ReceivedFrom_Dio(strDio);
            }
        }

        private void InvokeProcessControls_1st(string strScanner_1st)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ReceivedFrom_1stScannerDelegate(ReceivedFrom_1stScanner), strScanner_1st);
            }
            else
            {
                ReceivedFrom_1stScanner(strScanner_1st);
            }
        }

        private void InvokeProcessControls_2nd(string strScanner_2nd)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ReceivedFrom_2ndScannerDelegate(ReceivedFrom_2ndScanner), strScanner_2nd);
            }
            else
            {
                ReceivedFrom_2ndScanner(strScanner_2nd);
            }
        }

        private void ReceivedFrom_Dio(string strDio)
        {
            SetLogMessage("Reply from Digital I/O");
            this.tmrDioReply.Stop();
            this.boolDioAvailable = true;
        }

        private void ReceivedFrom_1stScanner(string scanData)
        {
            try
            {
                if (this.btn_Setting.Text != "Setting up")
                    this.btn_Setting.Enabled = false;

                this.lbl_Error.Visible = false;

                if (string.IsNullOrEmpty(lbl_Current1.Text))
                {
                    lbl_Current1.ForeColor = Color.DodgerBlue;
                    lbl_Current1.Text = scanData;
                }
                else
                {
                    if (lbl_Current1.ForeColor == Color.Red)
                    {
                        lbl_Current1.ForeColor = Color.DodgerBlue;
                        lbl_Current1.Text = scanData;
                    }
                    else
                    {
                        lbl_prev1.Text = lbl_Current1.Text;
                        lbl_prev2.Text = lbl_Current2.Text;
                        lbl_Current1.ForeColor = Color.DodgerBlue;
                        lbl_Current1.Text = scanData;
                        lbl_Current2.Text = string.Empty;
                    }
                }

                if (this.lbl_type.Text == "SINGLE")
                {
                    this.ProcessingSINGLE(scanData);
                }
                else
                {
                    this.Processing1stDOUBLE(scanData);
                }
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void ReceivedFrom_2ndScanner(string scanData)
        {
            try
            {
                if (this.btn_Setting.Text != "Setting up")
                    this.btn_Setting.Enabled = false;

                if (string.IsNullOrEmpty(lbl_Current2.Text))
                {
                    lbl_Current2.ForeColor = Color.DodgerBlue;
                    lbl_Current2.Text = scanData;
                }
                else
                {
                    if (lbl_Current2.ForeColor == Color.Red)
                    {
                        lbl_Current2.ForeColor = Color.DodgerBlue;
                        lbl_Current2.Text = scanData;
                    }
                    else
                    {
                        lbl_prev1.Text = lbl_Current1.Text;
                        lbl_prev2.Text = lbl_Current2.Text;
                        lbl_Current1.Text = string.Empty;
                        lbl_Current2.ForeColor = Color.DodgerBlue;
                        lbl_Current2.Text = scanData;
                    }
                }

                this.Processing2ndDOUBLE(scanData); // 무조건 DOUBLE만 있음.
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void DeleteOldFiles(string path, string strDate)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DateTime cmpTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                var fileCreatedTime = file.CreationTime;

                if (DateTime.Compare(fileCreatedTime, cmpTime) > 0)
                {
                    File.Delete(file.FullName);
                }
            }
        }

        private void ProcessingSINGLE(string scanData)
        {
            if (btn_Setting.Enabled)
                return;
            try
            {
                string strCmd = $@"exec [Sp_WorkPcProcedureV3]
                                @PS_GUBUN		= 'VERIFY_PCB_BARCODE'
                                ,@PS_ROUTING	= '{_routing}'
                                ,@PS_WORKORDER	= '{_Workorder}'
                                ,@PS_MATERIAL	= '{_Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{scanData}'";

                DataSet dataSet = DbAccess.Default.GetDataSet(strCmd);
                if (dataSet == null
                    || dataSet.Tables.Count == 0)
                {
                    string strErr = "Network problem occurred.";

                    this.lbl_Current1.ForeColor = Color.Red;
                    this.lbl_Error.Visible = true;
                    this.lbl_Error.Text = strErr;

                    SetLogMessage($"[{scanData}] {strErr}");

                    SetLogMessage($"Send NG signal to Digital I/O");
                    this.WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }

                if (dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    string strErr = dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr;

                    SetLogMessage($"[{scanData}] {strErr}");

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }


                if (dataSet.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") // Verify OK
                {
                    SetLogMessage($"{scanData} PCB Verify [OK]");

                    string insertQuery_Pcb = $@"
                                                insert into BoxTemp
                                                (WorkOrder, WorkCenter, PcbBcd, Created, Updated)
                                                Values
                                                ('{_Workorder}', '{_Workcenter}', '{scanData}', GETDATE(), GETDATE())
                                              ";
                    DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                    SetLogMessage($"{scanData} DataBase Save");

                    string Query_pcb = $@"
                                        SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                        WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL
                                        ORDER BY RecordId
                                                        
                                        SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                        FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' AND BoxBcd IS NULL

                                        SELECT COALESCE(B.Count, 0) + COALESCE(OH.OutQty, 0) Cnt
                                        FROM WorkOrder         WO
                                                LEFT OUTER JOIN (
                                                                SELECT B.WorkOrder
                                                                    , COUNT(( B.PcbBcd )) AS Count
                                                                FROM BoxTemp B
                                                                WHERE 1 = 1
                                                                AND B.PalletBcd IS NULL
                                                                GROUP BY
                                                                    B.WorkOrder
                                                                ) B
                                                                ON WO.WorkOrder = B.WorkOrder
                                                LEFT OUTER JOIN (
                                                                SELECT SUM(OH.OutQty) AS OutQty
                                                                    , OH.WorkOrder
                                                                FROM OutputHist AS OH
                                                                GROUP BY
                                                                    OH.WorkOrder
                                                                ) OH
                                                                ON WO.WorkOrder = OH.WorkOrder
                                        WHERE WO.WorkOrder = '{_Workorder}'
                                         ";
                    DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                    dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                    AutoScrollDatagridView();

                    string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                    lbl_Qty.Text = $@"{total_qty} / {_orderQty}";
                    lbl_BoxQty.Text = $@"{ds_pcb.Tables[1].Rows[0]["PcbCnt"]} / {_boxQty}";

                    SetLogMessage($"Send OK signal to Digital I/O");
                    this.WriteDigitalOut(1, true);

                    VerifyPrint_Box();
                }
                else
                {
                    string strErr = dataSet.Tables[0].Rows[0]["RTN_TXT"].ToString();

                    this.lbl_Current1.ForeColor = Color.Red;
                    this.lbl_Error.Visible = true;
                    this.lbl_Error.Text = strErr;

                    SetLogMessage($"[{scanData}] {strErr}");

                    SetLogMessage($"Send NG signal to Digital I/O");
                    this.WriteDigitalOut(2, true);
                }
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void Processing1stDOUBLE(string scanData)
        {
            if (btn_Setting.Enabled)
                return;
            try
            {
                string strCmd = $@"exec [Sp_WorkPcProcedureV3]
                                @PS_GUBUN		= 'VERIFY_PCB_BARCODE'
                                ,@PS_ROUTING	= '{_routing}'
                                ,@PS_WORKORDER	= '{_Workorder}'
                                ,@PS_MATERIAL	= '{_Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{scanData}'";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null
                    || ds1.Tables.Count == 0)
                {
                    string strErr = "Network problem occurred.";

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr + " / " + str2ndErrMsg;

                    switch (int2ndLabelFlag)
                    {
                        // 2nd에서 처리
                        case 0:
                            int1stLabelFlag = 2;
                            str1stErrMsg = strErr;
                            return;
                        case 1:
                            ResetFlagAll();

                            lbl_Current2.Text = "N/A";
                            lbl_Current2.ForeColor = Color.Red;

                            SetLogMessage($"[{scanData}] {strErr}");

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            throw new Exception(strErr);
                        default:
                            SetLogMessage($"[{scanData}],[{str2ndLabel}] {strErr} / {str2ndErrMsg}");

                            ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            this.WriteDigitalOut(2, true);
                            throw new Exception(strErr);
                    }
                }
                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    string strErr = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr + " / " + str2ndErrMsg;

                    if (int2ndLabelFlag == 0) // 2nd에서 처리
                    {
                        int1stLabelFlag = 2;
                        str1stErrMsg = strErr;
                        return;
                    }

                    // 1  or 2
                    SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {strErr}");

                    ResetFlagAll();

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }

                if (ds1.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") // Verify OK
                {
                    switch (this.int2ndLabelFlag)
                    {
                        // 2nd에서 처리
                        case 0:
                            this.str1stLabel = scanData;
                            this.int1stLabelFlag = 1;
                            return;
                        // OK 실적처리
                        ////////////////this.ResetFlagAll();
                        case 1 when scanData == this.str2ndLabel:
                        {
                            string strErr = "The 1st barcode and the 2nd barcode are the same.";

                            this.lbl_Current1.ForeColor = Color.Red;
                            this.lbl_Current2.ForeColor = Color.Red;
                            this.lbl_Error.Visible = true;
                            this.lbl_Error.Text = strErr;

                            this.ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            this.WriteDigitalOut(2, true);

                            return;
                        }
                        case 1:
                        {
                            string insertQuery_Pcb = $@"
                                                insert into BoxTemp
                                                (WorkOrder, WorkCenter, PcbBcd, Created, Updated)
                                                Values
                                                ('{_Workorder}', '{_Workcenter}', '{str2ndLabel}', GETDATE(), GETDATE()),
                                                ('{_Workorder}', '{_Workcenter}', '{scanData}', DATEADD(MILLISECOND, 5,GETDATE()), DATEADD(MILLISECOND, 5,GETDATE()))
                                                   ";
                            DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                            SetLogMessage($"[{scanData},{this.str2ndLabel}] Database save");

                            string Query_pcb = $@"
                                          SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                          WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL
                                          ORDER BY RecordId
                                                
                                          SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                          FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' AND BoxBcd IS NULL

                                          SELECT COALESCE(B.Count, 0) + COALESCE(OH.OutQty, 0) Cnt
                                            FROM WorkOrder         WO
                                                 LEFT OUTER JOIN (
                                                                 SELECT B.WorkOrder
                                                                      , COUNT(( B.PcbBcd )) AS Count
                                                                   FROM BoxTemp B
                                                                  WHERE 1 = 1
                                                                    AND B.PalletBcd IS NULL
                                                                  GROUP BY
                                                                      B.WorkOrder
                                                                 ) B
                                                                 ON WO.WorkOrder = B.WorkOrder
                                                 LEFT OUTER JOIN (
                                                                 SELECT SUM(OH.OutQty) AS OutQty
                                                                      , OH.WorkOrder
                                                                   FROM OutputHist AS OH
                                                                  GROUP BY
                                                                      OH.WorkOrder
                                                                 ) OH
                                                                 ON WO.WorkOrder = OH.WorkOrder
                                           WHERE WO.WorkOrder = '{_Workorder}'
                                             ";
                            DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                            dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                            AutoScrollDatagridView();

                            string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                            lbl_Qty.Text = $@"{total_qty} / {_orderQty}";
                            lbl_BoxQty.Text = $@"{ds_pcb.Tables[1].Rows[0]["PcbCnt"]} / {_boxQty}";

                            VerifyPrint_Box();

                            this.ResetFlagAll();

                            SetLogMessage($"Send OK signal to Digital I/O");
                            this.WriteDigitalOut(1, true);
                            break;
                        }
                        // 2nd가 NG이므로 NG처리하고 종료
                        default:
                            lbl_Current1.ForeColor = Color.Red;
                            this.lbl_Current1.Text = "N/A";
                            lbl_Error.Visible = true;
                            lbl_Error.Text = str1stErrMsg + " / " + this.str2ndErrMsg;

                            SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {str2ndErrMsg}");

                            this.ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            this.WriteDigitalOut(2, true);
                            return;
                    }
                }
                else
                {
                    string strErr = ds1.Tables[0].Rows[0]["RTN_TXT"].ToString();

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr + " / " + str2ndErrMsg;
                    str1stErrMsg = strErr;

                    switch (this.int2ndLabelFlag)
                    {
                        // 2nd에서 처리
                        case 0:
                            str1stLabel = scanData;
                            int1stLabelFlag = 2;
                            break;
                        case 1:
                            SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {str2ndErrMsg}");

                            ResetFlagAll();

                            lbl_Current2.Text = "N/A";
                            lbl_Current2.ForeColor = Color.Red;

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            return;
                        default:
                            SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {str2ndErrMsg}");

                            ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void Processing2ndDOUBLE(string scanData)
        {
            if (btn_Setting.Enabled)
                return;
            try
            {
                string strCmd = $@"
                                 exec [Sp_WorkPcProcedureV3]
                                 @PS_GUBUN		= 'VERIFY_PCB_BARCODE'
                                ,@PS_ROUTING	= '{_routing}'
                                ,@PS_WORKORDER	= '{_Workorder}'
                                ,@PS_MATERIAL	= '{_Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{scanData}'";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null
                    || ds1.Tables.Count == 0)
                {
                    string strErr = "Network problem occurred.";

                    lbl_Current2.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = str1stErrMsg + " / " + strErr;

                    if (int1stLabelFlag == 0) // 1st에서 처리
                    {
                        int2ndLabelFlag = 2;
                        str2ndErrMsg = strErr;
                        return;
                    }

                    // 1  or 2
                    SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {strErr}");

                    ResetFlagAll();

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    string strErr = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();

                    lbl_Current2.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = str1stErrMsg + " / " + strErr;

                    if (int1stLabelFlag == 0) // 1st에서 처리
                    {
                        int2ndLabelFlag = 2;
                        str2ndErrMsg = strErr;
                        return;
                    }

                    // 1  or 2
                    SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {strErr}");

                    ResetFlagAll();

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }

                if (ds1.Tables[0].Rows[0]["RTN_TXT"].ToString() != "OK") // Verify OK
                {
                    string strErr = ds1.Tables[0].Rows[0]["RTN_TXT"].ToString();

                    lbl_Current2.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = str1stErrMsg + " / " + strErr;
                    str2ndErrMsg = strErr;

                    switch (int1stLabelFlag)
                    {
                        // 1st에서 처리
                        case 0:
                            str2ndLabel = scanData;
                            int2ndLabelFlag = 2;
                            break;
                        case 1:
                            SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {str2ndErrMsg}");

                            ResetFlagAll();

                            lbl_Current1.Text = "N/A";
                            lbl_Current1.ForeColor = Color.Red;

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            return;
                        default:
                            SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {str2ndErrMsg}");

                            ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            return;
                    }

                    return;
                }

                switch (int1stLabelFlag)
                {
                    // 1st에서 처리
                    case 0:
                        str2ndLabel = scanData;
                        int2ndLabelFlag = 1;
                        break;
                    // OK 실적처리
                    ////////////////this.ResetFlagAll();
                    case 1 when scanData == str1stLabel:
                    {
                        string strErr = "The 1st barcode and the 2nd barcode are the same.";

                        lbl_Current1.ForeColor = Color.Red;
                        lbl_Current2.ForeColor = Color.Red;
                        lbl_Error.Visible = true;
                        lbl_Error.Text = strErr;

                        SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {strErr}");

                        ResetFlagAll();

                        SetLogMessage($"Send NG signal to Digital I/O");
                        WriteDigitalOut(2, true);

                        return;
                    }
                    case 1:
                    {
                        string insertQuery_Pcb = $@"
                                                insert into BoxTemp
                                                (WorkOrder, WorkCenter, PcbBcd, Created, Updated)
                                                Values
                                                ('{_Workorder}', '{_Workcenter}', '{scanData}', GETDATE(), GETDATE()),
                                                ('{_Workorder}', '{_Workcenter}', '{str1stLabel}', DATEADD(MILLISECOND, 5,GETDATE()), DATEADD(MILLISECOND, 5,GETDATE()))
                                                   ";

                        DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                        SetLogMessage($"[{this.str1stLabel},{scanData}] Database save");

                        string Query_pcb = $@"
                                        SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                        WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL
                                        ORDER BY Updated
                                                        
                                        SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                        FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' AND BoxBcd IS NULL

                                        SELECT COALESCE(B.Count, 0) + COALESCE(OH.OutQty, 0) Cnt
                                        FROM WorkOrder         WO
                                            LEFT OUTER JOIN (
                                                            SELECT B.WorkOrder
                                                                    , COUNT(( B.PcbBcd )) AS Count
                                                                FROM BoxTemp B
                                                                WHERE 1 = 1
                                                                AND B.PalletBcd IS NULL
                                                                GROUP BY
                                                                    B.WorkOrder
                                                            ) B
                                                            ON WO.WorkOrder = B.WorkOrder
                                            LEFT OUTER JOIN (
                                                            SELECT SUM(OH.OutQty) AS OutQty
                                                                    , OH.WorkOrder
                                                                FROM OutputHist AS OH
                                                                GROUP BY
                                                                    OH.WorkOrder
                                                            ) OH
                                                            ON WO.WorkOrder = OH.WorkOrder
                                        WHERE WO.WorkOrder = '{_Workorder}'
                                             ";
                        DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                        dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                        AutoScrollDatagridView();

                        string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                        lbl_Qty.Text = $@"{total_qty} / {_orderQty}";
                        lbl_BoxQty.Text = $@"{ds_pcb.Tables[1].Rows[0]["PcbCnt"]} / {_boxQty}";

                        VerifyPrint_Box();

                        ResetFlagAll();

                        SetLogMessage($"Send OK signal to Digital I/O");
                        WriteDigitalOut(1, true);
                        break;
                    }
                    // 1st가 NG이므로 NG처리하고 종료
                    default:
                        lbl_Current2.ForeColor = Color.Red;
                        lbl_Current2.Text = "N/A";
                        lbl_Error.Visible = true;
                        lbl_Error.Text = str1stErrMsg + " / " + str2ndErrMsg;

                        SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {str2ndErrMsg}");

                        ResetFlagAll();

                        SetLogMessage($"Send NG signal to Digital I/O");
                        WriteDigitalOut(2, true);
                        return;
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message);
            }
        }

        private void ResetFlagAll()
        {
            int1stLabelFlag = 0;
            int2ndLabelFlag = 0;
            str1stLabel = "";
            str2ndLabel = "";
            str1stErrMsg = "";
            str2ndErrMsg = "";
        }

        private void WriteDigitalOut(int intAdrs, bool bVal)
        {
            try
            {
                int i;
                for (i = 0; i < 15; i++)
                {
                    if (boolDioAvailable) break;
                    Thread.Sleep(100);
                    Application.DoEvents();
                }

                if (i < 3) // PLC에서 신호를 너무 빨리 받으면 안된다 하니, 좀 쉬었다 가자.
                {
                    for (int j = 0; j < (3 - i); j++)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                }

                byte[] bCmdPresetSingleRegister = {bSlaveId, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};

                bCmdPresetSingleRegister[3] = (byte) (intAdrs == 1 ? 0x02 : intAdrs == 2 ? 0x06 : intAdrs == 3 ? 0x0a : 0x0e); // Address      DO1:02,   DO2:06,     DO3:0a,       DO4:0e
                bCmdPresetSingleRegister[5] = (byte) (bVal ? 1 : 0);                                                           // Value
                bCmdPresetSingleRegister = CalcCRC.GetCRC(bCmdPresetSingleRegister);

                serialPort1.DiscardOutBuffer();
                serialPort1.DiscardInBuffer();
                serialPort1.Write(bCmdPresetSingleRegister, 0, bCmdPresetSingleRegister.Length);

                switch (intAdrs)
                {
                    case 1 when bVal:
                        tmrDio1.Start();
                        break;
                    case 2 when bVal:
                        tmrDio2.Start();
                        break;
                }

                boolDioAvailable = false;
                tmrDioReply.Start();
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message);
            }
        }

        private void VerifyPrint_Box()
        {
            try
            {
                string boxQ = $"select count(*) Count from BoxTemp where BoxBcd IS NULL and WorkCenter = '{_Workcenter}' and WorkOrder = '{_Workorder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();

                if (int.Parse(currentQty) < int.Parse(_boxQty)) return;
                Print("Boxing", _boxQty);

                Boxing(boxBcd, _boxQty);
                try
                {
                    var query =
                        $@"
                        SELECT COUNT(DISTINCT (BoxBcd)) AS Cnt
                          FROM BoxTemp
                         WHERE WorkCenter = '{_Workcenter}'
                           AND BoxBcd IS NOT NULL
                           AND PalletBcd IS NULL
                        ;

                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt
                             , BT.BoxBcd
                             , BT.Updated
                          FROM (
                                   SELECT BT.BoxBcd
                                        , MAX(BT.Updated) AS Updated
                                     FROM boxtemp BT
                                    WHERE WorkCenter = '{_Workcenter}'
                                      AND PalletBcd IS NULL
                                      AND BoxBcd IS NOT NULL
                                    GROUP BY BT.BoxBcd
                               ) AS BT
                         ORDER BY BT.Updated
                        ;

                        SELECT ROW_NUMBER() OVER (ORDER BY (
                                                               SELECT NULL
                                                           )) AS Cnt
                             , PcbBcd                         AS PCB_Barcode
                             , Updated
                          FROM BoxTemp
                         WHERE WorkCenter = '{_Workcenter}'
                           AND Boxbcd IS NULL
                         ORDER BY RecordId
                        ;
                        ";
                    var dataSet = DbAccess.Default.GetDataSet(query);

                    boxCount = dataSet.Tables[0].Rows[0]["Cnt"].ToString();
                    var nonBoxingPcb = dataSet.Tables[2].Rows.Count.ToString();

                    lbl_BoxQty.Text = $@"{nonBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_BoxBarcode.Text = $@"{boxBcd}";
                    dgv_boxInfo.DataSource = dataSet.Tables[1];
                    dgv_pcbInfo.DataSource = dataSet.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.VerifyPrint_Box', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                }

                VerifyPrint_Pallet();
            }
            catch (Exception ex)
            {
            }
        }

        private void VerifyPrint_Pallet()
        {
            try
            {
                string boxQ = $"select COUNT(DISTINCT(BoxBcd)) Count from BoxTemp where PalletBcd IS NULL and Boxbcd is not null and WorkCenter = '{_Workcenter}' and WorkOrder = '{_Workorder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();

                if (_palletQty != currentQty) return;
                Print("Palletizing", currentQty);

                Palletizing(palletBcd, _palletQty);
                try
                {
                    string query =
                        $@"
                        SELECT COUNT(DISTINCT (BoxBcd)) AS BoxCnt
                          FROM BoxTemp
                         WHERE WorkCenter = '{_Workcenter}'
                           AND PalletBcd IS NULL
                           AND BoxBcd IS NOT NULL
                        ;

                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt
                             , BT.PalletBcd
                             , BT.Updated
                          FROM (
                                   SELECT BT.PalletBcd
                                        , MAX(BT.Updated) AS Updated
                                     FROM boxtemp BT
                                    WHERE WorkCenter = '{_Workcenter}'
                                      AND PalletBcd IS NOT NULL
                                    GROUP BY BT.PalletBcd
                               ) AS BT
                         ORDER BY BT.Updated
                        ;

                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt
                             , BT.BoxBcd
                             , BT.Updated
                          FROM (
                                   SELECT BT.BoxBcd
                                        , MAX(BT.Updated) AS Updated
                                     FROM boxtemp BT
                                    WHERE WorkCenter = '{_Workcenter}'
                                      AND PalletBcd IS NULL
                                      AND BoxBcd IS NOT NULL
                                    GROUP BY BT.BoxBcd
                               ) AS BT
                         ORDER BY BT.Updated
                        ;
                        ";
                    var dataSet = DbAccess.Default.GetDataSet(query);

                    boxCount = dataSet.Tables[0].Rows[0]["BoxCnt"].ToString();
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_PalletBarcode.Text = $@"{palletBcd}";
                    dgv_palletInfo.DataSource = dataSet.Tables[1];
                    dgv_boxInfo.DataSource = dataSet.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.VerifyPrint_Pallet', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private bool Boxing(string barcode, string qty)
        {
            try
            {
                string updateQuery1 = $@"
                                    UPDATE BoxTemp set BoxBcd = '{barcode}', Updated = GETDATE()
                                    FROM (
                                    SELECT TOP ({qty}) PcbBcd FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and BoxBcd IS NULL ORDER BY RecordId ASC
                                    ) BT
                                    WHERE BoxTemp.PcbBcd = BT.PcbBcd
                                        ";
                string updateQuery2 = $@"
                                    UPDATE KeyRelation
                                    SET Box_WorkOrder = '{_Workorder}', Box_Material = '{_Material}', Box_Line = '{_Workcenter}', Boxed = CONVERT(VARCHAR, GETDATE(), 20), 
                                    Boxbcd = '{barcode}', Updated = GETDATE(), UpdateUser = '{WiseApp.Id}'
                                    FROM (SELECT PcbBcd from BoxTemp where BoxBcd = '{barcode}' AND WorkOrder = '{_Workorder}') BT
                                    WHERE KeyRelation.PcbBcd = BT.PcbBcd

                                    UPDATE KeyRelation
                                    SET Boxbcd = '{barcode}', Updated = GETDATE(), UpdateUser = '{WiseApp.Id}'
                                    FROM (SELECT PcbBcd from BoxTemp where BoxBcd = '{barcode}' AND WorkOrder <> '{_Workorder}') BT
                                    WHERE KeyRelation.PcbBcd = BT.PcbBcd
                                        ";

                DbAccess.Default.ExecuteQuery(updateQuery1);
                DbAccess.Default.ExecuteQuery(updateQuery2);
                return true;
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.Boxing', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                return false;
            }
        }

        private void Palletizing(string barcode, string qty)
        {
            try
            {
                string updateQuery1 = $@"
                            Update BoxTemp set PalletBcd = '{barcode}', Updated = GETDATE()
                            FROM (
                            SELECT PcbBcd FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and PalletBcd IS NULL AND BoxBcd IS NOT NULL
                            ) BT
                            WHERE BoxTemp.PcbBcd = BT.PcbBcd
                                        ";
                string updateQuery2 = $@"
                            UPDATE KeyRelation 
                            SET PalletBcd = '{barcode}', Palletized = CONVERT(VARCHAR, GETDATE(), 20), Updated = GETDATE(), UpdateUser = '{WiseApp.Id}'
                            FROM (SELECT PcbBcd from BoxTemp where PalletBcd = '{barcode}') BT
                            WHERE KeyRelation.PcbBcd = BT.PcbBcd
                                        ";

                DbAccess.Default.ExecuteQuery(updateQuery1);
                DbAccess.Default.ExecuteQuery(updateQuery2);

                string insertQuery;
                insertQuery = $@"
                            INSERT INTO Packing
                            (PcbBcd, BoxBcd, PalletBcd, WorkOrder, Material, Created, Updated )
                            SELECT PcbBcd, BoxBcd, PalletBcd, WorkOrder, '{_Material}', GETDATE(), GETDATE()
                            FROM BoxTemp WHERE WorkCenter = '{_Workcenter}'
                            AND PalletBcd = '{barcode}'

                            INSERT INTO PackingHist (IoType, PcbBcd, BoxBcd, PalletBcd, Material, Created, Updated, WorkOrder)
                            SELECT 'IN', PcbBcd, BoxBcd, PalletBcd, '{_Material}', GETDATE(), GETDATE(), WorkOrder
                            FROM BoxTemp WHERE WorkCenter = '{_Workcenter}'
                            AND PalletBcd = '{barcode}'
                                ";

                DbAccess.Default.ExecuteQuery(insertQuery);

                string Q = $@"SELECT count(*) FROM BoxTemp where PalletBcd = '{barcode}' AND WorkOrder = '{_Workorder}'";
                DataTable dtQ = DbAccess.Default.GetDataTable(Q);

                int pcbQty = int.Parse(dtQ.Rows[0][0].ToString());
                Good(pcbQty);
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        public void Good(int count)
        {
            try
            {
                EntryRequest r = new EntryRequest();
                r.UserColumns = new SortedList<string, object>();
                r.From = WiseApp.Id;
                r.Mode = WeMaintMode.Insert;
                r.ActiveJob = WbtCustomService.ActiveValues.ActiveJob;
                r.Workcenter = WbtCustomService.ActiveValues.Workcenter;
                r.Count = count;
                r.Cavity = 1;
                JobControl.Good.Insert(r);
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void Print(string ptype, string currentqty)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Print(ptype, currentqty); }));
            }
            else
            {
                try
                {
                    DataTable PNdt = DbAccess.Default.GetDataTable($@"SELECT LG_ITEM_CD, LG_ITEM_NM, Text FROM Material WHERE Material = '{_topMaterial}'");

                    string item_CD = PNdt.Rows[0]["LG_ITEM_CD"].ToString();
                    string item_NM = PNdt.Rows[0]["LG_ITEM_NM"].ToString();
                    string desc = PNdt.Rows[0]["Text"].ToString();

                    string Q = $"EXEC SP_Convert_Date '{_date}'";

                    string ymd = DbAccess.Default.ExecuteScalar(Q) as string;
                    line = _Workcenter.Substring(4, 1);

                    switch (ptype)
                    {
                        case "Boxing":
                        {
                            int qty = int.Parse(currentqty);

                            string tempqty1 = $"{qty:000#}";
                            string tempqty2 = $"{qty:0#}";

                            string boxbcd1 = item_CD + tempqty1;
                            string seqQ = $@"SELECT TOP 1 BoxBarcode_2 FROM BoxBcdPrintHist WITH(NOLOCK) WHERE BoxBarcode_2 like '%{ymd + line + item_CD.Substring(3, 8)}%' AND ProductionLine <> 'RePacking' ORDER BY SerialNo DESC";
                            //string seqQ = $@"SELECT TOP 1 BoxBarcode_2 FROM BoxBcdPrintHist WITH(NOLOCK) WHERE BoxBarcode_2 like '%{ymd + line + item_CD.Substring(3, 8)}%' ORDER BY SerialNo DESC";

                            int seq;
                            DataTable dtseq = DbAccess.Default.GetDataTable(seqQ);
                            if (dtseq.Rows.Count <= 0) seq = 1;
                            else seq = int.Parse(dtseq.Rows[0]["BoxBarcode_2"].ToString().Substring(15, 4)) + 1;
                            string tempseq = $"{seq:000#}";

                            boxBcd = "B" + tempqty2 + ymd + line + item_CD.Substring(3, 8) + tempseq;

                            string strSql = "";
                            DataTable dtMain = new DataTable();
                            
                            strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                            dtMain = DbAccess.Default.GetDataTable(strSql);

                            clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                            _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                            _clsBarcode.Data.SetText("PARTNO", item_CD);
                            _clsBarcode.Data.SetText("QTY", qty + " EA");
                            _clsBarcode.Data.SetText("DESC", desc);
                            _clsBarcode.Data.SetText("SPEC", item_NM);
                            _clsBarcode.Data.SetText("DATE", DateTime.ParseExact(_date, "yyyyMMdd", null).ToString("yyyy. MM. dd"));
                            _clsBarcode.Data.SetText("BARCODE1", boxbcd1);
                            _clsBarcode.Data.SetText("BARCODE2", boxBcd);

                            _clsBarcode.Print(false);

                            string b_printHistQ = $@"
                                    INSERT INTO BoxbcdPrintHist 
                                    (BoxBarcode_1, BoxBarcode_2, LG_PartNo, Spec, Description, ProductionDate, ProductionLine,
                                    Material, Qty, Mfg_Line, Mfg_ymd, SerialNo, Reprint, Updated, Updater)
                                    VALUES
                                    ('{boxbcd1}','{boxBcd}','{item_CD}','{item_NM}','{desc}', '{_date}', '{_Workcenter}',
                                    '{_Material}', '{qty}','{line}','{ymd}','{tempseq}', 0, GETDATE(),'{_Workcenter}')
                                        ";
                            DbAccess.Default.ExecuteQuery(b_printHistQ);

                            lbl_BoxBarcode.Text = boxBcd;
                            break;
                        }
                        case "Palletizing":
                        {
                            DataTable qty_dt = DbAccess.Default.GetDataTable($@"select count(PcbBcd) from BoxTemp where PalletBcd is null AND BoxBcd is not null and WorkOrder = '{_Workorder}'");
                            int qty = int.Parse(qty_dt.Rows[0][0].ToString());
                            string tempqty = $"{qty:000#}";

                            string seqQ = $@"SELECT TOP 1 PalletBcd FROM PalletbcdPrintHist WITH(NOLOCK) WHERE PalletBcd LIKE '%{ymd + line + item_CD.Substring(3, 8)}%' ORDER BY SerialNo DESC";
                            DataTable dtseq = DbAccess.Default.GetDataTable(seqQ);

                            int seq;
                            if (dtseq.Rows.Count <= 0) seq = 1;
                            else seq = int.Parse(dtseq.Rows[0]["PalletBcd"].ToString().Substring(17, 2)) + 1;
                            string tempseq = $"{seq:0#}";

                            palletBcd = "P" + tempqty + ymd + line + item_CD.Substring(3, 8) + tempseq;

                            string strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
                            DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                            clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                            _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                            _clsBarcode.Data.SetText("PARTNO", item_CD);
                            _clsBarcode.Data.SetText("MODEL", item_NM);
                            _clsBarcode.Data.SetText("QTY", qty + " EA");
                            _clsBarcode.Data.SetText("PALLETBCD", palletBcd);

                            _clsBarcode.Print(false);

                            string p_printHistQ = $@"
                                    INSERT INTO PalletbcdPrintHist
                                    (LG_PartNo, Model, Qty, PalletBcd, Mfg_ymd, Mfg_Line, ProductionDate, ProductionLine,
                                    Material, SerialNo, Reprint, Updated, Updater)
                                    VALUES
                                    ('{item_CD}','{item_NM}','{qty}','{palletBcd}','{ymd}','{line}', '{_date}', '{_Workcenter}',
                                    '{_Material}', '{tempseq}', 0, GETDATE(), '{_Workcenter}')
                                    ";
                            DbAccess.Default.ExecuteQuery(p_printHistQ);

                            lbl_PalletBarcode.Text = palletBcd;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.InsertIntoSysLog(ex.Message);
                }
            }
        }

        private void AutoScrollDatagridView()
        {
            if (dgv_pcbInfo.Rows.Count > 0)
            {
                dgv_pcbInfo.FirstDisplayedScrollingRowIndex = dgv_pcbInfo.Rows.Count - 1;
                dgv_pcbInfo.Rows[dgv_pcbInfo.Rows.Count - 1].Selected = true;
            }

            if (dgv_boxInfo.Rows.Count > 0)
            {
                dgv_boxInfo.FirstDisplayedScrollingRowIndex = dgv_boxInfo.Rows.Count - 1;
                dgv_boxInfo.Rows[dgv_boxInfo.Rows.Count - 1].Selected = true;
            }

            if (dgv_palletInfo.Rows.Count <= 0) return;
            dgv_palletInfo.FirstDisplayedScrollingRowIndex = dgv_palletInfo.Rows.Count - 1;
            dgv_palletInfo.Rows[dgv_palletInfo.Rows.Count - 1].Selected = true;
        }
         
        private void SetLogMessage(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message} \r\n";

            string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\Log\Box_Palletizing";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo = new FileInfo(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt");

            if (!fileInfo.Exists)
            {
                System.IO.File.AppendAllText(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt", logMessage, Encoding.Default);
            }

            System.IO.File.AppendAllText(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt", logMessage, Encoding.Default);
        }

        private void InsertIntoSysLog(string strMsg)
        {
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'WorkPC', LEFT(ISNULL(N'{strMsg}',''),3000), '{_Workcenter}', GETDATE())");
        }

        private void Process_threads_Boxing()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Boxing(); }));
            }
            else
            {
                string boxQ = $"select count(*) Count from BoxTemp where BoxBcd IS NULL and WorkCenter = '{_Workcenter}' and WorkOrder = '{_Workorder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();
                if (currentQty == "0")
                {
                    System.Windows.Forms.MessageBox.Show("没有PCB There is no PCB");
                    return;
                }

                if (System.Windows.Forms.MessageBox.Show
                        (
                         "你确定吗？ \r\nAre you sure?"
                       , "剩余数量已关闭(Box Remainder Closing)"
                       , MessageBoxButtons.YesNo
                       , MessageBoxIcon.Question
                        )
                    != DialogResult.Yes) return;
                Print("Boxing", currentQty);

                Boxing(boxBcd, currentQty);

                try
                {
                    string Query_box = $@"
                                        SELECT COUNT(DISTINCT(BoxBcd)) Cnt FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' AND BoxBcd IS NOT NULL AND PalletBcd IS NULL

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.BoxBcd, BT.Updated
                                        FROM (SELECT BT.BoxBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{_Workcenter}' AND PalletBcd IS NULL AND BoxBcd IS NOT NULL
                                        GROUP BY BT.BoxBcd) AS BT ORDER BY BT.Updated

                                        SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated
                                        FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL ORDER BY RecordId
                                             ";
                    DataSet ds_box = DbAccess.Default.GetDataSet(Query_box);

                    boxCount = ds_box.Tables[0].Rows[0]["Cnt"].ToString();
                    string nonBoxingPcb = ds_box.Tables[2].Rows.Count.ToString();

                    lbl_BoxQty.Text = $@"{nonBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_BoxBarcode.Text = $@"{boxBcd}";
                    dgv_boxInfo.DataSource = ds_box.Tables[1];
                    dgv_pcbInfo.DataSource = ds_box.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.SetControl', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                }

                VerifyPrint_Pallet();
            }
        }

        private void Process_threads_Palletizing()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Palletizing(); }));
            }
            else
            {
                string boxQ = $"select COUNT(DISTINCT(BoxBcd)) Count from BoxTemp where PalletBcd IS NULL and Boxbcd is not null and WorkCenter = '{_Workcenter}' and WorkOrder = '{_Workorder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();
                if (currentQty == "0")
                {
                    System.Windows.Forms.MessageBox.Show("没有箱子 There is no Box");
                    return;
                }

                string check_nonboxingQ = $@"SELECT PcbBcd FROM BoxTemp WHERE BoxBcd IS NULL AND WorkCenter = '{_Workcenter}' AND WorkOrder = '{_Workorder}'";
                DataTable dt_nonbox = DbAccess.Default.GetDataTable(check_nonboxingQ);
                if (dt_nonbox.Rows.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("有非盒装PCB There are Non-boxed PCBs");
                    return;
                }

                if (System.Windows.Forms.MessageBox.Show
                        (
                         "你确定吗？ \r\nAre you sure?", "剩余数量已关闭(Pallet Remainder Closing)",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        )
                    == DialogResult.Yes)
                {
                    Print("Palletizing", currentQty);

                    Palletizing(palletBcd, currentQty);

                    try
                    {
                        string Query_pallet = $@"
                                        SELECT COUNT(DISTINCT(BoxBcd)) AS BoxCnt FROM BoxTemp 
                                        WHERE WorkCenter = '{_Workcenter}' and PalletBcd IS NULL AND BoxBcd IS NOT NULL

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.PalletBcd, BT.Updated
                                        FROM (SELECT BT.PalletBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{_Workcenter}' AND PalletBcd IS NOT NULL
                                        GROUP BY BT.PalletBcd) AS BT ORDER BY BT.Updated

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.BoxBcd, BT.Updated
                                        FROM (SELECT BT.BoxBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{_Workcenter}' AND PalletBcd IS NULL AND BoxBcd IS NOT NULL
                                        GROUP BY BT.BoxBcd) AS BT ORDER BY BT.Updated
                                            ";
                        DataSet ds_pallet = DbAccess.Default.GetDataSet(Query_pallet);

                        boxCount = ds_pallet.Tables[0].Rows[0]["BoxCnt"].ToString();
                        lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                        lbl_PalletBarcode.Text = $@"{palletBcd}";
                        dgv_palletInfo.DataSource = ds_pallet.Tables[1];
                        dgv_boxInfo.DataSource = ds_pallet.Tables[2];
                        AutoScrollDatagridView();
                    }
                    catch (Exception ex)
                    {
                        DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.Process_threads_Palletizing', N'{ex.Message}', '{_Workcenter}', GETDATE())");
                    }
                }
            }
        }

        private void Process_threads_Clear()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Clear(); }));
            }
            else
            {
                if (dgv_pcbInfo.Rows.Count <= 0) return;

                if (System.Windows.Forms.MessageBox.Show
                        (
                         "你确定吗？ \r\nAre you sure?", "Clear",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        )
                    == DialogResult.Yes)
                {
                    string del_Q = $@"DELETE FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and BoxBcd IS NULL";
                    DbAccess.Default.ExecuteQuery(del_Q);

                    string Q = $@"
                    SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated 
                    FROM BoxTemp WHERE WorkCenter = '{_Workcenter}' and Boxbcd IS NULL ORDER BY RecordId

                    SELECT COALESCE(B.Count, 0) + COALESCE(OH.OutQty, 0) Cnt
                    FROM WorkOrder         WO
                        LEFT OUTER JOIN (
                                        SELECT B.WorkOrder
                                                , COUNT(( B.PcbBcd )) AS Count
                                            FROM BoxTemp B
                                            WHERE 1 = 1
                                            AND B.PalletBcd IS NULL
                                            GROUP BY
                                                B.WorkOrder
                                        ) B
                                        ON WO.WorkOrder = B.WorkOrder
                        LEFT OUTER JOIN (
                                        SELECT SUM(OH.OutQty) AS OutQty
                                                , OH.WorkOrder
                                            FROM OutputHist AS OH
                                            GROUP BY
                                                OH.WorkOrder
                                        ) OH
                                        ON WO.WorkOrder = OH.WorkOrder
                    WHERE WO.WorkOrder = '{_Workorder}'
                         ";
                    DataSet ds_Q = DbAccess.Default.GetDataSet(Q);
                    dgv_pcbInfo.DataSource = ds_Q.Tables[0];

                    string count_pcb = ds_Q.Tables[1].Rows[0]["Cnt"].ToString();
                    string count_nonBoxingPcb = ds_Q.Tables[0].Rows.Count.ToString();

                    lbl_Qty.Text = $"{count_pcb} / {_orderQty}";
                    lbl_BoxQty.Text = $"{count_nonBoxingPcb} / {_boxQty}";
                }
            }
        }

        private void Process_threads_Reprint_Box()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Reprint_Box(); }));
            }
            else
            {
                try
                {
                    if (lbl_BoxBarcode.Text == "-") return;

                    if (System.Windows.Forms.MessageBox.Show
                            (
                             "你确定吗？ \r\nAre you sure?"
                           , "Reprint"
                           , MessageBoxButtons.YesNo
                           , MessageBoxIcon.Question
                            )
                        != DialogResult.Yes) return;
                    string Query = $@"SELECT TOP 1 * FROM BoxbcdPrintHist WHERE BoxBarcode_2 = '{lbl_BoxBarcode.Text}' ORDER BY SerialNo DESC";
                    DataTable dt = DbAccess.Default.GetDataTable(Query);

                    string boxbarcode1 = dt.Rows[0]["BoxBarcode_1"].ToString();
                    string LG_PartNo = dt.Rows[0]["LG_PartNo"].ToString();
                    string spec = dt.Rows[0]["Spec"].ToString();
                    string desc = dt.Rows[0]["Description"].ToString();
                    string strQty = dt.Rows[0]["Qty"].ToString();
                    string productionDate = Convert.ToDateTime(dt.Rows[0]["ProductionDate"]).ToString("yyyy-MM-dd");

                    string tempqty = $"{strQty:0#}";

                    string strSql = "";
                    DataTable dtMain = new DataTable();

                    strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                    dtMain = DbAccess.Default.GetDataTable(strSql);

                    var _clsBarcode = new clsBarcode.clsBarcode();
                    _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                    _clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                    _clsBarcode.Data.SetText("QTY", tempqty + " EA");
                    _clsBarcode.Data.SetText("DESC", desc);
                    _clsBarcode.Data.SetText("SPEC", spec);
                    _clsBarcode.Data.SetText("DATE", productionDate.Substring(0, 4) + ". " + productionDate.Substring(5, 2) + ". " + productionDate.Substring(8, 2));
                    _clsBarcode.Data.SetText("BARCODE1", boxbarcode1);
                    _clsBarcode.Data.SetText("BARCODE2", lbl_BoxBarcode.Text);

                    _clsBarcode.Print(false);

                    string insert_Q = $@"
                                    INSERT INTO BoxbcdPrintHist 
                                    (BoxBarcode_1, BoxBarcode_2, LG_PartNo, Spec, Description, ProductionDate, ProductionLine,
                                    Material, Qty, Mfg_Line, Mfg_ymd, SerialNo, Reprint, Updated, Updater)
                                    SELECT TOP 1 BoxBarcode_1, BoxBarcode_2, LG_PartNo, Spec, Description, ProductionDate, ProductionLine,
                                    Material, Qty, Mfg_Line, Mfg_ymd, SerialNo, Reprint + 1, Updated, Updater FROM BoxbcdPrintHist 
                                    with(nolock) WHERE BoxBarcode_2 = '{lbl_BoxBarcode.Text}' ORDER BY SerialNo desc
                                            ";
                    DbAccess.Default.ExecuteQuery(insert_Q);
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog(ex.Message);
                }
            }
        }

        private void Process_threads_Reprint_Pallet()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Reprint_Pallet(); }));
            }
            else
            {
                try
                {
                    if (lbl_PalletBarcode.Text == "-") return;

                    if (System.Windows.Forms.MessageBox.Show
                            (
                             "你确定吗？ \r\nAre you sure?"
                           , "Reprint"
                           , MessageBoxButtons.YesNo
                           , MessageBoxIcon.Question
                            )
                        != DialogResult.Yes) return;
                    string Query = $@"SELECT TOP 1 * FROM PalletbcdPrintHist WITH(NOLOCK) WHERE PalletBcd = '{lbl_PalletBarcode.Text}' ORDER BY SerialNo DESC";
                    DataTable dt = DbAccess.Default.GetDataTable(Query);

                    string LG_PartNo = dt.Rows[0]["LG_PartNo"].ToString();
                    string model = dt.Rows[0]["Model"].ToString();
                    string strQty = dt.Rows[0]["Qty"].ToString();

                    string strSql = "";
                    DataTable dtMain = new DataTable();

                    strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                    dtMain = DbAccess.Default.GetDataTable(strSql);

                    clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                    _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                    _clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                    _clsBarcode.Data.SetText("MODEL", model);
                    _clsBarcode.Data.SetText("QTY", strQty + " EA");
                    _clsBarcode.Data.SetText("PALLETBCD", lbl_PalletBarcode.Text);

                    _clsBarcode.Print(false);

                    string insert_Q =
                        $@"
                            INSERT
                              INTO PalletbcdPrintHist ( LG_PartNo
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
                                       , Updated
                                       , Updater
                              FROM PalletbcdPrintHist
                             WHERE PalletBcd = '{lbl_PalletBarcode.Text}'
                             ORDER BY SerialNo DESC
                             ";
                    DbAccess.Default.ExecuteQuery(insert_Q);
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog(ex.Message);
                }
            }
        }

        #endregion

        #region Event

        private void Box_Palletizing_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

                this.serialPort1.PortName = this.strComPortName;
                this.serialPort1.BaudRate = 115200;

                this.serialPort1.Open();
                this.serialPort1.DataReceived += serialPort1_DataReceived;

                this.tmrDio1.Elapsed += tmrDio1_Elapsed;
                this.tmrDio2.Elapsed += tmrDio2_Elapsed;
                this.tmrDioReply.Elapsed += tmrDioReply_Elapsed;

                this._tmrIgnoreDataFromSerialPort2.Elapsed += TmrIgnoreDataFromSerialPort2_Elapsed;
                this._tmrIgnoreDataFromSerialPort3.Elapsed += TmrIgnoreDataFromSerialPort3_Elapsed;

                for (int i = 1; i < 5; i++)
                {
                    WriteDigitalOut(i, false);
                    boolDioAvailable = false;
                }

                dgv_pcbInfo.Focus();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }


            DeleteOldFiles("C:\\Program Files (x86)\\Wise-M Systems\\Wise-Mes\\Log", DateTime.Now.AddDays(-30).ToString("yyyyMMdd"));

            for (int i = 1; i < 5; i++)
            {
                WriteDigitalOut(i, false);
                boolDioAvailable = false;
            }
        }

        private void TmrIgnoreDataFromSerialPort2_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.boolIgnoreDataFromSerialPort2 = false;
        }

        private void TmrIgnoreDataFromSerialPort3_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.boolIgnoreDataFromSerialPort3 = false;
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPort1.BytesToRead > 0)
            {
                serialPort1.ReadExisting();
            }

            InvokeProcessControls_Dio("");
        }

        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string strScanner = this.serialPort2.ReadLine();

            if (this.boolIgnoreDataFromSerialPort2)
            {
                SetLogMessage("Scanner scan signal received again");
                return;
            }

            this.boolIgnoreDataFromSerialPort2 = true;
            this._tmrIgnoreDataFromSerialPort2.Start();

            InvokeProcessControls_1st(strScanner);
        }

        private void SerialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string strScanner = this.serialPort3.ReadLine();

            if (this.boolIgnoreDataFromSerialPort3)
            {
                SetLogMessage("Scanner scan signal received again");
                return;
            }

            this.boolIgnoreDataFromSerialPort3 = true;
            this._tmrIgnoreDataFromSerialPort3.Start();

            InvokeProcessControls_2nd(strScanner);
        }

        private void tmrDio1_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetLogMessage($"Turn off OK signal to Digital I/O");
            this.WriteDigitalOut(1, false);
        }

        private void tmrDio2_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetLogMessage($"Turn off NG signal to Digital I/O");
            this.WriteDigitalOut(2, false);
        }

        private void tmrDioReply_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.boolDioAvailable = true;
        }

        private void btn_testPrint_Box_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql;
                DataTable dtMain = new DataTable();

                strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                dtMain = DbAccess.Default.GetDataTable(strSql);

                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                _clsBarcode.Data.SetText("PARTNO", "EAY99999999");
                _clsBarcode.Data.SetText("QTY", "10 EA");
                _clsBarcode.Data.SetText("DESC", "TEST_DESC");
                _clsBarcode.Data.SetText("SPEC", "TEST_SPEC");
                _clsBarcode.Data.SetText("DATE", DateTime.Now.ToString("yyyy. MM. dd"));
                _clsBarcode.Data.SetText("BARCODE1", "EAY999999990010");
                _clsBarcode.Data.SetText("BARCODE2", "B10N8SG999999990001");

                //_clsBarcode.PrinterName = "Microsoft Print to PDF";
                _clsBarcode.Print(false);
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btn_testPrint_Pallet_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
                DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                _clsBarcode.Data.SetText("PARTNO", "EAY99999999");
                _clsBarcode.Data.SetText("MODEL", "MODEL_TEST");
                _clsBarcode.Data.SetText("QTY", "10 EA");
                _clsBarcode.Data.SetText("PALLETBCD", "P0010N8SG9999999901");

                _clsBarcode.Print(false);
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog(ex.Message);
            }
        }


        private void btn_boxing_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_boxing_Click(sender, e); }));
            }
            else
            {
                Thread thread = new Thread(Process_threads_Boxing);
                thread.Start();
            }
        }

        private void btn_palletizing_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Process_threads_Palletizing);
            thread.Start();
        }


        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Process_threads_Clear);
            thread.Start();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_Reset_Click(sender, e); }));
            }
            else
            {
                if (lbl_Current1.ForeColor != Color.Red
                    && lbl_Current2.ForeColor != Color.Red) return;

                this.ResetFlagAll();
                lbl_Current1.Text = string.Empty;
                lbl_Current2.Text = string.Empty;
            }
        }

        private void btn_BoxReprint_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Process_threads_Reprint_Box);
            thread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Process_threads_Reprint_Pallet);
            thread.Start();
        }

        private void btn_Setting_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_Setting_Click(sender, e); }));
            }
            else
            {
                if (btn_Setting.Text == "Setting up")
                {
                    if (System.Windows.Forms.MessageBox.Show("您确定要完成测试吗？ \r\nAre you sure you want to finish setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    lbl_Qty.ForeColor = Color.Black;
                    lbl_Qty.Text = $@"{ini_Qty} / {_orderQty}";
                    btn_Setting.Text = "设置\r\n (无数据处理)\r\nSetting\r\n(No data \r\nprocessing)";

                    lbl_Current1.Text = string.Empty;
                    lbl_Current2.Text = string.Empty;
                    lbl_prev1.Text = string.Empty;
                    lbl_prev2.Text = string.Empty;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show("您确定要开始测试吗？\r\nAre you sure you want to start setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    btn_Setting.Text = $@"Setting up";
                    lbl_Qty.ForeColor = Color.Red;
                    lbl_Qty.Text = "Setting...";
                }
            }
        }

        private void Box_Palletizing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.tmrDio1.Dispose();
                this.tmrDio2.Dispose();
                this.tmrDioReply.Dispose();

                this._tmrIgnoreDataFromSerialPort2.Dispose();
                this._tmrIgnoreDataFromSerialPort3.Dispose();
                serialPort1.Close();
                serialPort2.Close();
                serialPort3.Close();
            }
            catch (Exception ex)
            {
                Close();
            }
        }

        #endregion
    }
}
