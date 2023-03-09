using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRestaurantLocation3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Restaurants",
                type: "geography(Point, 0)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography(Point,4326)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "Location",
                table: "Restaurants",
                type: "geography(Point,4326)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geography(Point, 0)");
        }
    }
}
