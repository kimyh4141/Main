using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn.CheckSheetEdit
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnCSOverhaulSpec
    {
        private string _LIne;
        private string _Seq;
        private string _Route;

        private string _Parts;
        private string _Items;
        private string _DataType;
        private string _CheckTiming;
        private string _CheckPeriod;

        private string _Required;
        private string _ReplaceParts;

        private string _CheckMethod;
        private string _PlannedDate;
        private string _Factor;


        private DateTime _updated;

        public EditColumnCSOverhaulSpec()
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
        [CategoryAttribute("1.PRIMARY KEY")]
        public string Route
        {
            get { return _Route; }
            set { _Route = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Parts
        {
            get { return _Parts; }
            set { _Parts = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string Items
        {
            get { return _Items; }
            set { _Items = value; }
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
        public string CheckTiming
        {
            get { return _CheckTiming; }
            set { _CheckTiming = value; }
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
        public string CheckMethod
        {
            get { return _CheckMethod; }
            set { _CheckMethod = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Required
        {
            get { return _Required; }
            set { _Required = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string ReplaceParts
        {
            get { return _ReplaceParts; }
            set { _ReplaceParts = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string PlannedDate
        {
            get { return _PlannedDate; }
            set { _PlannedDate = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
