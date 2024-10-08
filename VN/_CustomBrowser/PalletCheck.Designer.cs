namespace WiseM.Browser
{
    partial class PalletCheck
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
            this.textBox_BoxScanBarcode = new System.Windows.Forms.TextBox();
            this.dataGridView_ScanList = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Clear = new System.Windows.Forms.FlowLayoutPanel();
            this.button_Reset = new System.Windows.Forms.Button();
            this.textBox_ItemName = new System.Windows.Forms.TextBox();
            this.textBox_ItemCode = new System.Windows.Forms.TextBox();
            this.textBox_Qty = new System.Windows.Forms.TextBox();
            this.label_ScanBarcode = new System.Windows.Forms.Label();
            this.label_Material = new System.Windows.Forms.Label();
            this.label_Qty = new System.Windows.Forms.Label();
            this.label_MaterialName = new System.Windows.Forms.Label();
            this.textBox_MaterialName = new System.Windows.Forms.TextBox();
            this.label_SubScanBarcode = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Right = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel_Reset = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel_Save = new System.Windows.Forms.FlowLayoutPanel();
            this.button_Save = new System.Windows.Forms.Button();
            this.textBox_Material = new System.Windows.Forms.TextBox();
            this.label_Spec = new System.Windows.Forms.Label();
            this.textBox_Spec = new System.Windows.Forms.TextBox();
            this.textBox_PalletScanBarcode = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel_Scan = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_ScanInformation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_Body = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox_BoxScanHist = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_SubList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_ScanCountText = new System.Windows.Forms.Label();
            this.label_NoGoodCount = new System.Windows.Forms.Label();
            this.label_NoGoodCountText = new System.Windows.Forms.Label();
            this.label_OkeyCountText = new System.Windows.Forms.Label();
            this.label_ScanCount = new System.Windows.Forms.Label();
            this.label_OkeyCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScanList)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel_Clear.SuspendLayout();
            this.tableLayoutPanel_Right.SuspendLayout();
            this.flowLayoutPanel_Save.SuspendLayout();
            this.tableLayoutPanel_Scan.SuspendLayout();
            this.groupBox_ScanInformation.SuspendLayout();
            this.tableLayoutPanel_Body.SuspendLayout();
            this.groupBox_BoxScanHist.SuspendLayout();
            this.tableLayoutPanel_SubList.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_BoxScanBarcode
            // 
            this.textBox_BoxScanBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_BoxScanBarcode.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_BoxScanBarcode.Location = new System.Drawing.Point(89, 2);
            this.textBox_BoxScanBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_BoxScanBarcode.Name = "textBox_BoxScanBarcode";
            this.textBox_BoxScanBarcode.Size = new System.Drawing.Size(620, 43);
            this.textBox_BoxScanBarcode.TabIndex = 1;
            this.textBox_BoxScanBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_BoxScanBarcode_KeyDown);
            // 
            // dataGridView_ScanList
            // 
            this.dataGridView_ScanList.AllowUserToAddRows = false;
            this.dataGridView_ScanList.AllowUserToDeleteRows = false;
            this.dataGridView_ScanList.AllowUserToResizeRows = false;
            this.dataGridView_ScanList.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView_ScanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel_SubList.SetColumnSpan(this.dataGridView_ScanList, 2);
            this.dataGridView_ScanList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_ScanList.Location = new System.Drawing.Point(2, 185);
            this.dataGridView_ScanList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_ScanList.Name = "dataGridView_ScanList";
            this.dataGridView_ScanList.ReadOnly = true;
            this.dataGridView_ScanList.RowHeadersVisible = false;
            this.dataGridView_ScanList.RowTemplate.Height = 27;
            this.dataGridView_ScanList.Size = new System.Drawing.Size(707, 546);
            this.dataGridView_ScanList.TabIndex = 1;
            this.dataGridView_ScanList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView_ScanList_DataBindingComplete);
            this.dataGridView_ScanList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridView_ScanList_RowsAdded);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.AutoSize = true;
            this.tableLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.flowLayoutPanel_Clear, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(2, 779);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(718, 70);
            this.tableLayoutPanel5.TabIndex = 6;
            // 
            // flowLayoutPanel_Clear
            // 
            this.flowLayoutPanel_Clear.AutoSize = true;
            this.flowLayoutPanel_Clear.Controls.Add(this.button_Reset);
            this.flowLayoutPanel_Clear.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.flowLayoutPanel_Clear.Location = new System.Drawing.Point(3, 5);
            this.flowLayoutPanel_Clear.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.flowLayoutPanel_Clear.Name = "flowLayoutPanel_Clear";
            this.flowLayoutPanel_Clear.Size = new System.Drawing.Size(126, 60);
            this.flowLayoutPanel_Clear.TabIndex = 2;
            // 
            // button_Reset
            // 
            this.button_Reset.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.button_Reset.Location = new System.Drawing.Point(0, 0);
            this.button_Reset.Margin = new System.Windows.Forms.Padding(0);
            this.button_Reset.Name = "button_Reset";
            this.button_Reset.Size = new System.Drawing.Size(126, 60);
            this.button_Reset.TabIndex = 0;
            this.button_Reset.Text = "Cài lại\r\n(Reset)";
            this.button_Reset.UseVisualStyleBackColor = true;
            this.button_Reset.Click += new System.EventHandler(this.button_Reset_Click);
            // 
            // textBox_ItemName
            // 
            this.textBox_ItemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ItemName.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ItemName.Location = new System.Drawing.Point(505, 164);
            this.textBox_ItemName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_ItemName.Name = "textBox_ItemName";
            this.textBox_ItemName.ReadOnly = true;
            this.textBox_ItemName.Size = new System.Drawing.Size(202, 43);
            this.textBox_ItemName.TabIndex = 16;
            // 
            // textBox_ItemCode
            // 
            this.textBox_ItemCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ItemCode.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ItemCode.Location = new System.Drawing.Point(157, 164);
            this.textBox_ItemCode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_ItemCode.Name = "textBox_ItemCode";
            this.textBox_ItemCode.ReadOnly = true;
            this.textBox_ItemCode.Size = new System.Drawing.Size(201, 43);
            this.textBox_ItemCode.TabIndex = 15;
            // 
            // textBox_Qty
            // 
            this.textBox_Qty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Qty.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Qty.Location = new System.Drawing.Point(505, 58);
            this.textBox_Qty.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_Qty.Name = "textBox_Qty";
            this.textBox_Qty.ReadOnly = true;
            this.textBox_Qty.Size = new System.Drawing.Size(202, 43);
            this.textBox_Qty.TabIndex = 12;
            // 
            // label_ScanBarcode
            // 
            this.label_ScanBarcode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_ScanBarcode.AutoSize = true;
            this.label_ScanBarcode.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ScanBarcode.Location = new System.Drawing.Point(68, 1);
            this.label_ScanBarcode.Name = "label_ScanBarcode";
            this.label_ScanBarcode.Size = new System.Drawing.Size(83, 50);
            this.label_ScanBarcode.TabIndex = 8;
            this.label_ScanBarcode.Text = "Đầu vào\r\n(Input)";
            this.label_ScanBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Material
            // 
            this.label_Material.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Material.AutoSize = true;
            this.label_Material.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Material.Location = new System.Drawing.Point(76, 107);
            this.label_Material.Name = "label_Material";
            this.label_Material.Size = new System.Drawing.Size(75, 50);
            this.label_Material.TabIndex = 2;
            this.label_Material.Text = "Code\r\n(CODE)";
            this.label_Material.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Qty
            // 
            this.label_Qty.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Qty.AutoSize = true;
            this.label_Qty.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Qty.Location = new System.Drawing.Point(410, 54);
            this.label_Qty.Name = "label_Qty";
            this.label_Qty.Size = new System.Drawing.Size(89, 50);
            this.label_Qty.TabIndex = 10;
            this.label_Qty.Text = "Số lượng\r\n(QTY)";
            this.label_Qty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_MaterialName
            // 
            this.label_MaterialName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_MaterialName.AutoSize = true;
            this.label_MaterialName.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_MaterialName.Location = new System.Drawing.Point(364, 107);
            this.label_MaterialName.Name = "label_MaterialName";
            this.label_MaterialName.Size = new System.Drawing.Size(135, 50);
            this.label_MaterialName.TabIndex = 3;
            this.label_MaterialName.Text = "Tên sản phẩm\r\n(NAME)";
            this.label_MaterialName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_MaterialName
            // 
            this.textBox_MaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MaterialName.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_MaterialName.Location = new System.Drawing.Point(505, 111);
            this.textBox_MaterialName.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_MaterialName.Name = "textBox_MaterialName";
            this.textBox_MaterialName.ReadOnly = true;
            this.textBox_MaterialName.Size = new System.Drawing.Size(202, 43);
            this.textBox_MaterialName.TabIndex = 6;
            // 
            // label_SubScanBarcode
            // 
            this.label_SubScanBarcode.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_SubScanBarcode.AutoSize = true;
            this.label_SubScanBarcode.Font = new System.Drawing.Font("맑은 고딕", 10.8F);
            this.label_SubScanBarcode.Location = new System.Drawing.Point(2, 0);
            this.label_SubScanBarcode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_SubScanBarcode.Name = "label_SubScanBarcode";
            this.label_SubScanBarcode.Size = new System.Drawing.Size(83, 50);
            this.label_SubScanBarcode.TabIndex = 0;
            this.label_SubScanBarcode.Text = "Đầu vào\r\n(Input)\r\n";
            this.label_SubScanBarcode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel_Right
            // 
            this.tableLayoutPanel_Right.AutoSize = true;
            this.tableLayoutPanel_Right.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Right.ColumnCount = 2;
            this.tableLayoutPanel_Right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Right.Controls.Add(this.flowLayoutPanel_Reset, 0, 0);
            this.tableLayoutPanel_Right.Controls.Add(this.flowLayoutPanel_Save, 1, 0);
            this.tableLayoutPanel_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Right.Enabled = false;
            this.tableLayoutPanel_Right.Location = new System.Drawing.Point(724, 779);
            this.tableLayoutPanel_Right.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel_Right.Name = "tableLayoutPanel_Right";
            this.tableLayoutPanel_Right.RowCount = 1;
            this.tableLayoutPanel_Right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Right.Size = new System.Drawing.Size(719, 70);
            this.tableLayoutPanel_Right.TabIndex = 5;
            // 
            // flowLayoutPanel_Reset
            // 
            this.flowLayoutPanel_Reset.AutoSize = true;
            this.flowLayoutPanel_Reset.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.flowLayoutPanel_Reset.Location = new System.Drawing.Point(3, 5);
            this.flowLayoutPanel_Reset.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.flowLayoutPanel_Reset.Name = "flowLayoutPanel_Reset";
            this.flowLayoutPanel_Reset.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel_Reset.TabIndex = 1;
            // 
            // flowLayoutPanel_Save
            // 
            this.flowLayoutPanel_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel_Save.AutoSize = true;
            this.flowLayoutPanel_Save.Controls.Add(this.button_Save);
            this.flowLayoutPanel_Save.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.flowLayoutPanel_Save.Location = new System.Drawing.Point(590, 5);
            this.flowLayoutPanel_Save.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.flowLayoutPanel_Save.Name = "flowLayoutPanel_Save";
            this.flowLayoutPanel_Save.Size = new System.Drawing.Size(126, 60);
            this.flowLayoutPanel_Save.TabIndex = 0;
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.button_Save.Location = new System.Drawing.Point(0, 0);
            this.button_Save.Margin = new System.Windows.Forms.Padding(0);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(126, 60);
            this.button_Save.TabIndex = 0;
            this.button_Save.Text = "Lưu\r\n(SAVE)";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // textBox_Material
            // 
            this.textBox_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Material.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Material.Location = new System.Drawing.Point(157, 111);
            this.textBox_Material.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_Material.Name = "textBox_Material";
            this.textBox_Material.ReadOnly = true;
            this.textBox_Material.Size = new System.Drawing.Size(201, 43);
            this.textBox_Material.TabIndex = 5;
            // 
            // label_Spec
            // 
            this.label_Spec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Spec.AutoSize = true;
            this.label_Spec.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Spec.Location = new System.Drawing.Point(3, 226);
            this.label_Spec.Name = "label_Spec";
            this.label_Spec.Size = new System.Drawing.Size(148, 25);
            this.label_Spec.TabIndex = 4;
            this.label_Spec.Text = "Quy cách(SPEC)";
            this.label_Spec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Spec
            // 
            this.textBox_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel_Scan.SetColumnSpan(this.textBox_Spec, 3);
            this.textBox_Spec.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Spec.Location = new System.Drawing.Point(157, 217);
            this.textBox_Spec.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_Spec.Name = "textBox_Spec";
            this.textBox_Spec.ReadOnly = true;
            this.textBox_Spec.Size = new System.Drawing.Size(550, 43);
            this.textBox_Spec.TabIndex = 7;
            // 
            // textBox_PalletScanBarcode
            // 
            this.textBox_PalletScanBarcode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel_Scan.SetColumnSpan(this.textBox_PalletScanBarcode, 3);
            this.textBox_PalletScanBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_PalletScanBarcode.Font = new System.Drawing.Font("맑은 고딕", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_PalletScanBarcode.Location = new System.Drawing.Point(157, 5);
            this.textBox_PalletScanBarcode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBox_PalletScanBarcode.Name = "textBox_PalletScanBarcode";
            this.textBox_PalletScanBarcode.Size = new System.Drawing.Size(550, 43);
            this.textBox_PalletScanBarcode.TabIndex = 0;
            this.textBox_PalletScanBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_ScanBarcode_KeyDown);
            // 
            // tableLayoutPanel_Scan
            // 
            this.tableLayoutPanel_Scan.AutoSize = true;
            this.tableLayoutPanel_Scan.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Scan.ColumnCount = 4;
            this.tableLayoutPanel_Scan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Scan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Scan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Scan.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_ItemName, 3, 3);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_ItemCode, 1, 3);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_Qty, 3, 1);
            this.tableLayoutPanel_Scan.Controls.Add(this.label_ScanBarcode, 0, 0);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_Material, 1, 2);
            this.tableLayoutPanel_Scan.Controls.Add(this.label_Material, 0, 2);
            this.tableLayoutPanel_Scan.Controls.Add(this.label_Qty, 2, 1);
            this.tableLayoutPanel_Scan.Controls.Add(this.label_MaterialName, 2, 2);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_MaterialName, 3, 2);
            this.tableLayoutPanel_Scan.Controls.Add(this.label_Spec, 0, 4);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_Spec, 1, 4);
            this.tableLayoutPanel_Scan.Controls.Add(this.textBox_PalletScanBarcode, 1, 0);
            this.tableLayoutPanel_Scan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Scan.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel_Scan.Location = new System.Drawing.Point(3, 29);
            this.tableLayoutPanel_Scan.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tableLayoutPanel_Scan.Name = "tableLayoutPanel_Scan";
            this.tableLayoutPanel_Scan.RowCount = 5;
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Scan.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel_Scan.Size = new System.Drawing.Size(710, 265);
            this.tableLayoutPanel_Scan.TabIndex = 0;
            // 
            // groupBox_ScanInformation
            // 
            this.groupBox_ScanInformation.AutoSize = true;
            this.groupBox_ScanInformation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox_ScanInformation.Controls.Add(this.tableLayoutPanel_Scan);
            this.groupBox_ScanInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_ScanInformation.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_ScanInformation.Location = new System.Drawing.Point(3, 5);
            this.groupBox_ScanInformation.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox_ScanInformation.Name = "groupBox_ScanInformation";
            this.groupBox_ScanInformation.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox_ScanInformation.Size = new System.Drawing.Size(716, 299);
            this.groupBox_ScanInformation.TabIndex = 2;
            this.groupBox_ScanInformation.TabStop = false;
            this.groupBox_ScanInformation.Text = "<(Pallet Information)>";
            // 
            // tableLayoutPanel_Body
            // 
            this.tableLayoutPanel_Body.ColumnCount = 2;
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Body.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Body.Controls.Add(this.groupBox_ScanInformation, 0, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.groupBox_BoxScanHist, 1, 0);
            this.tableLayoutPanel_Body.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel_Body.Controls.Add(this.tableLayoutPanel_Right, 1, 2);
            this.tableLayoutPanel_Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Body.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Bold);
            this.tableLayoutPanel_Body.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Body.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tableLayoutPanel_Body.Name = "tableLayoutPanel_Body";
            this.tableLayoutPanel_Body.RowCount = 3;
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Body.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel_Body.Size = new System.Drawing.Size(1445, 851);
            this.tableLayoutPanel_Body.TabIndex = 1;
            // 
            // groupBox_BoxScanHist
            // 
            this.groupBox_BoxScanHist.Controls.Add(this.tableLayoutPanel_SubList);
            this.groupBox_BoxScanHist.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox_BoxScanHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_BoxScanHist.Enabled = false;
            this.groupBox_BoxScanHist.Font = new System.Drawing.Font("맑은 고딕", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox_BoxScanHist.Location = new System.Drawing.Point(725, 5);
            this.groupBox_BoxScanHist.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox_BoxScanHist.Name = "groupBox_BoxScanHist";
            this.groupBox_BoxScanHist.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tableLayoutPanel_Body.SetRowSpan(this.groupBox_BoxScanHist, 2);
            this.groupBox_BoxScanHist.Size = new System.Drawing.Size(717, 767);
            this.groupBox_BoxScanHist.TabIndex = 3;
            this.groupBox_BoxScanHist.TabStop = false;
            this.groupBox_BoxScanHist.Text = "<(Box Scan Hist)>";
            // 
            // tableLayoutPanel_SubList
            // 
            this.tableLayoutPanel_SubList.ColumnCount = 2;
            this.tableLayoutPanel_SubList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_SubList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_SubList.Controls.Add(this.textBox_BoxScanBarcode, 1, 0);
            this.tableLayoutPanel_SubList.Controls.Add(this.label_SubScanBarcode, 0, 0);
            this.tableLayoutPanel_SubList.Controls.Add(this.dataGridView_ScanList, 0, 2);
            this.tableLayoutPanel_SubList.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel_SubList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_SubList.Location = new System.Drawing.Point(3, 29);
            this.tableLayoutPanel_SubList.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel_SubList.Name = "tableLayoutPanel_SubList";
            this.tableLayoutPanel_SubList.RowCount = 3;
            this.tableLayoutPanel_SubList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_SubList.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_SubList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel_SubList.Size = new System.Drawing.Size(711, 733);
            this.tableLayoutPanel_SubList.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel_SubList.SetColumnSpan(this.tableLayoutPanel1, 2);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label_ScanCountText, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_NoGoodCount, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_NoGoodCountText, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_OkeyCountText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_ScanCount, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_OkeyCount, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 54);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(705, 125);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label_ScanCountText
            // 
            this.label_ScanCountText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_ScanCountText.AutoSize = true;
            this.label_ScanCountText.Location = new System.Drawing.Point(62, 19);
            this.label_ScanCountText.Name = "label_ScanCountText";
            this.label_ScanCountText.Size = new System.Drawing.Size(111, 25);
            this.label_ScanCountText.TabIndex = 2;
            this.label_ScanCountText.Text = "Scan Count";
            // 
            // label_NoGoodCount
            // 
            this.label_NoGoodCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_NoGoodCount.AutoSize = true;
            this.label_NoGoodCount.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_NoGoodCount.ForeColor = System.Drawing.Color.Red;
            this.label_NoGoodCount.Location = new System.Drawing.Point(574, 79);
            this.label_NoGoodCount.Name = "label_NoGoodCount";
            this.label_NoGoodCount.Size = new System.Drawing.Size(24, 28);
            this.label_NoGoodCount.TabIndex = 5;
            this.label_NoGoodCount.Text = "0";
            // 
            // label_NoGoodCountText
            // 
            this.label_NoGoodCountText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_NoGoodCountText.AutoSize = true;
            this.label_NoGoodCountText.ForeColor = System.Drawing.Color.Red;
            this.label_NoGoodCountText.Location = new System.Drawing.Point(537, 19);
            this.label_NoGoodCountText.Name = "label_NoGoodCountText";
            this.label_NoGoodCountText.Size = new System.Drawing.Size(99, 25);
            this.label_NoGoodCountText.TabIndex = 0;
            this.label_NoGoodCountText.Text = "NG Count";
            // 
            // label_OkeyCountText
            // 
            this.label_OkeyCountText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_OkeyCountText.AutoSize = true;
            this.label_OkeyCountText.ForeColor = System.Drawing.Color.Green;
            this.label_OkeyCountText.Location = new System.Drawing.Point(303, 19);
            this.label_OkeyCountText.Name = "label_OkeyCountText";
            this.label_OkeyCountText.Size = new System.Drawing.Size(97, 25);
            this.label_OkeyCountText.TabIndex = 1;
            this.label_OkeyCountText.Text = "OK Count";
            // 
            // label_ScanCount
            // 
            this.label_ScanCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_ScanCount.AutoSize = true;
            this.label_ScanCount.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ScanCount.Location = new System.Drawing.Point(106, 79);
            this.label_ScanCount.Name = "label_ScanCount";
            this.label_ScanCount.Size = new System.Drawing.Size(24, 28);
            this.label_ScanCount.TabIndex = 3;
            this.label_ScanCount.Text = "0";
            // 
            // label_OkeyCount
            // 
            this.label_OkeyCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_OkeyCount.AutoSize = true;
            this.label_OkeyCount.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_OkeyCount.ForeColor = System.Drawing.Color.Green;
            this.label_OkeyCount.Location = new System.Drawing.Point(340, 79);
            this.label_OkeyCount.Name = "label_OkeyCount";
            this.label_OkeyCount.Size = new System.Drawing.Size(24, 28);
            this.label_OkeyCount.TabIndex = 4;
            this.label_OkeyCount.Text = "0";
            // 
            // PalletCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1445, 851);
            this.Controls.Add(this.tableLayoutPanel_Body);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PalletCheck";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pallet Check Program";
            this.Load += new System.EventHandler(this.PalletCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ScanList)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.flowLayoutPanel_Clear.ResumeLayout(false);
            this.tableLayoutPanel_Right.ResumeLayout(false);
            this.tableLayoutPanel_Right.PerformLayout();
            this.flowLayoutPanel_Save.ResumeLayout(false);
            this.tableLayoutPanel_Scan.ResumeLayout(false);
            this.tableLayoutPanel_Scan.PerformLayout();
            this.groupBox_ScanInformation.ResumeLayout(false);
            this.groupBox_ScanInformation.PerformLayout();
            this.tableLayoutPanel_Body.ResumeLayout(false);
            this.tableLayoutPanel_Body.PerformLayout();
            this.groupBox_BoxScanHist.ResumeLayout(false);
            this.tableLayoutPanel_SubList.ResumeLayout(false);
            this.tableLayoutPanel_SubList.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_BoxScanBarcode;
        private System.Windows.Forms.DataGridView dataGridView_ScanList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_SubList;
        private System.Windows.Forms.Label label_SubScanBarcode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Clear;
        private System.Windows.Forms.Button button_Reset;
        private System.Windows.Forms.TextBox textBox_ItemName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Scan;
        private System.Windows.Forms.TextBox textBox_ItemCode;
        private System.Windows.Forms.TextBox textBox_Qty;
        private System.Windows.Forms.Label label_ScanBarcode;
        private System.Windows.Forms.TextBox textBox_Material;
        private System.Windows.Forms.Label label_Material;
        private System.Windows.Forms.Label label_Qty;
        private System.Windows.Forms.Label label_MaterialName;
        private System.Windows.Forms.TextBox textBox_MaterialName;
        private System.Windows.Forms.Label label_Spec;
        private System.Windows.Forms.TextBox textBox_Spec;
        private System.Windows.Forms.TextBox textBox_PalletScanBarcode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Right;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Reset;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Save;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.GroupBox groupBox_ScanInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Body;
        private System.Windows.Forms.GroupBox groupBox_BoxScanHist;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_NoGoodCountText;
        private System.Windows.Forms.Label label_OkeyCountText;
        private System.Windows.Forms.Label label_ScanCountText;
        private System.Windows.Forms.Label label_ScanCount;
        private System.Windows.Forms.Label label_OkeyCount;
        private System.Windows.Forms.Label label_NoGoodCount;
    }
}