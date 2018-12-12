using System.Threading.Tasks;
using CXY.CJS.Role;
using CXY.CJS.Role.Dto;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace CXY.CJS.Tests.Application.Role
{
    public class RoleServiceTest: IClassFixture<CJSTestBase>
    {
        private readonly IRoleService _service;
        public RoleServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IRoleService>();
        }


        [Fact]
        public async Task ListRole_When_HasDatas()
        {
            var result = await _service.ListRole(new  ListRoleInput
            {
                PageIndex = 1,
                PageSize = 10
            });
            Assert.NotEmpty(result.Datas);

         
        }
    }
}