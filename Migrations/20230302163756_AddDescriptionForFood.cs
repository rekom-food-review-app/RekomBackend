using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionForFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Foods",
                type: "tinytext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Foods");
        }
    }
}
