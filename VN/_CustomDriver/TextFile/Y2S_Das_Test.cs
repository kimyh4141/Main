namespace WiseM.Driver
{
    public class Y2S_Das_Test : DriverCustomService
    {
        protected override void OnEvent(DriverEventArgs e)
        {
            BasisInfo(e);
            base.OnEvent(e);
        }

        protected override void OnEventOverdue(DriverEventArgs e)
        {
            BasisInfo(e);
            base.OnEventOverdue(e);
        }

        private void BasisInfo(DriverEventArgs e)
        {
            return;
        }

    }
}
