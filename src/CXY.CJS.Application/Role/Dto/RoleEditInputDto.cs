using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    public class RoleEditInput
    {
        public string Id { get; set; }
        ///// <summary>
        ///// 站点Id
        ///// </summary>
        //[Required]
        //public string WebSiteId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
    }
}
