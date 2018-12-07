using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
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
