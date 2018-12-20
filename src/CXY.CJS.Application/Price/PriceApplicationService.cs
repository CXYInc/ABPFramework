using Abp.Json;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.HttpClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    public class PriceAppService : CJSAppServiceBase, IPriceAppService
    {
        private readonly ApiUrlConfig _apiConfig;
        private readonly HttpClientHelper _httpClientHelper;

        public PriceAppService(IOptionsSnapshot<ApiUrlConfig> apiConfig, HttpClientHelper httpClientHelper)
        {
            _apiConfig = apiConfig.Value;
            _httpClientHelper = httpClientHelper;
        }
        [HttpPost]
        public async Task<Hashtable> IndoorPrice(IndoorPriceInput inputDto)
        {
            if (inputDto == null) return null;

            Hashtable postData = new Hashtable();
            postData.Add("source", "PC");
            postData.Add("websiteid", AbpSession.WebSiteId);
            postData.Add("userid", AbpSession.UserId);
            postData.Add("hphm", inputDto.CarNumber);
            postData.Add("fdjh", inputDto.EngineNo);
            postData.Add("cjh", inputDto.CarCode);
            postData.Add("hpzl", inputDto.CarType);
            postData.Add("hpzlmc", inputDto.CarTypeName);
            postData.Add("excelbatchid", inputDto.BatchId);
            postData.Add("enumcarnature", inputDto.CarNature);
            postData.Add("provinceCode", "");
            postData.Add("recordlist", "");

            Hashtable req_data = new Hashtable();
            req_data.Add("req_data", postData);
            string postDataStr = req_data.ToJsonString();

            var apiUrl = _apiConfig.QueryNewPriceApiUrl;

            var httpClientRequest = new HttpClientRequest
            {
                DataEncoding = Encoding.GetEncoding("gb2312"),
                PostData = postDataStr,
                ContentType = "text/plain",
                Url = apiUrl
            };

            var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);

            return httpClientResponse.Data.FromJsonString<Hashtable>();
        }


        [HttpPost]
        public async Task<Hashtable> IndoorPriceBatch(IndoorPriceInput inputDto)
        {
            if (inputDto == null) return null;

            Hashtable postData = new Hashtable();
            postData.Add("WebSiteId", AbpSession.WebSiteId);
            postData.Add("UserId", inputDto.UserId);
            postData.Add("BatchAskPriceId", inputDto.BatchId); //批次号 
            postData.Add("CarId", inputDto.CarId);//批次车辆号 
            postData.Add("CarNumber", inputDto.CarNumber);
            postData.Add("EngineNo", inputDto.EngineNo);
            postData.Add("CarCode", inputDto.CarCode);
            postData.Add("CarType", inputDto.CarType);
            postData.Add("CarTypeName", inputDto.CarTypeName);
            postData.Add("CarNature", inputDto.CarNature);
            postData.Add("IsChoose", inputDto.IsChoose);
            postData.Add("Islock", inputDto.Islock);
            postData.Add("enumpricesource", "");//违章地OR车牌地 
            postData.Add("provinceCode", "");

            Hashtable req_data = new Hashtable();
            req_data.Add("req_data", postData);
            string postDataStr = req_data.ToJsonString();

            var apiUrl = _apiConfig.QueryNewPriceApiUrl;

            var httpClientRequest = new HttpClientRequest
            {
                DataEncoding = Encoding.GetEncoding("gb2312"),
                PostData = postDataStr,
                ContentType = "text/plain",
                Url = apiUrl
            };

            var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);

            return httpClientResponse.Data.FromJsonString<Hashtable>();
        }
    }
}
