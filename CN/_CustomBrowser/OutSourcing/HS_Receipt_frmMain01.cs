using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;
using System.Drawing;

namespace WiseM.Browser
{
    public partial class HS_Receipt_frmMain01 : Form
    {
        #region Field

        private DataTable dtWO = new DataTable();
        private readonly string _strUser;
        /// <summary>
        /// 자공정실적용 히트싱크작업장
        /// </summary>
        private const string InsideWorkCenter = "WcMAA1";
        /// <summary>
        /// ERP인터페이스 데이터베이스 명
        /// </summary>
        private readonly string _erpDatabase;

        #endregion

        #region Constructor

        public HS_Receipt_frmMain01(string strUser)
        {
            InitializeComponent();
            _strUser = strUser;
            switch (DbAccess.Default.DataBase)
            {
                case "Y2sCn1Mes3":
                    _erpDatabase = "MES_IF_CN";
                    break;
                case "Y2sVn1Mes3":
                    _erpDatabase = "MES_IF_VN";
                    break;
            }
        }

        #endregion

        #region Method

        private DataTable GetReceiptList(string workOrder, int outQty)
        {
            try
            {
                string query = $@"
DECLARE @WorkOrder NVARCHAR(50) = '{workOrder}'
;

DECLARE @OutQty INT = '{outQty}'
;

SELECT SEQ
     , ALT_GROUP
     , KEY_ITEM
     , CHILD_ITEM_CD
     , CAVITY_QTY
     , GroupRemainQty
     , RemainQty
     , UsedQty AS GroupUsedQty
     , CASE
         WHEN PrefixSumRemainQty - RemainQty >= UsedQty
           THEN 0
         WHEN PrefixSumRemainQty - RemainQty < UsedQty AND PrefixSumRemainQty >= UsedQty
           THEN UsedQty - (PrefixSumRemainQty - RemainQty)
         WHEN PrefixSumRemainQty < UsedQty
           THEN CASE
                  WHEN PrefixSumRemainQty = GroupRemainQty AND RemainQty <> 0
                    THEN UsedQty - (PrefixSumRemainQty - RemainQty)
                    ELSE RemainQty
                END
       END     AS CurrentUsedQty
     , GroupStockOutQty
     , StockOutQty
     , GroupReportedQty
     , ReportedQty
  FROM (
         SELECT SEQ
              , ALT_GROUP
              , KEY_ITEM
              , CHILD_ITEM_CD
              , CAVITY_QTY
              , @OutQty * CAVITY_QTY                AS UsedQty
              , GroupStockOutQty - GroupReportedQty AS GroupRemainQty
              , StockOutQty - ReportedQty           AS RemainQty
              , CASE KEY_ITEM_ALTERNATE_ITEM_FLAG
                  WHEN N'N'
                    THEN StockOutQty - ReportedQty
                  WHEN N'Y'
                    THEN SUM(StockOutQty - ReportedQty) OVER ( PARTITION BY ALT_GROUP, KEY_ITEM ORDER BY SEQ)
                END                                 AS PrefixSumRemainQty
              , GroupStockOutQty
              , StockOutQty
              , GroupReportedQty
              , ReportedQty
           FROM (
                  SELECT EPIRBO.SEQ
                       , EPIRBO.ALT_GROUP
                       , EPIRBO.KEY_ITEM
                       , EPIRBO.CHILD_ITEM_CD
                       , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                       , CONVERT(INT, EPIRBO.CAVITY_QTY)                                        AS CAVITY_QTY
                       --그룹별 총 출고수량
                       , CONVERT(INT, COALESCE(CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                                 WHEN N'N'
                                                   THEN SUM(RSH.StockOutQty) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ)
                                                 WHEN N'Y'
                                                   THEN SUM(RSH.StockOutQty) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
                                               END, 0), 0)                                      AS GroupStockOutQty
                       , SUM(StockOutQty) OVER ( PARTITION BY ALT_GROUP, KEY_ITEM ORDER BY SEQ) AS PrefixSumStockOutQty
                       --출고수량
                       , CONVERT(INT, COALESCE(RSH.StockOutQty, 0))                             AS StockOutQty
                       , CONVERT(INT, COALESCE(EPIRBO.GroupReportedQty, 0))                     AS GroupReportedQty
                       , CONVERT(INT, EPIRBO.ReportedQty)                                       AS ReportedQty
                    FROM (
                           SELECT EPPO.PRODT_ORDER_NO
                                , EPPO.ORDER_QTY
                                , EPIRBO.ALT_GROUP
                                , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                , EPIRBO.KEY_ITEM
                                , EPIRBO.CHILD_ITEM_CD
                                , EPIRBO.SEQ
                                --그룹별 총 보고된 수량
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                    WHEN N'N'
                                      THEN SUM(RMUH.ReportedQty) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.CHILD_ITEM_CD)
                                    WHEN N'Y'
                                      THEN SUM(RMUH.ReportedQty) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
                                  END                           AS GroupReportedQty
                                --자재 별 보고된 수량
                                , COALESCE(RMUH.ReportedQty, 0) AS ReportedQty
                                --단위 별 소요수량
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                    WHEN N'N'
                                      THEN EPIRBO.REQ_QTY / EPPO.ORDER_QTY
                                    WHEN N'Y'
                                      THEN SUM(EPIRBO.REQ_QTY) OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP) / EPPO.ORDER_QTY
                                  END                           AS CAVITY_QTY
                             FROM WorkOrder                                AS WO
                                  INNER JOIN      (
                                                    SELECT ROW_NUMBER() OVER (PARTITION BY EPPO.PRODT_ORDER_NO ORDER BY EPPO.IF_TIME DESC) AS RowNumber
                                                         , EPPO.PRODT_ORDER_NO
                                                         , EPPO.ORDER_QTY
                                                         , EPPO.I_PROC_STEP
                                                      FROM {_erpDatabase}.dbo.ETM_P_PROD_ORDER AS EPPO WITH (NOLOCK)
                                                     WHERE 1 = 1
                                                  )                        AS EPPO
                                                  ON WO.ERP_WorkOrder = EPPO.PRODT_ORDER_NO
                                                    AND EPPO.RowNumber = 1
                                                    AND EPPO.I_PROC_STEP IN ('C', 'U')
                                  LEFT OUTER JOIN (
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
                                                      FROM {_erpDatabase}.dbo.ETM_P_ISSUE_REQ_BY_ORDER AS EPIRBO WITH (NOLOCK)
                                                  )                        AS EPIRBO
                                                  ON EPPO.PRODT_ORDER_NO = EPIRBO.PRODT_ORDER_NO
                                                    AND EPIRBO.RowNumber = 1
                                                    AND EPIRBO.I_PROC_STEP IN ('C', 'U')
                                                    --실리콘 자재 제외
                                                    AND EPIRBO.CHILD_ITEM_CD NOT LIKE '3LQ%'
                                  LEFT OUTER JOIN (
                                                    SELECT WO.ERP_WorkOrder
                                                         , RMUH.RawMaterial
                                                         , RMUH.RawMaterialSeq
                                                         , SUM(RMUH.Qty) AS ReportedQty
                                                      FROM RawMaterialUsageHist AS RMUH WITH (NOLOCK)
                                                           INNER JOIN WorkOrder AS WO WITH (NOLOCK)
                                                                      ON RMUH.WorkOrder = WO.WorkOrder
                                                     WHERE RMUH.Type = 'Outsourcing'
                                                     GROUP BY WO.ERP_WorkOrder
                                                            , RMUH.RawMaterialSeq
                                                            , RMUH.RawMaterial
                                                  )                        AS RMUH
                                                  ON EPIRBO.PRODT_ORDER_NO = RMUH.ERP_WorkOrder
                                                    AND EPIRBO.SEQ = RMUH.RawMaterialSeq
                                                    AND EPIRBO.CHILD_ITEM_CD = RMUH.RawMaterial
                                  INNER JOIN      {_erpDatabase}.dbo.M_MATERIAL AS MM WITH (NOLOCK)
                                                  ON EPIRBO.CHILD_ITEM_CD = MM.ITEM_CD
                                                    AND MM.ITEM_ACCT <> '25'
                            WHERE WO.WorkOrder = @WorkOrder
                         )                 AS EPIRBO
                         LEFT OUTER JOIN (
                                           SELECT RSH.Rm_Order
                                                , RSH.Rm_OrderSeq
                                                , RSH.Rm_Material
                                                , SUM(RSH.Rm_StockQty) AS StockOutQty
                                             FROM Rm_StockHist AS RSH WITH (NOLOCK)
                                            WHERE RSH.Rm_IO_Type = 'OUT'
                                              AND RSH.Rm_Bunch = 'MOVE'
                                            GROUP BY RSH.Rm_Order
                                                   , RSH.Rm_OrderSeq
                                                   , RSH.Rm_Material
                                         ) AS RSH
                                         ON EPIRBO.PRODT_ORDER_NO = RSH.Rm_Order
                                           AND EPIRBO.SEQ = RSH.Rm_OrderSeq
                                           AND EPIRBO.CHILD_ITEM_CD = RSH.Rm_Material
                ) AS T
       ) AS T
 ORDER BY SEQ
;
                            ";
                return DbAccess.Default.GetDataTable(query);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(e.Message);
                return null;
            }
        }

        private void ClearScreen()
        {
            dgv01.DataSource = null;
            dgv02.DataSource = null;
            txtWO.Text = "";
            txtMaterial.Text = "";
            txtOrderQty.Text = "";
            txtPrevReceiptQty.Text = "";
            nudCurrReceiptQty.Value = 0;
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'HS_Receipt', LEFT(ISNULL(N'{strMsg}',''),3000), '{this._strUser}', GETDATE())");
            }
            catch
            {
            }
        }

        #endregion

        #region Event

        private void btnSelWO_Click(object sender, EventArgs e)
        {
            try
            {
                var hsReceiptFrmSub10SelectWo = new HS_Receipt_frmSub10_SelectWO(_strUser);
                if (DialogResult.Yes != hsReceiptFrmSub10SelectWo.ShowDialog())
                    return;

                dtWO = hsReceiptFrmSub10SelectWo.dtSelectedWO;
                dgv01.DataSource = dtWO;

                foreach (DataGridViewColumn col in dgv01.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                txtWO.Text = dtWO.Rows[0]["WorkOrder"].ToString();
                txtMaterial.Text = dtWO.Rows[0]["Material"].ToString();
                txtOrderQty.Text = dtWO.Rows[0]["OrderQty"].ToString();
                txtPrevReceiptQty.Text = dtWO.Rows[0]["Qty_Sum"].ToString();
                dgv02.DataSource = null;
                btnSave.Enabled = false;
                checkBox_isVerify.Checked = true;
                checkBox_isVerify.Enabled = InsideWorkCenter.Equals(dtWO.Rows[0]["WorkCenter"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!btnSave.Enabled) return;
            try
            {
                string PS_GUBUN = "HS_SAVE_RECEIPT";
                string PS_ClientId = _strUser;
                string PS_WorkCenter = dtWO.Rows[0]["Workcenter"].ToString();
                string PS_Material = dtWO.Rows[0]["Material"].ToString();
                string PS_Routing = "Mi_Assy1";
                string PS_WorkOrder = dtWO.Rows[0]["WorkOrder"].ToString();
                string PN_OutQty = nudCurrReceiptQty.Value.ToString();
                string PS_Shift = "";
                string PS_MatAndQty = "";



                //자공정일 경우 자재없이 저장할 수 있도록 수정
                if (checkBox_isVerify.Checked)
                {
                    var dataTable = (DataTable)dgv02.DataSource;
                    DataRow[] fRows = dataTable.Select("CurrentUsedQty > 0");

                    if (fRows.Length < 1)
                    {
                        string strTmp = "사용자재내역이 입력되지 않았습니다.\n\n";
                        strTmp += "There is no Used Material Information.\n\n";
                        MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    foreach (DataRow row in fRows)
                    {
                        PS_MatAndQty += PS_WorkOrder + "\x07";
                        PS_MatAndQty += row["CHILD_ITEM_CD"] + "\x07";
                        PS_MatAndQty += row["SEQ"] + "\x07";
                        PS_MatAndQty += row["CurrentUsedQty"] + "\x07";
                    }

                    if (PS_MatAndQty.Length > 0) PS_MatAndQty = PS_MatAndQty.Remove(PS_MatAndQty.Length - 1, 1);
                }

                string strCmd = $@"exec [Sp_OutSourcingProcedureV4]
                            @PS_GUBUN		= '{PS_GUBUN}'
                            ,@PS_ClientId   = '{PS_ClientId}'
                            ,@PS_WorkCenter = '{PS_WorkCenter}'
                            ,@PS_Material   = '{PS_Material}'
                            ,@PS_Routing    = '{PS_Routing}'
                            ,@PS_WorkOrder  = '{PS_WorkOrder}'
                            ,@PN_OutQty     = {PN_OutQty}
                            ,@PS_Shift      = '{PS_Shift}'
                            ,@PS_MatAndQty  = '{PS_MatAndQty}'
                            ";

                var ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (intRC != -999) throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Saved successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                ClearScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            Close();
        }

        private void dgv02_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            var canSave = true;
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                if (checkBox_isVerify.Checked)
                {
                    if (Convert.ToInt32(dataGridViewRow.Cells["GroupRemainQty"].Value) < Convert.ToInt32(dataGridViewRow.Cells["GroupUsedQty"].Value))
                    {
                        dataGridViewRow.DefaultCellStyle.BackColor = Color.Red;
                        dataGridViewRow.DefaultCellStyle.ForeColor = Color.White;
                        canSave = false;
                        continue;
                    }
                }
                dataGridViewRow.Cells["GroupUsedQty"].Style.BackColor = Color.GreenYellow;
                dataGridViewRow.Cells["CurrentUsedQty"].Style.BackColor = Color.GreenYellow;
            }
            btnSave.Enabled = canSave;
            btnSave.BackColor = canSave ? Color.GreenYellow : Color.Red;
            btnSave.ForeColor = canSave ? Color.Black : Color.White;
        }

        private void button_Select_Click(object sender, EventArgs e)
        {
            if (this.dtWO.Rows.Count < 1)
            {
                string strTmp = "MES 작업지시가 선택되지 않았습니다.\n\n";
                strTmp += "You have to select WorkOrder for MES.n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (nudCurrReceiptQty.Value < 1)
            {
                string strTmp = "수량을 입력하세요.\n\n";
                strTmp += "You have to input the Receipt Qty.\n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (Convert.ToInt32(txtOrderQty.Text) < Convert.ToInt32(txtPrevReceiptQty.Text) + nudCurrReceiptQty.Value)
            {
                string strTmp = "작업지시 수량을 초과합니다.\n\n";
                strTmp += "It is larger than the WorkOrder Qty.\n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dataTable = GetReceiptList(txtWO.Text, (int)nudCurrReceiptQty.Value);
            dgv02.DataSource = dataTable;
        }

        #endregion

        private void nudCurrReceiptQty_ValueChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
        }

        private void checkBox_isVerify_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}