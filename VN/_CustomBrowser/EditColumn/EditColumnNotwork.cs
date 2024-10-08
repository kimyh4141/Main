using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnNotwork
    {
        private string _notwork;
        private string _text;
        private string _textKor;
        private string _bunch;
        private string _kind;
        private int _viewseq;
        private bool _status;
        private DateTime _updated;

        public EditColumnNotwork()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Notwork
        {
            get { return _notwork; }
            set { _notwork = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string TextKor
        {
            get { return _textKor; }
            set { _textKor = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Bunch
        {
            get { return _bunch; }
            set { _bunch = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        [CategoryAttribute("2.ETC")]
        public int ViewSeq
        {
            get { return _viewseq; }
            set { _viewseq = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
