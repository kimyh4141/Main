namespace WiseM.Browser
{
    partial class ShipBack
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_PalletList = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_Material = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_Spec = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_MaterialName = new System.Windows.Forms.TextBox();
            this.label_ID = new System.Windows.Forms.Label();
            this.textBox_ShippingHist = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_PalletCode = new System.Windows.Forms.TextBox();
            this.label_3 = new System.Windows.Forms.Label();
            this.textBox_Qty = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.button_ShipBack = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PalletList)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Material, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Spec, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBox_MaterialName, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label_ID, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_ShippingHist, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBox_PalletCode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Search, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView_PalletList, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.label_3, 2, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Qty, 3, 5);
            this.tableLayoutPanel2.Controls.Add(this.button_ShipBack, 3, 6);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 8;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(437, 441);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Pallet Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView_PalletList
            // 
            this.dataGridView_PalletList.AllowUserToAddRows = false;
            this.dataGridView_PalletList.AllowUserToDeleteRows = false;
            this.dataGridView_PalletList.AllowUserToResizeColumns = false;
            this.dataGridView_PalletList.AllowUserToResizeRows = false;
            this.dataGridView_PalletList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_PalletList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_PalletList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_PalletList.Location = new System.Drawing.Point(90, 237);
            this.dataGridView_PalletList.Name = "dataGridView_PalletList";
            this.dataGridView_PalletList.ReadOnly = true;
            this.dataGridView_PalletList.RowHeadersVisible = false;
            this.tableLayoutPanel2.SetRowSpan(this.dataGridView_PalletList, 2);
            this.dataGridView_PalletList.RowTemplate.Height = 23;
            this.dataGridView_PalletList.Size = new System.Drawing.Size(200, 201);
            this.dataGridView_PalletList.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(14, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 21);
            this.label6.TabIndex = 14;
            this.label6.Text = "Material";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Material
            // 
            this.textBox_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Material.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Material, 3);
            this.textBox_Material.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_Material.Location = new System.Drawing.Point(94, 107);
            this.textBox_Material.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_Material.Name = "textBox_Material";
            this.textBox_Material.ReadOnly = true;
            this.textBox_Material.Size = new System.Drawing.Size(336, 29);
            this.textBox_Material.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(38, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 21);
            this.label7.TabIndex = 19;
            this.label7.Text = "Spec";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Spec
            // 
            this.textBox_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Spec.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Spec, 3);
            this.textBox_Spec.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_Spec.Location = new System.Drawing.Point(94, 197);
            this.textBox_Spec.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_Spec.Name = "textBox_Spec";
            this.textBox_Spec.ReadOnly = true;
            this.textBox_Spec.Size = new System.Drawing.Size(336, 29);
            this.textBox_Spec.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(31, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_MaterialName
            // 
            this.textBox_MaterialName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_MaterialName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_MaterialName, 3);
            this.textBox_MaterialName.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_MaterialName.Location = new System.Drawing.Point(94, 152);
            this.textBox_MaterialName.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_MaterialName.Name = "textBox_MaterialName";
            this.textBox_MaterialName.ReadOnly = true;
            this.textBox_MaterialName.Size = new System.Drawing.Size(336, 29);
            this.textBox_MaterialName.TabIndex = 21;
            // 
            // label_ID
            // 
            this.label_ID.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_ID.AutoSize = true;
            this.label_ID.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ID.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label_ID.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_ID.Location = new System.Drawing.Point(59, 66);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(25, 21);
            this.label_ID.TabIndex = 22;
            this.label_ID.Text = "ID";
            this.label_ID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_ShippingHist
            // 
            this.textBox_ShippingHist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_ShippingHist.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_ShippingHist, 2);
            this.textBox_ShippingHist.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_ShippingHist.Location = new System.Drawing.Point(94, 62);
            this.textBox_ShippingHist.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_ShippingHist.Name = "textBox_ShippingHist";
            this.textBox_ShippingHist.ReadOnly = true;
            this.textBox_ShippingHist.Size = new System.Drawing.Size(234, 29);
            this.textBox_ShippingHist.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 21);
            this.label4.TabIndex = 24;
            this.label4.Text = "Pallet List";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_PalletCode
            // 
            this.textBox_PalletCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_PalletCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_PalletCode, 2);
            this.textBox_PalletCode.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.textBox_PalletCode.Location = new System.Drawing.Point(94, 12);
            this.textBox_PalletCode.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_PalletCode.Name = "textBox_PalletCode";
            this.textBox_PalletCode.Size = new System.Drawing.Size(234, 29);
            this.textBox_PalletCode.TabIndex = 25;
            // 
            // label_3
            // 
            this.label_3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_3.AutoSize = true;
            this.label_3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_3.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label_3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_3.Location = new System.Drawing.Point(296, 251);
            this.label_3.Name = "label_3";
            this.label_3.Size = new System.Drawing.Size(36, 21);
            this.label_3.TabIndex = 26;
            this.label_3.Text = "Qty";
            this.label_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Qty
            // 
            this.textBox_Qty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Qty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox_Qty.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_Qty.Location = new System.Drawing.Point(342, 242);
            this.textBox_Qty.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.textBox_Qty.Name = "textBox_Qty";
            this.textBox_Qty.ReadOnly = true;
            this.textBox_Qty.Size = new System.Drawing.Size(88, 39);
            this.textBox_Qty.TabIndex = 27;
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.BackColor = System.Drawing.Color.White;
            this.btn_Search.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(342, 2);
            this.btn_Search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(92, 50);
            this.btn_Search.TabIndex = 10;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // button_ShipBack
            // 
            this.button_ShipBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ShipBack.BackColor = System.Drawing.Color.White;
            this.button_ShipBack.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ShipBack.Location = new System.Drawing.Point(342, 291);
            this.button_ShipBack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_ShipBack.Name = "button_ShipBack";
            this.button_ShipBack.Size = new System.Drawing.Size(92, 148);
            this.button_ShipBack.TabIndex = 28;
            this.button_ShipBack.Text = "Ship Back";
            this.button_ShipBack.UseVisualStyleBackColor = false;
            this.button_ShipBack.Click += new System.EventHandler(this.button_ShipBack_Click);
            // 
            // ShipBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 441);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ShipBack";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ShippingSelectMaterial_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_PalletList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_PalletList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_Material;
        private System.Windows.Forms.TextBox textBox_Spec;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_MaterialName;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.TextBox textBox_ShippingHist;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_PalletCode;
        private System.Windows.Forms.Label label_3;
        private System.Windows.Forms.TextBox textBox_Qty;
        private System.Windows.Forms.Button button_ShipBack;
    }
}