using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 用户基础定义
    /// </summary>
    public class Users : Entity<string>
    {
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 代理商户
        /// </summary>
        public string Shortname { get; set; }
        /// <summary>
        /// 代理商简称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        public string Password { get; set; }
        /// <summary>
        /// 默认密码
        /// </summary>
        public string Safepassword { get; set; }
        public string WebSiteId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 启用禁用
        /// </summary>
        public int? Ispanuse { get; set; }

        public string RecommendUserName { get; set; }

        public string RecommendUserid { get; set; }

        public string CardNo { get; set; }

        public int? Isdelete { get; set; }

     
        public string Address { get; set; }

        ///// <summary>
        ///// 弃用
        ///// </summary>
        //public string UserRoleId { get; set; }

        public int? UserProvince { get; set; }
        public int? UserCity { get; set; }
        public string PaymentPwd { get; set; }

        /// <summary>
        /// 是否开启支付密码
        /// </summary>
        public int? IsPaymentPwd { get; set; }


        public Users()
        {
            CreationTime = Clock.Now;
            IsActive = true;
        }
    }
}
