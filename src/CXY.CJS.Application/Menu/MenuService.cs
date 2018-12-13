using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using CXY.CJS.Menu.Dto;
using CXY.CJS.Repository;
using CXY.CJS.Repository.Extensions;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;

namespace CXY.CJS.Menu
{
    public class MenuService : IMenuService
    {
        private IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuOutputItem> GetMenu(string id)
        {
            var menu = await GetByIdAsync(id);
            return menu == null ? null : menu.MapTo<MenuOutputItem>();
        }

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PaginationResult<MenuOutputItem>> ListMenu(ListMenuInput input)
        {
            return _menuRepository.QueryByWhereAsync<MenuOutputItem>
                (input, null, i => i.IsDeleted == false);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> SaveMenu(SaveMenuInput id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(UpdateMenuInput input)
        {
            var menu = await GetByIdAsync(input.Id);
            if (menu == null)
            {
                throw new UserFriendlyException("不存在该菜单！");
            }
            // 更新菜单时不允许更新父节点
            menu.MenuName = input.MenuName;
            menu.MenuLeval = input.MenuLeval;
            menu.MenuUrl = input.MenuUrl;
            menu.MenuLayer = input.MenuLayer;
            menu.IsSys = input.IsSys;
            menu.IsOut = input.IsOut;
            menu.IsParent = input.IsParent;
            menu.TargetFrame = input.TargetFrame;
            menu.Weight = input.Weight;
            menu.LastModificationTime=DateTime.Now;
            await _menuRepository.UpdateAsync(menu);
            return true;
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveMenu(string id)
        {
            var menu = await GetByIdAsync(id);
            if (menu == null)
            {
                throw new UserFriendlyException("不存在该菜单！");
            }
            menu.IsDeleted = true;
            await _menuRepository.UpdateAsync(menu);
            return true;
        }

        private async Task<Model.Menu> GetByIdAsync(string id)
        {
            return await _menuRepository.FirstOrDefaultAsync(i => i.Id == id);
        }


        private IQueryable<Model.Menu> ExcludeDeletedQueryable()
        {
            return _menuRepository.GetAll().Where(i => i.IsDeleted == false);
        }
    }
}