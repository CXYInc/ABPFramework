using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using CXY.CJS.Repository;

namespace CXY.CJS.EntityFrameworkCore.Repository
{
    public class UserRoleRepository : CJSRepositoryBase<UserRole, string>, IUserRoleRepository
    {
        public UserRoleRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}