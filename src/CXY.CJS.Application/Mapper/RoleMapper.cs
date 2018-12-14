﻿using AutoMapper;
using CXY.CJS.Application.Dto;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Mapper
{
    public class RoleMapper
    {
        internal class RoleProfile : Profile
        {
            public RoleProfile()
            {
                CreateMap<RoleEditInputDto, Role>();
            }
        }
    }
}
