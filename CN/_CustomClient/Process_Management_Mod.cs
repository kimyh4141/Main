using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Client
{
    public partial class Process_Management_Mod : Form
    {

        private string strComPortName;
        private string singlePort;
        private string leftPort;
        private string rightPort;
        private string thresholdQty;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public Process_Management_Mod()
        {
            InitializeComponent();

            string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo = new FileInfo(path + @"\Config.ini");

            StringBuilder tempStrQty = new StringBuilder();

            GetPrivateProfileString("Setting", "ThresholdQty", "", tempStrQty, 256, path + @"\Config.ini");

            this.thresholdQty = tempStrQty.ToString();

            num_qty.Value = int.Parse(thresholdQty);
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string path = @"C:\Program Files (x86)\Wise-M Systems\Wise-Mes\ProcessConfig";

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            FileInfo fileInfo = new FileInfo(path + @"\Config.ini");

            WritePrivateProfileString("Setting", "ThresholdQty", num_qty.Value.ToString(), path + @"\Config.ini");

            System.Windows.Forms.MessageBox.Show("Save Complete");
        }
    }
}
