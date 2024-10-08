
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Drawing;

namespace PdaServerProgram
{
    public partial class FormMonitoringUpload : Form
    {
        //string fileName;
        string connectionString;
        //TcpListener tcpListener;
        //Socket clientsocket;
        string ip;

        string bunch;
        string[] fileNames;
        public FormMonitoringUpload()
        {
            InitializeComponent();

            ip = "121.126.143.101";
            connectionString = "server = " + ip + "; uid = wisem2; pwd = wisem2608; database = Y2sVn1Mes3;";
            comboBoxKindAdd();
            lb_IP.Text = ip;
        }

        private void Clear()
        {
            bunch = string.Empty;
            comboBoxKindAdd();
            fileNames = null;
            dataGridView1.Rows.Clear();
            dataGridView2.DataSource = null;
        }
        private void btn_Upload_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.ColumnCount = 2;
                dataGridView1.Columns[0].Name = "folder";
                dataGridView1.Columns[1].Name = "filename";
                fileNames = new string[open.FileNames.Length];
                for (int i = 0; i < open.FileNames.Length; i++)
                {
                    fileNames[i] = open.FileNames[i].ToString();
                    string[] temp = new string[fileNames[i].Split('\\').Length];
                    temp = fileNames[i].Split('\\');
                    dataGridView1.Rows.Add(this.tb_Folder.Text.Trim().Trim('\\'), temp[temp.Length - 1]);
                }
            }
        }


        private void comboBoxKindAdd()
        {
            if (cb_kind.Items.Count != 0)
            {
                cb_kind.Items.Clear();
            }


            string temp = "Select Bunch From ProgramFile_Upload Group by Bunch";

            using (SqlConnection _con = new SqlConnection(connectionString))
            using (SqlCommand _cmd = new SqlCommand(temp, _con))
            {
                _con.Open();
                SqlDataReader dr = _cmd.ExecuteReader();
                while (dr.Read())
                {
                    cb_kind.Items.Add(dr.GetString(0));
                }
                _con.Close();
            }

        }


        private void cb_kind_SelectedIndexChanged(object sender, EventArgs e)
        {
            bunch = cb_kind.SelectedItem == null ? "" : cb_kind.SelectedItem.ToString();

            string query = "";
            if (string.IsNullOrEmpty(bunch))
                query = "Select filename,version,updated from ProgramFile_Upload";
            else
                query = "Select filename,version,updated from ProgramFile_Upload where bunch = '" + bunch + "'";

            using (SqlConnection _con = new SqlConnection(connectionString))
            using (SqlCommand _cmd = new SqlCommand(query, _con))
            {
                _con.Open();

                DataTable dt = new DataTable();
                dt.Load(_cmd.ExecuteReader());

                dataGridView2.DataSource = dt;
                _con.Close();
            }
        }

        private void btninsert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(bunch))
            {
                MessageBox.Show("업로드 종류를 선택 해 주세요.");
                return;
            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("업로드 파일을 선택 해 주세요._Select Count 0");
                return;
            }
            
            if(fileNames == null)
            {
                MessageBox.Show("업로드 파일을 선택 해 주세요._FileNames Null");
                return;
            }

            for (int i = 0; i < fileNames.Length; i++)
            {
                FileStream fs = new FileStream(fileNames[i], FileMode.Open, FileAccess.Read);
                int fileLength = (int)fs.Length;
                FileInfo fi = new FileInfo(fileNames[i]);

                BinaryReader br = new BinaryReader(fs);

                byte[] buffer;
                buffer = br.ReadBytes(fileLength);

                int seq = 0;
                string tempVersion = "";

                for (int z = 0; z < dataGridView2.Rows.Count; z++)
                {
                    if (dataGridView2.Rows[z].Cells[0].Value.ToString().Equals(dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        tempVersion = dataGridView2.Rows[z].Cells[1].Value.ToString();
                    }

                }

                if (!string.IsNullOrEmpty(tempVersion))
                {
                    if (tempVersion.Split('-')[2].Equals(DateTime.Now.ToString("dd")))
                    {
                        seq = int.Parse(tempVersion.Split('-')[3]) + 1;
                    }
                    else
                    {
                        seq = 1;
                    }
                }
                else
                    seq = 1;

                string version = DateTime.Now.ToString("yyyy-MM-dd") + "-" + seq;


                string strFolder = dataGridView1.Rows[i].Cells[0].Value.ToString();
                string strFileName = dataGridView1.Rows[i].Cells[1].Value.ToString();

                string queryStmt = "IF EXISTS(Select Version from ProgramFile_Upload where folder='" + strFolder + "' and filename = '" + strFileName + "' and Bunch = '" + bunch + "') \n"
                                   + "begin \n"
                                   + "    update ProgramFile_Upload set data = @Content, Updated = getdate(), Version = '" + version + "' where folder='" + strFolder + "' and filename = '" + strFileName + "' and Bunch = '" + bunch + "' \n"
                                   + "end \n"
                                   + "else \n"
                                   + "begin \n"
                                   + "    insert into ProgramFile_Upload (data,folder,filename,Bunch,Updated,Version) values (@Content, '" + strFolder + "', '" + strFileName + "','" + bunch + "',GETDATE(), '" + version + "') \n"
                                   + "end";

                using (SqlConnection _con = new SqlConnection(connectionString))
                using (SqlCommand _cmd = new SqlCommand(queryStmt, _con))
                {
                    _cmd.CommandTimeout = 0;
                    SqlParameter param = _cmd.Parameters.Add("@Content", SqlDbType.VarBinary);
                    param.Value = buffer;

                    _con.Open();
                    _cmd.ExecuteNonQuery();
                    _con.Close();
                }
                br.Close();
                fs.Close();
            }
            MessageBox.Show("Completion.", "PdaServer");
            Clear();
        }

        private void btn_kindAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_addKind.Text))
            {
                MessageBox.Show("종류를 입력 해 주세요.", "PdaServer");
                return;
            }


            string tempBunch = string.Empty;
            string result = string.Empty;

            tempBunch = tb_addKind.Text;
            tb_addKind.Text = string.Empty;

            string temp = "IF EXISTS(Select filename from ProgramFile_Upload where Bunch = '" + tempBunch + "') \n"
                              + "begin \n"
                              + "    Select 'f' \n"
                              + "end \n"
                              + "else \n"
                              + "begin \n"
                              + "select 't' \n"
                              + "end";

            using (SqlConnection _con = new SqlConnection(connectionString))
            using (SqlCommand _cmd = new SqlCommand(temp, _con))
            {
                _con.Open();
                SqlDataReader dr = _cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = (dr.GetString(0));
                }
                _con.Close();
            }

            if (result.Equals("f"))
            {
                MessageBox.Show("해당 종류는 이미 등록되어 있습니다.");
                return;
            }

            string temp2 = "insert into ProgramFile_Upload (data,filename,Bunch,Updated,Version) values (NULL , Null, '" + tempBunch + "' ,GETDATE(), '')";

            using (SqlConnection _con = new SqlConnection(connectionString))
            using (SqlCommand _cmd = new SqlCommand(temp2, _con))
            {
                _cmd.CommandTimeout = 0;
                _con.Open();
                _cmd.ExecuteNonQuery();
                _con.Close();
            }

            Clear();
        }
    }
}
