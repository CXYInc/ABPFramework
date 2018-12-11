using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using CXY.CJS.Enum;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;

namespace CXY.CJS.Repository.Extensions
{
    public static class QueryableExtension
    {



        /// <summary>
        /// 构建分页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> BuildPage<TEntity>(this IQueryable<TEntity> query,Pagination pagination)
        {
          return  query.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize);
        }



        /// <summary>
        /// 拼接where
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="query"></param>
        /// <param name="where">where条件，如" name=@1 and websiteid=@2 "</param>
        /// <param name="whereParams"> where的条件参数，需要注意顺序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> BuildWhere<TEntity>(this IQueryable<TEntity> query, string where, IEnumerable<object> whereParams)
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

        /// <summary>
        /// 拼接排序
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortBy">排序条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> BuildSort<TEntity>(
          this IQueryable<TEntity> query, IEnumerable<IHasSort> sortBy)
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
                if (sortStr.Length > 0)
                {
                    sortStr = sortStr.Remove(sortStr.Length - 1);
                    query = query.OrderBy(sortStr);
                }
            }
            return query;
        }
    }
}