
using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

using Abp.UI;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.Domain.Repositories;
using Abp.Application.Services;
using Abp.Application.Services.Dto;


using CXY.CJS.Model;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Application
{
    /// <summary>
    /// Orders应用层服务的接口方法
    ///</summary>
    public interface IOrderAppService : IApplicationService
    {
        /// <summary>
		/// 获取Orders的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiPageResult<OrderListDto>> GetPaged(GetOrdersInput input);


        /// <summary>
        /// 通过指定id获取OrdersListDto信息
        /// </summary>
        Task<OrderListDto> GetById(EntityDto<string> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetOrderForEditOutput> GetForEdit(OrderEditDto input);


        /// <summary>
        /// 添加或者修改Orders的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateOrderInput input);


        /// <summary>
        /// 删除Orders信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<string> input);


        /// <summary>
        /// 批量删除Orders
        /// </summary>
        Task BatchDelete(List<string> input);


        /// <summary>
        /// 导出Orders为excel表
        /// </summary>
        /// <returns></returns>
        //Task<FileDto> GetToExcel();

    }
}
