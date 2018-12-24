using Abp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public class OrderFlow : Entity<string>
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OccurrenceTime { get; set; }
        public int Event { get; set; }
        public string Detail { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
