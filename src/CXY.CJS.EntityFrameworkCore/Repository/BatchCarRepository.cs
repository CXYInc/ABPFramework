﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using CXY.CJS.EntityFrameworkCore;
using CXY.CJS.Extensions;
using CXY.CJS.Model;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Repository
{
    public class BatchCarRepository : CJSRepositoryBase<BatchCar, string>, IBatchCarRepository
    {
        public BatchCarRepository(IDbContextProvider<CJSDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination,
            IEnumerable<IHasSort> sorts, string @where = "", params object[] whereParams)
        {
            return this.GetAll().WhereSortPageAsync<BatchCar, TResult>(pagination, sorts, @where, whereParams);
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination,
            IEnumerable<IHasSort> sorts, Expression<Func<BatchCar, bool>> @where)
        {
            return this.GetAll().WhereSortPageAsync<BatchCar, TResult>(pagination, sorts, @where);
        }
    }
}