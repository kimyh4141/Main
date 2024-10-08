using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class JigSpec : UserControl
    {
        DataTable DataSource = null;

        public List<JigSpecItem> Items = null;

        bool DelMode = false;

        public JigSpec()
        {
            InitializeComponent();
            
            this.Items = new List<JigSpecItem>();
        }

        public void AddItems(DataTable dtSource)
        {
            this.DataSource = dtSource;

            if (this.DataSource != null && this.DataSource.Rows.Count > 0)
            {
                foreach (DataRow row in this.DataSource.Rows)
                {
                    this.AddSpec(row["Spec"].ToString(), row["MinValue"].ToString(), row["MaxValue"].ToString());
                }
            }
        }

        public void ResetValue()
        {
            if (this.Items != null)
            {
                foreach(JigSpecItem item in this.Items)
                {
                    item.MinValue = "0";
                    item.MaxValue = "0";
                }
            }
        }
        
        private void btn_Add_Click(object sender, EventArgs e)
        {
            this.AddSpec((this.Items.Count + 1).ToString());
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            DelMode = !DelMode;

            foreach (JigSpecItem item in this.Items)
            {
                DataRow tempRow = null;

                if (this.DataSource != null && this.DataSource.Rows.Count > 0)
                    tempRow = this.DataSource.Select(string.Format("Spec = '{0}'", item.Spec)).FirstOrDefault();

                if (tempRow == null)
                {
                    item.DelMode = DelMode;
                }
            }
        }

        private void AddSpec(string spec, string minValue = "0", string maxValue = "0")
        {
            JigSpecItem specItem = new JigSpecItem(spec, minValue, maxValue);

            this.Items.Add(specItem);

            this.panel_Body.Controls.Add(specItem);
            specItem.Dock = DockStyle.Top;
            specItem.BringToFront();

            specItem.DelMode = this.DelMode;

            specItem.DeleteButtonClicked += SpecItem_DeleteButtonClicked;
        }

        private void SpecItem_DeleteButtonClicked(object sender)
        {
            JigSpecItem item = (JigSpecItem)sender;

            if (item != null)
            {
                int itemIndex = Convert.ToInt32(item.Spec) - 1;

                this.panel_Body.Controls.Remove(this.Items[itemIndex]);

                this.Items.RemoveAt(itemIndex);

                for (int i = 0; i < this.Items.Count; i++)
                {
                    this.Items[i].Spec = (i + 1).ToString();
                }
            }
        }

        private void panel_Body_Paint(object sender, PaintEventArgs e)
        {
            int rightPadding = 0;

            if (this.panel_Body.VerticalScroll.Visible == true)
                rightPadding = 20;

            this.panel_Top.Padding = new Padding(0, 0, rightPadding, 0);
        }
    }
}
