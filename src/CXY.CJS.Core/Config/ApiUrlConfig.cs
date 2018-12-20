using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Core.Config
{
    /// <summary>
    /// Api地址
    /// </summary>
    [ConfigModel]
    public class ApiUrlConfig
    {
        /// <summary>
        /// 查询发动机和车架号位数接口地址
        /// </summary>
        public string QueryInputConditionApiUrl { get; set; }

        /// <summary>
        /// 查询发动机和车架号接口地址
        /// </summary>
        public string QueryCarCodeAndEngineCodeApiUrl { get; set; }

        /// <summary>
        /// 违章查询接口地址
        /// </summary>
        public string QueryViolationApiUrl { get; set; }
    }
}
