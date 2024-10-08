
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
    public partial class HS_Receipt_frmSub10_SelectWO : Form
    {
        private DataTable dtWO = new DataTable();
        public DataTable dtSelectedWO = new DataTable();

        private string strUser = "";
        public HS_Receipt_frmSub10_SelectWO(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string strFromDate = this.dtpFrom.Value.ToString("yyyy-MM-dd");
                string strToDate = this.dtpTo.Value.ToString("yyyy-MM-dd");
                string strRouting = "Mi_Assy1";
                string strSpec = this.txtSpec.Text.Trim();

                string strCmd = $@"exec [Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= 'HS_GET_WORKORDER'
                                ,@PS_FROMDATE   = '{strFromDate}'
                                ,@PS_TODATE   = '{strToDate}'
                                ,@PS_Routing   = '{strRouting}'
                                ,@PS_Spec   = '{strSpec}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    if (intRC == -999)
                    {
                        MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    }
                }

                this.dgv01.DataSource = ds1.Tables[0];

                foreach (DataGridViewColumn col in this.dgv01.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                this.dtWO = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.dgv01.Rows.Count < 1) return;

            this.dgv01_CellDoubleClick(this.dgv01, new DataGridViewCellEventArgs(0, this.dgv01.SelectedRows[0].Index));
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void dgv01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex < 0) return;

            this.dtSelectedWO = this.dtWO.Clone();
            this.dtSelectedWO.Rows.Add(this.dtWO.Rows[e.RowIndex].ItemArray);

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'HS_Receipt', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch { }
        }
    }
}
