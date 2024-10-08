using MES_Monitoring.UserControls.Lines;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MES_Monitoring.UserControls.Layouts
{
    public partial class UserControlLayout : UserControl, IComponent
    {
        public Dictionary<int, UserControlLine> LineList = new Dictionary<int, UserControlLine>();

        public UserControlLayout()
        {
            InitializeComponent();
        }

        public UserControlLayout(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}