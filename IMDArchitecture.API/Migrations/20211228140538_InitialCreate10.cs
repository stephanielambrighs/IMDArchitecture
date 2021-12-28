using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IMDArchitecture.API.Migrations
{
    public partial class InitialCreate10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent");

            migrationBuilder.RenameTable(
                name: "UserEvent",
                newName: "UserEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Events",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "UserEventId",
                table: "UserEvents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents",
                column: "UserEventId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Events_EventId",
                table: "UserEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEvents_Users_UserId",
                table: "UserEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEvents",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents");

            migrationBuilder.DropIndex(
                name: "IX_UserEvents_UserId",
                table: "UserEvents");

            migrationBuilder.RenameTable(
                name: "UserEvents",
                newName: "UserEvent");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Users",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventId",
                table: "Events",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserEventId",
                table: "UserEvent",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEvent",
                table: "UserEvent",
                column: "UserEventId");

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => new { x.EventId, x.UserId });
                    table.ForeignKey(
                        name: "FK_EventUser_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UserId",
                table: "EventUser",
                column: "UserId");
        }
    }
}
