using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public class UserScoreRepository: CJSRepositoryBase<UserScore, string>,IUserScoreRepository
    {
        public UserScoreRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}