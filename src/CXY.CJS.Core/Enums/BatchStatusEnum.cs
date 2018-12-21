using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CXY.CJS.Core.Enums
{
    /// <summary>
    /// 批次状态枚举
    /// </summary>
    public enum BatchStatusEnum
    {
        /// <summary>
        /// 待办理
        /// </summary>
        [Description("待办理")]
        WaitHandle = 1,

        /// <summary>
        /// 正在办理
        /// </summary>
        [Description("正在办理")]
        Handling = 2,

        /// <summary>
        /// 已退单
        /// </summary>
        [Description("已退单")]
        Backed = 3,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 4,

        /// <summary>
        /// 已出账
        /// </summary>
        [Description("已出账")]
        OutAccounted = 5,

        /// <summary>
        /// 已开票
        /// </summary>
        [Description("已开票")]
        Invoiced = 6,
    }
}
