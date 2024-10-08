using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.Rework
{
    public partial class BadEdit : Form
    {
        DataSet dataSet;
        private readonly string _key;

        public BadEdit(string keyByBadHist)
        {
            InitializeComponent();
            _key = keyByBadHist;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "确认 (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            var query = new StringBuilder();
            // KeyRelation Update
            query.AppendLine
                (
                 $@"
                BEGIN TRY
                    BEGIN TRAN
                        UPDATE BadHist
                           SET Routing    = '{comboBox_Routing.SelectedValue}'
                             , Workcenter = '{comboBox_WorkCenter.SelectedValue}'
                             , Bad        = '{comboBox_Bad.SelectedValue}'
                             , Updated    = GetDate()
                         WHERE BadHist = {_key}
                        UPDATE KeyRelation
                           SET Bad        = '{comboBox_Bad.SelectedValue}'
                             , UpdateUser = '{WiseApp.Id}'
                             , Updated    = GetDate()
                         WHERE 1 = 1
                           AND PcbBcd = '{textBox_Barcode.Text}';

                    COMMIT TRAN;
                    SELECT 0;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    SELECT -1;
                END CATCH
                "
                );
            try
            {
                if (DbAccess.Default.ExecuteScalar(query.ToString()) is int result)
                {
                    switch (result)
                    {
                        case 0:
                            MessageBox.Show("已完成(Completed)", "已完成(Completed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            break;
                        case -1:
                            MessageBox.Show("Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("数据库错误(Database Error)", "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("数据库错误(Database Error)\r\n" + exception.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "确认 (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            var query = new StringBuilder();
            // KeyRelation Update
            query.AppendLine
                (
                 $@"
                BEGIN TRY
                    BEGIN TRAN
                        UPDATE KeyRelation
                           SET Blocking   = 0
                             , Bad        = ''
                             , UpdateUser = '{WiseApp.Id}'
                             , Updated    = GetDate()
                         WHERE 1 = 1
                           AND PcbBcd = '{textBox_Barcode.Text}';

                        DELETE
                          FROM BadHist
                         WHERE BadHist = {_key};

                        DELETE
                          FROM RepairHist
                         WHERE PcbBcd = '{textBox_Barcode.Text}'
                           AND RepairResult = 'X';
                    COMMIT TRAN;
                    SELECT 0;
                END TRY
                BEGIN CATCH
                    ROLLBACK TRAN;
                    SELECT -1;
                END CATCH
                "
                );
            try
            {
                if (DbAccess.Default.ExecuteScalar(query.ToString()) is int result)
                {
                    switch (result)
                    {
                        case 0:
                            MessageBox.Show("已完成(Completed)", "已完成(Completed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                            break;
                        case -1:
                            MessageBox.Show("Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("数据库错误(Database Error)", "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("数据库错误(Database Error)\r\n" + exception.Message, "数据库错误(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BadEdit_Load(object sender, EventArgs e)
        {
            try
            {
                GetInfo();
                BindCombo_Routing();
                BindCombo_BadGroup();

                var query = new StringBuilder();
                query.AppendLine
                    (
                     $@"
SELECT BH.BadHist
     , BH.Division
     , BH.ClientId
     , BH.WorkCenter
     , BH.Material
     , M.Spec
     , BH.Routing
     , BH.WorkOrder
     , BH.Repeat
     , BH.Started
     , BH.Ended
     , B.Bunch
     , BH.Bad
     , BH.BadQty
     , BH.Status
     , BH.Updated
     , BH.IssueType
     , BH.Shift
     , BH.SourceHist
     , BH.SearchKey
     , BH.PalletNo
     , BH.BoxNo
     , BH.SerialType
     , BH.SerialNo
     , BH.OutputHist
     , BH.Cavity
     , BH.TransDate
     , BH.SkipLine
     , BH.ActiveWorkers
     , BH.PerQty
     , BH.TempKeyId
     , BH.BadReason
     , BH.SendStatusErp
  FROM BadHist AS BH
       LEFT OUTER JOIN Material AS M
                       ON BH.Material = M.Material
       LEFT OUTER JOIN Bad AS B
                       ON BH.Bad = B.Bad
 WHERE BadHist = {_key}
;
                    "
                    );
                var dataRow = DbAccess.Default.GetDataRow(query.ToString());
                textBox_Barcode.Text = dataRow["SerialNo"] as string;
                textBox_WorkOrder.Text = dataRow["WorkOrder"] as string;
                textBox_Material.Text = dataRow["Material"] as string;
                textBox_MaterialName.Text = dataRow["Spec"] as string;
                comboBox_Routing.SelectedValue = dataRow["Routing"] as string;
                comboBox_WorkCenter.SelectedValue = dataRow["WorkCenter"] as string;
                comboBox_BadGroup.SelectedValue = dataRow["Bunch"] as string;
                comboBox_Bad.SelectedValue = dataRow["Bad"] as string;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"信息加载失败。 请打开重试。(Information load fail. Please open retry.)\r\n{exception.Message}", "载入失败(Load Fail)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void GetInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine
                (
                 $@"
                SELECT Routing
                     , TextChn AS Text
                  FROM Routing
                 WHERE 1 = 1
                   AND Status = '1'
                   AND Routing NOT IN ('Ai_Mount', 'Mi_Mount', 'St_Mount', 'Mi_Assy1', 'Mi_Assy2', 'Pk_Boxing', 'Pk_StockIn')
                 ORDER BY
                     ViewSeq
                ;
                "
                );

            stringBuilder.AppendLine
                (
                 $@"
                SELECT C.Common
                     , CONCAT(C.Common, '/', C.Text) AS Text
                  FROM Common C
                 WHERE C.Category = '200'
                 ORDER BY
                     C.Common
                ;
                "
                );

            stringBuilder.AppendLine
                (
                 $@"
                SELECT B.Bad
                     , CONCAT(B.Bad, '/', B.Text) AS Text
                     , B.Bunch
                  FROM Bad B
                 WHERE B.Status = 1
                 ORDER BY
                     B.Bad
                "
                );

            dataSet = DbAccess.Default.GetDataSet(stringBuilder.ToString());

            if (dataSet == null) return;
            dataSet.Tables[0].TableName = "Routing";  // 공정
            dataSet.Tables[1].TableName = "BadGroup"; // 불량그룹
            dataSet.Tables[2].TableName = "Bad";
        }

        private void BindCombo_Routing()
        {
            comboBox_Routing.DataSource = null;

            if (dataSet == null
                || !dataSet.Tables.Contains("Routing")) return;
            var dtSource = dataSet.Tables["Routing"];

            dtSource.Rows.InsertAt(dtSource.NewRow(), 0);

            comboBox_Routing.DataSource = dtSource;
            comboBox_Routing.DisplayMember = "Text";
            comboBox_Routing.ValueMember = "Routing";
        }

        private void BindCombo_Line()
        {
            comboBox_WorkCenter.DataSource = null;
            var routing = comboBox_Routing.SelectedValue as string;
            if (string.IsNullOrEmpty(routing)) return;
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine
                (
                 $@"
                SELECT WC.WorkCenter
                     , WC.Text
                  FROM WorkCenter WC
                 WHERE COALESCE(WC.ERP_WC_CD, '') NOT IN ('C0010', 'C0058')
                   AND WC.Routing = '{routing}'
                ;
                "
                );

            var dataTable = DbAccess.Default.GetDataTable(stringBuilder.ToString());
            comboBox_WorkCenter.DataSource = dataTable;
            comboBox_WorkCenter.DisplayMember = "Text";
            comboBox_WorkCenter.ValueMember = "WorkCenter";
        }

        private void BindCombo_BadGroup()
        {
            comboBox_BadGroup.DataSource = null;

            if (dataSet == null
                || !dataSet.Tables.Contains("BadGroup")) return;
            var dtSource = dataSet.Tables["BadGroup"];

            dtSource.Rows.InsertAt(dtSource.NewRow(), 0);

            comboBox_BadGroup.DataSource = dtSource;
            comboBox_BadGroup.DisplayMember = "Text";
            comboBox_BadGroup.ValueMember = "Common";
        }

        private void BindCombo_Bad()
        {
            comboBox_Bad.DataSource = null;

            string badGroup = Convert.ToString(comboBox_BadGroup.SelectedValue);

            if (string.IsNullOrEmpty(badGroup)) return;
            if (dataSet == null
                || !dataSet.Tables.Contains("Bad")) return;
            var temp = dataSet.Tables["Bad"].Select($"Bunch = '{badGroup}'");

            if (temp.Length <= 0)
                temp = null;

            var dtSource = temp?.CopyToDataTable();

            comboBox_Bad.DataSource = dtSource;
            comboBox_Bad.DisplayMember = "Text";
            comboBox_Bad.ValueMember = "Bad";
        }

        /// <summary>
        /// 입력 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(textBox_Barcode.Text))
            {
                MessageBox.Show("请输入PCB条形码。(Please enter PCB barcode.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrEmpty(textBox_WorkOrder.Text))
            {
                MessageBox.Show("请输入PCB条形码。(Please enter PCB barcode.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_Routing.SelectedIndex)
            {
                MessageBox.Show("请选择路线。(Please select Routing.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_WorkCenter.SelectedIndex)
            {
                MessageBox.Show("请选择线路。(Please select Line.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_BadGroup.SelectedIndex)
            {
                MessageBox.Show("请选择不良组。(Please select Bad group.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_Bad.SelectedIndex)
            {
                MessageBox.Show("请选择不良代码。(Please select Bad.)", "不选择(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void comboBox_Routing_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Line();
        }

        private void comboBox_BadGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Bad();
        }
    }
}
