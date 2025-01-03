using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;


namespace WiseM.Browser
{
    class EndWorkOrderConfirm
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

                    if (0 < e.DbAccess.IsExist("WorkOrder", $"WorkOrder = '{workOrder}' AND ActiveStatus = 'Close'"))
                    {
                        MessageBox.Show($"作业指示已经完成。(the order is already closed.)\nWorkOrder : {workOrder}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    
                    stringBuilder.AppendLine
                        (
                         $@"
                        DECLARE @Result INT
                        EXEC @Result = SP_CloseWorkOrder '{workOrder}', '{WiseApp.Id}'
                        SELECT 'Return Value' = @Result
                        "
                        );
                    if (e.DbAccess.ExecuteScalar(stringBuilder.ToString()) is int result)
                    {
                        switch (result)
                        {
                            case -1:
                                MessageBox.Show($"return error.\r\n{workOrder}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            case 0:
                                stringBuilder.Length = 0;
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"return error.", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //저장완료 메시지
                e.AfterRefresh = WeRefreshPanel.Current;
                System.Windows.Forms.MessageBox.Show($@"作业指示已结束。(The work order has been closed.)", "成功(Success)", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{exception.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
