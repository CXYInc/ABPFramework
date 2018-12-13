using CXY.CJS.Core.Enum;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Menu.Dto
{
    public class ListMenuInput : Pagination
    {
        public string SortField { get; set; }

    }
}