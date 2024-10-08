namespace WiseM.Browser.Rework
{
    partial class BadEdit
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
            this.groupBox0 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_MaterialName = new System.Windows.Forms.TextBox();
            this.textBox_Material = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Routing = new System.Windows.Forms.ComboBox();
            this.comboBox_BadGroup = new System.Windows.Forms.ComboBox();
            this.comboBox_WorkCenter = new System.Windows.Forms.ComboBox();
            this.comboBox_Bad = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_WorkOrder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Barcode = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_Save = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.groupBox0.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox0
            // 
            this.groupBox0.BackColor = System.Drawing.Color.White;
            this.groupBox0.Controls.Add(this.tableLayoutPanel1);
            this.groupBox0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox0.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox0.ForeColor = System.Drawing.Color.Fuchsia;
            this.groupBox0.Location = new System.Drawing.Point(0, 0);
            this.groupBox0.Name = "groupBox0";
            this.groupBox0.Size = new System.Drawing.Size(981, 340);
            this.groupBox0.TabIndex = 13;
            this.groupBox0.TabStop = false;
            this.groupBox0.Text = "不良信息(Bad Information)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_MaterialName, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Material, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Routing, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_BadGroup, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_WorkCenter, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_Bad, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_WorkOrder, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Barcode, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(975, 319);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(3, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(3);
            this.label7.Size = new System.Drawing.Size(304, 30);
            this.label7.TabIndex = 28;
            this.label7.Text = "品种(Material)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_MaterialName
            // 
            this.textBox_MaterialName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_MaterialName.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MaterialName.Location = new System.Drawing.Point(645, 75);
            this.textBox_MaterialName.Name = "textBox_MaterialName";
            this.textBox_MaterialName.ReadOnly = true;
            this.textBox_MaterialName.Size = new System.Drawing.Size(327, 30);
            this.textBox_MaterialName.TabIndex = 30;
            // 
            // textBox_Material
            // 
            this.textBox_Material.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Material.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Material.Location = new System.Drawing.Point(313, 75);
            this.textBox_Material.Name = "textBox_Material";
            this.textBox_Material.ReadOnly = true;
            this.textBox_Material.Size = new System.Drawing.Size(326, 30);
            this.textBox_Material.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 111);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(304, 31);
            this.label2.TabIndex = 34;
            this.label2.Text = "工序/生产线(Routing/Line)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(304, 31);
            this.label4.TabIndex = 8;
            this.label4.Text = "不良组/代码(Bad Group/Code)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox_Routing
            // 
            this.comboBox_Routing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Routing.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Routing.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Routing.FormattingEnabled = true;
            this.comboBox_Routing.Location = new System.Drawing.Point(313, 111);
            this.comboBox_Routing.Name = "comboBox_Routing";
            this.comboBox_Routing.Size = new System.Drawing.Size(326, 31);
            this.comboBox_Routing.TabIndex = 15;
            this.comboBox_Routing.SelectedIndexChanged += new System.EventHandler(this.comboBox_Routing_SelectedIndexChanged);
            // 
            // comboBox_BadGroup
            // 
            this.comboBox_BadGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_BadGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_BadGroup.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_BadGroup.FormattingEnabled = true;
            this.comboBox_BadGroup.Location = new System.Drawing.Point(313, 148);
            this.comboBox_BadGroup.Name = "comboBox_BadGroup";
            this.comboBox_BadGroup.Size = new System.Drawing.Size(326, 31);
            this.comboBox_BadGroup.TabIndex = 31;
            this.comboBox_BadGroup.SelectedIndexChanged += new System.EventHandler(this.comboBox_BadGroup_SelectedIndexChanged);
            // 
            // comboBox_WorkCenter
            // 
            this.comboBox_WorkCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_WorkCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_WorkCenter.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_WorkCenter.FormattingEnabled = true;
            this.comboBox_WorkCenter.Location = new System.Drawing.Point(645, 111);
            this.comboBox_WorkCenter.Name = "comboBox_WorkCenter";
            this.comboBox_WorkCenter.Size = new System.Drawing.Size(327, 31);
            this.comboBox_WorkCenter.TabIndex = 17;
            // 
            // comboBox_Bad
            // 
            this.comboBox_Bad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox_Bad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Bad.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Bad.FormattingEnabled = true;
            this.comboBox_Bad.Location = new System.Drawing.Point(645, 148);
            this.comboBox_Bad.Name = "comboBox_Bad";
            this.comboBox_Bad.Size = new System.Drawing.Size(327, 31);
            this.comboBox_Bad.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(304, 30);
            this.label3.TabIndex = 41;
            this.label3.Text = "作业指示编号(WorkOrder)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_WorkOrder
            // 
            this.textBox_WorkOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_WorkOrder.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_WorkOrder.Location = new System.Drawing.Point(313, 39);
            this.textBox_WorkOrder.Name = "textBox_WorkOrder";
            this.textBox_WorkOrder.ReadOnly = true;
            this.textBox_WorkOrder.Size = new System.Drawing.Size(326, 30);
            this.textBox_WorkOrder.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(304, 30);
            this.label5.TabIndex = 47;
            this.label5.Text = "PCB 条码(PCB Barcode)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_Barcode
            // 
            this.textBox_Barcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_Barcode.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Barcode.Location = new System.Drawing.Point(313, 3);
            this.textBox_Barcode.Name = "textBox_Barcode";
            this.textBox_Barcode.ReadOnly = true;
            this.textBox_Barcode.Size = new System.Drawing.Size(326, 30);
            this.textBox_Barcode.TabIndex = 48;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.button_Save);
            this.flowLayoutPanel1.Controls.Add(this.button_Delete);
            this.flowLayoutPanel1.Controls.Add(this.btn_Cancel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(504, 240);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(468, 76);
            this.flowLayoutPanel1.TabIndex = 50;
            // 
            // button_Save
            // 
            this.button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Save.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Save.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Save.Location = new System.Drawing.Point(3, 3);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(150, 70);
            this.button_Save.TabIndex = 10;
            this.button_Save.Text = "储存\r\n(Save)";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Delete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Delete.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_Delete.Location = new System.Drawing.Point(159, 3);
            this.button_Delete.Name = "button_Delete";
            this.button_Delete.Size = new System.Drawing.Size(150, 70);
            this.button_Delete.TabIndex = 50;
            this.button_Delete.Text = "去掉\r\n(Delete)";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.button_Delete_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Cancel.Location = new System.Drawing.Point(315, 3);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(150, 70);
            this.btn_Cancel.TabIndex = 49;
            this.btn_Cancel.Text = "取消\r\n(Cancel)";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // BadEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 340);
            this.Controls.Add(this.groupBox0);
            this.Name = "BadEdit";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BadEdit";
            this.Load += new System.EventHandler(this.BadEdit_Load);
            this.groupBox0.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox0;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_MaterialName;
        private System.Windows.Forms.TextBox textBox_Material;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Routing;
        private System.Windows.Forms.ComboBox comboBox_BadGroup;
        private System.Windows.Forms.ComboBox comboBox_WorkCenter;
        private System.Windows.Forms.ComboBox comboBox_Bad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_WorkOrder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_Barcode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button btn_Cancel;
    }
}