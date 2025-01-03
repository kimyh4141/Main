using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace PdaServerProgram
{
    public partial class FormAndroidUpload : Form
    {
        string connectionString;
        string ip;

        string[] fileNames;

        public FormAndroidUpload()
        {
            InitializeComponent();
            //string ip = "123.235.1.131";
            //string ip = "121.126.143.101";
            //string ip = "192.168.169.3";
            string ip = "192.168.109.3";
            string uid = "wisem2";
            string pwd = "wisem2608";
            string database = "Y2sVn1Mes3";
            //string database = "Y2sCn1Mes3";

            connectionString = $"server = {ip};uid = {uid};pwd = {pwd};database = {database};Connection Timeout=30";
            lb_IP.Text = ip;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }


        private void Clear()
        {
            fileNames = null;
            dataGridView_UploadList.Rows.Clear();
            dataGridView2.DataSource = null;
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            dataGridView_UploadList.Rows.Clear();

            fileNames = new string[openFileDialog.FileNames.Length];
            for (var i = 0; i < openFileDialog.FileNames.Length; i++)
            {
                fileNames[i] = openFileDialog.FileNames[i];
                string[] temp = new string[fileNames[i].Split('\\').Length];
                temp = fileNames[i].Split('\\');
                dataGridView_UploadList.Rows.Add("", temp[temp.Length - 1], 0, "", 0);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
            Environment.Exit(0);
        }


        private void btninsert_Click(object sender, EventArgs e)
        {
            // if (string.IsNullOrEmpty(bunch))
            // {
            //     MessageBox.Show("업로드 종류를 선택 해 주세요.");
            //     return;
            // }

            if (dataGridView_UploadList.Rows.Count == 0)
            {
                MessageBox.Show("업로드 파일을 선택 해 주세요._Select Count 0");
                return;
            }

            if (fileNames == null)
            {
                MessageBox.Show("업로드 파일을 선택 해 주세요._FileNames Null");
                return;
            }

            for (int i = 0; i < fileNames.Length; i++)
            {
                var fileStream = new FileStream(fileNames[i], FileMode.Open, FileAccess.Read);
                int fileLength = (int)fileStream.Length;
                var fileInfo = new FileInfo(fileNames[i]);
                var binaryReader = new BinaryReader(fileStream);
                byte[] binary = binaryReader.ReadBytes(fileLength);

                int seq = 0;

                string query = $@"
                                BEGIN
                                    INSERT
                                      INTO ReleaseCustomFile
                                          (
                                              Type
                                          ,   Name
                                          ,   Version
                                          ,   FileBinary
                                          ,   Remark
                                          ,   Status
                                          ,   Updated
                                          )
                                    VALUES
                                        (
                                            @Type
                                        ,   @Name
                                        ,   @Version
                                        ,   @FileBinary
                                        ,   @Remark
                                        ,   @Status
                                        ,   GETDATE()
                                        )
                                END
                                ";


                using (var con = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.CommandTimeout = 0;
                    SqlParameterCollection sqlParameterCollection = cmd.Parameters;
                    sqlParameterCollection.Add("@Type", SqlDbType.NVarChar, 50).Value =
                        dataGridView_UploadList.Rows[i].Cells["Type"].Value;
                    sqlParameterCollection.Add("@Name", SqlDbType.NVarChar, 50).Value =
                        dataGridView_UploadList.Rows[i].Cells["Name"].Value;
                    sqlParameterCollection.Add("@Version", SqlDbType.Int).Value =
                        dataGridView_UploadList.Rows[i].Cells["Version"].Value;
                    sqlParameterCollection.Add("@FileBinary", SqlDbType.VarBinary).Value = binary;
                    sqlParameterCollection.Add("@Remark", SqlDbType.NVarChar, 0).Value =
                        dataGridView_UploadList.Rows[i].Cells["Remark"].Value;
                    sqlParameterCollection.Add("@Status", SqlDbType.Bit).Value =
                        dataGridView_UploadList.Rows[i].Cells["Status"].Value.ToString() != "0";
                    // sqlParameterCollection.AddWithValue("@Type", dataGridView_UploadList.Rows[i].Cells["Type"]);
                    // sqlParameterCollection.AddWithValue("@Name", dataGridView_UploadList.Rows[i].Cells["Name"]);
                    // sqlParameterCollection.AddWithValue("@Version", dataGridView_UploadList.Rows[i].Cells["Version"]);
                    // sqlParameterCollection.AddWithValue("@FileBinary", binary);
                    // sqlParameterCollection.AddWithValue("@Remark", dataGridView_UploadList.Rows[i].Cells["Remark"]);
                    // sqlParameterCollection.AddWithValue("@Status", dataGridView_UploadList.Rows[i].Cells["Status"]);
                    //sqlParameterCollection.AddWithValue("@FileBinary", binary);
                    //SqlParameter param = _cmd.Parameters.Add("@Content", SqlDbType.VarBinary);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                binaryReader.Close();
                fileStream.Close();
            }

            MessageBox.Show("Completion.", "PdaServer");

            Clear();
            OpenGridViewTypeList();
        }

        private void dataGridView_UploadList_BindingContextChanged(object sender, EventArgs e)
        {
            dataGridView_UploadList.Columns.Clear();
            dataGridView_UploadList.Columns.Add("Type", "Type");
            dataGridView_UploadList.Columns.Add("Name", "Name");
            dataGridView_UploadList.Columns.Add("Version", "Version");
            dataGridView_UploadList.Columns.Add("Remark", "Remark");
            dataGridView_UploadList.Columns.Add("Status", "Status");
        }

        private void OpenGridViewTypeList()
        {
            string query = $@"
                            SELECT Type
                                 , MAX(Version) AS Version
                                 , MAX(Updated) AS Updated
                              FROM ReleaseCustomFile
                             GROUP BY
                                 Type
                            ;
                            ";

            using (var sqlConnection = new SqlConnection(connectionString))
            using (var sqlCommand = new SqlCommand(query, sqlConnection))
            {
                sqlConnection.Open();
                var dataTable = new DataTable();
                dataTable.Load(sqlCommand.ExecuteReader());
                dataGridView2.DataSource = dataTable;
                sqlConnection.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OpenGridViewTypeList();
        }
    }
}