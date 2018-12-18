using System;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace CXY.CJS.Model
{
    /// <summary>
    /// 短信发送记录
    /// </summary>
    public class SmsSendRecord : Entity<string>
    {
        public string UserId { get; set; }

        public string Telephone { get; set; }

        public string Content { get; set; }
        public DateTime SendTime { get; set; }

        public bool Status { get; set; }

        public string Result { get; set; }

        public int SmsType { get; set; }

        public string ToUserId { get; set; }

        public string Operator { get; set; }

        public string BatchId { get; set; }

        public DateTime? BatchTime { get; set; }
    }
}