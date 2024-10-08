
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
    public partial class PI_frmMain00 : SkinForm
    {
        public static string strDbName = "";
        //public static string strDbName = "[Y2sCn1Mes3_Test].[dbo].";
        public PI_frmMain00()
        {
            InitializeComponent();
        }

        private void PI_frmMain00_Load(object sender, EventArgs e)
        {
            if (PI_frmMain00.strDbName.Length > 0)
            {
                MessageBox.Show("This is TEST PROGRAM.\nDo NOT use this program.\n\n" +
                                "这是测试程序。\n不要使用这个程序。", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Hide();
            PI_frmMain10 _form1 = new PI_frmMain10(WiseM.WiseApp.Id);
            _form1.ShowDialog();
            this.Close();

        }
    }
}