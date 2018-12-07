using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
{
    /// <summary>
    /// 配置Test的AutoMapper
    /// </summary>
    internal class TestMapperProfile : Profile
    {
        public TestMapperProfile()
        {
            CreateMap<TestDtoInput, Test>().ForMember(x => x.Id, map => map.MapFrom(x => x.UserId));
        }
    }
}
