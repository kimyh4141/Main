using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MES_Monitoring.Classes;
using MES_Monitoring.UserControls.Units;
using System.Collections.Generic;

namespace MES_Monitoring.UserControls.Lines
{
    public partial class UserControlLine : UserControl
    {
        [Browsable(true)] public int LineCode { get; set; }
        public List<UserControlUnit> UnitList = new List<UserControlUnit>();

        public UserControlLine()
        {
            InitializeComponent();
        }

        private void UserControlLineBase_Paint(object sender, PaintEventArgs e)
        {
            //Line 형태
            var linePen = new Pen(Color.Black, 1);
            var lineRectangle = new RectangleF((float)(e.ClipRectangle.Width * 0.01), (float)(e.ClipRectangle.Height * 0.25), (float)(e.ClipRectangle.Width * 0.98), (float)(e.ClipRectangle.Height * 0.5));
            e.Graphics.DrawRectangle(linePen, lineRectangle.X, lineRectangle.Y, lineRectangle.Width, lineRectangle.Height);
            //라인 코드
            var textBrush = new SolidBrush(Color.Black);
            e.Graphics.DrawString(LineCode.ToString(), Common.LineFont, textBrush, lineRectangle, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center });
        }
    }
}