using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using clsBarcode;

namespace WiseM.Browser.WMS
{
    public partial class NewBarcodeTestPrint : Form
    {

        private clsBarcode.clsBarcode cBarcode;

        public NewBarcodeTestPrint()
        {
            InitializeComponent();
        }

        private void button_PrintBox_Click(object sender, EventArgs e)
        {
            cBarcode = new clsBarcode.clsBarcode();
            cBarcode.LoadFromXml(DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_RmBox'").ToString());
            cBarcode.Data.SetText("BARCODE1", $"9X9X9X9X9X999999{DateTime.Today:yyyyMMdd}1234567123");
            cBarcode.Data.SetText("RAWMATERIAL", $"9X9X9X9X9X");
            cBarcode.Data.SetText("RAWMATERIALNAME", $"TEST Material");
            cBarcode.Data.SetText("SPEC", $"TEST Spec");
            cBarcode.Data.SetText("SUPPLIER", $"999999");
            cBarcode.Data.SetText("SUPPLIERNAME", $"TEST Supplier");
            cBarcode.Data.SetText("PRODUCTDATE", $"{DateTime.Today:yyyy-MM-dd}");
            cBarcode.Data.SetText("QTY", $"1234567");
            cBarcode.Data.SetText("SEQ", $"123");
            cBarcode.Data.SetText("LEVEL", "");
            cBarcode.Print(false);
        }

        private void button_PrintReel_Click(object sender, EventArgs e)
        {
            cBarcode = new clsBarcode.clsBarcode();
            cBarcode.LoadFromXml(DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_RmReel'").ToString());
            cBarcode.Data.SetText("BARCODE1", $"9X9X9X9X9X999999{DateTime.Today:yyyyMMdd}1234567123");
            cBarcode.Data.SetText("RAWMATERIAL", $"9X9X9X9X9X");
            cBarcode.Data.SetText("RAWMATERIALNAME", $"TEST Material");
            cBarcode.Data.SetText("SPEC", $"TEST Spec");
            cBarcode.Data.SetText("SUPPLIER", $"999999");
            cBarcode.Data.SetText("SUPPLIERNAME", $"TEST Supplier");
            cBarcode.Data.SetText("PRODUCTDATE", $"{DateTime.Today:yyyy-MM-dd}");
            cBarcode.Data.SetText("QTY", $"1234567");
            cBarcode.Data.SetText("SEQ", $"123");
            cBarcode.Data.SetText("LEVEL", "");
            cBarcode.Print(false);
        }

        private void button_PrintMSL_Click(object sender, EventArgs e)
        {
            cBarcode = new clsBarcode.clsBarcode();
            cBarcode.LoadFromXml(DbAccess.Default.ExecuteScalar($"SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_RmReel'").ToString());
            cBarcode.Data.SetText("BARCODE1", $"9X9X9X9X9X999999{DateTime.Today:yyyyMMdd}1234567123");
            cBarcode.Data.SetText("RAWMATERIAL", $"9X9X9X9X9X");
            cBarcode.Data.SetText("RAWMATERIALNAME", $"TEST Material");
            cBarcode.Data.SetText("SPEC", $"TEST Spec");
            cBarcode.Data.SetText("SUPPLIER", $"999999");
            cBarcode.Data.SetText("SUPPLIERNAME", $"TEST Supplier");
            cBarcode.Data.SetText("PRODUCTDATE", $"{DateTime.Today:yyyy-MM-dd}");
            cBarcode.Data.SetText("QTY", $"1234567");
            cBarcode.Data.SetText("SEQ", $"123");
            cBarcode.Data.SetText("LEVEL", "9");
            cBarcode.Print(false);
        }
    }
}
