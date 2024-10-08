using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser
{
    public partial class FeederInfoChangeFeeder : Form
    {
        private readonly string _workCenter;
        private readonly string _material;

        public FeederInfoChangeFeeder(string workCenter, string material)
        {
            _workCenter = workCenter;
            _material = material;
            InitializeComponent();
        }

        private void FeederInfoChangeFeeder_Load(object sender, EventArgs e)
        {
            label_WorkCenterValue.Text = _workCenter;
            label_MaterialValue.Text = _material;
            var query = new StringBuilder();
            query.AppendLine(
                $@"
                SELECT FI.Feeder
                     , COUNT(*) AS Count
                  FROM FeederInfo AS FI
                 WHERE FI.WorkCenter = '{_workCenter}'
                   AND FI.Material = '{_material}'
                 GROUP BY
                     FI.Feeder
                ;
                "
            );
            dataGridView_Feeder.DataSource = DbAccess.Default.GetDataTable(query.ToString());
        }

        private void dataGridView_Feeder_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            if (dataGridView.CurrentRow != null) textBox_OldFeeder.Text = dataGridView.CurrentRow.Cells["Feeder"].Value.ToString();
            textBox_NewFeeder.Text = string.Empty;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_OldFeeder.Text))
            {
                MessageBox.Show("Please Select Feeder.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(textBox_NewFeeder.Text))
            {
                MessageBox.Show("Please enter New Feeder.", "Not Select", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (0 < DbAccess.Default.IsExist("FeederInfo", $@"WorkCenter = '{_workCenter}' AND Material = '{_material}' AND Feeder = '{textBox_NewFeeder.Text}' "))
            {
                MessageBox.Show("The feeder number already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.Yes != System.Windows.Forms.MessageBox.Show($"Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk)) return;
            var query = new StringBuilder();
            query.AppendLine(
                $@"
                DECLARE @WorkCenter NVARCHAR(50) = '{_workCenter}'
                DECLARE @Material   NVARCHAR(50) = '{_material}'
                DECLARE @OldFeeder  NVARCHAR(10) = '{textBox_OldFeeder.Text}'
                DECLARE @NewFeeder  NVARCHAR(10) = '{textBox_NewFeeder.Text}'
                BEGIN TRY
                    BEGIN TRAN;
                    UPDATE FeederInfo
                       SET Feeder = @NewFeeder
                     WHERE WorkCenter = @WorkCenter
                       AND Material = @Material
                       AND Feeder = @OldFeeder;
                    UPDATE FeederWip
                       SET Feeder    = @NewFeeder
                         , RemainQty = 0
                         , Barcode   = NULL
                     WHERE WorkCenter = @WorkCenter
                       AND Material = @Material
                       AND Feeder = @OldFeeder;
                    UPDATE MountingInputHist
                       SET Status     = 0
                         , IsPrevious = 0
                     WHERE WorkCenter = @WorkCenter
                       AND Feeder = @OldFeeder
                       AND Status = 1;
                    COMMIT TRAN;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                END CATCH
                ;
                "
            );
            try
            {
                DbAccess.Default.ExecuteQuery(query.ToString());
                WiseM.MessageBox.Show("Saved Completed.", "Information", MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{ex.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void button_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
