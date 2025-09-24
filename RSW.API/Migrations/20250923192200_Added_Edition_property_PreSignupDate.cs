using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class Added_Edition_property_PreSignupDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "PreSignupClose",
                table: "Editions",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreSignupClose",
                table: "Editions");
        }
    }
}
