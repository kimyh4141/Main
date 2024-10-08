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

                //frmMain10 frm = new frmMain10();
                //frm.ShowDialog();
                //Palletizing_2 bp = new Palletizing_2();
                //bp.Text = "包装#2 Packing";
                //bp.ShowDialog();
                //113.160.209.155 192.168.109.3
                //ClientApplication.Start(service, "WpcSLA1", "113.160.209.155", "192.168.109.3");
                //ClientApplication.Start(service, "WpcPS11", "123.235.1.131", "192.168.169.3");
                WiseM.Client.ClientApplication.Start(service, "WpcPS11", "123.235.1.131", "192.168.169.3");
                //ClientApplication.Start(service, "WpcPBG1", "192.168.169.3", "127.0.0.1");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}