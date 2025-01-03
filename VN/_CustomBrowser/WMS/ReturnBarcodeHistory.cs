using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using WiseM.Data;
using System.Windows.Forms;


namespace WiseM.Browser.WMS
{
    public partial class ReturnBarcodeHistory : Form
    {
        public ReturnBarcodeHistory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 화면 플리커 제거
        /// </summary>
        /*
                 protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= 0x02000000;
                return createParams;
            }
        }
             */

        #region Event

        private void ReturnBarcodeHistory_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            LoadWorkOrder();
            ClearLabel();
        }

        private void dataGridView_WorkOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadRawMaterial();
            ClearLabel();
            dataGridView_ScanList.Rows.Clear();
        }

        private void button_RawMaterial_Search_Click(object sender, EventArgs e)
        {
            ScanRawMaterial();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                textBox_Barcode.ReadOnly = false;
                if (dataGridView_ScanList.Rows.Count <= 0)
                {
                    MessageBox.ShowCaption("No Data", "Error", MessageBoxIcon.Error);
                    return;
                }

                if (System.Windows.Forms.MessageBox.Show("Save?", "Xác nhận (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }

                SaveBarcodeInfoRows();
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Return.RM_Barcode', N'{ex.Message}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("Information insert failed!\r", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            LoadRawMaterial();
            dataGridView_RawMaterial.ClearSelection();
            for (int i = 0; i < dataGridView_RawMaterial.Rows.Count; i++)
            {
                if (dataGridView_RawMaterial.Rows[i].Cells[1].Value.ToString() == textBox_Scan_Material.Text)
                {
                    dataGridView_RawMaterial.Rows[i].Selected = true;
                    break;
                }
            }

            ClearLabel();
            dataGridView_ScanList.Rows.Clear();
            comboBox_From.Enabled = true;
            comboBox_To.Enabled = true;
            MessageBox.ShowCaption("Success", "Save", MessageBoxIcon.Information);
        }

        private void textBox_WorkOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            LoadWorkOrder();
            ClearLabel();
        }

        private void textBox_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            ScanRawMaterial();
        }

        private void button_Search_Clear_Click(object sender, EventArgs e)
        {
            ClearDataGridViewInfo();
            ClearLabel();
            textBox_Workorder.Text = "";
            textBox_Barcode.ReadOnly = false;
            comboBox_From.Enabled = true;
            comboBox_To.Enabled = true;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            ClearLabel();
            dataGridView_ScanList.Rows.Clear();
            dataGridView_RawMaterial.ClearSelection();
            textBox_Barcode.ReadOnly = false;
            comboBox_From.Enabled = true;
            comboBox_To.Enabled = true;
        }

        private void dataGridView_RawMaterial_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AddRawMaterialInfo();
            dataGridView_ScanList.Rows.Clear();
        }

        private void comboBox_location_group_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = $@" select Location from Rm_Location where Location_Group  = '{comboBox_location_group.Text}' AND Status = 1  order by Location ";
            DataTable locationTable = DbAccess.Default.GetDataTable(query);
            comboBox_location.DisplayMember = "Location";
            comboBox_location.DataSource = locationTable;
        }

        private void dataGridView_WorkOrder_DataSourceChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView_WorkOrder.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            textBox_Barcode.ReadOnly = false;
            if (string.IsNullOrEmpty(textBox_Barcode.Text))
            {
                MessageBox.ShowCaption("Scan Barcode", "Error", MessageBoxIcon.Error);
                return;
            }

            if (comboBox_From.SelectedIndex < 1)
            {
                MessageBox.ShowCaption("Select From", "Error", MessageBoxIcon.Error);
                return;
            }

            if (comboBox_To.SelectedIndex < 1)
            {
                MessageBox.ShowCaption("Select To", "Error", MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(comboBox_location.Text))
            {
                MessageBox.ShowCaption("Select Location", "Error", MessageBoxIcon.Error);
                return;
            }

            if (dataGridView_ScanList.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView_ScanList.Rows.Count; i++)
                {
                    if (textBox_Scan_Barcode.Text !=
                        dataGridView_ScanList.Rows[i].Cells["Rm_BarCode"].Value.ToString()) continue;
                    ClearBarcodeInfo();
                    MessageBox.ShowCaption("Already Add", "Error", MessageBoxIcon.Error);
                    return;
                }
            }
            
            try
            {
                string[] row = {
                    textBox_Scan_Barcode.Text,
                    "RETURN",
                    textBox_Scan_Material.Text,
                    textBox_Scan_Order.Text,
                    textBox_Scan_Supply.Text,
                    textBox_Scan_DATE.Text,
                    textBox_Scan_QTY.Text,
                    textBox_Scan_SEQ.Text,
                    comboBox_location_group.Text,
                    comboBox_location.Text,
                    comboBox_From.SelectedValue.ToString(),
                    comboBox_To.SelectedValue.ToString()
                };
                comboBox_From.Enabled = false;
                comboBox_To.Enabled = false;
                dataGridView_ScanList.Rows.Add(row);
                dataGridView_ScanList.ClearSelection();
                ClearBarcodeInfo();
                textBox_Barcode.Focus();
            }
            catch (Exception ex)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption(ex.StackTrace, "Error", MessageBoxIcon.Error);
            }
        }

        private void dataGridView_WorkOrder_Layout(object sender, LayoutEventArgs e)
        {
            dataGridView_WorkOrder.EnableHeadersVisualStyles = false;
            dataGridView_WorkOrder.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_WorkOrder.MultiSelect = false;
        }

        private void dataGridView_RawMaterial_Layout(object sender, LayoutEventArgs e)
        {
            dataGridView_RawMaterial.EnableHeadersVisualStyles = false;
            dataGridView_RawMaterial.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_RawMaterial.MultiSelect = false;
        }

        private void dataGridView_ScanList_Layout(object sender, LayoutEventArgs e)
        {
            dataGridView_ScanList.EnableHeadersVisualStyles = false;
            dataGridView_ScanList.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView_ScanList.MultiSelect = false;
            dataGridView_ScanList.ColumnCount = 12;
            dataGridView_ScanList.Columns[0].Name = "Rm_BarCode";
            dataGridView_ScanList.Columns[1].Name = "Rm_IO_Type";
            dataGridView_ScanList.Columns[2].Name = "Rm_Material";
            dataGridView_ScanList.Columns[3].Name = "Rm_Order";
            dataGridView_ScanList.Columns[4].Name = "Rm_Supplier";
            dataGridView_ScanList.Columns[5].Name = "Rm_ProdDate";
            dataGridView_ScanList.Columns[6].Name = "Rm_QtyinBox";
            dataGridView_ScanList.Columns[7].Name = "Rm_BoxSeq";
            dataGridView_ScanList.Columns[8].Name = "Rm_LocationGroup";
            dataGridView_ScanList.Columns[9].Name = "Rm_Location";
            dataGridView_ScanList.Columns[10].Name = "ERP_SL_CD_FROM";
            dataGridView_ScanList.Columns[11].Name = "ERP_SL_CD_TO";

            dataGridView_ScanList.Columns["Rm_BarCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_IO_Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_Material"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_Order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_Supplier"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_ProdDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_QtyinBox"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_ScanList.Columns["Rm_BoxSeq"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_ScanList.Columns["Rm_LocationGroup"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["Rm_Location"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["ERP_SL_CD_FROM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView_ScanList.Columns["ERP_SL_CD_TO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void dataGridView_RawMaterial_KeyUp(object sender, KeyEventArgs e)
        {
            AddRawMaterialInfo();
            dataGridView_ScanList.Rows.Clear();
        }

        private void dataGridView_WorkOrder_KeyUp(object sender, KeyEventArgs e)
        {
            LoadRawMaterial();
            ClearLabel();
            dataGridView_ScanList.Rows.Clear();
        }

        private void dataGridView_RawMaterial_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView_RawMaterial_KeyDown(object sender, KeyEventArgs e)
        {
        }

        #endregion

        /// <summary>
        /// </summary>

        #region Function

        private void Init()
        {
            comboBox_location_group.DataSource = null;
            string Query = " select '' as Location_Group union all " +
                           " select distinct Location_Group from Rm_Location_Group WHERE Status = 1 order by Location_Group ";
            DataTable LocationGroupDataTable = DbAccess.Default.GetDataTable(Query);
            comboBox_location_group.DisplayMember = "Location_Group";
            comboBox_location_group.DataSource = LocationGroupDataTable;
            comboBox_From.DataSource = GetDataTableStorage(2);
            comboBox_From.ValueMember = "SL_CD";
            comboBox_From.DisplayMember = "SL_NM";
            comboBox_To.DataSource = GetDataTableStorage(1);
            comboBox_To.ValueMember = "SL_CD";
            comboBox_To.DisplayMember = "SL_NM";
            dateTimePicker_Start.Value = DateTime.Now;
            dateTimePicker_End.Value = DateTime.Now;
        }

        private DataTable GetDataTableStorage(int type)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine
                (
                    $@"
                    SELECT '' AS SL_CD
                         , '' AS SL_NM
                     UNION
                    SELECT SL_CD
                         , SL_NM
                      FROM Y2sVn1Mes3.dbo.GetRawMaterialStorage(0,{type})
                    ;
                    "
                );
                return DbAccess.Default.GetDataTable(stringBuilder.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void LoadRawMaterial()
        {
            if (dataGridView_WorkOrder.SelectedRows.Count == 0) return;
            dataGridView_RawMaterial.DataSource = null;
            DataGridViewRow row = dataGridView_WorkOrder.CurrentRow;
            string orderNo = row.Cells["ORDER_NO"].Value.ToString();

            try
            {
                string query = $@"					  
      SELECT ROW_NUMBER() OVER (ORDER BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ, EPIRBO.ITEM_SEQ)                                                AS No
    
     , EPIRBO.CHILD_ITEM_CD                                                                                                           AS Material

     , EPIRBO.KEY_ITEM  as KEY_ITEM
	 , EPIRBO.ALT_GROUP as ALT_GROUP
	 , EPIRBO.SEQ       as ORDER_SEQ_NO
     , convert(int,EPIRBO.GROUP_QTY  )                                                                                                       AS GroupQty

     , COALESCE(RSH.OUT_QTY , 0)                                                                                                       AS ScanQty
	  , COALESCE(RSH_R.OUT_QTY_R , 0)                                                                                                       AS ReturnQty
     , EPIRBO.GROUP_QTY  - SUM(COALESCE(RSH.OUT_QTY , 0)  -  COALESCE(RSH_R.OUT_QTY_R , 0)   )  OVER ( PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP) AS RemainQty
	      
     , RM.Spec                                                                                                                        AS Spec
    
  FROM (
           SELECT EPPO.PRODT_ORDER_NO AS ORDER_NO
                , EPPO.ROUT_SEQ
                , EPPO.ROUT_SEQ_NM
                , EPPO.ORDER_QTY
                , EPPO.PLAN_START_DT  AS ORDER_DT
                , EPPO.ITEM_CD
				, EPPO.REMARK
             FROM (
                      SELECT ROW_NUMBER() OVER (PARTITION BY EPPO.PRODT_ORDER_NO ORDER BY EPPO.IF_TIME DESC) AS RowNumber
                           , EPPO.PRODT_ORDER_NO
                           , EPPO.ITEM_CD
                           , EPPO.ROUT_SEQ
                           , EPPO.ROUT_SEQ_NM
                           , EPPO.ORDER_QTY
                           , EPPO.PLAN_START_DT
                           , EPPO.I_PROC_STEP
						   , EPPO.REMARK
                        FROM MES_IF_VN.dbo.ETM_P_PROD_ORDER AS EPPO
                       WHERE 1 = 1
                  ) AS EPPO
            WHERE EPPO.RowNumber = 1
              AND EPPO.I_PROC_STEP IN ('C', 'U')
              AND EPPO.PLAN_START_DT between '{dateTimePicker_Start.Value:yyyy-MM-dd}' and '{dateTimePicker_End.Value:yyyy-MM-dd}'
              AND  EPPO.PRODT_ORDER_NO LIKE '%{orderNo}%'
       ) AS EPPO
       INNER JOIN
                       (
                           SELECT EPIRBO.IF_ID
                                , EPIRBO.PRODT_ORDER_NO
                                , EPIRBO.SEQ
                                , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                      WHEN N'N'
                                          THEN EPIRBO.CHILD_ITEM_CD
                                      WHEN N'Y'
                                          THEN EPIRBO.KEY_ITEM
                                  END            AS KEY_ITEM
                                , EPIRBO.CHILD_ITEM_CD
                                , EPIRBO.BASE_UNIT
                                , EPIRBO.ALT_GROUP
                                , EPIRBO.ITEM_SEQ
                                , CASE
                                      WHEN EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG = N'N'
                                          THEN EPIRBO.REQ_QTY
                                      ELSE SUM(EPIRBO.REQ_QTY) OVER ( PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
                                  END            AS GROUP_QTY
                                , EPIRBO.REQ_QTY AS ORDER_QTY
                                , EPIRBO.SL_CD
                                , EPIRBO.I_PROC_STEP
                                , EPIRBO.I_APPLY_STATUS
                                , EPIRBO.APPLY_FLAG
                                , EPIRBO.REQ_DT
                             FROM (
                                      SELECT ROW_NUMBER() OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ ORDER BY EPIRBO.IF_TIME DESC) AS RowNumber
                                           , EPIRBO.IF_ID
                                           , EPIRBO.PRODT_ORDER_NO
                                           , EPIRBO.SEQ
                                           , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                           , EPIRBO.KEY_ITEM
                                           , EPIRBO.CHILD_ITEM_CD
                                           , EPIRBO.BASE_UNIT
                                           , EPIRBO.ALT_GROUP
                                           , EPIRBO.ITEM_SEQ
                                           , EPIRBO.REQ_QTY
                                           , EPIRBO.SL_CD
                                           , EPIRBO.I_PROC_STEP
                                           , EPIRBO.I_APPLY_STATUS
                                           , EPIRBO.APPLY_FLAG
                                           , EPIRBO.REQ_DT
                                        FROM MES_IF_VN.dbo.ETM_P_ISSUE_REQ_BY_ORDER AS EPIRBO
                                  ) AS EPIRBO
                            WHERE EPIRBO.RowNumber = 1
                              AND EPIRBO.I_PROC_STEP IN ('C', 'U')
                              AND EPIRBO.I_APPLY_STATUS IN ('R')
                       ) AS EPIRBO
                       ON EPPO.ORDER_NO = EPIRBO.PRODT_ORDER_NO
       LEFT OUTER JOIN (
                           SELECT RSH.Rm_Order
                                , RSH.Rm_OrderSeq
                                , RSH.Rm_Material
                                , SUM(RSH.Rm_StockQty)            AS OUT_QTY
                              
                             FROM Rm_StockHist RSH
                            WHERE RSH.Rm_IO_Type = 'OUT'
						
                            GROUP BY RSH.Rm_Order
                                   , RSH.Rm_OrderSeq
                                   , RSH.Rm_Material
                       ) RSH
                       ON EPIRBO.PRODT_ORDER_NO = RSH.Rm_Order AND EPIRBO.SEQ = COALESCE(RSH.Rm_OrderSeq, EPIRBO.SEQ) AND EPIRBO.CHILD_ITEM_CD = RSH.Rm_Material
					     LEFT OUTER JOIN (
                           SELECT RSH_R.Rm_Order
                                , RSH_R.Rm_OrderSeq
                                , RSH_R.Rm_Material
                                , SUM(RSH_R.Rm_StockQty)            AS OUT_QTY_R
                              
                             FROM Rm_StockHist RSH_R
                            WHERE RSH_R.Rm_Bunch = 'RETURN' and RSH_R.Rm_IO_Type = 'IN'
						
                            GROUP BY RSH_R.Rm_Order
                                   , RSH_R.Rm_OrderSeq
                                   , RSH_R.Rm_Material
                       ) RSH_R
                       ON EPIRBO.PRODT_ORDER_NO = RSH_R.Rm_Order AND EPIRBO.SEQ = COALESCE(RSH_R.Rm_OrderSeq, EPIRBO.SEQ) AND EPIRBO.CHILD_ITEM_CD = RSH_R.Rm_Material
       LEFT OUTER JOIN Material AS M
                       ON EPPO.ITEM_CD = M.Material
       LEFT OUTER JOIN RawMaterial RM
                       ON EPIRBO.CHILD_ITEM_CD = RM.RawMaterial
 WHERE RM.Bunch NOT IN ('10', '25')
   
 ORDER BY SEQ
;
";

                var dataTable = DbAccess.Default.GetDataTable(query);
                if (dataTable.Rows.Count > 0)
                {
                    dataGridView_RawMaterial.DataSource = dataTable;

                    dataGridView_RawMaterial.Columns["No"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_RawMaterial.Columns["Material"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_RawMaterial.Columns["KEY_ITEM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_RawMaterial.Columns["ALT_GROUP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_RawMaterial.Columns["ORDER_SEQ_NO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridView_RawMaterial.Columns["Spec"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dataGridView_RawMaterial.Columns["GroupQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView_RawMaterial.Columns["ScanQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView_RawMaterial.Columns["ReturnQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView_RawMaterial.Columns["RemainQty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView_RawMaterial.ClearSelection();
                }
                else
                {
                    MessageBox.ShowCaption("Không tìm thấy。\r\nNot Found.", "Error)", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }

        private void LoadWorkOrder()
        {
            ClearLabel();
            if (dateTimePicker_End.Value < dateTimePicker_Start.Value)
            {
                MessageBox.ShowCaption("Check datetime", "Error", MessageBoxIcon.Error);
                return;
            }

            try
            {
                string query = $@"					  
      SELECT distinct
      EPIRBO.PRODT_ORDER_NO                                                                                                          AS ORDER_NO
	 , EPPO.REMARK																												      AS REMARK

     , EPPO.ROUT_SEQ
     , EPPO.ROUT_SEQ_NM
     , EPPO.ITEM_CD                                                                                                                   AS ModelCode
     , M.Spec                                                                                                                         AS ModelSpec

     
 
     , ORDER_DT
     FROM (
           SELECT EPPO.PRODT_ORDER_NO AS ORDER_NO
                , EPPO.ROUT_SEQ
                , EPPO.ROUT_SEQ_NM
                , EPPO.ORDER_QTY
                , EPPO.PLAN_START_DT  AS ORDER_DT
                , EPPO.ITEM_CD
				, EPPO.REMARK
             FROM (
                      SELECT ROW_NUMBER() OVER (PARTITION BY EPPO.PRODT_ORDER_NO ORDER BY EPPO.IF_TIME DESC) AS RowNumber
                           , EPPO.PRODT_ORDER_NO
                           , EPPO.ITEM_CD
                           , EPPO.ROUT_SEQ
                           , EPPO.ROUT_SEQ_NM
                           , EPPO.ORDER_QTY
                           , EPPO.PLAN_START_DT
                           , EPPO.I_PROC_STEP
						   , EPPO.REMARK
                        FROM MES_IF_VN.dbo.ETM_P_PROD_ORDER AS EPPO
                       WHERE 1 = 1
                  ) AS EPPO
            WHERE EPPO.RowNumber = 1
              AND EPPO.I_PROC_STEP IN ('C', 'U')
              AND EPPO.PLAN_START_DT between '{dateTimePicker_Start.Value:yyyy-MM-dd}' and '{dateTimePicker_End.Value:yyyy-MM-dd}'
              AND  EPPO.PRODT_ORDER_NO LIKE '%{textBox_Workorder.Text}%'
       ) AS EPPO
       INNER JOIN
                       (
                           SELECT EPIRBO.IF_ID
                                , EPIRBO.PRODT_ORDER_NO
                                , EPIRBO.SEQ
                                , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                , CASE EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                      WHEN N'N'
                                          THEN EPIRBO.CHILD_ITEM_CD
                                      WHEN N'Y'
                                          THEN EPIRBO.KEY_ITEM
                                  END            AS KEY_ITEM
                                , EPIRBO.CHILD_ITEM_CD
                                , EPIRBO.BASE_UNIT
                                , EPIRBO.ALT_GROUP
                                , EPIRBO.ITEM_SEQ
                                , CASE
                                      WHEN EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG = N'N'
                                          THEN EPIRBO.REQ_QTY
                                      ELSE SUM(EPIRBO.REQ_QTY) OVER ( PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.KEY_ITEM, EPIRBO.ALT_GROUP)
                                  END            AS GROUP_QTY
                                , EPIRBO.REQ_QTY AS ORDER_QTY
                                , EPIRBO.SL_CD
                                , EPIRBO.I_PROC_STEP
                                , EPIRBO.I_APPLY_STATUS
                                , EPIRBO.APPLY_FLAG
                                , EPIRBO.REQ_DT
                             FROM (
                                      SELECT ROW_NUMBER() OVER (PARTITION BY EPIRBO.PRODT_ORDER_NO, EPIRBO.SEQ ORDER BY EPIRBO.IF_TIME DESC) AS RowNumber
                                           , EPIRBO.IF_ID
                                           , EPIRBO.PRODT_ORDER_NO
                                           , EPIRBO.SEQ
                                           , EPIRBO.KEY_ITEM_ALTERNATE_ITEM_FLAG
                                           , EPIRBO.KEY_ITEM
                                           , EPIRBO.CHILD_ITEM_CD
                                           , EPIRBO.BASE_UNIT
                                           , EPIRBO.ALT_GROUP
                                           , EPIRBO.ITEM_SEQ
                                           , EPIRBO.REQ_QTY
                                           , EPIRBO.SL_CD
                                           , EPIRBO.I_PROC_STEP
                                           , EPIRBO.I_APPLY_STATUS
                                           , EPIRBO.APPLY_FLAG
                                           , EPIRBO.REQ_DT
                                        FROM MES_IF_VN.dbo.ETM_P_ISSUE_REQ_BY_ORDER AS EPIRBO
                                  ) AS EPIRBO
                            WHERE EPIRBO.RowNumber = 1
                              AND EPIRBO.I_PROC_STEP IN ('C', 'U')
                              AND EPIRBO.I_APPLY_STATUS IN ('R')
                       ) AS EPIRBO
                       ON EPPO.ORDER_NO = EPIRBO.PRODT_ORDER_NO
     
       LEFT OUTER JOIN Material AS M
                       ON EPPO.ITEM_CD = M.Material
       LEFT OUTER JOIN RawMaterial RM
                       ON EPIRBO.CHILD_ITEM_CD = RM.RawMaterial
       WHERE RM.Bunch NOT IN ('10', '25')
   
       ORDER BY ORDER_NO
;

";
                var dataTable = DbAccess.Default.GetDataTable(query);
                if (dataTable.Rows.Count > 0)
                {
                    dataGridView_WorkOrder.DataSource = dataTable;
                }
                else
                {
                    MessageBox.ShowCaption("Không tìm thấy。\r\nNot Found.", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }

        private void ScanRawMaterial()
        {
            if (dataGridView_RawMaterial.SelectedRows.Count == 0)
            {
                MessageBox.ShowCaption("Select RawMaterial Code", "Error", MessageBoxIcon.Error);
                return;
            }

            if (textBox_Barcode.TextLength != 34)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption("Wrong Barcode", "Error", MessageBoxIcon.Error);
                return;
            }

            if (DbAccess.Default.IsExist("Rm_Stock", $"Rm_BarCode = '{textBox_Barcode.Text}'") > 0)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption("Already In Rm_Stock", "Error", MessageBoxIcon.Error);
                return;
            }

            if (DbAccess.Default.IsExist("RmPrintHist", $"Rm_BarCode = '{textBox_Barcode.Text}'") <= 0)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption("No Barcode Print History", "Error", MessageBoxIcon.Error);
                return;
            }

            if (textBox_Barcode.Text.Substring(0, 10) != textBox_Scan_Material.Text)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption("Wrong RawMaterial Code", "Error", MessageBoxIcon.Error);
                return;
            }

            textBox_Scan_Barcode.Text = textBox_Barcode.Text;
            textBox_Scan_Material.Text = textBox_Barcode.Text.Substring(0, 10);
            textBox_Scan_Supply.Text = textBox_Barcode.Text.Substring(10, 6);
            textBox_Scan_DATE.Text = textBox_Barcode.Text.Substring(16, 8);
            textBox_Scan_QTY.Text = textBox_Barcode.Text.Substring(24, 7);
            textBox_Scan_SEQ.Text = textBox_Barcode.Text.Substring(31, 3);
            textBox_Scan_Order.Text = dataGridView_WorkOrder.SelectedRows[0].Cells["ORDER_NO"].Value.ToString();

            if (!(!string.IsNullOrEmpty(textBox_Scan_QTY.Text) && textBox_Scan_QTY.Text.All(char.IsDigit)))
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption("Not number", "Error", MessageBoxIcon.Error);
                return;
            }

            int returnQty = int.Parse(textBox_Scan_QTY.Text);
            int scanQty = int.Parse(dataGridView_RawMaterial.SelectedRows[0].Cells["ScanQty"].Value.ToString());
            int returnResultQty = int.Parse(dataGridView_RawMaterial.SelectedRows[0].Cells["ReturnQty"].Value.ToString());
            int ScanGridViewQty = 0;
            if (dataGridView_ScanList.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView_ScanList.Rows.Count; i++)
                {
                    ScanGridViewQty += int.Parse(dataGridView_ScanList.Rows[i].Cells["Rm_QtyinBox"].Value.ToString());
                }
            }

            if ((returnResultQty + returnQty + ScanGridViewQty) > scanQty)
            {
                ClearBarcodeInfo();
                MessageBox.ShowCaption($"Scan QTY : {scanQty.ToString()}" + Environment.NewLine + $"Return QTY : {(returnResultQty + returnQty + ScanGridViewQty).ToString()}", "Error", MessageBoxIcon.Error);
                return;
            }

            string supplyQuery =
                    $@"
                SELECT TOP (1) Text
                  FROM Supply
                 WHERE Supply = '{textBox_Scan_Supply.Text}'
                ;"
                ;
            var dataRowSupply = DbAccess.Default.GetDataRow(supplyQuery);

            string specQuery =
                    $@"
                SELECT TOP (1) Text
                             , Spec
                  FROM RawMaterial
                 WHERE RawMaterial = '{textBox_Scan_Material.Text}'
                ;"
                ;
            var dataRowSpec = DbAccess.Default.GetDataRow(specQuery);

            textBox_Supply_Text.Text = dataRowSupply["Text"].ToString();
            textBox_RM_Spec.Text = dataRowSpec["Spec"].ToString();
            textBox_RM_Text.Text = dataRowSpec["Text"].ToString();

            string rawMaterial = textBox_Scan_Material.Text;

            FindDataGridViewRawMaterialFirstRow(rawMaterial);
            textBox_Barcode.ReadOnly = true;
            if (dataGridView_ScanList.Rows.Count >= 1)
            {
                if (System.Windows.Forms.MessageBox.Show("Add?", "Xác nhận (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                button_Add.PerformClick();
            }
        }

        private void ClearDataGridViewInfo()
        {
            dataGridView_WorkOrder.DataSource = null;
            dataGridView_RawMaterial.DataSource = null;
            dataGridView_ScanList.Rows.Clear();
        }

        private void ClearLabel()
        {
            ClearTextBox();
            textBox_Scan_Order.Text = "";
            textBox_Scan_Material.Text = "";
            textBox_Barcode.Text = "";
            textBox_RM_Spec.Text = "";
            textBox_RM_Text.Text = "";
            comboBox_location.Text = "";
            comboBox_location_group.SelectedIndex = 0;
            comboBox_From.SelectedIndex = 0;
            comboBox_To.SelectedIndex = 0;
        }

        private void ClearTextBox()
        {
            textBox_Barcode.Text = "";
            textBox_Scan_Barcode.Text = "";
            textBox_Scan_DATE.Text = "";
            textBox_Scan_QTY.Text = "";
            textBox_Scan_SEQ.Text = "";
            textBox_Scan_Supply.Text = "";
            textBox_Supply_Text.Text = "";
        }

        private void ClearBarcodeInfo()
        {
            textBox_Scan_Barcode.Text = "";
            textBox_Scan_DATE.Text = "";
            textBox_Scan_QTY.Text = "";
            textBox_Scan_SEQ.Text = "";
            textBox_Scan_Supply.Text = "";
            textBox_Barcode.Text = "";
            textBox_Supply_Text.Text = "";
        }

        private void AddRawMaterialInfo()
        {
            if (dataGridView_RawMaterial.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView_RawMaterial.SelectedRows[0];

            textBox_Scan_Order.Text = dataGridView_WorkOrder.SelectedRows[0].Cells["ORDER_NO"].Value.ToString();
            textBox_Scan_Material.Text = row.Cells["Material"].Value.ToString();
            string Spec_Query = $@" select 
                                      top(1)
                                      Text,
                                      Spec
                                      from RawMaterial 
                                      where RawMaterial = '{textBox_Scan_Material.Text}' ;";
            var dataTable_Spec = DbAccess.Default.GetDataTable(Spec_Query);
            textBox_RM_Spec.Text = dataTable_Spec.Rows[0]["Spec"].ToString();
            textBox_RM_Text.Text = dataTable_Spec.Rows[0]["Text"].ToString();
            textBox_Scan_Order.Text = dataGridView_WorkOrder.SelectedRows[0].Cells["ORDER_NO"].Value.ToString();
            ClearTextBox();
        }

        private void SaveBarcodeInfoRows()
        {
            for (int i = 0; i < dataGridView_ScanList.Rows.Count; i++)
            {
                string Query = $@"select Rm_BarCode from Rm_Stock where Rm_BarCode = '{dataGridView_ScanList.Rows[i].Cells["Rm_BarCode"].Value.ToString()}' ";
                var dataTable = DbAccess.Default.GetDataTable(Query);
                if (dataTable.Rows.Count > 0)
                {
                    MessageBox.ShowCaption($"Already In Rm_Stock ({dataGridView_ScanList.Rows[i].Cells["Rm_BarCode"].Value.ToString()})", "Error", MessageBoxIcon.Error);
                    return;
                }
            }

            string Q = "";


            for (int i = 0; i < dataGridView_ScanList.Rows.Count; i++)
            {
                int QtyinBox = int.Parse(dataGridView_ScanList.Rows[i].Cells["Rm_QtyinBox"].Value.ToString());
                Q = $@"	DECLARE @TimeStamp				DATETIME		= GETDATE()
                        ;      

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
	                                                , Rm_Order
                                                    , SendStatusErp
                                                    , ERP_SL_CD_FROM
                                                    , ERP_SL_CD_TO
                                                    , Rm_OrderSeq
                                                    , Rm_Created
                                                    , Rm_Updated 
                                                    )
                                SELECT TOP 1 '{dataGridView_ScanList.Rows[i].Cells["Rm_BarCode"].Value}'
                                           , 'IN'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Material"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Supplier"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_ProdDate"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_QtyinBox"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_BoxSeq"].Value}'
                                           , 'RETURN'
                                           , '{QtyinBox}'
                                           , 1
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_LocationGroup"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Location"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Order"].Value}'
                                           , 0
                                           , '{dataGridView_ScanList.Rows[i].Cells["ERP_SL_CD_FROM"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["ERP_SL_CD_TO"].Value}'
                                           , '{dataGridView_RawMaterial.SelectedRows[0].Cells["ORDER_SEQ_NO"].Value}'
                                           , @TimeStamp
                                           , @TimeStamp
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
                                                , Rm_LocationGroup
                                                , Rm_Location 
                                                , Rm_Order
                                                , Storage 
                                                , Rm_Created
                                                , Rm_Updated
                                                 )
                                SELECT TOP 1 '{dataGridView_ScanList.Rows[i].Cells["Rm_BarCode"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Material"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Supplier"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_ProdDate"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_QtyinBox"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_BoxSeq"].Value}'
                                           , 'RETURN'
                                           , '{QtyinBox}'
                                           , 0
                                           , 1
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_LocationGroup"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Location"].Value}'
                                           , '{dataGridView_ScanList.Rows[i].Cells["Rm_Order"].Value}' 
                                           , '{dataGridView_ScanList.Rows[i].Cells["ERP_SL_CD_TO"].Value}' 
                                           , @TimeStamp
                                           , @TimeStamp
                                           ; ";
                int Q_Result = DbAccess.Default.ExecuteQuery(Q);
                if (Q_Result > 0) continue;
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Return.RM_Barcode', N'{Q}', '{WiseApp.Id}', GETDATE())");
                System.Windows.Forms.MessageBox.Show("Information insert failed!\r", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FindDataGridViewRawMaterialFirstRow(string rawMaterial)
        {
            dataGridView_RawMaterial.ClearSelection();
            foreach (DataGridViewRow dataGridViewRow in dataGridView_RawMaterial.Rows)
            {
                if ($"{dataGridViewRow.Cells["Material"].Value}" != rawMaterial) continue;
                dataGridViewRow.Selected = true;
                return;
            }

            ClearLabel();
            MessageBox.ShowCaption("Không tìm thấy。\r\nNot Found.", "Error", MessageBoxIcon.Error);
        }
    }

    #endregion
}