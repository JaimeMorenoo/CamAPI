using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Camp.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToCampingSpot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "campingSpots",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "PricePerNight",
                table: "campingSpots",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "campingSpots");

            migrationBuilder.DropColumn(
                name: "PricePerNight",
                table: "campingSpots");
        }
    }
}
