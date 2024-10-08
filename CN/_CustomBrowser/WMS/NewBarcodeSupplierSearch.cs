using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.WMS
{
    public partial class NewBarcodeSupplierSearch : Form
    {
        private string _material;
        private string _supplierCode;
        private string _supplierName;

        public NewBarcodeSupplierSearch()
        {
            InitializeComponent();
        }

        public NewBarcodeSupplierSearch(string material, string supplierCode, string supplierName)
        {
            InitializeComponent();
            _material = material;
            _supplierCode = supplierCode;
            _supplierName = supplierName;
            OpenGridViewList();
        }

        public void OpenGridViewList()
        {
            var query = new StringBuilder();
            query.AppendLine($@"
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
;

  WITH RMBS AS (
               SELECT RMBS.Supplier
                    , S.Text AS SupplierName
                    , 1      AS Type
                 FROM RawMaterialBySupplier  RMBS
                      LEFT OUTER JOIN Supply S
                                      ON RMBS.Supplier = S.Supply
                WHERE RawMaterial = '{_material}'
               )
SELECT ROW_NUMBER() OVER (ORDER BY Type DESC, Supplier) AS Seq
     , Type
     , Supplier
     , SupplierName
  FROM (
       SELECT Supplier
            , SupplierName
            , Type
         FROM RMBS
        UNION ALL
       SELECT S.Supply
            , S.Text AS SupplierName
            , 0      AS Type
         FROM Supply S
        WHERE NOT EXISTS(SELECT 'X'
                           FROM RMBS
                          WHERE S.Supply = RMBS.Supplier)
       ) T
 WHERE T.Supplier LIKE '%' + '{textBox_Supplier.Text}' + '%'
   AND T.SupplierName LIKE '%' + '{textBox__SupplierName.Text}' + '%'
 ORDER BY
     Seq
;
"
            );
            try
            {
                dataGridView_List.DataSource = DbAccess.Default.GetDataTable(query.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库错误。(Database error.)\r\n{e.Message}", "错误(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string GetResult(string name)
        {
            switch (name)
            {
                case "Code":
                    return _supplierCode;
                case "Name":
                    return _supplierName;
                default:
                    throw new ArgumentOutOfRangeException(nameof(name), typeof(string), null);
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            OpenGridViewList();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_Okay_Click(object sender, EventArgs e)
        {
            if (dataGridView_List.CurrentRow != null)
            {
                DialogResult = DialogResult.OK;
                _supplierCode = dataGridView_List.CurrentRow.Cells["Supplier"].Value as string;
                _supplierName = dataGridView_List.CurrentRow.Cells["SupplierName"].Value as string;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }

            Close();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView_List_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_List.CurrentRow != null)
            {
                DialogResult = DialogResult.OK;
                _supplierCode = dataGridView_List.CurrentRow.Cells["Supplier"].Value as string;
                _supplierName = dataGridView_List.CurrentRow.Cells["SupplierName"].Value as string;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }

            Close();
        }

        private void textBox_Supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            OpenGridViewList();
        }

        private void textBox__SupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            OpenGridViewList();
        }
    }
}