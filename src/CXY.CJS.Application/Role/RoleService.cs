using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Repository.SeedWork;
using System;
using System.Linq;
using CXY.CJS.Core.Extensions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [AllowAnonymous]
    public class RoleService : CJSAppServiceBase, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _uerRepository;
        private readonly IRepository<UserRole, string> _userRoleRepository;
        private readonly IRepository<RoleMenu, string> _roleMenuRepository;
        private readonly IRepository<Model.Menu, string> _menuRepository;

        private readonly IObjectMapper _objectMapper;

        public RoleService(IRoleRepository roleRepository, IObjectMapper objectMapper, IRepository<RoleMenu, string> roleMenuRepository, IUserRepository uerRepository, IRepository<UserRole, string> userRoleRepository, IRepository<Model.Menu, string> menuRepository)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _uerRepository = uerRepository;
            _userRoleRepository = userRoleRepository;
            _menuRepository = menuRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 创建/更新角色
        /// </summary>
        /// <param name="roleEditInputDto"></param>
        /// <returns></returns>
        public async Task<ApiResult<Role>> SaveOrUpdateRole(RoleEditInputDto roleEditInputDto)
        {
            var result = new ApiResult<Role>().Success();
            try
            {
                var roleList = _roleRepository.GetAll();
                var role = _objectMapper.Map<Model.Role>(roleEditInputDto);
                if (string.IsNullOrEmpty(roleEditInputDto.Id))
                {
                    var findRole = roleList.FirstOrDefault(o => o.WebSiteId == roleEditInputDto.WebSiteId && o.Name == roleEditInputDto.Name && !o.IsDeleted);
                    if (findRole == null)
                    {
                        role.Id = Guid.NewGuid().ToString("N");
                        await _roleRepository.InsertAsync(role);
                        result.Data = role;
                    }
                    else
                    {
                        return result.Error("角色名已经存在");
                    }
                }
                else
                {
                    var findRole = roleList.FirstOrDefault(o => o.WebSiteId == roleEditInputDto.WebSiteId && o.Id == roleEditInputDto.Id);
                    if (findRole == null)
                    {
                        return result.Error("更新的角色不存在");
                    }
                    else
                    {
                        await _roleRepository.UpdateAsync(role);
                        result.Data = role;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }

            return result;
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="input">角色Id</param>
        /// <returns></returns>
        public async Task<ApiResult<string>> DeleteRoleAsync(EntityDto<string> input)
        {
            var result = new ApiResult<string>().Success("删除成功");
            try
            {
                var role = await _roleRepository.FirstOrDefaultAsync(input.Id);
                if (role != null)
                {
                    role.IsDeleted = true;
                    await _roleRepository.UpdateAsync(role); result.Data = role.Id;
                }
                else
                {
                    return result.Error("角色不存在");
                }
            }
            catch (Exception ex)
            {
                result.Error(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 列出角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiPageResult<ListRoleOutputItem>> ListRole(ListRoleInput input)
        {
            // return await _roleRepository.QueryByWhereAsync<ListRoleOutputItem>(inputBase,  new IHasSort[]{ inputBase },"Name!=@0 and Name!=@1","haha","haha1");
            return (await _roleRepository.QueryByWhereAsync<ListRoleOutputItem>(input, new IHasSort[] { input })).ToApiPageResult();
        }


        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<MenuOutputItem>> GetMenusAsync()
        {
            //获取用户所含角色 
            var userRoles = await _userRoleRepository.GetAllListAsync(o => o.UserId == AbpSession.UserId);
            //根据角色获取菜单
            var roleMenu = await _roleMenuRepository.GetAllListAsync();
            var menuList = await _menuRepository.GetAllListAsync();
            var result = from a in userRoles
                         join b in roleMenu on a.RoleId equals b.RoleId //into temp
                         join m in menuList on b.MenuId equals m.Id 
                         select new Model.Menu {};
            result = result.Distinct().ToList();

            var dto = _objectMapper.Map<ListResultDto<MenuOutputItem>>(result);

            return dto;
        }
    }
}