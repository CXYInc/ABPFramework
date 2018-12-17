using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Repository.SeedWork
{
    public class Pagination
    {

        /// <summary>
        /// 页码
        /// </summary>
      
        public int PageIndex { get; set; } = 1;


        /// <summary>
        /// 每页显示行数
        /// </summary>
      
        public int PageSize { get; set; } = 10;

        public  PaginationResult<TEntity> SetResult<TEntity>(int totalCount, IEnumerable<TEntity> datas)
        {
            return new PaginationResult<TEntity>
            {
                Datas = datas,
                PageIndex = this.PageIndex,
                PageSize = this.PageSize,
                TotalCount = totalCount
            };
        }
    }

}