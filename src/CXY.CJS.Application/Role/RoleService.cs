using System.Threading.Tasks;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Repository.SeedWork;
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
        public async Task<PaginationResult<ListRoleOutput>> ListRole(ListRoleInput input)
        {
            // return await _roleRepository.QueryByWhereAsync<ListRoleOutput>(input,  new IHasSort[]{ input },"Name!=@0 and Name!=@1","haha","haha1");
            return await _roleRepository.QueryByWhereAsync<ListRoleOutput>(input, new IHasSort[] { input });
        }
    }
}