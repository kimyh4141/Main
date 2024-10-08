using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Client
{
    public partial class ConfirmType : Form
    {
        string _Material = WbtCustomService.ActiveValues.Material;
        string _Workorder = WbtCustomService.ActiveValues.WorkOrder;
        string _type = "";
        string _date = "";

        public ConfirmType()
        {
            InitializeComponent();

            string Q;
            Q = $"SELECT Type FROM Material WHERE Material = '{_Material}'";

            DataTable dt = DbAccess.Default.GetDataTable(Q);

            if (dt.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("There is no Material");
                this.Close();
            }
            else if (string.IsNullOrEmpty(dt.Rows[0]["Type"].ToString()))
            {
                MessageBox.ShowCaption("MES系统中未定义PCB类型（SINGLE / DOUBLE）。您必须在 MES Browser 程序中输入此项目的 PCB 类型[" + _Material + "]。\n\n" +
                            "PCB type (SINGLE / DOUBLE) is not defined in MES system.\n\n" +
                            "You have to enter the PCB type for this item [ " + _Material + " ] at MES Browser program.", "Error", MessageBoxIcon.Error);
                this.Close();
            }
            
            string type = dt.Rows[0]["Type"].ToString();

            if (type == "SINGLE") rb_Single.Checked = true;
            else rb_double.Checked = true;
        }

        public string GetItem(string name)
        {
            switch (name)
            {
                case "Type":
                    return _type;
                case "Date":
                    return _date;
                default:
                    return "";
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (rb_Single.Checked) _type = "SINGLE";
            else _type = "DOUBLE";

            _date = dtp_date.Value.ToString("yyyyMMdd");

            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void ConfirmType_Load(object sender, EventArgs e)
        {
            dtp_date.Value = DateTime.Today;
        }
    }
}
