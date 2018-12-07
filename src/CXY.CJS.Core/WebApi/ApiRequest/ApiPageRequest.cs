namespace CXY.CJS.WebApi
{
    /// <summary>
    /// Api分页请求实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiPageRequest<T> where T : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public T Data { get; set; }
    }
}
