using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.AutoMapper;
using CXY.CJS.Core.Enums;
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

            CreateMap<Test, TestOutDto>().ForMember(x => x.UserType, opt => opt.ResolveUsing<EnumValueResolver<SortEnum, int>, int>(x => int.Parse(x.Name)));
        }
    }
}
