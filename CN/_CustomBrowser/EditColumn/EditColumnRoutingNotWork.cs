using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnRoutingNotWork
    {
        private string _basistable;
        private string _notwork;
        private string _routing;
        private bool _status;
        private DateTime _updated;
        private string _notworkBunch;

        public EditColumnRoutingNotWork()
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
        public string NotworkBunch
        {
            get
            {
                string S = "";
                if (_notworkBunch != null)
                {
                    S = _notworkBunch;
                }
                else
                {
                    S = PropertyItemList._notworkBunchItems[0];
                }

                return S;
            }

            set { _notworkBunch = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("3.ETC")]
        public string Notwork
        {
            get
            {
                string S = "";
                if (_notwork != null)
                {
                    S = _notwork;
                }
                else
                {
                    S = PropertyItemList._notworkItems[0];
                }

                return S;
            }

            set { _notwork = value; }
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
