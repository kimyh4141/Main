using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnLocationGroup
    {
        private string _locationgroup;
        private string _locationgrouptext;
        private DateTime _updated;

        public EditColumnLocationGroup()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string LocationGroup
        {
            get { return _locationgroup; }
            set { _locationgroup = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string LocationGroupText
        {
            get { return _locationgrouptext; }
            set { _locationgrouptext = value; }
        }
       
        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
