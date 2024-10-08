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
    public partial class SPC003 : Form
    {
        CustomPanelLinkEventArgs ee = null;
        DataTable dt = null;
        bool isload = false;
        DateTime date;

        public SPC003(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            ee = e;

            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
        }

        private void set()
        {
            string script = "select distinct Model from SpcItems where InputType = 'Manual'";

            DataTable dt = ee.DbAccess.GetDataTable(script);

            cbomaterial.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbomaterial.Items.Add(dt.Rows[i]["Model"]);
            }
        }

        private void IDM_EditForm_Load(object sender, EventArgs e)
        {
            set();
            isload = true;
        }

        private void cbomaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "select distinct InspType from SpcItems where InputType = 'Manual' and Model = '" + cbomaterial.Text + "' ";
            dt = ee.DbAccess.GetDataTable(script);

            cboInspType.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboInspType.Items.Add(dt.Rows[i]["InspType"]);
            }
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
                if (e.ColumnIndex == 7)
                {
                    if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                        return;
                                    //Max값만 있을때
                    else if (string.IsNullOrEmpty(dt.Rows[0]["MinValue"].ToString()) && !string.IsNullOrEmpty(dt.Rows[0]["MaxValue"].ToString()))
                    {
                        if (Convert.ToDecimal(dt.Rows[0]["MaxValue"]) < Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {
                            System.Windows.Forms.MessageBox.Show("입력범위를 벗어 났습니다.");
                            return;
                        }
                    }
                                    //Min값만 있을때
                    else if (!string.IsNullOrEmpty(dt.Rows[0]["MinValue"].ToString()) && string.IsNullOrEmpty(dt.Rows[0]["MaxValue"].ToString()))
                    {
                        if (Convert.ToDecimal(dt.Rows[0]["MinValue"]) > Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
                        {
                            System.Windows.Forms.MessageBox.Show("입력범위를 벗어 났습니다.");
                            return;
                        }
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                    {
                        return;
                    }
                }
                

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

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(dataGridView1.Rows[i].Cells["Value"].Value.ToString()))
                {
                    System.Windows.Forms.MessageBox.Show("입력되지 않은 값이 존재합니다");
                    return;
                }
            }

            script = string.Empty;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.GreenYellow)
                {

                    script = "update [dbo].SpcHist set "
                            
                            + "Value      = '" + dataGridView1.Rows[i].Cells["Value"].Value.ToString() + "'"
                            + ",Updated = getdate()"
                            + ",Updater      = '" + WiseApp.Id+ "'"
                            + " where SpcDate = '" + date.ToString("yyyy-MM-dd") + "'"
                            + " and SpcItem = N'" + dataGridView1.Rows[i].Cells["SpcItem"].Value.ToString() + "'"
                            + " and Model = '" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "'"
                            + " and InspType = '" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'"
                            + " and ItemType = '" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "'"
                            + " and Seq = '" + dataGridView1.Rows[i].Cells["Seq"].Value.ToString() + "'";



                }

                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    script = "delete from [dbo].SpcHist where SpcDate = '" + date.ToString("yyyy-MM-dd") + "'"
                            + " and SpcItem = N'" + dataGridView1.Rows[i].Cells["SpcItem"].Value.ToString() + "'"
                            + " and Model = '" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "'"
                            + " and InspType = '" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'"
                            + " and ItemType = '" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "'"
                            + " and Seq = '" + dataGridView1.Rows[i].Cells["Seq"].Value.ToString() + "'";
                }

                    								
                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    
                        script = "insert into [dbo].SpcHist";
                        script += "(SpcDate";
                        script += ",SpcItem";
                        script += ",ItemType";
                        script += ",MaterialBunch";
                        script += ",Model";
                        script += ",InspType";
                        script += ",Seq";
                        script += ",Value";
                        script += ",Updated";
                        script += ",Updater)";
                        script += " values";
                        //script += " ('" + Convert.ToDateTime(dataGridView1.Rows[i].Cells["SpcDate"].Value).ToShortDateString() + "'";
                        script += " ( convert(date, '" + date.ToString("yyyy-MM-dd") + "') ";
                        script += " ,N'" + dataGridView1.Rows[i].Cells["SpcItem"].Value.ToString() + "'";
                        script += " ,'" + dataGridView1.Rows[i].Cells["ItemType"].Value.ToString() + "'";
                        script += " ,''";
                        script += " ,'" + dataGridView1.Rows[i].Cells["Model"].Value.ToString() + "'";
                        script += " ,'" + dataGridView1.Rows[i].Cells["InspType"].Value.ToString() + "'";
                        script += " ,'" + dataGridView1.Rows[i].Cells["Seq"].Value.ToString() + "'";
                        script += " ,'" + dataGridView1.Rows[i].Cells["Value"].Value.ToString() + "'";
                        script += " , getdate()";
                        script += " ,'" + WiseApp.Id + "')";
                    
                    //    script += ",'" + dataGridView1.Rows[i].Cells["X1"].Value.ToString() + "'";
                    //}
                    //if (dataGridView1.Rows[i].Cells["X2"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["X2"].Value.ToString() + "'";
                    //}

                    //if (dataGridView1.Rows[i].Cells["X3"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["X3"].Value.ToString() + "'";
                    //}
                    //if (dataGridView1.Rows[i].Cells["X4"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["X4"].Value.ToString() + "'";
                    //}
                    //if (dataGridView1.Rows[i].Cells["X5"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["X5"].Value.ToString() + "'";
                    //}
                    //if (dataGridView1.Rows[i].Cells["Xbar"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["Xbar"].Value.ToString() + "'";
                    //}
                    //if (dataGridView1.Rows[i].Cells["R"].Value.ToString() == "")
                    //{
                    //    script += ", null";
                    //}
                    //else
                    //{

                    //    script += ",'" + dataGridView1.Rows[i].Cells["R"].Value.ToString() + "'";
                    //}
                    //}
                    //+ ",'" + dataGridView1.Rows[i].Cells["X2"].Value.ToString() + "'"
                    //+ ",'" + dataGridView1.Rows[i].Cells["X3"].Value.ToString() + "'"
                    //+ ",'" + dataGridView1.Rows[i].Cells["X4"].Value.ToString() + "'"
                    //+ ",'" + dataGridView1.Rows[i].Cells["X5"].Value.ToString() + "'"
                    //+ ",'" + dataGridView1.Rows[i].Cells["Xbar"].Value.ToString() + "'"
                    //+ ",'" + dataGridView1.Rows[i].Cells["R"].Value.ToString() + "'"
                    //script += ", getdate())";

                    //string su1 = null;


                    //if (dataGridView1.Rows[i].Cells["X1"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["X1"].Value = DBNull.Value.Equals(su1);
                    //}
                    //if (dataGridView1.Rows[i].Cells["X2"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["X2"].Value = DBNull.Value.Equals(su1);
                    //}

                    //if (dataGridView1.Rows[i].Cells["X3"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["X3"].Value = DBNull.Value.Equals(su1);
                    //}
                    //if (dataGridView1.Rows[i].Cells["X4"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["X4"].Value = DBNull.Value.Equals(su1);
                    //}
                    //if (dataGridView1.Rows[i].Cells["X5"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["X5"].Value = DBNull.Value.Equals(su1);
                    //}

                    //if (dataGridView1.Rows[i].Cells["Xbar"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["Xbar"].Value = DBNull.Value.Equals(su1);
                    //}

                    //if (dataGridView1.Rows[i].Cells["R"].Value.ToString() == "")
                    //{
                    //    dataGridView1.Rows[i].Cells["R"].Value = DBNull.Value.Equals(su1);
                    //}
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
                        FailList += "SpcDate : " + date.ToString("yyyy-MM-dd") + "\r";
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

        private void cbospcitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isload)
            {
                string script = "select InputCnt, MinValue, MaxValue from SpcItems  where Model = '" + cbomaterial.Text + "' and InspType = '" + cboInspType.Text + "' and ItemType = '" + cboItemType.Text + "' and InputType = 'Manual' AND SpcItem = N'" + cbospcitem.Text + "' ";

                dt = ee.DbAccess.GetDataTable(script);

                int Cnt = Convert.ToInt32(dt.Rows[0]["InputCnt"]);

                date = dateTimePicker1.Value;

                script = "select Spcdate, Model,InspType, ItemType, SpcItem,  MaterialBunch,  Seq, Value, Line from SpcHist where Spcdate = '" + date.ToString("yyyy-MM-dd") + "' AND SpcItem = N'" + cbospcitem.Text + "' AND Model = '" + cbomaterial.Text + "' and InspType = '" + cboInspType.Text + "' and ItemType = '" + cboItemType.Text + "' ";

                DataTable dgvdt = ee.DbAccess.GetDataTable(script);

                dataGridView1.DataSource = dgvdt;

                int dgvCnt = dgvdt.Rows.Count;
                for (int i = 0; i < Cnt - dgvCnt; i++)
                {
                    DataRow dr = dgvdt.NewRow();
                    dgvdt.Rows.Add(dr);
                    dataGridView1.Rows[i].Cells["spcdate"].Value = date.ToString("yyyy-MM-dd");
                    dataGridView1.Rows[i].Cells["InspType"].Value = cboInspType.Text;
                    dataGridView1.Rows[i].Cells["ItemType"].Value = cboItemType.Text;
                    dataGridView1.Rows[i].Cells["Model"].Value = cbomaterial.Text;
                    dataGridView1.Rows[i].Cells["SpcItem"].Value = cbospcitem.Text;
                    dataGridView1.Rows[i].Cells["Seq"].Value = i + 1;

                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                    dataGridView1.Rows[i].Cells["spcdate"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["SpcItem"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["InspType"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["ItemType"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["MaterialBunch"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["Model"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["Seq"].ReadOnly = true;

                    dataGridView1.Rows[i].Cells["spcdate"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["SpcItem"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["InspType"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["ItemType"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["MaterialBunch"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["Model"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["Seq"].Style.BackColor = Color.LightGray;

                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    dataGridView1.Rows[i].Cells["spcdate"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["SpcItem"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["ItemType"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["InspType"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["MaterialBunch"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["Model"].ReadOnly = true;
                    dataGridView1.Rows[i].Cells["Seq"].ReadOnly = true;

                    dataGridView1.Rows[i].Cells["spcdate"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["SpcItem"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["ItemType"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["InspType"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["MaterialBunch"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["Model"].Style.BackColor = Color.LightGray;
                    dataGridView1.Rows[i].Cells["Seq"].Style.BackColor = Color.LightGray;
                }

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void cboItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "select SpcItem from SpcItems where Model = '" + cbomaterial.Text + "' and InspType = '" + cboInspType.Text + "' and ItemType = '" + cboItemType.Text + "' and InputType = 'Manual'";

            dt = ee.DbAccess.GetDataTable(script);

            cbospcitem.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cbospcitem.Items.Add(dt.Rows[i]["SpcItem"]);
            }
        }

        private void cboInspType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = "select distinct ItemType from SpcItems where Model = '" + cbomaterial.Text + "' and InspType = '" + cboInspType.Text + "' and InputType = 'Manual'";

            dt = ee.DbAccess.GetDataTable(script);

            cboItemType.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboItemType.Items.Add(dt.Rows[i]["ItemType"]);
            }
        }
    }
}
