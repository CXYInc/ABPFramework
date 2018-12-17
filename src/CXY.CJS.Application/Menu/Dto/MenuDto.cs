using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapFrom(typeof(Model.Menu))]
    public class MenuDto : Model.Menu
    {
    }
}
