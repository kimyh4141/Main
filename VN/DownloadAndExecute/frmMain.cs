
using System;
using System.Data;
using System.Windows.Forms;

using System.IO;
using System.Xml;
using System.Diagnostics;

namespace DownloadAndExecute
{
    public partial class frmMain : Form
    {
        DataSet dsConfig = new DataSet();
        string AppName = "MES_Monitoring.exe";

        public frmMain()
        {
            Visible = false;

            if (!ReadXmlConfig(dsConfig))
            {
                Close();
                Environment.Exit(0);
            }

            ConfigData.ConnectionIP = dsConfig.Tables[0].Rows[0]["ConnectionIP"].ToString().Trim();


            ProcessStart();

            Environment.Exit(1);

            InitializeComponent();
        }

        private void ProcessStart()
        {
            bool isSuccess = false;

            try
            {
                Download();

                isSuccess = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Download!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (isSuccess == false)
                MessageBox.Show("Fail Download!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (File.Exists(Application.StartupPath + @"\MonitoringConfig.xml"))
                Process.Start(Application.StartupPath + @"\" + AppName);
            else
                MessageBox.Show("Not Found Monitoring Program.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Download()
        {
            string query = string.Empty;

            query += "\r\n";
            query += "\r\n SELECT   data ";
            query += "\r\n         ,folder ";
            query += "\r\n         ,filename ";
            query += "\r\n FROM     ProgramFile_Upload ";
            query += "\r\n WHERE    Bunch = 'Monitoring' ";

            DataTable dt = DbAccess.GetDt(query);

            if (dt == null || dt.Rows.Count <= 0) return;
            foreach (DataRow row in dt.Rows)
            {
                byte[] dddd = (byte[])row["data"];

                string folder = row["folder"].ToString();
                string filename = row["filename"].ToString();

                string temp = filename.Substring(filename.LastIndexOf(@".") + 1).ToLower();

                FileSave(dddd, folder, filename, temp);
            }
        }

        public void FileSave(byte[] data, string folder, string fileName, string fileExt)
        {
            FileStream fileStream = null;

            try
            {
                byte[] byteData = new byte[data.Length];
                byteData = data;
                int ArraySize = new int();

                byteData.GetUpperBound(0);

                string strFolder = "";
                if (folder.Length > 0)
                {
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    strFolder = @"\" + folder;
                }

                fileStream = new FileStream(Application.StartupPath + strFolder + @"\" + fileName, FileMode.Create, FileAccess.Write);
                BinaryWriter Bw = new BinaryWriter(fileStream);
                Bw.Write(data);
                //Bw = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                fileStream?.Close();
            }
        }

        private bool ReadXmlConfig(DataSet dataSet)
        {
            var xmlFileName = $@"{Application.StartupPath}\MonitoringConfig.xml";
            try
            {
                dataSet.ReadXml(xmlFileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                using (var xmlTextWriter = new XmlTextWriter(xmlFileName, System.Text.Encoding.UTF8))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;

                    xmlTextWriter.WriteStartDocument();

                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    xmlTextWriter.WriteComment(" Monitoring 환경설정 ");
                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                    xmlTextWriter.WriteStartElement("MonitoringConfig");

                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    xmlTextWriter.WriteComment(" ConnectionIP : MES서버 IP주소                           ");
                    xmlTextWriter.WriteComment(" ChangeTick   : 화면 갱신 주기 [단위:천분의일초] ");
                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    // xmlTextWriter.WriteElementString("ConnectionIP", "121.126.143.101");
                    xmlTextWriter.WriteElementString("ConnectionIP", "192.168.109.3");
                    xmlTextWriter.WriteElementString("ChangeTick", "10000");
                    xmlTextWriter.WriteEndElement();
                }

                try
                {
                    dataSet.ReadXml(xmlFileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InsertIntoSysLog(ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
                return false;
            }
        }

        private void InsertIntoSysLog(string strMsg)
        {
            strMsg = strMsg.Replace("'", "\x07");
            DbAccess.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Monitoring', '{Application.ProductName}', LEFT(ISNULL(N'{strMsg}',''),3000), '', GETDATE())");
        }
    }

    public static class ConfigData
    {
        public static string ConnectionIP { get; set; } = "";
    }
}
