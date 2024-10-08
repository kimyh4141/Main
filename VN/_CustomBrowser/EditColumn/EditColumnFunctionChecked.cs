using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnFunctionChecked
    {
        private string _Model;
        private string _FuncError;
        private string _FuncErrorName;
        private bool _Status;
        private DateTime _Updated;

        public EditColumnFunctionChecked()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string FuncError
        {
            get { return _FuncError; }
            set { _FuncError = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string FuncErrorName
        {
            get { return _FuncErrorName; }
            set { _FuncErrorName = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _Updated; }
            set { _Updated = value; }
        }
    }
}
