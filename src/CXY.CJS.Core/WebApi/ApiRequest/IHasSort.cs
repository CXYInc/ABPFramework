using CXY.CJS.Core.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CXY.CJS.Core.WebApi
{
    /// <summary>
    /// 排序条件
    /// </summary>
    public interface IHasSort
    {
        /// <summary>
        /// 排序的字段
        /// </summary>
        string SortField { get; set; }

        /// <summary>
        /// 排序规则(正序或倒序)
        /// </summary>
        SortEnum SortOrder { get; set; } 
    }
}