using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookWallDataAccessLibrary.Migrations
{
    public partial class UpdatedFields2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
