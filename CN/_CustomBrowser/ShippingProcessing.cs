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

        private string _id = string.Empty;
        private string _orderNo = string.Empty;
        private string _orderNoSeq = string.Empty;
        private int _totalQty = 0;
        private int _remainQty = 0;
        private int _scanQty = 0;
        private string _material = string.Empty;
        private string _bpcd = string.Empty;
        private string _bpnm = string.Empty;

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
            _scanQty = 0;

            textBox_OrderNo.Text = string.Empty;
            textBox_OrderSeq.Text = string.Empty;
            label_OrderQtyValue.Text = string.Empty;
            textBox_BP_CD.Text = string.Empty;
            textBox_BP_NM.Text = string.Empty;

            textBox_Material.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;

            tb_pallet.Text = string.Empty;
            lb_qty.Text = _scanQty.ToString();

            dataGridView_List.Rows.Clear();

            numericUpDown_AddQty.Value = 0;
        }

        private bool VerifyPallet(string barcode)
        {
            if (checkBox_OrderStatus.Checked)
            {
                if (string.IsNullOrEmpty(textBox_OrderNo.Text))
                {
                    System.Windows.Forms.MessageBox.Show("请选择订单。(Please select an order.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_material))
                {
                    System.Windows.Forms.MessageBox.Show("请输入材料代码。(Please enter material code.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            try
            {
                //이미 입력됬는지 확인
                foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
                {
                    if (!dataGridViewRow.Cells["PalletBarcode"].Value.ToString().Equals(barcode)) continue;
                    System.Windows.Forms.MessageBox.Show("它已经在列表中存在了。(It already exists in the list.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                //Stock에 있는 Pallet 인지
                string stockQuery = "Select * from Stock where PalletBcd = '" + barcode + "'";
                if (DbAccess.Default.GetDataTable(stockQuery).Rows.Count < 1)
                {
                    System.Windows.Forms.MessageBox.Show("这个托盘没有存货。(This Pallet is not in Stock.)");
                    return false;
                }

                //팔레트에 블락킹되있는 박스가 있는지
                string blockCheckQuery = "Select Count(*) from Stock where PalletBcd = '" + barcode + "' AND Block = 1";

                if (Convert.ToInt32(DbAccess.Default.ExecuteScalar(blockCheckQuery)) > 0)
                {
                    System.Windows.Forms.MessageBox.Show("此托盘包括闭塞箱。(This Pallet include blocking Box.)");
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
                if (_material != material)
                {
                    System.Windows.Forms.MessageBox.Show(@"它与所指示的项目不符。(It does not match the indicated item.)");
                    return false;
                }

                // 선입선출 확인
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"无法装载托盘数据。(Fail to Load Pallet Data.) \r\n{e.Message}");
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
                               , NULLIF('{_orderNo}', '')
                               , NULLIF('{_orderNoSeq}', 0)
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
                         WHERE T.PLANT_CD = 'PL30'
                           AND T.SL_CD = '980318-P'
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
                             , NULLIF('{_orderNo}', '')
                             , NULLIF('{_orderNoSeq}', 0)
                             , '{textBox_Material.Text}'
                             , Qty
                             , PalletList
                             , 'PL30'
                             , '980318-P'
                             , @IF_TIME
                          FROM (
                                   SELECT STRING_AGG(T.PalletBcd, ',') AS PalletList
                                        , SUM(Qty)                     AS Qty
                                     FROM (
                                              SELECT T.PalletBcd
                                                   , COUNT(T.PcbBcd) AS Qty
                                                FROM #TEMP T
                                               WHERE T.PLANT_CD = 'PL30'
                                                 AND T.SL_CD = '980318-P'
                                               GROUP BY T.PalletBcd
                                          ) AS T
                               ) AS T
                    END
                IF (EXISTS
                    (
                        SELECT 'X'
                          FROM #TEMP T
                         WHERE T.PLANT_CD = 'PL35'
                           AND T.SL_CD = '980318'
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
                             , NULLIF('{_orderNo}', '')
                             , NULLIF('{_orderNoSeq}', 0)
                             , '{textBox_Material.Text}'
                             , Qty
                             , PalletList
                             , 'PL35'
                             , '980318'
                             , @IF_TIME
                          FROM (
                                   SELECT STRING_AGG(T.PalletBcd, ',') AS PalletList
                                        , SUM(Qty)                     AS Qty
                                     FROM (
                                              SELECT T.PalletBcd
                                                   , COUNT(T.PcbBcd) AS Qty
                                                FROM #TEMP T
                                               WHERE T.PLANT_CD = 'PL35'
                                                 AND T.SL_CD = '980318'
                                               GROUP BY T.PalletBcd
                                          ) AS T
                               ) AS T
                    END
                UPDATE MES_IF_CN.dbo.ETM_SHIP_REQ
                   SET APPLY_FLAG = N'Y'
                     , APPLY_TIME = @IF_TIME
                 WHERE IF_ID = '{_id}'
                   AND SO_NO = NULLIF('{_orderNo}', '')
                   AND SO_SEQ = NULLIF('{_orderNoSeq}', 0)
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
                     , NULLIF('{_orderNo}', '')
                     , NULLIF('{_orderNoSeq}', 0)
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
            var result = 0;
            try
            {
                result = DbAccess.Default.ExecuteQuery(query.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"Fail!\r\n{e.Message}", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                lb_qty.Text = _scanQty.ToString();

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
                  FROM MES_IF_CN.dbo.M_B_STORAGE_LOC
                 WHERE SL_GROUP_CD = 'G10'
                   AND PLANT_CD IN ('PL30', 'PL35')
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
                        _id = shippingRequestOrderSearch.Id;
                        _orderNo = shippingRequestOrderSearch.OrderNo;
                        _orderNoSeq = shippingRequestOrderSearch.OrderSeqNo;
                        _material = shippingRequestOrderSearch.Material;
                        _totalQty = Convert.ToInt32(shippingRequestOrderSearch.OrderQty);
                        _remainQty = Convert.ToInt32(shippingRequestOrderSearch.RemainQty);
                        _bpcd = shippingRequestOrderSearch.BusinessPartnerCode;
                        _bpnm = shippingRequestOrderSearch.BusinessPartnerName;
                    }
                }
            }

            if (string.IsNullOrEmpty(_orderNo))
            {
                textBox_OrderNo.Text = string.Empty;
                textBox_OrderSeq.Text = string.Empty;
                System.Windows.Forms.MessageBox.Show("请选择订单。(Please select Order.)");
                return;
            }

            textBox_OrderNo.Text = _orderNo;
            textBox_OrderSeq.Text = _orderNoSeq;
            label_OrderQtyValue.Text = $@"{_remainQty:#,###} / {_totalQty:#,###}";
            textBox_BP_CD.Text = _bpcd;
            textBox_BP_NM.Text = _bpnm;
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

                _material = shippingSelectMaterial.material;
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
                System.Windows.Forms.MessageBox.Show("没有找到匹配的项目。(No matching items found.)");
                textBox_Material.Text = string.Empty;
                textBox_MaterialName.Text = string.Empty;
                textBox_Spec.Text = string.Empty;
                _material = string.Empty;

                return false;
            }

            _material = material;
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
                && _remainQty < _scanQty + qty + numericUpDown_AddQty.Value)
            {
                System.Windows.Forms.MessageBox.Show("超出订购数量。(Exceed Ordered Quantity.)");
                return;
            }

            var time = DateTime.Now.Hour + " : " + DateTime.Now.Minute;
            var addIndex = dataGridView_List.Rows.Add(palletBarcode, qty, time);
            dataGridView_List.FirstDisplayedScrollingRowIndex = addIndex;

            _scanQty += qty;

            lb_qty.Text = _scanQty.ToString();
            tb_pallet.Text = string.Empty;
            tb_pallet.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkBox_OrderStatus.Checked && _remainQty < _scanQty + numericUpDown_AddQty.Value)
            {
                System.Windows.Forms.MessageBox.Show("订货数量不够。(Not enough Quantity for Order.)");
                return; 
            }

            if (MessageBox.Show("现在开始？(Process now?)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            if (Save())
            {
                Initialize();
            }
        }

        #endregion
        
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
            Initialize();
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
