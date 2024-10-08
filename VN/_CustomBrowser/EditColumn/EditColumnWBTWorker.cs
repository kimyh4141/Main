using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnWBTWorker
    {
        private string _worker;
        private string _clientID;
        private bool _status;

        public EditColumnWBTWorker()
        {
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string ClientID
        {
            get
            {
                string S = "";
                if (_clientID != null)
                {
                    S = _clientID;
                }
                else
                {
                    S = PropertyItemList._clientIDItems[0];
                }

                return S;
            }
            set { _clientID = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.FOREIGN KEY")]
        public string Worker
        {
            get
            {
                string S = "";
                if (_worker != null)
                {
                    S = _worker;
                }
                else
                {
                    S = PropertyItemList._workerItems[0];
                }

                return S;
            }
            set { _worker = value; }
        }

        [CategoryAttribute("3.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }
        
    }
}
