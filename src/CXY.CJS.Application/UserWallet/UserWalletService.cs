using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    public class UserWalletService : CJSAppServiceBase, IUserWalletService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<UserWallet, string> _userWalletRepository;
        private readonly IRepository<UserWalletFlow, string> _userWalletFlowRepository;
        public UserWalletService(IRepository<UserWallet, string> userWalletRepository, IUserRepository userRepository, IRepository<UserWalletFlow, string> userWalletFlowRepository)
        {
            _userRepository = userRepository;
            _userWalletRepository = userWalletRepository;
            _userWalletFlowRepository = userWalletFlowRepository;
        }

        /// <summary>
        /// 通过指定id获取UserWalletListDto信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ApiResult<UserWalletListDto>> GetById(EntityDto<string> input)
        {
            ApiResult<UserWalletListDto> apiResult = new ApiResult<UserWalletListDto>();
            var entity = await _userWalletRepository.GetAsync(input.Id);
            var result = entity.MapTo<UserWalletListDto>();
            return apiResult.Success(result);
        }
    }
}
