using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 违章导入业务实体
    /// </summary>
    public class ImportViolationInputDto
    {
        /// <summary>
        /// 批次ID
        /// </summary>
        [Required(ErrorMessage = "批次号不能为空")]
        public string BatchId { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        [Required(ErrorMessage = "违章文件不能为空")]
        public IFormFile File { get; set; }
    }
}
