using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using CXY.CJS.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CXY.CJS.Model
{
    /// <summary>
    /// WebSite应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class WebSiteAppService : CJSAppServiceBase, IWebSiteAppService
    {
        private readonly IRepository<WebSite, int> _entityRepository;



        /// <summary>
        /// 构造函数 
        ///</summary>
        public WebSiteAppService(
        IRepository<WebSite, int> entityRepository

        )
        {
            _entityRepository = entityRepository;

        }


        /// <summary>
        /// 获取WebSite的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<PagedResultDto<WebSiteListDto>> GetPaged(GetWebSitesInput input)
        {

            var query = _entityRepository.GetAll();
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

        public async Task<WebSiteListDto> GetById(EntityDto<int> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<WebSiteListDto>();
        }

        /// <summary>
        /// 获取编辑 WebSite
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetWebSiteForEditOutput> GetForEdit(NullableIdDto<int> input)
        {
            var output = new GetWebSiteForEditOutput();
            WebSiteEditDto editDto;

            if (input.Id.HasValue)
            {
                var entity = await _entityRepository.GetAsync(input.Id.Value);

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

            if (input.WebSite.Id.HasValue)
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


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<WebSiteEditDto>();
        }

        /// <summary>
        /// 编辑WebSite
        /// </summary>

        protected virtual async Task Update(WebSiteEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除WebSite信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除WebSite的方法
        /// </summary>

        public async Task BatchDelete(List<int> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出WebSite为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


