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

        #region Bulk
        public void BulkInsert<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            Context.BulkInsert(entities, bulkConfig, progress);
        }

        public void BulkInsertOrUpdate<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            Context.BulkInsertOrUpdate(entities, bulkConfig, progress);
        }

        public void BulkUpdate<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            Context.BulkUpdate(entities, bulkConfig, progress);
        }

        public void BulkDelete<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            Context.BulkDelete(entities, bulkConfig, progress);
        }

        // Async methods
        public async Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            await Context.BulkInsertAsync(entities, bulkConfig, progress);
        }

        public async Task BulkInsertOrUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            await Context.BulkInsertOrUpdateAsync(entities, bulkConfig, progress);
        }

        public async Task BulkUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
        {
            await Context.BulkUpdateAsync(entities, bulkConfig, progress);
        }

        public async Task BulkDeleteAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class
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
