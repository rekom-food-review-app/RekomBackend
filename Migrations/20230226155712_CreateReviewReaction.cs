using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateReviewReaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Tag = table.Column<string>(type: "longtext", nullable: false),
                    Point = table.Column<uint>(type: "int unsigned", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reactions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReviewReactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ReviewId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RekomerId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ReactionId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Reactions_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "Reactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReviewReactions_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_ReactionId",
                table: "ReviewReactions",
                column: "ReactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_RekomerId",
                table: "ReviewReactions",
                column: "RekomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReactions_ReviewId",
                table: "ReviewReactions",
                column: "ReviewId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewReactions");

            migrationBuilder.DropTable(
                name: "Reactions");
        }
    }
}
