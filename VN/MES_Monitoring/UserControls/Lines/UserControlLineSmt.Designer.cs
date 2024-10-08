using MES_Monitoring.UserControls.Units;

namespace MES_Monitoring.UserControls.Lines
{
    partial class UserControlLineSmt
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
            this.tableLayoutPanel_Line = new System.Windows.Forms.TableLayoutPanel();
            this.userControlUnit1 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit2 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit3 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.userControlUnit4 = new MES_Monitoring.UserControls.Units.UserControlUnit();
            this.tableLayoutPanel_Line.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Line
            // 
            this.tableLayoutPanel_Line.AutoSize = true;
            this.tableLayoutPanel_Line.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel_Line.ColumnCount = 9;
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel_Line.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit1, 1, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit2, 3, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit3, 5, 0);
            this.tableLayoutPanel_Line.Controls.Add(this.userControlUnit4, 7, 0);
            this.tableLayoutPanel_Line.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Line.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Line.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Line.Name = "tableLayoutPanel_Line";
            this.tableLayoutPanel_Line.RowCount = 1;
            this.tableLayoutPanel_Line.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Line.Size = new System.Drawing.Size(350, 50);
            this.tableLayoutPanel_Line.TabIndex = 100;
            // 
            // userControlUnit1
            // 
            this.userControlUnit1.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit1.Location = new System.Drawing.Point(13, 3);
            this.userControlUnit1.Name = "userControlUnit1";
            this.userControlUnit1.Size = new System.Drawing.Size(71, 44);
            this.userControlUnit1.TabIndex = 0;
            this.userControlUnit1.Text = "S/Print";
            // 
            // userControlUnit2
            // 
            this.userControlUnit2.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit2.Location = new System.Drawing.Point(100, 3);
            this.userControlUnit2.Name = "userControlUnit2";
            this.userControlUnit2.Size = new System.Drawing.Size(85, 44);
            this.userControlUnit2.TabIndex = 1;
            this.userControlUnit2.Text = "Mounter";
            // 
            // userControlUnit3
            // 
            this.userControlUnit3.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit3.Location = new System.Drawing.Point(201, 3);
            this.userControlUnit3.Name = "userControlUnit3";
            this.userControlUnit3.Size = new System.Drawing.Size(71, 44);
            this.userControlUnit3.TabIndex = 2;
            this.userControlUnit3.Text = "Reflow";
            // 
            // userControlUnit4
            // 
            this.userControlUnit4.Condition = MES_Monitoring.Classes.Common.Routing.Condition.None;
            this.userControlUnit4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlUnit4.Location = new System.Drawing.Point(288, 3);
            this.userControlUnit4.Name = "userControlUnit4";
            this.userControlUnit4.Size = new System.Drawing.Size(46, 44);
            this.userControlUnit4.TabIndex = 3;
            this.userControlUnit4.Text = "AOI";
            // 
            // UserControlLineSmt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel_Line);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControlLineSmt";
            this.Size = new System.Drawing.Size(350, 50);
            this.Load += new System.EventHandler(this.UserControlLineSmt_Load);
            this.tableLayoutPanel_Line.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Line;
        private UserControlUnit userControlUnit1;
        private UserControlUnit userControlUnit2;
        private UserControlUnit userControlUnit3;
        private UserControlUnit userControlUnit4;
    }
}
