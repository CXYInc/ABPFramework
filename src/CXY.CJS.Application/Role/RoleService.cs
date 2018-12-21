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
using Abp.AutoMapper;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [Authorize]
    public class RoleService : CJSAppServiceBase, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _uerRepository;
        private readonly IRepository<UserRole, string> _userRoleRepository;
        private readonly IRepository<RoleMenu, string> _roleMenuRepository;
        private readonly IRepository<Menu, string> _menuRepository;

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
        public async Task<ApiResult<Role>> SaveOrUpdateRole(RoleEditInput roleEditInputDto)
        {
            var result = new ApiResult<Role>().Success();
            try
            {
                var roleList = _roleRepository.GetAll();
                var role = _objectMapper.Map<Role>(roleEditInputDto);
                if (string.IsNullOrEmpty(roleEditInputDto.Id))
                {
                    var findRole = roleList.FirstOrDefault(o => o.WebSiteId == AbpSession.WebSiteId && o.Name == roleEditInputDto.Name && !o.IsDeleted);
                    if (findRole == null)
                    {
                        role.Id = Guid.NewGuid().ToString("N");
                        role.WebSiteId = AbpSession.WebSiteId;
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
                    var findRole = roleList.FirstOrDefault(o => o.WebSiteId == AbpSession.WebSiteId && o.Id == roleEditInputDto.Id);
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
        public async Task<ApiPageResult<ListRoleOutput>> ListRole(ListRoleInput input)
        {
            // return await _roleRepository.QueryByWhereAsync<ListRoleOutputItem>(inputBase,  new IHasSort[]{ inputBase },"Name!=@0 and Name!=@1","haha","haha1");
            return (await _roleRepository.QueryByWhereAsync<ListRoleOutput>(input, new IHasSort[] { input })).ToApiPageResult();
        }


        /// <summary>
        /// 获取用户的角色的菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<MenuOutputItem>> GetUserRoleMenusAsync()
        {
            try
            {
                //获取用户所含角色 
                var userRoles = await _userRoleRepository.GetAllListAsync(o => o.UserId == AbpSession.UserId);
                //根据角色获取菜单
                var roleMenu = await _roleMenuRepository.GetAllListAsync();
                var menuList = await _menuRepository.GetAllListAsync();
                var result = from a in userRoles
                             join b in roleMenu on a.RoleId equals b.RoleId
                             join m in menuList on b.MenuId equals m.Id
                             select new Model.Menu { };
                result = result.Distinct().ToList();

                return ApiResult.Success(result.MapTo<MenuOutputItem>());
            }
            catch (Exception ex)
            {
                return new ApiResult<MenuOutputItem>().Error(ex.Message);
            }

        }


        /// <summary>
        /// 给角色授权
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiResult> GrantOrRemoveRolMenu(GrantRoleMenuInput input)
        {
            try
            {
                var roleTask = _userRoleRepository.FirstOrDefaultAsync(input.RoleId);
                var menuTask = _menuRepository.FirstOrDefaultAsync(input.MenuId);
                var roleMenuTask = _roleMenuRepository.GetAllListAsync(o => o.RoleId == input.RoleId && o.MenuId == input.MenuId);
                var (role, menu, roleMenu) = await (roleTask, menuTask, roleMenuTask);
                if (role == null)
                {
                    return new ApiResult().Error("无法找到该角色！");
                }
                if (menu == null)
                {
                    return new ApiResult().Error("无法找到该菜单！");
                }
                if (input.IsGrant)
                {
                    if (roleMenuTask == null)
                    {
                        await _roleMenuRepository.InsertAsync(new RoleMenu
                        {
                            Id = Guid.NewGuid().ToString(),
                            RoleId = role.Id,
                            MenuId = menu.Id
                        });
                    }
                }
                else
                {
                    if (roleMenuTask != null)
                    {
                        await _roleMenuRepository.DeleteAsync(roleMenu.FirstOrDefault());
                    }
                }
                return new ApiResult().Success();
            }
            catch (Exception ex)
            {
                return new ApiResult().Error(ex.Message);
            }
        }
    }
}