using System;
using System.Windows.Forms;

namespace MES_Monitoring.UserControls.Pages
{
    public partial class Page_Base : UserControl
    {
        protected Page_Base()
        {
            InitializeComponent();
        }


        private void Page_Base_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
        }
    }
}