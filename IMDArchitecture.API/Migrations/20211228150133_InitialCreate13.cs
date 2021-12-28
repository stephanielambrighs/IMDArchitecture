using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDArchitecture.API.Migrations
{
    public partial class InitialCreate13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Users_UserId",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserEvents",
                newName: "UserRelationId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "UserEvents",
                newName: "EventRelationId");

            migrationBuilder.AddColumn<int>(
                name: "EventsEventId",
                table: "UserEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "UserEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventsEventId",
                table: "UserEvents",
                column: "EventsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_UsersUserId",
                table: "UserEvents",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Events_EventsEventId",
                table: "UserEvents",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Users_UsersUserId",
                table: "UserEvents",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventsEventId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Users_UsersUserId",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_EventsEventId",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_UsersUserId",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "EventsEventId",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "UserEvents");

            migrationBuilder.RenameColumn(
                name: "UserRelationId",
                table: "UserEvents",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "EventRelationId",
                table: "UserEvents",
                newName: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEvents_Users_UserId",
                table: "UserEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
