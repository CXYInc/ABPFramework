using System.Threading.Tasks;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Role.Dto;
using CXY.CJS.WebApi;

namespace CXY.CJS.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 列出用户角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PaginationResult<ListRoleInput>> ListRole(ListRoleInput input)
        {
            return await _roleRepository.QueryByWhereAsync<ListRoleInput>(input,  new IHasSort[]{ input });
        }
    }
}