using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;


namespace WiseM.Browser
{
    internal class EndWorkOrderCancelConfirm
    {
        public void ProcessStart(CustomPanelLinkEventArgs e)
        {
            try
            {
                var selectedRowCollection = e.DataGridView.SelectedRows;
                var stringBuilder = new StringBuilder();
                if (selectedRowCollection.Count <= 0)
                {
                    MessageBox.Show("请选择一行。(Please select a row.)", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn không？(Are you sure?)", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) != DialogResult.Yes) return;
                foreach (DataGridViewRow dataGridViewRow in selectedRowCollection)
                {
                    string workOrder = $"{dataGridViewRow.Cells["WorkOrder"].Value}";
                    if (0 < e.DbAccess.IsExist("ActiveJob", $"WorkOrder = '{workOrder}'"))
                    {
                        MessageBox.Show($"这是一个在制品订单。(This is a work in progress order.)\nWorkOrder : {workOrder}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    if (0 < e.DbAccess.IsExist("WorkOrder", $"WorkOrder = '{workOrder}' AND 'Seq' = dbo.WorkCenterKind(WorkCenter)"))
                    {
                        MessageBox.Show($"'Seq' line cannot be Canceled.\nWorkOrder : {workOrder}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }

                    stringBuilder.AppendLine
                        (
                         $@"
                        UPDATE WorkOrder
                           SET Closed            = NULL
                             , ActiveStatus      = 'Release'
                             , BeginActiveStatus = 'Close'
                             , Updater           = '{WiseApp.Id}'
                         WHERE WorkOrder = '{workOrder}'
                        ;
                        "
                        );
                }
                e.DbAccess.ExecuteQuery(stringBuilder.ToString());
                //저장완료 메시지
                e.AfterRefresh = WeRefreshPanel.Current;
                System.Windows.Forms.MessageBox.Show($@"Chỉ thị thao tác này đã đóng。(The work order has been unclosed.)", "Thành công(Success)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{exception.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}