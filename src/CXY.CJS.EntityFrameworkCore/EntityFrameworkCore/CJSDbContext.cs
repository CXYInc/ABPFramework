using Abp.EntityFrameworkCore;
using CXY.CJS.Model;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.EntityFrameworkCore
{
    //[AutoRepositoryTypes(typeof(IRepository<>), typeof(IRepository<,>), typeof(ICJSRepositoryBase<>), typeof(ICJSRepositoryBase<,>))]
    public class CJSDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public DbSet<Test> CjsTest { get; set; }

        #region DbSet

        public DbSet<WebSite> WebSites { get; set; }

        public DbSet<WebSiteConfig> WebSiteConfigs { get; set; }

        public DbSet<WebSitePayConfig> WebSitePayConfigs { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserMarkupSetting> UserMarkupSettings { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

        public DbSet<UserSysSetting> UserSysSettings { get; set; }

        public DbSet<UserWallet> UserWallets { get; set; }

       

        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        #endregion

        public CJSDbContext(DbContextOptions<CJSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new WebSiteConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
