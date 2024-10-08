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
    public partial class JigBasisManagement : SkinForm
    {
        //private CustomPanelLinkEventArgs e = null;
        byte[] content;
        private string connStr = null;

        private string TempPath = string.Empty;
        private string TempAttachData = "NULL";
        byte[] m_byteData;
        string m_fileName;

        public JigBasisManagement()
        {
            InitializeComponent();
            //this.connStr = "Data Source=172.16.1.20; Initial Catalog=ElentecVn1Mes3; uid=wisem; pwd=Passw0rd!";
            this.connStr = DbAccess.Default.ConnectionString;
            this.Customer();
            this.LocationGroup();
            this.MaintPeriodUnit();
            this.Model();

            this.TB_JigCode.Focus();
        }
        //모델 데이터 추출
        private void Model()
        {
            this.Combo_Model.Text = string.Empty;
            this.Combo_Model.Items.Clear();
            //string SelectQuery = " select Distinct Model  from MaterialMapping where Status = 1 order by Model  ";
            string SelectQuery = "select Distinct Model from Material ";
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
            this.Combo_MaintPeriodUnit.Text = string.Empty;
            this.Combo_MaintPeriodUnit.Items.Clear();
            this.Combo_MaintPeriodUnit.Items.Add("Y : Year");
            this.Combo_MaintPeriodUnit.Items.Add("M : Month");
            this.Combo_MaintPeriodUnit.Items.Add("W : Week");
            this.Combo_MaintPeriodUnit.Items.Add("D : Day");
        }

        //지그 위치 그룹
        private void LocationGroup()
        {
            this.Combo_LocationGroup.Text = string.Empty;
            this.Combo_LocationGroup.Items.Clear();
            string SelectQuery = " Select  * From Common where Category = '600' and  status = 1 ";
            DataTable dt = DbAccess.Default.GetDataTable(SelectQuery);

            if (dt.Rows.Count <= 0)
                return;
            else
            {
                this.Combo_LocationGroup.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.Combo_LocationGroup.Items.Add(dt.Rows[i]["Common"].ToString().Trim() + "/" + dt.Rows[i]["Text"].ToString().Trim());
                }
            }
        }
        //지그 위치 선택 시 발동
        private void Combo_LocationGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Combo_Location.Text = string.Empty;
            this.Combo_Location.Items.Clear();
            if (this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL02" || this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL21")
            {
                this.Combo_Location.Items.Add("None");
            }
            else if (this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() == "JL01")
            {
                this.Combo_Location.Items.Add("In");
                this.Combo_Location.Items.Add("Out");
            }
            else
            {
                this.Combo_Location.Items.Add("None");
            }
        }
        //고객사 데이터 추출
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

        //파일 첨부 
        private void button1_Click(object sender, EventArgs e)
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
        //신규
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

        //image 파일 Bitmap 으로 변환
        private byte[] ReadBitmap2ByteArray(string fileName)
        {
            using (Bitmap attachdata = new Bitmap(fileName))
            {
                MemoryStream stream = new MemoryStream();
                attachdata.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                return stream.ToArray();
            }
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
        //주기 관련 마지막 입력 후 , 확인
        private void button2_Click(object sender, EventArgs e)
        {
            //입력했을 때, 메세지 처리 
            #region
            if (string.IsNullOrEmpty(this.TB_MaintPeriod.Text))
            {
                return;
            }

            if (this.Combo_MaintPeriodUnit.Text.Split(':')[0].Trim() == "Y")
            {
                string YearNo = string.Empty;
                string YYQuery = string.Empty;
                string YearsCount = this.TB_MaintPeriod.Text;
                string StartDate = Convert.ToDateTime(this.Date_MaintStartDate.Text).ToString("yyyy-01-01");
                if (Convert.ToInt16(this.TB_MaintDate.Text) >= 0 || Convert.ToInt16(this.TB_MaintDate.Text) < 365)
                {
                    YearNo = this.TB_MaintDate.Text + " 째날";
                    YYQuery = "select  Dateadd(DD," + this.TB_MaintDate.Text + ",DATEADD(YY," + YearsCount + ",'" + StartDate + "')) DueDate ";
                }
                else
                {
                    MessageBox.Show("Error! Date Setting is fault", "Error", MessageBoxIcon.Error);
                    this.TB_MaintDate.Text = "0";
                    this.TB_MaintPeriod.Text = "1";
                    return;
                }
                DataTable YYQuerydt = DbAccess.Default.GetDataTable(YYQuery);

                this.TB_Remark.Text = "매" + this.TB_MaintPeriod.Text + " 년 주기로" + YearNo + " 보수처리 해야합니다.";
                this.TB_DueDate.Text = Convert.ToDateTime(YYQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            }
            else if (this.Combo_MaintPeriodUnit.Text.Split(':')[0].Trim() == "M")
            {
                string DayNo = string.Empty;
                string MMQuery = string.Empty;
                string DaysCount = this.TB_MaintPeriod.Text;
                string StartDate = Convert.ToDateTime(this.Date_MaintStartDate.Text).ToString("yyyy-MM-dd");
                int maintDate = 0;

                if (this.TB_MaintDate.Text == "0")
                {
                    DayNo = "";
                    MMQuery = "select DATEADD(MM," + DaysCount + ",'" + StartDate + "') DueDate ";
                }
                else if (int.TryParse(this.TB_MaintDate.Text, out maintDate) == false)
                {
                    MessageBox.Show(" ", "Error", MessageBoxIcon.Error);
                    this.TB_MaintDate.Text = "0";
                    this.TB_MaintPeriod.Text = "1";
                    return;
                }
                else if (maintDate > 31 || maintDate < 1)
                {
                    MessageBox.Show("Error! Date Setting is fault", "Error", MessageBoxIcon.Error);
                    this.TB_MaintDate.Text = "0";
                    this.TB_MaintPeriod.Text = "1";
                    return;
                }
                else
                {
                    DayNo = this.TB_MaintDate.Text + "일에";
                    MMQuery = " select CONVERT(varchar(7), DATEADD(MM," + DaysCount + ",'" + StartDate + "'),120)+'-" + this.TB_MaintDate.Text + "' DueDate ";


                }
                this.TB_Remark.Text = "매" + this.TB_MaintPeriod.Text + " 달주기로" + DayNo + " 보수처리 해야합니다.";
                DataTable MMQuerydt = DbAccess.Default.GetDataTable(MMQuery);
                this.TB_DueDate.Text = Convert.ToDateTime(MMQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            }
            else if (this.Combo_MaintPeriodUnit.Text.Split(':')[0].Trim() == "W")
            {
                #region
                string WeeksNo = string.Empty;
                string DwQuery = string.Empty;
                string WeeksCount = ((Convert.ToInt16(this.TB_MaintPeriod.Text) - 1) * 7).ToString();
                string StartDate = Convert.ToDateTime(this.Date_MaintStartDate.Text).ToString("yyyy-MM-dd");
                if (this.TB_MaintDate.Text == "0")
                {
                    WeeksNo = " 마다";
                    DwQuery = "select DATEADD(DD,7,'" + StartDate + "') DueDate ";
                }
                else if (this.TB_MaintDate.Text == "1")
                {
                    WeeksNo = " 일요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,13+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,12+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,11+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,10+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,9+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "2")
                {
                    WeeksNo = " 월요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,1+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,5+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,4+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,3+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,2+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "3")
                {
                    WeeksNo = " 화요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,2+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,5+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,4+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,3+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "4")
                {
                    WeeksNo = " 수요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,3+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,9+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,5+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,4+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "5")
                {
                    WeeksNo = " 목요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,4+" + WeeksCount + ",''" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,10+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,9+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,5+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "6")
                {
                    WeeksNo = " 금요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,5+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,11+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,10+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,9+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else if (this.TB_MaintDate.Text == "7")
                {
                    WeeksNo = " 토요일 마다";
                    DwQuery = "select Case When DATEPART(dw,'" + StartDate + "') = 1 Then  DATEADD(DD,6+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 2 Then  DATEADD(DD,12+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 3 Then  DATEADD(DD,11+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 4 Then  DATEADD(DD,10+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 5 Then  DATEADD(DD,9+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 6 Then  DATEADD(DD,8+" + WeeksCount + ",'" + StartDate + "') "
                            + " When DATEPART(dw,'" + StartDate + "') = 7 Then  DATEADD(DD,7+" + WeeksCount + ",'" + StartDate + "')  End DueDate ";
                }
                else
                {
                    MessageBox.Show("Error! Date Setting is fault", "Error", MessageBoxIcon.Error);
                    this.TB_MaintDate.Text = "0";
                    this.TB_MaintPeriod.Text = "1";
                    return;
                }
                this.TB_Remark.Text = "매" + this.TB_MaintPeriod.Text + "주 간격으로 " + WeeksNo + " 보수처리 해야합니다.";

                DataTable DwQuerydt = DbAccess.Default.GetDataTable(DwQuery);
                this.TB_DueDate.Text = Convert.ToDateTime(DwQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
                #endregion
            }
            else if (this.Combo_MaintPeriodUnit.Text.Split(':')[0].Trim() == "D")
            {
                this.TB_Remark.Text = "매일 보수 처리 해야합니다.";
                string DDQuery = string.Empty;
                string DaysCount = this.TB_MaintPeriod.Text;
                string StartDate = Convert.ToDateTime(this.Date_MaintStartDate.Text).ToString("yyyy-MM-dd");
                DDQuery = "select  Dateadd(DD,1,'" + StartDate + "') DueDate ";
                DataTable DDQuerydt = DbAccess.Default.GetDataTable(DDQuery);
                this.TB_DueDate.Text = Convert.ToDateTime(DDQuerydt.Rows[0]["DueDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss");
            }
            else
            {
                MessageBox.Show("Error! Date Setting is fault (Y,W,M.D is not Choice)", "Error", MessageBoxIcon.Error);
                return;
            }
            #endregion

        }
        //라디오 박스 색깔 표시
        #region
        //Jig Status
        private void RB_use_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_use.Checked == true)
            {
                this.RB_use.ForeColor = Color.Red;
                this.RB_unused.ForeColor = Color.Black;

            }
        }
        //JigStatus
        private void RB_unused_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_unused.Checked == true)
            {
                this.RB_use.ForeColor = Color.Black;
                this.RB_unused.ForeColor = Color.Red;
            }
        }
        //Current Jig Status
        private void RB_Stanby_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_Stanby.Checked == true)
            {
                this.RB_Stanby.ForeColor = Color.Red;
                this.RB_Repair.ForeColor = Color.Black;
                this.RB_Used.ForeColor = Color.Black;

            }
        }
        //Current Jig Status
        private void RB_Repair_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_Repair.Checked == true)
            {
                this.RB_Stanby.ForeColor = Color.Black;
                this.RB_Repair.ForeColor = Color.Red;
                this.RB_Used.ForeColor = Color.Black;

            }
        }
        //Current Jig Status
        private void RB_Used_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RB_Used.Checked == true)
            {
                this.RB_Stanby.ForeColor = Color.Black;
                this.RB_Repair.ForeColor = Color.Black;
                this.RB_Used.ForeColor = Color.Red;

            }
        }


        #endregion

        private void Btn_Clear_Click(object sender, EventArgs e)
        {
            this.Clear();
        }

        private void Clear()
        {
            this.TB_JigCode.Text = string.Empty;
            this.TB_JigCodeName.Text = string.Empty;
            this.TB_MaintPeriod.Text = string.Empty; //ReadOnly Ture
            this.TB_MaintPeriod.ReadOnly = true;
            this.TB_MaintDate.Text = string.Empty; //ReadOnly Ture
            this.TB_MaintDate.ReadOnly = true;
            this.TB_DueDate.Text = string.Empty; //ReadOnly Ture
            this.TB_DueDate.ReadOnly = true;
            this.TB_Remark.Text = string.Empty;//ReadOnly Ture
            this.TB_Remark.ReadOnly = true;
            this.RB_Repair.Checked = false;
            this.RB_Repair.ForeColor = Color.Black;
            this.RB_Stanby.Checked = false;
            this.RB_Stanby.ForeColor = Color.Black;
            this.RB_unused.Checked = false;
            this.RB_unused.ForeColor = Color.Black;
            this.RB_use.Checked = false;
            this.RB_use.ForeColor = Color.Black;
            this.RB_Used.Checked = false;
            this.RB_Used.ForeColor = Color.Black;
            this.TB_SpecA_Min.Text = "0";
            this.TB_SpecB_Min.Text = "0";
            this.TB_SpecC_Min.Text = "0";
            this.TB_SpecD_Min.Text = "0";
            this.TB_SpecE_Min.Text = "0";
            this.TB_SpecA_Max.Text = "0";
            this.TB_SpecB_Max.Text = "0";
            this.TB_SpecC_Max.Text = "0";
            this.TB_SpecD_Max.Text = "0";
            this.TB_SpecE_Max.Text = "0";
            this.Customer();
            this.Model();
            this.LocationGroup();
            this.MaintPeriodUnit();
            this.TB_JigCode.Focus();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TB_JigCode.Text) || string.IsNullOrEmpty(this.TB_JigCodeName.Text) || string.IsNullOrEmpty(this.TB_MaintDate.Text) ||
               string.IsNullOrEmpty(this.TB_MaintPeriod.Text) || string.IsNullOrEmpty(this.TB_DueDate.Text) || string.IsNullOrEmpty(this.Combo_Model.Text) ||
               string.IsNullOrEmpty(this.Combo_Customer.Text) || string.IsNullOrEmpty(this.Combo_Location.Text))
            {
                MessageBox.Show("Error! Data Setting is fault.", "Error", MessageBoxIcon.Error);
                return;
            }
            //if(string.IsNullOrEmpty(this.TB_LimiteMaintCount.Text))
            //{
            //    if (CB_Limit.Checked == true)
            //    {
            //        MessageBox.Show("Error! TB_LimiteMaintCount Setting is fault.", "Error", MessageBoxIcon.Error);
            //        return;
            //    }
            //    else
            //    { 

            //    }
            //}

            string SearchQuery = " Select  * From Jig where Jig = '" + this.TB_JigCode.Text.Trim() + "' ";
            DataTable SearchQuerydt = DbAccess.Default.GetDataTable(SearchQuery);
            if (SearchQuerydt.Rows.Count > 0)
            {
                MessageBox.Show("Error! Is already Jig code.", "Error", MessageBoxIcon.Error);
                return;
            }

            try
            {
                string insertQuery_jig = string.Empty;
                string insertQuery_jiginfo = string.Empty;
                if (this.m_byteData != null)
                {
                    this.TempAttachData = "@AttechData";

                }

                insertQuery_jig = "INSERT INTO [ElentecMy1Mes3].[dbo].[Jig] ";
                insertQuery_jig += " ([Jig] ,[Text]  ,[Model]  ,[Bunch]  ,[Kind] ,[Status] ,[CurrentStatus] ,[LocationBunch]  ,[Location] ,[Customer] ";
                insertQuery_jig += "  ,[MaintPeriodUnit] ,[MaintPeriord] ,[MaintDate] ,[LimitUseDate] ,[LimitMaintCount] ,[DrawingApplied],[Drawing] ";
                insertQuery_jig += " ,[Created],[Creator],[Updated],[Updater],[FirstUsed],[Discarder],[DisCardReason],[Discarded],[FileData] ";
                insertQuery_jig += " ,[SpecA_Min],[SpecA_Max],[SpecB_Min] ,[SpecB_Max] ,[SpecC_Min] ,[SpecC_Max] ,[SpecD_Min] ,[SpecD_Max] ,[SpecE_Min] ,[SpecE_Max]) ";
                insertQuery_jig += " VALUES (  ";
                insertQuery_jig += " N'" + this.TB_JigCode.Text.ToUpper().Trim() + "', ";
                insertQuery_jig += " N'" + this.TB_JigCodeName.Text + "', ";
                insertQuery_jig += " N'" + this.Combo_Model.Text + "', ";
                insertQuery_jig += " Null, ";
                insertQuery_jig += " Null, ";
                if (this.RB_use.Checked == true)
                {
                    insertQuery_jig += " 1, ";
                }
                else if (this.RB_unused.Checked == true)
                {
                    insertQuery_jig += " 0, ";
                }
                else
                {
                    MessageBox.Show("Error! Status is not Choice.", "Error", MessageBoxIcon.Error);
                    return;
                }
                if (this.RB_Stanby.Checked == true)
                {
                    insertQuery_jig += " 1, ";
                }
                else if (this.RB_Repair.Checked == true)
                {
                    insertQuery_jig += " 2, ";
                }
                else if (this.RB_Used.Checked == true)
                {
                    insertQuery_jig += " 3, ";
                }
                else
                {
                    MessageBox.Show("Error! Status is not Choice.", "Error", MessageBoxIcon.Error);
                    return;
                }
                insertQuery_jig += " N'" + this.Combo_LocationGroup.Text.Split('/')[0].ToString().Trim() + "', ";
                insertQuery_jig += " N'" + this.Combo_Location.Text + "', ";
                insertQuery_jig += " N'" + this.Combo_Customer.Text + "', ";
                insertQuery_jig += " N'" + this.Combo_MaintPeriodUnit.Text.Split(':')[0].ToString().Trim() + "', ";
                insertQuery_jig += " N'" + this.TB_MaintPeriod.Text + "', ";
                insertQuery_jig += " N'" + this.TB_MaintDate.Text + "', ";
                insertQuery_jig += " Null, ";
                insertQuery_jig += " Null, ";
                insertQuery_jig += " Getdate(), ";//DrawingApplied
                insertQuery_jig += " Null, ";//Drawing
                insertQuery_jig += " Getdate(), ";
                insertQuery_jig += " '" + WiseApp.Id + "', ";
                insertQuery_jig += " Getdate(), ";
                insertQuery_jig += " '" + WiseApp.Id + "', ";
                insertQuery_jig += " '" + Convert.ToDateTime(this.Date_FirstUsed.Text).ToString("yyyy-MM-dd HH:mm:ss") + "', ";
                insertQuery_jig += " '" + WiseApp.Id + "', ";
                insertQuery_jig += " Null, ";
                insertQuery_jig += " Null," + TempAttachData + " ,";
                insertQuery_jig += " " + this.TB_SpecA_Min.Text + " , " + this.TB_SpecA_Max.Text + ", ";
                insertQuery_jig += " " + this.TB_SpecB_Min.Text + " , " + this.TB_SpecB_Max.Text + ", ";
                insertQuery_jig += " " + this.TB_SpecC_Min.Text + " , " + this.TB_SpecC_Max.Text + ", ";
                insertQuery_jig += " " + this.TB_SpecD_Min.Text + " , " + this.TB_SpecD_Max.Text + ", ";
                insertQuery_jig += " " + this.TB_SpecE_Min.Text + " , " + this.TB_SpecE_Max.Text + " )";


                insertQuery_jiginfo = "Insert into jiginfo Values ( "
                                    + " N'" + this.TB_JigCode.Text + "', N'" + this.TB_DueDate.Text + "' , Null ,Null, Null) ";

                InsertQuery(insertQuery_jig);
                DbAccess.Default.ExecuteQuery(insertQuery_jiginfo);
                MessageBox.Show(" Success Working!! ", "OK", MessageBoxIcon.None);
                this.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex + " ", "Error", MessageBoxIcon.Error);
            }

        }
        private void InsertQuery(string Query)
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








    }
}
