using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 批次发票列表
    /// </summary>
    public class BatchInvoice : Entity<string>, IHasCreationTime, IHasModificationTime
    {

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// 发票金额
        /// </summary>
        public decimal InvoiceValue { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNumber { get; set; }

        public string CreatorUserId { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }


        public BatchInvoice()
        {
            CreationTime = Clock.Now;
        }
    }
}
