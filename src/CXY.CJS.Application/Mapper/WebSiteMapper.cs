using AutoMapper;
using CXY.CJS.Application.Dtos;

namespace CXY.CJS.Model.Mapper
{
    /// <summary>
    /// 配置WebSite的AutoMapper
    /// </summary>
    internal class WebSiteProfile : Profile
    {
        public WebSiteProfile()
        {
            CreateMap<WebSiteListDto, WebSite>();
            CreateMap<WebSiteEditDto, WebSite>();
        }
    }
}
