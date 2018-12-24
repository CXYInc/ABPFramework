using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Json;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Castle.Core.Logging;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Common;
using CXY.CJS.Core.Config;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Enums;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.HttpClient;
using CXY.CJS.Core.NPOI;
using CXY.CJS.Core.Utils;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using CXY.CJS.Repository;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 客服违章服务
    /// </summary>
    [Route(CJSConsts.RoutePrefix + "/BatchCarViolation/[action]")]
    [Authorize]
    public class BatchCarViolationService : CJSAppServiceBase, IBatchCarViolationService
    {
        private readonly IBatchCarRepository _batchCarRepository;
        private readonly IBatchAskPriceViolationAgentRepository _violationRepository;
        private readonly IBatchInfoRepository _batchInfoRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly ILogger _logger;
        private readonly IUserServices _userServices;
        private readonly IHostingEnvironment _env;
        private readonly ApiUrlConfig _apiUrlConfig;
        private readonly HttpClientHelper _httpClientHelper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityRepository"></param>
        /// <param name="objectMapper"></param>
        /// <param name="logger"></param>
        public BatchCarViolationService(IBatchCarRepository batchCarRepository, IUserServices userServices, IBatchInfoRepository batchInfoRepository,
            IBatchAskPriceViolationAgentRepository violationRepository, ILogger logger, IObjectMapper objectMapper, IHostingEnvironment env,
           IOptionsSnapshot<ApiUrlConfig> apiUrlConfig, HttpClientHelper httpClientHelper)
        {
            _batchCarRepository = batchCarRepository;
            _objectMapper = objectMapper;
            _logger = logger;
            _userServices = userServices;
            _violationRepository = violationRepository;
            _batchInfoRepository = batchInfoRepository;
            _apiUrlConfig = apiUrlConfig.Value;
            _httpClientHelper = httpClientHelper;
            _env = env;
        }

        /// <summary>
        /// 获取BatchCar的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PagedResultDto<BatchCarListDto>> GetPaged(GetBatchCarsInput input)
        {

            var query = _batchCarRepository.GetAll();
            // TODO:根据传入的参数添加过滤条件


            var count = query.Count();

            var entityList = query.OrderBy(input.Sorting).PageBy(input).ToList();

            // var entityListDtos = ObjectMapper.Map<List<BatchCarListDto>>(entityList);
            var entityListDtos = entityList.MapTo<List<BatchCarListDto>>();

            return new PagedResultDto<BatchCarListDto>(count, entityListDtos);
        }

        /// <summary>
        /// 通过指定id获取BatchCarListDto信息
        /// </summary>
        [HttpPost]
        public async Task<BatchCarListDto> GetById(EntityDto<string> input)
        {
            var entity = await _batchCarRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

            return entity.MapTo<BatchCarListDto>();
        }

        /// <summary>
        /// 获取编辑 BatchCar
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<GetBatchCarForEditOutput> GetForEdit(BatchCarEditDto input)
        {
            var output = new GetBatchCarForEditOutput();
            BatchCarEditDto editDto;

            if (!string.IsNullOrEmpty(input.Id))
            {
                var entity = await _batchCarRepository.FirstOrDefaultAsync(x => x.Id == input.Id);

                editDto = entity.MapTo<BatchCarEditDto>();

                //batchCarEditDto = ObjectMapper.Map<List<batchCarEditDto>>(entity);
            }
            else
            {
                editDto = new BatchCarEditDto();
            }

            output.BatchCar = editDto;
            return output;
        }

        /// <summary>
        /// 添加或者修改BatchCar的公共方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task CreateOrUpdate(CreateOrUpdateBatchCarInput input)
        {

            if (!string.IsNullOrEmpty(input.BatchCar.Id))
            {
                await Update(input.BatchCar);
            }
            else
            {
                await Create(input.BatchCar);
            }
        }

        /// <summary>
        /// 新增BatchCar
        /// </summary>
        [HttpPost]
        protected virtual async Task<BatchCarEditDto> Create(BatchCarEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            // var entity = ObjectMapper.Map <BatchCar>(input);
            var entity = input.MapTo<BatchCar>();


            entity = await _batchCarRepository.InsertAsync(entity);
            return entity.MapTo<BatchCarEditDto>();
        }

        /// <summary>
        /// 编辑BatchCar
        /// </summary>
        [HttpPost]
        protected virtual async Task Update(BatchCarEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _batchCarRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _batchCarRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除BatchCar信息的方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Delete(EntityDto<string> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _batchCarRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除BatchCar的方法
        /// </summary>
        [HttpPost]
        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _batchCarRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        ///// <summary>
        ///// 导出BatchCar为excel表,等待开发。
        ///// </summary>
        ///// <returns></returns>
        //public async Task<FileDto> GetToExcel()
        //{
        //	var users = await UserManager.Users.ToListAsync();
        //	var userListDtos = ObjectMapper.Map<List<UserListDto>>(users);
        //	await FillRoleNames(userListDtos);
        //	return _userListExcelExporter.ExportToFile(userListDtos);
        //}

        #region 违章导入业务
        /// <summary>
        /// 违章导入接口
        /// </summary>
        /// <param name="importViolationDto">数据导入实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<IList<BatchTableModelDto>>> ImportViolations([FromForm]ImportViolationDto importViolationDto)
        {
            try
            {
                var batchInfo = await _batchInfoRepository.FirstOrDefaultAsync(x => x.Id == importViolationDto.BatchId);

                var ds = NPOIExcelHelper.ReadExcel(importViolationDto.File);

                var dt = ds.Tables["订单信息"];

                var list = dt.ConvertToModel<BatchTableModelDto>().ToList();

                var errors = await CheckError(list);

                await QueryViolationInfo(list, batchInfo);

                var repeats = await CheckRepeat(list, importViolationDto.BatchId);

                list.ForEach(x =>
                {
                    x.Uniquecode = CommonHelper.GenerateViolationCode(x.车牌号, x.违章时间, x.违章原因);
                    x.BatchId = importViolationDto.BatchId;
                    x.CreateUserId = AbpSession.UserId;
                    x.CreateUserName = AbpSession.UserName;
                    x.WebSiteId = AbpSession.WebSiteId;
                });

                var fileName = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(importViolationDto.File.FileName)}";
                var fullFilePath = Path.Combine(_env.WebRootPath, "UploadFiles", fileName);
                await FileOperateHelper.SaveStreamToFileAsync(importViolationDto.File.OpenReadStream(), fullFilePath);

                return new ApiResult<IList<BatchTableModelDto>>().Success(list);
            }
            catch (Exception ex)
            {
                _logger.Error($"ImportViolations:{ex.Message}");
                return new ApiResult<IList<BatchTableModelDto>>().Error("系统忙，请稍后重试！");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="batchTableModels"></param>
        /// <returns></returns>
        private async Task InsertOrUpdateViolations(string batchId, List<BatchTableModelDto> batchTableModels)
        {
            if (batchId.IsNullOrWhiteSpace()) return;

            var batchInfo = await _batchInfoRepository.FirstOrDefaultAsync(x => x.Id == batchId);

            if (batchId == null) return;

            var oldBatchCars = await _batchCarRepository.GetAllListAsync(x => x.BatchId == batchId);

            var oldBatchViolations = await _violationRepository.GetAllListAsync(x => x.BatchId == batchId);

            //当前登录用户
            //var user = SessionHelper.User;

            var updateCarList = new List<BatchCar>();
            var addCarList = new List<BatchCar>();

            var updateViolationList = new List<BatchAskPriceViolationAgent>();
            var addViolationList = new List<BatchAskPriceViolationAgent>();

            //查询代办人列表
            var agentNames = batchTableModels.Select(x => x.代办方).ToList();
            //var users = GetUserInfoByKey(agentNames, SessionHelper.WebSite.WebSiteId);
            var batchCanComplete = false;

            foreach (var item in batchTableModels)
            {
                if (item == null) continue;
                var oldCar = oldBatchCars.FirstOrDefault(x => x.CarNumber == item.车牌号 && x.CarCode == item.车架号 && x.EngineNo == item.发动机号);
                var newCar = addCarList.FirstOrDefault(x => x.CarNumber == item.车牌号 && x.CarCode == item.车架号 && x.EngineNo == item.发动机号);

                if (oldCar == null)
                {
                    if (newCar == null)
                    {
                        newCar = _objectMapper.Map<BatchCar>(item);
                        addCarList.Add(newCar);
                    }
                }
                else
                {
                    if (!updateCarList.Any(x => x.CarNumber == item.车牌号))
                    {
                        _objectMapper.Map(item, oldCar);
                        updateCarList.Add(oldCar);
                    }
                }

                var oldViolations = oldBatchViolations.Where(x => x.OrderByNo == item.序号.ToInt() && x.Uniquecode == item.Uniquecode).ToList();
                BatchAskPriceViolationAgent newViolation = null;

                if (oldViolations == null || !oldViolations.Any())
                {
                    newViolation = _objectMapper.Map<BatchAskPriceViolationAgent>(item);
                    newViolation.CarId = oldCar == null ? newCar.Id : oldCar.Id;

                    if (item.DataStatus == (int)ViolationDataStatusEnum.Normal || item.DataStatus == (int)ViolationDataStatusEnum.OtherBatchRepeat
                        || item.DataStatus == (int)ViolationDataStatusEnum.ThisBatchRepeat)
                    {
                        batchCanComplete = true;
                    }

                    //在客服导入违章的时候 如果违章代码是空的且罚金和扣分都是0的 自动补充违章代码6050
                    if (newViolation.Code.IsNullOrWhiteSpace() && newViolation.Count == 0 && newViolation.Degree == 0)
                    {
                        newViolation.Code = "6050";
                    }

                    //价格来源
                    if (newViolation.Poundage != 0 || !newViolation.AgentUserId.IsNullOrEmpty())
                    {
                        newViolation.PriceFrom = (int)PriceFromEnum.Person; //0系统,1人工
                        newViolation.Poundage = Math.Round((decimal)newViolation.Poundage, 0, MidpointRounding.AwayFromZero); //人工报价金额-四舍五入
                        newViolation.CanProcess = 1;
                    }

                    addViolationList.Add(newViolation);
                }
                else
                {
                    oldViolations = oldViolations.Where(x => (x.State == (int)ViolationStateEnum.WaitHandle || x.State == (int)ViolationStateEnum.Backed ||
                    x.State == (int)ViolationStateEnum.ReSeted) && !updateViolationList.Any(y => y.OrderByNo == x.OrderByNo &&
                    y.Uniquecode == x.Uniquecode)).ToList();

                    oldViolations.ForEach(x =>
                    {

                        _objectMapper.Map(item, x);

                        x.CarId = oldCar == null ? newCar.Id : oldCar.Id;

                        //在客服导入违章的时候 如果违章代码是空的且罚金和扣分都是0的 自动补充违章代码6050
                        if (item.违法代码.IsNullOrWhiteSpace() && item.罚金.ToInt() == 0 && item.扣分.ToInt() == 0)
                        {
                            x.Code = "6050";
                        }

                        //价格来源
                        if (item.手续费.ToDecimal(0) != 0 || !x.AgentUserId.IsNullOrEmpty())
                        {
                            x.PriceFrom = (int)PriceFromEnum.Person; ; //0系统,1人工
                            x.Poundage = Math.Round((decimal)item.手续费.ToDecimal(), MidpointRounding.AwayFromZero); //人工报价金额-四舍五入
                            x.CanProcess = 1;
                        }
                        else
                        {
                            x.PriceFrom = (int)PriceFromEnum.System;
                            x.Poundage = 0;
                            x.CanProcess = 0;
                        }
                    });

                    updateViolationList.AddRange(oldViolations);
                }
            }

            batchInfo.CarCount += addCarList.Count;

            batchInfo.ViolationCount += addViolationList.Count;

            if (batchInfo.Status == 0)
                batchInfo.Status = (int)BatchStatusEnum.WaitHandle;

            if (batchInfo.Status == (int)BatchStatusEnum.Completed && batchCanComplete)
            {
                batchInfo.Status = (int)BatchStatusEnum.Handling;
                batchInfo.CompleteTime = null;
            }

            //车辆
            await _batchCarRepository.BulkInsertAsync(addCarList);
            await _batchCarRepository.BulkUpdateAsync(updateCarList);

            //违章
            await _violationRepository.BulkInsertAsync(addViolationList);
            await _violationRepository.BulkUpdateAsync(updateViolationList);

            await _batchInfoRepository.UpdateAsync(batchInfo);
        }

        private async Task<List<ViolationErrorInfo>> CheckError(List<BatchTableModelDto> batchTableModels)
        {
            var violationErrorInfos = new List<ViolationErrorInfo>();

            try
            {
                if (batchTableModels != null && batchTableModels.Count > 0)
                {
                    //查询代办人列表
                    var agentNames = batchTableModels.Where(x => !string.IsNullOrEmpty(x.代办方)).Select(x => x.代办方).ToList();
                    var users = new List<Users>();
                    if (agentNames.Any())
                        users = await _userServices.GetUsersByKeys(agentNames, AbpSession.WebSiteId);
                    var carTypeNames = EnumExtension.GetDescriptions<CarTypeEnum>();

                    #region 数据判断                  
                    foreach (var item in batchTableModels)
                    {
                        string ret = "";
                        bool checkSucssful = true;

                        if (string.IsNullOrEmpty(item.序号))
                        {
                            checkSucssful = false; ret += "序号不能为空！";
                        }
                        else if (batchTableModels.Where(x => x.序号 == item.序号).Count() >= 2)
                        {
                            checkSucssful = false; ret += $"序号：{item.序号}重复！";
                        }
                        if (string.IsNullOrEmpty(item.车牌号))
                        {
                            checkSucssful = false; ret += "车牌不能为空！";
                        }
                        if (string.IsNullOrEmpty(item.车型名称))
                        {
                            item.车型名称 = "小型汽车";
                        }

                        if (string.IsNullOrEmpty(item.车辆性质))
                        {
                            item.车辆性质 = "单位车";
                        }

                        #region 获取车型代码
                        string carTypeName = item.车型名称;
                        carTypeName = carTypeNames.Where(x => x == carTypeName).FirstOrDefault();
                        if (carTypeName == null)
                        {
                            checkSucssful = false; ret += "车型名称 格式有误！";
                        }

                        var carCodeResult = carTypeName.GetEnumByDesc<CarTypeEnum>();
                        item.车型代码 = ((int)carCodeResult).ToString().PadLeft(2, '0');
                        #endregion

                        if (string.IsNullOrEmpty(item.车架号))
                        {
                            checkSucssful = false; ret += "车架号不能为空！";
                        }
                        if (string.IsNullOrEmpty(item.发动机号))
                        {
                            checkSucssful = false; ret += "发动机号不能为空！";
                        }
                        if (string.IsNullOrWhiteSpace(item.是否超证))
                        {
                            checkSucssful = false; ret += "是否超证不能为空！";
                        }

                        //有违章
                        if (!string.IsNullOrWhiteSpace(item.违章时间) && !string.IsNullOrWhiteSpace(item.扣分))
                        {
                            if (!item.违章时间.IsDate())
                            {
                                checkSucssful = false; ret += "违章时间不是正确的日期格式！";
                            }

                            if (!item.扣分.IsNumber())
                            {
                                checkSucssful = false; ret += "扣分不是整数！";
                            }

                            if (string.IsNullOrWhiteSpace(item.违章城市))
                            {
                                checkSucssful = false; ret += "违章城市不能为空！";
                            }
                            else
                            {
                                #region 用违章城市进行反算城市代码
                                var cityQuery = CommonHelper.InByCityName(item.违章城市.ToString());
                                if (cityQuery != null)
                                {
                                    string Code = cityQuery.ID;
                                    if (Code.Length != 4)
                                        Code = Code.PadRight(4, '0');
                                    item.违章城市代码 = Code;
                                }
                                #endregion
                            }

                            if (string.IsNullOrEmpty(item.违章地点))
                            {
                                checkSucssful = false; ret += "违章地点不能为空！";
                            }

                            if (string.IsNullOrEmpty(item.违章原因))
                            {
                                checkSucssful = false; ret += "违章原因不能为空！";
                            }

                            if (string.IsNullOrEmpty(item.违法代码))
                            {
                                item.违法代码 = "";
                            }
                            else
                            {
                                if (!item.违法代码.IsDigitOrNumber())
                                {
                                    checkSucssful = false; ret += "违法代码错误！";
                                }
                                if (item.违法代码.Length > 10)
                                {
                                    checkSucssful = false; ret += "违法代码错误！";
                                }
                            }

                            if (string.IsNullOrEmpty(item.罚金))
                            {
                                checkSucssful = false; ret += "罚金不能为空！";
                            }
                            else
                            {
                                if (item.罚金.ToDecimal() == null)
                                {
                                    checkSucssful = false; ret += "罚金格式错误！";
                                }
                            }

                            if (string.IsNullOrWhiteSpace(item.滞纳金))
                            {
                                checkSucssful = false; ret += "滞纳金不能为空！";
                            }
                            else
                            {
                                if (item.滞纳金.ToDecimal() == null)
                                {
                                    checkSucssful = false; ret += "滞纳金格式错误！";
                                }
                            }

                            if (!item.文书号.IsNullOrEmpty() && item.文书号.Length > 32)
                            {
                                checkSucssful = false; ret += "文书号长度超过32位！";
                            }
                        }

                        //人工报价—服务费
                        if (!string.IsNullOrEmpty(item.手续费) && item.手续费.ToInt() > 0)
                        {
                            if (item.代办方.IsNullOrEmpty())
                            {
                                checkSucssful = false; ret += "代办方不能为空！";
                            }
                            else if (item.代办成本.IsNullOrEmpty())
                            {
                                checkSucssful = false; ret += "代办成本不能为空！";
                            }
                        }

                        if (!string.IsNullOrEmpty(item.代办成本) && decimal.TryParse(item.代办成本, out decimal dbcb))
                        {
                            if (item.代办方.IsNullOrEmpty())
                            {
                                checkSucssful = false;
                                if (!ret.Contains("代办方不能为空！"))
                                    ret += "代办方不能为空！";
                            }
                        }

                        if (!string.IsNullOrEmpty(item.代办方))
                        {
                            var userId = users.FirstOrDefault(x => (x.Shortname == item.代办方 || x.LoginName == item.代办方))?.Id;
                            if (string.IsNullOrEmpty(userId))
                            {
                                checkSucssful = false;
                                ret += $"代办方:{item.代办方}信息有误！";
                            }
                            if (item.代办成本.IsNullOrEmpty() || !decimal.TryParse(item.代办成本, out dbcb))
                            {
                                checkSucssful = false;
                                if (!ret.Contains("代办成本不能为空！"))
                                    ret += "代办成本不能为空！";
                            }
                        }

                        #region 正确与错误记录
                        if (!checkSucssful)
                        {
                            item.DataStatus = (int)ViolationDataStatusEnum.Error;

                            violationErrorInfos.Add(new ViolationErrorInfo
                            {
                                Index = item.序号,
                                Type = "错误",
                                Reason = ret
                            });
                        }
                        #endregion

                        var count = batchTableModels.Where(x => x.车牌号 == item.车牌号 && x.是否挑单 != item.是否挑单).Count();

                        if (count > 0)
                        {
                            violationErrorInfos.Add(new ViolationErrorInfo
                            {
                                Index = item.序号,
                                Type = "错误",
                                Reason = "车辆：" + item.车牌号 + "存在多种挑单方式！"
                            });
                        }
                    }
                    #endregion
                }
                else
                {
                    violationErrorInfos.Add(new ViolationErrorInfo
                    {
                        Index = "",
                        Type = "错误",
                        Reason = "导入的有效数据为空或者模板不正确"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"BatchCarViolationService.CheckError:{ex.Message}");
                return null;
            }

            return violationErrorInfos;
        }

        /// <summary>
        /// 校验重复数据
        /// </summary>
        /// <param name="batchTableModels"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        private async Task<List<string>> CheckRepeat(List<BatchTableModelDto> batchTableModels, string batchId)
        {
            var codeList = new List<string>();
            //本次导入数据是否有重复
            var thisImportData = batchTableModels.GroupBy(x => x.Uniquecode).Select(x => new { code = x.Key, count = x.Count() }).Where(x => x.count > 1).Select(x => x.code).ToList();

            //当前批次违章信息
            var oldBatchViolations = await _violationRepository.GetAllListAsync(x => x.WebSiteId == AbpSession.WebSiteId && x.BatchId == batchId);

            //其他批次违章信息
            var uCodes = batchTableModels.Where(x => x.DataStatus != (int)ViolationDataStatusEnum.Error).Select(x => x.Uniquecode).ToList();
            var otherBatchViolations = new List<BatchAskPriceViolationAgent>();
            if (uCodes != null && uCodes.Any())
                otherBatchViolations = await _violationRepository.GetAllListAsync(x => x.WebSiteId == AbpSession.WebSiteId && x.BatchId != batchId && uCodes.Contains(x.Uniquecode));

            foreach (var batchTableModel in batchTableModels)
            {
                if (batchTableModel.DataStatus == (int)ViolationDataStatusEnum.Error) continue;

                //本次导入
                if (thisImportData.Contains(batchTableModel.Uniquecode))
                {
                    batchTableModel.DataStatus = (int)ViolationDataStatusEnum.ThisBatchRepeat;
                    batchTableModel.DataErrorDesc = $"序号：{batchTableModel.序号}重复";
                    codeList.Add(batchTableModel.Uniquecode);
                }

                //当前批次
                var oldBatchViolation = oldBatchViolations.FirstOrDefault(x => x.Uniquecode == batchTableModel.Uniquecode);

                if (oldBatchViolation != null && oldBatchViolation.State != (int)ViolationStateEnum.WaitHandle
                    && oldBatchViolation.State != (int)ViolationStateEnum.Backed && oldBatchViolation.State != (int)ViolationStateEnum.ReSeted)
                {
                    batchTableModel.DataStatus = (int)ViolationDataStatusEnum.ThisBatchRepeat;
                    batchTableModel.DataErrorDesc = $"序号：{batchTableModel.序号} 数据重复";
                    codeList.Add(batchTableModel.Uniquecode);
                }

                //其他批次
                var otherBatchViolation = otherBatchViolations.Where(x => x.Uniquecode == batchTableModel.Uniquecode).ToList();

                if (otherBatchViolation == null || !otherBatchViolation.Any()) continue;

                if (otherBatchViolation.Any(x => x.State != (int)ViolationStateEnum.WaitHandle
                   && x.State != (int)ViolationStateEnum.Backed && x.State != (int)ViolationStateEnum.ReSeted))
                {
                    batchTableModel.DataStatus = (int)ViolationDataStatusEnum.OtherBatchHandling;
                    if (otherBatchViolation.Any(x => x.State == (int)ViolationStateEnum.Completed))
                        batchTableModel.DataStatus = (int)ViolationDataStatusEnum.OtherBatchCompleted;
                    codeList.Add(batchTableModel.Uniquecode);
                }
                else
                {
                    batchTableModel.DataStatus = (int)ViolationDataStatusEnum.OtherBatchRepeat;
                    codeList.Add(batchTableModel.Uniquecode);
                }
            }

            return codeList;
        }

        /// <summary>
        /// 查询车辆违章信息
        /// </summary>
        /// <param name="carInfo"></param>
        /// <param name="batchTableModel"></param>
        /// <param name="batchInfo"></param>
        /// <returns></returns>
        private async Task<List<BatchTableModelDto>> QueryViolationInfoByCarInfo(CarInfoDto carInfo, BatchTableModelDto batchTableModel, BatchInfo batchInfo)
        {
            try
            {
                //查询违章
                object postData = new
                {
                    req_data = new
                    {
                        carNumber = carInfo.CarNumber,
                        carCode = carInfo.CarCode,
                        engineCode = carInfo.EngineNo,
                        carType = ((int)carInfo.CarTypeName.GetEnumByDesc<CarTypeEnum>()).ToString().PadLeft(2, '0'),
                        carTypeName = carInfo.CarTypeName,
                        isUseHistory = 1,
                        userId = "009020201501192329000000000002",
                        provinceCode = "",
                        enumCarNature = carInfo.PrivateCar,
                        shortname = "有限公司",
                        source = "PC",
                        isCheckLock = 0
                    }
                };

                string postDataStr = postData.ToJsonString();
                string apiUrl = _apiUrlConfig.QueryViolationApiUrl;

                var httpClientRequest = new HttpClientRequest
                {
                    DataEncoding = Encoding.GetEncoding("gb2312"),
                    PostData = postDataStr,
                    ContentType = "text/plain",
                    Url = apiUrl
                };

                var httpClientResponse = await _httpClientHelper.PostStringAsync(httpClientRequest);

                var jo = httpClientResponse.Data.FromJsonString<JObject>();

                if (jo["code"].ToString() != "1")
                {
                    _logger.Error($"查询车辆违章信息出错, 参数【{postDataStr}】错误信息【{ httpClientResponse.Data}】");

                    return new List<BatchTableModelDto>();
                }

                var datajo = jo["data"].ToJsonString().FromJsonString<JObject>();

                var wzEntity = datajo["StatusObject"].ToString().FromJsonString<dynamic>();

                //有违章数据，写进违章表
                if (wzEntity.ErrorCode != "0")
                {
                    return null;
                }
                if (wzEntity.HasData == false || wzEntity.Record == null || wzEntity.Record.Count <= 0)
                {
                    return null;
                }

                var batchTableModels = new List<BatchTableModelDto>();

                var Savebpvlist = new List<BatchAskPriceViolationAgent>();
                var Updatebpvlist = new List<BatchAskPriceViolationAgent>();
                foreach (dynamic temp in wzEntity.Record)
                {
                    var batchTableMode = new BatchTableModelDto
                    {
                        车牌号 = carInfo.CarNumber,
                        车架号 = carInfo.CarCode,
                        发动机号 = carInfo.EngineNo,
                        车型名称 = carInfo.CarTypeName,
                        车型代码 = ((int)carInfo.CarTypeName.GetEnumByDesc<CarTypeEnum>()).ToString().PadLeft(2, '0'),
                        车辆性质 = carInfo.PrivateCar,
                        违章时间 = temp.Time,
                        文书号 = temp.Archive,
                        违章城市 = temp.LocationName,
                        违章地点 = temp.Location,
                        违章原因 = temp.Reason,
                        扣分 = temp.Degree,
                        罚金 = temp.count,
                        滞纳金 = temp.Latefine,
                        违法代码 = temp.Code,
                        是否挑单 = batchTableModel.是否挑单,
                        是否超证 = batchTableModel.是否超证,
                        手续费 = batchTableModel.手续费,
                        违章城市代码 = temp.Locationid,
                        Uniquecode = temp.UniqueCode
                    };

                    batchTableModels.Add(batchTableMode);
                }
                return batchTableModels;
            }
            catch (Exception ex)
            {
                _logger.Error($"QueryViolationInfoByCarInfo:{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 查询车辆违章信息
        /// </summary>
        /// <param name="batchTableModels"></param>
        /// <param name="batchInfo"></param>
        /// <returns></returns>
        private async Task<List<BatchTableModelDto>> QueryViolationInfo(List<BatchTableModelDto> batchTableModels, BatchInfo batchInfo)
        {
            //找出模板中无违章信息车辆
            var needQueryViolations = batchTableModels.Where(x => string.IsNullOrEmpty(x.违章时间) && string.IsNullOrEmpty(x.扣分))
                .GroupBy(x => new { x.车牌号, x.车架号, x.发动机号, x.车辆性质, x.车型名称 })
                .Select(x => new CarInfoDto { CarNumber = x.Key.车牌号, CarCode = x.Key.车架号, EngineNo = x.Key.发动机号, CarTypeName = x.Key.车型名称, PrivateCar = x.Key.车辆性质 }).ToList();

            foreach (var needQueryViolation in needQueryViolations)
            {
                var batchTableModel = batchTableModels.Where(x => string.IsNullOrEmpty(x.违章时间) && string.IsNullOrEmpty(x.扣分) &&
                x.车牌号 == needQueryViolation.CarNumber).FirstOrDefault();

                var carViolationInfos = await QueryViolationInfoByCarInfo(needQueryViolation, batchTableModel, batchInfo);

                carViolationInfos.ForEach(x =>
                {
                    x.代办方 = batchTableModel.代办方;
                    x.代办成本 = batchTableModel.代办成本;
                });

                batchTableModels.Remove(batchTableModel);
                if (carViolationInfos != null)
                    batchTableModels.AddRange(carViolationInfos);
            }

            return batchTableModels;
        }
        #endregion
    }
}


