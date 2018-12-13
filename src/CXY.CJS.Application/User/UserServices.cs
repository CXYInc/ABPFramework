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

namespace CXY.CJS.Application
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserServices : CJSAppServiceBase, IUserServices
    {
        private readonly IRepository<User, string> _repository;
        private readonly IObjectMapper _objectMapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="objectMapper"></param>
        public UserServices(IRepository<User, string> repository, IObjectMapper objectMapper)
        {
            _repository = repository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="userEditInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> Create(UserEditInputDto userEditInput)
        {
            var result = new ApiResult<string>().Success();

            if (userEditInput == null)
            {
                return result.Error("参数有误");
            }

            try
            {
                var user = _objectMapper.Map<User>(userEditInput);

                user.Id = Guid.NewGuid().ToString("N");
                user.EmailAddress = "hausthy@163.com";

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
        public async Task<ApiResult<string>> Update(UserEditInputDto userEditInput)
        {
            var result = new ApiResult<string>().Success();

            if (userEditInput == null || userEditInput.Id.IsNullOrEmpty())
            {
                return result.Error("参数有误");
            }

            try
            {
                var user = await _repository.GetAsync(userEditInput.Id);

                _objectMapper.Map(userEditInput, user);

                user.EmailAddress = "hausthy@163.com";

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
        /// 获取用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<UserOutDto>> Get(EntityDto<string> input)
        {
            var result = new ApiResult<UserOutDto> { Code = 1 };
            try
            {
                var user = await _repository.GetAsync(input.Id);

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

        #endregion
    }
}
