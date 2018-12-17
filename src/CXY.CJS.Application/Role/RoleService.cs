using Abp.Application.Services.Dto;
using Abp.ObjectMapping;
using CXY.CJS.Application.Dto;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;
using Abp.ObjectMapping;
using System;
using System.Linq;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Extensions;

namespace CXY.CJS.Application
{
    public class RoleService : CJSAppServiceBase, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IUserRepository _uerRepository;

        private readonly IObjectMapper _objectMapper;

        public RoleService(IRoleRepository roleRepository, IObjectMapper objectMapper, IRoleMenuRepository roleMenuRepository, IUserRepository uerRepository)
        {
            _roleRepository = roleRepository;
            _roleMenuRepository = roleMenuRepository;
            _uerRepository = uerRepository;
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

            if (roleEditInputDto == null)
            {
                return result.Error("参数有误");
            }
            try
            {
                var roleList = _roleRepository.GetAll();
                var role = _objectMapper.Map<Model.Role>(roleEditInputDto);
                if (string.IsNullOrEmpty(roleEditInputDto.Id))
                {
                    var findRole = roleList.Where(o => o.WebSiteId == roleEditInputDto.WebSiteId && o.Name == roleEditInputDto.Name && !o.IsDeleted);
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
                    var findRole = roleList.Where(o => o.WebSiteId == roleEditInputDto.WebSiteId && o.Id == roleEditInputDto.Id);
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
        public async Task<PaginationResult<ListRoleOutputItem>> ListRole(ListRoleInput input)
        {
            // return await _roleRepository.QueryByWhereAsync<ListRoleOutputItem>(inputBase,  new IHasSort[]{ inputBase },"Name!=@0 and Name!=@1","haha","haha1");
            return await _roleRepository.QueryByWhereAsync<ListRoleOutputItem>(input, new IHasSort[] { input });
        }

        ///// <summary>
        ///// 获取角色菜单
        ///// </summary>
        ///// <returns></returns>
        //public Task<ListResultDto<MenuOutputItem>> GetMenus()
        //{
        //    var userInfo = GetUserInfo();
        //    //获取用户所含角色 
        //    //根据角色获取菜单
        //}
    }
}