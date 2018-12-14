using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public class UserSysSettingRepository: CJSRepositoryBase<UserSysSetting, string>, IUserSysSettingRepository
    {
        public UserSysSettingRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}