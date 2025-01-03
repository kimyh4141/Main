using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class BadInput : SkinForm
    {
        #region Field

        DataSet dataSet;

        private readonly Color _backColorBadBarcode = Color.Yellow;

        #endregion

        #region Constructor

        public BadInput()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private void GetInfo()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(
                $@"
                SELECT Routing
                     , TextVie AS Text
                  FROM Routing
                 WHERE 1 = 1
                   AND Status = '1'
                   AND Routing NOT IN ('Ai_Mount', 'Mi_Mount', 'St_Mount', 'Mi_Assy1', 'Mi_Assy2', 'Pk_Boxing', 'Pk_StockIn')
                 ORDER BY
                     ViewSeq
                ;
                "
            );

            stringBuilder.AppendLine(
                $@"
                SELECT C.Common
                     , CONCAT(C.Common, '/', C.TextVie) AS Text
                  FROM Common C
                 WHERE C.Category = '200'
                 ORDER BY
                     C.Common
                ;
                "
            );

            stringBuilder.AppendLine(
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
            dataSet.Tables[0].TableName = "Routing"; // 공정
            dataSet.Tables[1].TableName = "BadGroup"; // 불량그룹
            dataSet.Tables[2].TableName = "Bad";
        }

        private void BindCombo_Routing()
        {
            comboBox_Routing.DataSource = null;

            if (dataSet == null || !dataSet.Tables.Contains("Routing")) return;
            var dtSource = dataSet.Tables["Routing"];

            dtSource.Rows.InsertAt(dtSource.NewRow(), 0);

            comboBox_Routing.DataSource = dtSource;
            comboBox_Routing.DisplayMember = "Text";
            comboBox_Routing.ValueMember = "Routing";
        }

        private void BindCombo_Owner()
        {
            comboBox_Owner.DataSource = null;

            var routing = comboBox_Routing.SelectedValue as string;

            if (string.IsNullOrEmpty(routing)) return;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(
                 $@"
                SELECT DISTINCT WC.Owner
                    FROM WorkCenter WC
                   WHERE WC.Routing = '{routing}'
                ;
                "
            );

            var dataTable = DbAccess.Default.GetDataTable(stringBuilder.ToString());

            dataTable.Rows.InsertAt(dataTable.NewRow(), 0);
            comboBox_Owner.DataSource = dataTable;
            comboBox_Owner.DisplayMember = "Owner";
            comboBox_Owner.ValueMember = "Owner";
        }

        private void BindCombo_Line()
        {
            comboBox_WorkCenter.DataSource = null;

            var routing = comboBox_Routing.SelectedValue as string;
            var owner = comboBox_Owner.SelectedValue as string;

            if (string.IsNullOrEmpty(routing)) return;
            if (string.IsNullOrEmpty(owner)) return;

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(
                $@"
                SELECT WC.WorkCenter
                     , WC.Text
                     , WC.Owner
                  FROM WorkCenter WC
                 WHERE COALESCE(WC.ERP_WC_CD, '') NOT IN ('C0010', 'C0058')
                   AND WC.Routing = '{routing}'
                   AND WC.Owner = '{owner}'
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

            if (dataSet == null || !dataSet.Tables.Contains("BadGroup")) return;
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
            if (dataSet == null || !dataSet.Tables.Contains("Bad")) return;
            var temp = dataSet.Tables["Bad"].Select($"Bunch = '{badGroup}'");

            if (temp.Length <= 0)
                temp = null;

            var dtSource = temp?.CopyToDataTable();

            comboBox_Bad.DataSource = dtSource;
            comboBox_Bad.DisplayMember = "Text";
            comboBox_Bad.ValueMember = "Bad";
        }

        private bool VerifyBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode))
            {
                MessageBox.Show("Vui lòng nhập Barcode。(Please input barcode.)", "Barcode trống(Empty Barcode)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            var query = new StringBuilder();
            query.AppendLine(
                $@"
                SELECT KR.PcbBcd
                     , KR.Routing
                     , R.Text  AS RoutingName
                     , KR.WorkOrder
                     , M.Material
                     , M.Spec  AS Spec
                     , WC.Workcenter
                     , WC.Text AS WorkCenterName
                     , KR.Bad
                     , KR.Blocking
                     , KR.ReWork
                  FROM (
                         SELECT KR.PcbBcd
                              , CASE
                                  WHEN KR.Packed IS NOT NULL
                                    THEN 'Pk_StockIn'
                                  WHEN KR.Palletized IS NOT NULL
                                    THEN 'Pk_Boxing'
                                  WHEN KR.Mi_Loaded IS NOT NULL
                                    THEN 'Mi_Load'
                                  WHEN KR.Smt_Unloaded IS NOT NULL
                                    THEN 'St_Unload'
                                  WHEN KR.Smt_Loaded IS NOT NULL
                                    THEN 'St_Load'
                                  WHEN KR.Ai_Unloaded IS NOT NULL
                                    THEN 'Ai_Unload'
                                  WHEN KR.Ai_Loaded IS NOT NULL
                                    THEN 'Ai_Load'
                                  WHEN KR.Ai_Loaded IS NULL
                                    THEN 'None'
                                END AS Routing
                              , CASE
                                  WHEN KR.Packed IS NOT NULL
                                    THEN KR.Pack_WorkOrder
                                  WHEN KR.Palletized IS NOT NULL
                                    THEN KR.Box_WorkOrder
                                  WHEN KR.Mi_Loaded IS NOT NULL
                                    THEN KR.Mi_WorkOrder
                                  WHEN KR.Smt_Unloaded IS NOT NULL
                                    THEN KR.Smt_WorkOrder_Unload
                                  WHEN KR.Smt_Loaded IS NOT NULL
                                    THEN KR.Smt_WorkOrder
                                  WHEN KR.Ai_Unloaded IS NOT NULL
                                    THEN KR.Ai_WorkOrder_Unload
                                  WHEN KR.Ai_Loaded IS NOT NULL
                                    THEN KR.Ai_WorkOrder_Load
                                  WHEN KR.Ai_Loaded IS NULL
                                    THEN 'None'
                                END AS WorkOrder
                              , CASE
                                  WHEN KR.Packed IS NOT NULL
                                    THEN KR.Pack_Material
                                  WHEN KR.Palletized IS NOT NULL
                                    THEN KR.Box_Material
                                  WHEN KR.Mi_Loaded IS NOT NULL
                                    THEN KR.Mi_Material
                                  WHEN KR.Smt_Unloaded IS NOT NULL
                                    THEN KR.Smt_Material_Unload
                                  WHEN KR.Smt_Loaded IS NOT NULL
                                    THEN KR.Smt_Material
                                  WHEN KR.Ai_Unloaded IS NOT NULL
                                    THEN KR.Ai_Material_Unload
                                  WHEN KR.Ai_Loaded IS NOT NULL
                                    THEN KR.Ai_Material_Load
                                  WHEN KR.Ai_Loaded IS NULL
                                    THEN 'None'
                                END AS Material
                              , CASE
                                  WHEN KR.Packed IS NOT NULL
                                    THEN KR.Pack_Line
                                  WHEN KR.Palletized IS NOT NULL
                                    THEN KR.Box_Line
                                  WHEN KR.Mi_Loaded IS NOT NULL
                                    THEN KR.Mi_Line
                                  WHEN KR.Smt_Unloaded IS NOT NULL
                                    THEN KR.Smt_Line_Unload
                                  WHEN KR.Smt_Loaded IS NOT NULL
                                    THEN KR.Smt_Line
                                  WHEN KR.Ai_Unloaded IS NOT NULL
                                    THEN KR.Ai_Line_Unload
                                  WHEN KR.Ai_Loaded IS NOT NULL
                                    THEN KR.Ai_Line_Load
                                  WHEN KR.Ai_Loaded IS NULL
                                    THEN 'None'
                                END AS WorkCenter
                              , KR.Bad
                              , KR.Blocking
                              , KR.ReWork
                           FROM KeyRelation KR
                          WHERE KR.PcbBcd = '{barcode}'
                       )                          KR
                       LEFT OUTER JOIN Routing    R
                                       ON KR.Routing = R.Routing
                       LEFT OUTER JOIN Material   M
                                       ON KR.Material = M.Material
                       LEFT OUTER JOIN WorkCenter WC
                                       ON KR.WorkCenter = WC.Workcenter
                ;
                    "
            );

            try
            {
                if (!(DbAccess.Default.GetDataRow(query.ToString()) is DataRow dataRow))
                {
                    MessageBox.Show("Không thể tìm thấy Barcode。(Barcode could not be found.)", "Không tìm thấy(Not Found)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!string.IsNullOrEmpty(Convert.ToString(dataRow["Bad"])))
                {
                    MessageBox.Show("Đã nhập Barcode。(Already input barcode.)", "Đã nhập(Already Input)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox_Barcode.Focus();
                    return false;
                }

                var pcbBcd = dataRow["PcbBcd"] as string;
                var workOrder = dataRow["WorkOrder"] as string;
                var material = dataRow["Material"] as string;
                var materialName = dataRow["Spec"] as string;
                var routing = dataRow["Routing"] as string;
                var workCenter = dataRow["WorkCenter"] as string;

                textBox_Barcode.Text = pcbBcd;
                textBox_WorkOrder.Text = workOrder;
                textBox_Material.Text = material;
                textBox_MaterialName.Text = materialName;
                textBox_Routing.Text = routing;
                textBox_WorkCenter.Text = workCenter;

                var control = GetChildControls(this).FirstOrDefault(c => c.GetType() == typeof(TextBox) && ((TextBox) c).ReadOnly && c.Text.Equals(barcode));
                if (control != null) control.BackColor = _backColorBadBarcode;
                textBox_Barcode.ReadOnly = true;
                return true;
            }
            catch
            {
                MessageBox.Show("Lỗi tìm kiếm Barcode. Vui lòng thử lại。 请再试一次。(Error to find barcode. Please try again.)", "Lỗi(Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        /// <summary>
        /// 입력 확인
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            if (string.IsNullOrEmpty(textBox_Barcode.Text))
            {
                MessageBox.Show("Vui lòng nhập Barcode PCB。(Please enter PCB barcode.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (string.IsNullOrEmpty(textBox_WorkOrder.Text))
            {
                MessageBox.Show("Vui lòng nhập Barcode PCB。(Please enter PCB barcode.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_Routing.SelectedIndex)
            {
                MessageBox.Show("Vui lòng chọn Routing。(Please select Routing.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_Owner.SelectedIndex)
            {
                MessageBox.Show("Vui lòng chọn Owner。(Please select Owner.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_WorkCenter.SelectedIndex)
            {
                MessageBox.Show("Vui lòng chọn Line。(Please select Line.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_BadGroup.SelectedIndex)
            {
                MessageBox.Show("Vui lòng chọn Bad Group。(Please select Bad group.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 > comboBox_Bad.SelectedIndex)
            {
                MessageBox.Show("Vui lòng chọn Bad。(Please select Bad.)", "Không chọn(Not Select)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (0 < DbAccess.Default.IsExist("BadHist", $"SerialNo = '{textBox_Barcode.Text}'"))
            {
                MessageBox.Show("Đây là phế liệu。(This already Scrap.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (0 < DbAccess.Default.IsExist("RepairInfo", $"PcbBcd = '{textBox_Barcode.Text}'"))
            {
                MessageBox.Show("đã được đăng ký。(already registered.)", "Cảnh báo(Warning)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 데이터 저장
        /// </summary>
        private bool SaveData()
        {
            var temp_txt =  comboBox_Bad.SelectedValue.ToString();
            string[] split_txt = temp_txt.Split('/');
            var query = new StringBuilder();
            // KeyRelation Update
            query.AppendLine(
                $@"UPDATE KeyRelation
                       SET Blocking   = 1
                         , Bad = '{comboBox_Bad.SelectedValue}'
                         , UpdateUser = '{WiseApp.Id}'
                         , Updated    = GetDate()
                     WHERE 1 = 1
                       AND PcbBcd = '{textBox_Barcode.Text}'
                    ;
                    "
            );

            query.AppendLine(
                rbtn_Scrap.Checked
                    ?
                    // BadHist Insert
                    $@"
                    INSERT
                      INTO BadHist
                          (
                              Division
                          ,   ClientId
                          ,   WorkCenter
                          ,   Material
                          ,   Routing
                          ,   WorkOrder
                          ,   Bad
                          ,   IssueType
                          ,   BadQty
                          ,   SerialType
                          ,   SerialNo
                          ,   TransDate
                          )
                    VALUES
                        (
                            'PL40'
                        ,   'Browser'
                        ,   N'{comboBox_WorkCenter.SelectedValue}'
                        ,   N'{textBox_Material.Text}'
                        ,   N'{comboBox_Routing.SelectedValue}'
                        ,   N'{textBox_WorkOrder.Text}'
                        ,   N'{split_txt}'          
                        ,   'Browser'
                        ,   1
                        ,   'PCB'
                        ,   N'{textBox_Barcode.Text}'
                        ,   GetDate()
                        )

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
                        SELECT 'IN'
                             , '{textBox_Barcode.Text}'
                             , '{textBox_Material.Text}'
                             , CASE Owner
                                 WHEN 'Sehyun'
                                   THEN 'VP20'
                                 WHEN 'SungJin'
                                   THEN 'VP20S'
                               END
                             , CASE Owner
                                 WHEN 'Sehyun'
                                   THEN 'VP20-M'
                                 WHEN 'SungJin'
                                   THEN 'VP20S-SM'
                               END
                             , 0
                             , GETDATE()
                          FROM WorkCenter
                         WHERE Workcenter = '{comboBox_WorkCenter.SelectedValue}'
                        ;
                    "
                    :
                    //comboBox_Bad.SelectedValue
                    // RepairInfo Insert
                    $@"
                     INSERT
                      INTO RepairInfo ( PcbBcd
                                      , WorkOrder
                                      , Material
                                      , Routing
                                      , WorkCenter
                                      , Status
                                      , Bad
                                      , Updater
                                      , Updated )
                    VALUES ( N'{textBox_Barcode.Text}'
                           , N'{textBox_WorkOrder.Text}'
                           , N'{textBox_Material.Text}'
                           , N'{comboBox_Routing.SelectedValue}'
                           , N'{comboBox_WorkCenter.SelectedValue}'
                           , 1
                           , N'{comboBox_Bad.SelectedValue}'
                           , N'{WiseApp.Id}'
                           , GETDATE() )
                    ;

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
                        SELECT 'IN'
                             , '{textBox_Barcode.Text}'
                             , '{textBox_Material.Text}'
                             , CASE Owner
                                 WHEN 'Sehyun'
                                   THEN 'VP20'
                                 WHEN 'SungJin'
                                   THEN 'VP20S'
                               END
                             , CASE Owner
                                 WHEN 'Sehyun'
                                   THEN 'VP20-M'
                                 WHEN 'SungJin'
                                   THEN 'VP20S-SM'
                               END
                             , 0
                             , GETDATE()
                          FROM WorkCenter
                         WHERE Workcenter = '{comboBox_WorkCenter.SelectedValue}'
                        ;
                    "
            );

            try
            {
                DbAccess.Default.ExecuteQuery(query.ToString());
                MessageBox.Show("Đã hoàn thành(Completed)", "Đã hoàn thành(Completed)", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi cơ sở dữ liệu(Database Error)\r\n" + e.Message, "Lỗi cơ sở dữ liệu(Database Error)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void Clear()
        {
            GetChildControls(this).Where(c => c.GetType() == typeof(TextBox))?.ToList().ForEach(c =>
            {
                c.Text = string.Empty;
                if (((TextBox) c).ReadOnly && ((TextBox) c).BackColor == _backColorBadBarcode) c.BackColor = SystemColors.Control;
            });
            comboBox_BadGroup.SelectedIndex = 0;
            comboBox_Routing.SelectedIndex = 0;
            textBox_Barcode.Focus();
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

        private void BadInput_Load(object sender, EventArgs e)
        {
            
            try
            {
                GetInfo();

                BindCombo_Routing();

                BindCombo_Owner();

                BindCombo_BadGroup();

                radioButton_Repair.Checked = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Lỗi tải thông tin. Vui lòng thử lại。 请打开重试。(Information load fail. Please open retry.)\r\n{exception.Message}", "Lỗi tải(Load Fail)", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
            }
        }

        private void comboBox_Routing_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Owner();
        }

        private void comboBox_Owner_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Line();
        }

        private void comboBox_BadGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCombo_Bad();
        }

        private void textBox_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(sender is TextBox textBox)) return;
            if (e.KeyCode != Keys.Enter) return;
            if (!VerifyBarcode(textBox.Text))
            {
                textBox_Barcode.Focus();
               
            }
        }

        private void button_Insert_Click(object sender, EventArgs e)
        {
            if (!CheckData()) return;
            if (!SaveData()) return;
            Clear();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        private void textBox_Barcode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox_Barcode.ReadOnly = false;
        }
    }
}