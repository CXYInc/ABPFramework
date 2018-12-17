using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.ObjectMapping;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Net;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Specifications;
using CXY.CJS.Core.Utils;
using CXY.CJS.Repository;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Repository.SeedWork;
using Microsoft.AspNetCore.Authorization;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserServices : CJSAppServiceBase, IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IRepository<UserRole, string> _userRolerepository;
        private readonly IObjectMapper _objectMapper;
        private ILowerAgentRepository _lowerAgentRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="objectMapper"></param>
        /// <param name="lowerAgentRepository"></param>
        /// <param name="userRolerepository"></param>
        public UserServices(IUserRepository repository, IObjectMapper objectMapper, ILowerAgentRepository lowerAgentRepository, IRepository<UserRole, string> userRolerepository)
        {
            _repository = repository;
            _objectMapper = objectMapper;
            _lowerAgentRepository = lowerAgentRepository;
            _userRolerepository = userRolerepository;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userEditInput"></param>
        /// <returns></returns>
        [HttpPost]
     
        public async Task<ApiResult<string>> Create(CreateUserInput userEditInput)
        {
            var user = _objectMapper.Map<Users>(userEditInput);
            var time = DateTime.Now;
            user.Id = userEditInput.WebSiteId + time.ToString("yyyyMMddHHmmss") + RNG.Next(10).ToString().PadLeft(10, '0');
            user.CreationTime = time;
            user.Password = Encryptor.MD5Entry(userEditInput.Password);
            await _repository.InsertAsync(user);

            return ApiResult.Success(user.Id);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userEditInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Update(UserEditInputDto userEditInput)
        {
            var user = await _repository.FirstOrDefaultAsync(i => i.Id == userEditInput.Id);
            if (user == null)
            {
                return ApiResult.DataNotFound();
            }
            user = userEditInput.MapTo<Model.Users>();
            if (!string.IsNullOrWhiteSpace(userEditInput.Password))
            {
                user.Password = Encryptor.MD5Entry(userEditInput.Password);
            }
            user.LastModificationTime = DateTime.Now;
            await _repository.UpdateAsync(user);
            return new ApiResult().Success();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Delete(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return ApiResult.ValidationError();
            }
            var user = await _repository.FirstOrDefaultAsync(input);
            if (user == null)
            {
                return ApiResult.DataNotFound();
            }
            user.LastModificationTime = DateTime.Now;
            user.IsDeleted = true;
            await _repository.UpdateAsync(user);
            return new ApiResult().Success();
        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<UserOutDto>> Get(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return ApiResult.ValidationError<UserOutDto>();
            }
            var user = await _repository.FirstOrDefaultAsync(input);
            if (user==null)
            {
                return ApiResult.DataNotFound<UserOutDto>();
            }
            var dto = _objectMapper.Map<UserOutDto>(user);
            return ApiResult.Success(dto);
        }

        /// <summary>
        /// 获取下级代理商用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiResult<PaginationResult<LowerAgentOutputItem>>> ListLowerAgent(ListLowerAgentInput input)
        {

            var where = BuildListLowerAgentWhere(input);

            var agents = await _lowerAgentRepository.QueryByWhereAsync<LowerAgent>(input, null, where);

            var pageDatas = new PaginationResult<LowerAgentOutputItem>(input)
                .SetReuslt(agents.TotalCount, LowerAgent.MapToList<LowerAgentOutputItem>(agents.Datas));

            return new ApiResult<PaginationResult<LowerAgentOutputItem>>().Success(pageDatas);

        }

        /// <summary>
        /// 获取下级代理商用户的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<LowerAgentOutputItem>> GetLowerAgent(string id)
        {
            var agent = await _lowerAgentRepository.GetAsync(id);
            if (agent == null)
            {
                return ApiResult.DataNotFound<LowerAgentOutputItem>();
            }
            var output = LowerAgent.MapTo<LowerAgentOutputItem>(agent);
            return new ApiResult<LowerAgentOutputItem>().Success(output);
        }

        /// <summary>
        /// 获取用户所拥有的角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<IEnumerable<UserRoleOutputItem>>> GetUserRoles(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return ApiResult.ValidationError<IEnumerable<UserRoleOutputItem>>();
            }

            var userRoles = await _userRolerepository.GetAllListAsync(i => i.UserId == id);

            return ApiResult.Success(userRoles.MapTo<IEnumerable<UserRoleOutputItem>>());
        }

        #region 私有方法

        /// <summary>
        /// 获取用户数据实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<Users> GetUser(string id)
        {
            return await _repository.FirstOrDefaultAsync(id);
        }


        private static Expression<Func<LowerAgent, bool>> BuildListLowerAgentWhere(ListLowerAgentInput input)
        {
            //todo:用户id和websiteId应该从session中拿

            Expression<Func<LowerAgent, bool>> where = i =>
                i.UserSysSetting.Userlayer == "0" &&
                i.UserSysSetting.ParentId == input.Id &&
                i.User.Id != input.Id
                && i.User.WebSiteId == input.WebSiteId;

            if (!string.IsNullOrWhiteSpace(input.Key))
            {
                where = where.And(i =>
                    i.User.RealName.Contains(input.Key) || i.User.LoginName.Contains(input.Key) ||
                    i.User.PhoneNumber.Contains(input.Key) || i.User.Shortname.Contains(input.Key));
            }

            if (!string.IsNullOrWhiteSpace(input.Swfzr))
            {
                where = where.And(i => i.UserSysSetting.Swfzr.Contains(input.Swfzr));
            }

            if (input.Start.HasValue)
            {
                where = where.And(i => i.UserSysSetting.ValidityDate >= input.Start);
            }

            if (input.End.HasValue)
            {
                where = where.And(i => i.UserSysSetting.ValidityDate <= input.End);
            }

            if (input.MinWdye.HasValue)
            {
                where = where.And(i => i.UserWallet.Wdye >= input.MinWdye);
            }

            if (input.MaxWdye.HasValue)
            {
                where = where.And(i => i.UserWallet.Wdye <= input.MaxWdye);
            }
            return where;
        }

        #endregion
    }
}
