using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookWallDataAccessLibrary.Migrations
{
    public partial class UpdatedNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "People");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "People",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "People");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "People",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "People",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
