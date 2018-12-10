﻿namespace CXY.CJS.WebApi
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
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    public class ApiResult : ApiResultBase<object>
    {
        public ApiResult Success(object data = null, string message = "")
        {
            Code = 1;
            Data = data;
            return this;
        }

        public ApiResult Error(string message = "")
        {
            Code = 0;
            Message = message;
            return this;
        }
    }

    /// <summary>
    /// api响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T> : ApiResultBase<T> where T : class
    {
        public ApiResult<T> Success(T data = null, string message = "")
        {
            Code = 1;
            Data = data;
            return this;
        }

        public ApiResult<T> Error(string message = "")
        {
            Code = 0;
            Message = message;
            return this;
        }
    }
}
