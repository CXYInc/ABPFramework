using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 违章错误信息
    /// </summary>
    public class ViolationErrorInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public  string Index { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string Reason { get; set; }
    }
}
