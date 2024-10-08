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

namespace WiseM.Browser
{
    public partial class DataSearching : SkinForm
    {
        private CustomPanelLinkEventArgs e = null;
        private string DbName = string.Empty;
        public DataSearching(CustomPanelLinkEventArgs e)
        {
            InitializeComponent();
            this.e = e;
            Process();
        }

        private void Process()
        {
            this.Check_HardPack.Checked = false;
            this.Check_CorePack.Checked = false;
            this.Check_ElentecSn.Checked = false;
            this.Check_PcmCode.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox1.Checked = false;
            this.List_HardPack.Items.Clear();
            this.List_CorePack.Items.Clear();
            this.List_ElentecSn.Items.Clear();
            this.List_PcmSn.Items.Clear();
            this.List_Serial.Items.Clear();
            this.List_Product.Items.Clear();
            this.TB_HardPackCode.Text = string.Empty;
            this.TB_CorePackCode.Text = string.Empty;
            this.TB_ElentecSnCode.Text = string.Empty;
            this.TB_PcmNoCode.Text = string.Empty;
            this.TB_SinglePackNo.Text = string.Empty;
            this.TB_ProductCode.Text = string.Empty;
            this.Check_HardPack.Enabled = false;
            this.Check_CorePack.Enabled = false;
            this.Check_ElentecSn.Enabled = false;
            this.Check_PcmCode.Enabled = false;
            this.checkBox1.Enabled = true;
            this.checkBox4.Enabled = true;
            this.TB_HardPackCode.Enabled = false;
            this.TB_CorePackCode.Enabled = false;
            this.TB_PcmNoCode.Enabled = false;
            this.TB_ElentecSnCode.Enabled = false;
            this.TB_ProductCode.Enabled = false;
            this.TB_SinglePackNo.Enabled = false;
            this.List_HardPack.Enabled = false;
            this.List_CorePack.Enabled = false;
            this.List_ElentecSn.Enabled = false;
            this.List_PcmSn.Enabled = false;
            this.List_Product.Enabled = false;
            this.List_Serial.Enabled = false;
            
            this.RowDataMonth();
        }

        private void RowDataMonth()
        {
            this.Combo_RawDateMonth.Items.Clear();
            this.Combo_RawDateMonth.Items.Add("01");
            this.Combo_RawDateMonth.Items.Add("02");
            this.Combo_RawDateMonth.Items.Add("03");
            this.Combo_RawDateMonth.Items.Add("04");
            this.Combo_RawDateMonth.Items.Add("05");
            this.Combo_RawDateMonth.Items.Add("06");
            this.Combo_RawDateMonth.Items.Add("07");
            this.Combo_RawDateMonth.Items.Add("08");
            this.Combo_RawDateMonth.Items.Add("09");
            this.Combo_RawDateMonth.Items.Add("10");
            this.Combo_RawDateMonth.Items.Add("11");
            this.Combo_RawDateMonth.Items.Add("12");

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked == true)
            {

                this.TB_HardPackCode.Text = string.Empty;
                this.TB_CorePackCode.Text = string.Empty;
                this.TB_ElentecSnCode.Text = string.Empty;
                this.TB_PcmNoCode.Text = string.Empty;
                this.Check_HardPack.Enabled = false;
                this.Check_CorePack.Enabled = true;
                this.Check_ElentecSn.Enabled = true;
                this.Check_PcmCode.Enabled = true;
                this.TB_HardPackCode.Enabled = false;
                this.TB_CorePackCode.Enabled = true;
                this.TB_PcmNoCode.Enabled = true;
                this.TB_ElentecSnCode.Enabled = true;
                this.List_HardPack.Enabled = false;
                this.List_CorePack.Enabled = true;
                this.List_ElentecSn.Enabled = true;
                this.List_PcmSn.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked == true)
            {
                
                this.TB_HardPackCode.Text = string.Empty;
                this.TB_CorePackCode.Text = string.Empty;
                this.TB_ElentecSnCode.Text = string.Empty;
                this.TB_PcmNoCode.Text = string.Empty;
                this.Check_HardPack.Enabled = true;
                this.Check_CorePack.Enabled = false;
                this.Check_ElentecSn.Enabled = true;
                this.Check_PcmCode.Enabled = true;
                this.TB_HardPackCode.Enabled = true;
                this.TB_CorePackCode.Enabled = false;
                this.TB_PcmNoCode.Enabled = true;
                this.TB_ElentecSnCode.Enabled = true;
                this.List_HardPack.Enabled = true;
                this.List_CorePack.Enabled = false;
                this.List_ElentecSn.Enabled = true;
                this.List_PcmSn.Enabled = true;
            }
        }

        private void Check_HardPack_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Check_HardPack.Checked == true)
            {
                this.List_CorePack.Items.Clear();
                this.List_ElentecSn.Items.Clear();
                this.List_PcmSn.Items.Clear();
                this.TB_PcmNoCode.Enabled = false;
                this.List_PcmSn.Enabled = false;
                this.TB_ElentecSnCode.Enabled = false;
                this.List_ElentecSn.Enabled = false;
                this.TB_CorePackCode.Enabled = false;
                this.List_CorePack.Enabled = false;
                this.TB_HardPackCode.Enabled = true;
                this.List_HardPack.Enabled = true;
                this.Check_ElentecSn.Checked = false;
                this.Check_PcmCode.Checked = false;
                this.Check_CorePack.Checked = false;
            }
            else
            {
                this.TB_HardPackCode.Enabled = false;
                this.List_HardPack.Enabled = false;
            }
        }

        private void Check_CorePack_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Check_CorePack.Checked == true)
            {
                this.List_HardPack.Items.Clear();
                this.List_ElentecSn.Items.Clear();
                this.List_PcmSn.Items.Clear();
                this.TB_PcmNoCode.Enabled = false;
                this.List_PcmSn.Enabled = false;
                this.TB_ElentecSnCode.Enabled = false;
                this.List_ElentecSn.Enabled = false;
                this.TB_CorePackCode.Enabled = true;
                this.List_CorePack.Enabled = true;
                this.TB_HardPackCode.Enabled = false;
                this.List_HardPack.Enabled = false;
                this.Check_ElentecSn.Checked = false;
                this.Check_PcmCode.Checked = false;
                this.Check_HardPack.Checked = false;
            }
            else
            {
                this.TB_CorePackCode.Enabled = false;
                this.List_CorePack.Enabled = false;
            }
        }

        private void Check_ElentecSn_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Check_ElentecSn.Checked == true)
            {
                this.List_CorePack.Items.Clear();
                this.List_HardPack.Items.Clear();
                this.List_PcmSn.Items.Clear();
                this.TB_PcmNoCode.Enabled = false;
                this.List_PcmSn.Enabled = false;
                this.TB_ElentecSnCode.Enabled = true;
                this.List_ElentecSn.Enabled = true;
                this.TB_CorePackCode.Enabled = false;
                this.List_CorePack.Enabled = false;
                this.TB_HardPackCode.Enabled = false;
                this.List_HardPack.Enabled = false;
                this.Check_CorePack.Checked = false;
                this.Check_PcmCode.Checked = false;
                this.Check_HardPack.Checked = false;
            }
            else
            {
                this.TB_ElentecSnCode.Enabled = false;
                this.List_ElentecSn.Enabled = false;
            }
        }

        private void Check_PcmCode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Check_PcmCode.Checked == true)
            {
                this.List_CorePack.Items.Clear();
                this.List_ElentecSn.Items.Clear();
                this.List_HardPack.Items.Clear();
                this.TB_PcmNoCode.Enabled = true;
                this.List_PcmSn.Enabled = true;
                this.TB_ElentecSnCode.Enabled = false;
                this.List_ElentecSn.Enabled = false;
                this.TB_CorePackCode.Enabled = false;
                this.List_CorePack.Enabled = false;
                this.TB_HardPackCode.Enabled = false;
                this.List_HardPack.Enabled = false;
                this.Check_CorePack.Checked = false;
                this.Check_ElentecSn.Checked = false;
                this.Check_HardPack.Checked = false;
            }
            else
            {
                this.TB_PcmNoCode.Enabled = false;
                this.List_PcmSn.Enabled = false;
            }
        }

        private void TB_HardPackCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_HardPack.Items.Add(this.TB_HardPackCode.Text.Replace(" ", ""));
                this.TB_HardPackCode.Text = string.Empty;
                this.TB_HardPackCode.Focus();
            }
        }

        private void TB_CorePackCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_CorePack.Items.Add(this.TB_CorePackCode.Text.Replace(" ", ""));
                this.TB_CorePackCode.Text = string.Empty;
                this.TB_CorePackCode.Focus();
            }
        }

        private void TB_ElentecSnCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_ElentecSn.Items.Add(this.TB_ElentecSnCode.Text.Replace(" ", ""));
                this.TB_ElentecSnCode.Text = string.Empty;
                this.TB_ElentecSnCode.Focus();
            }
        }

        private void TB_PcmNoCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_PcmSn.Items.Add(this.TB_PcmNoCode.Text.Replace(" ", ""));
                this.TB_PcmNoCode.Text = string.Empty;
                this.TB_PcmNoCode.Focus();
            }
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Combo_RawDateMonth.Text) || string.IsNullOrEmpty(this.Combo_RawDateYear.Text))
            {
                WiseM.MessageBox.Show("Error, is not choice Month!", "Warning", MessageBoxIcon.Information);                
                return;
            }
            string DbSearch = string.Empty;
            if (this.radioButton2.Checked == true)
            {
                DbName = " Select HpBcn SerialNo, _TransDate , CellVtg , IR  , PcmSN, PcmDate , PcmMaker , ElentecSn, ElentecDate , Model , Line , "
                           + " Judgment ,  ErrorItem, Cell_Voltage_Difference,Discharging_Current_Accuracy_MTap,Charging_Current_Accuracy_MTap,FCC,_Source , _RawData From ElentecSnDas" + this.Combo_RawDateYear.Text + ".dbo.CollectHHP";
            }
            else if (this.radioButton1.Checked == true)
            {
                DbName = " Select HpBcn SerialNo, _TransDate , Vtg CellVtg , IR  , PcmSN, PcmDate , PcmMaker , ElentecSn, ElentecDate , Model , Line , "
                               + " Judgment , S13 ErrorName, S14,S15,S16,S17,_Source , _RawData From ElentecSnDas" + this.Combo_RawDateYear.Text + ".dbo.CollectHCP";
            }

            DbSearch = DbName + this.Combo_RawDateMonth.Text + " Where ";

            if (this.List_HardPack.Items.Count != 0)
            {
                string HardPackBarCode = string.Empty;
                for (int H = 0; H < this.List_HardPack.Items.Count; H++)
                {
                    HardPackBarCode += "'" + this.List_HardPack.Items[H].ToString().Trim() + "',"; 
                }
                HardPackBarCode += "'a'";
                DbSearch = DbSearch + "HpBcn in (" + HardPackBarCode + ")"; 
            }

            if (this.List_CorePack.Items.Count != 0)
            {
                string CorePackCode = string.Empty;
                for (int H = 0; H < this.List_CorePack.Items.Count; H++)
                {
                    CorePackCode += "'" + this.List_CorePack.Items[H].ToString().Trim() + "',";
                }
                CorePackCode += "'a'";
                DbSearch = DbSearch + "Cpcode in (" + CorePackCode + ")";
            }

            if (this.List_ElentecSn.Items.Count != 0)
            {
                string ElentecSn = string.Empty;
                for (int H = 0; H < this.List_ElentecSn.Items.Count; H++)
                {
                    ElentecSn += "'" + this.List_ElentecSn.Items[H].ToString().Trim() + "',";
                }
                ElentecSn += "'a'";
                DbSearch = DbSearch + "ElentecSn + ElentecDate in (" + ElentecSn + ")";
            }

            if (this.List_PcmSn.Items.Count != 0)
            {
                string PcmSn = string.Empty;
                for (int H = 0; H < this.List_PcmSn.Items.Count; H++)
                {
                    PcmSn += "'" + this.List_PcmSn.Items[H].ToString().Trim() + "',";
                }
                PcmSn += "'a'";
                DbSearch = DbSearch + "PcmSn + PcmDate in (" + PcmSn + ")";
            }

            DataTable dt = new DataTable();
            dt = DbAccess.Default.GetDataTable(DbSearch);

            if (dt.Rows.Count == 0)
            {
                WiseM.MessageBox.Show("Error, is not data!", "Warning", MessageBoxIcon.Information);
                return;
            }

            this.dataGridView1.Rows.Clear();
            if (this.radioButton2.Checked == true)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.dataGridView1.Rows.Add(1);
                    DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                    if (this.dataGridView1.Columns.Count < 1) return;

                    dr.Cells["SerialNo"].Value = dt.Rows[i]["SerialNo"].ToString().Trim();
                    dr.Cells["_TransDate"].Value = Convert.ToDateTime(dt.Rows[i]["_TransDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                    dr.Cells["CellVtg"].Value = dt.Rows[i]["CellVtg"].ToString().Trim();
                    dr.Cells["IR"].Value = dt.Rows[i]["IR"].ToString().Trim();
                    dr.Cells["PcmSN"].Value = dt.Rows[i]["PcmSN"].ToString().Trim();
                    dr.Cells["PcmDate"].Value = dt.Rows[i]["PcmDate"].ToString().Trim();
                    dr.Cells["PcmMaker"].Value = dt.Rows[i]["PcmMaker"].ToString().Trim();
                    dr.Cells["ElentecSn"].Value = dt.Rows[i]["ElentecSn"].ToString().Trim();
                    dr.Cells["ElentecDate"].Value = dt.Rows[i]["ElentecDate"].ToString().Trim();
                    dr.Cells["Model"].Value = dt.Rows[i]["Model"].ToString().Trim();
                    dr.Cells["Line"].Value = dt.Rows[i]["Line"].ToString().Trim();
                    dr.Cells["Judgment"].Value = dt.Rows[i]["Judgment"].ToString().Trim();
                    dr.Cells["ErrorName"].Value = dt.Rows[i]["ErrorItem"].ToString().Trim();
                    dr.Cells["S14"].Value = dt.Rows[i]["Cell_Voltage_Difference"].ToString().Trim();
                    dr.Cells["S15"].Value = dt.Rows[i]["Discharging_Current_Accuracy_MTap"].ToString().Trim();
                    dr.Cells["S16"].Value = dt.Rows[i]["Charging_Current_Accuracy_MTap"].ToString().Trim();
                    dr.Cells["S17"].Value = dt.Rows[i]["FCC"].ToString().Trim();
                    dr.Cells["_Source"].Value = dt.Rows[i]["_Source"].ToString().Trim();
                    dr.Cells["_RawData"].Value = dt.Rows[i]["_RawData"].ToString().Trim();


                }
            }
            else if (this.radioButton1.Checked == true)
            { 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.dataGridView1.Rows.Add(1);
                    DataGridViewRow dr = this.dataGridView1.Rows[this.dataGridView1.RowCount - 1];
                    if (this.dataGridView1.Columns.Count < 1) return;

                    dr.Cells["SerialNo"].Value = dt.Rows[i]["SerialNo"].ToString().Trim();
                    dr.Cells["_TransDate"].Value = Convert.ToDateTime(dt.Rows[i]["_TransDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                    dr.Cells["CellVtg"].Value = dt.Rows[i]["CellVtg"].ToString().Trim();
                    dr.Cells["IR"].Value = dt.Rows[i]["IR"].ToString().Trim();
                    dr.Cells["PcmSN"].Value = dt.Rows[i]["PcmSN"].ToString().Trim();
                    dr.Cells["PcmDate"].Value = dt.Rows[i]["PcmDate"].ToString().Trim();
                    dr.Cells["PcmMaker"].Value = dt.Rows[i]["PcmMaker"].ToString().Trim();
                    dr.Cells["ElentecSn"].Value = dt.Rows[i]["ElentecSn"].ToString().Trim();
                    dr.Cells["ElentecDate"].Value = dt.Rows[i]["ElentecDate"].ToString().Trim();
                    dr.Cells["Model"].Value = dt.Rows[i]["Model"].ToString().Trim();
                    dr.Cells["Line"].Value = dt.Rows[i]["Line"].ToString().Trim();
                    dr.Cells["Judgment"].Value = dt.Rows[i]["Judgment"].ToString().Trim();
                    dr.Cells["ErrorName"].Value = dt.Rows[i]["ErrorName"].ToString().Trim();
                    dr.Cells["S14"].Value = dt.Rows[i]["S14"].ToString().Trim();
                    dr.Cells["S15"].Value = dt.Rows[i]["S15"].ToString().Trim();
                    dr.Cells["S16"].Value = dt.Rows[i]["S16"].ToString().Trim();
                    dr.Cells["S17"].Value = dt.Rows[i]["S17"].ToString().Trim();
                    dr.Cells["_Source"].Value = dt.Rows[i]["_Source"].ToString().Trim();
                    dr.Cells["_RawData"].Value = dt.Rows[i]["_RawData"].ToString().Trim();


                }
            
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Process();
        }

        private void TB_SinglePackNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_Serial.Items.Add(this.TB_SinglePackNo.Text);
                this.TB_SinglePackNo.Text = string.Empty;
                this.TB_SinglePackNo.Focus();
            }
        }

        private void Btn_HSPSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Combo_HSPYear.Text) || string.IsNullOrEmpty(this.Combo_Month.Text))
            {
                WiseM.MessageBox.Show("Error, is not choice Month!", "Warning", MessageBoxIcon.Information);
                return;
            }
            string DbSearch = string.Empty;
           
            DbName = " select _TransDate , Code , OCV , OCV_Min , OCV_Max , IR , IR_Max , IR_Min , Judgement  , Created , A_Check, B_Check , C_Check , _Source , _RawCols , _RawData , _Updated "
                   + " From  ElentecSnDas" + this.Combo_HSPYear.Text + ".dbo.CollectHSP";
           

            DbSearch = DbName + this.Combo_Month.Text + " Where ";

            if (this.List_Serial.Items.Count != 0)
            {
                string SinglePackSerial = string.Empty;
                for (int H = 0; H < this.List_Serial.Items.Count; H++)
                {
                    SinglePackSerial += "'" + this.List_Serial.Items[H].ToString().Trim() + "',";
                }
                SinglePackSerial += "'a'";
                DbSearch = DbSearch + "Code in (" + SinglePackSerial + ")";
            }

            if (this.List_Product.Items.Count != 0)
            {
                string ProductCode = string.Empty;
                for (int H = 0; H < this.List_Product.Items.Count; H++)
                {
                    ProductCode += "'" + this.List_Product.Items[H].ToString().Trim() + "',";
                }
                ProductCode += "'a'";
                DbSearch = DbSearch + "SUBSTRING(Code,5,3)  in (" + ProductCode + ")";
            }

            DataTable dt = new DataTable();
            dt = DbAccess.Default.GetDataTable(DbSearch);

            if (dt.Rows.Count == 0)
            {
                WiseM.MessageBox.Show("Error, is not data!", "Warning", MessageBoxIcon.Information);
                return;
            }

            this.dataGridView2.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.dataGridView2.Rows.Add(1);
                DataGridViewRow dr = this.dataGridView2.Rows[this.dataGridView2.RowCount - 1];
                if (this.dataGridView2.Columns.Count < 1) return;

                dr.Cells["Code"].Value = dt.Rows[i]["Code"].ToString().Trim();
                dr.Cells["TransDate"].Value = Convert.ToDateTime(dt.Rows[i]["_TransDate"].ToString().Trim()).ToString("yyyy-MM-dd");
                dr.Cells["OCV"].Value = dt.Rows[i]["OCV"].ToString().Trim();
                dr.Cells["OCV_Max"].Value = dt.Rows[i]["OCV_Max"].ToString().Trim();
                dr.Cells["OCV_Min"].Value = dt.Rows[i]["OCV_Min"].ToString().Trim();
                dr.Cells["IR_Hsp"].Value = dt.Rows[i]["IR"].ToString().Trim();
                dr.Cells["IR_Max"].Value = dt.Rows[i]["IR_Max"].ToString().Trim();
                dr.Cells["IR_Min"].Value = dt.Rows[i]["IR_Min"].ToString().Trim();
                dr.Cells["Judgement"].Value = dt.Rows[i]["Judgement"].ToString().Trim();
                dr.Cells["A_Check"].Value = dt.Rows[i]["A_Check"].ToString().Trim();
                dr.Cells["B_Check"].Value = dt.Rows[i]["B_Check"].ToString().Trim();
                dr.Cells["C_Check"].Value = dt.Rows[i]["C_Check"].ToString().Trim();
                dr.Cells["_Updated"].Value = Convert.ToDateTime(dt.Rows[i]["_Updated"].ToString().Trim()).ToString("yyyy-MM-dd");
                dr.Cells["RawData"].Value = dt.Rows[i]["_RawData"].ToString().Trim();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Process();
        }

        private void TB_ProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.List_Product.Items.Add(this.TB_ProductCode.Text);
                this.TB_ProductCode.Text = string.Empty;
                this.TB_ProductCode.Focus();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked == true)
            {
                this.List_Product.Items.Clear();              
                this.TB_ProductCode.Enabled = false;
                this.List_Product.Enabled = false;
               
                this.List_Serial.Enabled = true;
                this.TB_SinglePackNo.Enabled = true;
                this.TB_SinglePackNo.Focus();
                this.checkBox1.Checked = false;
            
            }
            else
            {
                this.TB_SinglePackNo.Enabled = false;
                this.List_Serial.Items.Clear();
                this.List_Serial.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.List_Serial.Items.Clear();
                this.TB_SinglePackNo.Enabled = false;
                this.List_Serial.Enabled = false;

                this.TB_ProductCode.Enabled = true;
                this.List_Product.Enabled = true;
                this.TB_ProductCode.Focus();
                this.checkBox4.Checked = false;

            }
            else
            {
                this.TB_ProductCode.Enabled = false;
                this.List_Product.Items.Clear();
                this.List_Product.Enabled = false;
            }
        }



    }
}
