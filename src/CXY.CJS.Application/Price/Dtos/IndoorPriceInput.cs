using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using CXY.CJS.Model;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapTo(typeof(BatchInfo))]
    public class IndoorPriceInput
    {
        public string WebSiteId { get; set; }
        public string BatchId { get; set; }
        public string UserId { get; set; }

        public string CarId { get; set; }

        public string CarNumber { get; set; }
        public string EngineNo { get; set; }
        public string CarCode { get; set; }
        public string CarType { get; set; }
        public string CarTypeName { get; set; }
        public string CarNature { get; set; }
        public int IsChoose { get; set; }
        public int Islock { get; set; }
    }
}
