using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnRoutingBad
    {
        private string _basistable;
        private string _bad;
        private string _routing;
        private bool _status;
        private DateTime _updated;
        private string _badBunch;

        public EditColumnRoutingBad()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY"), ReadOnlyAttribute(true)]
        public string BasisTable
        {
            get { return _basistable; }
            set { _basistable = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.BUNCH")]
        public string BadBunch
        {
            get
            {
                string S = "";
                if (_badBunch != null)
                {
                    S = _badBunch;
                }
                else
                {
                    S = PropertyItemList._badBunchItems[0];
                }

                return S;
            }

            set { _badBunch = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("3.ETC")]
        public string Bad
        {
            get
            {
                string S = "";
                if (_bad != null)
                {
                    S = _bad;
                }
                else
                {
                    S = PropertyItemList._badItems[0];
                }

                return S;
            }

            set { _bad = value; }
        }

        

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("3.ETC")]
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

        [CategoryAttribute("3.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [CategoryAttribute("3.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
