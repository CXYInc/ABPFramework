
using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;
namespace CXY.CJS.Model.Mapper
{

	/// <summary>
    /// 配置Order的AutoMapper
    /// </summary>
	internal static class OrderMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Order,OrderListDto>();
            configuration.CreateMap <OrderListDto,Order>();

            configuration.CreateMap <OrderEditDto,Order>();
            configuration.CreateMap <Order,OrderEditDto>();

        }
	}
}
