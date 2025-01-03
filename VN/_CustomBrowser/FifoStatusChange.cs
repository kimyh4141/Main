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
    public partial class FifoStatusChange : Form
    {
        private readonly DataGridViewRow _currentRow;

        public FifoStatusChange(DataGridViewRow dataGridViewRow)
        {
            InitializeComponent();
            _currentRow = dataGridViewRow;
            textBox_RawMaterial.Text = _currentRow.Cells["RawMaterial"].Value.ToString();
            textBox_Text.Text = _currentRow.Cells["Text"].Value.ToString();
            textBox_Spec.Text = _currentRow.Cells["Spec"].Value.ToString();
        }

        private bool ProcessSave()
        {
            try
            {
                string query = $@"
                            UPDATE RawMaterial
                               SET FifoStatus = '{comboBox_FifoStatus.SelectedValue}'
                            OUTPUT inserted.RawMaterial
                                 , inserted.FifoStatus
                                 , '{WiseApp.Id}'
                              INTO RawMaterialFifoChangeHist (RawMaterial, FifoStatus, updater)
                             WHERE RawMaterial = '{_currentRow.Cells["RawMaterial"].Value}'
                            ;";
                DbAccess.Default.ExecuteQuery(query);
                System.Windows.Forms.MessageBox.Show($@"Đăng ký thành công。(Registration Successful.)", "Đăng ký thành công。(Registration Successful.)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu。(Database error.)\r\n{ex.Message}", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void FifoStatusChange_Load(object sender, EventArgs e)
        {
            string query = $@"
                            SELECT Common
                                 , CONCAT_WS(' / ', Common, TextVie) AS Text
                              FROM Common
                             WHERE Category = '297'
                               AND Status = 1
                             ORDER BY ViewSeq
                            ;";

            var dataTable = DbAccess.Default.GetDataTable(query);
            if (dataTable == null || dataTable.Rows.Count < 1) return;
            comboBox_FifoStatus.DataSource = dataTable;
            comboBox_FifoStatus.DisplayMember = "Text";
            comboBox_FifoStatus.ValueMember = "Common";
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Bạn có chắc chắn không？ \r\nAre you sure?", "Đóng những hộp còn lại(Box Remainder Closing)", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (ProcessSave())
            {
            }
        }
    }
}