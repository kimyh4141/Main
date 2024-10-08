
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
using System.Data.OleDb;


namespace WiseM.Browser
{
    public partial class Outsourcing_Receipt_frmMain00 : SkinForm
    {
        public static string strDbName = "";
        //public static string strDbName = "[Y2sCn1Mes3_Test].[dbo].";
        public Outsourcing_Receipt_frmMain00()
        {
            InitializeComponent();
        }

        private void Outsourcing_Receipt_frmMain00_Load(object sender, EventArgs e)
        {
            if (Outsourcing_Receipt_frmMain00.strDbName.Length > 0)
            {
                MessageBox.Show("This is TEST PROGRAM.\nDo NOT use this program.\n\n" +
                                "这是测试程序。\n不要使用这个程序。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Hide();
            Outsourcing_Receipt_frmMain01 _form1 = new Outsourcing_Receipt_frmMain01(WiseM.WiseApp.Id);
            _form1.ShowDialog();
            this.Close();

        }
    }
}