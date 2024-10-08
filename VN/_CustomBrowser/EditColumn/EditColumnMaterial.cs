using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnMaterial
    {
        private string _material;
        private string _text;
        private bool _status;
        private DateTime _updated;
        private string _bunch;
        private string _kind;
        private string _Spec;
        private string _TextKor;
        private string _TextEng;
        private string _TextVnm;
        private string _CycleTime;
        private string _CellID;
        private string _CellQty;
        private string _BoxQty;

        private string _Plate_A;
        private string _Plate_B;
        private string _Plate_C;
        private string _Plate_D;
        private string _Plate_E;
        private string _Plate_F;
        private string _Plate_G;

        private string _Case_A;
        private string _Case_B;
        private string _Case_C;

        private string _Pcm;

        public EditColumnMaterial()
        {
        }

        [CategoryAttribute("1.PRIMARY KEY")]
        public string Material
        {
            get { return _material; }
            set { _material = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }     

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string Bunch
        {
            get
            {
                string S = "";
                if (_bunch != null)
                {
                    S = _bunch;
                }
                else
                {
                    S = PropertyItemList._BunchItems[0];

                }
                return S;
            }
            set { _bunch = value; }
        }

        [Browsable(true)]
        [TypeConverter(typeof(MyConverter))]
        [CategoryAttribute("2.ETC")]
        public string Kind
        {
            get 
            {
                string S = "";
                if (_bunch != null)
                {
                    S = _kind;
                }
                else
                {
                    S = PropertyItemList._KindItems[0];

                }
                return _kind; 
            }
            set { _kind = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string cell_id
        {
            get { return _CellID; }
            set { _CellID = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_A
        {
            get { return _Plate_A; }
            set { _Plate_A = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_B
        {
            get { return _Plate_B; }
            set { _Plate_B = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_C
        {
            get { return _Plate_C; }
            set { _Plate_C = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_D
        {
            get { return _Plate_D; }
            set { _Plate_D = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_E
        {
            get { return _Plate_E; }
            set { _Plate_E = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_F
        {
            get { return _Plate_F; }
            set { _Plate_F = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string plate_G
        {
            get { return _Plate_G; }
            set { _Plate_G = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string case_A
        {
            get { return _Case_A; }
            set { _Case_A = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string case_B
        {
            get { return _Case_B; }
            set { _Case_B = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string case_C
        {
            get { return _Case_C; }
            set { _Case_C = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string Pcm
        {
            get { return _Pcm; }
            set { _Pcm = value; }
        }

    
        [CategoryAttribute("2.ETC")]
        public string Spec
        {
            get { return _Spec; }
            set { _Spec = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string TextKor
        {
            get { return _TextKor; }
            set { _TextKor = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string TextEng
        {
            get { return _TextEng; }
            set { _TextEng = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CycleTime
        {
            get { return _CycleTime; }
            set { _CycleTime = value; }
        }

        [CategoryAttribute("2.ETC")]
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CellQty
        {
            get { return _CellQty; }
            set { _CellQty = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string BoxQty
        {
            get { return _BoxQty; }
            set { _BoxQty = value; }
        }

        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
