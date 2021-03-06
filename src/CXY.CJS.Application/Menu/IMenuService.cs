﻿using System.Threading.Tasks;
using Abp.Application.Services;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Repository.SeedWork; 

namespace CXY.CJS.Application
{
    public interface IMenuService : IApplicationService
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<MenuOutputItem>> GetMenu(string id);

        /// <summary>
        /// 菜单列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiPageResult<MenuOutputItem>> ListMenu(ListMenuInput input);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<string>> SaveMenu(SaveMenuInput input);

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateMenu(UpdateMenuInput input);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult> RemoveMenu(string id);
    }
}