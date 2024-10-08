using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;

namespace WiseM.Browser
{
    internal class PropertyItemList
    {
        internal static string[] _divisionItems;
        internal static string[] _workcenterItems;
        internal static string[] _materialItems;
        internal static string[] _customerItems;
        internal static string[] _routingItems;
        internal static string[] _shiftItems;
        internal static string[] _issuetypeItems;
        internal static string[] _badItems;
        internal static string[] _notworkItems;
        internal static string[] _repairItems;
        internal static string[] _workteamItems;
        internal static string[] _dayofweekItems;
        internal static string[] _bunchworkcenterItems;
        internal static string[] _workerItems;
        internal static string[] _clientIDItems;
        internal static string[] _badBunchItems;
        internal static string[] _notworkBunchItems;
        internal static string[] _BunchItems;
        internal static string[] _KindItems;
        internal static string[] _PlateItems;
        internal static string[] _PlateAItems;
        internal static string[] _PlateBItems;
        internal static string[] _PlateCItems;
        internal static string[] _PlateDItems;
        internal static string[] _PlateEItems;
        internal static string[] _Plate_CovItems;
        internal static string[] _Top_CaseItems;
        internal static string[] _Btm_CaseItems;
        internal static string[] _Cpk_LabelItems;
        internal static string[] _notworkGroupItems;
        internal static string[] _LocationGroup;

        //CheckSheet
        internal static string[] _csline;
        internal static string[] _checkPeriod;
        internal static string[] _cscode;
        internal static string[] _datatype;

    }

    public class MyConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            switch (context.PropertyDescriptor.Name)
            {
                case "Division":
                    return new StandardValuesCollection(PropertyItemList._divisionItems);
                case "Workcenter":
                    return new StandardValuesCollection(PropertyItemList._workcenterItems);
                case "Material":
                    return new StandardValuesCollection(PropertyItemList._materialItems);
                case "Customer":
                    return new StandardValuesCollection(PropertyItemList._customerItems);
                case "Routing":
                    return new StandardValuesCollection(PropertyItemList._routingItems);
                case "Shift":
                    return new StandardValuesCollection(PropertyItemList._shiftItems);
                case "IssueType":
                    return new StandardValuesCollection(PropertyItemList._issuetypeItems);
                case "Bad":
                    return new StandardValuesCollection(PropertyItemList._badItems);
                case "Notwork":
                    return new StandardValuesCollection(PropertyItemList._notworkItems);
                case "NotworkGroup":
                    return new StandardValuesCollection(PropertyItemList._notworkGroupItems);
                case "Repair":
                    return new StandardValuesCollection(PropertyItemList._repairItems);
                case "WorkTeam":
                    return new StandardValuesCollection(PropertyItemList._workteamItems);
                case "DayOfWeek":
                    return new StandardValuesCollection(PropertyItemList._dayofweekItems);
                case "BunchWorkcenter":
                    return new StandardValuesCollection(PropertyItemList._bunchworkcenterItems);
                case "Parent":
                    return new StandardValuesCollection(PropertyItemList._materialItems);
                case "Child":
                    return new StandardValuesCollection(PropertyItemList._materialItems);
                case "Worker":
                    return new StandardValuesCollection(PropertyItemList._workerItems);
                case "ClientID":
                    return new StandardValuesCollection(PropertyItemList._clientIDItems);
                case "BadBunch":
                    return new StandardValuesCollection(PropertyItemList._badBunchItems);
                case "NotworkBunch":
                    return new StandardValuesCollection(PropertyItemList._notworkBunchItems);
                case "Bunch":
                    return new StandardValuesCollection(PropertyItemList._BunchItems);
                case "Kind":
                    return new StandardValuesCollection(PropertyItemList._KindItems);
                case "Plate":
                    return new StandardValuesCollection(PropertyItemList._PlateItems);
                case "PlateA":
                    return new StandardValuesCollection(PropertyItemList._PlateAItems);
                case "PlateB":
                    return new StandardValuesCollection(PropertyItemList._PlateBItems);
                case "PlateC":
                    return new StandardValuesCollection(PropertyItemList._PlateCItems);
                case "PlateD":
                    return new StandardValuesCollection(PropertyItemList._PlateDItems);
                case "PlateE":
                    return new StandardValuesCollection(PropertyItemList._PlateEItems);
                case "Plate_Cov":
                    return new StandardValuesCollection(PropertyItemList._Plate_CovItems);
                case "Top_Case":
                    return new StandardValuesCollection(PropertyItemList._Top_CaseItems);
                case "Btm_Case":
                    return new StandardValuesCollection(PropertyItemList._Btm_CaseItems);
                case "Cpk_Label":
                    return new StandardValuesCollection(PropertyItemList._Cpk_LabelItems);
                case "LocationGroup":
                    return new StandardValuesCollection(PropertyItemList._LocationGroup);

                //CheckSheet
                case "LIne":
                    return new StandardValuesCollection(PropertyItemList._csline);
                case "CheckPeriod":
                    return new StandardValuesCollection(PropertyItemList._checkPeriod);
                case "CsCode":
                    return new StandardValuesCollection(PropertyItemList._cscode);
                case "DataType":
                    return new StandardValuesCollection(PropertyItemList._datatype);


                default:
                    return base.GetStandardValues(context);
            }
        }
    }

    class BasicCodeInfo
    {
        public BasicCodeInfo()
        {  
        }

        public static bool CheckWorkOrder(string workorder, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From WorkOrder Where WorkOrder = '" + workorder + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return false;
            else
                return true;
        }

        public static bool CheckWorkcenterInWorkOrder(string workcenter, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From WorkOrder Where Workcenter = '" + workcenter + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return false;
            else
                return true;
        }

        public static bool CheckWorkcenterInWorkcalendar(string workcenter, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * from Workcalendar Where Workcenter = '" + workcenter + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return false;
            else
                return true;
        }

        public static bool CheckNotwork(string notwork, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Notwork Where Notwork = '" + notwork + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckWorkTeam(string workteam, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From WorkTeam Where WorkTeam = '" + workteam + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckFunctionError(string Model, string ErrorCode ,CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From FunctionChecked Where FuncError = '" + ErrorCode + "' and  Model = '" + Model + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }


        public static bool CheckWorkTeamInWorkCalendarStd(string workteam, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From WorkCalendarStd Where WorkTeam = '" + workteam + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckMaterialRouting(string material, string routing, string issueseq, CustomProgramLinkEventArgs e)
        {
             string searchQuery = "Select top 1 * From MaterialRouting Where Material = '" + material + "' And Routing = '" 
                 + routing + "' And  IssueSeq = '" + issueseq + "'";
           
                return true;
        }

        public static bool CheckMaterialMapping(string material, CustomProgramLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From MaterialMapping Where Material = '" + material + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckPKWorkCalendarStd(string dayofweek, string workteam, string workcenter, CustomProgramLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From WorkCalendarStd Where WorkTeam = '" + workteam + "' And DayOfWeek = '" + dayofweek + "'"
                + " And Workcenter = '" + workcenter + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckNotworkInNotworkHist(string notwork, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From NotworkHist Where Notwork = '" + notwork + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckWorkcenter(string workcenter, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Workcenter Where Workcenter = '" + workcenter + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckBad(string bad, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Bad Where Bad = '" + bad + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckRawMaterial(string RawMaterial, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From RawMaterial Where RawMaterial = '" + RawMaterial + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckLocationGroup(string LocationGroup, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Rm_Location_Group Where Location_Group = '" + LocationGroup + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckLocation(string Location, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From RM_Location Where Location = '" + Location + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckRepair(string Repair, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Repair Where Repair = '" + Repair + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckWorker(string worker, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Worker Where Worker = '" + worker + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckWorkerInWorkerOutput(string worker, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From WorkerOutput Where Worker = '" + worker + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckBadInBadHist(string bad, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From BadHist Where Bad = '" + bad + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckBadInRepairHist(string Repair, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From RepairHist Where Repair = '" + Repair + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckMaterial(string material, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Material Where Material = '" + material + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckMaterialInOutputHist(string material, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From OutputHist Where Material = '" + material + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckRoutingInActiveJob(string routing, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From ActiveJob Where Routing = '" + routing + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckRouting(string routing, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Routing Where Routing = '" + routing + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckDivisionInActiveJob(string division, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select top 1 * From ActiveJob Where Division = '" + division + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckDivision(string division, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From Division Where Division = '" + division + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckFCTSpec(string MaterialCode, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From ElentecINMes3_PEPB_iData.dbo.FCT_Spec Where MaterialCode = '" + MaterialCode + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }

        public static bool CheckClientWorker(string clientID, string worker, CustomPanelLinkEventArgs e)
        {
            string searchQuery = "Select * From ClientWorker Where ClientID = '" + clientID + "' And Worker = '" + worker + "'";
            if (e.DbAccess.GetDataTable(searchQuery).Rows.Count <= 0)
                return true;
            else
                return false;
        }
        public static DataTable BasicFCTSpec(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select * From ElentecINMes3_PEPB_iData.dbo.FCT_Spec";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicDivision(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Division,Text From Division Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicBunchWorkcenter(CustomProgramLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Common,Text from Common Where category = '180' order by viewseq";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicWorkcenter(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Workcenter,Text From Workcenter Where WorkStatus = 1 Order By ViewSeq";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        //공정구분
        public static DataTable BasicBunch(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Common where Category = '400' order by ViewSeq desc";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        //제조형태 (제조사 , 원자재 구분)
        public static DataTable BasicKind(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Common where Category = '999' order by ViewSeq desc";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicRawMaterialBunch(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Common , Text from Common where Category = '401' order by ViewSeq desc";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicRawMaterialLocationGroup(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Location_Group , Location_Group_Text from Rm_Location_Group";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        //요기부터
        public static DataTable BasicPlate(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='Plate'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlateA(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='PlateA'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlateB(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='PlateB'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlateC(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='PlateC'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlateD(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='PlateD'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlateE(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='PlateE'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicPlate_Cov(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='Plate_Cov'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicTop_Case(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='Top_Case'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicBtm_Case(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='Btm_Case'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCpk_Label(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select * from Material where Bunch = 'Raw_Material' And Kind ='Cpk_Label'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicWorkcenterByWorkGroup(CustomPanelLinkEventArgs e, string workgroup)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Workcenter,Text from Workcenter Where WorkStatus = 1 And  Bunch = '" + workgroup + "' order by viewseq ";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicWorkGroup(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Common,Text from common where category = '180' order by viewseq";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicMaterial(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Material,Text As MaterialName From Material Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCustomer(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Customer,Text As CustomerName From Customer Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicRouting(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Routing,Text From Routing Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicShift()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {  new DataColumn("Shift"), 
                                                    new DataColumn("Time")
                                                   }
                                 );
            dt.Rows.Add("A", "08:00 ~ 16:00");
            dt.Rows.Add("B", "16:00 ~ 24:00");
            dt.Rows.Add("C", "00:00 ~ 08:00");
            return dt;
        }

        public static DataTable BasicIssueType()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {  new DataColumn("IssueType") 
                                                   }
                                 );
            dt.Rows.Add("Driver");
            dt.Rows.Add("Worker");
            return dt;
        }

        public static DataTable BasicDayOfWeekStd()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {  new DataColumn("DayOfWeek") 
                                                   }
                                 );
            dt.Rows.Add("WeekDay");
            dt.Rows.Add("Saturday");
            dt.Rows.Add("Sunday");
            return dt;
        }

        public static DataTable BasicWorkTeam(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select WorkTeam,Text From WorkTeam Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicBad(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Bad,Text From Bad Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicRepair(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Repair,Text From Repair Where Status = 1";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicNotwork(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Notwork,Text From Notwork Where Status = 1"; 
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicNotworkGroup(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Common,Text From Common Where Category = '210'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicWorker(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Worker,Text From Worker";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicClientID(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Common As ClientID,Text From Common Where Category = '300'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicBadBunch(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Common As BadBunch,Text From Common Where Category = '200' ";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicNotworkBunch(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Common As NotworkBunch,Text From Common Where Category = '210' ";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicBadbyBunch(CustomPanelLinkEventArgs e, string bunch)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Bad,Text From Bad Where Status = 1 And Bunch = '" + bunch + "'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicNotworkbyBunch(CustomPanelLinkEventArgs e, string bunch)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Notwork,Text From Notwork Where Status = 1 And Bunch = '" + bunch + "'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicNotworkBunch(CustomPanelLinkEventArgs e,string bunch)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Notwork,Text From Notwork Where Status = 1 And Bunch = '" + bunch + "'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        #region CheckSheet Check


        //CheckSheet
        public static bool CheckCSLineRoute(string Line, string Route, CustomPanelLinkEventArgs e)
        {
            string SearchQuery = " select * from CsLineRoute where Line = '" + Line.Split('/')[0].Trim() + "'and Route = '" + Route + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSDailySpec(string Line, string Seq, string Route, CustomPanelLinkEventArgs e)//일일장비점검
        {
            string SearchQuery = " select * from CsDailySpec where Line = '" + Line.Split('/')[0].Trim() + "'and Seq = '" + Seq + "'and Route = '" + Route + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSPeriodicSpec(string Line, string Seq, string Route, CustomPanelLinkEventArgs e)//정기장비점검
        {
            string SearchQuery = " select * from CsPeriodicSpec where Line = '" + Line.Split('/')[0].Trim() + "'and Seq = '" + Seq + "'and Route = '" + Route + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSPreventSpec(string Line, string Seq, string Route, CustomPanelLinkEventArgs e)//설비예방보전
        {
            string SearchQuery = " select * from CsPreventSpec where Line = '" + Line.Split('/')[0].Trim() + "'and Seq = '" + Seq + "'and Route = '" + Route + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSOverhaulSpec(string Line, string Seq, string Route, CustomPanelLinkEventArgs e)//연간 오버홀
        {
            string SearchQuery = " select * from CsOverhaulSpec where Line = '" + Line.Split('/')[0].Trim() + "'and Seq = '" + Seq + "'and Route = '" + Route + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCS3c5sSpec(string Line, string Seq, CustomPanelLinkEventArgs e)//3정5행
        {
            string SearchQuery = " select * from Cs3c5sSpec where Line = '" + Line.Split('/')[0].Trim() + "'and Seq = '" + Seq + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSCheckSheetSpec(string CsCode, string Seq, CustomPanelLinkEventArgs e)//체크시트
        {
            string SearchQuery = " select * from CsCheckSheetSpec where CsCode= '" + CsCode + "'and Seq = '" + Seq + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSParameterCheckSpec(string CsCode, string Seq, CustomPanelLinkEventArgs e)//파라매터
        {
            string SearchQuery = " select * from CsParameterCheckSpec where CsCode= '" + CsCode + "'and Seq = '" + Seq + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckCSSpec(string CsCode, CustomPanelLinkEventArgs e)//파라매터
        {
            string SearchQuery = " select * from CsSpec where CsCode= '" + CsCode + "'";
            if (e.DbAccess.GetDataTable(SearchQuery).Rows.Count <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //CheckSheet
        public static DataTable BasicCSLine(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Line,LineName From CsLine";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicCSDateType(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "Select Common,Text from Common where category ='701'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCSCheckPeriod(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select Common,Text from Common where category ='702'";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCSCheckPeriod1(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select CheckPeriod from CsDailySpec group by CheckPeriod";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCSCheckPeriod2(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select CheckPeriod from CsPeriodicSpec group by CheckPeriod";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCSCheckPeriod3(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select CheckPeriod from CsPreventSpec group by CheckPeriod";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }

        public static DataTable BasicCSCheckPeriod4(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select CheckPeriod from CsOverhaulSpec group by CheckPeriod";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicCSCode(CustomPanelLinkEventArgs e)   //체크시트
        {
            DataTable dt = new DataTable();
            //string searchQuery = "select [CsCode] from CsCheckSheetSpec group by [CsCode] ";
            string searchQuery = "select [CsCode]  from CsSpec where parts is not null and CsCode not like 'PC%' order by CsCode ";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicCSCheckPeriod5(CustomPanelLinkEventArgs e)
        {
            DataTable dt = new DataTable();
            string searchQuery = "select CheckPeriod from CsCheckSheetSpec group by CheckPeriod";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }
        public static DataTable BasicCSCode2(CustomPanelLinkEventArgs e)   //파라매터
        {
            DataTable dt = new DataTable();
            //string searchQuery = "select CsCode from CsParameterCheckSpec group by CsCode ";
            string searchQuery = " select CsCode from CsSpec where CsCode like 'PC%' ";
            dt = e.DbAccess.GetDataTable(searchQuery);
            return dt;
        }



        #endregion
    }
}
