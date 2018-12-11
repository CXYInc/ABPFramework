using Abp.Application.Services;
using CXY.CJS.Role.Dto;
using CXY.CJS.WebApi;
using System.Threading.Tasks;
using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Role
{
    public interface IRoleService: IApplicationService
    {
        /// <summary>
        /// 列出用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PaginationResult<ListRoleOutput>> ListRole(ListRoleInput input);
    }
}