using System;
using CXY.CJS.Repository.SeedWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CXY.CJS.Application.Dtos
{
    public class ListLowerAgentInput: Pagination
    {
        public string Key { get; set; }
        /// <summary>
        /// 商务负责人
        /// </summary>
        [JsonProperty("txtswfzr")]
        public string Swfzr { get; set; }

        /// <summary>
        ///  有效期起始时间
        /// </summary>
        [JsonProperty("txtstart")]
        public DateTime Start { get; set; }

        /// <summary>
        ///  有效期终止时间
        /// </summary>
        [JsonProperty("txtend")]
        public DateTime End { get; set; }

        /// <summary>
        /// 最小余额
        /// </summary>
        [JsonProperty("txtminwdye")]
        public string MinWdye { get; set; }

        /// <summary>
        /// 最大余额
        /// </summary>
        [JsonProperty("txtmaxwdye")]
        public string MaxWdye { get; set; }

    }
}