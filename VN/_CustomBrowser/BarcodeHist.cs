using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;
using WiseM.Browser;

namespace WiseM.Browser
{
    public partial class BarcodeHist : Form
    {
        public BarcodeHist()
        {
            InitializeComponent();

            pb_barcode.Load(@"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\2ddatamatrix.JPG");

            string bringBCDID = "Select DISTINCT SUBSTRING(BarcodeNo,1,22) as Barcodeno from barcodehist where status = '1' order by updated desc";
            DataTable BCDID_DT = DbAccess.Default.GetDataTable(bringBCDID);
            for (int i = 0; i < BCDID_DT.Rows.Count; i++)
            {
                cb_BcdID.Items.Add( BCDID_DT.Rows[i]["Barcodeno"].ToString().Substring(0,22) );
            }




        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            string BcdCMd = "^XA"
                + "^LH0,0"
                + "^FO10,10"
                + "^BXN,5,200,20,20"
                + "^FD20020573500EHQ202001010001^FS"
                + @"^FO10,120^zp,18,2^FPH\^FDBATT-PACK,MPP2,LG_HONDA^FS"
                + "^XZ";



                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
               // RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, printtest);







            //System.Windows.Forms.MessageBox.Show(BcdCMd);
            string bringBCD_inf = "Select * from BarcodeHist order by updated desc";
            DataTable Bcd_inf_DT = DbAccess.Default.GetDataTable(bringBCD_inf);








        }

        private void cb_BcdID_SelectedValueChanged(object sender, EventArgs e)
        {
            tb_desc.Text = cb_BcdID.Text;
        }
    }
}
