using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Lines;
using System.ComponentModel;

namespace MES_Monitoring.UserControls.Layouts
{
    public partial class UserControlLayoutMi : UserControlLayout
    {

        public UserControlLayoutMi()
        {
            InitializeComponent();
        }

        public UserControlLayoutMi(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            LineList.Add(userControlLineMi1.LineCode, userControlLineMi1);
            LineList.Add(userControlLineMi2.LineCode, userControlLineMi2);
            LineList.Add(userControlLineMi3.LineCode, userControlLineMi3);
            LineList.Add(userControlLineMi4.LineCode, userControlLineMi4);
            //LineList.Add(userControlLineMi5.LineCode, userControlLineMi5);
            //LineList.Add(userControlLineMi6.LineCode, userControlLineMi6);
            //LineList.Add(userControlLineMi7.LineCode, userControlLineMi7);
            LineList.Add(userControlLineMi8.LineCode, userControlLineMi8);
            LineList.Add(userControlLineMi9.LineCode, userControlLineMi9);
        }

        public void SetLineCondition(Common.Routing.Type type, int lineCode, Common.Routing.Condition condition)
        {
            if (!(LineList.TryGetValue(lineCode, out UserControlLine line))) return;
            switch (type)
            {
                case Common.Routing.Type.MI_Load:
                    line.UnitList[0].Condition = condition;
                    break;
                case Common.Routing.Type.Mi_1stFunc:
                    break;
                case Common.Routing.Type.Mi_Voltage:
                    line.UnitList[1].Condition = condition;
                    line.UnitList[2].Condition = condition;
                    break;
                case Common.Routing.Type.Mi_2ndFunc:
                    line.UnitList[3].Condition = condition;
                    break;
                case Common.Routing.Type.Pk_Boxing:
                    line.UnitList[4].Condition = condition;
                    break;
            }

        }
    }
}