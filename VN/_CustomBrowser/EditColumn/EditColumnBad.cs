using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnBad
    {
        private string _bad;
        private string _sdi_bad;
        private string _erp_bad;
        private string _text;
        private string _bunch;
        private string _kind;
        private string _textKor;
        private int _viewseq;
        private bool _repair;
        private bool _reinsp;
        private bool _loss;
        private bool _scrap;
        private bool _status;
        private DateTime _updated;

        public EditColumnBad()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Bad
        {
            get { return _bad; }
            set { _bad = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SDI_Bad
        {
            get { return _sdi_bad; }
            set { _sdi_bad = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string ERP_Bad
        {
            get { return _erp_bad; }
            set { _erp_bad = value; }
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
        public bool Repair
        {
            get { return _repair; }
            set { _repair = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool ReInsp
        {
            get { return _reinsp; }
            set { _reinsp = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Loss
        {
            get { return _loss; }
            set { _loss = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Scrap
        {
            get { return _scrap; }
            set { _scrap = value; }
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
