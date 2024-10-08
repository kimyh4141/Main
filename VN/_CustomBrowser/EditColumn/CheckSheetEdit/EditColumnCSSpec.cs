using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn.CheckSheetEdit
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnCSSpec
    {
        private string _CsCode;
        private string _LIne;

        private string _CsName;
        private string _Parts;
        private string _Route;
        private string _RouteName;
        private string _Items;
        

        private string _Checker;
        private string _Confirmer;
        private string _KeepPeriod;
        private string _Comment;
        private bool _status;      


        private DateTime _updated;


        public EditColumnCSSpec()
        {
        }



        [CategoryAttribute("1.PRIMARY KEY")]
        public string CsCode
        {
            get { return _CsCode; }
            set { _CsCode = value; }
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



        [CategoryAttribute("2.ETC")]
        public string CsName
        {
            get { return _CsName; }
            set { _CsName = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string Parts
        {
            get { return _Parts; }
            set { _Parts = value; }
        }
        [CategoryAttribute("2.ETC")]
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
        public string Items
        {
            get { return _Items; }
            set { _Items = value; }
        }



        [CategoryAttribute("2.ETC")]
        public string Checker
        {
            get { return _Checker; }
            set { _Checker = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Confirmer
        {
            get { return _Confirmer; }
            set { _Confirmer = value; }
        }
        
        [CategoryAttribute("2.ETC")]
        public string KeepPeriod
        {
            get { return _KeepPeriod; }
            set { _KeepPeriod = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
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
