using Abp.AutoMapper;

namespace CXY.CJS.Application.Dtos
{
    [AutoMapTo(typeof(Model.Users))]
    public class CreateUserInput
    {
        public string WebsiteId { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 代理商户
        /// </summary>
        public string Shortname { get; set; }
        /// <summary>
        /// 代理商简称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string WebSiteId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int UserType { get; set; }

        public string RecommendUserName { get; set; }

        public string RecommendUserid { get; set; }

        public string CardNo { get; set; }


        public string Address { get; set; }

        public int? UserProvince { get; set; }
        public int? UserCity { get; set; }
        public string PaymentPwd { get; set; }

        /// <summary>
        /// 是否开启支付密码
        /// </summary>
        public int? IsPaymentPwd { get; set; }
    }
}