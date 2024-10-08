using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Pages;
using Timer = System.Windows.Forms.Timer;

namespace MES_Monitoring.Forms
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        /// <summary>
        ///     화면 플리커 제거
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ExStyle |= 0x02000000;
                return createParams;
            }
        }

        private void MainForm_StyleChanged(object sender, EventArgs e)
        {
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        #region Filed

        private Common.Page.Type _currentPageType = Common.Page.Type.MAIN;
        private readonly Timer _changePageTimer = new Timer();
        private readonly Page_Main _pageMain = new Page_Main();
        private readonly Page_AI _pageAi = new Page_AI();
        private readonly Page_SMT _pageSmt = new Page_SMT();
        private readonly Page_MI _pageMi = new Page_MI();
        private readonly Page_BAD _pageBad = new Page_BAD();
        private readonly DataSet _dataSetConfig = new DataSet();
        private DataSet _dataSet = new DataSet();

        #endregion

        #region Method

        private bool ReadXmlConfig(DataSet dataSet)
        {
            var xmlFileName = $@"{Application.StartupPath}\MonitoringConfig.xml";
            try
            {
                dataSet.ReadXml(xmlFileName);
                return true;
            }
            catch (FileNotFoundException)
            {
                using (var xmlTextWriter = new XmlTextWriter(xmlFileName, Encoding.UTF8))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;

                    xmlTextWriter.WriteStartDocument();

                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    xmlTextWriter.WriteComment(" Monitoring 환경설정 ");
                    xmlTextWriter.WriteComment("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                    xmlTextWriter.WriteStartElement("MonitoringConfig");

                    xmlTextWriter.WriteComment(
                        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    xmlTextWriter.WriteComment(" ConnectionIP : MES서버 IP주소                           ");
                    xmlTextWriter.WriteComment(" ChangeTick   : 화면 갱신 주기 [단위:천분의일초] ");
                    xmlTextWriter.WriteComment(
                        "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    xmlTextWriter.WriteElementString("ConnectionIP", "121.126.143.101");
                    // xmlTextWriter.WriteElementString("ConnectionIP", "192.168.109.3");
                    xmlTextWriter.WriteElementString("ChangeTick", "10000");
                    xmlTextWriter.WriteEndElement();
                }

                try
                {
                    dataSet.ReadXml(xmlFileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InsertIntoSysLog(ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
                return false;
            }
        }

        private static void DrawToolStripButton(Graphics graphics, ToolStripButton toolStripButton)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            // var rectangle = new RectangleF(0, 0, toolStripButton.ContentRectangle.Width, toolStripButton.ContentRectangle.Height);
            var rectangleF = new RectangleF(toolStripButton.ContentRectangle.Location,
                toolStripButton.ContentRectangle.Size);
            var rectangleRoundStyles = new[]
            {
                CustomGraphics.RectangleRoundPoint.TopEnd, CustomGraphics.RectangleRoundPoint.BottomEnd
            };
            using (var path = CustomGraphics.GetRoundedRectanglePath(rectangleF, 16, rectangleRoundStyles))
            {
                var stringFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.DirectionVertical
                };
                SolidBrush backGroundBrush;
                SolidBrush textBrush;
                var pen = new Pen(Color.Gray, 3);
                //하이라이트 & 텍스트
                if (toolStripButton.Selected
                    || toolStripButton.Checked)
                {
                    backGroundBrush = new SolidBrush(Color.CornflowerBlue);
                    textBrush = new SolidBrush(Color.White);
                }
                else
                {
                    backGroundBrush = new SolidBrush(Color.White);
                    textBrush = new SolidBrush(Color.DarkGray);
                }

                graphics.FillPath(backGroundBrush, path);
                graphics.DrawString(toolStripButton.Text, toolStripButton.Font, textBrush, rectangleF, stringFormat);

                //테두리
                graphics.DrawPath(pen, path);
            }
        }

        private UserControl GetPageControlByType(Common.Page.Type type)
        {
            switch (type)
            {
                case Common.Page.Type.MAIN:
                    return _pageMain;
                case Common.Page.Type.AI:
                    return _pageAi;
                case Common.Page.Type.SMT:
                    return _pageSmt;
                case Common.Page.Type.MI:
                    return _pageMi;
                case Common.Page.Type.BAD:
                    return _pageBad;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private ToolStripButton GetLeftToolStripButtonByType(Common.Page.Type type)
        {
            switch (type)
            {
                case Common.Page.Type.MAIN:
                    return toolStripButton_Main;
                case Common.Page.Type.AI:
                    return toolStripButton_AI;
                case Common.Page.Type.SMT:
                    return toolStripButton_SMT;
                case Common.Page.Type.MI:
                    return toolStripButton_MI;
                case Common.Page.Type.BAD:
                    return toolStripButton_BAD;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void ChangeViewPage(Common.Page.Type type)
        {
            //현재 페이지타입 변경
            _currentPageType = type;

            //ViewPage 변경
            var page = GetPageControlByType(type);
            panel_Body.Controls[panel_Body.Controls.IndexOf(page)].BringToFront();
            label_Title.Text = Common.Page.GetTitle(type);

            //사이드바 체크 변경
            var nextToolStripButton = GetLeftToolStripButtonByType(type);
            foreach (ToolStripItem toolStripItem in nextToolStripButton.Owner.Items)
            {
                if (!(toolStripItem is ToolStripButton toolStripButton)) continue;
                if (nextToolStripButton.Equals(toolStripItem))
                {
                    toolStripButton.CheckState = CheckState.Checked;
                    continue;
                }
                toolStripButton.CheckState = CheckState.Unchecked;
            }

        }

        private void RunProcess()
        {
            _changePageTimer.Stop();
            try
            {
                if (ImportDataSet())
                {
                    label_Date.Text = Convert.ToDateTime(_dataSet.Tables[_dataSet.Tables.Count - 1].Rows[0]["DTTM"])
                        .ToString("MM\\/dd");
                    label_Time.Text = Convert.ToDateTime(_dataSet.Tables[_dataSet.Tables.Count - 1].Rows[0]["DTTM"])
                        .ToString("HH:mm");
                    _pageMain.SetPage(_dataSet);
                    _pageAi.SetPage(_dataSet);
                    _pageSmt.SetPage(_dataSet);
                    _pageMi.SetPage(_dataSet);
                    _pageBad.SetPage(_dataSet);
                    //화면자동전환 시
                    if (toolStripButton_AutoChangePage.Checked)
                        ChangeViewPage(Common.Page.GetNextPageType(_currentPageType));
                }

                _changePageTimer.Start();
            }
            catch (Exception ex)
            {
                InsertIntoSysLog(ex.Message);
            }
        }

        private bool ImportDataSet()
        {
            try
            {
                var strCmd = @"exec [Sp_ProdMonitoring]";
                _dataSet = DbAccess.GetDataSet(strCmd);
                if (_dataSet == null || _dataSet.Tables.Count != 4)
                    throw new Exception("Network problem occurred.");
                int result = Convert.ToInt16(_dataSet.Tables[_dataSet.Tables.Count - 1].Rows[0]["RC"]);
                if (result != 0) return false;
            }
            catch (Exception ex)
            {
                // 에러가 발생하면 재시작만 할 수 있도록
                // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InsertIntoSysLog(ex.Message);
                return false;
            }

            return true;
        }

        private static void InsertIntoSysLog(string strMsg)
        {
            strMsg = strMsg.Replace("'", "\a");
            DbAccess.ExecuteQuery(
                $"INSERT INTO SysLog (type, category, source, message, [user], updated) VALUES ('E',  'Monitoring', '{Application.ProductName}', LEFT(ISNULL(N'{strMsg}',''),3000), '', GETDATE())");
        }

        #endregion

        #region Event

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (!ReadXmlConfig(_dataSetConfig))
                {
                    Close();
                    Environment.Exit(0);
                }
                panel_Body.Controls.AddRange(new Control[]{ _pageMain, _pageAi, _pageSmt, _pageMi, _pageBad});
                ConfigData.ConnectionIP = _dataSetConfig.Tables[0].Rows[0]["ConnectionIP"].ToString().Trim();
                ConfigData.ChangeTick = int.Parse(_dataSetConfig.Tables[0].Rows[0]["ChangeTick"].ToString().Trim());
                _changePageTimer.Interval = ConfigData.ChangeTick;
                _changePageTimer.Tick += ChangePageTimerTick;

                ChangeViewPage(Common.Page.Type.MAIN);
                RunProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DbAccess.InsertIntoSystemLog("E", ex.Message);
            }
        }

        public void ChangePageTimerTick(object sender, EventArgs e)
        {
            RunProcess();
        }

        private void pictureBox_Logo_DoubleClick(object sender, EventArgs e)
        {
        }

        private void toolStrip_Left_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!(sender is ToolStrip toolStrip)) return;
            foreach (ToolStripItem toolStripItem in toolStrip.Items)
            {
                if (!(toolStripItem is ToolStripButton toolStripButton)) continue;
                if (e.ClickedItem.Equals(toolStripItem))
                {
                    toolStripButton.CheckState = CheckState.Checked;
                    continue;
                }

                toolStripButton.CheckState = CheckState.Unchecked;
            }
        }

        private void toolStripButton_Main_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is ToolStripButton toolStripButton)) return;
            DrawToolStripButton(e.Graphics, toolStripButton);
        }

        private void toolStripButton_Main_Click(object sender, EventArgs e)
        {
            if (_currentPageType == Common.Page.Type.MAIN) return;
            ChangeViewPage(Common.Page.Type.MAIN);
            RunProcess();
        }

        private void toolStripButton_AI_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is ToolStripButton toolStripButton)) return;
            DrawToolStripButton(e.Graphics, toolStripButton);
        }

        private void toolStripButton_AI_Click(object sender, EventArgs e)
        {
            if (_currentPageType == Common.Page.Type.AI) return;
            ChangeViewPage(Common.Page.Type.AI);
        }

        private void toolStripButton_SMT_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is ToolStripButton toolStripButton)) return;
            DrawToolStripButton(e.Graphics, toolStripButton);
        }

        private void toolStripButton_SMT_Click(object sender, EventArgs e)
        {
            if (_currentPageType == Common.Page.Type.SMT) return;
            ChangeViewPage(Common.Page.Type.SMT);
        }

        private void toolStripButton_MI_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is ToolStripButton toolStripButton)) return;
            DrawToolStripButton(e.Graphics, toolStripButton);
        }

        private void toolStripButton_MI_Click(object sender, EventArgs e)
        {
            if (_currentPageType == Common.Page.Type.MI) return;
            ChangeViewPage(Common.Page.Type.MI);
        }

        private void toolStripButton_BAD_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is ToolStripButton toolStripButton)) return;
            DrawToolStripButton(e.Graphics, toolStripButton);
        }

        private void toolStripButton_BAD_Click(object sender, EventArgs e)
        {
            if (_currentPageType == Common.Page.Type.BAD) return;
            ChangeViewPage(Common.Page.Type.BAD);
            
        }

        private void toolStripButton_AutoChangePage_CheckedChanged(object sender, EventArgs e)
        {
            // _changePageTimer.Enabled = toolStripButton_AutoChangePage.Checked;
        }

        private void label_Title_DoubleClick(object sender, EventArgs e)
        {
            switch (FormBorderStyle)
            {
                case FormBorderStyle.None:
                    FormBorderStyle = FormBorderStyle.Sizable;
                    WindowState = FormWindowState.Normal;
                    break;
                case FormBorderStyle.Sizable:
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Maximized;
                    break;
            }
            Invalidate();
        }

        private void tableLayoutPanel_DateTime_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}