using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace WiseM.Browser
{
    class DeleteWorkOrderConfirm
    {
        public void ProcessStart(CustomPanelLinkEventArgs e)
        {
            object WorkorderClosed = e.DataGridView.CurrentRow.Cells["WorkOrder"].Value;
            string messagstr =  "this Delete WorkOrder? ";
            if (MessageBox.Show(messagstr, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
            if (e.DataGridView.CurrentRow.Cells["ActiveStatus"].Value.ToString() == "Active")
            {
                WiseM.MessageBox.Show("The WorkOrder is in Progress ", "Warning", MessageBoxIcon.None);
            }

            else
            {
                //string Query = "Delete from Workorder where workorder = '" + WorkorderClosed.ToString() + "' ";
                //result = e.DbAccess.ExecuteQuery(Query);

                StringBuilder query = new StringBuilder();

                query.Append("\r\n DELETE FROM WorkOrder ");
                query.Append("\r\n WHERE WorkOrder = '" + WorkorderClosed.ToString() + "'");

                WiseM.Data.DbAccess.Default.ExecuteQuery(query.ToString());

                WiseM.MessageBox.Show("this Workorder data Delete . \r\n Please Refresh Data.", "Warning", MessageBoxIcon.None);
            }
        }
    }
}
