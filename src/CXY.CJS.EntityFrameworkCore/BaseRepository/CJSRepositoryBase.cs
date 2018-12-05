using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace CXY.CJS.EntityFrameworkCore
{
    public class CJSRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<CJSDbContext, TEntity, TPrimaryKey>, ICJSRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public CJSRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class CJSRepositoryBase<TEntity> : CJSRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        public CJSRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
