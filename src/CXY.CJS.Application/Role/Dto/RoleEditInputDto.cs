using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Role.Dto
{
    public class RoleEditInputDto
    {
        [MaxLength(36, ErrorMessage = "Id超出最大长度")]
        [MinLength(32, ErrorMessage = "Id小于最小长度")]
        [Required(ErrorMessage = "Id不能为空")]
        public string Id { get; set; }
        /// <summary>
        /// 站点Id
        /// </summary>
        [Required]
        public string WebSiteId { get; set; }

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
