using Microsoft.EntityFrameworkCore.Migrations;

namespace PManagement.DataProvider.Migrations
{
    public partial class adding_tst3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_PostCode",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address_PostCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "User");
        }
    }
}
