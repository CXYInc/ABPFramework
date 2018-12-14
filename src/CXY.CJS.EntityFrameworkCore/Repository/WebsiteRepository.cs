using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Model;

namespace CXY.CJS.Repository
{
    public class WebsiteRepository : CJSRepositoryBase<WebSite, string>, IWebsiteRepository
    {
        public WebsiteRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}