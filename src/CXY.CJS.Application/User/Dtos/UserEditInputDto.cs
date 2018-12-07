using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 用户业务编辑实体
    /// </summary>
    public class UserEditInputDto
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
        /// 用户密码
        /// </summary>
        public string Password { get; set; }

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
    }
}
