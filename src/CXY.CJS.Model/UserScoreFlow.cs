using Abp.Domain.Entities;
using System;

namespace CXY.CJS.Model
{
    public class UserScoreFlow : Entity<string>
    {
        public string WebSiteId { get; set; }
        public string Jfid { get; set; }
        public DateTime? Created { get; set; }
        public int? Fsjf { get; set; }
        public int? Fshjf { get; set; }
        public string Memo { get; set; }
        public string Jftx { get; set; }
        public int? State { get; set; }
        public string Fromuserid { get; set; }
        public string Dh { get; set; }
        public int PointsType { get; set; }
        public int? FlowType { get; set; }
        public string Operator { get; set; }
    }
}
