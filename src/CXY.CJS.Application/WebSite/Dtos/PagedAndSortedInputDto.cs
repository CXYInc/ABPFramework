using Abp.Application.Services.Dto;
using CXY.CJS.Core.Constant;

namespace CXY.CJS.Application.Dtos
{
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }

        public PagedAndSortedInputDto()
        {
            MaxResultCount = PageLtmConsts.DefaultPageSize;
        }
    }
}