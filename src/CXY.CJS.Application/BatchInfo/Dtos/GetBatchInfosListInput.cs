using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CXY.CJS.Repository.SeedWork;

namespace CXY.CJS.Application.Dtos
{
    public class GetBatchInfosListInput : Pagination
    {
        public  string Proxy { get; set; }

        public string Id { get; set; }
        //[Required]
        //public string WebSiteId { get; set; }

        public IEnumerable<int> Status { get; set; }

        public BatchInfosListInputTimeEnum TimeEnum { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTimeTime { get; set; }
    }

    public enum BatchInfosListInputTimeEnum
    {
        CreationTime, CompleteTime
    }
}