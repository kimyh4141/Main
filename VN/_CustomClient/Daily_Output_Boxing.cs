using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Client
{
    public partial class Daily_Output_Boxing : Form
    {
        string workcenter = WbtCustomService.ActiveValues.Workcenter;
        string date1;
        string date2;

        public Daily_Output_Boxing()
        {
            InitializeComponent();
        }

        private void Daily_Output_Boxing_Load(object sender, EventArgs e)
        {
            datetimepicker_date.Value = DateTime.Today;

            date1 = datetimepicker_date.Value.ToString("yyyy-MM-dd");
            date2 = datetimepicker_date.Value.AddDays(1).ToString("yyyy-MM-dd");

            string Query = $@"
SELECT BT.WorkOrder, M.Material, M.Spec, COUNT(BT.PcbBcd) Qty FROM BoxTemp BT
JOIN WorkOrder WO ON BT.WorkOrder = WO.WORKORDER
JOIN Material M ON M.Material = WO.Material
 WHERE BT.WorkCenter = '{workcenter}'
   AND BT.Created BETWEEN '{date1} 08:00:00' AND '{date2} 08:00:00'
 GROUP BY BT.WorkOrder, M.Material, M.Spec

UNION ALL

SELECT BH.WorkOrder, M.Material, M.Spec, COUNT(BH.PcbBcd) Qty FROM BoxingHist BH
JOIN WorkOrder WO ON BH.WorkOrder = WO.WORKORDER
JOIN Material M ON M.Material = WO.Material
 WHERE BH.WorkCenter = '{workcenter}'
   AND BH.Created BETWEEN '{date1} 08:00:00' AND '{date2} 08:00:00'
 GROUP BY BH.WorkOrder, M.Material, M.Spec
                             ";
            DataTable dt = DbAccess.Default.GetDataTable(Query);

            dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void datetimepicker_date_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            date1 = datetimepicker_date.Value.ToString("yyyy-MM-dd");
            date2 = datetimepicker_date.Value.AddDays(1).ToString("yyyy-MM-dd");

            string Query = $@"
SELECT BT.WorkOrder, M.Material, M.Spec, COUNT(BT.PcbBcd) Qty FROM BoxTemp BT
JOIN WorkOrder WO ON BT.WorkOrder = WO.WORKORDER
JOIN Material M ON M.Material = WO.Material
 WHERE BT.WorkCenter = '{workcenter}'
   AND BT.Created BETWEEN '{date1} 08:00:00' AND '{date2} 08:00:00'
 GROUP BY BT.WorkOrder, M.Material, M.Spec

UNION ALL

SELECT BH.WorkOrder, M.Material, M.Spec, COUNT(BH.PcbBcd) Qty FROM BoxingHist BH
JOIN WorkOrder WO ON BH.WorkOrder = WO.WORKORDER
JOIN Material M ON M.Material = WO.Material
 WHERE BH.WorkCenter = '{workcenter}'
   AND BH.Created BETWEEN '{date1} 08:00:00' AND '{date2} 08:00:00'
 GROUP BY BH.WorkOrder, M.Material, M.Spec
                             ";
            DataTable dt = DbAccess.Default.GetDataTable(Query);

            dataGridView1.DataSource = dt;

            this.dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }
}
