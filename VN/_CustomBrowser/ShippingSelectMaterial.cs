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
    public partial class ShippingSelectMaterial : Form
    {
        public string material;
        public string text;
        public string spec;
        
        public ShippingSelectMaterial()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string Q = $@"
                         SELECT Material, Text, Spec FROM Material
                          WHERE Material LIKE '%{tb_Material.Text}%'
                            AND LG_ITEM_NM LIKE '%{tb_Spec.Text}%'
                         ";
            DataTable dt = DbAccess.Default.GetDataTable(Q);

            dgv_MaterialInfo.DataSource = dt;

            foreach (DataGridViewColumn col in this.dgv_MaterialInfo.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgv_MaterialInfo.Columns["Material"].Width = 100;
            dgv_MaterialInfo.Columns["Text"].Width = 70;
            dgv_MaterialInfo.Columns["Spec"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgv_MaterialInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            material = dgv_MaterialInfo.CurrentRow.Cells["Material"].Value.ToString();
            text = dgv_MaterialInfo.CurrentRow.Cells["Text"].Value.ToString();
            spec = dgv_MaterialInfo.CurrentRow.Cells["Spec"].Value.ToString();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
