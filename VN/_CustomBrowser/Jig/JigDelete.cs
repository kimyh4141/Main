using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace WiseM.Browser
{
    class JigDelete
    {
        public void ProcessStart(CustomPanelLinkEventArgs e)
        {
            string currentJig = e.DataGridView.CurrentRow.Cells["Jig"].Value as string;
            string messageStr = "선택한 Jig 데이터를 삭제합니다. Jig Information = " + currentJig + "' ";
            if (DialogResult.Yes == WiseM.MessageBox.Show(messageStr, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DataTable dt = e.DbAccess.GetDataTable("Select * From JigMaintHist where Jig = '" + currentJig + "' ");
                if (dt.Rows.Count > 0)
                {
                    WiseM.MessageBox.Show("입출고, 보수 이력이 존재 함으로 삭제 할 수 없습니다.", "Information", MessageBoxIcon.None);
                    return;
                }
                else
                {
                    string DeleteQuery = "Delete  From Jig where Jig = '" + currentJig + "' ";
                    string DeleteQuery1 = "Delete  From JigInfo where Jig = '" + currentJig + "' ";
                    e.DbAccess.ExecuteQuery(DeleteQuery);
                    e.DbAccess.ExecuteQuery(DeleteQuery1);
                
                    WiseM.MessageBox.Show("데이터 삭제가 완료되었습니다.", "OK", MessageBoxIcon.None);
                }
            }
        }
    }
}
