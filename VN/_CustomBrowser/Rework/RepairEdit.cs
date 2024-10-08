using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class RepairEdit : SkinForm
    {
        #region Field

        private readonly CustomPanelLinkEventArgs _customPanelLinkEventArgs;

        private DataSet _dataSet;

        /// <summary>
        /// 스캔된 바코드의 최신행 키
        /// </summary>
        private string _repairHist;

        #endregion

        #region Constructor

        public RepairEdit(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();

            _customPanelLinkEventArgs = e;
        }

        #endregion

        #region Method

        private void GetInfo()
        {
            var query = string.Empty;
            query =
                $@"
                SELECT WT.WorkTeam
                     , CONCAT(WT.WorkTeam, ' / ', WT.Text) AS Text
                  FROM WorkTeam WT
                 WHERE WT.Status = 1
                 ORDER BY WT.Bunch
                ;

                SELECT Worker
                     , CONCAT(Worker, '/', Text) AS Text
                     , Bunch
                  FROM Worker
                 WHERE Status = 1
                 ORDER BY Worker
                ;

                SELECT C.Common
                     , CONCAT(C.Common, '/', C.TextVie) AS Text
                  FROM Common C
                 WHERE C.Category = '200'
                 ORDER BY C.Common
                ;

                SELECT B.Bad
                     , CONCAT(B.Bad, '/', B.Text) AS Text
                     , B.Bunch
                  FROM Bad B
                 WHERE B.Status = 1
                 ORDER BY B.Bad
                ;

                SELECT C.Common
                     , CONCAT(C.Common, '/', C.TextVie) AS Text
                  FROM Common C
                 WHERE C.Category = '200'
                 ORDER BY C.Common
                ;

                SELECT B.Bad
                     , CONCAT(B.Bad, '/', B.Text) AS Text
                     , B.Bunch
                  FROM Bad B
                 WHERE B.Status = 1
                 ORDER BY B.Bad
                ;

                SELECT Common
                     , CONCAT_WS(' / ', Common, TextVie) AS Text
                  FROM Common
                 WHERE Category = '180'
                   AND COALESCE(Common, '') != ''
                   AND COALESCE(TextVie, '') != ''
                 ORDER BY ViewSeq
                ;";

            _dataSet = DbAccess.Default.GetDataSet(query);

            if (_dataSet == null || _dataSet.Tables.Count != 7) return;
            _dataSet.Tables[0].TableName = "Dept";
            _dataSet.Tables[1].TableName = "Worker";
            _dataSet.Tables[2].TableName = "BadGroup";
            _dataSet.Tables[3].TableName = "Bad";
            _dataSet.Tables[4].TableName = "RepairGroup";
            _dataSet.Tables[5].TableName = "Repair";
            _dataSet.Tables[6].TableName = "PartRouting";
        }

        private void BindCombo_RepairResult()
        {
            var dt = new DataTable();
            dt.Columns.Add("DisplayMember");
            dt.Columns.Add("ValueMember");

            dt.Rows.Add("", "");
            dt.Rows.Add("C: Hoàn tất(Complete)", "C");
            dt.Rows.Add("X: Phế(Scrap)", "X");

            cb_RepairResult.DataSource = dt;
            cb_RepairResult.DisplayMember = "DisplayMember";
            cb_RepairResult.ValueMember = "ValueMember";
        }


        private void BindCombo_RepairGroup()
        {
            cb_RepairGroup.DataSource = null;

            if (_dataSet == null || !_dataSet.Tables.Contains("RepairGroup")) return;
            var dtSource = _dataSet.Tables["RepairGroup"];

            dtSource.Rows.InsertAt(dtSource.NewRow(), 0);

            cb_RepairGroup.DataSource = dtSource;
            cb_RepairGroup.DisplayMember = "Text";
            cb_RepairGroup.ValueMember = "Common";
        }

        private void BindCombo_PartRouting()
        {
            comboBox_PartRouting.DataSource = null;

            if (_dataSet == null || !_dataSet.Tables.Contains("PartRouting")) return;
            var dtSource = _dataSet.Tables["PartRouting"];

            dtSource.Rows.InsertAt(dtSource.NewRow(), 0);

            comboBox_PartRouting.DataSource = dtSource;
            comboBox_PartRouting.DisplayMember = "Text";
            comboBox_PartRouting.ValueMember = "Common";
        }

        private void BindCombo_Repair()
        {
            cb_Repair.DataSource = null;

            var repairGroup = Convert.ToString(cb_RepairGroup.SelectedValue);

            if (string.IsNullOrEmpty(repairGroup)) return;
            if (_dataSet == null || !_dataSet.Tables.Contains("Repair")) return;
            var temp = _dataSet.Tables["Repair"].Select($"Bunch = '{repairGroup}'");

            if (temp.Length <= 0)
                temp = null;

            var dtSource = temp?.CopyToDataTable();

            cb_Repair.DataSource = dtSource;
            cb_Repair.DisplayMember = "Text";
            cb_Repair.ValueMember = "Bad";
        }


        private bool VerifyBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Vui lòng nhập Barcode。(Please input barcode.)", "Barcode trống(Empty Barcode)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            try
            {
                string Q = 
                    $@"
                    SELECT TOP 1 RepairHist
                               , PcbBcd
                               , WorkOrder
                               , Workcenter
                               , Routing
                               , RepairResult
                               , PartRouting
                               , Repairer
                               , REPLACE(RH.RepairComment, CHAR(13) + CHAR(10), ' ')  AS RepairComment
                               , REPLACE(RH.RepairLocation, CHAR(13) + CHAR(10), ' ') AS RepairLocation
                               , RH.Material                                          AS Material
                               , MT.Spec                                              AS MaterialName
                               , CONCAT(C.Common, '/', C.TextVie)                     AS [BadGroup]
                               , CONCAT(B.Bad, '/', B.Text)                           AS [BadCode]
                      FROM RepairHist            RH
                           LEFT JOIN Material AS MT
                                     ON RH.Material = MT.Material
                           LEFT JOIN Bad      AS B
                                     ON B.Bad = RH.Bad
                           LEFT JOIN Common   AS C
                                     ON B.Bunch = C.Common AND C.Category = '200'
                     WHERE 1 = 1
                       AND RH.PcbBcd = '{tb_Barcode.Text}'
                     ORDER BY RH.Updated DESC
                    ;
                    ";

                DataTable dt = DbAccess.Default.GetDataTable(Q);
                if (dt.Rows.Count <= 0)
                {
                    return false;
                }

                _repairHist = $@"{dt.Rows[0]["RepairHist"]}";

                tb_Barcode.Text = $@"{dt.Rows[0]["PcbBcd"]}";
                tb_WorkOrder.Text = $@"{dt.Rows[0]["WorkOrder"]}";
                tb_Workcenter.Text = $@"{dt.Rows[0]["WorkCenter"]}";
                tb_Routing.Text = $@"{dt.Rows[0]["Routing"]}";
                tb_Material.Text = $@"{dt.Rows[0]["Material"]}";
                tb_MaterialName.Text = $@"{dt.Rows[0]["MaterialName"]}";
                tb_BadGroup.Text = dt.Rows[0]["BadGroup"].ToString();
                tb_BadCode.Text = dt.Rows[0]["BadCode"].ToString();
                cb_RepairGroup.Text = dt.Rows[0]["BadGroup"].ToString();
                cb_Repair.Text = dt.Rows[0]["BadCode"].ToString();

                tb_RepairComment.Text = dt.Rows[0]["RepairComment"].ToString();
                tb_RepairLocation.Text = dt.Rows[0]["RepairLocation"].ToString();
                cb_RepairResult.SelectedValue = dt.Rows[0]["RepairResult"].ToString();
                ComboBox_Worker.Text = dt.Rows[0]["Repairer"].ToString();
                comboBox_PartRouting.SelectedValue = dt.Rows[0]["PartRouting"].ToString();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Clear()
        {
            GetChildControls(this).Where(c => c.GetType() == typeof(TextBox)).ToList().ForEach(c => c.Text = string.Empty);

            _repairHist = null;
            cb_RepairResult.SelectedIndex = 0;
            ComboBox_Worker.SelectedIndex = -1;
            cb_RepairGroup.SelectedIndex = 0;
            comboBox_PartRouting.SelectedIndex = 0;
        }

        private static IEnumerable<Control> GetChildControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                yield return control;
            }

            foreach (Control control in parent.Controls)
            {
                foreach (var childControl in GetChildControls(control))
                {
                    yield return childControl;
                }
            }
        }

        #endregion

        #region Event

        private void RepairEdit_Load(object sender, EventArgs e)
        {
            try
            {
                GetInfo();

                BindCombo_RepairResult();

                BindCombo_RepairGroup();

                BindCombo_PartRouting();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Information load fail. Please open retry.\r\n{ex.Message}", "Load Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void cb_RepairResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_RepairResult.SelectedValue.ToString())
            {
                case "C":
                    cb_RepairGroup.Enabled = true;
                    cb_Repair.Enabled = true;
                    ComboBox_Worker.Enabled = true;
                    comboBox_PartRouting.Enabled = true;
                    break;
                case "X":
                    cb_RepairGroup.SelectedIndex = -1;
                    cb_Repair.SelectedIndex = -1;
                    ComboBox_Worker.SelectedIndex = -1;
                    comboBox_PartRouting.SelectedIndex = -1;
                    cb_RepairGroup.Enabled = false;
                    cb_Repair.Enabled = false;
                    ComboBox_Worker.Enabled = false;
                    comboBox_PartRouting.Enabled = false;
                    break;
            }
        }

        private void cb_RepairGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Repair();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tb_Barcode_DoubleClick(object sender, EventArgs e)
        {
            Clear();
            tb_Barcode.ReadOnly = false;
        }

        private void tb_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(tb_Barcode.Text) == true)
            {
                tb_Barcode.Focus();
                return;
            }

            if (!VerifyBarcode(tb_Barcode.Text))
            {
                MessageBox.Show("Không thể tìm thấy Barcode。(Barcode could not be found.)", "Không tìm thấy(Not Found)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tb_Barcode.ReadOnly = false;
                tb_Barcode.Focus();
                return;
            }

            tb_Barcode.ReadOnly = true;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (cb_RepairResult.SelectedValue.ToString() == "C")
            {
                if (ComboBox_Worker.Text == "" || cb_Repair.Text == "" || comboBox_PartRouting.Text == "")
                {
                    MessageBox.Show("Please select all items.", "Please select all items.", MessageBoxIcon.Warning);
                    return;
                }
            }

            string updateQuery = 
                $@"
                UPDATE RepairHist
                   SET Repairer      = '{ComboBox_Worker.SelectedItem}'
                     , Bad           = N'{cb_Repair.SelectedValue}'
                     , PartRouting   = N'{comboBox_PartRouting.SelectedValue}'
                     , RepairComment = N'{tb_RepairComment.Text.Replace("'", "''")}',
                                    RepairLocation = N'{tb_RepairLocation.Text.Replace("'", "''")}',
                                    Updated = GETDATE()
                 WHERE RepairHist = '{_repairHist}'
                ;
                ";

            try
            {
                DbAccess.Default.ExecuteQuery(updateQuery);
                MessageBox.Show("Đã hoàn thành(Completed)", "Đã hoàn thành(Completed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                tb_Barcode.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + ex.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion
    }
}