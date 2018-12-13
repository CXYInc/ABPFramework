using System;
using System.Collections.Generic;

namespace CXY.CJS.Core.WebApi
{
    /// <summary>
    /// Api响应分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageBaseResult<T> where T : class
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; } = int.MaxValue;

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal { get { return (int)Math.Ceiling((double)Count / PageSize); } }

        /// <summary>
        /// 当前页数据
        /// </summary>
        public List<T> PageData { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// Api分页响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageResult<T> : ApiResult<ApiPageBaseResult<T>> where T : class
    {
    }
}
