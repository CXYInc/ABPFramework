using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.EntityFrameworkCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchAskPriceViolationAgents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    BatchId = table.Column<string>(nullable: true),
                    CarId = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    ViolationTime = table.Column<DateTime>(nullable: false),
                    Archive = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Degree = table.Column<int>(nullable: false),
                    Count = table.Column<decimal>(nullable: false),
                    Latefine = table.Column<decimal>(nullable: false),
                    Poundage = table.Column<decimal>(nullable: true),
                    FirstPoundage = table.Column<decimal>(nullable: true),
                    IsAskPrice = table.Column<bool>(nullable: false),
                    Uniquecode = table.Column<string>(nullable: true),
                    LastTimePoundage = table.Column<decimal>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    FavorablePriceInfo = table.Column<string>(nullable: true),
                    OrderJsonSelectId = table.Column<string>(nullable: true),
                    CanProcess = table.Column<int>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Vat = table.Column<decimal>(nullable: true),
                    LockPoundage = table.Column<decimal>(nullable: true),
                    CommonPoundage = table.Column<decimal>(nullable: true),
                    Ddbjid = table.Column<string>(nullable: true),
                    PriceFrom = table.Column<int>(nullable: true),
                    OrderByNo = table.Column<int>(nullable: true),
                    AgentUserId = table.Column<string>(nullable: true),
                    AgentPrice = table.Column<decimal>(nullable: true),
                    AgentUserName = table.Column<string>(nullable: true),
                    CanprocessMsg = table.Column<string>(nullable: true),
                    DataStatus = table.Column<int>(nullable: true),
                    ViolationType = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ProxyRemarks = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchAskPriceViolationAgents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchCars",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    BatchId = table.Column<string>(nullable: true),
                    WebSiteId = table.Column<string>(maxLength: 6, nullable: true),
                    CarNumber = table.Column<string>(maxLength: 32, nullable: true),
                    CarCode = table.Column<string>(maxLength: 32, nullable: true),
                    EngineNo = table.Column<string>(maxLength: 32, nullable: true),
                    PrivateCar = table.Column<bool>(maxLength: 32, nullable: false),
                    CarType = table.Column<string>(maxLength: 32, nullable: true),
                    CarTypeName = table.Column<string>(maxLength: 32, nullable: true),
                    IsLock = table.Column<int>(maxLength: 32, nullable: false),
                    DriverName = table.Column<string>(maxLength: 32, nullable: true),
                    DriverPhone = table.Column<string>(maxLength: 32, nullable: true),
                    DriverLicense = table.Column<string>(maxLength: 32, nullable: true),
                    IsNeedSearch = table.Column<bool>(maxLength: 32, nullable: false),
                    HaveLockRule = table.Column<bool>(maxLength: 32, nullable: false),
                    IsChoose = table.Column<bool>(maxLength: 32, nullable: false),
                    ViolationMsg = table.Column<string>(maxLength: 32, nullable: true),
                    CreationTime = table.Column<DateTime>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchInfos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    CarCount = table.Column<int>(nullable: false),
                    ViolationCount = table.Column<int>(nullable: false),
                    NeedPriceCount = table.Column<int>(nullable: false),
                    HadPriceCount = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    Customer = table.Column<string>(nullable: true),
                    Proxy = table.Column<string>(nullable: true),
                    ProxyUserId = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CompleteTime = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    DeleterUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BatchInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarViolationDivisions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    ViolationId = table.Column<string>(nullable: true),
                    Fcuserid = table.Column<string>(nullable: true),
                    Fc = table.Column<decimal>(nullable: true),
                    Gdlr = table.Column<decimal>(nullable: true),
                    Fctype = table.Column<string>(nullable: true),
                    CalculationExpression = table.Column<string>(nullable: true),
                    ProfitType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarViolationDivisions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataSeeds",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    SeedIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSeeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ParentId = table.Column<string>(nullable: true),
                    MenuName = table.Column<string>(nullable: true),
                    MenuLeval = table.Column<int>(nullable: false),
                    MenuUrl = table.Column<string>(nullable: true),
                    MenuLayer = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsSys = table.Column<bool>(nullable: false),
                    IsOut = table.Column<bool>(nullable: false),
                    IsParent = table.Column<bool>(nullable: false),
                    TargetFrame = table.Column<string>(nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NoticeType = table.Column<int>(nullable: false),
                    NoticeTitle = table.Column<string>(nullable: true),
                    WebSiteId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    FromUserId = table.Column<string>(nullable: true),
                    NoticeContent = table.Column<string>(nullable: true),
                    Staus = table.Column<int>(nullable: false),
                    ValidityDate = table.Column<DateTime>(nullable: false),
                    IsRead = table.Column<int>(nullable: true),
                    FromUserName = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    MenuId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatorUserId = table.Column<long>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmsSendRecords",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    SmsType = table.Column<int>(nullable: false),
                    ToUserId = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    BatchId = table.Column<string>(nullable: true),
                    BatchTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsSendRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 65, nullable: false),
                    Name = table.Column<string>(maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserMarkupSettings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    Userid = table.Column<string>(nullable: true),
                    Gdlr = table.Column<decimal>(nullable: true),
                    ProfitJson = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMarkupSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    RealName = table.Column<string>(nullable: true),
                    Shortname = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    LoginName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Safepassword = table.Column<string>(nullable: true),
                    WebSiteId = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    CreatorUserId = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<string>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Ispanuse = table.Column<int>(nullable: true),
                    RecommendUserName = table.Column<string>(nullable: true),
                    RecommendUserid = table.Column<string>(nullable: true),
                    CardNo = table.Column<string>(nullable: true),
                    Isdelete = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    UserProvince = table.Column<int>(nullable: true),
                    UserCity = table.Column<int>(nullable: true),
                    PaymentPwd = table.Column<string>(nullable: true),
                    IsPaymentPwd = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserScoreFlows",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    Jfid = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Fsjf = table.Column<int>(nullable: true),
                    Fshjf = table.Column<int>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Jftx = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: true),
                    Fromuserid = table.Column<string>(nullable: true),
                    Dh = table.Column<string>(nullable: true),
                    PointsType = table.Column<int>(nullable: false),
                    FlowType = table.Column<int>(nullable: true),
                    Operator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScoreFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserScores",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    Drzsjf = table.Column<int>(nullable: true),
                    Drzsrq = table.Column<DateTime>(nullable: false),
                    DrzssyJf = table.Column<int>(nullable: true),
                    WdJf = table.Column<int>(nullable: true),
                    RzsJf = table.Column<int>(nullable: true),
                    GivePointsSurplusSameMonth = table.Column<int>(nullable: true),
                    GivePointsPerMonth = table.Column<int>(nullable: true),
                    GivePointsSameMonth = table.Column<int>(nullable: true),
                    NoteNumber = table.Column<int>(nullable: false),
                    JfPrice = table.Column<decimal>(nullable: true),
                    NotePrice = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserScores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSysSettings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    CzfwSl = table.Column<int>(nullable: true),
                    TxZh = table.Column<string>(nullable: true),
                    TxKhx = table.Column<string>(nullable: true),
                    TxSkr = table.Column<string>(nullable: true),
                    TxSj = table.Column<string>(nullable: true),
                    IsktSkd = table.Column<int>(nullable: true),
                    IsktDd = table.Column<int>(nullable: true),
                    Userlayer = table.Column<string>(nullable: true),
                    XjSl = table.Column<int>(nullable: true),
                    Dh = table.Column<string>(nullable: true),
                    Qq = table.Column<string>(nullable: true),
                    ScDlIp = table.Column<string>(nullable: true),
                    ScDlSj = table.Column<string>(nullable: true),
                    ZcDlIp = table.Column<string>(nullable: true),
                    ZcdlSj = table.Column<string>(nullable: true),
                    IsktPldr = table.Column<int>(nullable: true),
                    ViewDd = table.Column<int>(nullable: true),
                    IsktWeixin = table.Column<int>(nullable: true),
                    HireCar = table.Column<int>(nullable: true),
                    AlertWzxxLimit = table.Column<int>(nullable: true),
                    AlertWzxxKfLimit = table.Column<int>(nullable: true),
                    AlertWzxxNewAdd = table.Column<int>(nullable: true),
                    CarCount = table.Column<int>(nullable: true),
                    UserMenuType = table.Column<int>(nullable: true),
                    IsBackQuery = table.Column<int>(nullable: true),
                    IsOffer = table.Column<int>(nullable: true),
                    ValidityDate = table.Column<DateTime>(nullable: true),
                    BackQueryDay = table.Column<int>(nullable: true),
                    BackQueryBeginTime = table.Column<DateTime>(nullable: false),
                    BackQueryFailAgainNumber = table.Column<int>(nullable: false),
                    BackQueryFailAgainInterval = table.Column<int>(nullable: false),
                    BackQueryFailAgainMorning = table.Column<bool>(nullable: false),
                    Swfzr = table.Column<string>(nullable: true),
                    DdCalculationType = table.Column<string>(nullable: true),
                    PriceFirst = table.Column<int>(nullable: false),
                    AutoBillShield = table.Column<int>(nullable: false),
                    IsSendSms = table.Column<int>(nullable: true),
                    Provinceid = table.Column<string>(nullable: true),
                    WapDifferenceThreshold = table.Column<decimal>(nullable: true),
                    WapDifferenceAbsolute = table.Column<decimal>(nullable: true),
                    WapDifferencePercentage = table.Column<int>(nullable: true),
                    IsOpenUsPrice = table.Column<int>(nullable: true),
                    UsPriceFareFirst = table.Column<int>(nullable: true),
                    UsPriceFareSecond = table.Column<int>(nullable: true),
                    IsGetOrder = table.Column<int>(nullable: true),
                    IsOpenExaminedPrice = table.Column<int>(nullable: false),
                    OnlineExaminedPrice = table.Column<int>(nullable: true),
                    UnOnlineExaminedPrice = table.Column<int>(nullable: true),
                    IsReceiveOrderSms = table.Column<int>(nullable: false),
                    IsReceiveCapitalSms = table.Column<int>(nullable: false),
                    IsReceiveAssetsSms = table.Column<int>(nullable: false),
                    IsReceivePowerSms = table.Column<int>(nullable: false),
                    PointsBillShield = table.Column<int>(nullable: true),
                    PriceFirstType = table.Column<int>(nullable: true),
                    AnnualExpireReminder = table.Column<int>(nullable: false),
                    CompulsorExpireReminder = table.Column<int>(nullable: false),
                    CommercialExpireReminder = table.Column<int>(nullable: false),
                    IsRevice = table.Column<int>(nullable: true),
                    Txyh = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    H5AutoRecharge = table.Column<bool>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    CxysellerAppUserId = table.Column<string>(nullable: true),
                    IsOpenKfoffer = table.Column<int>(nullable: true),
                    IsOpenBkfoffer = table.Column<int>(nullable: true),
                    WarnJf = table.Column<int>(nullable: true),
                    WarnYe = table.Column<int>(nullable: true),
                    WarnDx = table.Column<int>(nullable: true),
                    IsRedPoint = table.Column<int>(nullable: true),
                    WeiXinPayH5 = table.Column<bool>(nullable: false),
                    IsReceiveMakeSms = table.Column<int>(nullable: true),
                    WeChatSubscription = table.Column<string>(nullable: true),
                    H5remark = table.Column<string>(nullable: true),
                    BatchAskPriceMenu = table.Column<int>(nullable: false),
                    FineSvPlusPrice = table.Column<int>(nullable: false),
                    IsShenZhou = table.Column<int>(nullable: false),
                    IsInvoice = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    RateType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSysSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWalletFlows",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    FlowType = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AfterAmount = table.Column<decimal>(nullable: false),
                    CarNumber = table.Column<string>(nullable: true),
                    BillNo = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWalletFlows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserWallets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Balance = table.Column<decimal>(nullable: false),
                    OverdrftAmount = table.Column<decimal>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<string>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeleterUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebSiteConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteMemo = table.Column<string>(nullable: true),
                    QuickAmount = table.Column<decimal>(nullable: false),
                    VisibleCalculationExpression = table.Column<int>(nullable: false),
                    WebFixedProfit = table.Column<decimal>(nullable: false),
                    MasterAgentDefaultGivePoints = table.Column<int>(nullable: false),
                    FirstAgentDefaultGivePoints = table.Column<int>(nullable: false),
                    SecondAgentDefaultGivePoints = table.Column<int>(nullable: false),
                    GivePointsPerMonth = table.Column<int>(nullable: false),
                    DefaultJfPrice = table.Column<decimal>(nullable: false),
                    DefaultNotePrice = table.Column<decimal>(nullable: false),
                    ReceivableAmount = table.Column<decimal>(nullable: false),
                    ReceivableDate = table.Column<DateTime>(nullable: false),
                    ExpirationReminder = table.Column<string>(nullable: true),
                    GivePointsSurplusSameMonth = table.Column<int>(nullable: false),
                    SmsSendInterval = table.Column<int>(nullable: false),
                    AskPriceMailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSiteConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebSitePayConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PayForAnother = table.Column<bool>(nullable: false),
                    OrderGiveNum = table.Column<int>(nullable: false),
                    AutoOrderShunt = table.Column<int>(nullable: false),
                    IsBalancePayment = table.Column<int>(nullable: false),
                    IsWeChatPayment = table.Column<int>(nullable: false),
                    WxappId = table.Column<string>(nullable: true),
                    WxmchId = table.Column<string>(nullable: true),
                    Wxkey = table.Column<string>(nullable: true),
                    WxsubAppid = table.Column<string>(nullable: true),
                    WxsubMchid = table.Column<string>(nullable: true),
                    WxsubKey = table.Column<string>(nullable: true),
                    WeiXinGiro = table.Column<int>(nullable: false),
                    WeiXinCodeUrl = table.Column<string>(nullable: true),
                    IsUseSysWeiXinPay = table.Column<int>(nullable: false),
                    AlipayAppId = table.Column<string>(nullable: true),
                    AlipayPublicKey = table.Column<string>(nullable: true),
                    AlipayPrivateKey = table.Column<string>(nullable: true),
                    IsWftPayment = table.Column<int>(nullable: false),
                    WftMchId = table.Column<string>(nullable: true),
                    WftKey = table.Column<string>(nullable: true),
                    IsAlipayPayment = table.Column<int>(nullable: false),
                    AlipayGiro = table.Column<int>(nullable: false),
                    AlipaySellerEmail = table.Column<string>(nullable: true),
                    AlipayKey = table.Column<string>(nullable: true),
                    AlipayPartner = table.Column<string>(nullable: true),
                    AlipayCodeUrl = table.Column<string>(nullable: true),
                    IsUseSysAlipay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSitePayConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebSites",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 6, nullable: false),
                    WebSiteName = table.Column<string>(maxLength: 65, nullable: true),
                    WebSiteKey = table.Column<string>(nullable: true),
                    WebSiteType = table.Column<string>(nullable: true),
                    WebSiteMater = table.Column<string>(nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    ConnectionString = table.Column<string>(maxLength: 65, nullable: true),
                    CreationTime = table.Column<DateTime>(maxLength: 65, nullable: false),
                    CreatorUserId = table.Column<string>(maxLength: 65, nullable: true),
                    LastModifierUserId = table.Column<string>(maxLength: 65, nullable: true),
                    LastModificationTime = table.Column<DateTime>(maxLength: 65, nullable: true),
                    DeleterUserId = table.Column<string>(maxLength: 65, nullable: true),
                    DeletionTime = table.Column<DateTime>(maxLength: 65, nullable: true),
                    IsDeleted = table.Column<bool>(maxLength: 65, nullable: false),
                    WebSiteDomains = table.Column<string>(nullable: true),
                    WorkerName = table.Column<string>(nullable: true),
                    CustQq = table.Column<string>(nullable: true),
                    CustSerPhone = table.Column<string>(nullable: true),
                    WebSiteMemo = table.Column<string>(nullable: true),
                    DefaultCarNumberForShort = table.Column<string>(nullable: true),
                    ConcernArea = table.Column<string>(nullable: true),
                    Copyright = table.Column<string>(nullable: true),
                    SiteLoginImage = table.Column<string>(nullable: true),
                    H5ImgUrl = table.Column<string>(nullable: true),
                    H5ImgAddTime = table.Column<DateTime>(nullable: false),
                    IsRevice = table.Column<int>(nullable: false),
                    IsDownApp = table.Column<int>(nullable: false),
                    IsInvoice = table.Column<int>(nullable: false),
                    TaxRate = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchAskPriceViolationAgents");

            migrationBuilder.DropTable(
                name: "BatchCars");

            migrationBuilder.DropTable(
                name: "BatchInfos");

            migrationBuilder.DropTable(
                name: "CarViolationDivisions");

            migrationBuilder.DropTable(
                name: "DataSeeds");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SmsSendRecords");

            migrationBuilder.DropTable(
                name: "Tests");

            migrationBuilder.DropTable(
                name: "UserMarkupSettings");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserScoreFlows");

            migrationBuilder.DropTable(
                name: "UserScores");

            migrationBuilder.DropTable(
                name: "UserSysSettings");

            migrationBuilder.DropTable(
                name: "UserWalletFlows");

            migrationBuilder.DropTable(
                name: "UserWallets");

            migrationBuilder.DropTable(
                name: "WebSiteConfigs");

            migrationBuilder.DropTable(
                name: "WebSitePayConfigs");

            migrationBuilder.DropTable(
                name: "WebSites");
        }
    }
}
