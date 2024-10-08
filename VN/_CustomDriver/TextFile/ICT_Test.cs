using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WiseM.Data;
using System.Globalization;
using System.IO;
using System.Data;
using System.Threading;

namespace WiseM.Driver
{
    public class ICT_Test : DriverCustomService
    {
        private string _lastFileName = string.Empty;

        protected override void OnTexFileWatched(TextFileWatchEventArgs e)
        {
            e.IsContinue = false;
            BasisInfo(e);
            base.OnTexFileWatched(e);
        }

        protected override void OnEvent(DriverEventArgs e)
        {
            base.OnEvent(e);
        }

        protected override void OnEventOverdue(DriverEventArgs e)
        {
            base.OnEventOverdue(e);
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
                queryUpdateKeyRelation.AppendLine($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}','{e.DriverName}' , '{strTested}';");
                if (DbAccess.Default.ExecuteQuery(queryUpdateKeyRelation.ToString()) < 1)
                {
                    //throw new Exception($"Can not update into table KeyRelation.Second_Func with PCB_ID:{pcbId}");
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
                      INTO Y2sVn1iData.dbo.ICT_Test ( PcbId
                                                    , Result
                                                    , Tested
                                                    , WorkCenter
                                                    , Updated
                                                    , Source
                                                    , E_RawData )
                    VALUES ( '{pcbId}'
                           , '{result}'
                           , '{strTested}'
                           , '{e.DriverName}'
                           , GETDATE()
                           , '{e.FullPath}'
                           , @Byte )
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
                    throw new Exception($"Can not insert into table ICT_Test with PCB_ID:{pcbId}");
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
            //try
            //{
            //    string ICT_Query = string.Empty;
            //    string ICT_KeyRelation = string.Empty;
            //    string Fullpath = e.Source;
            //    string[] split_fp = Fullpath.Split('\\');
            //    string FileName = split_fp[split_fp.Length - 1].Replace(".csv", "");
            //    string[] split_fn = FileName.Split('-');
            //    byte[] e_RawData = File.ReadAllBytes(e.Source);

            //    string PcbId = split_fn[1].Substring(14, 18);
            //    string Result = split_fn[split_fn.Length - 1].Substring(0, 4);
            //    string strTested = split_fn[1].Substring(0, 14);
            //    DateTime Tested = DateTime.ParseExact(strTested, "yyyyMMddHHmmss", CultureInfo.CurrentCulture);

            //    if (_lastFileName.Equals(FileName))
            //        return;

            //    ICT_Query = $"INSERT INTO [Y2sCn1iData].[dbo].[ICT_Test] ";
            //    ICT_Query += $"(PcbId, Result, Tested, WorkCenter, Updated, Source, E_RawData) ";
            //    ICT_Query += $"VALUES ";
            //    ICT_Query += $"('{PcbId}','{Result}','{Tested:yyyy-MM-dd HH:mm:ss}','{e.Workcenter}',GETDATE(),'{e.Source}',@Byte)";

            //    var parameter = new SqlParameter("@Byte", SqlDbType.VarBinary, -1);
            //    parameter.Value = e_RawData;

            //    if (DbAccess.Default.ExecuteQuery(ICT_Query, CommandType.Text, false, parameter) < 1)
            //    {
            //        throw new Exception($"Can not insert into table ICT_Test with PCB_ID:{PcbId}");
            //    }

            //    ICT_KeyRelation = $"UPDATE KeyRelation " +
            //                        $"SET    ICT_Tested = '{Tested:yyyy-MM-dd HH:mm:ss}', ICT_Result = '{Result}' " +
            //                        $"WHERE  PcbBcd = '{PcbId}'";

            //    if (DbAccess.Default.ExecuteQuery(ICT_KeyRelation) < 1)
            //    {
            //        throw new Exception($"Can not update table KeyRelation with PCB_ID:{PcbId}");
            //    }

            //    _lastFileName = FileName;
            //}
            //catch (Exception ex)
            //{
            //    this.InsertIntoSysLog(ex.Message, e.Workcenter);
            //}
        }

        private void InsertIntoSysLog(string strMsg, string strWorkCenter)
        {
            strMsg = strMsg.Replace("'", "''");
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'DAS', 'ICT_Test', LEFT(ISNULL(N'{strMsg}',''),3000), '{strWorkCenter}', GETDATE())");
        }
    }
}
