using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WiseM.Browser.EditColumn
{
    [DefaultPropertyAttribute("KeyValue")]
    public class EditColumnFctSpec
    {
        private string _materialcode;
        private string _ocv_min;
        private string _ocv_max;
        private string _ir_min;
        private string _ir_max;
        private string _sar_st_min;
        private string _sar_st_max;
        private string _sar_sv_min;
        private string _sar_sv_max;
        private string _sar_srv_min;
        private string _sar_srv_max;
        private string _dccv_min;
        private string _dccv_max;
        private string _cccv_min;
        private string _cccv_max;
        private string _cell_ocv_min;
        private string _cell_ocv_max;
        private string _focv_min;
        private string _focv_max;
        private string _cnt_t1_min;
        private string _cnt_t1_max;
        private string _cnt_v1_min;
        private string _cnt_v1_max;
        private string _cnt_t2_min;
        private string _cnt_t2_max;
        private string _cnt_v2_min;
        private string _cnt_v2_max;
        private string _r1_vfr_min;
        private string _r1_vfr_max;
        private string _r2_cfr_min;
        private string _r2_cfr_max;
        private DateTime _updated;

        public EditColumnFctSpec()
        {
        }
        
        [CategoryAttribute("2.ETC")]
        public string MaterialCode
        {
            get { return _materialcode; }
            set { _materialcode = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string OCV_Min
        {
            get { return _ocv_min; }
            set { _ocv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string OCV_Max
        {
            get { return _ocv_max; }
            set { _ocv_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string IR_Min
        {
            get { return _ir_min; }
            set { _ir_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string IR_Max
        {
            get { return _ir_max; }
            set { _ir_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string SAR_ST_Min
        {
            get { return _sar_st_min; }
            set { _sar_st_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SAR_ST_Max
        {
            get { return _sar_st_max; }
            set { _sar_st_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SAR_SV_Min
        {
            get { return _sar_sv_min; }
            set { _sar_sv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SAR_SV_Max
        {
            get { return _sar_sv_max; }
            set { _sar_sv_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SAR_SRV_Min
        {
            get { return _sar_srv_min; }
            set { _sar_srv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string SAR_SRV_Max
        {
            get { return _sar_srv_max; }
            set { _sar_srv_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string DCCV_Min
        {
            get { return _dccv_min; }
            set { _dccv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string DCCV_Max
        {
            get { return _dccv_max; }
            set { _dccv_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CCCV_Min
        {
            get { return _cccv_min; }
            set { _cccv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CCCV_Max
        {
            get { return _cccv_max; }
            set { _cccv_max = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CELL_OCV_Min
        {
            get { return _cell_ocv_min; }
            set { _cell_ocv_min = value; }
        }

        [CategoryAttribute("2.ETC")]
        public string CELL_OCV_Max
        {
            get { return _cell_ocv_max; }
            set { _cell_ocv_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string FOCV_Min
        {
            get { return _focv_min; }
            set { _focv_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string FOCV_Max
        {
            get { return _focv_max; }
            set { _focv_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_T1_Min
        {
            get { return _cnt_t1_min; }
            set { _cnt_t1_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_T1_Max
        {
            get { return _cnt_t1_max; }
            set { _cnt_t1_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_V1_Min
        {
            get { return _cnt_v1_min; }
            set { _cnt_v1_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_V1_Max
        {
            get { return _cnt_v1_max; }
            set { _cnt_v1_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_T2_Min
        {
            get { return _cnt_t2_min; }
            set { _cnt_t2_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_T2_Max
        {
            get { return _cnt_t2_max; }
            set { _cnt_t2_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_V2_Min
        {
            get { return _cnt_v2_min; }
            set { _cnt_v2_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string CNT_V2_Max
        {
            get { return _cnt_v2_max; }
            set { _cnt_v2_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string R1_VFR_Min
        {
            get { return _r1_vfr_min; }
            set { _r1_vfr_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string R1_VFR_Max
        {
            get { return _r1_vfr_max; }
            set { _r1_vfr_max = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string R2_CFR_Min
        {
            get { return _r2_cfr_min; }
            set { _r2_cfr_min = value; }
        }
        [CategoryAttribute("2.ETC")]
        public string R2_CFR_Max
        {
            get { return _r2_cfr_max; }
            set { _r2_cfr_max = value; }
        }
        [CategoryAttribute("2.ETC"), ReadOnlyAttribute(true)]
        public DateTime Updated
        {
            get { return _updated; }
            set { _updated = value; }
        }
    }
}
