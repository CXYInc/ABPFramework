using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Models
{
    public class Test : IEntity<string>
    {
        public string Id { get; set; }

        public bool IsTransient()
        {
            return true;
        }
    }
}
