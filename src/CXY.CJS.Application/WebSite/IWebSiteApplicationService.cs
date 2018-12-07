using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// WebSite应用层服务的接口方法
    ///</summary>
    public interface IWebSiteAppService : IApplicationService
    {
        /// <summary>
		/// 获取WebSite的分页列表信息
		///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<WebSiteListDto>> GetPaged(GetWebSitesInput input);


		/// <summary>
		/// 通过指定id获取WebSiteListDto信息
		/// </summary>
		Task<WebSiteListDto> GetById(EntityDto<string> input);


        /// <summary>
        /// 返回实体的EditDto
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetWebSiteForEditOutput> GetForEdit(EntityDto<string> input);


        /// <summary>
        /// 添加或者修改WebSite的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateWebSiteInput input);


        /// <summary>
        /// 删除WebSite信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task Delete(EntityDto<string> input);


        /// <summary>
        /// 批量删除WebSite
        /// </summary>
        Task BatchDelete(List<string> input);


		/// <summary>
        /// 导出WebSite为excel表
        /// </summary>
        /// <returns></returns>
		//Task<FileDto> GetToExcel();

    }
}
