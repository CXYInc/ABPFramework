using System;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 用户积分
    /// </summary>
    public class UserJf : Entity<string>
    {
        public string WebSiteId { get; set; }
        /// <summary>
        /// 当日赠送积分
        /// </summary>
        public int? Drzsjf { get; set; }
        /// <summary>
        /// 设置为当天，明天开始送
        /// </summary>
        public DateTime Drzsrq { get; set; }
    
        public int? DrzssyJf { get; set; }
        /// <summary>
        /// 剩余积分
        /// </summary>
        public int? WdJf { get; set; }
        /// <summary>
        ///日积分
        /// </summary>
        public int? RzsJf { get; set; }
        /// <summary>
        /// 超级总站赠积分
        /// </summary>
        public int? GivePointsSurplusSameMonth { get; set; }
        /// <summary>
        /// 月赠送积分
        /// </summary>
        public int? GivePointsPerMonth { get; set; }
        /// <summary>
        /// 实际赠送积分
        /// </summary>
        public int? GivePointsSameMonth { get; set; }

        public int NoteNumber { get; set; }

        public decimal? JfPrice { get; set; }

        public decimal? NotePrice { get; set; }
    }
}