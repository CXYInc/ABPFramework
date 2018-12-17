using Abp.Json;
using CXY.CJS.Application.Dtos;
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
using System.Net.Http;
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
                    var city = FindByCarPrefix(cpjc);
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

            var postDataStr = JsonConvert.SerializeObject(postData);

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
                    Hashtable postData = new Hashtable();
                    postData.Add("enginenolen", fdjhLen);
                    postData.Add("userid", "009020201501192329000000000002");
                    postData.Add("carcodelen", cjhLen);
                    postData.Add("carType", cx);
                    postData.Add("carnumber", cp);
                    postData.Add("source", "PC");
                    postData.Add("shortname", "有限公司");
                    postData.Add("websiteid", "009020");


                    Hashtable req_data = new Hashtable();
                    req_data.Add("req_data", postData);
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

            var apiUrl =_apiConfig.QueryViolationApiUrl;

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

        #region 
        private static List<CityCode> _ChinaCitys = null;

        private static readonly object padlock = new object();

        private static List<CityCode> ChinaCityQuery
        {
            get
            {
                if (_ChinaCitys == null || _ChinaCitys.Count == 0)
                {
                    lock (padlock)
                    {
                        if (_ChinaCitys == null || _ChinaCitys.Count == 0)
                        {
                            #region 实例化
                            _ChinaCitys = new List<CityCode>();
                            _ChinaCitys.Add(new CityCode() { ID = "11", AreaCode = "010", CarPrefix = "京", EName = "BeiJing", Name = "北京", ProvinceID = "11", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "12", AreaCode = "022", CarPrefix = "津", EName = "TianJin", Name = "天津", ProvinceID = "12", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "31", AreaCode = "021", CarPrefix = "沪", EName = "ShangHai", Name = "上海", ProvinceID = "31", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "50", AreaCode = "0811", CarPrefix = "渝", EName = "ZhongQing", Name = "重庆", ProvinceID = "50", Rank = "0", Status = "-1" });

                            _ChinaCitys.Add(new CityCode() { ID = "13", AreaCode = "", CarPrefix = "冀", EName = "", Name = "河北", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "14", AreaCode = "", CarPrefix = "晋", EName = "", Name = "山西", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "15", AreaCode = "", CarPrefix = "蒙", EName = "", Name = "内蒙古", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "21", AreaCode = "", CarPrefix = "辽", EName = "", Name = "辽宁", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "22", AreaCode = "", CarPrefix = "吉", EName = "", Name = "吉林", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "23", AreaCode = "", CarPrefix = "黑", EName = "", Name = "黑龙江", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "32", AreaCode = "", CarPrefix = "苏", EName = "", Name = "江苏", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "33", AreaCode = "", CarPrefix = "浙", EName = "", Name = "浙江", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "34", AreaCode = "", CarPrefix = "皖", EName = "", Name = "安徽", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "35", AreaCode = "", CarPrefix = "闽", EName = "", Name = "福建", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "36", AreaCode = "", CarPrefix = "赣", EName = "", Name = "江西", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "37", AreaCode = "", CarPrefix = "鲁", EName = "", Name = "山东", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "41", AreaCode = "", CarPrefix = "豫", EName = "", Name = "河南", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "42", AreaCode = "", CarPrefix = "鄂", EName = "", Name = "湖北", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "43", AreaCode = "", CarPrefix = "湘", EName = "", Name = "湖南", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "44", AreaCode = "", CarPrefix = "粤", EName = "", Name = "广东", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "45", AreaCode = "", CarPrefix = "桂", EName = "", Name = "广西", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "46", AreaCode = "", CarPrefix = "琼", EName = "", Name = "海南", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "51", AreaCode = "", CarPrefix = "川", EName = "", Name = "四川", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "52", AreaCode = "", CarPrefix = "贵", EName = "", Name = "贵州", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "53", AreaCode = "", CarPrefix = "云", EName = "", Name = "云南", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "54", AreaCode = "", CarPrefix = "藏", EName = "", Name = "西藏", ProvinceID = "54", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "61", AreaCode = "", CarPrefix = "陕", EName = "", Name = "陕西", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "62", AreaCode = "", CarPrefix = "甘", EName = "", Name = "甘肃", ProvinceID = "62", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "63", AreaCode = "", CarPrefix = "青", EName = "", Name = "青海", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "64", AreaCode = "", CarPrefix = "宁", EName = "", Name = "宁夏", ProvinceID = "64", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "65", AreaCode = "", CarPrefix = "新", EName = "", Name = "新疆", ProvinceID = "65", Rank = "0", Status = "1" });

                            _ChinaCitys.Add(new CityCode() { ID = "1301", AreaCode = "0311", CarPrefix = "冀A", EName = "ShiJiaZhuang", Name = "石家庄", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1302", AreaCode = "0315", CarPrefix = "冀B", EName = "TangShan", Name = "唐山", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1303", AreaCode = "0335", CarPrefix = "冀C", EName = "QinHuangDao", Name = "秦皇岛", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1304", AreaCode = "0310", CarPrefix = "冀D", EName = "HanDan", Name = "邯郸", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1305", AreaCode = "0319", CarPrefix = "冀E", EName = "XingTai", Name = "邢台", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1306", AreaCode = "0312", CarPrefix = "冀F", EName = "BaoDing", Name = "保定", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1307", AreaCode = "0313", CarPrefix = "冀G", EName = "ZhangJiaKou", Name = "张家口", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1308", AreaCode = "0314", CarPrefix = "冀H", EName = "ChengDe", Name = "承德", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1309", AreaCode = "0317", CarPrefix = "冀J", EName = "CangZhou", Name = "沧州", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1310", AreaCode = "0316", CarPrefix = "冀R", EName = "LangFang", Name = "廊坊", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1311", AreaCode = "0318", CarPrefix = "冀T", EName = "HengShui", Name = "衡水", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1312", AreaCode = "0317", CarPrefix = "冀S", EName = "CangZhou", Name = "沧州行署", ProvinceID = "13", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1401", AreaCode = "0351", CarPrefix = "晋A", EName = "TaiYuan", Name = "太原", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1402", AreaCode = "0352", CarPrefix = "晋B", EName = "DaTong", Name = "大同", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1403", AreaCode = "0353", CarPrefix = "晋C", EName = "YangQuan", Name = "阳泉", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1404", AreaCode = "0355", CarPrefix = "晋D", EName = "ChangZhi", Name = "长治", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1405", AreaCode = "0356", CarPrefix = "晋E", EName = "JinCheng", Name = "晋城", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1406", AreaCode = "0349", CarPrefix = "晋F", EName = "ShuoZhou", Name = "朔州", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1407", AreaCode = "0354", CarPrefix = "晋K", EName = "JinZhong", Name = "晋中", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1408", AreaCode = "0359", CarPrefix = "晋M", EName = "YunCheng", Name = "运城", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1409", AreaCode = "0350", CarPrefix = "晋H", EName = "XinZhou", Name = "忻州", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1410", AreaCode = "0357", CarPrefix = "晋L", EName = "LinFen", Name = "临汾", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1411", AreaCode = "0358", CarPrefix = "晋J", EName = "LvLiang", Name = "吕梁", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1423", AreaCode = "0352", CarPrefix = "晋G", EName = "EomBei", Name = "雁北", ProvinceID = "14", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1501", AreaCode = "0471", CarPrefix = "蒙A", EName = "HuHeHaoTe", Name = "呼和浩特", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1502", AreaCode = "0472", CarPrefix = "蒙B", EName = "BaoTou", Name = "包头", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1503", AreaCode = "0473", CarPrefix = "蒙C", EName = "WuHai", Name = "乌海", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1504", AreaCode = "0476", CarPrefix = "蒙D", EName = "ChiFeng", Name = "赤峰", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1505", AreaCode = "0475", CarPrefix = "蒙G", EName = "TongLiao", Name = "通辽", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1506", AreaCode = "0477", CarPrefix = "蒙K", EName = "EErDunSi", Name = "鄂尔多斯", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1507", AreaCode = "0470", CarPrefix = "蒙E", EName = "HuLunBeiErMeng", Name = "呼伦贝尔盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1508", AreaCode = "0478", CarPrefix = "蒙L", EName = "BaEomNaoErMeng", Name = "巴彦淖尔盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1509", AreaCode = "0474", CarPrefix = "蒙J", EName = "WuLanChaBuMeng", Name = "乌兰察布盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1522", AreaCode = "0482", CarPrefix = "蒙F", EName = "XinAnMeng", Name = "兴安盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1525", AreaCode = "0479", CarPrefix = "蒙H", EName = "EiLinGuoLeMeng", Name = "锡林郭勒盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "1529", AreaCode = "0483", CarPrefix = "蒙M", EName = "ALaShanMeng", Name = "阿拉善盟", ProvinceID = "15", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2101", AreaCode = "024", CarPrefix = "辽A", EName = "ShenYang", Name = "沈阳", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2102", AreaCode = "0411", CarPrefix = "辽B", EName = "DaLian", Name = "大连", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2103", AreaCode = "0412", CarPrefix = "辽C", EName = "AnShan", Name = "鞍山", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2104", AreaCode = "0413", CarPrefix = "辽D", EName = "FuShun", Name = "抚顺", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2105", AreaCode = "0414", CarPrefix = "辽E", EName = "BenEi", Name = "本溪", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2106", AreaCode = "0415", CarPrefix = "辽F", EName = "DanDong", Name = "丹东", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2107", AreaCode = "0416", CarPrefix = "辽G", EName = "JinZhou", Name = "锦州", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2108", AreaCode = "0417", CarPrefix = "辽H", EName = "YingKou", Name = "营口", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2109", AreaCode = "0418", CarPrefix = "辽J", EName = "FuXin", Name = "阜新", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2110", AreaCode = "0419", CarPrefix = "辽K", EName = "LiaoYang", Name = "辽阳", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2111", AreaCode = "0427", CarPrefix = "辽L", EName = "PanJin", Name = "盘锦", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2112", AreaCode = "0410", CarPrefix = "辽M", EName = "TieLing", Name = "铁岭", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2113", AreaCode = "0421", CarPrefix = "辽N", EName = "ChaoYang", Name = "朝阳", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2114", AreaCode = "0429", CarPrefix = "辽P", EName = "HuLuDao", Name = "葫芦岛", ProvinceID = "21", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2201", AreaCode = "0431", CarPrefix = "吉A", EName = "ChangChun", Name = "长春", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2202", AreaCode = "0432", CarPrefix = "吉B", EName = "JiLin", Name = "吉林", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2203", AreaCode = "0434", CarPrefix = "吉C", EName = "SiPing", Name = "四平", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2204", AreaCode = "0437", CarPrefix = "吉D", EName = "LiaoYuan", Name = "辽源", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2205", AreaCode = "0435", CarPrefix = "吉E", EName = "TongHua", Name = "通化", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2206", AreaCode = "0439", CarPrefix = "吉F", EName = "BaiShan", Name = "白山", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2207", AreaCode = "0438", CarPrefix = "吉J", EName = "SongYuan", Name = "松原", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2208", AreaCode = "0436", CarPrefix = "吉G", EName = "BaiCheng", Name = "白城", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2224", AreaCode = "0433", CarPrefix = "吉H", EName = "YanBianChaoXianZuZiZhiZhou", Name = "延边朝鲜族自治州", ProvinceID = "22", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2301", AreaCode = "0451", CarPrefix = "黑A", EName = "HaErBin", Name = "哈尔滨", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2302", AreaCode = "0452", CarPrefix = "黑B", EName = "QiQiHaEr", Name = "齐齐哈尔", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2303", AreaCode = "0453", CarPrefix = "黑G", EName = "JiXi", Name = "鸡西", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2304", AreaCode = "0468", CarPrefix = "黑H", EName = "HeGang", Name = "鹤岗", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2305", AreaCode = "0469", CarPrefix = "黑J", EName = "ShuangYaShan", Name = "双鸭山", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2306", AreaCode = "0459", CarPrefix = "黑E", EName = "DaQing", Name = "大庆", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2307", AreaCode = "0458", CarPrefix = "黑F", EName = "YiChun", Name = "伊春", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2308", AreaCode = "0454", CarPrefix = "黑D", EName = "JiaMuSi", Name = "佳木斯", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2309", AreaCode = "0453", CarPrefix = "黑K", EName = "QiTaiHe", Name = "七台河", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2310", AreaCode = "0453", CarPrefix = "黑C", EName = "MuDanJiang", Name = "牡丹江", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2311", AreaCode = "0456", CarPrefix = "黑N", EName = "HeiHe", Name = "黑河", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2312", AreaCode = "0455", CarPrefix = "黑M", EName = "SuiHua", Name = "绥化", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2327", AreaCode = "0457", CarPrefix = "黑P", EName = "DaXinAnLingDiQiu", Name = "大兴安岭地区", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2328", AreaCode = "0451", CarPrefix = "黑L", EName = "SongHuaJiangDiQiu", Name = "松花江地区", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "2329", AreaCode = "0456", CarPrefix = "黑R", EName = "NongKenEiTong", Name = "农垦系统", ProvinceID = "23", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3201", AreaCode = "025", CarPrefix = "苏A", EName = "NanJing", Name = "南京", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3202", AreaCode = "0510", CarPrefix = "苏B", EName = "WuEi", Name = "无锡", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3203", AreaCode = "0516", CarPrefix = "苏C", EName = "XuZhou", Name = "徐州", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3204", AreaCode = "0519", CarPrefix = "苏D", EName = "ChangZhou", Name = "常州", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3205", AreaCode = "0512", CarPrefix = "苏E", EName = "SuZhou", Name = "苏州", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3206", AreaCode = "0513", CarPrefix = "苏F", EName = "NanTong", Name = "南通", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3207", AreaCode = "0518", CarPrefix = "苏G", EName = "LianYunGang", Name = "连云港", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3208", AreaCode = "0517", CarPrefix = "苏H", EName = "HuaiAn", Name = "淮安", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3209", AreaCode = "0515", CarPrefix = "苏J", EName = "YanCheng", Name = "盐城", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3210", AreaCode = "0514", CarPrefix = "苏K", EName = "YangZhou", Name = "扬州", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3211", AreaCode = "0511", CarPrefix = "苏L", EName = "ZhenJiang", Name = "镇江", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3212", AreaCode = "0523", CarPrefix = "苏M", EName = "TaiZhou", Name = "泰州", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3213", AreaCode = "0527", CarPrefix = "苏N", EName = "SuQian", Name = "宿迁", ProvinceID = "32", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3301", AreaCode = "0571", CarPrefix = "浙A", EName = "HangZhou", Name = "杭州", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3302", AreaCode = "0574", CarPrefix = "浙B", EName = "NingBo", Name = "宁波", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3303", AreaCode = "0577", CarPrefix = "浙C", EName = "WenZhou", Name = "温州", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3304", AreaCode = "0573", CarPrefix = "浙F", EName = "JiaXin", Name = "嘉兴", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3305", AreaCode = "0572", CarPrefix = "浙E", EName = "HuZhou", Name = "湖州", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3306", AreaCode = "0575", CarPrefix = "浙D", EName = "ShaoXin", Name = "绍兴", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3307", AreaCode = "0579", CarPrefix = "浙G", EName = "JinHua", Name = "金华", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3308", AreaCode = "0570", CarPrefix = "浙H", EName = "QuZhou", Name = "衢州", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3309", AreaCode = "0580", CarPrefix = "浙L", EName = "ZhouShan", Name = "舟山", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3310", AreaCode = "0576", CarPrefix = "浙J", EName = "TaiZhou", Name = "台州", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3311", AreaCode = "0578", CarPrefix = "浙K", EName = "LiShui", Name = "丽水", ProvinceID = "33", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3401", AreaCode = "0551", CarPrefix = "皖A", EName = "HeFei", Name = "合肥", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3402", AreaCode = "0553", CarPrefix = "皖B", EName = "WuHu", Name = "芜湖", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3403", AreaCode = "0552", CarPrefix = "皖C", EName = "BangBu", Name = "蚌埠", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3404", AreaCode = "0554", CarPrefix = "皖D", EName = "HuaiNan", Name = "淮南", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3405", AreaCode = "0555", CarPrefix = "皖E", EName = "MaAnShan", Name = "马鞍山", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3406", AreaCode = "0561", CarPrefix = "皖F", EName = "HuaiBei", Name = "淮北", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3407", AreaCode = "0562", CarPrefix = "皖G", EName = "TongLing", Name = "铜陵", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3408", AreaCode = "0556", CarPrefix = "皖H", EName = "AnQing", Name = "安庆", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3410", AreaCode = "0559", CarPrefix = "皖J", EName = "HuangShan", Name = "黄山", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3411", AreaCode = "0550", CarPrefix = "皖M", EName = "ChuZhou", Name = "滁州", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3412", AreaCode = "0558", CarPrefix = "皖K", EName = "FuYang", Name = "阜阳", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3413", AreaCode = "0557", CarPrefix = "皖L", EName = "SuZhou", Name = "宿州", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3415", AreaCode = "0564", CarPrefix = "皖N", EName = "LiuAn", Name = "六安", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3416", AreaCode = "0558", CarPrefix = "皖S", EName = "BoZhou", Name = "亳州", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3417", AreaCode = "0566", CarPrefix = "皖R", EName = "ChiZhou", Name = "池州", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3418", AreaCode = "0563", CarPrefix = "皖P", EName = "XuanCheng", Name = "宣城", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3501", AreaCode = "0591", CarPrefix = "闽A", EName = "FuZhou", Name = "福州", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3502", AreaCode = "0592", CarPrefix = "闽D", EName = "XiaMen", Name = "厦门", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3503", AreaCode = "0594", CarPrefix = "闽B", EName = "PuTian", Name = "莆田", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3504", AreaCode = "0598", CarPrefix = "闽G", EName = "SanMing", Name = "三明", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3505", AreaCode = "0595", CarPrefix = "闽C", EName = "QuanZhou", Name = "泉州", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3506", AreaCode = "0596", CarPrefix = "闽E", EName = "ZhangZhou", Name = "漳州", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3507", AreaCode = "0599", CarPrefix = "闽H", EName = "NanPing", Name = "南平", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3508", AreaCode = "0597", CarPrefix = "闽F", EName = "LongYan", Name = "龙岩", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3509", AreaCode = "0593", CarPrefix = "闽J", EName = "NingDe", Name = "宁德", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3510", AreaCode = "0591", CarPrefix = "闽K", EName = "ShengZhiEiTong", Name = "省直系统", ProvinceID = "35", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3601", AreaCode = "0791", CarPrefix = "赣A", EName = "NanChang", Name = "南昌", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3602", AreaCode = "0798", CarPrefix = "赣H", EName = "JingDeZhen", Name = "景德镇", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3603", AreaCode = "0799", CarPrefix = "赣J", EName = "PingXiang", Name = "萍乡", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3604", AreaCode = "0792", CarPrefix = "赣G", EName = "JiuJiang", Name = "九江", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3605", AreaCode = "0790", CarPrefix = "赣K", EName = "XinYu", Name = "新余", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3606", AreaCode = "0701", CarPrefix = "赣L", EName = "YingTan", Name = "鹰潭", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3607", AreaCode = "0797", CarPrefix = "赣B", EName = "GanZhou", Name = "赣州", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3608", AreaCode = "0796", CarPrefix = "赣D", EName = "JiAn", Name = "吉安", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3609", AreaCode = "0795", CarPrefix = "赣C", EName = "IChun", Name = "宜春", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3610", AreaCode = "0794", CarPrefix = "赣F", EName = "FuZhou", Name = "抚州", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3611", AreaCode = "0793", CarPrefix = "赣E", EName = "ShangRao", Name = "上饶", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3612", AreaCode = "0791", CarPrefix = "赣M", EName = "NanChangShi,ShengZhiEiTong", Name = "南昌市,省直系统", ProvinceID = "36", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3701", AreaCode = "0531", CarPrefix = "鲁A", EName = "JiNan", Name = "济南", ProvinceID = "37", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3702", AreaCode = "0532", CarPrefix = "鲁B", EName = "KemDao", Name = "青岛", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3703", AreaCode = "0533", CarPrefix = "鲁C", EName = "ZiBo", Name = "淄博", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3704", AreaCode = "0632", CarPrefix = "鲁D", EName = "ZaoZhuang", Name = "枣庄", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3705", AreaCode = "0546", CarPrefix = "鲁E", EName = "DongYing", Name = "东营", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3706", AreaCode = "0535", CarPrefix = "鲁F", EName = "YanTai", Name = "烟台", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3707", AreaCode = "0536", CarPrefix = "鲁G", EName = "WeiFang", Name = "潍坊", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3708", AreaCode = "0537", CarPrefix = "鲁H", EName = "JiNing", Name = "济宁", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3709", AreaCode = "0538", CarPrefix = "鲁J", EName = "TaiAn", Name = "泰安", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3710", AreaCode = "0631", CarPrefix = "鲁K", EName = "WeiHai", Name = "威海", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3711", AreaCode = "0633", CarPrefix = "鲁L", EName = "RiZhao", Name = "日照", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3712", AreaCode = "0634", CarPrefix = "鲁S", EName = "LaiWu", Name = "莱芜", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3713", AreaCode = "0539", CarPrefix = "鲁Q", EName = "LinI", Name = "临沂", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3714", AreaCode = "0534", CarPrefix = "鲁N", EName = "DeZhou", Name = "德州", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3715", AreaCode = "0635", CarPrefix = "鲁P", EName = "LiaoCheng", Name = "聊城", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3716", AreaCode = "0543", CarPrefix = "鲁M", EName = "BinZhou", Name = "滨州", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3717", AreaCode = "0530", CarPrefix = "鲁R", EName = "HeZe", Name = "菏泽", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3718", AreaCode = "0532", CarPrefix = "鲁U", EName = "KemDaoShiZengBu", Name = "青岛市增补", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3719", AreaCode = "0536", CarPrefix = "鲁V", EName = "WeiFangShiZengBu", Name = "潍坊市增补", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "3720", AreaCode = "535", CarPrefix = "鲁Y", EName = "YanTaiShiZengBu", Name = "烟台市增补", ProvinceID = "37", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4101", AreaCode = "0371", CarPrefix = "豫A", EName = "ZhengZhou", Name = "郑州", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4102", AreaCode = "0378", CarPrefix = "豫B", EName = "KaiFeng", Name = "开封", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4103", AreaCode = "0379", CarPrefix = "豫C", EName = "LuoYang", Name = "洛阳", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4104", AreaCode = "0375", CarPrefix = "豫D", EName = "PingDingShan", Name = "平顶山", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4105", AreaCode = "0372", CarPrefix = "豫E", EName = "AnYang", Name = "安阳", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4106", AreaCode = "0392", CarPrefix = "豫F", EName = "HeBi", Name = "鹤壁", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4107", AreaCode = "0373", CarPrefix = "豫G", EName = "XinXiang", Name = "新乡", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4108", AreaCode = "0391", CarPrefix = "豫H", EName = "JiaoZuo", Name = "焦作", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4109", AreaCode = "0393", CarPrefix = "豫J", EName = "PuYang", Name = "濮阳", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4110", AreaCode = "0374", CarPrefix = "豫K", EName = "XuChang", Name = "许昌", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4111", AreaCode = "0395", CarPrefix = "豫L", EName = "LuoHe", Name = "漯河", ProvinceID = "41", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4112", AreaCode = "0398", CarPrefix = "豫M", EName = "SanMenXia", Name = "三门峡", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4113", AreaCode = "0377", CarPrefix = "豫R", EName = "NanYang", Name = "南阳", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4114", AreaCode = "0370", CarPrefix = "豫N", EName = "ShangQiu", Name = "商丘", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4115", AreaCode = "0376", CarPrefix = "豫S", EName = "XinYang", Name = "信阳", ProvinceID = "41", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4116", AreaCode = "0394", CarPrefix = "豫P", EName = "ZhouKou", Name = "周口", ProvinceID = "41", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4117", AreaCode = "0396", CarPrefix = "豫Q", EName = "ZhuMaDian", Name = "驻马店", ProvinceID = "41", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4201", AreaCode = "027", CarPrefix = "鄂A", EName = "WuHan", Name = "武汉", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4202", AreaCode = "0714", CarPrefix = "鄂B", EName = "HuangShi", Name = "黄石", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4203", AreaCode = "0719", CarPrefix = "鄂C", EName = "ShiEom", Name = "十堰", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4205", AreaCode = "0717", CarPrefix = "鄂E", EName = "IChang", Name = "宜昌", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4206", AreaCode = "0710", CarPrefix = "鄂F", EName = "XiangFan", Name = "襄阳", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4207", AreaCode = "0711", CarPrefix = "鄂G", EName = "EZhou", Name = "鄂州", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4208", AreaCode = "0724", CarPrefix = "鄂H", EName = "JingMen", Name = "荆门", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4209", AreaCode = "0712", CarPrefix = "鄂K", EName = "XiaoGan", Name = "孝感", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4210", AreaCode = "0716", CarPrefix = "鄂D", EName = "JingZhou", Name = "荆州", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4211", AreaCode = "0713", CarPrefix = "鄂J", EName = "HuangGang", Name = "黄冈", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4212", AreaCode = "0715", CarPrefix = "鄂L", EName = "XianNing", Name = "咸宁", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4213", AreaCode = "0722", CarPrefix = "鄂S", EName = "SuiZhou", Name = "随州", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4228", AreaCode = "0718", CarPrefix = "鄂Q", EName = "EnShiTuJiaZuMiaoZuZiZhiZhou", Name = "恩施土家族苗族自治州", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4301", AreaCode = "0731", CarPrefix = "湘A", EName = "ChangSha", Name = "长沙", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4302", AreaCode = "0733", CarPrefix = "湘B", EName = "ZhuZhou", Name = "株洲", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4303", AreaCode = "0732", CarPrefix = "湘C", EName = "XiangTan", Name = "湘潭", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4304", AreaCode = "0734", CarPrefix = "湘D", EName = "HengYang", Name = "衡阳", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4305", AreaCode = "0739", CarPrefix = "湘E", EName = "ShaoYang", Name = "邵阳", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4306", AreaCode = "0730", CarPrefix = "湘F", EName = "YueYang", Name = "岳阳", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4307", AreaCode = "0736", CarPrefix = "湘J", EName = "ChangDe", Name = "常德", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4308", AreaCode = "0744", CarPrefix = "湘G", EName = "ZhangJiaJie", Name = "张家界", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4309", AreaCode = "0737", CarPrefix = "湘H", EName = "IYang", Name = "益阳", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4310", AreaCode = "0735", CarPrefix = "湘L", EName = "ChenZhou", Name = "郴州", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4311", AreaCode = "0746", CarPrefix = "湘M", EName = "YongZhou", Name = "永州", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4312", AreaCode = "0745", CarPrefix = "湘N", EName = "HuaHua", Name = "怀化", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4313", AreaCode = "0738", CarPrefix = "湘K", EName = "LouDi", Name = "娄底", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4331", AreaCode = "0743", CarPrefix = "湘U", EName = "XiangXiTuJiaZuMiaoZuZiZhiZhou", Name = "湘西土家族苗族自治州", ProvinceID = "43", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4401", AreaCode = "020", CarPrefix = "粤A", EName = "GuangZhou", Name = "广州", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4402", AreaCode = "0751", CarPrefix = "粤F", EName = "ShaoGuan", Name = "韶关", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4403", AreaCode = "0755", CarPrefix = "粤B", EName = "ShenZhen", Name = "深圳", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4404", AreaCode = "0756", CarPrefix = "粤C", EName = "ZhuHai", Name = "珠海", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4405", AreaCode = "0754", CarPrefix = "粤D", EName = "ShanTou", Name = "汕头", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4406", AreaCode = "0757", CarPrefix = "粤E", EName = "FoShan", Name = "佛山", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4407", AreaCode = "0750", CarPrefix = "粤J", EName = "JiangMen", Name = "江门", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4408", AreaCode = "0759", CarPrefix = "粤G", EName = "ZhanJiang", Name = "湛江", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4409", AreaCode = "0668", CarPrefix = "粤K", EName = "MaoMing", Name = "茂名", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4412", AreaCode = "0758", CarPrefix = "粤H", EName = "ZhaoQing", Name = "肇庆", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4413", AreaCode = "0752", CarPrefix = "粤L", EName = "HuiZhou", Name = "惠州", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4414", AreaCode = "0753", CarPrefix = "粤M", EName = "MeiZhou", Name = "梅州", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4415", AreaCode = "0660", CarPrefix = "粤N", EName = "ShanWei", Name = "汕尾", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4416", AreaCode = "0762", CarPrefix = "粤P", EName = "HeYuan", Name = "河源", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4417", AreaCode = "0662", CarPrefix = "粤Q", EName = "YangJiang", Name = "阳江", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4418", AreaCode = "0763", CarPrefix = "粤R", EName = "QingYuan", Name = "清远", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4419", AreaCode = "0769", CarPrefix = "粤S", EName = "DongWan", Name = "东莞", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4420", AreaCode = "0760", CarPrefix = "粤T", EName = "ZhongShan", Name = "中山", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4451", AreaCode = "0768", CarPrefix = "粤U", EName = "ChaoZhou", Name = "潮州", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4452", AreaCode = "0663", CarPrefix = "粤V", EName = "JieYang", Name = "揭阳", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4453", AreaCode = "0766", CarPrefix = "粤W", EName = "YunFu", Name = "云浮", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4454", AreaCode = "0765", CarPrefix = "粤X", EName = "ShunDeQiu", Name = "顺德区", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4455", AreaCode = "020", CarPrefix = "粤Y", EName = "NanHaiQiu", Name = "南海区", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4480", AreaCode = "0085", CarPrefix = "粤Z", EName = "GangAo", Name = "港澳", ProvinceID = "44", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4501", AreaCode = "0771", CarPrefix = "桂A", EName = "NanNing", Name = "南宁", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4502", AreaCode = "0772", CarPrefix = "桂B", EName = "LiuZhou", Name = "柳州", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4503", AreaCode = "0773", CarPrefix = "桂C", EName = "GuiLin", Name = "桂林", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4504", AreaCode = "0774", CarPrefix = "桂D", EName = "WuZhou", Name = "梧州", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4505", AreaCode = "0779", CarPrefix = "桂E", EName = "BeiHai", Name = "北海", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4506", AreaCode = "0770", CarPrefix = "桂P", EName = "FangChengGang", Name = "防城港", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4507", AreaCode = "0777", CarPrefix = "桂N", EName = "QinZhou", Name = "钦州", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4508", AreaCode = "0775", CarPrefix = "桂R", EName = "GuiGang", Name = "贵港", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4509", AreaCode = "0775", CarPrefix = "桂K", EName = "YuLin", Name = "玉林", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4510", AreaCode = "0776", CarPrefix = "桂L", EName = "BaiSe", Name = "百色", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4511", AreaCode = "0774", CarPrefix = "桂J", EName = "HeZhou", Name = "贺州", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4512", AreaCode = "0778", CarPrefix = "桂M", EName = "HeChi", Name = "河池", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4513", AreaCode = "0772", CarPrefix = "桂G", EName = "LaiBin", Name = "来宾", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4514", AreaCode = "0771", CarPrefix = "桂F", EName = "ChongZuo", Name = "崇左", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4525", AreaCode = "0773", CarPrefix = "桂H", EName = "GuiLinDiQiu", Name = "桂林地区", ProvinceID = "45", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4601", AreaCode = "0898", CarPrefix = "琼A", EName = "HaiKou", Name = "海口", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4602", AreaCode = "0899", CarPrefix = "琼B", EName = "SanYa", Name = "三亚", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "4606", AreaCode = "0890", CarPrefix = "琼E", EName = "YangPuKaiFaQiu", Name = "洋浦开发区", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5101", AreaCode = "028", CarPrefix = "川A", EName = "ChengDou", Name = "成都", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5103", AreaCode = "0813", CarPrefix = "川C", EName = "ZiGong", Name = "自贡", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5104", AreaCode = "0812", CarPrefix = "川D", EName = "PanZhiHua", Name = "攀枝花", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5105", AreaCode = "0840", CarPrefix = "川E", EName = "LuZhou", Name = "泸州", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5106", AreaCode = "0838", CarPrefix = "川F", EName = "DeYang", Name = "德阳", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5107", AreaCode = "0816", CarPrefix = "川B", EName = "MianYang", Name = "绵阳", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5108", AreaCode = "0839", CarPrefix = "川H", EName = "GuangYuan", Name = "广元", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5109", AreaCode = "0825", CarPrefix = "川J", EName = "SuiNing", Name = "遂宁", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5110", AreaCode = "0832", CarPrefix = "川K", EName = "NaJiang", Name = "内江", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5111", AreaCode = "0833", CarPrefix = "川L", EName = "LeShan", Name = "乐山", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5113", AreaCode = "0817", CarPrefix = "川R", EName = "NanChong", Name = "南充", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5114", AreaCode = "028", CarPrefix = "川Z", EName = "MeiShan", Name = "眉山", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5115", AreaCode = "0831", CarPrefix = "川Q", EName = "IBin", Name = "宜宾", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5116", AreaCode = "0826", CarPrefix = "川X", EName = "GuangAn", Name = "广安", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5117", AreaCode = "0818", CarPrefix = "川S", EName = "DaZhou", Name = "达州", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5118", AreaCode = "0835", CarPrefix = "川T", EName = "YaAn", Name = "雅安", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5119", AreaCode = "0827", CarPrefix = "川Y", EName = "BaZhong", Name = "巴中", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5120", AreaCode = "028", CarPrefix = "川M", EName = "ZiYang", Name = "资阳", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5132", AreaCode = "0837", CarPrefix = "川U", EName = "ABa", Name = "阿坝", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5133", AreaCode = "0836", CarPrefix = "川V", EName = "GanZi", Name = "甘孜", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5134", AreaCode = "0834", CarPrefix = "川W", EName = "LiangShanIZuZiZhiZhou", Name = "凉山彝族自治州", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5201", AreaCode = "0851", CarPrefix = "贵A", EName = "GuiYang", Name = "贵阳", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5202", AreaCode = "0858", CarPrefix = "贵B", EName = "LiuPanShui", Name = "六盘水", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5203", AreaCode = "0852", CarPrefix = "贵C", EName = "ZunI", Name = "遵义", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5204", AreaCode = "0853", CarPrefix = "贵G", EName = "AnShun", Name = "安顺", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5205", AreaCode = "0857", CarPrefix = "贵F", EName = "BiJie", Name = "毕节", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5206", AreaCode = "0856", CarPrefix = "贵D", EName = "TongRen", Name = "铜仁", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5223", AreaCode = "0859", CarPrefix = "贵E", EName = "QianXiNanBuYiZuMiaoZuZiZhi", Name = "黔西南布依族苗族自治", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5226", AreaCode = "0855", CarPrefix = "贵H", EName = "QianDongNanMiaoZuDongZuZiZhiZh", Name = "黔东南苗族侗族自治州", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5227", AreaCode = "0854", CarPrefix = "贵J", EName = "QianNanBuYiZuMiaoZuZiZhiZhou", Name = "黔南布依族苗族自治州", ProvinceID = "52", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5301", AreaCode = "0871", CarPrefix = "云A", EName = "KunMing", Name = "昆明", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5303", AreaCode = "0874", CarPrefix = "云D", EName = "QuJing", Name = "曲靖", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5304", AreaCode = "0877", CarPrefix = "云F", EName = "YuEi", Name = "玉溪", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5305", AreaCode = "0875", CarPrefix = "云M", EName = "BaoShan", Name = "保山", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5306", AreaCode = "0870", CarPrefix = "云C", EName = "ZhaoTong", Name = "昭通", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5307", AreaCode = "0888", CarPrefix = "云P", EName = "LiJiang", Name = "丽江", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5308", AreaCode = "0879", CarPrefix = "云J", EName = "PuEr", Name = "普洱", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5309", AreaCode = "0883", CarPrefix = "云S", EName = "LinCang", Name = "临沧", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5323", AreaCode = "0878", CarPrefix = "云E", EName = "ChuXiongIZuZiZhiZhou", Name = "楚雄彝族自治州", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5325", AreaCode = "0873", CarPrefix = "云G", EName = "HongHeHaNiZuIZuZiZhiZhou", Name = "红河哈尼族彝族自治州", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5326", AreaCode = "0876", CarPrefix = "云H", EName = "WenShanZhuangZuMiaoZuZiZhiZhou", Name = "文山壮族苗族自治州", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5328", AreaCode = "0691", CarPrefix = "云K", EName = "XiShuangBanNaDaiZuZiZhiZhou", Name = "西双版纳傣族自治州", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5329", AreaCode = "0872", CarPrefix = "云L", EName = "DaLiBaiZuZiZhiZhou", Name = "大理白族自治州", ProvinceID = "53", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5331", AreaCode = "0692", CarPrefix = "云N", EName = "DeHongDaiZuJingPoZuZiZhiZhou", Name = "德宏傣族景颇族自治州", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5333", AreaCode = "0886", CarPrefix = "云Q", EName = "NuJiangLiSuZuZiZhiZhou", Name = "怒江傈僳族自治州", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5334", AreaCode = "0887", CarPrefix = "云R", EName = "DiQingCangZuZiZhiZhou", Name = "迪庆藏族自治州", ProvinceID = "53", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5401", AreaCode = "0891", CarPrefix = "藏A", EName = "LaSa", Name = "拉萨", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5421", AreaCode = "0895", CarPrefix = "藏B", EName = "ChangDouDiQiu", Name = "昌都地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5422", AreaCode = "0893", CarPrefix = "藏C", EName = "ShanNanDiQiu", Name = "山南地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5423", AreaCode = "0892", CarPrefix = "藏D", EName = "RiKaZeDiQiu", Name = "日喀则地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5424", AreaCode = "0896", CarPrefix = "藏E", EName = "NaQuDiQiu", Name = "那曲地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5425", AreaCode = "0897", CarPrefix = "藏F", EName = "ALiDiQiu", Name = "阿里地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "5426", AreaCode = "0894", CarPrefix = "藏G", EName = "LinZhiDiQiu", Name = "林芝地区", ProvinceID = "54", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6101", AreaCode = "029", CarPrefix = "陕A", EName = "XiAn", Name = "西安", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6102", AreaCode = "0919", CarPrefix = "陕B", EName = "TongChuan", Name = "铜川", ProvinceID = "61", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6103", AreaCode = "0917", CarPrefix = "陕C", EName = "BaoJi", Name = "宝鸡", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6104", AreaCode = "0910", CarPrefix = "陕D", EName = "XianYang", Name = "咸阳", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6105", AreaCode = "0913", CarPrefix = "陕E", EName = "WeiNan", Name = "渭南", ProvinceID = "61", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6106", AreaCode = "0911", CarPrefix = "陕J", EName = "YanAn", Name = "延安", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6107", AreaCode = "0916", CarPrefix = "陕F", EName = "HanZhong", Name = "汉中", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6108", AreaCode = "0912", CarPrefix = "陕K", EName = "YuLin", Name = "榆林", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6109", AreaCode = "0915", CarPrefix = "陕G", EName = "AnKang", Name = "安康", ProvinceID = "61", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6110", AreaCode = "0914", CarPrefix = "陕H", EName = "ShangLuo", Name = "商洛", ProvinceID = "61", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6125", AreaCode = "029", CarPrefix = "陕V", EName = "YangLingGaoXinNongYeShiFanQiu", Name = "杨凌高新农业示范区", ProvinceID = "61", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6201", AreaCode = "0931", CarPrefix = "甘A", EName = "LanZhou", Name = "兰州", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6202", AreaCode = "0937", CarPrefix = "甘B", EName = "JiaYuGuan", Name = "嘉峪关", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6203", AreaCode = "0935", CarPrefix = "甘C", EName = "JinChang", Name = "金昌", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6204", AreaCode = "0943", CarPrefix = "甘D", EName = "BaiYin", Name = "白银", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6205", AreaCode = "0938", CarPrefix = "甘E", EName = "TianShui", Name = "天水", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6206", AreaCode = "0935", CarPrefix = "甘H", EName = "WuWei", Name = "武威", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6207", AreaCode = "0936", CarPrefix = "甘G", EName = "ZhangYe", Name = "张掖", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6208", AreaCode = "0933", CarPrefix = "甘L", EName = "PingLiang", Name = "平凉", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6209", AreaCode = "0937", CarPrefix = "甘F", EName = "JiuQuan", Name = "酒泉", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6210", AreaCode = "0934", CarPrefix = "甘M", EName = "QingYang", Name = "庆阳", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6211", AreaCode = "0932", CarPrefix = "甘J", EName = "DingXi", Name = "定西", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6212", AreaCode = "0939", CarPrefix = "甘K", EName = "LongNan", Name = "陇南", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6229", AreaCode = "0930", CarPrefix = "甘N", EName = "LinXiaHuiZuZiZhiZhou", Name = "临夏回族自治州", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6230", AreaCode = "0941", CarPrefix = "甘P", EName = "GanNanCangZuZiZhiZhou", Name = "甘南藏族自治州", ProvinceID = "62", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6301", AreaCode = "0971", CarPrefix = "青A", EName = "XiNing", Name = "西宁", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6321", AreaCode = "0972", CarPrefix = "青B", EName = "HaiDongDiQiu", Name = "海东地区", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6322", AreaCode = "0970", CarPrefix = "青C", EName = "HaiBeiCangZuZiZhiZhou", Name = "海北藏族自治州", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6323", AreaCode = "0973", CarPrefix = "青D", EName = "HuangNanCangZuZiZhiZhou", Name = "黄南藏族自治州", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6325", AreaCode = "0974", CarPrefix = "青E", EName = "HaiNanCangZuZiZhiZhou", Name = "海南藏族自治州", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6326", AreaCode = "0975", CarPrefix = "青F", EName = "GuoLuoCangZuZiZhiZhou", Name = "果洛藏族自治州", ProvinceID = "63", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6327", AreaCode = "0976", CarPrefix = "青G", EName = "YuShuCangZuZiZhiZhou", Name = "玉树藏族自治州", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6328", AreaCode = "0977", CarPrefix = "青H", EName = "HaiXiMengGuZuCangZuZiZhiZhou", Name = "海西蒙古族藏族自治州", ProvinceID = "63", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6401", AreaCode = "0951", CarPrefix = "宁A", EName = "YinChuan", Name = "银川", ProvinceID = "64", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6402", AreaCode = "0952", CarPrefix = "宁B", EName = "ShiZuiShan", Name = "石嘴山", ProvinceID = "64", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6403", AreaCode = "0953", CarPrefix = "宁C", EName = "WuZhong", Name = "吴忠", ProvinceID = "64", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6404", AreaCode = "0954", CarPrefix = "宁D", EName = "GuYuan", Name = "固原", ProvinceID = "64", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6405", AreaCode = "0955", CarPrefix = "宁E", EName = "ZhongWei", Name = "中卫", ProvinceID = "64", Rank = "0", Status = "-1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6501", AreaCode = "0991", CarPrefix = "新A", EName = "WuLuMuQi", Name = "乌鲁木齐", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6502", AreaCode = "0990", CarPrefix = "新J", EName = "KeLaMaYi", Name = "克拉玛依", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6521", AreaCode = "0995", CarPrefix = "新K", EName = "TuLuFanDiQiu", Name = "吐鲁番地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6522", AreaCode = "0902", CarPrefix = "新L", EName = "HaMiDiQiu", Name = "哈密地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6523", AreaCode = "0994", CarPrefix = "新B", EName = "ChangJiHuiZuZiZhiZhou", Name = "昌吉回族自治州", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6527", AreaCode = "0909", CarPrefix = "新E", EName = "BoErTaLaMengGuZiZhiZhou", Name = "博尔塔拉蒙古自治州", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6528", AreaCode = "0996", CarPrefix = "新M", EName = "BaYinGuoLengMengGuZiZhiZhou", Name = "巴音郭楞蒙古自治州", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6529", AreaCode = "0997", CarPrefix = "新N", EName = "AKeSuDiQiu", Name = "阿克苏地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6530", AreaCode = "0908", CarPrefix = "新P", EName = "KeZiLeSuKeErKeZiZiZhi", Name = "克孜勒苏柯尔克孜自治", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6531", AreaCode = "0998", CarPrefix = "新Q", EName = "KaShiDiQiu", Name = "喀什地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6532", AreaCode = "0903", CarPrefix = "新R", EName = "HeTianDiQiu", Name = "和田地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6540", AreaCode = "0999", CarPrefix = "新F", EName = "YiLiHaSaKeZiZhiZhou", Name = "伊犁哈萨克自治州", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6542", AreaCode = "0901", CarPrefix = "新G", EName = "TaChengDiQiu", Name = "塔城地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "6543", AreaCode = "0906", CarPrefix = "新H", EName = "ALeTaiDiQiu", Name = "阿勒泰地区", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "340181", AreaCode = "0565", CarPrefix = "皖Q", EName = "ChaoHu", Name = "巢湖", ProvinceID = "34", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "419001", AreaCode = "0391", CarPrefix = "豫U", EName = "JiYuan", Name = "济源", ProvinceID = "41", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "429004", AreaCode = "0728", CarPrefix = "鄂M", EName = "XianTao", Name = "仙桃", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "429005", AreaCode = "0728", CarPrefix = "鄂N", EName = "QianJiang", Name = "潜江", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "429006", AreaCode = "0728", CarPrefix = "鄂R", EName = "TianMen", Name = "天门", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "429021", AreaCode = "0719", CarPrefix = "鄂P", EName = "ShenNongJiaLinQiu", Name = "神农架林区", ProvinceID = "42", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "469001", AreaCode = "0898", CarPrefix = "琼D", EName = "WuZhiShan", Name = "五指山", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "469002", AreaCode = "0898", CarPrefix = "琼C", EName = "QiongHai", Name = "琼海", ProvinceID = "46", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "513401", AreaCode = "0834", CarPrefix = "川W", EName = "XiChang", Name = "西昌", ProvinceID = "51", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "654003", AreaCode = "0992", CarPrefix = "新D", EName = "KuiTun", Name = "奎屯", ProvinceID = "65", Rank = "0", Status = "1" });
                            _ChinaCitys.Add(new CityCode() { ID = "659001", AreaCode = "0993", CarPrefix = "新C", EName = "ShiHeZi", Name = "石河子", ProvinceID = "65", Rank = "0", Status = "1" });
                            #endregion
                        }
                    }
                }
                return _ChinaCitys;
            }
        }

        private static CityCode FindByCarPrefix(string CarPrefix)
        {
            List<CityCode> query = ChinaCityQuery;
            foreach (CityCode entity in query)
            {
                if (entity.CarPrefix == CarPrefix)
                {
                    return entity;
                }
                if (CarPrefix.Length > 1)
                {
                    if (entity.CarPrefix == CarPrefix.Substring(0, 1))
                    {
                        return entity;
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
