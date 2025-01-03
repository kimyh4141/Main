﻿using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser.WMS
{
    public partial class RawMaterialBlocking : Form
    {
        public RawMaterialBlocking()
        {
            InitializeComponent();
        }

        private void RawMaterialBlocking_Load(object sender, EventArgs e)
        {
        }

        private void Search()
        {
            var query = new StringBuilder();
            query.AppendLine
            (
                $@"
                SELECT RS.Block
                     , RS.Rm_BarCode                              AS Barcode
                     , RS.Rm_Material                             AS Material
                     , RM.Spec
                     , RS.Rm_StockQty                             AS StockQty
                  FROM Rm_Stock                    AS RS
                       LEFT OUTER JOIN RawMaterial AS RM
                                       ON RS.Rm_Material = RM.RawMaterial
                 WHERE Rm_ProdDate BETWEEN '{dateTimePicker_From.Value:yyyy-MM-dd}' AND '{dateTimePicker_To.Value:yyyy-MM-dd}'
                   AND Rm_Material LIKE '%{textBox_Material.Text}%'
                   AND Spec LIKE '%{textBox_Spec.Text}%'
                   AND Rm_BarCode LIKE '%{textBox_Barcode.Text}%'
                 ORDER BY RS.Block
                ;
                "
            );
            try
            {
                wiseDataGridView_List.DataSource = DbAccess.Default.GetDataTable(query.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"数据库错误。(Database error.)\r\n{ex.Message}", "错误(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("你确定吗？(Are you sure?)", "问题(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            var dataTable = (DataTable)wiseDataGridView_List.DataSource;
            var checkList = (from DataRow dataRow in dataTable.Rows where dataRow.RowState == DataRowState.Modified select $@"{dataRow["Barcode"]}-{Convert.ToInt32(dataRow["Block"])}").ToList();

            string query = $@"
                            --'Barcode-Block'
                            DECLARE @PramList NVARCHAR(MAX) = '{string.Join(",", checkList.ToArray())}'
                            ;

                              WITH Main AS (
                                             SELECT LEFT(Split.value, CHARINDEX('-', Split.value) - 1)                                      AS Barcode
                                                  , CONVERT(BIT, SUBSTRING(Split.value, CHARINDEX('-', Split.value) + 1, LEN(Split.value))) AS Block
                                               FROM STRING_SPLIT(@PramList, ',') AS Split
                                           )
                            UPDATE Rm_Stock
                               SET Block      = Main.Block
                                 , Rm_Updated = GETDATE()
                            OUTPUT inserted.Rm_BarCode
                                 , inserted.Rm_Material
                                 , inserted.Rm_Supplier
                                 , inserted.Rm_ProdDate
                                 , inserted.Rm_QtyinBox
                                 , inserted.Rm_BoxSeq
                                 , inserted.Block
                                 , '{WiseApp.Id}' AS Creator                                 
                                 , inserted.Rm_Updated
                              INTO RawMaterialStockBlockingHist (Barcode, Material, Supplier, ProdDate, QtyinBox, BoxSeq, Block, Creator, Created)
                              FROM Main
                             WHERE Rm_Stock.Rm_BarCode = Main.Barcode
                            ";
            DbAccess.Default.ExecuteQuery(query);
            //저장완료 메시지
            System.Windows.Forms.MessageBox.Show($@"登录成功。(Registration Successful.)", "登录成功。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Search();
        }

        private void wiseDataGridView_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == wiseDataGridView_List.Columns["Block"].Index)
            {
                wiseDataGridView_List.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Convert.ToInt32(wiseDataGridView_List.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 1 ? 0 : 1;
            }
        }
    }
}