using System.Windows.Forms;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Lines;

namespace MES_Monitoring.UserControls.Layouts
{
    partial class UserControlLayoutAiRadial : UserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlLayoutAiRadial));
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Detail = new System.Windows.Forms.TableLayoutPanel();
            this.userControlLineAi2 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi1 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi4 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi3 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi8 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi9 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
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
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi2, 1, 2);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi1, 1, 0);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi4, 1, 6);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi3, 1, 4);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi8, 1, 8);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi9, 1, 10);
            this.tableLayoutPanel_Detail.Name = "tableLayoutPanel_Detail";
            // 
            // userControlLineAi2
            // 
            resources.ApplyResources(this.userControlLineAi2, "userControlLineAi2");
            this.userControlLineAi2.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi2.LineCode = 2;
            this.userControlLineAi2.Name = "userControlLineAi2";
            // 
            // userControlLineAi1
            // 
            resources.ApplyResources(this.userControlLineAi1, "userControlLineAi1");
            this.userControlLineAi1.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi1.LineCode = 1;
            this.userControlLineAi1.Name = "userControlLineAi1";
            // 
            // userControlLineAi4
            // 
            resources.ApplyResources(this.userControlLineAi4, "userControlLineAi4");
            this.userControlLineAi4.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi4.LineCode = 4;
            this.userControlLineAi4.Name = "userControlLineAi4";
            // 
            // userControlLineAi3
            // 
            resources.ApplyResources(this.userControlLineAi3, "userControlLineAi3");
            this.userControlLineAi3.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi3.LineCode = 3;
            this.userControlLineAi3.Name = "userControlLineAi3";
            // 
            // userControlLineAi8
            // 
            resources.ApplyResources(this.userControlLineAi8, "userControlLineAi8");
            this.userControlLineAi8.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi8.LineCode = 8;
            this.userControlLineAi8.Name = "userControlLineAi8";
            // 
            // userControlLineAi9
            // 
            resources.ApplyResources(this.userControlLineAi9, "userControlLineAi9");
            this.userControlLineAi9.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi9.LineCode = 9;
            this.userControlLineAi9.Name = "userControlLineAi9";
            // 
            // UserControlLayoutAiRadial
            // 
            this.Controls.Add(this.tableLayoutPanel_Body);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlLayoutAiRadial";
            this.Load += new System.EventHandler(this.UserControlLayoutAiRadial_Load);
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
        private UserControlLineAi userControlLineAi1;
        private UserControlLineAi userControlLineAi2;
        private UserControlLineAi userControlLineAi4;
        private UserControlLineAi userControlLineAi3;
        private UserControlLineAi userControlLineAi8;
        private UserControlLineAi userControlLineAi9;
    }
}
