﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSalesWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class correção : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserVotes",
                table: "Cars",
                newName: "Stock");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Cars",
                newName: "UserVotes");
        }
    }
}
