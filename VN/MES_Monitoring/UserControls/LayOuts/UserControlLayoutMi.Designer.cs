using System.Windows.Forms;
using MES_Monitoring.UserControls.Units;
using MES_Monitoring.UserControls.Lines;

namespace MES_Monitoring.UserControls.Layouts
{
    partial class UserControlLayoutMi : UserControlLayout
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
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlLayoutMi));
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Detail = new System.Windows.Forms.TableLayoutPanel();
            this.userControlLineMi1 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.userControlLineMi2 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.userControlLineMi3 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.userControlLineMi4 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.userControlLineMi8 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.userControlLineMi9 = new MES_Monitoring.UserControls.Lines.UserControlLineMi();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tableLayoutPanel_Detail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Body
            // 
            resources.ApplyResources(this.tableLayoutPanel_Body, "tableLayoutPanel_Body");
            this.tableLayoutPanel_Body.Controls.Add(this.label_Title, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.tableLayoutPanel_Detail, 0, 1);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            // 
            // label_Title
            // 
            resources.ApplyResources(this.label_Title, "label_Title");
            this.label_Title.BackColor = System.Drawing.Color.MidnightBlue;
            this.label_Title.ForeColor = System.Drawing.Color.White;
            this.label_Title.Name = "label_Title";
            // 
            // tableLayoutPanel_Detail
            // 
            resources.ApplyResources(this.tableLayoutPanel_Detail, "tableLayoutPanel_Detail");
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi1, 0, 1);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi2, 0, 3);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi3, 0, 5);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi4, 0, 7);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi8, 0, 9);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineMi9, 0, 11);
            this.tableLayoutPanel_Detail.Name = "tableLayoutPanel_Detail";
            // 
            // userControlLineMi1
            // 
            this.userControlLineMi1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi1, "userControlLineMi1");
            this.userControlLineMi1.LineCode = 1;
            this.userControlLineMi1.Name = "userControlLineMi1";
            // 
            // userControlLineMi2
            // 
            this.userControlLineMi2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi2, "userControlLineMi2");
            this.userControlLineMi2.LineCode = 2;
            this.userControlLineMi2.Name = "userControlLineMi2";
            // 
            // userControlLineMi3
            // 
            this.userControlLineMi3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi3, "userControlLineMi3");
            this.userControlLineMi3.LineCode = 3;
            this.userControlLineMi3.Name = "userControlLineMi3";
            // 
            // userControlLineMi4
            // 
            this.userControlLineMi4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi4, "userControlLineMi4");
            this.userControlLineMi4.LineCode = 4;
            this.userControlLineMi4.Name = "userControlLineMi4";
            // 
            // userControlLineMi8
            // 
            this.userControlLineMi8.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi8, "userControlLineMi8");
            this.userControlLineMi8.LineCode = 8;
            this.userControlLineMi8.Name = "userControlLineMi8";
            // 
            // userControlLineMi9
            // 
            this.userControlLineMi9.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.userControlLineMi9, "userControlLineMi9");
            this.userControlLineMi9.LineCode = 9;
            this.userControlLineMi9.Name = "userControlLineMi9";
            // 
            // UserControlLayoutMi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tableLayoutPanel_Body);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlLayoutMi";
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tableLayoutPanel_Detail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Detail;
        private UserControlLineMi userControlLineMi1;
        private UserControlLineMi userControlLineMi2;
        private UserControlLineMi userControlLineMi3;
        private UserControlLineMi userControlLineMi4;
        private UserControlLineMi userControlLineMi8;
        private UserControlLineMi userControlLineMi9;
    }
}
