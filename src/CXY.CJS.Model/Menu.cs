using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class Menu : Entity<string>, IHasCreationTime, IHasDeletionTime, ISoftDelete
    {
        public string ParentId { get; set; }
        public string MenuName { get; set; }

        public int MenuLeval { get; set; }
        public string MenuUrl { get; set; }

        public int MenuLayer { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime CreationTime { get; set; }

        public Menu()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
        }
    }
}
