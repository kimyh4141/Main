using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class WipInfo : SkinForm
    {
        private CustomPanelLinkEventArgs e = null;

        public WipInfo(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;

            InitializeField();
        }

        private void InitializeField()
        {
            this.tbAcmWip.Text = string.Empty;
            this.tbAplWip.Text = string.Empty;
            this.tbCstWip.Text = string.Empty;
            this.tbCwdWip.Text = string.Empty;
            this.tbCpkWip.Text = string.Empty;
            this.tbChpWip.Text = string.Empty;
            this.tbLastUpdated.Text = string.Empty;

            string sQuery = "Select * From WipInfo Order by ViewSeq ";
            DataTable dt = DbAccess.Default.GetDataTable(sQuery);

            if (dt.Rows.Count > 0)
            {
                this.tbAcmWip.Text = dt.Rows[0]["WipBase"].ToString();
                this.tbAplWip.Text = dt.Rows[1]["WipBase"].ToString();
                this.tbCstWip.Text = dt.Rows[2]["WipBase"].ToString();
                this.tbCwdWip.Text = dt.Rows[3]["WipBase"].ToString();
                this.tbCpkWip.Text = dt.Rows[4]["WipBase"].ToString();
                this.tbChpWip.Text = dt.Rows[5]["WipBase"].ToString();
                this.tbLastUpdated.Text = dt.Rows[0]["Updated"].ToString();

                this.tbAcmWip.Focus();
            }
            else
            {
                MessageBox.Show("Error! There is a error occurred.", "Error", MessageBoxIcon.Error);
                return;
            }
        }

        // Insert & Cancel Botton Process
        private void btnInsert_Click(object sender, EventArgs e)
        {
            string uQuery = "";

            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbAcmWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'ACM' ";
            int ret1 = DbAccess.Default.ExecuteQuery(uQuery);
            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbAplWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'APL' ";
            int ret2 = DbAccess.Default.ExecuteQuery(uQuery);
            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbCstWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'CST' ";
            int ret3 = DbAccess.Default.ExecuteQuery(uQuery);
            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbCwdWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'CWD' ";
            int ret4 = DbAccess.Default.ExecuteQuery(uQuery);
            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbCpkWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'CPK' ";
            int ret5 = DbAccess.Default.ExecuteQuery(uQuery);
            uQuery = "Update WipInfo Set WipBase = " + Convert.ToInt16(this.tbChpWip.Text) + ", WipBaseDate = '" + this.tbLastUpdated.Text + "', Updated = Getdate() Where Routing = 'CHP' ";
            int ret6 = DbAccess.Default.ExecuteQuery(uQuery);

            if (ret1 == 1 && ret2 == 1 && ret3 == 1 && ret4 == 1 && ret5 == 1 && ret6 == 1) 
                MessageBox.Show("WIP information is registered successfully!!", "Information", MessageBoxIcon.Information);
            else
                MessageBox.Show("Error! There are some error on registering WIP Information. ", "Error", MessageBoxIcon.Error);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
