using System;
using Abp.Dependency;
using Abp.Domain.Entities;
using CXY.CJS.Enum;
using CXY.CJS.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Repository
{
    /// <summary>
    /// 分页查询仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IQueryPageRepository<TEntity, TPrimaryKey> :
        ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 单表带条件排序的分页查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult">返回结果，可直接用DTO</typeparam>
        /// <param name="pagination">分页条件</param>
        /// <param name="where">where条件，如" name=@1 and websiteid=@2 "</param>
        /// <param name="whereParams"> where的条件参数，需要注意顺序, "name","websiteid"</param>
        /// <param name="sorts">排序条件</param>
        /// <returns></returns>
        Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts,
            string @where = "", params object[] whereParams);
    }
}