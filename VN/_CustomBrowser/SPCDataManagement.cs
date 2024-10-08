using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiseM.Forms;
using Janus.Windows.EditControls;

namespace SPCDataManagement
{
    public partial class SPCDataManagement : SkinForm
    {
        //private CustomPanelLinkEventArgs e = null;
        private string[] spcType = {"Length", "Width", "Thickness"};
        private int selectedTypeNum = 0;

        public SPCDataManagement()
        {
            InitializeComponent();
            this.BindingComboBox();
            this.uiRadioButtonLeft.Select();
            this.uiRadioButtonLength.Select();
            this.uiComboBoxMaterialPACK.SelectedIndex = 0;
            //this.e = e;
        }

        private void BindingComboBox()
        {
            string selectQuery = "Select Material, Product From MaterialMapping Where Status = 1 Order by Product ";
            DataTable materialDt = new DataTable();
            materialDt = WiseM.Data.DbAccess.Default.GetDataTable(selectQuery);
            if (materialDt != null || materialDt.Rows.Count > 0)
            {
                foreach (DataRow dr in materialDt.Rows)
                {
                    this.uiComboBoxMaterialPACK.Items.Add(dr["Product"].ToString(), dr["Material"]);
                }
            }
        }

        private void uiCheckBoxMaterial_CheckedChanged(object sender, EventArgs e)
        {
            this.DataGridViewUseCheckBox(this.dataGridViewSPC, "_check", this.uiCheckBoxSPC);
        }

        private void DataGridViewUseCheckBox(DataGridView selectedGridView, string checkBoxColumnName, UICheckBox selectedAllCheckBox)
        {
            if (selectedGridView.Rows.Count > 0)
            {
                if (selectedAllCheckBox.Checked == true)
                {
                    foreach (DataGridViewRow dr in selectedGridView.Rows)
                    {
                        dr.Cells[checkBoxColumnName].Value = true;
                    }
                }
                else if (selectedAllCheckBox.Checked == false)
                {
                    foreach (DataGridViewRow dr in selectedGridView.Rows)
                    {
                        dr.Cells[checkBoxColumnName].Value = false;
                    }
                }
            }
        }

        private void InsertQuery(string lot, string seq, string x1, string x2, string x3, string x4, string x5, string xBar, string R)
        {
            string query = " INSERT INTO [SpcHist] ";
            query += " ([SpcId], [SpcType], [Product], [Lot], [Seq], [X1], [X2], [X3], [X4], [X5], [xBar], [R], [Updated]) ";
            query += " VALUES ";
            query += " ( 'Pack', '" + this.spcType[selectedTypeNum] + "' , '" + this.uiComboBoxMaterialPACK.SelectedItem.Text 
                +"' , '" + lot + "' , '" + seq + "' , '" + x1 + "', '" + x2 + "', '" + x3 + "', '" + x4 + "', '" + x5 + "', '" + xBar + "', '" + R + "', GetDate() )";
            WiseM.Data.DbAccess.Default.ExecuteQuery(query);
        }

        private void uiButtonClearAll_Click(object sender, EventArgs e)
        {
            this.dataGridViewSPC.Rows.Clear();
        }

        private void uiButtonSelectedDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewSPC.Rows.Count > 1)
            {
                for (int i = this.dataGridViewSPC.Rows.Count - 2; i >= 0; i--)
                {
                    if (this.dataGridViewSPC.Rows[i].Cells["_check"].Value != null && (bool)this.dataGridViewSPC.Rows[i].Cells["_check"].Value == true)
                    {
                        this.dataGridViewSPC.Rows.RemoveAt(i);
                    }
                }
                
            }
        }

        private void uiButtonSave_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewSPC.Rows.Count <= 1)
                return;

            int insertCount = 0;

            foreach (DataGridViewRow dr in this.dataGridViewSPC.Rows)
            {
                if (dr.Cells["_check"].Value != null && (bool)dr.Cells["_check"].Value == true)
                {
                    for (int i = 1; i < 8; i++)
                    {
                        if (dr.Cells[i].Value == null || string.IsNullOrEmpty(dr.Cells[i].Value.ToString()) == true)
                        {
                            WiseM.MessageBox.Show("You should insert all information. Please fill out.", "Warning", MessageBoxIcon.Warning, null);
                            return;
                        }
                    }
                }
            }

            foreach (DataGridViewRow dr in this.dataGridViewSPC.Rows)
            {
                if (dr.Cells["_check"].Value != null && (bool)dr.Cells["_check"].Value == true)
                {
                    string xBarstr = ((Convert.ToSingle(dr.Cells["_x1"].Value.ToString()) + Convert.ToSingle(dr.Cells["_x2"].Value.ToString()) + Convert.ToSingle(dr.Cells["_x3"].Value.ToString()) +
                                       Convert.ToSingle(dr.Cells["_x4"].Value.ToString()) + Convert.ToSingle(dr.Cells["_x5"].Value.ToString())) / 5).ToString();
                    
                    float Rmin=1000, Rmax=0;
                    if (Rmin > Convert.ToSingle(dr.Cells["_x1"].Value.ToString())) { Rmin = Convert.ToSingle(dr.Cells["_x1"].Value.ToString()); }
                    if (Rmin > Convert.ToSingle(dr.Cells["_x2"].Value.ToString())) { Rmin = Convert.ToSingle(dr.Cells["_x2"].Value.ToString()); }
                    if (Rmin > Convert.ToSingle(dr.Cells["_x3"].Value.ToString())) { Rmin = Convert.ToSingle(dr.Cells["_x3"].Value.ToString()); }
                    if (Rmin > Convert.ToSingle(dr.Cells["_x4"].Value.ToString())) { Rmin = Convert.ToSingle(dr.Cells["_x4"].Value.ToString()); }
                    if (Rmin > Convert.ToSingle(dr.Cells["_x5"].Value.ToString())) { Rmin = Convert.ToSingle(dr.Cells["_x5"].Value.ToString()); }

                    if (Rmax < Convert.ToSingle(dr.Cells["_x1"].Value.ToString())) { Rmax = Convert.ToSingle(dr.Cells["_x1"].Value.ToString()); }
                    if (Rmax < Convert.ToSingle(dr.Cells["_x2"].Value.ToString())) { Rmax = Convert.ToSingle(dr.Cells["_x2"].Value.ToString()); }
                    if (Rmax < Convert.ToSingle(dr.Cells["_x3"].Value.ToString())) { Rmax = Convert.ToSingle(dr.Cells["_x3"].Value.ToString()); }
                    if (Rmax < Convert.ToSingle(dr.Cells["_x4"].Value.ToString())) { Rmax = Convert.ToSingle(dr.Cells["_x4"].Value.ToString()); }
                    if (Rmax < Convert.ToSingle(dr.Cells["_x5"].Value.ToString())) { Rmax = Convert.ToSingle(dr.Cells["_x5"].Value.ToString()); }
                   
                    this.InsertQuery(dr.Cells["_lot"].Value.ToString(), dr.Cells["_seq"].Value.ToString(), dr.Cells["_x1"].Value.ToString(), dr.Cells["_x2"].Value.ToString(),
                        dr.Cells["_x3"].Value.ToString(), dr.Cells["_x4"].Value.ToString(), dr.Cells["_x5"].Value.ToString(), xBarstr, (Rmax-Rmin).ToString());
                    insertCount++;
                }
            }

            WiseM.MessageBox.Show(insertCount.ToString() + " cases the data was stored.", "Success", MessageBoxIcon.Information, null);
            this.dataGridViewSPC.Rows.Clear();

        }

        private void uiRadioButtonLength_CheckedChanged(object sender, EventArgs e)
        {
            this.selectedTypeNum = 0;
        }

        private void uiRadioButtonWidth_CheckedChanged(object sender, EventArgs e)
        {
            this.selectedTypeNum = 1;
        }

        private void uiRadioButtonThickness_CheckedChanged(object sender, EventArgs e)
        {
            this.selectedTypeNum = 2;
        }
    }
}
