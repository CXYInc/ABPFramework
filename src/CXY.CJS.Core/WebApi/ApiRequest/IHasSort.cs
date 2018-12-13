using CXY.CJS.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace CXY.CJS.WebApi
{
    public interface IHasSort
    {
        /// <summary>
        /// 被排序的字段
        /// </summary>
        string SortField { get; set; }

        /// <summary>
        /// 倒序还是正序
        /// </summary>
        SortEnum SortOrder { get; set; } 
    }
}