namespace PdaServerProgram
{
    partial class FormAndroidUpload
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btninsert = new System.Windows.Forms.Button();
            this.label_CurruntAddress = new System.Windows.Forms.Label();
            this.lb_IP = new System.Windows.Forms.Label();
            this.dataGridView_UploadList = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UploadList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Upload
            // 
            this.btn_Upload.Location = new System.Drawing.Point(373, 265);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(110, 23);
            this.btn_Upload.TabIndex = 0;
            this.btn_Upload.Text = "APK 등록";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(485, 554);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(110, 23);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "종료";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btninsert
            // 
            this.btninsert.Location = new System.Drawing.Point(488, 265);
            this.btninsert.Name = "btninsert";
            this.btninsert.Size = new System.Drawing.Size(110, 23);
            this.btninsert.TabIndex = 2;
            this.btninsert.Text = "APK 적용";
            this.btninsert.UseVisualStyleBackColor = true;
            this.btninsert.Click += new System.EventHandler(this.btninsert_Click);
            // 
            // label_CurruntAddress
            // 
            this.label_CurruntAddress.AutoSize = true;
            this.label_CurruntAddress.Location = new System.Drawing.Point(10, 36);
            this.label_CurruntAddress.Name = "label_CurruntAddress";
            this.label_CurruntAddress.Size = new System.Drawing.Size(109, 12);
            this.label_CurruntAddress.TabIndex = 5;
            this.label_CurruntAddress.Text = "현재 접속 Server : ";
            // 
            // lb_IP
            // 
            this.lb_IP.AllowDrop = true;
            this.lb_IP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_IP.Location = new System.Drawing.Point(130, 36);
            this.lb_IP.Name = "lb_IP";
            this.lb_IP.Size = new System.Drawing.Size(454, 12);
            this.lb_IP.TabIndex = 6;
            // 
            // dataGridView_UploadList
            // 
            this.dataGridView_UploadList.AllowUserToAddRows = false;
            this.dataGridView_UploadList.AllowUserToDeleteRows = false;
            this.dataGridView_UploadList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_UploadList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_UploadList.Location = new System.Drawing.Point(29, 102);
            this.dataGridView_UploadList.Name = "dataGridView_UploadList";
            this.dataGridView_UploadList.RowHeadersVisible = false;
            this.dataGridView_UploadList.RowTemplate.Height = 23;
            this.dataGridView_UploadList.Size = new System.Drawing.Size(583, 143);
            this.dataGridView_UploadList.TabIndex = 13;
            this.dataGridView_UploadList.BindingContextChanged += new System.EventHandler(this.dataGridView_UploadList_BindingContextChanged);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(29, 334);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(556, 143);
            this.dataGridView2.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "서버 데이터 :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "업로드 목록 :";
            // 
            // FormAndroidUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1104, 730);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView_UploadList);
            this.Controls.Add(this.lb_IP);
            this.Controls.Add(this.label_CurruntAddress);
            this.Controls.Add(this.btninsert);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Upload);
            this.Name = "FormAndroidUpload";
            this.ShowIcon = false;
            this.Text = "PdaServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_UploadList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btninsert;
        private System.Windows.Forms.Label label_CurruntAddress;
        private System.Windows.Forms.Label lb_IP;
        private System.Windows.Forms.DataGridView dataGridView_UploadList;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

