using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
{
    class UserMapper
    {
        internal class UserProfile : Profile
        {
            public UserProfile()
            {
                CreateMap<UserEditInputDto, Users>();
                CreateMap<Users, UserOutDto>();
            }
        }
    }
}
