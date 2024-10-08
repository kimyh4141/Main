using System;
using System.Data.SqlClient;
using System.Text;
using WiseM.Data;
using System.IO;
using System.Data;
using System.Threading;

namespace WiseM.Driver
{
    public class Second_Func_Test : DriverCustomService
    {
        private string _lastFileName = string.Empty;

        protected override void OnTexFileWatched(TextFileWatchEventArgs e)
        {
            e.IsContinue = false;
            BasisInfo(e);
            base.OnTexFileWatched(e);
        }

        private void BasisInfo(TextFileWatchEventArgs e)
        {
            if (_lastFileName == e.FileName) return;
            try
            {
                string currentFileName = e.FileName;
                string[] splitFileName = currentFileName.Split('_');

                string strModel = "";
                string pcbId = "";
                string result = "";
                switch (splitFileName.Length)
                {
                    case 0:
                        break;
                    case 1:
                        strModel = splitFileName[0];
                        break;
                    case 2:
                        strModel = splitFileName[0];
                        pcbId = splitFileName[1];
                        break;
                    default:
                        strModel = splitFileName[0];
                        pcbId = splitFileName[1];
                        break;
                }

                if (pcbId.Length != 17)
                {
                    File.Delete(e.FullPath);
                    return;
                }

                if (currentFileName.Contains("PASS")) result = "PASS";
                if (currentFileName.Contains("FAIL")) result = "FAIL";

                var strTested = File.GetCreationTime(e.FullPath).ToString("yyyy-MM-dd HH:mm:ss");
                var queryUpdateKeyRelation = new StringBuilder();
                //System.Windows.Forms.MessageBox.Show($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}','{e.DriverName}' , '{strTested}';");
                queryUpdateKeyRelation.AppendLine($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}','{e.DriverName}' , '{strTested}';");
                if (DbAccess.Default.ExecuteQuery(queryUpdateKeyRelation.ToString()) < 1)
                {
                    //throw new Exception($"Can not update into table KeyRelation.Second_Func with PCB_ID:{pcbId}");
                }

                //보험 File.ReadAllBytes(e.FullPath)
                Thread.Sleep(500);
            

                var queryInsertInspectionData = new StringBuilder();
                var parameter = new SqlParameter("@Byte", SqlDbType.VarBinary, -1)
                                {
                                    Value = File.ReadAllBytes(e.FullPath)
                                };
                queryInsertInspectionData.AppendLine
                    (
                     $@"
                    INSERT
                      INTO Y2sVn1iData.dbo.Second_Func_Test ( PcbId
                                                           , Tested
                                                           , Model
                                                           , Result
                                                           , WorkCenter
                                                           , Updated
                                                           , Source
                                                           , E_RawData
                                                           )
                    VALUES ( '{pcbId}'
                           , '{strTested}'
                           , '{strModel}'
                           , '{result}'
                           , '{e.DriverName}'
                           , GetDate() 
                           , '{e.FullPath}'
                           , @Byte
                           )
                    ;
                    "
                    );
                if (DbAccess.Default.ExecuteQuery
                        (
                         queryInsertInspectionData.ToString(), CommandType.Text, false,
                         parameter
                        )
                    < 1)
                {
                    throw new Exception($"Can not insert into table Second_Func_Test with PCB_ID:{pcbId}");
                }

                File.Delete(e.FullPath);
                _lastFileName = e.FileName;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message, e.DriverName);
            }
            finally
            {
                e.IsContinue = false;
            }
        }

        private void InsertIntoSysLog(string strMsg, string strWorkCenter) => InsertIntoSysLog("E", strMsg, strWorkCenter);

        private void InsertIntoSysLog(string type, string strMsg, string strWorkCenter)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery
                (
                 $"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('{type}',  'DAS', 'Second_Func_Test', LEFT(ISNULL(N'{strMsg}',''),3000), '{strWorkCenter}', GETDATE())"
                );
        }

        protected override void OnEvent(DriverEventArgs e)
        {
            base.OnEvent(e);
        }

        protected override void OnEventOverdue(DriverEventArgs e)
        {
            base.OnEventOverdue(e);
        }
    }
}
