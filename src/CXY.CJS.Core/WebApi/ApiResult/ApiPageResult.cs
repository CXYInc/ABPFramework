using System;
using System.Collections.Generic;

namespace CXY.CJS.WebApi
{
    /// <summary>
    /// Api响应分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageBaseResult<T> where T : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; } = int.MaxValue;

        public int PageTotal { get { return (int)Math.Ceiling((double)Count / PageSize); } }

        public List<T> PageData { get; set; }

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
