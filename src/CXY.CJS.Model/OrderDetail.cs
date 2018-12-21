using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public class OrderDetail : Entity<string>
    {
        public string WebSiteId { get; set; }

      

    }
}
