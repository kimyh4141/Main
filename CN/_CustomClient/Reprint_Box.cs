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
    public partial class Reprint_Box : Form
    {
        string boxbarcode;
        string productionDate;
        string productionLine;

        string boxbarcode1;
        string LG_PartNo;
        string spec;
        string desc;
        string strQty;

        string pd;
        DataTable dt = new DataTable();

        string Query;
        public Reprint_Box()
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
            tb_boxbarcode.Text = string.Empty;
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
                        SELECT DISTINCT BoxBarcode_2 as Boxbarcode, Spec, Qty, CONVERT(NVARCHAR(50), ProductionDate, 23) ProductionDate, ProductionLine 
                        FROM BoxbcdPrintHist where ProductionDate = '{pd}' and ProductionLine = '{tb_line.Text}'
                        ORDER BY BoxBarcode_2
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
            boxbarcode = dgv_printhist.Rows[e.RowIndex].Cells[0].Value.ToString();
            productionDate = dgv_printhist.Rows[e.RowIndex].Cells[3].Value.ToString();
            productionLine = dgv_printhist.Rows[e.RowIndex].Cells[4].Value.ToString();

            tb_boxbarcode.Text = boxbarcode;
            tb_pd.Text = productionDate.Substring(0, 10);
            tb_pl.Text = productionLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_pd.Text))
            {
                MessageBox.ShowCaption("请选择要重新发行的箱号。\r\nPlease select the box number to be reissued.", "ERROR", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "剩余数量已关闭(Box Remainder Closing)",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Query = $@"SELECT * FROM BoxbcdPrintHist WHERE BoxBarcode_2 = '{boxbarcode}'";
                DataTable dt = DbAccess.Default.GetDataTable(Query);

                boxbarcode1 = dt.Rows[0]["BoxBarcode_1"].ToString();
                LG_PartNo = dt.Rows[0]["LG_PartNo"].ToString();
                spec = dt.Rows[0]["Spec"].ToString();
                desc = dt.Rows[0]["Description"].ToString();
                strQty = dt.Rows[0]["Qty"].ToString();

                string tempqty = $"{strQty:0#}";

                string[] pd_split = productionDate.Split('-');

                string strSql;

                strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_Box'";

                DataTable dtMain = DbAccess.Default.GetDataTable(strSql);

                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                _clsBarcode.Data.SetText("PARTNO", LG_PartNo);
                _clsBarcode.Data.SetText("QTY", tempqty + " EA");
                _clsBarcode.Data.SetText("DESC", desc);
                _clsBarcode.Data.SetText("SPEC", spec);
                _clsBarcode.Data.SetText("DATE", productionDate.Substring(0, 4) + ". " + productionDate.Substring(5, 2) + ". " + productionDate.Substring(8, 2));
                _clsBarcode.Data.SetText("BARCODE1", boxbarcode1);
                _clsBarcode.Data.SetText("BARCODE2", boxbarcode);

                //_clsBarcode.PrinterName = "Microsoft Print to PDF";
                _clsBarcode.Print(false);

                string insertQ = $@"
                        INSERT INTO BoxbcdPrintHist 
                        (BoxBarcode_1, BoxBarcode_2, LG_PartNo, Spec, Description, ProductionDate, ProductionLine,
                        Material, Qty, Mfg_Line, Mfg_ymd, SerialNo, Reprint, Updated, Updater)
                        SELECT TOP 1 
                        BoxBarcode_1, BoxBarcode_2, LG_PartNo, Spec, Description, ProductionDate, ProductionLine,
                        Material, Qty, Mfg_Line, Mfg_ymd, SerialNo, Reprint + 1, GETDATE(), '{WiseApp.Id}'
                        FROM BoxbcdPrintHist WHERE BoxBarcode_2 = '{boxbarcode}' ORDER BY Reprint DESC
                               ";
                DbAccess.Default.ExecuteQuery(insertQ);
            }
        }

        private void Reprint_Box_Load(object sender, EventArgs e)
        {
            dtp_pd.Value = DateTime.Today;
        }
    }
}
