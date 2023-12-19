using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace game_store_api.Migrations
{
    public partial class TokenMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Game",
                newName: "GameId");

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TokenValue = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Token_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Game",
                newName: "Id");
        }
    }
}
