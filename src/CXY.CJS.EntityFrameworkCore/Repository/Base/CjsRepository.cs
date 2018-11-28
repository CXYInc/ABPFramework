using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using CXY.CJS.EntityFrameworkCore;

namespace CXY.CJS.Repository
{
    public class CjsRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<CJSDbContext, TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public CjsRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }

    public class CjsRepositoryBase<TEntity> : CjsRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        public CjsRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
