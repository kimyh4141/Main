using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnMaterialMapping
    {
        private string _material;
        private bool _status;
        private DateTime _updated;
        private string _model;
        private string _product;
        private string _ErpMaterial;
        private string _CustomerMaterial;
        private string _CellModel;
        private string _CellCode;
        private int _CellQty;      
        private string _CorePackCode;
        private string _BarCode_Digit;
        private string _BarCode_Check;
        private bool _CpkCheck;
        private string _Rev;
        private string _CycleTime;
		private string _BoxCode_Check;
		private string _Box_digit;
		private string _Box_Qty;
        

        public EditColumnMaterialMapping()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CycleTime
        {
            get { return _CycleTime; }
            set { _CycleTime = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Product
        {
            get { return _product; }
            set { _product = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string BarCode_Digit
        {
            get { return _BarCode_Digit; }
            set { _BarCode_Digit = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string BarCode_Check
        {
            get { return _BarCode_Check; }
            set { _BarCode_Check = value; }
        }

		[CategoryAttribute("2.ETC")]
		public string Box_digit
		{
			get { return _Box_digit; }
			set { _Box_digit = value; }
		}

		[CategoryAttribute("2.ETC")]
		public string BoxCode_Check
		{
			get { return _BoxCode_Check; }
			set { _BoxCode_Check = value; }
		}

		[CategoryAttribute("2.ETC")]
		public string BoxQty
		{
			get { return _Box_Qty; }
			set { _Box_Qty = value; }
		}

		[CategoryAttribute("2.ETC")]
        public string Rev
        {
            get { return _Rev; }
            set { _Rev = value; }
        }

      
        [CategoryAttribute("2.ETC")]
        public string ErpMaterial
        {
            get { return _ErpMaterial; }
            set { _ErpMaterial = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CustomerMaterial
        {
            get { return _CustomerMaterial; }
            set { _CustomerMaterial = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CellModel
        {
            get { return _CellModel; }
            set { _CellModel = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CellCode
        {
            get { return _CellCode; }
            set { _CellCode = value; }
        }

        [CategoryAttribute("2.ETC")]
        public int CellQty
        {
            get { return _CellQty; }
            set { _CellQty = value; }
        }


        [CategoryAttribute("2.ETC")]
        public string CorePackCode
        {
            get { return _CorePackCode; }
            set { _CorePackCode = value; }
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

        [CategoryAttribute("2.ETC")]
        public bool CpkCheck
        {
            get { return _CpkCheck; }
            set { _CpkCheck = value; }
        }
    }
}
