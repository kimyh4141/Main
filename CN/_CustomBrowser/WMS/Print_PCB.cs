using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using WiseM.Data;

namespace WiseM.Browser.WMS
{
    public partial class Print_PCB : Form
    {
        private string _partNo;
        private string _partNm;
        private string _material;
        private string mfg_y;
        private string mfg_m;
        private string mfg_d;
        private string line;
        private string serialNo;
        private int print_qty;
        private string revision;
        private string barcode;
        private string label_size;

        public Print_PCB()
        {
            InitializeComponent();
        }

        private void Print_PCB_Load(object sender, EventArgs e)
        {
            Initialize();
        }

        public void Initialize()
        {
            tb_Search.Focus();

            //lbl_date.Text = string.Empty;
            dtp_date.Value = DateTime.Today;
            lbl_partno.Text = string.Empty;
            tb_Revision.Text = string.Empty;
            num_Qty.Value = 1;
            num_startNo.Value = 1;
            cb_Line.Text = string.Empty;
            lbl_labelSize.Text = string.Empty;

            //string Q = $"EXEC Sp_Convert_Date '{DateTime.Today:yyyyMMdd}'";
            //DataRow dr = DbAccess.Default.GetDataRow(Q);

            //mfg_y = dr[1].ToString();
            //mfg_m = dr[2].ToString();
            //mfg_d = dr[3].ToString();

            lbl_Supplier.Text = "T (YUYANG)";
            lbl_startPcb.Text = "-";
            lbl_endPcb.Text = "-";
            //lbl_date.Text = $"{mfg_y} ({DateTime.Today:yyyy}) {mfg_m} ({DateTime.Today:MM}) {mfg_d} ({DateTime.Today:dd})";

            dgv_partNo.DataSource = null;

            cb_Line.Items.AddRange(new object[]{ "G", "H", "I", "J", "K", "L", "T", "U", "V", "W" });

            foreach (DataGridViewColumn col in this.dgv_partNo.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            dgv_partNo.DataSource = null;

            try
            {
                string Q = $@"SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as Seq, Material, LG_ITEM_CD, LG_ITEM_NM, Revision FROM 
                                  (SELECT * FROM  Material WHERE Bunch = '10' and BP_CD = '114075') a
                                  WHERE a.Material LIKE '%{tb_Search.Text}%' or a.LG_ITEM_CD LIKE '%{tb_Search.Text}%'
                                 ";
                DataTable dt = DbAccess.Default.GetDataTable(Q);
                if (dt.Rows.Count > 0)
                {
                    dgv_partNo.DataSource = dt;
                    dgv_partNo.Columns["Seq"].Width = 40;
                    dgv_partNo.Columns["Material"].Width = 100;
                    dgv_partNo.Columns["Revision"].Width = 70;
                    dgv_partNo.Columns["LG_ITEM_NM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_partNo.Columns["LG_ITEM_NM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    MessageBox.ShowCaption("没有您要查找的信息。\r\nNot Found.", "误差 (Error)", MessageBoxIcon.Error);
                }
                tb_Search.Text = string.Empty;
                tb_Search.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }

        private void dgv_partNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!(sender is DataGridView dataGridView)) return;
            dtp_date.Value = DateTime.Today;
            lbl_partno.Text = string.Empty;
            tb_Revision.Text = string.Empty;
            num_Qty.Value = 1;
            num_startNo.Value = 1;
            cb_Line.SelectedIndex = -1;
            lbl_startPcb.Text = "-";
            lbl_endPcb.Text = "-";
            lbl_labelSize.Text = "";

            try
            {
                if (e.ColumnIndex < 0
                    || e.RowIndex < 0) return;

                _material = dataGridView.Rows[e.RowIndex].Cells["Material"].Value.ToString();
                _partNo = dataGridView.Rows[e.RowIndex].Cells["LG_ITEM_CD"].Value.ToString();
                _partNm = dataGridView.Rows[e.RowIndex].Cells["LG_ITEM_NM"].Value.ToString();
                revision = dataGridView.Rows[e.RowIndex].Cells["Revision"].Value.ToString();
                if (string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells["LG_ITEM_CD"].Value.ToString())
                    || string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells["LG_ITEM_NM"].Value.ToString())
                    || string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells["Revision"].Value.ToString()))
                {
                    System.Windows.Forms.MessageBox.Show($"有一些空的基础信息。\r\n" + $"请在商品基础信息中输入信息。\r\n" + $"There is some empty basis information. \r\n" + $"Please enter information in the item basis information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var query = $@"
                          SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                          DECLARE @P_Material NVARCHAR(50) = '{_material}'
                          ;
                          
                            WITH A AS
                                     (
                                     SELECT 1   AS Level
                                          , B.ITEM_CD
                                          , CASE B.ALTER_FLAG
                                                WHEN 'N'
                                                    THEN B.CHILD_ITEM_CD
                                                WHEN 'Y'
                                                    THEN B.KEY_ITEM
                                            END AS CHILD_ITEM_CD
                                          , B.CHILD_ITEM_SEQ
                                          , B.ALT_GROUP
                                       FROM BOM B
                                      WHERE B.ITEM_CD = @P_Material
                                      UNION ALL
                                     SELECT Level + 1
                                          , B.ITEM_CD
                                          , CASE B.ALTER_FLAG
                                                WHEN 'N'
                                                    THEN B.CHILD_ITEM_CD
                                                WHEN 'Y'
                                                    THEN B.KEY_ITEM
                                            END AS CHILD_ITEM_CD
                                          , B.CHILD_ITEM_SEQ
                                          , B.ALT_GROUP
                                       FROM A
                                            INNER JOIN BOM B
                                                       ON A.CHILD_ITEM_CD = B.ITEM_CD
                                     )
                          SELECT Level
                               , ITEM_CD
                               , CHILD_ITEM_CD
                               , Text
                               , CHILD_ITEM_SEQ
                               , ALT_GROUP
                            FROM A
                                 INNER JOIN      Common      C
                                        ON Category = '510'
                                            AND Common = A.CHILD_ITEM_CD
                          ;
                         ";
                DataTable check_dt = DbAccess.Default.GetDataTable(query);

                tb_Revision.Text = revision;

                if (check_dt.Rows.Count > 0)
                {
                    lbl_labelSize.Text = "40 * 8 mm";
                    label_size = "40";
                }
                else
                {
                    lbl_labelSize.Text = "32 * 8 mm";
                    label_size = "32";
                }

                if (_partNo.Length == 11)
                {
                }

                lbl_partno.Text = _partNo.Substring(3, 8);
                revision = tb_Revision.Text;
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "Error", MessageBoxIcon.Error);
            }
        }

        private void btn_Search1_Click(object sender, EventArgs e)
        {
            dgv_partNo.DataSource = null;

            try
            {
                string Q = $@"SELECT ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) as Seq, Material, LG_ITEM_CD, LG_ITEM_NM, Revision from 
                                  (select * from  Material where Bunch = '10' and BP_CD = '114075') a
                                  WHERE a.Spec LIKE '%{tb_Search1.Text}%'
                                 ";
                DataTable dt = DbAccess.Default.GetDataTable(Q);
                if (dt.Rows.Count > 0)
                {
                    dgv_partNo.DataSource = dt;
                    dgv_partNo.Columns["Seq"].Width = 40;
                    dgv_partNo.Columns["Material"].Width = 100;
                    dgv_partNo.Columns["Revision"].Width = 70;
                    dgv_partNo.Columns["LG_ITEM_NM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgv_partNo.Columns["LG_ITEM_NM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    tb_Search1.Text = string.Empty;
                    tb_Search1.Focus();
                }
                else
                {
                    MessageBox.ShowCaption("没有您要查找的信息。\r\nNot Found.", "误差 (Error)", MessageBoxIcon.Error);
                    tb_Search1.Text = string.Empty;
                    tb_Search1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            num_Qty.Value = 1;
            num_startNo.Value = 1;
            try
            {
                string Q = $"EXEC Sp_Convert_Date '{dtp_date.Value:yyyyMMdd}'";
                DataRow dr = DbAccess.Default.GetDataRow(Q);

                mfg_y = dr[1].ToString();
                mfg_m = dr[2].ToString();
                mfg_d = dr[3].ToString();

                if (string.IsNullOrEmpty(cb_Line.Text))
                    return;

                string tempBarcode = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}";

                string query = $@"
                                SELECT COALESCE(MAX(CONVERT(INTEGER, SerialNo)), 0) + 1
                                  FROM PcbPrintHist
                                 WHERE Bcd_Info LIKE '%{tempBarcode}%'
                                ";
                DataRow dr1 = DbAccess.Default.GetDataRow(query);

                int check_serial = int.Parse(dr1[0].ToString());
                num_startNo.Value = check_serial;

                print_qty = int.Parse(num_Qty.Value.ToString());

                serialNo = $"{check_serial:000#}";

                lbl_startPcb.Text = tempBarcode + serialNo;
                lbl_endPcb.Text = "-";
            }
            catch
            {
                return;
            }
        }

        private void cb_Line_SelectedIndexChanged(object sender, EventArgs e)
        {
            num_Qty.Value = 1;
            num_startNo.Value = 1;
            line = cb_Line.Text;
            try
            {
                string Q = $"EXEC Sp_Convert_Date '{dtp_date.Value:yyyyMMdd}'";
                DataRow dr = DbAccess.Default.GetDataRow(Q);

                mfg_y = dr[1].ToString();
                mfg_m = dr[2].ToString();
                mfg_d = dr[3].ToString();

                string tempBarcode = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}";

                string Query = $@"
                                SELECT COALESCE(MAX(CONVERT(INTEGER, SerialNo)), 0) + 1
                                  FROM PcbPrintHist
                                 WHERE Bcd_Info LIKE '%{tempBarcode}%'
                                ";
                DataRow dr1 = DbAccess.Default.GetDataRow(Query);

                int check_serial = int.Parse(dr1[0].ToString());
                num_startNo.Value = check_serial;

                print_qty = int.Parse(num_Qty.Value.ToString());

                serialNo = $"{check_serial:000#}";
                string endserialNo = $"{check_serial + print_qty - 1:000#}";

                lbl_startPcb.Text = tempBarcode + serialNo;
                lbl_endPcb.Text = "-";
            }
            catch
            {
                return;
            }
        }

        private void num_Qty_Leave(object sender, EventArgs e)
        {
            if (num_Qty.Value > 0)
            {
                if (string.IsNullOrEmpty(cb_Line.Text))
                    return;
                if (num_startNo.Value + num_Qty.Value > 10000)
                {
                    MessageBox.ShowCaption("PCB数量超过10000。\r\nThe PCB number is over 10000.", "Error", MessageBoxIcon.Error);
                    num_Qty.Value = 1;
                    lbl_endPcb.Text = "-";
                    return;
                }

                int serial = int.Parse(num_startNo.Value.ToString());
                print_qty = int.Parse(num_Qty.Value.ToString());

                serialNo = $"{serial:000#}";
                string endSerialNo = $"{serial + num_Qty.Value - 1:000#}";

                lbl_endPcb.Text = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}{endSerialNo}";
            }
            else
            {
                lbl_endPcb.Text = "-";
            }
        }

        private void num_startNo_Leave(object sender, EventArgs e)
        {
            if (num_startNo.Value <= 0) return;

            if (string.IsNullOrEmpty(cb_Line.Text))
                return;

            if (num_startNo.Value + num_Qty.Value > 10000)
            {
                MessageBox.ShowCaption("PCB数量超过10000。\r\nThe PCB number is over 10000.", "Error", MessageBoxIcon.Error);
                num_Qty.Value = 1;
                lbl_endPcb.Text = "-";
                return;
            }

            string tempBarcode = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}";

            serialNo = $"{num_startNo.Value:000#}";

            lbl_startPcb.Text = tempBarcode + serialNo;
            if (num_Qty.Value <= 0) return;
            string endSerialNo = $"{num_startNo.Value + num_Qty.Value - 1:000#}";
            lbl_endPcb.Text = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}{endSerialNo}";
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_partno.Text)
                || string.IsNullOrEmpty(cb_Line.Text)
                || string.IsNullOrEmpty(tb_Revision.Text)
                || num_Qty.Value < 1
                || num_startNo.Value < 1
               )
            {
                MessageBox.Show("并非所有信息都已输入。\r\nAll information has not been entered.", "警告 (Warning)", MessageBoxIcon.Error);
                return;
            }

            string query = $@"SELECT * FROM PcbPrintHist WITH(NOLOCK) WHERE Bcd_Info BETWEEN '{lbl_startPcb.Text}' AND '{lbl_endPcb.Text}'";
            var dataTable = DbAccess.Default.GetDataTable(query);
            if (dataTable.Rows.Count > 0)
            {
                MessageBox.ShowCaption("无法打印，因为它包含具有打印历史记录的条形码编号。" + "\r\nCannot print because it contains a barcode with print history.", "Error", MessageBoxIcon.Error);
                return;
            }


            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "确认 (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            string tempBarcode = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}";
            int serial = int.Parse(num_startNo.Value.ToString());
            print_qty = int.Parse(num_Qty.Value.ToString());

            if (serial + num_Qty.Value > 10000)
            {
                MessageBox.ShowCaption("PCB数量超过10000。\r\nThe PCB number is over 10000.", "Error", MessageBoxIcon.Error);
                return;
            }

            string strSql;
            DataTable dtMain;
            if (lbl_labelSize.Text == "32 * 8 mm")
            {
                strSql = "SELECT BcdData FROM BcdLblFmtr with(nolock) WHERE BcdName='Label_PCB'";
                dtMain = DbAccess.Default.GetDataTable(strSql);
            }
            else
            {
                strSql = "SELECT BcdData FROM BcdLblFmtr with(nolock) WHERE BcdName='Label_PCB_40'";
                dtMain = DbAccess.Default.GetDataTable(strSql);
            }

            string bcdData = dtMain.Rows[0][0].ToString();

            for (int i = serial; i < serial + print_qty; i++)
            {
                serialNo = $"{i:000#}";
                barcode = $"{tempBarcode}{serialNo}";

                if (!Print(barcode, bcdData))
                {
                    MessageBox.ShowCaption("打印错误 (Print Error)", "误差 (Error)", MessageBoxIcon.Error);
                    return;
                }

                databaseSave();
            }

            MessageBox.Show("打印完成 \r\nPrint Complete", "通知 (Notice)", MessageBoxIcon.Asterisk);
        }

        private void btn_datasave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lbl_partno.Text)
                || string.IsNullOrEmpty(cb_Line.Text)
                || string.IsNullOrEmpty(tb_Revision.Text)
                || num_Qty.Value < 1
                || num_startNo.Value < 1
               )
            {
                MessageBox.Show("并非所有信息都已输入。\r\nAll information has not been entered.", "警告 (Warning)", MessageBoxIcon.Error);
                return;
            }

            string Query = $@"SELECT * FROM PcbPrintHist WITH(NOLOCK) WHERE Bcd_Info BETWEEN '{lbl_startPcb.Text}' AND '{lbl_endPcb.Text}'";
            var dataTable = DbAccess.Default.GetDataTable(Query);

            if (dataTable.Rows.Count > 0)
            {
                MessageBox.ShowCaption("无法打印，因为它包含具有打印历史记录的条形码编号。" + "\r\nCannot save because it contains a barcode with print history.", "Error", MessageBoxIcon.Error);
                return;
            }

            if (System.Windows.Forms.MessageBox.Show("你确定吗？ \r\nAre you sure?", "确认 (Confirm)", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            else
            {
                string tempBarcode = $"T{mfg_y}{mfg_m}{mfg_d}{line}{_partNo.Substring(3, 8)}";
                int serial = int.Parse(num_startNo.Value.ToString());
                print_qty = int.Parse(num_Qty.Value.ToString());

                if (serial + num_Qty.Value > 10000)
                {
                    MessageBox.ShowCaption("PCB数量超过9999。\r\nThe PCB number is over 9999.", "Error", MessageBoxIcon.Error);
                    return;
                }

                for (int i = serial; i < serial + print_qty; i++)
                {
                    serialNo = $"{i:000#}";
                    barcode = $"{tempBarcode}{serialNo}";

                    databaseSave();
                }

                MessageBox.Show("已保存。 \r\n Save Complete", "通知 (Notice)", MessageBoxIcon.Asterisk);
            }
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool Print(string barcode, string bcdData)
        {
            try
            {
                clsBarcode.clsBarcode _clsBarcode = new clsBarcode.clsBarcode();
                _clsBarcode.LoadFromXml(bcdData);

                _clsBarcode.Data.SetText("BARCODE", barcode);
                _clsBarcode.Data.SetText("BARCODETEXT", $"{barcode} ({tb_Revision.Text})");
                _clsBarcode.Print(false);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void databaseSave()
        {
            try
            {
                string insertQuery;
                insertQuery = $@"
                            insert into PcbPrintHist 
                            (Material, LG_Material_CD, BCD_Info, Supplier, ProductionDate, Mfg_Year, Mfg_Month, Mfg_Day, 
                             Mfg_Line, LG_Material_NM, SerialNo, Revision, Label_Size, Reprint, Updated, Updater)
                            VALUES 
                            ('{_material}','{_partNo}','{barcode}','T', '{dtp_date.Value:yyyy-MM-dd}','{mfg_y}','{mfg_m}','{mfg_d}',
                             '{line}','{_partNm}','{serialNo}','{revision}', '{label_size}', 0, GETDATE(),'{WiseApp.Id}')
                                ";
                DbAccess.Default.ExecuteQuery(insertQuery);
            }
            catch (Exception ex)
            {
                MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
            }
        }


        private void tb_Search_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.ToolTipTitle = "";
            this.toolTip1.IsBalloon = true;
            this.toolTip1.SetToolTip(this.tb_Search, "输入要搜索的项目编号的一部分。");
        }

        private void tb_Search1_MouseHover(object sender, EventArgs e)
        {
            this.toolTip2.ToolTipTitle = "";
            this.toolTip2.IsBalloon = true;
            this.toolTip2.SetToolTip(this.tb_Search1, "请输入部分型号名称进行搜索");
        }

        private void btn_testPrint32_Click(object sender, EventArgs e)
        {
            var strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_PCB'";
            var dtMain = DbAccess.Default.GetDataTable(strSql);

            var clsBarcode = new clsBarcode.clsBarcode();
            clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

            clsBarcode.Data.SetText("BARCODE", "ABCDE123456780001");
            clsBarcode.Data.SetText("BARCODETEXT", $"ABCDE123456780001 (1.0)");

            clsBarcode.Print(false);
        }

        private void btn_testPrint40_Click(object sender, EventArgs e)
        {
            var strSql = "SELECT BcdData FROM BcdLblFmtr WHERE BcdName='Label_PCB_40'";
            var dtMain = DbAccess.Default.GetDataTable(strSql);

            var clsBarcode = new clsBarcode.clsBarcode();
            clsBarcode.LoadFromXml(dtMain.Rows[0][0].ToString());

            clsBarcode.Data.SetText("BARCODE", "ABCDE123456780001");
            clsBarcode.Data.SetText("BARCODETEXT", $"ABCDE123456780001 (1.0)");

            clsBarcode.Print(false);
        }


    }
}
