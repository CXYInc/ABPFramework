using Abp.Domain.Entities;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Enum;
using CXY.CJS.WebApi;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CXY.CJS.Extensions
{
    public static class QueryPageExtension
    {

        public static async Task<PaginationResult<TEntity>> WhereSortPageAsync<TEntity, TPrimaryKey>(this CJSRepositoryBase<TEntity, TPrimaryKey> repository,
            Pagination pagination, string where, IEnumerable<object> whereParams, IEnumerable<IHasSort> sorts, IEnumerable<string> resultField = null) where TEntity : class, IEntity<TPrimaryKey>
        {
            var query = repository.GetAll()
                    .BuildWhere<TEntity, TPrimaryKey>(where, whereParams)
                    .BuildSort<TEntity, TPrimaryKey>(sorts)
                    .AsNoTracking();

            var countTask = query.CountAsync();

            Task<List<TEntity>> datasTask = null;

            var pageQuery = query.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize);

            if (resultField != null && resultField.Count() > 0)
            {
                var selectStr = resultField?.Where(i => !string.IsNullOrWhiteSpace(i))
                                    .Aggregate((_, next) => $"{_},{next}");

                datasTask = pageQuery.Select(selectStr)
                            .ToDynamicListAsync<TEntity>();
            }
            else
            {
                datasTask = pageQuery.ToListAsync();
            }
            var (datas, count) = await (datasTask, countTask);
            return new PaginationResult<TEntity>
            {
                Datas = datas,
                PageIndex = pagination.PageIndex,
                PageSize = pagination.PageSize,
                TotalCount = count
            };
        }

        private static IQueryable<TEntity> BuildWhere<TEntity, TPrimaryKey>(this IQueryable<TEntity> query, string where, IEnumerable<object> whereParams) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (!string.IsNullOrWhiteSpace(where))
            {
                if (whereParams != null && whereParams.Count() > 0)
                {
                    query = query.Where(where, whereParams?.ToArray());
                }
                else
                {
                    query = query.Where(where);
                }
            }

            return query;
        }

        private static IQueryable<TEntity> BuildSort<TEntity, TPrimaryKey>(
          this IQueryable<TEntity> query, IEnumerable<IHasSort> sortBy) where TEntity : class, IEntity<TPrimaryKey>
        {
            if (sortBy != null && sortBy.Count() > 0)
            {
                var sortStr = "";
                foreach (var sort in sortBy)
                {
                    if (!string.IsNullOrWhiteSpace(sort.SortField))
                    {
                        if (sort.SortOrder == SortEnum.Asc)
                        {
                            sortStr += $"{sort.SortField},";
                        }

                        if (sort.SortOrder == SortEnum.Desc)
                        {
                            sortStr += $"{sort.SortField} Desc,";
                        }
                    }
                }
                if (sortStr.Length>0)
                {
                    sortStr = sortStr.Remove(sortStr.Length-1);
                    query = query.OrderBy(sortStr);
                }
            }
            return query;
        }


    }
}