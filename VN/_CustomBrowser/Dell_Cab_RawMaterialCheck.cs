using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Browser
{
    public partial class Dell_Cab_RawMaterialCheck : SkinForm
    {
        public Dell_Cab_RawMaterialCheck()
        {
            InitializeComponent();
            process();
        }

        public void process()
        {
            this.TB_Barcode.Text = string.Empty;
            this.LB_BoxNo_Info.Text = "-";
            this.LB_BoxNo_Judgement.Text = "-";
            this.LB_BoxQty_Info.Text = "-";
            this.LB_BoxQty_Judgement.Text = "-";
            this.LB_Comp_Suppier_ID_Info.Text = "-";
            this.LB_Comp_Suppier_ID_Judgement.Text = "-";
            this.LB_Comp_Supplier_Mfg_Dt_Info.Text = "-";
            this.LB_Comp_Supplier_Mfg_Dt_Jufgement.Text = "-";
            this.LB_Comp_Supplier_PN_Info.Text = "-";
            this.LB_Comp_Supplier_PN_Judgement.Text = "-";
            this.LB_Stock_Date_Info.Text = "-";
            this.LB_Stock_Date_Judgement.Text = "-";
            this.LB_StockYN_Info.Text = "-";
            this.LB_StockYN_Judgement.Text = "-";
            this.LB_ToolNo_Info.Text = "-";
            this.LB_ToolNo_Judgement.Text = "-";
            this.LB_Unique_Comp_Code_Info.Text = "-";
            this.LB_Unique_Comp_Code_Judgement.Text = "-";
        }

        private void TB_Barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                //31010673600   R01007   0001    01    20160126     1000     0024
                try
                {
                    if (this.TB_Barcode.Text.Length == 39)
                    {
                        //데이터 부
                        string DataSearch = " Select * from Material where Material = '" + this.TB_Barcode.Text.Substring(0, 11) + "' ";
                        DataTable dt = DbAccess.Default.GetDataTable(DataSearch);
                        if (dt.Rows.Count == 0)
                        {
                            this.LB_Comp_Supplier_PN_Info.Text = "Is not data.";
                            this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Red;
                            this.LB_Comp_Supplier_PN_Judgement.Text = "NG";
                        }
                        else
                        {
                            this.LB_Comp_Supplier_PN_Info.Text = this.TB_Barcode.Text.Substring(0, 11) + "/" + dt.Rows[0]["Text"].ToString();
                            this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Blue;
                            this.LB_Comp_Supplier_PN_Judgement.Text = "OK";
                        }
                        string Supplier_ID = " select * from Dell_Data.dbo.Cab_Login where Comp_Supplier_ID = '" + this.TB_Barcode.Text.Substring(11, 6) + "' ";
                        DataTable dt_ID = DbAccess.Default.GetDataTable(Supplier_ID);
                        if (dt_ID.Rows.Count == 0)
                        {
                            this.LB_Comp_Suppier_ID_Info.Text = "Is not Data.";
                            this.LB_Comp_Suppier_ID_Judgement.Text = "NG";
                            this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.LB_Comp_Suppier_ID_Info.Text = this.TB_Barcode.Text.Substring(11, 6) + "/" + dt_ID.Rows[0]["Comp_Supplier_Factory_Name"].ToString();
                            this.LB_Comp_Suppier_ID_Judgement.Text = "OK";
                            this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Blue;
                        }


                        string Comp_Code = " Select * From Dell_Data.dbo.Common where Common = '" + this.TB_Barcode.Text.Substring(17, 4) + "' ";
                        DataTable dt_comp_code = DbAccess.Default.GetDataTable(Comp_Code);
                        if (dt_comp_code.Rows.Count == 0)
                        {
                            this.LB_Unique_Comp_Code_Info.Text = "Is not Data.";
                            this.LB_Unique_Comp_Code_Judgement.Text = "NG";
                            this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.LB_Unique_Comp_Code_Info.Text = this.TB_Barcode.Text.Substring(17, 4) + "/" + dt_comp_code.Rows[0]["Text"].ToString();
                            this.LB_Unique_Comp_Code_Judgement.Text = "OK";
                            this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Blue;
                        }

                        this.LB_ToolNo_Info.Text = this.TB_Barcode.Text.Substring(21, 2);
                        this.LB_Comp_Supplier_Mfg_Dt_Info.Text = this.TB_Barcode.Text.Substring(23, 8);
                        if (this.LB_Comp_Supplier_Mfg_Dt_Info.Text.Substring(0, 2) != "20")
                        {
                            this.LB_Comp_Supplier_Mfg_Dt_Jufgement.Text = "NG";
                            
                            this.LB_Comp_Supplier_Mfg_Dt_Jufgement.ForeColor = Color.Red;
                        }
                        else
                        {
                            
                            this.LB_Comp_Supplier_Mfg_Dt_Jufgement.Text = "OK";
                            this.LB_Comp_Supplier_Mfg_Dt_Jufgement.ForeColor = Color.Blue;
                        }

                        this.LB_BoxQty_Info.Text = this.TB_Barcode.Text.Substring(31, 4);
                        this.LB_BoxNo_Info.Text = this.TB_Barcode.Text.Substring(35, 4);
                        string Search_Case = " Select * from Dell_Data.dbo.Cab_Case where CabID = '" + this.TB_Barcode.Text + "' ";
                        DataTable dt_case = DbAccess.Default.GetDataTable(Search_Case);
                        if (dt_case.Rows.Count > 0)
                        {
                            this.LB_StockYN_Info.Text = "Y";
                            this.LB_StockYN_Judgement.Text = "OK";
                            this.LB_StockYN_Judgement.ForeColor = Color.Blue;
                            this.LB_Stock_Date_Info.Text = Convert.ToDateTime(dt_case.Rows[0]["Updated"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            this.LB_Stock_Date_Judgement.Text = "OK";
                            this.LB_Stock_Date_Judgement.ForeColor = Color.Blue;
                        }
                        else
                        {
                            this.LB_StockYN_Info.Text = "N";
                            this.LB_StockYN_Judgement.Text = "NG";
                            this.LB_StockYN_Judgement.ForeColor = Color.Red;
                            this.LB_Stock_Date_Info.Text = "Is not data.";
                            this.LB_Stock_Date_Judgement.Text = "NG";
                            this.LB_Stock_Date_Judgement.ForeColor = Color.Red;
                        }


                        //판정 부 


                    }
                    else if (this.TB_Barcode.Text.Length > 39)
                    {
                        WiseM.MessageBox.Show("Error, Data Length is not 39 space", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                        ////데이터 부
                        //string DataSearch = " Select * from Material where Material = '" + this.TB_Barcode.Text.Substring(0, 11) + "' ";
                        //DataTable dt = DbAccess.Default.GetDataTable(DataSearch);
                        //if (dt.Rows.Count == 0)
                        //{
                        //    this.LB_Comp_Supplier_PN_Info.Text = "Is not data.";
                        //    this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Red;
                        //    this.LB_Comp_Supplier_PN_Judgement.Text = "NG";
                        //}
                        //else
                        //{
                        //    this.LB_Comp_Supplier_PN_Info.Text = this.TB_Barcode.Text.Substring(0, 11) + "/" + dt.Rows[0]["Text"].ToString();
                        //    this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Blue;
                        //    this.LB_Comp_Supplier_PN_Judgement.Text = "OK";
                        //}
                        //string Supplier_ID = " select * from Dell_Data.dbo.Cab_Login where Comp_Supplier_ID = '" + this.TB_Barcode.Text.Substring(11, 6) + "' ";
                        //DataTable dt_ID = DbAccess.Default.GetDataTable(Supplier_ID);
                        //if (dt_ID.Rows.Count == 0)
                        //{
                        //    this.LB_Comp_Suppier_ID_Info.Text = "Is not Data.";
                        //    this.LB_Comp_Suppier_ID_Judgement.Text = "NG";
                        //    this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Red;
                        //}
                        //else
                        //{
                        //    this.LB_Comp_Suppier_ID_Info.Text = this.TB_Barcode.Text.Substring(11, 6) + "/" + dt_ID.Rows[0]["Comp_Supplier_Factory_Name"].ToString();
                        //    this.LB_Comp_Suppier_ID_Judgement.Text = "OK";
                        //    this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Blue;
                        //}


                        //string Comp_Code = " Select * From Dell_Data.dbo.Common where Common = '" + this.TB_Barcode.Text.Substring(17, 4) + "' ";
                        //DataTable dt_comp_code = DbAccess.Default.GetDataTable(Comp_Code);
                        //if (dt_comp_code.Rows.Count == 0)
                        //{
                        //    this.LB_Unique_Comp_Code_Info.Text = "Is not Data.";
                        //    this.LB_Unique_Comp_Code_Judgement.Text = "NG";
                        //    this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Red;
                        //}
                        //else
                        //{
                        //    this.LB_Unique_Comp_Code_Info.Text = this.TB_Barcode.Text.Substring(17, 4) + "/" + dt_comp_code.Rows[0]["Text"].ToString();
                        //    this.LB_Unique_Comp_Code_Judgement.Text = "OK";
                        //    this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Blue;
                        //}

                        //this.LB_ToolNo_Info.Text = this.TB_Barcode.Text.Substring(21, 2);
                        //this.LB_Comp_Supplier_Mfg_Dt_Info.Text = this.TB_Barcode.Text.Substring(23, 8);
                        //this.LB_BoxQty_Info.Text = this.TB_Barcode.Text.Substring(31, 4);
                        //this.LB_BoxNo_Info.Text = this.TB_Barcode.Text.Substring(35, 4);
                        //string Search_Case = " Select * from Dell_Data.dbo.Cab_Case where CabID = '" + this.TB_Barcode.Text + "' ";
                        //DataTable dt_case = DbAccess.Default.GetDataTable(Search_Case);
                        //if (dt_case.Rows.Count > 0)
                        //{
                        //    this.LB_StockYN_Info.Text = "Y";
                        //    this.LB_StockYN_Judgement.Text = "OK";
                        //    this.LB_StockYN_Judgement.ForeColor = Color.Blue;
                        //    this.LB_Stock_Date_Info.Text = Convert.ToDateTime(dt_case.Rows[0]["Updated"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        //    this.LB_Stock_Date_Judgement.Text = "OK";
                        //    this.LB_Stock_Date_Judgement.ForeColor = Color.Blue;
                        //}
                        //else
                        //{
                        //    this.LB_StockYN_Info.Text = "N";
                        //    this.LB_StockYN_Judgement.Text = "NG";
                        //    this.LB_StockYN_Judgement.ForeColor = Color.Red;
                        //    this.LB_Stock_Date_Info.Text = "Is not data.";
                        //    this.LB_Stock_Date_Judgement.Text = "NG";
                        //    this.LB_Stock_Date_Judgement.ForeColor = Color.Red;
                        //}


                        //판정 부 

                    }
                    else if (this.TB_Barcode.Text.Length < 39)
                    {
                        WiseM.MessageBox.Show("Error, Data Length is not 39 space", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                        //    //데이터 부
                        //    string DataSearch = " Select * from Material where Material = '" + this.TB_Barcode.Text.Substring(0, 11) + "' ";
                        //    DataTable dt = DbAccess.Default.GetDataTable(DataSearch);
                        //    if (dt.Rows.Count == 0)
                        //    {
                        //        this.LB_Comp_Supplier_PN_Info.Text = "Is not data.";
                        //        this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Red;
                        //        this.LB_Comp_Supplier_PN_Judgement.Text = "NG";
                        //    }
                        //    else
                        //    {
                        //        this.LB_Comp_Supplier_PN_Info.Text = this.TB_Barcode.Text.Substring(0, 11) + "/" + dt.Rows[0]["Text"].ToString();
                        //        this.LB_Comp_Supplier_PN_Judgement.ForeColor = Color.Blue;
                        //        this.LB_Comp_Supplier_PN_Judgement.Text = "OK";
                        //    }
                        //    string Supplier_ID = " select * from Dell_Data.dbo.Cab_Login where Comp_Supplier_ID = '" + this.TB_Barcode.Text.Substring(11, 6) + "' ";
                        //    DataTable dt_ID = DbAccess.Default.GetDataTable(Supplier_ID);
                        //    if (dt_ID.Rows.Count == 0)
                        //    {
                        //        this.LB_Comp_Suppier_ID_Info.Text = "Is not Data.";
                        //        this.LB_Comp_Suppier_ID_Judgement.Text = "NG";
                        //        this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        this.LB_Comp_Suppier_ID_Info.Text = this.TB_Barcode.Text.Substring(11, 6) + "/" + dt_ID.Rows[0]["Comp_Supplier_Factory_Name"].ToString();
                        //        this.LB_Comp_Suppier_ID_Judgement.Text = "OK";
                        //        this.LB_Comp_Suppier_ID_Judgement.ForeColor = Color.Blue;
                        //    }


                        //    string Comp_Code = " Select * From Dell_Data.dbo.Common where Common = '" + this.TB_Barcode.Text.Substring(17, 4) + "' ";
                        //    DataTable dt_comp_code = DbAccess.Default.GetDataTable(Comp_Code);
                        //    if (dt_comp_code.Rows.Count == 0)
                        //    {
                        //        this.LB_Unique_Comp_Code_Info.Text = "Is not Data.";
                        //        this.LB_Unique_Comp_Code_Judgement.Text = "NG";
                        //        this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        this.LB_Unique_Comp_Code_Info.Text = this.TB_Barcode.Text.Substring(17, 4) + "/" + dt_comp_code.Rows[0]["Text"].ToString();
                        //        this.LB_Unique_Comp_Code_Judgement.Text = "OK";
                        //        this.LB_Unique_Comp_Code_Judgement.ForeColor = Color.Blue;
                        //    }

                        //    this.LB_ToolNo_Info.Text = this.TB_Barcode.Text.Substring(21, 2);
                        //    this.LB_Comp_Supplier_Mfg_Dt_Info.Text = this.TB_Barcode.Text.Substring(23, 8);
                        //    this.LB_BoxQty_Info.Text = this.TB_Barcode.Text.Substring(31, 4);
                        //    this.LB_BoxNo_Info.Text = this.TB_Barcode.Text.Substring(35, 4);
                        //    string Search_Case = " Select * from Dell_Data.dbo.Cab_Case where CabID = '" + this.TB_Barcode.Text + "' ";
                        //    DataTable dt_case = DbAccess.Default.GetDataTable(Search_Case);
                        //    if (dt_case.Rows.Count > 0)
                        //    {
                        //        this.LB_StockYN_Info.Text = "Y";
                        //        this.LB_StockYN_Judgement.Text = "OK";
                        //        this.LB_StockYN_Judgement.ForeColor = Color.Blue;
                        //        this.LB_Stock_Date_Info.Text = Convert.ToDateTime(dt_case.Rows[0]["Updated"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        //        this.LB_Stock_Date_Judgement.Text = "OK";
                        //        this.LB_Stock_Date_Judgement.ForeColor = Color.Blue;
                        //    }
                        //    else
                        //    {
                        //        this.LB_StockYN_Info.Text = "N";
                        //        this.LB_StockYN_Judgement.Text = "NG";
                        //        this.LB_StockYN_Judgement.ForeColor = Color.Red;
                        //        this.LB_Stock_Date_Info.Text = "Is not data.";
                        //        this.LB_Stock_Date_Judgement.Text = "NG";
                        //        this.LB_Stock_Date_Judgement.ForeColor = Color.Red;
                        //    }


                        //    //판정 부 
                        //}
                        // this.TB_CurrentBarCode.Text = this.TB_Barcode.Text;
                        // this.TB_Barcode.Text = string.Empty;
                        // this.TB_Barcode.Focus();
                    }
                }
                catch (Exception ex)
                {

                    process();
                }
                
                
            }
        }

       

    }
}
