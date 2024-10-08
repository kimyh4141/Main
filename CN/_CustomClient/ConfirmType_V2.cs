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

namespace WiseM.Client
{
    public partial class ConfirmType_V2 : Form
    {
        string _Material = WbtCustomService.ActiveValues.Material;
        string _Workorder = WbtCustomService.ActiveValues.WorkOrder;
        string _Workcenter = WbtCustomService.ActiveValues.Workcenter;
        string _type = "";
        string _date = "";
        string _palletbcd = "";
        string _boxbcd = "";
        int _completeQty = 0;
        int _boxQty = 0;

        public ConfirmType_V2()
        {
            InitializeComponent();
        }
        private void ConfirmType_Load(object sender, EventArgs e)
        {
            string Q;
            Q = $@"
                 SELECT M.Type, coalesce(M_T.QtyInBox, 0) QtyInBox, coalesce(M_T.BoxQtyInPallet, 0) BoxQtyInPallet
                      , coalesce(M_T.BoxQtyInPallet, 0) * coalesce(M_T.QtyInBox, 0) CompleteQty
                 FROM Material                 M
                      LEFT OUTER JOIN Material M_T
                                      ON M.TOP_ITEM_CD = M_T.Material
                 WHERE M.Material = '{_Material}'
                  ";

            DataTable dt = DbAccess.Default.GetDataTable(Q);

            if (dt.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no Material");
                this.Close();
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["Type"].ToString()))
            {
                MessageBox.ShowCaption("MES系统中未定义PCB类型（SINGLE / DOUBLE）。您必须在 MES Browser 程序中输入此项目的 PCB 类型[" + _Material + "]。\n\n" +
                            "PCB type (SINGLE / DOUBLE) is not defined in MES system.\n\n" +
                            "You have to enter the PCB type for this item [ " + _Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                this.Close();
            }
            else if (dt.Rows[0]["QtyInBox"].ToString() == "0" || dt.Rows[0]["BoxQtyInPallet"].ToString() == "0")
            {
                MessageBox.ShowCaption("箱子和托盘数量在MES系统中没有定义。您必须在 MES Browser 程序中输入此项目 [ " + _Material + " ] 的箱子数量和托盘数量。\n\n" +
                            "Box quantity and pallet quantity are not defined in the MES system.\n\n" +
                            "You have to enter box quantity and pallet quantity for this item [ " + _Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                this.Close();
            }

            string type = dt.Rows[0]["Type"].ToString();

            _completeQty = int.Parse(dt.Rows[0]["CompleteQty"].ToString());
            _boxQty = int.Parse(dt.Rows[0]["QtyInBox"].ToString());

            if (type == "SINGLE") rb_Single.Checked = true;
            else rb_double.Checked = true;

            string check = $@"SELECT TOP 1 * FROM BoxTemp WHERE WorkOrder = '{_Workorder}' AND WorkCenter = '{_Workcenter}'";

            DataTable check_dt = DbAccess.Default.GetDataTable(check);

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
            if (e.KeyCode == Keys.Enter)
            {
                string Q = $@"
SELECT Material FROM PalletbcdPrintHist WHERE PalletBcd = '{textbox_PalletBcd.Text}'

SELECT COUNT(PcbBcd) Cnt FROM Packing WHERE PalletBcd = '{textbox_PalletBcd.Text}'
";

                DataSet ds = DbAccess.Default.GetDataSet(Q);

                if (ds.Tables.Count != 2)
                {
                    MessageBox.ShowCaption("Wrong PalletBarcode", "Error", MessageBoxIcon.Error);
                    textbox_PalletBcd.Text = string.Empty;
                    textbox_PalletBcd.Focus();
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Material"].ToString() != _Material)
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
        }

        private void textbox_BoxBcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Q = $@"
SELECT Material FROM BoxbcdPrintHist WHERE BoxBarcode_2 = '{textbox_BoxBcd.Text}'

SELECT COUNT(PcbBcd) Cnt FROM Packing WHERE BoxBcd = '{textbox_BoxBcd.Text}'
";
                DataSet ds = DbAccess.Default.GetDataSet(Q);

                if (ds.Tables.Count != 2)
                {
                    MessageBox.ShowCaption("Wrong BoxBarcode", "Error", MessageBoxIcon.Error);
                    textbox_BoxBcd.Text = string.Empty;
                    textbox_BoxBcd.Focus();
                    return;
                }
                else if (ds.Tables[0].Rows[0]["Material"].ToString() != _Material)
                {
                    MessageBox.ShowCaption("Wrong BoxBarcode (Different Material)", "Error", MessageBoxIcon.Error);
                    textbox_BoxBcd.Text = string.Empty;
                    textbox_BoxBcd.Focus();
                    return;
                }
                else if (ds.Tables[1].Rows[0]["Cnt"].ToString() == "0")
                {
                    MessageBox.ShowCaption("Wrong BoxBarcode", "Error", MessageBoxIcon.Error);
                    textbox_BoxBcd.Text = string.Empty;
                    textbox_BoxBcd.Focus();
                    return;
                }
                else if (int.Parse(ds.Tables[1].Rows[0]["Cnt"].ToString()) >= _boxQty)
                {
                    MessageBox.ShowCaption("Wrong BoxBarcode (Complete Box)", "Error", MessageBoxIcon.Error);
                    textbox_BoxBcd.Text = string.Empty;
                    textbox_BoxBcd.Focus();
                    return;
                }
            }
        }

        public string GetItem(string name)
        {
            switch (name)
            {
                case "Type":
                    return _type;
                case "Date":
                    return _date;
                case "Palletbcd":
                    return _palletbcd;
                case "Boxbcd":
                    return _boxbcd;
                default:
                    return "";
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (rb_Single.Checked) _type = "SINGLE";
            else _type = "DOUBLE";

            _date = dtp_date.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(textbox_PalletBcd.Text)) _palletbcd = textbox_PalletBcd.Text;
            else _palletbcd = string.Empty;

            if (!string.IsNullOrEmpty(textbox_BoxBcd.Text)) _boxbcd = textbox_BoxBcd.Text;
            else _boxbcd = string.Empty;

            this.DialogResult = DialogResult.OK;
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