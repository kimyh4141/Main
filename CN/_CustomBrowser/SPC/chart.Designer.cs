namespace WiseM.Browser
{
    partial class chart
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
            this.shanuCPCPKChart = new ShanuCPCPKChart.ShanuCPCPKChart();
            this.SuspendLayout();
            // 
            // shanuCPCPKChart
            // 
            this.shanuCPCPKChart._MaxFlag = false;
            this.shanuCPCPKChart._MinFlag = false;
            this.shanuCPCPKChart.AutoScroll = true;
            this.shanuCPCPKChart.CokPpkValue = 1.33D;
            this.shanuCPCPKChart.cpcpkChartWaterMarkText = "Shanu SPC CHART";
            this.shanuCPCPKChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shanuCPCPKChart.Location = new System.Drawing.Point(0, 0);
            this.shanuCPCPKChart.LSL = 0D;
            this.shanuCPCPKChart.Name = "shanuCPCPKChart";
            this.shanuCPCPKChart.Size = new System.Drawing.Size(1639, 670);
            this.shanuCPCPKChart.TabIndex = 5;
            this.shanuCPCPKChart.USL = 0D;
            // 
            // chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1639, 670);
            this.Controls.Add(this.shanuCPCPKChart);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "chart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chart";
            this.ResumeLayout(false);

        }

        #endregion

        private ShanuCPCPKChart.ShanuCPCPKChart shanuCPCPKChart;




    }
}