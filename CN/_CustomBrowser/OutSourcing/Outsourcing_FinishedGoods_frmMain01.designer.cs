namespace WiseM.Browser
{
    partial class Outsourcing_FinishedGoods_frmMain01
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.txtPcbQty = new System.Windows.Forms.TextBox();
            this.btnSelPcbExcel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv02 = new System.Windows.Forms.DataGridView();
            this.btnMgmt = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv02)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1120, 840);
            this.panel1.TabIndex = 52;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel10);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(783, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(337, 840);
            this.panel4.TabIndex = 3;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btnMgmt);
            this.panel10.Controls.Add(this.txtPcbQty);
            this.panel10.Controls.Add(this.btnSelPcbExcel);
            this.panel10.Controls.Add(this.label4);
            this.panel10.Controls.Add(this.btnSave);
            this.panel10.Controls.Add(this.btnQuit);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(337, 840);
            this.panel10.TabIndex = 1;
            // 
            // txtPcbQty
            // 
            this.txtPcbQty.Enabled = false;
            this.txtPcbQty.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPcbQty.Location = new System.Drawing.Point(42, 132);
            this.txtPcbQty.Name = "txtPcbQty";
            this.txtPcbQty.Size = new System.Drawing.Size(254, 46);
            this.txtPcbQty.TabIndex = 45;
            this.txtPcbQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSelPcbExcel
            // 
            this.btnSelPcbExcel.BackColor = System.Drawing.Color.Yellow;
            this.btnSelPcbExcel.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSelPcbExcel.Location = new System.Drawing.Point(41, 50);
            this.btnSelPcbExcel.Name = "btnSelPcbExcel";
            this.btnSelPcbExcel.Size = new System.Drawing.Size(255, 48);
            this.btnSelPcbExcel.TabIndex = 44;
            this.btnSelPcbExcel.Text = "Select PCB Excel File";
            this.btnSelPcbExcel.UseVisualStyleBackColor = false;
            this.btnSelPcbExcel.Click += new System.EventHandler(this.btnSelPcbExcel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 19);
            this.label4.TabIndex = 44;
            this.label4.Text = "PCB Qty";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSave.Location = new System.Drawing.Point(42, 215);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(254, 48);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnQuit.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnQuit.Location = new System.Drawing.Point(42, 287);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(254, 48);
            this.btnQuit.TabIndex = 46;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 840);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(783, 840);
            this.panel3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv02);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox2.Size = new System.Drawing.Size(783, 840);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "< PCB Info. >";
            // 
            // dgv02
            // 
            this.dgv02.AllowUserToAddRows = false;
            this.dgv02.AllowUserToDeleteRows = false;
            this.dgv02.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv02.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv02.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv02.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv02.BackgroundColor = System.Drawing.Color.White;
            this.dgv02.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv02.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgv02.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv02.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv02.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv02.Location = new System.Drawing.Point(5, 25);
            this.dgv02.Name = "dgv02";
            this.dgv02.ReadOnly = true;
            this.dgv02.RowHeadersVisible = false;
            this.dgv02.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv02.Size = new System.Drawing.Size(773, 810);
            this.dgv02.TabIndex = 15;
            // 
            // btnMgmt
            // 
            this.btnMgmt.BackColor = System.Drawing.Color.LightBlue;
            this.btnMgmt.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnMgmt.Location = new System.Drawing.Point(42, 451);
            this.btnMgmt.Name = "btnMgmt";
            this.btnMgmt.Size = new System.Drawing.Size(254, 48);
            this.btnMgmt.TabIndex = 48;
            this.btnMgmt.Text = "Mgmt. of already processed data";
            this.btnMgmt.UseVisualStyleBackColor = false;
            this.btnMgmt.Click += new System.EventHandler(this.btnMgmt_Click);
            // 
            // Outsourcing_FinishedGoods_frmMain01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 840);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Outsourcing_FinishedGoods_frmMain01";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Outsourcing FinishedGoods Receipt Input";
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv02)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv02;
        private System.Windows.Forms.Button btnSelPcbExcel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.TextBox txtPcbQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMgmt;
    }
}