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
using System.Threading.Tasks;
using System.Linq.Expressions;
using Abp.Specifications;
using CXY.CJS.Core.Utils;
using CXY.CJS.Repository;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserServices : CJSAppServiceBase, IUserServices
    {
        private readonly IRepository<User, string> _repository;
        private readonly IObjectMapper _objectMapper;
        private ILowerAgentRepository _lowerAgentRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="objectMapper"></param>
        public UserServices(IRepository<User, string> repository, IObjectMapper objectMapper, ILowerAgentRepository lowerAgentRepository)
        {
            _repository = repository;
            _objectMapper = objectMapper;
            _lowerAgentRepository = lowerAgentRepository;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userEditInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Create(CreateUserInput userEditInput)
        {
            var result = new ApiResult<string>().Success();

            if (userEditInput == null)
            {
                return result.Error("参数有误");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(userEditInput.Password))
                {
                    return result.Error("请输入密码！");
                }
                var user = _objectMapper.Map<User>(userEditInput);
                var time = DateTime.Now;
                user.Id = userEditInput.WebSiteId + time.ToString("yyyyMMddHHmmss") + RNG.Next(10).ToString().PadLeft(10, '0');
                user.CreationTime = time;
                user.Password = Encryptor.MD5Entry(userEditInput.Password);
                await _repository.InsertAsync(user);
                result.Data = user.Id;
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userEditInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> Update(UserEditInputDto userEditInput)
        {
            var result = new ApiResult().Success();

            if (userEditInput == null || userEditInput.Id.IsNullOrEmpty())
            {
                return result.Error("参数有误");
            }
            try
            {
                var user = await _repository.GetAsync(userEditInput.Id);

                user.UserName = userEditInput.UserName;
                user.RealName = userEditInput.RealName;
                user.Shortname = userEditInput.Shortname;
                user.FullName = userEditInput.FullName;
                user.LoginName = userEditInput.LoginName;
                user.WebSiteId = userEditInput.WebSiteId;
                user.PhoneNumber = userEditInput.PhoneNumber;
                user.EmailAddress = userEditInput.EmailAddress;
                user.RecommendUserName = userEditInput.RecommendUserName;
                user.RecommendUserid = userEditInput.RecommendUserid;
                user.Address = userEditInput.Address;
                user.UserProvince = userEditInput.UserProvince;
                user.UserCity = userEditInput.UserCity;
                user.PaymentPwd = userEditInput.PaymentPwd;
                user.IsPaymentPwd = userEditInput.IsPaymentPwd;
                if (!string.IsNullOrWhiteSpace(userEditInput.Password))
                {
                    user.Password = Encryptor.MD5Entry(userEditInput.Password);
                }

                user.LastModificationTime=DateTime.Now;
                var dto = await _repository.UpdateAsync(user);
                result.Data = dto.Id;
            }
            catch (EntityNotFoundException)
            {
                result.Code = 0;
                result.Message = "未找到数据";
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiResult> Delete(string input)
        {
            var result = new ApiResult().Success();

            if (string.IsNullOrWhiteSpace(input))
            {
                return result.Error("参数有误");
            }
            try
            {
                var user = await _repository.GetAsync(input);
                user.LastModificationTime = DateTime.Now;
                user.IsDeleted = true;
               var dto = await _repository.UpdateAsync(user);
            }
            catch (EntityNotFoundException)
            {
                result.Code = 0;
                result.Message = "未找到数据";
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;

        }

        /// <summary>
        /// 获取用户详情信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<UserOutDto>> Get(string input)
        {
            var result = new ApiResult<UserOutDto> { Code = 1 };
            try
            {
                var user = await _repository.GetAsync(input);

                var dto = _objectMapper.Map<UserOutDto>(user);

                result.Data = dto;
            }
            catch (EntityNotFoundException)
            {
                result.Code = 1;
                result.Message = "";
            }
            catch (Exception ex)
            {
                result.Code = 0;
                result.Message = ex.Message;
            }

            return result;
        }

        ///// <summary>
        ///// 获取用户列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult<List<UserOutDto>>> GetList()
        //{
        //    var list = await _repository.GetAllListAsync();

        //    var dtos = _objectMapper.Map<List<UserOutDto>>(list);

        //    return new ApiResult<List<UserOutDto>>().Success(data: dtos);

        //}

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResult<string>> Delete(EntityDto<string> input)
        {
            var result = new ApiResult<string>().Success("删除成功");

            if (input == null || input.Id.IsNullOrEmpty())
            {
                return result.Error("参数有误");
            }

            try
            {
                var user = await _repository.GetAsync(input.Id);

                if (user != null)
                    await _repository.DeleteAsync(user);

                result.Data = user.Id;
            }
            catch (EntityNotFoundException)
            {
                result.Code = 0;
                result.Message = "未找到数据";
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;
        }

        public async Task<ApiResult<PaginationResult<LowerAgentOutputItem>>> ListLowerAgent(ListLowerAgentInput input)
        {

            var where = BuildListLowerAgentWhere(input);

            var agents = await _lowerAgentRepository.QueryByWhereAsync<LowerAgent>(input, null, where);

            var pageDatas = new PaginationResult<LowerAgentOutputItem>(input)
                .SetReuslt(agents.TotalCount, LowerAgent.MapToList<LowerAgentOutputItem>(agents.Datas));

            return new ApiResult<PaginationResult<LowerAgentOutputItem>>().Success(pageDatas);

        }

        public async Task<ApiResult<LowerAgentOutputItem>> GetLowerAgent(string id)
        {
            var agent = await _lowerAgentRepository.GetAsync(id);
            if (agent==null)
            {
                return  new ApiResult<LowerAgentOutputItem>().Error("不存在该数据");
            }
            var output = LowerAgent.MapTo<LowerAgentOutputItem>(agent);
            return  new ApiResult<LowerAgentOutputItem>().Success(output);
        }


        #region 私有方法

        /// <summary>
        /// 获取用户数据实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<User> GetUser(string id)
        {
            return await _repository.GetAsync(id);
        }


        private static Expression<Func<LowerAgent, bool>> BuildListLowerAgentWhere(ListLowerAgentInput input)
        {
            //todo:用户id和websiteId应该从session中拿

            Expression<Func<LowerAgent, bool>> where = i =>
                i.UserSysSetting.Userlayer == "0"&&
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
