using System.Windows.Forms;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Lines;
using MES_Monitoring.UserControls.Units;

namespace MES_Monitoring.UserControls.Layouts
{
    partial class UserControlLayoutAiAxial : UserControl
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
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.label_Title = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Detail = new System.Windows.Forms.TableLayoutPanel();
            this.userControlLineAi8 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi9 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi5 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi6 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi2 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi1 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi4 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi3 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.userControlLineAi7 = new MES_Monitoring.UserControls.Lines.UserControlLineAi();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.tableLayoutPanel_Detail.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Body
            // 
            this.tableLayoutPanel_Body.AutoSize = true;
            this.tableLayoutPanel_Body.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Body.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel_Body.ColumnCount = 1;
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Body.Controls.Add(this.label_Title, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.tableLayoutPanel_Detail, 0, 1);
            this.tableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Body.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            this.tableLayoutPanel_Body.RowCount = 2;
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.Size = new System.Drawing.Size(400, 525);
            this.tableLayoutPanel_Body.TabIndex = 0;
            // 
            // label_Title
            // 
            this.label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Title.AutoSize = true;
            this.label_Title.BackColor = System.Drawing.Color.MidnightBlue;
            this.label_Title.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold);
            this.label_Title.ForeColor = System.Drawing.Color.White;
            this.label_Title.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label_Title.Location = new System.Drawing.Point(3, 3);
            this.label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(394, 39);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "AXIAL";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel_Detail
            // 
            this.tableLayoutPanel_Detail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_Detail.ColumnCount = 5;
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Detail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi8, 1, 8);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi9, 3, 8);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi5, 3, 0);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi6, 3, 2);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi2, 1, 2);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi1, 1, 0);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi4, 1, 6);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi3, 1, 4);
            this.tableLayoutPanel_Detail.Controls.Add(this.userControlLineAi7, 3, 4);
            this.tableLayoutPanel_Detail.Location = new System.Drawing.Point(6, 48);
            this.tableLayoutPanel_Detail.Name = "tableLayoutPanel_Detail";
            this.tableLayoutPanel_Detail.RowCount = 9;
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel_Detail.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Detail.Size = new System.Drawing.Size(388, 471);
            this.tableLayoutPanel_Detail.TabIndex = 2;
            // 
            // userControlLineAi8
            // 
            this.userControlLineAi8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi8.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi8.LineCode = 8;
            this.userControlLineAi8.Location = new System.Drawing.Point(-4, 420);
            this.userControlLineAi8.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi8.Name = "userControlLineAi8";
            this.userControlLineAi8.Size = new System.Drawing.Size(200, 51);
            this.userControlLineAi8.TabIndex = 11;
            // 
            // userControlLineAi9
            // 
            this.userControlLineAi9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi9.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi9.LineCode = 9;
            this.userControlLineAi9.Location = new System.Drawing.Point(192, 420);
            this.userControlLineAi9.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi9.Name = "userControlLineAi9";
            this.userControlLineAi9.Size = new System.Drawing.Size(200, 51);
            this.userControlLineAi9.TabIndex = 10;
            // 
            // userControlLineAi5
            // 
            this.userControlLineAi5.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlLineAi5.LineCode = 5;
            this.userControlLineAi5.Location = new System.Drawing.Point(192, 0);
            this.userControlLineAi5.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi5.Name = "userControlLineAi5";
            this.userControlLineAi5.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi5.TabIndex = 6;
            // 
            // userControlLineAi6
            // 
            this.userControlLineAi6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi6.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi6.LineCode = 6;
            this.userControlLineAi6.Location = new System.Drawing.Point(192, 105);
            this.userControlLineAi6.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi6.Name = "userControlLineAi6";
            this.userControlLineAi6.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi6.TabIndex = 5;
            // 
            // userControlLineAi2
            // 
            this.userControlLineAi2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi2.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi2.LineCode = 2;
            this.userControlLineAi2.Location = new System.Drawing.Point(-4, 105);
            this.userControlLineAi2.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi2.Name = "userControlLineAi2";
            this.userControlLineAi2.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi2.TabIndex = 2;
            // 
            // userControlLineAi1
            // 
            this.userControlLineAi1.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlLineAi1.LineCode = 1;
            this.userControlLineAi1.Location = new System.Drawing.Point(-4, 0);
            this.userControlLineAi1.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi1.Name = "userControlLineAi1";
            this.userControlLineAi1.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi1.TabIndex = 0;
            // 
            // userControlLineAi4
            // 
            this.userControlLineAi4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi4.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi4.LineCode = 4;
            this.userControlLineAi4.Location = new System.Drawing.Point(-4, 315);
            this.userControlLineAi4.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi4.Name = "userControlLineAi4";
            this.userControlLineAi4.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi4.TabIndex = 4;
            // 
            // userControlLineAi3
            // 
            this.userControlLineAi3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi3.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi3.LineCode = 3;
            this.userControlLineAi3.Location = new System.Drawing.Point(-4, 210);
            this.userControlLineAi3.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi3.Name = "userControlLineAi3";
            this.userControlLineAi3.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi3.TabIndex = 7;
            // 
            // userControlLineAi7
            // 
            this.userControlLineAi7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.userControlLineAi7.BackColor = System.Drawing.Color.Transparent;
            this.userControlLineAi7.LineCode = 7;
            this.userControlLineAi7.Location = new System.Drawing.Point(192, 210);
            this.userControlLineAi7.Margin = new System.Windows.Forms.Padding(0);
            this.userControlLineAi7.Name = "userControlLineAi7";
            this.userControlLineAi7.Size = new System.Drawing.Size(200, 53);
            this.userControlLineAi7.TabIndex = 9;
            // 
            // UserControlLayoutAiAxial
            // 
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UserControlLayoutAiAxial";
            this.Size = new System.Drawing.Size(400, 525);
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.tableLayoutPanel_Detail.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi8;

        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi9;

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Detail;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi1;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi2;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi4;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi6;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi7;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi3;
        private MES_Monitoring.UserControls.Lines.UserControlLineAi userControlLineAi5;
    }
}
