using System.Windows.Forms;
using MES_Monitoring.UserControls.Units;

namespace MES_Monitoring.UserControls.Lines
{
    partial class UserControlLineAi
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userControlUnit = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.SuspendLayout();
            // 
            // userControlUnit
            // 
            this.userControlUnit.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit.Location = new System.Drawing.Point(0, 0);
            this.userControlUnit.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit.Name = "userControlUnit";
            this.userControlUnit.Size = new System.Drawing.Size(100, 100);
            this.userControlUnit.TabIndex = 0;
            this.userControlUnit.Text = "Unit Text";
            // 
            // UserControlLineAi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.userControlUnit);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControlLineAi";
            this.Size = new System.Drawing.Size(100, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlUnit userControlUnit;
    }
}
