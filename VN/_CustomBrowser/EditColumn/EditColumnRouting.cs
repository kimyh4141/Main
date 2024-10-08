using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnRouting
    {
        private string _routing;
        private string _text;
        private string _bunch;
        private string _kind;
        private string _textkor;
        private string _texteng;
        private string _textvnm;
        private int _viewseq;
        private bool _status;
        private DateTime _updated;

        public EditColumnRouting()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Routing
        {
            get { return _routing; }
            set { _routing = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
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
        public string TextKor
        {
            get { return _textkor; }
            set { _textkor = value; }
        }


        [CategoryAttribute("2.ETC")]
        public string TextEng
        {
            get { return _texteng; }
            set { _texteng = value; }
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
