using Abp.Extensions;
using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Common;
using CXY.CJS.Core.Enums;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Model;
using System;

namespace CXY.CJS.Application.Mapper
{
    /// <summary>
    /// BatchCarViolation实体映射
    /// </summary>
    internal class BatchCarViolationMapperProfile : Profile
    {
        public BatchCarViolationMapperProfile()
        {
            var mappingExpression = CreateMap<BatchTableModelDto, BatchAskPriceViolationAgent>();
            ViolationForMember(mappingExpression);
            var carMappingExpression = CreateMap<BatchTableModelDto, BatchCar>();
            CarForMember(carMappingExpression);
        }

        private void ViolationForMember(IMappingExpression<BatchTableModelDto, BatchAskPriceViolationAgent> mappingExpression)
        {
            mappingExpression.ForMember(x => x.State, map => map.ResolveUsing((s, d) =>
            {
                if (d.Id.IsNullOrWhiteSpace())
                    return (int)ViolationStateEnum.WaitHandle;
                return d.State;
            }))
                .ForMember(x => x.CreationTime, map => map.ResolveUsing((s, d) =>
                {
                    if (d.Id.IsNullOrWhiteSpace())
                        return DateTime.Now;
                    return d.CreationTime;
                }))
                .ForMember(x => x.Id, map => map.ResolveUsing((s, d) =>
                {
                    if (d.Id.IsNullOrWhiteSpace())
                        return Guid.NewGuid().ToString("N");
                    return d.Id;
                }))
                .ForMember(x => x.AgentPrice, map => map.MapFrom(x => x.代办成本.ToDecimal(0)))
                .ForMember(x => x.AgentUserId, map => map.MapFrom(x => x.AgentUserId))
                .ForMember(x => x.AgentUserName, map => map.MapFrom(x => x.AgentUserName))
                .ForMember(x => x.Archive, map => map.MapFrom(x => x.文书号))
                .ForMember(x => x.BatchId, map => map.ResolveUsing((s, d) =>
                {
                    if (d.BatchId.IsNullOrWhiteSpace())
                        return s.BatchId;
                    return d.BatchId;
                }))
                .ForMember(x => x.CanProcess, map => map.UseValue(0))
                .ForMember(x => x.Uniquecode, map => map.MapFrom(x => x.Uniquecode))
                .ForMember(x => x.Count, map => map.MapFrom(x => x.罚金.ToDecimal(0)))
                .ForMember(x => x.Latefine, map => map.MapFrom(x => x.滞纳金.ToDecimal(0)))
                .ForMember(x => x.Poundage, map => map.MapFrom(x => x.手续费.ToDecimal(0)))
                .ForMember(x => x.PriceFrom, map => map.ResolveUsing(s =>
                {
                    if (s.手续费.IsNullOrEmpty())
                        return (int)PriceFromEnum.System;

                    return (int)PriceFromEnum.Person;
                }))
                .ForMember(x => x.Code, map => map.MapFrom(x => x.违法代码))
                .ForMember(x => x.ViolationTime, map => map.MapFrom(x => DateTime.Parse(x.违章时间)))
                .ForMember(x => x.Remarks, map => map.MapFrom(x => x.备注))
                .ForMember(x => x.Degree, map => map.MapFrom(x => x.扣分.ToInt(0)))
                .ForMember(x => x.LocationId, map => map.ResolveUsing(x =>
                {
                    if (!x.违章城市代码.IsNullOrWhiteSpace())
                        return x.违章城市代码;
                    return CommonHelper.GetLocationId(x.违章城市);
                }))
                .ForMember(x => x.Location, map => map.MapFrom(x => x.违章地点))
                .ForMember(x => x.LocationName, map => map.MapFrom(x => x.违章城市))
                .ForMember(x => x.OrderByNo, map => map.MapFrom(x => x.序号))
                .ForMember(x => x.Reason, map => map.MapFrom(x => x.违章原因))
                .ForMember(x => x.Status, map => map.UseValue((int)ViolationStatusEnum.CanProcess))
                .ForMember(x => x.DataStatus, map => map.MapFrom(x => x.DataStatus))
                .ForMember(x => x.WebSiteId, map => map.ResolveUsing((s, d) =>
                {
                    if (d.WebSiteId.IsNullOrWhiteSpace())
                        return s.WebSiteId;
                    return d.WebSiteId;
                }));
        }

        private void CarForMember(IMappingExpression<BatchTableModelDto, BatchCar> mappingExpression)
        {
            mappingExpression.ForMember(x => x.CreationTime, map => map.ResolveUsing((s, d) =>
                {
                    if (d.Id.IsNullOrWhiteSpace())
                        return DateTime.Now;
                    return d.CreationTime;
                }))
                .ForMember(x => x.Id, map => map.ResolveUsing((s, d) =>
                {
                    if (d.Id.IsNullOrWhiteSpace())
                        return Guid.NewGuid().ToString("N");
                    return d.Id;
                }))
                .ForMember(x => x.CarNumber, map => map.MapFrom(x => x.车牌号))
                .ForMember(x => x.CarCode, map => map.MapFrom(x => x.车架号))
                .ForMember(x => x.EngineNo, map => map.MapFrom(x => x.发动机号))
                .ForMember(x => x.PrivateCar, map => map.ResolveUsing(s =>
                {
                    var carNatureResult = s.车型名称.GetEnumByDesc<CarNatureEnum>();
                    return carNatureResult == CarNatureEnum.Company;
                }))
                .ForMember(x => x.CarTypeName, map => map.MapFrom(x => x.车型名称))
                .ForMember(x => x.CarType, map => map.ResolveUsing(s =>
                 {
                     var carCodeResult = s.车型名称.GetEnumByDesc<CarTypeEnum>();
                     return ((int)carCodeResult).ToString().PadLeft(2, '0');
                 }))
                .ForMember(x => x.IsLock, map => map.ResolveUsing(s =>
                 {
                     if (s.是否超证.IsNullOrEmpty() || s.是否超证.Equals("否"))
                         return (int)CarChooseEnum.UnChoose;

                     return (int)CarChooseEnum.Choosed;
                 }))
                .ForMember(x => x.IsChoose, map => map.ResolveUsing(s =>
                {
                    if (s.是否挑单.IsNullOrEmpty() || s.是否挑单.Equals("是"))
                        return (int)CarChooseEnum.Choosed;

                    return (int)CarChooseEnum.UnChoose;
                }))
                .ForMember(x => x.BatchId, map => map.ResolveUsing((s, d) =>
                {
                    if (d.BatchId.IsNullOrWhiteSpace())
                        return s.BatchId;
                    return d.BatchId;
                }))
                .ForMember(x => x.WebSiteId, map => map.ResolveUsing((s, d) =>
                {
                    if (d.WebSiteId.IsNullOrWhiteSpace())
                        return s.WebSiteId;
                    return d.WebSiteId;
                }));
        }
    }
}
