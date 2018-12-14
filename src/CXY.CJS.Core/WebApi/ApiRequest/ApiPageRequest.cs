namespace CXY.CJS.Core.WebApi
{
    /// <summary>
    /// Api分页请求实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageRequest<T> where T : class
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }
}
