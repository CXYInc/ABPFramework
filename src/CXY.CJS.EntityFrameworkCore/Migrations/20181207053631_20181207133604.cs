using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CXY.CJS.Migrations
{
    public partial class _20181207133604 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CjsTest",
                table: "CjsTest");

            migrationBuilder.EnsureSchema(
                name: "CXY");

            migrationBuilder.RenameTable(
                name: "CjsTest",
                newName: "Tests",
                newSchema: "CXY");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "CXY",
                table: "Tests",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                schema: "CXY",
                table: "Tests",
                maxLength: 65,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                schema: "CXY",
                table: "Tests",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WebSites",
                schema: "CXY",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WebSiteId = table.Column<string>(maxLength: 65, nullable: true),
                    WebSiteName = table.Column<string>(maxLength: 65, nullable: true),
                    ConnectionString = table.Column<string>(maxLength: 65, nullable: true),
                    CreationTime = table.Column<DateTime>(maxLength: 65, nullable: false),
                    CreatorUserId = table.Column<string>(maxLength: 65, nullable: true),
                    LastModifierUserId = table.Column<string>(maxLength: 65, nullable: true),
                    LastModificationTime = table.Column<DateTime>(maxLength: 65, nullable: true),
                    DeleterUserId = table.Column<string>(maxLength: 65, nullable: true),
                    DeletionTime = table.Column<DateTime>(maxLength: 65, nullable: true),
                    IsDeleted = table.Column<bool>(maxLength: 65, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebSites", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebSites",
                schema: "CXY");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                schema: "CXY",
                table: "Tests");

            migrationBuilder.RenameTable(
                name: "Tests",
                schema: "CXY",
                newName: "CjsTest");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CjsTest",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "CjsTest",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 65);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CjsTest",
                table: "CjsTest",
                column: "Id");
        }
    }
}
