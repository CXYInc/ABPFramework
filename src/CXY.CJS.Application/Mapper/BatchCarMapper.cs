
using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Model.Mapper
{

	/// <summary>
    /// 配置BatchCar的AutoMapper
    /// </summary>
	internal static class BatchCarMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <BatchCar,BatchCarListDto>();
            configuration.CreateMap <BatchCarListDto,BatchCar>();

            configuration.CreateMap <BatchCarEditDto,BatchCar>();
            configuration.CreateMap <BatchCar,BatchCarEditDto>();

        }
	}
}
