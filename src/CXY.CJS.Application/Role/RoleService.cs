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

        public async Task<PaginationResult<Model.Role>> ListRole(ListRoleInput input)
        {
            return await _roleRepository.QueryByWhereAsync(input, null, null, new IHasSort[]{ input }, null);
        }
    }
}