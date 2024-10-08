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

namespace WiseM.Browser.WMS
{
    public partial class Reprint_PCB_M : Form
    {
        string material;
        string lg_nm;
        string revision;
        string pcbNo;

        private DataTable _dataTable;
        public Reprint_PCB_M()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            lbl_pcbNo.Text = string.Empty;
            lbl_Material.Text = string.Empty;
            lbl_LG_CD.Text = string.Empty;
            lbl_LG_NM.Text = string.Empty;
            lbl_Date.Text = string.Empty;
            lbl_Revision.Text = string.Empty;
            tb_Search.Text = string.Empty;
     
            pcbNo = string.Empty;
            _dataTable = null;
            tb_Search.Focus();
        }

        private void OpenDataGridView()
        {
            pcbNo = tb_Search.Text.ToUpper();
            string Q = $@"
SELECT PPH.Bcd_Info
     , PPH.Material
     , PPH.Label_Size
     , PPH.LG_Material_CD
     , PPH.LG_Material_NM
     , PPH.ProductionDate
     , M.Revision
  FROM PcbPrintHist             AS PPH WITH (NOLOCK)
       LEFT OUTER JOIN Material AS M
                       ON PPH.Material = M.Material
 WHERE Bcd_Info = '{pcbNo}'
 ORDER BY
     PPH.Updated DESC
OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY
;
                        ";
            _dataTable = DbAccess.Default.GetDataTable(Q);

            if (_dataTable.Rows.Count > 0)
            {
                lbl_pcbNo.Text = _dataTable.Rows[0]["Bcd_Info"].ToString();
                lbl_Material.Text = _dataTable.Rows[0]["Material"].ToString();
                lbl_LG_CD.Text = _dataTable.Rows[0]["LG_Material_CD"].ToString();
                lbl_LG_NM.Text = _dataTable.Rows[0]["LG_Material_NM"].ToString();
                lbl_Date.Text = _dataTable.Rows[0]["ProductionDate"].ToString();
                lbl_Revision.Text = _dataTable.Rows[0]["Revision"].ToString();
            }
            else
            {
                label_pcbbcd.Text = "Không tìm thấy";
                label_result.Text = "Not Found.";
                label_pcbbcd.ForeColor = Color.Red;
                label_result.ForeColor = Color.Red;             
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            label_pcbbcd.Text = "Printing";
            label_result.Text = "Wait";
            label_pcbbcd.ForeColor = Color.Red;
            label_result.ForeColor = Color.Red;
            OpenDataGridView();
            btn_Reprint.PerformClick();          
        }


        private void tb_Search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                label_pcbbcd.Text = "Printing";
                label_result.Text = "Wait";
                label_pcbbcd.ForeColor = Color.Red;
                label_result.ForeColor = Color.Red;
                OpenDataGridView();
                btn_Reprint.PerformClick();
            }
        }
        private void btn_Reprint_Click(object sender, EventArgs e)
        {
            if (_dataTable.Rows.Count == 1)
            {
                try
                {
                    string strSql;
                    DataTable dtMain;
                    switch (_dataTable.Rows[0]["Label_Size"].ToString())
                    {
                        case "32":
                            strSql = "SELECT BcdData FROM BcdLblFmtr with(nolock) WHERE BcdName='Label_PCB'";
                            break;
                        default:
                            strSql = "SELECT BcdData FROM BcdLblFmtr with(nolock) WHERE BcdName='Label_PCB_40'";
                            break;
                    }
                    dtMain = DbAccess.Default.GetDataTable(strSql);
                    clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                    _clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

                    _clsBarcode.Data.SetText("BARCODE", pcbNo);
                    _clsBarcode.Data.SetText("BARCODETEXT", $"{pcbNo} ({lbl_Revision.Text})");

                    //_clsBarcode.PrinterName = "Microsoft Print to PDF";
                    _clsBarcode.Print(false);

                    databaseSave();

                    label_pcbbcd.Text = pcbNo;
                    label_result.Text = "Print Complete";
                    label_pcbbcd.ForeColor = Color.Green;
                    label_result.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    label_pcbbcd.Text = pcbNo;
                    label_result.Text = "Print Fail";
                    label_pcbbcd.ForeColor = Color.Red;
                    label_result.ForeColor = Color.Red;
                  
                }
                finally {
                    init();
                }           
               
            }
            else
            {
                label_pcbbcd.Text = "Không tìm thấy PCB";
                label_result.Text = "Not Found PCB.";
                label_pcbbcd.ForeColor = Color.Red;
                label_result.ForeColor = Color.Red;         
                return;
            }
        }
        private void databaseSave()
        {
            try
            {
                string insertQuery;
                insertQuery = $@"
                            insert into PcbPrintHist 
                            (Material, LG_Material_CD, BCD_Info, Supplier, ProductionDate, Mfg_Year, Mfg_Month, Mfg_Day, 
                             Mfg_Line, LG_Material_NM, SerialNo, Revision, Label_Size, Reprint, Updated, Updater)
                            SELECT top 1 Material, LG_Material_CD, Bcd_Info, Supplier, ProductionDate, Mfg_Year, Mfg_Month, Mfg_Day,
                            Mfg_Line, LG_Material_NM, SerialNo, Revision, Label_Size, Reprint + 1, GETDATE(), '{WiseApp.Id}' FROM PcbPrintHist with(nolock) WHERE Bcd_Info = '{pcbNo}' ORDER BY Updated desc
                                ";
                DbAccess.Default.ExecuteQuery(insertQuery);
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }

    }
}
