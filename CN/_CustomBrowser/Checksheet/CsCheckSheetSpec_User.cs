using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using WiseM.Data;

namespace WiseM.Browser.Checksheet
{
    public partial class CsCheckSheetSpec_User : UserControl
    {
        public delegate void CsType04BtnClick(string CsCode ,string Type , string Seq);
        public event CsType04BtnClick ct04btnclick;

        public delegate void CS06SEQ(string CsCode,string Seq);
        public event CS06SEQ cs06seq;

        public string Seq { get { return this.lbl_Seq.Text; } set { this.lbl_Seq.Text = value; } }         //항목 

        public string CsCode { get; set; }
        public string CsShift { get; set; }

        public string CheckGroup { get { return this.lbl_CheckGroup.Text; } set { this.lbl_CheckGroup.Text = value; } }         //체크항목
        public string Checkitems { get { return this.lbl_CheckItem.Text; } set { this.lbl_CheckItem.Text = value; } }         //체크항목

        //public string CheckPeriod { get { return this.lbl_CheckPeriod.Text; } set { this.lbl_CheckPeriod.Text = value; } }   //점검주기

        public string Min { get { return this.lbl_Minimum.Text; } set { this.lbl_Minimum.Text = value; } }           //하한치
        public string Max { get { return this.lbl_Maximum.Text; } set { this.lbl_Maximum.Text = value; } }           //상한치
        public string DataUnit { get { return this.lbl_Unit.Text; } set { this.lbl_Unit.Text = value; } }           //상한치

        public string Values 
        { 
            get 
            {
                //return type == 0 ? this.btn_Value.Text : this.numericUpDown1.Value.ToString(); 
                return (type == 0 || type == 4) ? this.btn_Value.Text : this.tb_Value.Text; 
            } 
            set 
            {
                if (type == 0)
                {
                    this.btn_Value.Text = value;
                    if (string.IsNullOrEmpty(this.btn_Value.Text)) //기본값이 "" 인 빈값이면 양호로 바로 변경
                    {
                        this.btn_Value.Text = "";
                    }
                }
                else if (type == 1)
                {
                    if (Values.Equals("계획정지"))
                    {
                        this.tb_Value.Text = "계획정지";
                    }
                    else
                    {
                        this.tb_Value.Text = value.Equals("") ? "" : value;

                    }
                }
                else if (type == 2)
                {
                    if (Values.Equals("계획정지"))
                    {
                        this.tb_Value.Text = "계획정지";
                    }
                    else
                    {
                        this.tb_Value.Text = value.Equals("") ? "" : value;

                    }
                }
                else if (type == 3)
                {
                    if (Values.Equals("계획정지"))
                    {
                        this.tb_Value.Text = "계획정지";
                    }
                    else
                    {
                        this.tb_Value.Text = value.Equals("") ? "" : value;

                    }
                    //this.tb_Value.Text = value.Equals("") ? "" : value;
                }
                else if (type == 4)
                {
                    this.btn_Value.Text = value;
                    if (string.IsNullOrEmpty(this.btn_Value.Text)) //기본값이 "" 인 빈값이면 양호로 바로 변경
                    {
                        this.btn_Value.Text = "";
                    }
                }
            } 
        }  

        public string Remark { get { return this.tb_ReMark.Text; } set { this.tb_ReMark.Text = value; } }  //비고

        public int type { get; set; }

        //0: default, 1: 양호, 2:이상 3: 조치완료. 4: 해당없음
        private int btntype { get; set; }

        public int buttonInt = 0;

        public CsCheckSheetSpec_User()
        {
            InitializeComponent();

            if (btn_Value.Text.Equals(""))
            {
                buttonInt = 0;
            }
            else if (btn_Value.Text.Equals("양호"))
            {
                buttonInt = 1;
            }
            else if (btn_Value.Text.Equals("이상"))
            {
                buttonInt = 2;
            }
            else if (btn_Value.Text.Equals("조치완료"))
            {
                buttonInt = 3;
            }
            else if (btn_Value.Text.Equals("해당없음"))
            {
                buttonInt = 4;
            }
        }


        private void CsDailySpec_User_Load(object sender, EventArgs e)
        {
            if (type == 0)
            {
                //button 양호/이상/수리
                btn_Value.Visible = true;
                tb_Value.Visible = false;

                if (btn_Value.Text.Equals(""))
                {
                    buttonInt = 0;
                    btn_Value.BackColor = default(Color);
                    btntype = 0;
                }
                else if (btn_Value.Text.Equals("양호"))
                {
                    buttonInt = 1;
                    btn_Value.BackColor = Color.Green;
                    btntype = 1;
                    
                }
                else if (btn_Value.Text.Equals("이상"))
                {
                    buttonInt = 2;
                    btn_Value.BackColor = Color.Red;
                    btntype = 2;

                }
                else if (btn_Value.Text.Equals("조치완료"))
                {
                    buttonInt = 3;
                    btn_Value.BackColor = Color.GreenYellow;
                    btntype = 3;

                }
                else if (btn_Value.Text.Equals("해당없음"))
                {
                    buttonInt = 4;
                    btn_Value.BackColor = default(Color);
                    btntype = 4;
                }

            }
            else if (type == 1)        //int
            {
                //측정치(int)
                btn_Value.Visible = false;
                tb_Value.Visible =  true;

                if (Values.Equals("계획정지"))
                {

                }
                else if (string.IsNullOrEmpty(Min) || string.IsNullOrEmpty(Max))
                {
                    //둘 중 하나가 없음
                    //tb_Value.BackColor = Color.Red;
                }
                else if (string.IsNullOrEmpty(Values))
                {
                    //값 입력이 없음 노랑
                    //tb_Value.BackColor = Color.Yellow;
                }
                else if (Convert.ToDecimal(Min) <= Convert.ToDecimal(Values) && Convert.ToDecimal(Max) >= Convert.ToDecimal(Values))
                {
                    //상,하한값 안에 있음 하늘
                    //tb_Value.BackColor = Color.SkyBlue;
                }
                else
                {
                    //그 값이 틀림
                    tb_Value.BackColor = Color.Red;
                }

            }
            else if (type == 2)    //Decimal
            {
                //측정치(Decimal(10,2))
                btn_Value.Visible = false;
                tb_Value.Visible = true;

                if (Values.Equals("계획정지"))
                {

                }
                else if (string.IsNullOrEmpty(Min) || string.IsNullOrEmpty(Max))
                {
                    //둘 중 하나가 없음
                    //tb_Value.BackColor = Color.Red;
                }
                else if (string.IsNullOrEmpty(Values))
                {
                    //값 입력이 없음 노랑
                    //tb_Value.BackColor = Color.Yellow;
                }
                else if (Convert.ToDecimal(Min) <= Convert.ToDecimal(Values) && Convert.ToDecimal(Max) >= Convert.ToDecimal(Values))   
                {
                    //상,하한값 안에 있음 하늘
                    //tb_Value.BackColor = Color.SkyBlue;
                }
                else
                {
                    //그 값이 틀림
                    tb_Value.BackColor = Color.Red;
                }


            }
            else if (type == 3)    //문자열
            {
                btn_Value.Visible = false;
                tb_Value.Visible = true;
            }
            else if (type == 4)    //버튼 NU/TD/Theta
            {
                //button NU / TD / Theta
                btn_Value.Visible = true;
                tb_Value.Visible = false;

                if (btn_Value.Text.Equals(""))
                {
                    buttonInt = 0;
                    btn_Value.BackColor = default(Color);
                    btntype = 0;
                }
                if (btn_Value.Text.Equals("NU"))
                {
                    buttonInt = 1;
                    btntype = 1;

                }
                else if (btn_Value.Text.Equals("TD"))
                {
                    buttonInt = 2;
                    btntype = 2;

                }
                else if (btn_Value.Text.Equals("Theta"))
                {
                    buttonInt = 3;
                    btntype = 3;

                }
            }
        }

        private void btn_Value_Click(object sender, EventArgs e)
        {
            if (type == 0 || type == 1 || type == 2)
            {
                ////0: default, 1: 양호, 2:이상 3: 조치완료. 4: 해당없음
                if (btntype == 0 || btntype == 1 || btntype == 4) 
                {
                    buttonInt++;
                    if (buttonInt % 5 == 0)
                    {
                        btn_Value.Text = "";
                        btn_Value.BackColor = default(Color);
                    }
                    else if (buttonInt % 5 == 1)
                    {
                        //btn_Value.Text = "양호";
                        btn_Value.Text = "Good";
                        btn_Value.BackColor = Color.Green;
                    }
                    else if (buttonInt % 5 == 2)
                    {
                        //btn_Value.Text = "이상";
                        btn_Value.Text = "Bad";
                        btn_Value.BackColor = Color.Red;
                    }
                    else if (buttonInt % 5 == 3)
                    {
                        //btn_Value.Text = "조치완료";
                        btn_Value.Text = "Completed";
                        btn_Value.BackColor = Color.GreenYellow;
                    }
                    else if (buttonInt % 5 == 4)
                    {
                        //btn_Value.Text = "해당없음";
                        btn_Value.Text = "None";
                        btn_Value.BackColor = default(Color);
                    }
                }
                else if (btntype == 2)
                {
                    buttonInt++;
                    if (buttonInt % 2 == 0)
                    {
                        //btn_Value.Text = "이상";
                        btn_Value.Text = "Bad";
                        btn_Value.BackColor = Color.Red;
                    }
                    else
                    {
                        //btn_Value.Text = "조치완료";
                        btn_Value.Text = "Completed";
                        btn_Value.BackColor = Color.GreenYellow;
                    }

                }
                else if (btntype == 3)
                {

                    //btn_Value.Text = "조치완료";
                    btn_Value.Text = "Completed";
                    btn_Value.BackColor = Color.GreenYellow;

                }
            }
            else if(type ==4)
            {
                buttonInt++;
                if (buttonInt % 4 == 0)
                {
                    btn_Value.Text = "";
                    ct04btnclick(this.CsCode, "", this.Seq);
                    //btn_Value.BackColor = default(Color);
                }
                else if (buttonInt % 4 == 1)
                {
                    btn_Value.Text = "NU";

                    if (ct04btnclick !=null)
                    {
                        //public delegate void CsType04BtnClick(string CsCode ,string Type , string Seq,decimal Min,decimal Max);
                        ct04btnclick(this.CsCode, "NU", this.Seq);
                    }

                }
                else if (buttonInt % 4 == 2)
                {
                    btn_Value.Text = "TD";

                    if (ct04btnclick != null)
                    {
                        //public delegate void CsType04BtnClick(string CsCode ,string Type , string Seq,decimal Min,decimal Max);
                        ct04btnclick(this.CsCode, "TD", this.Seq);
                    }
                }
                else if (buttonInt % 4 == 3)
                {
                    btn_Value.Text = "Theta";

                    if (ct04btnclick != null)
                    {
                        //public delegate void CsType04BtnClick(string CsCode ,string Type , string Seq,decimal Min,decimal Max);
                        ct04btnclick(this.CsCode, "Theta", this.Seq);
                    }
                }
            }
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //투입자재량     영어,한글 안되게
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != Convert.ToChar(Keys.Back) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            Color c = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, (sender as Control).ClientRectangle, c, 1, ButtonBorderStyle.Solid,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid);
        }
        private void panel_Paint_NonLeftTop(object sender, PaintEventArgs e)
        {
            Color c = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, (sender as Control).ClientRectangle, 
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid);
        }

        private void panel_Paint_NonLeftRightTop(object sender, PaintEventArgs e)
        {
            Color c = Color.Black;
            ControlPaint.DrawBorder(e.Graphics, (sender as Control).ClientRectangle,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 0, ButtonBorderStyle.Solid,
                                        c, 1, ButtonBorderStyle.Solid);
        }


        private void panel_Resize(object sender, EventArgs e)
        {
            (sender as Control).Invalidate();
        }

        private void tb_Value_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (type == 1)  //int
            {
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != Convert.ToChar(Keys.Back) && e.KeyChar != '-')
                {
                    e.Handled = true;
                }
            }
            else if (type == 2)       //Decimal(10,2)
            {
                if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != Convert.ToChar(Keys.Back) && e.KeyChar != '.' && e.KeyChar != '-')
                {
                    e.Handled = true;
                }
            }

        }

        //클릭시 키패드
        private void tb_Value_Click(object sender, EventArgs e)
        {
            if (CsCode.Equals("CS06") && (Seq.Equals("3") || Seq.Equals("5")))
            {
                cs06seq(this.CsCode, this.Seq);
            }
            else if (this.type == 3)
            {

            }
            else
            {
                Virtual_Keyboard.NumKeyPad vk = new Virtual_Keyboard.NumKeyPad();
                vk._Ok += new Virtual_Keyboard.NumKeyPad.OnOk(vk__Ok);
                vk.oldqty = tb_Value.Text;
                vk.StartPosition = FormStartPosition.Manual;
                //vk.Location = new Point(200, 130);
                vk.StartPosition = FormStartPosition.CenterScreen;
                vk.DotPoint = 6;
                vk.BottomMinusButton = true;

                if (this.type == 1)
                {
                    vk.DotUse = false;         //정수 정수 
                }
                else if (this.type == 2)
                {
                    vk.DotUse = true;         //소수 정수 
                }
                DialogResult dr = vk.ShowDialog();


                var result = this.IsNumeric(tb_Value.Text);
            }
        }

        private bool IsNumeric(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return false;
            }
            else
            {
                Regex numericPattern = new Regex(@"\W");

                return numericPattern.IsMatch(strValue);
            }
        }

        void vk__Ok(string Text)
        {

            this.tb_Value.Text = Text;
        }

        //레이져마킹에서 4,8번만    2018-04-05-11-22-33(19자리)  ->2018년04월05일11시22분33초 로 변경
        private void tb_Value_Leave(object sender, EventArgs e)
        {
            //select * from CsSpec where csname like '%레이져마킹%' and CsCode='cs16'
            string laserquery = "select * from CsSpec where csname like '%레이져마킹%' and CsCode='" + CsCode + "'";
            DataTable laserDT = DbAccess.Default.GetDataTable(laserquery);
            if (laserDT.Rows.Count > 0)
            {
                if (Seq.ToString().Equals("4") || Seq.ToString().Equals("8"))
                {
                    if (Values.Length == 19)
                    {
                        Values = Values.Substring(0, 4) + "년" +
                                 Values.Substring(5, 2) + "월" +
                                 Values.Substring(8, 2) + "일" +
                                 Values.Substring(11, 2) + "시" +
                                 Values.Substring(14, 2) + "분" +
                                 Values.Substring(17, 2) + "초";
                    }
                }
            }
        }
    }
}
