using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public class Orders : Entity<string>
    {
        public string WebSiteId { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchId { get; set; }

        /// <summary>
        /// 当前支付批次号
        /// </summary>
        public string CurrentBatchId { get; set; }

        /// <summary>
        /// 违章Id
        /// </summary>
        public string ViolationId { get; set; }

        /// <summary>
        /// 车牌前缀
        /// </summary>
        public string PrefixCarNum { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public string VehicleType { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string VehicleTypeName { get; set; }
        /// <summary>
        /// 车架号VIN NO
        /// </summary>
        public string VINNO { get; set; }
        public string EngineNum { get; set; }
        /// <summary>
        /// 违章时间
        /// </summary>
        public DateTime ViolationTime { get; set; }
        /// <summary>
        /// 违章城市
        /// </summary>
        public string ViolationCity { get; set; }
        /// <summary>
        /// 文书编号
        /// </summary>
        public string DocumentNum { get; set; }

        /// <summary>
        /// 违章地点ID
        /// </summary>
        public string LocationId { get; set; }
        /// <summary>
        /// 违章地点
        /// </summary>
        public string ViolationLocale { get; set; }
        /// <summary>
        /// 违法类别 现场单、已处理未缴费
        /// </summary>
        public string ViolationType { get; set; }

        /// <summary>
        /// 订单类型 扣分单/非扣分单
        /// </summary>
        public string OrderType { get; set; }
        /// <summary>
        /// 违章代码
        /// </summary>
        public string ViolationCode { get; set; }
        /// <summary>
        /// 扣分
        /// </summary>
        public int Degree { get; set; }

        /// <summary>
        /// 罚金
        /// </summary>
        public decimal Fine { get; set; }

        /// <summary>
        /// 滞纳金
        /// </summary>
        public decimal LateFine { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>     
        public decimal ServiceCharge { get; set; }

        /// <summary>
        /// 收单合计费用
        /// </summary>
        public decimal TotalFee { get; set; }

        /// <summary>
        /// 代办罚金
        /// </summary>
        public decimal ProxyFine { get; set; }

        /// <summary>
        /// 代办服务费
        /// </summary>
        public decimal ProxyServiceCharge { get; set; }

        /// <summary>
        /// 代办合计费用
        /// </summary>
        public decimal ProxyTotalFee { get; set; }

        /// <summary>
        /// 补罚金
        /// </summary>
        public decimal MakeFine { get; set; }

        /// <summary>
        /// 补滞纳金
        /// </summary>
        public decimal MakeLateFine { get; set; }

        /// <summary>
        /// 补服务费
        /// </summary>
        public decimal MakeServiceCharge { get; set; }

        /// <summary>
        /// 办单代理
        /// </summary>
        public string Proxy { get; set; }
        /// <summary>
        /// 办单代理名
        /// </summary>
        public string Proxyname { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public DateTime? ProxyTime { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 唯一码
        /// </summary>
        public string UniqueCode { get; set; }
        /// <summary>
        /// 是否个人车
        /// </summary>
        public bool PrivateFlag { get; set; }
        /// <summary>
        /// 罚款修正Id
        /// </summary>
        public string ReviseId { get; set; }
        /// <summary>
        /// 价格来源地:车牌地/违章地
        /// </summary>
        public string PriceSource { get; set; }
        /// <summary>
        /// 违章原因
        /// </summary>
        public string ViolationReson { get; set; }
        /// <summary>
        /// 零售加价
        /// </summary>
        public decimal Difference { get; set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public decimal VAT { get; set; }
        /// <summary>
        /// 价格依据
        /// </summary>
        public string PriceBasis { get; set; }
        /// <summary>
        /// 驾驶证人
        /// </summary>
        public string DriverName { get; set; }
        /// <summary>
        /// 驾驶证手机号
        /// </summary>
        public string DriverPhone { get; set; }
        /// <summary>
        /// 驾驶证号
        /// </summary>
        public string DriverNo { get; set; }
        /// <summary>
        /// 跟单人
        /// </summary>
        public string TailUserId { get; set; }
        public string TailUserName { get; set; }
        /// <summary>
        /// 价格来源
        /// </summary>
        public int PriceFrom { get; set; }
        /// <summary>
        /// 订单来源
        /// </summary>
        public int OrderSource { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 导入序号
        /// </summary>
        public int OrderByNo { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 补款申请 0无需1需要2已补
        /// </summary>
        public int NeedMakeUpPrice { get; set; }
        /// <summary>
        /// 补款次数
        /// </summary>
        public int MakeUpTimes { get; set; }
        /// <summary>
        /// 补资料申请0无需1需要2已补
        /// </summary>
        public int NeedMakeUpData { get; set; }
        /// <summary>
        /// 需要补充内容
        /// </summary>
        public string NeedMakeDataEnum { get; set; }
        /// <summary>
        /// 已补充内容
        /// </summary>
        public string AlreadyMakeDataEnum { get; set; }

        /// <summary>
        /// 用户备注
        /// </summary>
        public string UserMemo { get; set; }
        /// <summary>
        /// 是否核销
        /// </summary>
        public int IsDestory { get; set; }

        public string DestoryRemark { get; set; }

        public string Apply { get; set; }

        public string Applytime { get; set; }

        public string PassId { get; set; }

        public string PassMemo { get; set; }

        /// <summary>
        /// 导出次数
        /// </summary>
        public int ExportNum { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string LastModifierUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public string DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public string Remark { get; set; } 
    }
}
