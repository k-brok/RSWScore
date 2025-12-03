using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class RenamedGrouptoUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Groups_groupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrols_Groups_GroupId",
                table: "Patrols");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "SignupCodes",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Patrols",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrols_GroupId",
                table: "Patrols",
                newName: "IX_Patrols_UnitId");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "AspNetUsers",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_groupId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UnitId");

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    AssociationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SignupCodes_UnitId",
                table: "SignupCodes",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_AssociationId",
                table: "Units",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrols_Units_UnitId",
                table: "Patrols",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SignupCodes_Units_UnitId",
                table: "SignupCodes",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrols_Units_UnitId",
                table: "Patrols");

            migrationBuilder.DropForeignKey(
                name: "FK_SignupCodes_Units_UnitId",
                table: "SignupCodes");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropIndex(
                name: "IX_SignupCodes_UnitId",
                table: "SignupCodes");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "SignupCodes",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Patrols",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrols_UnitId",
                table: "Patrols",
                newName: "IX_Patrols_GroupId");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AspNetUsers",
                newName: "groupId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UnitId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_groupId");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssociationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Associations_AssociationId",
                        column: x => x.AssociationId,
                        principalTable: "Associations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AssociationId",
                table: "Groups",
                column: "AssociationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Groups_groupId",
                table: "AspNetUsers",
                column: "groupId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrols_Groups_GroupId",
                table: "Patrols",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
