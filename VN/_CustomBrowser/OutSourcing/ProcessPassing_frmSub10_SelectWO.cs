
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
    public partial class ProcessPassing_frmSub10_SelectWO : Form
    {
        private DataTable dtWO = new DataTable();
        public DataTable dtSelectedWO = new DataTable();

        private string strUser = "";
        private string strRouting = "";
        private string strMaterial = "";
        private string strWorkOrder = "";
        private string strWorkCenter = "";
        public ProcessPassing_frmSub10_SelectWO(string strUser, string strRouting, string strMaterial, string strWorkOrder, string strWorkCenter)
        {
            InitializeComponent();

            this.strUser = strUser;
            this.strRouting = strRouting;
            this.strMaterial = strMaterial;
            this.strWorkOrder = strWorkOrder;
            this.strWorkCenter = strWorkCenter;
        }

        private void ProcessPassing_frmSub10_SelectWO_Load(object sender, EventArgs e)
        {
            try
            {
                string PS_GUBUN = "GET_ROUTING_AND_WORKCENTER_INFO";

                string strCmd = $@"exec [Sp_ProcessPassingProcedureV3]
                            @PS_GUBUN	    = '{PS_GUBUN}'
                            ,@PS_ROUTING    = '{this.strRouting}'
                            ,@PS_WORKCENTER = '{this.strWorkCenter}'
                        ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    string strMsg = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();
                    MessageBox.Show(strMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (intRC != -999) this.InsertIntoSysLog(strMsg);
                    return;
                }

                if (ds1.Tables.Count != 3)
                {
                    MessageBox.Show("Something wrong. Call IT!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.txtRouting.Text = ds1.Tables[0].Rows[0]["Routing"].ToString();
                this.cmbWorkCenter.DataSource = ds1.Tables[1];  //.Columns["WorkCenter"];
                this.cmbWorkCenter.ValueMember = "WorkCenter";
                this.cmbWorkCenter.DisplayMember = "WorkCenter";

                //if (this.strRouting == "St_Unload")
                //{
                //    this.dtpFrom.Enabled = false;
                //    this.dtpTo.Enabled = false;
                //    this.cmbWorkCenter.Enabled = false;

                //    this.btnSearch.PerformClick();

                //    this.btnSearch.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbWorkCenter.SelectedIndex < 0)
                {
                    MessageBox.Show("Bạn phải chọn 'Công đoạn' \n\nYou have to select 'WorkCenter'.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string strRouting = this.strRouting;
                string strWorkCenter = this.cmbWorkCenter.Text.Split(" - ".ToCharArray())[0];
                string strFromDate = this.dtpFrom.Value.ToString("yyyy-MM-dd");
                string strToDate = this.dtpTo.Value.ToString("yyyy-MM-dd");
                string strMaterial = this.strMaterial;
                string strWorkOrder = this.strWorkOrder;

                string strCmd = $@"exec [Sp_ProcessPassingProcedureV3]
                                @PS_GUBUN		    = 'GET_WORKORDER_BY_WORKCENTER'
                                ,@PS_ROUTING        = '{strRouting}'
                                ,@PS_WORKCENTER     = '{strWorkCenter}'
                                ,@PS_PLANNED_START  = '{strFromDate}'
                                ,@PS_PLANNED_END    = '{strToDate}'
                                ,@PS_MATERIAL       = '{strMaterial}'
                                ,@PS_WORKORDER      = '{strWorkOrder}'
                            ";
                System.Windows.Forms.MessageBox.Show(strCmd);
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
            if (e.RowIndex < 0  ||  e.ColumnIndex < 0) return;

            int intOrderQty = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["OrderQty"].Value);
            int intActualQty = Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["ActualQty"].Value);

            if (intActualQty >= intOrderQty)
            {
                string strMsg = "ERR20 - Hoàn tất số lượng yêu cầu。\n\nOrder quantity completed.";
                MessageBox.Show(strMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            /*
             gmryu 2023-08-31 청도법인과 동일하게 제외처리
                else if (intActualQty < 1)
            {
                string strMsg = "Chỉ thị thao tác này chưa từng được sản xuất。\n\nThis is a WorkOrder that has never been produced.";
                MessageBox.Show(strMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
             */


            this.dtSelectedWO.Rows.Clear();
            this.dtSelectedWO = this.dtWO.Clone();
            this.dtSelectedWO.Rows.Add();

            for (int i = 0; i < dgv.ColumnCount; i++)
                this.dtSelectedWO.Rows[0][i] = dgv.Rows[e.RowIndex].Cells[i].Value;

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void cmbWorkCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgv01.DataSource = null;
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'ProcessPassing', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch { }
        }
    }
}
