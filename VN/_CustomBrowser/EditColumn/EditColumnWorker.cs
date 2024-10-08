using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnWorker
    {
        private string _worker;
        private string _workername;
        private bool _status;
        private DateTime _updated;
        private string _workteam;

        public EditColumnWorker()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Worker
        {
            get { return _worker; }
            set { _worker = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string WorkerName
        {
            get { return _workername; }
            set { _workername = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string WorkTeam
        {
            get
            {
                string S = "";
                if (_workteam != null)
                {
                    S = _workteam;
                }
                else
                {
                    S = PropertyItemList._workteamItems[0];
                }

                return S;
            }

            set { _workteam = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
