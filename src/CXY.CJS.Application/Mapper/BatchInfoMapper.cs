
using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Model.Mapper
{

    /// <summary>
    /// 配置BatchInfo的AutoMapper
    /// </summary>
    internal class BatchInfoMapper : Profile
    {
        public BatchInfoMapper()
        {
            CreateMap<BatchCar, IndoorPriceInput>()
                .ForMember(x => x.WebSiteId, map => map.Ignore())
                .ForMember(x => x.BatchId, map => map.MapFrom(x => x.Id))
                .ForMember(x => x.UserId, map => map.Ignore())
                .ForMember(x => x.CarId, map => map.MapFrom(x => x.CarNumber))
                .ForMember(x => x.CarNature, map => map.MapFrom(x => x.PrivateCar? "个人" : "单位"));
        }
    }
}

