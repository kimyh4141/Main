using System;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Client
{
    public partial class ConfirmType : Form
    {
        private static string Material => WbtCustomService.ActiveValues.Material;
        private static string WorkOrder => WbtCustomService.ActiveValues.WorkOrder;
        private static string WorkCenter => WbtCustomService.ActiveValues.Workcenter;
        public string Type { get; private set; }
        public DateTime PackingDate { get; private set; }
        public string PalletBarcode { get; private set; }
        public string BoxBarcode { get; private set; }
        private int _completeQty;
        private int _boxQty;

        public ConfirmType()
        {
            InitializeComponent();
        }

        private void ConfirmType_Load(object sender, EventArgs e)
        {
            var query = $@"
                SELECT M.Type
                     , COALESCE(M_T.QtyInBox, 0)                                   AS QtyInBox
                     , COALESCE(M_T.BoxQtyInPallet, 0)                             AS BoxQtyInPallet
                     , COALESCE(M_T.BoxQtyInPallet, 0) * COALESCE(M_T.QtyInBox, 0) AS CompleteQty
                  FROM Material M
                       LEFT OUTER JOIN Material M_T
                                       ON M.TOP_ITEM_CD = M_T.Material
                 WHERE M.Material = '{Material}'
                  ";
            var dataTable = DbAccess.Default.GetDataTable(query);
            if (dataTable.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no Material");
                Close();
                return;
            }
            else if (string.IsNullOrEmpty(dataTable.Rows[0]["Type"].ToString()))
            {
                MessageBox.ShowCaption("Không xác định được loại PCB (Single/Double) trên hệ thống MES（SINGLE / DOUBLE）。tại chương trình MES Browser Bạn phải nhập loại PCB cho mục [" + Material + "]。\n\n" + "PCB type (SINGLE / DOUBLE) is not defined in MES system.\n\n" + "You have to enter the PCB type for this item [ " + Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                Close();
                return;
            }
            else if (dataTable.Rows[0]["QtyInBox"].ToString() == "0"
                     || dataTable.Rows[0]["BoxQtyInPallet"].ToString() == "0")
            {
                MessageBox.ShowCaption("Không xác định được số lượng Box và số lượng Pallet trên hệ thống MES。tại chương trình MES Browser 程序中输入此项目 [ " + Material + " ] 的箱子数量和托盘数量。\n\n" + "Box quantity and pallet quantity are not defined in the MES system.\n\n" + "You have to enter box quantity and pallet quantity for this item [ " + Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                Close();
                return;
            }

            string type = dataTable.Rows[0]["Type"].ToString();

            _completeQty = int.Parse(dataTable.Rows[0]["CompleteQty"].ToString());
            _boxQty = int.Parse(dataTable.Rows[0]["QtyInBox"].ToString());

            if (type == "SINGLE") rb_Single.Checked = true;
            else rb_double.Checked = true;

            var check = $@"SELECT TOP 1 * FROM BoxTemp WHERE WorkOrder = '{WorkOrder}' AND WorkCenter = '{WorkCenter}'";

            var check_dt = DbAccess.Default.GetDataTable(check);

            if (check_dt.Rows.Count > 0)
            {
                textbox_PalletBcd.Enabled = false;
                textbox_BoxBcd.Enabled = false;
            }

            dtp_date.Value = DateTime.Today;
            textbox_PalletBcd.Text = string.Empty;
            textbox_BoxBcd.Text = string.Empty;
        }

        private void textbox_PalletBcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string Q = $@"
                        SELECT TOP 1 COALESCE(Material, '') AS Material
                          FROM PalletbcdPrintHist
                         WHERE PalletBcd = '{textbox_PalletBcd.Text}'
                         ORDER BY Reprint DESC
                        SELECT COUNT(PcbBcd) AS Cnt
                          FROM Packing
                         WHERE PalletBcd = '{textbox_PalletBcd.Text}'
                        ";

            DataSet ds = DbAccess.Default.GetDataSet(Q);

            if (ds.Tables.Count != 2)
            {
                MessageBox.ShowCaption("Wrong PalletBarcode", "Error", MessageBoxIcon.Error);
                textbox_PalletBcd.Text = string.Empty;
                textbox_PalletBcd.Focus();
                return;
            }
            else if (ds.Tables[0].Rows[0]["Material"].ToString() != Material)
            {
                MessageBox.ShowCaption("Wrong PalletBarcode (Different Material)", "Error", MessageBoxIcon.Error);
                textbox_PalletBcd.Text = string.Empty;
                textbox_PalletBcd.Focus();
                return;
            }
            else if (ds.Tables[0].Rows[0]["Cnt"].ToString() == "0")
            {
                MessageBox.ShowCaption("Wrong PalletBarcode", "Error", MessageBoxIcon.Error);
                textbox_PalletBcd.Text = string.Empty;
                textbox_PalletBcd.Focus();
                return;
            }
            else if (int.Parse(ds.Tables[0].Rows[0]["Cnt"].ToString()) >= _completeQty)
            {
                MessageBox.ShowCaption("Wrong PalletBarcode", "Error", MessageBoxIcon.Error);
                textbox_PalletBcd.Text = string.Empty;
                textbox_PalletBcd.Focus();
                return;
            }

            textbox_BoxBcd.Focus();
        }

        private void textbox_BoxBcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string Q = $@"
                        SELECT COALESCE(Material, '') AS Material
                          FROM BoxbcdPrintHist
                         WHERE BoxBarcode_2 = '{textbox_BoxBcd.Text}'
                         ORDER BY Reprint DESC

                        SELECT COUNT(PcbBcd) AS Cnt
                          FROM Packing
                         WHERE BoxBcd = '{textbox_BoxBcd.Text}'
                        ";
            var ds = DbAccess.Default.GetDataSet(Q);

            if (ds.Tables.Count != 2)
            {
                MessageBox.ShowCaption("Wrong BoxBarcode", "Error", MessageBoxIcon.Error);
                textbox_BoxBcd.Text = string.Empty;
                textbox_BoxBcd.Focus();
                return;
            }

            if (ds.Tables[0].Rows[0]["Material"].ToString() != Material)
            {
                MessageBox.ShowCaption("Wrong BoxBarcode (Different Material)", "Error", MessageBoxIcon.Error);
                textbox_BoxBcd.Text = string.Empty;
                textbox_BoxBcd.Focus();
                return;
            }

            if (ds.Tables[1].Rows[0]["Cnt"].ToString() == "0")
            {
                MessageBox.ShowCaption("Wrong BoxBarcode", "Error", MessageBoxIcon.Error);
                textbox_BoxBcd.Text = string.Empty;
                textbox_BoxBcd.Focus();
                return;
            }

            if (int.Parse(ds.Tables[1].Rows[0]["Cnt"].ToString()) < _boxQty) return;

            MessageBox.ShowCaption("Wrong BoxBarcode (Complete Box)", "Error", MessageBoxIcon.Error);
            textbox_BoxBcd.Text = string.Empty;
            textbox_BoxBcd.Focus();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Type = rb_Single.Checked ? "SINGLE" : "DOUBLE";

            PackingDate = dtp_date.Value;

            PalletBarcode = !string.IsNullOrEmpty(textbox_PalletBcd.Text) ? textbox_PalletBcd.Text : string.Empty;

            BoxBarcode = !string.IsNullOrEmpty(textbox_BoxBcd.Text) ? textbox_BoxBcd.Text : string.Empty;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btn_palletClear_Click(object sender, EventArgs e)
        {
            textbox_PalletBcd.Text = string.Empty;
        }

        private void btn_boxClear_Click(object sender, EventArgs e)
        {
            textbox_BoxBcd.Text = string.Empty;
        }
    }
}
