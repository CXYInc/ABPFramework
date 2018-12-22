using Newtonsoft.Json;

namespace CXY.CJS.Core.WebApi
{
    /// <summary>
    /// api响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResultBase<T> where T : class
    {
        /// <summary>
        /// Api响应码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Api响应数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Api响应消息
        /// </summary>
        public string Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Code > 0;
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    public class ApiResult : ApiResultBase<object>
    {
        /// <summary>
        /// 格式化成功数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResult Success(object data = null, string message = "")
        {
            Code = 1;
            Data = data;
            return this;
        }

        public static ApiResult<T> Success<T>(T data, string message = "") where T : class
        {
            return new ApiResult<T>
            {
                Code = 1,
                Data = data,
                Message = message
            };
        }

        /// <summary>
        /// 格式化失败数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResult Error(string message = "")
        {
            Code = 0;
            Message = message;
            return this;
        }


        public static ApiResult ValidationError(string message = "参数有误")
        {
            return new ApiResult
            {
                Code = 0,
                Message = message
            };
        }

        public static ApiResult<T> ValidationError<T>(string message = "参数有误") where T : class
        {
            return new ApiResult<T>
            {
                Code = 0,
                Message = message
            };
        }

        public static ApiResult DataNotFound()
        {
            return new ApiResult
            {
                Code = 0,
                Message = "未找到数据"
            };
        }

        public static ApiResult<T> DataNotFound<T>() where T : class
        {
            return new ApiResult<T>
            {
                Code = 0,
                Message = "未找到数据"
            };
        }


        public static ApiResult<T> Error<T>(string message = "") where T : class
        {
            return new ApiResult<T>
            {
                Code = 0,
                Message = message
            };
        }
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResultBase<T> where T : class
    {
        /// <summary>
        /// 格式化成功数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResult<T> Success(T data = null, string message = "")
        {
            Code = 1;
            Data = data;
            return this;
        }

        /// <summary>
        /// 格式化失败数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ApiResult<T> Error(string message = "")
        {
            Code = 0;
            Message = message;
            return this;
        }
    }
}
