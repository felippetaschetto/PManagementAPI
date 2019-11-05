using Microsoft.EntityFrameworkCore.Migrations;

namespace PManagement.DataProvider.Migrations
{
    public partial class copany_config2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Company_CompanyId1",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_CompanyId1",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CompanyId1",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId1",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_CompanyId1",
                table: "User",
                column: "CompanyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Company_CompanyId1",
                table: "User",
                column: "CompanyId1",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
