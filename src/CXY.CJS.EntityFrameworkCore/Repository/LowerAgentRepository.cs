using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Extensions;
using CXY.CJS.Model;
using CXY.CJS.Repository.MixModel;
using CXY.CJS.Repository.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace CXY.CJS.Repository
{
    public class LowerAgentRepository : ILowerAgentRepository
    {
        private readonly IRepository<User, string> _userRepository;
        private readonly IRepository<UserSysSetting, string> _userSysSetRepository;
        private readonly IRepository<UserScore, string> _userScoreRepository;
        private readonly IRepository<UserWallet, string> _userWalletRepository;
        private readonly IRepository<UserMarkupSetting, string> _userMarkupSetRepository;

        public LowerAgentRepository(IRepository<User, string> userRepository, IRepository<UserSysSetting, string> userSysSetRepository, IRepository<UserScore, string> userScoreRepository, IRepository<UserWallet, string> userWalletRepository, IRepository<UserMarkupSetting, string> userMarkupSetRepository)
        {
            _userRepository = userRepository;
            _userSysSetRepository = userSysSetRepository;
            _userScoreRepository = userScoreRepository;
            _userWalletRepository = userWalletRepository;
            _userMarkupSetRepository = userMarkupSetRepository;
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, string @where = "",
            params object[] whereParams)
        {
            return GetAllNoTracking().WhereSortPageAsync<LowerAgent, TResult>(pagination, sorts, @where, whereParams);
        }

        public Task<PaginationResult<TResult>> QueryByWhereAsync<TResult>(Pagination pagination, IEnumerable<IHasSort> sorts, Expression<Func<LowerAgent, bool>> @where)
        {
            return GetAllNoTracking().WhereSortPageAsync<LowerAgent, TResult>(pagination, sorts, @where);
        }

        public IQueryable<LowerAgent> GetAll()
        {
            return from user in _userRepository.GetAll()

                   join userSysSetting in _userSysSetRepository.GetAll() on user.Id
                     equals userSysSetting.Id into userSysSettingTemp

                   join userScore in _userScoreRepository.GetAll() on user.Id
                     equals userScore.Id into userScoreTemp

                   join userWallet in _userWalletRepository.GetAll() on user.Id
                     equals userWallet.Id into userWalletTemp

                   join userMarkupSetting in _userMarkupSetRepository.GetAll() on user.Id
                     equals userMarkupSetting.Id into userMarkupSettingTemp

                   from sysSetting in userSysSettingTemp
                   from score in userScoreTemp.DefaultIfEmpty()
                   from wallet in userWalletTemp
                   from markupSetting in userMarkupSettingTemp.DefaultIfEmpty()

                   select new LowerAgent
                   {
                       User = user,
                       UserSysSetting = sysSetting,
                       UserScore = score,
                       UserWallet = wallet,
                       UserMarkupSetting = markupSetting,
                   };
        }

        public IQueryable<LowerAgent> GetAllNoTracking()
        {
            return GetAll().AsNoTracking();
        }

        public Task<LowerAgent> GetAsync(string id)
        {
            return GetAllNoTracking().FirstOrDefaultAsync(i => i.User.Id == id);
        }
    }
}