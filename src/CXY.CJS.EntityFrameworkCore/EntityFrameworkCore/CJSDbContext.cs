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

        public DbSet<Users> Users { get; set; }

        public DbSet<UserMarkupSetting> UserMarkupSettings { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

        public DbSet<UserSysSetting> UserSysSettings { get; set; }

        public DbSet<UserWallet> UserWallets { get; set; }



        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<SysMessage> Notices { get; set; }

        public DbSet<SmsSendRecord> SmsSendRecords { get; set; }

        public DbSet<BatchInfo> BatchInfos { get; set; }

        public DbSet<BatchCar> BatchCars { get; set; }

        public DbSet<BatchAskPriceViolationAgent> BatchAskPriceViolationAgents { get; set; }

        public DbSet<CarViolationDivision> CarViolationDivisions { get; set; }


        public DbSet<DataSeed> DataSeeds { get; set; }

        #endregion

        public CJSDbContext(DbContextOptions<CJSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new TestConfiguration());
            builder.ApplyConfiguration(new WebSiteConfiguration());
            builder.ApplyConfiguration(new BatchCarConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
