using CXY.CJS.Configuration;
using CXY.CJS.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CXY.CJS.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class CJSDbContextFactory : IDesignTimeDbContextFactory<CJSDbContext>
    {
        public CJSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CJSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(builder, configuration.GetConnectionString(CJSConsts.ConnectionStringName));

            return new CJSDbContext(builder.Options);
        }
    }
}