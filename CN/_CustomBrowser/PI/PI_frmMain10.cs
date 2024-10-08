
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using WiseM.Data;
using System.Data.OleDb;

namespace WiseM.Browser
{
    public partial class PI_frmMain10 : Form
    {
        private DataTable dtPI = new DataTable();

        private string strUser = "";

        public PI_frmMain10(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void PI_frmMain10_Load(object sender, EventArgs e)
        {
            this.btnBrowse.PerformClick();
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO {PI_frmMain00.strDbName}SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'PhysicalInventory', REPLACE(LEFT(ISNULL(N'{strMsg}',''),3000), '''', ''''''), '{this.strUser}', GETDATE())");
            }
            catch { }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes != MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                return;

            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_GET_RESVMASTER";
                string PS_RESVNO = "";
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_RESVNO		= '{PS_RESVNO}'
                                ,@PS_ClientId   = '{PS_ClientId}'
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
                {
                    //if (col.Name != "Chk") col.ReadOnly = true;
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                this.dtPI = ds1.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnCreateNewResv_Click(object sender, EventArgs e)
        {
            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_CHK_NEW_PI_CREATABLE";
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_ClientId   = '{PS_ClientId}'
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

                if (ds1.Tables[ds1.Tables.Count-1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PI_frmMain20 _form1 = new PI_frmMain20(this.strUser);
                if (DialogResult.Yes == _form1.ShowDialog())
                    this.btnBrowse.PerformClick();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void dgv01_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            //DataTable dt1 = this.dtPI.Clone();
            //dt1.Rows.Add(this.dtPI.Rows[e.RowIndex].ItemArray);

            if (this.dtPI.Rows[e.RowIndex]["PI_Status"].ToString() == "Deleted")
            {
                string strTmp = "삭제된 정보입니다.\n\n\n"
                    + "This was deleted.\n\n";
                MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string PS_RESVNO = this.dtPI.Rows[e.RowIndex]["ResvNo"].ToString();
            PI_frmMain30 _form1 = new PI_frmMain30(this.strUser, PS_RESVNO);
            _form1.ShowDialog();
        }

    }
}
