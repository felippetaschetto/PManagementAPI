using Microsoft.EntityFrameworkCore.Migrations;

namespace PManagement.DataProvider.Migrations
{
    public partial class change_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenInfos",
                table: "TokenInfos");

            migrationBuilder.RenameTable(
                name: "TokenInfos",
                newName: "TokenInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenInfo",
                table: "TokenInfo",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TokenInfo",
                table: "TokenInfo");

            migrationBuilder.RenameTable(
                name: "TokenInfo",
                newName: "TokenInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TokenInfos",
                table: "TokenInfos",
                column: "Id");
        }
    }
}
