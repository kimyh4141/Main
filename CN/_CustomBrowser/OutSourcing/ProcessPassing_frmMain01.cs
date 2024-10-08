
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WiseM.AppService;
using WiseM.Data;
using System.Data.OleDb;

namespace WiseM.Browser
{
    public partial class ProcessPassing_frmMain01 : Form
    {
        private DataTable dtRouting = new DataTable();
        private DataTable dtWO = new DataTable();

        private string strUser = "";
        private string strMaterial = "";
        
        public ProcessPassing_frmMain01(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void ProcessPassing_frmMain01_Load(object sender, EventArgs e)
        {
            this.txtPCB.Text = "";
            this.txtSpec.Text = "";
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        private void txtPCB_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.txtSpec.Text = "";
                this.dtRouting.Rows.Clear();
                this.dtWO.Rows.Clear();            

                if (e.KeyChar != Convert.ToChar(Keys.Enter)) return;
                TextBox tBox = (TextBox)sender;

                string PS_GUBUN = "GET_ROUTING_PASSING_INFO";

                string strCmd = $@"exec [Sp_ProcessPassingProcedureV3]
                            @PS_GUBUN	= '{PS_GUBUN}'
                            ,@PS_PCB    = '{tBox.Text}'
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

                if (ds1.Tables[0].Rows.Count < 1)
                {
                    MessageBox.Show("无法找到PCB \n\nPCB not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.strMaterial = ds1.Tables[0].Rows[0]["Material"].ToString();
                this.txtSpec.Text = ds1.Tables[0].Rows[0]["Spec"].ToString();
                this.dtRouting = ds1.Tables[1];
                this.dgv01.DataSource = this.dtRouting;

                foreach (DataGridViewColumn col in this.dgv01.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void dgv01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                if (dgv.Rows[e.RowIndex].Cells["DateTime"].Value.ToString() != "")      // 실적이 발생한 공정
                    return;

                string strRouting = dgv.Rows[e.RowIndex].Cells["Routing"].Value.ToString();
                string strWorkOrder = dgv.Rows[e.RowIndex].Cells["WorkOrder"].Value.ToString();     // 필요없을듯(전부다 블랭크일듯).. 2023-03-23 jwoh
                string strWorkCenter = dgv.Rows[e.RowIndex].Cells["WorkCenter"].Value.ToString();

                // 앞공정부터 차례대로 처리해야 함. (Ai_Load인 경우에는 실적이 없다면 처리 가능함)
                if ((strRouting == "Ai_Unload"  && dgv.Rows[e.RowIndex - 1].Cells["DateTime"].Value.ToString() == "") ||
                    (strRouting == "St_Load"    && dgv.Rows[e.RowIndex - 1].Cells["DateTime"].Value.ToString() == "") ||
                    (strRouting == "St_Unload"  && dgv.Rows[e.RowIndex - 1].Cells["DateTime"].Value.ToString() == "") ||
                    (strRouting == "Mi_Load"    && dgv.Rows[e.RowIndex - 1].Cells["DateTime"].Value.ToString() == ""))
                    return;

                ProcessPassing_frmSub10_SelectWO _form1 = new ProcessPassing_frmSub10_SelectWO(this.strUser, strRouting, this.strMaterial, strWorkOrder, strWorkCenter);
                if (DialogResult.Yes != _form1.ShowDialog())
                    return;

                this.dtWO = _form1.dtSelectedWO;
                this.dgv02.DataSource = this.dtWO;

                foreach (DataGridViewColumn col in this.dgv02.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtWO.Rows.Count < 1)
                    return;

                int intOrderQty = Convert.ToInt32(this.dtWO.Rows[0]["OrderQty"]);
                int intActualQty = Convert.ToInt32(this.dtWO.Rows[0]["ActualQty"]);

                if (intActualQty >= intOrderQty)
                {
                    string strMsg = "ERR20 - 已超出订单数量。\n\nOrder quantity completed.";
                    MessageBox.Show(strMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (DialogResult.Yes != MessageBox.Show("Are you sure want to save ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    return;


                string PS_GUBUN = "SAVE_PROCESS_PASSING_INFO";
                string PS_WORKORDER = this.dtWO.Rows[0]["WorkOrder"].ToString();

                string strCmd = $@"exec [Sp_ProcessPassingProcedureV3]
                        @PS_GUBUN	    = '{PS_GUBUN}'
                        ,@PS_WORKORDER  = '{PS_WORKORDER}'
                        ,@PS_USER       = '{this.strUser}'
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

                string strActiveStatus = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();
                InsertUpdateKeyRelation();

                KeyPressEventArgs kpea = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
                txtPCB_KeyPress(this.txtPCB, kpea);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
            }
        }

        private void InsertUpdateKeyRelation()
        {
            try
            {
                string PS_GUBUN = "INSERT_UPDATE_KEYRELATION";
                string PS_PCB = this.txtPCB.Text;
                string PS_ROUTING = this.dtWO.Rows[0]["Routing"].ToString();
                string PS_WORKORDER = this.dtWO.Rows[0]["WorkOrder"].ToString();
                string PS_MATERIAL = this.dtWO.Rows[0]["Material"].ToString();
                string PS_WORKCENTER = this.dtWO.Rows[0]["Workcenter"].ToString();
                string PS_USER = this.strUser;

                string strCmd = $@"exec [Sp_ProcessPassingProcedureV3]
                            @PS_GUBUN	    = '{PS_GUBUN}'
                            ,@PS_PCB        = '{PS_PCB}'
                            ,@PS_ROUTING    = '{PS_ROUTING}'
                            ,@PS_WORKORDER  = '{PS_WORKORDER}'
                            ,@PS_MATERIAL   = '{PS_MATERIAL}'
                            ,@PS_WORKCENTER = '{PS_WORKCENTER}'
                            ,@PS_USER       = '{PS_USER}'
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }
    }
}
