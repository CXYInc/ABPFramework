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
using CXY.CJS.Repository;

namespace CXY.CJS.Repository
{

    public class RoleMenuRepository : CJSRepositoryBase<RoleMenu, string>, IRoleMenuRepository
    {
        public RoleMenuRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, string @where = "", params object[] whereParams)
        {
            return this.GetAll().WhereSortPageAsync<RoleMenu, TResult>(pagination, sorts, @where, whereParams);
        }



        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, Expression<Func<RoleMenu, bool>> @where)
        {
            return this.GetAll().WhereSortPageAsync<RoleMenu, TResult>(pagination, sorts, @where);
        }
    }
}
