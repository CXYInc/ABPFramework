using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.ObjectMapping;
using Castle.Core.Logging;
using CXY.CJS.Application.Dtos;
using CXY.CJS.Core.Common;
using CXY.CJS.Core.Constant;
using CXY.CJS.Core.Enums;
using CXY.CJS.Core.Extensions;
using CXY.CJS.Core.NPOI;
using CXY.CJS.Core.WebApi;
using CXY.CJS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace CXY.CJS.Application
{
    /// <summary>
    /// 客服违章服务
    /// </summary>
    [Route(CJSConsts.RoutePrefix + "/BatchCarViolation/[action]")]
    [AllowAnonymous]
    public class BatchCarViolationService : CJSAppServiceBase, IBatchCarViolationService
    {
        private readonly IRepository<BatchCar, string> _entityRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly ILogger _logger;

        private readonly IUserServices _userServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="entityRepository"></param>
        /// <param name="objectMapper"></param>
        /// <param name="logger"></param>
        public BatchCarViolationService(IRepository<BatchCar, string> entityRepository, IObjectMapper objectMapper, ILogger logger, IUserServices userServices)
        {
            _entityRepository = entityRepository;
            _objectMapper = objectMapper;
            _logger = logger;
            _userServices = userServices;
        }

        /// <summary>
        /// 获取BatchCar的分页列表信息
        ///</summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PagedResultDto<BatchCarListDto>> GetPaged(GetBatchCarsInput input)
        {

            var query = _entityRepository.GetAll();
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
            var entity = await _entityRepository.GetAsync(input.Id);

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
                var entity = await _entityRepository.GetAsync(input.Id);

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


            entity = await _entityRepository.InsertAsync(entity);
            return entity.MapTo<BatchCarEditDto>();
        }

        /// <summary>
        /// 编辑BatchCar
        /// </summary>
        [HttpPost]
        protected virtual async Task Update(BatchCarEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _entityRepository.GetAsync(input.Id);
            input.MapTo(entity);

            // ObjectMapper.Map(input, entity);
            await _entityRepository.UpdateAsync(entity);
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
            await _entityRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除BatchCar的方法
        /// </summary>
        [HttpPost]
        public async Task BatchDelete(List<string> input)
        {
            // TODO:批量删除前的逻辑判断，是否允许删除
            await _entityRepository.DeleteAsync(s => input.Contains(s.Id));
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
        public ApiResult<IList<ViolationErrorInfo>> ImportViolations([FromForm]ImportViolationDto importViolationDto)
        {
            var ds = NPOIExcelHelper.ReadExcel(importViolationDto.File);

            var dt = ds.Tables["订单信息"];

            var list = dt.ConvertToModel<BatchTableModelDto>().ToList();

            list.ForEach(x =>
            {
                x.Uniquecode = CommonHelper.GenerateViolationCode(x.车牌号, x.违章时间, x.违章原因);
                x.BatchId = importViolationDto.BatchId;
                x.CreateUserId = AbpSession.UserId;
                x.CreateUserName = AbpSession.UserName;
                x.WebSiteId = AbpSession.WebSiteId;
            });

            var errors = CheckError(list);

            var t = _objectMapper.Map<List<BatchAskPriceViolationAgent>>(list);

            return new ApiResult<IList<ViolationErrorInfo>>().Success(errors);
        }

        /// <summary>
        /// 检测错误数据
        /// </summary>
        /// <param name="batchTableModels"></param>
        private List<ViolationErrorInfo> CheckError(List<BatchTableModelDto> batchTableModels)
        {
            var violationErrorInfos = new List<ViolationErrorInfo>();

            try
            {
                if (batchTableModels != null && batchTableModels.Count > 0)
                {
                    //查询代办人列表
                    var agentNames = batchTableModels.Where(x => !string.IsNullOrEmpty(x.代办方)).Select(x => x.代办方).ToList();
                    var users = _userServices.GetUsersByKeys(agentNames, AbpSession.WebSiteId).Result;
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
        /// <param name="batchTableModelDtos"></param>
        private void CheckRepeat(List<BatchTableModelDto> batchTableModelDtos)
        {

        }
        #endregion
    }
}


