using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Repository.SeedWork
{
    public class Pagination
    {

        /// <summary>
        /// 页码
        /// </summary>
      
        public int PageIndex { get; set; } = 1;


        /// <summary>
        /// 每页显示行数
        /// </summary>
      
        public int PageSize { get; set; } = 10;
    }

}