using CXY.CJS.Application;
using CXY.CJS.Role.Dto;
using System.Threading.Tasks;
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
        public async Task ListRole_When_NotFound()
        {
            var result = await _service.ListRole(new  ListRoleInput
            {
                PageIndex = 10,
                PageSize = 1000
            });
            Assert.Empty(result.Datas);

         
        }
    }
}