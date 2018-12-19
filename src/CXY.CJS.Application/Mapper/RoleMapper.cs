using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
{
    /// <summary>
    /// RoleMapper
    /// </summary>
    public class RoleMapper
    {
        internal class RoleProfile : Profile
        {
            public RoleProfile()
            {
                CreateMap<RoleEditInput, Role>();
            }
        }
    }
}
