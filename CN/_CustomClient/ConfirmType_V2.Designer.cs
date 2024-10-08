namespace WiseM.Client
{
    partial class ConfirmType_V2
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
            this.label2 = new System.Windows.Forms.Label();
            this.rb_Single = new System.Windows.Forms.RadioButton();
            this.rb_double = new System.Windows.Forms.RadioButton();
            this.btn_save = new System.Windows.Forms.Button();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textbox_PalletBcd = new System.Windows.Forms.TextBox();
            this.textbox_BoxBcd = new System.Windows.Forms.TextBox();
            this.btn_palletClear = new System.Windows.Forms.Button();
            this.btn_boxClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(12, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(434, 42);
            this.label2.TabIndex = 1;
            this.label2.Text = "您当前使用的模型类型\r\nThe type of model you are currently working with";
            // 
            // rb_Single
            // 
            this.rb_Single.AutoSize = true;
            this.rb_Single.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_Single.Location = new System.Drawing.Point(14, 72);
            this.rb_Single.Name = "rb_Single";
            this.rb_Single.Size = new System.Drawing.Size(101, 27);
            this.rb_Single.TabIndex = 2;
            this.rb_Single.TabStop = true;
            this.rb_Single.Text = "SINGLE";
            this.rb_Single.UseVisualStyleBackColor = true;
            // 
            // rb_double
            // 
            this.rb_double.AutoSize = true;
            this.rb_double.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_double.Location = new System.Drawing.Point(183, 72);
            this.rb_double.Name = "rb_double";
            this.rb_double.Size = new System.Drawing.Size(107, 27);
            this.rb_double.TabIndex = 3;
            this.rb_double.TabStop = true;
            this.rb_double.Text = "DOUBLE";
            this.rb_double.UseVisualStyleBackColor = true;
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_save.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(560, 22);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(189, 152);
            this.btn_save.TabIndex = 4;
            this.btn_save.Text = "Start";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // dtp_date
            // 
            this.dtp_date.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(14, 166);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(292, 30);
            this.dtp_date.TabIndex = 5;
            this.dtp_date.Value = new System.DateTime(2022, 12, 4, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 46);
            this.label3.TabIndex = 6;
            this.label3.Text = "包装印刷日期\r\nDate of printing on packaging";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(14, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 23);
            this.label1.TabIndex = 7;
            this.label1.Text = "PalletBcd";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(32, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "BoxBcd";
            // 
            // textbox_PalletBcd
            // 
            this.textbox_PalletBcd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_PalletBcd.Location = new System.Drawing.Point(122, 225);
            this.textbox_PalletBcd.Name = "textbox_PalletBcd";
            this.textbox_PalletBcd.Size = new System.Drawing.Size(324, 27);
            this.textbox_PalletBcd.TabIndex = 9;
            this.textbox_PalletBcd.Text = "P0000ABCD1234567800";
            this.textbox_PalletBcd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_PalletBcd_KeyDown);
            // 
            // textbox_BoxBcd
            // 
            this.textbox_BoxBcd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_BoxBcd.Location = new System.Drawing.Point(122, 266);
            this.textbox_BoxBcd.Name = "textbox_BoxBcd";
            this.textbox_BoxBcd.Size = new System.Drawing.Size(324, 27);
            this.textbox_BoxBcd.TabIndex = 10;
            this.textbox_BoxBcd.Text = "B00ABCD123456780000";
            this.textbox_BoxBcd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_BoxBcd_KeyDown);
            // 
            // btn_palletClear
            // 
            this.btn_palletClear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_palletClear.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_palletClear.Location = new System.Drawing.Point(473, 213);
            this.btn_palletClear.Name = "btn_palletClear";
            this.btn_palletClear.Size = new System.Drawing.Size(123, 39);
            this.btn_palletClear.TabIndex = 11;
            this.btn_palletClear.Text = "Clear";
            this.btn_palletClear.UseVisualStyleBackColor = false;
            this.btn_palletClear.Click += new System.EventHandler(this.btn_palletClear_Click);
            // 
            // btn_boxClear
            // 
            this.btn_boxClear.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_boxClear.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_boxClear.Location = new System.Drawing.Point(473, 266);
            this.btn_boxClear.Name = "btn_boxClear";
            this.btn_boxClear.Size = new System.Drawing.Size(123, 41);
            this.btn_boxClear.TabIndex = 12;
            this.btn_boxClear.Text = "Clear";
            this.btn_boxClear.UseVisualStyleBackColor = false;
            this.btn_boxClear.Click += new System.EventHandler(this.btn_boxClear_Click);
            // 
            // ConfirmType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(761, 319);
            this.Controls.Add(this.btn_boxClear);
            this.Controls.Add(this.btn_palletClear);
            this.Controls.Add(this.textbox_BoxBcd);
            this.Controls.Add(this.textbox_PalletBcd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.rb_double);
            this.Controls.Add(this.rb_Single);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmType";
            this.Text = "Select Type";
            this.Load += new System.EventHandler(this.ConfirmType_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rb_Single;
        private System.Windows.Forms.RadioButton rb_double;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textbox_PalletBcd;
        private System.Windows.Forms.TextBox textbox_BoxBcd;
        private System.Windows.Forms.Button btn_palletClear;
        private System.Windows.Forms.Button btn_boxClear;
    }
}