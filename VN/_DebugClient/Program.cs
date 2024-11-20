using System;
using System.Windows.Forms;

namespace WiseM.Client
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                WbtUserService service = new WbtUserService();

                //121.126.143.101 192.168.109.3
                //ClientApplication.Start(service, "WpcPB11", "192.168.109.3", "");
                ClientApplication.Start(service, "WcSL41", "127.0.0.1", "");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}