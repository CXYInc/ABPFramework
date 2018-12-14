using CXY.CJS.Core.Enums;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Role.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class ListRoleInput : Pagination, IHasSort
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField { get; set; }
       
        /// <summary>
        /// 排序方式
        /// </summary>
        public SortEnum SortOrder { get; set; }
    }
}