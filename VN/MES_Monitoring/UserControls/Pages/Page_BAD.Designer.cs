namespace MES_Monitoring.UserControls.Pages
{
    partial class Page_BAD
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_MI = new System.Windows.Forms.GroupBox();
            this.dataGridView_MI = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_SMT = new System.Windows.Forms.GroupBox();
            this.dataGridView_SMT = new System.Windows.Forms.DataGridView();
            this.dataGridViewColumn_SMT_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_SMT_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_SMT_ProdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_SMT_BadQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_SMT_BadRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_AI = new System.Windows.Forms.GroupBox();
            this.dataGridView_AI = new System.Windows.Forms.DataGridView();
            this.dataGridViewColumn_AI_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_AI_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_AI_ProdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_AI_BadQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_AI_BadRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel_Main.SuspendLayout();
            this.groupBox_MI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MI)).BeginInit();
            this.groupBox_SMT.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SMT)).BeginInit();
            this.groupBox_AI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AI)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Controls.Add(this.groupBox_MI, 1, 1);
            this.tableLayoutPanel_Main.Controls.Add(this.groupBox_SMT, 1, 0);
            this.tableLayoutPanel_Main.Controls.Add(this.groupBox_AI, 0, 0);
            this.tableLayoutPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Main.Name = "tableLayoutPanel_Main";
            this.tableLayoutPanel_Main.RowCount = 2;
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Main.Size = new System.Drawing.Size(1179, 612);
            this.tableLayoutPanel_Main.TabIndex = 2;
            // 
            // groupBox_MI
            // 
            this.groupBox_MI.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_MI.Controls.Add(this.dataGridView_MI);
            this.groupBox_MI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_MI.ForeColor = System.Drawing.Color.White;
            this.groupBox_MI.Location = new System.Drawing.Point(592, 309);
            this.groupBox_MI.Name = "groupBox_MI";
            this.groupBox_MI.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_MI.Size = new System.Drawing.Size(584, 300);
            this.groupBox_MI.TabIndex = 5;
            this.groupBox_MI.TabStop = false;
            this.groupBox_MI.Text = "MI";
            // 
            // dataGridView_MI
            // 
            this.dataGridView_MI.AllowUserToAddRows = false;
            this.dataGridView_MI.AllowUserToResizeColumns = false;
            this.dataGridView_MI.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_MI.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_MI.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_MI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_MI.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_MI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_MI.ColumnHeadersHeight = 50;
            this.dataGridView_MI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_MI.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_MI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_MI.EnableHeadersVisualStyles = false;
            this.dataGridView_MI.Location = new System.Drawing.Point(0, 26);
            this.dataGridView_MI.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_MI.Name = "dataGridView_MI";
            this.dataGridView_MI.ReadOnly = true;
            this.dataGridView_MI.RowHeadersVisible = false;
            this.dataGridView_MI.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView_MI.RowTemplate.Height = 40;
            this.dataGridView_MI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_MI.Size = new System.Drawing.Size(584, 274);
            this.dataGridView_MI.TabIndex = 2;
            this.dataGridView_MI.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_Layout);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Line";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Model";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "생산수량";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "불량수량";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "불량률";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // groupBox_SMT
            // 
            this.groupBox_SMT.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_SMT.Controls.Add(this.dataGridView_SMT);
            this.groupBox_SMT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_SMT.ForeColor = System.Drawing.Color.White;
            this.groupBox_SMT.Location = new System.Drawing.Point(592, 3);
            this.groupBox_SMT.Name = "groupBox_SMT";
            this.groupBox_SMT.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_SMT.Size = new System.Drawing.Size(584, 300);
            this.groupBox_SMT.TabIndex = 4;
            this.groupBox_SMT.TabStop = false;
            this.groupBox_SMT.Text = "SMT";
            // 
            // dataGridView_SMT
            // 
            this.dataGridView_SMT.AllowUserToAddRows = false;
            this.dataGridView_SMT.AllowUserToResizeColumns = false;
            this.dataGridView_SMT.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_SMT.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_SMT.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_SMT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_SMT.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_SMT.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_SMT.ColumnHeadersHeight = 50;
            this.dataGridView_SMT.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewColumn_SMT_Line,
            this.dataGridViewColumn_SMT_Model,
            this.dataGridViewColumn_SMT_ProdQty,
            this.dataGridViewColumn_SMT_BadQty,
            this.dataGridViewColumn_SMT_BadRate});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_SMT.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_SMT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_SMT.EnableHeadersVisualStyles = false;
            this.dataGridView_SMT.Location = new System.Drawing.Point(0, 26);
            this.dataGridView_SMT.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_SMT.Name = "dataGridView_SMT";
            this.dataGridView_SMT.ReadOnly = true;
            this.dataGridView_SMT.RowHeadersVisible = false;
            this.dataGridView_SMT.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView_SMT.RowTemplate.Height = 40;
            this.dataGridView_SMT.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_SMT.Size = new System.Drawing.Size(584, 274);
            this.dataGridView_SMT.TabIndex = 1;
            this.dataGridView_SMT.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_Layout);
            // 
            // dataGridViewColumn_SMT_Line
            // 
            this.dataGridViewColumn_SMT_Line.HeaderText = "Line";
            this.dataGridViewColumn_SMT_Line.Name = "dataGridViewColumn_SMT_Line";
            this.dataGridViewColumn_SMT_Line.ReadOnly = true;
            // 
            // dataGridViewColumn_SMT_Model
            // 
            this.dataGridViewColumn_SMT_Model.HeaderText = "Model";
            this.dataGridViewColumn_SMT_Model.Name = "dataGridViewColumn_SMT_Model";
            this.dataGridViewColumn_SMT_Model.ReadOnly = true;
            // 
            // dataGridViewColumn_SMT_ProdQty
            // 
            this.dataGridViewColumn_SMT_ProdQty.HeaderText = "생산수량";
            this.dataGridViewColumn_SMT_ProdQty.Name = "dataGridViewColumn_SMT_ProdQty";
            this.dataGridViewColumn_SMT_ProdQty.ReadOnly = true;
            // 
            // dataGridViewColumn_SMT_BadQty
            // 
            this.dataGridViewColumn_SMT_BadQty.HeaderText = "불량수량";
            this.dataGridViewColumn_SMT_BadQty.Name = "dataGridViewColumn_SMT_BadQty";
            this.dataGridViewColumn_SMT_BadQty.ReadOnly = true;
            // 
            // dataGridViewColumn_SMT_BadRate
            // 
            this.dataGridViewColumn_SMT_BadRate.HeaderText = "불량률";
            this.dataGridViewColumn_SMT_BadRate.Name = "dataGridViewColumn_SMT_BadRate";
            this.dataGridViewColumn_SMT_BadRate.ReadOnly = true;
            // 
            // groupBox_AI
            // 
            this.groupBox_AI.BackColor = System.Drawing.Color.Transparent;
            this.groupBox_AI.Controls.Add(this.dataGridView_AI);
            this.groupBox_AI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_AI.ForeColor = System.Drawing.Color.White;
            this.groupBox_AI.Location = new System.Drawing.Point(3, 3);
            this.groupBox_AI.Name = "groupBox_AI";
            this.groupBox_AI.Padding = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel_Main.SetRowSpan(this.groupBox_AI, 2);
            this.groupBox_AI.Size = new System.Drawing.Size(583, 606);
            this.groupBox_AI.TabIndex = 3;
            this.groupBox_AI.TabStop = false;
            this.groupBox_AI.Text = "AI";
            // 
            // dataGridView_AI
            // 
            this.dataGridView_AI.AllowUserToAddRows = false;
            this.dataGridView_AI.AllowUserToResizeColumns = false;
            this.dataGridView_AI.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_AI.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView_AI.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_AI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_AI.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_AI.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridView_AI.ColumnHeadersHeight = 50;
            this.dataGridView_AI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewColumn_AI_Line,
            this.dataGridViewColumn_AI_Model,
            this.dataGridViewColumn_AI_ProdQty,
            this.dataGridViewColumn_AI_BadQty,
            this.dataGridViewColumn_AI_BadRate});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_AI.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView_AI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_AI.EnableHeadersVisualStyles = false;
            this.dataGridView_AI.Location = new System.Drawing.Point(0, 26);
            this.dataGridView_AI.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_AI.Name = "dataGridView_AI";
            this.dataGridView_AI.ReadOnly = true;
            this.dataGridView_AI.RowHeadersVisible = false;
            this.dataGridView_AI.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridView_AI.RowTemplate.Height = 40;
            this.dataGridView_AI.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_AI.Size = new System.Drawing.Size(583, 580);
            this.dataGridView_AI.TabIndex = 0;
            this.dataGridView_AI.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_Layout);
            // 
            // dataGridViewColumn_AI_Line
            // 
            this.dataGridViewColumn_AI_Line.HeaderText = "Line";
            this.dataGridViewColumn_AI_Line.Name = "dataGridViewColumn_AI_Line";
            this.dataGridViewColumn_AI_Line.ReadOnly = true;
            // 
            // dataGridViewColumn_AI_Model
            // 
            this.dataGridViewColumn_AI_Model.HeaderText = "Model";
            this.dataGridViewColumn_AI_Model.Name = "dataGridViewColumn_AI_Model";
            this.dataGridViewColumn_AI_Model.ReadOnly = true;
            // 
            // dataGridViewColumn_AI_ProdQty
            // 
            this.dataGridViewColumn_AI_ProdQty.HeaderText = "생산수량";
            this.dataGridViewColumn_AI_ProdQty.Name = "dataGridViewColumn_AI_ProdQty";
            this.dataGridViewColumn_AI_ProdQty.ReadOnly = true;
            // 
            // dataGridViewColumn_AI_BadQty
            // 
            this.dataGridViewColumn_AI_BadQty.HeaderText = "불량수량";
            this.dataGridViewColumn_AI_BadQty.Name = "dataGridViewColumn_AI_BadQty";
            this.dataGridViewColumn_AI_BadQty.ReadOnly = true;
            // 
            // dataGridViewColumn_AI_BadRate
            // 
            this.dataGridViewColumn_AI_BadRate.HeaderText = "불량률";
            this.dataGridViewColumn_AI_BadRate.Name = "dataGridViewColumn_AI_BadRate";
            this.dataGridViewColumn_AI_BadRate.ReadOnly = true;
            // 
            // Page_BAD
            // 
            this.BackColor = System.Drawing.Color.MidnightBlue;
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Name = "Page_BAD";
            this.Size = new System.Drawing.Size(1179, 612);
            this.tableLayoutPanel_Main.ResumeLayout(false);
            this.groupBox_MI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_MI)).EndInit();
            this.groupBox_SMT.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_SMT)).EndInit();
            this.groupBox_AI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.DataGridView dataGridView_SMT;
        private System.Windows.Forms.DataGridView dataGridView_AI;
        private System.Windows.Forms.DataGridView dataGridView_MI;
        private System.Windows.Forms.GroupBox groupBox_AI;
        private System.Windows.Forms.GroupBox groupBox_SMT;
        private System.Windows.Forms.GroupBox groupBox_MI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_SMT_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_SMT_Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_SMT_ProdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_SMT_BadQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_SMT_BadRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_AI_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_AI_Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_AI_ProdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_AI_BadQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_AI_BadRate;
    }
}
