using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Forms;
using WiseM.Data;
using System.Data.SqlClient;



namespace WiseM.Browser.EditInfo
{
    public partial class TableList : SkinForm
    {
        public delegate void SendMsgDele(string msg);
        public event SendMsgDele SendMsg;

        //private WiseM.Browser.CustomPanelLinkEventArgs e = null;
        public TableList()
        {
            InitializeComponent();
        }

        

        
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.e.AfterRefresh = WiseM.Browser.WeRefreshPanel.Current;
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            DataTable Dt_tablelist = GetTableList();
            dgv_tablelist.DataSource = Dt_tablelist;
            dgv_tablelist.Columns["TableList"].Width = 215;
        }

        private DataTable GetTableList()
        {
            string query = "Select distinct a.name as TableList from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' ";
            return GetDt(query);
        }
        private DataTable TableSearch(string searchvalue)
        {
            string query = "Select distinct a.name as TableList from sys.tables a inner join sys.syscolumns b on a.object_id = b.id inner join sys.systypes c on c.xtype = b.xtype where c.name != 'sysname' and a.name='"+searchvalue+"'";
            return GetDt(query);
        }

        public static DataTable GetDt(string query)
        {
            string conninfo = "Data Source = 127.0.0.1; Initial Catalog = DualJYMes3; uid = sa; pwd = wisem";
            SqlConnection conn = new SqlConnection(conninfo);

            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
            }
            catch 
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return dt;
        }

        private void dgv_tablelist_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_tablename.Text = dgv_tablelist.CurrentCell.Value.ToString();
        }
        //부모폼에 테이블 정보 추가
        private void btn_add_Click(object sender, EventArgs e)
        {
            string newtable = dgv_tablelist.CurrentRow.Cells["TableList"].Value.ToString();
            //MessageBox.Show(MessageBox.WeConfirmType.Apply, newtable);
            SendMsg(tb_tablename.Text);
            this.Close();
        }

        //tb table  =  name dgv에서 검색
        private void btn_search_Click(object sender, EventArgs e)
        {
            string searchValue = tb_tablename.Text;
            

            //수정예정
            foreach (DataGridViewRow row in dgv_tablelist.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    //MessageBox.Show(MessageBox.WeConfirmType.Apply,"해당열이 존재합니다.");
                    //MessageBox.Show(MessageBox.WeConfirmType.Apply, row.Cells[0].Value.ToString());
                    CellClear();
                    //셀 컬럼을 지정해 주고 해당 열에 값 삽입
                    DataTable Dt_tablelist = TableSearch(searchValue);
                    dgv_tablelist.DataSource = Dt_tablelist;
                    dgv_tablelist.Columns["TableList"].Width = 215;
                    break;
                }
            }
        }
        private void CellClear()
        {
            for (int i = 0; i < dgv_tablelist.Rows.Count; i++)
            {
                if (dgv_tablelist.Rows[i].Cells[0].Value == null)
                {
                    i++;
                }
                dgv_tablelist.DataSource = DBNull.Value;
            }
        }
    }
}
