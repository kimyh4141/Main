using System.ComponentModel;
using System.Windows.Forms;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Lines;
using System.Collections.Generic;
using System;

namespace MES_Monitoring.UserControls.Layouts
{
    public partial class UserControlLayoutAiAxial : UserControl
    {
        public Dictionary<int, UserControlLineAi> LineList = new Dictionary<int, UserControlLineAi>();
        public UserControlLayoutAiAxial()
        {
            InitializeComponent();
        }

        public UserControlLayoutAiAxial(IContainer components)
        {
            InitializeComponent();
            this.components = components;
            LineList.Add(userControlLineAi1.LineCode, userControlLineAi1);
            LineList.Add(userControlLineAi2.LineCode, userControlLineAi2);
            LineList.Add(userControlLineAi3.LineCode, userControlLineAi3);
            LineList.Add(userControlLineAi4.LineCode, userControlLineAi4);
            LineList.Add(userControlLineAi5.LineCode, userControlLineAi5);
            LineList.Add(userControlLineAi6.LineCode, userControlLineAi6);
            LineList.Add(userControlLineAi7.LineCode, userControlLineAi7);
            LineList.Add(userControlLineAi8.LineCode, userControlLineAi8);
            LineList.Add(userControlLineAi9.LineCode, userControlLineAi9);
            foreach (KeyValuePair<int, UserControlLineAi> pair in LineList)
            {
                pair.Value.UnitList[0].Text = $@"A-{pair.Key:0#}";
            }
        }

        public void SetLineCondition(Common.Routing.Type type, int lineCode, Common.Routing.Condition condition)
        {
            if (!(LineList.TryGetValue(Convert.ToInt32(lineCode), out UserControlLineAi usercontrolLineAI))) return;
            usercontrolLineAI.UnitList[0].Condition = condition;
        }
    }
}