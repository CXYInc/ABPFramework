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
using System.Linq.Expressions;
using System.Threading.Tasks;
using CXY.CJS.Repository;
using CXY.CJS.Repository.Extensions;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Repository.SeedWork;
using Abp.Specifications;
using Abp.UI;
using CXY.CJS.Extensions;
using CXY.CJS.Utils;

namespace CXY.CJS.Application
{
    /// <summary>
    /// WebSite应用层服务的接口实现方法  
    ///</summary>
    [AllowAnonymous]
    public class WebSiteAppService : CJSAppServiceBase, IWebSiteAppService
    {
        private readonly IRepository<WebSite, string> _websiteRepository;

        private readonly IRepository<UserJf, string> _userJfRepository;

        private readonly IRepository<UserAtt, string> _userAttRepository;

        private readonly IRepository<User, string> _userRepository;

        private readonly IWebSiteFullRepository _siteFullRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public WebSiteAppService(IRepository<WebSite, string> websiteRepository, IWebSiteFullRepository siteFullRepository, IRepository<UserJf, string> userJfRepository, IRepository<UserAtt, string> userAttRepository, IRepository<User, string> userRepository)
        {
            _websiteRepository = websiteRepository;
            _siteFullRepository = siteFullRepository;
            _userJfRepository = userJfRepository;
            _userAttRepository = userAttRepository;
            _userRepository = userRepository;
        }


        ///// <summary>
        ///// 获取WebSite的分页列表信息
        /////</summary>
        ///// <param name="input"></param>
        ///// <returns></returns>

        //public async Task<PagedResultDto<WebSiteListDto>> GetPaged(GetWebSitesInput input)
        //{

        //    var query = _websiteRepository.GetAll();
        //    // TODO:根据传入的参数添加过滤条件


        //    var count = await query.CountAsync();

        //    var entityList = await query
        //            .OrderBy(input.Sorting).AsNoTracking()
        //            .PageBy(input)
        //            .ToListAsync();

        //    // var entityListDtos = ObjectMapper.Map<List<WebSiteListDto>>(entityList);
        //    var entityListDtos = entityList.MapTo<List<WebSiteListDto>>();

        //    return new PagedResultDto<WebSiteListDto>(count, entityListDtos);
        //}


        ///// <summary>
        ///// 通过指定id获取WebSiteListDto信息
        ///// </summary>

        //public async Task<WebSiteListDto> GetById(EntityDto<string> input)
        //{
        //    var entity = await _websiteRepository.GetAsync(input.Id);

        //    return entity.MapTo<WebSiteListDto>();
        //}

        ///// <summary>
        ///// 获取编辑 WebSite
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>

        //public async Task<GetWebSiteForEditOutput> GetForEdit(EntityDto<string> input)
        //{
        //    var output = new GetWebSiteForEditOutput();
        //    WebSiteEditDto editDto;

        //    if (!input.Id.IsNullOrEmpty())
        //    {
        //        var entity = await _websiteRepository.GetAsync(input.Id);

        //        editDto = entity.MapTo<WebSiteEditDto>();

        //        //webSiteEditDto = ObjectMapper.Map<List<webSiteEditDto>>(entity);
        //    }
        //    else
        //    {
        //        editDto = new WebSiteEditDto();
        //    }

        //    output.WebSite = editDto;
        //    return output;
        //}


        ///// <summary>
        ///// 添加或者修改WebSite的公共方法
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>

        //public async Task CreateOrUpdate(CreateOrUpdateWebSiteInput input)
        //{

        //    if (!input.WebSite.Id.IsNullOrEmpty())
        //    {
        //        await Update(input.WebSite);
        //    }
        //    else
        //    {
        //        await Create(input.WebSite);
        //    }
        //}


        ///// <summary>
        ///// 新增WebSite
        ///// </summary>

        //protected virtual async Task<WebSiteEditDto> Create(WebSiteEditDto input)
        //{
        //    //TODO:新增前的逻辑判断，是否允许新增

        //    // var entity = ObjectMapper.Map <WebSite>(input);
        //    var entity = input.MapTo<WebSite>();


        //    entity = await _websiteRepository.InsertAsync(entity);
        //    return entity.MapTo<WebSiteEditDto>();
        //}

        ///// <summary>
        ///// 编辑WebSite
        ///// </summary>

        //protected virtual async Task Update(WebSiteEditDto input)
        //{
        //    //TODO:更新前的逻辑判断，是否允许更新

        //    var entity = await _websiteRepository.GetAsync(input.Id);
        //    input.MapTo(entity);

        //    // ObjectMapper.Map(input, entity);
        //    await _websiteRepository.UpdateAsync(entity);
        //}



        ///// <summary>
        ///// 删除WebSite信息的方法
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>

        //public async Task Delete(EntityDto<string> input)
        //{
        //    //TODO:删除前的逻辑判断，是否允许删除
        //    await _websiteRepository.DeleteAsync(input.Id);
        //}



        ///// <summary>
        ///// 批量删除WebSite的方法
        ///// </summary>

        //public async Task BatchDelete(List<string> input)
        //{
        //    // TODO:批量删除前的逻辑判断，是否允许删除
        //    await _websiteRepository.DeleteAsync(s => input.Contains(s.Id));
        //}


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
            Expression<Func<WebSiteFull, bool>> where = null;

            if (input.IsHide)
            {
                where = i => i.WebSite.EndTime > DateTime.Now;
            }
            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                if (where != null)
                {
                    where = where.And(i => i.WebSite.WebSiteName.Contains(input.Key));
                }
                else
                {
                    where = i => i.WebSite.WebSiteName.Contains(input.Key);
                }
            }
            var resultTemp = await _siteFullRepository.QueryByWhereAsync<WebSiteFull>(input, null, where);

            return new PaginationResult<ListWebSiteOutputItem>(input)
                    .SetReuslt(resultTemp.TotalCount, WebSiteFull.MapToList<ListWebSiteOutputItem>(resultTemp.Datas));
        }

        /// <summary>
        /// 获取站点详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetWebsitOutput> GetWebSite(string id)
        {
            GetWebsitOutput result = null;
            var webSite = await _siteFullRepository.GetAsync(id);
            if (webSite != null)
            {
                result = WebSiteFull.MapTo<GetWebsitOutput>(webSite);
                if (!string.IsNullOrEmpty(webSite.WebSite.WebSiteMater))
                {
                    //获取关联的DefaultJFPrice和DefaultNotePrice
                    var defaultJFPriceAndDefaultNotePriceTask = _userJfRepository.GetAll()
                        .Where(i => i.Userid == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.JfPrice,
                            i.NotePrice
                        }).FirstOrDefaultAsync();

                    //获取关联的provinceid
                    var provinceidTask = _userAttRepository.GetAll().Where(i => i.UserId == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.Provinceid
                        }).FirstOrDefaultAsync();

                    //获取关联的Email和LoginName
                    var emailAndloginnameTask = _userRepository.GetAll().Where(i => i.Id == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.EmailAddress,
                            i.LoginName
                        }).FirstOrDefaultAsync();

                    var (price, province, info) = await (defaultJFPriceAndDefaultNotePriceTask, provinceidTask,
                        emailAndloginnameTask);

                    result.DefaultJFPrice = price?.JfPrice;
                    result.DefaultNotePrice = price?.NotePrice;
                    result.Provinceid = province?.Provinceid ?? "0";
                    result.Email = info?.EmailAddress;
                    result.loginname = info?.LoginName;
                }
            }

            return result;
        }

        /// <summary>
        /// 新增站点
        /// </summary>
        /// <returns></returns>
        public async Task<SaveWebSiteOutput> SaveWebSite(SaveWebSiteInput input)
        {
            //todo:LogInfoMsg(string.Format("页面【管理站点】新增站点,参数websiteid【{0}】,操作者【{1}】,操作站点【{2}】", SessionHelper.WebSite.WebSiteId, user.Userid, WebSiteId));

            // 检查数据库中是否存在数据
            var existedDatas = await _websiteRepository.GetAll()
                .Where(i => i.WebSiteKey == input.WebSiteKey || i.Id == input.WebSiteId)
                .Select(i => new { i.Id, i.WebSiteKey }).ToListAsync();

            if (existedDatas.Any(i => i.Id == input.WebSiteId))
            {
                //todo:LogInfoMsg(string.Format("页面【管理站点】新增站点,参数websiteid【{0}】,操作者【{1}】,操作站点【{2}】,结果【{3}】",
                // SessionHelper.WebSite.WebSiteId,
                // user.Userid,
                // WebSiteId,
                // message))
                throw new UserFriendlyException("站点Id已存在！");
            }
            if (existedDatas.Any(i => i.WebSiteKey == input.WebSiteKey))
            {
                //todo:LogInfoMsg(string.Format("页面【管理站点】新增站点,参数websiteid【{0}】,操作者【{1}】,操作站点【{2}】,结果【{3}】",
                // SessionHelper.WebSite.WebSiteId,
                // user.Userid,
                // WebSiteId,
                // message))
                throw new UserFriendlyException("订单Id前缀已存在！");
            }
            // 创建站点管理员账号
            // todo:管理员账号基础信息处理及角色分配
            DateTime time = DateTime.Now;
            string userId = input.WebSiteId + time.ToString("yyyyMMddHHmmss") + RNG.Next(10).ToString().PadLeft(10, '0');
            string safePassword = Guid.NewGuid().ToString("N").Substring(0, 6);
            await _userRepository.InsertAsync(new User
            {
                Id = userId,
                WebSiteId = input.WebSiteId,
                UserName = input.Loginname,
                LoginName = input.Loginname,
                EmailAddress = input.Email,
                Password = Encryptor.MD5Entry(safePassword),
                Safepassword = safePassword,
                IsActive = true,
            });
            var siteFull = WebSiteFull.MapFrom(input);
            siteFull.WebSite.Id = input.WebSiteId;
            await _siteFullRepository.InsertAsync(siteFull);

            return new SaveWebSiteOutput
            {
                Safepassword=safePassword,
                LoginName = input.Loginname
            };
        }

        public Task<bool> UpdateWebSite(UpdateWebSiteInput input)
        {
            throw new NotImplementedException();
        }
    }
}


