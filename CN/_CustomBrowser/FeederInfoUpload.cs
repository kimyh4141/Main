using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Browser.Properties;
using WiseM.Data;
using DataTable = System.Data.DataTable;

namespace WiseM.Browser
{
    public partial class FeederInfoUpload : Form
    {
        #region Field

        // 확장명 XLS (Excel 97~2003 용)
        private const string ConnectStrFrm_Excel97_2003 =
            "Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 8.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";

        // 확장명 XLSX (Excel 2007 이상용)
        private const string ConnectStrFrm_Excel =
            "Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source=\"{0}\";" +
            "Mode=ReadWrite|Share Deny None;" +
            "Extended Properties='Excel 12.0; HDR={1}; IMEX={2}';" +
            "Persist Security Info=False";

        #endregion

        #region Constructor

        public FeederInfoUpload()
        {
            InitializeComponent();
        }

        #endregion

        #region Method

        private DataTable GetDataTableWithExcel(string selectPath)
        {
            string oledbConnectionString = string.Empty;

            if (selectPath.IndexOf(".xlsx", StringComparison.Ordinal) > -1)
            {
                //엑셀 2007
                oledbConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + selectPath + ";Extended Properties=\"Excel 12.0\"";
            }
            else
            {
                //엑셀 2003 및 이하 버전
                oledbConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + selectPath + ";Extended Properties=\"Excel 8.0\"";
            }

            DataTable dataTable = null;
            try
            {
                using (var oleDbConnection = new OleDbConnection(oledbConnectionString))
                {
                    oleDbConnection.Open();
                    DataTable dt = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    string sheetName = dt.Rows[0]["TABLE_NAME"].ToString();                                                   //엑셀 첫 번째 시트명
                    string sQuery = $" SELECT Material, WorkCenter, Feeder, RawMaterial, RawMaterialSeq, InputQty FROM [{sheetName}] WHERE Material <> ''"; //쿼리

                    dataTable = new DataTable();
                    var oleDbDataAdapter = new OleDbDataAdapter(sQuery, oleDbConnection);
                    oleDbDataAdapter.Fill(dataTable);
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"{e.Message}", "Warring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
            return dataTable;
        }

        private bool UploadFeederInfo(DataTable dataTable)
        {
            var query = new StringBuilder();
            query.AppendLine(
                $@"
DECLARE @UpdateUser  NVARCHAR(50) = '{WiseApp.CurrentUser.Name}'
DECLARE @Updated     DATETIME = GETDATE()
DECLARE @DuplicationOption NVARCHAR(10) = '{comboBox_Duplication.SelectedItem}'
;

IF OBJECT_ID('tempdb..#temp') IS NOT NULL
    BEGIN
        DROP TABLE #temp;
    END
IF OBJECT_ID('tempdb..#Result') IS NOT NULL
    BEGIN
        DROP TABLE #Result;
    END
CREATE TABLE #Result
(
    DuplicationOption VARCHAR(50),
    ChangeType        VARCHAR(100)
)
;

                    SELECT Material
                         , WorkCenter
                         , Feeder
                         , RawMaterial
                         , RawMaterialSeq
                         , InputQty
                      INTO #Temp
                      FROM FeederInfo
                     WHERE 1 = 2
                    ;
                "
            );
            foreach (DataRow dataRow in dataTable.Rows)
            {
                query.AppendLine(
                    $@"
                    INSERT
                      INTO #Temp
                          (
                              Material
                          ,   WorkCenter
                          ,   Feeder
                          ,   RawMaterial
                          ,   RawMaterialSeq
                          ,   InputQty
                          )
                    VALUES
                    (
                          '{dataRow["Material"].ToString().Trim()}'
                      ,   '{dataRow["WorkCenter"].ToString().Trim()}'
                      ,   '{dataRow["Feeder"].ToString().Trim()}'
                      ,   '{dataRow["RawMaterial"].ToString().Trim()}'
                      ,   {dataRow["RawMaterialSeq"].ToString().Trim()}
                      ,   {dataRow["InputQty"].ToString().Trim()}
                    )
"
                );
            }

            query.AppendLine($@"
IF ( @DuplicationOption = 'Update' )
    BEGIN
        MERGE
        INTO FeederInfo FI
        USING #Temp AS T
        ON (
                    1 = 1
                AND FI.Material = T.Material
                AND FI.WorkCenter = T.WorkCenter
                AND FI.Feeder = T.Feeder
                AND FI.RawMaterial = T.RawMaterial
            )
        WHEN MATCHED
            THEN
            UPDATE
               SET FI.RawMaterialSeq = T.RawMaterialSeq
                 , FI.InputQty       = T.InputQty
                 , FI.Status         = 1
                 , FI.UpdateUser     = @UpdateUser
                 , FI.Updated        = @Updated
        WHEN NOT MATCHED
            THEN
            INSERT
                (
                    Material
                ,   WorkCenter
                ,   Feeder
                ,   RawMaterial
                ,   RawMaterialSeq
                ,   InputQty
                ,   Status
                ,   UpdateUser
                ,   Updated
                )
            VALUES
                (
                    T.Material
                ,   T.WorkCenter
                ,   T.Feeder
                ,   T.RawMaterial
                ,   T.RawMaterialSeq
                ,   InputQty
                ,   1
                ,   @UpdateUser
                ,   @Updated
                )
            OUTPUT 'Update', $action AS ChangeType INTO #Result (DuplicationOption, ChangeType);
    END
");
            query.AppendLine($@"
IF ( @DuplicationOption = 'Ignore' )
    BEGIN
        MERGE
        INTO FeederInfo FI
        USING #Temp AS T
        ON (
                    1 = 1
                AND FI.Material = T.Material
                AND FI.WorkCenter = T.WorkCenter
                AND FI.Feeder = T.Feeder
                AND FI.RawMaterial = T.RawMaterial
            )
        WHEN NOT MATCHED
            THEN
            INSERT
                (
                    Material
                ,   WorkCenter
                ,   Feeder
                ,   RawMaterial
                ,   RawMaterialSeq
                ,   InputQty
                ,   Status
                ,   UpdateUser
                ,   Updated
                )
            VALUES
                (
                    T.Material
                ,   T.WorkCenter
                ,   T.Feeder
                ,   T.RawMaterial
                ,   T.RawMaterialSeq
                ,   T.InputQty
                ,   1
                ,   @UpdateUser
                ,   @Updated
                )
            OUTPUT 'Ignore', $action AS ChangeType INTO #Result (DuplicationOption, ChangeType);
    END
");
            query.AppendLine($@"
SELECT COUNT(*) AS count
  FROM #Result
;
");
            query.AppendLine(
                $@"
IF OBJECT_ID('tempdb..#temp') IS NOT NULL
    BEGIN
        DROP TABLE #temp;
    END
IF OBJECT_ID('tempdb..#Result') IS NOT NULL
    BEGIN
        DROP TABLE #Result;
    END
;
");
            try
            {
                
                System.Windows.Forms.MessageBox.Show($"File Upload Completed. ({DbAccess.Default.ExecuteScalar(query.ToString())} Rows)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show($"File Upload Failed.\r\n{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

        #region Event

        private void FeederInfoUpload_Load(object sender, EventArgs e)
        {
            comboBox_Duplication.Items.Add("Update");
            comboBox_Duplication.Items.Add("Ignore");
            comboBox_Duplication.SelectedIndex = 0;
        }

        private void button_DownloadExcelFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = @"C:\", Filter = @"Excel Files(.xlsx)|*.xlsx"
            };

            if (DialogResult.OK != saveFileDialog.ShowDialog()) return;
            var selectedPath = saveFileDialog.FileName;
            var dataBytes = Resources.FeederInfo_Upload;
            using (var fileStream = new FileStream($"{selectedPath}", FileMode.Create))
            {
                fileStream.Write(dataBytes, 0, dataBytes.Length);
                fileStream.Close();
            }

            using (var fileStream = new FileStream($"{selectedPath}", FileMode.Create))
            {
                fileStream.Write(dataBytes, 0, dataBytes.Length);
                fileStream.Close();
            }
            

            System.Windows.Forms.MessageBox.Show($"File Save Completed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_FileSelect_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Multiselect = false, DefaultExt = @"Excel Files(.xlsx)|*.xlsx", Filter = @"Excel Files(.xls)|*.xls|Excel Files(.xlsx)|*.xlsx|Excel Files(*.xlsm)|*.xlsm", FilterIndex = 2
            };

            if (DialogResult.OK != openFileDialog.ShowDialog()) return;
            var dataTable = GetDataTableWithExcel($"{openFileDialog.FileName}");
            dataGridView_data.DataSource = dataTable;
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if (UploadFeederInfo(dataGridView_data.DataSource as DataTable))
            {
                //저장완료
            }
            else
            {
                //저장실패
            }
        }

        #endregion
    }
}