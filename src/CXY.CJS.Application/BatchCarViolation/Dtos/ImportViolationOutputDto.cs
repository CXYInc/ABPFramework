using System.Collections.Generic;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 导入违章返回业务实体
    /// </summary>
    public class ImportViolationOutputDto
    {
        /// <summary>
        /// 违章数据列表
        /// </summary>
        public List<BatchTableModelDto> SuccessList { get; set; }

        /// <summary>
        /// 错误信息列表
        /// </summary>
        public List<ViolationErrorInfo> ErrorList { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FilePath { get; set; }
    }
}
