using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnOutputHist
    {
        private int _outputhist;
        private string _division;
        private string _workcenter;
        private string _material;
        private string _customer;
        private string _routing;
        private string _workorder;
        private string _shift;
        private string _clientid;
        private DateTime _started;
        private DateTime _ended;
        private int _outQty;
        private string _palletno;
        private string _boxno;
        private string _serialno;
        private bool _status;
        private DateTime _updated;

        public EditColumnOutputHist()
        {
        }

        
        [CategoryAttribute("1.PRIMARY KEY"), ReadOnlyAttribute(true)]
        public int OutputHist
        {
            get { return _outputhist; }
            set { _outputhist = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Division
        {
            get
            {
                string S = "";
                if (_division != null)
                {
                    S = _division;
                }
                else
                {
                    S = PropertyItemList._divisionItems[0];
                }

                return S;
            }

            set { _division = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Workcenter
        {
            get
            {
                string S = "";
                if (_workcenter != null)
                {
                    S = _workcenter;
                }
                else
                {
                    S = PropertyItemList._workcenterItems[0];
                }

                return S;
            }

            set { _workcenter = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
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
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Customer
        {
            get
            {
                string S = "";
                if (_customer != null)
                {
                    S = _customer;
                }
                else
                {
                    S = PropertyItemList._customerItems[0];
                }

                return S;
            }

            set { _customer = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
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

        [CategoryAttribute("2.FOREIGN KEY")]
        public string WorkOrder
        {
            get { return _workorder; }
            set { _workorder = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("3.ETC")]
        public string Shift
        {
            get
            {
                string S = "";
                if (_shift != null)
                {
                    S = _shift;
                }
                else
                {
                    S = PropertyItemList._shiftItems[0];
                }

                return S;
            }

            set { _shift = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string ClientId
        {
            get { return _clientid; }
            set { _clientid = value; }
        }

        [CategoryAttribute("3.ETC")]
        public DateTime Started
        {
            get { return _started; }
            set { _started = value; }
        }

        [CategoryAttribute("3.ETC")]
        public DateTime Ended
        {
            get { return _ended; }
            set { _ended = value; }
        }

        [CategoryAttribute("3.ETC")]
        public int OutQty
        {
            get { return _outQty; }
            set { _outQty = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string PalletNo
        {
            get { return _palletno; }
            set { _palletno = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string BoxNo
        {
            get { return _boxno; }
            set { _boxno = value; }
        }

        [CategoryAttribute("3.ETC")]
        public string SerialNo
        {
            get { return _serialno; }
            set { _serialno = value; }
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
