using System;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using CXY.CJS.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using EFCore.BulkExtensions;

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

        #region 批量处理
        public void BulkInsert(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            Context.BulkInsert(entities, bulkConfig, progress);
        }

        public void BulkInsertOrUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            Context.BulkInsertOrUpdate(entities, bulkConfig, progress);
        }

        public void BulkUpdate(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            Context.BulkUpdate(entities, bulkConfig, progress);
        }

        public void BulkDelete(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            Context.BulkDelete(entities, bulkConfig, progress);
        }

        // Async methods
        public async Task BulkInsertAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            await Context.BulkInsertAsync(entities, bulkConfig, progress);
        }

        public async Task BulkInsertOrUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            await Context.BulkInsertOrUpdateAsync(entities, bulkConfig, progress);
        }

        public async Task BulkUpdateAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            await Context.BulkUpdateAsync(entities, bulkConfig, progress);
        }

        public async Task BulkDeleteAsync(IList<TEntity> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null)
        {
            await Context.BulkDeleteAsync(entities, bulkConfig, progress);
        }
        #endregion
    }

    public class CJSRepositoryBase<TEntity> : CJSRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        public CJSRepositoryBase(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
