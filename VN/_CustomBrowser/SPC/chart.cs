using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WiseM.Browser
{
    public partial class chart : Form
    {
        CustomPanelLinkEventArgs ee = null;
        DataTable dt = new DataTable();
        DataTable spccldt = new DataTable();
        Double CpkPpkAcceptanceValue = 1.33;
        Double USLs = 0;
        Double LSLs = 0;
        bool _MaxNullFlag = true;
        bool _MinNullFlag = true;
        public chart(CustomPanelLinkEventArgs e, DataTable spccltempdt)
        {
            InitializeComponent();
            this.Width = 1600;
            shanuCPCPKChart.Width = 1592;
            ee = e;
            DataTable tempdt = (DataTable)ee.DataGridView.DataSource;
            try
            {
                CpkPpkAcceptanceValue = Convert.ToDouble(tempdt.Rows[0]["CpkLimit"]);
            }
            catch
            {
            }
            spccldt = spccltempdt.Copy();
            dt = tempdt.Copy();

            string script = " select MinValue, MaxValue from SpcItems "
                          + " where SpcItem = N'" + tempdt.Rows[0]["spcitem"] + "' "
                          + "       and ItemType = '" + tempdt.Rows[0]["ItemType"] + "' "
                          + "       and Model = '" + tempdt.Rows[0]["Model"] + "' "
                          + "       and InspType = '" + tempdt.Rows[0]["InspType"] + "'";

            DataTable minmax = ee.DbAccess.GetDataTable(script);

            if (minmax.Rows.Count > 0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(minmax.Rows[0]["MaxValue"].ToString()))
                    {
                        _MaxNullFlag = false;
                        USLs = Convert.ToDouble(minmax.Rows[0]["MaxValue"]);
                    }
                    if (!string.IsNullOrEmpty(minmax.Rows[0]["MinValue"].ToString()))
                    {
                        _MinNullFlag = false;
                        LSLs = Convert.ToDouble(minmax.Rows[0]["MinValue"]);
                    }
                }
                catch
                {
                }
            }
            dt.Columns.Remove("spcitem");
            dt.Columns.Remove("Model");
            dt.Columns.Remove("InspType");
            dt.Columns.Remove("ItemType");
            dt.Columns.Remove("CpkLimit");

            if (e.Link.ToLower().Equals("chart"))
            {
                shanuCPCPKChart.ChartWaterMarkText = "X Bar / R Chart";
                shanuCPCPKChart.USL = USLs;
                shanuCPCPKChart.LSL = LSLs;
                shanuCPCPKChart.CpkPpKAcceptanceValue = CpkPpkAcceptanceValue;
                shanuCPCPKChart.Bindgrid(dt, spccldt);
                this.Text = "월간 일자별 검사값 Xbar-R Chart";               
            }
            else if (e.Link.ToLower().Equals("cp/cpk"))
            {
                shanuCPCPKChart.ChartWaterMarkText = "Cp / Cpk Chart";
                shanuCPCPKChart.USL = USLs;
                shanuCPCPKChart.LSL = LSLs;
                shanuCPCPKChart._MaxFlag = _MaxNullFlag;
                shanuCPCPKChart._MinFlag = _MinNullFlag;
                shanuCPCPKChart.CpkPpKAcceptanceValue = CpkPpkAcceptanceValue;
                shanuCPCPKChart.Bindgrid(dt, spccldt);

                this.Text = "월간 일자별 검사값 Cp / Cpk Chart";
            }
        }
            
    }
}

