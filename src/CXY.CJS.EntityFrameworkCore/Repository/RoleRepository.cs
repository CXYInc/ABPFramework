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

        public async Task<PaginationResult<Role>> QueryByWhereAsync(Pagination pagination, string @where, IEnumerable<object> whereParams, IEnumerable<IHasSort> sorts,
            IEnumerable<string> resultField = null)
        {
            return await this.WhereSortPageAsync(pagination, @where, whereParams,sorts, resultField);
        }
    }
}