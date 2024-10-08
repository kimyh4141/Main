using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnMaterialRouting
    {
        private string _material;
        private string _routing;
        private string _workcenter;
        private int _issueseq;
        private decimal _cycletime;
        private bool _status;
        private DateTime _ApplyDate;

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string Material
        {
            get
            {
                string S = "";
                if (_material != null)
                {
                    S = _material;
                }
                else
                {
                    S = PropertyItemList._materialItems[0];
                }

                return S;
            }

            set { _material = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
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

        //[Browsable(true)]
        //[TypeConverter(typeof(MyConverter))]
        //[CategoryAttribute("1.PRIMARY KEY")]
        //public string Workcenter
        //{
        //    get
        //    {
        //        string S = "";
        //        if (_workcenter != null)
        //        {
        //            S = _workcenter;
        //        }
        //        else
        //        {
        //            S = PropertyItemList._workcenterItems[0];
        //        }

        //        return S;
        //    }

        //    set { _workcenter = value; }
        //}

        [CategoryAttribute("1.PRIMARY KEY")]
        public int IssueSeq
        {
            get { return _issueseq; }
            set { _issueseq = value; }
        }

        [CategoryAttribute("2.ETC")]
        public decimal CycleTime
        {
            get { return _cycletime; }
            set { _cycletime = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [CategoryAttribute("2.ETC")]
        public DateTime ApplyDate
        {
            get { return _ApplyDate; }
            set { _ApplyDate = value; }
        }
    }
}
