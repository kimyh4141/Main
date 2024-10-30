namespace WiseM.Browser
{
    partial class JigSpecItem
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_Spec = new System.Windows.Forms.Label();
            this.tb_MinValue = new System.Windows.Forms.TextBox();
            this.tb_MaxValue = new System.Windows.Forms.TextBox();
            this.btn_Del = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_Spec, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_MinValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tb_MaxValue, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(30, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 21);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_Spec
            // 
            this.lbl_Spec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Spec.Location = new System.Drawing.Point(0, 0);
            this.lbl_Spec.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Spec.Name = "lbl_Spec";
            this.lbl_Spec.Size = new System.Drawing.Size(40, 21);
            this.lbl_Spec.TabIndex = 0;
            this.lbl_Spec.Text = "1";
            this.lbl_Spec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_MinValue
            // 
            this.tb_MinValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_MinValue.Location = new System.Drawing.Point(41, 1);
            this.tb_MinValue.Margin = new System.Windows.Forms.Padding(1);
            this.tb_MinValue.Name = "tb_MinValue";
            this.tb_MinValue.Size = new System.Drawing.Size(162, 21);
            this.tb_MinValue.TabIndex = 1;
            this.tb_MinValue.Text = "0";
            this.tb_MinValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_MaxValue
            // 
            this.tb_MaxValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb_MaxValue.Location = new System.Drawing.Point(205, 1);
            this.tb_MaxValue.Margin = new System.Windows.Forms.Padding(1);
            this.tb_MaxValue.Name = "tb_MaxValue";
            this.tb_MaxValue.Size = new System.Drawing.Size(162, 21);
            this.tb_MaxValue.TabIndex = 1;
            this.tb_MaxValue.Text = "0";
            this.tb_MaxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_Del
            // 
            this.btn_Del.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Del.Location = new System.Drawing.Point(0, 0);
            this.btn_Del.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(30, 21);
            this.btn_Del.TabIndex = 1;
            this.btn_Del.Text = "-";
            this.btn_Del.UseVisualStyleBackColor = true;
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // JigSpecItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btn_Del);
            this.Name = "JigSpecItem";
            this.Size = new System.Drawing.Size(398, 21);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_Spec;
        private System.Windows.Forms.TextBox tb_MinValue;
        private System.Windows.Forms.TextBox tb_MaxValue;
        private System.Windows.Forms.Button btn_Del;
    }
}
