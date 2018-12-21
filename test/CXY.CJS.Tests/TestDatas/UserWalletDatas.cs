using System;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.TestDatas
{
    public class UserWalletDatas
    {
        public static readonly UserWallet SuperWebSiteLowerAgentUserWallet = new UserWallet
        {
            Id = UserDatas.SuperWebSiteLowerAgent.Id,
            Balance=100,
        };
    }
}