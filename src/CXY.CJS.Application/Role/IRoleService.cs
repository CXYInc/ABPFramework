using Abp.Application.Services;
using CXY.CJS.Role.Dto;
using CXY.CJS.Core.WebApi;
using System.Threading.Tasks;
using CXY.CJS.Repository.SeedWork;
using Abp.Application.Services.Dto;

namespace CXY.CJS.Application
{
    public interface IRoleService : IApplicationService
    {
        /// <summary>
        /// 列出用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PaginationResult<ListRoleOutputItem>> ListRole(ListRoleInput input);


        /// <summary>
        /// 创建/更新角色
        /// </summary>
        /// <param name="roleEditInputDto"></param>
        /// <returns></returns>
        Task<ApiResult<Model.Role>> SaveOrUpdateRole(RoleEditInputDto roleEditInputDto);

        /// <summary>
        /// 删除角色(软删除)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteRoleAsync(EntityDto<string> input);
        //绑定菜单
    }
}