﻿using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 用户钱包(余额)
    /// </summary>
    public class UserWallet : Entity<string>
    {
        public string WebSiteId { get; set; }

        public string UserId { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 透支额度
        /// </summary>
        public decimal OverdrftAmount { get; set; }


        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeleterUserId { get; set; }

        public UserWallet()
        {
            CreationTime = Clock.Now;
            IsDeleted = false;
        }
    }
}