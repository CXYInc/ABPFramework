using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Repository.SeedWork
{
    public class Pagination
    {

        /// <summary>
        /// 页码
        /// </summary>
        [MinLength(1, ErrorMessage = "页码最小为1")]
        public int PageIndex { get; set; } = 1;


        /// <summary>
        /// 每页显示行数
        /// </summary>
        [MaxLength(10000, ErrorMessage = "行数最大为10000")]
        [MinLength(1, ErrorMessage = "行数最小为1")]
        public int PageSize { get; set; } = 10;
    }

}