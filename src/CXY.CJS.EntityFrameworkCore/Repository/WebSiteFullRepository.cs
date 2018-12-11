using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CXY.CJS.Extensions;
using CXY.CJS.Model;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.Repository
{
    public class WebSiteFullRepository : IWebSiteFullRepository
    {

        private readonly IRepository<WebSite, string> _websiteRepository;
        private readonly IRepository<WebSiteConfig, string> _siteConfigRepository;
        private readonly IRepository<WebSitePayConfig, string> _sitePayRepository;

        public WebSiteFullRepository(IRepository<WebSite, string> websiteRepository, IRepository<WebSiteConfig, string> siteConfigRepository, IRepository<WebSitePayConfig, string> sitePayRepository)
        {
            _websiteRepository = websiteRepository;
            _siteConfigRepository = siteConfigRepository;
            _sitePayRepository = sitePayRepository;
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, string @where = "",
            params object[] whereParams)
        {
            return GetAll().WhereSortPageAsync<WebSiteFull, TResult>(pagination, sorts, @where, whereParams);
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, Expression<Func<WebSiteFull, bool>> @where)
        {
            return GetAll().WhereSortPageAsync<WebSiteFull, TResult>(pagination, sorts, @where);
        }

        public IQueryable<WebSiteFull> GetAll()
        {
            return from website in _websiteRepository.GetAll()
                   join siteConfig in _siteConfigRepository.GetAll() on website.Id
                       equals siteConfig.WebSiteId into siteConfigTemp
                   join sitePay in _sitePayRepository.GetAll() on website.Id
                       equals sitePay.WebSiteId into sitePayTemp
                   from config in siteConfigTemp.DefaultIfEmpty()
                   from pay in sitePayTemp.DefaultIfEmpty()
                   select new WebSiteFull
                   {
                       WebSite = website,
                       WebSiteConfig = config,
                       WebSitePayConfig = pay
                   };
        }

        public Task<WebSiteFull> Get(string id)
        {
            return GetAll().FirstOrDefaultAsync(i => i.WebSite.Id == id);
        }
    }
}