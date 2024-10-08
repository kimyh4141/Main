using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class RePackingNew : Form
    {
        private string _type;
        private string _location;
        private string _line;
        private DateTime _date;
        private string _material;
        private string _name;
        private string _spec;
        private string _itemCode;
        private string _itemName;

        public RePackingNew()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            var typeList = new BindingList<object> { new { Key = "", Value = "" }, new { Key = "Box", Value = "箱(Box)" }, new { Key = "Pallet", Value = "托盘(Pallet)" } };
            comboBox_Type.DataSource = typeList;
            comboBox_Type.ValueMember = "Key";
            comboBox_Type.DisplayMember = "Value";
            comboBox_Type.SelectedIndex = 0;


            var locationList = new BindingList<object> { new { Key = "", Value = "" }, new { Key = "Packing", Value = "包装(Packing)" }, new { Key = "Warehouse", Value = "仓库(Warehouse)" } };
            comboBox_Location.DataSource = locationList;
            comboBox_Location.ValueMember = "Key";
            comboBox_Location.DisplayMember = "Value";
            comboBox_Location.SelectedIndex = 0;

            var dataTable = DbAccess.Default.GetDataTable
                (
                 $@"
                SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
                ;

                SELECT SUBSTRING(Workcenter, 5, 1) AS Line
                     , Text
                  FROM WorkCenter
                 WHERE Routing = 'Pk_Boxing'
                   AND Status = 1
                 ORDER BY Line
                ;
                "
                );
            comboBox_Line.DataSource = dataTable;
            comboBox_Line.ValueMember = "Line";
            comboBox_Line.ValueMember = "Text";
            comboBox_Location.SelectedIndex = 0;
        }

        private void RePackingNew_Load(object sender, EventArgs e)
        {

        }

        private DataTable GetDataTable()
        {
            var query = new StringBuilder();
            switch (comboBox_Location.SelectedValue.ToString())
            {
                case "Packing":
                    query.AppendLine
                        (
                         $@"
SELECT M.Material
     , M.Spec
     , M_TOP.LG_ITEM_CD
     , M_TOP.LG_ITEM_NM
     , M.Text AS MaterialName
  FROM Material                 AS M
       LEFT OUTER JOIN Material AS M_TOP
                       ON M.TOP_ITEM_CD = M_TOP.Material
 WHERE M.ROUT_SEQ = 'R070'
   AND M.Material LIKE '%{textBox_SearchMaterial.Text}%'
   AND COALESCE(M_TOP.Spec, '') LIKE '%{textBox_SearchSpec.Text}%'
 ORDER BY
     M.Material
;
                        "
                        );
                    break;
                case "Warehouse":
                    query.AppendLine
                        (
                         $@"
SELECT M.Material
     , M.Spec
     , M.LG_ITEM_CD
     , M.LG_ITEM_NM
     , M.Text AS MaterialName
  FROM Material AS M
 WHERE M.Bunch = '10'
   AND M.Material LIKE '%{textBox_SearchMaterial.Text}%'
   AND M.Spec LIKE '%{textBox_SearchSpec.Text}%'
;
                        "
                        );
                    break;
            }

            try
            {
                return DbAccess.Default.GetDataTable(query.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show($"数据库错误。(Database error.)\r\n{e.Message}", "错误(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void OpenGridView()
        {
            var dataTable = GetDataTable();
            if (dataTable != null
                && 0 < dataTable.Rows.Count)
            {
                dataGridView_Material.DataSource = dataTable;
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            OpenGridView();
        }

        public object GetValue(string value)
        {
            switch (value)
            {
                case "Type":
                    return _type;
                case "Location":
                    return _location;
                case "Line":
                    return _line;
                case "Date":
                    return _date;
                case "DateCode":
                    return DbAccess.Default.ExecuteScalar($"EXEC Sp_Convert_Date '{_date:yyyyMMdd}'");
                case "Material":
                    return _material;
                case "ItemCode":
                    return _itemCode;
                case "Name":
                    return _name;
                case "ItemName":
                    return _itemName;
                case "Spec":
                    return _spec;
                default:
                    return null;
            }
        }

        private void button_Created_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox_Type.SelectedValue.ToString()))
            {
                MessageBox.Show("请输入供应商。(Please select the type.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(comboBox_Location.SelectedValue.ToString()))
            {
                MessageBox.Show("请输入供应商。(Please select the location.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(comboBox_Line.SelectedValue.ToString()))
            {
                MessageBox.Show("请输入产品。(Please select the Line.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(dateTimePicker_PackDate.Text))
            {
                MessageBox.Show("请输入品种。(Please enter the date.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(textBox_Material.Text))
            {
                MessageBox.Show("请输入产品。(Please select the Material.)", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _type = comboBox_Type.SelectedValue.ToString();
            _location = comboBox_Location.SelectedValue.ToString();
            _line = comboBox_Line.Text;
            _date = dateTimePicker_PackDate.Value;
            _material = textBox_Material.Text;
            _itemCode = textBox_ItemCode.Text;
            _name = textBox_Name.Text;
            _itemName = textBox_ItemName.Text;
            _spec = textBox_Spec.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void comboBox_Location_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_Location.SelectedValue.ToString())
            {
                case "Packing":
                    tableLayoutPanel_Right.Enabled = true;
                    OpenGridView();
                    break;
                case "Warehouse":
                    tableLayoutPanel_Right.Enabled = true;
                    OpenGridView();
                    break;
                default:
                    dataGridView_Material.DataSource = null;
                    textBox_SearchMaterial.Text = string.Empty;
                    textBox_SearchSpec.Text = string.Empty;
                    tableLayoutPanel_Right.Enabled = false;
                    break;
            }

            textBox_Material.Text = string.Empty;
            textBox_ItemCode.Text = string.Empty;
            textBox_Name.Text = string.Empty;
            textBox_ItemName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;

            textBox_SearchMaterial.Text = string.Empty;
            textBox_SearchSpec.Text = string.Empty;
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void dataGridView_Material_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView dataGridView))
            {
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            var dataGridViewRow = dataGridView.Rows[e.RowIndex];
            string itemCode = dataGridViewRow.Cells["LG_ITEM_CD"].Value.ToString();
            if (string.IsNullOrEmpty(itemCode))
            {
                MessageBox.Show("请输入产品参考信息。(Please enter the product reference information.) - LG_ITEM_CD", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string itemName = dataGridViewRow.Cells["LG_ITEM_NM"].Value.ToString();
            if (string.IsNullOrEmpty(itemName))
            {
                MessageBox.Show("请输入产品参考信息。(Please enter the product reference information.) - LG_ITEM_NM", "警告(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            textBox_Material.Text = dataGridViewRow.Cells["Material"].Value.ToString();
            textBox_Name.Text = dataGridViewRow.Cells["MaterialName"].Value.ToString();
            textBox_Spec.Text = dataGridViewRow.Cells["Spec"].Value.ToString();
            textBox_ItemCode.Text = itemCode;
            textBox_ItemName.Text = itemName;
        }
    }
}
