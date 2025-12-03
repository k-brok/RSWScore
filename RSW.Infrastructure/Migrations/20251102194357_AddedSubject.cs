using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "EmailConfigs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "EmailConfigs");
        }
    }
}
