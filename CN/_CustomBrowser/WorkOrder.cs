using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class WorkOrder : SkinForm
    {
        private int _startSeq;

        public WorkOrder()
        {
            InitializeComponent();
        }

        private void WorkOrder_Load(object sender, EventArgs e)
        {
            //tabControl1.TabPages.Remove(tp_OrderCreate);

            #region W/O Confirmation

            this.dgv_Confirm.Columns["colCheck"].HeaderText = "";
            this.dgv_Confirm.Columns["colConfirmed"].HeaderText = "Confirmed";
            this.dgv_Confirm.Columns["colWorkOrder"].HeaderText = "Work Order";
            this.dgv_Confirm.Columns["colMaterial"].HeaderText = "Material";
            this.dgv_Confirm.Columns["colRouting"].HeaderText = "Routing";
            this.dgv_Confirm.Columns["colWorkCenter"].HeaderText = "Work Center";
            this.dgv_Confirm.Columns["colShift"].HeaderText = "Shift";
            this.dgv_Confirm.Columns["colQty"].HeaderText = "Qty";
            this.dgv_Confirm.Columns["colConfirmedDate"].HeaderText = "Confirm Date";

            #endregion

            #region W/O Generation

            try
            {
                SetCombo_Routing();
                SetCombo_Material();
                SetCombo_DayNight();
                tableLayoutPanel3.SetColumnSpan(cb_Material, 3);
                dateTimePicker_PlanStart.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.WorkOrder_Load', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("Failed to find information.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }

            #endregion
        }

        #region W/O Confirmation

        // 작업지시 조회
        private void btn_Search_Click(object sender, EventArgs e)
        {
            // 컨펌 저장되지 않은 데이터가 있는지 확인
            if (this.dgv_Confirm.Rows.Count > 0)
            {
                if (dgv_Confirm.Rows.Cast<DataGridViewRow>().Any(row => Convert.ToBoolean(row.Cells["colConfirmed"].Value).Equals(false)))
                {
                    if (System.Windows.Forms.MessageBox.Show("存在未确认项目！\r\n继续时不会保存。\r\n你想继续吗？\r\n\r\n" + "Exist unconfirmed item!\r\nIt will not be saved when you continue.\r\nWould you like to continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            // 컨펌 정보 클리어
            lbl_SelectedWorkOrder.Text = "-";
            lbl_routing.Text = "-";
            cb_DayNight_1.DataSource = null;
            cb_Line.DataSource = null;
            num_Qty.Value = 0;
            dgv_Confirm.Rows.Clear();

            string startDate = chk_StartDate.Checked ? dtp_StartDate.Value.AddDays(-1).ToString("yyyy-MM-dd 23:59:59") : "1970-01-01 00:00:00";
            string endDate = chk_EndDate.Checked ? dtp_EndDate.Value.AddDays(1).ToString("yyyy-MM-dd 00:00:00") : "9999-12-31 00:00:00";
            string workOrder = tb_WorkOrder.Text;

            string query = string.Empty;

            // ERP 작업지시 조회

            query = $@"
                    SELECT EPPO.PRODT_ORDER_NO COLLATE SQL_Latin1_General_CP1_CI_AS        AS WorkOrder
                         , EPPO.ITEM_CD                                                    AS Material
                         , M.Spec                                                          AS Spec
                         , R.Routing                                                       AS Routing
                         , COALESCE(EPPO.WC_CD, '')                                        AS WC_CD
                         , MPWC.WC_NM
                         , EPPO.ROUT_SEQ                                                   AS ROUT_SEQ
                         , EPPO.ROUT_SEQ_NM                                                AS ROUT_SEQ_NM
                         , CONVERT(INT, EPPO.ORDER_QTY)                                    AS [Order Qty]
                         , COALESCE(ConfirmQty, 0)                                         AS ConfirmQty
                         , IIF(( EPPO.ORDER_QTY - COALESCE(ConfirmQty, 0) ) <= 0, 'Y', '') AS Ordered
                         , CONVERT(VARCHAR(20), EPPO.PLAN_START_DT, 23)                    AS [Plan Start Date]
                         , CONVERT(VARCHAR(20), EPPO.PLAN_COMPT_DT, 23)                    AS [Plan End Date]
                      FROM (
                           SELECT ROW_NUMBER() OVER (PARTITION BY EPPO.PRODT_ORDER_NO ORDER BY EPPO.IF_TIME DESC) AS RowNumber
                                , EPPO.IF_ID
                                , EPPO.PRODT_ORDER_NO
                                , EPPO.ITEM_CD
                                , EPPO.ORDER_QTY
                                , EPPO.WC_CD
                                , EPPO.ROUT_SEQ
                                , EPPO.ROUT_SEQ_NM
                                , CONVERT(VARCHAR(20), EPPO.PLAN_START_DT, 23)                               AS PLAN_START_DT
                                , CONVERT(VARCHAR(20), EPPO.PLAN_COMPT_DT, 23)                               AS PLAN_COMPT_DT
                                , EPPO.I_APPLY_STATUS
                                , EPPO.I_PROC_STEP
                                , EPPO.APPLY_FLAG
                             FROM MES_IF_CN.dbo.ETM_P_PROD_ORDER EPPO
                            WHERE 1 = 1
                              AND EPPO.PRODT_ORDER_NO LIKE '%{workOrder}%'
                           )                        AS                   EPPO
                           LEFT OUTER JOIN MES_IF_CN.dbo.M_P_WORK_CENTER MPWC
                                           ON EPPO.WC_CD = MPWC.WC_CD
                           LEFT OUTER JOIN(
                                          SELECT DISTINCT
                                                 Routing
                                               , ERP_ROUT_SEQ
                                            FROM Routing
                                           WHERE Kind = '1'
                                          )         AS                   R
                                          ON EPPO.ROUT_SEQ = R.ERP_ROUT_SEQ
                           LEFT OUTER JOIN Material AS                   M
                                           ON EPPO.ITEM_CD = M.Material
                           LEFT OUTER JOIN (
                                           SELECT WO.Routing
                                                , WO.ERP_WorkOrder
                                                , SUM(WO.OrderQty) AS ConfirmQty
                                             FROM WorkOrder WO
                                            GROUP BY
                                                WO.Routing
                                              , WO.ERP_WorkOrder
                                           )                             Y
                                           ON EPPO.PRODT_ORDER_NO = Y.ERP_WorkOrder
                                               AND R.Routing = Y.Routing
                     WHERE 1 = 1
                       AND EPPO.RowNumber = 1
                       AND EPPO.I_PROC_STEP IN ('C', 'U')
                       AND EPPO.I_APPLY_STATUS IN ( 'R' )
                       --  AND EPPO.APPLY_FLAG IN ( 'N' )
                       AND EPPO.PLAN_START_DT >= '{startDate}'
                       AND EPPO.PLAN_START_DT < '{endDate}'
                       AND M.Spec LIKE '%{tb_Model.Text}%'
                     ORDER BY
                         IF_ID
                    ;
                   ";

            DataTable dt = null;

            try
            {
                dt = DbAccess.Default.GetDataTable(query);
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.btn_Search_Click', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("Search Fail!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            this.dgv_Search.DataSource = dt;
            if (dgv_Search.Rows.Count <= 0) return;
            this.dgv_Search.Columns["WorkOrder"].Width = 130;
            this.dgv_Search.Columns["Material"].Width = 100;
            this.dgv_Search.Columns["Spec"].Width = 250;
            this.dgv_Search.Columns["Plan Start Date"].Width = 140;
            this.dgv_Search.Columns["Plan End Date"].Width = 140;
            this.dgv_Search.Columns["Routing"].Width = 100;
            this.dgv_Search.Columns["ROUT_SEQ"].Width = 80;
            this.dgv_Search.Columns["ROUT_SEQ_NM"].Width = 100;
            this.dgv_Search.Columns["Ordered"].Width = 60;
            this.dgv_Search.Columns["Ordered"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgv_Search.Columns["ConfirmQty"].DefaultCellStyle.Format = "#,##0.#";
            this.dgv_Search.Columns["ConfirmQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgv_Search.Columns["ORDER QTY"].DefaultCellStyle.Format = "#,##0.#";
            this.dgv_Search.Columns["ORDER QTY"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // 상단 그리드에서 선택한 작업지시를 아래 컨펌 작업으로 지정
        private void dgv_Search_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BindingSelectOrder(dgv_Search.CurrentCell.OwningRow);
        }

        private void BindingSelectOrder(DataGridViewRow dataGridViewRow)
        {
            if (dataGridViewRow == null) return;
            dgv_Confirm.Rows.Clear();

            string workOrder = dataGridViewRow.Cells["WorkOrder"].Value.ToString();
            string routing = dataGridViewRow.Cells["Routing"].Value.ToString();
            string query = string.Empty;

            _startSeq = (int) DbAccess.Default.ExecuteScalar
                (
                 $@"
                SELECT COALESCE(MAX(CONVERT(INT, LEFT(WO.Split, 3))), 0) + 1 AS StartSeq
                  FROM WorkOrder AS WO
                 WHERE WO.ERP_WorkOrder = '{workOrder}'
                   AND TRY_CONVERT(INT, LEFT(WO.Split, 3)) IS NOT NULL
                ;
                "
                );

            cb_Line.DataSource = null;
            cb_Line.Items.Clear();
            lbl_routing.Text = routing;

            switch (dataGridViewRow.Cells["WC_CD"].Value as string)
            {
                case "C0009":
                    query += $@"
                    SELECT Common
                         , CONCAT(Common, ': ', Text) AS DisplayMember
                      FROM Common
                     WHERE Category = '100'
                       AND Common IN ('D', 'N')
                     ORDER BY
                         Common
                    ;";

                    query += $@"
                    SELECT WC.WorkCenter
                         , WC.Text
                      FROM WorkCenter WC
                     WHERE WC.Routing = '{routing}'
                       AND COALESCE(WC.ERP_WC_CD, '') IN ('C0009')
                       AND WC.Status = 1
                    ;";

                    num_Qty.Value = (int) dataGridViewRow.Cells["Order Qty"].Value;
                    num_Qty.Enabled = false;
                    break;
                case "C0010":
                case "C0058":
                    query += $@"
                    SELECT Common
                         , CONCAT(Common, ': ', Text) AS DisplayMember
                      FROM Common
                     WHERE Category = '100'
                       AND Common IN ('D')
                     ORDER BY
                         Common
                    ;";

                    query += $@"
                    SELECT WC.WorkCenter
                         , WC.Text
                      FROM WorkCenter WC
                     WHERE WC.Routing = '{routing}'
                       AND COALESCE(WC.ERP_WC_CD, '') IN ('C0010', 'C0058')
                       AND WC.Status = 1
                    ;";

                    num_Qty.Value = (int) dataGridViewRow.Cells["Order Qty"].Value;
                    num_Qty.Enabled = false;
                    break;
                default:
                    query += $@"
                    SELECT Common
                         , CONCAT(Common, ': ', Text) AS DisplayMember
                      FROM Common
                     WHERE Category = '100'
                       AND Common IN ('D', 'N')
                     ORDER BY
                         Common
                    ;";

                    query += $@"
                    SELECT WC.WorkCenter
                         , WC.Text
                      FROM WorkCenter WC
                     WHERE WC.Routing = '{routing}'
                       AND COALESCE(WC.ERP_WC_CD, '') NOT IN ('C0009', 'C0010', 'C0058')
                       AND WC.Status = 1
                    ;";

                    num_Qty.Enabled = true;
                    break;
            }

            try
            {
                var dataSet = DbAccess.Default.GetDataSet(query);

                if (dataSet != null
                    && dataSet.Tables.Count == 2)
                {
                    // 콤보박스에 주야 정보 삽입
                    {
                        dataSet.Tables[0].Rows.InsertAt(dataSet.Tables[0].NewRow(), 0);

                        this.cb_DayNight_1.DataSource = dataSet.Tables[0];
                        this.cb_DayNight_1.DisplayMember = "DisplayMember";
                        this.cb_DayNight_1.ValueMember = "Common";
                    }

                    {
                        dataSet.Tables[1].Rows.InsertAt(dataSet.Tables[1].NewRow(), 0);

                        this.cb_Line.DataSource = dataSet.Tables[1];
                        this.cb_Line.DisplayMember = "Text";
                        this.cb_Line.ValueMember = "WorkCenter";

                        this.cb_Line.SelectedIndex = 0;
                    }

                    // 컨펌 작업의 기준 작업지시로 표시
                    this.lbl_SelectedWorkOrder.Text = workOrder;
                }
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.BindingSelectOrder', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("Information inquiry failed!\r\n\r\nPlease select the work order again.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            query = string.Empty;

            query = $@"
                    SELECT 'True' AS Confirmed
                         , WO.WorkOrder
                         , WO.Material
                         , WO.Routing
                         , WO.WorkCenter
                         , WO.Shift
                         , WO.OrderQty
                         , WO.Released
                         , WO.Created
                      FROM WorkOrder AS WO
                     WHERE WO.ERP_WorkOrder = '{workOrder}'
                       AND WO.Routing = '{routing}'
                    ;
                    "
                ;

            try
            {
                var dataTable = DbAccess.Default.GetDataTable(query);

                if (dataTable == null
                    || dataTable.Rows.Count <= 0) return;
                foreach (DataRow row in dataTable.Rows)
                {
                    dgv_Confirm.Rows.Add
                        (
                         false
                       , row["Confirmed"]
                       , row["WorkOrder"]
                       , row["Material"]
                       , row["Routing"]
                       , row["WorkCenter"]
                       , row["Shift"]
                       , row["OrderQty"]
                       , row["Released"]
                       , row["Created"]
                        );
                }
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.BindingSelectOrder', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
            }
        }

        // 컨펌 정보를 하단 그리드에 추가
        private void btn_Add_WorkCenter_Click(object sender, EventArgs e)
        {
            // 상단 작업지시 선택 여부 확인
            if (string.IsNullOrEmpty(this.lbl_SelectedWorkOrder.Text))
            {
                System.Windows.Forms.MessageBox.Show("请选择工作指令。\r\n" + "Please select Work Order.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // 주야 선택 여부 확인
            if (string.IsNullOrEmpty(Convert.ToString(this.cb_DayNight_1.SelectedValue)))
            {
                System.Windows.Forms.MessageBox.Show("请选择白天/夜晚。\r\n" + "Please select Day/Night.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.cb_DayNight_1.Focus();
                return;
            }

            //Line 선택 여부 확인
            if (string.IsNullOrEmpty(Convert.ToString(this.cb_Line.SelectedValue)))
            {
                System.Windows.Forms.MessageBox.Show("请选择工作线。\r\n" + "Please select Line.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.cb_Line.Focus();
                return;
            }

            // 수량 입력 여부 확인
            if (this.num_Qty.Value <= 0)
            {
                System.Windows.Forms.MessageBox.Show("请选择数量。\r\n" + "Please select quantity.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.num_Qty.Focus();
                return;
            }

            string selectedERPWorkOrder = this.lbl_SelectedWorkOrder.Text;

            // 컨펌 추가 가능한 잔여 수량
            decimal remainQty = 0;

            // 상단 그리드에서 총수량 가져오기
            //remainQty = decimal.Parse(this.dgv_Search.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["WorkOrder"].Value.Equals(selectedERPWorkOrder)).FirstOrDefault().Cells["ORDER QTY"].Value.ToString());
            DataGridViewRow first = null;
            foreach (var row in this.dgv_Search.Rows)
            {
                if (!(row is DataGridViewRow r)) continue;
                if (!r.Cells["WorkOrder"].Value.Equals(selectedERPWorkOrder)) continue;
                first = r;
                break;
            }

            remainQty = decimal.Parse(first?.Cells["ORDER QTY"].Value.ToString() ?? string.Empty);

            // 하단 그리드에 컨펌 추가된 Row가 있으면 수량 차감
            //this.dgv_Confirm.Rows.OfType<DataGridViewRow>().ToList().ForEach(r => { remainQty -= long.Parse(r.Cells["colQty"].Value.ToString()); });
            foreach (var r in this.dgv_Confirm.Rows.OfType<DataGridViewRow>().ToList())
            {
                //string gridWorkOrder = r.Cells["colWorkOrder"].Value.ToString();
                ////현재 WorkOrder와 그리드 WorkOrder가 같지 않으면 continue
                //if (!gridWorkOrder.Substring(0, 14).Equals(selectedERPWorkOrder))
                //{
                //    continue;
                //}
                remainQty -= long.Parse(r.Cells["colQty"].Value.ToString());
            }

            // 남은 수량이 없거나, 컨펌하려는 수량이 많으면 리턴
            if (remainQty <= 0
                || remainQty < this.num_Qty.Value)
            {
                System.Windows.Forms.MessageBox.Show("数量超过！\r\n" + "Quantity Over!", "失败(Fail)", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.num_Qty.Focus();
                return;
            }


            // 새 MES 작업지시 생성하기
            // 기준 작업지시를 변형
            //string workOrderBase = string.Format("SD{0}", this.lbl_SelectedWorkOrder.Text.Substring(2));

            //PD202209160001 현재 작지
            string workOrderBase = this.lbl_SelectedWorkOrder.Text.Trim();
            if (!(cb_Line.SelectedValue is string workCenter)) return;

            string line = workCenter.Substring(4, 1);
            string routing = lbl_routing.Text;
            string shift = this.cb_DayNight_1.SelectedValue.ToString();

            string load_unload = "L";
            switch (lbl_routing.Text)
            {
                case "Ai_Unload":
                case "St_Unload":
                    load_unload = "U";
                    break;
                case "Pk_Boxing":
                    load_unload = "B";
                    break;
                case "Pk_StockIn":
                    load_unload = "S";
                    break;
                case "Mi_Assy1":
                    load_unload = "A";
                    break;
                default:
                    break;
            }

            // 작업지시
            string workOrder = workOrderBase + _startSeq++.ToString("000") + line + load_unload + shift;

            if (this.dgv_Confirm.Rows.Count > 0)
            {
                // 작업지시 중복 체크
                if (dgv_Confirm.Rows.Cast<DataGridViewRow>().Any(row => row.Cells["colWorkOrder"].Value.Equals(workOrder)))
                {
                    System.Windows.Forms.MessageBox.Show("已经注册了一个工作订单。\r\nAlready added work order.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 작업지시 두 개째 생성 시 ERP 작업지시에 수량을 초과하는지 확인
                if (remainQty < this.num_Qty.Value)
                {
                    System.Windows.Forms.MessageBox.Show("已超出当前工单数量。\r\nThe current work order quantity has been exceeded.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.num_Qty.Focus();
                    return;
                }
            }

            string material = this.dgv_Search.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["WorkOrder"].Value.Equals(this.lbl_SelectedWorkOrder.Text)).Select(r => Convert.ToString(r.Cells["Material"].Value)).FirstOrDefault();

            // 하단 그리드 추가
            int rowIndex = this.dgv_Confirm.Rows.Add();

            this.dgv_Confirm.Rows[rowIndex].Cells["colCheck"].Value = false;
            this.dgv_Confirm.Rows[rowIndex].Cells["colConfirmed"].Value = false;
            this.dgv_Confirm.Rows[rowIndex].Cells["colWorkOrder"].Value = workOrder;
            this.dgv_Confirm.Rows[rowIndex].Cells["colMaterial"].Value = material;
            this.dgv_Confirm.Rows[rowIndex].Cells["colRouting"].Value = routing;
            this.dgv_Confirm.Rows[rowIndex].Cells["colWorkCenter"].Value = workCenter;
            this.dgv_Confirm.Rows[rowIndex].Cells["colShift"].Value = shift;
            this.dgv_Confirm.Rows[rowIndex].Cells["colQty"].Value = this.num_Qty.Value;
            this.dgv_Confirm.Rows[rowIndex].Cells["PlanedStart"].Value = dateTimePicker_PlanStart.Value;
            this.dgv_Confirm.Rows[rowIndex].Cells["colConfirmedDate"].Value = DateTime.Today;
        }

        private void checkAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dgv_Confirm.Rows)
            {
                if (Convert.ToBoolean(row.Cells["colConfirmed"].Value).Equals(false))
                {
                    row.Cells["colCheck"].Value = this.checkAll.Checked;
                }
            }
        }

        private void dgv_Confirm_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.dgv_Confirm.EndEdit();

            if (this.dgv_Confirm.Rows.Count <= 0) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0
                || !this.dgv_Confirm.Columns[e.ColumnIndex].Name.Equals("colCheck")) return;
            if (Convert.ToBoolean(this.dgv_Confirm.Rows[e.RowIndex].Cells["colConfirmed"].Value).Equals(true))
            {
                this.dgv_Confirm.Rows[e.RowIndex].Cells["colCheck"].Value = false;
            }
            else
            {
                List<bool> colCheckList = this.dgv_Confirm.Rows.OfType<DataGridViewRow>().Where(r => Convert.ToBoolean(r.Cells["colConfirmed"].Value).Equals(false)).Select(r => Convert.ToBoolean(r.Cells["colCheck"].Value)).Distinct().ToList();
                this.checkAll.Checked = colCheckList.Count == 1 && colCheckList[0];
            }
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            if (dgv_Confirm.Rows.Count > 0)
            {
                if (System.Windows.Forms.MessageBox.Show("你确定吗？\r\nAre you Sure?", "注意 (Notice)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                var query = new StringBuilder();

                var notSavedRows = new List<DataGridViewRow>();

                double qty = 0;
                foreach (DataGridViewRow row in dgv_Confirm.Rows)
                {
                    if (Convert.ToBoolean(row.Cells["colConfirmed"].Value).Equals(false))
                    {
                        if (Convert.ToBoolean(row.Cells["colCheck"].Value).Equals(true))
                        {
                            qty += Convert.ToDouble(row.Cells["colQty"].Value);

                            query.AppendLine
                                (
                                 $@"
                                INSERT
                                  INTO WorkOrder
                                      (
                                          WorkOrder
                                      ,   Division
                                      ,   No
                                      ,   Split
                                      ,   Material
                                      ,   Routing
                                      ,   Workcenter
                                      ,   OrderQty
                                      ,   Created
                                      ,   Status
                                      ,   Shift
                                      ,   PlanedStart
                                      ,   Released
                                      ,   ActiveStatus
                                      ,   BeginActiveStatus
                                      ,   ERP_WorkOrder
                                      ,   WorkerCount
                                      ,   WorkTime
                                      )
                                VALUES
                                    (
                                        'S{row.Cells["colWorkOrder"].Value.ToString().Substring(1)}'
                                    ,   (
                                    SELECT TOP 1
                                           Division
                                      FROM WorkCenter
                                     WHERE WorkCenter = '{row.Cells["colWorkCenter"].Value}'
                                        )
                                    ,   'S{row.Cells["colWorkOrder"].Value.ToString().Substring(1, 13)}'
                                    ,   '{row.Cells["colWorkOrder"].Value.ToString().Substring(14)}'
                                    ,   '{row.Cells["colMaterial"].Value}'
                                    ,   '{row.Cells["colRouting"].Value}'
                                    ,   '{row.Cells["colWorkCenter"].Value}'
                                    ,   '{row.Cells["colQty"].Value}'
                                    ,   GETDATE()
                                    ,   1
                                    ,   '{row.Cells["colShift"].Value}'
                                    ,   CONVERT(DATE, '{row.Cells["PlanedStart"].Value:yyyy-MM-dd}')
                                    ,   CONVERT(DATE, '{row.Cells["PlanedStart"].Value:yyyy-MM-dd}')
                                    ,   'Release'
                                    ,   'Release'
                                    ,   '{lbl_SelectedWorkOrder.Text}'
                                    ,   1
                                    ,   1
                                    )
                                ;
                                "
                                );
                        }
                        else
                        {
                            notSavedRows.Add(row);
                        }
                    }
                    else
                    {
                        qty += Convert.ToDouble(row.Cells["colQty"].Value);
                    }
                }

                //WorkOrder 확정 부분 ERP 보고 쿼리
                query.AppendLine
                    (
                     $@"
                    UPDATE MES_IF_CN.dbo.ETM_P_PROD_ORDER
                       SET APPLY_FLAG = 'Y'
                         , APPLY_TIME = GETDATE()
                     WHERE PRODT_ORDER_NO = '{lbl_SelectedWorkOrder.Text.Trim()}'
                       AND COALESCE(APPLY_FLAG, 'N') = 'N'
                    "
                    );

                if (string.IsNullOrEmpty(query.ToString()))
                {
                    System.Windows.Forms.MessageBox.Show("请检查项目。\r\nPlease check item.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    DbAccess.Default.ExecuteQuery(query.ToString());

                    checkAll.Checked = false;

                    if (notSavedRows.Count > 0)
                    {
                        foreach (var row in notSavedRows)
                        {
                            dgv_Confirm.Rows.Add(row);
                        }

                        var rowsTemp = dgv_Confirm.Rows.OfType<DataGridViewRow>().OrderBy(r => Convert.ToString(r.Cells["colWorkOrder"].Value)).ToArray();

                        dgv_Confirm.Rows.Clear();

                        dgv_Confirm.Rows.AddRange(rowsTemp);
                    }

                    System.Windows.Forms.MessageBox.Show("保存完成。\r\nSave Complete.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.btn_Confirm_Click', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                    System.Windows.Forms.MessageBox.Show("保存错误。\r\nSave Error.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("没有添加任何工作订单。\r\nNo work orders have been added.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void chk_StartDate_CheckedChanged(object sender, EventArgs e)
        {
            this.dtp_StartDate.Enabled = this.chk_StartDate.Checked;
        }

        private void chk_EndDate_CheckedChanged(object sender, EventArgs e)
        {
            this.dtp_EndDate.Enabled = this.chk_EndDate.Checked;
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            var count = dgv_Confirm.Rows.Cast<DataGridViewRow>().Where(row => !Convert.ToBoolean(row.Cells["colConfirmed"].Value))
                                   .Count(row => Convert.ToBoolean(row.Cells["colCheck"].Value));
            if (count <= 0)
            {
                MessageBox.ShowCaption("没有要删除的行。\r\nThere are no lines to delete.", "注意 (Notice)", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("你确定吗？\r\nAre you Sure?", "注意 (Notice)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            for (int i = dgv_Confirm.Rows.Count - 1; i >= 0; i--)
            {
                if (Convert.ToBoolean(dgv_Confirm.Rows[i].Cells["colConfirmed"].Value)) continue;
                if (!Convert.ToBoolean(dgv_Confirm.Rows[i].Cells["colCheck"].Value)) continue;
                dgv_Confirm.Rows.Remove(dgv_Confirm.Rows[i]);
            }

            MessageBox.ShowCaption("删除完成。\r\nDelete Complete.", "注意 (Notice)", MessageBoxIcon.Asterisk);
        }

        #endregion

        #region W/O Generation

        private void SetCombo_Routing()
        {
            string query = string.Empty;

            query += "\r\n SELECT   Routing                         AS ValueMember ";
            query += "\r\n          ,CONCAT(Routing, ' / ', Text)   AS DisplayMember ";
            query += "\r\n FROM     Routing ";
            query += "\r\n WHERE    Status = 1 ";
            query += "\r\n      AND WorkStatus = 1 ";
            query += "\r\n      AND Kind = '1' ";
            query += "\r\n UNION ALL ";
            query += "\r\n SELECT   '', '' ";

            DataTable dt = DbAccess.Default.GetDataTable(query);

            if (dt == null
                || dt.Rows.Count <= 0) return;
            dt = dt.AsEnumerable().OrderBy(r => r["ValueMember"]).CopyToDataTable();
            this.cb_Routing.DataSource = dt;
            this.cb_Routing.DisplayMember = "DisplayMember";
            this.cb_Routing.ValueMember = "ValueMember";
        }

        private void SetCombo_Material()
        {
            string query = string.Empty;

            query += "\r\n SELECT   Material COLLATE Korean_Wansung_CI_AS AS ValueMember ";
            query += "\r\n          ,CONCAT(Material COLLATE Korean_Wansung_CI_AS, ' / ', Spec)  AS DisplayMember ";
            query += "\r\n FROM     Material ";
            query += "\r\n WHERE    Status = 1"; //and Customer is not null and Customer != '' ";
            query += "\r\n UNION ALL ";
            query += "\r\n SELECT   '', '' ";

            DataTable dt = DbAccess.Default.GetDataTable(query);
            if (dt == null
                || dt.Rows.Count <= 0) return;
            dt = dt.AsEnumerable().OrderBy(r => r["ValueMember"]).CopyToDataTable();

            this.cb_Material.DataSource = dt;
            this.cb_Material.DisplayMember = "DisplayMember";
            this.cb_Material.ValueMember = "ValueMember";
        }

        private void SetCombo_DayNight()
        {
            string query = string.Empty;

            query += "\r\n SELECT   Common                          AS ValueMember ";
            query += "\r\n          ,CONCAT(Common, ' : ', Text)    AS DisplayMember ";
            query += "\r\n FROM     Common ";
            query += "\r\n WHERE    Category = '100' ";
            query += "\r\n      AND Common IN ('D', 'N') ";
            query += "\r\n      AND Status = 1 ";

            var dataTable = DbAccess.Default.GetDataTable(query);
            if (dataTable == null || dataTable.Rows.Count <= 0) return;
            this.cb_DayNight_2.DataSource = dataTable;
            this.cb_DayNight_2.DisplayMember = "DisplayMember";
            this.cb_DayNight_2.ValueMember = "ValueMember";
        }

        private void cb_Routing_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.clb_WorkCenter.Items.Clear();

                string selectedRouting = Convert.ToString(this.cb_Routing.SelectedValue);

                if (string.IsNullOrEmpty(selectedRouting)) return;
                string query = string.Empty;

                query += "\r\n SELECT   WorkCenter ";
                query += "\r\n          ,Text ";
                query += "\r\n FROM     WorkCenter ";
                query += "\r\n WHERE    Routing = '" + selectedRouting.Split('/')[0].Trim() + "' ";
                query += "\r\n      AND Status = 1 ";
                query += "\r\n ORDER BY ViewSeq ";

                var dataTable = DbAccess.Default.GetDataTable(query);
                if (dataTable == null) return;
                foreach (DataRow row in dataTable.Rows)
                {
                    string workCenter = $"{row["WorkCenter"]} / {row["Text"]}";

                    if (!clb_WorkCenter.Items.Contains(workCenter)) clb_WorkCenter.Items.Add(workCenter);
                }
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.cb_Routing_SelectedIndexChanged', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("未找到任何信息。 请重新选择流程。\r\nFailed to find information.Please select routing again.", "失败 (Fail)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clb_WorkCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string workcenter = clb_WorkCenter.SelectedItem.ToString().Substring(0, 6);

            string Q = $@"SELECT RIGHT(Text, 1) Line FROM Workcenter WHERE WorkCenter = '{workcenter}'";
            DataTable dt = DbAccess.Default.GetDataTable(Q);

            tb_line.Text = dt.Rows[0]["Line"].ToString();
        }

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            #region 입력값 체크

            if (string.IsNullOrEmpty(Convert.ToString(this.cb_Routing.SelectedValue)) == true)
            {
                System.Windows.Forms.MessageBox.Show("请选择一个进程。\r\nPlease select Routing.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cb_Routing.Focus();
                return;
            }

            if (this.clb_WorkCenter.CheckedItems.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("请选择工作中心。\r\nPlease select WorkCenter.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.clb_WorkCenter.Focus();
                return;
            }

            if (string.IsNullOrEmpty(Convert.ToString(this.cb_Material.SelectedValue)) == true)
            {
                System.Windows.Forms.MessageBox.Show("请选择一种材料。\r\nPlease select Material.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cb_Material.Focus();
                return;
            }

            if (this.num_OrderQty.Value <= 0)
            {
                System.Windows.Forms.MessageBox.Show("请输入要订购的数量。\r\nPlease select Order Qty.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.num_OrderQty.Focus();
                return;
            }

            if (string.IsNullOrEmpty(Convert.ToString(this.cb_DayNight_2.SelectedValue)) == true)
            {
                System.Windows.Forms.MessageBox.Show("请选择白天/夜晚。\r\nPlease select Day/Night.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cb_DayNight_2.Focus();
                return;
            }

            //if (this.num_WorkerCount.Value <= 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("请选择工人人数。\r\nPlease select Worker Count.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    this.num_WorkerCount.Focus();
            //    return;
            //}

            //if (this.num_WorkTime.Value <= 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("请选择工作时间。\r\nPlease select Work Time.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    this.num_WorkTime.Focus();
            //    return;
            //}

            #endregion

            if (System.Windows.Forms.MessageBox.Show("你确定吗？\r\nAre you Sure?", "注意 (Notice)", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    string routing = Convert.ToString(this.cb_Routing.SelectedValue);

                    string material = Convert.ToString(this.cb_Material.SelectedValue);

                    string orderQty = this.num_OrderQty.Value.ToString();

                    string shift = Convert.ToString(this.cb_DayNight_2.SelectedValue);

                    string confirmDate = this.dtp_ReleaseDate.Value.ToString("yyyyMMdd");

                    string confirmDate_Hyphen = this.dtp_ReleaseDate.Value.ToString("yyyy-MM-dd");

                    string workerCount = "1";

                    string workTime = "1";

                    string lupb = routing.Substring(3, 1);

                    string seq = "0001";

                    StringBuilder query = new StringBuilder();

                    query.Append("\r\n DECLARE @WorkOrderSeq nvarchar(4) ");
                    query.Append("\r\n DECLARE @WorkOrder nvarchar(17) ");

                    foreach (object checkedItem in this.clb_WorkCenter.CheckedItems)
                    {
                        string workCenter = checkedItem.ToString().Split('/')[0].Trim();

                        string line = tb_line.Text;

                        string workOrderLike = $"SS{confirmDate}____{line}{lupb}{shift}";
                        string workOrder = $"SS{confirmDate}{seq}{line}{lupb}{shift}";
                        //SS202210180001AUD
                        query.Append("\r\n");
                        query.Append("\r\n SET @WorkOrderSeq = (SELECT TOP 1 SUBSTRING(WorkOrder, 11, 4) FROM WorkOrder WHERE WorkOrder LIKE '" + workOrderLike + "' ORDER BY WorkOrder DESC) ");
                        query.Append("\r\n");
                        query.Append("\r\n IF (ISNULL(@WorkOrderSeq, '') = '') ");
                        query.Append("\r\n BEGIN ");
                        query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                        query.Append("\r\n      VALUES ('" + workOrder + "' ");
                        query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                        query.Append("\r\n              ,'" + workOrder.Substring(0, 14) + "' ");
                        query.Append("\r\n              ,'" + workOrder.Substring(14, 3) + "' ");
                        query.Append("\r\n              ,'" + material + "' ");
                        //query.Append("\r\n              ,'" + Customer + "' ");
                        query.Append("\r\n              ,NULL "); //
                        query.Append("\r\n              ,'" + routing + "' ");
                        query.Append("\r\n              ,'" + workCenter + "' ");
                        query.Append("\r\n              ,'" + orderQty + "' ");
                        query.Append("\r\n              ,GETDATE() ");
                        query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                        query.Append("\r\n              ,'Release' ");
                        query.Append("\r\n              ,1 ");
                        query.Append("\r\n              ,'" + shift + "' ");
                        query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                        query.Append("\r\n              ," + workerCount + " ");
                        query.Append("\r\n              ," + workTime + " ");
                        query.Append("\r\n              ) ");
                        query.Append("\r\n END ");
                        query.Append("\r\n ELSE ");
                        query.Append("\r\n BEGIN ");
                        query.Append("\r\n      SET @WorkOrderSeq = (SELECT REPLICATE('0', 4 - LEN(CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) + (CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) ");
                        query.Append("\r\n      SET @WorkOrder = (SELECT 'SS" + confirmDate + "' + @WorkOrderSeq + '" + line + lupb + shift + "')");
                        query.Append("\r\n");
                        query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                        query.Append("\r\n      VALUES (@WorkOrder ");
                        query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                        query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 1, 14)) ");
                        query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 15, 3)) ");
                        query.Append("\r\n              ,'" + material + "' ");
                        //query.Append("\r\n              ,'" + Customer + "' ");
                        query.Append("\r\n              ,NULL "); //
                        query.Append("\r\n              ,'" + routing + "' ");
                        query.Append("\r\n              ,'" + workCenter + "' ");
                        query.Append("\r\n              ,'" + orderQty + "' ");
                        query.Append("\r\n              ,GETDATE() ");
                        query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                        query.Append("\r\n              ,'Release' ");
                        query.Append("\r\n              ,1 ");
                        query.Append("\r\n              ,'" + shift + "' ");
                        query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                        query.Append("\r\n              ," + workerCount + " ");
                        query.Append("\r\n              ," + workTime + " ");
                        query.Append("\r\n              ) ");
                        query.Append("\r\n END ");
                    }

                    #region 작업지시 두개생성

                    /*
                    foreach (object checkedItem in this.clb_WorkCenter.CheckedItems)
                    {
                        string workCenter = checkedItem.ToString().Split('/')[0].Trim();

                        string line = "1"; // 개발 시점에선 라인이 하나라서 1로 고정함. 추후 라인 추가 시 변경 필요.

                        string workOrderLike = "SS" + confirmDate + "____" + line + shift + "";

                        string workOrder = "SS" + confirmDate + seq + line + shift + "";

                        if (workCenter == "WeGb11")
                        {
                            for (int i = 0; i < 2; i++)
                            {
                                if (i == 0)
                                {
                                    // Gift_Box
                                    workOrderLike = "SS" + confirmDate + "____" + line + shift + "G" + "";
                                    workOrder = "SS" + confirmDate + seq + line + shift + "G" + "";

                                    query.Append("\r\n");
                                    query.Append("\r\n SET @WorkOrderSeq = (SELECT TOP 1 SUBSTRING(WorkOrder, 11, 4) FROM WorkOrder WHERE WorkOrder LIKE '" + workOrderLike + "' ORDER BY WorkOrder DESC) ");
                                    query.Append("\r\n");
                                    query.Append("\r\n IF (ISNULL(@WorkOrderSeq, '') = '') ");
                                    query.Append("\r\n BEGIN ");
                                    query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                                    query.Append("\r\n      VALUES ('" + workOrder + "' ");
                                    query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                                    query.Append("\r\n              ,'" + workOrder.Substring(0, 14) + "' ");
                                    query.Append("\r\n              ,'" + workOrder.Substring(14, 2) + "' ");
                                    query.Append("\r\n              ,'" + material + "' ");
                                    //query.Append("\r\n              ,'" + Customer  + "' ");
                                    query.Append("\r\n              ,NULL ");
                                    query.Append("\r\n              ,'" + routing + "' ");
                                    query.Append("\r\n              ,'" + workCenter + "' ");
                                    query.Append("\r\n              ,'" + orderQty + "' ");
                                    query.Append("\r\n              ,GETDATE() ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ,'Release' ");
                                    query.Append("\r\n              ,1 ");
                                    query.Append("\r\n              ,'" + shift + "' ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ," + workerCount + " ");
                                    query.Append("\r\n              ," + workTime + " ");
                                    query.Append("\r\n              ) ");
                                    query.Append("\r\n END ");
                                    query.Append("\r\n ELSE ");
                                    query.Append("\r\n BEGIN ");
                                    query.Append("\r\n      SET @WorkOrderSeq = (SELECT REPLICATE('0', 4 - LEN(CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) + (CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) ");
                                    query.Append("\r\n      SET @WorkOrder = (SELECT 'SS" + confirmDate + "' + @WorkOrderSeq + '" + line + shift + "G')");  //++
                                    query.Append("\r\n");
                                    query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                                    query.Append("\r\n      VALUES (@WorkOrder ");
                                    query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                                    query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 1, 14)) ");
                                    query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 15, 2)) ");
                                    query.Append("\r\n              ,'" + material + "' ");
                                    //query.Append("\r\n              ,'" + Customer + "' ");
                                    query.Append("\r\n              ,NULL ");
                                    query.Append("\r\n              ,'" + routing + "' ");
                                    query.Append("\r\n              ,'" + workCenter + "' ");
                                    query.Append("\r\n              ,'" + orderQty + "' ");
                                    query.Append("\r\n              ,GETDATE() ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ,'Release' ");
                                    query.Append("\r\n              ,1 ");
                                    query.Append("\r\n              ,'" + shift + "' ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ," + workerCount + " ");
                                    query.Append("\r\n              ," + workTime + " ");
                                    query.Append("\r\n              ) ");
                                    query.Append("\r\n END ");
                                }
                                else if (i == 1)
                                {
                                    // Mate-In
                                    workOrderLike = "SS" + confirmDate + "____" + line + shift + "M" + "";
                                    workOrder = "SS" + confirmDate + seq + line + shift + "M" + "";
                                    workCenter = "WeMa11";
                                    routing = "Match";

                                    query.Append("\r\n");
                                    query.Append("\r\n SET @WorkOrderSeq = (SELECT TOP 1 SUBSTRING(WorkOrder, 11, 4) FROM WorkOrder WHERE WorkOrder LIKE '" + workOrderLike + "' ORDER BY WorkOrder DESC) ");
                                    query.Append("\r\n");
                                    query.Append("\r\n IF (ISNULL(@WorkOrderSeq, '') = '') ");
                                    query.Append("\r\n BEGIN ");
                                    query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                                    query.Append("\r\n      VALUES ('" + workOrder + "' ");
                                    query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                                    query.Append("\r\n              ,'" + workOrder.Substring(0, 14) + "' ");
                                    query.Append("\r\n              ,'" + workOrder.Substring(14, 2) + "' ");
                                    query.Append("\r\n              ,'" + material + "' ");
                                    //query.Append("\r\n              ,'" + Customer  + "' ");
                                    query.Append("\r\n              ,NULL ");
                                    query.Append("\r\n              ,'" + routing + "' ");
                                    query.Append("\r\n              ,'" + workCenter + "' ");
                                    query.Append("\r\n              ,'" + orderQty + "' ");
                                    query.Append("\r\n              ,GETDATE() ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ,'Release' ");
                                    query.Append("\r\n              ,1 ");
                                    query.Append("\r\n              ,'" + shift + "' ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ," + workerCount + " ");
                                    query.Append("\r\n              ," + workTime + " ");
                                    query.Append("\r\n              ) ");
                                    query.Append("\r\n END ");
                                    query.Append("\r\n ELSE ");
                                    query.Append("\r\n BEGIN ");
                                    query.Append("\r\n      SET @WorkOrderSeq = (SELECT REPLICATE('0', 4 - LEN(CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) + (CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) ");
                                    query.Append("\r\n      SET @WorkOrder = (SELECT 'SS" + confirmDate + "' + @WorkOrderSeq + '" + line + shift + "M')");
                                    query.Append("\r\n");
                                    query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                                    query.Append("\r\n      VALUES (@WorkOrder ");
                                    query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                                    query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 1, 14)) ");
                                    query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 15, 2)) ");
                                    query.Append("\r\n              ,'" + material + "' ");
                                    //query.Append("\r\n              ,'" + Customer + "' ");
                                    query.Append("\r\n              ,NULL ");
                                    query.Append("\r\n              ,'" + routing + "' ");
                                    query.Append("\r\n              ,'" + workCenter + "' ");
                                    query.Append("\r\n              ,'" + orderQty + "' ");
                                    query.Append("\r\n              ,GETDATE() ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ,'Release' ");
                                    query.Append("\r\n              ,1 ");
                                    query.Append("\r\n              ,'" + shift + "' ");
                                    query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                                    query.Append("\r\n              ," + workerCount + " ");
                                    query.Append("\r\n              ," + workTime + " ");
                                    query.Append("\r\n              ) ");
                                    query.Append("\r\n END ");
                                }
                            }
                        }
                        else
                        {
                            query.Append("\r\n");
                            query.Append("\r\n SET @WorkOrderSeq = (SELECT TOP 1 SUBSTRING(WorkOrder, 11, 4) FROM WorkOrder WHERE WorkOrder LIKE '" + workOrderLike + "' ORDER BY WorkOrder DESC) ");
                            query.Append("\r\n");
                            query.Append("\r\n IF (ISNULL(@WorkOrderSeq, '') = '') ");
                            query.Append("\r\n BEGIN ");
                            query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                            query.Append("\r\n      VALUES ('" + workOrder + "' ");
                            query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                            query.Append("\r\n              ,'" + workOrder.Substring(0, 14) + "' ");
                            query.Append("\r\n              ,'" + workOrder.Substring(14, 2) + "' ");
                            query.Append("\r\n              ,'" + material + "' ");
                            //query.Append("\r\n              ,'" + Customer  + "' ");
                            query.Append("\r\n              ,NULL ");
                            query.Append("\r\n              ,'" + routing + "' ");
                            query.Append("\r\n              ,'" + workCenter + "' ");
                            query.Append("\r\n              ,'" + orderQty + "' ");
                            query.Append("\r\n              ,GETDATE() ");
                            query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                            query.Append("\r\n              ,'Release' ");
                            query.Append("\r\n              ,1 ");
                            query.Append("\r\n              ,'" + shift + "' ");
                            query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                            query.Append("\r\n              ," + workerCount + " ");
                            query.Append("\r\n              ," + workTime + " ");
                            query.Append("\r\n              ) ");
                            query.Append("\r\n END ");
                            query.Append("\r\n ELSE ");
                            query.Append("\r\n BEGIN ");
                            query.Append("\r\n      SET @WorkOrderSeq = (SELECT REPLICATE('0', 4 - LEN(CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) + (CAST(CAST(@WorkOrderSeq AS int) + 1 AS nvarchar(4)))) ");
                            query.Append("\r\n      SET @WorkOrder = (SELECT 'SS" + confirmDate + "' + @WorkOrderSeq + '" + line + shift + "')");
                            query.Append("\r\n");
                            query.Append("\r\n      INSERT INTO WorkOrder (WorkOrder, Division, No, Split, Material, Customer, Routing, Workcenter, OrderQty, Created, Released, ActiveStatus, Status, Shift, PlanedStart, WorkerCount, WorkTime) ");
                            query.Append("\r\n      VALUES (@WorkOrder ");
                            query.Append("\r\n              ,(SELECT TOP 1 Division FROM WorkCenter WHERE WorkCenter = '" + workCenter + "') ");
                            query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 1, 14)) ");
                            query.Append("\r\n              ,(SELECT SUBSTRING(@WorkOrder, 15, 2)) ");
                            query.Append("\r\n              ,'" + material + "' ");
                            //query.Append("\r\n              ,'" + Customer + "' ");
                            query.Append("\r\n              ,NULL ");
                            query.Append("\r\n              ,'" + routing + "' ");
                            query.Append("\r\n              ,'" + workCenter + "' ");
                            query.Append("\r\n              ,'" + orderQty + "' ");
                            query.Append("\r\n              ,GETDATE() ");
                            query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                            query.Append("\r\n              ,'Release' ");
                            query.Append("\r\n              ,1 ");
                            query.Append("\r\n              ,'" + shift + "' ");
                            query.Append("\r\n              ,'" + confirmDate_Hyphen + "' ");
                            query.Append("\r\n              ," + workerCount + " ");
                            query.Append("\r\n              ," + workTime + " ");
                            query.Append("\r\n              ) ");
                            query.Append("\r\n END ");
                        }
                    }
                    */

                    #endregion

                    DbAccess.Default.ExecuteQuery(query.ToString());

                    GridResult_Refresh();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.btn_Insert_Click', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                    System.Windows.Forms.MessageBox.Show(" '" + ex.ToString() + "'Fail!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv_Result.Rows.Count <= 0) return;
            if (dgv_Result.SelectedCells.Count <= 0) return;
            var selectedRows = this.dgv_Result.SelectedCells.OfType<DataGridViewCell>().Select(c => c.OwningRow).Distinct().OrderBy(r => r.Cells["WorkOrder"].Value).ToArray();
            if (selectedRows.Length <= 0) return;
            string msg = string.Join(", ", Array.ConvertAll(selectedRows, i => Convert.ToString(i.Cells["WorkOrder"].Value)));

            if (System.Windows.Forms.MessageBox.Show
                    (
                     "确定要删除工作订单吗？" + "\r\nAre you sure you want to delete the work order?" + "\r\n\r\nWork Order : " + msg
                   , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    )
                != DialogResult.Yes) return;
            {
                StringBuilder query = new StringBuilder();

                query.Append("\r\n DELETE FROM WorkOrder ");
                query.Append("\r\n WHERE WorkOrder IN ( ");
                query.Append("\r\n");
                query.Append(string.Join(", ", Array.ConvertAll(selectedRows, i => "'" + i.Cells["WorkOrder"].Value + "'")));
                query.Append("\r\n ) ");

                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    DbAccess.Default.ExecuteQuery(query.ToString());

                    System.Windows.Forms.MessageBox.Show("Complete!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GridResult_Refresh();
                }
                catch (Exception ex)
                {
                    DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.btn_Delete_Click', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                    System.Windows.Forms.MessageBox.Show("Fail!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            GridResult_Refresh();
        }

        private void GridResult_Refresh()
        {
            Cursor.Current = Cursors.WaitCursor;

            this.dgv_Result.DataSource = null;

            string query = string.Empty;

            query += "\r\n SELECT   WorkOrder ";
            query += "\r\n          ,No ";
            query += "\r\n          ,Split ";
            query += "\r\n          ,Material ";
            query += "\r\n          ,(SELECT TOP 1 Spec FROM Material WHERE A.Material = Material) AS Spec ";
            //query += "\r\n          ,Customer ";
            query += "\r\n          ,Routing ";
            query += "\r\n          ,WorkCenter ";
            query += "\r\n          ,OrderQty ";
            query += "\r\n          ,Created ";
            query += "\r\n FROM     WorkOrder A ";
            // query += "\r\n WHERE    Division = 'ENTMY02' ";
            query += "\r\n WHERE   1 = 1 ";
            query += "\r\n      AND ActiveStatus = 'Release' ";
            query += "\r\n      AND Released >= '" + this.dtp_ReleaseDate.Value.ToString("yyyy-MM-dd") + "' ";
            query += "\r\n      AND Released <  '" + this.dtp_ReleaseDate.Value.AddDays(1).ToString("yyyy-MM-dd") + "' ";
            query += "\r\n ORDER BY Created DESC, WorkOrder ";

            try
            {
                DataTable dt = DbAccess.Default.GetDataTable(query);

                this.dgv_Result.DataSource = dt;
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'WorkOrder.GridResult_Refresh', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
            }

            Cursor.Current = Cursors.Default;
        }

        #endregion
    }
}
