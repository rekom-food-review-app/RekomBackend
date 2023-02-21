using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RekomBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToRekomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rekomers",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rekomers");
        }
    }
}
