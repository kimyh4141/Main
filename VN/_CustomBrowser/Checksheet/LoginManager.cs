using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.Checksheet
{
    public partial class LoginManager : Form
    {
        private string CsDate;
        private string CsShift;
        private string Line;

        private string FormName;

        public LoginManager()
        {
            InitializeComponent();
        }

        public LoginManager(string CsDate, string CsShift, string Line, string FormName)
        {
            InitializeComponent();

            this.CsDate = CsDate;
            this.CsShift = CsShift;
            this.Line = Line;
            this.FormName = FormName;
        }

        public LoginManager(string CsDate, string Line, string FormName)
        {
            InitializeComponent();

            this.CsDate = CsDate;
            this.Line = Line;
            this.FormName = FormName;
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            DataTable dt = DbAccess.Default.GetDataTable("select TOP 1 * from users where UserId='" + tb_managerid.Text + "'");

            if (dt.Rows.Count > 0)
            {
                if (FormName.Equals(CsCheckSheetSpec.ThisText))
                {
                    //일일장비점검과는 다르게  체크시트 프로그램의 [관리자확인]에서 변경
                    CsCheckSheetSpec.ManagerCheck = tb_managerid.Text; //static변수 관리자이름

                    this.DialogResult = DialogResult.OK;
                    Close();
                }

                System.Windows.Forms.MessageBox.Show("관리자 확인 완료");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("등록되지않은 매니저 ID입니다. 브라우저에서 추가해주세요");
                return;
            }
        }
    }
}
