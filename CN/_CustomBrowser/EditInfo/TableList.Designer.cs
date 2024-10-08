namespace WiseM.Browser.EditInfo
{
    partial class TableList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv_tablelist = new System.Windows.Forms.DataGridView();
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.tb_tablename = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).BeginInit();
            this.SkinPager.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tablelist)).BeginInit();
            this.SuspendLayout();
            // 
            // SkinPager
            // 
            this.SkinPager.Controls.Add(this.btn_search);
            this.SkinPager.Controls.Add(this.tb_tablename);
            this.SkinPager.Controls.Add(this.btn_close);
            this.SkinPager.Controls.Add(this.btn_load);
            this.SkinPager.Controls.Add(this.btn_add);
            this.SkinPager.Controls.Add(this.panel1);
            this.SkinPager.Size = new System.Drawing.Size(247, 482);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgv_tablelist);
            this.panel1.Location = new System.Drawing.Point(5, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 408);
            this.panel1.TabIndex = 0;
            // 
            // dgv_tablelist
            // 
            this.dgv_tablelist.AllowUserToResizeColumns = false;
            this.dgv_tablelist.AllowUserToResizeRows = false;
            this.dgv_tablelist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tablelist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tablelist.Location = new System.Drawing.Point(0, 0);
            this.dgv_tablelist.Name = "dgv_tablelist";
            this.dgv_tablelist.ReadOnly = true;
            this.dgv_tablelist.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dgv_tablelist.RowHeadersVisible = false;
            this.dgv_tablelist.RowTemplate.Height = 27;
            this.dgv_tablelist.Size = new System.Drawing.Size(237, 408);
            this.dgv_tablelist.TabIndex = 0;
            this.dgv_tablelist.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_tablelist_CellClick);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(5, 7);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "추가";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(86, 7);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "조회";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(167, 7);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "닫기";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // tb_tablename
            // 
            this.tb_tablename.Location = new System.Drawing.Point(5, 36);
            this.tb_tablename.Name = "tb_tablename";
            this.tb_tablename.Size = new System.Drawing.Size(156, 25);
            this.tb_tablename.TabIndex = 4;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(166, 35);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 26);
            this.btn_search.TabIndex = 5;
            this.btn_search.Text = "검색";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // TableList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 482);
            this.Name = "TableList";
            this.Text = "TableList";
            ((System.ComponentModel.ISupportInitialize)(this.SkinPager)).EndInit();
            this.SkinPager.ResumeLayout(false);
            this.SkinPager.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tablelist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv_tablelist;
        public System.Windows.Forms.TextBox tb_tablename;
    }
}