using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class CityRoad
    {
        public string Id { get; set; }
        public int DataSourceId { get; set; }
        public int CityId { get; set; }
        public string RoadName { get; set; }
        public DateTime? AddDate { get; set; }
    }
}
