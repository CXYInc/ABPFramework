using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public class UserRepository : CJSRepositoryBase<User, string>, IUserRepository
    {
        public UserRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}