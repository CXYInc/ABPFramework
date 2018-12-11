using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Application.Dtos
{
    public class ListWebSiteInput : Pagination
    {
            
        public string Key { get; set; }

        /// <summary>
        /// 是否隐藏过期站点
        /// </summary>
        public bool IsHide { get; set; }
    }
}