using System.Windows.Forms;
using MES_Monitoring.UserControls.Lines;

namespace MES_Monitoring.UserControls.Layouts
{
    partial class UserControlLayoutSmt : UserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControlLayoutSmt));
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Detail = new System.Windows.Forms.TableLayoutPanel();
            this.userControlLineSmt1 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt2 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt3 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt4 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt5 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt8 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
            this.userControlLineSmt9 = new MES_Monitoring.UserControls.Lines.UserControlLineSmt();
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
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt1, 0, 1);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt2, 0, 3);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt3, 0, 5);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt4, 0, 7);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt5, 0, 9);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt8, 0, 11);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineSmt9, 0, 13);
            this.tableLayoutPanel_Detail.Name = "tableLayoutPanel_Detail";
            // 
            // userControlLineSmt1
            // 
            resources.ApplyResources(this.userControlLineSmt1, "userControlLineSmt1");
            this.userControlLineSmt1.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt1.LineCode = 1;
            this.userControlLineSmt1.Name = "userControlLineSmt1";
            // 
            // userControlLineSmt2
            // 
            resources.ApplyResources(this.userControlLineSmt2, "userControlLineSmt2");
            this.userControlLineSmt2.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt2.LineCode = 2;
            this.userControlLineSmt2.Name = "userControlLineSmt2";
            // 
            // userControlLineSmt3
            // 
            resources.ApplyResources(this.userControlLineSmt3, "userControlLineSmt3");
            this.userControlLineSmt3.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt3.LineCode = 3;
            this.userControlLineSmt3.Name = "userControlLineSmt3";
            // 
            // userControlLineSmt4
            // 
            resources.ApplyResources(this.userControlLineSmt4, "userControlLineSmt4");
            this.userControlLineSmt4.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt4.LineCode = 4;
            this.userControlLineSmt4.Name = "userControlLineSmt4";
            // 
            // userControlLineSmt5
            // 
            resources.ApplyResources(this.userControlLineSmt5, "userControlLineSmt5");
            this.userControlLineSmt5.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt5.LineCode = 5;
            this.userControlLineSmt5.Name = "userControlLineSmt5";
            // 
            // userControlLineSmt8
            // 
            resources.ApplyResources(this.userControlLineSmt8, "userControlLineSmt8");
            this.userControlLineSmt8.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt8.LineCode = 8;
            this.userControlLineSmt8.Name = "userControlLineSmt8";
            // 
            // userControlLineSmt9
            // 
            resources.ApplyResources(this.userControlLineSmt9, "userControlLineSmt9");
            this.userControlLineSmt9.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineSmt9.LineCode = 9;
            this.userControlLineSmt9.Name = "userControlLineSmt9";
            // 
            // UserControlLayoutSmt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tableLayoutPanel_Body);
            resources.ApplyResources(this, "$this");
            this.Name = "UserControlLayoutSmt";
            this.Load += new System.EventHandler(this.UserControlLayoutSmt_Load);
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tableLayoutPanel_Detail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Detail;
        private UserControlLineSmt userControlLineSmt1;
        private UserControlLineSmt userControlLineSmt2;
        private UserControlLineSmt userControlLineSmt3;
        private UserControlLineSmt userControlLineSmt4;
        private UserControlLineSmt userControlLineSmt5;
        private UserControlLineSmt userControlLineSmt8;
        private UserControlLineSmt userControlLineSmt9;
    }
}
