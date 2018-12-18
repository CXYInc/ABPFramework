using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using CXY.CJS.Repository;

namespace CXY.CJS.EntityFrameworkCore.Repository
{
    public class NoticeRepository : CJSRepositoryBase<Notice, string>, INoticeRepository
    {
        public NoticeRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}