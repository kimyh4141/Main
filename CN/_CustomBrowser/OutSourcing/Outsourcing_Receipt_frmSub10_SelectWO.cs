
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
    public partial class Outsourcing_Receipt_frmSub10_SelectWO : Form
    {
        private DataTable dtWO = new DataTable();
        public DataTable dtSelectedWO = new DataTable();

        private string strUser = "";
        private string strReceiptType = "";
        private string strWorkCenter = "";

        public Outsourcing_Receipt_frmSub10_SelectWO(string strUser, string strReceiptType, string strWorkCenter)
        {
            InitializeComponent();

            this.strUser = strUser;
            this.strReceiptType = strReceiptType;
            this.strWorkCenter = strWorkCenter;
        }

        private void Outsourcing_Receipt_frmSub10_SelectWO_Load(object sender, EventArgs e)
        {
            try
            {
                this.cmbRouting.Items.Add("Ai_Load");
                this.cmbRouting.Items.Add("Ai_Unload");
                this.cmbRouting.Items.Add("St_Load");
                this.cmbRouting.Items.Add("St_Unload");     // 2023-03-27 추가 jwoh
                this.cmbRouting.Items.Add("Mi_Load");
                if (this.strReceiptType == "Finished")
                    this.cmbRouting.Items.Add("Pk_Boxing");

                string strCmd = $@"exec {Outsourcing_Receipt_frmMain00.strDbName}[Sp_OutSourcingProcedureV4]
                            @PS_GUBUN		= 'OUTSOURCING_GET_WORKORDER'
                            ,@PS_FROMDATE   = ''
                            ,@PS_TODATE     = ''
                            ,@PS_WorkCenter = 'ZZZ'
                            ,@PS_Routing    = 'ZZZ'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);
                if (ds1 == null || ds1.Tables.Count == 0)
                    throw new Exception("Network problem occurred.");

                int intRC = Convert.ToInt16(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"]);
                if (intRC != 0)
                {
                    System.Windows.Forms.MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (intRC != -999) throw new Exception(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString());
                    this.Close();
                }

                this.dtSelectedWO.Columns.Add("Key_Routing");
                foreach (DataColumn col in ds1.Tables[0].Columns)
                    this.dtSelectedWO.Columns.Add(col.ColumnName);

                for (int i = 0; i < this.cmbRouting.Items.Count; i++)
                {
                    DataRow dRow = this.dtSelectedWO.NewRow();
                    dRow[0] = this.cmbRouting.Items[i];
                    this.dtSelectedWO.Rows.Add(dRow);
                }

                this.dgv02.DataSource = this.dtSelectedWO;

                foreach (DataGridViewColumn col in this.dgv02.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                if (this.cmbRouting.SelectedIndex < 0)
                {
                    MessageBox.Show("You have to select 'Routing'.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string strFromDate = this.dtpFrom.Value.ToString("yyyy-MM-dd");
                string strToDate = this.dtpTo.Value.ToString("yyyy-MM-dd");
                string strRouting = this.cmbRouting.Items[this.cmbRouting.SelectedIndex].ToString();

                string strCmd = $@"exec {Outsourcing_Receipt_frmMain00.strDbName}[Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= 'OUTSOURCING_GET_WORKORDER'
                                ,@PS_FROMDATE   = '{strFromDate}'
                                ,@PS_TODATE     = '{strToDate}'
                                ,@PS_WorkCenter = '{this.strWorkCenter}'
                                ,@PS_Routing    = '{strRouting}'
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

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (this.dtSelectedWO.Select("isnull(WorkOrder,'') <> ''").Length == 0)
            {
                string strTmp = "선택한 작업지시가 없습니다.\n이전화면으로 돌아가겠습니까?\n\n";
                strTmp += "There is no Selected WorkOrder.\nAre you sure want to return to previous screen?\n\n";
                if (DialogResult.Yes != MessageBox.Show(strTmp, "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                    return;
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
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

            for (int i = 0; i < dgv.ColumnCount; i++)
                dtSelectedWO.Rows[cmbRouting.SelectedIndex][i + 1] = dgv.Rows[e.RowIndex].Cells[i].Value;
        }

        private void cmbRouting_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv01.DataSource = null;
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO {Outsourcing_Receipt_frmMain00.strDbName}SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Outsourcing_Receipt', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch { }
        }
    }
}
