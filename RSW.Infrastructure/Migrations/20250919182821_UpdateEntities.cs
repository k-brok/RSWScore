using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SignupCodes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "JurySlots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SignupCodes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "JurySlots",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
