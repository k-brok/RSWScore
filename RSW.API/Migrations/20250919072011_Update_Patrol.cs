using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class Update_Patrol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrols_SubGroups_SubGroupId",
                table: "Patrols");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubGroupId",
                table: "Patrols",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrols_SubGroups_SubGroupId",
                table: "Patrols",
                column: "SubGroupId",
                principalTable: "SubGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrols_SubGroups_SubGroupId",
                table: "Patrols");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubGroupId",
                table: "Patrols",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrols_SubGroups_SubGroupId",
                table: "Patrols",
                column: "SubGroupId",
                principalTable: "SubGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
