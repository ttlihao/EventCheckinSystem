using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventCheckinSystem.Repo.Migrations
{
    /// <inheritdoc />
    public partial class FirstInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckoutTime",
                table: "GuestCheckins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckoutTime",
                table: "GuestCheckins",
                type: "datetime2",
                nullable: true);
        }
    }
}
