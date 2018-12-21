using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Application
{
    public interface IPriceAppService : IApplicationService
    {
        Task<ApiResult<List<PriceResultOutput>>> IndoorPriceBatch(IndoorPriceInput inputDto);

        Task<ApiResult> QuotePrice(PriceInput input);
    }
}
