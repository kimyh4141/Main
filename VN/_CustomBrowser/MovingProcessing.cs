using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class MovingProcessing : Form
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

        public MovingProcessing()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void Initialize()
        {
            ScanQty = 0;        
            tb_pallet.Text = string.Empty;
            lb_qty.Text = ScanQty.ToString();         
            dataGridView_List.Rows.Clear();
          
        }

        private bool VerifyPallet(string barcode)
        {        
            try
            {
                if (string.IsNullOrEmpty(tb_pallet.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Scan Pallet Barcode!");
                    return false;
                }
                if (tb_pallet.Text.Substring(0, 1) != "P")
                {
                    System.Windows.Forms.MessageBox.Show("It is not Pallet BarCode!");
                    return false;
                }
              
                if (string.IsNullOrEmpty(comboBox_category.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Select Stock In!");
                    return false;
                }
                string[] temp = comboBox_category.Text.Split('/');
                string category = temp[0].ToString().Trim();

                string query_slcd = $@"Select top(1) SL_CD from Stock where PalletBcd = '{tb_pallet.Text}'";
                var SL_CD = DbAccess.Default.ExecuteScalar(query_slcd);

                if (category == "VP10")
                {
                    if (SL_CD.ToString() == "VP10")
                    {
                        System.Windows.Forms.MessageBox.Show("Can Not Move VP10 -> VP10");
                        return false;
                    }
                    
                }
                if (category != "VP10")
                {
                    if (SL_CD.ToString() != "VP10")
                    {
                        System.Windows.Forms.MessageBox.Show("Can Not Move "+ category +" -> "+ SL_CD.ToString());
                        return false;
                    }
                   
                }             
                //첫번째 바코드 스캔시 품목 정보 입력
                if (dataGridView_List.Rows.Count == 0)
                {
                    string material_query = $@" select top(1) Stock.Material , Spec from Stock 
                                                left join Material as MT 
                                                on Stock.Material = MT.Material
                                                where PalletBcd = '{tb_pallet.Text}' ";
                    var material_code = DbAccess.Default.GetDataTable(material_query);
                    textBox_Material_Search.Text = material_code.Rows[0]["Spec"].ToString();
                    label_Material.Text = material_code.Rows[0]["Material"].ToString();
                }
                if (dataGridView_List.RowCount > 0) //성진/세현 섞어서 스캔하기 방지
                {
                    dataGridView_List.Rows[0].Cells["SL_CD"].Value.ToString();
                    string query2 = $@"Select top(1) SL_CD from Stock where PalletBcd = '{tb_pallet.Text}'";
                   // var SL_CD = DbAccess.Default.ExecuteScalar(query2);                 
                    if (dataGridView_List.Rows[0].Cells["SL_CD"].Value.ToString() != SL_CD.ToString())
                    {
                        System.Windows.Forms.MessageBox.Show("Input SL_CD = "+dataGridView_List.Rows[0].Cells["SL_CD"].Value.ToString() +Environment.NewLine+ "Scan SL_CD = "+ SL_CD);
                        return false;
                    }                
                }
                string count_query = $@"Select Count(*) from Stock where PalletBcd = '{tb_pallet.Text}'";
                if (Convert.ToInt32(DbAccess.Default.ExecuteScalar(count_query)) == 0)
                {
                    System.Windows.Forms.MessageBox.Show("This Barcode is Empty!");
                    return false;
                }
                //이미 입력됬는지 확인
                foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
                {
                    if (!dataGridViewRow.Cells["PalletBarcode"].Value.ToString().Equals(barcode)) continue;
                    System.Windows.Forms.MessageBox.Show("Đã tồn tại trong danh sách。(It already exists in the list.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                //선택한 통합작업을 이미 마쳤는지 여부
               
                string SL_CD_Query = $@" Select COUNT(*) from Stock where PalletBcd = '{tb_pallet.Text}' and SL_CD = '{category}' ";
                if (Convert.ToInt32(DbAccess.Default.ExecuteScalar(SL_CD_Query)) > 0)
                {
                    System.Windows.Forms.MessageBox.Show("This Pallet is Already Combine!");
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
           
                if (label_Material.Text != material)
                {
                    System.Windows.Forms.MessageBox.Show(@"Không khớp với mục được chỉ định。(It does not match the indicated item.)");
                    return false;
                }
                            
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"Lỗi tải dữ liệu Pallet。(Fail to Load Pallet Data.) \r\n{e.Message}");
                return false;
            }
            return true;
        }

        private void Clear()
        {
            Id = string.Empty;
            OrderNo = string.Empty;
            OrderNoSeq = string.Empty;
            label_Material.Text = "Material";
            TotalQty = 0;
            ScanQty = 0;
            lb_qty.Text = "0";
            Material = string.Empty;
            textBox_Material_Search.Text = string.Empty;
            
         
            comboBox_category.Enabled = true;
            comboBox_category.SelectedIndex = 0;
        }
       

        private bool Save()
        {
            string[] temp = comboBox_category.Text.Split('/');
            string category = temp[0].Trim();
            var palletList = string.Empty;          
            var query = new StringBuilder();
            query.AppendLine
                (
                 $@"
                DECLARE @IF_TIME DATETIME = GETDATE()
                ;
                "
                );          
            //스캔입력
            if (0 < dataGridView_List.Rows.Count)
            {
                foreach (DataGridViewRow dataGridViewRow in dataGridView_List.Rows)
                {
                    palletList += $@",'{dataGridViewRow.Cells["PalletBarcode"].Value}'";
                }
               //Update = Stock , Insert = StockHist(OUT , VP10G or VP10S) / StockHist(IN , VP10)
                query.AppendLine
                (
                 $@"
                BEGIN TRAN

                BEGIN TRY

                  INSERT
                    INTO StockHist (
                                     IoType
                                   , PcbBcd
                                   , BoxBcd
                                   , PalletBcd
                                   , Material
                                   , PLANT_CD
                                   , From_SL_CD
                                   , SL_CD
                                   , OldPcbBcd
                                   , OldMaterial
                                   , ErpOrderNo
                                   , ErpOrderNoSeq
                                   , Updated
                                   )
                  SELECT 'MOVE'
                       , PcbBcd
                       , BoxBcd
                       , PalletBcd
                       , Material
                       , PLANT_CD
                       , SL_CD
                       , '{category}'
                       , OldPcbBcd
                       , OldMaterial
                       , ''
                       , 0
                       , @IF_TIME
                    FROM Stock
                   WHERE PalletBcd IN ({palletList.Substring(1)});

                  UPDATE Stock
                     SET SL_CD   = '{category}'
                       , Updated = @IF_TIME
                   WHERE PalletBcd IN ({palletList.Substring(1)});
                  SELECT 'T'
                  COMMIT;

                END TRY
                BEGIN CATCH

                  ROLLBACK;

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
                           'E'
                         , 'Browser'
                         , 'Combine(G/S)'
                         , LEFT(ISNULL(ERROR_MESSAGE(), ''), 3000)
                         , '{WiseApp.Id}'
                         , GETDATE()
                         );
                  SELECT 'F'
                END CATCH
                ;
                "
                );
            }
            //int result;
            string result = "";
            try
            {
                //result = DbAccess.Default.ExecuteQuery(query.ToString());
                result = DbAccess.Default.ExecuteScalar(query.ToString()).ToString();
                if (result != "T")
                {
                    MessageBox.Show("Fail! Check Network", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show($"Fail!\r\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            //if (result != 0)
            //{
            //    MessageBox.Show("Success!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return true;
            //}

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

                   select ITEM_CD+' / '+M.Spec as Material from Material as M
                   left join [MES_IF_VN].[dbo].[M_B_ITEM] as IF_M
                   on M.Material = IF_M.ITEM_CD
                   where M.Status = 1 and LEFT( IF_M.ITEM_CD,1) = 1 and M.Spec not like '%PV%' and M.Spec not like '%사용금지%'
                   order by ITEM_CD 
                    ;
                    "
                    );
                        
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
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
            string query2 = $@"Select top(1) SL_CD from Stock where PalletBcd = '{palletBarcode}'";
            var SL_CD = DbAccess.Default.ExecuteScalar(query2);
            var time = DateTime.Now.Hour + " : " + DateTime.Now.Minute;
            var addIndex = dataGridView_List.Rows.Add(palletBarcode, qty, SL_CD, time);
            dataGridView_List.FirstDisplayedScrollingRowIndex = addIndex;
            ScanQty += qty;
          
         
            comboBox_category.Enabled = false;
            lb_qty.Text = ScanQty.ToString();
            tb_pallet.Text = string.Empty;
            tb_pallet.Focus();
            
        }

        private void comboBox_Material_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
            dataGridView_List.Rows.Clear();
        }

        private void textBox_Material_Search2_KeyUp(object sender, KeyEventArgs e)
        {
         
          

        }

        private void button_save_Click_1(object sender, EventArgs e)
        {
            if (dataGridView_List.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Scan Barcode!");
                return;
            }
            if (MessageBox.Show("Bạn có muốn xử lý ngay?？(Process now?)", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            if (Save())
            {
                Initialize();
                Clear();
            }
        }
        #endregion

        private void comboBox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
