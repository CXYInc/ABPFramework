using System.Net;

namespace CXY.CJS.HttpClient
{
    public class HttpClientResponse<T> where T : class
    {
        /// <summary>
        /// 是否请求成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public HttpStatusCode Code { get; set; }

        /// <summary>
        /// 请求返回数据
        /// </summary>
        public T Data { get; set; }
    }
}
