using Abp.Application.Services;
using CXY.CJS.Role.Dto;
using CXY.CJS.WebApi;
using System.Threading.Tasks;

namespace CXY.CJS.Role
{
    public interface IRoleService: IApplicationService
    {
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PaginationResult<Model.Role>> ListRole(ListRoleInput input);
    }
}