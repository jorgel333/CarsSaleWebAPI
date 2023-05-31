using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSalesWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class mudançanacamadadedominio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Average",
                table: "Cars",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Average",
                table: "Cars");
        }
    }
}
