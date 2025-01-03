using System;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class ShipBack : Form
    {
        public ShipBack()
        {
            InitializeComponent();
        }

        private void Search()
        {
            try
            {
                var query = $@"
                        SELECT TOP 1 
                               SH.ShippingHist
                             , SH.Type
                             , SH.IsScan
                             , SH.ErpOrderNo
                             , SH.ErpOrderNoSeq
                             , SH.Material
                             , M.Text
                             , M.Spec
                             , SH.Qty
                             , SH.PalletList
                             , SH.PLANT_CD
                             , SH.SL_CD
                             , SH.SendStatusErp
                             , SH.Updated
                          FROM ShippingHist             AS SH
                               LEFT OUTER JOIN Material AS M
                                               ON SH.Material = M.Material
                         WHERE PalletList LIKE '%{textBox_PalletCode.Text}%'
                         ";
                var dataTable = DbAccess.Default.GetDataTable(query);
                if (dataTable.Rows.Count < 1)
                {
                    System.Windows.Forms.MessageBox.Show($@"Not found.", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                textBox_ShippingHist.Text = $@"{dataTable.Rows[0]["ShippingHist"]}";
                textBox_Qty.Text = $@"{dataTable.Rows[0]["Qty"]}";
                textBox_Material.Text = $@"{dataTable.Rows[0]["Material"]}";
                textBox_MaterialName.Text = $@"{dataTable.Rows[0]["Text"]}";
                textBox_Spec.Text = $@"{dataTable.Rows[0]["Spec"]}";
                string palletList = $@"{dataTable.Rows[0]["PalletList"]}";
                foreach (var palletBarcode in palletList.Split(','))
                {
                    dataGridView_PalletList.Rows.Add(palletBarcode);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{e.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ProcessShipBack()
        {
            try
            {
                DbAccess.Default.ExecuteQuery($@"EXEC dbo.Sp_ShipBack @ShippingHist = {Convert.ToInt32(textBox_ShippingHist.Text)}, @ShipBackUser = '{WiseApp.Id}';");
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

        private void ShippingSelectMaterial_Load(object sender, EventArgs e)
        {
            dataGridView_PalletList.Columns.Add("Barcode", "Barcode");
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button_ShipBack_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)", "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)) return;
            if (ProcessShipBack())
            {
                Close();
            }
        }
    }
}