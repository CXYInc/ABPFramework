using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;

namespace CXY.CJS.Application
{
    /// <summary>
    /// BatchCar应用层服务的接口方法
    ///</summary>
    public interface IBatchCarViolationService : IApplicationService
    {
        /// <summary>
		/// 获取BatchCar的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<BatchCarListDto>> GetPaged(GetBatchCarsInput input);


		/// <summary>
		/// 通过指定id获取BatchCarListDto信息
		/// </summary>
		Task<BatchCarListDto> GetById(EntityDto<string> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetBatchCarForEditOutput> GetForEdit(BatchCarEditDto input);


        /// <summary>
        /// 添加或者修改BatchCar的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateBatchCarInput input);


        /// <summary>
        /// 删除BatchCar信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<string> input);


        /// <summary>
        /// 批量删除BatchCar
        /// </summary>
        Task BatchDelete(List<string> input);


		///// <summary>
  //      /// 导出BatchCar为excel表
  //      /// </summary>
  //      /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
