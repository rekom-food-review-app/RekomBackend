using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class CreateFavouriteRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouriteRestaurants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    RekomerId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RestaurantId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteRestaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavouriteRestaurants_Rekomers_RekomerId",
                        column: x => x.RekomerId,
                        principalTable: "Rekomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRestaurants_RekomerId",
                table: "FavouriteRestaurants",
                column: "RekomerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteRestaurants_RestaurantId",
                table: "FavouriteRestaurants",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteRestaurants");
        }
    }
}
