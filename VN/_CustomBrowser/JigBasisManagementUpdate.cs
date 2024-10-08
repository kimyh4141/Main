using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;
using System.IO;
using System.Data.SqlClient;

namespace WiseM.Browser
{
    public partial class JigBasisManagementUpdate : SkinForm
    {
        //private CustomPanelLinkEventArgs e = null;
        byte[] content;
        private string connStr = null;

        private string TempPath = string.Empty;
        private string TempAttachData = "NULL";
        byte[] m_byteData;
        string m_fileName;
        private string JigCode;

        public JigBasisManagementUpdate(string JigCode1)
        {
            InitializeComponent();
            //this.e = e;
            JigCode = JigCode1;
            //this.connStr = "Data Source=172.16.1.20; Initial Catalog=ElentecVn1Mes3; uid=wisem; pwd=Passw0rd!";
            this.connStr = DbAccess.Default.ConnectionString;

            string DataQuery = " Select w1.*,(Select NextMaintDate From Jiginfo where Jig = w1.Jig) NextMaintDate From Jig w1 Where Jig = '" + JigCode + "' ";
            DataTable dt = DbAccess.Default.GetDataTable(DataQuery);

            this.TB_JigCode.Text = dt.Rows[0]["Jig"].ToString();
            this.TB_JigCodeName.Text = dt.Rows[0]["Text"].ToString();
           
            this.TB_LocationGroup.Text = dt.Rows[0]["LocationBunch"].ToString();
            this.TB_Location.Text = dt.Rows[0]["Location"].ToString();
            this.TB_MaintPeriod.Text = dt.Rows[0]["MaintPeriord"].ToString();
            this.TB_MaintDate.Text = dt.Rows[0]["MaintDate"].ToString();
            this.TB_SpecA_Min.Text = dt.Rows[0]["SpecA_Min"].ToString();
            this.TB_SpecB_Min.Text = dt.Rows[0]["SpecB_Min"].ToString();
            this.TB_SpecC_Min.Text = dt.Rows[0]["SpecC_Min"].ToString();
            this.TB_SpecD_Min.Text = dt.Rows[0]["SpecD_Min"].ToString();
            this.TB_SpecE_Min.Text = dt.Rows[0]["SpecE_Min"].ToString();
            this.TB_SpecA_Max.Text = dt.Rows[0]["SpecA_Max"].ToString();
            this.TB_SpecB_Max.Text = dt.Rows[0]["SpecB_Max"].ToString();
            this.TB_SpecC_Max.Text = dt.Rows[0]["SpecC_Max"].ToString();
            this.TB_SpecD_Max.Text = dt.Rows[0]["SpecD_Max"].ToString();
            this.TB_SpecE_Max.Text = dt.Rows[0]["SpecE_Max"].ToString();
            if (dt.Rows[0]["CurrentStatus"].ToString() == "4")
            {
                this.CB_Limit.Checked = true;
                this.Date_Discarded.Text = Convert.ToDateTime(dt.Rows[0]["Discarded"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                this.TB_Discarder.Text = dt.Rows[0]["Discarder"].ToString();
                this.TB_Discarreason.Text = dt.Rows[0]["DiscardReason"].ToString();
                this.TB_Discarder.ReadOnly = false;
                this.TB_Discarreason.ReadOnly = false;
            }
            else
            {
                
                this.CB_Limit.Checked = false;
                this.TB_Discarder.ReadOnly = true;
                this.TB_Discarreason.ReadOnly = true;

            }
            this.MaintPeriodUnit();
            this.Customer();
            this.Model();
            this.Combo_Customer.Text = dt.Rows[0]["Customer"].ToString();
            this.Combo_Model.Text = dt.Rows[0]["Model"].ToString();

            if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["MaintPeriodUnit"])) == false)
                this.Combo_MaintPeriodUnit.Text = this.Combo_MaintPeriodUnit.Items.OfType<string>().Where(x => x.StartsWith(Convert.ToString(dt.Rows[0]["MaintPeriodUnit"]))).FirstOrDefault();

            //이미지 파일 없을 경우 , 이미지 생성 안함.
            if (string.IsNullOrEmpty(dt.Rows[0]["FileData"].ToString()))
            {
            }
            else
            {
                this.ImageDatastread();
            }
        }
        private void Customer()
        {
            this.Combo_Customer.Text = string.Empty;
            this.Combo_Customer.Items.Clear();
            string SelectQuery = " Select  * From Customer where status = 1 ";
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_Customer.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_Customer.Items.Add(dt.Rows[i]["Customer"].ToString().Trim());
                }
            }
        }

        private void Model()
        {
            this.Combo_Model.Text = string.Empty;
            this.Combo_Model.Items.Clear();
            //string SelectQuery = " select Distinct Model  from MaterialMapping where Status = 1 order by Model  ";
            string SelectQuery = "select Model from Material";
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_Model.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_Model.Items.Add(dt.Rows[i]["Model"].ToString().Trim());
                }
            }
        }
        //보수 주기단위 설정
        private void MaintPeriodUnit()
        {
            this.Combo_MaintPeriodUnit.Items.Clear();
            this.Combo_MaintPeriodUnit.Items.Add("Y : Year");
            this.Combo_MaintPeriodUnit.Items.Add("M : Month");
            this.Combo_MaintPeriodUnit.Items.Add("W : Week");
            this.Combo_MaintPeriodUnit.Items.Add("D : Day");
        }
        //이미지 파일 Picture박스로 보여줄때 처음 함수 시작.
        private void ImageDatastread()
        {
            //string connectionString = "Data Source=172.16.1.20; Initial Catalog=ElentecVn1Mes3; uid=wisem; pwd=Passw0rd!";
            string connectionString = DbAccess.Default.ConnectionString;
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            sqlConnection.Open();

            string queryString = string.Format("select  FileData,Getdate() From Jig Where Jig = '" + JigCode + "' ");

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            byte[] bData = new byte[0];
            bData = (byte[])reader[0];

            string filename = Convert.ToDateTime(reader[1].ToString()).ToString("yyyyMMddHHmmss") + ".jpg";
            string temp = filename.Substring(filename.LastIndexOf(@".") + 1, filename.Length - filename.LastIndexOf(@".") - 1).ToLower();

            switch (temp)
            {
                case "bmp":
                case "jpg":
                case "gif":
                case "png":
                    pictureBox1.Image = ConvertByteArrayToImage(bData);
                    break;
                default:
                    FileSave(bData, filename, temp);
                    break;
            }

            reader.Close();
            cmd.Dispose();
            sqlConnection.Close();
        }
        //이미지 파일일 경우 바로 Picture박스에 뿌려준다.
        public Image ConvertByteArrayToImage(byte[] pByte)
        {
            MemoryStream memoryStream = new MemoryStream();
            Image convertImage = null;

            try
            {
                memoryStream.Position = 0;
                memoryStream.Write(pByte, 0, (Int32)pByte.Length);
                convertImage = Image.FromStream(memoryStream);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return convertImage;
        }
        //이미지 파일이 아닌경우 해당 파일을 생성해준다.
        public void FileSave(byte[] data, string fileName, string fileExt)
        {
            FileDialog fileDlg = new SaveFileDialog();
            fileDlg.Filter = "files All files (*.*)|*.*";
            fileDlg.DefaultExt = fileExt;
            fileDlg.FileName = fileName;

            FileStream fileStream = null;

            //@"C:\NG.wav"
            string path = "C:\\" + fileName;

            try
            {
                byte[] byteData = new byte[0];

                byteData = data;
                double ArraySize = new double();
                ArraySize = byteData.GetUpperBound(0);


                fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                BinaryWriter b = new BinaryWriter(fileStream);
                b.Write(byteData);



                fileStream.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (fileStream != null) { fileStream.Close(); fileStream = null; }
            }
            pictureBox1.Image = Image.FromFile(path);

        }
        //이미지 선택
        private void Btn_FileChoice_Click(object sender, EventArgs e)
        {
            FileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "All files (*.*)|*.*|BMP(*.bmp)|*.bmp|(*.jpg)|*.jpg|(*.gif)|*.gif|(*.png)|*.png";
            fileDlg.InitialDirectory = @"C:\";

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                string temp = Path.GetExtension(fileDlg.FileName.ToLower());
                m_fileName = Path.GetFileName(fileDlg.FileName.ToLower());

                switch (temp)
                {
                    case ".bmp":
                    case ".jpg":
                    case ".gif":
                    case ".png":
                        {
                            Image image = Image.FromFile(fileDlg.FileName);
                            byte[] imageData = ConvertImageToByteArray(image);
                            m_byteData = imageData;
                        }
                        break;
                    default:
                        {
                            FileStream fs = new FileStream(fileDlg.FileName, FileMode.OpenOrCreate, FileAccess.Read);

                            byte[] MyData = new byte[fs.Length];
                            fs.Read(MyData, 0, System.Convert.ToInt32(fs.Length));
                            fs.Close();

                            m_byteData = new byte[0];
                            m_byteData = MyData;
                        }
                        break;
                }
                pictureBox1.Image = Image.FromFile(fileDlg.FileName);
            }

        }
        //파일변환
        public byte[] ConvertImageToByteArray(Image theImage)
        {
            MemoryStream memoryStream = new MemoryStream();
            byte[] convertByte;

            theImage.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            convertByte = new byte[memoryStream.Length];
            memoryStream.Position = 0;
            memoryStream.Read(convertByte, 0, (int)memoryStream.Length);
            memoryStream.Close();

            return convertByte;
        }
        //저장 버튼 클릭시.
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Model.Text))
            {
                MessageBox.Show("Error! Data Setting is fault.", "Error", MessageBoxIcon.Error);
                return;
            }
            if (this.m_byteData != null)
            {
                this.TempAttachData = "@AttechData";

            }
            if (string.IsNullOrEmpty(this.TB_SpecA_Min.Text))
            { this.TB_SpecA_Min.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecB_Min.Text))
            { this.TB_SpecB_Min.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecC_Min.Text))
            { this.TB_SpecC_Min.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecD_Min.Text))
            { this.TB_SpecD_Min.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecE_Min.Text))
            { this.TB_SpecE_Min.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecA_Max.Text))
            { this.TB_SpecA_Max.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecB_Max.Text))
            { this.TB_SpecB_Max.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecC_Max.Text))
            { this.TB_SpecC_Max.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecD_Max.Text))
            { this.TB_SpecD_Max.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecE_Max.Text))
            { this.TB_SpecE_Max.Text = "0"; }
            

            if (WiseM.MessageBox.Show("Are you sure? ", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                try
                {
                    string UpdateQuery = " Update Jig Set ";
                    UpdateQuery += " Text = N'" + this.TB_JigCodeName.Text + "', ";
                    UpdateQuery += " Customer = N'" + this.Combo_Customer.Text + "' ,";
                    UpdateQuery += " Model = N'" + this.Combo_Model.Text + "' ,";
                    UpdateQuery += " MaintPeriodUnit = N'" + this.Combo_MaintPeriodUnit.Text.Split(':')[0].ToString().Trim() + "', ";
                    UpdateQuery += " MaintPeriord = N'" + this.TB_MaintPeriod.Text + "', ";
                    UpdateQuery += " MaintDate = N'" + this.TB_MaintDate.Text + "', ";
                    if (this.CB_Limit.Checked == true)
                    {
                        UpdateQuery += " Discarded = N'" + Convert.ToDateTime(this.Date_Discarded.Text).ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                        UpdateQuery += " Discarder = N'" + this.TB_Discarder.Text + "', ";
                        UpdateQuery += " DiscardReason = N'" + this.TB_Discarreason.Text + "' ,";
                        UpdateQuery += " Status = 0 , CurrentStatus = 4, ";
                    }
                    else
                    {
                        
                    }
                    UpdateQuery += " SpecA_Min = " + this.TB_SpecA_Min.Text + ", ";
                    UpdateQuery += " SpecB_Min = " + this.TB_SpecB_Min.Text + ", ";
                    UpdateQuery += " SpecC_Min = " + this.TB_SpecC_Min.Text + ", ";
                    UpdateQuery += " SpecD_Min = " + this.TB_SpecD_Min.Text + ", ";
                    UpdateQuery += " SpecE_Min = " + this.TB_SpecE_Min.Text + ", ";
                    UpdateQuery += " SpecA_Max = " + this.TB_SpecA_Max.Text + ", ";
                    UpdateQuery += " SpecB_Max = " + this.TB_SpecB_Max.Text + ", ";
                    UpdateQuery += " SpecC_Max = " + this.TB_SpecC_Max.Text + ", ";
                    UpdateQuery += " SpecD_Max = " + this.TB_SpecD_Max.Text + ", ";
                    UpdateQuery += " SpecE_Max = " + this.TB_SpecE_Max.Text + ", ";
                    UpdateQuery += " updater = N'" + WiseApp.Id + "', ";
                    UpdateQuery += " Updated = Getdate() ";
                    if (this.m_byteData == null)
                    { }
                    else
                    {
                        UpdateQuery += " ,FileData = " + TempAttachData + " ";
                    }
                    UpdateQuery += " where Jig = N'" + this.TB_JigCode.Text + "' ";

                    DataUpdateQuery(UpdateQuery);
                    MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(" " + ex + " ", "Error", MessageBoxIcon.Error);
                }

            }
        }

        private void DataUpdateQuery(string Query)
        {

            SqlConnection conn = new SqlConnection(this.connStr);
            SqlCommand comm = new SqlCommand(Query);

            if (this.m_byteData == null)
            {
                conn.Open();
                comm.Connection = conn;
                DbAccess.Default.ExecuteQuery(comm);

            }
            else
            {
                conn.Open();
                comm.Connection = conn;
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@AttechData", m_byteData);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            conn.Close();
        }
        //주기 선택시
        private void Combo_MaintPeriodUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Combo_MaintPeriodUnit.Text.Split(':')[0].Trim() == "D")
            {
                this.TB_MaintPeriod.Text = "0";
                this.TB_MaintDate.Text = "0";
                this.TB_MaintPeriod.ReadOnly = true;
                this.TB_MaintDate.ReadOnly = true;
            }
            else
            {
                this.TB_MaintPeriod.ReadOnly = false;
                this.TB_MaintDate.ReadOnly = false;
            }
        }

        private void CB_Limit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.CB_Limit.Checked == true)
            {
                if (!string.IsNullOrEmpty(this.TB_Discarder.Text))
                {
                    MessageBox.Show("Error! is already disused.", "Error", MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    this.TB_Discarder.ReadOnly = false;
                    this.TB_Discarreason.ReadOnly = false;
                }
            }
            else
            {
                this.TB_Discarder.ReadOnly = true;
                this.TB_Discarreason.ReadOnly = true;
            }
        } 
    }
}
