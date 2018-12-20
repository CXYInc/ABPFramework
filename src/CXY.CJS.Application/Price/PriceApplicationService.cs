using Abp.Domain.Repositories;
using Abp.Json;
using Abp.Runtime.Caching;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
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
        private readonly IRepository<BatchCar, string> _batchCarRepository;
        private readonly IRepository<BatchInfo, string> _batchInfoRepository;
        private readonly ICacheManager _cacheManager;

        public PriceAppService(IOptionsSnapshot<ApiUrlConfig> apiConfig, HttpClientHelper httpClientHelper, ICacheManager cacheManager, IRepository<BatchCar, string> batchCarRepository, IRepository<BatchInfo, string> batchInfoRepository)
        {
            _apiConfig = apiConfig.Value;
            _httpClientHelper = httpClientHelper;
            _batchCarRepository = batchCarRepository;
            _batchInfoRepository = batchInfoRepository;
            _cacheManager = cacheManager;
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
        public async Task<ApiResult<List<PriceResultOutput>>> IndoorPriceBatch(IndoorPriceInput inputDto)
        {
            ApiResult<List<PriceResultOutput>> apiResult = new ApiResult<List<PriceResultOutput>>();
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

            var priceList = httpClientResponse.Data.FromJsonString<List<PriceResultOutput>>();
            return apiResult.Success(priceList);
        }



        public async Task<ApiResult> QuotePrice(PriceInput input)
        {
            ApiResult apiResult = new ApiResult();
            Hashtable hst = new Hashtable();
            if (!Limit(hst, input.BatchId, "QueryViolation_Agent", out string globalKey))
            {
                return apiResult.Error();
            }
            var batchInfo = await _batchInfoRepository.GetAsync(input.BatchId);
            var carList = await _batchCarRepository.GetAllListAsync(o => o.WebSiteId == AbpSession.WebSiteId && o.BatchId == input.BatchId);

            Hashtable data = new Hashtable
            {
                ["id"] = input.BatchId,
                ["allCount"] = carList.Count,
                ["completeCount"] = 0,
                ["SearchSuccessCount"] = 0,
                ["searchMessage"] = null,
                ["successCount"] = 0
            };
            await _cacheManager.GetCache(globalKey).SetAsync(globalKey, data);



            return apiResult.Success();
        }

        private bool Limit(Hashtable hst, string batchid, string business, out string globalKey)
        {
            globalKey = batchid + "." + business;

            Hashtable data1 = _cacheManager.GetCache(globalKey) as Hashtable;
            if (!object.Equals(data1, null))
            {
                hst["code"] = 2;
                hst["message"] = "你上次的查询的任务还未执行完,请等任务执行完再开始下一次任务！";
                hst["errormsg"] = "你上次的查询的任务还未执行完,请等任务执行完再开始下一次任务！";

                hst["data"] = data1;
                return false;
            }
            return true;
        }
    }
}
