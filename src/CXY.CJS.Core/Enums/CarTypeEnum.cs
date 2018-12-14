using System.ComponentModel;

namespace CXY.CJS.Core.Enums
{
    /// <summary>
    /// 车辆类型枚举
    /// </summary>
    public enum CarTypeEnum
    {
        /// <summary>
        /// 小型汽车
        /// </summary>
        [Description("小型汽车")]
        C = 2,

        /// <summary>
        /// 大型客车（A1）
        /// </summary>
        [Description("大型客车(A1)")]
        A1 = 1,

        /// <summary>
        /// 牵引货车（A2）
        /// </summary>
        [Description("牵引货车(A2)")]
        A2 = 1,

        /// <summary>
        /// 中型客车（B1）
        /// </summary>
        [Description("中型客车(B1)")]
        B1 = 1,

        /// <summary>
        /// 大型货车（B2）
        /// </summary>
        [Description("大型货车(B2)")]
        B2 = 1,

        /// <summary>
        /// 两、三轮摩托车
        /// </summary>
        [Description("两、三轮摩托车")]
        E = 7
    }
}
