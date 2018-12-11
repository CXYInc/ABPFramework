using System.Collections.Generic;

namespace CXY.CJS.Repository.SeedWork
{
    public class PaginationResult<TEntity> : Pagination
    {
        public int TotalCount { get; set; }

        public IEnumerable<TEntity> Datas { get; set; } = new List<TEntity>();
    }
}
