
using System;
using System.ComponentModel.DataAnnotations;

namespace CXY.CJS.Application.Dtos
{
    public class OrderEditDto
    {

        /// <summary>
        /// Id
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        public string Id { get; set; }



        /// <summary>
        /// WebSiteId
        /// </summary>
        [Required(ErrorMessage = "WebSiteId不能为空")]
        public string WebSiteId { get; set; }



        /// <summary>
        /// BatchId
        /// </summary>
        [Required(ErrorMessage = "BatchId不能为空")]
        public string BatchId { get; set; }



        /// <summary>
        /// CurrentBatchId
        /// </summary>
        public string CurrentBatchId { get; set; }



        /// <summary>
        /// ViolationId
        /// </summary>
        public string ViolationId { get; set; }



        /// <summary>
        /// PrefixCarNum
        /// </summary>
        public string PrefixCarNum { get; set; }



        /// <summary>
        /// CarNumber
        /// </summary>
        public string CarNumber { get; set; }



        /// <summary>
        /// VehicleType
        /// </summary>
        public string VehicleType { get; set; }



        /// <summary>
        /// VehicleTypeName
        /// </summary>
        public string VehicleTypeName { get; set; }



        /// <summary>
        /// VINNO
        /// </summary>
        public string VINNO { get; set; }



        /// <summary>
        /// EngineNum
        /// </summary>
        public string EngineNum { get; set; }



        /// <summary>
        /// ViolationTime
        /// </summary>
        public DateTime ViolationTime { get; set; }



        /// <summary>
        /// ViolationCity
        /// </summary>
        public string ViolationCity { get; set; }



        /// <summary>
        /// DocumentNum
        /// </summary>
        public string DocumentNum { get; set; }



        /// <summary>
        /// LocationId
        /// </summary>
        public string LocationId { get; set; }



        /// <summary>
        /// ViolationLocale
        /// </summary>
        public string ViolationLocale { get; set; }



        /// <summary>
        /// ViolationType
        /// </summary>
        public string ViolationType { get; set; }



        /// <summary>
        /// OrderType
        /// </summary>
        public string OrderType { get; set; }



        /// <summary>
        /// ViolationCode
        /// </summary>
        public string ViolationCode { get; set; }



        /// <summary>
        /// Degree
        /// </summary>
        public int Degree { get; set; }



        /// <summary>
        /// Fine
        /// </summary>
        public decimal Fine { get; set; }



        /// <summary>
        /// LateFine
        /// </summary>
        public decimal LateFine { get; set; }



        /// <summary>
        /// ServiceCharge
        /// </summary>
        public decimal ServiceCharge { get; set; }



        /// <summary>
        /// TotalFee
        /// </summary>
        public decimal TotalFee { get; set; }



        /// <summary>
        /// ProxyFine
        /// </summary>
        public decimal ProxyFine { get; set; }



        /// <summary>
        /// ProxyServiceCharge
        /// </summary>
        public decimal ProxyServiceCharge { get; set; }



        /// <summary>
        /// ProxyTotalFee
        /// </summary>
        public decimal ProxyTotalFee { get; set; }



        /// <summary>
        /// MakeFine
        /// </summary>
        public decimal MakeFine { get; set; }



        /// <summary>
        /// MakeLateFine
        /// </summary>
        public decimal MakeLateFine { get; set; }



        /// <summary>
        /// MakeServiceCharge
        /// </summary>
        public decimal MakeServiceCharge { get; set; }



        /// <summary>
        /// Proxy
        /// </summary>
        public string Proxy { get; set; }



        /// <summary>
        /// Proxyname
        /// </summary>
        public string Proxyname { get; set; }



        /// <summary>
        /// ProxyTime
        /// </summary>
        public DateTime? ProxyTime { get; set; }



        /// <summary>
        /// PayType
        /// </summary>
        public string PayType { get; set; }



        /// <summary>
        /// UniqueCode
        /// </summary>
        public string UniqueCode { get; set; }



        /// <summary>
        /// PrivateFlag
        /// </summary>
        public bool PrivateFlag { get; set; }



        /// <summary>
        /// ReviseId
        /// </summary>
        public string ReviseId { get; set; }



        /// <summary>
        /// PriceSource
        /// </summary>
        public string PriceSource { get; set; }



        /// <summary>
        /// ViolationReson
        /// </summary>
        public string ViolationReson { get; set; }



        /// <summary>
        /// Difference
        /// </summary>
        public decimal Difference { get; set; }



        /// <summary>
        /// VAT
        /// </summary>
        public decimal VAT { get; set; }



        /// <summary>
        /// PriceBasis
        /// </summary>
        public string PriceBasis { get; set; }



        /// <summary>
        /// DriverName
        /// </summary>
        public string DriverName { get; set; }



        /// <summary>
        /// DriverPhone
        /// </summary>
        public string DriverPhone { get; set; }



        /// <summary>
        /// DriverNo
        /// </summary>
        public string DriverNo { get; set; }



        /// <summary>
        /// TailUserId
        /// </summary>
        public string TailUserId { get; set; }



        /// <summary>
        /// TailUserName
        /// </summary>
        public string TailUserName { get; set; }



        /// <summary>
        /// PriceFrom
        /// </summary>
        public int PriceFrom { get; set; }



        /// <summary>
        /// OrderSource
        /// </summary>
        public int OrderSource { get; set; }



        /// <summary>
        /// TaxRate
        /// </summary>
        public decimal TaxRate { get; set; }



        /// <summary>
        /// OrderByNo
        /// </summary>
        public int OrderByNo { get; set; }



        /// <summary>
        /// State
        /// </summary>
        public int State { get; set; }



        /// <summary>
        /// NeedMakeUpPrice
        /// </summary>
        public int NeedMakeUpPrice { get; set; }



        /// <summary>
        /// MakeUpTimes
        /// </summary>
        public int MakeUpTimes { get; set; }



        /// <summary>
        /// NeedMakeUpData
        /// </summary>
        public int NeedMakeUpData { get; set; }



        /// <summary>
        /// NeedMakeDataEnum
        /// </summary>
        public string NeedMakeDataEnum { get; set; }



        /// <summary>
        /// AlreadyMakeDataEnum
        /// </summary>
        public string AlreadyMakeDataEnum { get; set; }



        /// <summary>
        /// UserMemo
        /// </summary>
        public string UserMemo { get; set; }



        /// <summary>
        /// IsDestory
        /// </summary>
        public int IsDestory { get; set; }



        /// <summary>
        /// DestoryRemark
        /// </summary>
        public string DestoryRemark { get; set; }



        /// <summary>
        /// Apply
        /// </summary>
        public string Apply { get; set; }



        /// <summary>
        /// Applytime
        /// </summary>
        public string Applytime { get; set; }



        /// <summary>
        /// PassId
        /// </summary>
        public string PassId { get; set; }



        /// <summary>
        /// PassMemo
        /// </summary>
        public string PassMemo { get; set; }



        /// <summary>
        /// ExportNum
        /// </summary>
        public int ExportNum { get; set; }



        /// <summary>
        /// CreatorUserId
        /// </summary>
        public string CreatorUserId { get; set; }



        /// <summary>
        /// CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }



        /// <summary>
        /// LastModifierUserId
        /// </summary>
        public string LastModifierUserId { get; set; }



        /// <summary>
        /// LastModificationTime
        /// </summary>
        public DateTime? LastModificationTime { get; set; }



        /// <summary>
        /// DeleterUserId
        /// </summary>
        public string DeleterUserId { get; set; }



        /// <summary>
        /// DeletionTime
        /// </summary>
        public DateTime? DeletionTime { get; set; }



        /// <summary>
        /// IsDeleted
        /// </summary>
        public bool IsDeleted { get; set; }



        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }




    }
}