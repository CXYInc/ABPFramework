using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
using Abp.ObjectMapping;

namespace CXY.CJS.Application
{
    /// <summary>
    /// BatchCar应用层服务的接口实现方法  
    ///</summary>
    [AbpAuthorize]
    public class BatchCarViolationService : CJSAppServiceBase, IBatchCarViolationService
    {
        private readonly IRepository<BatchCar, string> _entityRepository;
        private readonly IObjectMapper _objectMapper;

        public BatchCarViolationService(
        IRepository<BatchCar, string> entityRepository,
        IObjectMapper objectMapper
        )
        {
            _entityRepository = entityRepository;
            _objectMapper = objectMapper;
        }


        /// <summary>
        /// 获取BatchCar的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

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

        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(input.Id);
        }



        /// <summary>
        /// 批量删除BatchCar的方法
        /// </summary>

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

    }
}


