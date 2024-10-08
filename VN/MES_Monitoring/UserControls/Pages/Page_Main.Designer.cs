using MES_Monitoring.UserControls.Layouts;

namespace MES_Monitoring.UserControls.Pages
{
    partial class Page_Main
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this._setLayoutAiAxial = new MES_Monitoring.UserControls.Layouts.UserControlLayoutAiAxial(this.components);
            this._setLayoutAiRadial = new MES_Monitoring.UserControls.Layouts.UserControlLayoutAiRadial(this.components);
            this._setLayoutSmt = new MES_Monitoring.UserControls.Layouts.UserControlLayoutSmt(this.components);
            this._setLayoutMi = new MES_Monitoring.UserControls.Layouts.UserControlLayoutMi(this.components);
            this.tableLayoutPanel_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Controls.Add(this._setLayoutAiAxial, 0, 0);
            this.tableLayoutPanel_Main.Controls.Add(this._setLayoutAiRadial, 1, 0);
            this.tableLayoutPanel_Main.Controls.Add(this._setLayoutSmt, 0, 1);
            this.tableLayoutPanel_Main.Controls.Add(this._setLayoutMi, 1, 1);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 2;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1179, 612);
            this.tableLayoutPanel_Main.TabIndex = 1;
            // 
            // _setLayoutAiAxial
            // 
            this._setLayoutAiAxial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._setLayoutAiAxial.BackColor = System.Drawing.Color.Transparent;
            this._setLayoutAiAxial.Location = new System.Drawing.Point(0, 0);
            this._setLayoutAiAxial.Margin = new System.Windows.Forms.Padding(0);
            this._setLayoutAiAxial.Name = "_setLayoutAiAxial";
            this._setLayoutAiAxial.Size = new System.Drawing.Size(589, 306);
            this._setLayoutAiAxial.TabIndex = 0;
            // 
            // _setLayoutAiRadial
            // 
            this._setLayoutAiRadial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._setLayoutAiRadial.BackColor = System.Drawing.Color.Transparent;
            this._setLayoutAiRadial.Location = new System.Drawing.Point(589, 0);
            this._setLayoutAiRadial.Margin = new System.Windows.Forms.Padding(0);
            this._setLayoutAiRadial.Name = "_setLayoutAiRadial";
            this._setLayoutAiRadial.Size = new System.Drawing.Size(590, 306);
            this._setLayoutAiRadial.TabIndex = 1;
            // 
            // _setLayoutSmt
            // 
            this._setLayoutSmt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._setLayoutSmt.BackColor = System.Drawing.Color.Transparent;
            this._setLayoutSmt.Location = new System.Drawing.Point(0, 306);
            this._setLayoutSmt.Margin = new System.Windows.Forms.Padding(0);
            this._setLayoutSmt.Name = "_setLayoutSmt";
            this._setLayoutSmt.Size = new System.Drawing.Size(589, 306);
            this._setLayoutSmt.TabIndex = 2;
            // 
            // _setLayoutMi
            // 
            this._setLayoutMi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._setLayoutMi.BackColor = System.Drawing.Color.Transparent;
            this._setLayoutMi.Location = new System.Drawing.Point(589, 306);
            this._setLayoutMi.Margin = new System.Windows.Forms.Padding(0);
            this._setLayoutMi.Name = "_setLayoutMi";
            this._setLayoutMi.Size = new System.Drawing.Size(590, 306);
            this._setLayoutMi.TabIndex = 3;
            // 
            // Page_Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Name = "Page_Main";
            this.Size = new System.Drawing.Size(1179, 612);
            this.Load += new System.EventHandler(this.Page_Main_Load);
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControlLayoutAiAxial _setLayoutAiAxial;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private UserControlLayoutAiRadial _setLayoutAiRadial;
        private UserControlLayoutSmt _setLayoutSmt;
        private UserControlLayoutMi _setLayoutMi;
    }
}
