using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using CXY.CJS.Core.WebApi;
using System.Threading.Tasks;
using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserServices : IApplicationService
    {
        ///// <summary>
        ///// 创建用户
        ///// </summary>
        ///// <param name="userEditInput"></param>
        ///// <returns></returns>
        //Task<ApiResult<string>> Create(UserEditInputDto userEditInput);

        ///// <summary>
        ///// 获取用户信息
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ApiResult<UserOutDto>> Get(EntityDto<string> input);

        ///// <summary>
        ///// 更新用户
        ///// </summary>
        ///// <param name="userEditInput"></param>
        ///// <returns></returns>
        //Task<ApiResult<string>> Update(UserEditInputDto userEditInput);

        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ApiResult<string>> Delete(EntityDto<string> input);

         //用户绑定角色

        /// <summary>
        /// 列出下游代理商
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<PaginationResult<LowerAgentOutputItem>>> ListLowerAgent(ListLowerAgentInput input);

        /// <summary>
        /// 获取代理商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<LowerAgentOutputItem>> GetLowerAgent(string id);
    }
}
