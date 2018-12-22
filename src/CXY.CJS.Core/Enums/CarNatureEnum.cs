using System.ComponentModel;

namespace CXY.CJS.Core.Enums
{
    /// <summary>
    /// 车辆性质枚举
    /// </summary>
    public enum CarNatureEnum
    {
        /// <summary>
        /// 个人车
        /// </summary>
        [Description("个人车")]
        Person = 0,

        /// <summary>
        /// 单位车
        /// </summary>
        [Description("单位车")]
        Company = 1
    }
}
