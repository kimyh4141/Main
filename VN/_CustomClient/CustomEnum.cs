using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiseM.Client
{
    /// <summary>
    /// 
    /// </summary>
    public static class CustomEnum
    {
        /// <summary>
        /// Mounting변경유형
        /// </summary>
        public enum MountingChangeCaseType
        {
            /// <summary>
            /// 초기 값
            /// </summary>
            None = 0

           ,
            /// <summary>
            /// Mounting작업정보 생성
            /// 작업지시 시작 시 이전에 작업한 이력이 없으며, 직전모델이 일치하지 않을 경우
            /// </summary>
            Create = 1

           ,

            /// <summary>
            /// Mounting작업정보 이어하기
            /// 작업지시 시작 시 동일라인에서 직전의 모델과 작업할 모델이 일치할 경우
            /// </summary>
            Continue = 2

           ,

            /// <summary>
            /// Mounting작업정보 불러오기
            /// 작업지시 시작 시 동일라인에서 이전에 작업한 작업지시의 경우(FeederWip 유무)(일단 수삽만)
            /// </summary>
            Import = 3

           ,

            /// <summary>
            /// Mounting작업정보 중지
            /// 작업지시 종료, 취소 할 경우
            /// </summary>
            Stop = 4

           ,

            /// <summary>
            /// Mounting작업정보 재생성
            /// 작업지시 시작 시 Import, Continue 상황일 때 거부할 경우
            /// </summary>
            ReCreate = 5
        }
    }
}
