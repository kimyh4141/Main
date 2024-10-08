namespace WiseM.Browser
{
    public static class CustomEnum
    {
        /// <summary>
        /// 자재발행유형
        /// </summary>
        public enum RawMaterialPrintType
        {
            /// <summary>
            /// 신규발행(사용안함.)
            /// </summary>
            Base = 0,

            /// <summary>
            /// 수량변경 바코드 발행
            /// </summary>
            Change = 1,

            /// <summary>
            /// 재발행
            /// </summary>
            Reprint = 2,

            /// <summary>
            /// 잔량발행, 임의발행
            /// </summary>
            Remain = 3,

            /// <summary>
            /// 분할발행
            /// </summary>
            Split = 4
        }

        /// <summary>
        /// 라벨 유형
        /// </summary>
        public enum LabelType
        {
            /// <summary>
            /// 없음
            /// </summary>
            None = 0,

            /// <summary>
            /// PCB
            /// </summary>
            Pcb = 1,

            /// <summary>
            /// 상품박스
            /// </summary>
            ProductBox = 2,

            /// <summary>
            /// 상품팰릿
            /// </summary>
            Pallet = 3
        }

        /// <summary>
        /// 재포장유형
        /// </summary>
        public enum RepackingProcessType
        {
            /// <summary>
            /// 없음
            /// </summary>
            None = 0,

            /// <summary>
            /// 신규
            /// </summary>
            New = 1,

            /// <summary>
            /// 변경
            /// </summary>
            Change = 2,

            /// <summary>
            /// 해체
            /// </summary>
            Unpacking = 3,

            /// <summary>
            /// 전부해체
            /// </summary>
            UnpackingAll = 4
        }
    }
}