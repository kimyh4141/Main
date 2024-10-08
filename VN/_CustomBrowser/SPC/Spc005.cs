using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiseM.Browser.SPC
{
    public partial class Spc005 : Form
    {
        CustomPanelLinkEventArgs Spc005e = null;
        DataTable dt = new DataTable();
        string[] SpcClDate;

        public Spc005(CustomPanelLinkEventArgs e, string[] tempScript)
        {
            InitializeComponent();
            Spc005e = e;
            DataTable tempdt = (DataTable)Spc005e.DataGridView.DataSource;
            dt = tempdt.Copy();
            SpcClDate = tempScript;
            labelSpcClDate.Text = SpcClDate[1].ToString();
            labelSpcItem.Text = SpcClDate[3].ToString();
            labelItemType.Text = SpcClDate[5].ToString();
            labelSpcClModel.Text = SpcClDate[7].ToString();
            labelInspType.Text = SpcClDate[9].ToString();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBoxXBarUcl.Text) && !string.IsNullOrEmpty(textBoxXBarLcl.Text) &&
               !string.IsNullOrEmpty(textBoxRUcl.Text) && !string.IsNullOrEmpty(textBoxRLcl.Text))
            {
                double XBarUcl = 0;
                double XBarLcl = 0;
                double RUcl = 0;
                double RLcl = 0;
                if (double.TryParse(textBoxXBarUcl.Text, out XBarUcl) && double.TryParse(textBoxXBarLcl.Text, out XBarLcl) &&
                    double.TryParse(textBoxRUcl.Text, out RUcl) && double.TryParse(textBoxRLcl.Text, out RLcl))
                {

                    if (XBarUcl < XBarLcl || RUcl < RLcl)
                    {
                        MessageBox.Show("Ucl값은 Lcl보다 커야 합니다.", "Warning", MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        string script = "Insert Into [dbo].SpcCl Values ("
                        + "'" + SpcClDate[1].ToString() + "', "             //SpcDate
                        + "'" + SpcClDate[7].ToString() + "', "             //Model
                        + "'" + SpcClDate[9].ToString() + "', "             //InspType
                        + "'" + SpcClDate[5].ToString() + "',"            //ItemType
                        + "'" + SpcClDate[3].ToString() + "', "             //SpcItem                
                        + XBarUcl + ", "
                        + XBarLcl + ", "
                        + RUcl + ", "
                        + RLcl + ", "
                        + "getdate()" + ", "
                        + "'" + WiseApp.Id + "');";


                        int Result = 0;
                        try
                        {
                            Result = Spc005e.DbAccess.ExecuteQuery(script);
                        }
                        catch (Exception ex)
                        {
                            WiseM.MessageBox.Show(ex.Message, "Error", MessageBoxIcon.Information);
                        }

                        if (Result == 1)
                        {
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("CL 값이 잘못 입력되었습니다.", "Warning", MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("CL 값을 입력해야합니다.", "Warning", MessageBoxIcon.Warning);
            }            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
