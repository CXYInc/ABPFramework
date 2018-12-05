
using AutoMapper;
using CXY.CJS.Model;
using CXY.CJS.Model.Dtos;

namespace CXY.CJS.Model.Mapper
{

	/// <summary>
    /// 配置WebSite的AutoMapper
    /// </summary>
	internal static class WebSiteMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap <WebSite,WebSiteListDto>();
            configuration.CreateMap <WebSiteListDto,WebSite>();

            configuration.CreateMap <WebSiteEditDto,WebSite>();
            configuration.CreateMap <WebSite,WebSiteEditDto>();

        }
	}
}
