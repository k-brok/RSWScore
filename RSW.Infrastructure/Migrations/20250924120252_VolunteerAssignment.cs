using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSW.API.Migrations
{
    /// <inheritdoc />
    public partial class VolunteerAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignment_AspNetUsers_UserId",
                table: "VolunteerAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignment_Editions_EditionId",
                table: "VolunteerAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignment_VolunteerTasks_TaskId",
                table: "VolunteerAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerAssignment",
                table: "VolunteerAssignment");

            migrationBuilder.RenameTable(
                name: "VolunteerAssignment",
                newName: "VolunteerAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignment_UserId",
                table: "VolunteerAssignments",
                newName: "IX_VolunteerAssignments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignment_TaskId",
                table: "VolunteerAssignments",
                newName: "IX_VolunteerAssignments_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignment_EditionId",
                table: "VolunteerAssignments",
                newName: "IX_VolunteerAssignments_EditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerAssignments",
                table: "VolunteerAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignments_AspNetUsers_UserId",
                table: "VolunteerAssignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignments_Editions_EditionId",
                table: "VolunteerAssignments",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignments_VolunteerTasks_TaskId",
                table: "VolunteerAssignments",
                column: "TaskId",
                principalTable: "VolunteerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignments_AspNetUsers_UserId",
                table: "VolunteerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignments_Editions_EditionId",
                table: "VolunteerAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerAssignments_VolunteerTasks_TaskId",
                table: "VolunteerAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VolunteerAssignments",
                table: "VolunteerAssignments");

            migrationBuilder.RenameTable(
                name: "VolunteerAssignments",
                newName: "VolunteerAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignments_UserId",
                table: "VolunteerAssignment",
                newName: "IX_VolunteerAssignment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignments_TaskId",
                table: "VolunteerAssignment",
                newName: "IX_VolunteerAssignment_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_VolunteerAssignments_EditionId",
                table: "VolunteerAssignment",
                newName: "IX_VolunteerAssignment_EditionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VolunteerAssignment",
                table: "VolunteerAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignment_AspNetUsers_UserId",
                table: "VolunteerAssignment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignment_Editions_EditionId",
                table: "VolunteerAssignment",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerAssignment_VolunteerTasks_TaskId",
                table: "VolunteerAssignment",
                column: "TaskId",
                principalTable: "VolunteerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
