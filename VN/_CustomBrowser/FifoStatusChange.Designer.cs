namespace WiseM.Browser
{
    partial class FifoStatusChange
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
            this.label_Text = new System.Windows.Forms.Label();
            this.comboBox_FifoStatus = new System.Windows.Forms.ComboBox();
            this.button_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_RawMaterial = new System.Windows.Forms.Label();
            this.label_Spec = new System.Windows.Forms.Label();
            this.textBox_Spec = new System.Windows.Forms.TextBox();
            this.textBox_Text = new System.Windows.Forms.TextBox();
            this.textBox_RawMaterial = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Text
            // 
            this.label_Text.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Text.AutoSize = true;
            this.label_Text.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.label_Text.Location = new System.Drawing.Point(61, 52);
            this.label_Text.Name = "label_Text";
            this.label_Text.Size = new System.Drawing.Size(62, 21);
            this.label_Text.TabIndex = 0;
            this.label_Text.Text = "Name";
            this.label_Text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBox_FifoStatus
            // 
            this.comboBox_FifoStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.comboBox_FifoStatus, 2);
            this.comboBox_FifoStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_FifoStatus.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.comboBox_FifoStatus.FormattingEnabled = true;
            this.comboBox_FifoStatus.Location = new System.Drawing.Point(129, 132);
            this.comboBox_FifoStatus.Name = "comboBox_FifoStatus";
            this.comboBox_FifoStatus.Size = new System.Drawing.Size(332, 29);
            this.comboBox_FifoStatus.TabIndex = 1;
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_Save.Location = new System.Drawing.Point(361, 228);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(100, 50);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "Save";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label_Spec, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_FifoStatus, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_Text, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_Cancel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_Save, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_RawMaterial, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Spec, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Text, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_RawMaterial, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(464, 281);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(52, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 42);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fifo\r\nStatus";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_Cancel.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.button_Cancel.Location = new System.Drawing.Point(3, 228);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(100, 50);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // label_RawMaterial
            // 
            this.label_RawMaterial.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_RawMaterial.AutoSize = true;
            this.label_RawMaterial.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.label_RawMaterial.Location = new System.Drawing.Point(3, 10);
            this.label_RawMaterial.Name = "label_RawMaterial";
            this.label_RawMaterial.Size = new System.Drawing.Size(120, 21);
            this.label_RawMaterial.TabIndex = 4;
            this.label_RawMaterial.Text = "RawMaterial";
            // 
            // label_Spec
            // 
            this.label_Spec.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label_Spec.AutoSize = true;
            this.label_Spec.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold);
            this.label_Spec.Location = new System.Drawing.Point(63, 94);
            this.label_Spec.Name = "label_Spec";
            this.label_Spec.Size = new System.Drawing.Size(60, 21);
            this.label_Spec.TabIndex = 7;
            this.label_Spec.Text = "Spec";
            this.label_Spec.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textBox_Spec
            // 
            this.textBox_Spec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_Spec, 2);
            this.textBox_Spec.Font = new System.Drawing.Font("굴림", 15.75F);
            this.textBox_Spec.Location = new System.Drawing.Point(130, 89);
            this.textBox_Spec.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Spec.Name = "textBox_Spec";
            this.textBox_Spec.ReadOnly = true;
            this.textBox_Spec.Size = new System.Drawing.Size(330, 32);
            this.textBox_Spec.TabIndex = 23;
            // 
            // textBox_Text
            // 
            this.textBox_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_Text, 2);
            this.textBox_Text.Font = new System.Drawing.Font("굴림", 15.75F);
            this.textBox_Text.Location = new System.Drawing.Point(130, 47);
            this.textBox_Text.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_Text.Name = "textBox_Text";
            this.textBox_Text.ReadOnly = true;
            this.textBox_Text.Size = new System.Drawing.Size(330, 32);
            this.textBox_Text.TabIndex = 24;
            // 
            // textBox_RawMaterial
            // 
            this.textBox_RawMaterial.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.textBox_RawMaterial, 2);
            this.textBox_RawMaterial.Font = new System.Drawing.Font("굴림", 15.75F);
            this.textBox_RawMaterial.Location = new System.Drawing.Point(130, 5);
            this.textBox_RawMaterial.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_RawMaterial.Name = "textBox_RawMaterial";
            this.textBox_RawMaterial.ReadOnly = true;
            this.textBox_RawMaterial.Size = new System.Drawing.Size(330, 32);
            this.textBox_RawMaterial.TabIndex = 25;
            // 
            // FifoStatusChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FifoStatusChange";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FifoStatusChange_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Text;
        private System.Windows.Forms.ComboBox comboBox_FifoStatus;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_RawMaterial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_Spec;
        private System.Windows.Forms.TextBox textBox_Spec;
        private System.Windows.Forms.TextBox textBox_Text;
        private System.Windows.Forms.TextBox textBox_RawMaterial;
    }
}