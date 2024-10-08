using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn.CheckSheetEdit
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnCS3c5sSpec
    {
        private string _LIne;
        private string _Seq;

        private string _Items;
        private string _Comment;

        private DateTime _updated;

        public EditColumnCS3c5sSpec()
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
        public string Seq
        {
            get { return _Seq; }
            set { _Seq = value; }
        }
        

        [CategoryAttribute("2.ETC")]
        public string Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }


        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
