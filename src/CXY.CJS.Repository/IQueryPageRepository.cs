using System;
using Abp.Dependency;
using Abp.Domain.Entities;
using CXY.CJS.Enum;
using CXY.CJS.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CXY.CJS.Repository
{
    public interface IQueryPageRepository<TEntity, TPrimaryKey> :
        ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        Task<PaginationResult<TEntity>> QueryByWhereAsync(Pagination pagination, string where, IEnumerable<Object> whereParams, IEnumerable<IHasSort> sorts, IEnumerable<string> resultField = null);
    }
}