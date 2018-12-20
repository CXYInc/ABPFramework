using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;


namespace CXY.CJS.Application
{
    /// <summary>
    /// BatchInfo应用层服务的接口方法
    ///</summary>
    public interface IBatchInfoAppService : IApplicationService
    {
        /// <summary>
		/// 获取批次的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiPageResult<BatchInfoListDto>> ListBatchInfo(GetBatchInfosListInput input);


        ///// <summary>
        ///// 通过指定id获取BatchInfoListDto信息
        ///// </summary>
        //Task<BatchInfoListDto> GetById(EntityDto<string> input);

        /// <summary>
        /// 删除批次
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult> Delete(string id);


        /// <summary>
        /// 获取批次号
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        Task<ApiResult<string>> GetBatchNo(DateTime? time=null);
    }
}
