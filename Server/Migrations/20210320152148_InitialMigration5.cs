using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class InitialMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shooks_Users_CreatorId",
                table: "Shooks");

            migrationBuilder.DropForeignKey(
                name: "FK_Shooks_Users_WinnerId",
                table: "Shooks");

            migrationBuilder.DropIndex(
                name: "IX_Shooks_CreatorId",
                table: "Shooks");

            migrationBuilder.DropIndex(
                name: "IX_Shooks_WinnerId",
                table: "Shooks");

            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Shooks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Shooks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WinnerId",
                table: "Shooks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Shooks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Shooks_CreatorId",
                table: "Shooks",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Shooks_WinnerId",
                table: "Shooks",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shooks_Users_CreatorId",
                table: "Shooks",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shooks_Users_WinnerId",
                table: "Shooks",
                column: "WinnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
