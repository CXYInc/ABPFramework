using System;
using Abp.AutoMapper;
using Newtonsoft.Json;

namespace CXY.CJS.Menu.Dto
{
    [AutoMapTo(typeof(Model.Menu))]
    public class SaveMenuInput
    {

        [JsonProperty("Parentid")]

        public string ParentId { get; set; }

        [JsonProperty("Sitename")]
        public string MenuName { get; set; }

        [JsonProperty("Siteleval")]
        public int MenuLeval { get; set; }

        [JsonProperty("Siteurl")]
        public string MenuUrl { get; set; }

        [JsonProperty("Sitelayer")]
        public int MenuLayer { get; set; }

        /// <summary>
        /// 是否系统菜单
        /// </summary>
        [JsonProperty("Issys")]
        public bool IsSys { get; set; }

        /// <summary>
        /// 是否为外部链接
        /// </summary>
        [JsonProperty("Isout")]
        public bool IsOut { get; set; }

        /// <summary>
        /// 是否拥有下级菜单
        /// </summary>
        [JsonProperty("Isparent")]
        public bool IsParent { get; set; }


        /// <summary>
        /// 目标框架
        /// </summary>
        [JsonProperty("Sitetarget")]
        public string TargetFrame { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }

        [JsonProperty("Uptime")]
        public DateTime LastModificationTime { get; set; }
    }

    public class UpdateMenuInput 
    {
        [JsonProperty("Siteid")]
        public string Id { get; set; }

        [JsonProperty("Sitename")]
        public string MenuName { get; set; }

        [JsonProperty("Siteleval")]
        public int MenuLeval { get; set; }

        [JsonProperty("Siteurl")]
        public string MenuUrl { get; set; }

        [JsonProperty("Sitelayer")]
        public int MenuLayer { get; set; }

        /// <summary>
        /// 是否系统菜单
        /// </summary>
        [JsonProperty("Issys")]
        public bool IsSys { get; set; }

        /// <summary>
        /// 是否为外部链接
        /// </summary>
        [JsonProperty("Isout")]
        public bool IsOut { get; set; }

        /// <summary>
        /// 是否拥有下级菜单
        /// </summary>
        [JsonProperty("Isparent")]
        public bool IsParent { get; set; }

        /// <summary>
        /// 目标框架
        /// </summary>
        [JsonProperty("Sitetarget")]
        public string TargetFrame { get; set; }

        /// <summary>
        /// 权重
        /// </summary>
        public int Weight { get; set; }

        [JsonProperty("Uptime")]
        public DateTime LastModificationTime { get; set; }
    }
}