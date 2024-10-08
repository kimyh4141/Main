using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using WiseM.Data;
using System.Data;
using System.Threading;

namespace WiseM.Driver
{
    public class Voltage_Test : DriverCustomService
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
                string[] split_fn = currentFileName.Split('_');
                string pcbId = "";
                string result = "";
                switch (split_fn.Length)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        pcbId = split_fn[2];
                        break;
                    default:
                        pcbId = split_fn[2];
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
                //System.Windows.Forms.MessageBox.Show($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}', '{e.DriverName}' , '{strTested}';");
                queryUpdateKeyRelation.AppendLine($"EXEC Sp_InspectionProcedure '{GetType().Name}', '{pcbId}', '{result}', '{e.DriverName}' , '{strTested}';");
                if (DbAccess.Default.ExecuteQuery(queryUpdateKeyRelation.ToString()) < 0)
                {
                   // throw new Exception($"Can not update into table KeyRelation.Voltage with PCB_ID:{pcbId}");
                }

                //보험 File.ReadAllBytes(e.FullPath)
                Thread.Sleep(500);
            
                //3번째 줄이 없을경우 오류.. 처리 해야함.
                string E_RawData = File.ReadAllLines(e.FullPath, Encoding.Default)[2];

                string[] arrData = E_RawData.Split(',');
                if (arrData.Length != 17) return;
                string TestDate = arrData.Length > 0 ? arrData[0].Trim().Replace("'", "''") : "";
                string TestTime = arrData.Length > 1 ? arrData[1].Trim().Replace("'", "''") : "";
                string Model = arrData.Length > 2 ? arrData[2].Trim().Replace("'", "''") : "";
                string PcbId = arrData.Length > 3 ? arrData[3].Trim().Replace("'", "''") : "";
                string Ir_Min = arrData.Length > 4 ? arrData[4].Trim().Replace("'", "''") : "";
                string Ir_Max = arrData.Length > 5 ? arrData[5].Trim().Replace("'", "''") : "";
                string Ir_Value = arrData.Length > 6 ? arrData[6].Trim().Replace("'", "''") : "";
                string Ir_Result = arrData.Length > 7 ? arrData[7].Trim().Replace("'", "''") : "";
                string Acw1_Min = arrData.Length > 8 ? arrData[8].Trim().Replace("'", "''") : "";
                string Acw1_Max = arrData.Length > 9 ? arrData[9].Trim().Replace("'", "''") : "";
                string Acw1_Value = arrData.Length > 10 ? arrData[10].Trim().Replace("'", "''") : "";
                string Acw1_Result = arrData.Length > 11 ? arrData[11].Trim().Replace("'", "''") : "";
                string Acw2_Min = arrData.Length > 12 ? arrData[12].Trim().Replace("'", "''") : "";
                string Acw2_Max = arrData.Length > 13 ? arrData[13].Trim().Replace("'", "''") : "";
                string Acw2_Value = arrData.Length > 14 ? arrData[14].Trim().Replace("'", "''") : "";
                string Acw2_Result = arrData.Length > 15 ? arrData[15].Trim().Replace("'", "''") : "";
                string Total_Result = arrData.Length > 16 ? arrData[16].Trim().Replace("'", "''") : "";
                string Source = e.FullPath;

                string TestDateTime = TestDate + " " + TestTime;

                var queryInsertInspectionData = new StringBuilder();
                queryInsertInspectionData.AppendLine
                    (
                     $@"
                    INSERT
                      INTO [Y2sVn1iData].[dbo].[Voltage_Test] ( TestDateTime
                                                              , PcbId
                                                              , Model
                                                              , Ir_Min
                                                              , Ir_Max
                                                              , Ir_Value
                                                              , Ir_Result
                                                              , Acw1_Min
                                                              , Acw1_Max
                                                              , Acw1_Value
                                                              , Acw1_Result
                                                              , Acw2_Min
                                                              , Acw2_Max
                                                              , Acw2_Value
                                                              , Acw2_Result
                                                              , Total_Result
                                                              , WorkCenter
                                                              , Updated
                                                              , Source
                                                              , E_RawData )
                    VALUES ( '{TestDateTime}'
                           , '{PcbId}'
                           , '{Model}'
                           , '{Ir_Min}'
                           , '{Ir_Max}'
                           , '{Ir_Value}'
                           , '{Ir_Result}'
                           , '{Acw1_Min}'
                           , '{Acw1_Max}'
                           , '{Acw1_Value}'
                           , '{Acw1_Result}'
                           , '{Acw2_Min}'
                           , '{Acw2_Max}'
                           , '{Acw2_Value}'
                           , '{Acw2_Result}'
                           , '{Total_Result}'
                           , '{e.DriverName}'
                           , GetDate()
                           , '{e.FullPath}'
                           , '{E_RawData}' )
                                        "
                    );

                if (DbAccess.Default.ExecuteQuery(queryInsertInspectionData.ToString()) < 1)
                {
                    throw new Exception($"Can not insert into table Voltage_Test with PCB_ID:{pcbId}");
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
            DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('{type}',  'DAS', 'Voltage_Test', LEFT(ISNULL(N'{strMsg}',''),3000), '{strWorkCenter}', GETDATE())");
        }

        private void InsertIntoSysLog(string strMsg, string strWorkCenter)
        {
            InsertIntoSysLog("E", strMsg, strWorkCenter);
        }
    }
}
