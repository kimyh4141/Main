using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn.CheckSheetEdit
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnCSLineRoute
    {
        private string _LIne;
        private string _Route;

        private string _RouteName;
        private string _updater;
        private string _updaterName;
        private DateTime _updated;

        public EditColumnCSLineRoute()
        {
        }
       
        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string LIne
        {
            get
            {
                string S = "";
                if (_LIne != null)
                {
                    S = _LIne;
                }
                else
                {
                    S = PropertyItemList._csline[0];
                }

                return S;
            }

            set { _LIne = value; }
        }


        [CategoryAttribute("1.PRIMARY KEY")]
        public string Route
        {
            get { return _Route; }
            set { _Route = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string RouteName
        {
            get { return _RouteName; }
            set { _RouteName = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string updater
        {
            get { return _updater; }
            set { _updater = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string updaterName
        {
            get { return _updaterName; }
            set { _updaterName = value; }
        }
        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
