using System;
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
    public partial class PalletCheck : Form
    {
        #region F

        private readonly DataTable _dataTableScanHist = new DataTable();

        #endregion

        #region C

        public PalletCheck()
        {
            InitializeComponent();
        }

        #endregion

        #region M

        private bool ScanPallet(string barcode)
        {
            var query = new StringBuilder();
            query.AppendLine(
                $@"
DECLARE @Barcode NVARCHAR(50) = '{barcode}'
;

SELECT S.PalletBcd
     , S.Count
     , S.Material
     , M.Text
     , M.Spec
     , M.LG_ITEM_CD
     , M.LG_ITEM_NM
  FROM (
      SELECT S.PalletBcd
           , S.Material
           , COUNT(DISTINCT S.BoxBcd) AS Count
        FROM Stock S
       WHERE S.PalletBcd = @Barcode
       GROUP BY S.PalletBcd
              , S.Material
       ) AS S
       LEFT OUTER JOIN Material M
                       ON S.Material = M.Material
;
"
            );
            try
            {
                var dataRow = DbAccess.Default.GetDataRow(query.ToString());
                if (dataRow == null)
                {
                    System.Windows.Forms.MessageBox.Show($@"Thông tin Label không tồn tại。(Label information does not exist.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                textBox_Qty.Text = $@"{dataRow["Count"]:#,###}";
                textBox_Material.Text = dataRow["Material"].ToString();
                textBox_MaterialName.Text = dataRow["Text"].ToString();
                textBox_Spec.Text = dataRow["Spec"].ToString();
                textBox_ItemCode.Text = dataRow["LG_ITEM_CD"].ToString();
                textBox_ItemName.Text = dataRow["LG_ITEM_NM"].ToString();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private bool ScanBox(string barcode)
        {
            try
            {
                if (0 < DbAccess.Default.IsExist("Stock", $"PalletBcd = '{textBox_PalletScanBarcode.Text}' AND BoxBcd = '{barcode}'"))
                {
                    if (_dataTableScanHist.Rows.Cast<DataRow>().Any(dataRow => barcode.Equals(dataRow.Field<string>("Barcode"))))
                    {
                        System.Windows.Forms.MessageBox.Show($"已存在。(Already included in Hist.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    _dataTableScanHist.Rows.Add(_dataTableScanHist.Rows.Count + 1, barcode, "OK", $"{DbAccess.GetServerTime():yyyy-MM-dd HH:mm:ss}");
                    label_OkeyCount.Text = $@"{int.Parse(label_OkeyCount.Text) + 1}";
                    label_ScanCount.Text = $@"{int.Parse(label_ScanCount.Text) + 1}";
                }
                else
                {
                    _dataTableScanHist.Rows.Add(_dataTableScanHist.Rows.Count + 1, barcode, "NG", $"{DbAccess.GetServerTime():yyyy-MM-dd HH:mm:ss}");
                    label_NoGoodCount.Text = $@"{int.Parse(label_NoGoodCount.Text) + 1}";
                    label_ScanCount.Text = $@"{int.Parse(label_ScanCount.Text) + 1}";
                }


                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private bool Save()
        {
            var query = new StringBuilder();
            query.AppendLine($@"DECLARE @PalletBcd NVARCHAR(50) = '{textBox_PalletScanBarcode.Text}';");
            query.AppendLine($@"DECLARE @IF_TIME DATETIME = GETDATE();");

            foreach (DataRow dataRow in _dataTableScanHist.Rows)
            {
                query.AppendLine(
                    $@"
INSERT
  INTO PalletCheckHist ( PalletBcd
                       , Material
                       , ScanSeq
                       , boxBcd
                       , Result
                       , ScanTime
                       , UpdateUser
                       , Updated
                       )
VALUES ( @PalletBcd
       , '{textBox_Material.Text}'
       , '{dataRow["Seq"]}'
       , '{dataRow["Barcode"]}'
       , '{dataRow["Result"]}'
       , '{dataRow["ScanTime"]}'
       , '{WiseApp.Id}'
       , @IF_TIME
       )
"
                );
            }

            try
            {
                DbAccess.Default.ExecuteQuery(query.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Clear()
        {
            textBox_PalletScanBarcode.Text = string.Empty;
            textBox_PalletScanBarcode.ReadOnly = false;

            label_ScanCount.Text = "0";
            label_OkeyCount.Text = "0";
            label_NoGoodCount.Text = "0";

            textBox_Material.Text = string.Empty;
            textBox_Qty.Text = string.Empty;
            textBox_MaterialName.Text = string.Empty;
            textBox_Spec.Text = string.Empty;
            textBox_ItemCode.Text = string.Empty;
            textBox_ItemName.Text = string.Empty;

            groupBox_BoxScanHist.Enabled = false;
            tableLayoutPanel_Right.Enabled = false;

            _dataTableScanHist.Clear();
        }

        #endregion

        #region E

        private void PalletCheck_Load(object sender, EventArgs e)
        {
            _dataTableScanHist.Columns.Add("Seq");
            _dataTableScanHist.Columns.Add("Barcode");
            _dataTableScanHist.Columns.Add("Result");
            _dataTableScanHist.Columns.Add("ScanTime");
            dataGridView_ScanList.DataSource = _dataTableScanHist;
        }

        private void textBox_ScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!(sender is TextBox textBox)) return;
            textBox.Text = textBox.Text.Trim();
            if (ScanPallet(textBox.Text))
            {
                textBox.ReadOnly = true;
                groupBox_BoxScanHist.Enabled = true;
                tableLayoutPanel_Right.Enabled = true;
                textBox_BoxScanBarcode.Focus();
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        #endregion

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (_dataTableScanHist.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show($@"Không có hạng mục thay đổi。(Nothing has changed.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)", "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) return;
            if (Save())
            {
                //저장완료 메시지
                System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)", "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Clear
                Clear();
            }
            else
            {
            }
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)", "Câu hỏi(Question)", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
            {
                Clear();
            }
        }

        private void textBox_BoxScanBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (!(sender is TextBox textBox)) return;

            textBox.Text = textBox.Text.Trim();
            if (string.IsNullOrEmpty(textBox.Text))
            {
                return;
            }

            if (ScanBox(textBox.Text))
            {
                textBox.Text = string.Empty;
            }
            else
            {
                textBox.Text = string.Empty;
            }
        }

        private void dataGridView_ScanList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                switch (dataGridViewColumn.Name)
                {
                    case "Seq":
                        dataGridViewColumn.Width = 70;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Barcode":
                        dataGridViewColumn.Width = 200;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "Result":
                        dataGridViewColumn.Width = 70;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "ScanTime":
                        dataGridViewColumn.Width = 200;
                        dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }

                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void dataGridView_ScanList_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            var dataGridViewRow = dataGridView.Rows[e.RowIndex];
            if (dataGridViewRow.Cells["Result"].Value.ToString().Equals("NG"))
            {
                //Color 변경
                dataGridViewRow.Cells["Result"].Style.BackColor = Color.Red;
                dataGridViewRow.Cells["Result"].Style.ForeColor = Color.White;
            }
            else
            {
                dataGridViewRow.Cells["Result"].Style.BackColor = Color.Green;
                dataGridViewRow.Cells["Result"].Style.ForeColor = Color.White;
            }

            dataGridView.FirstDisplayedScrollingRowIndex = e.RowIndex;
        }
    }
}