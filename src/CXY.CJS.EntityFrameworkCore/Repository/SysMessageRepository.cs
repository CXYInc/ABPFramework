using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.EntityFrameworkCore.Repository
{
    public class SysMessageRepository : CJSRepositoryBase<SysMessage, string>, ISysMessageRepository
    {
        public SysMessageRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task SetRead(string id)
        {
            var entity = await FirstOrDefaultAsync(id);
            entity.IsRead = 1;
            await  UpdateAsync(entity);
        }
    }
}