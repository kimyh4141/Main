namespace WiseM.Browser
{
    partial class JigstockOut
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
            this.label1 = new System.Windows.Forms.Label();
            this.Combo_LocationGroup = new System.Windows.Forms.ComboBox();
            this.Tb_Jig = new System.Windows.Forms.TextBox();
            this.Btn_Save = new System.Windows.Forms.Button();
            this.Combo_Location = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.JigCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JigName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationBunchTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Recipient = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.ComBo_Recipient = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).BeginInit();
            this.SkinPager.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // SkinPager
            // 
            this.SkinPager.Controls.Add(this.Btn_Save);
            this.SkinPager.Controls.Add(this.tableLayoutPanel2);
            this.SkinPager.Controls.Add(this.tableLayoutPanel3);
            this.SkinPager.Controls.Add(this.tableLayoutPanel4);
            this.SkinPager.Controls.Add(this.Combo_Location);
            this.SkinPager.Controls.Add(this.tableLayoutPanel1);
            this.SkinPager.Size = new System.Drawing.Size(696, 569);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(161, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "JigCode : ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Combo_LocationGroup
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.Combo_LocationGroup, 2);
            this.Combo_LocationGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Combo_LocationGroup.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Combo_LocationGroup.FormattingEnabled = true;
            this.Combo_LocationGroup.Location = new System.Drawing.Point(176, 6);
            this.Combo_LocationGroup.Name = "Combo_LocationGroup";
            this.Combo_LocationGroup.Size = new System.Drawing.Size(487, 27);
            this.Combo_LocationGroup.TabIndex = 4;
            this.Combo_LocationGroup.SelectedIndexChanged += new System.EventHandler(this.Combo_LocationGroup_SelectedIndexChanged);
            // 
            // Tb_Jig
            // 
            this.Tb_Jig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tb_Jig.Location = new System.Drawing.Point(176, 6);
            this.Tb_Jig.Name = "Tb_Jig";
            this.Tb_Jig.Size = new System.Drawing.Size(487, 25);
            this.Tb_Jig.TabIndex = 5;
            this.Tb_Jig.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Tb_Jig.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tb_Jig_KeyDown);
            // 
            // Btn_Save
            // 
            this.Btn_Save.BackColor = System.Drawing.Color.Blue;
            this.Btn_Save.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Btn_Save.ForeColor = System.Drawing.Color.White;
            this.Btn_Save.Location = new System.Drawing.Point(12, 512);
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(669, 48);
            this.Btn_Save.TabIndex = 10;
            this.Btn_Save.Text = "SAVE";
            this.Btn_Save.UseVisualStyleBackColor = false;
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // Combo_Location
            // 
            this.Combo_Location.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Combo_Location.FormattingEnabled = true;
            this.Combo_Location.Location = new System.Drawing.Point(599, 534);
            this.Combo_Location.Name = "Combo_Location";
            this.Combo_Location.Size = new System.Drawing.Size(272, 27);
            this.Combo_Location.TabIndex = 11;
            this.Combo_Location.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 112);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(669, 394);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JigCode,
            this.JigName,
            this.LocationFrom,
            this.LocationBunchTo,
            this.LocationTo,
            this.Recipient});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(657, 382);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            // 
            // JigCode
            // 
            this.JigCode.HeaderText = "JigCode";
            this.JigCode.Name = "JigCode";
            this.JigCode.ReadOnly = true;
            // 
            // JigName
            // 
            this.JigName.HeaderText = "JigName";
            this.JigName.Name = "JigName";
            this.JigName.ReadOnly = true;
            // 
            // LocationFrom
            // 
            this.LocationFrom.HeaderText = "LocationFrom";
            this.LocationFrom.Name = "LocationFrom";
            this.LocationFrom.ReadOnly = true;
            // 
            // LocationBunchTo
            // 
            this.LocationBunchTo.HeaderText = "LocationBunchTo";
            this.LocationBunchTo.Name = "LocationBunchTo";
            this.LocationBunchTo.ReadOnly = true;
            // 
            // LocationTo
            // 
            this.LocationTo.HeaderText = "LocationTo";
            this.LocationTo.Name = "LocationTo";
            this.LocationTo.ReadOnly = true;
            // 
            // Recipient
            // 
            this.Recipient.HeaderText = "Recipient";
            this.Recipient.Name = "Recipient";
            this.Recipient.ReadOnly = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.3954F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.6046F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.ComBo_Recipient, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 41);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(669, 32);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(161, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Recipient : ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ComBo_Recipient
            // 
            this.ComBo_Recipient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComBo_Recipient.FormattingEnabled = true;
            this.ComBo_Recipient.Location = new System.Drawing.Point(176, 6);
            this.ComBo_Recipient.Name = "ComBo_Recipient";
            this.ComBo_Recipient.Size = new System.Drawing.Size(487, 23);
            this.ComBo_Recipient.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("굴림", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(161, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "TransLocation : ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.3954F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.6046F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.Tb_Jig, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(12, 74);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(669, 32);
            this.tableLayoutPanel4.TabIndex = 16;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.52553F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.28228F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.10526F));
            this.tableLayoutPanel3.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Combo_LocationGroup, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(12, 6);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(669, 32);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // JigstockOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 569);
            this.Name = "JigstockOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JigstockOut";
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).EndInit();
            this.SkinPager.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Tb_Jig;
        private System.Windows.Forms.ComboBox Combo_LocationGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Save;
        private System.Windows.Forms.ComboBox Combo_Location;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ComBo_Recipient;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn JigCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn JigName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationBunchTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Recipient;
    }
}