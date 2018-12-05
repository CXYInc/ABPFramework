
using AutoMapper;
using CXY.CJS.Model;
using CXY.CJS.Model.Dtos;

namespace CXY.CJS.Model.Mapper
{

	/// <summary>
    /// 配置Test的AutoMapper
    /// </summary>
	internal static class TestMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <Test,TestListDto>();
            configuration.CreateMap <TestListDto,Test>();

            configuration.CreateMap <TestEditDto,Test>();
            configuration.CreateMap <Test,TestEditDto>();

        }
	}
}
