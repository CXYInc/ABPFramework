using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using CXY.CJS.Repository;

namespace CXY.CJS.EntityFrameworkCore.Repository
{
    public class SmsSendRecordRepository: CJSRepositoryBase<SmsSendRecord, string>, ISmsSendRecordRepository
    {
        public SmsSendRecordRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}