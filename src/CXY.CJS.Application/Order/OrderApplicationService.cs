using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using CXY.CJS.Model;
using CXY.CJS.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.WebApi;
using System.Linq.Expressions;
using System;

namespace CXY.CJS.Application
{
    /// <summary>
    /// Order应用层服务的接口实现方法  
    ///</summary> 
    [Route(CJSConsts.RoutePrefix + "/Order/[action]")]
    [Authorize]
    public class OrderAppService : CJSAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, string> _orderRepository;

        private readonly IRepository<OrderFlow, string> _orderFlowRepository;
        private readonly IRepository<OrderDivision, string> _orderDivisionRepository;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public OrderAppService(
        IRepository<Order, string> orderRepository,
        IRepository<OrderFlow, string> orderFlowRepository,
        IRepository<OrderDivision, string> orderDivisionRepository
        )
        {
            _orderRepository = orderRepository;
            _orderFlowRepository = orderFlowRepository;
            _orderDivisionRepository = orderDivisionRepository;
        }


        /// <summary>
        /// 获取Order的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<ApiPageResult<OrderListDto>> GetPaged(GetOrdersInput input)
        {
            Expression<Func<Order, bool>> where = i => !i.IsDeleted && i.WebSiteId == AbpSession.WebSiteId;
            if (!input.Id.IsNullOrEmpty())
            {
                where = where.And(i => i.Id.Contains(input.Id));
            }

            if (!input.Proxy.IsNullOrEmpty())
            {
                where = where.And(i => i.Proxy.Contains(input.Proxy));
            }

            if (input.StartTime.HasValue)
            {
                switch (input.TimeEnum)
                {
                    case BatchInfosListInputTimeEnum.CreationTime:
                        where = where.And(i => i.CreationTime >= input.StartTime);
                        break;
                    case BatchInfosListInputTimeEnum.CompleteTime:
                        where = where.And(i => i.CompleteTime >= input.StartTime);
                        break;
                }
            }
            if (input.EndTimeTime.HasValue)
            {
                switch (input.TimeEnum)
                {
                    case BatchInfosListInputTimeEnum.CreationTime:
                        where = where.And(i => i.CreationTime <= input.EndTimeTime);
                        break;
                    case BatchInfosListInputTimeEnum.CompleteTime:
                        where = where.And(i => i.CompleteTime <= input.EndTimeTime);
                        break;
                }
            }

            if (!input.Status.IsNullOrEmpty())
            {
                where = where.And(i => input.Status.Contains(i.Status));
            }

            var pageReuslt = await _entityRepository.QueryByWhereAsync<BatchInfoListDto>(input, null, where);
            return pageReuslt.ToApiPageResult();
        }


        /// <summary>
        /// 通过指定id获取OrderListDto信息
        /// </summary>

        public async Task<OrderListDto> GetById(EntityDto<string> input)
        {
            var entity = await _orderRepository.GetAsync(input.Id);

            return entity.MapTo<OrderListDto>();
        }

        /// <summary>
        /// 获取编辑 Order
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<GetOrderForEditOutput> GetForEdit(OrderEditDto input)
        {
            var output = new GetOrderForEditOutput();
            OrderEditDto editDto;

            if (!string.IsNullOrEmpty(input.Id))
            {
                var entity = await _orderRepository.GetAsync(input.Id);

                editDto = entity.MapTo<OrderEditDto>();

                //OrderEditDto = ObjectMapper.Map<List<OrderEditDto>>(entity);
            }
            else
            {
                editDto = new OrderEditDto();
            }

            output.Order = editDto;
            return output;
        }


        /// <summary>
        /// 添加或者修改Order的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task CreateOrUpdate(CreateOrUpdateOrderInput input)
        {

            if (!string.IsNullOrEmpty(input.Order.Id))
            {
                await Update(input.Order);
            }
            else
            {
                await Create(input.Order);
            }
        }


        /// <summary>
        /// 新增Order
        /// </summary>

        protected virtual async Task<OrderEditDto> Create(OrderEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <Order>(input);
            var entity = input.MapTo<Order>();


            entity = await _orderRepository.InsertAsync(entity);
            return entity.MapTo<OrderEditDto>();
        }

        /// <summary>
        /// 编辑Order
        /// </summary>

        protected virtual async Task Update(OrderEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _orderRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _orderRepository.UpdateAsync(entity);
        }



        /// <summary>
        /// 删除Order信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _orderRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除Order的方法
        /// </summary>

        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _orderRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        /// <summary>
        /// 导出Order为excel表,等待开发。
        /// </summary>
        /// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

    }
}


