namespace MES_Monitoring.UserControls.Lines
{
    public partial class UserControlLineSmt : UserControlLine
    {
        public UserControlLineSmt()
        {
            InitializeComponent();
        }

        private void UserControlLineSmt_Load(object sender, System.EventArgs e)
        {
            UnitList.Add(userControlUnit1);
            UnitList.Add(userControlUnit2);
            UnitList.Add(userControlUnit3);
            UnitList.Add(userControlUnit4);
        }
    }
}