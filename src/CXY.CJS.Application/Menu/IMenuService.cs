﻿using System.Threading.Tasks;
using Abp.Application.Services;
using CXY.CJS.Menu.Dto;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Role.Dto;

namespace CXY.CJS.Menu
{
    public interface IMenuService : IApplicationService
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuOutputItem> GetMenu(string id);

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PaginationResult<MenuOutputItem>> ListMenu(ListMenuInput input);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> SaveMenu(SaveMenuInput id);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> UpdateMenu(UpdateMenuInput id);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> RemoveMenu(string id);
    }
}