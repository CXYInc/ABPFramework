using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    public interface IUserWalletService : IApplicationService
    {

        /// <summary>
        /// 通过指定id获取UserWalletListDto信息
        /// </summary>
        Task<ApiResult<UserWalletListDto>> GetById(EntityDto<string> input);
    }
}

