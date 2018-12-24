
using Abp.Runtime.Validation;
using CXY.CJS.Repository.SeedWork;
using System;

namespace CXY.CJS.Application.Dtos
{
    public class GetOrdersInput : Pagination
    {
        public string OrderId { get; set; }
        public string BatchId { get; set; }
        public string CarNumber { get; set; }
        /// <summary>
        /// 日期类型:下单、派单、完成
        /// </summary>
        public string DataType { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }

        public string UpString { get; set; }
        public string DownString { get; set; }

        public string OrderState { get; set; }

        public string ProxyState { get; set; }
    }
}
