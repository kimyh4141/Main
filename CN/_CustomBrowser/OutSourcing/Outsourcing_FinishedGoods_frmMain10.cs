
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using WiseM.Data;

namespace WiseM.Browser
{
    public partial class Outsourcing_FinishedGoods_frmMain10 : Form
    {
        private DataTable dtMain10 = new DataTable();

        private string strUser;
        public Outsourcing_FinishedGoods_frmMain10(string strUser)
        {
            InitializeComponent();

            this.strUser = strUser;
        }

        private void Outsourcing_FinishedGoods_frmMain10_Load(object sender, EventArgs e)
        {
            this.dtpFromDate.Value = DateTime.Now.AddDays(-1);
            this.dtpToDate.Value = DateTime.Now;
        }

        private void InsertIntoSysLog(string strMsg)
        {
            try
            {
                strMsg = strMsg.Replace("'", "\x07");
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Browser', 'Outsourcing_Receipt', LEFT(ISNULL(N'{strMsg}',''),3000), '{this.strUser}', GETDATE())");
            }
            catch { }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.dgv01.DataSource = null;
                this.btnCheckAll.Text = "Check All";
                Application.DoEvents();

                string PS_GUBUN = "OUTSOURCING_GET_PROCESSED_DATA";
                string PS_FROMDATE = this.dtpFromDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpFromTime.Value.ToString("HH:mm:ss");
                string PS_TODATE = this.dtpToDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpToTime.Value.ToString("HH:mm:ss");

                string strCmd = $@"exec [Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_FROMDATE   = '{PS_FROMDATE}'
                                ,@PS_TODATE     = '{PS_TODATE}'
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

                this.dtMain10 = ds1.Tables[0];
                this.dgv01.DataSource = this.dtMain10;

                foreach (DataGridViewColumn col in this.dgv01.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;

                this.dgv01.Columns["StockHist"].Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void dgv01_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;

                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                string strColName = dgv.Columns[e.ColumnIndex].Name;
                if (strColName != "Chk") e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Button btn = (Button)sender;

                if (btn.Text.StartsWith("Check"))
                {
                    //this.dtMain10.Columns["Chk"].Expression = "1";
                    foreach (DataRow row in this.dtMain10.Rows)
                    {
                        row["Chk"] = true;
                    }
                    btn.Text = "Uncheck All";
                }
                else
                {
                    //this.dtMain10.Columns["Chk"].Expression = "0";
                    foreach (DataRow row in this.dtMain10.Rows)
                    {
                        row["Chk"] = false;
                    }
                    btn.Text = "Check All";
                }
                this.dtMain10.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtMain10 == null  ||  this.dtMain10.Rows.Count < 1)
                    return;

                if (DialogResult.Yes != MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    return;

                Cursor.Current = Cursors.WaitCursor;

                DataRow[] fRows = this.dtMain10.Select("Chk = 'true'", "StockHist");
                if (fRows.Length == 0)
                {
                    string strTmp = "삭제할 데이터가 없습니다.\n\n\n"
                                  + "There is no data to delete.\n\n";
                    MessageBox.Show(strTmp, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MemoryStream ms1 = new MemoryStream();
                StreamWriter sw1 = new StreamWriter(ms1);
                foreach (DataRow row in fRows)
                {
                    sw1.Write(row["StockHist"].ToString() + "\x07");
                    sw1.Flush();
                }

                string PS_PcbNo = Encoding.Default.GetString(ms1.ToArray());
                if (PS_PcbNo.Length > 0) PS_PcbNo = PS_PcbNo.Remove(PS_PcbNo.Length - 1, 1);


                string PS_GUBUN = "OUTSOURCING_DELETE_PROCESSED_DATA";

                string strCmd = $@"exec [Sp_OutSourcingProcedureV4]
                                @PS_GUBUN		= '{PS_GUBUN}'
                                ,@PS_PcbNo      = '{PS_PcbNo}'
                                ,@PS_ClientId  = '{this.strUser}'
                            ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd, CommandType.Text, null, 10800);
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
                    //MessageBox.Show(ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (ds1.Tables.Count >= 2)
                    {
                        Outsourcing_FinishedGoods_frmMain12 _form1 = new Outsourcing_FinishedGoods_frmMain12(ds1.Tables[ds1.Tables.Count - 2]);
                        _form1.ShowDialog();
                    }
                    return;
                }

                MessageBox.Show("Deleted successfully.", "", MessageBoxButtons.OK, MessageBoxIcon.None);

                this.btnBrowse.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.InsertIntoSysLog(ex.Message);
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
