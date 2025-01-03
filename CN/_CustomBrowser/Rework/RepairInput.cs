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
    public partial class RepairInput : SkinForm
    {
        #region Field

        private readonly CustomPanelLinkEventArgs _customPanelLinkEventArgs;

        private DataSet _dataSet;

        #endregion

        #region Constructor

        public RepairInput(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();

            _customPanelLinkEventArgs = e;
        }

        #endregion

        #region Method

        private void SetBadInfo()
        {
            // if (!(_customPanelLinkEventArgs.DataGridView is DataGridView dataGridView))
            // {
            //     MessageBox.Show("Invalid Data. Check repair information.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     Close();
            //     return;
            // }
            //
            // if (!(dataGridView.CurrentRow is DataGridViewRow dataGridViewRow))
            // {
            //     MessageBox.Show("Invalid Data. Check repair information.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     Close();
            //     return;
            // }
            //
            // if (!(dataGridViewRow.Cells["PcbBcd"].Value is string pcbBcd))
            // {
            //     MessageBox.Show("Invalid Data. Check repair information.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     Close();
            //     return;
            // }
            //
            // string Q = $@"
            //             SELECT C.Common + ' / '  + C.[Text] AS BadGroup, B.Bad + ' / ' + B.[Text] AS Bad  FROM Bad B
            //             	LEFT OUTER JOIN Common C ON B.Bunch = C.Common
            //             WHERE 1=1
            //               AND C.Category = '200'
            //               AND B.Bad = '{dataGridViewRow.Cells["Bad"].Value}'
            //             ";
            // DataTable dt = DbAccess.Default.GetDataTable(Q);
            //
            // tb_Barcode.Text = pcbBcd;
            //
            // tb_Workcenter.Text = $@"{dataGridViewRow.Cells["WorkCenter"].Value}/{dataGridViewRow.Cells["WorkCenterName"].Value}";
            // tb_Routing.Text = $@"{dataGridViewRow.Cells["Routing"].Value}/{dataGridViewRow.Cells["RoutingName"].Value}";
            //
            // tb_WorkOrder.Text = $@"{dataGridViewRow.Cells["WorkOrder"].Value}";
            // tb_Material.Text = $@"{dataGridViewRow.Cells["Material"].Value}";
            //
            // tb_BadGroup.Text = dt.Rows[0]["BadGroup"].ToString();
            // tb_BadCode.Text = dt.Rows[0]["Bad"].ToString();
            //
            // tb_MaterialName.Text = $@"{dataGridViewRow.Cells["Spec"].Value}";
        }

        private void GetInfo()
        {
            var query = string.Empty;

            query += $@"
                        SELECT WT.WorkTeam
                             , CONCAT(WT.WorkTeam, ' / ', WT.Text) AS Text
                          FROM WorkTeam WT
                         WHERE WT.Status = 1
                         ORDER BY
                             WT.Bunch
                    ;";
            query += "\r\n";
            query += "\r\n SELECT Worker, CONCAT(Worker, '/', Text) AS Text, Bunch FROM Worker WHERE Status = 1 ORDER BY Worker ";
            query += $@"
                        SELECT C.Common
                             , CONCAT(C.Common, '/', C.Text) AS Text
                          FROM Common C
                         WHERE C.Category = '200'
                         ORDER BY
                             C.Common
                    ;";
            query += $@"
                        SELECT B.Bad
                             , CONCAT(B.Bad, '/', B.Text) AS Text
                             , B.Bunch
                          FROM Bad B
                         WHERE B.Status = 1
                         ORDER BY
                             B.Bad
                    ;";
            query += "\r\n";
            query += $@"     SELECT C.Common
                             , CONCAT(C.Common, '/', C.TextChn) AS Text
                             FROM Common C
                             WHERE C.Category = '200'
                             ORDER BY
                             C.Common 
                             ; ";
            query += "\r\n";
            query += $@" 	   
                             SELECT B.Bad
                             , CONCAT(B.Bad, '/', B.Text) AS Text
                             , B.Bunch
                             FROM Bad B
                             WHERE B.Status = 1
                             ORDER BY
                             B.Bad 
                             ; ";
            query += "\r\n";
            query += $@"
                        SELECT Common
                             , CONCAT_WS(' / ', Common, TextChn) AS Text
                          FROM Common
                         WHERE Category = '180'
                         ORDER BY
                             ViewSeq
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
            dt.Rows.Add("C: 修理完成(Complete)", "C");
            dt.Rows.Add("X: 报废(Scrap)", "X");

            cb_RepairResult.DataSource = dt;
            cb_RepairResult.DisplayMember = "DisplayMember";
            cb_RepairResult.ValueMember = "ValueMember";
            cb_RepairResult.SelectedIndex = 0;
        }

        private void BindCombo_Dept()
        {
            // cb_Dept.DataSource = null;
            //
            // if (_dataSet == null || !_dataSet.Tables.Contains("Dept")) return;
            // var dtSource = _dataSet.Tables["Dept"];
            //
            // dtSource.Rows.InsertAt(dtSource.NewRow(), 0);
            //
            // cb_Dept.DataSource = dtSource;
            // cb_Dept.DisplayMember = "Text";
            // cb_Dept.ValueMember = "WorkTeam";
        }

        private void BindCombo_Worker()
        {
            // cb_Worker.DataSource = null;
            //
            // var dept = Convert.ToString(cb_Dept.SelectedValue);
            //
            // if (string.IsNullOrEmpty(dept)) return;
            // if (_dataSet == null || !_dataSet.Tables.Contains("Worker")) return;
            // var temp = _dataSet.Tables["Worker"].Select($"Bunch = '{dept}'");
            //
            // if (temp.Length <= 0)
            //     temp = null;
            //
            // var dtSource = temp?.CopyToDataTable();
            //
            // cb_Worker.DataSource = dtSource;
            // cb_Worker.DisplayMember = "Text";
            // cb_Worker.ValueMember = "Worker";
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
            cb_Repair.ValueMember = "Repair";
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

        private void Clear()
        {
            GetChildControls(this).Where(c => c.GetType() == typeof(TextBox) && ((TextBox)c).ReadOnly).ToList().ForEach(c => c.Text = string.Empty);

            cb_RepairResult.SelectedIndex = 0;
            cb_RepairGroup.SelectedIndex = 0;

            tb_RepairComment.Text = string.Empty;
            tb_RepairLocation.Text = string.Empty;
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

        private void RepairInput_Load(object sender, EventArgs e)
        {
            try
            {
                GetInfo();

                BindCombo_RepairResult();

                BindCombo_Dept();

                BindCombo_RepairGroup();

                BindCombo_PartRouting();

                SetBadInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Information load fail. Please open retry.\r\n{ex.Message}", "Load Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
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

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            var repairResult = Convert.ToString(cb_RepairResult.SelectedValue);
            var repairComment = tb_RepairComment.Text;
            var RepairLocation = tb_RepairLocation.Text;
            string[] Bad_Temp = cb_Repair.Text.Split('/');
            string Bad_Split = Bad_Temp[0];

            if (0 > cb_RepairResult.SelectedIndex)
            {
                MessageBox.Show("请选择维修结果。(Please select repair result.)", "不选择(Not Select)", MessageBoxIcon.Warning);
                return;
            }

            var query = new StringBuilder();

            switch (repairResult)
            {
                case "C":
                    if (0 > comboBox_PartRouting.SelectedIndex)
                    {
                        //请选择部品工程
                        MessageBox.Show("请选择部品工程。(Please select part routing.)", "不选择(Not Select)", MessageBoxIcon.Warning);
                        return;
                    }

                    if (0 > ComboBox_Worker.SelectedIndex)
                    {
                        //请选择维修员(Repairer)
                        MessageBox.Show("请选择维修员。(Please select Repairer.)", "不选择(Not Select)", MessageBoxIcon.Warning);
                        return;
                    }

                    switch (tb_Routing.Text)
                    {
                        case "Mi_2ndFunc":
                        case "Mi_Visual-Inspection":
                            query.AppendLine(
                                $@"
                                UPDATE KeyRelation
                                   SET Blocking           = 0
                                     , Bad                = NULL
                                     , Second_Func_Result = NULL
                                     , Second_Func_Count  = 0
                                     , UpdateUser         = N'{WiseApp.Id}'
                                     , Updated            = GETDATE()
                                 WHERE PcbBcd = '{tb_Barcode.Text}'
                                ;
                                "
                            );
                            break;
                        default:
                            query.AppendLine(
                                $@"
                                UPDATE KeyRelation
                                   SET Blocking           = 0
                                     , Bad                = NULL
                                     , ICT_Result         = NULL
                                     , ICT_Count          = 0
                                     , First_Func_Result  = NULL
                                     , First_Func_Count   = 0
                                     , Voltage_Result     = NULL
                                     , Voltage_Count      = 0
                                     , Second_Func_Result = NULL
                                     , Second_Func_Count  = 0
                                     , UpdateUser         = N'{WiseApp.Id}'
                                     , Updated            = GETDATE()
                                 WHERE PcbBcd = '{tb_Barcode.Text}'
                                ;
                                "
                            );
                            break;
                    }

                    query.AppendLine(
                        $@"
                        INSERT
                          INTO RepairStockHist (
                                                 Type
                                               , PcbBarcode
                                               , Material
                                               , ERP_SL_CD_FROM
                                               , ERP_SL_CD_TO
                                               , SendStatusERP
                                               , Created
                                               )
                        SELECT 'OUT'
                             , RI.PcbBcd
                             , RI.Material
                             , '980328-4'
                             , '980318'
                             , 0
                             , GETDATE()
                          FROM RepairInfo                 AS RI
                         WHERE RI.PcbBcd = '{tb_Barcode.Text}'
                        ;
                        "
                    );

                    break;
                case "X":
                    query.AppendLine(
                        $@"
                        INSERT
                          INTO BadHist ( Division
                                       , ClientId
                                       , WorkCenter
                                       , Material
                                       , Routing
                                       , WorkOrder
                                       , Bad
                                       , BadQty
                                       , IssueType
                                       , SerialType
                                       , SerialNo )
                        SELECT TOP 1 'PL30'
                                   , 'Browser'
                                   , WorkCenter
                                   , Material
                                   , Routing
                                   , WorkOrder
                                   , Bad
                                   , 1
                                   , 'Browser'
                                   , 'PCB'
                                   , PcbBcd
                          FROM RepairInfo
                         WHERE PcbBcd = '{tb_Barcode.Text}'
                        ;
                    "
                    );
                    break;
                default:
                    throw new ArgumentOutOfRangeException("repairResult");
            }

            query.AppendLine(
                $@"
                INSERT
                  INTO RepairHist ( PcbBcd
                                  , WorkOrder
                                  , Material
                                  , Routing
                                  , WorkCenter
                                  , Bad
                                  , Repairer
                                  , RepairResult
                                  , PartRouting
                                  , RepairComment
                                  , RepairLocation
                                  , updater )
                SELECT TOP 1 PcbBcd
                           , WorkOrder
                           , Material
                           , Routing
                           , WorkCenter
                           , N'{Bad_Split}'
                           , N'{ComboBox_Worker.Text}'
                           , N'{repairResult}'
                           , '{comboBox_PartRouting.SelectedValue}'
                           , N'{repairComment}'
                           , N'{RepairLocation}'
                           , N'{WiseApp.Id}'
                  FROM RepairInfo
                 WHERE PcbBcd = '{tb_Barcode.Text}'
                ;
                "
            );

            query.AppendLine(
                $@"
                    DELETE
                      FROM RepairInfo
                     WHERE PcbBcd = '{tb_Barcode.Text}'
                    ;
                "
            );

            try
            {
                DbAccess.Default.ExecuteQuery(query.ToString());
                MessageBox.Show("已完成(Completed)", "已完成(Completed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //  Close();
                TextBox_Clear();

                tb_Barcode.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库错误(Database Error)\r\n" + ex.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_RepairResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cb_RepairResult.SelectedValue.ToString())
            {
                case "C":
                    cb_Repair.Enabled = true;
                    cb_RepairGroup.Enabled = true;
                    ComboBox_Worker.Enabled = true;
                    comboBox_PartRouting.Enabled = true;
                    break;
                case "X":
                    cb_Repair.SelectedIndex = -1;
                    cb_RepairGroup.SelectedIndex = -1;
                    ComboBox_Worker.SelectedIndex = -1;
                    comboBox_PartRouting.SelectedIndex = -1;
                    cb_Repair.Enabled = false;
                    cb_RepairGroup.Enabled = false;
                    ComboBox_Worker.Enabled = false;
                    comboBox_PartRouting.Enabled = false;
                    break;
            }
        }

        #endregion

        private bool VerifyBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Vui lòng nhập Barcode。(Please input barcode.)", "Barcode trống(Empty Barcode)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            try
            {
                
                string Q = $@"
                    select  
                    PcbBcd,
                    WorkOrder,
                    WorkCenter,
                    Routing,
                    RI.Material as Material ,
                    MT.Spec as MaterialName ,
                    CONCAT(C.Common, '/', C.TextVie) AS [BadGroup],
                    CONCAT(B.Bad, '/', B.Text) AS [BadCode] 
                    from
                    RepairInfo RI
                    left join Material as MT
                    on RI.Material = MT.Material
                    left join Bad as B 
                    on B.Bad = RI.Bad
                    left join Common as C
                    on B.Bunch = C.Common
                    where 
                    C.Category ='200' 
                    and RI.PcbBcd = '{tb_Barcode.Text}'
                        ";
                DataTable dt = DbAccess.Default.GetDataTable(Q);
                if (dt.Rows.Count <= 0)
                {
                    return false;
                }

                tb_WorkOrder.Text = $@"{dt.Rows[0]["WorkOrder"]}";
                tb_Workcenter.Text = $@"{dt.Rows[0]["WorkCenter"]}";
                tb_Routing.Text = $@"{dt.Rows[0]["Routing"]}";
                tb_Material.Text = $@"{dt.Rows[0]["Material"]}";
                tb_MaterialName.Text = $@"{dt.Rows[0]["MaterialName"]}";

                tb_BadGroup.Text = dt.Rows[0]["BadGroup"].ToString();
                tb_BadCode.Text = dt.Rows[0]["BadCode"].ToString();
                cb_RepairGroup.Text = dt.Rows[0]["BadGroup"].ToString();
                cb_Repair.Text = dt.Rows[0]["BadCode"].ToString();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void TextBox_Clear()
        {
            try
            {
                tb_Barcode.Text = "";
                tb_WorkOrder.Text = "";
                tb_Workcenter.Text = "";
                tb_Routing.Text = "";
                tb_Material.Text = "";
                tb_MaterialName.Text = "";
                tb_BadGroup.Text = "";
                tb_BadCode.Text = "";
                tb_RepairComment.Text = "";
                tb_RepairLocation.Text = "";
                cb_RepairGroup.SelectedIndex = 0;
                cb_Repair.SelectedIndex = 0;
                cb_RepairResult.SelectedIndex = 0;
                ComboBox_Worker.SelectedIndex = 0;
                comboBox_PartRouting.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        private void tb_Barcode_DoubleClick(object sender, EventArgs e)
        {
            TextBox_Clear();
            tb_Barcode.ReadOnly = false;
        }

        private void tb_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (string.IsNullOrEmpty(tb_Barcode.Text))
            {
                tb_Barcode.Focus();
                return;
            }

            if (!VerifyBarcode(tb_Barcode.Text))
            {
                MessageBox.Show("找不到条形码。(Barcode could not be found.)", "未找到(Not Found)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tb_Barcode.ReadOnly = false;
                tb_Barcode.Focus();
                return;
            }

            tb_Barcode.ReadOnly = true;
        }
    }
}