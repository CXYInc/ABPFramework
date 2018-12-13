using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class RemoveExtraId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "WebSites");

            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "WebSitePayConfigs");

            migrationBuilder.DropColumn(
                name: "WebSiteId",
                table: "WebSiteConfigs");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "UserScores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSysSettings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WebSiteId",
                table: "WebSites",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSiteId",
                table: "WebSitePayConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebSiteId",
                table: "WebSiteConfigs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Userid",
                table: "UserScores",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserSysSettings",
                nullable: true);
        }
    }
}
