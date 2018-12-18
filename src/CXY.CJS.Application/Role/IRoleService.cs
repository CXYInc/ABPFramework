using Abp.Application.Services;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Repository.SeedWork;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRoleService : IApplicationService
    {
        /// <summary>
        /// 列出用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiPageResult<ListRoleOutput>> ListRole(ListRoleInput input);

        /// <summary>
        /// 创建/更新角色
        /// </summary>
        /// <param name="roleEditInputDto"></param>
        /// <returns></returns>
        Task<ApiResult<Role>> SaveOrUpdateRole(RoleEditInput roleEditInputDto);

        /// <summary>
        /// 删除角色(软删除)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteRoleAsync(EntityDto<string> input);

        /// <summary>
        /// 获取用户角色菜单
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<MenuOutputItem>> GetUserRoleMenusAsync();


        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult> GrantOrRemoveRolMenu(GrantRoleMenuInput input);
    }
}