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
    public partial class SPC002 : Form
    {
        CustomPanelLinkEventArgs ee = null;
        DataTable dt = null;
        bool isload = false;

        public SPC002(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            ee = e;
            DataTable temp = (DataTable)e.DataGridView.DataSource;
            dt = temp.Copy();
            dt.Rows.Clear();

            for (int i = 0; i < e.DataGridView.Rows.Count; i++)
            {
                dt.Rows.Add((e.DataGridView.Rows[i].DataBoundItem as DataRowView).Row.ItemArray);
            }
        }

        private void set()
        {
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["Model"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["spcitem"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["ItemType"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["InspType"].ReadOnly = true;

                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }

                dataGridView1.Rows[i].Cells["Model"].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells["spcitem"].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells["ItemType"].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells["InspType"].Style.BackColor = Color.LightGray;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            }

            dataGridView1.Columns["updated"].Visible = false;
            dataGridView1.Columns["updater"].Visible = false;
        }

        

        private void IDM_EditForm_Load(object sender, EventArgs e)
        {
            set();
            isload = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dataGridView1.Rows[dt.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[dt.Rows.Count - 1].Index;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
            {
                dt.Rows[dataGridView1.CurrentRow.Index].Delete();
            }
            else
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.CurrentRow.ReadOnly = true;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isload)
            {
                if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Yellow)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            int T_Cnt = 0;
            int S_Cnt = 0;
            string FailList = string.Empty;

            //DataTable itemcheckdt = null;
            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
            //    {
            //        script = "select * from SpcItems where SpcItem = '" + dataGridView1.Rows[i].Cells["SpcItem"].Value.ToString() + "'";

            //        //itemcheckdt = ee.DbAccess.GetDataTable(script);

            //        //if (itemcheckdt.Rows.Count == 0)
            //        //{
            //        //    WiseM.MessageBox.Show("'" + dataGridView1.Rows[i].Cells["SpcItem"].Value.ToString() + "' this Material is not exist", "Error", MessageBoxIcon.Information);
            //        //    return;
            //        //}
            //    }
            //}

            script = string.Empty;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.GreenYellow)
                {
                    script = "update [dbo].SpcItems set ";
                    script += " SpcitemName = N'" + dataGridView1.Rows[i].Cells["SpcitemName"].Value.ToString() + "'";
                    script += " ,InputType = '" + dataGridView1.Rows[i].Cells["InputType"].Value.ToString() + "'";
                    script += " ,InputCnt = '" + dataGridView1.Rows[i].Cells["InputCnt"].Value.ToString() + "'";
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["MinValue"].Value.ToString()))
                        script += " ,MinValue = Null";
                    else
                        script += " ,MinValue = '" + dataGridView1.Rows[i].Cells["MinValue"].Value.ToString() + "'";
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["MaxValue"].Value.ToString()))
                        script += " ,MaxValue = Null";
                    else
                        script += " ,MaxValue = '" + dataGridView1.Rows[i].Cells["MaxValue"].Value.ToString() + "'";
                    script += " ,Updated = getdate()";
                    script += " ,Updater = '" + WiseApp.Id + "'";
                    script += " where Model = N'" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "' and SpcItem = N'" + dataGridView1.Rows[i].Cells["spcitem"].Value.ToString() + "' and ItemType = N'" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "' and InspType = N'" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'"; 
                }
                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    script = "delete from [dbo].SpcItems where Model = N'" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "' and SpcItem = N'" + dataGridView1.Rows[i].Cells["spcitem"].Value.ToString() + "' and ItemType = N'" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "' and InspType = N'" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'";
                }

                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    script = "insert into [dbo].SpcItems";
                    script += "(Model, Spcitem";
                    script += ",SpcitemName";
                    script += ",ItemType";
                    script += ",InspType";
                    script += ",InputType";
                    script += ",InputCnt";
                    script += ",MinValue";
                    script += ",MaxValue";
                    script += ",Updated";
                    script += ",Updater)";
                    script += "values";
                    script += "(N'" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "', N'" + dataGridView1.Rows[i].Cells["Spcitem"].Value.ToString() + "'";
                    script += ",N'" + dataGridView1.Rows[i].Cells["SpcitemName"].Value.ToString() + "'";
                    script += ",'" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "'";
                    script += ",N'" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'";
                    script += ",'" + dataGridView1.Rows[i].Cells["InputType"].Value.ToString() + "'";
                    script += ",'" + dataGridView1.Rows[i].Cells["InputCnt"].Value.ToString() + "'";
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["MinValue"].Value.ToString()))
                        script += ",Null";
                    else
                        script += ",'" + dataGridView1.Rows[i].Cells["MinValue"].Value.ToString() + "'";
                    if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["MaxValue"].Value.ToString()))
                        script += ",Null";
                    else
                        script += ",'" + dataGridView1.Rows[i].Cells["MaxValue"].Value.ToString() + "'";
                    script += ", getdate(),'" + WiseApp.Id + "')";
                }

                if (!string.IsNullOrEmpty(script))
                {

                    T_Cnt = T_Cnt + 1;

                    int Result = 0;
                    try
                    {
                        Result = ee.DbAccess.ExecuteQuery(script);
                    }
                    catch (Exception ex)
                    {
                        WiseM.MessageBox.Show(ex.Message, "Error", MessageBoxIcon.Information);
                    }

                    if (Result == 1)
                    {
                        S_Cnt = S_Cnt + 1;
                    }
                    else
                    {
                        FailList += "Spcitem : " + dataGridView1.Rows[i].Cells["Spcitem"].Value.ToString() + "\r";
                    }
                    script = string.Empty;
                }
            }
                        string msg = "Try : " + T_Cnt.ToString() + ", Success : " + S_Cnt.ToString();
 
            if (!string.IsNullOrEmpty(FailList))
            {
                msg += "\rFailList \r" + FailList;
            }

            WiseM.MessageBox.Show(msg, "Information", MessageBoxIcon.Information);

            if (string.IsNullOrEmpty(FailList))
            {
                Close();
            }
        }

        private void IDM_EditForm_SizeChanged(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 46;
        }

    }
}
