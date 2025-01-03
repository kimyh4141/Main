﻿
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
    public partial class PI_frmMain20 : Form
    {
        private DataTable dtPI = new DataTable();

        private string strUser = "";

        public PI_frmMain20(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string PS_BUNCH = "RawMaterial";
                string PS_GUBUN = "RM_CREATE_NEW_PI";
                string PS_BEGINDATE = this.dtpBeginDate.Value.ToString("yyyy-MM-dd");
                string PS_ENDDATE = this.dtpEndDate.Value.ToString("yyyy-MM-dd");
                string PS_ClientId = this.strUser;

                string strCmd = $@"exec {PI_frmMain00.strDbName}[Sp_PhysicalInventoryProcedureV2]
                                @PS_BUNCH		= '{PS_BUNCH}'
                                ,@PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_BEGINDATE  = '{PS_BEGINDATE}'
                                ,@PS_ENDDATE    = '{PS_ENDDATE}'
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

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString() != "")
                {
                    MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Created successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.DialogResult = DialogResult.Yes;
                this.Close();
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

    }
}
