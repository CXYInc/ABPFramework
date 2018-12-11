using System;
using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
{
    /// <summary>
    /// 配置WebSite的AutoMapper
    /// </summary>s
    internal class WebSiteProfile : Profile
    {
        public WebSiteProfile()
        {
            CreateMap<WebSiteListDto, WebSite>();
            CreateMap<WebSiteEditDto, WebSite>();

           


            //CreateMap<Tuple<WebSite,WebSiteConfig,WebSitePayConfig>, ListWebSiteOutputItem>().ForAllMembers(options =>
            //{
               
            //    options.UseDestinationValue();
            //});
        }
    }
}
