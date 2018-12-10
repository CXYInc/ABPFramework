using CXY.CJS.Enum;
using CXY.CJS.WebApi;

namespace CXY.CJS.Role.Dto
{
    public class ListRoleInput : Pagination, IHasSort
    {
        public string SortField { get; set; }

        public SortEnum SortOrder { get; set; }
    }
}