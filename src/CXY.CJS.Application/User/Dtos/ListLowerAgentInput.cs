using System;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using CXY.CJS.Repository.SeedWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CXY.CJS.Application.Dtos
{
    public class ListLowerAgentInput: Pagination
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public  string WebSiteId { get; set; }

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
        public DateTime? Start { get; set; }/* = DateTime.Now.AddYears(-10);*/

        /// <summary>
        ///  有效期终止时间
        /// </summary>
        [JsonProperty("txtend")]
        public DateTime? End { get; set; }/* = DateTime.Now.AddDays(1);*/

        /// <summary>
        /// 最小余额
        /// </summary>
        [JsonProperty("txtminwdye")]
        public decimal? MinWdye { get; set; }/* = -10000000;*/

        /// <summary>
        /// 最大余额
        /// </summary>
        [JsonProperty("txtmaxwdye")]
        public decimal? MaxWdye { get; set; }/* = 10000000;*/
    }
}