namespace WiseM.Browser.WMS
{
    partial class RawMaterialBlocking
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.wiseDataGridView_List = new WiseM.Forms.WiseDataGridView();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Search = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.textBox_Spec = new System.Windows.Forms.TextBox();
            this.textBox_Material = new System.Windows.Forms.TextBox();
            this.textBox_Barcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_DateText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wiseDataGridView_List)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.wiseDataGridView_List, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_Save, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_Search, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // wiseDataGridView_List
            // 
            this.wiseDataGridView_List.AllowUserToAddRows = false;
            this.wiseDataGridView_List.AllowUserToDeleteRows = false;
            this.wiseDataGridView_List.AllowUserToOrderColumns = true;
            this.wiseDataGridView_List.AllowUserToResizeRows = false;
            this.wiseDataGridView_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.wiseDataGridView_List.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.wiseDataGridView_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.wiseDataGridView_List, 2);
            this.wiseDataGridView_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wiseDataGridView_List.IsPivotGrid = false;
            this.wiseDataGridView_List.Location = new System.Drawing.Point(4, 172);
            this.wiseDataGridView_List.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.wiseDataGridView_List.Name = "wiseDataGridView_List";
            this.wiseDataGridView_List.ReadOnly = true;
            this.wiseDataGridView_List.RowHeadersVisible = false;
            this.wiseDataGridView_List.RowTemplate.Height = 23;
            this.wiseDataGridView_List.Size = new System.Drawing.Size(936, 423);
            this.wiseDataGridView_List.TabIndex = 5;
            this.wiseDataGridView_List.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.wiseDataGridView_List_CellClick);
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold);
            this.button_Save.Location = new System.Drawing.Point(784, 603);
            this.button_Save.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(156, 75);
            this.button_Save.TabIndex = 7;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Search
            // 
            this.button_Search.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold);
            this.button_Search.Location = new System.Drawing.Point(784, 3);
            this.button_Search.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button_Search.Name = "button_Search";
            this.button_Search.Size = new System.Drawing.Size(156, 161);
            this.button_Search.TabIndex = 6;
            this.button_Search.Text = "Search";
            this.button_Search.UseVisualStyleBackColor = true;
            this.button_Search.Click += new System.EventHandler(this.button_Search_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker_From, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dateTimePicker_To, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Spec, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Material, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBox_Barcode, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label_DateText, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 157);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // dateTimePicker_From
            // 
            this.dateTimePicker_From.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_From.CalendarFont = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_From.Checked = false;
            this.dateTimePicker_From.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_From.Location = new System.Drawing.Point(126, 6);
            this.dateTimePicker_From.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker_From.Name = "dateTimePicker_From";
            this.dateTimePicker_From.Size = new System.Drawing.Size(318, 27);
            this.dateTimePicker_From.TabIndex = 23;
            // 
            // dateTimePicker_To
            // 
            this.dateTimePicker_To.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker_To.CalendarFont = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker_To.Checked = false;
            this.dateTimePicker_To.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker_To.Location = new System.Drawing.Point(450, 6);
            this.dateTimePicker_To.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker_To.Name = "dateTimePicker_To";
            this.dateTimePicker_To.Size = new System.Drawing.Size(319, 27);
            this.dateTimePicker_To.TabIndex = 24;
            // 
            // textBox_Spec
            // 
            this.textBox_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Spec, 2);
            this.textBox_Spec.Location = new System.Drawing.Point(127, 123);
            this.textBox_Spec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Spec.Name = "textBox_Spec";
            this.textBox_Spec.Size = new System.Drawing.Size(641, 27);
            this.textBox_Spec.TabIndex = 22;
            // 
            // textBox_Material
            // 
            this.textBox_Material.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Material, 2);
            this.textBox_Material.Location = new System.Drawing.Point(127, 84);
            this.textBox_Material.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Material.Name = "textBox_Material";
            this.textBox_Material.Size = new System.Drawing.Size(641, 27);
            this.textBox_Material.TabIndex = 21;
            // 
            // textBox_Barcode
            // 
            this.textBox_Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.textBox_Barcode, 2);
            this.textBox_Barcode.Location = new System.Drawing.Point(127, 45);
            this.textBox_Barcode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Barcode.Name = "textBox_Barcode";
            this.textBox_Barcode.Size = new System.Drawing.Size(641, 27);
            this.textBox_Barcode.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(73, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 18;
            this.label3.Text = "Spec";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_DateText
            // 
            this.label_DateText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_DateText.AutoSize = true;
            this.label_DateText.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_DateText.Location = new System.Drawing.Point(75, 9);
            this.label_DateText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_DateText.Name = "label_DateText";
            this.label_DateText.Size = new System.Drawing.Size(44, 21);
            this.label_DateText.TabIndex = 15;
            this.label_DateText.Text = "Date";
            this.label_DateText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(4, 87);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 16;
            this.label1.Text = "Material Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(49, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "Barcode";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // RawMaterialBlocking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(944, 681);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "RawMaterialBlocking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RawMaterialBlocking";
            this.Load += new System.EventHandler(this.RawMaterialBlocking_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wiseDataGridView_List)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Forms.WiseDataGridView wiseDataGridView_List;
        private System.Windows.Forms.Label label_DateText;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Search;
        private System.Windows.Forms.TextBox textBox_Spec;
        private System.Windows.Forms.TextBox textBox_Material;
        private System.Windows.Forms.TextBox textBox_Barcode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_From;
        private System.Windows.Forms.DateTimePicker dateTimePicker_To;
    }
}