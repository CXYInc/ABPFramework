using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.Application.Dtos
{
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(1, PageLtmConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public PagedInputDto()
        {
            MaxResultCount = PageLtmConsts.DefaultPageSize;
        }
    }
}