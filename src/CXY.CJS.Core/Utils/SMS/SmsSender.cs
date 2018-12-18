using System;
using System.Text;
using System.Threading.Tasks;
using Abp.Json;
using CXY.CJS.Core.HttpClient;

namespace CXY.CJS.Core.Utils.SMS
{
    public interface ISmsSender
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="telephone"></param>
        /// <param name="pars"></param>
        /// <param name="templeteId"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        Task<SendSmsResult> SendSmsAsync(string telephone, string pars, string templeteId, string moduleName);
    }

    public class SmsSender : ISmsSender
    {
        private readonly HttpClientHelper _httpClientHelper;
        private readonly SmsSenderConfiguration _configuration;

        public SmsSender(HttpClientHelper httpClientHelper, SmsSenderConfiguration configuration)
        {
            _httpClientHelper = httpClientHelper;
            _configuration = configuration;
        }

        public async Task<SendSmsResult> SendSmsAsync(string telephone, string pars, string templeteId, string moduleName)
        {
            var httpClientRequest = new HttpClientRequest
            {
                DataEncoding = Encoding.GetEncoding("utf-8"),
                PostData = $"templeteId={templeteId}&phoneNumber={telephone}&moduleName={moduleName}&contentParameters={pars}",
                ContentType = "application/x-www-form-urlencoded",
                Url = _configuration.Url
            };
            var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);
            //todo:记日志
            return httpClientResponse.Data.FromJsonString<SendSmsResult>();
        }
    }
}