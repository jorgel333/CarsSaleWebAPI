using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSalesWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class segundacorreção : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Office",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Office",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
