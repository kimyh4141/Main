using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace WiseM.Browser
{
    class ToCSV
    {
        // try
        //            {
        //                StreamWriter sw = new StreamWriter(
        //           Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\" + e.Program + ".CSV",
        //           true, System.Text.Encoding.Default);
        //ToCSV.WriteToStream(sw, (e.DataGridView.DataSource as DataTable), true, false);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Stop, null);
        //            }



        //StreamWriter sw = new StreamWriter("CSV.csv");
        // WriteToStream(sw, ds.Tables[0], false, false);

        //WriteToStream
        //세번째 TRUE 면 헤더 표시
        //네번째 TRUE 면 " " 표시


        public static void WriteToStream(TextWriter stream, DataTable table, bool header, bool quoteall)
        {
            if (header)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, table.Columns[i].Caption, quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write("\r\n");
                }
            }
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    WriteItem(stream, row[i], quoteall);
                    if (i < table.Columns.Count - 1)
                        stream.Write(',');
                    else
                        stream.Write("\r\n");
                }
            }
            stream.Flush();
            stream.Close();
        }

        private static void WriteItem(TextWriter stream, object item, bool quoteall)
        {
            if (item == null)
                return;
            string s = item.ToString();
            if (quoteall || s.IndexOfAny("\",\x0A\x0D".ToCharArray()) > -1)
                stream.Write("\"" + s.Replace("\"", "\"\"") + "\"");
            else
                stream.Write(s);
            stream.Flush();
        }


    }
}
