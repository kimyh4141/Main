namespace MES_Monitoring.UserControls.Pages
{
    partial class Page_MI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView_Right = new System.Windows.Forms.DataGridView();
            this.dataGridViewColumn_Right_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Right_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Right_PlannedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Right_ProdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Right_ProdRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Right_BadRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_Left = new System.Windows.Forms.DataGridView();
            this.dataGridViewColumn_Left_Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Left_Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Left_PlannedQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Left_ProdQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Left_ProdRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewColumn_Left_BadRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._setLayoutMi = new MES_Monitoring.UserControls.Layouts.UserControlLayoutMi(this.components);
            this.tableLayoutPanel_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Right)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Left)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel_Main
            // 
            this.tableLayoutPanel_Main.ColumnCount = 2;
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Main.Controls.Add(this.dataGridView_Right, 1, 1);
            this.tableLayoutPanel_Main.Controls.Add(this.dataGridView_Left, 0, 1);
            this.tableLayoutPanel_Main.Controls.Add(this._setLayoutMi, 0, 0);
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
            // dataGridView_Right
            // 
            this.dataGridView_Right.AllowUserToAddRows = false;
            this.dataGridView_Right.AllowUserToResizeColumns = false;
            this.dataGridView_Right.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_Right.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_Right.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_Right.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Right.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Right.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_Right.ColumnHeadersHeight = 50;
            this.dataGridView_Right.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewColumn_Right_Line,
            this.dataGridViewColumn_Right_Model,
            this.dataGridViewColumn_Right_PlannedQty,
            this.dataGridViewColumn_Right_ProdQty,
            this.dataGridViewColumn_Right_ProdRate,
            this.dataGridViewColumn_Right_BadRate});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Right.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Right.EnableHeadersVisualStyles = false;
            this.dataGridView_Right.Location = new System.Drawing.Point(589, 306);
            this.dataGridView_Right.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_Right.Name = "dataGridView_Right";
            this.dataGridView_Right.ReadOnly = true;
            this.dataGridView_Right.RowHeadersVisible = false;
            this.dataGridView_Right.RowTemplate.Height = 40;
            this.dataGridView_Right.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Right.Size = new System.Drawing.Size(590, 306);
            this.dataGridView_Right.TabIndex = 1;
            this.dataGridView_Right.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_Layout);
            // 
            // dataGridViewColumn_Right_Line
            // 
            this.dataGridViewColumn_Right_Line.HeaderText = "Line";
            this.dataGridViewColumn_Right_Line.Name = "dataGridViewColumn_Right_Line";
            this.dataGridViewColumn_Right_Line.ReadOnly = true;
            // 
            // dataGridViewColumn_Right_Model
            // 
            this.dataGridViewColumn_Right_Model.HeaderText = "Model";
            this.dataGridViewColumn_Right_Model.Name = "dataGridViewColumn_Right_Model";
            this.dataGridViewColumn_Right_Model.ReadOnly = true;
            // 
            // dataGridViewColumn_Right_PlannedQty
            // 
            this.dataGridViewColumn_Right_PlannedQty.HeaderText = "계획수량";
            this.dataGridViewColumn_Right_PlannedQty.Name = "dataGridViewColumn_Right_PlannedQty";
            this.dataGridViewColumn_Right_PlannedQty.ReadOnly = true;
            // 
            // dataGridViewColumn_Right_ProdQty
            // 
            this.dataGridViewColumn_Right_ProdQty.HeaderText = "생산수량";
            this.dataGridViewColumn_Right_ProdQty.Name = "dataGridViewColumn_Right_ProdQty";
            this.dataGridViewColumn_Right_ProdQty.ReadOnly = true;
            // 
            // dataGridViewColumn_Right_ProdRate
            // 
            this.dataGridViewColumn_Right_ProdRate.HeaderText = "진척률";
            this.dataGridViewColumn_Right_ProdRate.Name = "dataGridViewColumn_Right_ProdRate";
            this.dataGridViewColumn_Right_ProdRate.ReadOnly = true;
            // 
            // dataGridViewColumn_Right_BadRate
            // 
            this.dataGridViewColumn_Right_BadRate.HeaderText = "불량률";
            this.dataGridViewColumn_Right_BadRate.Name = "dataGridViewColumn_Right_BadRate";
            this.dataGridViewColumn_Right_BadRate.ReadOnly = true;
            // 
            // dataGridView_Left
            // 
            this.dataGridView_Left.AllowUserToAddRows = false;
            this.dataGridView_Left.AllowUserToResizeColumns = false;
            this.dataGridView_Left.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView_Left.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView_Left.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_Left.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Left.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Left.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView_Left.ColumnHeadersHeight = 50;
            this.dataGridView_Left.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewColumn_Left_Line,
            this.dataGridViewColumn_Left_Model,
            this.dataGridViewColumn_Left_PlannedQty,
            this.dataGridViewColumn_Left_ProdQty,
            this.dataGridViewColumn_Left_ProdRate,
            this.dataGridViewColumn_Left_BadRate});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView_Left.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Left.EnableHeadersVisualStyles = false;
            this.dataGridView_Left.Location = new System.Drawing.Point(0, 306);
            this.dataGridView_Left.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView_Left.Name = "dataGridView_Left";
            this.dataGridView_Left.ReadOnly = true;
            this.dataGridView_Left.RowHeadersVisible = false;
            this.dataGridView_Left.RowTemplate.Height = 40;
            this.dataGridView_Left.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView_Left.Size = new System.Drawing.Size(589, 306);
            this.dataGridView_Left.TabIndex = 0;
            this.dataGridView_Left.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_Layout);
            // 
            // dataGridViewColumn_Left_Line
            // 
            this.dataGridViewColumn_Left_Line.HeaderText = "Line";
            this.dataGridViewColumn_Left_Line.Name = "dataGridViewColumn_Left_Line";
            this.dataGridViewColumn_Left_Line.ReadOnly = true;
            // 
            // dataGridViewColumn_Left_Model
            // 
            this.dataGridViewColumn_Left_Model.HeaderText = "Model";
            this.dataGridViewColumn_Left_Model.Name = "dataGridViewColumn_Left_Model";
            this.dataGridViewColumn_Left_Model.ReadOnly = true;
            // 
            // dataGridViewColumn_Left_PlannedQty
            // 
            this.dataGridViewColumn_Left_PlannedQty.HeaderText = "계획수량";
            this.dataGridViewColumn_Left_PlannedQty.Name = "dataGridViewColumn_Left_PlannedQty";
            this.dataGridViewColumn_Left_PlannedQty.ReadOnly = true;
            // 
            // dataGridViewColumn_Left_ProdQty
            // 
            this.dataGridViewColumn_Left_ProdQty.HeaderText = "생산수량";
            this.dataGridViewColumn_Left_ProdQty.Name = "dataGridViewColumn_Left_ProdQty";
            this.dataGridViewColumn_Left_ProdQty.ReadOnly = true;
            // 
            // dataGridViewColumn_Left_ProdRate
            // 
            this.dataGridViewColumn_Left_ProdRate.HeaderText = "진척률";
            this.dataGridViewColumn_Left_ProdRate.Name = "dataGridViewColumn_Left_ProdRate";
            this.dataGridViewColumn_Left_ProdRate.ReadOnly = true;
            // 
            // dataGridViewColumn_Left_BadRate
            // 
            this.dataGridViewColumn_Left_BadRate.HeaderText = "불량률";
            this.dataGridViewColumn_Left_BadRate.Name = "dataGridViewColumn_Left_BadRate";
            this.dataGridViewColumn_Left_BadRate.ReadOnly = true;
            // 
            // _setLayoutMi
            // 
            this._setLayoutMi.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel_Main.SetColumnSpan(this._setLayoutMi, 2);
            this._setLayoutMi.Dock = System.Windows.Forms.DockStyle.Fill;
            this._setLayoutMi.Location = new System.Drawing.Point(0, 0);
            this._setLayoutMi.Margin = new System.Windows.Forms.Padding(0);
            this._setLayoutMi.Name = "_setLayoutMi";
            this._setLayoutMi.Size = new System.Drawing.Size(1179, 306);
            this._setLayoutMi.TabIndex = 2;
            // 
            // Page_MI
            // 
            this.Controls.Add(this.tableLayoutPanel_Main);
            this.Name = "Page_MI";
            this.Size = new System.Drawing.Size(1179, 612);
            this.tableLayoutPanel_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Right)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Left)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Main;
        private System.Windows.Forms.DataGridView dataGridView_Right;
        private System.Windows.Forms.DataGridView dataGridView_Left;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_PlannedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_ProdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_ProdRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Left_BadRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_PlannedQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_ProdQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_ProdRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewColumn_Right_BadRate;
        private Layouts.UserControlLayoutMi _setLayoutMi;
    }
}
