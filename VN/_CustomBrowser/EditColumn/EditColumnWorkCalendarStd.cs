using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnWorkCalendarStd
    {
        private string _dayofweek;
        private string _workcenter;
        private string _workteam;
        private decimal _meanworker;
        private decimal _workingtime;
        private int _starthour;
        private int _endhour;
        private DateTime _updated;

        public EditColumnWorkCalendarStd()
        {
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
        public string DayOfWeek
        {
            get
            {
                string S = "";
                if (_dayofweek != null)
                {
                    S = _dayofweek;
                }
                else
                {
                    S = PropertyItemList._dayofweekItems[0];
                }

                return S;
            }

            set { _dayofweek = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("1.PRIMARY KEY")]
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
        [CategoryAttribute("1.PRIMARY KEY")]
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

        [CategoryAttribute("2.ETC")]
        public decimal MeanWorker
        {
            get { return _meanworker; }
            set { _meanworker = value; }
        }

        [CategoryAttribute("2.ETC")]
        public decimal WorkingTime
        {
            get { return _workingtime; }
            set { _workingtime = value; }
        }

        [CategoryAttribute("2.ETC")]
        public int StartHour
        {
            get { return _starthour; }
            set { _starthour = value; }
        }

        [CategoryAttribute("2.ETC")]
        public int EndHour
        {
            get { return _endhour; }
            set { _endhour = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }

    }
}
