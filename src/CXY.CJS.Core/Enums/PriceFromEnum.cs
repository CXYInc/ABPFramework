using System.ComponentModel;

namespace CXY.CJS.Core.Enums
{
    /// <summary>
    /// 报价来源枚举
    /// </summary>
    public enum PriceFromEnum
    {
        /// <summary>
        /// 系统报价
        /// </summary>
        [Description("系统报价")]
        System = 0,

        /// <summary>
        /// 人工报价
        /// </summary>
        [Description("人工报价")]
        Person = 1
    }
}
