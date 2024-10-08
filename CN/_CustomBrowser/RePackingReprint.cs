﻿using System;
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
    public partial class RepackingReprint : Form
    {
        string barcode;
        DataTable dataTable = new DataTable();

        string Query;

        public RepackingReprint()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            dataTable.Rows.Clear();
            dataGridView_List.DataSource = null;
            dataGridView_List.Rows.Clear();
            dataGridView_List.Refresh();
            textBox_Type.Text = string.Empty;
            textBox_Barcode.Text = string.Empty;

            dateTimePicker_From.Value = DateTime.Today;
            dateTimePicker_To.Value = DateTime.Today;

            dataGridView_List.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
            OpenGridView();
        }

        private void OpenGridView()
        {
            dataTable.Rows.Clear();
            dataGridView_List.DataSource = null;
            dataGridView_List.Rows.Clear();
            dataGridView_List.Refresh();

            string Q = $@"
SELECT Material
     , PackType
     , NewBarcode AS Barcode
     , Updated
  FROM RePackingHist RPH
 WHERE UpdateType <> 'Delete'
   AND CONVERT(DATE, Updated) >= '{dateTimePicker_From.Value:yyyy-MM-dd}'
   AND CONVERT(DATE, Updated) <= '{dateTimePicker_To.Value:yyyy-MM-dd}'
   AND EXISTS(
         SELECT 'X'
           FROM Packing P
          WHERE RPH.NewBarcode = P.PalletBcd
             OR RPH.NewBarcode = P.BoxBcd
          UNION ALL
         SELECT 'X'
           FROM Stock S
          WHERE RPH.NewBarcode = S.PalletBcd
             OR RPH.NewBarcode = S.BoxBcd
     )
 ORDER BY
     Updated
                         ";

            dataTable = DbAccess.Default.GetDataTable(Q);
            dataGridView_List.DataSource = dataTable;

            this.dataGridView_List.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView_List.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView_List.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridView_List.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        private void dgv_printhist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_Type.Text = dataGridView_List.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox_Barcode.Text = dataGridView_List.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Barcode.Text))
            {
                MessageBox.ShowCaption("请选择要重新发行的箱号。\r\nPlease select the box number to be reissued.", "ERROR", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "剩余数量已关闭(Box Remainder Closing)",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    clsBarcode.clsBarcode clsBarcode = new clsBarcode.clsBarcode();
                    DataRow dataRow = null;
                    string type = textBox_Type.Text;
                    string barcode = textBox_Barcode.Text;
                    string query;
                    string histQuery = string.Empty;

                    switch (type)
                    {
                        case "ProductBox":
                        {
                            object bcdData = string.Empty;

                            query = $@"
SELECT TOP 1
    Seq
     , BoxBarcode_1
     , BoxBarcode_2
     , Material
     , LG_PartNo
     , Spec
     , Description
     , Qty
     , ProductionDate
     , ProductionLine
     , Mfg_Line
     , Mfg_ymd
     , SerialNo
     , Reprint
     , Updated
     , Updater
  FROM BoxbcdPrintHist
 WHERE BoxBarcode_2 = '{barcode}'
 ORDER BY
     Updated DESC
;
";
                            dataRow = DbAccess.Default.GetDataRow(query);
                            bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'");
                            clsBarcode.LoadFromXml(bcdData.ToString());
                            clsBarcode.Data.SetText("PARTNO", dataRow["LG_PartNo"] as string);
                            clsBarcode.Data.SetText("QTY", dataRow["Qty"] + " EA");
                            clsBarcode.Data.SetText("DESC", dataRow["Description"] as string);
                            clsBarcode.Data.SetText("SPEC", dataRow["Spec"] as string);
                            clsBarcode.Data.SetText("DATE", $"{dataRow["ProductionDate"]:yyyy. MM. dd}");
                            clsBarcode.Data.SetText("BARCODE1", $"{dataRow["BoxBarcode_1"] as string}");
                            clsBarcode.Data.SetText("BARCODE2", $"{dataRow["BoxBarcode_2"] as string}");
                            histQuery = $@"
INSERT
  INTO BoxbcdPrintHist
      (
          BoxBarcode_1
      ,   BoxBarcode_2
      ,   Material
      ,   LG_PartNo
      ,   Spec
      ,   Description
      ,   Qty
      ,   ProductionDate
      ,   ProductionLine
      ,   Mfg_Line
      ,   Mfg_ymd
      ,   SerialNo
      ,   Reprint
      ,   Updated
      ,   Updater
      )
SELECT BoxBarcode_1
     , BoxBarcode_2
     , Material
     , LG_PartNo
     , Spec
     , Description
     , Qty
     , ProductionDate
     , ProductionLine
     , Mfg_Line
     , Mfg_ymd
     , SerialNo
     , Reprint + 1
     , GETDATE()
     , '{WiseApp.Id}'
  FROM BoxbcdPrintHist
 WHERE Seq = '{dataRow["Seq"]}'
;
";
                        }
                            break;
                        case "Pallet":
                        {
                            var bcdData = DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'");
                            clsBarcode.LoadFromXml(bcdData.ToString());
                            query = $@"
SELECT TOP 1
    Seq
     , PalletBcd
     , LG_PartNo
     , Model
     , Material
     , Qty
     , ProductionDate
     , ProductionLine
     , Mfg_ymd
     , Mfg_Line
     , SerialNo
     , Reprint
     , Updated
     , Updater
  FROM PalletbcdPrintHist
 WHERE PalletBcd = '{barcode}'
 ORDER BY
     Updated DESC
;
";
                            dataRow = DbAccess.Default.GetDataRow(query);
                            //Print
                            clsBarcode.Data.SetText("PARTNO", dataRow["LG_PartNo"] as string);
                            clsBarcode.Data.SetText("MODEL", dataRow["Model"] as string);
                            clsBarcode.Data.SetText("QTY", dataRow["Qty"] + " EA");
                            clsBarcode.Data.SetText("PALLETBCD", dataRow["PalletBcd"] as string);
                            histQuery = $@"
INSERT
  INTO PalletbcdPrintHist
      (
          PalletBcd
      ,   LG_PartNo
      ,   Model
      ,   Material
      ,   Qty
      ,   ProductionDate
      ,   ProductionLine
      ,   Mfg_ymd
      ,   Mfg_Line
      ,   SerialNo
      ,   Reprint
      ,   Updated
      ,   Updater
      )
SELECT PalletBcd
     , LG_PartNo
     , Model
     , Material
     , Qty
     , ProductionDate
     , ProductionLine
     , Mfg_ymd
     , Mfg_Line
     , SerialNo
     , Reprint + 1
     , GETDATE()
     , '{WiseApp.Id}'
  FROM PalletbcdPrintHist
 WHERE Seq = '{dataRow["Seq"]}'
";
                        }
                            break;
                    }

                    clsBarcode.Print(false);
                    DbAccess.Default.ExecuteQuery(histQuery);
                    System.Windows.Forms.MessageBox.Show($@"登录成功。(Registration Successful.)", "登录成功。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private void dateTimePicker_From_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dateTimePicker_To_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            OpenGridView();
        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}