using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnLocation
    {
        private string _locationgroup;
        private string _location;
        private string _locationtext;
        private string _kind;
        private string _bunch;
        private DateTime _updated;

        public EditColumnLocation()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string LocationGroup
        {
            get
            {
                string S = "";
                if (_locationgroup != null)
                {
                    S = _locationgroup;
                }
                else
                {
                    S = PropertyItemList._LocationGroup[0];
                }

                return S;
            }           
            set { _locationgroup = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string LocationText
        {
            get { return _locationtext; }
            set { _locationtext = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Kind
        {
            get { return _kind; }
            set { _kind = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Bunch
        {
            get { return _bunch; }
            set { _bunch = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
