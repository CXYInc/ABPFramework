using AutoMapper;
using CXY.CJS.Application.Dtos;

namespace CXY.CJS.Model.Mapper
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
