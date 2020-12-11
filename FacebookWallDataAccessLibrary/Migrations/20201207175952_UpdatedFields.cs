using Microsoft.EntityFrameworkCore.Migrations;

namespace FacebookWallDataAccessLibrary.Migrations
{
    public partial class UpdatedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_People_AuthorId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_AuthorId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Replies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Replies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Replies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AuthorId",
                table: "Replies",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_People_AuthorId",
                table: "Replies",
                column: "AuthorId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Posts_PostId",
                table: "Replies",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
