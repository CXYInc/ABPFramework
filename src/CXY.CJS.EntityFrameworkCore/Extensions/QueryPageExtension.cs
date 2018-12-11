using Abp.Domain.Entities;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Enum;
using CXY.CJS.WebApi;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Repository.Extensions;

namespace CXY.CJS.Extensions
{
    public static class QueryPageExtension
    {
        /// <summary>
        /// 单表带条件排序的分页查询,<url>https://github.com/StefH/System.Linq.Dynamic.Core</url>
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="pagination">分页条件</param>
        /// <param name="where">where条件，如" name=@1 and websiteid=@2 "</param>
        /// <param name="whereParams"> where的条件参数，需要注意顺序, "name","websiteid"</param>
        /// <param name="sorts">排序条件</param>
        /// <returns></returns>
        public static async Task<PaginationResult<TResult>> WhereSortPageAsync<TEntity, TResult>(this IQueryable<TEntity> query,
            Pagination pagination, IEnumerable<IHasSort> sorts, string where, params object[] whereParams) 
        {
            query = query
                    .BuildWhere<TEntity>(where, whereParams)
                    .BuildSort<TEntity>(sorts);

            var countTask = query.CountAsync();

            var pageQuery = query.BuildPage(pagination);


            Task<List<TResult>> datasTask = null;

            if (typeof(TResult)== typeof(TEntity))
            {
                 datasTask = pageQuery.OfType<TResult>().ToListAsync();
            }
            else
            {
                datasTask = pageQuery.ProjectTo<TResult>().ToListAsync();
            }

            var (datas, count) = await (datasTask, countTask);
            return new PaginationResult<TResult>
            {
                Datas = datas,
                PageIndex = pagination.PageIndex,
                PageSize = pagination.PageSize,
                TotalCount = count
            };
        }

    }
}