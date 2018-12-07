namespace CXY.CJS.WebApi
{
    /// <summary>
    /// api响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultBase<T> where T : class
    {
        public int Code { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    public class ApiResult : ApiResultBase<object>
    {
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResultBase<T> where T : class
    {

    }
}
