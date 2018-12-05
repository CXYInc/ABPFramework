using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class _20181205162718 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CjsTest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CjsTest");
        }
    }
}
