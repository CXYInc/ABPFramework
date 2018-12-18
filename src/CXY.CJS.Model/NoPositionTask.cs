using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class NoPositionTask
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime? PositionTime { get; set; }
        public int? PositionFlag { get; set; }
        public string PositionResult { get; set; }
    }
}
