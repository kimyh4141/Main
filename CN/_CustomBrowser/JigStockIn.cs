using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace WiseM.Browser
{
    class JigStockIn
    {
        public void ProcessStart(CustomPanelLinkEventArgs e)
        {
            string currentJig = e.DataGridView.CurrentRow.Cells["Jig"].Value as string;
            string messageStr = "선택한 Jig 데이터를 입고 처리합니다. Jig Information = " + currentJig + "' ";
            if (DialogResult.Yes == WiseM.MessageBox.Show(messageStr, "StockIn", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DataTable dt = e.DbAccess.GetDataTable("Select * From Jig where Jig = '" + currentJig + "' ");
                if (dt.Rows[0]["LocationBunch"].ToString() == "JL02")
                {
                    WiseM.MessageBox.Show("현재 JIG의 위치는 지그보관실입니다.", "Information", MessageBoxIcon.None);
                    return;
                }
                else
                {
                    string InsertQuery  = " Insert into JigStockHist Values (";
                           InsertQuery += " 'In' , Null, (Select LocationBunch From Jig where Jig ='" + currentJig + "') , '" + currentJig + "', getdate(), '" + WiseApp.Id + "' , 'JL02') ";

                    string UpdateQuery = "Update Jig set LocationBunch = 'JL02' , Location = 'None' where Jig = '" + currentJig + "' ";

                    e.DbAccess.ExecuteQuery(InsertQuery);
                    e.DbAccess.ExecuteQuery(UpdateQuery);

                    WiseM.MessageBox.Show("Success , processing stock in.", "OK", MessageBoxIcon.None);
                }
            }
        }
    }
}
