using System.ComponentModel;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Lines;
using System.Collections.Generic;

namespace MES_Monitoring.UserControls.Layouts
{
    public partial class UserControlLayoutSmt
    {
        public Dictionary<int, UserControlLineSmt> LineList = new Dictionary<int, UserControlLineSmt>();
        public UserControlLayoutSmt()
        {
            InitializeComponent();
        }

        public UserControlLayoutSmt(IContainer components)
        {
            InitializeComponent();
            this.components = components;
        }

        public void SetLineCondition(Common.Routing.Type type, int lineCode, Common.Routing.Condition condition)
        {
            if (!(LineList.TryGetValue(lineCode, out UserControlLineSmt line))) return;
            switch(type)
            {
                case Common.Routing.Type.SMT_Load:
                    line.UnitList[0].Condition = condition;
                    line.UnitList[1].Condition = condition;
                    break;
                case Common.Routing.Type.SMT_Unload:
                    line.UnitList[2].Condition = condition;
                    line.UnitList[3].Condition = condition;
                    break;
            }
            
        }

        private void UserControlLayoutSmt_Load(object sender, System.EventArgs e)
        {
            LineList.Add(userControlLineSmt1.LineCode, userControlLineSmt1);
            LineList.Add(userControlLineSmt2.LineCode, userControlLineSmt2);
            LineList.Add(userControlLineSmt3.LineCode, userControlLineSmt3);
            LineList.Add(userControlLineSmt4.LineCode, userControlLineSmt4);
            LineList.Add(userControlLineSmt5.LineCode, userControlLineSmt5);
            //LineList.Add(userControlLineSmt6.LineCode, userControlLineSmt6);
            //LineList.Add(userControlLineSmt7.LineCode, userControlLineSmt7);
            LineList.Add(userControlLineSmt8.LineCode, userControlLineSmt8);
            LineList.Add(userControlLineSmt9.LineCode, userControlLineSmt9);
        }
    }
}