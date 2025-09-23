using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEditionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JurySlots_Editions_EditionId",
                table: "JurySlots");

            migrationBuilder.AlterColumn<Guid>(
                name: "EditionId",
                table: "JurySlots",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_JurySlots_Editions_EditionId",
                table: "JurySlots",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JurySlots_Editions_EditionId",
                table: "JurySlots");

            migrationBuilder.AlterColumn<Guid>(
                name: "EditionId",
                table: "JurySlots",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JurySlots_Editions_EditionId",
                table: "JurySlots",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
