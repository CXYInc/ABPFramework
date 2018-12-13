using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Core.HttpClient
{
    public class HttpClientRequest
    {
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 数据编码类型
        /// </summary>
        public Encoding DataEncoding { get; set; }


        /// <summary>
        /// 请求头集合
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
    }
}
