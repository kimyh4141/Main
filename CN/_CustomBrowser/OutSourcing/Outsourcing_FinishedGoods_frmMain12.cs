
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class Outsourcing_FinishedGoods_frmMain12 : Form
    {
        private DataTable dtMain10 = new DataTable();
        public Outsourcing_FinishedGoods_frmMain12(DataTable dtMain10)
        {
            InitializeComponent();

            this.dtMain10 = dtMain10;
        }

        private void Outsourcing_FinishedGoods_frmMain12_Load(object sender, EventArgs e)
        {
            this.dgv01.DataSource = this.dtMain10;

            foreach (DataGridViewColumn col in this.dgv01.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
