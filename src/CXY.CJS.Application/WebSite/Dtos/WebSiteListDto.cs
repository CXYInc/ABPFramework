
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    public class WebSiteListDto : EntityDto<int>
    {


        /// <summary>
        /// WebSiteId
        /// </summary>
        [MaxLength(6, ErrorMessage = "WebSiteId超出最大长度")]
        [MinLength(6, ErrorMessage = "WebSiteId小于最小长度")]
        [Required(ErrorMessage = "WebSiteId不能为空")]
        public string WebSiteId { get; set; }



        /// <summary>
        /// WebSiteName
        /// </summary>
        [MaxLength(50, ErrorMessage = "WebSiteName超出最大长度")]
        [MinLength(1, ErrorMessage = "WebSiteName小于最小长度")]
        [Required(ErrorMessage = "WebSiteName不能为空")]
        public string WebSiteName { get; set; }



        /// <summary>
        /// ConnectionString
        /// </summary>
        public string ConnectionString { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// CreatorUserId
        /// </summary>
        public long? CreatorUserId { get; set; }



        /// <summary>
        /// LastModifierUserId
        /// </summary>
        public long? LastModifierUserId { get; set; }



        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime? LastModificationTime { get; set; }



        /// <summary>
        /// DeleterUserId
        /// </summary>
        public long? DeleterUserId { get; set; }



        /// <summary>
        /// DeletionTime
        /// </summary>
        public DateTime? DeletionTime { get; set; }



        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }




    }
}