using System;
using System.Collections.Generic;

namespace CXY.CJS.Model
{
    public  class BatchAskPricre
    {
        public string Id { get; set; }
        public string IncBatchId { get; set; }
        public string ViolationId { get; set; }
        public string CarNumber { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public int? PriceFrom { get; set; }
        public DateTime? CreateTime { get; set; }
        public string PriceId { get; set; }
    }
}
