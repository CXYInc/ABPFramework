using System.Collections.Generic;

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

        public PaginationResult<TEntity> SetReuslt(int totalCount, IEnumerable<TEntity> datas)
        {
            this.TotalCount = totalCount;
            this.Datas = datas;
            return this;
        }


    }
}
