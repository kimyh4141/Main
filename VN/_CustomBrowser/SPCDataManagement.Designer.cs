namespace SPCDataManagement
{
    partial class SPCDataManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem4 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.dataGridViewSPC = new System.Windows.Forms.DataGridView();
            this._check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this._lot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._x3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._x4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._x5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uiRadioButtonThickness = new Janus.Windows.EditControls.UIRadioButton();
            this.uiRadioButtonWidth = new Janus.Windows.EditControls.UIRadioButton();
            this.uiRadioButtonLength = new Janus.Windows.EditControls.UIRadioButton();
            this.uiComboBoxMaterialPACK = new Janus.Windows.EditControls.UIComboBox();
            this.uiCheckBoxSPC = new Janus.Windows.EditControls.UICheckBox();
            this.uiButtonClearAll = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.uiRadioButtonRight = new Janus.Windows.EditControls.UIRadioButton();
            this.uiComboBoxMaterialTENSILITY = new Janus.Windows.EditControls.UIComboBox();
            this.uiRadioButtonLeft = new Janus.Windows.EditControls.UIRadioButton();
            this.uiButtonSelectedDelete = new Janus.Windows.EditControls.UIButton();
            this.uiButtonSave = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).BeginInit();
            this.SkinPager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSPC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SkinPager
            // 
            this.SkinPager.Controls.Add(this.uiButtonSave);
            this.SkinPager.Controls.Add(this.uiButtonSelectedDelete);
            this.SkinPager.Controls.Add(this.uiGroupBox2);
            this.SkinPager.Controls.Add(this.uiButtonClearAll);
            this.SkinPager.Controls.Add(this.uiCheckBoxSPC);
            this.SkinPager.Controls.Add(this.uiGroupBox1);
            this.SkinPager.Controls.Add(this.dataGridViewSPC);
            this.SkinPager.Margin = new System.Windows.Forms.Padding(4);
            this.SkinPager.Padding = new System.Windows.Forms.Padding(4);
            this.SkinPager.Size = new System.Drawing.Size(984, 601);
            // 
            // dataGridViewSPC
            // 
            this.dataGridViewSPC.AllowUserToDeleteRows = false;
            this.dataGridViewSPC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSPC.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSPC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSPC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._check,
            this._lot,
            this._seq,
            this._x1,
            this._x2,
            this._x3,
            this._x4,
            this._x5});
            this.dataGridViewSPC.Location = new System.Drawing.Point(12, 154);
            this.dataGridViewSPC.Name = "dataGridViewSPC";
            this.dataGridViewSPC.RowTemplate.Height = 23;
            this.dataGridViewSPC.Size = new System.Drawing.Size(957, 381);
            this.dataGridViewSPC.TabIndex = 0;
            // 
            // _check
            // 
            this._check.HeaderText = "Check";
            this._check.Name = "_check";
            this._check.Width = 47;
            // 
            // _lot
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._lot.DefaultCellStyle = dataGridViewCellStyle2;
            this._lot.HeaderText = "Lot(Month)";
            this._lot.Name = "_lot";
            this._lot.Width = 92;
            // 
            // _seq
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._seq.DefaultCellStyle = dataGridViewCellStyle3;
            this._seq.HeaderText = "Seq(Day)";
            this._seq.Name = "_seq";
            this._seq.Width = 84;
            // 
            // _x1
            // 
            this._x1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._x1.DefaultCellStyle = dataGridViewCellStyle4;
            this._x1.HeaderText = "X1";
            this._x1.Name = "_x1";
            this._x1.Width = 130;
            // 
            // _x2
            // 
            this._x2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._x2.DefaultCellStyle = dataGridViewCellStyle5;
            this._x2.HeaderText = "X2";
            this._x2.Name = "_x2";
            this._x2.Width = 130;
            // 
            // _x3
            // 
            this._x3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._x3.DefaultCellStyle = dataGridViewCellStyle6;
            this._x3.HeaderText = "X3";
            this._x3.Name = "_x3";
            this._x3.Width = 130;
            // 
            // _x4
            // 
            this._x4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._x4.DefaultCellStyle = dataGridViewCellStyle7;
            this._x4.HeaderText = "X4";
            this._x4.Name = "_x4";
            this._x4.Width = 130;
            // 
            // _x5
            // 
            this._x5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._x5.DefaultCellStyle = dataGridViewCellStyle8;
            this._x5.HeaderText = "X5";
            this._x5.Name = "_x5";
            this._x5.Width = 130;
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.BackColor = System.Drawing.Color.Transparent;
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.uiRadioButtonThickness);
            this.uiGroupBox1.Controls.Add(this.uiRadioButtonWidth);
            this.uiGroupBox1.Controls.Add(this.uiRadioButtonLength);
            this.uiGroupBox1.Controls.Add(this.uiComboBoxMaterialPACK);
            this.uiGroupBox1.Location = new System.Drawing.Point(13, 13);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(475, 106);
            this.uiGroupBox1.TabIndex = 2;
            this.uiGroupBox1.Text = "PACK";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product :";
            // 
            // uiRadioButtonThickness
            // 
            this.uiRadioButtonThickness.Location = new System.Drawing.Point(6, 76);
            this.uiRadioButtonThickness.Name = "uiRadioButtonThickness";
            this.uiRadioButtonThickness.Size = new System.Drawing.Size(104, 23);
            this.uiRadioButtonThickness.TabIndex = 6;
            this.uiRadioButtonThickness.Text = "Thickness";
            this.uiRadioButtonThickness.CheckedChanged += new System.EventHandler(this.uiRadioButtonThickness_CheckedChanged);
            // 
            // uiRadioButtonWidth
            // 
            this.uiRadioButtonWidth.Location = new System.Drawing.Point(6, 47);
            this.uiRadioButtonWidth.Name = "uiRadioButtonWidth";
            this.uiRadioButtonWidth.Size = new System.Drawing.Size(104, 23);
            this.uiRadioButtonWidth.TabIndex = 5;
            this.uiRadioButtonWidth.Text = "Width";
            this.uiRadioButtonWidth.CheckedChanged += new System.EventHandler(this.uiRadioButtonWidth_CheckedChanged);
            // 
            // uiRadioButtonLength
            // 
            this.uiRadioButtonLength.Location = new System.Drawing.Point(6, 18);
            this.uiRadioButtonLength.Name = "uiRadioButtonLength";
            this.uiRadioButtonLength.Size = new System.Drawing.Size(104, 23);
            this.uiRadioButtonLength.TabIndex = 4;
            this.uiRadioButtonLength.Text = "Length";
            this.uiRadioButtonLength.CheckedChanged += new System.EventHandler(this.uiRadioButtonLength_CheckedChanged);
            // 
            // uiComboBoxMaterialPACK
            // 
            this.uiComboBoxMaterialPACK.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "GLASS";
            uiComboBoxItem4.FormatStyle.Alpha = 0;
            uiComboBoxItem4.IsSeparator = false;
            uiComboBoxItem4.Text = "FPCB";
            this.uiComboBoxMaterialPACK.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem3,
            uiComboBoxItem4});
            this.uiComboBoxMaterialPACK.Location = new System.Drawing.Point(235, 20);
            this.uiComboBoxMaterialPACK.Name = "uiComboBoxMaterialPACK";
            this.uiComboBoxMaterialPACK.Size = new System.Drawing.Size(234, 21);
            this.uiComboBoxMaterialPACK.TabIndex = 3;
            // 
            // uiCheckBoxSPC
            // 
            this.uiCheckBoxSPC.BackColor = System.Drawing.Color.Transparent;
            this.uiCheckBoxSPC.Location = new System.Drawing.Point(12, 125);
            this.uiCheckBoxSPC.Name = "uiCheckBoxSPC";
            this.uiCheckBoxSPC.Size = new System.Drawing.Size(81, 23);
            this.uiCheckBoxSPC.TabIndex = 4;
            this.uiCheckBoxSPC.Text = "Select All";
            this.uiCheckBoxSPC.CheckedChanged += new System.EventHandler(this.uiCheckBoxMaterial_CheckedChanged);
            // 
            // uiButtonClearAll
            // 
            this.uiButtonClearAll.Location = new System.Drawing.Point(13, 541);
            this.uiButtonClearAll.Name = "uiButtonClearAll";
            this.uiButtonClearAll.Size = new System.Drawing.Size(107, 48);
            this.uiButtonClearAll.TabIndex = 5;
            this.uiButtonClearAll.Text = "Clear All";
            this.uiButtonClearAll.Click += new System.EventHandler(this.uiButtonClearAll_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.BackColor = System.Drawing.Color.Transparent;
            this.uiGroupBox2.Controls.Add(this.label2);
            this.uiGroupBox2.Controls.Add(this.uiRadioButtonRight);
            this.uiGroupBox2.Controls.Add(this.uiComboBoxMaterialTENSILITY);
            this.uiGroupBox2.Controls.Add(this.uiRadioButtonLeft);
            this.uiGroupBox2.Location = new System.Drawing.Point(494, 13);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(475, 106);
            this.uiGroupBox2.TabIndex = 4;
            this.uiGroupBox2.Text = "TENSILITY";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(181, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Model :";
            // 
            // uiRadioButtonRight
            // 
            this.uiRadioButtonRight.Location = new System.Drawing.Point(6, 47);
            this.uiRadioButtonRight.Name = "uiRadioButtonRight";
            this.uiRadioButtonRight.Size = new System.Drawing.Size(104, 23);
            this.uiRadioButtonRight.TabIndex = 5;
            this.uiRadioButtonRight.Text = "Right";
            // 
            // uiComboBoxMaterialTENSILITY
            // 
            this.uiComboBoxMaterialTENSILITY.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "GLASS";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "FPCB";
            this.uiComboBoxMaterialTENSILITY.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2});
            this.uiComboBoxMaterialTENSILITY.Location = new System.Drawing.Point(235, 20);
            this.uiComboBoxMaterialTENSILITY.Name = "uiComboBoxMaterialTENSILITY";
            this.uiComboBoxMaterialTENSILITY.Size = new System.Drawing.Size(234, 21);
            this.uiComboBoxMaterialTENSILITY.TabIndex = 3;
            // 
            // uiRadioButtonLeft
            // 
            this.uiRadioButtonLeft.Location = new System.Drawing.Point(6, 18);
            this.uiRadioButtonLeft.Name = "uiRadioButtonLeft";
            this.uiRadioButtonLeft.Size = new System.Drawing.Size(104, 23);
            this.uiRadioButtonLeft.TabIndex = 4;
            this.uiRadioButtonLeft.Text = "Left";
            // 
            // uiButtonSelectedDelete
            // 
            this.uiButtonSelectedDelete.Location = new System.Drawing.Point(135, 541);
            this.uiButtonSelectedDelete.Name = "uiButtonSelectedDelete";
            this.uiButtonSelectedDelete.Size = new System.Drawing.Size(107, 48);
            this.uiButtonSelectedDelete.TabIndex = 6;
            this.uiButtonSelectedDelete.Text = "Delete Line";
            this.uiButtonSelectedDelete.Click += new System.EventHandler(this.uiButtonSelectedDelete_Click);
            // 
            // uiButtonSave
            // 
            this.uiButtonSave.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButtonSave.Location = new System.Drawing.Point(862, 541);
            this.uiButtonSave.Name = "uiButtonSave";
            this.uiButtonSave.Size = new System.Drawing.Size(107, 48);
            this.uiButtonSave.TabIndex = 7;
            this.uiButtonSave.Text = "Save / Clear";
            this.uiButtonSave.Click += new System.EventHandler(this.uiButtonSave_Click);
            // 
            // SPCDataManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 601);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "SPCDataManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPC Data Management";
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).EndInit();
            this.SkinPager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSPC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            this.uiGroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewSPC;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UICheckBox uiCheckBoxSPC;
        private Janus.Windows.EditControls.UIButton uiButtonClearAll;
        private Janus.Windows.EditControls.UIComboBox uiComboBoxMaterialPACK;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.EditControls.UIComboBox uiComboBoxMaterialTENSILITY;
        private Janus.Windows.EditControls.UIButton uiButtonSave;
        private Janus.Windows.EditControls.UIButton uiButtonSelectedDelete;
        private System.Windows.Forms.Label label2;
        private Janus.Windows.EditControls.UIRadioButton uiRadioButtonRight;
        private Janus.Windows.EditControls.UIRadioButton uiRadioButtonLeft;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIRadioButton uiRadioButtonThickness;
        private Janus.Windows.EditControls.UIRadioButton uiRadioButtonWidth;
        private Janus.Windows.EditControls.UIRadioButton uiRadioButtonLength;
        private System.Windows.Forms.DataGridViewCheckBoxColumn _check;
        private System.Windows.Forms.DataGridViewTextBoxColumn _lot;
        private System.Windows.Forms.DataGridViewTextBoxColumn _seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn _x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn _x3;
        private System.Windows.Forms.DataGridViewTextBoxColumn _x4;
        private System.Windows.Forms.DataGridViewTextBoxColumn _x5;
    }
}