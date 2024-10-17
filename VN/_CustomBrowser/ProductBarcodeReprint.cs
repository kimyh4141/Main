using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Client
{
    public partial class ProductBarcodeReprint : Form
    {
        public ProductBarcodeReprint(string type, string line)
        {
            InitializeComponent();
            textBox_SearchType.Text = type;
            textBox_SearchLine.Text = line;
        }

        public void ClearForm()
        {
            textBox_Barcode.Text = string.Empty;
            textBox_ProdDate.Text = string.Empty;
            textBox_Line.Text = string.Empty;
        }

        private void SearchGrid()
        {
            string query = string.Empty;
            switch (textBox_SearchType.Text)
            {
                case "ProductBox":
                {
                    query = $@"
                            SELECT DISTINCT BoxBarcode_2                              AS Barcode
                                          , Spec
                                          , Qty
                                          , CONVERT(NVARCHAR(50), ProductionDate, 23) AS ProductionDate
                                          , ProductionLine
                              FROM BoxbcdPrintHist
                             WHERE ProductionDate BETWEEN '{dateTimePicker_From.Value:yyyy-MM-dd}' AND '{dateTimePicker_To.Value:yyyy-MM-dd}'
                               AND BoxBarcode_2 LIKE '%{textBox_SearchBarcode.Text}%'
                               AND Spec LIKE '%{textBox_SearchSpec.Text}%'
                               AND 1 = CASE
                                         WHEN 'Repacking' = '{textBox_SearchLine.Text}'
                                           THEN 1
                                           ELSE IIF(ProductionLine = '{textBox_SearchLine.Text}', 1, 0)
                                       END
                             ORDER BY BoxBarcode_2
                         ";
                    break;
                }
                case "Pallet":
                {
                    query = $@"
                            SELECT DISTINCT PalletBcd                                 AS Barcode
                                          , Model
                                          , Qty
                                          , CONVERT(NVARCHAR(50), ProductionDate, 23) AS ProductionDate
                                          , ProductionLine
                              FROM PalletbcdPrintHist
                             WHERE ProductionDate BETWEEN '{dateTimePicker_From.Value:yyyy-MM-dd}' AND '{dateTimePicker_To.Value:yyyy-MM-dd}'
                               AND PalletBcd LIKE '%{textBox_SearchBarcode.Text}%'
                               AND Model LIKE '%{textBox_SearchSpec.Text}%'
                               AND 1 = CASE
                                         WHEN 'Repacking' = '{textBox_SearchLine.Text}'
                                           THEN 1
                                           ELSE IIF(ProductionLine = '{textBox_SearchLine.Text}', 1, 0)
                                       END
                             ORDER BY PalletBcd
                         ";
                    break;
                }
            }

            dataGridView_PrintHist.DataSource = DbAccess.Default.GetDataTable(query);
        }

        private bool Reprint()
        {
            try
            {
                string query = string.Empty;

                if (dataGridView_PrintHist.CurrentRow == null) return false;

                switch (textBox_SearchType.Text)
                {
                    case "ProductBox":
                    {
                        query = $@"
                            SELECT TOP 1 *
                              FROM BoxbcdPrintHist
                             WHERE BoxBarcode_2 = '{dataGridView_PrintHist.CurrentRow.Cells["Barcode"].Value}'
                             ORDER BY Updated DESC
                            ";
                        var dataTable = DbAccess.Default.GetDataTable(query);

                        var dataTableLabel = DbAccess.Default.GetDataTable("SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'");

                        var clsBarcode = new clsBarcode.clsBarcode();
                        clsBarcode.LoadFromXml(dataTableLabel.Rows[0][0].ToString());

                        clsBarcode.Data.SetText("PARTNO", $"{dataTable.Rows[0]["LG_PartNo"]}");
                        clsBarcode.Data.SetText("QTY", $"{dataTable.Rows[0]["Qty"]:0#} EA");
                        clsBarcode.Data.SetText("DESC", $"{dataTable.Rows[0]["Description"]}");
                        clsBarcode.Data.SetText("SPEC", $"{dataTable.Rows[0]["Spec"]}");
                        //clsBarcode.Data.SetText("DATE", $"{productionDate.Substring(0, 4)}. {productionDate.Substring(5, 2)}. {productionDate.Substring(8, 2)}");
                        clsBarcode.Data.SetText("DATE", $"{dataTable.Rows[0]["ProductionDate"]:YYYY. MM. DD}");
                        clsBarcode.Data.SetText("BARCODE1", $"{dataTable.Rows[0]["BoxBarcode_1"]}");
                        clsBarcode.Data.SetText("BARCODE2", $"{dataTable.Rows[0]["BoxBarcode_2"]}");
                        //gmryu 2023-10-10 일반라인/영문 라인 구분
                        var lineCheck = $"{dataTable.Rows[0]["ProductionLine"]}"[4];
                        clsBarcode.Data.SetText("Y2SOLUTION", $"Y2 SOLUTION{(char.IsDigit(lineCheck) ? "(VN)" : "")}");
                        clsBarcode.Print(false);
                        string queryInsert =
                            $@"
                        INSERT
                          INTO BoxbcdPrintHist
                          (
                            BoxBarcode_1
                          , BoxBarcode_2
                          , LG_PartNo
                          , Spec
                          , Description
                          , ProductionDate
                          , ProductionLine
                          , Material
                          , Qty
                          , Mfg_Line
                          , Mfg_ymd
                          , SerialNo
                          , Reprint
                          , Updated
                          , Updater
                          )
                        SELECT TOP 1 BoxBarcode_1
                                   , BoxBarcode_2
                                   , LG_PartNo
                                   , Spec
                                   , Description
                                   , ProductionDate
                                   , ProductionLine
                                   , Material
                                   , Qty
                                   , Mfg_Line
                                   , Mfg_ymd
                                   , SerialNo
                                   , Reprint + 1
                                   , GETDATE()
                                   , '{WiseApp.Id}'
                          FROM BoxbcdPrintHist
                         WHERE BoxBarcode_2 = '{dataTable.Rows[0]["BoxBarcode_2"]}'
                         ORDER BY Reprint DESC
                       ";
                        DbAccess.Default.ExecuteQuery(queryInsert);
                        break;
                    }
                    case "Pallet":
                    {
                        query = $@"SELECT * FROM PalletbcdPrintHist WHERE PalletBcd = '{dataGridView_PrintHist.CurrentRow.Cells["Barcode"].Value}'";
                        var dataTable = DbAccess.Default.GetDataTable(query);

                        string strSql = @"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'";
                        DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                        var clsBarcode = new clsBarcode.clsBarcode();
                        clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                        clsBarcode.Data.SetText("PARTNO", $"{dataTable.Rows[0]["LG_PartNo"]}");
                        clsBarcode.Data.SetText("MODEL", $"{dataTable.Rows[0]["Model"]}");
                        clsBarcode.Data.SetText("QTY", dataTable.Rows[0]["Qty"] + " EA");
                        clsBarcode.Data.SetText("PALLETBCD", $"{dataTable.Rows[0]["PalletBcd"]}");

                        clsBarcode.Print(false);

                        string queryInsert =
                            $@"
                        INSERT
                          INTO PalletbcdPrintHist
                          (
                            LG_PartNo
                          , Model
                          , Qty
                          , PalletBcd
                          , Mfg_ymd
                          , Mfg_Line
                          , ProductionDate
                          , ProductionLine
                          , Material
                          , SerialNo
                          , Reprint
                          , Updated
                          , Updater
                          )
                        SELECT TOP 1 LG_PartNo
                                   , Model
                                   , Qty
                                   , PalletBcd
                                   , Mfg_ymd
                                   , Mfg_Line
                                   , ProductionDate
                                   , ProductionLine
                                   , Material
                                   , SerialNo
                                   , Reprint + 1
                                   , GETDATE()
                                   , '{WiseApp.Id}'
                          FROM PalletbcdPrintHist
                         WHERE PalletBcd = '{dataTable.Rows[0]["PalletBcd"]}'
                         ORDER BY Reprint DESC
                        ";
                        DbAccess.Default.ExecuteQuery(queryInsert);
                        break;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void dataGridView_PrintHist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_PrintHist.CurrentRow == null) return;
            textBox_Barcode.Text = $@"{dataGridView_PrintHist.CurrentRow.Cells["Barcode"].Value}";
            textBox_ProdDate.Text = $@"{dataGridView_PrintHist.CurrentRow.Cells["ProductionDate"].Value:YYYY-MM-DD}";
            textBox_Line.Text = $@"{dataGridView_PrintHist.CurrentRow.Cells["ProductionLine"].Value}";
        }

        private void button_Print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_ProdDate.Text))
            {
                MessageBox.ShowCaption("Vui lòng chọn số Box phát hành lại。\r\nPlease select the box number to be reissued.", "ERROR", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？ \r\nAre you sure?", "Đóng những hộp còn lại(Box Remainder Closing)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (Reprint())
            {
                //저장완료 메시지
                System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)", "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void Reprint_Load(object sender, EventArgs e)
        {
            
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            ClearForm();
            SearchGrid();
        }

        private void dataGridView_PrintHist_DataSourceChanged(object sender, EventArgs e)
        {
            // dataGridView_PrintHist.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            // dataGridView_PrintHist.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            // dataGridView_PrintHist.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            // dataGridView_PrintHist.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            // dataGridView_PrintHist.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}