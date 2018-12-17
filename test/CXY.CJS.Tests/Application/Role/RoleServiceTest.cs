using CXY.CJS.Application;
using CXY.CJS.Application.Dto;
using System.Threading.Tasks;
using CXY.CJS.Core.Enums;
using CXY.CJS.Model;
using CXY.CJS.Tests.TestDatas;
using Xunit;

namespace CXY.CJS.Tests.Application.Role
{
    public class RoleServiceTest : IClassFixture<CJSTestBase>
    {
        private readonly IRoleService _service;
        public RoleServiceTest(CJSTestBase testBase)
        {
            _service = testBase.Ioc.Resolve<IRoleService>();
        }

        [Fact]
        public async Task ListRole_When_NotFound()
        {
            var result = await _service.ListRole(new ListRoleInput
            {
                PageIndex = 10,
                PageSize = 1000
            });
            Assert.Empty(result.Datas);


        }

        [Fact]
        public async Task ListRole_When_Found()
        {
            var result = await _service.ListRole(new ListRoleInput
            {
                PageIndex = 1,
                PageSize = 10,
                SortField = nameof(Model.Role.CreationTime),
                SortOrder = SortEnum.Desc
            });
            Assert.NotEmpty(result.Datas);


        }
    }
}