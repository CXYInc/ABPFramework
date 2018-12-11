﻿using CXY.CJS.Enum;
using CXY.CJS.Repository.SeedWork;
using CXY.CJS.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CXY.CJS.Role.Dto
{
    public class ListRoleInput : Pagination, IHasSort
    {
        public string SortField { get; set; }
       
        public SortEnum SortOrder { get; set; }
    }
}