namespace WiseM.Browser.WMS
{
    partial class ReturnBarcodeHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_WorkOrder = new System.Windows.Forms.DataGridView();
            this.dataGridView_RawMaterial = new System.Windows.Forms.DataGridView();
            this.button_Search = new System.Windows.Forms.Button();
            this.textBox_Workorder = new System.Windows.Forms.TextBox();
            this.button_Serarch_Clear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView_ScanList = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Barcode = new System.Windows.Forms.TextBox();
            this.comboBox_To = new System.Windows.Forms.ComboBox();
            this.button_RawMaterial_Search = new System.Windows.Forms.Button();
            this.label_Workorder = new System.Windows.Forms.Label();
            this.comboBox_From = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Scan_Order = new System.Windows.Forms.TextBox();
            this.label_Barcode = new System.Windows.Forms.Label();
            this.comboBox_location = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox_location_group = new System.Windows.Forms.ComboBox();
            this.textBox_Scan_Barcode = new System.Windows.Forms.TextBox();
            this.label_Supply = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Scan_DATE = new System.Windows.Forms.TextBox();
            this.textBox_RM_Text = new System.Windows.Forms.TextBox();
            this.textBox_Scan_SEQ = new System.Windows.Forms.TextBox();
            this.textBox_Scan_QTY = new System.Windows.Forms.TextBox();
            this.label_Date = new System.Windows.Forms.Label();
            this.label_RM = new System.Windows.Forms.Label();
            this.textBox_RM_Spec = new System.Windows.Forms.TextBox();
            this.label_SEQ = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Supply_Text = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Scan_Supply = new System.Windows.Forms.TextBox();
            this.label_QTY = new System.Windows.Forms.Label();
            this.textBox_Scan_Material = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkOrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RawMaterial)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScanList)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_WorkOrder
            // 
            this.dataGridView_WorkOrder.AllowUserToAddRows = false;
            this.dataGridView_WorkOrder.AllowUserToDeleteRows = false;
            this.dataGridView_WorkOrder.AllowUserToResizeColumns = false;
            this.dataGridView_WorkOrder.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView_WorkOrder.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_WorkOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_WorkOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView_WorkOrder, 2);
            this.dataGridView_WorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_WorkOrder.Location = new System.Drawing.Point(3, 46);
            this.dataGridView_WorkOrder.MultiSelect = false;
            this.dataGridView_WorkOrder.Name = "dataGridView_WorkOrder";
            this.dataGridView_WorkOrder.ReadOnly = true;
            this.dataGridView_WorkOrder.RowHeadersVisible = false;
            this.dataGridView_WorkOrder.RowTemplate.Height = 23;
            this.dataGridView_WorkOrder.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_WorkOrder.Size = new System.Drawing.Size(859, 173);
            this.dataGridView_WorkOrder.TabIndex = 0;
            this.dataGridView_WorkOrder.DataSourceChanged += new System.EventHandler(this.dataGridView_WorkOrder_DataSourceChanged);
            this.dataGridView_WorkOrder.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_WorkOrder_CellClick);
            this.dataGridView_WorkOrder.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView_WorkOrder_KeyUp);
            this.dataGridView_WorkOrder.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_WorkOrder_Layout);
            // 
            // dataGridView_RawMaterial
            // 
            this.dataGridView_RawMaterial.AllowUserToAddRows = false;
            this.dataGridView_RawMaterial.AllowUserToDeleteRows = false;
            this.dataGridView_RawMaterial.AllowUserToResizeColumns = false;
            this.dataGridView_RawMaterial.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView_RawMaterial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_RawMaterial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_RawMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView_RawMaterial, 2);
            this.dataGridView_RawMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_RawMaterial.Location = new System.Drawing.Point(3, 225);
            this.dataGridView_RawMaterial.MultiSelect = false;
            this.dataGridView_RawMaterial.Name = "dataGridView_RawMaterial";
            this.dataGridView_RawMaterial.ReadOnly = true;
            this.dataGridView_RawMaterial.RowHeadersVisible = false;
            this.dataGridView_RawMaterial.RowTemplate.Height = 23;
            this.dataGridView_RawMaterial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_RawMaterial.Size = new System.Drawing.Size(859, 173);
            this.dataGridView_RawMaterial.TabIndex = 1;
            this.dataGridView_RawMaterial.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_RawMaterial_CellClick);
            this.dataGridView_RawMaterial.SelectionChanged += new System.EventHandler(this.dataGridView_RawMaterial_SelectionChanged);
            this.dataGridView_RawMaterial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_RawMaterial_KeyDown);
            this.dataGridView_RawMaterial.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridView_RawMaterial_KeyUp);
            this.dataGridView_RawMaterial.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_RawMaterial_Layout);
            // 
            // button_Search
            // 
            this.button_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Search.AutoSize = true;
            this.button_Search.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Search.Location = new System.Drawing.Point(3, 3);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(86, 31);
            this.button_Search.TabIndex = 5;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // textBox_Workorder
            // 
            this.textBox_Workorder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Workorder.Location = new System.Drawing.Point(368, 3);
            this.textBox_Workorder.Name = "textBox_Workorder";
            this.textBox_Workorder.Size = new System.Drawing.Size(210, 21);
            this.textBox_Workorder.TabIndex = 4;
            this.textBox_Workorder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_WorkOrder_KeyDown);
            // 
            // button_Serarch_Clear
            // 
            this.button_Serarch_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Serarch_Clear.AutoSize = true;
            this.button_Serarch_Clear.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Serarch_Clear.Location = new System.Drawing.Point(95, 3);
            this.button_Serarch_Clear.Name = "button_Serarch_Clear";
            this.button_Serarch_Clear.Size = new System.Drawing.Size(77, 31);
            this.button_Serarch_Clear.TabIndex = 6;
            this.button_Serarch_Clear.Text = "Clear";
            this.button_Serarch_Clear.UseVisualStyleBackColor = true;
            this.button_Serarch_Clear.Click += new System.EventHandler(this.button_Search_Clear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(871, 601);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RmOrder Scan";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_RawMaterial, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_WorkOrder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView_ScanList, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(865, 581);
            this.tableLayoutPanel1.TabIndex = 37;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.button_Search);
            this.flowLayoutPanel2.Controls.Add(this.button_Serarch_Clear);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(687, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(175, 37);
            this.flowLayoutPanel2.TabIndex = 38;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.dateTimePicker_Start);
            this.flowLayoutPanel1.Controls.Add(this.label9);
            this.flowLayoutPanel1.Controls.Add(this.dateTimePicker_End);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.textBox_Workorder);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(678, 27);
            this.flowLayoutPanel1.TabIndex = 38;
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_Start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_Start.Location = new System.Drawing.Point(3, 3);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(130, 21);
            this.dateTimePicker_Start.TabIndex = 34;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "~";
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_End.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker_End.Location = new System.Drawing.Point(160, 3);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(126, 21);
            this.dateTimePicker_End.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(292, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "Workorder";
            // 
            // dataGridView_ScanList
            // 
            this.dataGridView_ScanList.AllowUserToAddRows = false;
            this.dataGridView_ScanList.AllowUserToDeleteRows = false;
            this.dataGridView_ScanList.AllowUserToResizeColumns = false;
            this.dataGridView_ScanList.AllowUserToResizeRows = false;
            this.dataGridView_ScanList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView_ScanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dataGridView_ScanList, 2);
            this.dataGridView_ScanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ScanList.Location = new System.Drawing.Point(3, 404);
            this.dataGridView_ScanList.Name = "dataGridView_ScanList";
            this.dataGridView_ScanList.ReadOnly = true;
            this.dataGridView_ScanList.RowHeadersVisible = false;
            this.dataGridView_ScanList.RowTemplate.Height = 23;
            this.dataGridView_ScanList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_ScanList.Size = new System.Drawing.Size(859, 174);
            this.dataGridView_ScanList.TabIndex = 39;
            this.dataGridView_ScanList.Layout += new System.Windows.Forms.LayoutEventHandler(this.dataGridView_ScanList_Layout);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1264, 601);
            this.splitContainer1.SplitterDistance = 871;
            this.splitContainer1.TabIndex = 37;
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(389, 601);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Barcode Scan";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.textBox_Barcode, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_To, 1, 28);
            this.tableLayoutPanel2.Controls.Add(this.button_RawMaterial_Search, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label_Workorder, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_From, 1, 26);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 26);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_Order, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label_Barcode, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_location, 1, 24);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 22);
            this.tableLayoutPanel2.Controls.Add(this.comboBox_location_group, 1, 22);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_Barcode, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label_Supply, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_DATE, 1, 20);
            this.tableLayoutPanel2.Controls.Add(this.textBox_RM_Text, 1, 14);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_SEQ, 1, 18);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_QTY, 1, 16);
            this.tableLayoutPanel2.Controls.Add(this.label_Date, 0, 20);
            this.tableLayoutPanel2.Controls.Add(this.label_RM, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.textBox_RM_Spec, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.label_SEQ, 0, 18);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 14);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Supply_Text, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_Supply, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label_QTY, 0, 16);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Scan_Material, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 28);
            this.tableLayoutPanel2.Controls.Add(this.button_Clear, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_Save, 3, 30);
            this.tableLayoutPanel2.Controls.Add(this.button_Add, 1, 30);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 31;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.666668F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(383, 581);
            this.tableLayoutPanel2.TabIndex = 40;
            // 
            // textBox_Barcode
            // 
            this.textBox_Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Barcode, 2);
            this.textBox_Barcode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Barcode.Location = new System.Drawing.Point(3, 8);
            this.textBox_Barcode.Name = "textBox_Barcode";
            this.textBox_Barcode.Size = new System.Drawing.Size(184, 21);
            this.textBox_Barcode.TabIndex = 2;
            this.textBox_Barcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Barcode_KeyDown);
            // 
            // comboBox_To
            // 
            this.comboBox_To.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.comboBox_To, 3);
            this.comboBox_To.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_To.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_To.FormattingEnabled = true;
            this.comboBox_To.Location = new System.Drawing.Point(98, 500);
            this.comboBox_To.Name = "comboBox_To";
            this.comboBox_To.Size = new System.Drawing.Size(282, 20);
            this.comboBox_To.TabIndex = 39;
            // 
            // button_RawMaterial_Search
            // 
            this.button_RawMaterial_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_RawMaterial_Search.AutoSize = true;
            this.button_RawMaterial_Search.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_RawMaterial_Search.Location = new System.Drawing.Point(196, 3);
            this.button_RawMaterial_Search.Name = "button_RawMaterial_Search";
            this.button_RawMaterial_Search.Size = new System.Drawing.Size(86, 31);
            this.button_RawMaterial_Search.TabIndex = 14;
            this.button_RawMaterial_Search.Text = "Scan";
            this.button_RawMaterial_Search.UseVisualStyleBackColor = true;
            this.button_RawMaterial_Search.Click += new System.EventHandler(this.button_RawMaterial_Search_Click);
            // 
            // label_Workorder
            // 
            this.label_Workorder.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Workorder.AutoSize = true;
            this.label_Workorder.Location = new System.Drawing.Point(20, 52);
            this.label_Workorder.Name = "label_Workorder";
            this.label_Workorder.Size = new System.Drawing.Size(72, 12);
            this.label_Workorder.TabIndex = 18;
            this.label_Workorder.Text = "WorkOrder";
            // 
            // comboBox_From
            // 
            this.comboBox_From.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.comboBox_From, 3);
            this.comboBox_From.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_From.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_From.FormattingEnabled = true;
            this.comboBox_From.Location = new System.Drawing.Point(98, 466);
            this.comboBox_From.Name = "comboBox_From";
            this.comboBox_From.Size = new System.Drawing.Size(282, 20);
            this.comboBox_From.TabIndex = 36;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 470);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 12);
            this.label7.TabIndex = 37;
            this.label7.Text = "From";
            // 
            // textBox_Scan_Order
            // 
            this.textBox_Scan_Order.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_Order, 3);
            this.textBox_Scan_Order.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_Order.Location = new System.Drawing.Point(98, 48);
            this.textBox_Scan_Order.Name = "textBox_Scan_Order";
            this.textBox_Scan_Order.ReadOnly = true;
            this.textBox_Scan_Order.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_Order.TabIndex = 20;
            // 
            // label_Barcode
            // 
            this.label_Barcode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Barcode.AutoSize = true;
            this.label_Barcode.Location = new System.Drawing.Point(33, 87);
            this.label_Barcode.Name = "label_Barcode";
            this.label_Barcode.Size = new System.Drawing.Size(59, 12);
            this.label_Barcode.TabIndex = 10;
            this.label_Barcode.Text = "Barcode";
            // 
            // comboBox_location
            // 
            this.comboBox_location.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.comboBox_location, 3);
            this.comboBox_location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_location.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_location.FormattingEnabled = true;
            this.comboBox_location.Location = new System.Drawing.Point(98, 432);
            this.comboBox_location.Name = "comboBox_location";
            this.comboBox_location.Size = new System.Drawing.Size(282, 20);
            this.comboBox_location.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(26, 413);
            this.label6.Name = "label6";
            this.tableLayoutPanel2.SetRowSpan(this.label6, 3);
            this.label6.Size = new System.Drawing.Size(66, 24);
            this.label6.TabIndex = 35;
            this.label6.Text = " Location\r\n   Group";
            // 
            // comboBox_location_group
            // 
            this.comboBox_location_group.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.comboBox_location_group, 3);
            this.comboBox_location_group.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_location_group.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_location_group.FormattingEnabled = true;
            this.comboBox_location_group.Location = new System.Drawing.Point(98, 398);
            this.comboBox_location_group.Name = "comboBox_location_group";
            this.comboBox_location_group.Size = new System.Drawing.Size(282, 20);
            this.comboBox_location_group.TabIndex = 33;
            this.comboBox_location_group.SelectedIndexChanged += new System.EventHandler(this.comboBox_location_group_SelectedIndexChanged);
            // 
            // textBox_Scan_Barcode
            // 
            this.textBox_Scan_Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_Barcode, 3);
            this.textBox_Scan_Barcode.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_Barcode.Location = new System.Drawing.Point(98, 83);
            this.textBox_Scan_Barcode.Name = "textBox_Scan_Barcode";
            this.textBox_Scan_Barcode.ReadOnly = true;
            this.textBox_Scan_Barcode.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_Barcode.TabIndex = 21;
            // 
            // label_Supply
            // 
            this.label_Supply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Supply.AutoSize = true;
            this.label_Supply.Location = new System.Drawing.Point(42, 122);
            this.label_Supply.Name = "label_Supply";
            this.label_Supply.Size = new System.Drawing.Size(50, 12);
            this.label_Supply.TabIndex = 12;
            this.label_Supply.Text = "Supply";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "Text";
            // 
            // textBox_Scan_DATE
            // 
            this.textBox_Scan_DATE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_DATE, 3);
            this.textBox_Scan_DATE.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_DATE.Location = new System.Drawing.Point(98, 363);
            this.textBox_Scan_DATE.Name = "textBox_Scan_DATE";
            this.textBox_Scan_DATE.ReadOnly = true;
            this.textBox_Scan_DATE.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_DATE.TabIndex = 26;
            // 
            // textBox_RM_Text
            // 
            this.textBox_RM_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_RM_Text, 3);
            this.textBox_RM_Text.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_RM_Text.Location = new System.Drawing.Point(98, 258);
            this.textBox_RM_Text.Name = "textBox_RM_Text";
            this.textBox_RM_Text.ReadOnly = true;
            this.textBox_RM_Text.Size = new System.Drawing.Size(282, 21);
            this.textBox_RM_Text.TabIndex = 32;
            // 
            // textBox_Scan_SEQ
            // 
            this.textBox_Scan_SEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_SEQ, 3);
            this.textBox_Scan_SEQ.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_SEQ.Location = new System.Drawing.Point(98, 328);
            this.textBox_Scan_SEQ.Name = "textBox_Scan_SEQ";
            this.textBox_Scan_SEQ.ReadOnly = true;
            this.textBox_Scan_SEQ.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_SEQ.TabIndex = 24;
            // 
            // textBox_Scan_QTY
            // 
            this.textBox_Scan_QTY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_QTY, 3);
            this.textBox_Scan_QTY.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_QTY.Location = new System.Drawing.Point(98, 293);
            this.textBox_Scan_QTY.Name = "textBox_Scan_QTY";
            this.textBox_Scan_QTY.ReadOnly = true;
            this.textBox_Scan_QTY.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_QTY.TabIndex = 25;
            // 
            // label_Date
            // 
            this.label_Date.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Date.AutoSize = true;
            this.label_Date.Location = new System.Drawing.Point(58, 367);
            this.label_Date.Name = "label_Date";
            this.label_Date.Size = new System.Drawing.Size(34, 12);
            this.label_Date.TabIndex = 11;
            this.label_Date.Text = "Date";
            // 
            // label_RM
            // 
            this.label_RM.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_RM.AutoSize = true;
            this.label_RM.Location = new System.Drawing.Point(10, 192);
            this.label_RM.Name = "label_RM";
            this.label_RM.Size = new System.Drawing.Size(82, 12);
            this.label_RM.TabIndex = 8;
            this.label_RM.Text = "RawMareial";
            // 
            // textBox_RM_Spec
            // 
            this.textBox_RM_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_RM_Spec, 3);
            this.textBox_RM_Spec.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_RM_Spec.Location = new System.Drawing.Point(98, 223);
            this.textBox_RM_Spec.Name = "textBox_RM_Spec";
            this.textBox_RM_Spec.ReadOnly = true;
            this.textBox_RM_Spec.Size = new System.Drawing.Size(282, 21);
            this.textBox_RM_Spec.TabIndex = 30;
            // 
            // label_SEQ
            // 
            this.label_SEQ.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_SEQ.AutoSize = true;
            this.label_SEQ.Location = new System.Drawing.Point(59, 332);
            this.label_SEQ.Name = "label_SEQ";
            this.label_SEQ.Size = new System.Drawing.Size(33, 12);
            this.label_SEQ.TabIndex = 19;
            this.label_SEQ.Text = "SEQ";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 12);
            this.label3.TabIndex = 31;
            this.label3.Text = "RawText";
            // 
            // textBox_Supply_Text
            // 
            this.textBox_Supply_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Supply_Text, 3);
            this.textBox_Supply_Text.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Supply_Text.Location = new System.Drawing.Point(98, 153);
            this.textBox_Supply_Text.Name = "textBox_Supply_Text";
            this.textBox_Supply_Text.ReadOnly = true;
            this.textBox_Supply_Text.Size = new System.Drawing.Size(282, 21);
            this.textBox_Supply_Text.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 12);
            this.label2.TabIndex = 29;
            this.label2.Text = "RawSpec";
            // 
            // textBox_Scan_Supply
            // 
            this.textBox_Scan_Supply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_Supply, 3);
            this.textBox_Scan_Supply.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_Supply.Location = new System.Drawing.Point(98, 118);
            this.textBox_Scan_Supply.Name = "textBox_Scan_Supply";
            this.textBox_Scan_Supply.ReadOnly = true;
            this.textBox_Scan_Supply.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_Supply.TabIndex = 22;
            // 
            // label_QTY
            // 
            this.label_QTY.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_QTY.AutoSize = true;
            this.label_QTY.Location = new System.Drawing.Point(59, 297);
            this.label_QTY.Name = "label_QTY";
            this.label_QTY.Size = new System.Drawing.Size(33, 12);
            this.label_QTY.TabIndex = 7;
            this.label_QTY.Text = "QTY";
            // 
            // textBox_Scan_Material
            // 
            this.textBox_Scan_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Scan_Material, 3);
            this.textBox_Scan_Material.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Scan_Material.Location = new System.Drawing.Point(98, 188);
            this.textBox_Scan_Material.Name = "textBox_Scan_Material";
            this.textBox_Scan_Material.ReadOnly = true;
            this.textBox_Scan_Material.Size = new System.Drawing.Size(282, 21);
            this.textBox_Scan_Material.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 504);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 12);
            this.label8.TabIndex = 38;
            this.label8.Text = "To";
            // 
            // button_Clear
            // 
            this.button_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Clear.AutoSize = true;
            this.button_Clear.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Clear.Location = new System.Drawing.Point(297, 3);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(83, 31);
            this.button_Clear.TabIndex = 15;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.AutoSize = true;
            this.button_Save.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Save.Location = new System.Drawing.Point(297, 547);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(83, 31);
            this.button_Save.TabIndex = 3;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Add
            // 
            this.button_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Add.AutoSize = true;
            this.button_Add.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_Add.Location = new System.Drawing.Point(105, 547);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(82, 31);
            this.button_Add.TabIndex = 40;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // ReturnBarcodeHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 601);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "ReturnBarcodeHistory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barcode Return ";
            this.Load += new System.EventHandler(this.ReturnBarcodeHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_WorkOrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_RawMaterial)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScanList)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_WorkOrder;
        private System.Windows.Forms.DataGridView dataGridView_RawMaterial;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox textBox_Workorder;
        private System.Windows.Forms.Button button_Serarch_Clear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox textBox_Barcode;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.ComboBox comboBox_To;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_RawMaterial_Search;
        private System.Windows.Forms.Label label_Workorder;
        private System.Windows.Forms.ComboBox comboBox_From;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_Scan_Order;
        private System.Windows.Forms.Label label_Barcode;
        private System.Windows.Forms.ComboBox comboBox_location;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox_location_group;
        private System.Windows.Forms.TextBox textBox_Scan_Barcode;
        private System.Windows.Forms.Label label_Supply;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Scan_DATE;
        private System.Windows.Forms.TextBox textBox_RM_Text;
        private System.Windows.Forms.TextBox textBox_Scan_SEQ;
        private System.Windows.Forms.TextBox textBox_Scan_QTY;
        private System.Windows.Forms.Label label_Date;
        private System.Windows.Forms.Label label_RM;
        private System.Windows.Forms.TextBox textBox_RM_Spec;
        private System.Windows.Forms.Label label_SEQ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Supply_Text;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Scan_Supply;
        private System.Windows.Forms.Label label_QTY;
        private System.Windows.Forms.TextBox textBox_Scan_Material;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dataGridView_ScanList;
        private System.Windows.Forms.Button button_Add;
    }
}