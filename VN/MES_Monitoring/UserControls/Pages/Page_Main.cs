using System;
using System.Data;
using System.Windows.Forms;
using MES_Monitoring.Classes;

namespace MES_Monitoring.UserControls.Pages
{
    public partial class Page_Main : Page_Base
    {
        public Page_Main()
        {
            InitializeComponent();
        }

        private void Page_Main_Load(object sender, EventArgs e)
        {
        }

        public void SetPage(DataSet dataSet)
        {
            try
            {
                foreach (DataRow row in dataSet.Tables[1].Rows)
                {
                    var routing = row["Routing"].ToString();
                    var line = Convert.ToInt32(row["Line"]);

                    int intCondition;
                    try
                    {
                        intCondition =
                            Convert.ToInt16(row["Condition"].ToString().Substring(0, 1)) +
                            1; // 0:가동중, 1:계획정지, 2:일시정지, 3:고장  "04" "05" "14" "15"
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }

                    switch (routing)
                    {
                        case "Ai_Load":
                            _setLayoutAiAxial.SetLineCondition(Common.Routing.Type.AI_Load, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "Ai_Unload":
                            _setLayoutAiRadial.SetLineCondition(Common.Routing.Type.AI_Unload, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "St_Load":
                            _setLayoutSmt.SetLineCondition(Common.Routing.Type.SMT_Load, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "St_Unload":
                            _setLayoutSmt.SetLineCondition(Common.Routing.Type.SMT_Unload, line,  (Common.Routing.Condition)intCondition);
                            break;
                        case "Mi_Load":
                            _setLayoutMi.SetLineCondition(Common.Routing.Type.MI_Load, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "Mi_1stFunc":
                            _setLayoutMi.SetLineCondition(Common.Routing.Type.Mi_1stFunc, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "Mi_Voltage":
                            _setLayoutMi.SetLineCondition(Common.Routing.Type.Mi_Voltage, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "Mi_2ndFunc":
                            _setLayoutMi.SetLineCondition(Common.Routing.Type.Mi_2ndFunc, line, (Common.Routing.Condition)intCondition);
                            break;
                        case "Pk_Boxing":
                            _setLayoutMi.SetLineCondition(Common.Routing.Type.Pk_Boxing, line, (Common.Routing.Condition)intCondition);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // this.InsertIntoSysLog(ex.Message);
            }
        }
    }
}