using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shooks_Users_UserId",
                table: "Shooks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Shooks",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Shooks_UserId",
                table: "Shooks",
                newName: "IX_Shooks_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shooks_Users_CreatorId",
                table: "Shooks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shooks_Users_CreatorId",
                table: "Shooks");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Shooks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Shooks_CreatorId",
                table: "Shooks",
                newName: "IX_Shooks_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shooks_Users_UserId",
                table: "Shooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
