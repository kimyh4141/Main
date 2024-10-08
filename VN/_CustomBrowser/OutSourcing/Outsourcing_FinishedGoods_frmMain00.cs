
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
    public partial class Outsourcing_FinishedGoods_frmMain00 : SkinForm
    {
        public Outsourcing_FinishedGoods_frmMain00()
        {
            InitializeComponent();
        }

        private void Outsourcing_FinishedGoods_frmMain00_Load(object sender, EventArgs e)
        {
            this.Hide();
            Outsourcing_FinishedGoods_frmMain01 _form1 = new Outsourcing_FinishedGoods_frmMain01(WiseM.WiseApp.Id);
            _form1.ShowDialog();
            this.Close();

        }
    }
}