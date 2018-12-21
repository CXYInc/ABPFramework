using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public class OrderDetail : Entity<string>
    {
        public string WebSiteId { get; set; }
        public int State { get; set; }
        public string Apply { get; set; }
        public string Applytime { get; set; }
        public string PassId { get; set; }

        /// <summary>
        /// 是否核销
        /// </summary>
        public int IsDestory { get; set; }
        public string DestoryRemark { get; set; }
        public string PassMemo { get; set; }
        public int ExportNum { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }

    }
}
