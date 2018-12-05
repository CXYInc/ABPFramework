
using Abp.Runtime.Validation;
using CXY.CJS.Dtos;
using CXY.CJS.Model;

namespace CXY.CJS.Model.Dtos
{
    public class GetWebSitesInput : PagedSortedAndFilteredInputDto, IShouldNormalize
    {

        /// <summary>
        /// 正常化排序使用
        /// </summary>
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "Id";
            }
        }

    }
}
