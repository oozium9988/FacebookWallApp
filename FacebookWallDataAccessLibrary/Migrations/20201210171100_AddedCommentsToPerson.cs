using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookWallDataAccessLibrary.Migrations
{
    public partial class AddedCommentsToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_PersonId",
                table: "Replies",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_People_PersonId",
                table: "Replies",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_People_PersonId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_PersonId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Replies");
        }
    }
}
