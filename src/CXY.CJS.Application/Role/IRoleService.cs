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
        Task<ApiPageResult<ListRoleOutputItem>> ListRole(ListRoleInput input);

        /// <summary>
        /// 创建/更新角色
        /// </summary>
        /// <param name="roleEditInputDto"></param>
        /// <returns></returns>
        Task<ApiResult<Role>> SaveOrUpdateRole(RoleEditInputDto roleEditInputDto);

        /// <summary>
        /// 删除角色(软删除)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteRoleAsync(EntityDto<string> input);
        //绑定菜单

       // Task<ListResultDto<MenuOutputItem>> GetMenus();
    }
}