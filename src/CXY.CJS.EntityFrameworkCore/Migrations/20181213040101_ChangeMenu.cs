using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class ChangeMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menus");
        }
    }
}
