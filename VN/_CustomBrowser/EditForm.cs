using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class EditForm : Form
    {
        private string SettingData = string.Empty;
        private CustomPanelLinkEventArgs e = null;
        private WiseM.Browser.EditColumn.EditColumnFctSpec columnFctSpec = null;
        private WiseM.Browser.EditColumn.EditColumnOutputHist columnOutputHist = null;
        private WiseM.Browser.EditColumn.EditColumnBadHist columnBadHist = null;
        private WiseM.Browser.EditColumn.EditColumnRepairHist columnRepairHist = null;
        private WiseM.Browser.EditColumn.EditColumnNotworkHist columnNotworkHist = null;
        private WiseM.Browser.EditColumn.EditColumnDivision columnDivision = null;
        private WiseM.Browser.EditColumn.EditColumnRouting columnRouting = null;
        private WiseM.Browser.EditColumn.EditColumnMaterial columnMaterial = null;
        private WiseM.Browser.EditColumn.EditColumnRawMaterial columnRawMaterial = null;
        private WiseM.Browser.EditColumn.EditColumnBad columnBad = null;
        private WiseM.Browser.EditColumn.EditColumnNotwork columnNotwork = null;
        private WiseM.Browser.EditColumn.EditColumnRepair columnRepair = null;
        private WiseM.Browser.EditColumn.EditColumnWorkTeam columnWorkTeam = null;
        private WiseM.Browser.EditColumn.EditColumnWorkCalendarStd columnWorkCalendarStd = null;
        private WiseM.Browser.EditColumn.EditColumnWorkcenter columnWorkcenter = null;
        private WiseM.Browser.EditColumn.EditColumnRoutingBad columnRoutingBad = null;
        private WiseM.Browser.EditColumn.EditColumnRoutingNotWork columnRoutingNotWork = null;
        private WiseM.Browser.EditColumn.EditColumnWorker columnWorker = null;
        private WiseM.Browser.EditColumn.EditColumnMaterialMapping columnMaterialMapping = null;
        private WiseM.Browser.EditColumn.EditColumnMaterialRouting columnMaterialRouting = null;
        private WiseM.Browser.EditColumn.EditColumnWBTWorker columnWBTWorker = null;
        private WiseM.Browser.EditColumn.EditColumnFunctionChecked columnFunctionChecked = null;
        private WiseM.Browser.EditColumn.EditColumnLocationGroup columnLocationGroup = null;
        private WiseM.Browser.EditColumn.EditColumnLocation columnLocation = null;

        //ChechSheet
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSLineRoute ColumnCSLineRoute = null; // Cst001 라인정보
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSDailySpec ColumnCSDailySpec = null; // Cst003 일일장비점검 사양관리
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSPeriodicSpec ColumnCSPeriodicSpec = null; // Cst004 일일장비점검 사양관리
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSPreventSpec ColumnCSPreventSpec = null; // Cst005 설비예방보전 사양관리
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSOverhaulSpec ColumnCSOverhaulSpec = null; // Cst007 연간오버홀 사양관리
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCS3c5sSpec ColumnCS3c5sSpec = null; // Cst006 3정5행

        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSCheckSheetSpec ColumnCSCheckSheetSpec = null; // Cst008 체크시트
        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSParameterCheckSpec ColumnCSParameterCheckSpec = null; // Cst009 파라매터

        private WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSSpec ColumnCSSpec = null; // Cst002 CSspec


        private string targetClientID = null;
        private string targetWorker = null;

        public EditForm(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
            this.Process();
        }

        private bool NullCheckBasicCode(string basicCode)
        {
            if (string.IsNullOrEmpty(basicCode))
                return true;
            else
                return false;
        }

        // Foreign Key에 대한 정보을 얻어서 Dialogue Box에 추가
        private void SearchBasicCode()
        {
            switch (this.e.Program.ToLower())
            {
                case "prm003":
                    DataTable prm003divisionDt = BasicCodeInfo.BasicDivision(e);
                    PropertyItemList._divisionItems = new String[prm003divisionDt.Rows.Count];
                    for (int i = 0; i < prm003divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._divisionItems[i] = prm003divisionDt.Rows[i]["Division"].ToString() + "/" + prm003divisionDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm003routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[prm003routingDt.Rows.Count];
                    for (int i = 0; i < prm003routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = prm003routingDt.Rows[i]["Routing"].ToString() + "/" + prm003routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm003bunchDt = BasicCodeInfo.BasicBunchWorkcenter(e);
                    PropertyItemList._bunchworkcenterItems = new String[prm003bunchDt.Rows.Count];
                    for (int i = 0; i < prm003bunchDt.Rows.Count; i++)
                    {
                        PropertyItemList._bunchworkcenterItems[i] = prm003bunchDt.Rows[i]["Common"].ToString() + "/" + prm003bunchDt.Rows[i]["Text"].ToString();
                    }
                    break;


                case "prm101":
                    DataTable prm101divisionDt = BasicCodeInfo.BasicBunch(e);
                    PropertyItemList._BunchItems = new String[prm101divisionDt.Rows.Count];
                    for (int i = 0; i < prm101divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._BunchItems[i] = prm101divisionDt.Rows[i]["Common"].ToString();
                    }

                    DataTable prm101KindDt = BasicCodeInfo.BasicKind(e);
                    PropertyItemList._KindItems = new String[prm101KindDt.Rows.Count];
                    for (int i = 0; i < prm101KindDt.Rows.Count; i++)
                    {
                        PropertyItemList._KindItems[i] = prm101KindDt.Rows[i]["Common"].ToString();
                    }
                   
                    break;

                case "prm103":
                    //DataTable prm103workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    //PropertyItemList._workcenterItems = new String[prm103workcenterDt.Rows.Count];
                    //for (int i = 0; i < prm103workcenterDt.Rows.Count; i++)
                    //{
                    //    PropertyItemList._workcenterItems[i] = prm103workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + prm103workcenterDt.Rows[i]["Text"].ToString();
                    //}
                    DataTable prm103materialDt = BasicCodeInfo.BasicMaterial(e);
                    PropertyItemList._materialItems = new String[prm103materialDt.Rows.Count];
                    for (int i = 0; i < prm103materialDt.Rows.Count; i++)
                    {
                        PropertyItemList._materialItems[i] = prm103materialDt.Rows[i]["Material"].ToString() + "/" + prm103materialDt.Rows[i]["MaterialName"].ToString();
                    }
                    DataTable prm103routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[prm103routingDt.Rows.Count];
                    for (int i = 0; i < prm103routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = prm103routingDt.Rows[i]["Routing"].ToString() + "/" + prm103routingDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm104":
                    DataTable prm104BunchDt = BasicCodeInfo.BasicRawMaterialBunch(e);
                    PropertyItemList._BunchItems = new String[prm104BunchDt.Rows.Count];
                    for (int i = 0; i < prm104BunchDt.Rows.Count; i++)
                    {
                        PropertyItemList._BunchItems[i] = prm104BunchDt.Rows[i]["Common"].ToString() + "/" + prm104BunchDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm105":
                    DataTable prm105LocationGroupDt = BasicCodeInfo.BasicRawMaterialLocationGroup(e);
                    PropertyItemList._LocationGroup = new String[prm105LocationGroupDt.Rows.Count];
                    for (int i = 0; i < prm105LocationGroupDt.Rows.Count; i++)
                    {
                        PropertyItemList._LocationGroup[i] = prm105LocationGroupDt.Rows[i]["Location_Group"].ToString() + "/" + prm105LocationGroupDt.Rows[i]["Location_Group_Text"].ToString();
                    }
                    break;

                case "prm203":
                    DataTable prm203routingBadDt = BasicCodeInfo.BasicBad(e);
                    PropertyItemList._badItems = new String[prm203routingBadDt.Rows.Count];
                    for (int i = 0; i < prm203routingBadDt.Rows.Count; i++)
                    {
                        PropertyItemList._badItems[i] = prm203routingBadDt.Rows[i]["Bad"].ToString() + "/" + prm203routingBadDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm203routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[prm203routingDt.Rows.Count];
                    for (int i = 0; i < prm203routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = prm203routingDt.Rows[i]["Routing"].ToString() + "/" + prm203routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm203bunchDt = BasicCodeInfo.BasicBadBunch(e);
                    PropertyItemList._badBunchItems = new String[prm203bunchDt.Rows.Count];
                    for (int i = 0; i < prm203bunchDt.Rows.Count; i++)
                    {
                        PropertyItemList._badBunchItems[i] = prm203bunchDt.Rows[i]["BadBunch"].ToString() + "/" + prm203bunchDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm204":
                    DataTable prm204routingNotworkDt = BasicCodeInfo.BasicNotwork(e);
                    PropertyItemList._notworkItems = new String[prm204routingNotworkDt.Rows.Count];
                    for (int i = 0; i < prm204routingNotworkDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkItems[i] = prm204routingNotworkDt.Rows[i]["Notwork"].ToString() + "/" + prm204routingNotworkDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm204routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[prm204routingDt.Rows.Count];
                    for (int i = 0; i < prm204routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = prm204routingDt.Rows[i]["Routing"].ToString() + "/" + prm204routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm204bunchDt = BasicCodeInfo.BasicNotworkBunch(e);
                    PropertyItemList._notworkBunchItems = new String[prm204bunchDt.Rows.Count];
                    for (int i = 0; i < prm204bunchDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkBunchItems[i] = prm204bunchDt.Rows[i]["NotworkBunch"].ToString() + "/" + prm204bunchDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm302":
                    DataTable prm302workTeamDt = BasicCodeInfo.BasicWorkTeam(e);
                    PropertyItemList._workteamItems = new String[prm302workTeamDt.Rows.Count];
                    for (int i = 0; i < prm302workTeamDt.Rows.Count; i++)
                    {
                        PropertyItemList._workteamItems[i] = prm302workTeamDt.Rows[i]["WorkTeam"].ToString() + "/" + prm302workTeamDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm303":
                    DataTable prm303workerDt = BasicCodeInfo.BasicWorker(e);
                    PropertyItemList._workerItems = new String[prm303workerDt.Rows.Count];
                    for (int i = 0; i < prm303workerDt.Rows.Count; i++)
                    {
                        PropertyItemList._workerItems[i] = prm303workerDt.Rows[i]["Worker"].ToString() + "/" + prm303workerDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm303clientIDDt = BasicCodeInfo.BasicClientID(e);
                    PropertyItemList._clientIDItems = new String[prm303clientIDDt.Rows.Count];
                    for (int i = 0; i < prm303clientIDDt.Rows.Count; i++)
                    {
                        PropertyItemList._clientIDItems[i] = prm303clientIDDt.Rows[i]["ClientID"].ToString() + "/" + prm303clientIDDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "prm401":
                    DataTable prm401workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    PropertyItemList._workcenterItems = new String[prm401workcenterDt.Rows.Count];
                    for (int i = 0; i < prm401workcenterDt.Rows.Count; i++)
                    {
                        PropertyItemList._workcenterItems[i] = prm401workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + prm401workcenterDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm401workteamDt = BasicCodeInfo.BasicWorkTeam(e);
                    PropertyItemList._workteamItems = new String[prm401workteamDt.Rows.Count];
                    for (int i = 0; i < prm401workteamDt.Rows.Count; i++)
                    {
                        PropertyItemList._workteamItems[i] = prm401workteamDt.Rows[i]["WorkTeam"].ToString() + "/" + prm401workteamDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prm401dayofweekDt = BasicCodeInfo.BasicDayOfWeekStd();
                    PropertyItemList._dayofweekItems = new String[prm401dayofweekDt.Rows.Count];
                    for (int i = 0; i < prm401dayofweekDt.Rows.Count; i++)
                    {
                        PropertyItemList._dayofweekItems[i] = prm401dayofweekDt.Rows[i]["DayOfWeek"].ToString();
                    }
                    break;

                case "prd001":
                    DataTable prd001divisionDt = BasicCodeInfo.BasicDivision(e);
                    PropertyItemList._divisionItems = new String[prd001divisionDt.Rows.Count];
                    for (int i = 0; i < prd001divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._divisionItems[i] = prd001divisionDt.Rows[i]["Division"].ToString() + "/" + prd001divisionDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prd001workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    PropertyItemList._workcenterItems = new String[prd001workcenterDt.Rows.Count];
                    for (int i = 0; i < prd001workcenterDt.Rows.Count; i++)
                    {
                        PropertyItemList._workcenterItems[i] = prd001workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + prd001workcenterDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prd001materialDt = BasicCodeInfo.BasicMaterial(e);
                    PropertyItemList._materialItems = new String[prd001materialDt.Rows.Count];
                    for (int i = 0; i < prd001materialDt.Rows.Count; i++)
                    {
                        PropertyItemList._materialItems[i] = prd001materialDt.Rows[i]["Material"].ToString() + "/" + prd001materialDt.Rows[i]["MaterialName"].ToString();
                    }
                    DataTable prd001customerDt = BasicCodeInfo.BasicCustomer(e);
                    PropertyItemList._customerItems = new String[prd001customerDt.Rows.Count];
                    for (int i = 0; i < prd001customerDt.Rows.Count; i++)
                    {
                        PropertyItemList._customerItems[i] = prd001customerDt.Rows[i]["Customer"].ToString() + "/" + prd001customerDt.Rows[i]["CustomerName"].ToString();
                    }
                    DataTable prd001routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[prd001routingDt.Rows.Count];
                    for (int i = 0; i < prd001routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = prd001routingDt.Rows[i]["Routing"].ToString() + "/" + prd001routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable prd001shiftDt = BasicCodeInfo.BasicShift();
                    PropertyItemList._shiftItems = new String[prd001shiftDt.Rows.Count];
                    for (int i = 0; i < prd001shiftDt.Rows.Count; i++)
                    {
                        PropertyItemList._shiftItems[i] = prd001shiftDt.Rows[i]["Shift"].ToString();
                    }
                    DataTable prd001issuetypeDt = BasicCodeInfo.BasicIssueType();
                    PropertyItemList._issuetypeItems = new String[prd001issuetypeDt.Rows.Count];
                    for (int i = 0; i < prd001issuetypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._issuetypeItems[i] = prd001issuetypeDt.Rows[i]["IssueType"].ToString();
                    }
                    break;

                case "qis001":
                    DataTable qis001divisionDt = BasicCodeInfo.BasicDivision(e);
                    PropertyItemList._divisionItems = new String[qis001divisionDt.Rows.Count];
                    for (int i = 0; i < qis001divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._divisionItems[i] = qis001divisionDt.Rows[i]["Division"].ToString() + "/" + qis001divisionDt.Rows[i]["Text"].ToString();
                    }
                    DataTable qis001workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    PropertyItemList._workcenterItems = new String[qis001workcenterDt.Rows.Count];
                    for (int i = 0; i < qis001workcenterDt.Rows.Count; i++)
                    {
                        PropertyItemList._workcenterItems[i] = qis001workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + qis001workcenterDt.Rows[i]["Text"].ToString();
                    }
                    DataTable qis001materialDt = BasicCodeInfo.BasicMaterial(e);
                    PropertyItemList._materialItems = new String[qis001materialDt.Rows.Count];
                    for (int i = 0; i < qis001materialDt.Rows.Count; i++)
                    {
                        PropertyItemList._materialItems[i] = qis001materialDt.Rows[i]["Material"].ToString() + "/" + qis001materialDt.Rows[i]["MaterialName"].ToString();
                    }
                    DataTable qis001customerDt = BasicCodeInfo.BasicCustomer(e);
                    PropertyItemList._customerItems = new String[qis001customerDt.Rows.Count];
                    for (int i = 0; i < qis001customerDt.Rows.Count; i++)
                    {
                        PropertyItemList._customerItems[i] = qis001customerDt.Rows[i]["Customer"].ToString() + "/" + qis001customerDt.Rows[i]["CustomerName"].ToString();
                    }
                    DataTable qis001routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[qis001routingDt.Rows.Count];
                    for (int i = 0; i < qis001routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = qis001routingDt.Rows[i]["Routing"].ToString() + "/" + qis001routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable qis001badDt = BasicCodeInfo.BasicBad(e);
                    PropertyItemList._badItems = new String[qis001badDt.Rows.Count];
                    for (int i = 0; i < qis001badDt.Rows.Count; i++)
                    {
                        PropertyItemList._badItems[i] = qis001badDt.Rows[i]["Bad"].ToString() + "/" + qis001badDt.Rows[i]["Text"].ToString();
                    }
                    DataTable qis001shiftDt = BasicCodeInfo.BasicShift();
                    PropertyItemList._shiftItems = new String[qis001shiftDt.Rows.Count];
                    for (int i = 0; i < qis001shiftDt.Rows.Count; i++)
                    {
                        PropertyItemList._shiftItems[i] = qis001shiftDt.Rows[i]["Shift"].ToString();
                    }
                    DataTable qis001issuetypeDt = BasicCodeInfo.BasicIssueType();
                    PropertyItemList._issuetypeItems = new String[qis001issuetypeDt.Rows.Count];
                    for (int i = 0; i < qis001issuetypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._issuetypeItems[i] = qis001issuetypeDt.Rows[i]["IssueType"].ToString();
                    }
                    break;

                case "rep001":
                    DataTable rep001divisionDt = BasicCodeInfo.BasicDivision(e);
                    PropertyItemList._divisionItems = new String[rep001divisionDt.Rows.Count];
                    for (int i = 0; i < rep001divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._divisionItems[i] = rep001divisionDt.Rows[i]["Division"].ToString() + "/" + rep001divisionDt.Rows[i]["Text"].ToString();
                    }
                    DataTable rep001workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    PropertyItemList._workcenterItems = new String[rep001workcenterDt.Rows.Count];
                    for (int i = 0; i < rep001workcenterDt.Rows.Count; i++)
                    {
                        PropertyItemList._workcenterItems[i] = rep001workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + rep001workcenterDt.Rows[i]["Text"].ToString();
                    }
                    DataTable rep001materialDt = BasicCodeInfo.BasicMaterial(e);
                    PropertyItemList._materialItems = new String[rep001materialDt.Rows.Count];
                    for (int i = 0; i < rep001materialDt.Rows.Count; i++)
                    {
                        PropertyItemList._materialItems[i] = rep001materialDt.Rows[i]["Material"].ToString() + "/" + rep001materialDt.Rows[i]["MaterialName"].ToString();
                    }
                    DataTable rep001customerDt = BasicCodeInfo.BasicCustomer(e);
                    PropertyItemList._customerItems = new String[rep001customerDt.Rows.Count];
                    for (int i = 0; i < rep001customerDt.Rows.Count; i++)
                    {
                        PropertyItemList._customerItems[i] = rep001customerDt.Rows[i]["Customer"].ToString() + "/" + rep001customerDt.Rows[i]["CustomerName"].ToString();
                    }
                    DataTable rep001routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[rep001routingDt.Rows.Count];
                    for (int i = 0; i < rep001routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = rep001routingDt.Rows[i]["Routing"].ToString() + "/" + rep001routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable rep001badDt = BasicCodeInfo.BasicRepair(e);
                    PropertyItemList._repairItems = new String[rep001badDt.Rows.Count];
                    for (int i = 0; i < rep001badDt.Rows.Count; i++)
                    {
                        PropertyItemList._repairItems[i] = rep001badDt.Rows[i]["Repair"].ToString() + "/" + rep001badDt.Rows[i]["Text"].ToString();
                    }
                    DataTable rep001shiftDt = BasicCodeInfo.BasicShift();
                    PropertyItemList._shiftItems = new String[rep001shiftDt.Rows.Count];
                    for (int i = 0; i < rep001shiftDt.Rows.Count; i++)
                    {
                        PropertyItemList._shiftItems[i] = rep001shiftDt.Rows[i]["Shift"].ToString();
                    }
                    DataTable rep001issuetypeDt = BasicCodeInfo.BasicIssueType();
                    PropertyItemList._issuetypeItems = new String[rep001issuetypeDt.Rows.Count];
                    for (int i = 0; i < rep001issuetypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._issuetypeItems[i] = rep001issuetypeDt.Rows[i]["IssueType"].ToString();
                    }
                    break;

                case "wpm001":
                    DataTable wpm001divisionDt = BasicCodeInfo.BasicDivision(e);
                    PropertyItemList._divisionItems = new String[wpm001divisionDt.Rows.Count];
                    for (int i = 0; i < wpm001divisionDt.Rows.Count; i++)
                    {
                        PropertyItemList._divisionItems[i] = wpm001divisionDt.Rows[i]["Division"].ToString() + "/" + wpm001divisionDt.Rows[i]["Text"].ToString();
                    }
                    DataTable wpm001workcenterDt = BasicCodeInfo.BasicWorkcenter(e);
                    PropertyItemList._workcenterItems = new String[wpm001workcenterDt.Rows.Count];
                    for (int i = 0; i < wpm001workcenterDt.Rows.Count; i++)
                    {
                        PropertyItemList._workcenterItems[i] = wpm001workcenterDt.Rows[i]["Workcenter"].ToString() + "/" + wpm001workcenterDt.Rows[i]["Text"].ToString();
                    }
                    DataTable wpm001materialDt = BasicCodeInfo.BasicMaterial(e);
                    PropertyItemList._materialItems = new String[wpm001materialDt.Rows.Count];
                    for (int i = 0; i < wpm001materialDt.Rows.Count; i++)
                    {
                        PropertyItemList._materialItems[i] = wpm001materialDt.Rows[i]["Material"].ToString() + "/" + wpm001materialDt.Rows[i]["MaterialName"].ToString();
                    }
                    DataTable wpm001routingDt = BasicCodeInfo.BasicRouting(e);
                    PropertyItemList._routingItems = new String[wpm001routingDt.Rows.Count];
                    for (int i = 0; i < wpm001routingDt.Rows.Count; i++)
                    {
                        PropertyItemList._routingItems[i] = wpm001routingDt.Rows[i]["Routing"].ToString() + "/" + wpm001routingDt.Rows[i]["Text"].ToString();
                    }
                    DataTable wpm001notworkGroupDt = BasicCodeInfo.BasicNotworkGroup(e);
                    PropertyItemList._notworkGroupItems = new String[wpm001notworkGroupDt.Rows.Count];
                    for (int i = 0; i < wpm001notworkGroupDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkGroupItems[i] = wpm001notworkGroupDt.Rows[i]["Common"].ToString() + "/" + wpm001notworkGroupDt.Rows[i]["Text"].ToString();
                    }
                    DataTable wpm001notworkDt = BasicCodeInfo.BasicNotwork(e);
                    PropertyItemList._notworkItems = new String[wpm001notworkDt.Rows.Count];
                    for (int i = 0; i < wpm001notworkDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkItems[i] = wpm001notworkDt.Rows[i]["Notwork"].ToString() + "/" + wpm001notworkDt.Rows[i]["Text"].ToString();
                    }
                    DataTable wpm001shiftDt = BasicCodeInfo.BasicShift();
                    PropertyItemList._shiftItems = new String[wpm001shiftDt.Rows.Count];
                    for (int i = 0; i < wpm001shiftDt.Rows.Count; i++)
                    {
                        PropertyItemList._shiftItems[i] = wpm001shiftDt.Rows[i]["Shift"].ToString();
                    }
                    DataTable wpm001issuetypeDt = BasicCodeInfo.BasicIssueType();
                    PropertyItemList._issuetypeItems = new String[wpm001issuetypeDt.Rows.Count];
                    for (int i = 0; i < wpm001issuetypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._issuetypeItems[i] = wpm001issuetypeDt.Rows[i]["IssueType"].ToString();
                    }
                    break;


                //CheckSheet
                case "cst001":    //체크시트 사양정보
                    DataTable cst001cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst001cslineDt.Rows.Count];
                    for (int i = 0; i < cst001cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst001cslineDt.Rows[i]["Line"].ToString() + "/" + cst001cslineDt.Rows[i]["LineName"].ToString();
                    }
                    break;
                case "cst003":    //일일장비점검 사양정보
                    DataTable cst003cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst003cslineDt.Rows.Count];
                    for (int i = 0; i < cst003cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst003cslineDt.Rows[i]["Line"].ToString() + "/" + cst003cslineDt.Rows[i]["LineName"].ToString();
                    }

                    //체크시트 점검주기
                    DataTable cst003cscheckperiodDt = BasicCodeInfo.BasicCSCheckPeriod(e);
                    PropertyItemList._checkPeriod = new String[cst003cscheckperiodDt.Rows.Count];
                    for (int i = 0; i < cst003cscheckperiodDt.Rows.Count; i++)
                    {
                        PropertyItemList._checkPeriod[i] = cst003cscheckperiodDt.Rows[i]["Common"].ToString() + "/" + cst003cscheckperiodDt.Rows[i]["Text"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst003datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst003datatypeDt.Rows.Count];
                    for (int i = 0; i < cst003datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst003datatypeDt.Rows[i]["Common"].ToString() + "/" + cst003datatypeDt.Rows[i]["Text"].ToString();
                    }

                    break;
                case "cst004":    //정기장비점검 사양정보 CsPeriodicSpec
                    DataTable cst004cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst004cslineDt.Rows.Count];
                    for (int i = 0; i < cst004cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst004cslineDt.Rows[i]["Line"].ToString() + "/" + cst004cslineDt.Rows[i]["LineName"].ToString();
                    }

                    //체크시트 점검주기
                    DataTable cst004cscheckperiodDt = BasicCodeInfo.BasicCSCheckPeriod(e);
                    PropertyItemList._checkPeriod = new String[cst004cscheckperiodDt.Rows.Count];
                    for (int i = 0; i < cst004cscheckperiodDt.Rows.Count; i++)
                    {
                        PropertyItemList._checkPeriod[i] = cst004cscheckperiodDt.Rows[i]["Common"].ToString() + "/" + cst004cscheckperiodDt.Rows[i]["Text"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst004datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst004datatypeDt.Rows.Count];
                    for (int i = 0; i < cst004datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst004datatypeDt.Rows[i]["Common"].ToString() + "/" + cst004datatypeDt.Rows[i]["Text"].ToString();
                    }

                    break;
                case "cst005":    //설비예방보전 사양정보 CsPreventSpec
                    DataTable cst005cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst005cslineDt.Rows.Count];
                    for (int i = 0; i < cst005cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst005cslineDt.Rows[i]["Line"].ToString() + "/" + cst005cslineDt.Rows[i]["LineName"].ToString();
                    }

                    //체크시트 점검주기
                    DataTable cst005cscheckperiodDt = BasicCodeInfo.BasicCSCheckPeriod(e);
                    PropertyItemList._checkPeriod = new String[cst005cscheckperiodDt.Rows.Count];
                    for (int i = 0; i < cst005cscheckperiodDt.Rows.Count; i++)
                    {
                        PropertyItemList._checkPeriod[i] = cst005cscheckperiodDt.Rows[i]["Common"].ToString() + "/" + cst005cscheckperiodDt.Rows[i]["Text"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst005datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst005datatypeDt.Rows.Count];
                    for (int i = 0; i < cst005datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst005datatypeDt.Rows[i]["Common"].ToString() + "/" + cst005datatypeDt.Rows[i]["Text"].ToString();
                    }

                    break;
                case "cst007":    //연간 오버홀 사양정보
                    DataTable cst007cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst007cslineDt.Rows.Count];
                    for (int i = 0; i < cst007cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst007cslineDt.Rows[i]["Line"].ToString() + "/" + cst007cslineDt.Rows[i]["LineName"].ToString();
                    }

                    //체크시트 점검주기
                    DataTable cst007cscheckperiodDt = BasicCodeInfo.BasicCSCheckPeriod(e);
                    PropertyItemList._checkPeriod = new String[cst007cscheckperiodDt.Rows.Count];
                    for (int i = 0; i < cst007cscheckperiodDt.Rows.Count; i++)
                    {
                        PropertyItemList._checkPeriod[i] = cst007cscheckperiodDt.Rows[i]["Common"].ToString() + "/" + cst007cscheckperiodDt.Rows[i]["Text"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst007datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst007datatypeDt.Rows.Count];
                    for (int i = 0; i < cst007datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst007datatypeDt.Rows[i]["Common"].ToString() + "/" + cst007datatypeDt.Rows[i]["Text"].ToString();
                    }

                    break;
                case "cst006":    //3정5행
                    DataTable cst006cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst006cslineDt.Rows.Count];
                    for (int i = 0; i < cst006cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst006cslineDt.Rows[i]["Line"].ToString() + "/" + cst006cslineDt.Rows[i]["LineName"].ToString();
                    }
                    break;

                case "cst008":    //체크시트
                    DataTable cst008cscodeDt = BasicCodeInfo.BasicCSCode(e);
                    PropertyItemList._cscode = new String[cst008cscodeDt.Rows.Count];
                    for (int i = 0; i < cst008cscodeDt.Rows.Count; i++)
                    {
                        PropertyItemList._cscode[i] = cst008cscodeDt.Rows[i]["CsCode"].ToString();
                    }
                    //체크시트 점검주기

                    DataTable cst008cscheckperiodDt = BasicCodeInfo.BasicCSCheckPeriod(e);
                    PropertyItemList._checkPeriod = new String[cst008cscheckperiodDt.Rows.Count];
                    for (int i = 0; i < cst008cscheckperiodDt.Rows.Count; i++)
                    {
                        PropertyItemList._checkPeriod[i] = cst008cscheckperiodDt.Rows[i]["Common"].ToString() + "/" + cst008cscheckperiodDt.Rows[i]["Text"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst008datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst008datatypeDt.Rows.Count];
                    for (int i = 0; i < cst008datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst008datatypeDt.Rows[i]["Common"].ToString() + "/" + cst008datatypeDt.Rows[i]["Text"].ToString();
                    }
                    break;

                case "cst009":    //파라매터
                    DataTable cst009cscodeDt = BasicCodeInfo.BasicCSCode2(e);
                    PropertyItemList._cscode = new String[cst009cscodeDt.Rows.Count];
                    for (int i = 0; i < cst009cscodeDt.Rows.Count; i++)
                    {
                        PropertyItemList._cscode[i] = cst009cscodeDt.Rows[i]["CsCode"].ToString();
                    }

                    //체크시트 Datatype
                    DataTable cst009datatypeDt = BasicCodeInfo.BasicCSDateType(e);
                    PropertyItemList._datatype = new String[cst009datatypeDt.Rows.Count];
                    for (int i = 0; i < cst009datatypeDt.Rows.Count; i++)
                    {
                        PropertyItemList._datatype[i] = cst009datatypeDt.Rows[i]["Common"].ToString() + "/" + cst009datatypeDt.Rows[i]["Text"].ToString();
                    }

                    break;
                case "cst002":    //CSSpec
                    DataTable cst002cslineDt = BasicCodeInfo.BasicCSLine(e);
                    PropertyItemList._csline = new String[cst002cslineDt.Rows.Count];
                    for (int i = 0; i < cst002cslineDt.Rows.Count; i++)
                    {
                        PropertyItemList._csline[i] = cst002cslineDt.Rows[i]["Line"].ToString() + "/" + cst002cslineDt.Rows[i]["LineName"].ToString();
                    }
                    break;
            }
        }

        private void EditForm_SizeChanged(object sender, EventArgs e)
        {
            Size propertyGridSize = new Size(this.Size.Width - 33, this.Size.Height - 79);
            this.propertyGridEdit.Size = propertyGridSize;
        }

        private void CheckMessageBox(string message)
        {
            string checkMessage = "Please Check " + message + " Value.";
            MessageBox.Show(checkMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 어플리케이션별 Field 정의
        private void Process()
        {
            switch (this.e.Program.ToLower())
            {
                case "prm001":
                    this.Text = "Edit Division Code";
                    this.columnDivision = new WiseM.Browser.EditColumn.EditColumnDivision();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Division"].Value)))
                        {
                            this.columnDivision.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnDivision.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnDivision.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnDivision.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnDivision.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnDivision.TextKor = this.e.DataGridView.CurrentRow.Cells["TextKor"].Value.ToString();
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Updated"].Value.ToString()))
                            {
                                //this.columnDivision.Updated = "";
                            }
                            else
                            {
                                this.columnDivision.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            }
                            this.columnDivision.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnDivision;
                    break;

                case "prm002":
                    this.Text = "Edit Routing Code";
                    this.columnRouting = new WiseM.Browser.EditColumn.EditColumnRouting();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Routing"].Value)))
                        {
                            this.BtnEnableStatus(1);
                            this.columnRouting.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnRouting.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnRouting.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnRouting.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnRouting.TextKor = this.e.DataGridView.CurrentRow.Cells["TextKor"].Value.ToString();
                            this.columnRouting.TextEng = this.e.DataGridView.CurrentRow.Cells["TextEng"].Value.ToString();
                            this.columnRouting.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRouting.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnRouting.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRouting;
                    break;

                case "prm003":
                    this.Text = "Edit Workcenter Code";
                    this.columnWorkcenter = new WiseM.Browser.EditColumn.EditColumnWorkcenter();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value)))
                        {
                            this.columnWorkcenter.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnWorkcenter.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnWorkcenter.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnWorkcenter.BunchWorkcenter = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnWorkcenter.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnWorkcenter.Text = this.e.DataGridView.CurrentRow.Cells["TextKor"].Value.ToString();
                            this.columnWorkcenter.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnWorkcenter.SdiLine = this.e.DataGridView.CurrentRow.Cells["SdiLine"].Value.ToString();
                            this.columnWorkcenter.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnWorkcenter.WorkStatus = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["WorkStatus"].Value);
                            this.columnWorkcenter.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value.ToString()))
                                this.columnWorkcenter.ViewSeq = 0;
                            else
                                this.columnWorkcenter.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                            this.columnWorkcenter.AllowNotwork = this.e.DataGridView.CurrentRow.Cells["AllowNotwork"].Value.ToString();
                        }

                    }
                    else
                        this.BtnEnableStatus(0);

                        this.propertyGridEdit.SelectedObject = this.columnWorkcenter;
                    break;

                case "prm101":
                    this.Text = "Edit Material Code";
                    this.columnMaterial = new WiseM.Browser.EditColumn.EditColumnMaterial();
                    SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Material"].Value)))
                        {
                            this.BtnEnableStatus(1);
                            string SearchMaterialData = " Select * from Material where Material = '" + this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString() +"' ";
                            DataTable SearchMaterialDatadt = DbAccess.Default.GetDataTable(SearchMaterialData);

                            this.columnMaterial.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnMaterial.Text = SearchMaterialDatadt.Rows[0]["Text"].ToString();
                            this.columnMaterial.Bunch = SearchMaterialDatadt.Rows[0]["Bunch"].ToString();
                            this.columnMaterial.Kind = SearchMaterialDatadt.Rows[0]["Kind"].ToString();
                            this.columnMaterial.TextKor = SearchMaterialDatadt.Rows[0]["TextKor"].ToString();
                            this.columnMaterial.CycleTime = SearchMaterialDatadt.Rows[0]["CycleTime"].ToString();
                            this.columnMaterial.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnMaterial.Spec = SearchMaterialDatadt.Rows[0]["Spec"].ToString();
                            this.columnMaterial.cell_id = SearchMaterialDatadt.Rows[0]["Cell_ID"].ToString();

                            this.columnMaterial.case_A = SearchMaterialDatadt.Rows[0]["Case_A"].ToString();
                            this.columnMaterial.case_B = SearchMaterialDatadt.Rows[0]["Case_B"].ToString();
                            this.columnMaterial.case_C = SearchMaterialDatadt.Rows[0]["Case_C"].ToString();
                            this.columnMaterial.plate_A = SearchMaterialDatadt.Rows[0]["Plate_A"].ToString();
                            this.columnMaterial.plate_B = SearchMaterialDatadt.Rows[0]["Plate_B"].ToString();
                            this.columnMaterial.plate_C = SearchMaterialDatadt.Rows[0]["Plate_C"].ToString();
                            this.columnMaterial.plate_D = SearchMaterialDatadt.Rows[0]["Plate_D"].ToString();
                            this.columnMaterial.plate_E = SearchMaterialDatadt.Rows[0]["Plate_E"].ToString();
                            this.columnMaterial.plate_F = SearchMaterialDatadt.Rows[0]["Plate_F"].ToString();
                            this.columnMaterial.plate_G = SearchMaterialDatadt.Rows[0]["Plate_G"].ToString();
                            this.columnMaterial.Pcm = SearchMaterialDatadt.Rows[0]["Pcm"].ToString();

                            this.columnMaterial.CellQty = SearchMaterialDatadt.Rows[0]["CellQty"].ToString();
                            this.columnMaterial.BoxQty = SearchMaterialDatadt.Rows[0]["BoxQty"].ToString();
                            this.columnMaterial.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }

                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnMaterial;
                    break;

                case "prm102":
                    this.Text = "Edit Material Mapping";
                    this.columnMaterialMapping = new WiseM.Browser.EditColumn.EditColumnMaterialMapping();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Material"].Value)))
                        {
                            this.BtnEnableStatus(1);
                            this.columnMaterialMapping.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnMaterialMapping.CycleTime = this.e.DataGridView.CurrentRow.Cells["CycleTime"].Value.ToString();
                            this.columnMaterialMapping.Model = this.e.DataGridView.CurrentRow.Cells["Model"].Value.ToString();
                            this.columnMaterialMapping.Product = this.e.DataGridView.CurrentRow.Cells["Product"].Value.ToString();
                            this.columnMaterialMapping.ErpMaterial = this.e.DataGridView.CurrentRow.Cells["ErpMaterial"].Value.ToString();
                            this.columnMaterialMapping.CustomerMaterial = this.e.DataGridView.CurrentRow.Cells["CustomerMaterial"].Value.ToString();
                            this.columnMaterialMapping.CellModel = this.e.DataGridView.CurrentRow.Cells["CellModel"].Value.ToString();
                            this.columnMaterialMapping.CellCode = this.e.DataGridView.CurrentRow.Cells["CellCode"].Value.ToString();
                            this.columnMaterialMapping.Rev = this.e.DataGridView.CurrentRow.Cells["Rev"].Value.ToString();

                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["CellQty"].Value.ToString()))
                                this.columnMaterialMapping.CellQty = 0;
                            else
                                this.columnMaterialMapping.CellQty = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["CellQty"].Value);

                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Barcode_Digit"].Value.ToString()))
                                this.columnMaterialMapping.BarCode_Digit = "0";
                            else
                                this.columnMaterialMapping.BarCode_Digit = this.e.DataGridView.CurrentRow.Cells["Barcode_Digit"].Value.ToString();

                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Barcode_Check"].Value.ToString()))
                                this.columnMaterialMapping.BarCode_Check = "0";
                            else
                                this.columnMaterialMapping.BarCode_Check = this.e.DataGridView.CurrentRow.Cells["Barcode_Check"].Value.ToString();

							if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Box_digit"].Value.ToString()))
								this.columnMaterialMapping.Box_digit = "0";
							else
								this.columnMaterialMapping.Box_digit = this.e.DataGridView.CurrentRow.Cells["Box_digit"].Value.ToString();

							if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["BoxCode_Check"].Value.ToString()))
								this.columnMaterialMapping.BoxCode_Check = "0";
							else
								this.columnMaterialMapping.BoxCode_Check = this.e.DataGridView.CurrentRow.Cells["BoxCode_Check"].Value.ToString();

							if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Box_Qty"].Value.ToString()))
								this.columnMaterialMapping.BoxQty = "0";
							else
								this.columnMaterialMapping.BoxQty = this.e.DataGridView.CurrentRow.Cells["Box_Qty"].Value.ToString();

							this.columnMaterialMapping.CorePackCode = this.e.DataGridView.CurrentRow.Cells["CorePackCode"].Value.ToString();
                            this.columnMaterialMapping.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnMaterialMapping.CpkCheck = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Cpkcheck"].Value);
                            this.columnMaterialMapping.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                           
                          
                        }
                    }
                    else
                        this.BtnEnableStatus(0);
     
                    this.propertyGridEdit.SelectedObject = this.columnMaterialMapping;
                    break;

                case "prm103":
                    this.Text = "Edit MaterialRouting Code";
                    this.columnMaterialRouting = new WiseM.Browser.EditColumn.EditColumnMaterialRouting();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Material"].Value)))
                        {
                            this.columnMaterialRouting.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnMaterialRouting.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            //this.columnMaterialRouting.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnMaterialRouting.ApplyDate = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["ApplyDate"].Value);
                            this.columnMaterialRouting.IssueSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["IssueSeq"].Value);
                            this.columnMaterialRouting.CycleTime = Convert.ToDecimal(this.e.DataGridView.CurrentRow.Cells["CycleTime"].Value);
                            this.columnMaterialRouting.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnMaterialRouting;
                    break;

                case "prm104":
                    this.Text = "Edit RawMaterial Code";
                    this.columnRawMaterial = new WiseM.Browser.EditColumn.EditColumnRawMaterial();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["RawMaterial"].Value)))
                        {
                            this.BtnEnableStatus(1);
                            this.columnRawMaterial.RawMaterial = this.e.DataGridView.CurrentRow.Cells["RawMaterial"].Value.ToString();
                            this.columnRawMaterial.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnRawMaterial.Spec = this.e.DataGridView.CurrentRow.Cells["Spec"].Value.ToString();
                            this.columnRawMaterial.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnRawMaterial.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnRawMaterial.Unit = this.e.DataGridView.CurrentRow.Cells["Unit"].Value.ToString();
                            this.columnRawMaterial.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRawMaterial.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRawMaterial;
                    break;

                case "prm105":
                    if (e.Link.ToLower() == "editlocation")
                    {
                        SettingData = "editlocation";
                        this.Text = "Edit Location Code";
                        this.columnLocation = new WiseM.Browser.EditColumn.EditColumnLocation();
                        SearchBasicCode();
                        if (this.e.DataGridView.CurrentRow != null)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Location"].Value)))
                            {
                                string SearchLocationData = " Select * from RM_Location where Location = '" + this.e.DataGridView.CurrentRow.Cells["Location"].Value.ToString() + "' ";
                                DataTable SearchLocationDatadt = DbAccess.Default.GetDataTable(SearchLocationData);

                                this.columnLocation.Location = this.e.DataGridView.CurrentRow.Cells["Location"].Value.ToString();
                                this.columnLocation.LocationGroup = SearchLocationDatadt.Rows[0]["Location_Group"].ToString();
                                this.columnLocation.Bunch = SearchLocationDatadt.Rows[0]["Bunch"].ToString();
                                this.columnLocation.Kind = SearchLocationDatadt.Rows[0]["Kind"].ToString();
                                this.columnLocation.LocationText = SearchLocationDatadt.Rows[0]["Location_Text"].ToString();
                                this.columnLocation.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            }

                        }
                        else
                        { 
                        }
                            //this.BtnEnableStatus(0);

                       this.propertyGridEdit.SelectedObject = this.columnLocation;
                    }
                    else if(e.Link.ToLower() == "editlocationgroup")
                    {
                        SettingData = "editlocationgroup";
                        this.Text = "Edit LocationGroup Code";
                        this.columnLocationGroup = new WiseM.Browser.EditColumn.EditColumnLocationGroup();
                        if (this.e.DataGridView.CurrentRow != null)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Location_Group"].Value)))
                            {
                                string SearchLocation_Group = " Select * from Rm_Location_Group where Location_Group = '" + this.e.DataGridView.CurrentRow.Cells["Location_Group"].Value.ToString() + "' ";
                                DataTable SearchLocation_Groupdt = DbAccess.Default.GetDataTable(SearchLocation_Group);

                                this.columnLocationGroup.LocationGroup = SearchLocation_Groupdt.Rows[0]["Location_Group"].ToString();
                                this.columnLocationGroup.LocationGroupText = SearchLocation_Groupdt.Rows[0]["Location_Group_Text"].ToString();
                                this.columnLocationGroup.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            }

                        }
                        else
                        {
                        }
                        //this.BtnEnableStatus(0);

                        this.propertyGridEdit.SelectedObject = this.columnLocationGroup;
                    }
                    
                    break;

                case "prm201":
                    this.Text = "Edit Bad Code";
                    this.columnBad = new WiseM.Browser.EditColumn.EditColumnBad();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Bad"].Value)))
                        {
                            this.columnBad.Bad = this.e.DataGridView.CurrentRow.Cells["Bad"].Value.ToString();
                            this.columnBad.SDI_Bad = this.e.DataGridView.CurrentRow.Cells["SDI_Bad"].Value.ToString();
                            this.columnBad.ERP_Bad = this.e.DataGridView.CurrentRow.Cells["ERP_Bad"].Value.ToString();
                            this.columnBad.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnBad.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnBad.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnBad.Repair = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Repair"].Value);
                            this.columnBad.ReInsp = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["ReInsp"].Value);
                            this.columnBad.Loss = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Loss"].Value);
                            this.columnBad.Scrap = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Scrap"].Value);
                            this.columnBad.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnBad.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnBad.TextKor = this.e.DataGridView.CurrentRow.Cells["TextKor"].Value.ToString();
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value.ToString()))
                            {
                                this.columnBad.ViewSeq = 0;
                            }
                            else
                            {
                                this.columnBad.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                            }
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnBad;
                    break;

                case "prm202":
                    this.Text = "Edit Notwork Code";
                    this.columnNotwork = new WiseM.Browser.EditColumn.EditColumnNotwork();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Notwork"].Value)))
                        {
                            this.columnNotwork.Notwork = this.e.DataGridView.CurrentRow.Cells["Notwork"].Value.ToString();
                            this.columnNotwork.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnNotwork.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnNotwork.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnNotwork.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnNotwork.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnNotwork.TextKor = this.e.DataGridView.CurrentRow.Cells["TextKor"].Value.ToString();
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value.ToString()))
                            {
                                this.columnNotwork.ViewSeq = 0;
                            }
                            else
                            {
                                this.columnNotwork.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                            }
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnNotwork;
                    break;

                case "prm203":
                    this.Text = "Edit Bad Code by Routing";
                    this.columnRoutingBad = new WiseM.Browser.EditColumn.EditColumnRoutingBad();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Bad"].Value)))
                        {
                            this.columnRoutingBad.BasisTable = this.e.DataGridView.CurrentRow.Cells["BasisTable"].Value.ToString();
                            this.columnRoutingBad.Bad = this.e.DataGridView.CurrentRow.Cells["Bad"].Value.ToString();
                            this.columnRoutingBad.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnRoutingBad.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRoutingBad.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnRoutingBad.BadBunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.ChangeSearchBasic("prm203", "BadBunch", this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString());
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRoutingBad;
                    break;

                case "prm204":
                    this.Text = "Edit Notwork Code by Routing";
                    this.columnRoutingNotWork = new WiseM.Browser.EditColumn.EditColumnRoutingNotWork();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Notwork"].Value)))
                        {
                            this.columnRoutingNotWork.BasisTable = this.e.DataGridView.CurrentRow.Cells["BasisTable"].Value.ToString();
                            this.columnRoutingNotWork.Notwork = this.e.DataGridView.CurrentRow.Cells["Notwork"].Value.ToString();
                            this.columnRoutingNotWork.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnRoutingNotWork.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRoutingNotWork.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnRoutingNotWork.NotworkBunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.ChangeSearchBasic("prm204", "NotworkBunch", this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString());
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRoutingNotWork;
                    break;

                case "prm205":
                    this.Text = "Edit Repair Code";
                    this.columnRepair = new WiseM.Browser.EditColumn.EditColumnRepair();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Repair"].Value)))
                        {
                            this.columnRepair.Repair = this.e.DataGridView.CurrentRow.Cells["Repair"].Value.ToString();
                            this.columnRepair.Text = this.e.DataGridView.CurrentRow.Cells["Text"].Value.ToString();
                            this.columnRepair.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnRepair.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnRepair.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRepair.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value.ToString()))
                            {
                                this.columnRepair.ViewSeq = 0;
                            }
                            else
                            {
                                this.columnRepair.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                            }
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRepair;
                    break;

                case "prm302":
                    this.Text = "Edit Worker Code";
                    this.columnWorker = new WiseM.Browser.EditColumn.EditColumnWorker();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Worker"].Value)))
                        {
                            this.columnWorker.Worker = this.e.DataGridView.CurrentRow.Cells["Worker"].Value.ToString();
                            this.columnWorker.WorkerName = this.e.DataGridView.CurrentRow.Cells["WorkerName"].Value.ToString();
                            this.columnWorker.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnWorker.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnWorker.WorkTeam = this.e.DataGridView.CurrentRow.Cells["WorkTeam"].Value.ToString();
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnWorker;
                    break;

                case "prm303":
                    this.Text = "Edit Worker Code by WBT";
                    this.columnWBTWorker = new WiseM.Browser.EditColumn.EditColumnWBTWorker();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["ClientID"].Value)))
                        {
                            this.targetClientID = this.e.DataGridView.CurrentRow.Cells["ClientID"].Value.ToString();
                            this.targetWorker = this.e.DataGridView.CurrentRow.Cells["Worker"].Value.ToString();
                            this.columnWBTWorker.ClientID = this.e.DataGridView.CurrentRow.Cells["ClientID"].Value.ToString();
                            this.columnWBTWorker.Worker = this.e.DataGridView.CurrentRow.Cells["Worker"].Value.ToString();
                            this.columnWBTWorker.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnWBTWorker;
                    break;

                case "prm304":
                    this.Text = "Edit WorkTeam Code";
                    this.columnWorkTeam = new WiseM.Browser.EditColumn.EditColumnWorkTeam();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["WorkTeam"].Value)))
                        {
                            this.columnWorkTeam.WorkTeam = this.e.DataGridView.CurrentRow.Cells["WorkTeam"].Value.ToString();
                            this.columnWorkTeam.Text = this.e.DataGridView.CurrentRow.Cells["WorkTeamName"].Value.ToString();
                            this.columnWorkTeam.Bunch = this.e.DataGridView.CurrentRow.Cells["Bunch"].Value.ToString();
                            this.columnWorkTeam.Kind = this.e.DataGridView.CurrentRow.Cells["Kind"].Value.ToString();
                            this.columnWorkTeam.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnWorkTeam.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value.ToString()))
                                this.columnWorkTeam.ViewSeq = 0;
                            else
                                this.columnWorkTeam.ViewSeq = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["ViewSeq"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnWorkTeam;
                    break;

                case "prm401":
                    this.Text = "Edit WorkCalendarStd Code";
                    this.columnWorkCalendarStd = new WiseM.Browser.EditColumn.EditColumnWorkCalendarStd();
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["DayOfWeek"].Value)))
                        {
                            this.columnWorkCalendarStd.DayOfWeek = this.e.DataGridView.CurrentRow.Cells["DayOfWeek"].Value.ToString();
                            this.columnWorkCalendarStd.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnWorkCalendarStd.WorkTeam = this.e.DataGridView.CurrentRow.Cells["WorkTeam"].Value.ToString();
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["MeanWorker"].Value.ToString()))
                                this.columnWorkCalendarStd.MeanWorker = 0;
                            else
                                this.columnWorkCalendarStd.MeanWorker = Convert.ToDecimal(this.e.DataGridView.CurrentRow.Cells["MeanWorker"].Value);

                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["WorkingTime"].Value.ToString()))
                                this.columnWorkCalendarStd.WorkingTime = 0;
                            else
                                this.columnWorkCalendarStd.WorkingTime = Convert.ToDecimal(this.e.DataGridView.CurrentRow.Cells["WorkingTime"].Value);

                            this.columnWorkCalendarStd.StartHour = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["StartHour"].Value);
                            this.columnWorkCalendarStd.EndHour = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["EndHour"].Value);
                            this.columnWorkCalendarStd.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnWorkCalendarStd;
                    break;

                case "prm501":
                    this.Text = "Edit BasicCode FucnErrorCode";
                    this.columnFunctionChecked = new WiseM.Browser.EditColumn.EditColumnFunctionChecked();
                    if (this.e.DataGridView.CurrentRow != null)
                    { 
                           if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["Model"].Value)))
                        {
                            this.columnFunctionChecked.Model = this.e.DataGridView.CurrentRow.Cells["Model"].Value.ToString();
                            this.columnFunctionChecked.FuncError = this.e.DataGridView.CurrentRow.Cells["FuncError"].Value.ToString();
                            this.columnFunctionChecked.FuncErrorName = this.e.DataGridView.CurrentRow.Cells["FuncErrorName"].Value.ToString();
                            this.columnFunctionChecked.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnFunctionChecked.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnFunctionChecked;
                    break;
                    

                case "prd001":
                    this.Text = "Edit Prod.Result by Occurrence";
                    this.columnOutputHist = new WiseM.Browser.EditColumn.EditColumnOutputHist();
                    //this.BtnEnableStatus(0);  // 수정,삭제 버튼 안되게
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["OutputHist"].Value)))
                        {
                            this.columnOutputHist.OutputHist = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["OutputHist"].Value);
                            this.columnOutputHist.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnOutputHist.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnOutputHist.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnOutputHist.Customer = this.e.DataGridView.CurrentRow.Cells["Customer"].Value.ToString();
                            this.columnOutputHist.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnOutputHist.WorkOrder = this.e.DataGridView.CurrentRow.Cells["WorkOrder"].Value.ToString();
                            this.columnOutputHist.Shift = this.e.DataGridView.CurrentRow.Cells["Shift"].Value.ToString();
                            this.columnOutputHist.ClientId = this.e.DataGridView.CurrentRow.Cells["ClientId"].Value.ToString();
                            this.columnOutputHist.Started = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Started"].Value);
                            this.columnOutputHist.Ended = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Ended"].Value);
                            this.columnOutputHist.OutQty = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["OutQty"].Value);
                            this.columnOutputHist.PalletNo = this.e.DataGridView.CurrentRow.Cells["PalletNo"].Value.ToString();
                            this.columnOutputHist.BoxNo = this.e.DataGridView.CurrentRow.Cells["BoxNo"].Value.ToString();
                            this.columnOutputHist.SerialNo = this.e.DataGridView.CurrentRow.Cells["SerialNo"].Value.ToString();
                            this.columnOutputHist.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnOutputHist.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnOutputHist;
                    break;

                case "prm900":
                    this.Text = "Edit FCT Spec";
                    this.columnFctSpec = new WiseM.Browser.EditColumn.EditColumnFctSpec();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["MaterialCode"].Value)))
                        {
                            this.columnFctSpec.MaterialCode = this.e.DataGridView.CurrentRow.Cells["MaterialCode"].Value.ToString();
                            this.columnFctSpec.OCV_Min = this.e.DataGridView.CurrentRow.Cells["OCV_Min"].Value.ToString();
                            this.columnFctSpec.OCV_Max = this.e.DataGridView.CurrentRow.Cells["OCV_Max"].Value.ToString();
                            this.columnFctSpec.IR_Min = this.e.DataGridView.CurrentRow.Cells["IR_Min"].Value.ToString();
                            this.columnFctSpec.IR_Max = this.e.DataGridView.CurrentRow.Cells["IR_Max"].Value.ToString();
                            this.columnFctSpec.SAR_ST_Min = this.e.DataGridView.CurrentRow.Cells["SAR_ST_Min"].Value.ToString();
                            this.columnFctSpec.SAR_ST_Max = this.e.DataGridView.CurrentRow.Cells["SAR_ST_Max"].Value.ToString();
                            this.columnFctSpec.SAR_SV_Min = this.e.DataGridView.CurrentRow.Cells["SAR_SV_Min"].Value.ToString();
                            this.columnFctSpec.SAR_SV_Max = this.e.DataGridView.CurrentRow.Cells["SAR_SV_Max"].Value.ToString();
                            this.columnFctSpec.SAR_SRV_Min = this.e.DataGridView.CurrentRow.Cells["SAR_SRV_Min"].Value.ToString();
                            this.columnFctSpec.SAR_SRV_Max = this.e.DataGridView.CurrentRow.Cells["SAR_SRV_Max"].Value.ToString();
                            this.columnFctSpec.DCCV_Min = this.e.DataGridView.CurrentRow.Cells["DCCV_Min"].Value.ToString();
                            this.columnFctSpec.DCCV_Max = this.e.DataGridView.CurrentRow.Cells["DCCV_Max"].Value.ToString();
                            this.columnFctSpec.CCCV_Min = this.e.DataGridView.CurrentRow.Cells["CCCV_Min"].Value.ToString();
                            this.columnFctSpec.CCCV_Max = this.e.DataGridView.CurrentRow.Cells["CCCV_Max"].Value.ToString();
                            this.columnFctSpec.CELL_OCV_Min = this.e.DataGridView.CurrentRow.Cells["CELL_OCV_Min"].Value.ToString();
                            this.columnFctSpec.CELL_OCV_Max = this.e.DataGridView.CurrentRow.Cells["CELL_OCV_Max"].Value.ToString();
                            this.columnFctSpec.FOCV_Min = this.e.DataGridView.CurrentRow.Cells["FOCV_Min"].Value.ToString();
                            this.columnFctSpec.FOCV_Max = this.e.DataGridView.CurrentRow.Cells["FOCV_Max"].Value.ToString();
                            this.columnFctSpec.CNT_T1_Min = this.e.DataGridView.CurrentRow.Cells["CNT_T1_Min"].Value.ToString();
                            this.columnFctSpec.CNT_T1_Max = this.e.DataGridView.CurrentRow.Cells["CNT_T1_Max"].Value.ToString();
                            this.columnFctSpec.CNT_V1_Min = this.e.DataGridView.CurrentRow.Cells["CNT_V1_Min"].Value.ToString();
                            this.columnFctSpec.CNT_V1_Max = this.e.DataGridView.CurrentRow.Cells["CNT_V1_Max"].Value.ToString();
                            this.columnFctSpec.CNT_T2_Min = this.e.DataGridView.CurrentRow.Cells["CNT_T2_Min"].Value.ToString();
                            this.columnFctSpec.CNT_T2_Max = this.e.DataGridView.CurrentRow.Cells["CNT_T2_Max"].Value.ToString();
                            this.columnFctSpec.CNT_V2_Min = this.e.DataGridView.CurrentRow.Cells["CNT_V2_Min"].Value.ToString();
                            this.columnFctSpec.CNT_V2_Max = this.e.DataGridView.CurrentRow.Cells["CNT_V2_Max"].Value.ToString();
                            this.columnFctSpec.R1_VFR_Min = this.e.DataGridView.CurrentRow.Cells["R1_VFR_Min"].Value.ToString();
                            this.columnFctSpec.R1_VFR_Max = this.e.DataGridView.CurrentRow.Cells["R1_VFR_Max"].Value.ToString();
                            this.columnFctSpec.R2_CFR_Min = this.e.DataGridView.CurrentRow.Cells["R2_CFR_Min"].Value.ToString();
                            this.columnFctSpec.R2_CFR_Max = this.e.DataGridView.CurrentRow.Cells["R2_CFR_Max"].Value.ToString();
                            if (string.IsNullOrEmpty(this.e.DataGridView.CurrentRow.Cells["Updated"].Value.ToString()))
                            {
                                //this.columnDivision.Updated = "";
                            }
                            else
                            {
                                this.columnFctSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            }
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnFctSpec;
                    break;

                case "qis001":
                    this.Text = "Edit NG.Result by Occurrence";
                    this.columnBadHist = new WiseM.Browser.EditColumn.EditColumnBadHist();
                    this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["BadHist"].Value)))
                        {
                            this.columnBadHist.BadHist = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["BadHist"].Value);
                            this.columnBadHist.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnBadHist.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnBadHist.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnBadHist.Customer = this.e.DataGridView.CurrentRow.Cells["Customer"].Value.ToString();
                            this.columnBadHist.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnBadHist.WorkOrder = this.e.DataGridView.CurrentRow.Cells["WorkOrder"].Value.ToString();
                            this.columnBadHist.Bad = this.e.DataGridView.CurrentRow.Cells["Bad"].Value.ToString();
                            this.columnBadHist.Shift = this.e.DataGridView.CurrentRow.Cells["Shift"].Value.ToString();
                            //this.columnBadHist.ClientId = this.e.DataGridView.CurrentRow.Cells["ClientId"].Value.ToString();
                            this.columnBadHist.Started = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Started"].Value);
                            this.columnBadHist.Ended = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Ended"].Value);
                            this.columnBadHist.BadQty = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["BadQty"].Value);
                            //this.columnBadHist.PalletNo = this.e.DataGridView.CurrentRow.Cells["PalletNo"].Value.ToString();
                            //this.columnBadHist.BoxNo = this.e.DataGridView.CurrentRow.Cells["BoxNo"].Value.ToString();
                            this.columnBadHist.SerialNo = this.e.DataGridView.CurrentRow.Cells["SerialNo"].Value.ToString();
                            this.columnBadHist.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnBadHist.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnBadHist;
                    break;

                case "rep001":
                    this.Text = "Edit Repair Result by Occurrence";
                    this.columnRepairHist = new WiseM.Browser.EditColumn.EditColumnRepairHist();
                    this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["RepairHist"].Value)))
                        {
                            this.columnRepairHist.RepairHist = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["RepairHist"].Value);
                            this.columnRepairHist.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnRepairHist.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnRepairHist.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnRepairHist.Customer = this.e.DataGridView.CurrentRow.Cells["Customer"].Value.ToString();
                            this.columnRepairHist.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnRepairHist.WorkOrder = this.e.DataGridView.CurrentRow.Cells["WorkOrder"].Value.ToString();
                            this.columnRepairHist.Repair = this.e.DataGridView.CurrentRow.Cells["Repair"].Value.ToString();
                            this.columnRepairHist.Shift = this.e.DataGridView.CurrentRow.Cells["Shift"].Value.ToString();
                            this.columnRepairHist.ClientId = this.e.DataGridView.CurrentRow.Cells["ClientId"].Value.ToString();
                            this.columnRepairHist.Started = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Started"].Value);
                            this.columnRepairHist.Ended = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Ended"].Value);
                            this.columnRepairHist.RepairQty = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["RepairQty"].Value);
                            this.columnRepairHist.PalletNo = this.e.DataGridView.CurrentRow.Cells["PalletNo"].Value.ToString();
                            this.columnRepairHist.BoxNo = this.e.DataGridView.CurrentRow.Cells["BoxNo"].Value.ToString();
                            this.columnRepairHist.SerialNo = this.e.DataGridView.CurrentRow.Cells["SerialNo"].Value.ToString();
                            this.columnRepairHist.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnRepairHist.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnRepairHist;
                    break;

                case "wpm001":
                    this.Text = "Edit Notwork.Result by Occurrence";
                    this.columnNotworkHist = new WiseM.Browser.EditColumn.EditColumnNotworkHist();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["NotworkHist"].Value)))
                        {
                            this.columnNotworkHist.NotworkHist = Convert.ToInt32(this.e.DataGridView.CurrentRow.Cells["NotworkHist"].Value);
                            this.columnNotworkHist.Division = this.e.DataGridView.CurrentRow.Cells["Division"].Value.ToString();
                            this.columnNotworkHist.Workcenter = this.e.DataGridView.CurrentRow.Cells["Workcenter"].Value.ToString();
                            this.columnNotworkHist.Material = this.e.DataGridView.CurrentRow.Cells["Material"].Value.ToString();
                            this.columnNotworkHist.Customer = this.e.DataGridView.CurrentRow.Cells["Customer"].Value.ToString();
                            this.columnNotworkHist.Routing = this.e.DataGridView.CurrentRow.Cells["Routing"].Value.ToString();
                            this.columnNotworkHist.WorkOrder = this.e.DataGridView.CurrentRow.Cells["WorkOrder"].Value.ToString();
                            this.columnNotworkHist.Notwork = this.e.DataGridView.CurrentRow.Cells["Notwork"].Value.ToString();
                            this.columnNotworkHist.Shift = this.e.DataGridView.CurrentRow.Cells["Shift"].Value.ToString();
                            this.columnNotworkHist.ClientId = this.e.DataGridView.CurrentRow.Cells["ClientId"].Value.ToString();
                            this.columnNotworkHist.Started = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Started"].Value);
                            this.columnNotworkHist.Ended = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Ended"].Value);
                            this.columnNotworkHist.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);
                            this.columnNotworkHist.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                            this.columnNotworkHist.TotalMinute = Convert.ToDecimal(this.e.DataGridView.CurrentRow.Cells["TotalMinute"].Value);
                            this.columnNotworkHist.TotalNotwork = this.e.DataGridView.CurrentRow.Cells["TotalNotwork"].Value.ToString();
                            this.columnNotworkHist.TotalRecess = this.e.DataGridView.CurrentRow.Cells["TotalRecess"].Value.ToString();
                            this.columnNotworkHist.NetNotwork = this.e.DataGridView.CurrentRow.Cells["NetNotwork"].Value.ToString();
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.columnNotworkHist;
                    break;



                //CheckSheet
                case "cst001":
                    this.Text = "Edit CheckSheet CSLineRoute";
                    this.ColumnCSLineRoute = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSLineRoute();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCSLineRoute.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCSLineRoute.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();

                            this.ColumnCSLineRoute.RouteName = this.e.DataGridView.CurrentRow.Cells["RouteName"].Value.ToString();
                            this.ColumnCSLineRoute.updater = this.e.DataGridView.CurrentRow.Cells["updater"].Value.ToString();
                            this.ColumnCSLineRoute.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);

                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSLineRoute;
                    break;

                case "cst003":    //일일정기점검 사양관리
                    this.Text = "Edit CheckSheet CSDailySpec";
                    this.ColumnCSDailySpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSDailySpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCSDailySpec.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCSDailySpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();
                            this.ColumnCSDailySpec.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();

                            this.ColumnCSDailySpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();
                            this.ColumnCSDailySpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSDailySpec.CheckTiming = this.e.DataGridView.CurrentRow.Cells["CheckTiming"].Value.ToString();
                            this.ColumnCSDailySpec.CheckPeriod = this.e.DataGridView.CurrentRow.Cells["CheckPeriod"].Value.ToString();
                            this.ColumnCSDailySpec.ValueMin = this.e.DataGridView.CurrentRow.Cells["ValueMin"].Value.ToString();
                            this.ColumnCSDailySpec.ValueMax = this.e.DataGridView.CurrentRow.Cells["ValueMax"].Value.ToString();

                            this.ColumnCSDailySpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);

                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSDailySpec;
                    break;

                case "cst004":    //정기장비점검 사양관리
                    this.Text = "Edit CheckSheet CSPeriodicSpec";
                    this.ColumnCSPeriodicSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSPeriodicSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCSPeriodicSpec.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCSPeriodicSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();
                            this.ColumnCSPeriodicSpec.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();

                            this.ColumnCSPeriodicSpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();
                            this.ColumnCSPeriodicSpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSPeriodicSpec.CheckTiming = this.e.DataGridView.CurrentRow.Cells["CheckTiming"].Value.ToString();
                            this.ColumnCSPeriodicSpec.CheckPeriod = this.e.DataGridView.CurrentRow.Cells["CheckPeriod"].Value.ToString();
                            this.ColumnCSPeriodicSpec.ValueMin = this.e.DataGridView.CurrentRow.Cells["ValueMin"].Value.ToString();
                            this.ColumnCSPeriodicSpec.ValueMax = this.e.DataGridView.CurrentRow.Cells["ValueMax"].Value.ToString();

                            this.ColumnCSPeriodicSpec.CheckMethod = this.e.DataGridView.CurrentRow.Cells["CheckMethod"].Value.ToString();
                            this.ColumnCSPeriodicSpec.PlannedDate = this.e.DataGridView.CurrentRow.Cells["PlannedDate"].Value.ToString().Substring(0, 10);
                            this.ColumnCSPeriodicSpec.Factor = this.e.DataGridView.CurrentRow.Cells["Factor"].Value.ToString();

                            this.ColumnCSPeriodicSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);

                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSPeriodicSpec;
                    break;

                case "cst005":    //설비예방보전 사양관리
                    this.Text = "Edit CheckSheet CSPreventSpec";
                    this.ColumnCSPreventSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSPreventSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCSPreventSpec.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCSPreventSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();
                            this.ColumnCSPreventSpec.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();

                            this.ColumnCSPreventSpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();
                            this.ColumnCSPreventSpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSPreventSpec.CheckTiming = this.e.DataGridView.CurrentRow.Cells["CheckTiming"].Value.ToString();
                            this.ColumnCSPreventSpec.CheckPeriod = this.e.DataGridView.CurrentRow.Cells["CheckPeriod"].Value.ToString();

                            this.ColumnCSPreventSpec.CheckMethod = this.e.DataGridView.CurrentRow.Cells["CheckMethod"].Value.ToString();
                            this.ColumnCSPreventSpec.PlannedDate = this.e.DataGridView.CurrentRow.Cells["PlannedDate"].Value.ToString().Substring(0, 10);

                            this.ColumnCSPreventSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);

                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSPreventSpec;
                    break;

                case "cst007":    //연간 오버홀 사양관리
                    this.Text = "Edit CheckSheet CsOverhaulSpec";
                    this.ColumnCSOverhaulSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSOverhaulSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCSOverhaulSpec.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCSOverhaulSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();
                            this.ColumnCSOverhaulSpec.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();

                            this.ColumnCSOverhaulSpec.Parts = this.e.DataGridView.CurrentRow.Cells["Parts"].Value.ToString();
                            this.ColumnCSOverhaulSpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();
                            this.ColumnCSOverhaulSpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSOverhaulSpec.CheckTiming = this.e.DataGridView.CurrentRow.Cells["CheckTiming"].Value.ToString();
                            this.ColumnCSOverhaulSpec.CheckPeriod = this.e.DataGridView.CurrentRow.Cells["CheckPeriod"].Value.ToString();

                            this.ColumnCSOverhaulSpec.CheckMethod = this.e.DataGridView.CurrentRow.Cells["CheckMethod"].Value.ToString();
                            this.ColumnCSOverhaulSpec.Required = this.e.DataGridView.CurrentRow.Cells["Required"].Value.ToString();
                            this.ColumnCSOverhaulSpec.ReplaceParts = this.e.DataGridView.CurrentRow.Cells["ReplaceParts"].Value.ToString();
                            this.ColumnCSOverhaulSpec.PlannedDate = this.e.DataGridView.CurrentRow.Cells["PlannedDate"].Value.ToString().Substring(0, 10);

                            this.ColumnCSOverhaulSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);

                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSOverhaulSpec;
                    break;
                case "cst006":    //3정5행
                    this.Text = "Edit CheckSheet Cs3c5sSpec";
                    this.ColumnCS3c5sSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCS3c5sSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["line"].Value)))
                        {
                            this.ColumnCS3c5sSpec.LIne = this.e.DataGridView.CurrentRow.Cells["Line"].Value.ToString();
                            this.ColumnCS3c5sSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();

                            this.ColumnCS3c5sSpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();
                            this.ColumnCS3c5sSpec.Comment = this.e.DataGridView.CurrentRow.Cells["Comment"].Value.ToString();

                            this.ColumnCS3c5sSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCS3c5sSpec;
                    break;

                case "cst008":    //체크시트
                    this.Text = "Edit CheckSheet CsCheckSheetSpec";
                    this.ColumnCSCheckSheetSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSCheckSheetSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["CsCode"].Value)))
                        {
                            this.ColumnCSCheckSheetSpec.CsCode = this.e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();

                            this.ColumnCSCheckSheetSpec.CheckGroup = this.e.DataGridView.CurrentRow.Cells["CheckGroup"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.CheckItems = this.e.DataGridView.CurrentRow.Cells["CheckItems"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.DataUnit = this.e.DataGridView.CurrentRow.Cells["DataUnit"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.CheckPeriod = this.e.DataGridView.CurrentRow.Cells["CheckPeriod"].Value.ToString();

                            this.ColumnCSCheckSheetSpec.ValueMin = this.e.DataGridView.CurrentRow.Cells["ValueMin"].Value.ToString();
                            this.ColumnCSCheckSheetSpec.ValueMax = this.e.DataGridView.CurrentRow.Cells["ValueMax"].Value.ToString();

                            this.ColumnCSCheckSheetSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSCheckSheetSpec;
                    break;

                case "cst009":    //파라매터
                    this.Text = "Edit CheckSheet CsParameterCheckSpec";
                    this.ColumnCSParameterCheckSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSParameterCheckSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["CsCode"].Value)))
                        {
                            this.ColumnCSParameterCheckSpec.CsCode = this.e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString();
                            this.ColumnCSParameterCheckSpec.Seq = this.e.DataGridView.CurrentRow.Cells["Seq"].Value.ToString();

                            this.ColumnCSParameterCheckSpec.ParameterGroup = this.e.DataGridView.CurrentRow.Cells["ParameterGroup"].Value.ToString();
                            this.ColumnCSParameterCheckSpec.ParameterItems = this.e.DataGridView.CurrentRow.Cells["ParameterItems"].Value.ToString();
                            this.ColumnCSParameterCheckSpec.DataType = this.e.DataGridView.CurrentRow.Cells["DataType"].Value.ToString();
                            this.ColumnCSParameterCheckSpec.DataUnit = this.e.DataGridView.CurrentRow.Cells["DataUnit"].Value.ToString();

                            this.ColumnCSParameterCheckSpec.ValueMin = this.e.DataGridView.CurrentRow.Cells["ValueMin"].Value.ToString();
                            this.ColumnCSParameterCheckSpec.ValueMax = this.e.DataGridView.CurrentRow.Cells["ValueMax"].Value.ToString();

                            //this.ColumnCSParameterCheckSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSParameterCheckSpec;
                    break;

                case "cst002":    //CSSpec
                    this.Text = "Edit CheckSheet CSSpec";
                    this.ColumnCSSpec = new WiseM.Browser.EditColumn.CheckSheetEdit.EditColumnCSSpec();
                    //this.BtnEnableStatus(1);  // 추가 비활성, 수정,삭제만 가능
                    this.SearchBasicCode();
                    if (this.e.DataGridView.CurrentRow != null)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(this.e.DataGridView.CurrentRow.Cells["CsCode"].Value)))
                        {
                            this.ColumnCSSpec.CsCode = this.e.DataGridView.CurrentRow.Cells["CsCode"].Value.ToString();
                            this.ColumnCSSpec.LIne = this.e.DataGridView.CurrentRow.Cells["LIne"].Value.ToString();

                            this.ColumnCSSpec.CsName = this.e.DataGridView.CurrentRow.Cells["CsName"].Value.ToString();
                            this.ColumnCSSpec.Parts = this.e.DataGridView.CurrentRow.Cells["Parts"].Value.ToString();
                            this.ColumnCSSpec.Route = this.e.DataGridView.CurrentRow.Cells["Route"].Value.ToString();
                            this.ColumnCSSpec.RouteName = this.e.DataGridView.CurrentRow.Cells["RouteName"].Value.ToString();
                            this.ColumnCSSpec.Items = this.e.DataGridView.CurrentRow.Cells["Items"].Value.ToString();

                            this.ColumnCSSpec.Checker = this.e.DataGridView.CurrentRow.Cells["Checker"].Value.ToString();
                            this.ColumnCSSpec.Confirmer = this.e.DataGridView.CurrentRow.Cells["Confirmer"].Value.ToString();
                            this.ColumnCSSpec.KeepPeriod = this.e.DataGridView.CurrentRow.Cells["KeepPeriod"].Value.ToString();
                            this.ColumnCSSpec.Comment = this.e.DataGridView.CurrentRow.Cells["Comment"].Value.ToString();
                            this.ColumnCSSpec.Status = Convert.ToBoolean(this.e.DataGridView.CurrentRow.Cells["Status"].Value);

                            this.ColumnCSSpec.Updated = Convert.ToDateTime(this.e.DataGridView.CurrentRow.Cells["Updated"].Value);
                        }
                    }
                    else
                        this.BtnEnableStatus(0);

                    this.propertyGridEdit.SelectedObject = this.ColumnCSSpec;
                    break;
            }
        }

        private void BtnEnableStatus(int status)
        {
            switch (status)
            {
                case 0: // 수정,삭제 버튼 비 활성화
                    this.btnEdit.Enabled = false;
                    this.btnDelete.Enabled = false;
                    break;
                case 1: // 추가 버튼 미 활성화
                    this.btnInsert.Enabled = false;
                    break;
                default :
                    break;
            }
        }

        private void MaterialRoutingStatusChange(string workcenter, bool beforeStatus)
        {
            string updateQuery = null;
            if (beforeStatus == true)
            {
                updateQuery = "Update MaterialRouting Set Status = 0, Updated = GetDate() Where Workcenter = '" + workcenter + "'";
            }
            else
            {
                updateQuery = "Update MaterialRouting Set Status = 1, Updated = GetDate() Where Workcenter = '" + workcenter + "'";
            }
            this.e.DbAccess.ExecuteQuery(updateQuery);
        }

        private void ChangeRoutingMaterialRouting(string beforeRouting, string currentRouting, string currentWorkCenter, decimal cycleTime)
        {
            //MaterialRouting 에서 Routing 만 수정된 것이 전체 삭제 후 ERP 에서 조회 후 전체 Insert 로 변경
            //string updateQuery = "Update MaterialRouting Set Routing = '" + currentRouting + "', CycleTime = '" + cycleTime.ToString() + "'";
            //updateQuery += " , Updated = GetDate() Where Routing = '" + beforeRouting + "' And WorkCenter = '" + currentWorkCenter + "'";
            //this.e.DbAccess.ExecuteQuery(updateQuery);

            string deleteQuery = "Delete MaterialRouting";
            DbAccess.Default.ExecuteQuery(deleteQuery);

            string insertQuery = "insert into materialrouting (Material,Routing,WorkCenter,IssueSeq,CycleTime,MaterialName,ApplyDate,[Status],Updated,TransDate) ";
            insertQuery += " select Distinct W1.Code As Material,WC.Routing,WC.WorkCenter,row_number() Over(partition by W1.Code, WC.Routing ";
            insertQuery += " order by W1.Code, WC.routing, WC.workcenter ) As IssueSeq, ";
            insertQuery += " WC.CycleTime, W1.Text As MaterialName,'1900-01-01 00:00:00' As ApplyDate,1 As [Status],GetDate() As Updated,GetDate() As TransDate ";
            insertQuery += " from workcenter As WC ";
            insertQuery += " left Join ";
            insertQuery += " (Select Distinct ERP.Code,ERP.CompWorkcenter,MA.Text From ErpRecvBasic ERP, Material MA ";
            insertQuery += " Where ERP.TableType = 'WorkItem' And ERP.CrudType = 'C' And ERP.Code = MA.Material ) W1 ";
            insertQuery += " On W1.CompWorkCenter = WC.Routing ";
            insertQuery += " Where WC.Status = 1 And  WC.CycleTime is not null ";
            insertQuery += " Order by W1.Code, WC.Routing, WC.WorkCenter ";
            DbAccess.Default.ExecuteQuery(insertQuery);
        }

        // Insert, Edit, Delete & Close 버튼을 클릭한 경우
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to insert Data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;

            switch (this.e.Program.ToLower())
            {
                case "prm900":
                    if (this.NullCheckBasicCode(this.columnFctSpec.MaterialCode))
                    {
                        this.CheckMessageBox("FCT Spec");
                        return;
                    }

                    if (BasicCodeInfo.CheckFCTSpec(this.columnFctSpec.MaterialCode, this.e))
                    {
                        this.InsertQueryFCTSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("FCT Spec");
                        return;
                    }
                    break;
                case "prm001":
                    if (this.NullCheckBasicCode(this.columnDivision.Division))
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }

                    if (BasicCodeInfo.CheckDivision(this.columnDivision.Division, this.e))
                    {
                        this.insertQueryDivision();
                    }
                    else
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }
                    break;

                case "prm002":
                    if (this.NullCheckBasicCode(this.columnRouting.Routing))
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }

                    if (BasicCodeInfo.CheckRouting(this.columnRouting.Routing, this.e))
                    {
                        this.insertQueryRouting();
                    }
                    else
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }
                    break;

                case "prm003":
                    if (this.NullCheckBasicCode(this.columnWorkcenter.Workcenter))
                    {
                        this.CheckMessageBox("Workcenter");
                        return;
                    }

                    if (BasicCodeInfo.CheckWorkcenter(this.columnWorkcenter.Workcenter, this.e))
                    {
                        this.InsertQueryWorkcenter();
                    }
                    else
                    {
                        this.CheckMessageBox("Workcenter");
                        return;
                    }
                    break;

                case "prm101":
                    if (this.NullCheckBasicCode(this.columnMaterial.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (BasicCodeInfo.CheckMaterial(this.columnMaterial.Material, this.e))
                    {
                        this.InsertQueryMaterial();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm102":
                    
                    if (this.NullCheckBasicCode(this.columnMaterialMapping.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (BasicCodeInfo.CheckMaterialMapping(this.columnMaterialMapping.Material, this.e))
                    {
                        this.InsertQueryMaterialMapping();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm103":
                    if (BasicCodeInfo.CheckMaterialRouting(this.columnMaterialRouting.Material.Split('/')[0].Trim(), this.columnMaterialRouting.Routing.Split('/')[0]
                        ,  this.columnMaterialRouting.IssueSeq.ToString(), this.e))
                    {
                        this.InsertQueryMaterialRouting();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm105":
                    if (SettingData == "editlocation")
                    {
                        if (!BasicCodeInfo.CheckLocation(this.columnLocation.Location, this.e))
                        {
                            this.CheckMessageBox("Location");
                            return;
                        }
                        else
                        {
                            
                            this.InsertLocation();
                        }
                    }
                    else if (SettingData == "editlocationgroup")
                    {
                        if (!BasicCodeInfo.CheckLocationGroup(this.columnLocationGroup.LocationGroup, this.e))
                        {
                            this.CheckMessageBox("LocationGroup");
                            return;
                        }
                        else
                        {
                            this.InsertLocationGroup();
                        }
                    }
                    break;


                case "prm201":
                    if (this.NullCheckBasicCode(this.columnBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (BasicCodeInfo.CheckBad(this.columnBad.Bad, this.e))
                    {
                        this.InsertQueryBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm202":
                    if (this.NullCheckBasicCode(this.columnNotwork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (BasicCodeInfo.CheckNotwork(this.columnNotwork.Notwork, this.e))
                    {
                        this.InsertQueryNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm203":
                    if (this.NullCheckBasicCode(this.columnRoutingBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (!BasicCodeInfo.CheckBad(this.columnRoutingBad.Bad.Split('/')[0].Trim(), this.e)
                        && !BasicCodeInfo.CheckRouting(this.columnRoutingBad.Routing.Split('/')[0].Trim(), this.e))
                    {
                        this.InsertQueryRoutingBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm204":
                    if (this.NullCheckBasicCode(this.columnRoutingNotWork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (!BasicCodeInfo.CheckNotwork(this.columnRoutingNotWork.Notwork.Split('/')[0].Trim(), this.e)
                        && !BasicCodeInfo.CheckRouting(this.columnRoutingNotWork.Routing.Split('/')[0].Trim(), this.e))
                    {
                        this.InsertQueryRoutingNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm205":
                    if (this.NullCheckBasicCode(this.columnRepair.Repair))
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }

                    if (BasicCodeInfo.CheckBad(this.columnRepair.Repair, this.e))
                    {
                        this.InsertQueryRepair();
                    }
                    else
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }
                    break;

                case "prm302":
                    if (this.NullCheckBasicCode(this.columnWorker.Worker))
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }

                    if (BasicCodeInfo.CheckWorker(this.columnWorker.Worker, this.e))
                    {
                        this.InsertQueryWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }
                    break;

                case "prm303":
                    if (BasicCodeInfo.CheckClientWorker(this.columnWBTWorker.ClientID.Split('/')[0].Trim()
                        , this.columnWBTWorker.Worker.Split('/')[0].Trim(), this.e))
                    {
                        this.InsertQueryClientWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm304":
                    if (this.NullCheckBasicCode(this.columnWorkTeam.WorkTeam))
                    {
                        this.CheckMessageBox("WorkTeam");
                        return;
                    }

                    if (BasicCodeInfo.CheckWorkTeam(this.columnWorkTeam.WorkTeam, this.e))
                    {
                        this.InsertQueryWorkTeam();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkTeam");
                        return;
                    }
                    break;

                case "prm401":
                    if (this.NullCheckBasicCode(this.columnWorkCalendarStd.DayOfWeek) || this.NullCheckBasicCode(this.columnWorkCalendarStd.Workcenter)
                        || this.NullCheckBasicCode(this.columnWorkCalendarStd.WorkTeam))
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }

                    if (BasicCodeInfo.CheckPKWorkCalendarStd(this.columnWorkCalendarStd.DayOfWeek, this.columnWorkCalendarStd.WorkTeam.Split('/')[0].Trim(),
                        this.columnWorkCalendarStd.Workcenter.Split('/')[0].Trim(), this.e))
                    {
                        this.InsertQueryWorkCalendarStd();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm501":
                  
                    if (BasicCodeInfo.CheckFunctionError(this.columnFunctionChecked.Model, this.columnFunctionChecked.FuncError, this.e))
                    {
                        this.InsertQueryFucntionError();
                    }
                    else
                    {
                        this.CheckMessageBox("FunctionError");
                        return;
                    }
                    break;

                case "prd001":
                    if (BasicCodeInfo.CheckWorkOrder(this.columnOutputHist.WorkOrder, this.e))
                    {
                        this.InsertQueryOutputHist();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkOrder");
                        return;
                    }
                    break;

                case "qis001":
                    if (BasicCodeInfo.CheckWorkOrder(this.columnBadHist.WorkOrder, this.e))
                    {
                        this.InsertQueryBadHist();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkOrder");
                        return;
                    }
                    break;

                case "wpm001":
                    if (BasicCodeInfo.CheckWorkOrder(this.columnNotworkHist.WorkOrder, this.e))
                    {
                        this.InsertQueryNotworkHist();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkOrder");
                        return;
                    }
                    break;

                case "rep001":
                    if (BasicCodeInfo.CheckWorkOrder(this.columnRepairHist.WorkOrder, this.e))
                    {
                        this.InsertQueryRepairHist();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkOrder");
                        return;
                    }
                    break;

                //CheckSheet
                case "cst001":
                    if (this.NullCheckBasicCode(this.ColumnCSLineRoute.Route))
                    {
                        this.CheckMessageBox("CsLineRoute");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSLineRoute(this.ColumnCSLineRoute.LIne, this.ColumnCSLineRoute.Route, this.e))
                    {
                        this.InsertCSLineRoute();
                    }
                    else
                    {
                        this.CheckMessageBox("CsLineRoute");
                        return;
                    }
                    break;

                case "cst003": //일일정보 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSDailySpec.Route))
                    {
                        this.CheckMessageBox("CsDailySpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSDailySpec(this.ColumnCSDailySpec.LIne, this.ColumnCSDailySpec.Seq, this.ColumnCSDailySpec.Route, this.e))
                    {
                        this.InsertCSDailySpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CsDailySpec");
                        return;
                    }
                    break;
                case "cst004": //정기장비 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSPeriodicSpec.Route))
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSPeriodicSpec(this.ColumnCSPeriodicSpec.LIne, this.ColumnCSPeriodicSpec.Seq, this.ColumnCSPeriodicSpec.Route, this.e))
                    {
                        this.InsertCSPeriodicSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }
                    break;
                case "cst005": //설비예방보전 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSPreventSpec.Route))
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSPreventSpec(this.ColumnCSPreventSpec.LIne, this.ColumnCSPreventSpec.Seq, this.ColumnCSPreventSpec.Route, this.e))
                    {
                        this.InsertCSPreventSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    break;
                case "cst007": //연간 오버홀 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSOverhaulSpec.Route))
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSOverhaulSpec(this.ColumnCSOverhaulSpec.LIne, this.ColumnCSOverhaulSpec.Seq, this.ColumnCSOverhaulSpec.Route, this.e))
                    {
                        this.InsertCSOverhaulSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }
                    break;
                case "cst006": //3정오행 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCS3c5sSpec.Seq))
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCS3c5sSpec(this.ColumnCS3c5sSpec.LIne, this.ColumnCS3c5sSpec.Seq, this.e))
                    {
                        this.InsertCS3c5sSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }
                    break;
                case "cst008": //체크시트
                    if (this.NullCheckBasicCode(this.ColumnCSCheckSheetSpec.Seq))
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSCheckSheetSpec(this.ColumnCSCheckSheetSpec.CsCode, this.ColumnCSCheckSheetSpec.Seq, this.e))
                    {
                        this.InsertCSCheckSheetSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }
                    break;

                case "cst009": //파라매터
                    if (this.NullCheckBasicCode(this.ColumnCSParameterCheckSpec.Seq))
                    {
                        this.CheckMessageBox("CsParameterCheckSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSParameterCheckSpec(this.ColumnCSParameterCheckSpec.CsCode, this.ColumnCSParameterCheckSpec.Seq, this.e))
                    {
                        this.InsertCSParameterCheckSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CsParameterCheckSpec");
                        return;
                    }
                    break;

                case "cst002": //csspec
                    if (this.NullCheckBasicCode(this.ColumnCSSpec.CsCode))
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }
                    if (BasicCodeInfo.CheckCSSpec(this.ColumnCSSpec.CsCode, this.e))
                    {
                        this.InsertCSSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }
                    break;
            }
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to edit Data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                , MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            switch (this.e.Program.ToLower())
            {
                case "prm900":
                    this.EditQueryFCTSpec();
                    break;
                case "prd001":
                    this.EditQueryOutputHist();
                    break;

                case "qis001":
                    this.EditQueryBadHist();
                    break;

                case "rep001":
                    this.EditQueryRepairHist();
                    break;

                case "wpm001":
                    this.EditQueryNotworkHist();
                    break;

                case "prm001":
                    if (this.NullCheckBasicCode(this.columnDivision.Division))
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }

                    if (!BasicCodeInfo.CheckDivision(this.columnDivision.Division, this.e))
                    {
                        this.EditQueryDivision();
                    }
                    else
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }
                    break;

                case "prm002":
                    if (this.NullCheckBasicCode(this.columnRouting.Routing))
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }

                    if (!BasicCodeInfo.CheckRouting(this.columnRouting.Routing, this.e))
                    {
                        this.EditQueryRouting();
                    }
                    else
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }
                    break;

                case "prm003":
                    if (this.NullCheckBasicCode(this.columnWorkcenter.Workcenter))
                    {
                        this.CheckMessageBox("Workcenter");
                        return;
                    }

                    if (!BasicCodeInfo.CheckWorkcenter(this.columnWorkcenter.Workcenter, this.e))
                    {
                        this.EditQueryWorkcenter();
                    }
                    else
                    {
                        this.CheckMessageBox("Workcenter");
                        return;
                    }
                    break;

                    //if (!BasicCodeInfo.CheckWorkcenter(this.columnWorkcenter.Workcenter, this.e))
                    //{
                    //    //Status Change 수정부분//
                    //    string beforeStatusQuery = "Select Status From WorkCenter Where WorkCenter = '" + this.columnWorkcenter.Workcenter + "'";
                    //    bool beforeStatus = Convert.ToBoolean(this.e.DbAccess.ExecuteScalar(beforeStatusQuery));
                    //    if (beforeStatus != this.columnWorkcenter.Status)
                    //    {
                    //        this.MaterialRoutingStatusChange(this.columnWorkcenter.Workcenter, beforeStatus);
                    //    }
                    //    //////////////////////////
                    //    //MaterialRouting 수정부분//
                    //    string beforeRoutingQuery = "Select Routing From WorkCenter Where WorkCenter = '" + this.columnWorkcenter.Workcenter + "'";
                    //    string beforeRouting = this.e.DbAccess.ExecuteScalar(beforeRoutingQuery) as string;
                    //    if (beforeRouting.Trim() != this.columnWorkcenter.Routing.Split('/')[0].Trim())
                    //    {
                    //        this.EditQueryWorkcenter();
                    //        this.ChangeRoutingMaterialRouting(beforeRouting.Trim(), this.columnWorkcenter.Routing.Split('/')[0].Trim()
                    //            , this.columnWorkcenter.Workcenter, this.columnWorkcenter.CycleTime);
                    //    }
                    //    else
                    //    {
                    //        this.EditQueryWorkcenter();
                    //    }
                    //}
                    //else
                    //{
                    //    this.CheckMessageBox("Workcenter");
                    //    return;
                    //}

                    //break;

                case "prm101":
                    if (this.NullCheckBasicCode(this.columnMaterial.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (!BasicCodeInfo.CheckMaterial(this.columnMaterial.Material, this.e))
                    {
                        this.EditQueryMaterial();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm102":
                    if (this.NullCheckBasicCode(this.columnMaterialMapping.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (!BasicCodeInfo.CheckMaterialMapping(this.columnMaterialMapping.Material, this.e))
                    {
                        this.EditQueryMaterialMapping();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm103":
                    if (!BasicCodeInfo.CheckMaterialRouting(this.columnMaterialRouting.Material.Split('/')[0].Trim(), this.columnMaterialRouting.Routing.Split('/')[0]
                        ,  this.columnMaterialRouting.IssueSeq.ToString(), this.e))
                    {
                        this.EditQueryMaterialRouting();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm104":
                    if (this.NullCheckBasicCode(this.columnRawMaterial.RawMaterial))
                    {
                        this.CheckMessageBox("RawMaterial");
                        return;
                    }

                    if (!BasicCodeInfo.CheckRawMaterial(this.columnRawMaterial.RawMaterial, this.e))
                    {
                        this.EditQueryRawMaterial();
                    }
                    else
                    {
                        this.CheckMessageBox("RawMaterial");
                        return;
                    }
                    break;

                case "prm105":
                    if (SettingData == "editlocation")
                    {
                        if (this.NullCheckBasicCode(this.columnLocation.Location))
                        {
                            this.CheckMessageBox("Location");
                            return;
                        }

                        if (!BasicCodeInfo.CheckLocation(this.columnLocation.Location, this.e))
                        {
                            this.EditQueryLocation();
                        }
                        else
                        {
                            this.CheckMessageBox("Location");
                            return;
                        }
                    }
                    else if (SettingData == "editlocationgroup")
                    {
                        if (this.NullCheckBasicCode(this.columnLocationGroup.LocationGroup))
                        {
                            this.CheckMessageBox("LocationGroup");
                            return;
                        }

                        if (!BasicCodeInfo.CheckLocationGroup(this.columnLocationGroup.LocationGroup, this.e))
                        {
                            this.EditQueryLocationGroup();
                        }
                        else
                        {
                            this.CheckMessageBox("LocationGroup");
                            return;
                        }
                    }
                    break;

                case "prm201":
                    if (this.NullCheckBasicCode(this.columnBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (!BasicCodeInfo.CheckBad(this.columnBad.Bad, this.e))
                    {
                        this.EditQueryBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm202":
                    if (this.NullCheckBasicCode(this.columnNotwork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (!BasicCodeInfo.CheckNotwork(this.columnNotwork.Notwork, this.e))
                    {
                        this.EditQueryNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm203":
                    if (this.NullCheckBasicCode(this.columnRoutingBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (!BasicCodeInfo.CheckBad(this.columnRoutingBad.Bad.Split('/')[0].Trim(), this.e)
                        && !BasicCodeInfo.CheckRouting(this.columnRoutingBad.Routing.Split('/')[0].Trim(), this.e))
                    {
                        this.EditQueryRoutingBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm204":
                    if (this.NullCheckBasicCode(this.columnRoutingNotWork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (!BasicCodeInfo.CheckNotwork(this.columnRoutingNotWork.Notwork.Split('/')[0].Trim(), this.e)
                        && !BasicCodeInfo.CheckRouting(this.columnRoutingNotWork.Routing.Split('/')[0].Trim(), this.e))
                    {
                        this.EditQueryRoutingNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm205":
                    if (this.NullCheckBasicCode(this.columnRepair.Repair))
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }

                    if (!BasicCodeInfo.CheckRepair(this.columnRepair.Repair, this.e))
                    {
                        this.EditQueryRepair();
                    }
                    else
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }
                    break;

                case "prm302":
                    if (this.NullCheckBasicCode(this.columnWorker.Worker))
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }

                    if (!BasicCodeInfo.CheckWorker(this.columnWorker.Worker, this.e))
                    {
                        this.EditQueryWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }
                    break;

                case "prm303":
                    if (!BasicCodeInfo.CheckClientWorker(this.targetClientID, this.targetWorker, this.e))
                    {
                        this.EditQueryClientWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm304":
                    if (this.NullCheckBasicCode(this.columnWorkTeam.WorkTeam))
                    {
                        this.CheckMessageBox("WorkTeam");
                        return;
                    }

                    if (!BasicCodeInfo.CheckWorkTeam(this.columnWorkTeam.WorkTeam, this.e))
                    {
                        this.EditQueryWorkTeam();
                    }
                    else
                    {
                        this.CheckMessageBox("WorkTeam");
                        return;
                    }
                    break;

                case "prm401":
                    if (this.NullCheckBasicCode(this.columnWorkCalendarStd.DayOfWeek) || this.NullCheckBasicCode(this.columnWorkCalendarStd.Workcenter)
                        || this.NullCheckBasicCode(this.columnWorkCalendarStd.WorkTeam))
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }

                    if (!BasicCodeInfo.CheckPKWorkCalendarStd(this.columnWorkCalendarStd.DayOfWeek, this.columnWorkCalendarStd.WorkTeam.Split('/')[0].Trim(),
                        this.columnWorkCalendarStd.Workcenter.Split('/')[0].Trim(), this.e))
                    {
                        this.EditQueryWorkCalendarStd();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm501":
                    if (this.NullCheckBasicCode(this.columnFunctionChecked.FuncError) || this.NullCheckBasicCode(this.columnFunctionChecked.Model))
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }

                    if (!BasicCodeInfo.CheckFunctionError(this.columnFunctionChecked.Model, this.columnFunctionChecked.FuncError, this.e))
                    {
                        this.EditQueryFucntionError();
                    }
                    else
                    {
                        this.CheckMessageBox("Model");
                        return;
                    }
                    break;


                //CheckSheet - Update
                case "cst001":
                    if (this.NullCheckBasicCode(this.ColumnCSLineRoute.Route))
                    {
                        this.CheckMessageBox("LineRoute");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSLineRoute(this.ColumnCSLineRoute.LIne, this.ColumnCSLineRoute.Route, this.e))
                    {
                        this.EditQueryCSLineRoute();
                    }
                    else
                    {
                        this.CheckMessageBox("LineRoute");
                        return;
                    }
                    break;
                //일일장비 사양관리
                case "cst003":
                    if (this.NullCheckBasicCode(this.ColumnCSDailySpec.Route))
                    {
                        this.CheckMessageBox("DailySpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSDailySpec(this.ColumnCSDailySpec.LIne, this.ColumnCSDailySpec.Seq, this.ColumnCSDailySpec.Route, this.e))
                    {
                        this.EditQueryCSDailySpec();
                    }
                    else
                    {
                        this.CheckMessageBox("DailySpec");
                        return;
                    }
                    break;
                //정기장비 사양관리
                case "cst004":
                    if (this.NullCheckBasicCode(this.ColumnCSPeriodicSpec.Route))
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSPeriodicSpec(this.ColumnCSPeriodicSpec.LIne, this.ColumnCSPeriodicSpec.Seq, this.ColumnCSPeriodicSpec.Route, this.e))
                    {
                        this.EditQueryCSPeriodicSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }
                    break;
                //설비예방보전 사양관리
                case "cst005":
                    if (this.NullCheckBasicCode(this.ColumnCSPreventSpec.Route))
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSPreventSpec(this.ColumnCSPreventSpec.LIne, this.ColumnCSPreventSpec.Seq, this.ColumnCSPreventSpec.Route, this.e))
                    {
                        this.EditQueryCSPreventSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    break;
                //연간 오버홀 사양관리
                case "cst007":
                    if (this.NullCheckBasicCode(this.ColumnCSOverhaulSpec.Seq))
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSOverhaulSpec(this.ColumnCSOverhaulSpec.LIne, this.ColumnCSOverhaulSpec.Seq, this.ColumnCSOverhaulSpec.Route, this.e))
                    {
                        this.EditQueryCSOverhaulSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSOverhaulSpec");
                        return;
                    }
                    break;
                //3정5행 사양관리
                case "cst006":
                    if (this.NullCheckBasicCode(this.ColumnCS3c5sSpec.Seq))
                    {
                        this.CheckMessageBox("CS3c5sSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCS3c5sSpec(this.ColumnCS3c5sSpec.LIne, this.ColumnCS3c5sSpec.Seq, this.e))
                    {
                        this.EditQueryCS3c5sSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CS3c5sSpec");
                        return;
                    }
                    break;
                //체크시트 사양관리
                case "cst008":
                    if (this.NullCheckBasicCode(this.ColumnCSCheckSheetSpec.Seq))
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSCheckSheetSpec(this.ColumnCSCheckSheetSpec.CsCode, this.ColumnCSCheckSheetSpec.Seq, this.e))
                    {
                        this.EditQueryCSCheckSheetSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }
                    break;
                //파라매터 사양관리
                case "cst009":
                    if (this.NullCheckBasicCode(this.ColumnCSParameterCheckSpec.Seq))
                    {
                        this.CheckMessageBox("CSParameterCheckSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSParameterCheckSpec(this.ColumnCSParameterCheckSpec.CsCode, this.ColumnCSParameterCheckSpec.Seq, this.e))
                    {
                        this.EditQueryCSParameterCheckSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSParameterCheckSpec");
                        return;
                    }
                    break;

                //CSspec 사양관리
                case "cst002":
                    if (this.NullCheckBasicCode(this.ColumnCSSpec.CsCode))
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }

                    if (!BasicCodeInfo.CheckCSSpec(this.ColumnCSSpec.CsCode, this.e))
                    {
                        this.EditQueryCSSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }
                    break;
            }
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete Data ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning
                , MessageBoxDefaultButton.Button1) == DialogResult.No)
            {
                return;
            }

            switch (this.e.Program.ToLower())
            {
                case "prm900":
                    this.DeleteQueryFCTSpec();
                    break;
                case "prd001":
                    this.DeleteQueryOutputHist();
                    break;

                case "qis001":
                    this.DeleteQueryBadHist();
                    break;

                case "rep001":
                    this.DeleteQueryRepairHist();
                    break;

                case "wpm001":
                    this.DeleteQueryNotworkHist();
                    break;

                case "prm001":
                    if (this.NullCheckBasicCode(this.columnDivision.Division))
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }

                    if (BasicCodeInfo.CheckDivisionInActiveJob(this.columnDivision.Division, this.e))
                    {
                        this.DeleteQueryDivision();
                    }
                    else
                    {
                        this.CheckMessageBox("Division");
                        return;
                    }
                    break;

                case "prm002":
                    if (this.NullCheckBasicCode(this.columnRouting.Routing))
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }

                    if (BasicCodeInfo.CheckRoutingInActiveJob(this.columnRouting.Routing, this.e))
                    {
                        this.DeleteQueryRouting();
                    }
                    else
                    {
                        this.CheckMessageBox("Routing");
                        return;
                    }
                    break;

                case "prm003":
                    if (this.NullCheckBasicCode(this.columnWorkcenter.Workcenter))
                    {
                        this.CheckMessageBox("Workcenter");
                        return;
                    }

                    if (BasicCodeInfo.CheckWorkcenterInWorkcalendar(this.columnWorkcenter.Workcenter, this.e) == false &&
                        BasicCodeInfo.CheckWorkcenterInWorkOrder(this.columnWorkcenter.Workcenter, this.e) == false)
                    {
                        this.DeleteQueryMaterialRoutingByWorkcenter();
                        this.DeleteQueryWorkcenter();
                    }
                    else
                    {
                        //Get this if you want to do this first
                        string messageStr = "WorkOrder or WorkCalendar Data have currented workcenter.\r\nGet this if you want to do delete "
                            + "workorder and workcalendar data first.";
                        MessageBox.Show(messageStr,"Information",MessageBoxIcon.Information);
                        return;
                    }
                    break;

                case "prm101":
                    if (this.NullCheckBasicCode(this.columnMaterial.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (BasicCodeInfo.CheckMaterialInOutputHist(this.columnMaterial.Material, this.e))
                    {
                        this.DeleteQueryMaterial();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm102":
                    if (this.NullCheckBasicCode(this.columnMaterialMapping.Material))
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }

                    if (!BasicCodeInfo.CheckMaterialMapping(this.columnMaterialMapping.Material, this.e))
                    {
                        this.DeleteQueryMaterialMapping();
                    }
                    else
                    {
                        this.CheckMessageBox("Material");
                        return;
                    }
                    break;

                case "prm103":
                    //if (!BasicCodeInfo.CheckMaterialRouting(this.columnMaterialRouting.Material.Split('/')[0].Trim(), this.columnMaterialRouting.Routing.Split('/')[0]
                    //    ,  this.columnMaterialRouting.IssueSeq.ToString(), this.e))
                    //{
                        this.DeleteQueryMaterialRouting();
                    //}
                    //else
                    //{
                    //    this.CheckMessageBox("PRIMARY KEY");
                    //    return;
                    //}
                    break;

                case "prm201":
                    if (this.NullCheckBasicCode(this.columnBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (BasicCodeInfo.CheckBadInBadHist(this.columnBad.Bad, this.e))
                    {
                        this.DeleteQueryBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm202":
                    if (this.NullCheckBasicCode(this.columnNotwork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (BasicCodeInfo.CheckNotworkInNotworkHist(this.columnNotwork.Notwork, this.e))
                    {
                        this.DeleteQueryNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm203":
                    if (this.NullCheckBasicCode(this.columnRoutingBad.Bad))
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }

                    if (BasicCodeInfo.CheckBadInBadHist(this.columnRoutingBad.Bad, this.e))
                    {
                        this.DeleteQueryRoutingBad();
                    }
                    else
                    {
                        this.CheckMessageBox("Bad");
                        return;
                    }
                    break;

                case "prm204":
                    if (this.NullCheckBasicCode(this.columnRoutingNotWork.Notwork))
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }

                    if (BasicCodeInfo.CheckNotworkInNotworkHist(this.columnRoutingNotWork.Notwork, this.e))
                    {
                        this.DeleteQueryRoutingNotwork();
                    }
                    else
                    {
                        this.CheckMessageBox("Notwork");
                        return;
                    }
                    break;

                case "prm205":
                    if (this.NullCheckBasicCode(this.columnRepair.Repair))
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }

                    if (BasicCodeInfo.CheckBadInRepairHist(this.columnRepair.Repair, this.e))
                    {
                        this.DeleteQueryRepair();
                    }
                    else
                    {
                        this.CheckMessageBox("Repair");
                        return;
                    }
                    break;

                case "prm302":
                    if (this.NullCheckBasicCode(this.columnWorker.Worker))
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }

                    if (BasicCodeInfo.CheckWorkerInWorkerOutput(this.columnWorker.Worker, this.e))
                    {
                        this.DeleteQueryWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("Worker");
                        return;
                    }
                    break;

                case "prm303":
                    if (!BasicCodeInfo.CheckClientWorker(this.columnWBTWorker.ClientID.Split('/')[0].Trim()
                        , this.columnWBTWorker.Worker.Split('/')[0].Trim(), this.e))
                    {
                        this.DeleteQueryClientWorker();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;

                case "prm304":
                    if (this.NullCheckBasicCode(this.columnWorkTeam.WorkTeam))
                    {
                        this.CheckMessageBox("WorkTeam");
                        return;
                    }

                    //if (BasicCodeInfo.CheckWorkTeamInWorkCalendarStd(this.columnWorkTeam.WorkTeam, this.e))
                    //{
                    //    this.DeleteQueryWorkTeam();
                    //}
                    //else
                    //{
                    //    this.CheckMessageBox("WorkTeam");
                    //}

                    this.DeleteQueryWorkTeam();

                    break;

                case "prm401":
                    if (this.NullCheckBasicCode(this.columnWorkCalendarStd.DayOfWeek) || this.NullCheckBasicCode(this.columnWorkCalendarStd.Workcenter)
                        || this.NullCheckBasicCode(this.columnWorkCalendarStd.WorkTeam))
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }

                    if (!BasicCodeInfo.CheckPKWorkCalendarStd(this.columnWorkCalendarStd.DayOfWeek, this.columnWorkCalendarStd.WorkTeam.Split('/')[0].Trim(),
                        this.columnWorkCalendarStd.Workcenter.Split('/')[0].Trim(), this.e))
                    {
                        this.DeleteQueryWorkCalendarStd();
                    }
                    else
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }
                    break;


                case "prm501":
                    if (this.NullCheckBasicCode(this.columnFunctionChecked.FuncError) || this.NullCheckBasicCode(this.columnFunctionChecked.Model))
                    {
                        this.CheckMessageBox("PRIMARY KEY");
                        return;
                    }

                    if (!BasicCodeInfo.CheckFunctionError(this.columnFunctionChecked.Model, this.columnFunctionChecked.FuncError, this.e))
                    {
                        this.DeleteQueryFucntionError();
                    }
                    else
                    {
                        this.CheckMessageBox("Model");
                        return;
                    }
                    break;


                //CheckSheet - Delete
                case "cst001":    //체크시트 라인정보
                    if (this.NullCheckBasicCode(this.ColumnCSLineRoute.Route))
                    {
                        this.CheckMessageBox("LineRoute");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSLineRoute(this.ColumnCSLineRoute.LIne, this.ColumnCSLineRoute.Route, this.e))
                    {
                        this.DeleteQueryCSLineRoute();
                    }
                    else
                    {
                        this.CheckMessageBox("LineRoute");
                        return;
                    }
                    break;

                case "cst003":    //일일장비 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSDailySpec.Route))
                    {
                        this.CheckMessageBox("CSDailySpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSDailySpec(this.ColumnCSDailySpec.LIne, this.ColumnCSDailySpec.Seq, this.ColumnCSDailySpec.Route, this.e))
                    {
                        this.DeleteQueryCSDailySpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSDailySpec");
                        return;
                    }
                    break;
                case "cst004":    //정기장비 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSPeriodicSpec.Route))
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSPeriodicSpec(this.ColumnCSPeriodicSpec.LIne, this.ColumnCSPeriodicSpec.Seq, this.ColumnCSPeriodicSpec.Route, this.e))
                    {
                        this.DeleteQueryCSPeriodicSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPeriodicSpec");
                        return;
                    }
                    break;
                case "cst005":    //설비예바보전 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSPreventSpec.Route))
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSPreventSpec(this.ColumnCSPreventSpec.LIne, this.ColumnCSPreventSpec.Seq, this.ColumnCSPreventSpec.Route, this.e))
                    {
                        this.DeleteQueryCSPreventSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    break;
                case "cst007":    //연간 오버홀 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSOverhaulSpec.Route))
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSOverhaulSpec(this.ColumnCSOverhaulSpec.LIne, this.ColumnCSOverhaulSpec.Seq, this.ColumnCSOverhaulSpec.Route, this.e))
                    {
                        this.DeleteQueryCSOverhaulSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    break;
                case "cst006":    //3정5행 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCS3c5sSpec.Seq))
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCS3c5sSpec(this.ColumnCS3c5sSpec.LIne, this.ColumnCS3c5sSpec.Seq, this.e))
                    {
                        this.DeleteQueryCS3c5sSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSPreventSpec");
                        return;
                    }
                    break;
                case "cst008":    //체크시트 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSCheckSheetSpec.Seq))
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSCheckSheetSpec(this.ColumnCSCheckSheetSpec.CsCode, this.ColumnCSCheckSheetSpec.Seq, this.e))
                    {
                        this.DeleteQueryCSCheckSheetSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSCheckSheetSpec");
                        return;
                    }
                    break;

                case "cst009":    //파라매터 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSParameterCheckSpec.Seq))
                    {
                        this.CheckMessageBox("CSParameterCheckSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSParameterCheckSpec(this.ColumnCSParameterCheckSpec.CsCode, this.ColumnCSParameterCheckSpec.Seq, this.e))
                    {
                        this.DeleteQueryCSParameterCheckSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("CSParameterCheckSpec");
                        return;
                    }
                    break;

                case "cst002":    //Csspec 사양관리
                    if (this.NullCheckBasicCode(this.ColumnCSSpec.CsCode))
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }
                    if (!BasicCodeInfo.CheckCSSpec(this.ColumnCSSpec.CsCode, this.e))
                    {
                        this.DeleteQueryCSSpec();
                    }
                    else
                    {
                        this.CheckMessageBox("ColumnCSSpec");
                        return;
                    }
                    break;

            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // SQL Query 
        private void RunExecuteQuery(string query, string message)
        {
            int status = DbAccess.Default.ExecuteQuery(query);
            if (status == 1)
            {
                MessageBox.Show(message + " Data is over.\r\nPlease Refresh Data.","Notice",MessageBoxIcon.None);
            }
        }

        #region 각 기준정보별 Edit/Insert/Delete Procedure
        // ClientWorker 정보 수정/삭제/추가
        private void InsertQueryClientWorker()
        {
            string insertQuery = "INSERT INTO [ClientWorker] ";
            insertQuery += " ([ClientId] ";
            insertQuery += " ,[Worker] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated]) ";
            insertQuery += " VALUES ";
            insertQuery += " (N'" + this.columnWBTWorker.ClientID.Split('/')[0].Trim() + "'";
            insertQuery += " ,N'" + this.columnWBTWorker.Worker.Split('/')[0].Trim() + "' ";
            if (this.columnWBTWorker.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            } 
            insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryClientWorker()
        {
            string editQuery = "UPDATE [ClientWorker] ";
            editQuery += " SET [ClientId] = N'" + this.columnWBTWorker.ClientID.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Worker] = N'" + this.columnWBTWorker.Worker.Split('/')[0].Trim() + "' ";
            if (this.columnWBTWorker.Status == false)
            {
                editQuery += " ,[Status] =  0 ";
            }
            else
            {
                editQuery += " ,[Status] =  1 ";
            } 
            editQuery += " ,[Updated] = GetDate() ";
            editQuery += " WHERE ClientId = '" + this.targetClientID + "' And Worker = '" + this.targetWorker + "' ";
            this.RunExecuteQuery(editQuery, "Edit");

        }

        private void DeleteQueryClientWorker()
        {
            string deleteQuery = "Delete From ClientWorker Where ClientId = '" + this.columnWBTWorker.ClientID.Split('/')[0].Trim() 
                + "' And Worker = '" + this.columnWBTWorker.Worker.Split('/')[0].Trim() + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // MaterialRouting 정보 수정/삭제/추가
        private void InsertQueryMaterialRouting()
        {
            string SearchMaterial = " Select Max(IssueSeq) IssueSeq From MaterialRouting where Material = '" + this.columnMaterialRouting.Material.Split('/')[0].Trim() + "' order by IssueSeq ";
            DataTable dt = DbAccess.Default.GetDataTable(SearchMaterial);
            string insertQuery = "INSERT INTO [MaterialRouting] ";
            insertQuery += " ([Material] ";
            insertQuery += " ,[Routing] ";
            insertQuery += " ,[WorkCenter] ";
            insertQuery += " ,[IssueSeq] ";
            insertQuery += " ,[ApplyDate] ";
            insertQuery += " ,[MaterialName] ";
            insertQuery += " ,[CycleTime] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[TransDate]) ";
            insertQuery += " VALUES ";
            insertQuery += " (N'" + this.columnMaterialRouting.Material.Split('/')[0].Trim() + "' ";
            insertQuery += " ,N'" + this.columnMaterialRouting.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,Null ";
            if (string.IsNullOrEmpty(dt.Rows[0]["IssueSeq"].ToString()))
            {
                insertQuery += " , '1' ";
            }
            else
            {
                insertQuery += " ,'" + Convert.ToInt32(Convert.ToInt32(this.columnMaterialRouting.IssueSeq) + 1) + "' ";
            }
            
            insertQuery += " ,'" + this.columnMaterialRouting.ApplyDate +"' ";
            if (this.columnMaterialRouting.Material.IndexOf('/') <= 0)
            {
                insertQuery += " ,(Select Text From Material where Material = '" + this.columnMaterialRouting.Material.Split('/')[0].Trim() + "')   ";

            }
            else
            {
                insertQuery += " ,'" + this.columnMaterialRouting.Material.Split('/')[1].Trim() + "' ";
            }
            insertQuery += " ,'" + this.columnMaterialRouting.CycleTime + "' ";
            if (this.columnMaterialRouting.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            } 
            insertQuery += " ,GetDate() ";
            insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");

        }

        private void InsertLocation()
        {
            string insertQuery = "INSERT INTO RM_Location ";
            insertQuery += "(Location_Group , Location , Location_Text , Kind , Bunch , Updated) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnLocation.LocationGroup.Split('/')[0].ToString() + "'";
            insertQuery += " ,'" + this.columnLocation.Location + "' ";
            insertQuery += " ,'" + this.columnLocation.LocationText + "' ";
            insertQuery += " ,'" + this.columnLocation.Kind + "' ";
            insertQuery += " ,'" + this.columnLocation.Bunch + "', Getdate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void InsertLocationGroup()
        {
            string insertQuery = "INSERT INTO Rm_Location_Group ";
            insertQuery += "(Location_Group ,Location_Group_Text, Updated) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnLocationGroup.LocationGroup + "'";
            insertQuery += " ,'" + this.columnLocationGroup.LocationGroupText + "', Getdate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }


        private void EditQueryMaterialRouting()
        {
            string editQuery = "UPDATE [MaterialRouting] ";
            editQuery += " SET  ";
            editQuery += " [CycleTime] = N'" + this.columnMaterialRouting.CycleTime + "' ";
            if (this.columnMaterialRouting.Status == false)
            {
                editQuery += " ,[Status] =  0 ";
            }
            else
            {
                editQuery += " ,[Status] =  1 ";
            } 
            editQuery += " ,[Updated] = GetDate() ";
            editQuery += " WHERE  Material = '" + this.columnMaterialRouting.Material.Split('/')[0].Trim() + "' And Routing = '" + this.columnMaterialRouting.Routing.Split('/')[0].Trim() 
                + "' And  IssueSeq = '" + this.columnMaterialRouting.IssueSeq + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryMaterialRouting()
        {
            string deleteQuery = "Delete From MaterialRouting";
            deleteQuery += " WHERE  Material = '" + this.columnMaterialRouting.Material.Split('/')[0].Trim() + "' And Routing = '" + this.columnMaterialRouting.Routing.Split('/')[0].Trim()
                + "' And  IssueSeq = '" + this.columnMaterialRouting.IssueSeq + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Material Mapping 정보 수정/삭제/추가
        private void InsertQueryMaterialMapping()
        {
            string insertQuery = "INSERT INTO MaterialMapping ";
            insertQuery += "(Material, Model, Product, ErpMaterial, CustomerMaterial, CellModel, CellCode, CellQty,CorePackCode,  Status,Cpkcheck,Rev,Updated) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnMaterialMapping.Material + "'";
            insertQuery += " ,'" + this.columnMaterialMapping.Model + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.Product + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.ErpMaterial + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.CustomerMaterial + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.CellModel + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.CellCode + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.CellQty + "' ";
            insertQuery += " ,'" + this.columnMaterialMapping.CorePackCode + "' ";
            if (this.columnMaterialMapping.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            if (this.columnMaterialMapping.CpkCheck == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnMaterialMapping.Rev + "',Getdate() ";
            insertQuery += ")";
            //insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");

        }

        private void EditQueryMaterialMapping()
        {
            string editQuery = "UPDATE MaterialMapping SET "; 
            editQuery += " Model = '" + this.columnMaterialMapping.Model + "', ";
            editQuery += " CycleTime = '" + this.columnMaterialMapping.CycleTime + "', ";
            editQuery += " Product = '" + this.columnMaterialMapping.Product + "', ";
            editQuery += " BarCode_Digit = '" + this.columnMaterialMapping.BarCode_Digit + "', ";
            editQuery += " BarCode_Check = '" + this.columnMaterialMapping.BarCode_Check + "', ";
			editQuery += " Box_Digit = '" + this.columnMaterialMapping.Box_digit + "', ";
			editQuery += " BoxCode_Check = '" + this.columnMaterialMapping.BoxCode_Check + "', ";
			editQuery += " Box_Qty = '" + this.columnMaterialMapping.BoxQty + "', ";
			editQuery += " ErpMaterial = '" + this.columnMaterialMapping.ErpMaterial + "', ";
            editQuery += " CustomerMaterial = '" + this.columnMaterialMapping.CustomerMaterial + "', ";
            editQuery += " CellModel = '" + this.columnMaterialMapping.CellModel + "', ";
            editQuery += " CellCode = '" + this.columnMaterialMapping.CellCode + "', ";
            editQuery += " CellQty = '" + this.columnMaterialMapping.CellQty + "', ";
            editQuery += " Rev = '" + this.columnMaterialMapping.Rev + "', ";
            if (this.columnMaterialMapping.Status == false)
            {
                editQuery += " [Status] =  0, ";
            }
            else
            {
                editQuery += " [Status] =  1, ";
            }
            if (this.columnMaterialMapping.CpkCheck == false)
            {
                editQuery += " [CpkCheck] =  0 ";
            }
            else
            {
                editQuery += " [CpkCheck] =  1 ";
            }
            
            editQuery += ", [Updated] = GetDate() ";
            editQuery += " WHERE Material ='" + this.columnMaterialMapping.Material + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryMaterialMapping()
        {
            string deleteQuery = "Delete From MaterialMapping WHERE Material = '" + this.columnMaterialMapping.Material + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // WorkCenter Code 수정/삭제/추가
        private void InsertQueryWorkcenter()
        {
            string insertQuery = "INSERT INTO [WorkCenter] ";
            insertQuery += " ([Workcenter] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[Division] ";
            insertQuery += " ,[Routing] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[WorkStatus] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[SdiLine] ";
            insertQuery += " ,[ViewSeq] ";
            insertQuery += " ,[AllowNotwork]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnWorkcenter.Workcenter + "' ";
            insertQuery += " ,N'" + this.columnWorkcenter.Text + "' ";
            insertQuery += " ,N'" + this.columnWorkcenter.TextKor + "' ";
            insertQuery += " ,'" + this.columnWorkcenter.Division.Split('/')[0].Trim() + "'";
            insertQuery += " ,'" + this.columnWorkcenter.Routing.Split('/')[0].Trim() + "'";
            if (this.columnWorkcenter.Status == false)
                insertQuery += " , 0 ";
            else
                insertQuery += " , 1 ";
            if (this.columnWorkcenter.WorkStatus == false)
                insertQuery += " , 0 ";
            else
                insertQuery += " , 1 ";
            insertQuery += " ,GetDate() ";
            insertQuery += " ,'" + this.columnWorkcenter.BunchWorkcenter.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnWorkcenter.Kind + "' ";
            insertQuery += " ,'" + this.columnWorkcenter.SdiLine + "' ";
            insertQuery += " ,'" + this.columnWorkcenter.ViewSeq + "'";
            insertQuery += " ,'" + this.columnWorkcenter.AllowNotwork + "') ";
            if (this.e.DbAccess.ExecuteQuery(insertQuery) == 1)
            {
                MessageBox.Show("Insert Successfully!!\r\nBut you should maintain Material-Routing information in manual.","Information",MessageBoxIcon.Information);
            }
        }

        private void EditQueryWorkcenter()
        {
            string editQuery = "UPDATE [WorkCenter] ";
            editQuery += " SET ";
            editQuery += "  [Text] = N'" + this.columnWorkcenter.Text + "' ";
            editQuery += " ,[TextKor] = N'" + this.columnWorkcenter.TextKor + "' ";
            editQuery += " ,[Division] = '" + this.columnWorkcenter.Division.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Routing] = '" + this.columnWorkcenter.Routing.Split('/')[0].Trim() + "' ";
            if (this.columnWorkcenter.Status == false)
                editQuery += " ,[Status] =  0 ";
            else
                editQuery += " ,[Status] =  1 ";
            if (this.columnWorkcenter.WorkStatus == false)
                editQuery += " ,[WorkStatus] =  0 ";
            else
                editQuery += " ,[WorkStatus] =  1 ";
            editQuery += " ,[Updated] = GetDate() ";

            editQuery += " ,[Bunch] = '" + this.columnWorkcenter.BunchWorkcenter.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Kind] = '" + this.columnWorkcenter.Kind + "'";
            editQuery += " ,[SdiLine] = '" + this.columnWorkcenter.SdiLine + "' ";
            editQuery += " ,[ViewSeq] = '" + this.columnWorkcenter.ViewSeq + "' ";
            editQuery += " ,[AllowNotwork] = '" + this.columnWorkcenter.AllowNotwork + "' ";
            editQuery += " WHERE Workcenter = '" + this.columnWorkcenter.Workcenter + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryWorkcenter()
        {
            string deleteQuery = "Delete From Workcenter Where Workcenter = '" + this.columnWorkcenter.Workcenter + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        //
        private void DeleteQueryMaterialRoutingByWorkcenter()
        {
            string deleteQuery = "Delete From MaterialRouting Where Workcenter = '" + this.columnWorkcenter.Workcenter + "'";
            this.e.DbAccess.ExecuteQuery(deleteQuery);
        }

        //
        private void InsertQueryWorkCalendarStd()
        {
            string insertQuery = "INSERT INTO [WorkCalendarStd] ";
           insertQuery += " ([DayOfWeek] ";
           insertQuery += " ,[WorkCenter] ";
           insertQuery += " ,[WorkTeam] ";
           insertQuery += " ,[MeanWorker] ";
           insertQuery += " ,[WorkingTime] ";
           insertQuery += " ,[StartHour] ";
           insertQuery += " ,[EndHour] ";
           insertQuery += " ,[Updated]) ";
           insertQuery += " VALUES ";
           insertQuery += " ('" + this.columnWorkCalendarStd.DayOfWeek + "' ";
           insertQuery += " ,'" + this.columnWorkCalendarStd.Workcenter.Split('/')[0].Trim() + "' ";
           insertQuery += " ,'" + this.columnWorkCalendarStd.WorkTeam.Split('/')[0].Trim() + "' ";
           insertQuery += " ,'" + this.columnWorkCalendarStd.MeanWorker.ToString() + "'";
           insertQuery += " ,'" + this.columnWorkCalendarStd.WorkingTime.ToString() + "' ";
           insertQuery += " ,'" + this.columnWorkCalendarStd.StartHour.ToString() + "' ";
           insertQuery += " ,'" + this.columnWorkCalendarStd.EndHour.ToString() + "' ";
           insertQuery += " ,GetDate()) ";
           this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryWorkCalendarStd()
        {
            string editQuery = "UPDATE [WorkCalendarStd] ";
            editQuery += " SET  ";
            editQuery += "  [MeanWorker] = '" + this.columnWorkCalendarStd.MeanWorker.ToString() + "' ";
            editQuery += " ,[WorkingTime] = '" + this.columnWorkCalendarStd.WorkingTime.ToString() + "' ";
            editQuery += " ,[StartHour] = '" + this.columnWorkCalendarStd.StartHour.ToString() + "' ";
            editQuery += " ,[EndHour] = '" + this.columnWorkCalendarStd.EndHour.ToString() + "' ";
            editQuery += " ,[Updated] = GetDate() ";
            editQuery += " WHERE DayOfWeek = '" + this.columnWorkCalendarStd.DayOfWeek + "' And Workcenter = '" + this.columnWorkCalendarStd.Workcenter.Split('/')[0].Trim()
                + "' And WorkTeam = '" + this.columnWorkCalendarStd.WorkTeam.Split('/')[0].Trim() + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void InsertQueryFucntionError()
        {
            string insertQuery = "INSERT INTO [FunctionChecked] ";
            insertQuery += " ([Model] ";
            insertQuery += " ,[FuncError] ";
            insertQuery += " ,[FuncErrorName] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated] )";         
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnFunctionChecked.Model + "' ";
            insertQuery += " ,'" + this.columnFunctionChecked.FuncError + "' ";
            insertQuery += " ,'" + this.columnFunctionChecked.FuncErrorName + "' ";
            if (this.columnFunctionChecked.Status == false)
                insertQuery += " ,  0 ";
            else
                insertQuery += " ,  1 ";
            insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryFucntionError()
        {
            string editQuery = "UPDATE [FunctionChecked] ";
            editQuery += " SET  ";
            editQuery += "  [Model] = '" + this.columnFunctionChecked.Model.ToString() + "' ";
            editQuery += " ,[FuncError] = '" + this.columnFunctionChecked.FuncError.ToString() + "' ";
            editQuery += " ,[FuncErrorName] = '" + this.columnFunctionChecked.FuncErrorName.ToString() + "' ";
            if (this.columnFunctionChecked.Status == false)
                editQuery += " ,[Status] =  0 ";
            else
                editQuery += " ,[Status] =  1 ";
            editQuery += " ,[Updated] = GetDate() ";
            editQuery += " WHERE Model = '" + this.columnFunctionChecked.Model + "' And FuncError = '" + this.columnFunctionChecked.FuncError + "' ";
             
            this.RunExecuteQuery(editQuery, "Edit");        
        }

        private void DeleteQueryFucntionError()
        {
            string deleteQuery = "Delete From FunctionChecked Where Model = '" + this.columnFunctionChecked.Model + "'"
                    + " And FuncError = '" + this.columnFunctionChecked.FuncError + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void DeleteQueryWorkCalendarStd()
        {
            string deleteQuery = "Delete From WorkCalendarStd Where DayOfWeek = '" + this.columnWorkCalendarStd.DayOfWeek + "'"
                + " And Workcenter = '" + this.columnWorkCalendarStd.Workcenter + "' And WorkTeam = '" + this.columnWorkCalendarStd.WorkTeam + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // WorkTeam Code 수정/삭제/추가
        private void InsertQueryWorkTeam()
        {
            string insertQuery = "INSERT INTO [WorkTeam] ";
            insertQuery += " ([WorkTeam] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[ViewSeq]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnWorkTeam.WorkTeam + "' ";
            insertQuery += " ,N'" + this.columnWorkTeam.Text + "' ";
            if (this.columnWorkTeam.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnWorkTeam.Bunch + "' ";
            insertQuery += " ,'" + this.columnWorkTeam.Kind + "' ";
            insertQuery += " , " + this.columnWorkTeam.ViewSeq + " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryWorkTeam()
        {
            string editQuery = "UPDATE [WorkTeam] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnWorkTeam.Text + "'";
            if (this.columnWorkTeam.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnWorkTeam.Bunch + "' ";
            editQuery += " ,[Kind] = '" + this.columnWorkTeam.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnWorkTeam.ViewSeq;
            editQuery += " WHERE WorkTeam = '" + this.columnWorkTeam.WorkTeam + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryWorkTeam()
        {
            string deleteQuery = "Delete From WorkTeam Where WorkTeam = '" + this.columnWorkTeam.WorkTeam + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // 
        private void InsertQueryRoutingNotwork()
        {
            string divisionQuery = "select top 1 division from workcenter where routing = '"
                + this.columnRoutingNotWork.Routing.Split('/')[0].Trim() + "'";
            string division = this.e.DbAccess.ExecuteScalar(divisionQuery) as string;
            string insertQuery = " INSERT INTO [BasisTable] ";
            insertQuery += " ([Type] ";
            insertQuery += " ,[Division] ";
            insertQuery += " ,[Routing] ";
            insertQuery += " ,[ValueType] ";
            insertQuery += " ,[Value] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('NotWorkDetail' ";
            if (string.IsNullOrEmpty(division))
            {
                insertQuery += " ,NULL";
            }
            else
            {
                insertQuery += " ,'" + division + "'";
            }
            insertQuery += " ,'" + this.columnRoutingNotWork.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'Default' ";
            insertQuery += " ,'" + this.columnRoutingNotWork.Notwork.Split('/')[0].Trim() + "' ";
            if (this.columnRoutingNotWork.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryRoutingNotwork()
        {
            string editQuery = "Update [BasisTable] Set [Value] = '" + this.columnRoutingNotWork.Notwork.Split('/')[0].Trim();
            editQuery += "' , [Routing] = '" + this.columnRoutingNotWork.Routing.Split('/')[0].Trim() + "'";
            if (this.columnRoutingNotWork.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " , Updated = GetDate() Where Type = 'NotWorkDetail' And BasisTable = '" + this.columnRoutingNotWork.BasisTable + "'";
            this.RunExecuteQuery(editQuery, "Edit"); 
        }

        private void DeleteQueryRoutingNotwork()
        {
            string deleteQuery = "Delete From BasisTable Where BasisTable = '" + this.columnRoutingNotWork.BasisTable + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Worker Code 수정/삭제/추가
        private void InsertQueryWorker()
        {
            string insertQuery = "INSERT INTO [Worker] ";
            insertQuery += " ([Worker] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnWorker.Worker + "' ";
            insertQuery += " ,N'" + this.columnWorker.WorkerName + "' ";
            if (this.columnWorker.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,GetDate() ";
            if (string.IsNullOrEmpty(this.columnWorker.WorkTeam))
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " , '" + this.columnWorker.WorkTeam.Split('/')[0].Trim() + "' ";
            }
            insertQuery += " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryWorker()
        {
            string editQuery = "UPDATE [Worker] ";
            editQuery += " SET ";
            editQuery += " [Text] = N'" + this.columnWorker.WorkerName + "' ";
            if (this.columnWorker.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Updated] = GetDate() ";
            if (!string.IsNullOrEmpty(this.columnWorker.WorkTeam))
            {
                editQuery += " ,[Bunch] = '" + this.columnWorker.WorkTeam.Split('/')[0].Trim() + "'";
            }
            editQuery += " WHERE Worker = '" + this.columnWorker.Worker + "' ";
            this.RunExecuteQuery(editQuery, "Edit");

        }

        private void DeleteQueryWorker()
        {
            string deleteQuery = "Delete From Worker Where Worker = '" + this.columnWorker.Worker + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Notwork Code 수정/삭제/추가
        private void InsertQueryNotwork()
        {
            string insertQuery = "INSERT INTO [Notwork] ";
            insertQuery += " ([Notwork] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[ViewSeq], [Updated]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnNotwork.Notwork + "' ";
            insertQuery += " ,N'" + this.columnNotwork.Text + "' ";
            insertQuery += " ,N'" + this.columnNotwork.TextKor + "' ";
            if (this.columnNotwork.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnNotwork.Bunch + "' ";
            insertQuery += " ,'" + this.columnNotwork.Kind + "' ";
            insertQuery += " , " + this.columnNotwork.ViewSeq + ", Getdate() ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryNotwork()
        {
            string editQuery = "UPDATE [Notwork] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnNotwork.Text + "'"
                          + " ,[TextKor] = N'" + this.columnNotwork.TextKor + "'";
            if (this.columnNotwork.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnNotwork.Bunch + "' ";
            editQuery += " ,[Kind] = '" + this.columnNotwork.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnNotwork.ViewSeq;
            editQuery += " ,[Updated] = Getdate() ";
            editQuery += " WHERE Notwork = '" + this.columnNotwork.Notwork + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryNotwork()
        {
            string deleteQuery = "Delete From Notwork Where Notwork = '" + this.columnNotwork.Notwork + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // 
        private void InsertQueryRoutingBad()
        {
            string divisionQuery = "select top 1 division from workcenter where routing = '" 
                + this.columnRoutingBad.Routing.Split('/')[0].Trim() + "'";
            string division = this.e.DbAccess.ExecuteScalar(divisionQuery) as string;
            string insertQuery = " INSERT INTO [BasisTable] ";
            insertQuery += " ([Type] ";
            insertQuery += " ,[Division] ";
            insertQuery += " ,[Routing] ";
            insertQuery += " ,[ValueType] ";
            insertQuery += " ,[Value] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('BadDetail' ";
            if (string.IsNullOrEmpty(division))
            {
                insertQuery += " ,NULL";
            }
            else
            {
                insertQuery += " ,'" + division + "'";
            }
            insertQuery += " ,'" + this.columnRoutingBad.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'Default' ";
            insertQuery += " ,'" + this.columnRoutingBad.Bad.Split('/')[0].Trim() + "' ";
            if (this.columnRoutingBad.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,GetDate()) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryRoutingBad()
        {
            string editQuery = "Update [BasisTable] Set [Value] = '" + this.columnRoutingBad.Bad.Split('/')[0].Trim();
            editQuery += "' , [Routing] = '" + this.columnRoutingBad.Routing.Split('/')[0].Trim() + "'";
            if (this.columnRoutingBad.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " , Updated = GetDate() Where Type = 'BadDetail' And BasisTable = '" + this.columnRoutingBad.BasisTable + "'";
            this.RunExecuteQuery(editQuery, "Edit");   
        }

        private void DeleteQueryRoutingBad()
        {
            string deleteQuery = "Delete From BasisTable Where BasisTable = '" + this.columnRoutingBad.BasisTable + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Bad Code 수정/삭제/추가
        private void InsertQueryBad()
        {
            if (this.columnBad.Loss == true )
            {
                if (this.columnBad.Repair == true || this.columnBad.ReInsp == true || this.columnBad.Scrap == true)
                {
                    MessageBox.Show("Pleas If Loss value true, other Bad column value false check", "Error", MessageBoxIcon.None);
                    return;
                }
            }

            if (this.columnBad.Loss == false && this.columnBad.Repair == false && this.columnBad.ReInsp == false && this.columnBad.Scrap == false)
            {
                MessageBox.Show("Please check at least one", "Error", MessageBoxIcon.None);
                return;
            }

            string insertQuery = "INSERT INTO [Bad] ";
            insertQuery += " ([Bad] ";
            insertQuery += " ,[SDI_Bad] ";
            insertQuery += " ,[ERP_Bad] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[Repair] ";
            insertQuery += " ,[ReInsp] ";
            insertQuery += " ,[Loss] ";
            insertQuery += " ,[Scrap] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[ViewSeq]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnBad.Bad + "' ";
            insertQuery += " ,N'" + this.columnBad.SDI_Bad + "' ";
            insertQuery += " ,N'" + this.columnBad.ERP_Bad + "' ";
            insertQuery += " ,N'" + this.columnBad.Text + "' ";
            insertQuery += " ,N'" + this.columnBad.TextKor + "' ";
            if (this.columnBad.Repair == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            if (this.columnBad.ReInsp == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            if (this.columnBad.Loss == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            if (this.columnBad.Scrap == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            if (this.columnBad.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnBad.Bunch + "' ";
            insertQuery += " ,'" + this.columnBad.Kind + "' ";
            insertQuery += " , " + this.columnBad.ViewSeq + " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryRawMaterial()
        {
            string editQuery = "UPDATE [RawMaterial] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnRawMaterial.Text + "'"
                          + " ,[Spec] = N'" + this.columnRawMaterial.Spec + "'";
                if (this.columnRawMaterial.Status == false)
                {
                    editQuery += " , Status = 0 ";
                }
                else
                {
                    editQuery += " , Status = 1 ";
                }
            editQuery += " ,[Bunch] = '" + this.columnRawMaterial.Bunch.ToString().Split('/')[0] + "' ";
            editQuery += " ,[Kind] = '" + this.columnRawMaterial.Kind + "' ";
            editQuery += " ,[Unit] = '" + this.columnRawMaterial.Unit + "' ";
            editQuery += " ,Updated = GetDate() WHERE RawMaterial = '" + this.columnRawMaterial.RawMaterial + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryLocation()
        {
            string editQuery = "UPDATE [RM_Location] ";
                   editQuery += " SET  ";
                   editQuery += "  [Location_Group] = N'" + this.columnLocation.LocationGroup + "'";
                   editQuery += " ,[Location_Text] = N'" + this.columnLocation.LocationText + "'";
                   editQuery += " ,[Kind] = N'" + this.columnLocation.Kind + "'";
                   editQuery += " ,[Bunch] = N'" + this.columnLocation.Bunch + "'";            
                   editQuery += " ,Updated = GetDate() WHERE Location = '" + this.columnLocation.Location + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryLocationGroup()
        {
            string editQuery = "UPDATE [Rm_Location_Group] ";
            editQuery += " SET  ";
            editQuery += "  [Location_Group_Text] = N'" + this.columnLocationGroup.LocationGroupText + "'";
            editQuery += " ,Updated = GetDate() WHERE Location_Group = '" + this.columnLocationGroup.LocationGroup + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryBad()
        {

            if (this.columnBad.Loss == true)
            {
                if(this.columnBad.Repair == true || this.columnBad.ReInsp == true || this.columnBad.Scrap == true)
                    MessageBox.Show( "Pleas If Loss value true, other Bad column value false check", "Error", MessageBoxIcon.None);
                    return;
            }

             if (this.columnBad.Loss == false && this.columnBad.Repair == false && this.columnBad.ReInsp == false && this.columnBad.Scrap == false)
            {
                MessageBox.Show("Please check at least one", "Error", MessageBoxIcon.None);
                return;
            }
            string editQuery = "UPDATE [Bad] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnBad.Text + "'"
                          + " ,[TextKor] = N'" + this.columnBad.TextKor + "'"
                          + " ,[SDI_Bad] = N'" + this.columnBad.SDI_Bad + "'"
                          + " ,[ERP_Bad] = N'" + this.columnBad.ERP_Bad + "'";
            if (this.columnBad.Repair == false)
            {
                editQuery += " , Repair = 0 ";
            }
            else
            {
                editQuery += " , Repair = 1 ";
            }
            if (this.columnBad.ReInsp == false)
            {
                editQuery += " , ReInsp = 0 ";
            }
            else
            {
                editQuery += " , ReInsp = 1 ";
            }
            if (this.columnBad.Loss == false)
            {
                editQuery += " , Loss = 0 ";
            }
            else
            {
                editQuery += " , Loss = 1 ";
            }
            if (this.columnBad.Scrap == false)
            {
                editQuery += " , Scrap = 0 ";
            }
            else
            {
                editQuery += " , Scrap = 1 ";
            }
            if (this.columnBad.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnBad.Bunch + "' ";
            editQuery += " ,[Kind] = '" + this.columnBad.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnBad.ViewSeq;
            editQuery += " ,Updated = GetDate() WHERE Bad = '" + this.columnBad.Bad + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryBad()
        {
            string deleteQuery = "Delete From Bad Where Bad = '" + this.columnBad.Bad + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Repair Code 수정/삭제/추가
        private void InsertQueryRepair()
        {
            string insertQuery = "INSERT INTO [Repair] ";
            insertQuery += " ([Repair] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[ViewSeq]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnRepair.Repair + "' ";
            insertQuery += " ,N'" + this.columnRepair.Text + "' ";
            if (this.columnRepair.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnRepair.Bunch + "' ";
            insertQuery += " ,'" + this.columnRepair.Kind + "' ";
            insertQuery += " ,getdate() ";
            insertQuery += " , " + this.columnRepair.ViewSeq + " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryRepair()
        {
            string editQuery = "UPDATE [Repair] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnRepair.Text + "'";
            if (this.columnRepair.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnRepair.Bunch + "' ";
            editQuery += " ,[Kind] = '" + this.columnRepair.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnRepair.ViewSeq;
            editQuery += " ,Updated = GetDate() WHERE Repair = '" + this.columnRepair.Repair + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryRepair()
        {
            string deleteQuery = "Delete From Repair Where Repair = '" + this.columnRepair.Repair + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Material Code 수정/삭제/추가
        private void InsertQueryMaterial()
        {
            string insertQuery = "INSERT INTO [Material] ";
            insertQuery += " ([Material] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[CellQty] ";
            insertQuery += " ,[BoxQty] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[Spec] ";
            insertQuery += " ,[CycleTime] ";
            insertQuery += " ,[Cell_ID] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[TextEng] ";
            insertQuery += " ,[Case_A] ";
            insertQuery += " ,[Case_B] ";
            insertQuery += " ,[Case_C] ";
            insertQuery += " ,[Plate_A] ";
            insertQuery += " ,[Plate_B] ";
            insertQuery += " ,[Plate_C] ";
            insertQuery += " ,[Plate_D] ";
            insertQuery += " ,[Plate_E] ";
            insertQuery += " ,[Plate_F] ";
            insertQuery += " ,[Plate_G] ";
            insertQuery += " ,[Label_A] ";
            insertQuery += " ,[Label_B] ";
            insertQuery += " ,[Label_C] ";
            insertQuery += " ,[Label_D] ";
            insertQuery += " ,[Label_E] )";
                     
            //insertQuery += " ,[Type]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnMaterial.Material + "' ";
            insertQuery += " ,N'" + this.columnMaterial.Text + "' ";
            if (this.columnMaterial.Status == false)
                insertQuery += " , 0 ";
            else
                insertQuery += " , 1 ";
            insertQuery += " ,'" + this.columnMaterial.CellQty + "' ";
            insertQuery += " ,'" + this.columnMaterial.BoxQty + "' ";
            insertQuery += " ,'" + this.columnMaterial.Bunch + "' ";
            insertQuery += " ,'" + this.columnMaterial.Kind + "' ";
            insertQuery += " ,'" + this.columnMaterial.Spec + "' ";
            insertQuery += " ,'" + this.columnMaterial.CycleTime + "' ";
            insertQuery += " ,'" + this.columnMaterial.cell_id + "' ";
            insertQuery += " ,'" + this.columnMaterial.TextKor + "' ";
            insertQuery += " ,'" + this.columnMaterial.TextEng + "' ) ";
       

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        private void EditQueryMaterial()
        {
            string editQuery = "UPDATE [Material] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnMaterial.Text + "'";
                    if (this.columnMaterial.Status == false)
                        editQuery += " , Status = 0 ";
                    else
                        editQuery += " , Status = 1 ";
                    editQuery += " ,[CellQty] = '" + this.columnMaterial.CellQty + "' ";
                    editQuery += " ,[BoxQty] = '" + this.columnMaterial.BoxQty + "' ";
                    editQuery += " ,[Bunch] = '" + this.columnMaterial.Bunch + "' ";
                    editQuery += " ,[Kind] = '" + this.columnMaterial.Kind + "' ";
                    editQuery += " ,[CycleTime] = '" + this.columnMaterial.CycleTime + "' ";
                    editQuery += " ,[Spec] = '" + this.columnMaterial.Spec + "' ";
                    editQuery += " ,[Cell_ID] = '" + this.columnMaterial.cell_id + "' ";
                    editQuery += " ,[Case_A] = '" + this.columnMaterial.case_A + "' ";
                    editQuery += " ,[Case_B] = '" + this.columnMaterial.case_B + "' ";
                    editQuery += " ,[Case_C] = '" + this.columnMaterial.case_C + "' ";
                    editQuery += " ,[Plate_A] = '" + this.columnMaterial.plate_A + "' ";
                    editQuery += " ,[Plate_B] = '" + this.columnMaterial.plate_B + "' ";
                    editQuery += " ,[Plate_C] = '" + this.columnMaterial.plate_C + "' ";
                    editQuery += " ,[Plate_D] = '" + this.columnMaterial.plate_D + "' ";
                    editQuery += " ,[Plate_E] = '" + this.columnMaterial.plate_E + "' ";
                    editQuery += " ,[Plate_F] = '" + this.columnMaterial.plate_F + "' ";
                    editQuery += " ,[Plate_G] = '" + this.columnMaterial.plate_G + "' ";
                    editQuery += " ,[Pcm] = '" + this.columnMaterial.Pcm + "' ";
                    editQuery += " ,[TextKor] = '" + this.columnMaterial.TextKor + "' ";
                    editQuery += " ,[TextEng] = '" + this.columnMaterial.TextEng + "' ";                
                    editQuery += " ,[Updated] = 'getdate()' ";
                    editQuery += " WHERE Material = '" + this.columnMaterial.Material + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryMaterial()
        {
            string deleteQuery = "Delete From Material Where Material = '" + this.columnMaterial.Material + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        // Routing Code 수정/삭제/추가
        private void DeleteQueryRouting()
        {
            string deleteQuery = "Delete From Routing Where Routing = '" + this.columnRouting.Routing + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void EditQueryRouting()
        {
            string editQuery = "UPDATE [Routing] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnRouting.Text + "'";
            if (this.columnRouting.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnRouting.Bunch + "' ";
            editQuery += " ,[TextKor] = N'" + this.columnRouting.TextKor + "' ";
            editQuery += " ,[TextEng] = N'" + this.columnRouting.TextEng + "' ";
            editQuery += " ,[Kind] = '" + this.columnRouting.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnRouting.ViewSeq;
            editQuery += " WHERE Routing = '" + this.columnRouting.Routing + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void insertQueryRouting()
        {
            string insertQuery = "INSERT INTO [Routing] ";
            insertQuery += " ([Routing] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[TextEng] ";
            insertQuery += " ,[ViewSeq],[Updated]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnRouting.Routing + "' ";
            insertQuery += " ,N'" + this.columnRouting.Text + "' ";
            if (this.columnRouting.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnRouting.Bunch + "' ";
            insertQuery += " ,'" + this.columnRouting.Kind + "' ";
            insertQuery += " ,N'" + this.columnRouting.TextKor + "' ";
            insertQuery += " ,N'" + this.columnRouting.TextEng + "' ";
            insertQuery += " , " + this.columnRouting.ViewSeq + ", Getdate() ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        // Division Code 수정/삭제/추가
        private void EditQueryDivision()
        {
            string editQuery = "UPDATE [Division] "
                          + " SET  "
                          + "  [Text] = N'" + this.columnDivision.Text + "'"
                          + " ,[TextKor] = N'" + this.columnDivision.TextKor + "'";
            if (this.columnDivision.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Bunch] = '" + this.columnDivision.Bunch + "' ";
            editQuery += " ,[Kind] = '" + this.columnDivision.Kind + "' ";
            editQuery += " ,[ViewSeq] = " + this.columnDivision.ViewSeq;
            editQuery += " ,[Updated] = Getdate() ";
            editQuery += " WHERE Division = '" + this.columnDivision.Division + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void DeleteQueryDivision()
        {
            string deleteQuery = "Delete From Division Where Division = '" + this.columnDivision.Division + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void insertQueryDivision()
        {
            string insertQuery = "INSERT INTO [Division] ";
            insertQuery += " ([Division] ";
            insertQuery += " ,[Text] ";
            insertQuery += " ,[TextKor] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[Bunch] ";
            insertQuery += " ,[Kind] ";
            insertQuery += " ,[ViewSeq]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnDivision.Division + "' ";
            insertQuery += " ,N'" + this.columnDivision.Text + "' ";
            insertQuery += " ,N'" + this.columnDivision.TextKor + "' ";
            if (this.columnDivision.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'" + this.columnDivision.Bunch + "' ";
            insertQuery += " ,'" + this.columnDivision.Kind + "' ";
            insertQuery += " , " + this.columnDivision.ViewSeq + " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        // NotworkHist 수정/삭제/추가
        private void DeleteQueryNotworkHist()
        {
            string deleteQuery = "Delete NotworkHist Where NotworkHist = " + this.columnNotworkHist.NotworkHist;
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void EditQueryNotworkHist()
        {
            string editQuery = "UPDATE [NotworkHist] ";
            editQuery += " SET [Division] = '" + this.columnNotworkHist.Division.Split('/')[0].Trim() + "' ";
            editQuery += " ,[ClientId] = '" + this.columnNotworkHist.ClientId + "' ";
            editQuery += " ,[Workcenter] = '" + this.columnNotworkHist.Workcenter.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Material] = '" + this.columnNotworkHist.Material.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Routing] = '" + this.columnNotworkHist.Routing.Split('/')[0].Trim() + "' ";
            editQuery += " ,[WorkOrder] = '" + this.columnNotworkHist.WorkOrder + "' ";
            if (!string.IsNullOrEmpty(this.columnNotworkHist.Notwork.Split('/')[0].Trim()))
            {
                editQuery += " ,[Notwork] = '" + this.columnNotworkHist.Notwork.Split('/')[0].Trim() + "'";
            }
            if (this.columnNotworkHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Started] = NULL ";
            }
            else
            {
                editQuery += " ,[Started] = '" + this.columnNotworkHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnNotworkHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Ended] = NULL ";
            }
            else
            {
                editQuery += " ,[Ended] = '" + this.columnNotworkHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnNotworkHist.Status == false)
            {
                editQuery += " ,[Status] = 0 ";
            }
            else
            {
                editQuery += " ,[Status] = 1 ";
            }
            editQuery += " ,[IssueType] = 'Worker' ";
            editQuery += " ,[Shift] = '" + this.columnNotworkHist.Shift + "' ";
            editQuery += " ,[TotalNotwork] = '" + this.columnNotworkHist.TotalNotwork + "' ";
            editQuery += " ,[TotalRecess] = '" + this.columnNotworkHist.TotalRecess + "' ";
            editQuery += " ,[NetNotwork] = '" + this.columnNotworkHist.NetNotwork + "'";
            editQuery += " ,[TotalMinute] =  " + this.columnNotworkHist.TotalMinute;
            editQuery += " WHERE NotworkHist = " + this.columnNotworkHist.NotworkHist;
            this.RunExecuteQuery(editQuery, "Edit");

        }

        private void InsertQueryNotworkHist()
        {
            string insertQuery = "INSERT INTO [NotworkHist] ";
            insertQuery += " ([Division] ";
            insertQuery += " ,[ClientId] ";
            insertQuery += " ,[Workcenter] ";
            insertQuery += " ,[Material] ";
            insertQuery += " ,[Customer] ";
            insertQuery += " ,[Routing] ";
            insertQuery += " ,[WorkOrder] ";
            insertQuery += " ,[Notwork] ";
            insertQuery += " ,[Started] ";
            insertQuery += " ,[Ended] ";
            insertQuery += " ,[Status] ";
            insertQuery += " ,[IssueType] ";
            insertQuery += " ,[Shift] ";
            insertQuery += " ,[TotalNotwork] ";
            insertQuery += " ,[TotalRecess] ";
            insertQuery += " ,[NetNotwork] ";
            insertQuery += " ,[TotalMinute] ";
            insertQuery += " ) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.columnNotworkHist.Division.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.ClientId + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.Workcenter.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.Material.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.Customer.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.WorkOrder + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.Notwork.Split('/')[0].Trim() + "' ";
            if (this.columnNotworkHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnNotworkHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnNotworkHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnNotworkHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnNotworkHist.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,'Worker' ";
            insertQuery += " ,'" + this.columnNotworkHist.Shift + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.TotalNotwork + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.TotalRecess + "' ";
            insertQuery += " ,'" + this.columnNotworkHist.NetNotwork + "'";
            insertQuery += " , " + this.columnNotworkHist.TotalMinute;
            insertQuery += " ) ";

            this.RunExecuteQuery(insertQuery, "Insert");

        }

        // BadHist 수정/삭제/추가
        private void DeleteQueryBadHist()
        {
            string deleteQuery = "Delete BadHist Where BadHist = " + this.columnBadHist.BadHist;
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void EditQueryBadHist()
        {
            string editQuery = "UPDATE [BadHist] ";
            editQuery += " SET [Division] = '" + this.columnBadHist.Division.Split('/')[0].Trim() + "' ";
            editQuery += " ,[ClientId] = '" + this.columnBadHist.ClientId + "' ";
            editQuery += " ,[Workcenter] = '" + this.columnBadHist.Workcenter.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Material] = '" + this.columnBadHist.Material.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Customer] = '" + this.columnBadHist.Customer.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Routing] = '" + this.columnBadHist.Routing.Split('/')[0].Trim() + "' ";
            editQuery += " ,[WorkOrder] = '" + this.columnBadHist.WorkOrder + "' ";
            if (this.columnBadHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Started] = NULL ";
            }
            else
            {
                editQuery += " ,[Started] = '" + this.columnBadHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnBadHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Ended] = NULL ";
            }
            else
            {
                editQuery += " ,[Ended] = '" + this.columnBadHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            editQuery += " ,[Bad] = '" + this.columnBadHist.Bad.Split('/')[0].Trim() + "' ";
            editQuery += " ,[BadQty] = " + this.columnBadHist.BadQty;
            if (this.columnBadHist.Status == false)
            {
                editQuery += " ,[Status] = 0 ";
            }
            else
            {
                editQuery += " ,[Status] = 1 ";
            }
            editQuery += " ,[IssueType] = 'Worker' ";
            editQuery += " ,[Shift] = '" + this.columnBadHist.Shift + "'";
            editQuery += " ,[PalletNo] = '" + this.columnBadHist.PalletNo + "'";
            editQuery += " ,[BoxNo] = '" + this.columnBadHist.BoxNo + "'";
            editQuery += " ,[SerialNo] = '" + this.columnBadHist.SerialNo + "'";
            editQuery += " WHERE BadHist = " + this.columnBadHist.BadHist;
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void InsertQueryBadHist()
        {
            string insertQuery = "INSERT INTO [BadHist] "
                               + " ([Division] "
                               + " ,[ClientId] "
                               + " ,[Workcenter] "
                               + " ,[Material] "
                               + " ,[Customer] "
                               + " ,[Routing] "
                               + " ,[WorkOrder] "
                               + " ,[Started] "
                               + " ,[Ended] "
                               + " ,[Bad] "
                               + " ,[BadQty] "
                               + " ,[Status] "
                               + " ,[IssueType] "
                               + " ,[Shift] "
                               + " ,[PalletNo] "
                               + " ,[BoxNo] "
                               + " ,[SerialNo] "
                               + " ) "
                               + " VALUES "
                               + " ( '" + this.columnBadHist.Division.Split('/')[0].Trim() + "' "
                               + " ,'" + this.columnBadHist.ClientId + "' ";
            insertQuery += " ,'" + this.columnBadHist.Workcenter.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnBadHist.Material.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnBadHist.Customer.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnBadHist.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnBadHist.WorkOrder + "' ";
            if (this.columnBadHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnBadHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnBadHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnBadHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            insertQuery += " ,'" + this.columnBadHist.Bad.Split('/')[0].Trim() + "' ";

            insertQuery += " , " + this.columnBadHist.BadQty + " , ";
            if (this.columnBadHist.Status == false)
            {
                insertQuery += " 0 ";
            }
            else
            {
                insertQuery += " 1 ";
            }
            insertQuery += " ,'Worker' ";
            insertQuery += " ,'" + this.columnBadHist.Shift + "' ";
            insertQuery += " ,'" + this.columnBadHist.PalletNo + "' ";
            insertQuery += " ,'" + this.columnBadHist.BoxNo + "' ";
            insertQuery += " ,'" + this.columnBadHist.SerialNo + "' ";
            insertQuery += " ) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        // RepairHist 수정/삭제/추가
        private void DeleteQueryRepairHist()
        {
            string deleteQuery = "Delete RepairHist Where RepairHist = " + this.columnRepairHist.RepairHist;
            this.RunExecuteQuery(deleteQuery, "Delete");
        }

        private void EditQueryRepairHist()
        {
            string editQuery = "UPDATE [RepairHist] ";
            editQuery += " SET [Division] = '" + this.columnRepairHist.Division.Split('/')[0].Trim() + "' ";
            editQuery += " ,[ClientId] = '" + this.columnRepairHist.ClientId + "' ";
            editQuery += " ,[Workcenter] = '" + this.columnRepairHist.Workcenter.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Material] = '" + this.columnRepairHist.Material.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Customer] = '" + this.columnRepairHist.Customer.Split('/')[0].Trim() + "' ";
            editQuery += " ,[Routing] = '" + this.columnRepairHist.Routing.Split('/')[0].Trim() + "' ";
            editQuery += " ,[WorkOrder] = '" + this.columnRepairHist.WorkOrder + "' ";
            if (this.columnRepairHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Started] = NULL ";
            }
            else
            {
                editQuery += " ,[Started] = '" + this.columnRepairHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnRepairHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                editQuery += " ,[Ended] = NULL ";
            }
            else
            {
                editQuery += " ,[Ended] = '" + this.columnRepairHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            editQuery += " ,[Repair] = '" + this.columnRepairHist.Repair.Split('/')[0].Trim() + "' ";
            editQuery += " ,[RepairQty] = " + this.columnRepairHist.RepairQty;
            if (this.columnRepairHist.Status == false)
            {
                editQuery += " ,[Status] = 0 ";
            }
            else
            {
                editQuery += " ,[Status] = 1 ";
            }
            editQuery += " ,[IssueType] = 'Worker' ";
            editQuery += " ,[Shift] = '" + this.columnRepairHist.Shift + "'";
            editQuery += " ,[PalletNo] = '" + this.columnRepairHist.PalletNo + "'";
            editQuery += " ,[BoxNo] = '" + this.columnRepairHist.BoxNo + "'";
            editQuery += " ,[SerialNo] = '" + this.columnRepairHist.SerialNo + "'";
            editQuery += " WHERE RepairHist = " + this.columnRepairHist.RepairHist;
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void InsertQueryRepairHist()
        {
            string insertQuery = "INSERT INTO [RepairHist] "
                               + " ([Division] "
                               + " ,[ClientId] "
                               + " ,[Workcenter] "
                               + " ,[Material] "
                               + " ,[Customer] "
                               + " ,[Routing] "
                               + " ,[WorkOrder] "
                               + " ,[Started] "
                               + " ,[Ended] "
                               + " ,[Repair] "
                               + " ,[RepairQty] "
                               + " ,[Status] "
                               + " ,[IssueType] "
                               + " ,[Shift] "
                               + " ,[PalletNo] "
                               + " ,[BoxNo] "
                               + " ,[SerialNo] "
                               + " ) "
                               + " VALUES "
                               + " ( '" + this.columnRepairHist.Division.Split('/')[0].Trim() + "' "
                               + " ,'" + this.columnRepairHist.ClientId + "' ";
            insertQuery += " ,'" + this.columnRepairHist.Workcenter.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnRepairHist.Material.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnRepairHist.Customer.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnRepairHist.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnRepairHist.WorkOrder + "' ";
            if (this.columnRepairHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnRepairHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnRepairHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnRepairHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            insertQuery += " ,'" + this.columnRepairHist.Repair.Split('/')[0].Trim() + "' ";

            insertQuery += " , " + this.columnRepairHist.RepairQty + " , ";
            if (this.columnRepairHist.Status == false)
            {
                insertQuery += " 0 ";
            }
            else
            {
                insertQuery += " 1 ";
            }
            insertQuery += " ,'Worker' ";
            insertQuery += " ,'" + this.columnRepairHist.Shift + "' ";
            insertQuery += " ,'" + this.columnRepairHist.PalletNo + "' ";
            insertQuery += " ,'" + this.columnRepairHist.BoxNo + "' ";
            insertQuery += " ,'" + this.columnRepairHist.SerialNo + "' ";
            insertQuery += " ) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //FCTSpec 수정/삭제/추가
        private void DeleteQueryFCTSpec()
        {
            string deleteQuery = "Delete from ElentecINMes3_PEPB_iData.dbo.FCT_Spec Where MaterialCode = '" + this.columnFctSpec.MaterialCode + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void EditQueryFCTSpec()
        {
            string editQuery = "UPDATE [ElentecINMes3_PEPB_iData].[dbo].[FCT_Spec] ";
            editQuery += " SET [MaterialCode] = '" + this.columnFctSpec.MaterialCode.Trim() + "' ";
            editQuery += " ,[OCV_Min] = '" + this.columnFctSpec.OCV_Min.Trim() + "' ";
            editQuery += " ,[OCV_Max] = '" + this.columnFctSpec.OCV_Max.Trim() + "' ";
            editQuery += " ,[IR_Min] = '" + this.columnFctSpec.IR_Min.Trim() + "' ";
            editQuery += " ,[IR_Max] = '" + this.columnFctSpec.IR_Max.Trim() + "' ";
            editQuery += " ,[SAR_ST_Min] = '" + this.columnFctSpec.SAR_ST_Min.Trim() + "' ";
            editQuery += " ,[SAR_ST_Max] = '" + this.columnFctSpec.SAR_ST_Max.Trim() + "' ";
            editQuery += " ,[SAR_SV_Min] = '" + this.columnFctSpec.SAR_SV_Min.Trim() + "' ";
            editQuery += " ,[SAR_SV_Max] = '" + this.columnFctSpec.SAR_SV_Max.Trim() + "' ";
            editQuery += " ,[SAR_SRV_Min] = '" + this.columnFctSpec.SAR_SRV_Min.Trim() + "' ";
            editQuery += " ,[SAR_SRV_Max] = '" + this.columnFctSpec.SAR_SRV_Max.Trim() + "' ";
            editQuery += " ,[DCCV_Min] = '" + this.columnFctSpec.DCCV_Min.Trim() + "' ";
            editQuery += " ,[DCCV_Max] = '" + this.columnFctSpec.DCCV_Max.Trim() + "' ";
            editQuery += " ,[CCCV_Min] = '" + this.columnFctSpec.CCCV_Min.Trim() + "' ";
            editQuery += " ,[CCCV_Max] = '" + this.columnFctSpec.CCCV_Max.Trim() + "' ";
            editQuery += " ,[CELL_OCV_Min] = '" + this.columnFctSpec.CELL_OCV_Min.Trim() + "' ";
            editQuery += " ,[CELL_OCV_Max] = '" + this.columnFctSpec.CELL_OCV_Max.Trim() + "' ";
            editQuery += " ,[FOCV_Min] = '" + this.columnFctSpec.FOCV_Min.Trim() + "' ";
            editQuery += " ,[FOCV_Max] = '" + this.columnFctSpec.FOCV_Max.Trim() + "' ";
            editQuery += " ,[CNT_T1_Min] = '" + this.columnFctSpec.CNT_T1_Min.Trim() + "' ";
            editQuery += " ,[CNT_T1_Max] = '" + this.columnFctSpec.CNT_T1_Max.Trim() + "' ";
            editQuery += " ,[CNT_V1_Min] = '" + this.columnFctSpec.CNT_V1_Min.Trim() + "' ";
            editQuery += " ,[CNT_V1_Max] = '" + this.columnFctSpec.CNT_V1_Max.Trim() + "' ";
            editQuery += " ,[CNT_T2_Min] = '" + this.columnFctSpec.CNT_T2_Min.Trim() + "' ";
            editQuery += " ,[CNT_T2_Max] = '" + this.columnFctSpec.CNT_T2_Max.Trim() + "' ";
            editQuery += " ,[CNT_V2_Min] = '" + this.columnFctSpec.CNT_V2_Min.Trim() + "' ";
            editQuery += " ,[CNT_V2_Max] = '" + this.columnFctSpec.CNT_V2_Max.Trim() + "' ";
            editQuery += " ,[R1_VFR_Min] = '" + this.columnFctSpec.R1_VFR_Min.Trim() + "' ";
            editQuery += " ,[R1_VFR_Max] = '" + this.columnFctSpec.R1_VFR_Max.Trim() + "' ";
            editQuery += " ,[R2_CFR_Min] = '" + this.columnFctSpec.R2_CFR_Min.Trim() + "' ";
            editQuery += " ,[R2_CFR_Max] = '" + this.columnFctSpec.R2_CFR_Max.Trim() + "' ";
            editQuery += " ,[Updated] = getdate() ";
            editQuery += " WHERE MaterialCode = '" + this.columnFctSpec.MaterialCode + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }
        private void InsertQueryFCTSpec()
        {
            string insertQuery = "INSERT INTO [ElentecINMes3_PEPB_iData].[dbo].[FCT_Spec] VALUES(";
            insertQuery += "N'" + this.columnFctSpec.MaterialCode + "', ";
            insertQuery += "N'" + this.columnFctSpec.OCV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.OCV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.IR_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.IR_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_ST_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_ST_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_SV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_SV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_SRV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.SAR_SRV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.DCCV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.DCCV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CCCV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CCCV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CELL_OCV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CELL_OCV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.FOCV_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.FOCV_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_T1_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_T1_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_V1_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_V1_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_T2_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_T2_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_V2_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.CNT_V2_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.R1_VFR_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.R1_VFR_Max + "', ";
            insertQuery += "N'" + this.columnFctSpec.R2_CFR_Min + "', ";
            insertQuery += "N'" + this.columnFctSpec.R2_CFR_Max + "', ";
            insertQuery += "getdate() ) ";
            this.RunExecuteQuery(insertQuery, "Insert");
        }

        // OutputHist 수정/삭제/추가
        private void DeleteQueryOutputHist()
        {
            string deleteQuery = "Delete OutputHist Where OutputHist = " + this.columnOutputHist.OutputHist;
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void EditQueryOutputHist()
        {
            string editQuery = "UPDATE [OutputHist] ";
            editQuery +=  " SET [Division] = '" + this.columnOutputHist.Division.Split('/')[0].Trim() + "' ";
            editQuery +=  " ,[ClientId] = '" + this.columnOutputHist.ClientId + "' ";
            editQuery +=  " ,[Workcenter] = '" + this.columnOutputHist.Workcenter.Split('/')[0].Trim() + "' ";
            editQuery +=  " ,[Material] = '" + this.columnOutputHist.Material.Split('/')[0].Trim() + "' ";
            editQuery +=  " ,[Customer] = '" + this.columnOutputHist.Customer.Split('/')[0].Trim() + "' ";
            editQuery +=  " ,[Routing] = '" + this.columnOutputHist.Routing.Split('/')[0].Trim() + "' ";
            editQuery +=  " ,[WorkOrder] = '" + this.columnOutputHist.WorkOrder + "' ";
            if (this.columnOutputHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                editQuery += " ,[Started] = Null ";
            else
                editQuery += " ,[Started] = '" + this.columnOutputHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (this.columnOutputHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                editQuery += " ,[Ended] = Null ";
            else
                editQuery += " ,[Ended] = '" + this.columnOutputHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            editQuery +=  " ,[OutQty] = " + this.columnOutputHist.OutQty;
            if (this.columnOutputHist.Status == false)
                editQuery += " ,[Status] = 0 ";
            else
                editQuery += " ,[Status] = 1 ";
            editQuery += " ,[IssueType] = 'Worker' ";
            editQuery += " ,[Shift] = '" + this.columnOutputHist.Shift + "' ";
            editQuery += " ,[PalletNo] = '" + this.columnOutputHist.PalletNo + "' ";
            editQuery += " ,[BoxNo] = '" + this.columnOutputHist.BoxNo + "' ";
            editQuery += " ,[SerialNo] = '" + this.columnOutputHist.SerialNo + "' ";
            editQuery += " WHERE OutputHist = " + this.columnOutputHist.OutputHist;
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void InsertQueryOutputHist()
        {
            string insertQuery = "INSERT INTO [OutputHist] "
                               + " ([Division],[ClientId],[Workcenter],[Material],[Customer],[Routing],[WorkOrder],[Started],[Ended],[OutQty],[Status],[IssueType],[Shift],[PalletNo],[BoxNo] ,[SerialNo]) "
                               + " VALUES "
                               + " ('" + this.columnOutputHist.Division.Split('/')[0].Trim() + "' "
                               + " ,'" + this.columnOutputHist.ClientId + "' ";
            insertQuery += " ,'" + this.columnOutputHist.Workcenter.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnOutputHist.Material.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnOutputHist.Customer.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnOutputHist.Routing.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.columnOutputHist.WorkOrder + "' ";
            
            if (this.columnOutputHist.Started.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnOutputHist.Started.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            if (this.columnOutputHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                insertQuery += " , NULL ";
            }
            else
            {
                insertQuery += " ,'" + this.columnOutputHist.Ended.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            }
            insertQuery += " , " + this.columnOutputHist.OutQty + " , ";
            if (this.columnOutputHist.Status == false)
            {
                insertQuery += " 0 ";
            }
            else
            {
                insertQuery += " 1 ";
            }
            insertQuery += " ,'Worker' ";
            insertQuery += " ,'" + this.columnOutputHist.Shift + "' ";
            insertQuery += " ,'" + this.columnOutputHist.PalletNo + "' ";
            insertQuery += " ,'" + this.columnOutputHist.BoxNo + "' ";
            insertQuery += " ,'" + this.columnOutputHist.SerialNo + "' ";
            insertQuery += " )";
            this.RunExecuteQuery(insertQuery, "Insert");

        }

        #endregion

        private void propertyGridEdit_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.e.Program.ToLower() == "prm203")
            {
                if (e.ChangedItem.Label == "BadBunch")
                {
                    this.ChangeSearchBasic("prm203", "BadBunch", e.ChangedItem.Value.ToString().Split('/')[0]);
                }
            }
            else if (this.e.Program.ToLower() == "prm204")
            {
                if (e.ChangedItem.Label == "NotworkBunch")
                {
                    this.ChangeSearchBasic("prm204", "NotworkBunch", e.ChangedItem.Value.ToString().Split('/')[0]);
                }
            }
            else if (this.e.Program.ToLower() == "wpm001")
            {
                if (e.ChangedItem.Label == "NotworkGroup")
                {
                      this.ChangeSearchBasic("wpm001", "NotworkGroup", e.ChangedItem.Value.ToString().Split('/')[0]);
                  
                }
            }    
        }

        private void ChangeSearchBasic(string program, string label, string bunch)
        {
            if (program == "prm203")
            {
                if (label == "BadBunch")
                {
                    DataTable prm203routingBadDt = BasicCodeInfo.BasicBadbyBunch(this.e, bunch);
                    PropertyItemList._badItems = new String[prm203routingBadDt.Rows.Count];
                    for (int i = 0; i < prm203routingBadDt.Rows.Count; i++)
                    {
                        PropertyItemList._badItems[i] = prm203routingBadDt.Rows[i]["Bad"].ToString()
                            + "/" + prm203routingBadDt.Rows[i]["Text"].ToString();
                    }
                }
            }
            else if (program == "prm204")
            {
                if (label == "NotworkBunch")
                {
                    DataTable prm204routingNotworkDt = BasicCodeInfo.BasicNotworkbyBunch(this.e, bunch);
                    PropertyItemList._notworkItems = new String[prm204routingNotworkDt.Rows.Count];
                    for (int i = 0; i < prm204routingNotworkDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkItems[i] = prm204routingNotworkDt.Rows[i]["Notwork"].ToString()
                            + "/" + prm204routingNotworkDt.Rows[i]["Text"].ToString();
                    }
                }
            }
             else if (program == "wpm001")
            {
                if (label == "NotworkGroup")
                {
                    DataTable prm204routingNotworkDt = BasicCodeInfo.BasicNotworkbyBunch(this.e, bunch);
                    PropertyItemList._notworkItems = new String[prm204routingNotworkDt.Rows.Count];
                    for (int i = 0; i < prm204routingNotworkDt.Rows.Count; i++)
                    {
                        PropertyItemList._notworkItems[i] = prm204routingNotworkDt.Rows[i]["Notwork"].ToString()
                            + "/" + prm204routingNotworkDt.Rows[i]["Text"].ToString();
                    }
                }
            }
           
            
        }


        #region CheckSheetQuery 

        //CheckSheet - Update
        private void EditQueryCSLineRoute()
        {
            string editQuery = "UPDATE [CsLineRoute] "
                          + " SET  "
                          + "  [RouteName] = '" + this.ColumnCSLineRoute.RouteName + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + this.ColumnCSLineRoute.updater + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCSLineRoute.LIne.Split('/')[0].Trim() + "' and Route='" + ColumnCSLineRoute.Route + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryCSDailySpec()   //일일장비 사양관리 수정
        {
            string editQuery = "UPDATE [CSDailySpec] "
                          + " SET  "
                          + "  [Items] = '" + this.ColumnCSDailySpec.Items + "'"
                          + ",  [DataType] = '" + this.ColumnCSDailySpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [CheckTiming] = '" + this.ColumnCSDailySpec.CheckTiming + "'"
                          + ",  [CheckPeriod] = '" + this.ColumnCSDailySpec.CheckPeriod.Split('/')[0].Trim() + "'"
                          + ",  [ValueMin] = '" + this.ColumnCSDailySpec.ValueMin + "'"
                          + ",  [ValueMax] = '" + this.ColumnCSDailySpec.ValueMax + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCSDailySpec.LIne.Split('/')[0].Trim() + "' and Seq = '" + this.ColumnCSDailySpec.Seq + "' and Route='" + ColumnCSDailySpec.Route + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryCSPeriodicSpec()   //일일장비 사양관리 수정
        {
            string editQuery = "UPDATE [CsPeriodicSpec] "
                          + " SET  "
                          + "  [Items] = '" + this.ColumnCSPeriodicSpec.Items + "'"
                          + ",  [DataType] = '" + this.ColumnCSPeriodicSpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [CheckTiming] = '" + this.ColumnCSPeriodicSpec.CheckTiming + "'"
                          + ",  [CheckPeriod] = '" + this.ColumnCSPeriodicSpec.CheckPeriod.Split('/')[0].Trim() + "'"
                          + ",  [ValueMin] = '" + this.ColumnCSPeriodicSpec.ValueMin + "'"
                          + ",  [ValueMax] = '" + this.ColumnCSPeriodicSpec.ValueMax + "'"
                          + ",  [CheckMethod] = '" + this.ColumnCSPeriodicSpec.CheckMethod + "'"
                          + ",  [PlannedDate] = '" + this.ColumnCSPeriodicSpec.PlannedDate.Substring(0, 10) + "'"
                          + ",  [Factor] = '" + this.ColumnCSPeriodicSpec.Factor + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCSPeriodicSpec.LIne.Split('/')[0].Trim() + "' and Seq = '" + this.ColumnCSPeriodicSpec.Seq + "' and Route='" + ColumnCSPeriodicSpec.Route + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryCSPreventSpec()   //설비예방보전 사양관리 수정
        {
            string editQuery = "UPDATE [CsPreventSpec] "
                          + " SET  "
                          + "  [Items] = '" + this.ColumnCSPreventSpec.Items + "'"
                          + ",  [DataType] = '" + this.ColumnCSPreventSpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [CheckTiming] = '" + this.ColumnCSPreventSpec.CheckTiming + "'"
                          + ",  [CheckPeriod] = '" + this.ColumnCSPreventSpec.CheckPeriod.Split('/')[0].Trim() + "'"
                          + ",  [CheckMethod] = '" + this.ColumnCSPreventSpec.CheckMethod + "'"
                          + ",  [PlannedDate] = '" + this.ColumnCSPreventSpec.PlannedDate.Substring(0, 10) + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCSPreventSpec.LIne.Split('/')[0].Trim() + "' and Seq = '" + this.ColumnCSPreventSpec.Seq + "' and Route='" + ColumnCSPreventSpec.Route + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryCSOverhaulSpec()   //연간 오버홀 사양관리 수정
        {
            string editQuery = "UPDATE [CsOverhaulSpec] "
                          + " SET  "
                          + "  [Parts] = '" + this.ColumnCSOverhaulSpec.Parts + "'"
                          + ",  [Items] = '" + this.ColumnCSOverhaulSpec.Items + "'"
                          + ",  [DataType] = '" + this.ColumnCSOverhaulSpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [CheckTiming] = '" + this.ColumnCSOverhaulSpec.CheckTiming + "'"
                          + ",  [CheckPeriod] = '" + this.ColumnCSOverhaulSpec.CheckPeriod.Split('/')[0].Trim() + "'"
                          + ",  [Required] = '" + this.ColumnCSOverhaulSpec.Required + "'"
                          + ",  [ReplaceParts] = '" + this.ColumnCSOverhaulSpec.ReplaceParts + "'"
                          + ",  [CheckMethod] = '" + this.ColumnCSOverhaulSpec.CheckMethod + "'"
                          + ",  [PlannedDate] = '" + this.ColumnCSOverhaulSpec.PlannedDate.Substring(0, 10) + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCSOverhaulSpec.LIne.Split('/')[0].Trim() + "' and Seq = '" + this.ColumnCSOverhaulSpec.Seq + "' and Route='" + ColumnCSOverhaulSpec.Route + "' ";
            this.RunExecuteQuery(editQuery, "Edit");
        }
        private void EditQueryCS3c5sSpec()   //3정5행 사양관리 수정
        {
            string editQuery = "UPDATE [Cs3c5sSpec] "
                          + " SET  "
                          + "  [Items] = '" + this.ColumnCS3c5sSpec.Items + "'"
                          + ", [Comment] = '" + this.ColumnCS3c5sSpec.Comment + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE Line = '" + this.ColumnCS3c5sSpec.LIne.Split('/')[0].Trim() + "' and Seq = '" + this.ColumnCS3c5sSpec.Seq + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }
        private void EditQueryCSCheckSheetSpec()   //체크시트 사양관리 수정
        {
            string editQuery = "UPDATE [CsCheckSheetSpec] "
                          + " SET  "
                          + "  [CheckGroup] = '" + this.ColumnCSCheckSheetSpec.CheckGroup + "'"
                          + ",  [CheckItems] = '" + this.ColumnCSCheckSheetSpec.CheckItems + "'"
                          + ",  [DataType] = '" + this.ColumnCSCheckSheetSpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [DataUnit] = '" + this.ColumnCSCheckSheetSpec.DataUnit + "'"
                          + ",  [CheckPeriod] = '" + this.ColumnCSCheckSheetSpec.CheckPeriod.Split('/')[0].Trim() + "'"
                          + ",  [ValueMin] = '" + this.ColumnCSCheckSheetSpec.ValueMin + "'"
                          + ",  [ValueMax] = '" + this.ColumnCSCheckSheetSpec.ValueMax + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE CsCode = '" + this.ColumnCSCheckSheetSpec.CsCode + "' and Seq = '" + this.ColumnCSCheckSheetSpec.Seq + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }
        private void EditQueryCSParameterCheckSpec()   //파라매터 사양관리 수정
        {
            string editQuery = "UPDATE [CsParameterCheckSpec] "
                          + " SET  "
                          + "  [ParameterGroup] = '" + this.ColumnCSParameterCheckSpec.ParameterGroup + "'"
                          + ",  [ParameterItems] = '" + this.ColumnCSParameterCheckSpec.ParameterItems + "'"
                          + ",  [DataType] = '" + this.ColumnCSParameterCheckSpec.DataType.Split('/')[0].Trim() + "'"
                          + ",  [DataUnit] = '" + this.ColumnCSParameterCheckSpec.DataUnit + "'"
                          + ",  [ValueMin] = '" + this.ColumnCSParameterCheckSpec.ValueMin + "'"
                          + ",  [ValueMax] = '" + this.ColumnCSParameterCheckSpec.ValueMax + "'";
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE CsCode = '" + this.ColumnCSParameterCheckSpec.CsCode + "' and Seq = '" + this.ColumnCSParameterCheckSpec.Seq + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }

        private void EditQueryCSSpec()   //Csspec 사양관리 수정
        {
            string editQuery = "UPDATE [CsSpec] "
                          + " SET  "
                          + "  [Line] = '" + this.ColumnCSSpec.LIne.Split('/')[0].Trim() + "'"
                          + ",  [CsName] = '" + this.ColumnCSSpec.CsName + "'"
                          + ",  [Parts] = '" + this.ColumnCSSpec.Parts + "'"
                          + ",  [Route] = '" + this.ColumnCSSpec.Route + "'"
                          + ",  [RouteName] = '" + this.ColumnCSSpec.RouteName + "'"
                          + ",  [Items] = '" + this.ColumnCSSpec.Items + "'"
                          + ",  [Checker] = '" + this.ColumnCSSpec.Checker + "'"
                          + ",  [Confirmer] = '" + this.ColumnCSSpec.Confirmer + "'"
                          + ",  [KeepPeriod] = '" + this.ColumnCSSpec.KeepPeriod + "'"
                          + ",  [Comment] = '" + this.ColumnCSSpec.Comment + "'";
            if (this.ColumnCSSpec.Status == false)
            {
                editQuery += " , Status = 0 ";
            }
            else
            {
                editQuery += " , Status = 1 ";
            }
            editQuery += " ,[Updated] = getdate()";
            editQuery += " ,[Updater] = '" + WiseApp.Id + "' ";
            editQuery += " WHERE CsCode = '" + this.ColumnCSSpec.CsCode + "'";
            this.RunExecuteQuery(editQuery, "Edit");
        }


        //CheckSheet - Delete
        private void DeleteQueryCSLineRoute() //라인정보 
        {
            string deleteQuery = "Delete From CsLineRoute WHERE Line = '" + this.ColumnCSLineRoute.LIne + "' and Route='" + ColumnCSLineRoute.Route + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSDailySpec() //일일정보 사양관리
        {
            string deleteQuery = "Delete From CsDailySpec WHERE Line = '" + this.ColumnCSDailySpec.LIne + "' and Seq = '" + this.ColumnCSDailySpec.Seq + "' and Route='" + ColumnCSDailySpec.Route + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSPeriodicSpec() //정기장비 사양관리
        {
            string deleteQuery = "Delete From CsPeriodicSpec WHERE Line = '" + this.ColumnCSPeriodicSpec.LIne + "' and Seq = '" + this.ColumnCSPeriodicSpec.Seq + "' and Route='" + ColumnCSPeriodicSpec.Route + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSPreventSpec() //설비예방보전 사양관리
        {
            string deleteQuery = "Delete From CsPreventSpec WHERE Line = '" + this.ColumnCSPreventSpec.LIne + "' and Seq = '" + this.ColumnCSPreventSpec.Seq + "' and Route='" + ColumnCSPreventSpec.Route + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSOverhaulSpec() //설비예방보전 사양관리
        {
            string deleteQuery = "Delete From CsOverhaulSpec WHERE Line = '" + this.ColumnCSOverhaulSpec.LIne + "' and Seq = '" + this.ColumnCSOverhaulSpec.Seq + "' and Route='" + ColumnCSOverhaulSpec.Route + "' ";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCS3c5sSpec() //3정5행 사양관리
        {
            string deleteQuery = "Delete From Cs3c5sSpec WHERE Line = '" + this.ColumnCS3c5sSpec.LIne + "' and Seq = '" + this.ColumnCS3c5sSpec.Seq + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSCheckSheetSpec() //체크시트 사양관리
        {
            string deleteQuery = "Delete From CsCheckSheetSpec WHERE CsCode = '" + this.ColumnCSCheckSheetSpec.CsCode + "' and Seq = '" + this.ColumnCSCheckSheetSpec.Seq + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSParameterCheckSpec() //파라매터 사양관리
        {
            string deleteQuery = "Delete From CsParameterCheckSpec WHERE CsCode = '" + this.ColumnCSParameterCheckSpec.CsCode + "' and Seq = '" + this.ColumnCSParameterCheckSpec.Seq + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }
        private void DeleteQueryCSSpec() //Csspec 사양관리
        {
            string deleteQuery = "Delete From CsSpec WHERE CsCode = '" + this.ColumnCSSpec.CsCode + "'";
            this.RunExecuteQuery(deleteQuery, "Delete");
        }



        //CheckSheet
        private void InsertCSLineRoute()
        {
            string insertQuery = "INSERT INTO [CsLineRoute] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Route] ";
            insertQuery += " ,[RouteName] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSLineRoute.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSLineRoute.Route + "' ";
            insertQuery += " ,'" + this.ColumnCSLineRoute.RouteName + "' ";
            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + this.ColumnCSLineRoute.updater + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //일일정보 사양관리 insert
        private void InsertCSDailySpec()
        {
            string insertQuery = "INSERT INTO [CSDailySpec] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[Route] ";

            insertQuery += " ,[Items] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[CheckTiming] ";
            insertQuery += " ,[CheckPeriod] ";
            insertQuery += " ,[ValueMin] ";
            insertQuery += " ,[ValueMax] ";

            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSDailySpec.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.Route + "' ";

            insertQuery += " ,'" + this.ColumnCSDailySpec.Items + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.CheckTiming + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.CheckPeriod.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.ValueMin + "' ";
            insertQuery += " ,'" + this.ColumnCSDailySpec.ValueMax + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //정기장비 사양관리 insert
        private void InsertCSPeriodicSpec()
        {
            string insertQuery = "INSERT INTO [CsPeriodicSpec] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[Route] ";

            insertQuery += " ,[Items] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[CheckTiming] ";
            insertQuery += " ,[CheckPeriod] ";
            insertQuery += " ,[ValueMin] ";
            insertQuery += " ,[ValueMax] ";

            insertQuery += " ,[CheckMethod] ";
            insertQuery += " ,[PlannedDate] ";
            insertQuery += " ,[Factor] ";

            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSPeriodicSpec.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.Route + "' ";

            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.Items + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.CheckTiming + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.CheckPeriod.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.ValueMin + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.ValueMax + "' ";

            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.CheckMethod + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.PlannedDate.Substring(0, 10) + "' ";
            insertQuery += " ,'" + this.ColumnCSPeriodicSpec.Factor + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }


        //설비예반보전 사양관리 insert
        private void InsertCSPreventSpec()
        {
            string insertQuery = "INSERT INTO [CsPreventSpec] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[Route] ";

            insertQuery += " ,[Items] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[CheckTiming] ";
            insertQuery += " ,[CheckPeriod] ";

            insertQuery += " ,[CheckMethod] ";
            insertQuery += " ,[PlannedDate] ";

            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSPreventSpec.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.Route + "' ";

            insertQuery += " ,'" + this.ColumnCSPreventSpec.Items + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.CheckTiming + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.CheckPeriod.Split('/')[0].Trim() + "' ";

            insertQuery += " ,'" + this.ColumnCSPreventSpec.CheckMethod + "' ";
            insertQuery += " ,'" + this.ColumnCSPreventSpec.PlannedDate.Substring(0, 10) + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //연간 오버홀 사양관리 insert
        private void InsertCSOverhaulSpec()
        {
            string insertQuery = "INSERT INTO [CsOverhaulSpec] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[Route] ";

            insertQuery += " ,[Parts] ";
            insertQuery += " ,[Items] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[CheckTiming] ";
            insertQuery += " ,[CheckPeriod] ";

            insertQuery += " ,[CheckMethod] ";
            insertQuery += " ,[Required] ";
            insertQuery += " ,[ReplaceParts] ";
            insertQuery += " ,[PlannedDate] ";

            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSOverhaulSpec.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.Route + "' ";

            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.Parts + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.Items + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.CheckTiming + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.CheckPeriod.Split('/')[0].Trim() + "' ";

            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.CheckMethod + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.Required + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.ReplaceParts + "' ";
            insertQuery += " ,'" + this.ColumnCSOverhaulSpec.PlannedDate.Substring(0, 10) + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //3정5행 사양관리 insert
        private void InsertCS3c5sSpec()
        {
            string insertQuery = "INSERT INTO [Cs3c5sSpec] ";
            insertQuery += " ([Line] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[Items] ";
            insertQuery += " ,[Comment] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCS3c5sSpec.LIne.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCS3c5sSpec.Seq + "' ";

            insertQuery += " ,'" + this.ColumnCS3c5sSpec.Items + "' ";
            insertQuery += " ,'" + this.ColumnCS3c5sSpec.Comment + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }
        //체크시트 insert
        private void InsertCSCheckSheetSpec()
        {
            string insertQuery = "INSERT INTO [CsCheckSheetSpec] ";
            insertQuery += " ([CsCode] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[CheckGroup] ";
            insertQuery += " ,[CheckItems] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[DataUnit] ";
            insertQuery += " ,[CheckPeriod] ";
            insertQuery += " ,[ValueMin] ";
            insertQuery += " ,[ValueMax] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSCheckSheetSpec.CsCode + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.CheckGroup + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.CheckItems + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.DataUnit + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.CheckPeriod.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.ValueMin + "' ";
            insertQuery += " ,'" + this.ColumnCSCheckSheetSpec.ValueMax + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //파라매터 insert
        private void InsertCSParameterCheckSpec()
        {
            string insertQuery = "INSERT INTO [CsParameterCheckSpec] ";
            insertQuery += " ([CsCode] ";
            insertQuery += " ,[Seq] ";
            insertQuery += " ,[ParameterGroup] ";
            insertQuery += " ,[ParameterItems] ";
            insertQuery += " ,[DataType] ";
            insertQuery += " ,[DataUnit] ";
            insertQuery += " ,[ValueMin] ";
            insertQuery += " ,[ValueMax] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSParameterCheckSpec.CsCode + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.Seq + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.ParameterGroup + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.ParameterItems + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.DataType.Split('/')[0].Trim() + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.DataUnit + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.ValueMin + "' ";
            insertQuery += " ,'" + this.ColumnCSParameterCheckSpec.ValueMax + "' ";

            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }

        //CSspec insert
        private void InsertCSSpec()
        {
            string insertQuery = "INSERT INTO [CsSpec] ";
            insertQuery += " ([CsCode] ";
            insertQuery += " ,[Line] ";

            insertQuery += " ,[CsName] ";
            insertQuery += " ,[Parts] ";
            insertQuery += " ,[Route] ";
            insertQuery += " ,[RouteName] ";
            insertQuery += " ,[Items] ";

            insertQuery += " ,[Checker] ";
            insertQuery += " ,[confirmer] ";
            insertQuery += " ,[Keepperiod] ";
            insertQuery += " ,[comment] ";

            insertQuery += " ,[Status] ";
            insertQuery += " ,[Updated] ";
            insertQuery += " ,[Updater]) ";
            insertQuery += " VALUES ";
            insertQuery += " ('" + this.ColumnCSSpec.CsCode + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.LIne.Split('/')[0].Trim() + "' ";

            insertQuery += " ,'" + this.ColumnCSSpec.CsName + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.Parts + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.Route + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.RouteName + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.Items + "' ";

            insertQuery += " ,'" + this.ColumnCSSpec.Checker + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.Confirmer + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.KeepPeriod + "' ";
            insertQuery += " ,'" + this.ColumnCSSpec.Comment + "' ";


            if (this.ColumnCSSpec.Status == false)
            {
                insertQuery += " , 0 ";
            }
            else
            {
                insertQuery += " , 1 ";
            }
            insertQuery += " ,getdate() ";
            insertQuery += " ,'" + WiseApp.Id + "' ) ";

            this.RunExecuteQuery(insertQuery, "Insert");
        }





        #endregion



    }
}


