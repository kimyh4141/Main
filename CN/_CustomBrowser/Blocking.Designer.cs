namespace WiseM.Browser
{
    partial class Blocking
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Unblock = new System.Windows.Forms.Button();
            this.btn_Block = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).BeginInit();
            this.SkinPager.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // SkinPager
            // 
            this.SkinPager.Controls.Add(this.dataGridView1);
            this.SkinPager.Controls.Add(this.groupBox1);
            this.SkinPager.Size = new System.Drawing.Size(984, 561);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.btn_Unblock);
            this.groupBox1.Controls.Add(this.btn_Block);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(978, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Blocking";
            // 
            // btn_Unblock
            // 
            this.btn_Unblock.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Unblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Unblock.Location = new System.Drawing.Point(145, 20);
            this.btn_Unblock.Name = "btn_Unblock";
            this.btn_Unblock.Size = new System.Drawing.Size(130, 45);
            this.btn_Unblock.TabIndex = 1;
            this.btn_Unblock.Text = "Reset Blocking";
            this.btn_Unblock.UseVisualStyleBackColor = false;
            this.btn_Unblock.Click += new System.EventHandler(this.btn_Unblock_Click);
            // 
            // btn_Block
            // 
            this.btn_Block.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Block.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Block.Location = new System.Drawing.Point(9, 20);
            this.btn_Block.Name = "btn_Block";
            this.btn_Block.Size = new System.Drawing.Size(130, 45);
            this.btn_Block.TabIndex = 0;
            this.btn_Block.Text = "Set Blocking";
            this.btn_Block.UseVisualStyleBackColor = false;
            this.btn_Block.Click += new System.EventHandler(this.btn_Block_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(978, 476);
            this.dataGridView1.TabIndex = 1;
            // 
            // Blocking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Blocking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blocking";
            this.Load += new System.EventHandler(this.Blocking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).EndInit();
            this.SkinPager.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_Unblock;
        private System.Windows.Forms.Button btn_Block;
    }
}