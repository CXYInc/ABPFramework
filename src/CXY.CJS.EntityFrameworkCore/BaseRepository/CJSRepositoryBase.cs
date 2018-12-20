using System;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using CXY.CJS.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CXY.CJS.EntityFrameworkCore
{
    public class CJSRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<CJSDbContext, TEntity, TPrimaryKey>, ICJSRepositoryBase<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        public CJSRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override IQueryable<TEntity> GetAll()
        {
            return base.GetAll().AsNoTracking();
        }

        public async Task<bool> IsExistedAsync(TPrimaryKey id)
        {
            var count = await GetAll().Where(i => i.Id.Equals(id)).CountAsync();
            return count > 0;
        }

        public async Task<bool> IsExistedAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var count = await GetAll().Where(predicate).CountAsync();
            return count > 0;
        }
    }

    public class CJSRepositoryBase<TEntity> : CJSRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        public CJSRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
