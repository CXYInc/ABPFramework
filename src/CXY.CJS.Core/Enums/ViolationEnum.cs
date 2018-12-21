using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CXY.CJS.Core.Enums
{
    /// <summary>
    /// 违章处理状态枚举
    /// </summary>
    public enum ViolationStateEnum
    {
        /// <summary>
        /// 待办理
        /// </summary>
        [Description("待办理")]
        WaitHandle = 1,

        /// <summary>
        /// 待支付
        /// </summary>
        [Description("待支付")]
        NoPay = 2,

        /// <summary>
        /// 已支付待派单
        /// </summary>
        [Description("已支付待派单")]
        Payed = 3,

        /// <summary>
        /// 正在办理
        /// </summary>
        [Description("正在办理")]
        Handling = 4,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 5,

        /// <summary>
        /// 已退单
        /// </summary>
        [Description("已退单")]
        Backed = 6,

        /// <summary>
        /// 重下单
        /// </summary>
        [Description("重下单")]
        ReSeted = 7
    }

    /// <summary>
    /// 违章数据状态枚举
    /// </summary>
    public enum ViolationDataStatusEnum
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 1,

        /// <summary>
        /// 当前批次重复
        /// </summary>
        [Description("当前批次重复")]
        ThisBatchRepeat = 2,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 3,

        /// <summary>
        /// 正常
        /// </summary>
        [Description("其他批次重复")]
        OtherBatchRepeat = 4,

        /// <summary>
        /// 其他批次办理中
        /// </summary>
        [Description("其他批次办理中")]
        OtherBatchHandling = 5,

        /// <summary>
        /// 其他批次已完成
        /// </summary>
        [Description("其他批次已完成")]
        OtherBatchCompleted = 6
    }

    /// <summary>
    /// 违章数据可办理枚举
    /// </summary>
    public enum ViolationStatusEnum
    {
        /// <summary>
        /// 可办理
        /// </summary>
        [Description("可办理")]
        CanProcess = 0,

        /// <summary>
        /// 办理中
        /// </summary>
        [Description("办理中")]
        Handling = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        Completed = 2
    }
}
