using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Extensions;
using CXY.CJS.Model;
using CXY.CJS.WebApi;

namespace CXY.CJS.Repository
{
    public class RoleRepository: CJSRepositoryBase<Role, string>,IRoleRepository
    {
        public RoleRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, string @where = "", params object[] whereParams)
        {
            return this.GetAll().WhereSortPageAsync<Role,TResult>(pagination, sorts, @where, whereParams);
        }
    }
}