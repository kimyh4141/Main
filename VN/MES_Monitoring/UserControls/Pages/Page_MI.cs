using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MES_Monitoring.Classes;

namespace MES_Monitoring.UserControls.Pages
{
    public partial class Page_MI : Page_Base
    {
        public Page_MI()
        {
            InitializeComponent();
        }

        public void SetPage(DataSet dataSet)
        {
            try
            {
                var rows = dataSet.Tables[0].Select("Routing = 'Pk_Boxing'");
                dataGridView_Left.Rows.Clear();
                dataGridView_Right.Rows.Clear();
                foreach (var row in rows)
                {
                    if (dataGridView_Left.Rows.Count == 8 && dataGridView_Right.Rows.Count == 8)
                        break;
                    if (dataGridView_Left.Rows.Count < 8)
                        dataGridView_Left.Rows.Add(row["Line"], row["Spec"], row["PlannedQty"], row["ProdQty"], row["ProdRate"], row["BadRate"]);
                    else
                        dataGridView_Right.Rows.Add(row["Line"], row["Spec"], row["PlannedQty"], row["ProdQty"], row["ProdRate"], row["BadRate"]);
                }

                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    var routing = row["Routing"].ToString();
                    var line = Convert.ToInt32(row["Line"]);
                    int intCondition;
                    Common.Routing.Type type;
                    try
                    {
                        // 0:가동중, 1:계획정지, 2:일시정지, 3:고장  "04" "05" "14" "15"
                        intCondition = Convert.ToInt16(row["Condition"].ToString().Substring(0, 1)) + 1;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }

                    switch (routing)
                    {
                        case "Mi_Load":
                            type = Common.Routing.Type.MI_Load;
                            break;
                        case "Mi_1stFunc":
                            type = Common.Routing.Type.Mi_1stFunc;
                            break;
                        case "Mi_Voltage":
                            type = Common.Routing.Type.Mi_Voltage;
                            break;
                        case "Mi_2ndFunc":
                            type = Common.Routing.Type.Mi_2ndFunc;
                            break;
                        case "Pk_Boxing":
                            type = Common.Routing.Type.Pk_Boxing;
                            break;
                        default:
                            type = Common.Routing.Type.None;
                            break;
                    }
                    _setLayoutMi.SetLineCondition(type, line, (Common.Routing.Condition)intCondition);
                }
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
            //dgv.Columns[1].Width = 500;
            dataGridView.Columns[2].Width = 110;
            dataGridView.Columns[3].Width = 110;
            dataGridView.Columns[4].Width = 160;
            dataGridView.Columns[5].Width = 160;
            dataGridView.Columns[1].Width = dataGridView.Width - (3 + dataGridView.Columns[0].Width +
                                                                  dataGridView.Columns[2].Width +
                                                                  dataGridView.Columns[3].Width +
                                                                  dataGridView.Columns[4].Width +
                                                                  dataGridView.Columns[5].Width);

            dataGridView.Columns[0].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[1].DefaultCellStyle.Font = new Font("Tahoma", 14, FontStyle.Bold);
            dataGridView.Columns[2].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[3].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[4].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);
            dataGridView.Columns[5].DefaultCellStyle.Font = new Font("Tahoma", 20, FontStyle.Bold);

            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
        }
    }
}