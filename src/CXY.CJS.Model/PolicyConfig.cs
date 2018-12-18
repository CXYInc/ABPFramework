using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class PolicyConfig
    {
        public string Id { get; set; }
        public string CarNumberPrefixOne { get; set; }
        public string CarNumberPrefixTwo { get; set; }
        public string CarType { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityId { get; set; }
        public DateTime? InvalidBeginTime { get; set; }
        public DateTime? InvalidEndTime { get; set; }
        public string ViolationCode { get; set; }
        public int? Points { get; set; }
        public int? Level { get; set; }
        public int Result { get; set; }
        public string Remark { get; set; }
    }
}
