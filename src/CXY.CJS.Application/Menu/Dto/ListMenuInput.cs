using CXY.CJS.Enum;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;

namespace CXY.CJS.Menu.Dto
{
    public class ListMenuInput : Pagination
    {
        public string SortField { get; set; }

    }
}