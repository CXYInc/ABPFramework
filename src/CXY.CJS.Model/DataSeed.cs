using System;
using System.Collections.Generic;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    public  class DataSeed:Entity<string>
    {
        public int SeedIndex { get; set; }
    }
}
