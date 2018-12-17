using AutoMapper;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Tests.Mapper
{
    public class MenuMapper
    {
        internal class MenuProfile : Profile
        {
            public MenuProfile()
            {
                CreateMap<Model.Menu, UpdateMenuInput>();
                CreateMap<UpdateMenuInput, Model.Menu>();
            }
        }
    }
}