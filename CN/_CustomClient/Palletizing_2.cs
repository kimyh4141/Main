using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WiseM.AppService;
using WiseM.Data;
using WiseM.Forms;

namespace WiseM.Client
{
    public partial class Palletizing_2 : SkinForm
    {
        #region Field

        /// <summary>
        /// PCB 검증 결과
        /// </summary>
        private enum Sign
        {
            /// <summary>
            /// OK?
            /// </summary>
            OK,
            ERR00,
            ERR01,
            ERR02,
            ERR03,

            /// <summary>
            /// Current process already passed.
            /// </summary>
            ERR10,
            ERR11,
            ERR12,
            ERR13,
            ERR14,

            ERR20,
            ERR21,
            ERR22,

            Save_OK,
            Save_NG,
        };

        private bool _isProcess;
        string _Workorder = WbtCustomService.ActiveValues.WorkOrder;
        string _Material = WbtCustomService.ActiveValues.Material;
        string _Workcenter = WbtCustomService.ActiveValues.Workcenter;

        DataTable dt1 = new DataTable();
        DataTable dtQ = new DataTable();

        string ini_Qty;
        private string OrderQty { get; set; }

        private int CurrentPcbQty { get; set; }

        string scanData;

        string _childMaterial;

        #endregion

        public Palletizing_2()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            lbl_workorder.Text = string.Empty;
            lbl_item.Text = string.Empty;
            lbl_Qty.Text = "0 / 0";
            rTB_log.Text = string.Empty;
            dtQ.Rows.Clear();
            lbl_Error.Visible = false;

            lbl_workorder.Text = _Workorder;

            string Q;
            Q = $"select m.Material, m.Spec, o.OrderQty, c.Kind, c.Routing from WorkOrder o ";
            Q += $"join Material m on o.Material = m.Material ";
            Q += $"join WorkCenter c on o.Workcenter = c.Workcenter ";
            Q += $"where WorkOrder = '{_Workorder}'";

            dtQ = DbAccess.Default.GetDataTable(Q);
            lbl_item.Text = _Material + " / " + dtQ.Rows[0]["Spec"];
            OrderQty = dtQ.Rows[0]["OrderQty"].ToString();

            string QtyQuery = $@"Select coalesce(SUM(OutQty), 0) as OutQty FROM OutputHist where WorkOrder = '{_Workorder}'";
            DataTable dt_Qty = DbAccess.Default.GetDataTable(QtyQuery);
            ini_Qty = dt_Qty.Rows[0]["OutQty"].ToString();
            CurrentPcbQty = Convert.ToInt32(ini_Qty);
            lbl_Qty.Text = $@"{ini_Qty} / {OrderQty}";

            string child_Q = $@"
                            SELECT CHILD_ITEM_CD 
                              FROM BOM                 B
                                   INNER JOIN Material M
                                              ON B.CHILD_ITEM_CD = M.Material AND M.Bunch = '25'
                             WHERE ITEM_CD = '{_Material}'
                                ";
            DataTable child_dt = DbAccess.Default.GetDataTable(child_Q);
            if (child_dt.Rows.Count <= 0)
            {
                System.Windows.Forms.MessageBox.Show("Not Found Child Material");
                this.Close();
            }
            else
            {
                _childMaterial = child_dt.Rows[0]["CHILD_ITEM_CD"].ToString();
            }

            dt1.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("No"),
                new DataColumn("Pallet BCD"),
                new DataColumn("Box BCD"),
                new DataColumn("DateTime")
            });

            SetLogMessage("Program Start");

            dgv_List.DefaultCellStyle.Font = new Font("Tahoma", 10, FontStyle.Regular);
        }

        private void Palletizing_2_Load(object sender, EventArgs e)
        {
            tb_scan.Focus();
        }

        private void tb_scan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            if (_isProcess) return;
            try
            {
                _isProcess = true;
                scanData = tb_scan.Text.ToUpper();
                int scanQty = Convert.ToInt32(DbAccess.Default.ExecuteScalar(
                    $@"
                                SELECT COUNT(PcbBcd)
                                  FROM Packing
                                 WHERE PalletBcd = '{scanData}'
                             "
                    ));
                int compareOrderQty = int.Parse(OrderQty);
                if (scanQty + CurrentPcbQty > compareOrderQty)
                {
                    System.Windows.Forms.MessageBox.Show("Scan Qty :" + (scanQty + CurrentPcbQty) + " > Order Qty : " + compareOrderQty);
                    return;
                }

                if (!VerifyBarcode(scanData))
                {
                    //NG
                }
                else
                {
                    if (!InsertResult())
                    {
                        SetControl(scanData, Sign.Save_NG);
                        return;
                    }

                    SetControl(scanData, Sign.Save_OK);
                }
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Palletizing_2.tb_scan_KeyUp', N'{ex.Message}', '{_Workcenter}', GETDATE())");
            }
            finally
            {
                tb_scan.Clear();
                tb_scan.Focus();
                _isProcess = false;
            }
        }

        private bool VerifyBarcode(string barcode)
        {
            Sign sign;

            //barcode length check
            if (!(barcode.Length == 19 || barcode.Length == 21))
            {
                sign = Sign.ERR02;
                SetControl(barcode, sign);
                return false;
            }

            //Pallet barcode check
            if (barcode.Substring(0, 1) != "P")
            {
                sign = Sign.ERR03;
                SetControl(barcode, sign);
                return false;
            }

            var query1 = new StringBuilder();
            query1.AppendLine($"SELECT TOP 1 * FROM Stock WITH(NOLOCK) WHERE PalletBcd = '{barcode}'");
            var dataTable1 = DbAccess.Default.GetDataTable(query1.ToString());

            //Stock check
            if (dataTable1.Rows.Count > 0)
            {
                sign = Sign.ERR10;
                SetControl(barcode, sign);
                return false;
            }

            var query = new StringBuilder();
            query.AppendLine($"SELECT TOP 1 * FROM Packing with(nolock) WHERE PalletBcd = '{barcode}'");
            var dataTable = DbAccess.Default.GetDataTable(query.ToString());

            //Packing check
            if (dataTable.Rows.Count == 0)
            {
                sign = Sign.ERR13;
                SetControl(barcode, sign);
                return false;
            }

            //Material check
            if (_childMaterial != dataTable.Rows[0]["Material"].ToString())
            {
                sign = Sign.ERR22;
                SetControl(barcode, sign);
                return false;
            }

            sign = Sign.OK;
            SetControl(barcode, sign);
            return true;
        }

        private void SetControl(string barcode, Sign sign)
        {
            lbl_Error.Visible = false;
            //lbl_Error.Text = sign.ToString();

            var logMessage = string.Empty;
            switch (sign)
            {
                case Sign.OK:
                    lbl_Current.ForeColor = Color.DodgerBlue;
                    Label_Control(barcode, "OK");
                    logMessage = $"[{barcode}] Verified [OK]";
                    SetLogMessage(logMessage);
                    break;
                case Sign.ERR02:
                    SetLogMessage($"[{barcode}] Barcode Length Error [NG]");
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR02 - 条码长度错误。Barcode Length Error.";
                    break;
                case Sign.ERR03:
                    SetLogMessage($"[{barcode}] Barcode Error not pallet barcode [NG]");
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR03 - 它不是货盘条形码。This barcode is not a pallet barcode.";
                    break;
                case Sign.ERR10:
                    SetLogMessage($"[{barcode}] Barcode errors that already pass the current process [NG]");
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR10 - 已经通过当前流程的条码错误 Current process already passed.";
                    break;
                case Sign.ERR13:
                    SetLogMessage($"[{barcode}] Unknown Barcode [NG]");
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR13 - 未知条码 Unknown Barcode.";
                    break;
                case Sign.ERR14:
                    SetLogMessage($"[{barcode}] The box with the pallet barcode was read. [NG]");
                    lbl_Error.Visible = true;
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Text = "ERR14 - 带有托盘条码的盒子被读取。 This box has been palletized.";
                    break;
                case Sign.ERR22:
                    SetLogMessage($"[{barcode}] Wrong Item [NG]");
                    lbl_Current.ForeColor = Color.Red;
                    Label_Control(barcode, "NG");
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR22 - 品目信息不同 Wrong Item Information.";
                    break;
                case Sign.Save_OK:
                    try
                    {
                        var query = new StringBuilder();
                        query.AppendLine($@"Select coalesce(SUM(OutQty), 0) as OutQty FROM OutputHist where WorkOrder = '{_Workorder}'");
                        var dataTable = DbAccess.Default.GetDataTable(query.ToString());

                        var query1 = new StringBuilder();
                        query1.AppendLine($@"SELECT COUNT(*) Cnt FROM Stock WHERE PalletBcd = '{barcode}'");
                        var datatable1 = DbAccess.Default.GetDataTable(query1.ToString());

                        if (dataTable.Rows.Count > 0)
                        {
                            dgv_List.Rows.Add(dgv_List.Rows.Count + 1, barcode, datatable1.Rows[0]["Cnt"].ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("没有保存的数据 There is no saved data");
                        }

                        CurrentPcbQty = Convert.ToInt32(dataTable.Rows[0]["OutQty"]);
                        lbl_Qty.Text = string.Format($@"{CurrentPcbQty} / {OrderQty}");
                        SetLogMessage($"Database Save Complete");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.ShowCaption(ex.Message, "", MessageBoxIcon.Error);
                    }

                    break;
                case Sign.Save_NG:
                    lbl_Current.ForeColor = Color.Red;
                    lbl_Error.Visible = true;
                    lbl_Error.Text = "ERR06 - 数据库错误 DB error";
                    SetLogMessage($"DataBase Save Error");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sign), sign, null);
            }
        }

        private bool InsertResult()
        {
            try
            {
                string strCmd = $@"
                                 EXEC [Sp_WorkPcStock]
                                 @PS_GUBUN			=   'SAVE_STOCK_IN'
                                ,@PS_PALLET			=   '{scanData}'
                                ,@PS_MATERIAL		=   '{_Material}'
                                ,@PS_WORKORDER		=   '{_Workorder}'
                                ,@PS_WORKCENTER		=   '{_Workcenter}'
                                ,@PS_UPDATEUSER		=   '{WiseApp.Id}'
                                    ";

                DataSet ds1 = DbAccess.Default.GetDataSet(strCmd);

                if (ds1.Tables[ds1.Tables.Count - 1].Rows[0]["RC"].ToString() != "0")
                {
                    string strErr = ds1.Tables[ds1.Tables.Count - 1].Rows[0]["ERR_MSG"].ToString();
                }

                var query = new StringBuilder();
                query.AppendLine($"SELECT COUNT(*) Count FROM Stock WITH(NOLOCK) WHERE PalletBcd = '{scanData}'");
                var dataTable = DbAccess.Default.GetDataTable(query.ToString());

                int output = int.Parse(dataTable.Rows[0][0].ToString());

                Good(output);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Good(int count)
        {
            try
            {
                EntryRequest r = new EntryRequest();
                r.UserColumns = new SortedList<string, object>();
                r.From = WiseApp.Id;
                r.Mode = WeMaintMode.Insert;
                r.ActiveJob = WbtCustomService.ActiveValues.ActiveJob;
                r.Workcenter = WbtCustomService.ActiveValues.Workcenter;
                r.Count = count;
                r.Cavity = 1;
                JobControl.Good.Insert(r);
            }
            catch (Exception ex)
            {
                DbAccess.Default.ExecuteQuery($"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Client', 'Palletizing_2.Good', N'{ex.Message}', '{_Workcenter}', GETDATE())");
            }
        }

        private void SetLogMessage(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message} \r\n";
            if (rTB_log.Text.Length + logMessage.Length > 1932735281)
            {
                rTB_log.Text = "";
            }

            rTB_log.AppendText(logMessage);
            rTB_log.ScrollToCaret();
        }

        public void Label_Control(string barcode, string result)
        {
            if (string.IsNullOrEmpty(lbl_Current.Text))
            {
                lbl_Current.Text = barcode;
            }
            else
            {
                if (lbl_Current.ForeColor == Color.Red)
                {
                    lbl_Current.Text = barcode;
                }
                else
                {
                    lbl_prev.Text = lbl_Current.Text;
                    lbl_Current.Text = barcode;
                }
            }

            lbl_Current.ForeColor = result == "OK" ? Color.DodgerBlue : Color.Red;
        }
    }
}