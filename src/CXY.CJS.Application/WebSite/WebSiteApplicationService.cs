using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
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
using CXY.CJS.Core.Config;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace CXY.CJS.Application
{
    /// <summary>
    /// WebSite应用层服务的接口实现方法  
    ///</summary>
    [AllowAnonymous]
    public class WebSiteAppService : CJSAppServiceBase, IWebSiteAppService
    {
        private readonly IWebsiteRepository _websiteRepository;

        private readonly IUserScoreRepository _userScoreRepository;

        private readonly IUserSysSettingRepository _userSysSettingRepository;

        private readonly IUserRepository _userRepository;

        private readonly IWebSiteFullRepository _siteFullRepository;

        private readonly SysAlipayConfig _sysAlipayConfig;

        private readonly SysWeiXinPayConfig _sysWeiXinPayConfig;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public WebSiteAppService(IWebsiteRepository websiteRepository, IWebSiteFullRepository siteFullRepository, SysAlipayConfig sysAlipayConfig, SysWeiXinPayConfig sysWeiXinPayConfig, IUserScoreRepository userScoreRepository, IUserSysSettingRepository userSysSettingRepository, IUserRepository userRepository)
        {
            _websiteRepository = websiteRepository;
            _siteFullRepository = siteFullRepository;
            _sysAlipayConfig = sysAlipayConfig;
            _sysWeiXinPayConfig = sysWeiXinPayConfig;
            _userScoreRepository = userScoreRepository;
            _userSysSettingRepository = userSysSettingRepository;
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
            Expression<Func<WebSiteFull, bool>> where = i=>i.WebSite.IsDeleted==false;

            if (input.IsHide)
            {
                where = where.And(i=>i.WebSite.EndTime > DateTime.Now);
            }
            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                where = where.And(i => i.WebSite.WebSiteName.Contains(input.Key));
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
                if (webSite.WebSite.IsDeleted)
                {
                    return null;
                }
                result = WebSiteFull.MapTo<GetWebsitOutput>(webSite);
                if (!string.IsNullOrEmpty(webSite.WebSite.WebSiteMater))
                {
                    //获取关联的DefaultJFPrice和DefaultNotePrice
                    var price = _userScoreRepository.GetAll()
                        .Where(i => i.Id == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.JfPrice,
                            i.NotePrice
                        }).FirstOrDefault();

                    //获取关联的provinceid
                    var province = _userSysSettingRepository.GetAll().Where(i => i.Id == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.Provinceid
                        }).FirstOrDefault();

                    //获取关联的Email和LoginName
                    var info = _userRepository.GetAll().Where(i => i.Id == result.WebSiteMater)
                        .Select(i => new
                        {
                            i.EmailAddress,
                            i.LoginName
                        }).FirstOrDefault();

                    //var (price, province, info) = await (defaultJFPriceAndDefaultNotePriceTask, provinceidTask,
                    //    emailAndloginnameTask);

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
        [HttpPost]
        public async Task<SaveWebSiteOutput> SaveWebSite(SaveWebSiteInput input)
        {
            // 是否使用系统的配置
            WhenUseSysAlipayPayment(input);
            WhenUseSysWeiXinPay(input);

            //todo:记日志

            // 检查数据库中是否存在数据
            var existedDatas = await _websiteRepository.GetAll()
                .Where(i => i.WebSiteKey == input.WebSiteKey || i.Id == input.Id)
                .Select(i => new { i.Id, i.WebSiteKey }).ToDynamicListAsync();

            if (existedDatas.Any(i => i.Id == input.Id))
            {
                throw new UserFriendlyException("站点Id已存在！");
            }
            if (existedDatas.Any(i => i.WebSiteKey == input.WebSiteKey))
            {
                throw new UserFriendlyException("订单Id前缀已存在！");
            }
            // 创建站点管理员账号
            // todo:管理员账号基础信息处理及角色分配
            DateTime time = DateTime.Now;
            string userId = input.Id + time.ToString("yyyyMMddHHmmss") + RNG.Next(10).ToString().PadLeft(10, '0');
            string safePassword = Guid.NewGuid().ToString("N").Substring(0, 6);

            // 创建站点
            var siteFull = WebSiteFull.MapFrom(input);
            siteFull.WebSite.Id = input.Id;
            siteFull.WebSite.WebSiteMater = userId;

            var insertWebSiteTask = _siteFullRepository.InsertAsync(siteFull);

            // 创建站点管理员账号信息

            var insertUserTask = _userRepository.InsertAsync(new User
            {
                Id = userId,
                WebSiteId = input.Id,
                UserName = input.Loginname,
                LoginName = input.Loginname,
                EmailAddress = input.Email,
                Password = Encryptor.MD5Entry(safePassword),
                Safepassword = safePassword,
                IsActive = true,
            });

            // 创建站点管理员附属信息(商务经理、省)

            var insertUserSysSettingTask = _userSysSettingRepository.InsertAsync(new UserSysSetting
            {
                Id = userId,
                Swfzr = input.WorkerName,
                Provinceid = input.PROVINCEID,
                WebSiteId = input.Id
            });

            // 创建站点管理员的每月积分
            var insertUserScoreTask = _userScoreRepository.InsertAsync(new UserScore
            {
                Id = userId,
                GivePointsPerMonth = input.GivePointsPerMonth,
                NotePrice = input.DefaultJfPrice,
                JfPrice = input.DefaultJfPrice
            });

            await (insertWebSiteTask, insertUserTask, insertUserSysSettingTask, insertUserScoreTask);

            return new SaveWebSiteOutput
            {
                Safepassword = safePassword,
                LoginName = input.Loginname
            };
        }

        /// <summary>
        /// 更新站点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> UpdateWebSite(UpdateWebSiteInput input)
        {
            // 是否使用系统的配置
            WhenUseSysAlipayPayment(input);
            WhenUseSysWeiXinPay(input);

            //todo:记录日志
            var websiteTemp =  _siteFullRepository.GetAllNoTracking()
                        .Where(i => i.WebSite.Id == input.Id)
                .Select(i => new
                {
                    i.WebSite.WebSiteMater,
                    i.WebSite.WorkerName,
                    i.WebSiteConfig.GivePointsPerMonth,
                    i.WebSiteConfig.DefaultNotePrice,
                    i.WebSiteConfig.DefaultJfPrice
                }).FirstOrDefault();

            if (websiteTemp == null)
            {
                throw new UserFriendlyException("该站点不存在,无法编辑！");
            }

            // 更新站点
            var insertWebsite = WebSiteFull.MapFrom(input);
            insertWebsite.WebSite.Id = input.Id;
            await _siteFullRepository.UpdateAsync(insertWebsite);

            // 站点管理员的 WorkerName 和 PROVINCEID  变更时
            var UserSysSetting = await _userSysSettingRepository
                .FirstOrDefaultAsync(i => i.Id == input.WebSiteMater && i.WebSiteId == input.Id);
            if (UserSysSetting == null)
            {
                throw new UserFriendlyException("注意，总站信息不对!");
            }
            if (UserSysSetting.Provinceid != input.PROVINCEID
                || UserSysSetting.Swfzr != input.WorkerName)
            {
                UserSysSetting.Provinceid = input.PROVINCEID;
                UserSysSetting.Swfzr = input.WorkerName;
                await _userSysSettingRepository.UpdateAsync(UserSysSetting);
            }

            // 每月赠送次数 变更时

            var UserScore = await _userScoreRepository.FirstOrDefaultAsync(i => i.Id == input.WebSiteMater);
            if (UserScore != null)
            {
                if (UserScore.GivePointsPerMonth!= input.GivePointsPerMonth)
                {
                    UserScore.GivePointsPerMonth = input.GivePointsPerMonth;
                    await _userScoreRepository.UpdateAsync(UserScore);
                }
            }

            //站点短信与积分单价变化时，更新所有站点用户的短信与积分单价,暂时不做这个操作
            //if (website.DefaultJfPrice!=input.DefaultJfPrice
            //    ||website.DefaultNotePrice!=input.DefaultNotePrice)
            //{
            //}
            return true;
        }

        /// <summary>
        /// 重设站点密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> ResetPassword(ResetPasswordInput input)
        {
            var user = await _userRepository.FirstOrDefaultAsync(input.UserId);
            if (user==null)
            {
                throw new UserFriendlyException("找不到该用户！");
            }
            string srtPassword = "123456";
            srtPassword = Encryptor.MD5Entry(srtPassword);
            user.Password = srtPassword;
            await _userRepository.UpdateAsync(user);
            return true;
        }

        private void WhenUseSysAlipayPayment(UpdateOrSaveWebSiteInputBase input)
        {
            if (input.IsAlipayPayment == 1 && input.IsUseSysAlipay == 1)
            {
                input.AlipayKey = _sysAlipayConfig?.AlipayKey;
                input.AlipayPartner = _sysAlipayConfig?.AlipayPartner;
                input.AlipaySellerEmail = _sysAlipayConfig?.AlipaySellerEmail;
                input.AlipayAppID = _sysAlipayConfig?.AlipayAppID;
                input.AlipayPrivateKey = _sysAlipayConfig?.AlipayPrivateKey;
                input.AlipayPublicKey = _sysAlipayConfig?.AlipayPublicKey;
            }
        }

        private void WhenUseSysWeiXinPay(UpdateOrSaveWebSiteInputBase input)
        {
            if (input.IsWeChatPayment == 1 && input.IsUseSysWeiXinPay == 1)
            {
                input.WxappID = _sysWeiXinPayConfig?.WxAppId;
                input.WxmchId = _sysWeiXinPayConfig?.WxMchId;
                input.Wxkey = _sysWeiXinPayConfig?.WxKey;
                input.WxsubAppId = _sysWeiXinPayConfig?.WxSubAppId;
                input.WxSubMchId = _sysWeiXinPayConfig?.WxSubMchId;
                input.WxsubKey = _sysWeiXinPayConfig?.WxSubKey;
            }
        }
    }
}


