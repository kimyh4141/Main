namespace WiseM.Browser.WMS
{
    partial class BarcodeGeneratorTestPrint
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
            this.button_PrintBox = new System.Windows.Forms.Button();
            this.button_PrintReel = new System.Windows.Forms.Button();
            this.button_PrintMSL = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.button_PrintBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_PrintReel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_PrintMSL, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(302, 113);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_PrintBox
            // 
            this.button_PrintBox.BackColor = System.Drawing.Color.White;
            this.button_PrintBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PrintBox.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_PrintBox.Location = new System.Drawing.Point(3, 3);
            this.button_PrintBox.Name = "button_PrintBox";
            this.button_PrintBox.Size = new System.Drawing.Size(94, 107);
            this.button_PrintBox.TabIndex = 0;
            this.button_PrintBox.Text = "BOX";
            this.button_PrintBox.UseVisualStyleBackColor = false;
            this.button_PrintBox.Click += new System.EventHandler(this.button_PrintBox_Click);
            // 
            // button_PrintReel
            // 
            this.button_PrintReel.BackColor = System.Drawing.Color.White;
            this.button_PrintReel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PrintReel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.button_PrintReel.Location = new System.Drawing.Point(103, 3);
            this.button_PrintReel.Name = "button_PrintReel";
            this.button_PrintReel.Size = new System.Drawing.Size(94, 107);
            this.button_PrintReel.TabIndex = 1;
            this.button_PrintReel.Text = "REEL";
            this.button_PrintReel.UseVisualStyleBackColor = false;
            this.button_PrintReel.Click += new System.EventHandler(this.button_PrintReel_Click);
            // 
            // button_PrintMSL
            // 
            this.button_PrintMSL.BackColor = System.Drawing.Color.White;
            this.button_PrintMSL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PrintMSL.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.button_PrintMSL.Location = new System.Drawing.Point(203, 3);
            this.button_PrintMSL.Name = "button_PrintMSL";
            this.button_PrintMSL.Size = new System.Drawing.Size(96, 107);
            this.button_PrintMSL.TabIndex = 2;
            this.button_PrintMSL.Text = "MSL\r\n";
            this.button_PrintMSL.UseVisualStyleBackColor = false;
            this.button_PrintMSL.Click += new System.EventHandler(this.button_PrintMSL_Click);
            // 
            // NewBarcodeTestPrintcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(302, 113);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NewBarcodeTestPrintcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Test Print";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_PrintBox;
        private System.Windows.Forms.Button button_PrintReel;
        private System.Windows.Forms.Button button_PrintMSL;
    }
}