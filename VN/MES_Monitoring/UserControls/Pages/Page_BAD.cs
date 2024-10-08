using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MES_Monitoring.UserControls.Pages
{
    public partial class Page_BAD : Page_Base
    {
        public Page_BAD()
        {
            InitializeComponent();
        }

        public void SetPage(DataSet dataSet)
        {
            try
            {
                dataGridView_AI.Rows.Clear();
                dataGridView_SMT.Rows.Clear();
                dataGridView_MI.Rows.Clear();
                foreach (var row in dataSet.Tables[2].Select("Routing IN ('Ai_Unload')"))
                    dataGridView_AI.Rows.Add(row["Line"], row["Spec"], row["ProdQty"], row["BadQty"], row["BadRate"]);

                foreach (var row in dataSet.Tables[2].Select("Routing IN ('St_Unload')"))
                    dataGridView_SMT.Rows.Add(row["Line"], row["Spec"], row["ProdQty"], row["BadQty"], row["BadRate"]);

                foreach (var row in dataSet.Tables[2].Select("Routing IN ('Pk_Boxing')"))
                    dataGridView_MI.Rows.Add(row["Line"], row["Spec"], row["ProdQty"], row["BadQty"], row["BadRate"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Layout(object sender, LayoutEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn col in dataGridView.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            var cellStyle = dataGridView.ColumnHeadersDefaultCellStyle;
            cellStyle.Font = Application.CurrentCulture.ToString() == "zh-CN"
                ? new Font("Microsoft YaHei", 16, FontStyle.Bold)
                : new Font("Tahoma", 16, FontStyle.Bold);

            dataGridView.Columns[0].Width = 60;
            dataGridView.Columns[2].Width = 110;
            dataGridView.Columns[3].Width = 110;
            dataGridView.Columns[4].Width = 160;
            dataGridView.Columns[1].Width = dataGridView.Width - (3 + dataGridView.Columns[0].Width +
                                                                  dataGridView.Columns[2].Width +
                                                                  dataGridView.Columns[3].Width +
                                                                  dataGridView.Columns[4].Width);

            dataGridView.Columns[0].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[1].DefaultCellStyle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            dataGridView.Columns[2].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[3].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[4].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);

            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }
    }
}