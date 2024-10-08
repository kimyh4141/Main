using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnWorkcenter
    {
        private string _workcenter;
        private string _text;
        private string _textKor;
        private string _division;
        private string _routing;
        private string _bunch;
        private string _kind;
        private string _sdiline;
        private int _viewseq;
        private bool _status;
        private bool _workstatus;
        private DateTime _updated;
        private string _allownotwork;

        public EditColumnWorkcenter()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Workcenter
        {
            get { return _workcenter; }
            set { _workcenter = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Division
        {
            get
            {
                string S = "";
                if (_division != null)
                {
                    S = _division;
                }
                else
                {
                    S = PropertyItemList._divisionItems[0];
                }

                return S;
            }
            set { _division = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Routing
        {
            get
            {
                string S = "";
                if (_routing != null)
                {
                    S = _routing;
                }
                else
                {
                    S = PropertyItemList._routingItems[0];
                }

                return S;
            }
            set { _routing = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string BunchWorkcenter
        {
            get
            {
                string S = "";
                if (_bunch != null)
                {
                    S = _bunch;
                }
                else
                {
                    S = PropertyItemList._bunchworkcenterItems[0];
                }

                return S;
            }
            set { _bunch = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string TextKor
        {
            get { return _textKor; }
            set { _textKor = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string SdiLine
        {
            get { return _sdiline; }
            set { _sdiline = value; }
        }

        [CategoryAttribute("3.ETC")]
        public int ViewSeq
        {
            get { return _viewseq; }
            set { _viewseq = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string AllowNotwork
        {
            get { return _allownotwork; }
            set { _allownotwork = value; }
        }

        [CategoryAttribute("3.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [CategoryAttribute("3.ETC")]
        public bool WorkStatus
        {
            get { return _workstatus; }
            set { _workstatus = value; }
        }

        [CategoryAttribute("3.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
