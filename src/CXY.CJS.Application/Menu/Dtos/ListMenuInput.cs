﻿using CXY.CJS.Core.Enums;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.Core.WebApi;

namespace CXY.CJS.Application.Dtos
{
    public class ListMenuInput : Pagination
    {
        public string SortField { get; set; }

    }
}