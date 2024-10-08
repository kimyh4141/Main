using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnRawMaterial
    {
        private string _rawmaterial;
        private string _text;
        private bool _status;
        private DateTime _updated;
        private string _bunch;
        private string _kind;
        private string _Spec;
        private string _unit;
      
        public EditColumnRawMaterial()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string RawMaterial
        {
            get { return _rawmaterial; }
            set { _rawmaterial = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }     

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string Bunch
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
                    S = PropertyItemList._BunchItems[0];

                }
                return S;
            }
            set { _bunch = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string Kind
        {
            get 
            {
                string S = "";
                if (_bunch != null)
                {
                    S = _kind;
                }
                else
                {
                    S = PropertyItemList._KindItems[0];

                }
                return _kind; 
            }
            set { _kind = value; }
        }
       

        [CategoryAttribute("2.ETC")]
        public string Spec
        {
            get { return _Spec; }
            set { _Spec = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
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
