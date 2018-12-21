using System.ComponentModel;

namespace CXY.CJS.Core.Enums
{
    public enum CarLockEnum
    {
        /// <summary>
        /// 不超证
        /// </summary>
        [Description("不超证")]
        UnLock = 0,

        /// <summary>
        /// 超证
        /// </summary>
        [Description("超证")]
        Locked = 1
    }
}
