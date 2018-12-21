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
            ForMemberExpression(mappingExpression);
        }

        private void ForMemberExpression(IMappingExpression<BatchTableModelDto, BatchAskPriceViolationAgent> mappingExpression)
        {
            mappingExpression.ForMember(x => x.Id, map => map.ResolveUsing(s => { return Guid.NewGuid().ToString("N"); }))
                .ForMember(x => x.AgentPrice, map => map.MapFrom(x => x.代办成本.ToDecimal(0)))
                .ForMember(x => x.Archive, map => map.MapFrom(x => x.文书号))
                .ForMember(x => x.BatchId, map => map.MapFrom(x => x.BatchId))
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
                    return CommonHelper.GetLocationId(x.违章城市代码);
                }))
                .ForMember(x => x.Location, map => map.MapFrom(x => x.违章地点))
                .ForMember(x => x.LocationName, map => map.ResolveUsing(x => x.违章城市))
                .ForMember(x => x.OrderByNo, map => map.MapFrom(x => x.序号))
                .ForMember(x => x.Reason, map => map.MapFrom(x => x.违章原因))
                .ForMember(x => x.State, map => map.UseValue((int)ViolationStateEnum.WaitHandle))
                .ForMember(x => x.Status, map => map.UseValue((int)ViolationStatusEnum.CanProcess))
                .ForMember(x => x.DataStatus, map => map.MapFrom(x => x.DataStatus))
                .ForMember(x => x.WebSiteId, map => map.MapFrom(x => x.WebSiteId))
                .ForMember(x => x.CreationTime, map => map.UseValue(DateTime.Now))
                .ForMember(x => x.CreationTime, map => map.UseValue(DateTime.Now))
                .ForMember(x => x.CreationTime, map => map.UseValue(DateTime.Now));
        }
    }
}
