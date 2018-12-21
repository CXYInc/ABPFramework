using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CXY.CJS.Application.Dtos
{
    public class UserWalletListDto
    {
        [MaxLength(12, ErrorMessage = "WebSiteId超出最大长度")]
        [MinLength(5, ErrorMessage = "WebSiteId小于最小长度")]
        [Required(ErrorMessage = "WebSiteId不能为空")]
        public string WebSiteId { get; set; }

        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public decimal Overdrftamount { get; set; }


        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string DeleterUserId { get; set; }
    }
}
