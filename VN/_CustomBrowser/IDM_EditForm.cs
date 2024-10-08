using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class IDM_EditForm : Form
    {
        CustomPanelLinkEventArgs ee = null;
        DataTable dt = null;
        bool isload = false;

        public IDM_EditForm(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            ee = e;
            dt = (DataTable)e.DataGridView.DataSource;
        }

        private void set()
        {
            dataGridView1.DataSource = dt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows[i].Cells["prd_cd"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["pack_type"].ReadOnly = true;

                if (i % 2 ==  0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.AliceBlue;
                }

                dataGridView1.Rows[i].Cells["prd_cd"].Style.BackColor = Color.LightGray;
                dataGridView1.Rows[i].Cells["pack_type"].Style.BackColor = Color.LightGray;
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //dataGridView1.Columns["prd_cd"].DefaultCellStyle.BackColor = Color.DarkGray;
            //dataGridView1.Columns["pack_type"].DefaultCellStyle.BackColor = Color.DarkGray;
        }

        private void IDM_EditForm_Load(object sender, EventArgs e)
        {
            set();
            isload = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dataGridView1.Rows[dt.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DefaultCellStyle.BackColor == Color.Yellow)
            {
                dt.Rows[dataGridView1.CurrentRow.Index].Delete();
            }
            else
            {
                dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                dataGridView1.CurrentRow.ReadOnly = true;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isload)
            {
                if (dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor != Color.Yellow)
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            int T_Cnt = 0;
            int S_Cnt = 0;
            string FailList = string.Empty;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.GreenYellow)
                {
                    script = "update [VNMES].[dbo].TB_STD_MODEL set "
                            + "plant_cd = '" + dataGridView1.Rows[i].Cells["plant_cd"].Value.ToString() + "'"
                            + ",factory_cd = '" + dataGridView1.Rows[i].Cells["factory_cd"].Value.ToString() + "'"
                            + ",shop_cd = '" + dataGridView1.Rows[i].Cells["shop_cd"].Value.ToString() + "'"
                            + ",model_name = '" + dataGridView1.Rows[i].Cells["model_name"].Value.ToString() + "'"
                            + ",pack_info = '" + dataGridView1.Rows[i].Cells["pack_info"].Value.ToString() + "'"
                            + ",ic_no = '" + dataGridView1.Rows[i].Cells["ic_no"].Value.ToString() + "'"
                            + ",ic_no_sdi = '" + dataGridView1.Rows[i].Cells["ic_no_sdi"].Value.ToString() + "'"
                            + ",model_no = '" + dataGridView1.Rows[i].Cells["model_no"].Value.ToString() + "'"
                            + ",unseal_passwd1 = '" + dataGridView1.Rows[i].Cells["unseal_passwd1"].Value.ToString() + "'"
                            + ",unseal_passwd2 = '" + dataGridView1.Rows[i].Cells["unseal_passwd2"].Value.ToString() + "'"
                            + ",wakeup_volt = '" + dataGridView1.Rows[i].Cells["wakeup_volt"].Value.ToString() + "'"
                            + ",wakeup_curr = '" + dataGridView1.Rows[i].Cells["wakeup_curr"].Value.ToString() + "'"
                            + ",wakeup_time = '" + dataGridView1.Rows[i].Cells["wakeup_time"].Value.ToString() + "'"
                            + ",cpcharge_volt = '" + dataGridView1.Rows[i].Cells["cpcharge_volt"].Value.ToString() + "'"
                            + ",cpcharge_curr = '" + dataGridView1.Rows[i].Cells["cpcharge_curr"].Value.ToString() + "'"
                            + ",cpcharge_time = '" + dataGridView1.Rows[i].Cells["cpcharge_time"].Value.ToString() + "'"
                            + ",cpdischarge_curr = '" + dataGridView1.Rows[i].Cells["cpdischarge_curr"].Value.ToString() + "'"
                            + ",cpdischarge_time = '" + dataGridView1.Rows[i].Cells["cpdischarge_time"].Value.ToString() + "'"
                            + ",hpcharge_volt = '" + dataGridView1.Rows[i].Cells["hpcharge_volt"].Value.ToString() + "'"
                            + ",hpcharge_curr = '" + dataGridView1.Rows[i].Cells["hpcharge_curr"].Value.ToString() + "'"
                            + ",hpcharge_time = '" + dataGridView1.Rows[i].Cells["hpcharge_time"].Value.ToString() + "'"
                            + ",hpdischarge_curr = '" + dataGridView1.Rows[i].Cells["hpdischarge_curr"].Value.ToString() + "'"
                            + ",hpdischarge_time = '" + dataGridView1.Rows[i].Cells["hpdischarge_time"].Value.ToString() + "'"
                            + ",fetcharge_curr = '" + dataGridView1.Rows[i].Cells["fetcharge_curr"].Value.ToString() + "'"
                            + ",fetdischarge_curr = '" + dataGridView1.Rows[i].Cells["fetdischarge_curr"].Value.ToString() + "'"
                            + ",fet_count = '" + dataGridView1.Rows[i].Cells["fet_count"].Value.ToString() + "'"
                            + ",system_presence = '" + dataGridView1.Rows[i].Cells["system_presence"].Value.ToString() + "'"
                            + ",t_terminal = '" + dataGridView1.Rows[i].Cells["t_terminal"].Value.ToString() + "'"
                            + ",pcm_data_format = '" + dataGridView1.Rows[i].Cells["pcm_data_format"].Value.ToString() + "'"
                            + ",fcc_up_use = '" + dataGridView1.Rows[i].Cells["fcc_up_use"].Value.ToString() + "'"
                            + ",fcc_up_min = '" + dataGridView1.Rows[i].Cells["fcc_up_min"].Value.ToString() + "'"
                            + ",fcc_up_max = '" + dataGridView1.Rows[i].Cells["fcc_up_max"].Value.ToString() + "'"
                            + ",option1 = '" + dataGridView1.Rows[i].Cells["option1"].Value.ToString() + "'"
                            + ",option2 = '" + dataGridView1.Rows[i].Cells["option2"].Value.ToString() + "'"
                            + ",option3 = '" + dataGridView1.Rows[i].Cells["option3"].Value.ToString() + "'"
                            + ",option4 = '" + dataGridView1.Rows[i].Cells["option4"].Value.ToString() + "'"
                            + ",option5 = '" + dataGridView1.Rows[i].Cells["option5"].Value.ToString() + "'"
                            + ",option6 = '" + dataGridView1.Rows[i].Cells["option6"].Value.ToString() + "'"
                            + ",option7 = '" + dataGridView1.Rows[i].Cells["option7"].Value.ToString() + "'"
                            + ",option8 = '" + dataGridView1.Rows[i].Cells["option8"].Value.ToString() + "'"
                            + ",option9 = '" + dataGridView1.Rows[i].Cells["option9"].Value.ToString() + "'"
                            + ",option10 = '" + dataGridView1.Rows[i].Cells["option10"].Value.ToString() + "'" //쿼리에 없는것
                            + ",barcode = '" + dataGridView1.Rows[i].Cells["barcode"].Value.ToString() + "'" //쿼리에 없는것
                            + ",start_position = '" + dataGridView1.Rows[i].Cells["start_position"].Value.ToString() + "'" //쿼리에 없는것
                            + ",end_position = '" + dataGridView1.Rows[i].Cells["end_position"].Value.ToString() + "'" //쿼리에 없는것
                            + ",model_version = '" + dataGridView1.Rows[i].Cells["model_version"].Value.ToString() + "'"
                            + ",func_version = '" + dataGridView1.Rows[i].Cells["func_version"].Value.ToString() + "'" //쿼리에 없는것
                            + ",device_id1 = '" + dataGridView1.Rows[i].Cells["device_id1"].Value.ToString() + "'"
                            + ",device_id2 = '" + dataGridView1.Rows[i].Cells["device_id2"].Value.ToString() + "'"
                            + ",device_id3 = '" + dataGridView1.Rows[i].Cells["device_id3"].Value.ToString() + "'"
                            + ",device_id4 = '" + dataGridView1.Rows[i].Cells["device_id4"].Value.ToString() + "'"
                            + ",device_id5 = '" + dataGridView1.Rows[i].Cells["device_id5"].Value.ToString() + "'"
                            + ",max_voltage = '" + dataGridView1.Rows[i].Cells["max_voltage"].Value.ToString() + "'"
                            + ",max_current = '" + dataGridView1.Rows[i].Cells["max_current"].Value.ToString() + "'"
                            + ",work_date = '" + dataGridView1.Rows[i].Cells["work_date"].Value.ToString() + "'" //쿼리에 없는것
                            + ",serial_no = '" + dataGridView1.Rows[i].Cells["serial_no"].Value.ToString() + "'" //쿼리에 없는것
                            + ",del_sw = '" + dataGridView1.Rows[i].Cells["del_sw"].Value.ToString() + "'"
                            + ",worker_id = '" + dataGridView1.Rows[i].Cells["worker_id"].Value.ToString() + "'"
                            + ",trans_src = '" + dataGridView1.Rows[i].Cells["trans_src"].Value.ToString() + "'"
                            + ",update_date = '" + dataGridView1.Rows[i].Cells["update_date"].Value.ToString() + "'"
                            + ",insert_date = '" + dataGridView1.Rows[i].Cells["insert_date"].Value.ToString() + "'"
                            + ",hp_pattern = '" + dataGridView1.Rows[i].Cells["hp_pattern"].Value.ToString() + "'"
                            + ",rout_cd = '" + dataGridView1.Rows[i].Cells["rout_cd"].Value.ToString() + "'"
                            + ",sp_worker_id = '" + dataGridView1.Rows[i].Cells["sp_worker_id"].Value.ToString() + "'" //쿼리에 없는것
                            + ",sp_update_date = '" + dataGridView1.Rows[i].Cells["sp_update_date"].Value.ToString() + "'" //쿼리에 없는것
                            + ",natural_name = '" + dataGridView1.Rows[i].Cells["natural_name"].Value.ToString() + "'" //쿼리에 없는것
                            + ",chipset_id = '" + dataGridView1.Rows[i].Cells["chipset_id"].Value.ToString() + "'" //쿼리에 없는것
                            + ",model_type_id = '" + dataGridView1.Rows[i].Cells["model_type_id"].Value.ToString() + "'" //쿼리에 없는것
                            + ",hp_version = '" + dataGridView1.Rows[i].Cells["hp_version"].Value.ToString() + "'" //쿼리에 없는것
                            + ",laser_fg = '" + dataGridView1.Rows[i].Cells["laser_fg"].Value.ToString() + "'"
                            + ",customer_box_label_fg = '" + dataGridView1.Rows[i].Cells["customer_box_label_fg"].Value.ToString() + "'" //쿼리에 없는것
                            + ",customer_label_name = '" + dataGridView1.Rows[i].Cells["customer_label_name"].Value.ToString() + "'" //쿼리에 없는것
                            + ",sorting_check = '" + dataGridView1.Rows[i].Cells["sorting_check"].Value.ToString() + "'" //쿼리에 없는것
                            + ",batch_rank_check = '" + dataGridView1.Rows[i].Cells["batch_rank_check"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr3_result = '" + dataGridView1.Rows[i].Cells["dr3_result"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr3_remark = '" + dataGridView1.Rows[i].Cells["dr3_remark"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr3_user = '" + dataGridView1.Rows[i].Cells["dr3_user"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr3_time = '" + dataGridView1.Rows[i].Cells["dr3_time"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr4_result = '" + dataGridView1.Rows[i].Cells["dr4_result"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr4_remark = '" + dataGridView1.Rows[i].Cells["dr4_remark"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr4_user = '" + dataGridView1.Rows[i].Cells["dr4_user"].Value.ToString() + "'" //쿼리에 없는것
                            + ",dr4_time = '" + dataGridView1.Rows[i].Cells["dr4_time"].Value.ToString() + "'" //쿼리에 없는것
                            + ",remark = '" + dataGridView1.Rows[i].Cells["remark"].Value.ToString() + "'" //쿼리에 없는것
                            + ",spec_check_flag = '" + dataGridView1.Rows[i].Cells["spec_check_flag"].Value.ToString() + "'" //쿼리에 없는것
                            + ",spec_check_seq = '" + dataGridView1.Rows[i].Cells["spec_check_seq"].Value.ToString() + "'" //쿼리에 없는것
                            + ",spec_check_testdata = '" + dataGridView1.Rows[i].Cells["spec_check_testdata"].Value.ToString() + "'" //쿼리에 없는것
                            + ",cell_lmt = '" + dataGridView1.Rows[i].Cells["cell_lmt"].Value.ToString() + "'" //쿼리에 없는것
                            + ",cell_code_name = '" + dataGridView1.Rows[i].Cells["cell_code_name"].Value.ToString() + "'" //쿼리에 없는것
                            + ",cell_code_chk_fg = '" + dataGridView1.Rows[i].Cells["cell_code_chk_fg"].Value.ToString() + "'" //쿼리에 없는것
                            + "where prd_cd = '" + dataGridView1.Rows[i].Cells["prd_cd"].Value.ToString() + "' and pack_type = '" + dataGridView1.Rows[i].Cells["pack_type"].Value.ToString() + "'";
                }
                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                {
                    script = "delete from [VNMES].[dbo].TB_STD_MODEL where prd_cd = '" + dataGridView1.Rows[i].Cells["prd_cd"].Value.ToString() + "' and pack_type = '" + dataGridView1.Rows[i].Cells["pack_type"].Value.ToString() + "'";
                }
                else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Yellow)
                {
                    script = "insert into [VNMES].[dbo].TB_STD_MODEL"
                           + "(plant_cd"
                           + ",factory_cd"
                           + ",shop_cd"
                           + ",prd_cd"
                           + ",model_name"
                           + ",pack_type"
                           + ",pack_info"
                           + ",ic_no"
                           + ",ic_no_sdi"
                           + ",model_no"
                           + ",unseal_passwd1"
                           + ",unseal_passwd2"
                           + ",wakeup_volt"
                           + ",wakeup_curr"
                           + ",wakeup_time"
                           + ",cpcharge_volt"
                           + ",cpcharge_curr"
                           + ",cpcharge_time"
                           + ",cpdischarge_curr"
                           + ",cpdischarge_time"
                           + ",hpcharge_volt"
                           + ",hpcharge_curr"
                           + ",hpcharge_time"
                           + ",hpdischarge_curr"
                           + ",hpdischarge_time"
                           + ",fetcharge_curr"
                           + ",fetdischarge_curr"
                           + ",fet_count"
                           + ",system_presence"
                           + ",t_terminal"
                           + ",pcm_data_format"
                           + ",fcc_up_use"
                           + ",fcc_up_min"
                           + ",fcc_up_max"
                           + ",option1"
                           + ",option2"
                           + ",option3"
                           + ",option4"
                           + ",option5"
                           + ",option6"
                           + ",option7"
                           + ",option8"
                           + ",option9"
                           + ",option10"
                           + ",barcode"
                           + ",start_position"
                           + ",end_position"
                           + ",model_version"
                           + ",func_version"
                           + ",device_id1"
                           + ",device_id2"
                           + ",device_id3"
                           + ",device_id4"
                           + ",device_id5"
                           + ",max_voltage"
                           + ",max_current"
                           + ",work_date"
                           + ",serial_no"
                           + ",del_sw"
                           + ",worker_id"
                           + ",trans_src"
                           + ",update_date"
                           + ",insert_date"
                           + ",hp_pattern"
                           + ",rout_cd"
                           + ",sp_worker_id"
                           + ",sp_update_date"
                           + ",natural_name"
                           + ",chipset_id"
                           + ",model_type_id"
                           + ",hp_version"
                           + ",laser_fg"
                           + ",customer_box_label_fg"
                           + ",customer_label_name"
                           + ",sorting_check"
                           + ",batch_rank_check"
                           + ",dr3_result"
                           + ",dr3_remark"
                           + ",dr3_user"
                           + ",dr3_time"
                           + ",dr4_result"
                           + ",dr4_remark"
                           + ",dr4_user"
                           + ",dr4_time"
                           + ",remark"
                           + ",spec_check_flag"
                           + ",spec_check_seq"
                           + ",spec_check_testdata"
                           + ",cell_lmt"
                           + ",cell_code_name"
                           + ",cell_code_chk_fg)"
                     + "values"
                           + "('" + dataGridView1.Rows[i].Cells["plant_cd"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["factory_cd"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["shop_cd"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["prd_cd"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["model_name"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["pack_type"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["pack_info"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["ic_no"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["ic_no_sdi"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["model_no"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["unseal_passwd1"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["unseal_passwd2"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["wakeup_volt"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["wakeup_volt"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["wakeup_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["wakeup_curr"].Value.ToString() + "' end "
                           + ",'" + dataGridView1.Rows[i].Cells["wakeup_time"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["cpcharge_volt"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["cpcharge_volt"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["cpcharge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["cpcharge_curr"].Value.ToString() + "' end " 
                           + ",'" + dataGridView1.Rows[i].Cells["cpcharge_time"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["cpdischarge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["cpdischarge_curr"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["cpdischarge_time"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["cpdischarge_time"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["hpcharge_volt"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["hpcharge_volt"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["hpcharge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["hpcharge_curr"].Value.ToString() + "' end "   
                           + ",'" + dataGridView1.Rows[i].Cells["hpcharge_time"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["hpdischarge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["hpdischarge_curr"].Value.ToString() + "' end "  
                           + ",'" + dataGridView1.Rows[i].Cells["hpdischarge_time"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["fetcharge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["fetcharge_curr"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["fetdischarge_curr"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["fetdischarge_curr"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["fet_count"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["fet_count"].Value.ToString() + "' end "  
                           + ",'" + dataGridView1.Rows[i].Cells["system_presence"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["t_terminal"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["pcm_data_format"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["fcc_up_use"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["fcc_up_min"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["fcc_up_max"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option1"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option2"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option3"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option4"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option5"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option6"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option7"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option8"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option9"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["option10"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["barcode"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["start_position"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["end_position"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["model_version"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["func_version"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["device_id1"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["device_id2"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["device_id3"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["device_id4"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["device_id5"].Value.ToString() + "'"
                           + ",case when '" + dataGridView1.Rows[i].Cells["max_voltage"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["max_voltage"].Value.ToString() + "' end "
                           + ",case when '" + dataGridView1.Rows[i].Cells["max_current"].Value.ToString() + "' = '' then Null else '" + dataGridView1.Rows[i].Cells["max_current"].Value.ToString() + "' end "  
                           + ",'" + dataGridView1.Rows[i].Cells["work_date"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["serial_no"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["del_sw"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["worker_id"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["trans_src"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["update_date"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["insert_date"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["hp_pattern"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["rout_cd"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["sp_worker_id"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["sp_update_date"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["natural_name"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["chipset_id"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["model_type_id"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["hp_version"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["laser_fg"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["customer_box_label_fg"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["customer_label_name"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["sorting_check"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["batch_rank_check"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr3_result"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr3_remark"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr3_user"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr3_time"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr4_result"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr4_remark"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr4_user"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["dr4_time"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["remark"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["spec_check_flag"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["spec_check_seq"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["spec_check_testdata"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["cell_lmt"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["cell_code_name"].Value.ToString() + "'"
                           + ",'" + dataGridView1.Rows[i].Cells["cell_code_chk_fg"].Value.ToString() + "')";
                }

                if (!string.IsNullOrEmpty(script))
                {
                    
                    T_Cnt = T_Cnt + 1;

                    int Result = 0;
                    try
                    {
                        Result = ee.DbAccess.ExecuteQuery(script);
                    }
                    catch (Exception ex)
                    {
                        WiseM.MessageBox.Show(ex.Message, "Error", MessageBoxIcon.Information);
                    }

                    if (Result == 1)
                    {
                        S_Cnt = S_Cnt + 1;
                    }
                    else
                    {
                        FailList += "prd_cd : " + dataGridView1.Rows[i].Cells["prd_cd"].Value.ToString() + ", pack_type : " + dataGridView1.Rows[i].Cells["pack_type"].Value.ToString() + "\r";
                    }

                    script = string.Empty;
                }
            }

            WiseM.MessageBox.Show("Try : " + T_Cnt.ToString() + ", Success : " + S_Cnt.ToString() + "\rFailList \r" + FailList, "Information", MessageBoxIcon.Information);

            Close();
        }

        private void IDM_EditForm_SizeChanged(object sender, EventArgs e)
        {
            //WiseM.MessageBox.Show(splitContainer1.SplitterDistance.ToString(), "Information", MessageBoxIcon.Information);
            splitContainer1.SplitterDistance = 46;
        }
    }
}
