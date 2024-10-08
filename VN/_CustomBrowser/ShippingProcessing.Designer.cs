namespace WiseM.Browser
{
    partial class ShippingProcessing
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_qty = new System.Windows.Forms.Label();
            this.button_SelectOrder = new System.Windows.Forms.Button();
            this.tb_pallet = new System.Windows.Forms.TextBox();
            this.dataGridView_List = new System.Windows.Forms.DataGridView();
            this.PalletBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel_Information = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_BP_CD = new System.Windows.Forms.TextBox();
            this.label_BP = new System.Windows.Forms.Label();
            this.textBox_OrderSeq = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_OrderNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_OrderStatus = new System.Windows.Forms.CheckBox();
            this.label_OrderQty = new System.Windows.Forms.Label();
            this.label_OrderQtyValue = new System.Windows.Forms.Label();
            this.textBox_BP_NM = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Search = new System.Windows.Forms.Button();
            this.textBox_MaterialName = new System.Windows.Forms.TextBox();
            this.textBox_Spec = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Material = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox_Storage = new System.Windows.Forms.ComboBox();
            this.numericUpDown_AddQty = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_List)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel_Information.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AddQty)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Save.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Save.Location = new System.Drawing.Point(639, 581);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(618, 92);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "Lưu\r\n(Save)";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 82);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pallet\r\n(Pallet)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(233, 82);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tổng số lượng\r\n(TotalQty)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lb_qty
            // 
            this.lb_qty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lb_qty.AutoSize = true;
            this.lb_qty.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lb_qty.ForeColor = System.Drawing.Color.OrangeRed;
            this.lb_qty.Location = new System.Drawing.Point(415, 30);
            this.lb_qty.Name = "lb_qty";
            this.lb_qty.Size = new System.Drawing.Size(35, 41);
            this.lb_qty.TabIndex = 4;
            this.lb_qty.Text = "0";
            this.lb_qty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_SelectOrder
            // 
            this.button_SelectOrder.AutoSize = true;
            this.button_SelectOrder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_SelectOrder.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_SelectOrder.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button_SelectOrder.Location = new System.Drawing.Point(143, 7);
            this.button_SelectOrder.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.button_SelectOrder.Name = "button_SelectOrder";
            this.button_SelectOrder.Size = new System.Drawing.Size(152, 71);
            this.button_SelectOrder.TabIndex = 5;
            this.button_SelectOrder.Text = "Hiển thị lựa chọn(SelectOrder)";
            this.button_SelectOrder.UseVisualStyleBackColor = false;
            this.button_SelectOrder.Click += new System.EventHandler(this.button_SelectOrder_Click);
            // 
            // tb_pallet
            // 
            this.tb_pallet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_pallet.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tb_pallet.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold);
            this.tb_pallet.Location = new System.Drawing.Point(131, 27);
            this.tb_pallet.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.tb_pallet.Name = "tb_pallet";
            this.tb_pallet.Size = new System.Drawing.Size(488, 47);
            this.tb_pallet.TabIndex = 6;
            this.tb_pallet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_pallet_KeyDown);
            // 
            // dataGridView_List
            // 
            this.dataGridView_List.AllowUserToAddRows = false;
            this.dataGridView_List.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.dataGridView_List.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_List.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_List.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PalletBarcode,
            this.Qty,
            this.InputTime});
            this.tableLayoutPanel2.SetColumnSpan(this.dataGridView_List, 2);
            this.dataGridView_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_List.GridColor = System.Drawing.Color.White;
            this.dataGridView_List.Location = new System.Drawing.Point(3, 373);
            this.dataGridView_List.Name = "dataGridView_List";
            this.dataGridView_List.ReadOnly = true;
            this.dataGridView_List.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.dataGridView_List.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView_List.RowTemplate.Height = 27;
            this.dataGridView_List.RowTemplate.ReadOnly = true;
            this.dataGridView_List.Size = new System.Drawing.Size(1258, 197);
            this.dataGridView_List.TabIndex = 7;
            // 
            // PalletBarcode
            // 
            this.PalletBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PalletBarcode.DataPropertyName = "bcd";
            this.PalletBarcode.FillWeight = 50F;
            this.PalletBarcode.HeaderText = "PalletBarcode";
            this.PalletBarcode.Name = "PalletBarcode";
            this.PalletBarcode.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Qty.DataPropertyName = "qty";
            this.Qty.FillWeight = 30F;
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            // 
            // InputTime
            // 
            this.InputTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputTime.DataPropertyName = "time";
            this.InputTime.FillWeight = 20F;
            this.InputTime.HeaderText = "InputTime";
            this.InputTime.Name = "InputTime";
            this.InputTime.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Save, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView_List, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1264, 681);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lb_qty, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(635, 265);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(626, 102);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tb_pallet, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 265);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(626, 102);
            this.tableLayoutPanel4.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel_Information);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(626, 256);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<Order Information>";
            // 
            // tableLayoutPanel_Information
            // 
            this.tableLayoutPanel_Information.AutoSize = true;
            this.tableLayoutPanel_Information.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel_Information.ColumnCount = 4;
            this.tableLayoutPanel_Information.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Information.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Information.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel_Information.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel_Information.Controls.Add(this.textBox_BP_CD, 0, 4);
            this.tableLayoutPanel_Information.Controls.Add(this.label_BP, 0, 4);
            this.tableLayoutPanel_Information.Controls.Add(this.textBox_OrderSeq, 1, 2);
            this.tableLayoutPanel_Information.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel_Information.Controls.Add(this.textBox_OrderNo, 1, 1);
            this.tableLayoutPanel_Information.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel_Information.Controls.Add(this.checkBox_OrderStatus, 0, 0);
            this.tableLayoutPanel_Information.Controls.Add(this.button_SelectOrder, 1, 0);
            this.tableLayoutPanel_Information.Controls.Add(this.label_OrderQty, 2, 2);
            this.tableLayoutPanel_Information.Controls.Add(this.label_OrderQtyValue, 3, 2);
            this.tableLayoutPanel_Information.Controls.Add(this.textBox_BP_NM, 2, 4);
            this.tableLayoutPanel_Information.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel_Information.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel_Information.Name = "tableLayoutPanel_Information";
            this.tableLayoutPanel_Information.RowCount = 5;
            this.tableLayoutPanel_Information.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Information.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Information.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Information.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Information.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel_Information.Size = new System.Drawing.Size(620, 220);
            this.tableLayoutPanel_Information.TabIndex = 7;
            // 
            // textBox_BP_CD
            // 
            this.textBox_BP_CD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_BP_CD.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_BP_CD.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_BP_CD.Location = new System.Drawing.Point(144, 183);
            this.textBox_BP_CD.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_BP_CD.Name = "textBox_BP_CD";
            this.textBox_BP_CD.ReadOnly = true;
            this.textBox_BP_CD.Size = new System.Drawing.Size(150, 29);
            this.textBox_BP_CD.TabIndex = 14;
            // 
            // label_BP
            // 
            this.label_BP.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_BP.AutoSize = true;
            this.label_BP.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_BP.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label_BP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_BP.Location = new System.Drawing.Point(45, 187);
            this.label_BP.Name = "label_BP";
            this.label_BP.Size = new System.Drawing.Size(89, 21);
            this.label_BP.TabIndex = 12;
            this.label_BP.Text = "BP_CD/NM";
            this.label_BP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_OrderSeq
            // 
            this.textBox_OrderSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_OrderSeq.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_OrderSeq.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_OrderSeq.Location = new System.Drawing.Point(144, 138);
            this.textBox_OrderSeq.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_OrderSeq.Name = "textBox_OrderSeq";
            this.textBox_OrderSeq.ReadOnly = true;
            this.textBox_OrderSeq.Size = new System.Drawing.Size(150, 29);
            this.textBox_OrderSeq.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Location = new System.Drawing.Point(3, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 42);
            this.label5.TabIndex = 3;
            this.label5.Text = "Thứ tự đặt hàng\r\n(OrderSeq)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_OrderNo
            // 
            this.textBox_OrderNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_OrderNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel_Information.SetColumnSpan(this.textBox_OrderNo, 3);
            this.textBox_OrderNo.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_OrderNo.Location = new System.Drawing.Point(144, 93);
            this.textBox_OrderNo.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_OrderNo.Name = "textBox_OrderNo";
            this.textBox_OrderNo.ReadOnly = true;
            this.textBox_OrderNo.Size = new System.Drawing.Size(469, 29);
            this.textBox_OrderNo.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.Location = new System.Drawing.Point(33, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 42);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số đặt hàng\r\n(OrderNo)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_OrderStatus
            // 
            this.checkBox_OrderStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox_OrderStatus.AutoSize = true;
            this.checkBox_OrderStatus.Checked = true;
            this.checkBox_OrderStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OrderStatus.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_OrderStatus.Location = new System.Drawing.Point(3, 30);
            this.checkBox_OrderStatus.Name = "checkBox_OrderStatus";
            this.checkBox_OrderStatus.Size = new System.Drawing.Size(117, 25);
            this.checkBox_OrderStatus.TabIndex = 0;
            this.checkBox_OrderStatus.Text = "OrderStatus";
            this.checkBox_OrderStatus.UseVisualStyleBackColor = true;
            this.checkBox_OrderStatus.CheckedChanged += new System.EventHandler(this.checkBox_OrderStatus_CheckedChanged);
            // 
            // label_OrderQty
            // 
            this.label_OrderQty.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_OrderQty.AutoSize = true;
            this.label_OrderQty.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_OrderQty.Location = new System.Drawing.Point(304, 131);
            this.label_OrderQty.Name = "label_OrderQty";
            this.label_OrderQty.Size = new System.Drawing.Size(148, 42);
            this.label_OrderQty.TabIndex = 9;
            this.label_OrderQty.Text = "Số lượng đặt hàng\n(OrderQty)";
            this.label_OrderQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_OrderQtyValue
            // 
            this.label_OrderQtyValue.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_OrderQtyValue.AutoSize = true;
            this.label_OrderQtyValue.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label_OrderQtyValue.ForeColor = System.Drawing.Color.Black;
            this.label_OrderQtyValue.Location = new System.Drawing.Point(529, 142);
            this.label_OrderQtyValue.Name = "label_OrderQtyValue";
            this.label_OrderQtyValue.Size = new System.Drawing.Size(17, 21);
            this.label_OrderQtyValue.TabIndex = 11;
            this.label_OrderQtyValue.Text = "-";
            this.label_OrderQtyValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_BP_NM
            // 
            this.textBox_BP_NM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_BP_NM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel_Information.SetColumnSpan(this.textBox_BP_NM, 2);
            this.textBox_BP_NM.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_BP_NM.Location = new System.Drawing.Point(308, 183);
            this.textBox_BP_NM.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_BP_NM.Name = "textBox_BP_NM";
            this.textBox_BP_NM.ReadOnly = true;
            this.textBox_BP_NM.Size = new System.Drawing.Size(305, 29);
            this.textBox_BP_NM.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.groupBox2.Location = new System.Drawing.Point(635, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 256);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "<Material Information>";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Search, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.textBox_MaterialName, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBox_Spec, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.textBox_Material, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(620, 220);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tableLayoutPanel3.SetColumnSpan(this.btn_Search, 2);
            this.btn_Search.Enabled = false;
            this.btn_Search.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.btn_Search.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_Search.Location = new System.Drawing.Point(6, 7);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(169, 71);
            this.btn_Search.TabIndex = 11;
            this.btn_Search.Text = "Tìm kiếm\r\n(Search)";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // textBox_MaterialName
            // 
            this.textBox_MaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MaterialName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_MaterialName.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_MaterialName.Location = new System.Drawing.Point(164, 138);
            this.textBox_MaterialName.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_MaterialName.Name = "textBox_MaterialName";
            this.textBox_MaterialName.ReadOnly = true;
            this.textBox_MaterialName.Size = new System.Drawing.Size(449, 29);
            this.textBox_MaterialName.TabIndex = 16;
            // 
            // textBox_Spec
            // 
            this.textBox_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Spec.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Spec.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_Spec.Location = new System.Drawing.Point(164, 183);
            this.textBox_Spec.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_Spec.Name = "textBox_Spec";
            this.textBox_Spec.ReadOnly = true;
            this.textBox_Spec.Size = new System.Drawing.Size(449, 29);
            this.textBox_Spec.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(3, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "Code liệu(Material)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(85, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 42);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tên liệu\r\n(Name)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(76, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 42);
            this.label7.TabIndex = 14;
            this.label7.Text = "Quy cách\n(Spec)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Material
            // 
            this.textBox_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Material.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Material.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_Material.Location = new System.Drawing.Point(164, 93);
            this.textBox_Material.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_Material.Name = "textBox_Material";
            this.textBox_Material.ReadOnly = true;
            this.textBox_Material.Size = new System.Drawing.Size(449, 29);
            this.textBox_Material.TabIndex = 15;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.comboBox_Storage, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.numericUpDown_AddQty, 1, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 576);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(625, 100);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.tableLayoutPanel5.SetRowSpan(this.label8, 2);
            this.label8.Size = new System.Drawing.Size(238, 82);
            this.label8.TabIndex = 5;
            this.label8.Text = "Thêm số lượng\r\n(AddQty)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_Storage
            // 
            this.comboBox_Storage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Storage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Storage.DropDownWidth = 600;
            this.comboBox_Storage.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_Storage.FormattingEnabled = true;
            this.comboBox_Storage.Location = new System.Drawing.Point(247, 3);
            this.comboBox_Storage.Name = "comboBox_Storage";
            this.comboBox_Storage.Size = new System.Drawing.Size(375, 33);
            this.comboBox_Storage.TabIndex = 7;
            this.comboBox_Storage.SelectedIndexChanged += new System.EventHandler(this.comboBox_Storage_SelectedIndexChanged);
            // 
            // numericUpDown_AddQty
            // 
            this.numericUpDown_AddQty.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.numericUpDown_AddQty.Font = new System.Drawing.Font("맑은 고딕", 22.2F, System.Drawing.FontStyle.Bold);
            this.numericUpDown_AddQty.Location = new System.Drawing.Point(502, 46);
            this.numericUpDown_AddQty.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown_AddQty.Name = "numericUpDown_AddQty";
            this.numericUpDown_AddQty.Size = new System.Drawing.Size(120, 47);
            this.numericUpDown_AddQty.TabIndex = 6;
            this.numericUpDown_AddQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_AddQty.ThousandsSeparator = true;
            this.numericUpDown_AddQty.Leave += new System.EventHandler(this.numericUpDown_AddQty_Leave);
            // 
            // ShippingProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ShippingProcessing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vận chuyển(Shipment)";
            this.Load += new System.EventHandler(this.ShippingProcessing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_List)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel_Information.ResumeLayout(false);
            this.tableLayoutPanel_Information.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_AddQty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_qty;
        private System.Windows.Forms.Button button_SelectOrder;
        private System.Windows.Forms.TextBox tb_pallet;
        private System.Windows.Forms.DataGridView dataGridView_List;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBox_OrderStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Information;
        private System.Windows.Forms.Label label_OrderQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_OrderQtyValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_OrderNo;
        private System.Windows.Forms.TextBox textBox_OrderSeq;
        private System.Windows.Forms.TextBox textBox_Spec;
        private System.Windows.Forms.TextBox textBox_MaterialName;
        private System.Windows.Forms.TextBox textBox_Material;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.NumericUpDown numericUpDown_AddQty;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox textBox_BP_CD;
        private System.Windows.Forms.Label label_BP;
        private System.Windows.Forms.TextBox textBox_BP_NM;
        private System.Windows.Forms.ComboBox comboBox_Storage;
    }
}