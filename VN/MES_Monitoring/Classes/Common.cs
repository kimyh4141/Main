using System;
using System.Drawing;

namespace MES_Monitoring.Classes
{
    public static class Common
    {
        public static Font LineFont = new Font("Tahoma", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);

        public static class Page
        {
            /// <summary>
            /// 화면 구분
            /// </summary>
            public enum Type
            {
                /// <summary>
                /// [ 전체 ] 종합 생산진행 & 설비 상태 (요약)
                /// </summary>
                MAIN = 0,

                /// <summary>
                /// [ AI ] 종합 생산진행 & 설비 상태
                /// </summary>
                AI = 1,

                /// <summary>
                /// [ SMT ] 종합 생산진행 & 설비 상태
                /// </summary>
                SMT = 2,

                /// <summary>
                /// [ MI ] 종합 생산진행 & 설비 상태
                /// </summary>
                MI = 3,

                /// <summary>
                ///     [ BAD ] 종합 생산/불량현황
                /// </summary>
                BAD = 4
            }

            public static string GetTitle(Type type)
            {
                switch (type)
                {
                    case Type.MAIN:
                        return "[ 전체 ] 종합 생산진행 & 설비 상태 (요약)";
                    case Type.AI:
                        return "[ AI ] 종합 생산진행 & 설비 상태";
                    case Type.SMT:
                        return "[ SMT ] 종합 생산진행 & 설비 상태";
                    case Type.MI:
                        return "[ MI ] 종합 생산진행 & 설비 상태";
                    case Type.BAD:
                        return "[ BAD ] 종합 생산/불량현황";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }

            public static Type GetNextPageType(Type type)
            {
                switch (type)
                {
                    case Type.MAIN:
                        return Type.AI;
                    case Type.AI:
                        return Type.SMT;
                    case Type.SMT:
                        return Type.MI;
                    case Type.MI:
                        return Type.BAD;
                    case Type.BAD:
                        return Type.MAIN;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
        }

        public static class Routing
        {
            /// <summary>
            /// 공정 상태
            /// </summary>
            public enum Condition
            {
                /// <summary>
                /// 없음
                /// </summary>
                None = 0,

                /// <summary>
                /// 가동 : 5분 내 생산량 有
                /// </summary>
                Active = 1,

                /// <summary>
                /// 계획 정지 : 계획 정비 , 계획 휴업
                /// </summary>
                PlanStop = 2,

                /// <summary>
                /// 순간 정지 : 5분 초과 생산량 無
                /// </summary>
                Stop = 3,

                /// <summary>
                /// 고장(LongStop) : 설비 상태 고장
                /// </summary>
                LongStop = 4,

                /// <summary>
                /// PM 일정 경과 : 수리 기한 경과
                /// </summary>
                PM = 5
            }

            /// <summary>
            /// 타입
            /// </summary>
            public enum Type
            {
                /// <summary>
                /// 없음
                /// </summary>
                None = 0,

                /// <summary>
                /// AI 로딩
                /// </summary>
                AI_Load = 1,

                /// <summary>
                /// AI 언로딩
                /// </summary>
                AI_Unload = 2,

                /// <summary>
                /// SMT 로딩
                /// </summary>
                SMT_Load = 3,

                /// <summary>
                /// SMT 언로딩
                /// </summary>
                SMT_Unload = 4,

                /// <summary>
                /// 수삽
                /// </summary>
                MI_Load = 5,

                /// <summary>
                /// 1차검사
                /// </summary>
                Mi_1stFunc = 6,

                /// <summary>
                /// 내압검사
                /// </summary>
                Mi_Voltage = 7,

                /// <summary>
                /// 2차검사
                /// </summary>
                Mi_2ndFunc = 8,

                /// <summary>
                /// Packing
                /// </summary>
                Pk_Boxing = 9
            }

            public static Color GetBackColor(Condition condition)
            {
                switch (condition)
                {
                    case Condition.None:
                        return Color.White;
                    case Condition.Active:
                        return Color.Lime;
                    case Condition.PlanStop:
                        return Color.White;
                    case Condition.Stop:
                        return Color.Gold;
                    case Condition.LongStop:
                        return Color.Red;
                    case Condition.PM:
                        return Color.Lime;
                    default:
                        return Color.Lime;
                }
            }

            public static Color GetBorderColor(Condition condition)
            {
                switch (condition)
                {
                    case Condition.None:
                        return Color.Black;
                    case Condition.Active:
                        return Color.Black;
                    case Condition.PlanStop:
                        return Color.Black;
                    case Condition.Stop:
                        return Color.Black;
                    case Condition.LongStop:
                        return Color.Black;
                    case Condition.PM:
                        return Color.Red;
                    default:
                        return Color.Black;
                }
            }
        }
    }
}