using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;
using System.IO;
using System.Data.SqlClient;

namespace WiseM.Browser
{
    public partial class JigRepairInsert : SkinForm
    {
        //private CustomPanelLinkEventArgs e = null;
        byte[] content;
        private string connStr = null;
        private string SpecA_Min = string.Empty;
        private string SpecB_Min = string.Empty;
        private string SpecC_Min = string.Empty;
        private string SpecD_Min = string.Empty;
        private string SpecE_Min = string.Empty;
        private string SpecA_Max = string.Empty;
        private string SpecB_Max = string.Empty;
        private string SpecC_Max = string.Empty;
        private string SpecD_Max = string.Empty;
        private string SpecE_Max = string.Empty;
        private string JigMaintHist1 = string.Empty;
        private string Jigcode = string.Empty;
        private string TempPath = string.Empty;
        byte[] m_byteData;
        string m_fileName;
        public JigRepairInsert(string JigCode, string JigMaintHist)
        {
            InitializeComponent();
            //this.e = e;
            this.connStr = "Data Source=192.168.2.11; Initial Catalog=ElentecIN2Mes3; uid=wisem2; pwd=wisem2608";

            JigMaintHist1 = JigMaintHist;
            Jigcode = JigCode;
            string SearchFileData = " Select *, (Select FileData From Jig where Jig = w1.Jig) FileData From JigMaintHist   w1 where JigMaintHist = '" + JigMaintHist + "' ";
            DataTable dt = DbAccess.Default.GetDataTable(SearchFileData);

            string SearchJigBasicInfo = " Select * from Jig where Jig = '" + JigCode + "' ";
            DataTable dt1 = DbAccess.Default.GetDataTable(SearchJigBasicInfo);
            this.TB_JigCode.Text = JigCode;
            this.TB_JigCodeName.Text = dt1.Rows[0]["Text"].ToString();

            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecA_Min"].ToString()))
            { this.TB_SpecA_Min.Text = "0"; }
            else
            { this.TB_SpecA_Min.Text = dt1.Rows[0]["SpecA_Min"].ToString(); }

            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecB_Min"].ToString()))
            { this.TB_SpecB_Min.Text = "0"; }
            else
            { this.TB_SpecB_Min.Text = dt1.Rows[0]["SpecB_Min"].ToString(); }

            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecC_Min"].ToString()))
            { this.TB_SpecC_Min.Text = "0"; }
            else
            { this.TB_SpecC_Min.Text = dt1.Rows[0]["SpecC_Min"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecD_Min"].ToString()))
            { this.TB_SpecD_Min.Text = "0"; }
            else
            { this.TB_SpecD_Min.Text = dt1.Rows[0]["SpecD_Min"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecE_Min"].ToString()))
            { this.TB_SpecE_Min.Text = "0"; }
            else
            { this.TB_SpecE_Min.Text = dt1.Rows[0]["SpecE_Min"].ToString(); }
            

            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecA_Max"].ToString()))
            { this.TB_SpecA_Max.Text = "0"; }
            else
            { this.TB_SpecA_Max.Text = dt1.Rows[0]["SpecA_Max"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecB_Max"].ToString()))
            { TB_SpecB_Max.Text = "0"; }
            else
            { this.TB_SpecB_Max.Text = dt1.Rows[0]["SpecB_Max"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecC_Max"].ToString()))
            { this.TB_SpecC_Max.Text = "0"; }
            else
            { this.TB_SpecC_Max.Text = dt1.Rows[0]["SpecC_Max"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecD_Max"].ToString()))
            { this.TB_SpecD_Max.Text = "0"; }
            else
            { this.TB_SpecD_Max.Text = dt1.Rows[0]["SpecD_Max"].ToString(); }
            if (string.IsNullOrEmpty(dt1.Rows[0]["SpecE_Max"].ToString()))
            { this.TB_SpecE_Max.Text = "0"; }
            else
            { this.TB_SpecE_Max.Text = dt1.Rows[0]["SpecE_Max"].ToString(); }
           
            //이미지 파일 없을 경우 , 이미지 생성 안함.
            if (string.IsNullOrEmpty(dt.Rows[0]["FileData"].ToString()))
            {
            }
            else
            {
                this.ImageDatastread();
            }
        }
        //이미지 파일 Picture박스로 보여줄때 처음 함수 시작.
        private void ImageDatastread()
        {
            string connectionString = "Server=192.168.2.11;database=ElentecIN2Mes3;User ID=wisem;PWD=wisem";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            sqlConnection.Open();

            string queryString = string.Format("select  FileData,Getdate() From Jig Where Jig = '" + Jigcode + "' ");

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

        private void Btn_SAVE_Click(object sender, EventArgs e)
        {
            string SearchData = " Select * from JigMaintHist where JigMaintHist  = '" + JigMaintHist1 + "' ";
            DataTable Searchdt = DbAccess.Default.GetDataTable(SearchData);

            if (!string.IsNullOrEmpty(Searchdt.Rows[0]["MaintEnded"].ToString()))
            {
                MessageBox.Show("Not Insert Jig Repair , Is already repair data. \r\n Please confirm the previous data after processing ", "OK", MessageBoxIcon.None);
                return;
            }
            try
            {             
                 string UpdateQuery    =  "UPDATE [JigMaintHist] ";
                        UpdateQuery   += " SET [MaintEnded] = Getdate() ";
                        UpdateQuery   += " ,[Spec_A] = '" + this.TB_SpecA.Text + "' ";                          
                        UpdateQuery   += " ,[Spec_B] = '" + this.TB_SpecB.Text+ "' ";
                        UpdateQuery   += " ,[Spec_C] = '" + this.TB_SpecC.Text + "' ";
                        UpdateQuery   += " ,[Spec_D] = '" + this.TB_SpecD.Text + "' ";
                        UpdateQuery   += " ,[Spec_E] = '" + this.TB_SpecE.Text + "' ";
                        UpdateQuery   += " ,[VisualA_Judgement] = '" + this.ComboVisualA.Text + "' ";
                        UpdateQuery   += " ,[VisualB_Judgement] = '" + this.ComboVisualB.Text + "' ";
                        UpdateQuery   += " ,[VisualC_Judgement] = '" + this.ComboVisualC.Text + "' ";
                        UpdateQuery   += " ,[VisualD_Judgement] = '" + this.ComboVisualD.Text + "' ";
                        UpdateQuery   += " ,[VisualE_Judgement] = '" + this.ComboVisualE.Text + "' ";
                        UpdateQuery   += " ,[VisualA] = '" + this.Combo_VisualA.Text + "' ";
                        UpdateQuery   += " ,[VisualB] = '" + this.Combo_VisualB.Text + "' ";
                        UpdateQuery   += " ,[VisualC] = '" + this.Combo_VisualC.Text + "' ";
                        UpdateQuery   += " ,[VisualD] = '" + this.Combo_VisualD.Text + "' ";
                        UpdateQuery   += " ,[VisualE] = '" + this.Combo_VisualE.Text + "' ";
                        UpdateQuery   += " ,[VisualF_Judgement] = '" + this.ComboVisualF.Text + "' ";
                        UpdateQuery   += " ,[VisualG_Judgement] = '" + this.ComboVisualG.Text + "' ";
                        UpdateQuery   += " ,[VisualH_Judgement] = '" + this.ComboVisualH.Text + "' ";
                        UpdateQuery   += " ,[VisualI_Judgement] = '" + this.ComboVisualI.Text + "' ";
                        UpdateQuery   += " ,[VisualJ_Judgement] = '" + this.ComboVisualJ.Text + "' ";
                        UpdateQuery   += " ,[VisualF] = '" + this.Combo_VisualF.Text + "' ";
                        UpdateQuery   += " ,[VisualG] = '" + this.Combo_VisualG.Text + "' ";
                        UpdateQuery   += " ,[VisualH] = '" + this.Combo_VisualH.Text + "' ";
                        UpdateQuery   += " ,[VisualI] = '" + this.Combo_VisualI.Text + "' ";
                        UpdateQuery   += " ,[VisualJ] = '" + this.Combo_VisualJ.Text + "' ";
                        UpdateQuery   += " WHERE JigMaintHist = '" + JigMaintHist1 + "' ";            
               
                DbAccess.Default.ExecuteQuery(UpdateQuery);
                MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex+"", "OK", MessageBoxIcon.Warning);
            }
        }
        private void DataInsertQuery(string Query)
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

        private void JigRepairInsert_Load(object sender, EventArgs e)
        {
            string SearchFileData = " Select * From JigMaintHist w1 where JigMaintHist = '" + JigMaintHist1 + "' ";
            DataTable dt = DbAccess.Default.GetDataTable(SearchFileData);

            if (!string.IsNullOrEmpty(dt.Rows[0]["MaintEnded"].ToString()))
            {
                System.Windows.Forms.MessageBox.Show("해당 지그는 현재 'Confirm대기' 상태입니다. \r\nJig đang trong trạng thái 'Đợi comfirm'");
                this.Close();
            }
        }
        //Spec판정
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TB_SpecA.Text))
            { this.TB_SpecA.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecB.Text))
            { this.TB_SpecB.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecC.Text))
            { this.TB_SpecC.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecD.Text))
            { this.TB_SpecD.Text = "0"; }
            if (string.IsNullOrEmpty(this.TB_SpecE.Text))
            { this.TB_SpecE.Text = "0"; }


            if (Convert.ToDouble(this.TB_SpecA_Min.Text) <= Convert.ToDouble(this.TB_SpecA.Text) && Convert.ToDouble(this.TB_SpecA_Max.Text) >= Convert.ToDouble(this.TB_SpecA.Text))
            {
                this.SpecA_Judgement.BackColor = Color.Yellow;
                this.SpecA_Judgement.ForeColor = Color.Blue;
                this.SpecA_Judgement.Text = "OK";
            }
            else
            {
                this.SpecA_Judgement.BackColor = Color.Yellow;
                this.SpecA_Judgement.ForeColor = Color.Red;
                this.SpecA_Judgement.Text = "NG";
            }

            if (Convert.ToDouble(this.TB_SpecB_Min.Text) <= Convert.ToDouble(this.TB_SpecB.Text) && Convert.ToDouble(this.TB_SpecB_Max.Text) >= Convert.ToDouble(this.TB_SpecB.Text))
            {
                this.SpecB_Judgement.BackColor = Color.Yellow;
                this.SpecB_Judgement.ForeColor = Color.Blue;
                this.SpecB_Judgement.Text = "OK";
            }
            else
            {
                this.SpecB_Judgement.BackColor = Color.Yellow;
                this.SpecB_Judgement.ForeColor = Color.Red;
                this.SpecB_Judgement.Text = "NG";
            }

            if (Convert.ToDouble(this.TB_SpecC_Min.Text) <= Convert.ToDouble(this.TB_SpecC.Text) && Convert.ToDouble(this.TB_SpecC_Max.Text) >= Convert.ToDouble(this.TB_SpecC.Text))
            {
                this.SpecC_Judgement.BackColor = Color.Yellow;
                this.SpecC_Judgement.ForeColor = Color.Blue;
                this.SpecC_Judgement.Text = "OK";
            }
            else
            {
                this.SpecC_Judgement.BackColor = Color.Yellow;
                this.SpecC_Judgement.ForeColor = Color.Red;
                this.SpecC_Judgement.Text = "NG";
            }

            if (Convert.ToDouble(this.TB_SpecD_Min.Text) <= Convert.ToDouble(this.TB_SpecD.Text) && Convert.ToDouble(this.TB_SpecD_Max.Text) >= Convert.ToDouble(this.TB_SpecD.Text))
            {
                this.SpecD_Judgement.BackColor = Color.Yellow;
                this.SpecD_Judgement.ForeColor = Color.Blue;
                this.SpecD_Judgement.Text = "OK";
            }
            else
            {
                this.SpecD_Judgement.BackColor = Color.Yellow;
                this.SpecD_Judgement.ForeColor = Color.Red;
                this.SpecD_Judgement.Text = "NG";
            }

            if (Convert.ToDouble(this.TB_SpecE_Min.Text) <= Convert.ToDouble(this.TB_SpecE.Text) && Convert.ToDouble(this.TB_SpecE_Max.Text) >= Convert.ToDouble(this.TB_SpecE.Text))
            {
                this.SpecE_Judgement.BackColor = Color.Yellow;
                this.SpecE_Judgement.ForeColor = Color.Blue;
                this.SpecE_Judgement.Text = "OK";
            }
            else
            {
                this.SpecE_Judgement.BackColor = Color.Yellow;
                this.SpecE_Judgement.ForeColor = Color.Red;
                this.SpecE_Judgement.Text = "NG";
            }
        }
    }
}
