using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class AddBatchModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BatchAskPriceViolationAgents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    WebSiteId = table.Column<string>(nullable: true),
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
                    WebSiteId = table.Column<string>(maxLength: 6, nullable: true),
                    CarNumber = table.Column<string>(maxLength: 32, nullable: true),
                    CarCode = table.Column<string>(maxLength: 32, nullable: true),
                    EngineNo = table.Column<string>(maxLength: 32, nullable: true),
                    PrivateCar = table.Column<bool>(maxLength: 32, nullable: false),
                    CarType = table.Column<string>(maxLength: 32, nullable: true),
                    CarTypeName = table.Column<string>(maxLength: 32, nullable: true),
                    IsLock = table.Column<string>(maxLength: 32, nullable: true),
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BatchAskPriceViolationAgents");

            migrationBuilder.DropTable(
                name: "BatchCars");

            migrationBuilder.DropTable(
                name: "BatchInfos");
        }
    }
}
