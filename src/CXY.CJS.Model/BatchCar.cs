using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Model
{
    public class BatchCar : Entity<string>, IHasCreationTime
    {
        public BatchCar()
        {
            CreationTime = Clock.Now;
            IsNeedSearch = true;
            PrivateCar = false;
        }

        public string BatchId { get; set; }
        public string WebSiteId { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }
        /// <summary>
        /// 车架号
        /// </summary>
        public string CarCode { get; set; }
        /// <summary>
        /// 发动机号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 是否个人车
        /// </summary>
        public bool PrivateCar { get; set; }
        public string CarType { get; set; }
        public string CarTypeName { get; set; }
        public string IsLock { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string DriverLicense { get; set; }
        public bool IsNeedSearch { get; set; }

        public bool HaveLockRule { get; set; }

        public bool IsChoose { get; set; }

        public string ViolationMsg { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
