using System;
using System.Collections.Generic;
using System.Text;

namespace CXY.CJS.Application.Dtos
{

    public class PriceResultOutput
    {
        /// <summary>
        /// 报价人
        /// </summary>
        public string UserId { get; set; }

        public string ShortName { get; set; }

        /// <summary>
        /// 报价Id
        /// </summary>
        public string PriceId { get; set; }

        /// <summary>
        /// 能否办理
        /// </summary>
        public int CanProcess { get; set; }

        public string CanprocessMsg { get; set; }
        /// <summary>
        /// 违章标识码
        /// </summary>
        public string UniqueCode { get; set; }

        /// <summary>
        ///违章办理状态  0未处理 1是处理中 2处理完毕 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 手续费，即服务费 
        /// </summary>
        public decimal Poundage { get; set; }

        /// <summary>
        /// 手价
        /// </summary>
        public decimal PlusPrice { get; set; }

        /// <summary>
        /// 加价
        /// </summary>
        public decimal ParentPlusPrice { get; set; }

        /// <summary>
        /// 违章类型 1扣分单 2 本人本证扣分单 3 非扣分单 4 BHP(X+Y)
        /// </summary>
        public int ViolationType { get; set; }

        ///// <summary>
        ///// 手续费，即服务费
        ///// </summary>
        //public decimal LockPoundage { get; set; }

        public List<FcQuery> fcQuery { get; set; }

    }

    public class FcQuery
    {
        public string ID { get; set; }

        public string PriceId { get; set; }

        public string FCUSERID { get; set; }

        public decimal FC { get; set; }

        public string FCTYPE { get; set; }

        public string WebSiteId { get; set; }

        public int ProfitType { get; set; }

        public int ViolationType { get; set; }

        public string UniqueCode { get; set; }
    }
}
