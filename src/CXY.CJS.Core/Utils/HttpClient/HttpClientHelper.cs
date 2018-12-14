using Abp.Extensions;
using Abp.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CXY.CJS.Core.HttpClient
{
    /// <summary>
    /// HttpClientHelper
    /// </summary>
    public class HttpClientHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpClientFactory"></param>
        public HttpClientHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #region Get异步请求
        public async Task<HttpClientResponse<string>> GetStringAsync(HttpClientRequest httpClientRequest)
        {
            HttpClientResponse<string> httpClientResponse;
            try
            {
                var httpResponseMessage = await GetAsync(httpClientRequest);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    httpClientResponse = new HttpClientResponse<string>
                    {
                        IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                        Code = httpResponseMessage.StatusCode
                    };

                    return httpClientResponse;
                }

                httpClientResponse = new HttpClientResponse<string>
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Code = httpResponseMessage.StatusCode,
                    Data = await httpResponseMessage.Content.ReadAsStringAsync()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return httpClientResponse;
        }

        public async Task<HttpClientResponse<T>> GetAsync<T>(HttpClientRequest httpClientRequest) where T : class
        {
            HttpClientResponse<T> httpClientResponse;
            try
            {
                var httpResponseMessage = await GetAsync(httpClientRequest);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    httpClientResponse = new HttpClientResponse<T>
                    {
                        IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                        Code = httpResponseMessage.StatusCode
                    };

                    return httpClientResponse;
                }

                httpClientResponse = new HttpClientResponse<T>
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Code = httpResponseMessage.StatusCode,
                    Data = (await httpResponseMessage.Content.ReadAsStringAsync()).FromJsonString<T>()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return httpClientResponse;
        }

        public async Task<HttpResponseMessage> GetAsync(HttpClientRequest httpClientRequest)
        {
            try
            {
                if (httpClientRequest == null) throw new ArgumentNullException("请求参数不能为空");

                if (httpClientRequest.Url.IsNullOrEmpty()) throw new ArgumentNullException("Url不能为空");

                var httpClient = _httpClientFactory.CreateClient();
                //设置请求头
                httpClient.DefaultRequestHeaders.Clear();
                if (httpClientRequest.Headers != null && httpClientRequest.Headers.Count > 0)
                {
                    foreach (var header in httpClientRequest.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                if (!httpClientRequest.ContentType.IsNullOrEmpty())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(httpClientRequest.ContentType));
                }

                return await httpClient.GetAsync(httpClientRequest.Url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Post异步请求
        /// <summary>
        ///  通过Post请求获取String返回值
        /// </summary>
        /// <param name="httpClientRequest"></param>
        /// <returns></returns>
        public async Task<HttpClientResponse<string>> PostStringAsync(HttpClientRequest httpClientRequest)
        {
            HttpClientResponse<string> httpClientResponse;
            try
            {
                var httpResponseMessage = await PostAsync(httpClientRequest);

                if (httpResponseMessage == null) return null;

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    httpClientResponse = new HttpClientResponse<string>
                    {
                        IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                        Code = httpResponseMessage.StatusCode
                    };

                    return httpClientResponse;
                }

                httpClientResponse = new HttpClientResponse<string>
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Code = httpResponseMessage.StatusCode,
                    Data = await httpResponseMessage.Content.ReadAsStringAsync()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return httpClientResponse;
        }

        /// <summary>
        /// 通过Post请求获取指定泛型返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClientRequest"></param>
        /// <returns></returns>
        public async Task<HttpClientResponse<T>> PostAsync<T>(HttpClientRequest httpClientRequest) where T : class
        {
            HttpClientResponse<T> httpClientResponse;
            try
            {
                var httpResponseMessage = await PostAsync(httpClientRequest);

                if (httpResponseMessage == null) return null;

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    httpClientResponse = new HttpClientResponse<T>
                    {
                        IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                        Code = httpResponseMessage.StatusCode
                    };

                    return httpClientResponse;
                }

                httpClientResponse = new HttpClientResponse<T>
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Code = httpResponseMessage.StatusCode,
                    Data = (await httpResponseMessage.Content.ReadAsStringAsync()).FromJsonString<T>()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return httpClientResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpClientRequest"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(HttpClientRequest httpClientRequest)
        {
            try
            {
                if (httpClientRequest == null) throw new ArgumentNullException("请求参数不能为空");

                if (httpClientRequest.Url.IsNullOrEmpty()) throw new ArgumentNullException("Url不能为空");

                var httpClient = _httpClientFactory.CreateClient();

                //设置请求头
                httpClient.DefaultRequestHeaders.Clear();
                if (httpClientRequest.Headers != null && httpClientRequest.Headers.Count > 0)
                {
                    foreach (var header in httpClientRequest.Headers)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
                var httpContent = new StringContent(httpClientRequest.PostData, httpClientRequest.DataEncoding);
                if (!httpClientRequest.ContentType.IsNullOrEmpty())
                {
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue(httpClientRequest.ContentType);
                }

                return await httpClient.PostAsync(httpClientRequest.Url, httpContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
