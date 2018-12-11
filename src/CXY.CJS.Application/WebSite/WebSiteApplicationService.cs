using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper;
using CXY.CJS.Extensions;
using CXY.CJS.Repository.Extensions;
using CXY.CJS.Repository.SeedWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CXY.CJS.Application
{
    /// <summary>
    /// WebSite应用层服务的接口实现方法  
    ///</summary>
    [AllowAnonymous]
    public class WebSiteAppService : CJSAppServiceBase, IWebSiteAppService
    {
        private readonly IRepository<WebSite, string> _websiteRepository;
        private readonly IRepository<WebSiteConfig, string> _siteConfigRepository;
        private readonly IRepository<WebSitePayConfig, string> _sitePayRepository;
        /// <summary>
        /// 构造函数 
        ///</summary>
        public WebSiteAppService(IRepository<WebSite, string> websiteRepository,
            IRepository<WebSiteConfig, string> siteConfigRepository,
            IRepository<WebSitePayConfig, string> sitePayRepository)
        {
            _websiteRepository = websiteRepository;
            _siteConfigRepository = siteConfigRepository;
            _sitePayRepository = sitePayRepository;
        }


        /// <summary>
        /// 获取WebSite的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<WebSiteListDto>> GetPaged(GetWebSitesInput input)
        {

            var query = _websiteRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = await query.CountAsync();

            var entityList = await query
                    .OrderBy(input.Sorting).AsNoTracking()
                    .PageBy(input)
                    .ToListAsync();

            // var entityListDtos = ObjectMapper.Map<List<WebSiteListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<WebSiteListDto>>();

            return new PagedResultDto<WebSiteListDto>(count, entityListDtos);
        }


        /// <summary>
        /// 通过指定id获取WebSiteListDto信息
        /// </summary>

        public async Task<WebSiteListDto> GetById(EntityDto<string> input)
        {
            var entity = await _websiteRepository.GetAsync(input.Id);

            return entity.MapTo<WebSiteListDto>();
        }

        /// <summary>
        /// 获取编辑 WebSite
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetWebSiteForEditOutput> GetForEdit(EntityDto<string> input)
        {
            var output = new GetWebSiteForEditOutput();
            WebSiteEditDto editDto;

            if (!input.Id.IsNullOrEmpty())
            {
                var entity = await _websiteRepository.GetAsync(input.Id);

                editDto = entity.MapTo<WebSiteEditDto>();

                //webSiteEditDto = ObjectMapper.Map<List<webSiteEditDto>>(entity);
            }
            else
            {
                editDto = new WebSiteEditDto();
            }

            output.WebSite = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改WebSite的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateWebSiteInput input)
        {

            if (!input.WebSite.Id.IsNullOrEmpty())
            {
                await Update(input.WebSite);
            }
            else
            {
                await Create(input.WebSite);
            }
        }


        /// <summary>
        /// 新增WebSite
        /// </summary>

        protected virtual async Task<WebSiteEditDto> Create(WebSiteEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <WebSite>(input);
            var entity = input.MapTo<WebSite>();


            entity = await _websiteRepository.InsertAsync(entity);
            return entity.MapTo<WebSiteEditDto>();
        }

        /// <summary>
        /// 编辑WebSite
        /// </summary>

        protected virtual async Task Update(WebSiteEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _websiteRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _websiteRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除WebSite信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _websiteRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除WebSite的方法
        /// </summary>

        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _websiteRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        ///// <summary>
        ///// 导出WebSite为excel表,等待开发。
        ///// </summary>
        ///// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}


        /// <summary>
        /// 列出站点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PaginationResult<ListWebSiteOutputItem>> ListWebSite(ListWebSiteInput input)
        {
            var result = new PaginationResult<ListWebSiteOutputItem>(input);

            var query = from website in _websiteRepository.GetAll()
                        join siteConfig in _siteConfigRepository.GetAll() on website.Id
                            equals siteConfig.WebSiteId into siteConfigTemp
                        join sitePay in _sitePayRepository.GetAll() on website.Id
                            equals sitePay.WebSiteId into sitePayTemp
                        from config in siteConfigTemp.DefaultIfEmpty()
                        from pay in sitePayTemp.DefaultIfEmpty()
                        select new
                        {
                            website,
                            config,
                            pay
                        };

            if (input.IsHide)
            {
                query = query.Where(i => i.website.EndTime > DateTime.Now);
            }

            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                query = query.Where(i => i.website.WebSiteName.Contains(input.Key));
            }

            var countTask = query.CountAsync();
            var datasTask = query.BuildPage(input).ToListAsync();
            var (datas, count) = await (datasTask, countTask);
            //var datasTuple = datas.Select(i => Tuple.Create(i.website, i.config, i.pay));
            var list= datas.Select(i =>
            {
                var temp = JObject.FromObject(i.website);
                if (i.config!=null)
                {
                    temp.Merge(JObject.FromObject(i.config));
                }
                if (i.pay != null)
                {
                    temp.Merge(JObject.FromObject(i.pay));
                }
                return temp.ToObject<ListWebSiteOutputItem>();
            });
            return result.SetReuslt(count, list);
        }
    }
}


