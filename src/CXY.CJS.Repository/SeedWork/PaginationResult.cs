using System.Collections.Generic;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Repository.SeedWork
{
    public class PaginationResult<TEntity> : Pagination
    {
        public int TotalCount { get; set; }

        public IEnumerable<TEntity> Datas { get; set; } = new List<TEntity>();

        public PaginationResult()
        {
        }

        public PaginationResult(Pagination p)
        {
            this.PageIndex = p.PageIndex;
            this.PageSize = p.PageSize;
        }

        public ApiPageResult<TEntity> ToApiPageResult()
        {
            return new ApiPageResult<TEntity>
            {
                Code = 1,
                Data = new ApiPageBaseResult<TEntity>
                {
                    Count = TotalCount,
                    PageData = Datas,
                    PageSize = PageSize,
                    PageIndex = PageIndex
                }
            };
        }


    }
}
