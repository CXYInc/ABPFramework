﻿using Abp.Domain.Entities;
using Abp.Timing;
using System;

namespace CXY.CJS.Model
{
    public class WebSite : Entity<string>
    {
        public string WebSiteName { get; set; }

        public string ConnectionString { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

        public WebSite()
        {
            CreationTime = Clock.Now;
        }

    }
}
