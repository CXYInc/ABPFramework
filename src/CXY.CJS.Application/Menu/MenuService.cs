using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using CXY.CJS.Menu.Dto;
using CXY.CJS.Repository;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;
using Microsoft.EntityFrameworkCore;

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
                (input,null, i => i.IsDeleted == false);
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
            if (menu==null)
            {
                throw new UserFriendlyException("不存在该站点！");
            }
            // 更新菜单时不允许更新父节点
            var updateMenu = input.MapTo<Model.Menu>();
            updateMenu.ParentId = menu.ParentId;
            await _menuRepository.UpdateAsync(updateMenu);
            return true;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> RemoveMenu(string id)
        {
            throw new System.NotImplementedException();
        }

        private async Task<Model.Menu> GetByIdAsync(string id)
            => await ExcludeDeletedQueryable().FirstOrDefaultAsync(i => i.Id == id);


        private IQueryable<Model.Menu> ExcludeDeletedQueryable()
        {
            return _menuRepository.GetAll().Where(i => i.IsDeleted == false);
        }
    }
}