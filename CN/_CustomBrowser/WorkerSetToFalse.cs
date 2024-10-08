using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace WiseM.Browser
{
    class WorkerSetToFalse
    {
        public void ProcessStart(CustomPanelLinkEventArgs e)
        {
            string currentWorker = e.DataGridView.CurrentRow.Cells["Worker"].Value as string;
            string currentWorkerName = e.DataGridView.CurrentRow.Cells["WorkerName"].Value as string;
            string messageStr = "If you want to set false " + currentWorkerName + "'s Status";
            if (DialogResult.Yes == MessageBox.Show(messageStr, "Set to False", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string updateQuery = "Update Worker Set Status = 0 Where Worker = '" + currentWorker + "'";

                if (e.DbAccess.ExecuteQuery(updateQuery) == 1)
                    MessageBox.Show("Status change is over.\r\nPlease Refresh Data.", "Error", MessageBoxIcon.Error);
            }
        }
    }
}
