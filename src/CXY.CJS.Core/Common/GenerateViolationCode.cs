using CXY.CJS.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Core.Common
{
    /// <summary>
    /// 系统定制帮助类
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// 生成违章代码
        /// </summary>
        /// <param name="carNumber">车牌号</param>
        /// <param name="vTime">违章时间</param>
        /// <param name="vReason">违章原因</param>
        /// <returns></returns>
        public static string GenerateViolationCode(string carNumber, string vTime, string vReason)
        {
            vReason = string.IsNullOrEmpty(vReason) ? "" : vReason.Trim();
            return Encryptor.MD5EntryCN(carNumber.Trim() + DateTime.Parse(vTime.Trim()).ToString("yyyy-MM-dd HH:mm:ss") + vReason.Trim());
        }
    }
}
