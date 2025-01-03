using System;
using System.Linq;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.WMS
{
    public partial class DirectReceiptDelete : Form
    {
        public DirectReceiptDelete()
        {
            InitializeComponent();
        }

        private void button_RawMaterial_Search_Click(object sender, EventArgs e)
        {
            ScanRawMaterial();
        }

        private void ScanRawMaterial()
        {
            if (textBox_Barcode.TextLength != 34)
            {
                MessageBox.ShowCaption("Wrong Barcode", "Error", MessageBoxIcon.Error);
                return;
            }

            if (DbAccess.Default.IsExist("Rm_StockTempHist", $"Rm_BarCode = '{textBox_Barcode.Text}'") < 1)
            {
                MessageBox.ShowCaption("Barcode not found.", "Error", MessageBoxIcon.Error);
                return;
            }

            textBox_Scan_Barcode.Text = textBox_Barcode.Text.Trim();
            textBox_Scan_Material.Text = textBox_Barcode.Text.Substring(0, 10);
            textBox_Scan_Supply.Text = textBox_Barcode.Text.Substring(10, 6);
            textBox_Scan_DATE.Text = textBox_Barcode.Text.Substring(16, 8);
            textBox_Scan_QTY.Text = textBox_Barcode.Text.Substring(24, 7);
            textBox_Scan_SEQ.Text = textBox_Barcode.Text.Substring(31, 3);

            if (!(!string.IsNullOrEmpty(textBox_Scan_QTY.Text) && textBox_Scan_QTY.Text.All(char.IsDigit)))
            {
                MessageBox.ShowCaption("Not number", "Error", MessageBoxIcon.Error);
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
        }

        private bool ProcessDirectReceiptDelete()
        {
            try
            {
                var query = $@"
                            DELETE Rm_StockTempHist
                            OUTPUT deleted.Rm_BarCode
                                 , deleted.Rm_IO_Type
                                 , deleted.Rm_Material
                                 , deleted.Rm_Supplier
                                 , deleted.Rm_ProdDate
                                 , deleted.Rm_QtyinBox
                                 , deleted.Rm_BoxSeq
                                 , deleted.Rm_Bunch
                                 , deleted.Rm_Kind
                                 , deleted.Rm_StockQty
                                 , deleted.Rm_Status
                                 , deleted.Rm_Order
                                 , deleted.Rm_MoveStatus
                                 , deleted.Rm_BadLot
                                 , '{WiseApp.Id}'
                                 , GETDATE()
                              INTO RawMaterialStockDirectReceiptDeleteHist (Rm_BarCode, Rm_IO_Type, Rm_Material, Rm_Supplier, Rm_ProdDate, Rm_QtyinBox, Rm_BoxSeq, Rm_Bunch, Rm_Kind, Rm_StockQty, Rm_Status, Rm_Order, Rm_MoveStatus, Rm_BadLot, Creator, Rm_Created)
                             WHERE Rm_BarCode = '%{textBox_Scan_Barcode.Text}%'
                            ";
                DbAccess.Default.ExecuteQuery(query);
                //저장완료 메시지
                System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)", "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (DbAccess.Default.IsExist("Rm_StockTempHist", $"Rm_BarCode = '{textBox_Barcode.Text}'") < 1)
            {
                MessageBox.ShowCaption("Barcode not found.", "Error", MessageBoxIcon.Error);
                return;
            }

            if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)", "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)) return;
            if (ProcessDirectReceiptDelete())
            {
                Close();
            }
        }

        private void textBox_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ScanRawMaterial();
            }
        }
    }
}