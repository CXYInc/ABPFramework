using System;

namespace CXY.CJS.Application.Dtos
{
    /// <summary>
    /// 代理商用户实体
    /// </summary>
    public class LowerAgentOutputItem
    {
        public string WebSiteId { get; set; }
        public string Id { get; set; }
        public string Username { get; set; }
        public string Loginname { get; set; }
        //public string Password { get; set; }
        public string PaymentPwd { get; set; }
        public string Safepassword { get; set; }
        public string Realname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreationTime { get; set; }
        public string Address { get; set; }
        public string Shortname { get; set; }
        public string Fullname { get; set; }
        public int Ispanuse { get; set; }
        public string Recommendusername { get; set; }
        public string Recommenduserid { get; set; }
        public string Cardno { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        ///// <summary>
        ///// 弃用
        ///// </summary>
        //public string UserRoleId { get; set; }

        public int? UserProvince { get; set; }
        public int IsPaymentPwd { get; set; }
        public int? Roleid { get; set; }
        public string Parentid { get; set; }
        public int? Czfwsl { get; set; }
        /// <summary>
        /// 提现账号
        /// </summary>
        public string TxZh { get; set; }
        /// <summary>
        ///  提现开户行
        /// </summary>
        public string TxKhx { get; set; }
        /// <summary>
        ///  提现收款人
        /// </summary>
        public string TxSkr { get; set; }
        /// <summary>
        /// 提现手机号码
        /// </summary>
        public string TxSj { get; set; }
        /// <summary>
        /// 是否赋予此账号非扣分单报价功能权限
        /// </summary>
        public int? Isktskd { get; set; }
         /// <summary>
        /// 是否开通违章报价
        /// </summary>
        public int? Isktdd { get; set; }
        public string Userlayer { get; set; }
        /// <summary>
        /// 下级数量
        /// </summary>
        public int? XjSl { get; set; }
        public string Dh { get; set; }
        public string Qq { get; set; }
        public string Scdlip { get; set; }
        public string Scdlsj { get; set; }
        public string Zcdlip { get; set; }
        public string Zcdlsj { get; set; }
        /// <summary>
        /// 是否开通批量导入
        /// </summary>
        public int? Isktpldr { get; set; }
        /// <summary>
        /// 查违章加报价?
        /// </summary>
        public int? Viewdd { get; set; }
        /// <summary>
        /// 是否允许此账号在微信上登录本系统
        /// </summary>
        public int? Isktweixin { get; set; }
        /// <summary>
        /// 菜单？{id: 0, text: '标准菜单' }, { id: 1, text: '租车菜单' }, { id: 2, text: '租车菜单限车牌'}
        /// </summary>
        public int? Hirecar { get; set; }

        /// <summary>
        /// 违章罚款提醒
        /// </summary>
        public int? AlertWzxxlimit { get; set; }
        /// <summary>
        /// 违章扣分提醒
        /// </summary>
        public int? AlertWzxxKflimit { get; set; }
        /// <summary>
        /// 违章条数提醒
        /// </summary>
        public int? AlertWzxxNewAdd { get; set; }
        /// <summary>
        /// 最多添加车辆数量限制
        /// </summary>
        public int? CarCount { get; set; }

        public int? UserMenuType { get; set; }
        /// <summary>
        ///  后台自动轮询
        /// </summary>
        public int? IsBackQuery { get; set; }
        public int? IsOffer { get; set; }

        public DateTime? ValidityDate { get; set; }
        /// <summary>
        /// 自动轮询间隔
        /// </summary>
        public int? BackQueryDay { get; set; }

        public int BackQueryFailAgainNumber { get; set; }
        /// <summary>
        /// 自动轮询失败车辆重查间隔
        /// </summary>
        public int BackQueryFailAgainInterval { get; set; }
        /// <summary>
        /// 自动轮询失败车辆凌晨0：30自动重查
        /// </summary>
        public bool BackQueryFailAgainMorning { get; set; }

        /// <summary>
        /// 商务负责人
        /// </summary>
        public string Swfzr { get; set; }
        public string DdCalculationType { get; set; }
        /// <summary>
        /// 是否属于优先报价入来的
        /// </summary>
        public int PriceFirst { get; set; }
        public int AutoBillShield { get; set; }
        public int? IsSendSms { get; set; }
        public string Provinceid { get; set; }
        /// <summary>
        /// 零售价服务费小于
        /// </summary>
        public decimal? WapDifferenceThreshold { get; set; }
        /// <summary>
        /// 零售价设置服务费
        /// </summary>
        public decimal? WapDifferenceAbsolute { get; set; }
        /// <summary>
        /// 零售价设置服务费百分百
        /// </summary>
        public int? WapDifferencePercentage { get; set; }
        /// <summary>
        /// 是否开通本人本证
        /// </summary>
        public int? IsOpenUsPrice { get; set; }
        /// <summary>
        /// 本人本证扣分单加价设置
        /// </summary>
        public int? UsPriceFareFirst { get; set; }

        public int? UsPriceFareSecond { get; set; }
        /// <summary>
        /// 是否接单
        /// </summary>
        public int? IsGetOrder { get; set; }

        public int IsOpenExaminedPrice { get; set; }
        /// <summary>
        /// 年审上线检加价设置
        /// </summary>
        public int? OnlineExaminedPrice { get; set; }
        /// <summary>
        /// 年审非上线检加价设置
        /// </summary>
        public int? UnOnlineExaminedPrice { get; set; }
        /// <summary>
        /// 开启系统订单类短信通知
        /// </summary>
        public int IsReceiveOrderSms { get; set; }
        /// <summary>
        /// 开启系统资金类短信通知
        /// </summary>
        public int IsReceiveCapitalSms { get; set; }
        /// <summary>
        /// 开启系统资产类短信通知
        /// </summary>
        public int IsReceiveAssetsSms { get; set; }
        /// <summary>
        /// 开启系统权限申请类短信通知
        /// </summary>
        public int IsReceivePowerSms { get; set; }
        public int? PointsBillShield { get; set; }

        /// <summary>
        /// 年审到期提醒时间
        /// </summary>
        public int AnnualExpireReminder { get; set; }

        /// <summary>
        /// 交强险到期提醒时间
        /// </summary>
        public int CompulsorExpireReminder { get; set; }
        /// <summary>
        /// 商业险到期提醒时间
        /// </summary>
        public int CommercialExpireReminder { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int? IsRevice { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        /// <summary>
        /// H5 自动充值
        /// </summary>
        public bool H5AutoRecharge { get; set; }
        public string WeChatSubscription { get; set; }
        public bool WeiXinPayH5 { get; set; }
        public string Operator { get; set; }
        public string CxySellerAppUserId { get; set; }
        /// <summary>
        /// 是否开通扣分单下单
        /// </summary>
        public int? IsOpenKfoffer { get; set; }
        /// <summary>
        /// 是否为神州
        /// </summary>
        public int? IsShenZhou { get; set; }
        /// <summary>
        /// 是否开通不扣分单下单
        /// </summary>
        public int? IsOpenBkfoffer { get; set; }
        /// <summary>
        /// 余额警告阀值
        /// </summary>
        public int? WarnYe { get; set; }
        /// <summary>
        /// 短信警告阀值
        /// </summary>
        public int? WarnDx { get; set; }
        public int BatchAskPriceMenu { get; set; }
        public int IsInvoice { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 开票金额类型
        /// </summary>
        public int RateType { get; set; }
        /// <summary>
        /// 当日赠送积分
        /// </summary>
        public int? Drzsjf { get; set; }
        /// <summary>
        /// 设置为当天，明天开始送
        /// </summary>
        public DateTime Drzsrq { get; set; }

        public int? DrzssyJf { get; set; }
        /// <summary>
        /// 剩余积分
        /// </summary>
        public int? WdJf { get; set; }
        /// <summary>
        ///日积分
        /// </summary>
        public int? RzsJf { get; set; }

        public decimal? JfPrice { get; set; }

        public decimal? NotePrice { get; set; }

        public int NoteNumber { get; set; }
        /// <summary>
        /// 月赠送积分
        /// </summary>
        public int? GivePointsPerMonth { get; set; }
        /// <summary>
        /// 实际赠送积分
        /// </summary>
        public int? GivePointsSameMonth { get; set; }

        /// <summary>
        /// 超级总站赠积分
        /// </summary>
        public int? GivePointsSurplusSameMonth { get; set; }

        /// <summary>
        /// 当前余额
        /// </summary>
        public decimal? Wdye { get; set; }
        /// <summary>
        /// 透支额度
        /// </summary>
        public decimal? Overdrftamount { get; set; }
        /// <summary>
        /// 扣分单利润JSON版
        /// </summary>
        public string ProfitJson { get; set; }
        /// <summary>
        /// 扣分单利润INT版
        /// </summary>
        public decimal? Gdlr { get; set; }
        /// <summary>
        /// 扣分单价格优先类型
        /// </summary>
        public int? PriceFirstType { get; set; }
        /// <summary>
        /// 收款银行列
        /// </summary>
        public string Txyh { get; set; }
        public int? UserCity { get; set; }
    }
}