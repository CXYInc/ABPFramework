using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class AddSmsRecordAndNotice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "SmsSendRecords");
        }
    }
}
