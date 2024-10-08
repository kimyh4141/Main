using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser.Checksheet
{
    public partial class CsCheckSheetSpecDetail : SkinForm
    {
        bool IsNew = false;

        string CsCode = string.Empty;

        CustomPanelLinkEventArgs EventSource = null;

        public CsCheckSheetSpecDetail()
        {
            InitializeComponent();
        }

        public CsCheckSheetSpecDetail(bool isNew, CustomPanelLinkEventArgs e)
            : this()
        {
            this.IsNew = isNew;

            this.EventSource = e;

            if (this.IsNew == true)
            {
                string parameters = e.Script.Substring(e.Script.IndexOf("/Parameter"), e.Script.LastIndexOf("Parameter/") - e.Script.IndexOf("/Parameter")).Replace("/Parameter", "").Replace("Parameter/", "").Trim();

                string[] split = parameters.Replace("[", "").Replace("]", "").Split(':');

                this.CsCode = split[1].Replace("'", "");
            }
        }

        private void CsCheckSheetSpecDetail_Load(object sender, EventArgs e)
        {
            string query = string.Empty;

            query += "\r\n";
            query += "\r\n SELECT   * ";
            query += "\r\n FROM     Common ";
            query += "\r\n WHERE    Category = '701' ";
            query += "\r\n      AND Status = 1 ";
            query += "\r\n ORDER BY ViewSeq ";

            DataTable dtDataType = null;

            try
            {
                dtDataType = DbAccess.Default.GetDataTable(query);

                this.cb_DataType.DataSource = dtDataType;
                this.cb_DataType.DisplayMember = "Text";
                this.cb_DataType.ValueMember = "Common";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fail Found Data Type Information\r\n{ex.Message}", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            if (IsNew)
            {
                lbl_CsCode.Text = CsCode;

                query = string.Empty;

                query += "\r\n";
                query += "\r\n SELECT   ISNULL(MAX(Seq), 0) ";
                query += "\r\n FROM     CsSpecDetail ";
                query += "\r\n WHERE    CsCode = '" + this.CsCode + "' ";

                try
                {
                    int seq = Convert.ToInt32(DbAccess.Default.ExecuteScalar(query));

                    this.lbl_Seq.Text = (seq + 1).ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Fail Found Seq.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            else
            {
                if (this.EventSource == null
                    || this.EventSource.DataGridView == null
                    || this.EventSource.DataGridView.Rows.Count <= 0
                    || this.EventSource.DataGridView.CurrentCell == null
                    )
                {
                    MessageBox.Show("Please select item.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.Close();
                }

                DataGridViewRow row = this.EventSource.DataGridView.CurrentCell.OwningRow;

                this.lbl_CsCode.Text = Convert.ToString(row.Cells["CsCode"].Value);
                this.lbl_Seq.Text = Convert.ToString(row.Cells["Seq"].Value);
                this.tb_CheckGroup.Text = Convert.ToString(row.Cells["CheckGroup"].Value);
                this.tb_CheckItem.Text = Convert.ToString(row.Cells["CheckItems"].Value);
                this.tb_DataUnit.Text = Convert.ToString(row.Cells["DataUnit"].Value);
                this.tb_ValueMin.Text = Convert.ToString(row.Cells["ValueMin"].Value);
                this.tb_ValueMax.Text = Convert.ToString(row.Cells["ValueMax"].Value);
                this.tb_Comment.Text = Convert.ToString(row.Cells["Comment"].Value);
                this.cb_DataType.Text = Convert.ToString(dtDataType.Select(string.Format("Common = '{0}'", row.Cells["DataType"].Value))?.FirstOrDefault()?["Text"]);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lbl_CsCode.Text) == true)
            {
                MessageBox.Show("Not Valid 'CheckSheet No'.\r\nPlease open it again.", "Open Retry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(this.lbl_Seq.Text) == true)
            {
                MessageBox.Show("Not Valid 'Seq'.\r\nPlease open it again.", "Open Retry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (string.IsNullOrEmpty(this.tb_CheckGroup.Text) == true)
            {
                MessageBox.Show("Not Valid 'Group'.\r\nPlease input data.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tb_CheckGroup.Focus();
            }
            else if (string.IsNullOrEmpty(this.tb_CheckItem.Text) == true)
            {
                MessageBox.Show("Not Valid 'Item'.\r\nPlease input data.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tb_CheckItem.Focus();
            }
            else if (string.IsNullOrEmpty(this.cb_DataType.Text) == true)
            {
                MessageBox.Show("Not Valid 'Data Type'.\r\nPlease select type.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.cb_DataType.Focus();
            }
            else
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    string query = string.Empty;

                    string csCode = this.lbl_CsCode.Text;
                    string seq = this.lbl_Seq.Text;
                    string checkGroup = this.tb_CheckGroup.Text;
                    string checkItem = this.tb_CheckItem.Text;
                    object dataType = this.cb_DataType.SelectedValue;
                    string dataUnit = this.tb_DataUnit.Text;
                    string valueMin = this.tb_ValueMin.Text;
                    string valueMax = this.tb_ValueMax.Text;
                    string comment = this.tb_Comment.Text;

                    if (this.IsNew == true)
                    {
                        query += "\r\n";
                        query += "\r\n INSERT INTO CsSpecDetail (CsCode, Seq, CheckGroup, CheckItems, DataType, DataUnit, ValueMin, ValueMax, Comment, Updated, Updater) ";
                        query += "\r\n VALUES ( ";
                        query += "\r\n          '" + csCode + "' ";
                        query += "\r\n         ,'" + seq + "' ";
                        query += "\r\n         ,'" + checkGroup + "' ";
                        query += "\r\n         ,'" + checkItem + "' ";
                        query += "\r\n         ,'" + dataType + "' ";
                        query += "\r\n         ,'" + dataUnit + "' ";
                        query += "\r\n         ,'" + valueMin + "' ";
                        query += "\r\n         ,'" + valueMax + "' ";
                        query += "\r\n         ,'" + comment + "' ";
                        query += "\r\n         ,GETDATE() ";
                        query += "\r\n         ,'" + WiseApp.CurrentUser.Name + "' ";
                        query += "\r\n          ) ";
                    }
                    else
                    {
                        query += "\r\n";
                        query += "\r\n UPDATE   CsSpecDetail ";
                        query += "\r\n SET       CheckGroup = '" + checkGroup + "' ";
                        query += "\r\n          ,CheckItems = '" + checkItem + "' ";
                        query += "\r\n          ,DataType   = '" + dataType + "' ";
                        query += "\r\n          ,DataUnit   = '" + dataUnit + "' ";
                        query += "\r\n          ,ValueMin   = '" + valueMin + "' ";
                        query += "\r\n          ,ValueMax   = '" + valueMax + "' ";
                        query += "\r\n          ,Comment    = '" + comment + "' ";
                        query += "\r\n          ,Updated    = GETDATE() ";
                        query += "\r\n          ,Updater    = '" + WiseApp.CurrentUser.Name + "' ";
                        query += "\r\n WHERE    CsCode  = '" + csCode + "' ";
                        query += "\r\n      AND Seq     = '" + seq + "' ";
                    }

                    try
                    {
                        int executeRowCount = DbAccess.Default.ExecuteQuery(query);

                        if (executeRowCount == 1)
                        {
                            MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Fail", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool Delete(CustomPanelLinkEventArgs e)
        {
            bool bReturn = false;

            if (e == null
                || e.DataGridView == null
                || e.DataGridView.Rows.Count <= 0
                || e.DataGridView.CurrentCell == null
                || string.IsNullOrEmpty(Convert.ToString(e.DataGridView.CurrentCell.OwningRow.Cells["CsCode"].Value)) == true
                || string.IsNullOrEmpty(Convert.ToString(e.DataGridView.CurrentCell.OwningRow.Cells["Seq"].Value)) == true
                )
            {
                MessageBox.Show("Not Select", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                string csCode = string.Empty;
                string seq = string.Empty;

                csCode  = Convert.ToString(e.DataGridView.CurrentCell.OwningRow.Cells["CsCode"].Value);
                seq     = Convert.ToString(e.DataGridView.CurrentCell.OwningRow.Cells["Seq"].Value);

                if (MessageBox.Show("Are you sure?\r\nCsCode : " + csCode + "\r\nSeq : " + seq + "", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(csCode) == true)
                        {
                            MessageBox.Show("Not Valid 'CheckSheet No'", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else if (string.IsNullOrEmpty(seq) == true || int.TryParse(seq, out int iSeq) == false)
                        {
                            MessageBox.Show("Not Valid 'Seq'", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            string query = string.Empty;

                            query += "\r\n";
                            query += "\r\n DELETE FROM CsSpecDetail ";
                            query += "\r\n WHERE  CsCode = '" + csCode + "' ";
                            query += "\r\n    AND Seq    = '" + iSeq + "' ";

                            int executeRowCount = DbAccess.Default.ExecuteQuery(query);

                            if (executeRowCount == 1)
                            {
                                MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bReturn = true;
                            }
                            else
                            {
                                MessageBox.Show("Fail", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                bReturn = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        bReturn = false;
                    }
                }
            }

            return bReturn;
        }
    }
}
