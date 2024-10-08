using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Forms;
using System.Collections.Generic;
using System.Linq;


namespace WiseMEdit
{
    public partial class EditInfo : SkinForm
    {
        string selected_value_child;
        private WiseM.Browser.CustomPanelLinkEventArgs e;
        private string tableName;
        private DataTable  pkColumnName = null;


        WiseM.Browser.EditInfo.TableList tblist = new WiseM.Browser.EditInfo.TableList();


        public EditInfo(string tableName, WiseM.Browser.CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
            this.tableName = tableName;
            this.Text = "(" + tableName + ") 수정 / 추가 / 삭제";
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {
            DataTable mergeDt = new DataTable();

            mergeDt.Columns.Add(new DataColumn("EditTableName",typeof(string)));
            mergeDt.Columns.Add(new DataColumn("EditColumn", typeof(string)));

            //컬럼 3
            DataTable bindDt = this.SearchColumn();
            //컬럼 2
            DataTable bindDt2 = this.SearchColumn2();


            //seq배정 안된 애들 찾기 
            //binddt2에서 editcolumn 값 가져오고 binddt 에 있는지 조회 후 없으면 mergedt에 추가
            //seq배정 안된 값들만 넣어주면 됨.
            int status = 1;

            for (int i = 0; i < bindDt2.Rows.Count; i++)
            {
                for (int j = 0; j < bindDt.Rows.Count; j++)
                {
                    if (bindDt2.Rows[i]["EditColumn"].ToString() == bindDt.Rows[j]["EditColumn"].ToString())
                    {
                        status = 0;
                        //MessageBox.Show(bindDt2.Rows[j]["EditColumn"].ToString());
                    }
                }
                if (status == 1)
                {
                    Addrow(mergeDt,bindDt2.Rows[i]["EditTableName"].ToString(),bindDt2.Rows[i]["EditColumn"].ToString());
                }
                
                status = 1;
            }

            //sususu
            bindDt.Merge(mergeDt);
            //dataGridViewResult.DataSource = diff;
            dataGridViewResult.DataSource = bindDt;        
        }
        public static DataTable CompareTwoDataTable(DataTable dt1, DataTable dt2)
        {
            dt1.Merge(dt2);

            DataTable d3 = dt2.GetChanges();
            return d3;

        }

        private static void Addrow(DataTable dt, string table, string column)
        {
            try
            {
                DataRow dr = dt.NewRow();
                dr["EditTableName"] = table;
                dr["EditColumn"] = column;
                dt.Rows.Add(dr);
            }
            catch
            {
                MessageBox.Show("추가실패");
            }
        }

        private DataTable SearchColumn()
        {
            string query2 = "Select a.name as EditTableName, d.seq as seq, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype inner join EditInfo d on d.EditTableName = a.name and d.EditColumn = b.name where c.name != 'sysname' and a.name = '" + this.e.DataGridView.CurrentRow.Cells[0].Value.ToString() + "' order by seq asc";
            return this.e.DbAccess.GetDataTable(query2);
        }



        //SearchColumn()2
        private DataTable SearchColumn2()
        {
            string query = "Select a.name as EditTableName, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' and a.name = '" + this.e.DataGridView.CurrentRow.Cells[0].Value.ToString() + "' ";
            return this.e.DbAccess.GetDataTable(query);
        }
        //selected_value_child
        private DataTable SearchColumn3()
        {
            //string query2 = "Select a.name as EditTableName, d.seq as seq, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype inner join EditInfo d on d.EditTableName = a.name and d.EditColumn = b.name where c.name != 'sysname' and a.name = '" + this.e.DataGridView.CurrentRow.Cells[0].Value.ToString() + "' order by seq asc";
            string query2 = "Select a.name as EditTableName, d.seq as seq, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype inner join EditInfo d on d.EditTableName = a.name and d.EditColumn = b.name where c.name != 'sysname' and a.name = '" + selected_value_child + "' order by seq asc";
            return this.e.DbAccess.GetDataTable(query2);
        }

        private DataTable SearchColumn4()
        {
            //string query = "Select a.name as EditTableName, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' and a.name = '" + this.e.DataGridView.CurrentRow.Cells[0].Value.ToString() + "' ";
            string query = "Select a.name as EditTableName, b.name as EditColumn from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' and a.name = '" + selected_value_child + "' ";
            return this.e.DbAccess.GetDataTable(query);
        }








        private DataTable GetTableList()
        {
            string query = "Select distinct a.name as EditTableName from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' ";
            return this.e.DbAccess.GetDataTable(query);
        }

        private bool NullCheckBasicCode(string basicCode)
        {
            if (string.IsNullOrEmpty(basicCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        private void CheckMessageBox(string message)
        {
            string checkMessage = "Please Check " + message + " Value.";
            MessageBox.Show(checkMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnEnableStatus(int status)
        {
            switch (status)
            {
                //0일 경우 수정 삭제 버튼 비 활성화
                case 0:
                    //this.btn_save.Enabled = false;
                    //this.btn_delete.Enabled = false;
                    break;
                //1일 경우 추가 삭제 버튼 비 활성화
                case 1:
                    //this.btn_reload.Enabled = false;
                    //this.btn_delete.Enabled = false;
                    break;

                case 2:
                    break;
            }

        }



        private int DeleteProcess(string[,] pkValue)
        {
            string whereQuery = null;
            for (int i = 0; i < 10; i++)
            {

                if (string.IsNullOrEmpty(pkValue[i, 0]) == true)
                {
                    break;
                }
                else
                {
                    whereQuery += pkValue[i, 0] + " = '" + pkValue[i, 1] + "' AND ";
                }
            }
            whereQuery = whereQuery.Remove(whereQuery.Length - 4, 4);

            string query = "Delete From " + this.tableName + " Where " + whereQuery;
            return this.e.DbAccess.ExecuteQuery(query);
        }

        private bool CheckPKData(out string[,] pkValue)
        {
            DataTable dtPKColumnName = this.pkColumnName;
            pkValue = new string[10, 2];

            int rowCount = 0;
            foreach (DataGridViewRow dr in this.dataGridViewResult.Rows)
            {
                for (int i = 0; i < dtPKColumnName.Rows.Count; i++)
                {
                    if (dr.Cells["ColumnName"].Value.ToString() == dtPKColumnName.Rows[i]["COLUMN_NAME"].ToString())
                    {
                        if (dr.Cells["ColumnText"].Value == null || string.IsNullOrEmpty(dr.Cells["ColumnText"].Value.ToString()) == true)
                        {
                        }
                        else
                        {
                            pkValue[rowCount, 0] = dtPKColumnName.Rows[i]["COLUMN_NAME"].ToString();
                            pkValue[rowCount, 1] = dr.Cells["ColumnText"].Value.ToString();
                        }
                    }
                }
                rowCount++;
            }

            if (string.IsNullOrEmpty(pkValue[0, 0]) == false)
                return true;
            else
                return false;
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {

            if (this.dataGridViewResult.Rows.Count <= 0)
                return;

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.e.AfterRefresh = WiseM.Browser.WeRefreshPanel.Current;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //데이터 그리드 뷰에서만 삭제, insert시 삭제하고 넣기 때문
            dataGridViewResult.Rows.Remove(dataGridViewResult.CurrentRow);

        }

        private void EditForm_SizeChanged(object sender, EventArgs e)
        {
            Size propertyGridSize = new Size(this.Size.Width - 40, this.Size.Height - 120);
            this.dataGridViewResult.Size = propertyGridSize;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            //btn_relaod
            DataTable mergeDt = new DataTable();

            mergeDt.Columns.Add(new DataColumn("EditTableName", typeof(string)));
            mergeDt.Columns.Add(new DataColumn("EditColumn", typeof(string)));

            //해당 테이블의 전체 컬럼 조회
            //컬럼 1
            DataTable bindDt = this.SearchColumn();
            
            //해당 테이블 EditInfo에서 조회
            //컬럼 2
            DataTable bindDt2 = this.SearchColumn2();


            //
            int status = 1;
            for (int i = 0; i < bindDt2.Rows.Count; i++)
            {
                for (int j = 0; j < bindDt.Rows.Count; j++)
                {
                    if (bindDt2.Rows[i]["EditColumn"].ToString() == bindDt.Rows[j]["EditColumn"].ToString())
                    {
                        status = 0;
                    }
                }
                if (status == 1)
                {
                    Addrow(mergeDt, bindDt2.Rows[i]["EditTableName"].ToString(), bindDt2.Rows[i]["EditColumn"].ToString());
                }

                status = 1;
            }
            bindDt.Merge(mergeDt);
            dataGridViewResult.DataSource = bindDt;

            MessageBox.Show("정렬완료");
        }
        private void newtablelaod()
        {
            //btn_relaod
            DataTable mergeDt = new DataTable();

            mergeDt.Columns.Add(new DataColumn("EditTableName", typeof(string)));
            mergeDt.Columns.Add(new DataColumn("EditColumn", typeof(string)));

            //컬럼 3
            DataTable bindDt = this.SearchColumn3();
            //컬럼 2
            DataTable bindDt2 = this.SearchColumn4();

            int status = 1;
            for (int i = 0; i < bindDt2.Rows.Count; i++)
            {
                for (int j = 0; j < bindDt.Rows.Count; j++)
                {
                    if (bindDt2.Rows[i]["EditColumn"].ToString() == bindDt.Rows[j]["EditColumn"].ToString())
                    {
                        status = 0;
                    }
                }
                if (status == 1)
                {
                    Addrow(mergeDt, bindDt2.Rows[i]["EditTableName"].ToString(), bindDt2.Rows[i]["EditColumn"].ToString());
                }

                status = 1;
            }
            bindDt.Merge(mergeDt);
            dataGridViewResult.DataSource = bindDt;

            MessageBox.Show("정렬완료");
        }


        //insertprocess 수정
        private void InsertProcess(string a, string b , string c)
        {
            //DataGridViewRow dr in this.dataGridViewResult.Rows
            string query1 = "insert into EditInfo (EditTableName,Seq,EditColumn,Status,Updated) values ('"+a+"','"+b+"','"+c+"',1,GETDATE())";
            
        }


        private bool CheckInsertColumnText()
        {
            foreach (DataGridViewRow dr in this.dataGridViewResult.Rows)
            {
                if (Convert.ToBoolean(dr.Cells["Check"].Value) == true)
                {
                    if (dr.Cells["ColumnText"].Value == null || string.IsNullOrEmpty(dr.Cells["ColumnText"].Value.ToString()))
                        return false;
                }
            }
            return true;
        }

        private int InsertQuery(string insertColumn, string insertValue)
        {
            string query = " Insert Into " + this.tableName + " ( " + insertColumn + " ) VALUES ( " + insertValue + " ) ";
            return this.e.DbAccess.ExecuteQuery(query);
        }

        ///sususu
        private void buttonEdit_Click(object sender, EventArgs e)
        {
           this.InsertProcess1();

        }
        private void InsertProcess1()
        {
            //로직적으로 수정 필요 
            try
            {
                string query3 = "delete from EditInfo where EditTableName='"+ dataGridViewResult.Rows[0].Cells["EditTableName"].Value.ToString() +"'";
                this.e.DbAccess.ExecuteQuery(query3);
                for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
                {
                    //MessageBox.Show(dataGridViewResult.Rows[35].Cells["Seq"].Value);
                    if (dataGridViewResult.Rows[i].Cells["Seq"].Value.ToString() == "")
                    {
                      // MessageBox.Show(i + "번재 셀 seq가 비어있습니다.");
                    }
                    else
                    {
                        string edittablename = dataGridViewResult.Rows[i].Cells["EditTableName"].Value.ToString();
                        string seq = dataGridViewResult.Rows[i].Cells["Seq"].Value.ToString();
                        string editcolumn = dataGridViewResult.Rows[i].Cells["EditColumn"].Value.ToString();

                        string qeury2 = "insert into EditInfo(EditTableName,Seq,EditColumn,Status,Updated) values ('" + edittablename + "','" + seq + "','" + editcolumn + "',1,GETDATE())";
                        this.e.DbAccess.ExecuteQuery(qeury2);
                    }

                }
                MessageBox.Show("저장 완료");
            }
           
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EditProcess()
        {
            string[,] pkValue = new string[10, 2];
            if (this.CheckPKData(out pkValue) == false)
            {
                string pkColumnNameStr = null;
                DataTable pkColumnNameDt = this.pkColumnName;
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
                    if (Convert.ToBoolean(dr.Cells["Check"].Value) == true)
                    {
                        editValue += dr.Cells["ColumnName"].Value + "= '" + dr.Cells["ColumnText"].Value + "' ,";
                    }
                }
                editValue = editValue.Remove(editValue.Length - 1);
                if (this.EditQuery(pkValue, editValue) == 1)
                    MessageBox.Show("Has been edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.InsertProcess1();
        }

        private void 정렬ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //btn_relaod
            DataTable mergeDt = new DataTable();



            mergeDt.Columns.Add(new DataColumn("EditTableName", typeof(string)));
            mergeDt.Columns.Add(new DataColumn("EditColumn", typeof(string)));


            //컬럼 3
            DataTable bindDt = this.SearchColumn();
            //컬럼 2
            DataTable bindDt2 = this.SearchColumn2();


            int status = 1;
            for (int i = 0; i < bindDt2.Rows.Count; i++)
            {
                for (int j = 0; j < bindDt.Rows.Count; j++)
                {
                    if (bindDt2.Rows[i]["EditColumn"].ToString() == bindDt.Rows[j]["EditColumn"].ToString())
                    {
                        status = 0;
                    }
                }
                if (status == 1)
                {
                    Addrow(mergeDt, bindDt2.Rows[i]["EditTableName"].ToString(), bindDt2.Rows[i]["EditColumn"].ToString());
                }

                status = 1;
            }
            bindDt.Merge(mergeDt);
            dataGridViewResult.DataSource = bindDt;

            MessageBox.Show("정렬완료");
        }

        private void 셀초기화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridViewResult.Rows.Count; i++)
            {
                if (dataGridViewResult.Rows[i].Cells[2].Value == null)
                {
                    i++;
                }
                dataGridViewResult.Rows[i].Cells[2].Value = DBNull.Value;
            }
        }

        private void 셀제거ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewResult.Rows.Remove(dataGridViewResult.CurrentRow);
        }

       

        private void 닫기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.e.AfterRefresh = WiseM.Browser.WeRefreshPanel.Current;
        }

        private void EditInfo_Load(object sender, EventArgs e)
        {
            
            tblist.SendMsg += new WiseM.Browser.EditInfo.TableList.SendMsgDele(child_SendMsg);
        }
        void child_SendMsg(string msg)
        {
            // MessageBox.Show("Fromchild");
            //tb_test.Text = tblist.tb_tablename.Text;
            selected_value_child = tblist.tb_tablename.Text;
            newtablelaod();


        }

        private int EditQuery(string[,] pkValue, string editValue)
        {
            string whereQuery = null;
            for (int i = 0; i < 10; i++)
            {

                if (string.IsNullOrEmpty(pkValue[i, 0]) == true)
                {
                    break;
                }
                else
                {
                    whereQuery += pkValue[i, 0] + " = '" + pkValue[i, 1] + "' AND ";
                }
            }
            whereQuery = whereQuery.Remove(whereQuery.Length - 4, 4);

            string query = "UPDATE " + this.tableName + " SET " + editValue + " , Updated = getdate() WHERE " + whereQuery;
            return this.e.DbAccess.ExecuteQuery(query);

        }
        private void 테이블목록ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //123
            tblist.ShowDialog(this);
        }
    }
}
