using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapFrom(typeof(Model.Users))]
    public class UserOutDto: Model.Users
    {
    }
}
