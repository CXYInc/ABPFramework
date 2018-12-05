

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CXY.CJS.Model;

namespace CXY.CJS.Model.Dtos
{
    public class CreateOrUpdateWebSiteInput
    {
        [Required]
        public WebSiteEditDto WebSite { get; set; }

    }
}