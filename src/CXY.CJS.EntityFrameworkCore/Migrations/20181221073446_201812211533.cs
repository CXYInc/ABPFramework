using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class _201812211533 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BatchId",
                table: "BatchCars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BatchId",
                table: "BatchAskPriceViolationAgents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "BatchCars");

            migrationBuilder.DropColumn(
                name: "BatchId",
                table: "BatchAskPriceViolationAgents");
        }
    }
}
