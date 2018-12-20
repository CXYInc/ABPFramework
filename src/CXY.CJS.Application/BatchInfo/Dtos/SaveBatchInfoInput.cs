using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace CXY.CJS.Application.Dtos
{
    
    public class SaveBatchInfoInput
    {
        /// <summary>
        /// 批次号
        /// </summary>
        [Required]
        public string Id { get; set; }

        [Required]
        public string ProxyUserId { get; set; }

        [Required]
        public string Proxy { get; set; }

        public string Remark { get; set; }
    }
}