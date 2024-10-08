using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Forms;
using System.Collections.Generic;

namespace WiseM.Browser.WiseMEdit
{
    public partial class EditForm : SkinForm
    {
        private WiseM.Browser.CustomPanelLinkEventArgs e = null;

        private DataTable dtPKColumnName = null;
        private DataTable dtFKColumnName = null;
        private DataTable dtEditInfoKeyCheck = null;

        private Dictionary<string, DataGridViewComboBoxCell> dicCell = new Dictionary<string, DataGridViewComboBoxCell>();

        private string tableName = string.Empty;

        public EditForm(string tableName, WiseM.Browser.CustomPanelLinkEventArgs e)
        {
            InitializeComponent();

            this.e = e;

            this.tableName = tableName;

            this.Text = "(" + tableName + ") 수정 / 추가 / 삭제";
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            try
            {
                // 기본키 컬럼 조회
                this.dtPKColumnName = this.SearchPKColumn();

                // 외래키 컬럼 조회
                this.dtFKColumnName = this.SearchFKColumn();

                if (this.dtFKColumnName != null && this.dtFKColumnName.Rows.Count > 0)
                {
                    // 외래키 참조 테이블 조회
                    this.dtEditInfoKeyCheck = this.EditInfoTB_Name_Column();
                }

                // Binding Grid
                this.BindDataGridView();

                this.Translation();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to find information.\r\n{ex.Message}", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.Close();
            }
        }

        /// <summary>
        /// 해당 테이블 기본키 조회 
        /// </summary>
        /// <returns></returns>
        private DataTable SearchPKColumn()
        {
            string query = string.Empty;

            query += "\r\n SELECT   DISTINCT ";
            query += "\r\n          COLUMN_NAME ";
            query += "\r\n FROM     INFORMATION_SCHEMA.TABLE_CONSTRAINTS A ";
            query += "\r\n JOIN     INFORMATION_SCHEMA.KEY_COLUMN_USAGE B ";
            query += "\r\n      ON  A.TABLE_NAME = B.TABLE_NAME ";
            query += "\r\n      AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME ";
            query += "\r\n WHERE    A.TABLE_NAME = '" + this.tableName + "' ";
            query += "\r\n      AND A.CONSTRAINT_TYPE = 'PRIMARY KEY' ";

            return this.e.DbAccess.GetDataTable(query);
        }

        /// <summary>
        /// 해당 테이블에 외래키 있는지 조회
        /// </summary>
        private DataTable SearchFKColumn()
        {
            string query = string.Empty;

            query += "\r\n SELECT   DISTINCT ";
            query += "\r\n          B.COLUMN_NAME ";
            query += "\r\n FROM     INFORMATION_SCHEMA.TABLE_CONSTRAINTS A ";
            query += "\r\n JOIN     INFORMATION_SCHEMA.KEY_COLUMN_USAGE B ";
            query += "\r\n      ON  A.TABLE_NAME = B.TABLE_NAME ";
            query += "\r\n      AND A.CONSTRAINT_NAME = B.CONSTRAINT_NAME ";
            query += "\r\n WHERE    A.TABLE_NAME = '" + this.tableName + "' ";
            query += "\r\n      AND A.CONSTRAINT_TYPE = 'FOREIGN KEY' " ;
            query += "\r\n      AND B.COLUMN_NAME IN ('Division', 'Material', 'Routing', 'WorkCenter', 'Common', 'Category', 'Parent', 'Child',' ') ";

            return this.e.DbAccess.GetDataTable(query);
        }

        /// <summary>
        /// 외래키 참조 테이블 조회
        /// </summary>
        /// <returns></returns>
        private DataTable EditInfoTB_Name_Column()
        {
            string query = string.Empty;

            query += "\r\n SELECT   F.Name                                                      AS Key_NAME ";
            query += "\r\n          ,OBJECT_NAME(F.referenced_object_id)                        AS ReferenceTableName ";
            query += "\r\n          ,COL_NAME(FC.referenced_object_id, FC.referenced_column_id) AS ReferenceColumnName ";
            query += "\r\n FROM     sys.foreign_Keys F ";
            query += "\r\n JOIN     sys.foreign_Key_columns FC ";
            query += "\r\n      ON  F.OBJECT_ID = FC.constraint_object_id ";
            query += "\r\n WHERE    OBJECT_NAME(f.parent_object_id) = '" + this.tableName + "' ";
            query += "\r\n ORDER BY KEY_NAME ";

            return this.e.DbAccess.GetDataTable(query);
        }

        private void BindDataGridView()
        {
            DataTable dtBind = this.SearchColumn();

            // 외래키 참조 테이블을 조회하여 Dictionary에 콤보박스 추가
            if (dtFKColumnName.Rows.Count > 0)
            {
                for (int i = 0; i < dtFKColumnName.Rows.Count; i++)
                {
                    DataTable dtForeignTable = SearchTable_OfForeignKey(i);

                    if (dtForeignTable != null)
                    {
                        DataGridViewComboBoxCell comboBoxCell = new DataGridViewComboBoxCell();

                        foreach (DataRow row in dtForeignTable.Rows)
                        {
                            comboBoxCell.Items.Add(row["COLUMN_NAME"]);
                        }

                        dicCell.Add(dtFKColumnName.Rows[i]["COLUMN_NAME"].ToString(), comboBoxCell);
                    }
                }
            }

            for (int i = 0; i < dtBind.Rows.Count; i++)
            {
                int cCell_count_num = dicCell.Count;

                this.dataGridViewResult.Rows.Add();

                for (int j = 0; j < dtPKColumnName.Rows.Count; j++)
                {
                    if (dtBind.Rows[i]["EditColumn"].Equals(dtPKColumnName.Rows[j]["COLUMN_NAME"]))
                    {
                        dataGridViewResult.Rows[i].Cells["Check"].Value = "PK";
                    }
                }

                // Dictionary 에 추가했던 콤보박스를 외래키 컬럼 Cell 에 할당
                if (this.dtFKColumnName != null)
                {
                    foreach (DataRow row in dtFKColumnName.Rows)
                    {
                        if (!dtBind.Rows[i]["EditColumn"].Equals(row["COLUMN_NAME"])) continue;
                        dataGridViewResult.Rows[i].Cells["Check"].Value = "FK";
                        if (!dataGridViewResult.Rows[i].Cells["Check"].Value.Equals("FK")) continue;
                        if(dicCell.ContainsKey(row["COLUMN_NAME"].ToString()))
                            dataGridViewResult.Rows[i].Cells["ColumnValue"] = dicCell[row["COLUMN_NAME"].ToString()];
                    }
                }
                this.dataGridViewResult.Rows[i].Cells["ColumnId"].Value = dtBind.Rows[i]["EditColumn"];
            }


            if (this.tableName == "Material")
            {
                //Type 콤보박스
                var cCell = new DataGridViewComboBoxCell();

                cCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;

                cCell.Items.Add("");
                cCell.Items.Add("SINGLE");
                cCell.Items.Add("DOUBLE");

                dataGridViewResult.Rows[5].Cells["ColumnValue"] = cCell;

                var bitCheckBoxCell = new DataGridViewCheckBoxCell();
                dataGridViewResult.Rows[11].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[12].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[13].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[14].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[15].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[16].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[17].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
                dataGridViewResult.Rows[18].Cells["ColumnValue"] = bitCheckBoxCell.Clone() as DataGridViewCheckBoxCell;
            }



            // 브라우저 패널에서 선택한 데이터가 있다면 데이터 할당
            if (this.e.DataGridView.CurrentRow == null) return;
            {
                foreach (DataGridViewColumn column in this.e.DataGridView.Columns)
                {
                    for (int i = 0; i < dtBind.Rows.Count; i++)
                    {
                        if (this.dataGridViewResult.Rows[i].Cells["ColumnId"].Value.ToString().ToUpper().Equals(column.Name.ToString().ToUpper()))
                        {
                            this.dataGridViewResult.Rows[i].Cells["ColumnValue"].Value = this.e.DataGridView.CurrentRow.Cells[column.Name].Value;
                        }
                    }
                }
            }
        }

        //EditInfo테이블에 정의된 테이블 중 상태 값이 1인 것들만 조회 
        private DataTable SearchColumn()
        {
            string query = "SELECT * FROM EditInfo WHERE EditTableName = '" + this.tableName + "' AND Status = 1 ORDER BY LEN(Seq) ASC, Seq ASC";

            return this.e.DbAccess.GetDataTable(query);
        }

        /// <summary>
        /// 외래키에서 참조중인 테이블 조회
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataTable SearchTable_OfForeignKey(int index)
        {
            string colName = dtEditInfoKeyCheck.Rows[index]["ReferenceTableName"].ToString();
            string from = dtEditInfoKeyCheck.Rows[index]["ReferenceColumnName"].ToString();

            string query = $"SELECT {colName} AS COLUMN_NAME FROM {from}";

            return this.e.DbAccess.GetDataTable(query);
        }

        // 번역
        private void Translation()
        {
            this.btn_Edit.Text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Label, this.btn_Edit.Text);
            this.btn_Delete.Text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Label, this.btn_Delete.Text);
            this.btn_Insert.Text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Label, this.btn_Insert.Text);
            this.btn_Clear.Text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Label, this.btn_Clear.Text);
            this.btn_Close.Text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Label, this.btn_Close.Text);

            foreach(DataGridViewColumn col in this.dataGridViewResult.Columns)
            {
                col.HeaderText = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Column, col.Name);
            }

            if (this.dataGridViewResult.Rows.Count <= 0) return;
            foreach(DataGridViewRow row in this.dataGridViewResult.Rows)
            {
                string text = WiseM.Global.Globalizer.ConvertTo(WiseM.Global.WeLangSubType.Column, row.Cells["ColumnId"].Value.ToString());
                row.Cells["ColumnName"].Value = text;
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.EditProcess();
        }

        private void EditProcess()
        {
            try
            {
                string[,] pkValue = new string[10, 2];
                if (this.CheckPKData(out pkValue) == false)
                {
                    string pkColumnNameStr = null;
                    DataTable pkColumnNameDt = this.dtPKColumnName;
                    for (int i = 0; i < pkColumnNameDt.Rows.Count; i++)
                    {
                        pkColumnNameStr += pkColumnNameDt.Rows[i]["COLUMN_NAME"].ToString() + " , ";
                    }
                    pkColumnNameStr = pkColumnNameStr.Remove(pkColumnNameStr.Length - 3, 3);
                    MessageBox.Show("You can edit and enter key Value(" + pkColumnNameStr + ")", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string editValue = null;
                    foreach (DataGridViewRow dr in this.dataGridViewResult.Rows)
                    {
                        if (dr.Cells["ColumnId"].Value.ToString().ToUpper().Equals("UPDATED")) continue;
                        //SELECT B.name AS [Table] ,A.name AS [Colum] FROM syscolumns A JOIN sysobjects B ON B.id = A.id WHERE A.status = 128 and B.name = 'RawMaterial_Hist'
                        string SidentityColumn = "SELECT A.name AS [Colum] FROM syscolumns A JOIN sysobjects B ON B.id = A.id WHERE A.status = 128 and B.name = '" + dr.Cells["ColumnId"].Value.ToString() + "'";
                        DataTable dt = this.e.DbAccess.GetDataTable(SidentityColumn);
                        if (dt.Rows.Count < 1)
                        {
                            editValue += dr.Cells["ColumnId"].Value + "= N'" + dr.Cells["ColumnValue"].Value + "' ,";
                        }
                    }
                    editValue = editValue.Remove(editValue.Length - 1);
                    if (this.EditQuery(pkValue, editValue) == 1)
                        MessageBox.Show("Has been edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" '" +ex+ "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.MessageBox.Show(" 데이터를 삭제 하시겠습니까?    ", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string[,] pkValue = new string[10, 2];
                    string pkColumnNameStr = null;
                    DataTable pkColumnNameDt = this.dtPKColumnName;
                    for (int i = 0; i < pkColumnNameDt.Rows.Count; i++)
                    {
                        pkColumnNameStr += pkColumnNameDt.Rows[i]["COLUMN_NAME"].ToString() + " , ";
                    }
                    pkColumnNameStr = pkColumnNameStr.Remove(pkColumnNameStr.Length - 3, 3);
                    if (this.CheckPKData(out pkValue) == false)
                    {
                        MessageBox.Show("You can delete and enter key Value(" + pkColumnNameStr + ")", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (this.DeleteProcess(pkValue) == 1)
                            MessageBox.Show("Has been deleted.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" '" +ex+ "' ", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CheckPKData(out string[,] pkValue)
        {
            DataTable dtPKColumnName = this.dtPKColumnName;
            pkValue = new string[10, 2];

            int rowCount = 0;
            foreach (DataGridViewRow dr in this.dataGridViewResult.Rows)
            {
                if (!"PK".Equals(dr.Cells["Check"].Value) && !"FK".Equals(dr.Cells["Check"].Value)) continue;
                for (int i = 0; i < dtPKColumnName.Rows.Count; i++)
                {
                    if (dr.Cells["ColumnId"].Value.ToString() != dtPKColumnName.Rows[i]["COLUMN_NAME"].ToString()) continue;
                    if (dr.Cells["ColumnValue"].Value == null || string.IsNullOrEmpty(dr.Cells["ColumnValue"].Value.ToString()) == true)
                    {
                    }
                    else
                    {
                        pkValue[rowCount, 0] = dtPKColumnName.Rows[i]["COLUMN_NAME"].ToString();
                        pkValue[rowCount, 1] = dr.Cells["ColumnValue"].Value.ToString();

                    }
                }
                rowCount++;
            }

            return !string.IsNullOrEmpty(pkValue[0, 0]);
        }

        private int DeleteProcess(string[,] pkValue)
        {
            string whereQuery = null;
            for (int i = 0; i < 10; i++)
            {
                if (string.IsNullOrEmpty(pkValue[i, 0])) break;
                whereQuery += pkValue[i, 0] + " = '" + pkValue[i, 1] + "' AND ";
            }
            whereQuery = whereQuery.Remove(whereQuery.Length - 4, 4);

            string query = "Delete From " + this.tableName + " Where " + whereQuery;

            return this.e.DbAccess.ExecuteQuery(query);


            //MessageBox.Show(e.ToString());

        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            this.InsertProcess();
        }

        private void InsertProcess()
        {
            try
            {
                string insertColumn = null, insertValue = null;
                foreach (DataGridViewRow dr in this.dataGridViewResult.Rows)
                {
                    if (dr.Cells["ColumnId"].Value.ToString().ToUpper().Equals("UPDATED") == false)
                    {
                        insertColumn += dr.Cells["ColumnId"].Value + " ,";
                        insertValue += "N'" + dr.Cells["ColumnValue"].Value + "' ,";
                    }
                    //Updated 컬럼 gridview <-> getdate()
                    if (dr.Cells["ColumnId"].Value.ToString() != "Updated") continue;
                    insertColumn += dr.Cells["ColumnId"].Value + " ,";
                    insertValue += " getdate()" + " ,";
                }
                insertColumn = insertColumn.Remove(insertColumn.Length - 1);
                insertValue = insertValue.Remove(insertValue.Length - 1);
                if (this.InsertQuery(insertColumn, insertValue) == 1)
                    MessageBox.Show("Has been Insert.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private int InsertQuery(string insertColumn, string insertValue)
        {
            string query = "Insert Into " + this.tableName + " ( " + insertColumn + " ) VALUES ( " + insertValue + " ) ";
            return this.e.DbAccess.ExecuteQuery(query);
        }

        private void btn_clear_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
            {
                if (dataGridViewResult.Rows[i].Cells["ColumnValue"].Value == null)
                    continue;

                dataGridViewResult.Rows[i].Cells["ColumnValue"].Value = DBNull.Value;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.e.AfterRefresh = WiseM.Browser.WeRefreshPanel.Current;
        }

        // PK셀 더블 클릭시 메세지 박스
        private void dataGridViewResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ("PK".Equals(dataGridViewResult.Rows[e.RowIndex].Cells["Check"].Value))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("{0} 값은 기본키 값입니다.", dataGridViewResult.Rows[e.RowIndex].Cells["ColumnId"].Value));
                }
            }
            catch { }
        }

        private void dataGridViewResult_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int column = dataGridViewResult.CurrentCell.ColumnIndex;
            string headerText = dataGridViewResult.Columns[column].HeaderText;

            if (!headerText.Equals("ColumnValue")) return;
            if (!(e.Control is TextBox tb)) return;
            tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tb.AutoCompleteCustomSource = AutoCompleteLoad();
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
            {
                if (dataGridViewResult.Rows[i].Cells["ColumnValue"].Value == null)
                {
                    i++;
                }
                dataGridViewResult.Rows[i].Cells["ColumnValue"].Value = DBNull.Value;
            }
        }
        private void ClearProcess()
        {
            for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
            {
                if (dataGridViewResult.Rows[i].Cells["ColumnValue"].Value == null)
                {
                    i++;
                }
                dataGridViewResult.Rows[i].Cells["ColumnValue"].Value = DBNull.Value;
            }
        }

        public AutoCompleteStringCollection AutoCompleteLoad()
        {
            AutoCompleteStringCollection str = new AutoCompleteStringCollection();
            for (int i = 0; i <= 5; i++)
            {
                str.Add("Name" + i);
            }
            return str;
        }
        void MenuClick(object obj, EventArgs ea)
        {
            MenuItem mI = (MenuItem)obj;
            string str = mI.Text;

            switch (str)
            {
                case "셀 초기화":
                    //MessageBox.Show("프로그램을 초기화하였습니다");
                    ClearProcess();
                    break;
                case "새로고침":
                    System.Windows.Forms.MessageBox.Show("프로그램을 삭제하였습니다");
                    break;
                case "프로그램 종료":
                    Close();
                    break;
            }
        }

        private void dataGridViewResult_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            EventHandler eh = MenuClick;
            MenuItem[] ami = {
                new MenuItem("셀 초기화",eh),
                new MenuItem("새로고침",eh),
                new MenuItem("-",eh),
                new MenuItem("프로그램 종료",eh),
            };
            ContextMenu = new ContextMenu(ami);
        }

        public void addItems(AutoCompleteStringCollection col)
        {
            col.Add("Product 1");
            col.Add("Product 2");
            col.Add("Product 3");
        }
        private int EditQuery(string[,] pkValue, string editValue)
        {
            string whereQuery = null;
            for (int i = 0; i < 10; i++)
            {
                if (string.IsNullOrEmpty(pkValue[i, 0]))
                {
                    break;
                }

                whereQuery += pkValue[i, 0] + " = '" + pkValue[i, 1] + "' AND ";
            }
            whereQuery = whereQuery.Remove(whereQuery.Length - 4, 4);
            
            string query = "UPDATE " + this.tableName + " SET " + editValue + " , Updated = getdate() WHERE " + whereQuery;
            return this.e.DbAccess.ExecuteQuery(query);
        }

    }    

}
