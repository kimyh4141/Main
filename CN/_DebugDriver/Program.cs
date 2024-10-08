using System;
using System.Windows.Forms;
using WiseM.Driver;

namespace WiseM.Driver
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

            //SendProdInfoErp net = new SendProdInfoErp();
            //net.DebugStart("192.169.1.3", null, "SendProdInfoErp"); //192.169.1.3

            //Func_Test_1 net = new Func_Test_1();
            //net.DebugStart("127.0.0.1", null, "Func_Test_1");

            //Heater_Test net = new Heater_Test();
            //net.DebugStart("127.0.0.1", null, "Heater_Test");

            //PEPB_FunctionTest01 net2 = new PEPB_FunctionTest01();
            //net2.DebugStart("192.168.0.70", null, "PEPB_FunctionTest01");
        }
    }
}