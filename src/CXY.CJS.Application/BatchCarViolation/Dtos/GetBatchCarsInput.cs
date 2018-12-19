
using Abp.Runtime.Validation;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Dtos
{
    public class GetBatchCarsInput : PagedSortedAndFilteredInputDto, IShouldNormalize
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
