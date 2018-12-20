using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Abp.AutoMapper;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Application.Services.Dto;
using Abp.Extensions;
using Abp.Specifications;
using Castle.Core.Internal;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using Microsoft.AspNetCore.Authorization;

namespace CXY.CJS.Application
{
    /// <summary>
    /// BatchInfo应用层服务的接口实现方法  
    ///</summary>
    [Authorize]
    public class BatchInfoAppService : CJSAppServiceBase, IBatchInfoAppService
    {
        private readonly IBatchInfoRepository _entityRepository;
        private readonly IDataSeedRepository _dataSeedRepository;
        private readonly SiteConfig _webSiteConfig;
        private readonly IAbpSessionExtension _session;

        /// <summary>
        /// 构造函数 
        ///</summary>
        public BatchInfoAppService(
            IBatchInfoRepository entityRepository, IDataSeedRepository dataSeedRepository, SiteConfig webSiteConfig, IAbpSessionExtension session)
        {
            _entityRepository = entityRepository;
            _dataSeedRepository = dataSeedRepository;
            _webSiteConfig = webSiteConfig;
            _session = session;
        }


        /// <summary>
        /// 获取批次列表
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<ApiPageResult<BatchInfoListDto>> ListBatchInfo(GetBatchInfosListInput input)
        {
            //todo:WebSiteId
            Expression<Func<BatchInfo, bool>> where = i => !i.IsDeleted/*&&i.WebSiteId==input.WebSiteId*/;
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


        ///// <summary>
        ///// 通过指定id获取BatchInfoListDto信息
        ///// </summary>

        //public async Task<BatchInfoListDto> GetById(EntityDto<string> input)
        //{
        //    var entity = await _entityRepository.GetAsync(input.Id);

        //    return entity.MapTo<BatchInfoListDto>();
        //}

        /// <summary>
        /// 删除批次
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ApiResult> Delete(string id)
        {
            var entity = await _entityRepository.FirstOrDefaultAsync(id);
            if (entity == null)
            {
                return ApiResult.DataNotFound();
            }

            if (entity.Status == 2)
            {
                return new ApiResult().Error("当前状态不允许删除");
            }

            entity.IsDeleted = true;
            await _entityRepository.UpdateAsync(entity);
            return new ApiResult().Success();
        }


        /// <summary>
        /// 获取批次号
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> GetBatchNo(DateTime? time = null)
        {
            var timeStr = $"{DateTime.Now:yyyyMMdd}";
            if (time != null & time.HasValue)
            {
                timeStr = $"{time.Value:yyyyMMdd}";
            }
            var id= $"{_session.WebSiteId}_{timeStr}";
            var dataSeed = await _dataSeedRepository.FirstOrDefaultAsync(id);
            var isNewDataSeed = false;
            if (dataSeed == null)
            {
                isNewDataSeed = true;
                dataSeed = new DataSeed
                {
                    Id = id,
                    SeedIndex =1
                };
            }
            var prefix = _webSiteConfig.WebSitePrefix;
            var prefixs = prefix?.Split(';');
            prefix = prefixs?.FirstOrDefault(x => x.Contains(_session.WebSiteId));
            if (!prefix.IsNullOrEmpty())
            {
                prefix = prefix.Split(',').LastOrDefault();
                prefix = prefix.IsNullOrEmpty() ? "BAP" : prefix;
            }
            else
            {
                prefix = "BAP";
            }
            var code = prefix + timeStr + dataSeed.SeedIndex.ToString().PadLeft(4, '0');//形如
            dataSeed.SeedIndex += 1;
           
            if (isNewDataSeed)
            {
                await _dataSeedRepository.InsertAsync(dataSeed);
            }
            else
            {
                await _dataSeedRepository.UpdateAsync(dataSeed);
            }
            return ApiResult.Success(code);
        }
    }
}


