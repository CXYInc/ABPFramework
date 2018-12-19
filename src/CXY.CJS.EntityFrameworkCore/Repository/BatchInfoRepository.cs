using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Extensions;
using CXY.CJS.Model;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Repository
{
    public class BatchInfoRepository : CJSRepositoryBase<BatchInfo, string>, IBatchInfoRepository
    {
        public BatchInfoRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination,
            IEnumerable<IHasSort> sorts, string @where = "", params object[] whereParams)
        {
            return this.GetAll().WhereSortPageAsync<BatchInfo, TResult>(pagination, sorts, @where, whereParams);
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination,
            IEnumerable<IHasSort> sorts, Expression<Func<BatchInfo, bool>> @where)
        {
            return this.GetAll().WhereSortPageAsync<BatchInfo, TResult>(pagination, sorts, @where);
        }
    }
}