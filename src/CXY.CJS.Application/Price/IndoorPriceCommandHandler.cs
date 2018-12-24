using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Runtime.Caching;
using Abp.Specifications;
using Abp.Threading;
using Castle.Core.Internal;
using Castle.Core.Logging;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Bus.Commands;
using CXY.CJS.Core.Extension;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using MediatR;
using Nito.AsyncEx;

namespace CXY.CJS.Application
{
  
    public class IndoorPriceCommandHandler :
        ICommandHandler<IndoorPriceAndSaveCommand, List<PriceResultOutput>>
    {
        private readonly IPriceAppService _priceAppService;
        private readonly ICacheManager _cacheManager;
        private static readonly Mutex _mut = new Mutex();
        private readonly ILogger _logger;
        private readonly IBatchAskPriceViolationAgentRepository _violationAgentRepository;
        private readonly IBatchCarRepository _batchCarRepository;
        private readonly ICarViolationDivisionRepository _carViolationDivisionRepository;
        private readonly IUserSysSettingRepository _settingRepository;

        public IndoorPriceCommandHandler(IPriceAppService priceAppService, ICacheManager cacheManager, ILogger logger, IBatchAskPriceViolationAgentRepository violationAgentRepository, IBatchCarRepository batchCarRepository, ICarViolationDivisionRepository carViolationDivisionRepository, IUserSysSettingRepository settingRepository)
        {
            _priceAppService = priceAppService;
            _cacheManager = cacheManager;
            _logger = logger;
            _violationAgentRepository = violationAgentRepository;
            _batchCarRepository = batchCarRepository;
            _carViolationDivisionRepository = carViolationDivisionRepository;
            _settingRepository = settingRepository;
        }

        [UnitOfWork]
        public virtual async Task<List<PriceResultOutput>> Handle(IndoorPriceAndSaveCommand request, CancellationToken cancellationToken)
        {
            // 获取报价结果
            var result = await _priceAppService.IndoorPriceBatch(request.IndoorPrice);
            if (result.IsSuccess)
            {
              
                // 获取用户分成配置
                var rateInfo = _settingRepository.GetAll().Where(i => i.Id == request.IndoorPrice.UserId).Select(i => new
                {
                    i.Rate,
                    i.RateType
                }).FirstOrDefault();
                if (rateInfo != null)
                {
                    // 处理返回报价信息,写分成
                    await UpdateAskPriceViolationInfo(request.IndoorPrice.CarNumber, request.IndoorPrice.BatchId, rateInfo.Rate, rateInfo.RateType, result.Data);

                    // 更新报价缓存的状态
                    if (!request.GlobalKey.IsNullOrWhiteSpace())
                    {
                        try
                        {
                            //等待3分钟
                            _mut.WaitOne(TimeSpan.FromMinutes(3));
                            var globalKey = request.GlobalKey;
                            var cacheObject = await _cacheManager.GetCache(globalKey).GetOrDefaultAsync(globalKey);
                            if (cacheObject != null)
                            {
                                QuotePriceStation station = (QuotePriceStation)cacheObject;
                                station.CompleteCount = station.CompleteCount + 1;
                                await _cacheManager.GetCache(globalKey).SetAsync(globalKey, station);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.ErrorFormat("Action:IndoorPriceAndSaveCommand-UpdateCacheObject,Request:{0},Exception{1}", request, ex);
                        }
                        finally
                        {
                            _mut.ReleaseMutex();
                        }
                    }
                }
                return result.Data;
            }
            return null;
        }


        /// <summary>
        /// 处理返回报价信息,写分成
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="batchId"></param>
        /// <param name="rate"></param>
        /// <param name="rateType"></param>
        /// <param name="listPriceModel"></param>
        /// <returns></returns>
        private async Task UpdateAskPriceViolationInfo(string carNumber, string batchId, decimal rate, int rateType,
            List<PriceResultOutput> listPriceModel)
        {
            if (!listPriceModel.IsNullOrEmpty())
            {
                //获取全部违章Code
                var uniqueCodes = listPriceModel.Select(x => x.UniqueCode).ToList();
                if (!uniqueCodes.IsNullOrEmpty())
                {

                    var carIds = await _batchCarRepository.GetAll()
                        .Where(i => i.CarNumber == carNumber && i.BatchId == batchId).Select(i => i.Id)
                        .ToDynamicListAsync<string>();
                    Expression<Func<BatchAskPriceViolationAgent, bool>> violationWhere = i =>
                        i.PriceFrom == 0 && uniqueCodes.Contains(i.Uniquecode);
                    if (!carIds.IsNullOrEmpty())
                    {
                        violationWhere = violationWhere.And(i => carIds.Contains(i.CarId));
                    }
                    //获取全部违章
                    var violations = await _violationAgentRepository.GetAllListAsync(violationWhere);
                    var violationIds = violations.Select(i => i.Id).ToList();
                    if (!violations.IsNullOrEmpty())
                    {
                        //插入的新分成
                        var newViolationDivisions = new List<CarViolationDivision>();

                        //获取已存在的分成
                        var oldViolationDivisions = await _carViolationDivisionRepository.GetAllListAsync(i =>
                            violationIds.Contains(i.ViolationId));
                       
                        //处理数据
                        foreach (var item in violations)
                        {
                            if (item == null) continue;
                            decimal vat = 0M;
                            decimal price = 0M;
                            var priceModel = listPriceModel.FirstOrDefault(x => x.UniqueCode == item.Uniquecode);

                            if (priceModel == null) continue;

                            if (priceModel.CanProcess == 1)
                            {
                                #region 分成
                                //获取接口的分成保存到分成表中
                                if (!priceModel.fcQuery.IsNullOrEmpty())
                                {
                                    newViolationDivisions = priceModel.fcQuery.Select(fcEntity =>
                                        new CarViolationDivision
                                        {
                                            CalculationExpression = "",
                                            Fc = fcEntity.FC,
                                            Id = Guid.NewGuid().ToString(),
                                            Fctype = fcEntity.FCTYPE,
                                            Fcuserid = fcEntity.FCUSERID,
                                            Gdlr = 0,
                                            ProfitType = fcEntity.ProfitType,
                                            WebSiteId = fcEntity.WebSiteId,
                                            ViolationId = item.Id
                                        }).ToList();
                                }
                                #endregion

                                #region 计算最终价格
                                //违章报价 =(成本)+手价+加价
                                decimal violationPrice = priceModel.Poundage + priceModel.PlusPrice + priceModel.ParentPlusPrice;
                                if (rateType == 0)
                                    vat = Math.Round(violationPrice * rate / 100);
                                else if (rateType == 1)
                                    vat = Math.Round((item.Count + item.Latefine) * rate / 100);
                                else if (rateType == 2)
                                    vat = Math.Round((item.Count + item.Latefine + violationPrice) * rate / 100); ;

                                price = Math.Round(violationPrice);

                                item.IsAskPrice = false;
                                item.Status = priceModel.Status.ToInt();
                                item.Poundage = price;
                                item.CanProcess = priceModel.CanProcess;
                                item.Vat = vat;
                                item.Ddbjid = priceModel.PriceId;
                                item.AgentUserId = priceModel.UserId;
                                item.AgentUserName = priceModel.ShortName;
                                item.AgentPrice = priceModel.Poundage;
                                item.ViolationType = priceModel.ViolationType;
                                #endregion
                            }
                            else
                            {
                                //如果已经在办理的违章 更新状态为在办理中 或者办理完毕
                                if (!string.IsNullOrEmpty(priceModel.Status))
                                {
                                    //更新违章
                                    item.IsAskPrice = false;
                                    item.Status = priceModel.Status.ToInt();
                                    item.Ddbjid = priceModel.PriceId;
                                    item.CanprocessMsg = priceModel.CanprocessMsg;
                                }
                            }
                        }

                        if (!oldViolationDivisions.IsNullOrEmpty())
                        {
                            await _carViolationDivisionRepository.BulkDeleteAsync(oldViolationDivisions);
                        }
                        await (
                        _carViolationDivisionRepository.BulkInsertAsync(newViolationDivisions),
                            _violationAgentRepository.BulkUpdateAsync(violations));
                    }
                }
            }
        }
    }
}