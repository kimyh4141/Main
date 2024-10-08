using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace MES_Monitoring.UserControls.Units
{
    public partial class UserControlUnit : UserControl, INotifyPropertyChanged
    {
        #region Field

        private string _text;
        
        /// <summary>
        /// 유닛텍스트
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => string.IsNullOrEmpty(_text) ? "" : _text; 
            set => SetField(ref _text, value);
        }

        private Common.Routing.Condition _condition;

        /// <summary>
        /// 유닛상태
        /// </summary>
        [Browsable(true)]
        public Common.Routing.Condition Condition
        {
            get => _condition;
            set => SetField(ref _condition, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructor

        public UserControlUnit()
        {
            InitializeComponent();
            PropertyChanged += (sender, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Text):
                        break;
                    case nameof(Condition):
                        break;
                }
            };
        }

        #endregion

        #region Method

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        #endregion

        #region Event

        private void UserControlUnitBase_Load(object sender, EventArgs e)
        {
        }

        private void UserControlUnitBase_Paint(object sender, PaintEventArgs e)
        {
            //Line 형태
            // var linePen = new Pen(Color.Black, 1);
            // var lineRectangle = new Rectangle(5, LineAreaH / 2 - Common.LineThickness / 2, e.ClipRectangle.Width - 10,
            //     Common.LineThickness);
            // e.Graphics.DrawRectangle(linePen, lineRectangle);
            //Unit 
            //크기
            //  가로 : 전체의 3/4
            //  세로 : 전체의 3/4
            var unitRectangle = new RectangleF((float)(e.ClipRectangle.Width * 0.125), (float)(e.ClipRectangle.Height * 0.125), (float)(e.ClipRectangle.Width * 0.75), (float)(e.ClipRectangle.Height * 0.75));
            var unitBrush = new SolidBrush(Common.Routing.GetBackColor(Condition));
            var unitPen = new Pen(Common.Routing.GetBorderColor(Condition), 2);
            e.Graphics.FillRectangle(unitBrush, unitRectangle);
            e.Graphics.DrawRectangle(unitPen, unitRectangle.X, unitRectangle.Y, unitRectangle.Width, unitRectangle.Height);

            var textBrush = new SolidBrush(Color.Black);
            e.Graphics.DrawString(Text, Common.LineFont, textBrush, unitRectangle, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}