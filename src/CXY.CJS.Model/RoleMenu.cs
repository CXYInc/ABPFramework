using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class RoleMenu : Entity<string>, IHasCreationTime
    {
        public string RoleId { get; set; }
        public string MenuId { get; set; }
        public DateTime CreationTime { get; set; }
        public RoleMenu()
        {
            CreationTime = Clock.Now;
        }
    }
}
