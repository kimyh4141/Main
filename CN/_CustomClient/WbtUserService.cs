using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace WiseM.Client
{
    public class WbtUserService : WbtCustomService
    {
        protected override void JobRunUserButtonInfo(JobRunUserButtonInfo e)
        {
            bool exitFlag = false;

            int intMyId = System.Diagnostics.Process.GetCurrentProcess().Id;

            var processes = System.Diagnostics.Process.GetProcessesByName("wiseclient");
            foreach (var process in processes)
            {
                if (process.Id == intMyId) continue;
                exitFlag = true;
                break;
            }

            base.JobRunUserButtonInfo(e);
            if (!exitFlag) return;
            System.Windows.Forms.MessageBox.Show(new Form { TopMost = true }, "The Client Program is already running.");
            Environment.Exit(0);
        }

        //OtherMaint 프로그램 추가
        protected override void OtherMaint(UserPopUpEventArgs e)
        {
            switch (e.Name)
            {
                case "Ai_Loading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var processManagementV2 = new Process_Management_V2(true) { Text = "自动插入 (加载) Auto-Insert (Loading)" };
                    try
                    {
                        processManagementV2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Ai_Loading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var AL2 = new Process_Management_V2(false);
                    AL2.Text = "自动插入 (加载) Auto-Insert (Loading)";
                    try
                    {
                        AL2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Ai_Unloading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var AU = new Process_Management_V2(true);
                    AU.Text = "自动插入 (卸货) Auto-Insert (Unloading)";
                    try
                    {
                        AU.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Ai_Unloading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var AU2 = new Process_Management_V2(false);
                    AU2.Text = "自动插入 (卸货) Auto-Insert (Unloading)";
                    try
                    {
                        AU2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;

                case "Smt_Loading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SL = new Process_Management_V2(true);
                    SL.Text = "表面贴装技术（加载）Surface-Mount Technology (Loading)";
                    try
                    {
                        SL.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Smt_Loading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SL2 = new Process_Management_V2(false);
                    SL2.Text = "表面贴装技术（加载）Surface-Mount Technology (Loading)";
                    try
                    {
                        SL2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Smt_Unloading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SU = new Process_Management_V2(true);
                    SU.Text = "表面贴装技术（卸货）Surface-Mount Technology (Unloading)";
                    try
                    {
                        SU.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;

                case "Smt_Unloading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SU2 = new Process_Management_V2(false);
                    SU2.Text = "表面贴装技术（卸货）Surface-Mount Technology (Unloading)";
                    try
                    {
                        SU2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;

                case "Mi_Loading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var ML = new Process_Management_V2(true);
                    ML.Text = "手动插入（加载）Manual-Insert (Loading)";
                    try
                    {
                        ML.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Mi_Loading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var ML2 = new Process_Management_V2(false);
                    ML2.Text = "手动插入（加载）Manual-Insert (Loading)";
                    try
                    {
                        ML2.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "BoxingPalletizing":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    ConfirmType CT = new ConfirmType();
                    CT.Text = "Select Type";
                    try
                    {
                        if (DialogResult.OK == CT.ShowDialog())
                        {
                            Box_Palletizing BP = new Box_Palletizing(CT.GetItem("Type"), CT.GetItem("Date"));
                            BP.Text = "装箱 & 码垛 Boxing & Palletizing";
                            try
                            {
                                BP.ShowDialog();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "BoxingPalletizing_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var CT2 = new ConfirmType_V2();
                    CT2.Text = "Select Type";
                    try
                    {
                        if (DialogResult.OK == CT2.ShowDialog())
                        {
                            var BP = new Box_Palletizing_V2(CT2.GetItem("Type"), CT2.GetItem("Date"), CT2.GetItem("Palletbcd"), CT2.GetItem("Boxbcd"));
                            BP.Text = "装箱 & 码垛 Boxing & Palletizing";
                            try
                            {
                                BP.ShowDialog();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "RePrint_Box":
                    Reprint_Box rb = new Reprint_Box();
                    rb.ShowDialog();
                    break;
                case "RePrint_Pallet":
                    Reprint_Pallet rp = new Reprint_Pallet();
                    rp.ShowDialog();
                    break;
                case "Daily_Output":
                    Daily_Output daily_output = new Daily_Output();
                    daily_output.ShowDialog();
                    break;
                case "Daily_Output_Boxing":
                    Daily_Output_Boxing daily_output_boxing = new Daily_Output_Boxing();
                    daily_output_boxing.ShowDialog();
                    break;
                case "Stock_In":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"未选择任何工作订单。\r\n请选择一个工作订单。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"工作订单尚未开始。\r\n请开始工作订单。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    Palletizing_2 pt = new Palletizing_2();
                    pt.Text = "包装#2 成品收货 Stock In";
                    try
                    {
                        pt.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
            }

            base.OtherMaint(e);
        }

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private bool ChangeFeederWipStatus(CustomEnum.MountingChangeCaseType mountingChangeCaseType, string previousWorkOrder = "", string thresholdQty = "0")
        {
            var query = new StringBuilder();
            query.AppendLine
            (
                $@"
                    DECLARE @WorkOrder NVARCHAR(50) = '{ActiveValues.WorkOrder}'
                    ;

                    DECLARE @PreviousWorkOrder NVARCHAR(50) = '{previousWorkOrder}'
                    ;

                    DECLARE @WorkCenter NVARCHAR(50) = '{ActiveValues.Workcenter}'
                    ;

                    DECLARE @Material NVARCHAR(50) = '{ActiveValues.Material}'
                    ;

                    DECLARE @MountingWorkCenter NVARCHAR(50) = dbo.GetLinkWorkCenter(@WorkCenter)
                    ;
                    "
            );
            switch (mountingChangeCaseType)
            {
                /*
                 *
                 */
                case CustomEnum.MountingChangeCaseType.Create:
                    query.AppendLine
                    (
                        $@"
                            UPDATE MountingInputHist
                               SET IsPrevious = 0
                             WHERE IsPrevious = 1
                               AND WorkCenter = @MountingWorkCenter
                            ;

                            INSERT
                              INTO FeederWip( Material
                                            , WorkCenter
                                            , Feeder
                                            , RawMaterial
                                            , ThresholdQty
                                            , InputQty
                                            , RawMaterialSeq
                                            , WorkOrder
                                            , UpdateUser
                                            , Updated )
                            SELECT Material
                                 , WorkCenter
                                 , Feeder
                                 , RawMaterial
                                 , '{thresholdQty}' * InputQty
                                 , InputQty
                                 , RawMaterialSeq
                                 , @WorkOrder
                                 , @WorkCenter AS UpdateUser
                                 , GetDate()   AS Updated
                              FROM FeederInfo
                             WHERE Status = '1'
                               AND Material = @Material
                               AND WorkCenter = @MountingWorkCenter
                            ;
                            "
                    );
                    break;
                case CustomEnum.MountingChangeCaseType.Continue:
                    query.AppendLine
                    (
                        $@"
                            IF (@PreviousWorkOrder <> @WorkOrder)
                                BEGIN
                                    DELETE
                                      FROM FeederWip
                                     WHERE 1 = 1
                                       AND WorkOrder = @WorkOrder
                                       AND WorkCenter = @MountingWorkCenter;
                                END

                            UPDATE FeederWip
                               SET WorkOrder = @WorkOrder
                                 , InputSeq  = 0
                             WHERE WorkOrder = @PreviousWorkOrder
                               AND WorkCenter = @MountingWorkCenter
                            ;

                            INSERT
                              INTO MountingInputHist ( WorkCenter
                                                     , WorkOrder
                                                     , Feeder
                                                     , RawMaterial
                                                     , InputSeq
                                                     , Barcode
                                                     , TotalQty
                                                     , InputQty
                                                     , UsedQty
                                                     , Status
                                                     , IsPrevious
                                                     , CreateUser )
                            SELECT WorkCenter
                                 , @WorkOrder
                                 , Feeder
                                 , RawMaterial
                                 , 0
                                 , Barcode
                                 , TotalQty - UsedQty
                                 , InputQty
                                 , 0
                                 , 1
                                 , 0
                                 , @WorkCenter
                              FROM MountingInputHist
                             WHERE TotalQty > UsedQty
                               AND IsPrevious = 1
                               AND WorkCenter = @MountingWorkCenter
                            ;

                            UPDATE MountingInputHist
                               SET IsPrevious = 0
                             WHERE IsPrevious = 1
                               AND WorkCenter = @MountingWorkCenter
                            ;
                            "
                    );
                    break;
                case CustomEnum.MountingChangeCaseType.Import:
                    query.AppendLine
                    (
                        $@"
                            UPDATE MountingInputHist
                               SET IsPrevious = 0
                             WHERE IsPrevious = 1
                               AND WorkCenter = @MountingWorkCenter
                            ;

                            UPDATE MountingInputHist
                               SET Status = 1
                             WHERE 1 = 1
                               AND Status = 0
                               AND WorkOrder = @WorkOrder
                               AND WorkCenter = @MountingWorkCenter
                            ;
                            "
                    );
                    break;
                case CustomEnum.MountingChangeCaseType.Stop:
                    query.AppendLine
                    (
                        $@"
                            UPDATE MountingInputHist
                               SET Status     = 0
                                 , IsPrevious = 1
                             WHERE Status = 1
                               AND 1 = 1
                               AND WorkOrder = @WorkOrder
                               AND WorkCenter = @MountingWorkCenter
                            ;
                            "
                    );
                    break;
                case CustomEnum.MountingChangeCaseType.ReCreate:
                    query.AppendLine
                    (
                        $@"
                            DELETE
                              FROM FeederWip
                             WHERE 1 = 1
                               AND WorkOrder = @WorkOrder
                               AND WorkCenter = @MountingWorkCenter
                            ;
                            "
                    );
                    goto case CustomEnum.MountingChangeCaseType.Create;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mountingChangeCaseType), mountingChangeCaseType, null);
            }

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
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction?.Rollback();
                        InsertIntoSysLog("E", ex.Message, ActiveValues.Workcenter);
                        MessageBox.Show("数据库错误(Database Error)\r\n" + ex.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("E", ex.Message, ActiveValues.Workcenter);
                    MessageBox.Show("数据库错误(Database Error)\r\n" + ex.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 외주업체는 작업지시를 시작하지 못 하도록 검증
        /// </summary>
        /// <returns></returns>
        private bool VerifyWorkOrder(string workOrder)
        {
            return DbAccess.Default.ExecuteScalar
                   (
                       $@"
                        SELECT COALESCE(WC.ERP_WC_CD, '') AS ERP_WC_CD
                          FROM WorkOrder AS WO
                               INNER JOIN WorkCenter AS WC
                                          ON WO.WorkCenter = WC.WorkCenter
                         WHERE WO.WorkOrder = '{workOrder}'
                        ;
                        "
                   ) is string erpWorkCenterCode
                   && !erpWorkCenterCode.EqualsOr("C0010", "C0058");
        }

        protected override void UserJobControl(JobControlEventArgs e)
        {
            try
            {
                string query = string.Empty;
                switch (e.Action)
                {
                    case WeJobControlAction.JobStart:
                        switch (ActiveValues.Routing)
                        {
                            case "St_Unload":
                                break;
                            case "Ai_Load":
                            case "Ai_Unload":
                            case "St_Load":
                            case "Mi_Load":
                                //자삽 Load작업지시의 경우 Seq에 마운팅이 하나라도 되어있지 않으면 시작할 수 없도록
                                if (ActiveValues.Routing.Equals("Ai_Load"))
                                {
                                    string queryVerifyWorkOrderCanStart =
                                        $@"
                                        SP_VerifyWorkOrderCanStart '{ActiveValues.WorkOrder}'
                                        ;
                                        ";
                                    if (DbAccess.Default.ExecuteScalar(queryVerifyWorkOrderCanStart) is string result
                                        && result.Equals("F"))
                                    {
                                        string message = "未完成SEQ作业！\r\n(Not yet work of SEQ mounting!)";
                                        System.Windows.Forms.MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        e.Cancel = true;
                                        return;
                                    }
                                }

                                string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";

                                if (!Directory.Exists(path))
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
                                    WritePrivateProfileString("Setting", "ThresholdQty", "0", path + @"\Config.ini");
                                }

                                var stringBuilder = new StringBuilder();
                                GetPrivateProfileString("Setting", "thresholdQty", "", stringBuilder, 256, path + @"\Config.ini");
                                var thresholdQty = stringBuilder.ToString();

                                //SEQ라인이면 넘어감.
                                if (0 < DbAccess.Default.IsExist("WorkCenter", $"WorkCenter = '{ActiveValues.Workcenter}' AND Kind = 'Seq'"))
                                {
                                    break;
                                }

                                //동일라인에서 직전의 모델과 작업할 모델이 일치할 경우
                                var lastRow = DbAccess.Default.GetDataRow
                                (
                                    $@"
                                     SELECT TOP 1 Material, WorkOrder
                                          FROM ActiveJobHist WTIH (NOLOCK)
                                         WHERE ActiveStatus IN ('Cancel','End')
                                           AND WorkCenter = '{ActiveValues.Workcenter}'
                                           
                                         ORDER BY Updated DESC
                                    ;
                                    "
                                );
                                var mountingChangeCaseType = CustomEnum.MountingChangeCaseType.None;
                                if (lastRow != null)
                                {
                                    string lastWorkOrder = lastRow["WorkOrder"] as string;
                                    string lastMaterial = lastRow["Material"] as string;
                                    if (ActiveValues.Material.Equals(lastMaterial))
                                    {
                                        e.PassUserConfirm = true;
                                        const string message = "与上一工作指示相同型号的指示。是否继续操作？\r\n(This is the order of the same model as the previous order. Do you want to continue with the work information?)";
                                        var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                                        switch (result)
                                        {
                                            case DialogResult.Yes:
                                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Continue;
                                                break;
                                            case DialogResult.No:
                                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;
                                                break;
                                            case DialogResult.Cancel:
                                                e.Cancel = true;
                                                return;
                                        }

                                        InsertIntoSysLog("N", $"Start WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)}\r\nThreshold : {thresholdQty}", ActiveValues.Workcenter);
                                        if (ChangeFeederWipStatus(mountingChangeCaseType, lastWorkOrder, thresholdQty)) return;
                                        e.Cancel = true;
                                        return;
                                    }
                                }

                                //동일라인에서 이전에 작업한 작업지시의 경우(FeederWip 유무)
                                if (DbAccess.Default.IsExist
                                    (
                                        "FeederWip",
                                        $@"1 = 1 
                                                    AND WorkOrder = '{ActiveValues.WorkOrder}'
                                                    AND WorkCenter = dbo.GetLinkWorkCenter('{ActiveValues.Workcenter}')"
                                    )
                                    > 0
                                   )
                                {
                                    e.PassUserConfirm = true;
                                    //수삽일 경우
                                    if (ActiveValues.Routing.Equals("Mi_Load"))
                                    {
                                        const string message = "在同一产线，生产之前的作业指示。确定召回之前的信息吗？\r\n(Do you want to continue with the work information?)";
                                        var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                        switch (result)
                                        {
                                            case DialogResult.Yes:
                                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Import;
                                                break;
                                            case DialogResult.No:
                                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;
                                                break;
                                            case DialogResult.Cancel:
                                                e.Cancel = true;
                                                return;
                                        }

                                        InsertIntoSysLog("N", $"Start WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)}\r\nThreshold : {thresholdQty}", ActiveValues.Workcenter);
                                        if (ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty)) return;
                                        e.Cancel = true;
                                        return;
                                    }

                                    mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;
                                    InsertIntoSysLog("N", $"Start WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)}\r\nThreshold : {thresholdQty}", ActiveValues.Workcenter);
                                    if (ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty)) return;
                                    e.Cancel = true;
                                    return;
                                }

                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Create;
                                InsertIntoSysLog("N", $"Start WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)}\r\nThreshold : {thresholdQty}", ActiveValues.Workcenter);
                                if (ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty)) return;
                                e.Cancel = true;
                                return;
                            case "Pk_Boxing":
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"这台电脑无法启动工作进程 \r\n" + $"작업시작을 할 수 없는 공정의 PC입니다.");
                                break;
                        }

                        break;
                    case WeJobControlAction.JobEnd:
                        switch (ActiveValues.Routing)
                        {
                            case "St_Unload":
                                break;
                            case "Ai_Load":
                            case "Ai_Unload":
                            case "Mi_Load":
                            case "St_Load":
                                if (0 < DbAccess.Default.IsExist("WorkCenter", $"WorkCenter = '{ActiveValues.Workcenter}' AND Kind = 'Seq'")) break;
                                e.PassUserConfirm = true;
                                const string message = "Do you want to end the operation?";
                                var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                switch (result)
                                {
                                    case DialogResult.Yes:
                                        //SEQ 라인이 아니면
                                        InsertIntoSysLog("N", $"End WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), CustomEnum.MountingChangeCaseType.Stop)}", ActiveValues.Workcenter);
                                        if (ChangeFeederWipStatus(CustomEnum.MountingChangeCaseType.Stop)) return;
                                        e.Cancel = true;
                                        break;
                                    case DialogResult.No:
                                        e.Cancel = true;
                                        break;
                                }
                                return;
                            case "Pk_Boxing":
                                try
                                {
                                    query = $@"
                                   IF( EXISTS(SELECT * FROM BoxTemp WHERE PalletBcd IS NULL AND WorkCenter = '{ActiveValues.Workcenter}'))
                                   	BEGIN
                                   	    SELECT 'F'
                                   	END
                                   ELSE
                                   	BEGIN
                                        INSERT INTO BoxingHist (WorkOrder, WorkCenter, PcbBcd, BoxBcd, PalletBcd, Created, Updated, Ended)
                                        SELECT WorkOrder, WorkCenter, PcbBcd, BoxBcd, PalletBcd, Created, Updated, GETDATE() FROM BoxTemp WHERE WorkCenter = '{ActiveValues.Workcenter}'

                                   	    DELETE FROM BoxTemp WHERE WorkCenter = '{ActiveValues.Workcenter}'
                                        SELECT 'T'
                                   	END
                                  ";
                                    if (DbAccess.Default.ExecuteScalar(query).ToString() == "F")
                                    {
                                        e.Cancel = true;
                                        System.Windows.Forms.MessageBox.Show($"无法完成工单，因为托盘不完整。 \r\n" + $"The work order cannot be completed because there are incomplete pallets.");
                                    }
                                }
                                catch (Exception exception)
                                {
                                    e.Cancel = true;
                                    Console.WriteLine(exception);
                                    throw;
                                }

                                break;
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"这台电脑无法取消工作进程 \r\n" + $"작업취소을 할 수 없는 공정의 PC입니다.");
                                break;
                        }

                        break;
                    case WeJobControlAction.WorkOrderSelect:
                        if (!VerifyWorkOrder(ActiveValues.WorkOrder))
                        {
                            e.Cancel = true;
                            System.Windows.Forms.MessageBox.Show($"不能启动这个作业指示\r\nThe work order cannot be started.", "cannot be started", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        break;
                    case WeJobControlAction.JobCancel:
                        switch (ActiveValues.Routing)
                        {
                            case "St_Unload":
                                break;
                            case "Ai_Load":
                            case "Ai_Unload":
                            case "Mi_Load":
                            case "St_Load":
                                if (0 < DbAccess.Default.IsExist("WorkCenter", $"WorkCenter = '{ActiveValues.Workcenter}' AND Kind = 'Seq'")) break;
                                e.PassUserConfirm = true;
                                const string message = "Do you want to cancel the operation?";
                                var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                switch (result)
                                {
                                    case DialogResult.Yes:
                                        //SEQ 라인이 아니면
                                        InsertIntoSysLog("N", $"Cancel WorkOrder : {ActiveValues.WorkOrder}\r\nFeederWip : {Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), CustomEnum.MountingChangeCaseType.Stop)}", ActiveValues.Workcenter);
                                        if (ChangeFeederWipStatus(CustomEnum.MountingChangeCaseType.Stop)) return;
                                        e.Cancel = true;
                                        break;
                                    case DialogResult.No:
                                        e.Cancel = true;
                                        break;
                                }
                                return;
                            case "Pk_Boxing":
                                query =
                                    $@"
                                 IF (EXISTS
                                    (
                                        SELECT *
                                          FROM BoxTemp
                                         WHERE PalletBcd IS NULL
                                           AND WorkCenter = '{ActiveValues.Workcenter}'
                                    ))
                                    BEGIN
                                        SELECT 'F'
                                    END
                                ELSE
                                    BEGIN
                                        INSERT
                                          INTO BoxingHist ( WorkOrder
                                                          , WorkCenter
                                                          , PcbBcd
                                                          , BoxBcd
                                                          , PalletBcd
                                                          , Created
                                                          , Updated
                                                          , Ended )
                                        SELECT WorkOrder
                                             , WorkCenter
                                             , PcbBcd
                                             , BoxBcd
                                             , PalletBcd
                                             , Created
                                             , Updated
                                             , GETDATE()
                                          FROM BoxTemp
                                         WHERE WorkCenter = '{ActiveValues.Workcenter}'

                                        DELETE
                                          FROM BoxTemp
                                         WHERE WorkCenter = '{ActiveValues.Workcenter}'
                                        SELECT 'T'
                                    END
                                  ";
                                if (DbAccess.Default.ExecuteScalar(query).ToString() == "F")
                                {
                                    e.Cancel = true;
                                    System.Windows.Forms.MessageBox.Show($"无法完成工单，因为托盘不完整。 \r\n" + $"The work order cannot be completed because there are incomplete pallets.");
                                }

                                break;
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"这台电脑无法取消工作进程 \r\n" + $"작업취소을 할 수 없는 공정의 PC입니다.");
                                break;
                        }

                        break;
                }
            }
            catch (Exception exception)
            {
                InsertIntoSysLog("E", exception.Message, ActiveValues.Workcenter);
                MessageBox.Show("数据库错误(Database Error)\r\n" + exception.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                throw;
            }
        }

        protected override IWbtUserControl GetUserControl(UserControlEventArgs e)
        {
            return null;
        }


        private void InsertIntoSysLog(string strMsg, string strWorkCenter) => InsertIntoSysLog("E", strMsg, strWorkCenter);

        private void InsertIntoSysLog(string type, string strMsg, string strWorkCenter)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery
            (
                $@"
                INSERT
                  INTO SysLog (
                                Type
                              , Category
                              , Source
                              , Message
                              , [user]
                              , Updated
                              )
                VALUES (
                         '{type}'
                       , 'Client'
                       , '{WiseApp.Id}'
                       , LEFT(ISNULL(N'{strMsg}', ''), 3000)
                       , '{strWorkCenter}'
                       , GETDATE()
                       )
                "
            );
        }
    }
}