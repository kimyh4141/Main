﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class Blocking : SkinForm
    {
        CustomPanelLinkEventArgs e = null;

        string BlockingType = string.Empty;

        public Blocking(string blockingType, CustomPanelLinkEventArgs e)
        {
            InitializeComponent();

            this.BlockingType = blockingType;
            this.e = e;
        }

        private void Blocking_Load(object sender, EventArgs e)
        {
            if (this.BlockingType.Equals("RM") == true
                || this.BlockingType.Equals("PACK") == true)
            {
                this.Text = this.groupBox1.Text = this.BlockingType + " Blocking";
            }
            else
            {
                MessageBox.Show("Blocking Type is wrong.", "Wrong Block", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }

            if(this.e != null
                && this.e.DataGridView != null
                && this.e.DataGridView.Columns.Count > 0
                && this.e.DataGridView.Rows.Count > 0)
            {
                this.dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Check", HeaderText = "Check", Width = 50 });
                this.dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Block", HeaderText = "Block", Width = 50, ReadOnly = true });
                
                foreach (DataGridViewColumn col in this.e.DataGridView.Columns)
                {
                    if (this.dataGridView1.Columns.Contains(col.Name) == false)
                        this.dataGridView1.Columns.Add(
                            new DataGridViewTextBoxColumn()
                            {
                                Name = col.Name,
                                HeaderText = col.HeaderText,
                                ReadOnly = true,
                                Width = col.Width
                            });
                } 

                foreach(DataGridViewRow row in this.e.DataGridView.Rows)
                {
                    int rowIndex = this.dataGridView1.Rows.Add();

                    this.dataGridView1.Rows[rowIndex].Cells["Check"].Value = false;

                    foreach(DataGridViewColumn col in this.e.DataGridView.Columns)
                    {
                        this.dataGridView1.Rows[rowIndex].Cells[col.Name].Value = row.Cells[col.Name].Value;
                    }
                }
            }
        }

        private void btn_Block_Click(object sender, EventArgs e)
        {
            var checkedRows = this.dataGridView1.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["Check"].Value.Equals(true));

            if(checkedRows == null || checkedRows.Count() <= 0)
            {
                MessageBox.Show("Please select items.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if(checkedRows.Where(r => r.Cells["Block"].Value.Equals(true)).FirstOrDefault() != null)
            {
                MessageBox.Show("Please select only unBlocked items.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            StringBuilder query = new StringBuilder();
            StringBuilder query_BlockingHist = new StringBuilder();

            if(this.BlockingType.Equals("RM"))
            {
                query.Append("\r\n");
                query.Append("\r\n UPDATE   Rm_Stock ");
                query.Append("\r\n SET      Block = '1' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (DataGridViewRow row in checkedRows)
                {
                    query.Append("\r\n      OR Rm_BarCode = '" + row.Cells["Rm_BarCode"].Value + "' ");
                    query_BlockingHist.Append("\r\n ,   ('" + row.Cells["Rm_BarCode"].Value + "', '" + row.Cells["Rm_Material"].Value + "', 1, 'RM', '" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
            }
            else if (this.BlockingType.Equals("PACK"))
            {
                query.Append("\r\n");
                query.Append("\r\n UPDATE   Stock ");
                query.Append("\r\n SET      Block = '1' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (DataGridViewRow row in checkedRows)
                {
                    query.Append("\r\n      OR SerialNo = '" + row.Cells["SerialNo"].Value + "' ");
                    query_BlockingHist.Append("\r\n ,   ('" + row.Cells["SerialNo"].Value + "', '" + row.Cells["Material"].Value + "', 1, 'PACK', '" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
            }

            query.Append("\r\n");
            query.Append("\r\n INSERT INTO StockBlockingHist (BarcodeNo, Material, Block, Bunch, Updater, Updated) ");
            query.Append("\r\n VALUES   ");
            query.Append(query_BlockingHist.ToString().Substring(6));

            try
            {
                this.e.DbAccess.ExecuteQuery(query.ToString());

                MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Fail", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Unblock_Click(object sender, EventArgs e)
        {
            var checkedRows = this.dataGridView1.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["Check"].Value.Equals(true));

            if (checkedRows == null || checkedRows.Count() <= 0)
            {
                MessageBox.Show("Please select items.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (checkedRows.Where(r => r.Cells["Block"].Value.Equals(false)).FirstOrDefault() != null)
            {
                MessageBox.Show("Please select only blocked items.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            StringBuilder query = new StringBuilder();
            StringBuilder query_BlockingHist = new StringBuilder();

            if (this.BlockingType.Equals("RM"))
            {
                query.Append("\r\n");
                query.Append("\r\n UPDATE   Rm_Stock ");
                query.Append("\r\n SET      Block = '0' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (DataGridViewRow row in checkedRows)
                {
                    query.Append("\r\n      OR Rm_BarCode = '" + row.Cells["Rm_BarCode"].Value + "' ");
                    query_BlockingHist.Append("\r\n ,   ('" + row.Cells["Rm_BarCode"].Value + "', '" + row.Cells["Rm_Material"].Value + "', 0, 'RM', '" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
            }
            else if (this.BlockingType.Equals("PACK"))
            {
                query.Append("\r\n");
                query.Append("\r\n UPDATE   Stock ");
                query.Append("\r\n SET      Block = '0' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (DataGridViewRow row in checkedRows)
                {
                    query.Append("\r\n      OR SerialNo = '" + row.Cells["SerialNo"].Value + "' ");
                    query_BlockingHist.Append("\r\n ,   ('" + row.Cells["SerialNo"].Value + "', '" + row.Cells["Material"].Value + "', 0, 'PACK', '" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
            }

            query.Append("\r\n");
            query.Append("\r\n INSERT INTO StockBlockingHist (BarcodeNo, Material, Block, Bunch, Updater, Updated) ");
            query.Append("\r\n VALUES   ");
            query.Append(query_BlockingHist.ToString().Substring(6));

            try
            {
                this.e.DbAccess.ExecuteQuery(query.ToString());

                MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Fail", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
