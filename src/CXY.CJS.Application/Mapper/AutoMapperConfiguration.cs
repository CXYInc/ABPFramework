using AutoMapper;

namespace CXY.CJS.Model.Mapper
{
    public static class AutoMapperConfiguration
    {
        internal static void AutoMapperConfigure(this IMapperConfigurationExpression mapperConfigurationExpression)
        {
            WebSiteMapper.CreateMappings(mapperConfigurationExpression);
        }
    }
}
