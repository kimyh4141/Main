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
        CustomPanelLinkEventArgs e;

        private readonly string _blockingType;

        public Blocking(string blockingType, CustomPanelLinkEventArgs e)
        {
            InitializeComponent();

            _blockingType = blockingType;
            this.e = e;
        }

        private void Blocking_Load(object sender, EventArgs e)
        {
            if (_blockingType.Equals("RM") || _blockingType.Equals("PACK"))
            {
                Text = groupBox1.Text = _blockingType + " Blocking";
            }
            else
            {
                MessageBox.Show("Blocking Type is wrong.", "Wrong Block", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Close();
            }

            if (this.e?.DataGridView == null
                || this.e.DataGridView.Columns.Count <= 0
                || this.e.DataGridView.Rows.Count <= 0) return;
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Check", HeaderText = "Check", Width = 50 });
            dataGridView1.Columns.Add(new DataGridViewCheckBoxColumn() { Name = "Block", HeaderText = "Block", Width = 50, ReadOnly = true });
                
            foreach (DataGridViewColumn col in this.e.DataGridView.Columns)
            {
                if (!dataGridView1.Columns.Contains(col.Name))
                    dataGridView1.Columns.Add(
                        new DataGridViewTextBoxColumn
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

                dataGridView1.Rows[rowIndex].Cells["Check"].Value = false;

                foreach(DataGridViewColumn col in this.e.DataGridView.Columns)
                {
                    dataGridView1.Rows[rowIndex].Cells[col.Name].Value = row.Cells[col.Name].Value;
                }
            }
        }

        private void btn_Block_Click(object sender, EventArgs e)
        {
            var checkedRows = dataGridView1.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["Check"].Value.Equals(true)).ToList();
            if(!checkedRows.Any())
            {
                MessageBox.Show("Please select items.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if(checkedRows.FirstOrDefault(r => r.Cells["Block"].Value.Equals(true)) != null)
            {
                MessageBox.Show("Please select only unBlocked items.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            StringBuilder query = new StringBuilder();
            StringBuilder query_BlockingHist = new StringBuilder();
   
            
                query.Append("\r\n");
                query.Append("\r\n UPDATE   Stock ");
                query.Append("\r\n SET      Block = '1' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (var row in checkedRows)
                {
                    query.Append("\r\n      OR PcbBcd = '" + row.Cells["PcbBcd"].Value + "' ");
                    query_BlockingHist.Append("\r\n  ,   ('" + row.Cells["PcbBcd"].Value + "', '" + row.Cells["Material"].Value + "', 1, '" + row.Cells["PalletBcd"].Value + "', '" + row.Cells["BoxBcd"].Value + "' ,'" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
            
            query.Append("\r\n");

            var PcbList = (from DataGridViewRow dataGridViewRow in dataGridView1.Rows
                           where dataGridViewRow.Cells["Check"].Value.Equals(true)
                           select dataGridViewRow.Cells["PcbBcd"].Value.ToString()).ToList();

            string temp = "";
            foreach (var item in PcbList)
            {
                temp += "'"+item+"',";
            }
            string temp_PcbBcd = temp.Substring(0, temp.Length - 1);
      
             query.Append($@"
                            INSERT
                              INTO StockBlockingHist
                              (
                                PcbBcd
                              , BoxBcd
                              , PalletBcd
                              , Material
                              , Block
                              , Updated
                              , Updater
                              )
                            SELECT PcbBcd
                                 , BoxBcd
                                 , PalletBcd
                                 , Material
                                 , 1
                                 , GETDATE()
                                 , '{ WiseApp.CurrentUser.Name }'
                              FROM Stock
                             WHERE PcbBcd IN ({ temp_PcbBcd})
                        ");
 
            try
            {
               
                this.e.DbAccess.ExecuteQuery(query.ToString());

                MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fail!\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Unblock_Click(object sender, EventArgs e)
        {
            var checkedRows = dataGridView1.Rows.OfType<DataGridViewRow>().Where(r => r.Cells["Check"].Value.Equals(true));

            var dataGridViewRows = checkedRows.ToList();
            if (!dataGridViewRows.Any())
            {
                MessageBox.Show("Please select items.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (dataGridViewRows.FirstOrDefault(r => r.Cells["Block"].Value.Equals(false)) != null)
            {
                MessageBox.Show("Please select only blocked items.", "Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            StringBuilder query = new StringBuilder();         
            StringBuilder query_BlockingHist = new StringBuilder();
    
            query.Append("\r\n");
                query.Append("\r\n UPDATE   Stock ");
                query.Append("\r\n SET      Block = '0' ");
                query.Append("\r\n WHERE    1=2 ");

                foreach (DataGridViewRow row in dataGridViewRows)
                {
                    query.Append("\r\n      OR PcbBcd = '" + row.Cells["PcbBcd"].Value + "' ");
                    query_BlockingHist.Append("\r\n  ,   ('" + row.Cells["PcbBcd"].Value + "', '" + row.Cells["Material"].Value + "', 0, '" + row.Cells["PalletBcd"].Value + "', '" + row.Cells["BoxBcd"].Value + "', '" + WiseApp.CurrentUser.Name + "', GETDATE())");
                }
        

            query.Append("\r\n");
         
            var PcbList = (from DataGridViewRow dataGridViewRow in dataGridView1.Rows
                           where dataGridViewRow.Cells["Check"].Value.Equals(true)
                           select dataGridViewRow.Cells["PcbBcd"].Value.ToString()).ToList();

            string temp = PcbList.Aggregate("", (current, item) => current + $"'{item}',");
            string temp_PcbBcd = temp.Substring(0, temp.Length - 1);

            query.Append($@"insert into 
                          StockBlockingHist
                          (PcbBcd, 
                          BoxBcd, 
                          PalletBcd, 
                          Material, 
                          Block,
                          Updated, 
                          Updater) 
                          select
                          PcbBcd, 
                          BoxBcd,
                          PalletBcd, 
                          Material,
                          0,
                          GETDATE(), 
                          '{ WiseApp.CurrentUser.Name }' 
                          from Stock 
                          where 
                          PcbBcd in(  { temp_PcbBcd }  )");

            try
            {             
                this.e.DbAccess.ExecuteQuery(query.ToString());

                MessageBox.Show("Complete", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fail!\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetCheckAllRows(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetCheckAllRows(false);
        }


        //gmryu 2023-09-18 
        private void SetCheckAllRows(bool isChecked)
        {

            int check = 0;
            int uncheck = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {            
                DataGridViewCheckBoxCell checkBlockCell = row.Cells["Block"] as DataGridViewCheckBoxCell;
                if (checkBlockCell == null) continue;
                if (Convert.ToBoolean(checkBlockCell.Value))
                {                     
                    check++;
                }
                else
                {                    
                    uncheck++;                     
                }
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {             
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Check"] as DataGridViewCheckBoxCell;
                if (checkBoxCell != null && check == 0 || uncheck == 0)
                {
                    checkBoxCell.Value = isChecked;
                }

            }
        }
    }
}
