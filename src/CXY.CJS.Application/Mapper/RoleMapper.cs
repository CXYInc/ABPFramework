using AutoMapper;
using CXY.CJS.Role.Dto;


namespace CXY.CJS.Application.Mapper
{
    class RoleMapper
    {
        internal class RoleProfile : Profile
        {
            public RoleProfile()
            {
                CreateMap<RoleEditInputDto, Model.Role>();
            }
        }
    }
}
