using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Common;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Extension;
using CXY.CJS.Core.NPOI;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 客服违章服务
    /// </summary>
    [Route(CJSConsts.RoutePrefix + "/BatchCarViolation/[action]")]
    [AllowAnonymous]
    public class BatchCarViolationService : CJSAppServiceBase, IBatchCarViolationService
    {
        private readonly IRepository<BatchCar, string> _entityRepository;
        private readonly IObjectMapper _objectMapper;

        public BatchCarViolationService(IRepository<BatchCar, string> entityRepository, IObjectMapper objectMapper)
        {
            _entityRepository = entityRepository;
            _objectMapper = objectMapper;
        }

        /// <summary>
        /// 获取BatchCar的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PagedResultDto<BatchCarListDto>> GetPaged(GetBatchCarsInput input)
        {

            var query = _entityRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = query.Count();

            var entityList = query.OrderBy(input.Sorting).PageBy(input).ToList();

            // var entityListDtos = ObjectMapper.Map<List<BatchCarListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<BatchCarListDto>>();

            return new PagedResultDto<BatchCarListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取BatchCarListDto信息
        /// </summary>
        [HttpPost]
        public async Task<BatchCarListDto> GetById(EntityDto<string> input)
        {
            var entity = await _entityRepository.GetAsync(input.Id);

            return entity.MapTo<BatchCarListDto>();
        }

        /// <summary>
        /// 获取编辑 BatchCar
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GetBatchCarForEditOutput> GetForEdit(BatchCarEditDto input)
        {
            var output = new GetBatchCarForEditOutput();
            BatchCarEditDto editDto;

            if (!string.IsNullOrEmpty(input.Id))
            {
                var entity = await _entityRepository.GetAsync(input.Id);

                editDto = entity.MapTo<BatchCarEditDto>();

                //batchCarEditDto = ObjectMapper.Map<List<batchCarEditDto>>(entity);
            }
            else
            {
                editDto = new BatchCarEditDto();
            }

            output.BatchCar = editDto;
            return output;
        }

        /// <summary>
        /// 添加或者修改BatchCar的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateOrUpdate(CreateOrUpdateBatchCarInput input)
        {

            if (!string.IsNullOrEmpty(input.BatchCar.Id))
            {
                await Update(input.BatchCar);
            }
            else
            {
                await Create(input.BatchCar);
            }
        }

        /// <summary>
        /// 新增BatchCar
        /// </summary>
        [HttpPost]
        protected virtual async Task<BatchCarEditDto> Create(BatchCarEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BatchCar>(input);
            var entity = input.MapTo<BatchCar>();


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<BatchCarEditDto>();
        }

        /// <summary>
        /// 编辑BatchCar
        /// </summary>
        [HttpPost]
        protected virtual async Task Update(BatchCarEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除BatchCar信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除BatchCar的方法
        /// </summary>
        [HttpPost]
        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        ///// <summary>
        ///// 导出BatchCar为excel表,等待开发。
        ///// </summary>
        ///// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        #region 违章导入业务
        /// <summary>
        /// 违章导入接口
        /// </summary>
        /// <param name="importViolationDto">数据导入实体</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<IList<BatchTableModelDto>> ImportViolations([FromForm]ImportViolationDto importViolationDto)
        {
            var ds = NPOIExcelHelper.ReadExcel(importViolationDto.File);

            var tempTable = ds.Tables["订单信息"];

            var list = tempTable.ConvertToModel<BatchTableModelDto>().ToList();

            list.ForEach(x => x.Uniquecode = CommonHelper.GenerateViolationCode(x.车牌号, x.违章时间, x.违章原因));

            return new ApiResult<IList<BatchTableModelDto>>().Success(list);
        }
        #endregion
    }
}


