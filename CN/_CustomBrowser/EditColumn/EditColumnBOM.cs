using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnBOM
    {
        private string _parent;
        private string _child;
        private decimal _reqqty;
        private bool _status;
        private DateTime _updated;

        public EditColumnBOM()
        {
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string Parent
        {
            get
            {
                string S = "";
                if (_parent != null)
                {
                    S = _parent;
                }
                else
                {
                    S = PropertyItemList._materialItems[0];
                }

                return S;
            }

            set { _parent = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string Child
        {
            get
            {
                string S = "";
                if (_child != null)
                {
                    S = _child;
                }
                else
                {
                    S = PropertyItemList._materialItems[0];
                }

                return S;
            }

            set { _child = value; }
        }

        [CategoryAttribute("2.ETC")]
        public decimal ReqQty
        {
            get { return _reqqty; }
            set { _reqqty = value; }
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
