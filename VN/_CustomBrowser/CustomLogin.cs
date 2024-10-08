using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class CustomLogin : Form
    {
        public CustomLogin()
        {
            InitializeComponent();
        }

        public CustomLogin(WiseM.AppService.LoginController controller) : this()
        {
            // Form 에 Design 된 각각의 Control 을 Controller 에 설정
            controller.ControlItemForm = this;
            controller.ControlItemID = textBox1;
            controller.ControlItemPassword = textBox2;
            controller.ControlItemRemember = checkBox1;
            controller.ControlItemLanguage = comboBox1;
            controller.ControlItemNewID = linkLabel1;
            controller.ControlItemEnter = button1;
            controller.ControlItemCancel = button2;
            controller.ControlItemConnection = button3;

            // Controller 준비완료
            controller.Ready();
        }

        private void CustomLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // other process
            
            if (this.DialogResult != DialogResult.Cancel && this.DialogResult != DialogResult.Retry)
            {
                // Cancel 이나 Retry 가 아닌 경우 로그인이 정상상태 임.
                // 이곳에서 추가 프로세스 구성하면 됨
            }
        }
    }
}
