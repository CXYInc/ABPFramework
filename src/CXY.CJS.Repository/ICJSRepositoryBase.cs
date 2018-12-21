using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using EFCore.BulkExtensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CXY.CJS.Repository
{
    public interface ICJSRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistedAsync(TPrimaryKey id);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistedAsync(Expression<Func<TEntity, bool>> predicate);

        #region 批量处理
        void BulkDelete<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        Task BulkDeleteAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        void BulkInsert<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        Task BulkInsertAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        void BulkInsertOrUpdate<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        Task BulkInsertOrUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        void BulkUpdate<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;

        Task BulkUpdateAsync<T>(IList<T> entities, BulkConfig bulkConfig = null, Action<decimal> progress = null) where T : class;
        #endregion
    }

    public interface ICJSRepositoryBase<TEntity> : ICJSRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
    }
}