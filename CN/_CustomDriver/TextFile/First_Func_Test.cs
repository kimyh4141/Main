using System;
using System.Data.SqlClient;
using System.Text;
using WiseM.Data;
using System.IO;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using WiseM.JobControl;

namespace WiseM.Driver
{
    public class First_Func_Test : DriverCustomService
    {
        private string _lastFileName = string.Empty;
        private readonly string _inspectionDatabase = string.Empty;
        public First_Func_Test()
        {
            switch (DbAccess.Default.DataBase)
            {
                case "Y2sCn1Mes3":
                    _inspectionDatabase = "Y2sCn1iData";
                    break;
                case "Y2sVn1Mes3":
                    _inspectionDatabase = "Y2sVn1iData";
                    break;
            }
        }

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
                string[] split_fn = currentFileName.Split('_');

                string strModel = "";
                string pcbId = "";
                string result = "";
                switch (split_fn.Length)
                {
                    case 0:
                        break;
                    case 1:
                        strModel = split_fn[0];
                        break;
                    case 2:
                        strModel = split_fn[0];
                        pcbId = split_fn[1];
                        break;
                    default:
                        strModel = split_fn[0];
                        pcbId = split_fn[1];
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
                queryUpdateKeyRelation.AppendLine($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}', '{e.DriverName}', '{strTested}';");
                if (DbAccess.Default.ExecuteQuery(queryUpdateKeyRelation.ToString()) < 1)
                {
                    //throw new Exception($"Can not update into table KeyRelation.First_Func with PCB_ID:{pcbId}");
                }

                //보험 File.ReadAllBytes(e.FullPath)
                Thread.Sleep(500);
                //

                var queryInsertInspectionData = new StringBuilder();
                var parameter = new SqlParameter("@Byte", SqlDbType.VarBinary, -1)
                                {
                                    Value = File.ReadAllBytes(e.FullPath)
                                };
                queryInsertInspectionData.AppendLine
                    (
                     $@"
                    INSERT
                      INTO Y2sCn1iData.dbo.First_Func_Test ( PcbId
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
                if (DbAccess.Default.ExecuteQuery(queryInsertInspectionData.ToString(), CommandType.Text, false, parameter) < 1)
                {
                    throw new Exception($"Can not insert into table First_Func_Test with PCB_ID:{pcbId}");
                }

                File.Delete(e.FullPath);
                _lastFileName = e.FileName;
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message, e.DriverName);
            }
        }

        private void InsertIntoSysLog(string type, string strMsg, string strWorkCenter)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('{type}',  'DAS', 'First_Func_Test', LEFT(ISNULL(N'{strMsg}',''),3000), '{strWorkCenter}', GETDATE())");
        }

        private void InsertIntoSysLog(string strMsg, string strWorkCenter)
        {
            InsertIntoSysLog("E", strMsg, strWorkCenter);
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
