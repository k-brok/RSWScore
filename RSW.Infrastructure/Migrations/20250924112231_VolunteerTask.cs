using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerAssignment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    EditionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TaskId = table.Column<Guid>(type: "TEXT", nullable: false),
                    HasVOG = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerAssignment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolunteerAssignment_Editions_EditionId",
                        column: x => x.EditionId,
                        principalTable: "Editions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VolunteerAssignment_VolunteerTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "VolunteerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAssignment_EditionId",
                table: "VolunteerAssignment",
                column: "EditionId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAssignment_TaskId",
                table: "VolunteerAssignment",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAssignment_UserId",
                table: "VolunteerAssignment",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerAssignment");

            migrationBuilder.DropTable(
                name: "VolunteerTasks");
        }
    }
}
