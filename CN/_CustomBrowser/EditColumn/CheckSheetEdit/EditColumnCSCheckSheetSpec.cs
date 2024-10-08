using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn.CheckSheetEdit
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnCSCheckSheetSpec
    {
        private string _CsCode;
        private string _Seq;

        private string _CheckGroup;
        private string _CheckItems;
        private string _DataType;
        private string _DataUnit;
        private string _CheckPeriod;

        private string _ValueMin;
        private string _ValueMax;

        private DateTime _updated;

        public EditColumnCSCheckSheetSpec()
        {
        }
       
        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string CsCode
        {
            get
            {
                string S = "";
                if (_CsCode != null)
                {
                    S = _CsCode;
                }
                else
                {
                    S = PropertyItemList._cscode[0];
                }

                return S;
            }

            set { _CsCode = value; }
        }
        [CategoryAttribute("1.PRIMARY KEY")]
        public string Seq
        {
            get { return _Seq; }
            set { _Seq = value; }
        }
        
        [CategoryAttribute("2.ETC")]
        public string CheckGroup
        {
            get { return _CheckGroup; }
            set { _CheckGroup = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CheckItems
        {
            get { return _CheckItems; }
            set { _CheckItems = value; }
        }
        
        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string DataType
        {
            get
            {
                string S = "";
                if (_DataType != null)
                {
                    S = _DataType;
                }
                else
                {
                    S = PropertyItemList._datatype[0];
                }

                return S;
            }

            set { _DataType = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string DataUnit
        {
            get { return _DataUnit; }
            set { _DataUnit = value; }
        }
        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string CheckPeriod
        {
            get
            {
                string S = "";
                if (_CheckPeriod != null)
                {
                    S = _CheckPeriod;
                }
                else
                {
                    S = PropertyItemList._checkPeriod[0];
                }

                return S;
            }

            set { _CheckPeriod = value; }
        }
       
        [CategoryAttribute("2.ETC")]
        public string ValueMin
        {
            get { return _ValueMin; }
            set { _ValueMin = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string ValueMax
        {
            get { return _ValueMax; }
            set { _ValueMax = value; }
        }
      

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
