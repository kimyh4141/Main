using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using WiseM.AppService;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Client
{
    public partial class Process_Management_V2 : SkinForm
    {
        #region Field

        // lock
        private readonly object _lockObject = new object();

        string _topMaterial;

        DataTable dt1 = new DataTable();
        DataTable dtQ = new DataTable();

        string ini_Qty;
        private string _orderQty;
        private string _kind;

        private SerialPort serialPort1 = new SerialPort(); // for Digital IO
        private SerialPort serialPort2 = new SerialPort(); // for AutoScanner #1
        private SerialPort serialPort3 = new SerialPort(); // for AutoScanner #2

        private byte bSlaveId;
        private string strComPortName;
        private string singlePort;
        private string leftPort;
        private string rightPort;
        private string _thresholdQty;

        private byte[] bRcv = new byte[999];
        private int intRcvBytes = 0;
        bool DIOCheck = true;

        bool boolDio3Flag = true; // DIO 3번 접점 FLAG

        /// <summary>
        /// DIO로 명령을 보낼 수 있는 플래그
        /// </summary>
        bool boolDioAvailable = true;

        // DIO로 명령 보낸후 false,
        // DIO에서 응답이 오거나, TimeOut 걸리면 true
        bool reelCheckFlag = false;
        private bool _lineStopFlag;


        System.Timers.Timer tmrDio1 = new System.Timers.Timer { AutoReset = false, Interval = 1000 }; // OK 접점 꺼주기 위해
        System.Timers.Timer tmrDio2 = new System.Timers.Timer { AutoReset = false, Interval = 1000 }; // NG 접점 꺼주기 위해
        System.Timers.Timer tmrDioReply = new System.Timers.Timer { AutoReset = false, Interval = 1000 }; // DIO가 응답을 안 할 수 있으니..
        System.Timers.Timer tmrCheckReel = new System.Timers.Timer { AutoReset = false, Interval = 1500 }; // Reel 잔량 체크 
        System.Timers.Timer tmrCheckReel_NG = new System.Timers.Timer { AutoReset = false, Interval = 10000 }; // Reel 잔량 체크 

        /// <summary>
        /// 오토스캐너로 부터 데이터 수신 후, 일정시간 내에 수신되는 데이터 무시
        /// </summary>
        private System.Timers.Timer _tmrIgnoreDataFromSerialPort2 = new System.Timers.Timer { AutoReset = false, Interval = 3000 };

        private System.Timers.Timer _tmrIgnoreDataFromSerialPort3 = new System.Timers.Timer { AutoReset = false, Interval = 3000 };

        /// <summary>
        /// true:IgnoreTimer가 가동중이므로, 이때 수신되는 데이터 무시.
        /// </summary>
        private bool _boolIgnoreDataFromSerialPort2;

        private bool _boolIgnoreDataFromSerialPort3;

        int int1stLabelFlag = 0; // 0:Init  1:OK  2:NG
        int int2ndLabelFlag = 0;

        string str1stLabel = ""; // 1st Scanner 값
        string str2ndLabel = "";

        string str1stErrMsg = ""; // 1st Scanner 에러메세지
        string str2ndErrMsg = "";

        private delegate void ReceivedFrom_DioDelegate(string strDio);

        private delegate void ReceivedFrom_1stScannerDelegate(string strScanner_1st);

        private delegate void ReceivedFrom_2ndScannerDelegate(string strScanner_2nd);

        #endregion

        #region Constructor

        public Process_Management_V2(bool lineStopFlag)
        {
            InitializeComponent();

            this._lineStopFlag = lineStopFlag;

            try
            {
                string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";

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
                    WritePrivateProfileString("Setting", "ThresholdQty", "0", path + @"\Config.ini");
                }

                var tempStrComPortName1 = new StringBuilder();
                var tempStrComPortName2 = new StringBuilder();
                var tempStrComPortName3 = new StringBuilder();
                var tempStrComPortName4 = new StringBuilder();
                var tempStrQty = new StringBuilder();

                GetPrivateProfileString("Setting", "Digital I/O", "", tempStrComPortName1, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "SINGLE", "", tempStrComPortName2, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "Left", "", tempStrComPortName3, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "Right", "", tempStrComPortName4, 256, path + @"\Config.ini");
                GetPrivateProfileString("Setting", "ThresholdQty", "", tempStrQty, 256, path + @"\Config.ini");

                bSlaveId = (byte)(1);
                strComPortName = tempStrComPortName1.ToString();
                singlePort = tempStrComPortName2.ToString();
                leftPort = tempStrComPortName3.ToString();
                rightPort = tempStrComPortName4.ToString();
                _thresholdQty = tempStrQty.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            Init();
        }

        #endregion

        #region Method

        #region Extern Method

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion

        private void Init()
        {
            try
            {
                lbl_workorder.Text = string.Empty;
                lbl_item.Text = string.Empty;
                lbl_Qty.Text = @"0 / 0";
                lbl_type.Text = string.Empty;
                dtQ.Rows.Clear();
                lbl_Error.Visible = false;
                lbl_Reel.Text = $@"Feeder核对中… Feeder Checking .. (Feeder临界数量 : {_thresholdQty})";

                lbl_workorder.Text = WbtCustomService.ActiveValues.WorkOrder;

                var dataSet = DbAccess.Default.GetDataSet($@"EXEC [Sp_WorkPcProcedureV3] @PS_GUBUN = 'GET_TOPMOST_MATERIAL', @PS_MATERIAL = '{WbtCustomService.ActiveValues.Material}';");
                if (dataSet == null
                    || dataSet.Tables.Count == 0)
                {
                    throw new Exception("Network problem occurred.");
                }

                if (dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    System.Windows.Forms.MessageBox.Show(dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                _topMaterial = dataSet.Tables[0].Rows[0]["ITEM_CD"].ToString();

                var query = new StringBuilder();
                query.AppendLine
                (
                    $@"
                    SELECT TOP 1 M.Material
                               , M.Spec
                               , M.Type
                               , WO.OrderQty
                               , COALESCE(OH.OutQty, 0) AS OutQty
                               , WC.Kind
                               , WC.Routing
                      FROM WorkOrder WO
                           LEFT OUTER JOIN (
                                               SELECT OH.WorkOrder
                                                    , SUM(OH.OutQty) AS OutQty
                                                 FROM OutputHist AS OH
                                                GROUP BY OH.WorkOrder
                                           ) AS OH
                                           ON WO.WorkOrder = OH.WorkOrder
                           INNER JOIN      Material M
                                           ON WO.Material = M.Material
                           INNER JOIN      WorkCenter WC
                                           ON WO.WorkCenter = WC.WorkCenter
                     WHERE WO.WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                    "
                );
                try
                {
                    dtQ = DbAccess.Default.GetDataTable(query.ToString());
                    if (dtQ == null
                        || dtQ.Rows.Count <= 0)
                    {
                        throw new Exception("Not Found WorkOrder");
                    }

                    lbl_item.Text = $@"{WbtCustomService.ActiveValues.Material} / {dtQ.Rows[0]["Spec"]}";
                    _orderQty = dtQ.Rows[0]["OrderQty"].ToString();
                    _kind = dtQ.Rows[0]["Kind"].ToString();
                    ini_Qty = dtQ.Rows[0]["OutQty"].ToString();

                    lbl_Qty.Text = $@"{ini_Qty} / {_orderQty}";
                    lbl_type.Text = dtQ.Rows[0]["Type"].ToString().Trim();

                    if (string.IsNullOrEmpty(lbl_type.Text))
                    {
                        MessageBox.ShowCaption("Single 和 Double 未在项目基础信息中指定。 请在注册项目标准信息后开始。\n\n" + "PCB type (SINGLE / DOUBLE) is not defined in MES system.\n\n" + "You have to enter the PCB type for this item [ " + WbtCustomService.ActiveValues.Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    switch (WbtCustomService.ActiveValues.Routing)
                    {
                        case "Ai_Load" when _kind == "Seq":
                        case "St_Unload":
                        case "Mi_Load":
                            lbl_Reel.Visible = false;
                            break;
                    }

                    if (lbl_type.Text == "SINGLE")
                    {
                        lbl_prev2.Visible = false;
                        btn_clear.Visible = false;
                        tableLayoutPanel8.SetColumnSpan(tableLayoutPanel7, 2);
                        tableLayoutPanel1.SetColumnSpan(lbl_prev1, 2);
                        lbl_prev1.Text = string.Empty;

                        lbl_Current2.Visible = false;
                        tableLayoutPanel1.SetColumnSpan(lbl_Current1, 2);
                        lbl_Current1.Text = string.Empty;
                    }
                    else
                    {
                        lbl_prev2.Visible = true;
                        tableLayoutPanel1.SetColumnSpan(lbl_prev1, 1);
                        lbl_prev1.Text = string.Empty; 
                        lbl_prev2.Text = string.Empty;

                        lbl_Current2.Visible = true;
                        tableLayoutPanel1.SetColumnSpan(lbl_Current1, 1);
                        lbl_Current1.Text = string.Empty;
                        lbl_Current2.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("Process_Management.init", ex.Message);
                    System.Windows.Forms.MessageBox.Show($"检索基线信息时出现问题。\r\nA problem occurred while retrieving baseline information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
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
                    new[]
                    {
                        new DataColumn("Cnt"), new DataColumn("PCB #1"), new DataColumn("PCB #2"), new DataColumn("约会时间 (DateTime)")
                    }
                );

                SetLogMessage("Program started.");

                dgv_List.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
                rTB_log.SelectionFont = new Font("Tahoma", 9, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.init2", ex.Message);
            }
        }

        private bool CheckFeeder()
        {
            switch (WbtCustomService.ActiveValues.Routing)
            {
                case "Ai_Load" when _kind == "Seq":
                case "St_Unload":
                case "Mi_Load":
                    return true;
            }

            var query = new StringBuilder();
            try
            {
                string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";
                var tempStrQty = new StringBuilder();
                GetPrivateProfileString("Setting", "ThresholdQty", "", tempStrQty, 256, path + @"\Config.ini");
                if (Convert.ToInt32(tempStrQty.ToString()) != Convert.ToInt32(_thresholdQty))
                {
                    query.AppendLine
                    (
                        $@"
                            SET NOCOUNT ON;
                            UPDATE FeederWip
                               SET ThresholdQty = {tempStrQty} * InputQty
                             WHERE 1 = 1
                               AND WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                               AND WorkCenter = dbo.GetLinkWorkCenter('{WbtCustomService.ActiveValues.Workcenter}')
                            ;
                         "
                    );
                    InsertIntoSysLog("CheckFeeder()", $"Chenge Threshold [{_thresholdQty}] -> [{tempStrQty}]");
                    _thresholdQty = tempStrQty.ToString();
                }

                query.AppendLine
                (
                    $@"
                        IF (EXISTS
                            (
                                SELECT 'X'
                                  FROM FeederWip WITH (NOLOCK)
                                 WHERE 1 = 1
                                   AND WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                                   AND WorkCenter = dbo.GetLinkWorkCenter('{WbtCustomService.ActiveValues.Workcenter}')
                            ))
                            BEGIN
                                IF (EXISTS
                                    (
                                        SELECT 'X'
                                          FROM (
                                                   SELECT MAX(FW.ThresholdQty) AS ThresholdQty
                                                        , SUM(FW.RemainQty)    AS RemainQty
                                                     FROM FeederWip FW WITH (NOLOCK)
                                                    WHERE 1 = 1
                                                      AND WorkOrder = '{WbtCustomService.ActiveValues.WorkOrder}'
                                                      AND WorkCenter = dbo.GetLinkWorkCenter('{WbtCustomService.ActiveValues.Workcenter}')
                                                    GROUP BY FW.WorkOrder
                                                           , FW.WorkCenter
                                                           , FW.Feeder
                                               ) AS FW
                                         WHERE FW.RemainQty < FW.ThresholdQty 
                                            OR FW.RemainQty <=0 
                                    ))
                                    BEGIN
                                        SELECT 'F'
                                    END
                                ELSE
                                    BEGIN
                                        SELECT 'T'
                                    END
                            END
                        ELSE
                            BEGIN
                                SELECT 'T'
                            END
                    "
                );


                switch (DbAccess.Default.ExecuteScalar(query.ToString()))
                {
                    case "T":
                        lbl_Reel.ForeColor = Color.Gray;
                        lbl_Reel.Text = $@"Feeder内余量OK。 (Feeder Remaining OK..) (Feeder临界数量: {_thresholdQty})";

                        if (!boolDio3Flag)
                        {
                            if (_lineStopFlag) WriteDigitalOut(3, false);
                            SetLogMessage("Conveyor Belt Restart");
                        }

                        boolDio3Flag = true;
                        return true;
                    case "F":
                        lbl_Reel.ForeColor = Color.Red;
                        lbl_Reel.Text = $@"警告！Feeder内资材数量不足！(Warning ! Feeder almost shortage) (Feeder临界数量 : {_thresholdQty})";

                        if (boolDio3Flag)
                        {
                            if (_lineStopFlag) WriteDigitalOut(3, true);
                            SetLogMessage($"Conveyor Belt Stops.");
                        }

                        boolDio3Flag = false;
                        tmrCheckReel_NG.Start();
                        return false;
                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                InsertIntoSysLog("Process_Management.CheckFeeder", e.Message);
                return false;
            }
        }

        private void DeleteOldFiles(string path, string strDate)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                var cmpTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
                foreach (var file in dirInfo.GetFiles())
                {
                    var fileCreatedTime = file.CreationTime;

                    if (DateTime.Compare(fileCreatedTime, cmpTime) > 0)
                    {
                        File.Delete(file.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.DeleteOldFiles", ex.Message);
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                Invoke(new ReceivedFrom_1stScannerDelegate(ReceivedFrom_1stScanner), strScanner_1st);
            }
            else
            {
                ReceivedFrom_1stScanner(strScanner_1st);
            }
        }

        private void InvokeProcessControls_2nd(string strScanner_2nd)
        {
            if (InvokeRequired)
            {
                Invoke(new ReceivedFrom_2ndScannerDelegate(ReceivedFrom_2ndScanner), strScanner_2nd);
            }
            else
            {
                ReceivedFrom_2ndScanner(strScanner_2nd);
            }
        }

        private void ReceivedFrom_Dio(string strDio)
        {
            string Message = "Reply from Digital I/O";
            if (strDio == "SettingChange")
            {
                Message = $"Digital I/O Setting Change(Before DI2+DO2 After DO4)";
            }

            tmrDioReply.Stop();
            SetLogMessage(Message);
            boolDioAvailable = true;
        }

        private void ReceivedFrom_1stScanner(string scanData)
        {
            try
            {
                if (btn_Settings.Text != "Setting up")
                    btn_Settings.Enabled = false;

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
                    VerifyAndSave(scanData);
                }
                else
                {
                    Processing1StDouble(scanData);
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.ReceivedFrom_1stScanner", ex.Message);
            }
        }

        private void Processing1StDouble(string scanData)
        {
            try
            {
                str1stLabel = scanData;

                switch (int2ndLabelFlag)
                {
                    // 2nd에서 처리
                    case 0:
                        int1stLabelFlag = 1;
                        return;
                    case 1:
                    {
                        string strPcbBcd = str1stLabel + "^" + str2ndLabel; // SP에서 Split해서 처리하기 위해 "^" 삽입
                        VerifyAndSave(strPcbBcd);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.Processing1StDouble", ex.Message);
            }
        }

        private void ReceivedFrom_2ndScanner(string scanData)
        {
            try
            {
                if (btn_Settings.Text != "Setting up")
                    btn_Settings.Enabled = false;

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
                InsertIntoSysLog("Process_Management.ReceivedFrom_2ndScanner", ex.Message);
            }
        }

        private void Processing2ndDOUBLE(string scanData)
        {
            try
            {
                str2ndLabel = scanData;

                switch (int1stLabelFlag)
                {
                    // 1st에서 처리
                    case 0:
                        int2ndLabelFlag = 1;
                        return;
                    case 1:
                    {
                        string strPcbBcd = str1stLabel + "^" + str2ndLabel; // SP에서 Split해서 처리하기 위해 "^" 삽입
                        VerifyAndSave(strPcbBcd);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.Processing2ndDOUBLE", ex.Message);
            }
        }

        private void VerifyAndSave(string strPcbBcd)
        {
            try
            {
                Monitor.Enter(_lockObject);
                AllFlagReset();

                if (btn_Settings.Enabled) return;
                if (!CheckFeeder()) return;
                string query = $@"exec [Sp_WorkPcProcedureV3]
                                 @PS_GUBUN		= 'VERIFY_AND_SAVE'
                                ,@PS_ROUTING	= '{WbtCustomService.ActiveValues.Routing}'
                                ,@PS_WORKCENTER = '{WbtCustomService.ActiveValues.Workcenter}'
                                ,@PS_WORKORDER	= '{WbtCustomService.ActiveValues.WorkOrder}'
                                ,@PS_MATERIAL	= '{WbtCustomService.ActiveValues.Material}'
                                ,@PS_TOPMOST_MAT= '{_topMaterial}'
                                ,@PS_PCBBCD		= '{strPcbBcd}'";

                var dataSet = new DataSet();
                using (var connection = new SqlConnection(DbAccess.Default.ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new SqlCommand(query, connection))
                            {
                                command.Transaction = transaction;
                                using (var dataAdapter = new SqlDataAdapter(command))
                                {
                                    dataAdapter.Fill(dataSet);
                                }
                            }

                            if (dataSet == null || dataSet.Tables.Count == 0)
                            {
                                string strErr = "Network problem occurred.";
                                lbl_Current1.ForeColor = Color.Red;
                                lbl_Current2.ForeColor = Color.Red;
                                lbl_Error.Visible = true;
                                lbl_Error.Text = strErr;

                                SetLogMessage($"Send NG signal to Digital I/O");
                                WriteDigitalOut(2, true);
                                throw new Exception(strErr);
                            }

                            if (dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                            {
                                string strErr = dataSet.Tables[dataSet.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();

                                lbl_Current1.ForeColor = Color.Red;
                                lbl_Current2.ForeColor = Color.Red;
                                lbl_Error.Visible = true;
                                lbl_Error.Text = strErr;

                                SetLogMessage($"Send NG signal to Digital I/O");
                                WriteDigitalOut(2, true);
                                return;
                            }

                            if (dataSet.Tables.Count < 2) // 이런 경우는 안나오겠지만..
                            {
                                SetLogMessage($"Send NG signal to Digital I/O");
                                WriteDigitalOut(2, true);
                                throw new Exception("Unknown error occurred.");
                            }

                            string strLog = "";
                            string strRtn = "";

                            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                            {
                                strLog += $"[{dataSet.Tables[0].Rows[i]["PCBBCD"]}] {dataSet.Tables[0].Rows[i]["RTN_TXT"]} \n";
                                strRtn += $"{dataSet.Tables[0].Rows[i]["RTN_TXT"]} / ";
                            }

                            if (strLog.Length > 3)
                            {
                                strLog = strLog.Substring(0, strLog.Length - 1); // 마지막 "\n" 제거
                                strRtn = strRtn.Substring(0, strRtn.Length - 3); //       " / " 제거
                            }

                            if (dataSet.Tables[0].Select("RTN_TXT <> 'OK'").Length != 0)
                            {
                                if (lbl_type.Text == "DOUBLE")
                                {
                                    if (dataSet.Tables[0].Rows[0]["RTN_TXT"].ToString() == "OK") lbl_Current1.Text = "N/A";
                                    if (dataSet.Tables[0].Rows[1]["RTN_TXT"].ToString() == "OK") lbl_Current2.Text = "N/A";
                                }

                                lbl_Current1.ForeColor = Color.Red;
                                lbl_Current2.ForeColor = Color.Red;
                                lbl_Error.Visible = true;
                                lbl_Error.Text = strRtn;

                                SetLogMessage(strLog);

                                SetLogMessage($"Send NG signal to Digital I/O");
                                WriteDigitalOut(2, true);
                                return;
                            }

                            if (Good(dataSet.Tables[0].Rows.Count))
                            {
                                transaction.Commit();
                                lbl_Qty.Text = $@"{dataSet.Tables[0].Rows[0]["CUR_QTY"]} / {_orderQty}";
                                int intCnt = Convert.ToInt32(dataSet.Tables[0].Rows[0]["CUR_QTY"]);
                                string strPcb1 = dataSet.Tables[0].Rows[0]["PCBBCD"].ToString();
                                string strPcb2 = dataSet.Tables[0].Rows.Count > 1 ? dataSet.Tables[0].Rows[1]["PCBBCD"].ToString() : "";
                                SetLogMessage(strPcb2 == "" ? $"[{strPcb1}] Verify and Database Save OK" : $"[{strPcb1}],[{strPcb2}] Verify and Database Save OK");
                                dt1.Rows.Add(intCnt, strPcb1, strPcb2, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                dgv_List.DataSource = dt1;
                                dgv_List.FirstDisplayedScrollingRowIndex = dgv_List.Rows.Count - 1;
                                dgv_List.Rows[dgv_List.Rows.Count - 1].Selected = true;
                                SetLogMessage($"Send OK signal to Digital I/O");
                                WriteDigitalOut(1, true);
                            }
                            else
                            {
                                transaction.Rollback();
                                const string errorMessage = "Network error (Write for result failed. [Good])";
                                lbl_Current1.ForeColor = Color.Red;
                                lbl_Current2.ForeColor = Color.Red;
                                lbl_Error.Visible = true;
                                lbl_Error.Text = errorMessage;
                                SetLogMessage(errorMessage);
                                SetLogMessage($"Send NG signal to Digital I/O");
                                WriteDigitalOut(2, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            System.Windows.Forms.MessageBox.Show($"{ex}\r\n{DbAccess.Default.ConnectionString}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.VerifyAndSave", ex.Message);
                SetLogMessage($"Send NG signal to Digital I/O  {ex.Message}");
                WriteDigitalOut(2, true);
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }


        private void AllFlagReset()
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
                    for (int j = 0; j < 3 - i; j++)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                }

                byte[] bCmdPresetSingleRegister = { bSlaveId, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                bCmdPresetSingleRegister[3] = (byte)(intAdrs == 1 ? 0x02 : intAdrs == 2 ? 0x06 : intAdrs == 3 ? 0x0a : 0x0e); // Address      DO1:02,   DO2:06,     DO3:0a,       DO4:0e
                // Value
                bCmdPresetSingleRegister[5] = (byte)(bVal ? 1 : 0);
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
                InsertIntoSysLog("Process_Management.WriteDigitalOut", ex.Message);
            }
        }

        private bool Good(int count)
        {
            try
            {
                var entryRequest = new EntryRequest
                {
                    UserColumns = new SortedList<string, object>(),
                    From = WiseApp.Id,
                    Mode = WeMaintMode.Insert,
                    ActiveJob = WbtCustomService.ActiveValues.ActiveJob,
                    Workcenter = WbtCustomService.ActiveValues.Workcenter,
                    Count = count,
                    Cavity = 1
                };
                JobControl.Good.Insert(entryRequest);
                return true;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.Good", ex.Message);
                return false;
            }
        }

        private void SetLogMessage(string message)
        {
            if (rTB_log.IsDisposed) return;

            try
            {
                if (rTB_log.InvokeRequired)
                {
                    rTB_log.Invoke(new EventHandler(delegate { SetLogMessage(message); }));
                }
                else
                {
                    string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message} \r\n";

                    string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\Log\ProcessManagement";

                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }

                    string fileName = $@"\{DateTime.Now:yyyy-MM-dd}.txt";
                    var fileInfo = new FileInfo(path + fileName);

                    if (!fileInfo.Exists)
                    {
                        File.AppendAllText(path + fileName, logMessage, Encoding.UTF8);
                    }

                    File.AppendAllText(path + fileName, logMessage, Encoding.UTF8);

                    if (rTB_log.Text.Length + logMessage.Length > rTB_log.MaxLength * 9)
                    {
                        rTB_log.Text = "";
                    }

                    rTB_log.AppendText(logMessage);
                    rTB_log.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                InsertIntoSysLog("Process_Management.SetLogMessage", ex.Message);
            }
        }

        #endregion

        #region Event

        private void Process_Management_V2_Load(object sender, EventArgs e)
        {
            try
            {
                Text = $@"LineStop [{(_lineStopFlag ? "O" : "X")}] " + Text;
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                serialPort1.PortName = strComPortName;
                serialPort1.BaudRate = 115200;
                serialPort1.Open();
                serialPort1.DataReceived += SerialPort1_DataReceived;
                tmrDio1.Elapsed += tmrDio1_Elapsed;
                tmrDio2.Elapsed += tmrDio2_Elapsed;
                tmrDioReply.Elapsed += tmrDioReply_Elapsed;
                _tmrIgnoreDataFromSerialPort2.Elapsed += TmrIgnoreDataFromSerialPort2_Elapsed;
                _tmrIgnoreDataFromSerialPort3.Elapsed += TmrIgnoreDataFromSerialPort3_Elapsed;
                tmrCheckReel.Elapsed += tmrCheckReel_Elapsed;
                tmrCheckReel_NG.Elapsed += tmrCheckReel_NG_Elapsed;

                if (DIOCheck)
                {
                    ReadConfigRegisters(); //DIO 현재 셋팅값 
                    for (int j = 0; j < 5; j++)
                    {
                        Thread.Sleep(100); //잠깐 멈춰야함
                        Application.DoEvents(); //이거 왜 쓰는거?
                    }

                    if (bRcv[4] != 2)
                    {
                        WriteDIOSetting(2); //0 = DI2+DO2 , 1 = DI4  , 2 = DO4
                        InvokeProcessControls_Dio("SettingChange");
                    }
                }

                for (int i = 1; i < 5; i++)
                {
                    WriteDigitalOut(i, false);
                    boolDioAvailable = false;
                }

                DIOCheck = false; // DIO 체크는 한번만             
                intRcvBytes = 0;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Close();
            }

            dgv_List.Focus();
            DeleteOldFiles("C:\\Program Files (x86)\\Wise-M Systems\\Wise-Mes\\Log", DateTime.Now.AddDays(-30).ToString("yyyyMMdd"));
        }

        private void ReadConfigRegisters()
        {
            byte[] bCmdReadHoldingRegisters = { bSlaveId, 0x03, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00 };
            bCmdReadHoldingRegisters = CalcCRC.GetCRC(bCmdReadHoldingRegisters);
            serialPort1.DiscardOutBuffer();
            serialPort1.DiscardInBuffer();
            serialPort1.Write(bCmdReadHoldingRegisters, 0, bCmdReadHoldingRegisters.Length);
        }

        private void WriteDIOSetting(byte bVal)
        {
            byte[] bCmdPresetSingleRegister = { bSlaveId, 0x06, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00 };
            bCmdPresetSingleRegister[3] = 0; // 0:DIO 셋팅 
            bCmdPresetSingleRegister[5] = bVal; // 0:DI2+DO2,   1:DI4   ,2:DO4
            bCmdPresetSingleRegister = CalcCRC.GetCRC(bCmdPresetSingleRegister);
            serialPort1.DiscardOutBuffer();
            serialPort1.DiscardInBuffer();
            serialPort1.Write(bCmdPresetSingleRegister, 0, bCmdPresetSingleRegister.Length);
            for (int j = 0; j < 5; j++)
            {
                Thread.Sleep(100); //잠깐 멈춰야 적용됨
                Application.DoEvents(); // <- 이거 꼭 써야함? 
            }
        }

        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPort1.BytesToRead > 0)
            {
                if (DIOCheck) bRcv[intRcvBytes++] = (byte)serialPort1.ReadByte();
                else serialPort1.ReadExisting();
            }

            InvokeProcessControls_Dio("");
        }

        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var strScanner = serialPort2.ReadLine();

            if (_boolIgnoreDataFromSerialPort2) return;
            _boolIgnoreDataFromSerialPort2 = true;
            _tmrIgnoreDataFromSerialPort2.Start();
            InvokeProcessControls_1st(strScanner);
        }

        private void SerialPort3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var strScanner = serialPort3.ReadLine();
            if (_boolIgnoreDataFromSerialPort3) return;

            _boolIgnoreDataFromSerialPort3 = true;
            _tmrIgnoreDataFromSerialPort3.Start();

            InvokeProcessControls_2nd(strScanner);
        }


        private void TmrIgnoreDataFromSerialPort2_Elapsed(object sender, ElapsedEventArgs e)
        {
            _boolIgnoreDataFromSerialPort2 = false;
        }

        private void TmrIgnoreDataFromSerialPort3_Elapsed(object sender, ElapsedEventArgs e)
        {
            _boolIgnoreDataFromSerialPort3 = false;
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

        private void tmrCheckReel_Elapsed(object sender, ElapsedEventArgs e)
        {
        }

        private void tmrCheckReel_NG_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { CheckFeeder(); }));
            }
            else
            {
                CheckFeeder();
            }
        }

        private void dgv_List_BindingContextChanged(object sender, EventArgs e)
        {
            if (lbl_type.Text == "SINGLE")
            {
                if (!(sender is DataGridView dataGridView)) return;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    switch (column.Index)
                    {
                        case 0:
                            column.Width = 50;
                            break;
                        case 1:
                            column.Width = 500;
                            break;
                        case 2:
                            column.Width = 0;
                            break;
                        case 3:
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            break;
                    }

                    dgv_List.Columns[2].Visible = false;
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            else
            {
                if (!(sender is DataGridView dataGridView)) return;
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    switch (column.Index)
                    {
                        case 0:
                            column.Width = 50;
                            break;
                        case 1:
                            column.Width = 250;
                            break;
                        case 2:
                            column.Width = 250;
                            break;
                        case 3:
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            break;
                    }

                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_Settings_Click(sender, e); }));
            }
            else
            {
                if (btn_Settings.Text == "Setting up")
                {
                    if (System.Windows.Forms.MessageBox.Show("您确定要完成测试吗？ \r\nAre you sure you want to finish setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    lbl_Qty.ForeColor = Color.Black;
                    lbl_Qty.Text = $@"{ini_Qty} / {_orderQty}";
                    btn_Settings.Text = "设置\r\n Setting";

                    lbl_Current1.Text = string.Empty;
                    lbl_Current2.Text = string.Empty;
                    lbl_prev1.Text = string.Empty;
                    lbl_prev2.Text = string.Empty;
                }
                else
                {
                    if (System.Windows.Forms.MessageBox.Show("您确定要开始测试吗？\r\nAre you sure you want to start setup?", "Notice", MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return;
                    btn_Settings.Text = $@"Setting up";
                    lbl_Qty.ForeColor = Color.Red;
                    lbl_Qty.Text = "Setting...";
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler(delegate { btn_clear_Click(sender, e); }));
            }
            else
            {
                AllFlagReset();
                lbl_Current1.Text = string.Empty;
                lbl_Current2.Text = string.Empty;
            }
        }

        private void InsertIntoSysLog(string strSource, string strMsg)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', '{strSource}', LEFT(ISNULL(N'{strMsg}',''),3000), '{WbtCustomService.ActiveValues.Workcenter}', GETDATE())");
        }

        private void btn_mod_Click(object sender, EventArgs e)
        {
            var mod = new Process_Management_Mod();
            mod.ShowDialog();
        }

        private void Process_Management_V2_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                tmrDio1.Dispose();
                tmrDio2.Dispose();
                tmrDioReply.Dispose();
                _tmrIgnoreDataFromSerialPort2.Dispose();
                _tmrIgnoreDataFromSerialPort3.Dispose();
                tmrCheckReel.Dispose();
                tmrCheckReel_NG.Dispose();

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