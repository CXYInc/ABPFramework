using Abp.Json;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Common;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 违章查询服务
    /// </summary>
    public class ViolationQueryService : CJSAppServiceBase, IViolationQueryService
    {
        private readonly ApiUrlConfig _apiConfig;
        private readonly HttpClientHelper _httpClientHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiConfig"></param>
        /// <param name="httpClientHelper"></param>
        public ViolationQueryService(IOptionsSnapshot<ApiUrlConfig> apiConfig, HttpClientHelper httpClientHelper)
        {
            _apiConfig = apiConfig.Value;
            _httpClientHelper = httpClientHelper;
        }

        /// <summary>
        /// 查询需要发动机位数
        /// </summary>
        /// <param name="carConditionInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ConditionInfo>> QueryCondition(CarConditionInputDto carConditionInput)
        {
            if (carConditionInput == null) return null;
            var ProvinceID = "0";
            var cpjc = carConditionInput.CarNumber.Substring(0, 2);

            #region 苏，苏在城市字典中查不到，因此特殊处理
            if (ProvinceID == "0")
            {
                // 获取车牌指定的数据源
                if (cpjc.Substring(0, 1) == "苏")
                {
                    ProvinceID = "32";
                }
                else
                {
                    var city = CommonHelper.FindByCarPrefix(cpjc);
                    if (city == null) return null;
                    ProvinceID = city.ProvinceID;
                }
            }
            #endregion
            #region 以下三个省所有车都支持，特殊处理
            string left2ProvinceID = ProvinceID.Substring(0, 2);
            string shortCpjc = cpjc.Substring(0, 1);
            /*
            44 广东 后6后6
            32 江苏 全部车架号
            31 上海 全部发动机
            51 四川 车架号后6位
            11 北京 全部发动机
            42 湖北 车架号后6位
             */
            if (left2ProvinceID == "44" ||
                left2ProvinceID == "32" ||
                left2ProvinceID == "31" ||
                //left2ProvinceID == "51" ||
                left2ProvinceID == "11"
                //|| left2ProvinceID == "42"
                )
            {
                int CarCodeLen = 0;
                int CarEngineLen = 0;
                if (left2ProvinceID == "44")
                {
                    CarCodeLen = 6;
                    CarEngineLen = 6;//发动机号，本来广东只要求4位，但是非好易的渠道都要求6位，因此这里改成要求6位即可
                }
                else if (left2ProvinceID == "32")
                {
                    //CarCodeLen = 99;
                    //CarEngineLen = 0;
                    CarCodeLen = 0;
                    CarEngineLen = 6;
                }
                else if (left2ProvinceID == "31")
                {
                    CarCodeLen = 0;
                    CarEngineLen = 99;
                }
                //else if (left2ProvinceID == "51")
                //{
                //    CarCodeLen = 6;
                //    CarEngineLen = 0;
                //}
                else if (left2ProvinceID == "11")
                {
                    CarCodeLen = 0;
                    CarEngineLen = 99;
                }
                //else if (left2ProvinceID == "42")
                //{
                //    CarCodeLen = 5;
                //    CarEngineLen = 0;
                //}

                var result = new ConditionInfo { CarCodeLen = CarCodeLen, CarEngineLen = CarEngineLen };

                return new ApiResult<ConditionInfo>().Success(result);
            }
            #endregion
            #region 以下四个按省处理，不按市
            var cpjcTemp = cpjc.Substring(0, 1);
            var cityTemps = new List<string> { "京", "沪", "津", "渝" };

            if (cityTemps.Contains(cpjcTemp))
            {
                cpjc = cpjcTemp;
            }
            #endregion

            dynamic postData = new ExpandoObject();
            postData.req_data = new ExpandoObject();
            postData.req_data.cpjc = cpjc;
            postData.req_data.left2ProvinceID = ProvinceID;
            postData.req_data.scource = "PC";

            var postDataStr = postData.ToJsonString();

            var apiUrl = _apiConfig.QueryInputConditionApiUrl;

            var httpClientRequest = new HttpClientRequest
            {
                DataEncoding = Encoding.GetEncoding("gb2312"),
                PostData = postDataStr,
                ContentType = "text/plain",
                Url = apiUrl
            };

            var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);

            var conditionApiResult = httpClientResponse.Data.FromJsonString<ConditionApiResult>();

            return new ApiResult<ConditionInfo>().Success(conditionApiResult.Data);
        }

        /// <summary>
        /// 查询发动机号和车架号
        /// </summary>
        /// <param name="carCodeInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<Hashtable>> QueryCarCodeAndEngineCode(QueryCarCodeInputDto carCodeInfo)
        {
            Hashtable result = new Hashtable();

            try
            {
                string fdjhLen = carCodeInfo.CarEngineLen.ToString();
                string cjhLen = carCodeInfo.CarCodeLen.ToString();
                string cp = carCodeInfo.CarNumber.ToLower();
                string cx = carCodeInfo.CarType.PadLeft(2, '0');
                switch (cx)
                {
                    case "C":
                        cx = "02";
                        break;
                    case "A1":
                        cx = "01";
                        break;
                    case "A2":
                        cx = "01";
                        break;
                    case "B1":
                        cx = "01";
                        break;
                    case "B2":
                        cx = "01";
                        break;
                    case "E":
                        cx = "07";
                        break;
                    default:
                        cx = "02";
                        break;
                }

                string engineNo = "", carCode = "", carType = "", carTypeName = "", seating = "", privateCar = "", initialRegDate = "",
                    ExaminedType = "OnlineExamined", isbaseOk = "0";

                if (cp.Length >= 7)//去接口查
                {
                    var postData = new Hashtable
                    {
                        { "enginenolen", fdjhLen },
                        { "userid", AbpSession.UserId },
                        { "carcodelen", cjhLen },
                        { "carType", cx },
                        { "carnumber", cp },
                        { "source", "PC" },
                        { "shortname", AbpSession.UserName },
                        { "websiteid", AbpSession.WebSiteId }
                    };

                    var req_data = new Hashtable
                    {
                        { "req_data", postData }
                    };
                    string postDataStr = req_data.ToJsonString();

                    var apiUrl = _apiConfig.QueryCarCodeAndEngineCodeApiUrl;

                    var httpClientRequest = new HttpClientRequest
                    {
                        DataEncoding = Encoding.GetEncoding("gb2312"),
                        PostData = postDataStr,
                        ContentType = "text/plain",
                        Url = apiUrl
                    };

                    var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);

                    var hst = httpClientResponse.Data.FromJsonString<Hashtable>();

                    if (hst != null && hst.ContainsKey("code") && hst["code"].ToString() == "1" && hst.ContainsKey("data"))
                    {
                        var resultData = hst["data"].ToString().FromJsonString<Hashtable>();
                        if (resultData != null && resultData.ContainsKey("carCode") && resultData.ContainsKey("engineNo") && resultData.ContainsKey("carType") && resultData.ContainsKey("carTypeName"))
                        {
                            engineNo = resultData["engineNo"].ToString();
                            carCode = resultData["carCode"].ToString();
                            carType = resultData["carType"].ToString();
                            carTypeName = resultData["carTypeName"].ToString();
                        }
                    }
                }

                result["code"] = 1;
                result["message"] = "查询成功！";
                result["FDJH"] = engineNo;
                result["CJH"] = carCode;
                result["HPZLMC"] = carTypeName;
                result["HPZL"] = carType;
                result["privateCar"] = privateCar;
                result["seating"] = seating;
                result["initialRegDate"] = initialRegDate;
                result["ExaminedType"] = ExaminedType;
                result["isbaseOk"] = isbaseOk;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return new ApiResult<Hashtable>().Success(result);
        }

        /// <summary>
        ///  违章查询
        /// </summary>
        /// <param name="inputDto">违章查询输入参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Hashtable> QueryViolation(QueryViolationInputDto inputDto)
        {
            if (inputDto == null) return null;

            Hashtable postData = new Hashtable();
            postData.Add("carNumber", inputDto.CarNumber);
            postData.Add("carCode", inputDto.CarCode);
            postData.Add("engineCode", inputDto.CarEngine);
            postData.Add("carType", inputDto.CarType.ToString().PadLeft(2, '0'));
            postData.Add("carTypeName", inputDto.CarTypeName);
            postData.Add("isUseHistory", inputDto.IsUseHistory.ToString());
            postData.Add("userId", "009020201501192329000000000002");
            postData.Add("provinceCode", inputDto.ProvinceCode);
            postData.Add("enumCarNature", (int)inputDto.EnumCarNature);
            postData.Add("shortname", "有限公司");
            postData.Add("isCheckLock", inputDto.IsCheckLock.ToString());
            postData.Add("source", "PC");

            Hashtable req_data = new Hashtable();
            req_data.Add("req_data", postData);
            string postDataStr = req_data.ToJsonString();

            var apiUrl = _apiConfig.QueryViolationApiUrl;

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
