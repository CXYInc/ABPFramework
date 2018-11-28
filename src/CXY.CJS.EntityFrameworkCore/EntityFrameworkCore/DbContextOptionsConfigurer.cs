using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CXY.CJS.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CJSDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CJSDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
