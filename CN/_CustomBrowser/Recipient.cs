using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
	public partial class Recipient : SkinForm
	{
		public Recipient()
		{
			InitializeComponent();
			SearchData();
		}

		public void SearchData()
		{
			string SearchCommon = " Select Common From Common Where Category = '2' And Status = 1 ";
			DataTable dt = DbAccess.Default.GetDataTable(SearchCommon);

			this.dataGridView1.Rows.Clear();
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.dataGridView1.Rows.Add(1);
				DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
				if (this.dataGridView1.Columns.Count < 1) return;

				dr.Cells["Common"].Value = dt.Rows[i]["Common"].ToString().Trim();
				dr.Cells["Delete"].Value = "Delete";
			
			}
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                string DeleteQuery = " Delete From Common Where Category = '2' And Common  = '" + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + "' ";
                DbAccess.Default.ExecuteQuery(DeleteQuery);
                MessageBox.Show("Successfully.", "Information", MessageBoxIcon.Information);

                SearchData();
                this.textBox1.Text = string.Empty;
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
		{
            Add();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Add();
            }
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(this.textBox1.Text) == false)
            {
                string CheckData = "  Select * From Common Where Category = '2' And Common = '" + this.textBox1.Text + "' ";
                DataTable CheckDatadt = DbAccess.Default.GetDataTable(CheckData);

                if (CheckDatadt.Rows.Count > 0)
                {
                    MessageBox.Show("Is already include data.", "Warning", MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string InsertQuery = " Insert Into Common (Category, Common, Text, Status, Updated, ViewSeq, TextKor, TextEng, TextVnm) Values ('2' , '" + this.textBox1.Text + "' , '" + this.textBox1.Text + "' , 1, Getdate() , Null,Null,Null,Null ) ";
                    DbAccess.Default.ExecuteQuery(InsertQuery);
                    MessageBox.Show("Successfully.", "Information", MessageBoxIcon.Information);

                    SearchData();
                    this.textBox1.Text = string.Empty;
                }
            }
        }
    }
}
