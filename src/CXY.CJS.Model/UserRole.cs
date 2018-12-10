using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class UserRole : Entity<string>, IHasCreationTime
    {
        public string WebSiteId { get; set; }
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public UserRole()
        {
            CreationTime = Clock.Now;
        }

    }
}
