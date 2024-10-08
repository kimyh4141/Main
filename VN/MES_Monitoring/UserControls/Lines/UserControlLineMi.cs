namespace MES_Monitoring.UserControls.Lines
{
    public partial class UserControlLineMi : UserControlLine
    {

        public UserControlLineMi()
        {
            InitializeComponent();
        }

        private void UserControlLineMi_Load(object sender, System.EventArgs e)
        {
            UnitList.Add(userControlUnit1);
            UnitList.Add(userControlUnit2);
            UnitList.Add(userControlUnit3);
            UnitList.Add(userControlUnit4);
            UnitList.Add(userControlUnit5);
        }
    }
}