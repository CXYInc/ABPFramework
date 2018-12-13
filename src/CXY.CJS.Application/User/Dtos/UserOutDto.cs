using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UserOutDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 站点ID
        /// </summary>
        public string WebSiteId { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public int UserType { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 最后编辑人ID
        /// </summary>
        public long? LastModifierUserId { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// 删除人ID
        /// </summary>
        public long? DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeletionTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
