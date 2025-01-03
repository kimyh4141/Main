using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using WiseM.Other;

namespace WiseM.Client
{
    public class WbtUserService : WbtCustomService
    {
        //OtherMaint 프로그램 추가
        protected override void OtherMaint(UserPopUpEventArgs e)
        {
            switch (e.Name)
            {
                case "Ai_Loading":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var AL = new Process_Management_V2(true);
                    AL.Text = "Gắn tự động (Loading) Auto-Insert (Loading)";
                    try
                    {
                        AL.ShowDialog();
                    }
                    catch (Exception)
                    {
                    }

                    break;
                case "Ai_Loading_Test":
                    if (string.IsNullOrEmpty(ActiveValues.WorkOrder))
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var AL2 = new Process_Management_V2(false);
                    AL2.Text = "Gắn tự động (Loading) Auto-Insert (Loading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SL = new Process_Management_V2(true);
                    SL.Text = "Công nghệ gắn bề mặt (Loading) Surface -Mount Technology (Loading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SL2 = new Process_Management_V2(false);
                    SL2.Text = "Công nghệ gắn bề mặt (Loading) Surface -Mount Technology (Loading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SU = new Process_Management_V2(true);
                    SU.Text = "Công nghệ gắn bề mặt (Unloading) Surface-Mount Technology (Unloading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var SU2 = new Process_Management_V2(false);
                    SU2.Text = "Công nghệ gắn bề mặt (Unloading) Surface-Mount Technology (Unloading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var ML = new Process_Management_V2(true);
                    ML.Text = "Gắn thủ công (Loading) Manual-Insert (Loading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var ML2 = new Process_Management_V2(false);
                    ML2.Text = "Gắn thủ công (Loading) Manual-Insert (Loading)";
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var confirmType = new ConfirmType();
                    confirmType.Text = "Select Type";
                    try
                    {
                        if (DialogResult.OK == confirmType.ShowDialog())
                        {
                            var BP = new Box_Palletizing(confirmType.Type, confirmType.PackingDate, confirmType.PalletBarcode, confirmType.BoxBarcode);
                            BP.Text = "Boxing & Palletizing";
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
                {
                    var productBarcodeReprint = new ProductBarcodeReprint("ProductBox");
                    productBarcodeReprint.ShowDialog();
                }
                    break;
                case "RePrint_Pallet":
                {
                    var productBarcodeReprint = new ProductBarcodeReprint("Pallet");
                    productBarcodeReprint.ShowDialog();
                }
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
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 선택되지 않았습니다. \r\n작업지시를 선택해주세요.");
                        break;
                    }

                    if (ActiveValues.ActiveJobStatus != WeActiveJobStatus.Active)
                    {
                        System.Windows.Forms.MessageBox.Show($"Chưa chọn chỉ thị thao tác。\r\nVui lòng chọn chỉ thị thao tác。\r\n" + $"작업지시가 시작되지 않았습니다.\r\n작업지시를 시작해주세요.");
                        break;
                    }

                    var palletizing = new Palletizing();
                    palletizing.Text = "Đóng gói#2 Còn hàng Stock In";
                    try
                    {
                        palletizing.ShowDialog();
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
                                 , '{thresholdQty}'
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
                                   AND WorkCenter = @MountingWorkCenter
                              END
                            ;

                            UPDATE FeederWip
                               SET WorkOrder = @WorkOrder
                                 , InputSeq  = 0
                             WHERE WorkOrder = @PreviousWorkOrder
                               AND WorkCenter = @MountingWorkCenter
                            ;  

                            INSERT
                              INTO MountingInputHist (
                                                       WorkCenter
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
                                                     , CreateUser
                                                     )
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
                        MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + ex.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    InsertIntoSysLog("E", ex.Message, ActiveValues.Workcenter);
                    MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + ex.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// 작업지시를 검증
        /// 1. 작업지시의 작업장과 현재 작업장과 같은 Owner인지 검증
        /// </summary>
        /// <returns></returns>
        private bool VerifyWorkOrder(string workOrder, string routing)
        {
            if (routing == "Mi_Load")
            {
                if (!CheckOutQtyForMIProcess(workOrder)) return false;
            }
            string query =
                $@"
                IF (
                  EXISTS
                    (
                      SELECT 'X'
                        FROM WorkOrder             AS WO
                             INNER JOIN WorkCenter AS WC
                                        ON WO.Workcenter = WC.Workcenter
                       WHERE WO.WorkOrder = '{workOrder}'
                         AND WC.Owner = (
                                          SELECT WC.Owner
                                            FROM WorkCenter AS WC
                                           WHERE WC.Workcenter = '{ActiveValues.Workcenter}'
                                        )
                    )
                  )
                  BEGIN
                    SELECT 1
                  END
                ELSE
                  BEGIN
                    SELECT 0
                  END;
                ";
            return Convert.ToBoolean(DbAccess.Default.ExecuteScalar(query));
        }

        private bool CheckOutQtyForMIProcess(string workOrder)
        {
            try
            {
                string query =
                    $@"
                IF EXISTS (
                        SELECT *
                        FROM (
                            SELECT EPIRBO.PRODT_ORDER_NO
                                , EPIRBO.ORDER_QTY
                                , EPIRBO.ALT_GROUP
                                , EPIRBO.KEY_ITEM
                                , EPIRBO.CHILD_ITEM_CD
                                , EPIRBO.SEQ
                                , EPIRBO.CAVITY_QTY
                                , RSH.RowNumber
                                , RSH.Rm_BarCode
                                --그룹별 총 출고수량
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                    WHEN N'N'
                                    THEN SUM(COALESCE(RSH.StockOutQty, 0) - COALESCE(RSH_R.StockReturnQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ)
                                    WHEN N'Y'
                                    THEN SUM(COALESCE(RSH.StockOutQty, 0) - COALESCE(RSH_R.StockReturnQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
		                        END                                                              AS GroupTotalStockOutQty
                                , EPIRBO.GroupTotalReportedQty
                                , EPIRBO.ReportedQty
                                --출고누적합 그룹별 수량
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                    WHEN N'N'
                                    THEN SUM(COALESCE(RSH.StockOutQty, 0) - COALESCE(RSH_R.StockReturnQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ ORDER BY RSH.RowNumber)
                                    WHEN N'Y'
                                    THEN SUM(COALESCE(RSH.StockOutQty, 0) - COALESCE(RSH_R.StockReturnQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP ORDER BY RSH.RowNumber)
                                END                                                              AS PrefixSumGroupStockOutQty
                                --자재이력 별 출고수량
                                , COALESCE(RSH.StockOutQty, 0) - COALESCE(RSH_R.StockReturnQty, 0) AS StockOutQty
                            FROM (
                                SELECT EPPO.PRODT_ORDER_NO
                                     , EPPO.ORDER_QTY
                                     , EPIRBO.ALT_GROUP
                                     , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                     , EPIRBO.KEY_ITEM
                                     , EPIRBO.CHILD_ITEM_CD
                                     , EPIRBO.SEQ
                                     --그룹별 총 보고된수량
                                     , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
					                        WHEN N'N'
						                        THEN SUM(COALESCE(RMUH.ReportedQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.CHILD_ITEM_CD)
					                        WHEN N'Y'
						                        THEN SUM(COALESCE(RMUH.ReportedQty, 0)) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
				                        END                           AS GroupTotalReportedQty
                                     --자재 별 보고된수량
                                     , COALESCE(RMUH.ReportedQty, 0) AS ReportedQty
                                     --단위별 소요수량
                                     , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                            WHEN N'N'
                                                THEN EPIRBO.REQ_QTY / EPPO.ORDER_QTY
                                            WHEN N'Y'
                                                THEN SUM(EPIRBO.REQ_QTY) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP) / EPPO.ORDER_QTY
				                        END                           AS CAVITY_QTY
			                        FROM 
				                        (
				                        SELECT WO.WorkOrder
					                         , WO.Routing
					                         , WO.ERP_WorkOrder
				                        FROM WorkOrder AS WO WITH (NOLOCK)
				                        WHERE WorkOrder = '{workOrder}'
			                        ) AS WO
		                        INNER JOIN
			                        (
				                        SELECT RowNumber
					                         , PRODT_ORDER_NO
					                         , ORDER_QTY
					                         , I_PROC_STEP
				                        FROM (
					                        SELECT ROW_NUMBER() OVER (PARTITION BY EPPO.PRODT_ORDER_NO ORDER BY EPPO.IF_TIME DESC) AS RowNumber
						                         , EPPO.PRODT_ORDER_NO
						                         , EPPO.ORDER_QTY
						                         , EPPO.I_PROC_STEP
					                        FROM MES_IF_VN.dbo.ETM_P_PROD_ORDER AS EPPO WITH (NOLOCK)
					                        WHERE 1 = 1
				                        ) AS EPPO
				                        WHERE EPPO.RowNumber = 1
				                        AND EPPO.I_PROC_STEP IN ('C', 'U')
		                        ) AS EPPO
		                        ON WO.ERP_WorkOrder = EPPO.PRODT_ORDER_NO
		                        INNER JOIN (
			                        SELECT ROW_NUMBER() OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ ORDER BY EPIRBO.IF_TIME DESC) AS RowNumber
				                         , EPIRBO.PRODT_ORDER_NO
				                         , EPIRBO.SEQ
				                         , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
				                         , EPIRBO.KEY_ITEM
				                         , EPIRBO.ALT_GROUP
				                         , EPIRBO.ITEM_RATE
				                         , EPIRBO.CHILD_ITEM_CD
				                         , EPIRBO.REQ_QTY
				                         , EPIRBO.I_PROC_STEP
				                         , EPIRBO.APPLY_FLAG
			                        FROM MES_IF_VN.dbo.ETM_P_ISSUE_REQ_BY_ORDER AS EPIRBO WITH (NOLOCK)
		                        ) AS EPIRBO
		                        ON EPPO.PRODT_ORDER_NO = EPIRBO.PRODT_ORDER_NO
			                        AND EPIRBO.RowNumber = 1
			                        AND EPIRBO.I_PROC_STEP IN ('C', 'U')
		                        LEFT OUTER JOIN (
				                        --수삽 BOM은 두개이상의 같은자재인 행이 존재하지 않는다고 함.
				                        --만약 생길 경우 RawMaterialHist 저장 시에 어떠한 그룹으로 소모되었는지 기록한 다음 남은계산 수량 계산 시에 사용해야함.
				                        --2023-10-27 확인 시 존재한다고 함. 테이블 변경 및 RawMaterialSeq 추가
			                        SELECT WO.ERP_WorkOrder
				                         , RMUH.RawMaterial
				                         , RMUH.RawMaterialSeq
				                         , SUM(RMUH.Qty) AS ReportedQty
			                        FROM RawMaterialUsageHist AS RMUH WITH (NOLOCK)
			                        INNER JOIN WorkOrder AS WO WITH (NOLOCK)
				                        ON RMUH.WorkOrder = WO.WorkOrder
				                        WHERE RMUH.Type = 'MI'
				                        GROUP BY WO.ERP_WorkOrder, RMUH.RawMaterialSeq, RMUH.RawMaterial
			                        ) AS RMUH
			                        ON EPIRBO.PRODT_ORDER_NO = RMUH.ERP_WorkOrder
			                        AND EPIRBO.SEQ = RMUH.RawMaterialSeq
			                        AND EPIRBO.CHILD_ITEM_CD = RMUH.RawMaterial
			                        INNER JOIN      MES_IF_VN.dbo.M_MATERIAL AS MM WITH (NOLOCK)
							                        ON EPIRBO.CHILD_ITEM_CD = MM.ITEM_CD
							                        AND MM.ITEM_ACCT <> '25'
			                        ) AS EPIRBO
			                        LEFT OUTER JOIN (
				                        SELECT ROW_NUMBER() OVER (PARTITION BY RSH.Rm_Order ORDER BY RSH.Rm_StockHist) AS RowNumber
					                         , RSH.Rm_Order
					                         , RSH.Rm_OrderSeq
					                         , RSH.Rm_Material
					                         , RSH.Rm_BarCode
					                         , RSH.Rm_StockQty                                                         AS StockOutQty
				                        FROM Rm_StockHist AS RSH WITH (NOLOCK)
				                        WHERE 
					                        RSH.Rm_IO_Type = 'OUT'
					                        AND RSH.Rm_Bunch = 'MOVE'
				                        ) AS RSH
				                        ON EPIRBO.PRODT_ORDER_NO = RSH.Rm_Order
					                        AND EPIRBO.SEQ = RSH.Rm_OrderSeq
					                        AND EPIRBO.CHILD_ITEM_CD = RSH.Rm_Material
			                        LEFT OUTER JOIN (
				                        SELECT ROW_NUMBER() OVER (PARTITION BY RSH.Rm_Order ORDER BY RSH.Rm_StockHist) AS RowNumber
					                         , RSH.Rm_Order
					                         , RSH.Rm_OrderSeq
					                         , RSH.Rm_Material
					                         , RSH.Rm_BarCode
					                         , RSH.Rm_StockQty                                                         AS StockReturnQty
				                        FROM Rm_StockHist AS RSH WITH (NOLOCK)
				                        WHERE 
					                        RSH.Rm_IO_Type = 'IN'
					                        AND RSH.Rm_Bunch = 'RETURN'
			                        ) AS RSH_R
			                        ON EPIRBO.PRODT_ORDER_NO = RSH_R.Rm_Order
			                        AND EPIRBO.SEQ = RSH_R.Rm_OrderSeq
			                        AND EPIRBO.CHILD_ITEM_CD = RSH_R.Rm_Material
	                        ) AS T
                        WHERE 
	                        GroupTotalStockOutQty - GroupTotalReportedQty < CAVITY_QTY
                 )
                  BEGIN
                    SELECT 0
                  END
                ELSE
                  BEGIN
                    SELECT 1
                  END;
                ";
                return Convert.ToBoolean(DbAccess.Default.ExecuteScalar(query));
            }
            catch (Exception exception)
            {
                InsertIntoSysLog("E", exception.Message, ActiveValues.Workcenter);
                MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + exception.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
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
                                string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";

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
                                // var lastRow = DbAccess.Default.GetDataRow
                                //     (
                                //      $@"
                                //          SELECT TOP 1 Material, WorkOrder
                                //           FROM ActiveJobHist WTIH (NOLOCK)
                                //          WHERE ActiveStatus = 'End'
                                //            AND WorkCenter = '{ActiveValues.Workcenter}'
                                //            
                                //          ORDER BY Updated DESC
                                //         ;
                                //         "
                                //     );
                                var mountingChangeCaseType = CustomEnum.MountingChangeCaseType.None;
                                // if (lastRow != null)
                                // {
                                //     string lastWorkOrder = lastRow["WorkOrder"] as string;
                                //     string lastMaterial = lastRow["Material"] as string;
                                //     if (ActiveValues.Material.Equals(lastMaterial))
                                //     {
                                //         e.PassUserConfirm = true;
                                //         string message = "Đây là chỉ thị của Model tương tự với chỉ thị trước đó。Bạn có muốn tiếp tục với thông tin thao tác này hay không?\r\n(This is the order of the same model as the previous order. Do you want to continue with the work information?)";
                                //         var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                //
                                //         switch (result)
                                //         {
                                //             case DialogResult.Yes:
                                //                 mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Continue;
                                //                 break;
                                //             case DialogResult.No:
                                //                 mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;
                                //                 break;
                                //             case DialogResult.Cancel:
                                //                 e.Cancel = true;
                                //                 return;
                                //         }
                                //         ChangeFeederWipStatus(mountingChangeCaseType, lastWorkOrder, thresholdQty);
                                //         InsertIntoSysLog("N", $"Start WorkOrder({ActiveValues.WorkOrder})\r\nFeederWip Change({Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)})", ActiveValues.Workcenter);
                                //         return;
                                //     }
                                //
                                // }

                                //동일라인에서 이전에 작업한 작업지시의 경우(FeederWip 유무)
                                if (DbAccess.Default.IsExist("FeederWip", $@"1 = 1 AND WorkOrder = '{ActiveValues.WorkOrder}' AND WorkCenter = dbo.GetLinkWorkCenter('{ActiveValues.Workcenter}')") > 0)
                                {
                                    // e.PassUserConfirm = true;
                                    // //수삽일 경우
                                    // if (ActiveValues.Routing.Equals("Mi_Load"))
                                    // {
                                    //     var message = "Bạn có muốn tiếp tục với thông tin thao tác này không?\r\n(Do you want to continue with the work information?)";
                                    //
                                    //     var result = System.Windows.Forms.MessageBox.Show(message, "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    //     switch (result)
                                    //     {
                                    //         case DialogResult.Yes:
                                    //             mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Import;
                                    //             break;
                                    //         case DialogResult.No:
                                    //             mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;
                                    //             break;
                                    //         case DialogResult.Cancel:
                                    //             e.Cancel = true;
                                    //             return;
                                    //     }
                                    //
                                    //     ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty);
                                    //     InsertIntoSysLog("N", $"Start WorkOrder({ActiveValues.WorkOrder})\r\nFeederWip Change({Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)})", ActiveValues.Workcenter);
                                    //     return;
                                    // }

                                    mountingChangeCaseType = CustomEnum.MountingChangeCaseType.ReCreate;

                                    ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty);
                                    InsertIntoSysLog("N", $"Start WorkOrder({ActiveValues.WorkOrder})\r\nFeederWip Change({Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)})", ActiveValues.Workcenter);
                                    return;
                                }

                                mountingChangeCaseType = CustomEnum.MountingChangeCaseType.Create;
                                ChangeFeederWipStatus(mountingChangeCaseType, "", thresholdQty);
                                InsertIntoSysLog("N", $"Start WorkOrder({ActiveValues.WorkOrder})\r\nFeederWip Change({Enum.GetName(typeof(CustomEnum.MountingChangeCaseType), mountingChangeCaseType)})", ActiveValues.Workcenter);
                                break;
                            case "Pk_Boxing":
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"Máy tính của công đoạn không thể bắt đầu được thao tác. \r\n" + $"작업시작을 할 수 없는 공정의 PC입니다.");
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
                                query = $@"
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
                                          INTO BoxingHist (
                                                            WorkOrder
                                                          , WorkCenter
                                                          , PcbBcd
                                                          , BoxBcd
                                                          , PalletBcd
                                                          , Created
                                                          , Updated
                                                          , Ended
                                                          )
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
                                    System.Windows.Forms.MessageBox.Show($"Không thể hoàn thành chỉ thị thao tác do có Pallet chưa hoàn thiện。 \r\n" + $"The work order cannot be completed because there are incomplete pallets.");
                                }

                                break;
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"Máy tính của công đoạn không thể hủy được thao tác \r\n" + $"작업취소을 할 수 없는 공정의 PC입니다.");
                                break;
                        }

                        break;
                    case WeJobControlAction.WorkOrderSelect:
                        if (!VerifyWorkOrder(ActiveValues.WorkOrder, ActiveValues.Routing))
                        {
                            e.Cancel = true;
                            System.Windows.Forms.MessageBox.Show($"Không thể chọn chỉ thị thao tác\r\nThe work order cannot be selected.", "cannot be selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    System.Windows.Forms.MessageBox.Show($"Không thể hoàn thành chỉ thị thao tác do có Pallet chưa hoàn thiện。 \r\n" + $"The work order cannot be completed because there are incomplete pallets.");
                                }

                                break;
                            case "Pk_StockIn":
                                break;
                            default:
                                e.Cancel = true;
                                System.Windows.Forms.MessageBox.Show($"Máy tính của công đoạn không thể hủy được thao tác \r\n" + $"작업취소을 할 수 없는 공정의 PC입니다.");
                                break;
                        }

                        break;
                }
            }
            catch (Exception exception)
            {
                InsertIntoSysLog("E", exception.Message, ActiveValues.Workcenter);
                MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + exception.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        // protected override void OpenCustomJobWorkers(OpenCustomJobWorkersEventArgs e)
        // {
        //     WiseM.Client.WorkerSelect.WorkerSelect ws = new WiseM.Client.WorkerSelect.WorkerSelect(WiseApp.Id, WbtCustomService.ActiveValues.Workcenter);
        //     ws.SaveButton_Click += new WiseM.Client.WorkerSelect.WorkerSelect.OnEvent(ws_SaveButton_Click);
        //     int result = Convert.ToInt32(ws.ShowDialog());
        //
        //     if (result == 5)
        //     {
        //         e.OpenDefaultSelect = true;
        //     }
        //     else if (result == 2)
        //     {
        //         e.Cancel = true;
        //     }
        //     else
        //     {
        //         e.OpenDefaultSelect = false;
        //         e.Cancel = false;
        //         e.SelectedWorkers.Clear();
        //
        //         foreach (DataRow item in wsList.Rows)
        //         {
        //             e.SelectedWorkers.Add(item["Worker"].ToString());
        //         }
        //
        //     }
        // }
        //
        // DataTable wsList = null;
        // void ws_SaveButton_Click(DataTable Workers)
        // {
        //     wsList = Workers;
        // }

        protected override void OpenCustomJobWorkers(OpenCustomJobWorkersEventArgs e)
        {
            e.SelectedWorkers = new WList<string>();
            e.Cancel = true;
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
                $"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('{type}',  'Client', '{WiseApp.Id}', LEFT(ISNULL(N'{strMsg}',''),3000), '{strWorkCenter}', GetDate())"
            );
        }
    }
}