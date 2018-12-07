using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CXY.CJS.Constant;

namespace  CXY.CJS.Application.Dtos
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        [Range(1, PageLtmConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public string FilterText { get; set; }


		 
		 
         


        public PagedAndFilteredInputDto()
        {
            MaxResultCount = PageLtmConsts.DefaultPageSize;
        }
    }
}