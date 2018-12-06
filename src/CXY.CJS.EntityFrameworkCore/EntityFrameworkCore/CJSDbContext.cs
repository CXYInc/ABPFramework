using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.EntityFrameworkCore
{
    //[AutoRepositoryTypes(typeof(IRepository<>), typeof(IRepository<,>), typeof(ICJSRepositoryBase<>), typeof(ICJSRepositoryBase<,>))]
    public class CJSDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        //public DbSet<Test> CjsTest { get; set; }
        public DbSet<WebSite> CjsWebSite { get; set; }

        public CJSDbContext(DbContextOptions<CJSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Test>().HasKey(c => new { c.Id });
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
