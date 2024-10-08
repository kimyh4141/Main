using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class ShippingProcessing : Form
    {
        #region Field

        private string Id { get; set; }
        private string OrderNo { get; set; }
        private string OrderNoSeq { get; set; }
        private int TotalQty { get; set; }
        private int RemainQty { get; set; }
        private int ScanQty { get; set; }
        private string Material { get; set; }
        private string BusinessPartnerCode { get; set; }
        private string BusinessPartnerName { get; set; }

        #endregion

        #region Constructor

        public ShippingProcessing()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void Initialize()
        {
            ScanQty = 0;

            textBox_OrderNo.Text = string.Empty;
            textBox_OrderSeq.Text = string.Empty;
            label_OrderQtyValue.Text = string.Empty;

            textBox_Material.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;

            tb_pallet.Text = string.Empty;
            lb_qty.Text = ScanQty.ToString();

            textBox_BP_CD.Text = string.Empty;
            textBox_BP_NM.Text = string.Empty;
            dataGridView_List.Rows.Clear();

            numericUpDown_AddQty.Value = 0;         
        }

        private bool VerifyPallet(string barcode)
        {
            if (checkBox_OrderStatus.Checked)
            {
                if (string.IsNullOrEmpty(textBox_OrderNo.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Vui lòng chọn một đơn hàng。(Please select an order.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(Material))
                {
                    System.Windows.Forms.MessageBox.Show("Vui lòng nhập mã vật liệu。(Please enter material code.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            try
            {
                //이미 입력됬는지 확인
                foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
                {
                    if (dataGridViewRow.Cells["PalletBarcode"].Value.ToString().Equals(barcode))
                    {
                        System.Windows.Forms.MessageBox.Show("Đã tồn tại trong danh sách。(It already exists in the list.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                //Stock에 있는 Pallet 인지
                string stockQuery = "Select * from Stock where PalletBcd = '" + barcode + "'";
                if (DbAccess.Default.GetDataTable(stockQuery).Rows.Count < 1)
                {
                    System.Windows.Forms.MessageBox.Show("Pallet này không có trong kho。(This Pallet is not in Stock.)");
                    return false;
                }
                //VP10 확인 
                string VpCheckQuery = $@" select COUNT(*) from Stock where PalletBcd = '{barcode}' and SL_CD  = 'VP10' ";
                if (Convert.ToInt32(DbAccess.Default.ExecuteScalar(VpCheckQuery)) < 1)
                {
                    System.Windows.Forms.MessageBox.Show("This PalletBCD(" + barcode + ") is Not VP10!");
                    return false;
                }
                //팔레트에 블락킹되있는 박스가 있는지
                string BlockQuery = "Select Count(*) from Stock where PalletBcd = '" + barcode + "' AND Block = 1";

                if (Convert.ToInt32(DbAccess.Default.ExecuteScalar(BlockQuery)) > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Pallet này có bao gồm Box bị Blocking。(This Pallet include blocking Box.)");
                    return false;
                }

                //품목이 같은지 확인
                string material = DbAccess.Default.ExecuteScalar
                    (
                     $@"
                    SELECT DISTINCT Material
                      FROM Stock
                     WHERE PalletBcd = '{barcode}'
                    ;
                    "
                    ).ToString();
                if (Material != material)
                {
                    System.Windows.Forms.MessageBox.Show(@"Không khớp với mục được chỉ định。(It does not match the indicated item.)");
                    return false;
                }

                // 선입선출 확인
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi tải dữ liệu Pallet。(Fail to Load Pallet Data.) \r\n{e.Message}");
                return false;
            }

            return true;
        }

        private bool Save()
        {
            var palletList = string.Empty;


            string type = checkBox_OrderStatus.Checked ? "ORDER" : "OTHER";
            var query = new StringBuilder();

            query.AppendLine
                (
                 $@"
                DECLARE @IF_TIME DATETIME = GETDATE()
                ;
                "
                );
            //수기입력
            if (0 < comboBox_Storage.SelectedIndex && 0 < numericUpDown_AddQty.Value)
            {
                if (!(comboBox_Storage.SelectedItem is DataRowView dataRowView)) return false;
                query.AppendLine
                    (
                     $@"
                    BEGIN
                        INSERT
                          INTO ShippingHist ( Type
                                            , IsScan
                                            , ErpOrderNo
                                            , ErpOrderNoSeq
                                            , Material
                                            , Qty
                                            , PLANT_CD
                                            , SL_CD
                                            , Updated
                                            )
                        VALUES ( '{type}'
                               , 0
                               , NULLIF('{OrderNo}', '')
                               , NULLIF('{OrderNoSeq}', 0)
                               , '{textBox_Material.Text}'
                               , '{(int)numericUpDown_AddQty.Value}'
                               , '{dataRowView.Row["PLANT_CD"]}'
                               , '{dataRowView.Row["SL_CD"]}'
                               , @IF_TIME
                               );
                    END
                    "
                    );
            }
            //스캔입력
            if (0 < dataGridView_List.Rows.Count)
            {
                foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
                {
                    palletList += $@",'{dataGridViewRow.Cells["PalletBarcode"].Value}'";
                }

                query.AppendLine
                (
                 $@"
IF OBJECT_ID('tempdb..#TEMP') IS NOT NULL
    BEGIN
        DROP TABLE #TEMP;
    END
SELECT RecordID
     , PcbBcd
     , BoxBcd
     , PalletBcd
     , Material
     , Manufacturer
     , PLANT_CD
     , SL_CD
     , Block
     , OldPcbBcd
     , OldMaterial
     , Created
     , Updated
  INTO #TEMP
  FROM Stock
 WHERE PalletBcd IN ({palletList.Substring(1)}) 
  
;

IF (EXISTS
    (
        SELECT 'X'
          FROM #TEMP T
         WHERE T.PLANT_CD = 'PL40'
           AND T.SL_CD = 'VP10'
    ))
    BEGIN
        INSERT
          INTO ShippingHist ( Type
                            , IsScan
                            , ErpOrderNo
                            , ErpOrderNoSeq
                            , Material
                            , Qty
                            , PalletList
                            , PLANT_CD
                            , SL_CD
                            , Updated )
        SELECT '{type}' AS Type
             , 1        AS IsScan
             , NULLIF('{OrderNo}', '')
             , NULLIF('{OrderNoSeq}', 0)
             , '{textBox_Material.Text}'
             , Qty
             , PalletList
             , 'PL40'
             , 'VP10'
             , @IF_TIME
          FROM (
                   SELECT STRING_AGG(T.PalletBcd, ',') AS PalletList
                        , SUM(Qty)                     AS Qty
                     FROM (
                              SELECT T.PalletBcd
                                   , COUNT(T.PcbBcd) AS Qty
                                FROM #TEMP T
                               WHERE T.PLANT_CD = 'PL40'
                                 AND T.SL_CD = 'VP10'
                               GROUP BY T.PalletBcd
                          ) AS T
               ) AS T
    END
UPDATE MES_IF_VN.dbo.ETM_SHIP_REQ
   SET APPLY_FLAG = N'Y'
     , APPLY_TIME = @IF_TIME
 WHERE IF_ID = '{Id}'
   AND SO_NO = NULLIF('{OrderNo}', '')
   AND SO_SEQ = NULLIF('{OrderNoSeq}', 0)
;

INSERT
  INTO StockHist ( IoType
                 , PcbBcd
                 , BoxBcd
                 , PalletBcd
                 , Material
                 , PLANT_CD
                 , SL_CD
                 , OldPcbBcd
                 , OldMaterial
                 , ErpOrderNo
                 , ErpOrderNoSeq )
SELECT 'OUT'
     , PcbBcd
     , BoxBcd
     , PalletBcd
     , Material
     , PLANT_CD
     , SL_CD
     , OldPcbBcd
     , OldMaterial
     , NULLIF('{OrderNo}', '')
     , NULLIF('{OrderNoSeq}', 0)
  FROM #TEMP
;

DELETE
  FROM Stock
 WHERE EXISTS
     (
         SELECT 'X'
           FROM #TEMP T
          WHERE Stock.RecordID = T.RecordID
     )
;
                "
                );
            }
            int result;
            try
            {
                result = DbAccess.Default.ExecuteQuery(query.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Fail!\r\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (result != 0)
            {
                MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            string msg = "";
            msg += "Stock, ";

            MessageBox.Show("Error : " + msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        #endregion

        #region Event

        private void ShippingProcessing_Load(object sender, EventArgs e)
        {
            try
            {
                lb_qty.Text = ScanQty.ToString();

                var dataTable = DbAccess.Default.GetDataTable
                    (
                     $@"
                    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
                    ;

                    SELECT '' AS PLANT_CD
                         , '' AS SL_CD
                         , '' AS SL_NM
                     UNION ALL
                    SELECT PLANT_CD
                         , SL_CD
                         , SL_NM
                      FROM MES_IF_VN.dbo.M_B_STORAGE_LOC
                     WHERE SL_GROUP_CD = 'G10'
                       AND PLANT_CD IN ('PL40')
                    ;
                    "
                    );

                comboBox_Storage.DataSource = dataTable;
                comboBox_Storage.DisplayMember = "SL_NM";
                comboBox_Storage.ValueMember = "SL_CD";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }

        private void button_SelectOrder_Click(object sender, EventArgs e)
        {
            using (var shippingRequestOrderSearch = new ShippingRequestOrderSearch())
            {
                shippingRequestOrderSearch.ShowDialog();
                if (shippingRequestOrderSearch.DialogResult == DialogResult.OK)
                {
                    if (SetMaterial(shippingRequestOrderSearch.Material))
                    {
                        Id = shippingRequestOrderSearch.Id;
                        OrderNo = shippingRequestOrderSearch.OrderNo;
                        OrderNoSeq = shippingRequestOrderSearch.OrderSeqNo;
                        Material = shippingRequestOrderSearch.Material;
                        TotalQty = Convert.ToInt32(shippingRequestOrderSearch.OrderQty);
                        RemainQty = Convert.ToInt32(shippingRequestOrderSearch.RemainQty);
                        BusinessPartnerCode = shippingRequestOrderSearch.BusinessPartnerCode;
                        BusinessPartnerName = shippingRequestOrderSearch.BusinessPartnerName;
                    }
                }
            }

            if (string.IsNullOrEmpty(OrderNo))
            {
                textBox_OrderNo.Text = string.Empty;
                textBox_OrderSeq.Text = string.Empty;
                System.Windows.Forms.MessageBox.Show("Vui lòng chọn một đơn hàng。(Please select Order.)");
                return;
            }

            textBox_OrderNo.Text = OrderNo;
            textBox_OrderSeq.Text = OrderNoSeq;
            label_OrderQtyValue.Text = $@"{RemainQty:#,###} / {TotalQty:#,###}";
            textBox_BP_CD.Text = BusinessPartnerCode;
            textBox_BP_NM.Text = BusinessPartnerName;
            dataGridView_List.Rows.Clear();
            tb_pallet.Focus();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            using (var shippingSelectMaterial = new ShippingSelectMaterial())
            {
                shippingSelectMaterial.ShowDialog();
                if (shippingSelectMaterial.DialogResult != DialogResult.OK) return;
                textBox_Material.Text = shippingSelectMaterial.material;
                textBox_MaterialName.Text = shippingSelectMaterial.text;
                textBox_Spec.Text = shippingSelectMaterial.spec;

                Material = shippingSelectMaterial.material;
            }
        }

        private bool SetMaterial(string material)
        {
            var query = new StringBuilder();
            query.AppendLine
                (
                 $@"
                SELECT Material
                     , Text
                     , Spec
                  FROM Material
                 WHERE Material = '{material}'
                ;
                "
                );
            var dataRow = DbAccess.Default.GetDataRow(query.ToString());
            if (dataRow is null)
            {
                System.Windows.Forms.MessageBox.Show("Không tìm thấy danh mục phù hợp。(No matching items found.)");
                textBox_Material.Text = string.Empty;
                textBox_MaterialName.Text = string.Empty;
                textBox_Spec.Text = string.Empty;
                Material = string.Empty;

                return false;
            }

            Material = material;
            textBox_Material.Text = material;
            textBox_MaterialName.Text = dataRow["Text"].ToString();
            textBox_Spec.Text = dataRow["Spec"].ToString();
            return true;
        }

        private void tb_pallet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            var palletBarcode = tb_pallet.Text.Trim();
            //검증
            if (!VerifyPallet(palletBarcode))
            {
                tb_pallet.Text = string.Empty;
                return;
            }

            //몇개의 박스가 선택한 팔레트로 적재되있는지 가져오기
            string query = $@"Select Count(*) from Stock where PalletBcd = '{palletBarcode}'";
            int qty = Convert.ToInt32(DbAccess.Default.ExecuteScalar(query));
            if (checkBox_OrderStatus.Checked
                && RemainQty < ScanQty + qty + numericUpDown_AddQty.Value)
            {
                System.Windows.Forms.MessageBox.Show("Vượt quá số lượng đã yêu cầu。(Exceed Ordered Quantity.)");
                return;
            }

            var time = DateTime.Now.Hour + " : " + DateTime.Now.Minute;
            var addIndex = dataGridView_List.Rows.Add(palletBarcode, qty, time);
            dataGridView_List.FirstDisplayedScrollingRowIndex = addIndex;

            ScanQty += qty;

            lb_qty.Text = ScanQty.ToString();
            tb_pallet.Text = string.Empty;
            tb_pallet.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Material.Text))
            {
                return;
            }
            if (checkBox_OrderStatus.Checked && RemainQty < ScanQty + numericUpDown_AddQty.Value)
            {
                System.Windows.Forms.MessageBox.Show("Không đủ số lượng yêu cầu。(Not enough Quantity for Order.)");
                return; 
            }
            //
            // //gmryu 2023-08-12 지시수량이 토탈수량보다 적을때 경고매시지 팝업            
            // int compare_order_qty = int.Parse(label_OrderQtyValue.Text.Replace(",", "").Trim());
            // int compare_total_qty = int.Parse(lb_qty.Text.Replace(",", "").Trim());           
            // if (compare_order_qty < compare_total_qty)//비교
            // {
            //     //yes or no
            //     if (MessageBox.Show("지시수량이 토탈 수량보다 적은데 등록하시겠습니까?(번역 필요)", "Cảnh báo(Warning)", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            // }

            if (MessageBox.Show("Bạn có muốn xử lý ngay?？(Process now?)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;

            if (Save())
            {
                Initialize();
            }
        }

        #endregion

        private void Clear()
        {
            textBox_Material.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;
            textBox_OrderNo.Text = string.Empty;
            textBox_OrderSeq.Text = string.Empty;
            textBox_BP_CD.Text = string.Empty;
            textBox_BP_NM.Text = string.Empty;
            label_OrderQtyValue.Text = "-";

            Id = string.Empty;
            OrderNo = string.Empty;
            OrderNoSeq = string.Empty;
            TotalQty = 0;
            ScanQty = 0;
            Material = string.Empty;
        }

        private void checkBox_OrderStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_OrderStatus.Checked)
            {
                button_SelectOrder.Enabled = true;
                btn_Search.Enabled = false;
                label_OrderQtyValue.Visible = true;
            }
            else
            {
                button_SelectOrder.Enabled = false;
                btn_Search.Enabled = true;
                label_OrderQtyValue.Visible = true;
            }

            Clear();
        }

        private void numericUpDown_AddQty_Leave(object sender, EventArgs e)
        {
            if (!(sender is NumericUpDown numericUpDown)) return;
            if (string.IsNullOrEmpty(numericUpDown.Text))
            {
                numericUpDown.Value = 0;
            }
        }

        private void comboBox_Storage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(sender is ComboBox comboBox)) return;
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    numericUpDown_AddQty.Value = 0;
                    numericUpDown_AddQty.Enabled = false;
                    break;
                default:
                    numericUpDown_AddQty.Enabled = true;
                    break;
            }
        }
    }
}
