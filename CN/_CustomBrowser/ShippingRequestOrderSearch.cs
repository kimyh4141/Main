using System;
using System.Data;
using System.Drawing;
using WiseM.Data;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class ShippingRequestOrderSearch : Form
    {
        private DataTable _dataTable;

        public string Id { get; private set; }
        public string Material { get; private set; }
        public string Spec { get; private set; }
        public string OrderNo { get; private set; }
        public string OrderSeqNo { get; private set; }
        public string BusinessPartnerCode { get; private set; }
        public string BusinessPartnerName { get; private set; }
        public int OrderQty { get; private set; }
        public int RemainQty { get; private set; }

        public ShippingRequestOrderSearch()
        {
            InitializeComponent();
        }

        private void Search()
        {
            var startDate = dtp_StartDate.Checked ? dtp_StartDate.Value.AddDays(-1).ToString("yyyy-MM-dd 23:59:59") : "1970-01-01 00:00:00";
            var endDate = dtp_EndDate.Checked ? dtp_EndDate.Value.AddDays(1).ToString("yyyy-MM-dd 00:00:00") : "9999-12-31 00:00:00";
            var spec = tb_Spec.Text;
            var workOrder = tb_WorkOrder.Text;

            string query = $@"
                    SELECT ESR.IF_ID
                         , ESR.SO_NO
                         , ESR.SO_SEQ
                         , ESR.BP_CD
                         , MBBP.BP_NM
                         , ESR.ITEM_CD                                        AS Material
                         , M.Spec
                         , CONVERT(INTEGER, ESR.SO_QTY)                       AS OrderQty
                         , CONVERT(INTEGER, ESR.SO_QTY - COALESCE(SH.Qty, 0)) AS RemainQty
                         , IIF(COALESCE(SH.Qty, 0) < ESR.SO_QTY, 'N', 'Y')    AS IsCompleted
                         , ESR.SO_DT
                         , ESR.DLVY_DT
                      FROM (
                               SELECT ROW_NUMBER() OVER (PARTITION BY ESR.SO_NO, ESR.SO_SEQ ORDER BY ESR.IF_ID DESC) AS RowNumber
                                    , ESR.IF_ID
                                    , ESR.IF_TIME
                                    , ESR.I_PROC_STEP
                                    , ESR.I_APPLY_STATUS
                                    , ESR.SO_NO
                                    , ESR.SO_SEQ
                                    , ESR.SO_DT
                                    , ESR.ITEM_CD
                                    , ESR.PLANT_CD
                                    , ESR.SL_CD
                                    , ESR.BP_CD
                                    , ESR.DLVY_DT
                                    , ESR.SO_QTY
                                    , ESR.BASE_UNIT
                                    , ESR.RET_ITEM_FLAG
                                    , ESR.REMARK
                                    , ESR.APPLY_FLAG
                                    , ESR.APPLY_TIME
                                 FROM MES_IF_CN.dbo.ETM_SHIP_REQ AS ESR
                           ) AS ESR
                           LEFT OUTER JOIN Material AS M
                                           ON ESR.ITEM_CD = M.Material
                           LEFT OUTER JOIN (
                                               SELECT SH.ErpOrderNo
                                                    , SH.ErpOrderNoSeq
                                                    , SH.Material
                                                    , COALESCE(SUM(SH.Qty), 0) AS Qty
                                                 FROM ShippingHist SH
                                                GROUP BY SH.ErpOrderNo
                                                       , SH.ErpOrderNoSeq
                                                       , SH.Material
                                           ) AS SH
                                           ON ESR.SO_NO = SH.ErpOrderNo
                                               AND ESR.SO_SEQ = SH.ErpOrderNoSeq
                                               AND ESR.ITEM_CD = SH.Material
                           LEFT OUTER JOIN MES_IF_CN.dbo.M_B_BIZ_PARTNER AS MBBP
                                           ON ESR.BP_CD = MBBP.BP_CD
                     WHERE 1 = 1
                       AND ESR.RowNumber = 1
                       AND ESR.I_PROC_STEP IN ('C', 'U')
                       AND ESR.SO_DT BETWEEN '{startDate}' AND '{endDate}'
                       --   AND ESR.I_APPLY_STATUS IN ( 'R' )
                       AND M.Spec LIKE '%{spec}%'
                       AND ESR.SO_NO LIKE '%{workOrder}%'
                       AND ESR.BP_CD LIKE '%{textBox_BP_CD.Text}%'
                       AND MBBP.BP_NM LIKE '%{textBox_BP_NM.Text}%'
                     ORDER BY SO_DT
                    ;
                    ";

            _dataTable = DbAccess.Default.GetDataTable(query);
            dataGridView_List.DataSource = _dataTable;
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void ShippingRequestOrderSearch_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView_List_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataGridViewRow = dataGridView_List.CurrentRow;
            if (dataGridViewRow == null) return;
            if (dataGridViewRow.Cells["IsCompleted"].Value as string == "Y") return;
            Id = dataGridViewRow.Cells["IF_ID"].Value.ToString();
            OrderNo = dataGridViewRow.Cells["SO_NO"].Value.ToString();
            OrderSeqNo = dataGridViewRow.Cells["SO_SEQ"].Value.ToString();
            Material = dataGridViewRow.Cells["Material"].Value.ToString();
            OrderQty = Convert.ToInt32(dataGridViewRow.Cells["OrderQty"].Value);
            RemainQty = Convert.ToInt32(dataGridViewRow.Cells["RemainQty"].Value);
            Spec = dataGridViewRow.Cells["Spec"].Value.ToString();
            BusinessPartnerCode = dataGridViewRow.Cells["BP_CD"].Value.ToString();
            BusinessPartnerName = dataGridViewRow.Cells["BP_NM"].Value.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void dataGridView_List_DataSourceChanged(object sender, EventArgs e)
        {
            if (dataGridView_List == null) return;
            dataGridView_List.Columns["IF_ID"].Visible = false;
            dataGridView_List.Columns["IsCompleted"].Visible = true;
            
            foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
            {
                if (dataGridViewRow.Cells["IsCompleted"].Value as string == "Y")
                {
                    dataGridViewRow.DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
            dataGridView_List.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }
    }
}