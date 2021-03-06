﻿// <auto-generated />
using System;
using CXY.CJS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CXY.CJS.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(CJSDbContext))]
    partial class CJSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CXY.CJS.Model.BatchAskPriceViolationAgent", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("AgentPrice");

                    b.Property<string>("AgentUserId");

                    b.Property<string>("AgentUserName");

                    b.Property<string>("Archive");

                    b.Property<string>("BatchId");

                    b.Property<int?>("CanProcess");

                    b.Property<string>("CanprocessMsg");

                    b.Property<string>("CarId");

                    b.Property<string>("Category");

                    b.Property<string>("Code");

                    b.Property<decimal?>("CommonPoundage");

                    b.Property<decimal>("Count");

                    b.Property<DateTime>("CreationTime");

                    b.Property<int?>("DataStatus");

                    b.Property<string>("Ddbjid");

                    b.Property<int>("Degree");

                    b.Property<string>("FavorablePriceInfo");

                    b.Property<decimal?>("FirstPoundage");

                    b.Property<bool>("IsAskPrice");

                    b.Property<decimal?>("LastTimePoundage");

                    b.Property<decimal>("Latefine");

                    b.Property<string>("Location");

                    b.Property<string>("LocationId");

                    b.Property<string>("LocationName");

                    b.Property<decimal?>("LockPoundage");

                    b.Property<int?>("OrderByNo");

                    b.Property<string>("OrderJsonSelectId");

                    b.Property<decimal?>("Poundage");

                    b.Property<int?>("PriceFrom");

                    b.Property<string>("ProxyRemarks");

                    b.Property<string>("Reason");

                    b.Property<string>("Remarks");

                    b.Property<int>("State");

                    b.Property<int?>("Status");

                    b.Property<string>("Uniquecode");

                    b.Property<decimal?>("Vat");

                    b.Property<DateTime>("ViolationTime");

                    b.Property<int?>("ViolationType");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("BatchAskPriceViolationAgents");
                });

            modelBuilder.Entity("CXY.CJS.Model.BatchCar", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(32);

                    b.Property<string>("BatchId");

                    b.Property<string>("CarCode")
                        .HasMaxLength(32);

                    b.Property<string>("CarNumber")
                        .HasMaxLength(32);

                    b.Property<string>("CarType")
                        .HasMaxLength(32);

                    b.Property<string>("CarTypeName")
                        .HasMaxLength(32);

                    b.Property<DateTime>("CreationTime")
                        .HasMaxLength(32);

                    b.Property<string>("DriverLicense")
                        .HasMaxLength(32);

                    b.Property<string>("DriverName")
                        .HasMaxLength(32);

                    b.Property<string>("DriverPhone")
                        .HasMaxLength(32);

                    b.Property<string>("EngineNo")
                        .HasMaxLength(32);

                    b.Property<bool>("HaveLockRule")
                        .HasMaxLength(32);

                    b.Property<bool>("IsChoose")
                        .HasMaxLength(32);

                    b.Property<int>("IsLock")
                        .HasMaxLength(32);

                    b.Property<bool>("IsNeedSearch")
                        .HasMaxLength(32);

                    b.Property<bool>("PrivateCar")
                        .HasMaxLength(32);

                    b.Property<string>("ViolationMsg")
                        .HasMaxLength(32);

                    b.Property<string>("WebSiteId")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("BatchCars");
                });

            modelBuilder.Entity("CXY.CJS.Model.BatchInfo", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CarCount");

                    b.Property<DateTime?>("CompleteTime");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("Customer");

                    b.Property<string>("CustomerId");

                    b.Property<string>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<int>("HadPriceCount");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<string>("LastModifierUserId");

                    b.Property<int>("NeedPriceCount");

                    b.Property<string>("Proxy");

                    b.Property<string>("ProxyUserId");

                    b.Property<string>("Remark");

                    b.Property<int>("Status");

                    b.Property<int>("ViolationCount");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("BatchInfos");
                });

            modelBuilder.Entity("CXY.CJS.Model.CarViolationDivision", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CalculationExpression");

                    b.Property<decimal?>("Fc");

                    b.Property<string>("Fctype");

                    b.Property<string>("Fcuserid");

                    b.Property<decimal?>("Gdlr");

                    b.Property<int?>("ProfitType");

                    b.Property<string>("ViolationId");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("CarViolationDivisions");
                });

            modelBuilder.Entity("CXY.CJS.Model.DataSeed", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SeedIndex");

                    b.HasKey("Id");

                    b.ToTable("DataSeeds");
                });

            modelBuilder.Entity("CXY.CJS.Model.Menu", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsOut");

                    b.Property<bool>("IsParent");

                    b.Property<bool>("IsSys");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<int>("MenuLayer");

                    b.Property<int>("MenuLeval");

                    b.Property<string>("MenuName");

                    b.Property<string>("MenuUrl");

                    b.Property<string>("ParentId");

                    b.Property<string>("TargetFrame");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CXY.CJS.Model.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CXY.CJS.Model.RoleMenu", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("MenuId");

                    b.Property<string>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("RoleMenus");
                });

            modelBuilder.Entity("CXY.CJS.Model.SmsSendRecord", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BatchId");

                    b.Property<DateTime?>("BatchTime");

                    b.Property<string>("Content");

                    b.Property<string>("Operator");

                    b.Property<string>("Result");

                    b.Property<DateTime>("SendTime");

                    b.Property<int>("SmsType");

                    b.Property<bool>("Status");

                    b.Property<string>("Telephone");

                    b.Property<string>("ToUserId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("SmsSendRecords");
                });

            modelBuilder.Entity("CXY.CJS.Model.SysMessage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("FromUserId");

                    b.Property<string>("FromUserName");

                    b.Property<int?>("IsRead");

                    b.Property<string>("NoticeContent");

                    b.Property<string>("NoticeTitle");

                    b.Property<int>("NoticeType");

                    b.Property<string>("Operator");

                    b.Property<int>("Staus");

                    b.Property<string>("UserId");

                    b.Property<DateTime>("ValidityDate");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("Notices");
                });

            modelBuilder.Entity("CXY.CJS.Model.Test", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(65);

                    b.Property<string>("Name")
                        .HasMaxLength(512);

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserMarkupSetting", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<decimal?>("Gdlr");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<string>("LastModifierUserId");

                    b.Property<string>("ProfitJson");

                    b.Property<string>("Userid");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserMarkupSettings");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("UserId");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CXY.CJS.Model.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("CardNo");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("IsPaymentPwd");

                    b.Property<int?>("Isdelete");

                    b.Property<int?>("Ispanuse");

                    b.Property<DateTime?>("LastLoginTime");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<string>("LastModifierUserId");

                    b.Property<string>("LoginName");

                    b.Property<string>("Password");

                    b.Property<string>("PaymentPwd");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RealName");

                    b.Property<string>("RecommendUserName");

                    b.Property<string>("RecommendUserid");

                    b.Property<string>("Safepassword");

                    b.Property<string>("Shortname");

                    b.Property<int?>("UserCity");

                    b.Property<string>("UserName");

                    b.Property<int?>("UserProvince");

                    b.Property<int>("UserType");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserScore", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Drzsjf");

                    b.Property<DateTime>("Drzsrq");

                    b.Property<int?>("DrzssyJf");

                    b.Property<int?>("GivePointsPerMonth");

                    b.Property<int?>("GivePointsSameMonth");

                    b.Property<int?>("GivePointsSurplusSameMonth");

                    b.Property<decimal?>("JfPrice");

                    b.Property<int>("NoteNumber");

                    b.Property<decimal?>("NotePrice");

                    b.Property<int?>("RzsJf");

                    b.Property<int?>("WdJf");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserScores");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserScoreFlow", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Created");

                    b.Property<string>("Dh");

                    b.Property<int?>("FlowType");

                    b.Property<string>("Fromuserid");

                    b.Property<int?>("Fshjf");

                    b.Property<int?>("Fsjf");

                    b.Property<string>("Jfid");

                    b.Property<string>("Jftx");

                    b.Property<string>("Memo");

                    b.Property<string>("Operator");

                    b.Property<int>("PointsType");

                    b.Property<int?>("State");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserScoreFlows");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserSysSetting", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlertWzxxKfLimit");

                    b.Property<int?>("AlertWzxxLimit");

                    b.Property<int?>("AlertWzxxNewAdd");

                    b.Property<int>("AnnualExpireReminder");

                    b.Property<int>("AutoBillShield");

                    b.Property<DateTime>("BackQueryBeginTime");

                    b.Property<int?>("BackQueryDay");

                    b.Property<int>("BackQueryFailAgainInterval");

                    b.Property<bool>("BackQueryFailAgainMorning");

                    b.Property<int>("BackQueryFailAgainNumber");

                    b.Property<int>("BatchAskPriceMenu");

                    b.Property<int?>("CarCount");

                    b.Property<string>("City");

                    b.Property<int>("CommercialExpireReminder");

                    b.Property<int>("CompulsorExpireReminder");

                    b.Property<string>("CxysellerAppUserId");

                    b.Property<int?>("CzfwSl");

                    b.Property<string>("DdCalculationType");

                    b.Property<string>("Dh");

                    b.Property<int>("FineSvPlusPrice");

                    b.Property<bool>("H5AutoRecharge");

                    b.Property<string>("H5remark");

                    b.Property<int?>("HireCar");

                    b.Property<int?>("IsBackQuery");

                    b.Property<int?>("IsGetOrder");

                    b.Property<int>("IsInvoice");

                    b.Property<int?>("IsOffer");

                    b.Property<int?>("IsOpenBkfoffer");

                    b.Property<int>("IsOpenExaminedPrice");

                    b.Property<int?>("IsOpenKfoffer");

                    b.Property<int?>("IsOpenUsPrice");

                    b.Property<int>("IsReceiveAssetsSms");

                    b.Property<int>("IsReceiveCapitalSms");

                    b.Property<int?>("IsReceiveMakeSms");

                    b.Property<int>("IsReceiveOrderSms");

                    b.Property<int>("IsReceivePowerSms");

                    b.Property<int?>("IsRedPoint");

                    b.Property<int?>("IsRevice");

                    b.Property<int?>("IsSendSms");

                    b.Property<int>("IsShenZhou");

                    b.Property<int?>("IsktDd");

                    b.Property<int?>("IsktPldr");

                    b.Property<int?>("IsktSkd");

                    b.Property<int?>("IsktWeixin");

                    b.Property<int?>("OnlineExaminedPrice");

                    b.Property<string>("Operator");

                    b.Property<string>("ParentId");

                    b.Property<int?>("PointsBillShield");

                    b.Property<int>("PriceFirst");

                    b.Property<int?>("PriceFirstType");

                    b.Property<string>("Province");

                    b.Property<string>("Provinceid");

                    b.Property<string>("Qq");

                    b.Property<decimal>("Rate");

                    b.Property<int>("RateType");

                    b.Property<int?>("RoleId");

                    b.Property<string>("ScDlIp");

                    b.Property<string>("ScDlSj");

                    b.Property<string>("Swfzr");

                    b.Property<string>("TxKhx");

                    b.Property<string>("TxSj");

                    b.Property<string>("TxSkr");

                    b.Property<string>("TxZh");

                    b.Property<string>("Txyh");

                    b.Property<int?>("UnOnlineExaminedPrice");

                    b.Property<int?>("UsPriceFareFirst");

                    b.Property<int?>("UsPriceFareSecond");

                    b.Property<int?>("UserMenuType");

                    b.Property<string>("Userlayer");

                    b.Property<DateTime?>("ValidityDate");

                    b.Property<int?>("ViewDd");

                    b.Property<decimal?>("WapDifferenceAbsolute");

                    b.Property<int?>("WapDifferencePercentage");

                    b.Property<decimal?>("WapDifferenceThreshold");

                    b.Property<int?>("WarnDx");

                    b.Property<int?>("WarnJf");

                    b.Property<int?>("WarnYe");

                    b.Property<string>("WeChatSubscription");

                    b.Property<string>("WebSiteId");

                    b.Property<bool>("WeiXinPayH5");

                    b.Property<int?>("XjSl");

                    b.Property<string>("ZcDlIp");

                    b.Property<string>("ZcdlSj");

                    b.HasKey("Id");

                    b.ToTable("UserSysSettings");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserWallet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Balance");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<string>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<string>("LastModifierUserId");

                    b.Property<decimal>("OverdrftAmount");

                    b.Property<string>("UserId");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserWallets");
                });

            modelBuilder.Entity("CXY.CJS.Model.UserWalletFlow", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("AfterAmount");

                    b.Property<decimal>("Amount");

                    b.Property<string>("BillNo");

                    b.Property<string>("CarNumber");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("CreatorUserId");

                    b.Property<int>("FlowType");

                    b.Property<string>("Remark");

                    b.Property<string>("TypeName");

                    b.Property<string>("WebSiteId");

                    b.HasKey("Id");

                    b.ToTable("UserWalletFlows");
                });

            modelBuilder.Entity("CXY.CJS.Model.WebSite", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6);

                    b.Property<string>("ConcernArea");

                    b.Property<string>("ConnectionString")
                        .HasMaxLength(65);

                    b.Property<string>("Copyright");

                    b.Property<DateTime>("CreationTime")
                        .HasMaxLength(65);

                    b.Property<string>("CreatorUserId")
                        .HasMaxLength(65);

                    b.Property<string>("CustQq");

                    b.Property<string>("CustSerPhone");

                    b.Property<string>("DefaultCarNumberForShort");

                    b.Property<string>("DeleterUserId")
                        .HasMaxLength(65);

                    b.Property<DateTime?>("DeletionTime")
                        .HasMaxLength(65);

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("H5ImgAddTime");

                    b.Property<string>("H5ImgUrl");

                    b.Property<bool>("IsDeleted")
                        .HasMaxLength(65);

                    b.Property<int>("IsDownApp");

                    b.Property<int>("IsInvoice");

                    b.Property<int>("IsRevice");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasMaxLength(65);

                    b.Property<string>("LastModifierUserId")
                        .HasMaxLength(65);

                    b.Property<string>("SiteLoginImage");

                    b.Property<decimal>("TaxRate");

                    b.Property<string>("WebSiteDomains");

                    b.Property<string>("WebSiteKey");

                    b.Property<string>("WebSiteMater");

                    b.Property<string>("WebSiteMemo");

                    b.Property<string>("WebSiteName")
                        .HasMaxLength(65);

                    b.Property<string>("WebSiteType");

                    b.Property<string>("WorkerName");

                    b.HasKey("Id");

                    b.ToTable("WebSites");
                });

            modelBuilder.Entity("CXY.CJS.Model.WebSiteConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AskPriceMailAddress");

                    b.Property<decimal>("DefaultJfPrice");

                    b.Property<decimal>("DefaultNotePrice");

                    b.Property<string>("ExpirationReminder");

                    b.Property<int>("FirstAgentDefaultGivePoints");

                    b.Property<int>("GivePointsPerMonth");

                    b.Property<int>("GivePointsSurplusSameMonth");

                    b.Property<int>("MasterAgentDefaultGivePoints");

                    b.Property<decimal>("QuickAmount");

                    b.Property<decimal>("ReceivableAmount");

                    b.Property<DateTime>("ReceivableDate");

                    b.Property<int>("SecondAgentDefaultGivePoints");

                    b.Property<int>("SmsSendInterval");

                    b.Property<int>("VisibleCalculationExpression");

                    b.Property<decimal>("WebFixedProfit");

                    b.Property<string>("WebSiteMemo");

                    b.HasKey("Id");

                    b.ToTable("WebSiteConfigs");
                });

            modelBuilder.Entity("CXY.CJS.Model.WebSitePayConfig", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AlipayAppId");

                    b.Property<string>("AlipayCodeUrl");

                    b.Property<int>("AlipayGiro");

                    b.Property<string>("AlipayKey");

                    b.Property<string>("AlipayPartner");

                    b.Property<string>("AlipayPrivateKey");

                    b.Property<string>("AlipayPublicKey");

                    b.Property<string>("AlipaySellerEmail");

                    b.Property<int>("AutoOrderShunt");

                    b.Property<int>("IsAlipayPayment");

                    b.Property<int>("IsBalancePayment");

                    b.Property<int>("IsUseSysAlipay");

                    b.Property<int>("IsUseSysWeiXinPay");

                    b.Property<int>("IsWeChatPayment");

                    b.Property<int>("IsWftPayment");

                    b.Property<int>("OrderGiveNum");

                    b.Property<bool>("PayForAnother");

                    b.Property<string>("WeiXinCodeUrl");

                    b.Property<int>("WeiXinGiro");

                    b.Property<string>("WftKey");

                    b.Property<string>("WftMchId");

                    b.Property<string>("WxappId");

                    b.Property<string>("Wxkey");

                    b.Property<string>("WxmchId");

                    b.Property<string>("WxsubAppid");

                    b.Property<string>("WxsubKey");

                    b.Property<string>("WxsubMchid");

                    b.HasKey("Id");

                    b.ToTable("WebSitePayConfigs");
                });
#pragma warning restore 612, 618
        }
    }
}
