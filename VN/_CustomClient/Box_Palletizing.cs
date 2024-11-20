using clsBarcode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using WiseM.AppService;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Client
{
    public partial class Box_Palletizing : SkinForm
    {
        private static string WorkOrder => WbtCustomService.ActiveValues.WorkOrder;
        private static string Material => WbtCustomService.ActiveValues.Material;
        private static string WorkCenter => WbtCustomService.ActiveValues.Workcenter;
        private static int OrderQty => (int) WbtCustomService.ActiveValues.OrderQty;
        private static string Routing => WbtCustomService.ActiveValues.Routing;
        private string Type { get; }
        private DateTime PackingDate { get; }
        private string PreviousBoxBarcode { get; }
        private string PreviousPalletBarcode { get; }

        //변경필요한 것들
        private string ini_Qty = "0";
       // private string line;
        private string _boxBcd;
        private string _palletBcd;
        private string _boxQty;
        private string _palletQty;
        private string _topMaterial;

        private SerialPort serialPort1 = new SerialPort(); // for Digital IO
        private SerialPort serialPort2 = new SerialPort(); // for AutoScanner #1
        private SerialPort serialPort3 = new SerialPort(); // for AutoScanner #2

        private byte bSlaveId;
        private string strComPortName;
        private string singlePort;
        private string leftPort;
        private string rightPort;
        private string thresholdQty;

        bool boolDioAvailable = true; // DIO로 명령을 보낼 수 있는 플래그
        // DIO로 명령 보낸후 false,
        // DIO에서 응답이 오거나, TimeOut 걸리면 true

        System.Timers.Timer tmrDio1 = new System.Timers.Timer {AutoReset = false, Interval = 1000};     // OK 접점 꺼주기 위해
        System.Timers.Timer tmrDio2 = new System.Timers.Timer {AutoReset = false, Interval = 1000};     // NG 접점 꺼주기 위해
        System.Timers.Timer tmrDioReply = new System.Timers.Timer {AutoReset = false, Interval = 1000}; // DIO가 응답을 안 할 수 있으니..

        System.Timers.Timer tmrIgnoreDataFromSerialPort2 = new System.Timers.Timer {AutoReset = false, Interval = 3000}; // 오토스캐너로부터 데이터 수신후, 일정시간 내에 수신되는 데이터 무시
        System.Timers.Timer tmrIgnoreDataFromSerialPort3 = new System.Timers.Timer {AutoReset = false, Interval = 3000};

        bool boolIgnoreDataFromSerialPort2; // true:IgnoreTimer가 가동중이므로, 이때 수신되는 데이터 무시.
        bool boolIgnoreDataFromSerialPort3;

        int int1stLabelFlag; // 0:ClearForm  1:OK  2:NG
        int int2ndLabelFlag;

        string str1stLabel = ""; // 1st Scanner 값
        string str2ndLabel = "";

        string str1stErrMsg = ""; // 1st Scanner 에러메세지
        string str2ndErrMsg = "";

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        public Box_Palletizing(string type, DateTime packingDate, string previousPalletBarcode = "", string previousBoxBarcode = "")
        {
            InitializeComponent();
            try
            {
                Type = type;
                PackingDate = packingDate;
                PreviousPalletBarcode = string.IsNullOrEmpty(previousPalletBarcode) ? string.Empty : previousPalletBarcode;
                PreviousBoxBarcode = string.IsNullOrEmpty(previousBoxBarcode) ? string.Empty : previousBoxBarcode;
                const string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\PackagingConfig";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                    var fileInfo = new FileInfo(path + @"\Config.ini");

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

                    bSlaveId = 1;
                    strComPortName = tempStrComPortName1.ToString();
                    singlePort = tempStrComPortName2.ToString();
                    leftPort = tempStrComPortName3.ToString();
                    rightPort = tempStrComPortName4.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            Init();
        }


        private void Box_Palletizing_Load(object sender, EventArgs e)
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");

                serialPort1.PortName = strComPortName;
                serialPort1.BaudRate = 115200;

                serialPort1.Open();
                serialPort1.DataReceived += serialPort1_DataReceived;

                tmrDio1.Elapsed += tmrDio1_Elapsed;
                tmrDio2.Elapsed += tmrDio2_Elapsed;
                tmrDioReply.Elapsed += tmrDioReply_Elapsed;

                tmrIgnoreDataFromSerialPort2.Elapsed += TmrIgnoreDataFromSerialPort2_Elapsed;
                tmrIgnoreDataFromSerialPort3.Elapsed += TmrIgnoreDataFromSerialPort3_Elapsed;

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
                Close();
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
            boolIgnoreDataFromSerialPort2 = false;
        }

        private void TmrIgnoreDataFromSerialPort3_Elapsed(object sender, ElapsedEventArgs e)
        {
            boolIgnoreDataFromSerialPort3 = false;
        }


        private void tmrDio1_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetLogMessage($"Turn off OK signal to Digital I/O");
            WriteDigitalOut(1, false);
        }

        private void tmrDio2_Elapsed(object sender, ElapsedEventArgs e)
        {
            SetLogMessage($"Turn off NG signal to Digital I/O");
            WriteDigitalOut(2, false);
        }

        private void tmrDioReply_Elapsed(object sender, ElapsedEventArgs e)
        {
            boolDioAvailable = true;
        }

        private void DeleteOldFiles(string path, string strDate)
        {
            var directoryInfo = new DirectoryInfo(path);
            var time = DateTime.ParseExact(strDate, "yyyyMMdd", null);

            foreach (var file in directoryInfo.GetFiles())
            {
                var fileCreatedTime = file.CreationTime;

                if (DateTime.Compare(fileCreatedTime, time) > 0)
                {
                    File.Delete(file.FullName);
                }
            }
        }

        private void Init()
        {
            try
            {
                lbl_workorder.Text = string.Empty;
                lbl_item.Text = string.Empty;
                lbl_Qty.Text = "0 / 0";
                lbl_Error.Visible = false;
                lbl_BoxQty.Text = "0 / 0";
                lbl_PalletQty.Text = "0 / 0";
                lbl_Date.Text = $@"{PackingDate:yyyy-MM-dd}";
                lbl_workorder.Text = WorkOrder;

                try
                {
                    string strCmd = $@"exec [Sp_WorkPcProcedureV3]
                                @PS_GUBUN		= 'GET_TOPMOST_MATERIAL'
                                ,@PS_MATERIAL	= '{Material}'";

                    var ds1 = DbAccess.Default.GetDataSet(strCmd);
                    if (ds1 == null
                        || ds1.Tables.Count == 0)
                        throw new Exception("Network problem occurred.");

                    if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                    {
                        System.Windows.Forms.MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    _topMaterial = ds1.Tables[0].Rows[0]["ITEM_CD"].ToString();

                    string queryGetStandardQty = $@"SELECT QtyInBox, BoxQtyInPallet FROM Material WHERE Material = '{_topMaterial}'";
                    var dataTable = DbAccess.Default.GetDataTable(queryGetStandardQty);
                    if (!string.IsNullOrEmpty(PreviousPalletBarcode)
                        || !string.IsNullOrEmpty(PreviousBoxBarcode))
                    {
                        string tempQuery = $@"
                                            INSERT
                                              INTO BoxTemp ( WorkOrder
                                                           , WorkCenter
                                                           , PcbBcd
                                                           , BoxBcd
                                                           , PalletBcd )
                                            SELECT WorkOrder
                                                 , '{WorkCenter}'
                                                 , PcbBcd
                                                 , BoxBcd
                                                 , NULL
                                              FROM Packing
                                             WHERE PalletBcd = '{PreviousPalletBarcode}'

                                            UPDATE BoxTemp
                                               SET BoxBcd = NULL
                                             WHERE BoxBcd = '{PreviousBoxBarcode}'

                                            INSERT
                                              INTO PackingHist ( IoType
                                                               , PcbBcd
                                                               , BoxBcd
                                                               , PalletBcd
                                                               , Material
                                                               , Created
                                                               , Updated
                                                               , WorkOrder )
                                            SELECT 'Repacking'
                                                 , PcbBcd
                                                 , BoxBcd
                                                 , PalletBcd
                                                 , Material
                                                 , GETDATE()
                                                 , GETDATE()
                                                 , WorkOrder
                                              FROM Packing
                                             WHERE PalletBcd = '{PreviousPalletBarcode}'

                                            DELETE Packing
                                             WHERE PalletBcd = '{PreviousPalletBarcode}'
                                              ";
                        DbAccess.Default.ExecuteQuery(tempQuery);
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
                             WHERE WO.WorkOrder = '{WorkOrder}'

                    SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated
                    FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL ORDER BY RecordId

                    SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.BoxBcd, BT.Updated
                    FROM (SELECT BT.BoxBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{WorkCenter}' AND PalletBcd IS NULL AND BoxBcd IS NOT NULL
                    GROUP BY BT.BoxBcd) AS BT ORDER BY BT.Updated

                    SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.PalletBcd, BT.Updated
                    FROM (SELECT BT.PalletBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{WorkCenter}' AND PalletBcd IS NOT NULL
                    GROUP BY BT.PalletBcd) AS BT ORDER BY BT.Updated

                    SELECT TOP 1 BoxBcd FROM BoxTemp WHERE WorkOrder = '{WorkOrder}' AND BoxBcd IS NOT NULL ORDER BY RecordId DESC

                    SELECT TOP 1 PalletBcd FROM BoxTemp WHERE WorkOrder = '{WorkOrder}' AND PalletBcd IS NOT NULL ORDER BY RecordId DESC
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

                    lbl_type.Text = Type;

                    lbl_item.Text = $@"{Material} / {WbtCustomService.ActiveValues.MaterialSpec}";
                    _boxQty = dataTable.Rows[0]["QtyInBox"].ToString();
                    _palletQty = dataTable.Rows[0]["BoxQtyInPallet"].ToString();

                    if (_boxQty == "0" || _palletQty == "0")
                    {
                        MessageBox.ShowCaption("Chưa cài đặt số lượng PCB/Box hoặc số lượng Box/Pallet。\r\n" + "The quantity of pcb per box or quantity of boxes per pallet is not set. \r\n", "Error", MessageBoxIcon.Error);
                        Close();
                    }

                    lbl_Qty.Text = $@"{count_pcb} / {OrderQty}";
                    lbl_BoxQty.Text = $@"{count_noBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $@"{count_box} / {_palletQty}";

                    lbl_BoxBarcode.Text = dt_ini.Tables[4].Rows.Count <= 0 ? "-" : dt_ini.Tables[4].Rows[0]["BoxBcd"].ToString();
                    lbl_PalletBarcode.Text = dt_ini.Tables[5].Rows.Count <= 0 ? "-" : dt_ini.Tables[5].Rows[0]["PalletBcd"].ToString();
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("ClearForm", ex.Message);
                    System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
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
                InsertIntoSysLog("ClearForm(2)", ex.Message);
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                                    
            }
        }

        void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (this.serialPort1.BytesToRead > 0)
            {
                string strTmp = this.serialPort1.ReadExisting();
            }

            InvokeProcessControls_Dio("");
        }

        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string strScanner = serialPort2.ReadLine();

            if (boolIgnoreDataFromSerialPort2)
            {
                SetLogMessage("Scanner scan signal received again");
                return;
            }

            boolIgnoreDataFromSerialPort2 = true;
            tmrIgnoreDataFromSerialPort2.Start();

            InvokeProcessControls_1st(strScanner);
        }

        private void SerialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string strScanner = serialPort3.ReadLine();

            if (boolIgnoreDataFromSerialPort3)
            {
                SetLogMessage("Scanner scan signal received again");
                return;
            }

            boolIgnoreDataFromSerialPort3 = true;
            tmrIgnoreDataFromSerialPort3.Start();

            InvokeProcessControls_2nd(strScanner);
        }

        private delegate void ReceivedFrom_DioDelegate(string strDio);

        private delegate void ReceivedFrom_1stScannerDelegate(string strScanner_1st);

        private delegate void ReceivedFrom_2ndScannerDelegate(string strScanner_2nd);

        private void InvokeProcessControls_Dio(string strDio)
        {
            if (InvokeRequired)
            {
                Invoke(new ReceivedFrom_DioDelegate(ReceivedFrom_Dio), strDio);
            }
            else
            {
                ReceivedFrom_Dio(strDio);
            }
        }

        private void InvokeProcessControls_1st(string strScanner_1st)
        {
            if (InvokeRequired)
            {
                Invoke(new ReceivedFrom_1stScannerDelegate(InvokeProcessControls_1st), strScanner_1st);
            }
            else
            {
                ReceivedFrom_1stScanner(Regex.Replace(strScanner_1st, @"[^a-zA-Z0-9]", "", RegexOptions.Singleline));
            }
        }

        private void InvokeProcessControls_2nd(string strScanner_2nd)
        {
            if (InvokeRequired)
            {
                Invoke(new ReceivedFrom_2ndScannerDelegate(InvokeProcessControls_2nd), strScanner_2nd);
            }
            else
            {
                ReceivedFrom_2ndScanner(Regex.Replace(strScanner_2nd, @"[^a-zA-Z0-9]", "", RegexOptions.Singleline));
            }
        }

        private void ReceivedFrom_Dio(string strDio)
        {
            SetLogMessage("Reply from Digital I/O");
            tmrDioReply.Stop();
            boolDioAvailable = true;
        }

        private void ReceivedFrom_1stScanner(string scanData)
        {
            try
            {
                if (btn_Setting.Text != "Setting up")
                    btn_Setting.Enabled = false;

                lbl_Error.Visible = false;

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

                if (lbl_type.Text == "SINGLE")
                {
                    ProcessingSINGLE(scanData);
                }
                else
                {
                    Processing1stDOUBLE(scanData);
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("ReceivedFrom_1stScanner", ex.Message);
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
                                ,@PS_ROUTING	= '{Routing}'
                                ,@PS_WORKORDER	= '{WorkOrder}'
                                ,@PS_MATERIAL	= '{Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{scanData}'";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null
                    || ds1.Tables.Count == 0)
                {
                    string strErr = "Network problem occurred.";

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr;

                    SetLogMessage($"[{scanData}] {strErr}");

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    string strErr = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = strErr;

                    SetLogMessage($"[{scanData}] {strErr}");

                    SetLogMessage($"Send NG signal to Digital I/O");
                    WriteDigitalOut(2, true);
                    throw new Exception(strErr);
                }


                if (ds1.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") // Verify OK
                {
                    SetLogMessage($"{scanData} PCB Verify [OK]");

                    string insertQuery_Pcb = $@"
                                            INSERT
                                              INTO BoxTemp
                                              ( WorkOrder
                                              , WorkCenter
                                              , PcbBcd
                                              , Created
                                              , Updated )
                                            VALUES ( '{WorkOrder}'
                                                   , '{WorkCenter}'
                                                   , '{scanData}'
                                                   , GETDATE()
                                                   , GETDATE() )
                                                       ";
                    DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                    SetLogMessage($"{scanData} DataBase Save");

                    string Query_pcb = $@"
                                SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL
                                ORDER BY RecordId
                          
                                SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' AND BoxBcd IS NULL
                    
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
                                        WHERE WO.WorkOrder = '{WorkOrder}'
                                          ";
                    DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                    dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                    AutoScrollDatagridView();

                    string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                    lbl_Qty.Text = $@"{total_qty} / {OrderQty}";
                    lbl_BoxQty.Text = $@"{ds_pcb.Tables[1].Rows[0]["PcbCnt"]} / {_boxQty}";

                    SetLogMessage($"Send OK signal to Digital I/O");
                    WriteDigitalOut(1, true);

                    VerifyPrint_Box();
                }
                else
                {
                    string strErr = ds1.Tables[0].Rows[0]["RTN_TXT"].ToString();

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
                InsertIntoSysLog("ProcessingSINGLE", ex.Message);
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
                                ,@PS_ROUTING	= '{Routing}'
                                ,@PS_WORKORDER	= '{WorkOrder}'
                                ,@PS_MATERIAL	= '{Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{scanData}'";

                var ds1 = DbAccess.Default.GetDataSet(strCmd);
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
                            WriteDigitalOut(2, true);
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
                    }
                    else // 1  or 2
                    {
                        SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {strErr}");

                        ResetFlagAll();

                        SetLogMessage($"Send NG signal to Digital I/O");
                        WriteDigitalOut(2, true);
                        throw new Exception(strErr);
                    }
                }

                else if (ds1.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") // Verify OK
                {
                    switch (int2ndLabelFlag)
                    {
                        // 2nd에서 처리
                        case 0:
                            str1stLabel = scanData;
                            int1stLabelFlag = 1;
                            return;
                        // OK 실적처리
                        ////////////////this.ResetFlagAll();
                        case 1 when scanData == str2ndLabel:
                        {
                            string strErr = "The 1st barcode and the 2nd barcode are the same.";

                            lbl_Current1.ForeColor = Color.Red;
                            lbl_Current2.ForeColor = Color.Red;
                            lbl_Error.Visible = true;
                            lbl_Error.Text = strErr;

                            ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);

                            return;
                        }
                        case 1:
                        {
                            string insertQuery_Pcb = $@"
                                                    INSERT
                                                      INTO BoxTemp
                                                      ( WorkOrder
                                                      , WorkCenter
                                                      , PcbBcd
                                                      , Created
                                                      , Updated )
                                                    VALUES ( '{WorkOrder}'
                                                           , '{WorkCenter}'
                                                           , '{str2ndLabel}'
                                                           , GETDATE()
                                                           , GETDATE() )
                                                         , ( '{WorkOrder}'
                                                           , '{WorkCenter}'
                                                           , '{scanData}'
                                                           , DATEADD(MILLISECOND, 5, GETDATE())
                                                           , DATEADD(MILLISECOND, 5, GETDATE()) )
                                                   ";
                            DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                            SetLogMessage($"[{scanData},{str2ndLabel}] Database save");

                            string Query_pcb = $@"
                                    SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                    WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL
                                    ORDER BY RecordId
                                                        
                                    SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                    FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' AND BoxBcd IS NULL

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
                                        WHERE WO.WorkOrder = '{WorkOrder}'
                                                     ";
                            DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                            dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                            AutoScrollDatagridView();

                            string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                            lbl_Qty.Text = $@"{total_qty} / {OrderQty}";
                            lbl_BoxQty.Text = $@"{ds_pcb.Tables[1].Rows[0]["PcbCnt"]} / {_boxQty}";

                            VerifyPrint_Box();

                            ResetFlagAll();

                            SetLogMessage($"Send OK signal to Digital I/O");
                            WriteDigitalOut(1, true);
                            break;
                        }
                        // 2nd가 NG이므로 NG처리하고 종료
                        default:
                            lbl_Current1.ForeColor = Color.Red;
                            lbl_Current1.Text = "N/A";
                            lbl_Error.Visible = true;
                            lbl_Error.Text = str1stErrMsg + " / " + str2ndErrMsg;

                            SetLogMessage($"[{scanData}], [{str2ndLabel}] {str1stErrMsg} / {str2ndErrMsg}");

                            ResetFlagAll();

                            SetLogMessage($"Send NG signal to Digital I/O");
                            WriteDigitalOut(2, true);
                            return;
                    }
                }
                else
                {
                    string strErr = ds1.Tables[0].Rows[0]["RTN_TXT"].ToString();

                    lbl_Current1.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = $@"{strErr} / {str2ndErrMsg}";
                    str1stErrMsg = strErr;

                    switch (int2ndLabelFlag)
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
                InsertIntoSysLog("Processing1stDOUBLE", ex.Message);
            }
        }

        private void ReceivedFrom_2ndScanner(string scanData)
        {
            try
            {
                if (btn_Setting.Text != "Setting up")
                    btn_Setting.Enabled = false;

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

                Processing2ndDOUBLE(scanData); // 무조건 DOUBLE만 있음.
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("ReceivedFrom_2ndScanner", ex.Message);
            }
        }

        private void Processing2ndDOUBLE(string scanData)
        {
            if (btn_Setting.Enabled)
                return;
            try
            {
                string strCmd = $@"exec [Sp_WorkPcProcedureV3]
                                @PS_GUBUN		= 'VERIFY_PCB_BARCODE'
                                ,@PS_ROUTING	= '{Routing}'
                                ,@PS_WORKORDER	= '{WorkOrder}'
                                ,@PS_MATERIAL	= '{Material}'
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

                if (ds1.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") // Verify OK
                {
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
                                                    INSERT
                                                      INTO BoxTemp
                                                      ( WorkOrder
                                                      , WorkCenter
                                                      , PcbBcd
                                                      , Created
                                                      , Updated )
                                                    VALUES ( '{WorkOrder}'
                                                           , '{WorkCenter}'
                                                           , '{scanData}'
                                                           , GETDATE()
                                                           , GETDATE() )
                                                         , ( '{WorkOrder}'
                                                           , '{WorkCenter}'
                                                           , '{str1stLabel}'
                                                           , DATEADD(MILLISECOND, 5, GETDATE())
                                                           , DATEADD(MILLISECOND, 5, GETDATE()) )
                                                   ";

                            DbAccess.Default.ExecuteQuery(insertQuery_Pcb);

                            SetLogMessage($"[{str1stLabel},{scanData}] Database save");

                            string Query_pcb = $@"
                                    SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated FROM BoxTemp 
                                    WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL
                                    ORDER BY Updated
                                                        
                                    SELECT COUNT(DISTINCT(PcbBcd)) PcbCnt
                                    FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' AND BoxBcd IS NULL

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
                                        WHERE WO.WorkOrder = '{WorkOrder}'
                                                     ";
                            DataSet ds_pcb = DbAccess.Default.GetDataSet(Query_pcb);
                            dgv_pcbInfo.DataSource = ds_pcb.Tables[0];
                            AutoScrollDatagridView();

                            string total_qty = ds_pcb.Tables[2].Rows[0]["Cnt"].ToString();
                            lbl_Qty.Text = $@"{total_qty} / {OrderQty}";
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
                else
                {
                    string strErr = ds1.Tables[0].Rows[0]["RTN_TXT"].ToString();

                    lbl_Current2.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = str1stErrMsg + " / " + strErr;
                    str2ndErrMsg = strErr;

                    if (int1stLabelFlag == 0) // 1st에서 처리
                    {
                        str2ndLabel = scanData;
                        int2ndLabelFlag = 2;
                    }
                    else if (int1stLabelFlag == 1)
                    {
                        SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {str2ndErrMsg}");

                        ResetFlagAll();

                        lbl_Current1.Text = "N/A";
                        lbl_Current1.ForeColor = Color.Red;

                        SetLogMessage($"Send NG signal to Digital I/O");
                        WriteDigitalOut(2, true);
                        return;
                    }
                    else
                    {
                        SetLogMessage($"[{str1stLabel}], [{scanData}] {str1stErrMsg} / {str2ndErrMsg}");

                        ResetFlagAll();

                        SetLogMessage($"Send NG signal to Digital I/O");
                        WriteDigitalOut(2, true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                this.InsertIntoSysLog("Processing2ndDOUBLE", ex.Message);
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
                InsertIntoSysLog("WriteDigitalOut", ex.Message);
            }
        }

        private void VerifyPrint_Box()
        {
            try
            {
                string boxQ = $"select count(*) Count from BoxTemp where BoxBcd IS NULL and WorkCenter = '{WorkCenter}' and WorkOrder = '{WorkOrder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();

                if (int.Parse(currentQty) < int.Parse(_boxQty)) return;
                Print("Boxing", _boxQty);
                Boxing(_boxBcd, _boxQty);
                try
                {
                    var query =
                        $@"
                        SELECT COUNT(DISTINCT (BoxBcd)) AS Cnt
                          FROM BoxTemp
                         WHERE WorkCenter = '{WorkCenter}'
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
                                    WHERE WorkCenter = '{WorkCenter}'
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
                         WHERE WorkCenter = '{WorkCenter}'
                           AND Boxbcd IS NULL
                         ORDER BY RecordId
                        ;
                        ";
                    var dataSet = DbAccess.Default.GetDataSet(query);

                    var boxCount = dataSet.Tables[0].Rows[0]["Cnt"].ToString();
                    var nonBoxingPcb = dataSet.Tables[2].Rows.Count.ToString();

                    lbl_BoxQty.Text = $@"{nonBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_BoxBarcode.Text = $@"{_boxBcd}";
                    dgv_boxInfo.DataSource = dataSet.Tables[1];
                    dgv_pcbInfo.DataSource = dataSet.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("VerifyPrint_Box", ex.Message);
                }

                VerifyPrint_Pallet();
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("VerifyPrint_Box(2)", ex.Message);
            }
        }

        private void VerifyPrint_Pallet()
        {
            try
            {
                string boxQ = $"select COUNT(DISTINCT(BoxBcd)) Count from BoxTemp where PalletBcd IS NULL and Boxbcd is not null and WorkCenter = '{WorkCenter}' and WorkOrder = '{WorkOrder}'";
                DataTable dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();

                if (_palletQty != currentQty) return;
                Print("Palletizing", currentQty, false);

                Palletizing(_palletBcd, _palletQty);
                try
                {
                    string query =
                        $@"
                        SELECT COUNT(DISTINCT (BoxBcd)) AS BoxCnt
                          FROM BoxTemp
                         WHERE WorkCenter = '{WorkCenter}'
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
                                    WHERE WorkCenter = '{WorkCenter}'
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
                                    WHERE WorkCenter = '{WorkCenter}'
                                      AND PalletBcd IS NULL
                                      AND BoxBcd IS NOT NULL
                                    GROUP BY BT.BoxBcd
                               ) AS BT
                         ORDER BY BT.Updated
                        ;
                        ";
                    var dataSet = DbAccess.Default.GetDataSet(query);

                    var boxCount = dataSet.Tables[0].Rows[0]["BoxCnt"].ToString();
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_PalletBarcode.Text = $@"{_palletBcd}";
                    dgv_palletInfo.DataSource = dataSet.Tables[1];
                    dgv_boxInfo.DataSource = dataSet.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("VerifyPrint_Pallet", ex.Message);
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("VerifyPrint_Pallet(2)", ex.Message);
            }
        }

        private bool Boxing(string barcode, string qty)
        {
            try
            {
                string query = $@" 
                                BEGIN TRY
                                    BEGIN TRAN
                                        DECLARE @Now DATETIME = GETDATE();
                                        UPDATE BoxTemp
                                           SET BoxBcd  = '{barcode}'
                                             , Updated = @Now
                                          FROM (
                                                   SELECT TOP ({qty}) PcbBcd
                                                     FROM BoxTemp
                                                    WHERE WorkCenter = '{WorkCenter}'
                                                      AND BoxBcd IS NULL
                                                    ORDER BY RecordId
                                               ) BT
                                         WHERE BoxTemp.PcbBcd = BT.PcbBcd

                                        UPDATE KeyRelation
                                           SET Box_WorkOrder = '{WorkOrder}'
                                             , Box_Material  = '{Material}'
                                             , Box_Line      = '{WorkCenter}'
                                             , Boxed         = CONVERT(VARCHAR, @Now, 20)
                                             , BoxBcd        = '{barcode}'
                                             , Updated       = @Now
                                             , UpdateUser    = '{WiseApp.Id}'
                                          FROM (
                                                   SELECT PcbBcd
                                                     FROM BoxTemp
                                                    WHERE BoxBcd = '{barcode}'
                                                      AND WorkOrder = '{WorkOrder}'
                                               ) BT
                                         WHERE KeyRelation.PcbBcd = BT.PcbBcd

                                        UPDATE KeyRelation
                                           SET BoxBcd     = '{barcode}'
                                             , Updated    = @Now
                                             , UpdateUser = '{WiseApp.Id}'
                                          FROM (
                                                   SELECT PcbBcd
                                                     FROM BoxTemp
                                                    WHERE BoxBcd = '{barcode}'
                                                      AND WorkOrder <> '{WorkOrder}'
                                               ) BT
                                         WHERE KeyRelation.PcbBcd = BT.PcbBcd
                                    COMMIT TRAN
                                END TRY
                                BEGIN CATCH
                                    ROLLBACK TRAN
                                    --Log Insert
                                    INSERT
                                      INTO SysLog ( [Type]
                                                  , Category
                                                  , [Source]
                                                  , [Message]
                                                  , [User]
                                                  , Updated )
                                    VALUES ( 'E'
                                           , 'SP'
                                           , 'Box_Palletizing.Boxing'
                                           , LEFT(ISNULL(ERROR_MESSAGE(), ''), 4000)
                                           , 'Box_Palletizing.Boxing'
                                           , @Now )
                                END CATCH

                                       ";
                DbAccess.Default.ExecuteQuery(query);
                return true;
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Box_Palletizing.Boxing', N'{ex.Message}', '{WorkCenter}', GETDATE())");
                return false;
            }
        }

        private void Palletizing(string barcode, string qty)
        {
            try
            {
                string query =
                    $@"
                    BEGIN TRY
                        DECLARE @Now DATETIME = GETDATE();
                        BEGIN TRAN
                            UPDATE BoxTemp
                               SET PalletBcd = '{barcode}'
                                 , Updated   = @Now
                              FROM (
                                       SELECT PcbBcd
                                         FROM BoxTemp
                                        WHERE WorkCenter = '{WorkCenter}'
                                          AND PalletBcd IS NULL
                                          AND BoxBcd IS NOT NULL
                                   ) BT
                             WHERE BoxTemp.PcbBcd = BT.PcbBcd
                            UPDATE KeyRelation
                               SET PalletBcd  = '{barcode}'
                                 , Palletized = CONVERT(VARCHAR, @Now, 20)
                                 , Updated    = @Now
                                 , UpdateUser = '{WiseApp.Id}'
                              FROM (
                                       SELECT PcbBcd
                                         FROM BoxTemp
                                        WHERE PalletBcd = '{barcode}'
                                   ) BT
                             WHERE KeyRelation.PcbBcd = BT.PcbBcd
                            INSERT
                              INTO Packing
                              ( PcbBcd
                              , BoxBcd
                              , PalletBcd
                              , WorkOrder
                              , Material
                              , Created
                              , Updated )
                            SELECT PcbBcd
                                 , BoxBcd
                                 , PalletBcd
                                 , WorkOrder
                                 , '{Material}'
                                 , @Now
                                 , @Now
                              FROM BoxTemp
                             WHERE WorkCenter = '{WorkCenter}'
                               AND PalletBcd = '{barcode}'

                            INSERT
                              INTO PackingHist ( IoType
                                               , PcbBcd
                                               , BoxBcd
                                               , PalletBcd
                                               , Material
                                               , Created
                                               , Updated
                                               , WorkOrder )
                            SELECT 'IN'
                                 , PcbBcd
                                 , BoxBcd
                                 , PalletBcd
                                 , '{Material}'
                                 , @Now
                                 , @Now
                                 , WorkOrder
                              FROM BoxTemp
                             WHERE WorkCenter = '{WorkCenter}'
                               AND PalletBcd = '{barcode}'
                        COMMIT TRAN
                    END TRY
                    BEGIN CATCH
                        ROLLBACK TRAN
                        --Log Insert
                        INSERT
                          INTO SysLog ( [Type]
                                      , Category
                                      , [Source]
                                      , [Message]
                                      , [User]
                                      , Updated )
                        VALUES ( 'E'
                               , 'SP'
                               , 'Box_Palletizing.Palletizing'
                               , LEFT(ISNULL(ERROR_MESSAGE(), ''), 4000)
                               , 'Box_Palletizing.Palletizing'
                               , @Now )
                    END CATCH
                    ";
                DbAccess.Default.ExecuteQuery(query);
                var pcbQty = DbAccess.Default.IsExist("BoxTemp", $@"PalletBcd = '{barcode}' AND WorkOrder = '{WorkOrder}'");
                Good(pcbQty);
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Palletizing", ex.Message);
            }
        }

        public void Good(int count)
        {
            try
            {
                var r = new EntryRequest();
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
                InsertIntoSysLog("Good", ex.Message);
            }
        }

        private void Print(string printType, string currentQty, bool isPrint = true)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Print(printType, currentQty); }));
            }
            else
            {
                try
                {
                     
                    var PNdt = DbAccess.Default.GetDataTable($@"SELECT LG_ITEM_CD, LG_ITEM_NM, Text FROM Material WHERE Material = '{_topMaterial}'");
                    var item_CD = PNdt.Rows[0]["LG_ITEM_CD"].ToString();
                    var item_NM = PNdt.Rows[0]["LG_ITEM_NM"].ToString();
                    var desc = PNdt.Rows[0]["Text"].ToString();
                    var Q = $"EXEC SP_Convert_Date '{PackingDate:yyyy-MM-dd}'";
                    var ymd = DbAccess.Default.ExecuteScalar(Q) as string;
                    string line = WorkCenter.Substring(4, 1);

                    //gmryu 2023-10-10 일반라인 / 영문 라인 구분
                    char workOrderLine = WorkOrder[WorkOrder.Length - 3];
                    if (!char.IsDigit(workOrderLine))
                    {
                        line = workOrderLine.ToString();
                    }
                    //gmryu 2024-02-20 임시
                    if (line.Equals("3"))
                    {
                        line = "1";
                    }

                    switch (printType)
                    {
                        case "Boxing":
                        {
                            var qty = int.Parse(currentQty);

                            var tempqty1 = $"{qty:000#}";
                            var tempqty2 = $"{qty:0#}";
                            var boxbcd1 = item_CD + tempqty1;

                            var seqQ = $@"SELECT TOP 1 BoxBarcode_2 FROM BoxBcdPrintHist WITH(NOLOCK) WHERE BoxBarcode_2 like '%{ymd + line + item_CD.Substring(3, 8)}%' AND ProductionLine <> 'RePacking' ORDER BY SerialNo DESC";

                            int seq;
                            var dtseq = DbAccess.Default.GetDataTable(seqQ);
                            if (dtseq.Rows.Count <= 0) seq = 1;
                            else seq = int.Parse(dtseq.Rows[0]["BoxBarcode_2"].ToString().Substring(15, 4)) + 1;
                            var tempseq = $"{seq:000#}";

                            _boxBcd = "B" + tempqty2 + ymd + line + item_CD.Substring(3, 8) + tempseq;

                            var strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                            var dataTable = DbAccess.Default.GetDataTable(strSql);

                            var _clsBarcode = new clsBarcode.clsBarcode();
                            _clsBarcode.LoadFromXml(dataTable.Rows[0][0].ToString());

                            _clsBarcode.Data.SetText("PARTNO", item_CD);
                            _clsBarcode.Data.SetText("QTY", qty + " EA");
                            _clsBarcode.Data.SetText("DESC", desc);
                            _clsBarcode.Data.SetText("SPEC", item_NM);

                                //gmryu 2023-08-31 홍팀장님 요청 : S라인일 경우 박스라벨에 (VN) 미표기                  
                                _clsBarcode.Data.SetText("Y2SOLUTION", $"Y2 SOLUTION{(char.IsDigit(workOrderLine) ? "(VN)" : "")}");

                                _clsBarcode.Data.SetText("DATE", $"{PackingDate:yyyy. MM. dd}");
                            _clsBarcode.Data.SetText("BARCODE1", boxbcd1);
                            _clsBarcode.Data.SetText("BARCODE2", _boxBcd);

                            _clsBarcode.Print(false);

                            var queryInsertPrintHist = $@"
                                                INSERT
                                                  INTO BoxbcdPrintHist
                                                  ( BoxBarcode_1
                                                  , BoxBarcode_2
                                                  , LG_PartNo
                                                  , Spec
                                                  , Description
                                                  , ProductionDate
                                                  , ProductionLine
                                                  , Material
                                                  , Qty
                                                  , Mfg_Line
                                                  , Mfg_ymd
                                                  , SerialNo
                                                  , Reprint
                                                  , Updated
                                                  , Updater )
                                                VALUES ( '{boxbcd1}'
                                                       , '{_boxBcd}'
                                                       , '{item_CD}'
                                                       , '{item_NM}'
                                                       , '{desc}'
                                                       , '{PackingDate:yyyy-MM-dd}'
                                                       , '{WorkCenter}'
                                                       , '{Material}'
                                                       , '{qty}'
                                                       , '{line}'
                                                       , '{ymd}'
                                                       , '{tempseq}'
                                                       , 0
                                                       , GETDATE()
                                                       , '{WorkCenter}' )
                                                    ";
                            DbAccess.Default.ExecuteQuery(queryInsertPrintHist);

                            lbl_BoxBarcode.Text = _boxBcd;
                            break;
                        }
                        case "Palletizing":
                        {
                            var qty_dt = DbAccess.Default.GetDataTable($@"select count(PcbBcd) from BoxTemp where PalletBcd is null AND BoxBcd is not null and WorkOrder = '{WorkOrder}'");
                            var qty = int.Parse(qty_dt.Rows[0][0].ToString());
                            var tempqty = $"{qty:000#}";

                            var seqQ = $@"SELECT TOP 1 PalletBcd FROM PalletbcdPrintHist WITH(NOLOCK) WHERE PalletBcd LIKE '%{ymd + line + item_CD.Substring(3, 8)}%' ORDER BY SerialNo DESC";
                            var dtseq = DbAccess.Default.GetDataTable(seqQ);

                            int seq;
                            if (dtseq.Rows.Count <= 0) seq = 1;
                            else seq = int.Parse(dtseq.Rows[0]["PalletBcd"].ToString().Substring(17, 2)) + 1;
                            var tempseq = $"{seq:0#}";

                            _palletBcd = "P" + tempqty + ymd + line + item_CD.Substring(3, 8) + tempseq;
                            if (isPrint)
                            {
                                string strSql = @"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
                                DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                                _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                                _clsBarcode.Data.SetText("PARTNO", item_CD);
                                _clsBarcode.Data.SetText("MODEL", item_NM);
                                _clsBarcode.Data.SetText("QTY", qty + " EA");
                                _clsBarcode.Data.SetText("PALLETBCD", _palletBcd);

                                _clsBarcode.Print(false);
                            }
                            string p_printHistQ = $@"
                                            INSERT INTO PalletbcdPrintHist
                                            (LG_PartNo, Model, Qty, PalletBcd, Mfg_ymd, Mfg_Line, ProductionDate, ProductionLine,
                                            Material, SerialNo, Reprint, Updated, Updater)
                                            VALUES
                                            ('{item_CD}','{item_NM}','{qty}','{_palletBcd}','{ymd}','{line}', '{PackingDate:yyyy-MM-dd}', '{WorkCenter}',
                                            '{Material}', '{tempseq}', 0, GETDATE(), '{WorkCenter}')
                                            ";
                            DbAccess.Default.ExecuteQuery(p_printHistQ);
                            lbl_PalletBarcode.Text = _palletBcd;
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Print", ex.Message);
                }
            }
        }

        private void btn_testPrint_Box_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql;
                DataTable dtMain = new DataTable();

                strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";
                dtMain = DbAccess.Default.GetDataTable(strSql);

                var _clsBarcode = new clsBarcode.clsBarcode();
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
                InsertIntoSysLog("btn_testPrint_Box_Click", ex.Message);
            }
        }

        private void btn_testPrint_Pallet_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = @"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
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
                InsertIntoSysLog("btn_testPrint_Pallet_Click", ex.Message);
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

            if (dgv_palletInfo.Rows.Count > 0)
            {
                dgv_palletInfo.FirstDisplayedScrollingRowIndex = dgv_palletInfo.Rows.Count - 1;
                dgv_palletInfo.Rows[dgv_palletInfo.Rows.Count - 1].Selected = true;
            }
        }

        private void SetLogMessage(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message} \r\n";

            string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\Log\Box_Palletizing";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            var fileInfo = new FileInfo(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt");

            if (!fileInfo.Exists)
            {
                File.AppendAllText(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt", logMessage, Encoding.Default);
            }

            File.AppendAllText(path + $@"\{DateTime.Now:yyyy-MM-dd}.txt", logMessage, Encoding.Default);
        }

        private void InsertIntoSysLog(string strMsg)
        {
            InsertIntoSysLog("WorkPC", strMsg);
        }

        private void InsertIntoSysLog(string source, string strMsg)
        {
            InsertIntoSysLog("E", source, strMsg);
        }

        private void InsertIntoSysLog(string type, string source, string strMsg)
            
        {
            strMsg = strMsg.Replace("'", "\x07"); //일부러? 
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('{type}',  'Client', 'Box_Palletizing.{source}', LEFT(ISNULL(N'{strMsg}',''),3000), '{WorkCenter}', GETDATE())");
        }

        #region Remainder_Closing

        private void btn_boxing_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_boxing_Click(sender, e); }));
            }
            else
            {
                var thread = new Thread(Process_threads_Boxing);
                thread.Start();
            }
        }

        private void btn_palletizing_Click(object sender, EventArgs e)
        {
            var thread = new Thread(Process_threads_Palletizing);
            thread.Start();
        }

        private void Process_threads_Boxing()
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { Process_threads_Boxing(); }));
            }
            else
            {
                string boxQ = $"select count(*) Count from BoxTemp where BoxBcd IS NULL and WorkCenter = '{WorkCenter}' and WorkOrder = '{WorkOrder}'";
                var dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                string currentQty = dt_boxQ.Rows[0]["Count"].ToString();
                if (currentQty == "0")
                {
                    System.Windows.Forms.MessageBox.Show("Không có PCB There is no PCB");
                    return;
                }

                if (System.Windows.Forms.MessageBox.Show
                        (
                         "Bạn có chắc chắn không？ \r\nAre you sure?", "Đóng những hộp còn lại(Box Remainder Closing)",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        )
                    != DialogResult.Yes) return;
                Print("Boxing", currentQty);

                Boxing(_boxBcd, currentQty);

                try
                {
                    string Query_box = $@"
                                        SELECT COUNT(DISTINCT(BoxBcd)) Cnt FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' AND BoxBcd IS NOT NULL AND PalletBcd IS NULL

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt, BT.BoxBcd, BT.Updated
                                        FROM (SELECT BT.BoxBcd, MAX(BT.Updated) AS Updated FROM boxtemp BT WHERE WorkCenter = '{WorkCenter}' AND PalletBcd IS NULL AND BoxBcd IS NOT NULL
                                        GROUP BY BT.BoxBcd) AS BT ORDER BY BT.Updated

                                        SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated
                                        FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL ORDER BY RecordId
                                             ";
                    var ds_box = DbAccess.Default.GetDataSet(Query_box);

                    var boxCount = ds_box.Tables[0].Rows[0]["Cnt"].ToString();
                    string nonBoxingPcb = ds_box.Tables[2].Rows.Count.ToString();

                    lbl_BoxQty.Text = $@"{nonBoxingPcb} / {_boxQty}";
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_BoxBarcode.Text = $@"{_boxBcd}";
                    dgv_boxInfo.DataSource = ds_box.Tables[1];
                    dgv_pcbInfo.DataSource = ds_box.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Process_threads_Boxing", ex.Message);
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
                var boxQ = $"select COUNT(DISTINCT(BoxBcd)) Count from BoxTemp where PalletBcd IS NULL and Boxbcd is not null and WorkCenter = '{WorkCenter}' and WorkOrder = '{WorkOrder}'";
                var dt_boxQ = DbAccess.Default.GetDataTable(boxQ);
                var currentQty = dt_boxQ.Rows[0]["Count"].ToString();
                if (currentQty == "0")
                {
                    System.Windows.Forms.MessageBox.Show("Không có Box There is no Box");
                    return;
                }

                var check_nonboxingQ = $@"SELECT PcbBcd FROM BoxTemp WHERE BoxBcd IS NULL AND WorkCenter = '{WorkCenter}' AND WorkOrder = '{WorkOrder}'";
                var dt_nonbox = DbAccess.Default.GetDataTable(check_nonboxingQ);
                if (dt_nonbox.Rows.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Có PCB chưa được đóng Box There are Non-boxed PCBs");
                    return;
                }

                if (System.Windows.Forms.MessageBox.Show
                        (
                         "Bạn có chắc chắn không？ \r\nAre you sure?", "Đóng những hộp còn lại(Pallet Remainder Closing)",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        )
                    != DialogResult.Yes) return;
                Print("Palletizing", currentQty);
                Palletizing(_palletBcd, currentQty);
                try
                {
                    var Query_pallet = $@"
                                        SELECT COUNT(DISTINCT (BoxBcd)) AS BoxCnt
                                          FROM BoxTemp
                                         WHERE WorkCenter = '{WorkCenter}'
                                           AND PalletBcd IS NULL
                                           AND BoxBcd IS NOT NULL

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt
                                             , BT.PalletBcd
                                             , BT.Updated
                                          FROM (
                                                   SELECT BT.PalletBcd
                                                        , MAX(BT.Updated) AS Updated
                                                     FROM BoxTemp BT
                                                    WHERE WorkCenter = '{WorkCenter}'
                                                      AND PalletBcd IS NOT NULL
                                                    GROUP BY BT.PalletBcd
                                               ) AS BT
                                         ORDER BY BT.Updated

                                        SELECT ROW_NUMBER() OVER (ORDER BY BT.Updated) AS Cnt
                                             , BT.BoxBcd
                                             , BT.Updated
                                          FROM (
                                                   SELECT BT.BoxBcd
                                                        , MAX(BT.Updated) AS Updated
                                                     FROM BoxTemp BT
                                                    WHERE WorkCenter = '{WorkCenter}'
                                                      AND PalletBcd IS NULL
                                                      AND BoxBcd IS NOT NULL
                                                    GROUP BY BT.BoxBcd
                                               ) AS BT
                                         ORDER BY BT.Updated
                                            ";
                    var ds_pallet = DbAccess.Default.GetDataSet(Query_pallet);

                    var boxCount = ds_pallet.Tables[0].Rows[0]["BoxCnt"].ToString();
                    lbl_PalletQty.Text = $@"{boxCount} / {_palletQty}";
                    lbl_PalletBarcode.Text = $@"{_palletBcd}";
                    dgv_palletInfo.DataSource = ds_pallet.Tables[1];
                    dgv_boxInfo.DataSource = ds_pallet.Tables[2];
                    AutoScrollDatagridView();
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Process_threads_Palletizing", ex.Message);
                }
            }
        }

        #endregion

        private void Box_Palletizing_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tmrDio1.Dispose();
                tmrDio2.Dispose();
                tmrDioReply.Dispose();

                tmrIgnoreDataFromSerialPort2.Dispose();
                tmrIgnoreDataFromSerialPort3.Dispose();

                serialPort1.Close();
                serialPort2.Close();
                serialPort3.Close();
            }
            catch (Exception ex)
            {
                Close();
            }
        }


        private void btn_Clear_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Process_threads_Clear);
            thread.Start();
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
                         "Bạn có chắc chắn không？ \r\nAre you sure?", "Clear",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question
                        )
                    != DialogResult.Yes) return;

                string del_Q = $@"DELETE FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' and BoxBcd IS NULL";
                DbAccess.Default.ExecuteQuery(del_Q);

                string Q = $@"
                    SELECT ROW_NUMBER() OVER(ORDER BY(SELECT NULL)) Cnt, PcbBcd as PCB_Barcode, Updated 
                    FROM BoxTemp WHERE WorkCenter = '{WorkCenter}' and Boxbcd IS NULL ORDER BY RecordId

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
                    WHERE WO.WorkOrder = '{WorkOrder}'
                         ";
                var ds_Q = DbAccess.Default.GetDataSet(Q);
                dgv_pcbInfo.DataSource = ds_Q.Tables[0];

                var count_pcb = ds_Q.Tables[1].Rows[0]["Cnt"].ToString();
                var count_nonBoxingPcb = ds_Q.Tables[0].Rows.Count.ToString();

                lbl_Qty.Text = $@"{count_pcb} / {OrderQty}";
                lbl_BoxQty.Text = $@"{count_nonBoxingPcb} / {_boxQty}";
            }
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

                ResetFlagAll();
                lbl_Current1.Text = string.Empty;
                lbl_Current2.Text = string.Empty;
            }
        }

        private void btn_BoxReprint_Click(object sender, EventArgs e)
        {
            var thread = new Thread(Process_threads_Reprint_Box);
            thread.Start();
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
                             "Bạn có chắc chắn không？ \r\nAre you sure?", "Reprint",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question
                            )
                        != DialogResult.Yes) return;
                    string query = $@"SELECT TOP 1 * FROM BoxbcdPrintHist WHERE BoxBarcode_2 = '{lbl_BoxBarcode.Text}' ORDER BY SerialNo DESC";
                    var dataTable = DbAccess.Default.GetDataTable(query);

                    string boxbarcode1 = dataTable.Rows[0]["BoxBarcode_1"].ToString();
                    string LG_PartNo = dataTable.Rows[0]["LG_PartNo"].ToString();
                    string spec = dataTable.Rows[0]["Spec"].ToString();
                    string desc = dataTable.Rows[0]["Description"].ToString();
                    string strQty = dataTable.Rows[0]["Qty"].ToString();
                    string productionDate = dataTable.Rows[0]["ProductionDate"].ToString();

                    string tempqty = $"{strQty:0#}";
                    var bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'");
                    var clsBarcode = new clsBarcode.clsBarcode();
                    clsBarcode.LoadFromXml(bcdData.ToString());

                    clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                    clsBarcode.Data.SetText("QTY", tempqty + " EA");
                    clsBarcode.Data.SetText("DESC", desc);
                    clsBarcode.Data.SetText("SPEC", spec);
                    clsBarcode.Data.SetText("DATE", productionDate.Substring(0, 4) + ". " + productionDate.Substring(5, 2) + ". " + productionDate.Substring(8, 2));
                    clsBarcode.Data.SetText("BARCODE1", boxbarcode1);
                    clsBarcode.Data.SetText("BARCODE2", lbl_BoxBarcode.Text);

                    clsBarcode.Print(false);

                    string queryInsertBoxPrintHist =
                        $@"
                        INSERT
                          INTO BoxbcdPrintHist
                          ( BoxBarcode_1
                          , BoxBarcode_2
                          , LG_PartNo
                          , Spec
                          , Description
                          , ProductionDate
                          , ProductionLine
                          , Material
                          , Qty
                          , Mfg_Line
                          , Mfg_ymd
                          , SerialNo
                          , Reprint
                          , Updated
                          , Updater )
                        SELECT TOP 1 BoxBarcode_1
                                   , BoxBarcode_2
                                   , LG_PartNo
                                   , Spec
                                   , Description
                                   , ProductionDate
                                   , ProductionLine
                                   , Material
                                   , Qty
                                   , Mfg_Line
                                   , Mfg_ymd
                                   , SerialNo
                                   , Reprint + 1
                                   , GETDATE()
                                   , '{WorkCenter}'
                          FROM BoxbcdPrintHist WITH (NOLOCK)
                         WHERE BoxBarcode_2 = '{lbl_BoxBarcode.Text}'
                         ORDER BY SerialNo DESC
                        ";
                    DbAccess.Default.ExecuteQuery(queryInsertBoxPrintHist);
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Process_threads_Reprint_Box", ex.Message);
                }
            }
        }

        private void button_PalletReprint_Click(object sender, EventArgs e)
        {
            var thread = new Thread(Process_threads_Reprint_Pallet);
            thread.Start();
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
                             "Bạn có chắc chắn không？ \r\nAre you sure?", "Reprint",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question
                            )
                        != DialogResult.Yes) return;
                    string query = $@"SELECT TOP 1 * FROM PalletbcdPrintHist WITH(NOLOCK) WHERE PalletBcd = '{lbl_PalletBarcode.Text}' ORDER BY SerialNo DESC";
                    var dataTable = DbAccess.Default.GetDataTable(query);

                    string LG_PartNo = dataTable.Rows[0]["LG_PartNo"].ToString();
                    string model = dataTable.Rows[0]["Model"].ToString();
                    string strQty = dataTable.Rows[0]["Qty"].ToString();

                    string strSql = @"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
                    DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                    var _clsBarcode = new clsBarcode.clsBarcode();
                    _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                    _clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                    _clsBarcode.Data.SetText("MODEL", model);
                    _clsBarcode.Data.SetText("QTY", strQty + " EA");
                    _clsBarcode.Data.SetText("PALLETBCD", lbl_PalletBarcode.Text);

                    _clsBarcode.Print(false);

                    string queryInsertPalletPrintHist =
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
                                   , '{WorkCenter}'
                          FROM PalletbcdPrintHist WITH (NOLOCK)
                         WHERE PalletBcd = '{lbl_PalletBarcode.Text}'
                         ORDER BY SerialNo DESC
                        ";
                    DbAccess.Default.ExecuteQuery(queryInsertPalletPrintHist);
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Process_threads_Reprint_Pallet", ex.Message);
                }
            }
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
                    if (System.Windows.Forms.MessageBox.Show("Bạn có chắc muốn hoàn tất cài đặt không? \r\nAre you sure you want to finish setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    lbl_Qty.ForeColor = Color.Black;
                    lbl_Qty.Text = $@"{ini_Qty} / {OrderQty}";
                    btn_Setting.Text = "设置\r\n (Không xử lý dữ liệu)\r\nSetting\r\n(No data \r\nprocessing)";

                    lbl_Current1.Text = string.Empty;
                    lbl_Current2.Text = string.Empty;
                    lbl_prev1.Text = string.Empty;
                    lbl_prev2.Text = string.Empty;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show("Bạn có chắc muốn bắt đầu cài đặt không?？\r\nAre you sure you want to start setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    btn_Setting.Text = $@"Setting up";
                    lbl_Qty.ForeColor = Color.Red;
                    lbl_Qty.Text = "Setting...";
                }
            }
        }
    }
}
