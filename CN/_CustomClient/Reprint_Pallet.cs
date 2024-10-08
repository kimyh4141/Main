using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Client
{
    public partial class Reprint_Pallet : Form
    {
        string palletbarcode;
        string productionDate;
        string productionLine;

        string LG_PartNo;
        string model;
        string strQty;

        string pd;
        DataTable dt = new DataTable();

        string Query;

        public Reprint_Pallet()
        {
            InitializeComponent();

            init();
        }

        public void init()
        {
            dt.Rows.Clear();
            dgv_printhist.DataSource = null;
            dgv_printhist.Rows.Clear();
            dgv_printhist.Refresh();
            tb_palletbarcode.Text = string.Empty;
            tb_pd.Text = string.Empty;
            tb_pl.Text = string.Empty;

            tb_line.Text = WbtCustomService.ActiveValues.Workcenter;
            
            dgv_printhist.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
        }

        private void dtp_pd_ValueChanged(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            dgv_printhist.DataSource = null;
            dgv_printhist.Rows.Clear();
            dgv_printhist.Refresh();

            pd = dtp_pd.Value.ToString("yyyy-MM-dd");

            string Q = $@"
                          SELECT DISTINCT PalletBcd, Model, Qty, CONVERT(NVARCHAR(50), ProductionDate, 23) ProductionDate, ProductionLine
                          FROM PalletbcdPrintHist 
                          WHERE ProductionDate = '{pd}' and ProductionLine = '{tb_line.Text}'
                          ORDER BY PalletBcd
                         ";
            dt = DbAccess.Default.GetDataTable(Q);
            dgv_printhist.DataSource = dt;

            this.dgv_printhist.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_printhist.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_printhist.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dgv_printhist.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgv_printhist.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dgv_printhist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            palletbarcode = dgv_printhist.Rows[e.RowIndex].Cells[0].Value.ToString();
            productionDate = dgv_printhist.Rows[e.RowIndex].Cells[3].Value.ToString();
            productionLine = dgv_printhist.Rows[e.RowIndex].Cells[4].Value.ToString();

            tb_palletbarcode.Text = palletbarcode;
            tb_pd.Text = productionDate.Substring(0, 10);
            tb_pl.Text = productionLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_pd.Text))
            {
                MessageBox.ShowCaption("请选择要补发的托盘号。\r\nPlease select the pallet number to be reissued.", "", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "剩余数量已关闭(Box Remainder Closing)",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Query = $@"SELECT * FROM PalletbcdPrintHist WHERE PalletBcd = '{palletbarcode}'";
                DataTable dt = DbAccess.Default.GetDataTable(Query);

                LG_PartNo = dt.Rows[0]["LG_PartNo"].ToString();
                model = dt.Rows[0]["Model"].ToString();
                strQty = dt.Rows[0]["Qty"].ToString();

                string strSql = string.Format("SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Pallet'");
                DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                _clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                _clsBarcode.Data.SetText("MODEL", model);
                _clsBarcode.Data.SetText("QTY", strQty + " EA");
                _clsBarcode.Data.SetText("PALLETBCD", palletbarcode);

                _clsBarcode.Print(false);

                string updateQ = $@"
                        INSERT INTO PalletbcdPrintHist
                        (LG_PartNo, Model, Qty, PalletBcd, Mfg_ymd, Mfg_Line, ProductionDate, ProductionLine,
                        Material, SerialNo, Reprint, Updated, Updater)
                        SELECT TOP 1
                        LG_PartNo, Model, Qty, PalletBcd, Mfg_ymd, Mfg_Line, ProductionDate, ProductionLine,
                        Material, SerialNo, Reprint + 1, GETDATE(), '{WiseApp.Id}'
                        FROM PalletbcdPrintHist WHERE PalletBcd = '{palletbarcode}' ORDER BY Reprint DESC
                               ";
                DbAccess.Default.ExecuteQuery(updateQ);
            }
        }

        private void Reprint_Pallet_Load(object sender, EventArgs e)
        {
            dtp_pd.Value = DateTime.Today;
        }
    }
}
