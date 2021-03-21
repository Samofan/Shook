using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class InitialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShookUser",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    ShooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShookUser", x => new { x.MemberId, x.ShooksId });
                    table.ForeignKey(
                        name: "FK_ShookUser_Shooks_ShooksId",
                        column: x => x.ShooksId,
                        principalTable: "Shooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShookUser_Users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShookUser_ShooksId",
                table: "ShookUser",
                column: "ShooksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShookUser");
        }
    }
}
