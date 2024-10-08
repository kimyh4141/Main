using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    class RepairStart
    {
        public void ProcessStart(string JigCode)
        {
            string currentJig = JigCode;
            string messageStr = "선택한 Jig의 보전을 시작하시겠습니까? \r\n Do you want to start Maint the Selected Jig? \r\n . Jig Information = " + currentJig + "' ";
            if (DialogResult.Yes == WiseM.MessageBox.Show(messageStr, "Start", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DataTable dt = DbAccess.Default.GetDataTable("Select * From JigMaintHist where Jig = '" + currentJig + "' and ConfirmDate is null ");
                if (dt.Rows.Count > 0)
                {
                    WiseM.MessageBox.Show("해당 지그는 현재 '보전정보등록대기'/'Confirm대기' 상태입니다. \r\n This Jig Status is [Waiting Confirm]'", "Information", MessageBoxIcon.None);
                    return;
                }
                else
                {
                    string insertQuery = " Insert Into JigMaintHist Values ('" + currentJig + "' ,Getdate() , Null,'" + WiseApp.Id + "' ,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,(Select NextMaintDate From Jiginfo where Jig ='" + currentJig + "') ,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null )";
                    DbAccess.Default.ExecuteQuery(insertQuery);
                    string updateQuery = "Update Jiginfo  Set LastMaintHist = (Select JigMaintHist From JigMaintHist where Jig = '" + currentJig + "' and confirmDate is null) , LastMaintStarted = Getdate() , TotalMaintCount = (Select Count(*) From JigMaintHist where Jig = '" + currentJig + "')   where Jig = '" + currentJig + "' ";
                    DbAccess.Default.ExecuteQuery(updateQuery);


                    WiseM.MessageBox.Show("Success!", "OK", MessageBoxIcon.None);
                }
            }
        }
    }
}
