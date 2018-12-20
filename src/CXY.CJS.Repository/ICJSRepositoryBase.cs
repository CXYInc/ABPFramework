﻿using System;
using System.Collections.Generic;
using Abp.Dependency;
using Abp.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CXY.CJS.Repository
{
    public interface ICJSRepositoryBase<TEntity, TPrimaryKey> : ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

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

        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    }

    public interface ICJSRepositoryBase<TEntity> : ITransientDependency where TEntity : class, IEntity<int>
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> FirstOrDefaultAsync(int id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}