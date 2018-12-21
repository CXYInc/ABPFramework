using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wdye",
                table: "UserWallets");

            migrationBuilder.RenameColumn(
                name: "Overdrftamount",
                table: "UserWallets",
                newName: "OverdrftAmount");

            migrationBuilder.AlterColumn<decimal>(
                name: "OverdrftAmount",
                table: "UserWallets",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "UserWallets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserWallets",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsLock",
                table: "BatchCars",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarViolationDivisions");

            migrationBuilder.DropTable(
                name: "UserScoreFlows");

            migrationBuilder.DropTable(
                name: "UserWalletFlows");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "UserWallets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserWallets");

            migrationBuilder.RenameColumn(
                name: "OverdrftAmount",
                table: "UserWallets",
                newName: "Overdrftamount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Overdrftamount",
                table: "UserWallets",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "Wdye",
                table: "UserWallets",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IsLock",
                table: "BatchCars",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(int),
                oldMaxLength: 32);
        }
    }
}
