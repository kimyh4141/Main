using MES_Monitoring.UserControls.Units;

namespace MES_Monitoring.UserControls.Lines
{
    partial class UserControlLineMi
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
            this.tableLayoutPanel_Line = new System.Windows.Forms.TableLayoutPanel();
            this.userControlUnit1 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit2 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit3 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit4 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit5 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.tableLayoutPanel_Line.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Line
            // 
            this.tableLayoutPanel_Line.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel_Line.ColumnCount = 11;
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit1, 1, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit2, 3, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit3, 5, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit4, 7, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit5, 9, 0);
            this.tableLayoutPanel_Line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Line.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Line.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Line.Name = "tableLayoutPanel_Line";
            this.tableLayoutPanel_Line.RowCount = 1;
            this.tableLayoutPanel_Line.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Line.Size = new System.Drawing.Size(416, 196);
            this.tableLayoutPanel_Line.TabIndex = 101;
            // 
            // userControlUnit1
            // 
            this.userControlUnit1.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit1.Location = new System.Drawing.Point(12, 0);
            this.userControlUnit1.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit1.Name = "userControlUnit1";
            this.userControlUnit1.Size = new System.Drawing.Size(62, 196);
            this.userControlUnit1.TabIndex = 0;
            this.userControlUnit1.Text = "MI";
            // 
            // userControlUnit2
            // 
            this.userControlUnit2.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit2.Location = new System.Drawing.Point(86, 0);
            this.userControlUnit2.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit2.Name = "userControlUnit2";
            this.userControlUnit2.Size = new System.Drawing.Size(58, 196);
            this.userControlUnit2.TabIndex = 1;
            this.userControlUnit2.Text = "#1 Test";
            // 
            // userControlUnit3
            // 
            this.userControlUnit3.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit3.Location = new System.Drawing.Point(156, 0);
            this.userControlUnit3.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit3.Name = "userControlUnit3";
            this.userControlUnit3.Size = new System.Drawing.Size(58, 196);
            this.userControlUnit3.TabIndex = 2;
            this.userControlUnit3.Text = "Hi-POT";
            // 
            // userControlUnit4
            // 
            this.userControlUnit4.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit4.Location = new System.Drawing.Point(226, 0);
            this.userControlUnit4.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit4.Name = "userControlUnit4";
            this.userControlUnit4.Size = new System.Drawing.Size(58, 196);
            this.userControlUnit4.TabIndex = 3;
            this.userControlUnit4.Text = "#2 Test";
            // 
            // userControlUnit5
            // 
            this.userControlUnit5.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit5.Location = new System.Drawing.Point(296, 0);
            this.userControlUnit5.Margin = new System.Windows.Forms.Padding(0);
            this.userControlUnit5.Name = "userControlUnit5";
            this.userControlUnit5.Size = new System.Drawing.Size(104, 196);
            this.userControlUnit5.TabIndex = 4;
            this.userControlUnit5.Text = "Packing";
            // 
            // UserControlLineMi
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel_Line);
            this.Name = "UserControlLineMi";
            this.Size = new System.Drawing.Size(416, 196);
            this.Load += new System.EventHandler(this.UserControlLineMi_Load);
            this.tableLayoutPanel_Line.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Line;
        private UserControlUnit userControlUnit1;
        private UserControlUnit userControlUnit2;
        private UserControlUnit userControlUnit3;
        private UserControlUnit userControlUnit4;
        private UserControlUnit userControlUnit5;
    }
}
